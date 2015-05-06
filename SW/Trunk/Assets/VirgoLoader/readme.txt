

Virgo Loader - Virgo Games Studio - 2013


Versão:
v 0.5


Descrição:
	Virgo Loader é uma ferramenta que gerencia o carregamento de cenas e bundles, locais ou via
web, permitindo aos desenvolvedores um modo rápido e prático de carregar conteúdo in-game com 
apenas um comando!


Releases:
	- Versão 0.5:
		* Identificação e diferenciação entre bundles e cenas;
		* Guardar bundles em disco ( não funciona em builds WebPlayer ) para serem reutilizados;
		* Carregar assincronamente cenas e permitir opções de serem aditivas a cena atual, ou 
	   descarregar cena atual;
	    * Uso de eventos para comunicação com outros componentes da aplicação;


Modo de usar:
	- Jogue o prefab "Loader" que se encontra na pasta "VirgoLoader/Resources" em sua Scene View;
	- Preencha os campos "Path/Url" com o nome da cena ( certificar-se de que a cena esta inclusa 
no Build Settings ) ou a url do AssetBundle ( com a extensão "unity3d" );
	- Quando você terminar o preenchimento, configure as opções que serão "highlighted";
	- Em Play Mode, use nos seus scripts Loader.Load() para disparar o carregamento dos arquivos
em "Path/Url";
	- Use Loader.Percent para acessar o progresso atual do carregamento;
	- Eventos: utilize os eventos LoadBegin, LoadComplete e LoadError para fazer seu game agir de 
acordo com o estado do carregamento.
	

Erros Conhecidos:
	- Até a versão 0.5, o Loader apresenta instabilidade se tentar ser usado no Edit Mode;
	- Cenas atualmente dão problema ao serem carregadas aditivamente quando nao estao em "Play";
	- Algumas vezes, o Unity reclama de um NullReference da instancia do Loader quando nao se esta
em Play Mode. Para resolver, apenas deletar o Loader em cena e arrastar o prefab novamente.
	- Percentual nao mostra o progresso durante o Edit Mode;
 
Contato:
 	support@virgogames.com
 