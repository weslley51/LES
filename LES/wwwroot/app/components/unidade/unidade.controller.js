app.controller('UnidadeController', function ($scope, $location, $state, $stateParams, api, cep) {

	let vm = this;

	vm.unidade = {};
	vm.unidades = [];

	vm.index = function () {
		$state.go('unidade.index');
	};

	vm.limpar = function () {
		vm.busca = {};
	};

	vm.editar = function () {
		vm.atualizar = true;
	};

	vm.visualizar = function () {
		api.get('Unidade', { Id: $stateParams.id }, result => {

			if (!result || result.length === 0)
				vm.index();

			vm.unidade = result;
		});
	};

	vm.pesquisar = function (filtro) {
		let filtros = [];

		if (!filtro)
			return;

		filtros.push({ Property: 'Nome', Value: filtro.Nome });
		filtros.push({ Property: 'CPF', Value: filtro.CPF });
		filtros.push({ Property: 'UF', Value: !filtro.Estado ? '' : filtro.Estado.Id });

		api.filter('Unidade', filtros, response => vm.unidades = response);
	};

	vm.excluir = function (entidade) {
		swal({
			title: "Deseja realmente inativar o unidade " + entidade.NomeComnpleto + " ?",
			type: "warning",
			showCancelButton: true,
			confirmButtonColor: "#dd6b55 !important",
			confirmButtonText: "Sim, confirmar!",
			closeOnConfirm: false
		})
			.then(function (willDelete) {
				if (!willDelete)
					return;

				api.delete('Unidade' + entidade.Id, vm.index());
			});
	};

	vm.salvar = function (entidade) {
		var entidades = [entidade];

		if ($location.absUrl().includes('novo'))
			api.post('Unidade', entidades, vm.mensagemSucesso);
		else 
			api.put('Unidade', entidades, vm.mensagemSucesso);			
	};
	
	vm.mensagemSucesso = function () {
		swal("Unidade salva com sucesso !", '', "success");

		setTimeout(() => {
			vm.index();
		}, 3000);
	};

	vm.consultaCEP = function (valor) {
		if (valor.length >= 8) {
			cep.buscar(valor, function (response) {
				let endereco = response.data;
				vm.unidade.Endereco.Logradouro = endereco.logradouro;
				vm.unidade.Endereco.Bairro = endereco.bairro;
				vm.unidade.Endereco.Municipio = endereco.localidade;
				vm.unidade.Endereco.Complemento = endereco.complemento;
				vm.unidade.Endereco.Estado = vm.UFs.find(x => x.UF === endereco.uf);
			});
		}
	};

	vm.init = function () {

		api.get('Estado', null, result => vm.UFs = result);

		if ($location.absUrl().includes('visualizar')) {
			vm.visualizar();
			vm.atualizar = false;
		}
		else if ($location.absUrl().includes('novo'))
			vm.atualizar = true;
		else
			vm.pesquisar(vm.unidade);
	};

	vm.init();
});
