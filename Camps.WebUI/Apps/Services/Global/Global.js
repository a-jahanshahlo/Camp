

campsApp.factory('preloaderfa', ['$rootScope', preloaderfn]);



function preloaderfn($rootScope) {
    var Data = Preloader();

    return Data;

}
