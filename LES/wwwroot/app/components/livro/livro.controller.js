app.controller('LivroController', function ($location, $state, $stateParams, api) {

	let vm = this;

	vm.titulo = {};
	vm.titulos = [];

	vm.index = function () {
		$state.go('livro.index');
	};

	vm.limpar = function () {
		vm.busca = {};
	};

	vm.editar = function () {
		vm.atualizar = true;
	};

	vm.visualizar = function () {
		api.get('Titulo', { Id: $stateParams.id }, result => {

			if (!result || result.length === 0)
				vm.index();

			vm.titulo = result;

			vm.buscarLivros(vm.titulo.Id);
		});
	};

	vm.pesquisar = function (filtro) {
		let filtros = [];

		if (!filtro)
			return;

		filtros.push({ Property: 'Nome', Value: filtro.Nome });
		filtros.push({ Property: 'CodigoBiblioteca', Value: filtro.CodigoBiblioteca });
		filtros.push({ Property: 'Editora', Value: !filtro.Editora ? '' : filtro.Editora.Id });
		filtros.push({ Property: 'DataPublicacaoInicio', Value: filtro.DataPublicacaoInicio });
		filtros.push({ Property: 'DataPublicacaoFinal', Value: filtro.DataPublicacaoFinal });
		filtros.push({ Property: 'Genero', Value: !filtro.Genero ? '' : filtro.Genero.Id });
		filtros.push({ Property: 'Autor', Value: !filtro.Autor ? '' : filtro.Autor.Id });

		api.filter('Titulo', filtros, response => vm.titulos = response);
	};

	vm.excluir = function (entidade) {
		swal({
			title: "Deseja realmente inativar o titulo " + entidade.Nome + " ?",
			type: "warning",
			showCancelButton: true,
			confirmButtonColor: "#dd6b55 !important",
			confirmButtonText: "Sim, confirmar!",
			closeOnConfirm: false
		})
			.then(function (willDelete) {
				if (!willDelete)
					return;

				api.delete('Titulo' + entidade.Id, vm.index());
			});
	};

	vm.excluirLivro = function (entidade) {
		swal({
			title: "Deseja realmente inativar o livro ?",
			type: "warning",
			showCancelButton: true,
			confirmButtonColor: "#dd6b55 !important",
			confirmButtonText: "Sim, confirmar!",
			closeOnConfirm: false
		})
			.then(function (willDelete) {
				if (!willDelete)
					return;

				api.delete('Livro' + entidade.Id, vm.index());
			});
	};

	vm.salvar = function (entidade) {
		var entidades = [entidade];

		if ($location.absUrl().includes('novo'))
			api.post('Titulo', entidades, vm.mensagemSucesso);
		else
			api.put('Titulo', entidades, vm.mensagemSucesso);
	};

	vm.editarLivro = function () {
		vm.atualizarLivro = true;
	};

	vm.salvarLivro = function (entidade) {
		let entidades = [vm.livro];
		vm.mensagem = 'Livro alterado com sucesso!';
		api.put('Livro', entidades, vm.mensagemSucessoLivro);
	};

	vm.buscarLivros = function (id) {
		api.filter('Livro', [{ Property: 'Titulo', Value: id }], result => {

			if (!result || result.length === 0)
				return;

			vm.titulo.Livros = result;
		});
	};

	vm.salvarLancamento = function (entidade) {
		entidade.Titulo = vm.titulo;
		entidade.TipoMovimentacao = 'Entrada';
		entidade.Unidade = { Id: 1 };//usuario.Unidade;

		let entidades = [entidade];
		vm.mensagem = 'Livro(s) lançado(s) no estoque com sucesso!';
		api.post('Estoque', entidades, vm.mensagemSucessoLivro);
	};

	vm.mensagemSucesso = function () {
		swal("Titulo salvo com sucesso !", '', "success");

		setTimeout(() => {
			vm.index();
		}, 3000);
	};

	vm.mensagemSucessoLivro = function () {
		swal(vm.mensagem, '', "success");

		setTimeout(() => {
			vm.index();
		}, 3000);
	};

	vm.init = function () {

		api.get('Genero', null, result => vm.generos = result);
		api.get('Autor', null, result => vm.autores = result);
		api.get('Editora', null, result => vm.editoras = result);
		api.get('Unidade', null, result => vm.unidades = result);

		if ($location.absUrl().includes('visualizar')) {
			vm.visualizar();
			vm.liberarLivros = true;
			vm.atualizar = false;
		}
		else if ($location.absUrl().includes('novo'))
			vm.atualizar = true;
	};

	vm.abrirModal = function (entidade) {		
		vm.atualizarLivro = false;
		vm.alteracao = true;
		vm.livro = angular.copy(entidade);
		$("#modal-livro").modal("show");
	};

	vm.init();
});
