app.controller('ClienteController', function ($scope, $location, $state, $stateParams, api, cep) {

	let vm = this;

	vm.cliente = {};
	vm.clientes = [];

	vm.index = function () {
		$state.go('cliente.index');
	};

	vm.limpar = function () {
		vm.busca = {};
	};

	vm.editar = function () {
		vm.atualizar = true;
	};

	vm.visualizar = function () {
		api.get('Cliente', { Id: $stateParams.id }, result => {

			if (!result || result.length === 0)
				vm.index();

			if (result.Saldo) {
				vm.Saldo = result.Saldo;
				result.Saldo = result.Saldo.toFixed(2).replace(',', '').replace('.', ',');
			}

			vm.cliente = result;
		});
	};

	vm.pesquisar = function (filtro) {
		let filtros = [];

		if (!filtro)
			return;

		filtros.push({ Property: 'Ativo', Value: true });
		filtros.push({ Property: 'Nome', Value: filtro.Nome });
		filtros.push({ Property: 'CPF', Value: filtro.CPF });
		filtros.push({ Property: 'UF', Value: !filtro.Estado ? '' : filtro.Estado.Id });

		api.filter('Cliente', filtros, response => vm.clientes = response);
	};

	vm.excluir = function (entidade) {
		swal({
			title: "Deseja realmente inativar o cliente " + entidade.NomeCompleto + " ?",
			type: "warning",
			showCancelButton: true,
			confirmButtonColor: "#dd6b55 !important",
			confirmButtonText: "Sim, confirmar!",
			closeOnConfirm: false
		})
			.then(function (willDelete) {
				if (!willDelete)
					return;

				api.delete('Cliente/' + entidade.Id, vm.index());
			});
	};

	vm.reativar = function (entidade) {
		if (entidade.Ativo === false) {
			swal({
				title: "Deseja realmente reativar o(a) cliente ?",
				type: "warning",
				showCancelButton: true,
				confirmButtonColor: "#dd6b55 !important",
				confirmButtonText: "Sim, confirmar!",
				closeOnConfirm: false
			})
				.then(function (willDelete) {
					if (!willDelete)
						return;

					vm.cliente.Ativo = true;
					vm.salvar(vm.cliente);
				});
		}
	};

	vm.salvar = function (entidade, cb) {
		var entidades = [entidade];

		let func = cb ? cb : vm.mensagemSucesso;

		if ($location.absUrl().includes('novo'))
			api.post('Cliente', entidades, func);
		else 
			api.put('Cliente', entidades, func);			
	};
	
	vm.mensagemSucesso = function (mensagem) {
		swal(mensagem ? mensagem : "Cliente salvo com sucesso !", '', "success");

		setTimeout(() => {
			vm.index();
		}, 3000);
	};

	vm.mensagemSucessoContato = function () {
		swal("Contato salvo com sucesso !", '', "success");

		setTimeout(() => {
			$location.reload();
		}, 3000);
	};

	vm.consultaCEP = function (valor) {
		if (valor.length >= 8) {
			cep.buscar(valor, function (response) {
				let endereco = response.data;
				vm.cliente.Endereco.Logradouro = endereco.logradouro;
				vm.cliente.Endereco.Bairro = endereco.bairro;
				vm.cliente.Endereco.Municipio = endereco.localidade;
				vm.cliente.Endereco.Complemento = endereco.complemento;
				vm.cliente.Endereco.Estado = vm.UFs.find(x => x.UF === endereco.uf);
			});
		}
	};
	
	vm.abrirModal = function (contato, alterar) {
		vm.titulo = !alterar ? "Novo" : "Detalhes";
		vm.atualizarContato = !alterar;
		vm.alteracao = alterar;
		vm.contato = contato;
		$("#modal-contato").modal("show");
	};

	vm.editarContato = function () {
		vm.atualizarContato = true;
	};

	vm.salvarContato = function (entidade) {
		let regex = RegExp("[^0-9]");

		vm.contato.Email = regex.test(vm.contato.Valor);

		let contatos = [vm.contato];

		if (!vm.alteracao) 
			api.post('Cliente/AdicionarContato/' + $stateParams.id, contatos, vm.mensagemSucessoContato);
		else
			api.put('Contato', contatos, vm.mensagemSucessoContato);
	};

	vm.lancarMulta = function () {
		api.get('Cliente', { Id: $stateParams.id }, result => {

			if (!result || result.length === 0)
				vm.index();

			vm.clienteAtualizado = result;
			vm.clienteAtualizado.Saldo = vm.clienteAtualizado.Saldo - parseFloat(vm.Valor.replace(',', '.'));
			vm.salvar(vm.clienteAtualizado, () => { vm.mensagemSucesso('Pagamento salvo com sucesso!'); });
		});	
	};

	vm.listarInativos = function (filtro) {
		let filtros = [];

		if (filtro) {
			filtros.push({ Property: 'Nome', Value: filtro.Nome });
			filtros.push({ Property: 'CPF', Value: filtro.CPF });
			filtros.push({ Property: 'UF', Value: !filtro.Estado ? '' : filtro.Estado.Id });
		}

		filtros.push({ Property: 'Ativo', Value: false });
		api.filter('Cliente', filtros, response => vm.clientes = response);
	};

	vm.init = function () {

		api.get('Estado', null, result => vm.UFs = result);
		api.filter('Multa', [{ Property: 'Cliente', Value: $stateParams.id }], response => vm.multas = response);

		if ($location.absUrl().includes('visualizar')) {
			vm.visualizar();
			vm.liberarContatos = true;
			vm.atualizar = false;
		}
		else if ($location.absUrl().includes('novo'))
			vm.atualizar = true;
		else
			vm.pesquisar(vm.cliente);
	};

	vm.init();
});
