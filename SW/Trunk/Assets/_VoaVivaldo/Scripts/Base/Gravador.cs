using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gravador : MonoBehaviour 
{
	public bool				gravando = false;
	public List<NotaInfo> 	notas;

	public float			tempo;
	public float 			cTime;
	public float 			iTime;

	public List<Tecla>		teclado;

	public void Gravar()
	{
//		gravando 	= true;
//		iTime 		= Time.realtimeSinceStartup;
//		notas 		= new List<NotaInfo> ();

//		if( gMusica.s.musicaAtual == null )
//			gMusica.s.NovaMusica ("Base1", "Instrumento1");
	}

	public void Parar()
	{
		gravando = false;
	}
		
	public void SalvarGravacao()
	{
		Parar ();

//		gSave.s.Salvar( PrepararClasseParaGravar () );
	}

	
	public void Update()
	{

		if (gravando == false)
						return;

		cTime = Time.realtimeSinceStartup;
		tempo = cTime - iTime;

		foreach (Tecla t in teclado) {
			ProcessarNota(t);		
		
		}

		if (Input.GetKeyUp (KeyCode.P))
						SalvarGravacao ();

	}

	public NotaInfo NovaNota( Tecla t )
	{
		NotaInfo novaNota 	= new NotaInfo ();
//		novaNota.duracao 	= t.duracao;
//		novaNota.noTempo 	= t.inicio;
		novaNota.timbre 	= t.nota;
		novaNota.compasso 	= t.compasso;
		novaNota.batida 	= t.batida;

		return 				novaNota;
	}

	public void ProcessarNota( Tecla tecla )
	{
		if (Input.GetKeyDown (tecla.key)) 
						tecla.inicio = tempo;

		if (Input.GetKeyUp (tecla.key)) 
		{
			tecla.fim 		= tempo;
			tecla.duracao 	= tecla.fim - tecla.inicio;
			tecla.compasso 	= gRitmo.s.compassoAtual;
			tecla.batida	= gRitmo.s.contagem;
			notas.Add		( NovaNota(tecla) );
		}
	}


}
