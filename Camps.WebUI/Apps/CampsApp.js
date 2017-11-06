
//(function() {

//var campsApp = angular.module('campsApp', ['ngSanitize', 'ngAnimate',  'ui.bootstrap']);

var campsApp = angular.module('campsApp', ['ngMessages', 'ngRoute', 'angularFileUpload']);

campsApp.config([
    '$httpProvider',
    '$routeProvider',
    '$locationProvider',

    function ($httpProvider,
        $routeProvider,
        $locationProvider
        )
    {
        var viewBase = '/app/';
       
        $routeProvider
            .when('/home', {
                controller: 'indexCtrl',
                templateUrl: '/apps/index.html'

            })
            .when('/profile', {
                controller: 'userProfileCtrl',
                templateUrl: '/apps/views/UserProfile/UserProfileIndex.html'

            })
            .when("/videoCategory", {
                templateUrl: "/Apps/Templates/Gallery/VideoCategory.html",
                controller: "videosCategoryCtrl"
            })
            .when("/audioCategory", {
                templateUrl: "/Apps/Templates/Gallery/audioCategory.html",
                controller: "audiosCategoryCtrl"
            })
            .when("/photoCategory", {
                templateUrl: "/Apps/Templates/Gallery/photoCategory.html",
                controller: "photoCategoryCtrl"
            })
            .when("/allFileCategory", {
                templateUrl: "/Apps/Templates/Gallery/allFileCategory.html",
                controller: "allFileCategoryCtrl"
            })
            .when("/allFiles", {
                templateUrl: "/Apps/Templates/Gallery/allFiles.html",
                controller: "allFilesCtrl"
            })
            .when("/campGallery", {
                templateUrl: "/Apps/Templates/Camps/galleries.html"

            })
            .when("/campGalleryDetails", {
                templateUrl: "/Apps/Templates/Camps/galleryDetails.html"

            })
            .when("/galleryDetails", {
                templateUrl: "/Apps/Templates/Gallery/galleryDetails.html"

            })
            //.when('/customerorders/:customerId', {
            //    controller: 'CustomerOrdersController',
            //    templateUrl: viewBase + 'customers/customerOrders.html',
            //    controllerAs: 'vm'
            //})
            //.when('/customeredit/:customerId', {
            //    controller: 'CustomerEditController',
            //    templateUrl: viewBase + 'customers/customerEdit.html',
            //    controllerAs: 'vm',
            //    secure: true //This route requires an authenticated user
            //})
            //.when('/orders', {
            //    controller: 'OrdersController',
            //    templateUrl: viewBase + 'orders/orders.html',
            //    controllerAs: 'vm'
            //})
            //.when('/about', {
            //    controller: 'AboutController',
            //    templateUrl: viewBase + 'about.html',
            //    controllerAs: 'vm'
            //})
            //.when('/login/:redirect*?', {
            //    controller: 'LoginController',
            //    templateUrl: viewBase + 'login.html',
            //    controllerAs: 'vm'
            //})
            .otherwise({ redirectTo: '/home' });
 

    }
]).run(function () {

    //console.info(ADMdtp.getOptions());

});

//}());
