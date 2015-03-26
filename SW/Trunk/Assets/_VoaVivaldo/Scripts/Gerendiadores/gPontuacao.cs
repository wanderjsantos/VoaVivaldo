using UnityEngine;
using System.Collections;

public class gPontuacao : MonoBehaviour {

	public delegate void UpdatePontuacao( int quantidade );
	public static event UpdatePontuacao onUpdateEstrelas;
	public static event UpdatePontuacao onUpdatePontuacao;
	public static event UpdatePontuacao onUpdateCombo;

	public static gPontuacao s;
	
	public int tolerancia = 1;

	public int pontuacaoMinimaParaVitoria = 100;
	
	public 	int pontosPorNota = 100;
	public 	int aCadaXNotas = 5;

	public 	int	multiplicador = 1;
	public 	int maxMultiplicador = 3;
	
	
	public int pontuacao;
	
	public int	acertos = 0;
	public int	erros	= 0;
	
	public int estrelasGanhas = 1;	
	
	
			
	public int min1Estrela	= 15;
	public int min2Estrelas = 30;
	public int min3Estrelas = 50;
	
//	public int minParaDerrota = 40;
	
	float porcentagemAcertos = 0f;
//	float porcentagemErros = 0f;
				
	public	int notasAcertadasNaSequencia = 0;
	
	public bool pontuandoNotaLonga = false;
	public int	pontosNotaLonga = 10;
	public float adicionarACada = .03f;					
	
	
	public bool drawNumeros = false;
	

	void Awake()
	{
		s = this;
	}
	
	void OnEnable()
	{
		gGame.onInit += Resetar;
		gGame.onReset += Resetar;
	}
	
	void OnDisable()
	{
		gGame.onInit += Resetar;
		gGame.onReset += Resetar;
//		gComandosDeMusica.onStop
	}

	void Resetar ()
	{
		notasAcertadasNaSequencia = 0;
		multiplicador = 1;
		acertos = 0;
		erros = 0;
//		porcentagemErros = 0f;
		porcentagemAcertos = 0f;
		estrelasGanhas = 0;
		pontuacao = 0;
		
		if( onUpdateEstrelas != null ) onUpdateEstrelas( 0 );
		if( onUpdateCombo != null ) onUpdateCombo(0);
		if( onUpdatePontuacao != null ) onUpdatePontuacao( 0 );
	}

	public bool VerificarPontuacao( Nota n, Player p )
	{
		if( (int) n.mInfo.timbre >= p.mController.pista - tolerancia && (int) n.mInfo.timbre <= p.mController.pista + tolerancia )
		{
			return true;
		}	
		return false;
	}

	public bool VerificarPontuacaoLonga (Nota nota, Player player)
	{
		if( (int) nota.mInfo.timbre >= player.mController.pista -2 && (int) nota.mInfo.timbre <= player.mController.pista + 2 )
		{
			return true;
		}	

		return false;
	}
		
	public void Pontuar(Nota nota, Player p )
	{
		switch( nota.mInfo.tipo )
		{
			case TipoDeNota.NOTA:
			PontuarNotaComum(nota, p);
			break;
			case TipoDeNota.NOTA_X2:
			PontuarNotaLonga(nota, p);
			break;
			case TipoDeNota.PAUSA:
			break;
			default:
			break;
		}
		
		if( gAudio.s.trilhaBloqueada  ) gAudio.s.RecuperarAudio();
	
	}

	public void PontuarNotaComum (Nota nota, Player p)
	{
		if (nota.kill ) return;
		
		 nota.kill = true;
	
		int pontuacaoAtual = p.mInfo.pontuacao;
		
		p.mInfo.pontuacao += pontosPorNota * multiplicador ;
		
		notasAcertadasNaSequencia ++;
		
		if( notasAcertadasNaSequencia >= (aCadaXNotas * multiplicador) )
		{
			multiplicador += 1;
			multiplicador = Mathf.Clamp( multiplicador, 1, maxMultiplicador );
		}
		acertos++;
		
		pontuacao = p.mInfo.pontuacao;
		
		if( p.mInfo.pontuacao == pontuacaoAtual ) return;
		
		Atualizar( p.mInfo.pontuacao, 0, multiplicador );
		
		AtualizarErrosEAcertos();
		
	}

