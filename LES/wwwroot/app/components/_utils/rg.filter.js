app.filter("rg", function () {
	return function (input) {

		if (!input)
			return input;

		return input.substring(0, 2) + '.' + input.substring(2, 5) + '.' + input.substring(5, 7) + '-' + input.substring(7);
	};
});