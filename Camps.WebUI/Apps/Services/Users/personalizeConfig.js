campsApp.service("personalizeConfigService", function ($http) {

    //Function to Read current user
    this.getBackgroundImage = function () {
        return $http.get("/api/userprofile/GetCurrentUser");
    };
});
