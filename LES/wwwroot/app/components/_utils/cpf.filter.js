app.filter("cpf", function () {
	return function (input) {

		if (!input)
			return input;

		return input.substring(0, 3) + '.' + input.substring(3, 6) + '.' + input.substring(6, 9) + '-' + input.substring(9);
	};
});