var audiosCategoryCtrl = campsApp.controller('audiosCategoryCtrl', ['$scope', '$compile', 'galleryService', audiosCategoryCtrlfn]);



function audiosCategoryCtrlfn($scope, $compile, galleryService) {
    $scope.galleriesList = [];
    var $id = "#mainGalleryWrapper";


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
