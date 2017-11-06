campsApp.service("suiteService", function ($http, $cacheFactory) {
    var suiteGradeCache = $cacheFactory('suiteGradeCache');
    var suiteOwnerCache = $cacheFactory('suiteOwnerCache');
    var suiteTypeCache = $cacheFactory('suiteTypeCache');
    var suitesCache = $cacheFactory('suitesCache');

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

    function getPagedResource(controlName, skip) {
        return $http.get(controlName + skip);//.then(function (response) {
    }
    var baseUrl = "/api/suite/";
    var pageSkip = 0;
    this.resetPagesize = function () {
        pageSkip = 0;
    };

    this.refreshCache = function () {
        suiteGradeCache.removeAll();
        suiteOwnerCache.removeAll();
        suiteTypeCache.removeAll();
    }
    this.loadSuites = function () {
        suitesCache.removeAll();
        this.notifyObservers();
    }
    this.getSuits = function (skip, pageSize) {
        return $http.get(baseUrl + "get?skip=" + skip + "&pageSize=" + pageSize, { cache: this.suitesCache });
    };
    this.getSuiteGrade = function () {
        return $http.get(baseUrl + "GetSuiteGrade", { cache: this.suiteGradeCache });
    };
    this.getSuiteOwner = function () {
        return $http.get(baseUrl + "GetAllSuiteOwner", { cache: this.suiteOwnerCache });
    };
    this.getSuiteTypes = function () {
        return $http.get(baseUrl + "GetSuiteType", { cache: this.suiteTypeCache });

    };
    this.postNewSuite = function (model) {
        var obj = JSON.stringify(model);
        return $http.post(baseUrl, obj);

    };
    this.deleteSuite = function (id) {

        return $http.delete(baseUrl + "DeleteSuite/" + id);

    };
    this.removeGallery = function (id) {
        return $http.delete(baseUrl + "RemoveGallery/" + id);
    };
    this.editSuite = function (id,model) {
        return  $http.put(baseUrl + "?id=" + id, model);
    };
});
