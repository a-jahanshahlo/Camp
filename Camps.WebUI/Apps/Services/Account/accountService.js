campsApp.service("accountService", function ($http, $cacheFactory) {

    var allAccountCache = $cacheFactory('allAccountCache');
    var observerCallbacks = [];

    //register an observer
    this.watchOberver = function (callback) {
        observerCallbacks.push(callback);
    };

    //call this when you know 'foo' has been changed
    this.notifyObservers = function () {
        angular.forEach(observerCallbacks, function (callback) {
            callback();
        });
    };

    var baseUrl = "/api/AccountApi/";

    function getPagedResource(controlName, skip) {
        return $http.get(controlName + skip);//.then(function (response) {
    }

    this.loadData = function () {
        allAccountCache.removeAll();
        this.notifyObservers();
    }
    this.post = function (model) {
        var obj = JSON.stringify(model);
        var post = $http.post(baseUrl + "PostAdminRegister/", obj);
        return post;
    };
    this.resetPass = function (model) {
        var obj = JSON.stringify(model);
        var post = $http.post(baseUrl + "ResetPassword/", obj);
        return post;
    };
    this.addToRole = function (userid, roleid) {
        var model = { userId: userid, roleId: roleid };
        var obj = JSON.stringify(model);
        var post = $http.post(baseUrl + "AddToRole/", obj);
        return post;
    };
    this.removeFromRole = function (userid, roleid) {
        var model = { userId: userid, roleId: roleid };
        var obj = JSON.stringify(model);
        var post = $http.post(baseUrl + "RemoveFromRole/", obj);
        return post;
    };
    this.delete = function (id) {

        return $http.delete(baseUrl + "Delete/" + id);

    };
    this.edit = function (id, model) {
        return $http.put(baseUrl + "?id=" + id, model);
    };
    this.getAlls = function () {

        return $http.get(baseUrl + "get", { cache: this.allAccountCache });
    };
    this.getCurrentUserRoles = function () {

        return $http.get(baseUrl + "GetCurrentUserRoles");
    };
    this.getPaging = function (skip, pageSize) {
        return $http.get(baseUrl + "?skip=" + skip + "&pageSize=" + pageSize, { cache: this.allAccountCache });
    };
});
