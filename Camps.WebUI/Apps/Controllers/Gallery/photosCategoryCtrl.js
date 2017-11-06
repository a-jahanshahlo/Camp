var photoCategoryCtrl = campsApp.controller('photoCategoryCtrl', ['$scope', '$compile', 'galleryService', 'shareGalleryDataFa', photoCategoryCtrlfn]);



function photoCategoryCtrlfn($scope, $compile, galleryService, shareGalleryDataFa) {
 var shareData=  shareGalleryDataFa;
   

 function getPhotoGallery() {
     $scope.photoList = shareData.gallery.photos;

 };

    shareData.registerObserverCallback(getPhotoGallery);
    getPhotoGallery();

}