	public void PontuarNotaLonga (Nota nota, Player p)
	{
		if( nota.pontuando ) return;
		
		nota.pontuando = true;
//		lastNota = nota;
		notasAcertadasNaSequencia ++;
		
		if( notasAcertadasNaSequencia >= (aCadaXNotas * multiplicador) )
		{
			multiplicador += 1;
			multiplicador = Mathf.Clamp( multiplicador, 1, maxMultiplicador );
			
		}
		acertos++;
		pontuandoNotaLonga = true;		
		initPontuacao = Time.realtimeSinceStartup;
		
		Atualizar( p.mInfo.pontuacao, 0, multiplicador );
	}
	
	float initPontuacao;
//	Nota lastNota;
	public void Update()
	{
		if( !pontuandoNotaLonga ) return;
		
		
//		if( VerificarPontuacaoLonga( lastNota, gGame.s.player ) == false ) 
//		{
//			pontuandoNotaLonga = false;	
//			lastNota.kill = true;
//		}
		
		float currentTime = Time.realtimeSinceStartup - initPontuacao;
		
		if( currentTime > adicionarACada ) 
		{
			initPontuacao = Time.realtimeSinceStartup;
			SomarPontuacao( pontosNotaLonga );
		}
	}

	void SomarPontuacao (int pontos)
	{
		gGame.s.player.mInfo.pontuacao += pontos * multiplicador;
		
		Atualizar( gGame.s.player.mInfo.pontuacao, 0,0);
		
		AtualizarErrosEAcertos();
	}
	
	public void Atualizar( int pontos = 0, int estrelas = 0, int combo = 0 )
	{
		if( combo != 0 )
			if( onUpdateCombo != null ) onUpdateCombo( combo );
		if( estrelas != 0 )
			if( onUpdateEstrelas != null) onUpdateEstrelas(estrelas );
		if( pontos !=0 )
			if( onUpdatePontuacao != null) onUpdatePontuacao( pontos );
	}
	
	public void CancelarPontos()
	{
		notasAcertadasNaSequencia = 0;
		multiplicador = 1;
		erros ++;
		
		AtualizarErrosEAcertos();
		if( onUpdateCombo != null ) onUpdateCombo(0);
		
	}
	
	public void ForcarAtualizarPontosEstrelas()
	{	
//		if( onUpdateEstrelas != null ) onUpdateEstrelas( estrelasGanhas );
//		if( onUpdatePontuacao != null ) onUpdatePontuacao( gGame.s.player.mInfo.pontuacao);
		Atualizar( gGame.s.player.mInfo.pontuacao , estrelasGanhas, multiplicador );
	}
	
	void AtualizarErrosEAcertos ()
	{
		int eAntes = estrelasGanhas;
	
		if( acertos > 0 )
			porcentagemAcertos 	=(float)( (acertos * 100f) / gNotas.s.quantidadeDeNotasNaPista);
//		if( erros > 0 )
//			porcentagemErros	=(float)( (  erros * 100f ) / gNotas.s.quantidadeDeNotasNaPista);
		
//		Debug.Log("Porcentagem : " + porcentagemAcertos );
		
		if( porcentagemAcertos >= min1Estrela && porcentagemAcertos < min2Estrelas   ) estrelasGanhas = 1;
		else
		if( porcentagemAcertos >= min2Estrelas && porcentagemAcertos < min3Estrelas) estrelasGanhas = 2;
		else
		if( porcentagemAcertos > min2Estrelas && porcentagemAcertos >= min3Estrelas) estrelasGanhas = 3;
		
		if( eAntes == estrelasGanhas ) return;
		
//		if( onUpdateEstrelas != null ) onUpdateEstrelas( estrelasGanhas );
		
		Atualizar( 0,estrelasGanhas,0);
	}
	
	public void OnGUI()
	{
		if( !drawNumeros ) return;
		
		GUILayout.Box( "Notas na sequencia: " + notasAcertadasNaSequencia );	
		GUILayout.Box( "Multiplicador: " + multiplicador );	
	}
}
