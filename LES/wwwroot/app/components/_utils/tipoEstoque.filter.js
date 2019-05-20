app.filter("tipoEstoque", function () {
	return function (input) {

		if (!input)
			return input;

		switch (String.fromCharCode(input)) {
			case 'E':
				return "ENTRADA";
			case 'S':
				return "SAÍDA";
			case 'T':
				return "TRANSFERÊNCIA";
			default:
				return;
		}
	};
});