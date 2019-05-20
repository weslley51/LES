app.filter("tipoMulta", function () {
	return function (input) {

		if (!input)
			return input;

		switch (String.fromCharCode(input)) {
			case 'A':
				return "ATRASO";
			case 'P':
				return "PAGAMENTO";
			default:
				return;
		}
	};
});