campsApp.service("deptService", function ($http, $cacheFactory) {

    var allDeptCache = $cacheFactory('allDeptCache');
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

    var baseUrl = "/api/Department/";

    function getPagedResource(controlName, skip) {
        return $http.get(controlName + skip);//.then(function (response) {
    }

    this.loadData = function () {
       allDeptCache.removeAll();
        this.notifyObservers();
    }
    this.postNewDept = function (model) {
        var obj = JSON.stringify(model);
        var post = $http.post(baseUrl, obj);
        return post;
    };
    this.deleteDept = function (id) {

        return $http.delete(baseUrl + "DeleteDept/" + id);

    };
    this.editDept = function (id, model) {
        return $http.put(baseUrl + "?id=" + id, model);
    };
    this.getAlls = function () {

        return $http.get(baseUrl + "getall", { cache: this.allDeptCache });
    };
    this.getDepts = function (skip, pageSize) {
        return $http.get(baseUrl + "getdept?skip=" + skip + "&pageSize=" + pageSize, { cache: this.allDeptCache });
    };
    this.getPaging = function (skip, pageSize) {
        return $http.get(baseUrl + "getdept?skip=" + skip + "&pageSize=" + pageSize, { cache: this.allDeptCache });
    };
});
