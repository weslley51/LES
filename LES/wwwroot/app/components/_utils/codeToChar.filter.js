app.filter("codeToChar", function () {
	return function (input) {

		if (!input)
			return input;

		return String.fromCharCode(input);
	};
});