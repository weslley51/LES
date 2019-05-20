app.factory('loadingInterceptor', function ($q, $rootScope, $timeout) {

	var reqCount = 0;

	return {
		request: function (config) {

			$timeout(function () {
				if (reqCount > 0) {
					$rootScope.loading = true;
				}
			}, 200);

			reqCount++;

			return config;
		},

		requestError: function (rejection) {
			reqCount = 0;
			$rootScope.loading = false;
			return $q.reject(rejection);
		},

		response: function (response) {

			reqCount--;

			if (reqCount <= 0) {
				$rootScope.loading = false;
			}

			return response;
		},

		responseError: function (rejection) {
			reqCount = 0;
			$rootScope.loading = false;
			return $q.reject(rejection);
		}
	};
});