app.controller('LoginController', function ($rootScope, $scope, $location, $state, $stateParams, api, $httpParamSerializer) {

	let vm = this;

	vm.user = {};

	vm.logar = function (user) {
		api.post('Usuario/Logar', user, result => {
			if (!result)
				swal("Erro!", "Usuario ou senha incorretos!", "error");
			else {
				$rootScope.usuario = result;
				$location.url('/aluguel/atrasado');
			}
		});		
	};
});
