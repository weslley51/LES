app.controller('UsuarioController', function ($scope, $location, $state, $stateParams, api) {

	let vm = this;

	vm.usuario = {};
	vm.usuarios = [];

	vm.index = function () {
		$state.go('usuario.index');
	};

	vm.limpar = function () {
		vm.busca = {};
	};

	vm.editar = function () {
		return;
	};

	vm.visualizar = function () {
		api.get('Usuario', { Id: $stateParams.id }, result => {

			if (!result || result.length === 0)
				vm.index();

			vm.usuario = result;
		});
	};

	vm.pesquisar = function (filtro) {
		let filtros = [];

		if (!filtro)
			return;

		filtros.push({ Property: 'Nome', Value: filtro.Nome });
		filtros.push({ Property: 'Login', Value: filtro.Login });
		filtros.push({ Property: 'Perfil', Value: filtro.Perfil });

		api.filter('Usuario', filtros, response => vm.usuarios = response);
	};

	vm.excluir = function (entidade) {
		swal({
			title: "Deseja realmente inativar o usuário: " + entidade.Nome + " ?",
			type: "warning",
			showCancelButton: true,
			confirmButtonColor: "#dd6b55 !important",
			confirmButtonText: "Sim, confirmar!",
			closeOnConfirm: false
		})
			.then(function (willDelete) {
				if (!willDelete)
					return;

				api.delete('Usuario' + entidade.Id, vm.index());
			});
	};

	vm.salvar = function (entidade) {
		var entidades = [entidade];
		api.post('Usuario', entidades, vm.mensagemSucesso);
	};

	vm.mensagemSucesso = function () {
		swal("Usuário salvo com sucesso !", '', "success");

		setTimeout(() => {
			vm.index();
		}, 3000);
	};

	vm.resetarSenha = function (usuario) {
		usuario.Senha = null;
		api.put('Usuario', [usuario], (result) => swal("Senha redefinida com sucesso !", '', "success"));
	};

	vm.init = function () {

		api.get('Perfil', null, result => vm.Perfis = result);

		if ($location.absUrl().includes('visualizar')) {
			vm.visualizar();
			vm.edit = false;
		}
		else if ($location.absUrl().includes('novo'))
			vm.edit = true;
		else
			vm.pesquisar(vm.usuario);
	};

	vm.init();
});
