app.directive("modal", function () {
	return {
		templateUrl: '/app/directives/modal/modal.html',
		replace: true,
		restrict: "E",
		transclude: true,
		scope: {
			entity: "=entity",
			title: "=title",
			modalId: "@modalId",
			size: "@size",
			showFooter: "@showFooter",
			modalForm: "=modalForm",
			save: "&save",
			update: "=update",
			edit: "&edit"
		}
	};
});