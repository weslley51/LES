app.controller('RelatorioController', function ($rootScope, $scope, $location, $state, $stateParams, api, $httpParamSerializer) {

	let vm = this;

	vm.relatorio = {};
	vm.relatorios = [];

	vm.index = function () {
		$state.go('relatorio.index');
	};

	vm.limpar = function () {
		vm.filtro = {};
	};

	vm.pesquisar = function (filtro) {

		let filtros = [];

		if (!filtro)
			return;

		filtros.push({ Property: 'Inicio', Value: filtro.Inicio });
		filtros.push({ Property: 'Fim', Value: filtro.Fim });

		api.post('Relatorio/GenerosPorMes', filtros, result => {
			vm.relatorio = result[0];

			$scope.series = Object.keys(vm.relatorio.Valores);
			$scope.labels = vm.relatorio.Titulos;
			let data = [];

			for (var i = 0; i < $scope.series.length; i++) {
				data.push(vm.relatorio.Valores[$scope.series[i]]);
			}

			$scope.data = data;	
		});
	};

	vm.init = function () {
		vm.nomeEntidade = "Relatório";//$rootScope.nomeEntidade;
		vm.tituloBox = "Gêneros mais alugados por mês";//$rootScope.tituloBox;		
	};

	vm.init();
});
