app.directive('filter', function () {

	link = function (scope, element, attrs) {

		scope.hidden = true;

		scope.collapse = function () {
			scope.hidden = !scope.hidden;
		};
	};

	return {
		templateUrl: '/app/directives/filter/filter.html',
		replace: true,
		restrict: 'E',
		transclude: true,
		scope: {
			search: '&search',
			clear: '&clear'
		},
		link: link
	};
});