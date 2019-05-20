app.controller('EstoqueController', function ($scope, $location, $state, $stateParams, api) {

	let vm = this;

	vm.lancamento = {};
	vm.lancamentos = [];
	vm.tiposMovimentacao = ['Entrada', 'Saida', 'Transferência'];
	
	vm.index = function () {
		$state.go('lancamento.index');
	};

	vm.limpar = function () {
		vm.filtro = {};
	};

	vm.editar = function () {
		vm.edit = true;
	};

	vm.visualizar = function () {
		api.get('Estoque', { Id: $stateParams.id }, result => {

			if (!result || result.length === 0)
				vm.index();

			vm.lancamento = result;
		});
	};
	
	vm.pesquisar = function (filtro) {
		let filtros = [];

		if (!filtro)
			return;

		filtros.push({ Property: 'Descricao', Value: filtro.Descricao });		
		filtros.push({ Property: 'Titulo', Value: !filtro.Titulo ? '' : filtro.Titulo.Id });
		filtros.push({ Property: 'TiposMovimentacao', Value: !filtro.TiposMovimentacao ? '' : filtro.TiposMovimentacao.substring(0, 1) });		
		filtros.push({ Property: 'Unidade', Value: !filtro.Unidade ? '' : filtro.Unidade.Id });		

		api.filter('Estoque', filtros, response => vm.lancamentos = response);
	};

	vm.excluir = function () {
		api.delete('Estoque/' + $stateParams.id, result => {
			$('#modal-exclusao').modal('hide');
			setTimeout(() => { vm.index(); }, 2000);
		});
	};

	vm.salvar = function (entidade) {
		var entidades = [entidade];

		if ($location.absUrl().includes('novo')) {
			api.post('Estoque', entidades, result => {
				$('#modal-confirmacao').modal('show');
				setTimeout(() => {
					$('#modal-confirmacao').modal('hide');
					vm.index();
				}, 2000);
			});
		}
		else {
			api.put('Estoque', entidades, result => {
				$('#modal-confirmacao').modal('show');
				setTimeout(() => {
					$('#modal-confirmacao').modal('hide');
					vm.index();
				}, 2000);
			});
		}
	};

	vm.init = function () {

		api.get('Titulo', null, result => vm.titulos = result);
		api.get('Unidade', null, result => vm.unidades = result);

		if ($location.absUrl().includes('visualizar')) {
			vm.visualizar();
			vm.edit = false;
		}
		else if ($location.absUrl().includes('novo'))
			vm.edit = true;
		else
			vm.pesquisar(null);
	};

	vm.init();
});
