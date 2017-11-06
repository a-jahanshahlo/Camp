var eventData = campsApp.factory('eventData', function ($http,$log) {
    return {
        getEvent:function (successcb) {
            $http({ method: 'GET', url: '/home/getevent' })
                .success(function(data, status, headers, config) {
                    successcb(data);
                })
                .error(function(data, status, headers, config) {
                    $log.warn(data, status, headers, config);
            });
        }

    };
});