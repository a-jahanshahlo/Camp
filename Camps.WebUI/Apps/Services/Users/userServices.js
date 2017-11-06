campsApp.service("userServices", function ($http) {


    this.getCurrentUser = function () {
        return $http.get("/api/userprofile/GetCurrentUser");

    };
    this.getBackgroundImage = function () {
       return $http.get("/api/userprofile/GetBackgroundImage");
        //var deferred = $q.defer();
        //$http.get("/api/userprofile/GetBackgroundImage").success(function (result) {
        //    deferred.resolve(result);
        //}).error(function (result) { deferred.reject(result); });
        //return deferred.promise;
    };
    this.setBackgroundImage = function (imagePath) {
        var request = $http.post("/api/userprofile/SetBackgroundImage/", "'"+imagePath+"'");
        return request;
    };


    //Fundction to Read Employee based upon id
    this.getEmployee = function (id) {
        return $http.get("/api/EmployeeInfoAPI/" + id);
    };

    //Function to create new Employee
    this.post = function (Employee) {
        var request = $http({
            method: "post",
            url: "/api/EmployeeInfoAPI",
            data: Employee
        });
        return request;
    };

    //Function  to Edit Employee based upon id 
    this.put = function (id, Employee) {
        var request = $http({
            method: "put",
            url: "/api/EmployeeInfoAPI/" + id,
            data: Employee
        });
        return request;
    };

    //Function to Delete Employee based upon id
    this.delete = function (id) {
        var request = $http({
            method: "delete",
            url: "/api/EmployeeInfoAPI/" + id
        });
        return request;
    };
});

//campsApp.factory('userServices', userServices);