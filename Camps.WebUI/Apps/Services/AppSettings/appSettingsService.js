campsApp.service("appSettingsService", function ($http) {

    var baseUrl = "/api/appsettings/";
    this.getAppSettings = function () {
        return $http.get(baseUrl+"getappsettings");
    };
    this.getBackgroundImages = function () {
        return $http.get(baseUrl + "GetBackgroundImages");
    };

});
