

campsApp.factory('shareGalleryDataFa', ['$rootScope', 'galleryService', shareUserDataFafn]);



function shareUserDataFafn($rootScope,galleryService ) {

    var service = galleryService;
    var observerCallbacks = [];
    var selectedGalleryobserverCallbacks = [];
    var Data = {
        galleryId: 0,
        skip: 0,
        refresh:null,
        gallery: null,
        galleries: [],
        getGallery: null,
        getAllGallery: null
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

    //register an observer for selected gallery
    Data.registerObserverCallbackSelectedGallery = function (callback) {
        selectedGalleryobserverCallbacks.push(callback);
    };

    //call this when selected gallery has been changed
    Data.notifyObserversSelectedGallery = function () {
        angular.forEach(selectedGalleryobserverCallbacks, function (callback) {
            callback();
        });
    };



    Data.refresh=function() {
        Data.skip = 0;
        Data.galleries = [];
        Data.gallery = [];
        Data.getAllGallery();
    }
    //get special gallery with id
    Data.getGallery = function (id) {
        Data.galleryId = id;
        var galleryById = service.getGalleryById(id);
        galleryById.success(function (data) {
           
            Data.gallery = data;
            Data.notifyObservers();
            Data.notifyObserversSelectedGallery();
        }).error(function (data) {
         //   Data.gallery = data;
        });

    }
    //get all galleries list
    Data.getAllGallery = function () {

        var galleryById = service.getGalleries(Data.skip);
        galleryById.success(function (data) {

            $.each(data, function (key, value) {
                Data.galleries.push(value);
            });

            Data.notifyObservers();
            Data.skip += 100;
        }).error(function (data) {
            //   Data.gallery = data;
        });

    }
    return Data;

}
