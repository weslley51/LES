app.filter("ellipsis", function () {
	return function (input, size, isEllipsis) {
		if (!input)
			return input;

		if (input.length <= size)
			return input;

		if (isEllipsis)
			return input.substring(0, size) + '...';

		var Value = input.split(' ');
		var NewValue = [];

		for (var i = 0; i < Value.length; i++) {

			//if (Value[i].length < 3)
			//	continue;

			if (NewValue.join(' ').length + Value[i].length + 1 <= size)
				NewValue.push((Value[i]));
			else
				break;
		}

		return NewValue.join(' ');
	}
});