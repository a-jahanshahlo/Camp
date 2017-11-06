var allFileCategoryCtrl = campsApp.controller('allFileCategoryCtrl', ['$scope', '$compile', 'galleryService', 'shareGalleryDataFa', allFileCategoryCtrlfn]);



function allFileCategoryCtrlfn($scope, $compile, galleryService, shareGalleryDataFa) {
    var shareData = shareGalleryDataFa;

    function loadData() {
        $scope.gallery = shareData.gallery;
    }

    shareData.registerObserverCallback(loadData);

    // $scope.
}
