var galleryCtrl = campsApp.controller('galleryCtrl', ['$scope', '$upload', '$route', '$compile', '$location', 'shareFilesDataFa', 'galleryService', 'shareGalleryDataFa', galleryCtrlfn]);



function galleryCtrlfn($scope, $upload, $route, $compile, $location, shareFilesDataFa, galleryService, shareGalleryDataFa) {
    var shareGallery = shareGalleryDataFa;
    shareGallery.registerObserverCallback(loadData);
    shareGallery.registerObserverCallback(refreshScroll);
 var sharefiles=   shareFilesDataFa;

    var newGalleryWindow = 'newGalleryWindow';


    $scope.newGallery = function () {
        if (!isExistElement(newGalleryWindow)) {
            var modalup = modalWindows(newGalleryWindow, 'گالری جدید', $compile("<div ng-include=\"'/Apps/Views/Gallery/NewGallery.html'\"></div>")($scope));
            modalup.reposition("center");
            modalup.resize(208, 60);

        }
        //  var modalup = modalWindows('addPhoto',"بارگذاری عکس", " ");
        // modalup.content.append("<div id='file_upload' style='margin:10px;'><input type='file'></div>");
    };
    $scope.registerGallery = function (galleryname) {

        galleryService.newGalleriy(galleryname).success(function (data) {
            refreshGalleryCategory();
            successHint();
        }).error(function (data) {
            alert("faild:" + data);
        });
    };
    $scope.uploadImage = function () {

        var modalup = modalUpload("بارگذاری عکس", $compile("<div ng-include=\"'/Apps/Templates/Upload/UploadPanel.html'\"></div>")($scope));
        modalup.resize(800, 400);
        modalup.reposition("center");
        //  var modalup = modalWindows('addPhoto',"بارگذاری عکس", " ");
        // modalup.content.append("<div id='file_upload' style='margin:10px;'><input type='file'></div>");
    };




    $scope.galleriesList = [];
    var $id = "#mainGalleryWrapper";

    $scope.isVisibleHome = true;

    $scope.galleryName = "";

    $scope.message = "this is me";

    $scope.selectCategory = function (id) {

        shareGallery.getGallery(id);

        $scope.allFilesCategory();

    };
    $scope.refreshData = function () {
        shareGallery.refresh();
        $scope.allFilesCategory();
        sharefiles.refresh();

    };
    $scope.allFiles = function () {

        $location.url('/allFiles');

    };
    $scope.allFilesCategory = function () {

        //  window.location.reload();
        $location.url('/allFileCategory');
        $route.reload();
    };
    $scope.videoCategory = function () {

        $location.url('/videoCategory');
    };
    $scope.audioCategory = function () {

        $location.url('/audioCategory');
    };
    $scope.photoCategory = function () {

        $location.url('/photoCategory');
    };
    $scope.getGalleries = function () {

        refreshGalleryCategory();
    };

    /*---------------Scroll Bar in Panel---------------*/

    var myScroll = new IScroll($id, {
        bounceEasing: 'elastic',
        bounceTime: 1200,
        scrollbars: true,
        mouseWheel: true,
        interactiveScrollbars: true,
        shrinkScrollbars: 'scale',
        fadeScrollbars: false
    });
    //------------------------------------
    function refreshScroll() {
        myScroll.refresh();
    }
    //-----------------delete selected gallery
    $scope.deleteGalleryById = function (id) {
  
        var modalYesNo = modalConfirmYesNo("حذف رکورد", "آیا از حذف رکورد مطمئن هستید؟", function () {
            //if click on YES
            galleryService.deleteGalleryById(id)
            .success(function (data) {
                refreshGalleryCategory();
                successHint();
            }).error(function (data) {
                errorHintMessage("متاسفانه حذف نشد");
            });

        }, function () {
            // if click on No
        });

    };
    //-----------------rename selected gallery
    $scope.changeGalleryName = function() {
        galleryService.renameGallery($scope.renamedGalleryName.id, $scope.renamedGalleryName.galleryName)
        .success(function (data) {
            refreshGalleryCategory();
            successHint();
        }).error(function (data) {
            errorHintMessage("متاسفانه تغییر نام انجام نشد");
        });
    }
    $scope.renamedGalleryName ={id:"",galleryName:""}
    $scope.renameGallery = function (id, galleryName) {
        $scope.renamedGalleryName.id = id;
        $scope.renamedGalleryName.galleryName = galleryName;
        var renameGalleryWindow = "renameGalleryWindow";
        if (!isExistElement(renameGalleryWindow)) {
            var renameGalleryModal = modalWindows(renameGalleryWindow, 'عنوان جدید', $compile("<div ng-include=\"'/Apps/Templates/Gallery/RenameGallery.html'\"></div>")($scope));
            renameGalleryModal.reposition("center");
            renameGalleryModal.resize(230, 60);

        }

    };
    function refreshGalleryCategory() {
        shareGallery.refresh();
        myScroll.refresh();
    }
    /*------------------------------*/
    function loadData() {

        //var promiseGet = galleryService.getGalleries(skip);

        //promiseGet.then(function (response) {
        $scope.galleriesList = [];
        $scope.galleriesList = shareGallery.galleries;
        //},
        //    function (error) {
        //        //  $scope.error = 'failure loading ', errorPl;
        //    });

    }

    refreshGalleryCategory();

    myScroll.refresh();
    myScroll.on('scrollEnd', function () {

        var h = this.maxScrollY;
        var call = this.y - 100;
        if (call < h) {
            refreshGalleryCategory();
            //$.each(data, function (key, value) {
            //    alert(key + ":" + value);
            //});
            //for (var i = 0; i <= 20; i++) {
            //    $('#' +$id+' div.scroller ul').append("<li>jnjnjnj</li>");
            //}

            myScroll.refresh();
        }
    });


    /*------------------------------*/
    $scope.addImage = function (file) {
        if (file && file.length) {
            $scope.files.push({ progress: 0, file: file });
        }
    }
    $scope.file = null;
    $scope.files = [];
    $scope.$watch('files', function () {
        $scope.onFileSelect($scope.files);
    });
    $scope.abortUpload = function (index) {
        $scope.files[index].abort();
    }
    $scope.removeFile = function (index) {
        $scope.files.splice(index, 1);
    }
    $scope.fileUploadObj = { testString1: "Test string 1", testString2: "Test string 2" };

    $scope.onFileSelect = function ($item) {
        if ($item.file && $item.file.length) {


            //$files: an array of files selected, each file has name, size, and type.
            //for (var i = 0; i < $files.length; i++) {
            //    var $file = $files[i];
            //   (function (index) {
            // $scope.files[index] =
            $upload.upload({
                url: "/api/files/UploadFile", // webapi url
                method: "POST",
                data: { fileUploadObj: $scope.fileUploadObj },
                file: $item.file
            }).progress(function (evt) {
                // get upload percentage
                var value = parseInt(100.0 * evt.loaded / evt.total);
              //  console.log('percent: ' + value);
                $item.progress = value + '%';
            }).success(function (data, status, headers, config) {
                // file is uploaded successfully
              
                $scope.removeFile($item);
             var msg=   successHintMessage( "با موفقیت بارگذاری شد");
          
                // console.log(data);
            }).error(function (data, status, headers, config) {
                // file failed to upload
                errorHintMessage("متاسفانه فایل بارگذاری نشد");
                // console.log(data);
            });
            //     })(i);
            //  }


        }
    };




}
