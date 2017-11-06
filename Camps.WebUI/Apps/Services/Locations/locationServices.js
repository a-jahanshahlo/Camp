campsApp.service("locationServices", function ($http) {
    function getPagedResource(controlName, skip) {
        return $http.get(controlName + skip);//.then(function (response) {
            return response.data;

            //return {
            //    totalRecords: parseInt(response.headers('X-InlineCount')),
            //    results: custs
            //};
       // });
    }
    var baseUrl = "/api/location/";
    var pageSkip = 0;
    this.resetPagesize = function () {
        pageSkip = 0;
    };
    this.getProvinces = function () {
        return getPagedResource(baseUrl + "GetProvinces/");
    };
    this.getCities = function () {
        //return $http.get("/api/appsettings/GetBackgroundImages");
        return getPagedResource(baseUrl + "GetCities/");
    };
    this.getCitiesByProvince = function (id) {
        //return $http.get("/api/appsettings/GetBackgroundImages");
        return getPagedResource(baseUrl + "GetCitiesByProvince/", id);
    };

});
