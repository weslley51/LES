app
	.config(function ($urlRouterProvider, $httpProvider) {
		$urlRouterProvider.otherwise('/');
		$httpProvider.interceptors.push('loadingInterceptor');
	})

	.run(function ($transitions, $rootScope) {
		$transitions.onStart({}, transition => {
			$rootScope.mensagensErro = null;
		});
	});