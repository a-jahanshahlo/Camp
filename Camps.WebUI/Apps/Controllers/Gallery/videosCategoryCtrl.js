var videosCategoryCtrl = campsApp.controller('videosCategoryCtrl', ['$scope', '$compile', 'galleryService', videosCategoryCtrlfn]);



function videosCategoryCtrlfn($scope, $compile, galleryService) {
    $scope.galleriesList = [];


    $scope.getGalleries = function () {

        loadData(0);
    };
    function loadData(skip) {

        var promiseGet = galleryService.getGalleries(skip);

        promiseGet.then(function (response) {
            $.each(response.data, function (key, value) {
                $scope.galleriesList.push(value);
            });

        },
            function (error) {
                //  $scope.error = 'failure loading ', errorPl;
            });

    }

    loadData();

 

}
