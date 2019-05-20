app.controller('BasicoController', function ($rootScope, $state, api) {

	let vm = this;

	vm.entidade = {};
	vm.entidades = [];
	var ativos = true;

	vm.index = function () {
		$state.go($state.current, {}, { reload: true });
	};

	vm.editar = function () {
		vm.atualizar = true;
	};

	vm.excluir = function (entidade) {
		swal({
			title: "Deseja realmente inativar o(a) " + $rootScope.titulo + " " + entidade.Nome + " ?",
			type: "warning",
			showCancelButton: true,
			confirmButtonColor: "#dd6b55 !important",
			confirmButtonText: "Sim, confirmar!",
			closeOnConfirm: false
		})
			.then(function (willDelete) {
				if (!willDelete)
					return;

				api.delete(vm.nomeEntidade + '/' + entidade.Id, vm.index());
			});
	};

	vm.salvar = function (entidade) {
		var entidades = [vm.entidade];

		if (!vm.alteracao)
			api.post(vm.nomeEntidade, entidades, vm.mensagemSucesso);
		else
			api.put(vm.nomeEntidade, entidades, vm.mensagemSucesso);
	};

	vm.mensagemSucesso = function () {
		swal(vm.titulo + " salvo com sucesso !", '', "success");

		setTimeout(() => {
			vm.index();
		}, 3000);
	};

	vm.abrirModal = function (entidade, alterar) {

		vm.alteracao = alterar;
		vm.entidade = entidade;

		if (entidade.Ativo === false) {
			swal({
				title: "Deseja realmente reativar o(a) " + $rootScope.titulo + " " + entidade.Nome + " ?",
				type: "warning",
				showCancelButton: true,
				confirmButtonColor: "#dd6b55 !important",
				confirmButtonText: "Sim, confirmar!",
				closeOnConfirm: false
			})
				.then(function (willDelete) {
					if (!willDelete)
						return;

					vm.entidade.Ativo = true;
					vm.salvar(entidade);
				});
		}
		else {
			vm.titulo = !alterar ? "Novo" : "Detalhes";
			vm.atualizar = !alterar;			
			$("#modal-entidade").modal("show");
		}
	};

	vm.listarInativos = function () {
		ativos = !ativos;
		let filtro = { Property: 'Ativo', Value: ativos };
		api.filter(vm.nomeEntidade, [filtro], response => vm.entidades = response);
	};

	vm.init = function () {
		vm.administrador = true;
		vm.nomeEntidade = $rootScope.nomeEntidade;
		vm.tituloBox = $rootScope.tituloBox;
		api.get(vm.nomeEntidade, null, result => vm.entidades = result);
	};

	vm.init();
});
