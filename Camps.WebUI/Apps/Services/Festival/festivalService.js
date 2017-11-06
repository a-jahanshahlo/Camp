campsApp.service("festivalService", function ($http, $cacheFactory) {

    var allFestivalCache = $cacheFactory('allFestivalCache');
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

    var baseUrl = "/api/Festival/";

    function getPagedResource(controlName, skip) {
        return $http.get(controlName + skip);//.then(function (response) {
    }

    this.loadData = function () {
        allFestivalCache.removeAll();
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
    this.getAlls = function () {

        return $http.get(baseUrl + "get", { cache: this.allFestivalCache });
    };
    this.getPaging = function (skip, pageSize) {
        return $http.get(baseUrl + "?skip=" + skip + "&pageSize=" + pageSize, { cache: this.allFestivalCache });
    };
});
