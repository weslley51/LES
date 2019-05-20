app.controller('AluguelController', function ($scope, $location, $state, $stateParams, api) {

	let vm = this;

	vm.livros = [];
	vm.alugueis = [];
	
	vm.index = function () {
		$state.go('aluguel.index');
	};

	vm.limpar = function () {
		vm.busca = {};
		vm.livros = [];
		vm.cliente = {};
		vm.bloquear = false;
		vm.bloquearCliente = false;
	};

	vm.limparLivro = function () {		
		vm.bloquearLivro = false;
		vm.livro = {};
	};

	vm.pesquisar = function (filtro) {
		let filtros = [];

		if (!filtro)
			return;

		filtros.push({ Property: 'Nome', Value: filtro.Titulo });
		filtros.push({ Property: 'Codigo', Value: filtro.CodigoBiblioteca });
		filtros.push({ Property: 'Editora', Value: !filtro.Editora ? '' : filtro.Editora.Id });
		filtros.push({ Property: 'Cliente', Value: filtro.Cliente });
		filtros.push({ Property: 'CPF', Value: filtro.CPF });
		filtros.push({ Property: 'Atrasado', Value: filtro.Atrasado });
		
		api.filter('Aluguel', filtros, response => vm.alugueis = response);
	};

	vm.excluir = function () {

		swal({
			title: "Deseja realmente excluir o aluguel do cliente " + entidade.NomeCompleto + " ?",
			text: "Livro: " + entidade.Livro.Titulo.Nome,
			type: "warning",
			showCancelButton: true,
			confirmButtonColor: "#dd6b55 !important",
			confirmButtonText: "Sim, confirmar!",
			closeOnConfirm: false
		})
			.then(function (willDelete) {
				if (!willDelete)
					return;

				api.delete('Aluguel' + entidade.Id, vm.index());
			});	
	};

	vm.salvar = function (livros, cliente) {
		let entidades = [];
		let dataAtual = new Date(Date.now());

		livros.forEach(function (livro) {

			let devolucao = new Date();
			devolucao.setDate(dataAtual.getDate() + livro.Titulo.PrazoDevolucao);

			let entidade = {
				Cliente: cliente,
				Livro: livro,
				Unidade: { Id: 1 },
				DataPrevistaDevolucao: devolucao.toISOString()
			};

			entidades.push(entidade);
		});

		api.post('Aluguel', entidades, vm.mensagemSucesso);
	};

	vm.mensagemSucesso = function () {
		swal("Aluguel(is) salvo(s) com sucesso !", '', "success");

		setTimeout(() => {
			vm.index();
		}, 3000);
	};

	vm.buscaCliente = function (cpf) {
		api.filter('Cliente', [{ Property: "CPF", Value: cpf }], result => {
			vm.cliente = result[0];

			if (result[0])
				vm.bloquearCliente = true;
		});
	};

	vm.buscaLivro = function (codigoBiblioteca) {
		api.filter('Livro', [{ Property: "Codigo", Value: codigoBiblioteca }], result => {
			vm.livro = result[0];

			if (result[0])
				vm.bloquearLivro = true;
		});	
	};

	vm.adicionarLivro = function (livro) {
		vm.livros.push(angular.copy(livro));
		vm.bloquearLivro = false;
	};

	vm.devolver = function (aluguel) {
		swal({
			title: "Devolução de Livros",
			text: "Deseja realmente confirmar a devolução do livro " + aluguel.Livro.CodigoBiblioteca + " ?",
			type: "warning",
			showCancelButton: true,
			confirmButtonColor: "#dd6b55 !important",
			confirmButtonText: "Sim, confirmar!",
			closeOnConfirm: false
		})
			.then(function (willConfirm) {
				if (!willConfirm)
					return;
				
				aluguel.DataDevolucao = new Date(Date.now()).toISOString();
				api.put('Aluguel', [aluguel], vm.mensagemSucesso);						
			});	
	};

	vm.init = function () {
		api.get('Editora', null, result => vm.Editoras = result);

		vm.bloquear = false;

		if ($location.absUrl().includes('visualizar'))
			vm.visualizar();
		else if ($location.absUrl().includes('atrasados'))
			vm.pesquisar({ Atrasado: true });
	};

	vm.init();
});
