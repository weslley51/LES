app.controller('MultaController', function ($rootScope, $scope, $location, $state, $stateParams, api, $httpParamSerializer) {

	let vm = this;

	vm.multas = [];

	vm.index = function () {
		$state.go('multa.index');
	};

	vm.limpar = function () {
		vm.filtro = {};
	};

	vm.pesquisar = function (filtro) {

		let filtros = [];

		if (!filtro)
			return;

		filtros.push({ Property: 'Cliente', Value: !filtro.Cliente ? '' : filtro.Cliente.Id });
		api.filter('Multa', filtros, response => vm.multas = response);
	};

	vm.init = function () {	
		api.get('Cliente', null, result => vm.clientes = result);
		vm.pesquisar(vm.multa);
	};

	vm.init();
});
