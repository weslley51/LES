app.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {

	$urlRouterProvider.otherwise('/');
	$locationProvider.hashPrefix('');

	$stateProvider
		.state('home',
			{
				url: '/',
				templateUrl: 'app/components/aluguel/index.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Listagem de aluguéis';
				}
			})
		.state('cliente',
			{
				abstract: true,
				url: '/cliente',
				template: '<ui-view></ui-view>'
			})
		.state('cliente.index',
			{
				url: '/',
				templateUrl: 'app/components/cliente/index.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Listagem de Clientes';
				}
			})
		.state('cliente.novo',
			{
				url: '/novo',
				templateUrl: 'app/components/cliente/model.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Novo Cliente';
				}
			})
		.state('cliente.visualizar',
			{
				url: '/visualizar/:id',
				templateUrl: 'app/components/cliente/model.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Detalhes do Cliente';
				}
			})
		.state('relatorio',
			{
				abstract: true,
				url: '/relatorio',
				template: '<ui-view></ui-view>'
			})
		.state('relatorio.index',
			{
				url: '/',
				templateUrl: 'app/components/relatorio/index.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Listagem de Relatórios';
				}
		})
		.state('multa',
			{
				abstract: true,
				url: '/multa',
				template: '<ui-view></ui-view>'
			})
		.state('multa.index',
			{
				url: '/',
				templateUrl: 'app/components/multa/index.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Listagem de Multas';
				}
			})
		.state('usuario',
			{
				abstract: true,
				url: '/usuario',
				template: '<ui-view></ui-view>'
			})
		.state('usuario.index',
			{
				url: '/',
				templateUrl: 'app/components/usuario/index.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Listagem de Usuarios';
				}
			})
		.state('usuario.novo',
			{
				url: '/novo',
				templateUrl: 'app/components/usuario/model.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Novo Usuario';
				}
			})
		.state('usuario.visualizar',
			{
				url: '/visualizar/:id',
				templateUrl: 'app/components/usuario/model.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Detalhes do Usuario';
				}
			})
		.state('usuario.deslogar',
			{
				url: '/deslogar',
				templateUrl: 'app/components/aluguel/index.html',
				onEnter: function ($rootScope, api) {
					$rootScope.usuario = null;
					api.get('Usuario/Deslogar');
				}
			})
		.state('aluguel',
			{
				abstract: true,
				url: '/aluguel',
				template: '<ui-view></ui-view>'
			})
		.state('aluguel.index',
			{
				url: '/',
				templateUrl: 'app/components/aluguel/index.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Listagem de Aluguéis';
				}
			})
		.state('aluguel.novo',
			{
				url: '/novo',
				templateUrl: 'app/components/aluguel/model.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Novo Aluguel';
				}
		})
		.state('aluguel.atrasados',
			{
				url: '/atrasados',
				templateUrl: 'app/components/aluguel/index.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Listagem de Aluguéis atrasados';
				}
			})
		.state('aluguel.visualizar',
			{
				url: '/visualizar/:id',
				templateUrl: 'app/components/aluguel/model.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Detalhes do Aluguel';
				}
			})
		.state('livro',
			{
				abstract: true,
				url: '/livro',
				template: '<ui-view></ui-view>'
			})
		.state('livro.index',
			{
				url: '/',
				templateUrl: 'app/components/livro/index.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Listagem de Livros';
				}
			})
		.state('livro.novo',
			{
				url: '/novo',
				templateUrl: 'app/components/livro/model.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Novo Livro';
				}
			})
		.state('livro.visualizar',
			{
				url: '/visualizar/:id',
				templateUrl: 'app/components/livro/model.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Detalhes do Livro';
				}
		})
		.state('unidade',
			{
				abstract: true,
				url: '/unidade',
				template: '<ui-view></ui-view>'
			})
		.state('unidade.index',
			{
				url: '/',
				templateUrl: 'app/components/unidade/index.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Listagem de Unidades';
				}
			})
		.state('unidade.novo',
			{
				url: '/novo',
				templateUrl: 'app/components/unidade/model.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Nova Unidade';
				}
			})
		.state('unidade.visualizar',
			{
				url: '/visualizar/:id',
				templateUrl: 'app/components/unidade/model.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Detalhes do Unidade';
				}
			})
		.state('estoque',
			{
				abstract: true,
				url: '/estoque',
				template: '<ui-view></ui-view>'
			})
		.state('estoque.index',
			{
				url: '/',
				templateUrl: 'app/components/estoque/index.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Listagem de Estoque de livros';
				}
			})
		.state('estoque.novo',
			{
				url: '/novo',
				templateUrl: 'app/components/estoque/model.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Novo Lançamento de estoque';
				}
			})
		.state('estoque.visualizar',
			{
				url: '/visualizar/:id',
				templateUrl: 'app/components/estoque/model.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.titulo = 'Detalhes do lançamento';
				}
			})
		.state('autor',
			{
				abstract: true,
				url: '/autor',
				template: '<ui-view></ui-view>'
			})
		.state('autor.index',
			{
				url: '/',
				templateUrl: 'app/components/basico/index.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.nomeEntidade = 'autor';
					$rootScope.tituloBox = 'Listagem de Autores';
					$rootScope.titulo = 'Autor';
				}
			})
		.state('editora',
			{
				abstract: true,
				url: '/editora',
				template: '<ui-view></ui-view>'
			})
		.state('editora.index',
			{
				url: '/',
				templateUrl: 'app/components/basico/index.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.nomeEntidade = 'editora';
					$rootScope.tituloBox = 'Listagem de Editoras';
					$rootScope.titulo = 'Editora';
				}
			})
		
		.state('genero',
			{
				abstract: true,
				url: '/genero',
				template: '<ui-view></ui-view>'
			})
		.state('genero.index',
			{
				url: '/',
				templateUrl: 'app/components/basico/index.html',
				onEnter: function ($rootScope, api) {
					api.filter('Usuario', [{ Property: 'Logado', Value: true }], result => $rootScope.usuario = result[0]);
					api.filter('Aluguel', [{ Property: 'Atrasado', Value: true }], result => $rootScope.alugueisAtrasados = result.length);
					$rootScope.nomeEntidade = 'genero';
					$rootScope.tituloBox = 'Listagem de Gêneros';
					$rootScope.titulo = 'Gênero';
				}
			});
});
