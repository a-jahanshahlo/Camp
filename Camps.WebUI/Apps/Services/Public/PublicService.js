campsApp.service("publiceService", function ($http, $cacheFactory) {

 
    var baseUrl = "/api/public/";

    function getPagedResource(controlName, skip) {
        return $http.get(controlName + skip);//.then(function (response) {
    }

 

    this.getGenders = function () {

        return $http.get(baseUrl + "GetGender");
    };
 
});
