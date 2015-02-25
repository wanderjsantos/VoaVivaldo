using UnityEngine;
using System.Collections;

public class gPontuacao : MonoBehaviour {

	public static gPontuacao s;

	public 	int pontosPorNota = 100;
	public 	int aCadaXNotas = 5;

	public 	int	multiplicador = 1;
	public 	int maxMultiplicador = 3;
	
	public int	acertos = 0;
	public int	erros	= 0;
	
	public int estrelasGanhas = 1;	
		
	public int min2Estrelas = 30;
	public int min3Estrelas = 50;
	
//	public int minParaDerrota = 40;
	
	float porcentagemAcertos = 0f;
	float porcentagemErros = 0f;
				
	public	int notasAcertadasNaSequencia = 0;

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
	}

	void Resetar ()
	{
		notasAcertadasNaSequencia = 0;
		multiplicador = 1;
		acertos = 0;
		erros = 0;
		porcentagemErros = 0f;
		porcentagemAcertos = 0f;
		estrelasGanhas = 1;
	}

	public bool VerificarPontuacao( Nota n, Player p )
	{
		if( (int) n.mInfo.timbre == p.mController.pista )
		{
			return true;
		}	
		return false;
	}
		
	public void Pontuar( Player p )
	{
		p.mInfo.pontuacao += pontosPorNota * multiplicador ;
		
		notasAcertadasNaSequencia ++;
		
		if( notasAcertadasNaSequencia >= (aCadaXNotas * multiplicador) )
		{
			multiplicador = Mathf.Clamp( multiplicador++, 1, maxMultiplicador );
		}
		acertos++;
		
		AtualizarErrosEAcertos();
	}
	
	public void CancelarPontos()
	{
		notasAcertadasNaSequencia = 0;
		multiplicador = 1;
		erros ++;
		
		AtualizarErrosEAcertos();
	}
	
	void AtualizarErrosEAcertos ()
	{
		if( acertos > 0 )
			porcentagemAcertos 	=(float)( (acertos * 100f) / gNotas.s.quantidadeDeNotasNaPista);
		if( erros > 0 )
			porcentagemErros	=(float)( (  erros * 100f ) / gNotas.s.quantidadeDeNotasNaPista);
			
		if( porcentagemAcertos >= min2Estrelas && porcentagemAcertos < min3Estrelas) estrelasGanhas = 2;
		else
		if( porcentagemAcertos > min2Estrelas && porcentagemAcertos >= min3Estrelas) estrelasGanhas = 3;
		
	}
}
