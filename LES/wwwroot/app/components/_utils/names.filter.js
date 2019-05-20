app.filter("names", function () {
	return function (input) {

		if (!input)
			return input;

		return input.map(x => x.Nome).join(', ');
	};
});