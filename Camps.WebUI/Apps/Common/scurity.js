campsApp.factory('httpInterceptor', function ($q, $rootScope, $location, $log) {
    function parseErrors(response) {
        var errors = [];
        for (var key in response.data.modelState) {
            for (var i = 0; i < response.data.modelState[key].length; i++) {
                errors.push(response.data.modelState[key][i]);
            }
        }
        return errors;
    }

    return {
        request: function (config) {
            return config || $q.when(config);
        },
        response: function (response) {
            return response || $q.when(response);
        },
        responseError: function (response) {
            switch (response.status) {
            case 401:
                $rootScope.$broadcast('unauthorized');
                $location.url("/account/login");
                break;
                case 400:
 
                    errorHintMessage(parseErrors(response));
                    break;
            default:
            }
 
            return $q.reject(response);
        }
    };
})
.config(function ($httpProvider) {
    $httpProvider.interceptors.push('httpInterceptor');
});