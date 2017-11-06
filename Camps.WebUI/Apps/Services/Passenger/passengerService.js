var passengerService = campsApp.service("passengerService", function($http, $cacheFactory) {
    var passengerCache = $cacheFactory('passengerCache');
 

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
    var baseUrl = "/api/passenger/";
    var pageSkip = 0;
    this.resetPagesize = function () {
        pageSkip = 0;
    };

    this.refreshCache = function () {
        passengerCache.removeAll();

    }
    this.loadPassengers = function () {
        passengerCache.removeAll();
        this.notifyObservers();
    }
    this.getPassengers = function (skip, pageSize) {
        return $http.get(baseUrl + "get?skip=" + skip + "&pageSize=" + pageSize, { cache: this.passengerCache });
    };
});