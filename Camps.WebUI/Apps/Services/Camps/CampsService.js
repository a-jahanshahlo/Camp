campsApp.service("campsService", function ($http, $cacheFactory) {

    var allCampsCache = $cacheFactory.get('allCampsCache');
    var observerCallbacks = [];
 
    //register an observer
    this.regObvCallback = function (callback) {
        observerCallbacks.push(callback);
    };

    //call this when you know 'foo' has been changed
    this.notifyObservers  = function () {
        angular.forEach(observerCallbacks, function (callback) {
            callback();
        });
    };
    this.refresh    =function () {
         pageSkip = 0;
       
    }


    function getPagedResource(controlName, skip) {
        return $http.get(controlName + skip);//.then(function (response) {
          

            //return {
            //    totalRecords: parseInt(response.headers('X-InlineCount')),
            //    results: custs
            //};
       // });
    }
    var baseUrl = "/api/camps/";
    var pageSkip = 0;
    this.resetPagesize = function () {
     
    };
    this.refreshCache = function () {
        allCampsCache.removeAll();
    }
    this.findCamp = function (id) {

        return getPagedResource(baseUrl + "GetFind/", id);
    };
    this.getAllCamps = function () {
        return $http.get(baseUrl + "GetAll", { cache: allCampsCache });
    };
    this.getCamps = function (skip) {
    
        return getPagedResource(baseUrl + "?skip=" , skip);
    };

    this.getBackgroundImages = function () {
        return $http.get("/api/appsettings/GetBackgroundImages");
    };
    this.postCamp = function (camp) {
        var obj = JSON.stringify(camp);
        var post = $http.post(baseUrl , obj);
        return post;
    };
    this.putCamp = function (id,camp) {
        var obj = JSON.stringify(camp);
        var post = $http.put(baseUrl + "?id=" + id, obj);
        return post;
    };
    this.deleteCamp = function (id) {
      //  var obj = JSON.stringify(camp);
        var deleteitem = $http.delete(baseUrl +"?id="+ id);
        return deleteitem;
    };
});
