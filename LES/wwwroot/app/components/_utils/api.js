app.factory('api', function ($rootScope, $http) {

	function tratarRetorno (response, cb) {
		if (!response.data.Messages || response.data.Messages.length === 0)
			return cb(response.data.Data);
		else 
			mostrarErros(response.data.Messages);		
	}

	function mostrarErros(messages) {
		let errors = [];

		messages.forEach(function (e) {
			errors.push(e.Text);
		});

		swal("Erro!", errors.join('<\br>'), "error");
	}

	return {
		post: function (url, data, cb) {
			$http.post('api/' + url, data)
				.then(response => tratarRetorno(response, cb),
				err => {
					swal("Erro!", err, "error");
				});
		},

		put: function (url, data, cb) {
			$http.put('api/' + url, data)
				.then(response => tratarRetorno(response, cb),
				err => {
					swal("Erro!", err, "error");
				});
		},

		delete: function (url, cb) {
			$http.delete('api/' + url)
				.then(response => tratarRetorno(response, cb),
				err => {
					swal("Erro!", err, "error");
				});
		},

		get: function (url, data, cb) {
			$http.get('api/' + url, {
				params: data
				//headers: { Authorization: 'Basic QWxhZGRpbjpvcGVuIHNlc2FtZQ==' }
			})
				.then(response => tratarRetorno(response, cb),
				err => {
					swal("Erro!", err, "error");
				});
		},

		filter: function (url, data, cb) {
			$http.post('api/' + url + '/Filter', data)
				.then(response => tratarRetorno(response, cb),
				err => {
					swal("Erro!", err, "error");
				});
		},

		upload: function (url, arquivo, cb) {
			$http({
				method: 'POST',
				url: 'api/' + url,
				data: {
					upload: arquivo
				},
				headers: {
					'Content-Type': 'multipart/form-data'
				},
				transformRequest: function (data, headersGetter) {
					var formData = new FormData();
					angular.forEach(data, function (value, key) {
						formData.append(key, value);
					});

					var headers = headersGetter();
					delete headers['Content-Type'];

					return formData;
				}
			})
				.then(response => {
					if (!response.data.Erro) {
						return cb(response.data)
					}
					else {
						$rootScope.mensagensErro = response.data.Erro.Messages;
					}
				},
					err => {
						$rootScope.mensagensErro = err;
					});
		}		
	};
});