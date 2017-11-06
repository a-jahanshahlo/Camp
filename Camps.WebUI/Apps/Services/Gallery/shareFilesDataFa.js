

campsApp.factory('shareFilesDataFa', ['$rootScope', 'galleryService', shareFilesDataFafn]);



function shareFilesDataFafn($rootScope, galleryService) {

    var service = galleryService;
    var observerCallbacks = [];
    var Data = {
        skip:0,
        gallery: null,
        refresh:null,
        getFileList: null
    };
    //register an observer
    Data.registerObserverCallback = function (callback) {
        observerCallbacks.push(callback);
    };

    //call this when you know 'foo' has been changed
    Data.notifyObservers = function () {
        angular.forEach(observerCallbacks, function (callback) {
            callback();
        });
    };
    Data.refresh = function () {
        Data.skip = 0;
      //  Data.getFileList();
    }
    Data.getFileList = function () {
        // this code must remove in the next change when pagination is requiered
        Data.refresh();
        var galleryById = service.getFiles(Data.skip);
        galleryById.success(function (data) {
           
            Data.gallery = data;
            Data.notifyObservers();
            Data.skip += 10;
        }).error(function (data) {
         //   Data.gallery = data;
        });

    }

    return Data;

}
