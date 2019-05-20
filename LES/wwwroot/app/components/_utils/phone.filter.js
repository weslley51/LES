app.filter("phone", function () {
	return function (input, isPhone) {
		if (!isPhone || !input)
			return input;

		if (input.length === 11)
			return "(" + input.substring(0, 2) + ") " + input.substring(2, 7) + '-' + input.substring(7);
		else
			return "(" + input.substring(0, 2) + ") " + input.substring(2, 6) + '-' + input.substring(6);
	}
});