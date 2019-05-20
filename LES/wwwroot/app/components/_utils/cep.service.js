app.factory('cep', function ($http) {

	return {
		buscar: function (cep, cb) {
			$http.get('https://viacep.com.br/ws/' + cep + '/json/')
				.then(res => {
					cb(res);
				}, err => { console.log(err) });
		}
	}
});
