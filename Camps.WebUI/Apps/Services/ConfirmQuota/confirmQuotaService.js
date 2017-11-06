campsApp.service("confirmQuotaService", function ($http, $cacheFactory) {

    var allconfirmQuotaCache = $cacheFactory('allconfirmQuotaCache');
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

    var baseUrl = "/api/confirmQuota/";

    function getPagedResource(controlName, skip) {
        return $http.get(controlName + skip);//.then(function (response) {
    }

    this.loadData = function () {
        allconfirmQuotaCache.removeAll();
        this.notifyObservers();
    }
    this.post = function (model) {
        var obj = JSON.stringify(model);
        var post = $http.post(baseUrl + "Post/", obj);
        return post;
    };
    this.delete = function (id) {

        return $http.delete(baseUrl + "Delete/" + id);

    };
    this.edit = function (id, model) {
        return $http.put(baseUrl + "?id=" + id, model);
    };
    this.refuse = function (id) {
        return $http.delete(baseUrl + "DeleteRefuse/" + id);
    };
    this.getAlls = function () {

        return $http.get(baseUrl + "get");
    };

    this.getPaging = function (skip, pageSize) {

        return $http.get(baseUrl + "?skip=" + skip + "&pageSize=" + pageSize);

    };
});
