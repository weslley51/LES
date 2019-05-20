app.filter("contactType", function () {
	return function (input) {
		return input ? 'Email' : 'Telefone';
	}
});