var allFilesCtrl = campsApp.controller('allFilesCtrl', ['$scope', '$compile', 'shareGalleryDataFa', 'galleryService', 'shareFilesDataFa', allFilesCtrlfn]);



function allFilesCtrlfn($scope, $compile, shareGalleryDataFa, galleryService, shareFilesDataFa) {
    var shareData = shareFilesDataFa;
    shareGalleryDataFa.registerObserverCallback(bindGalleries);
    var imagesBoardName = 'imagesBoard';
    var modalImagesBoard = null;
    function loadData() {
        $scope.gallery = shareData.gallery;

    }
    $scope.showImagesBoard = function () {


        if (!isExistElement(imagesBoardName)) {
            modalImagesBoard = $.jsPanel({
                id: imagesBoardName,
                selector: 'body',
                overflow: { horizontal: 'hidden', vertical: 'scroll' },
                controls: {
                    iconfont: 'font-awesome'
                    , buttons: 'none'
                },
                content: $compile("<div ng-include=\"'/Apps/Templates/Gallery/FilesBoard.html'\"></div>")($scope),
                size: { width: 400, height: 'auto' },
                //    position: "center",
                theme: "primary",
                title: ' انتخاب فایل ',
                toolbarFooter: [
    {
        item: $compile('<button class="btn btn-success" ng-click="saveSelectedFilesToGallery()">ثبت</button>')($scope),
        event: 'click',
        btnclass: 'btn-xs'
    },
        {
            item: '        <button class="btn btn-danger">انصراف</button>',
            event: 'click',
            btnclass: 'btn-xs',
            btntext: ' X ',
            callback: function (event) { event.data.close() }
        }
                ]
            });

            modalImagesBoard.reposition("center");


        }

    };

    $scope.unCheck = function (id, $index) {
        var ids = id;

        $('#x' + ids).attr('checked', false);

        $scope.removeFromArray($index);

    }
    shareData.registerObserverCallback(loadData);
    /*---------------Delete File ---------------*/
    $scope.deleteFile = function (guid) {

        var modalYesNo = modalConfirmYesNo("حذف رکورد", "آیا از حذف رکورد مطمئن هستید؟", function () {
            //if click on YES

            galleryService.deleteFile(guid)
                            .success(function (data) {
                                findAndRemove($scope.gallery, "guid", guid);
                                successHint();
                            }).error(function (data) {
                                errorHintMessage("متاسفانه حذف نشد");
                            });

        }, function () {
            // if click on No
        });


    }

    /*---------------Download File ---------------*/
    $scope.downloadFile = function (file) {
        galleryService.downloadFile(file.guid);
        //.success(function (data) {
        //    successHint();
        //}).error(function (data) {
        //    alert("faild:" + data);
        //});
    }

    /*---------------Update File ---------------*/
    $scope.seelectedFileToUpdate = null;
    $scope.updateFile = function (object) {
        $scope.seelectedFileToUpdate = object;
        var updateFileWindow = "updateFileWindow";
        if (!isExistElement(updateFileWindow)) {
            var updateFileModal = modalWindows(updateFileWindow, 'ویرایش فایل ', $compile("<div ng-include=\"'/Apps/Templates/Gallery/updatefile.html'\"></div>")($scope));
            updateFileModal.reposition("center");
            updateFileModal.resize(600, 300);

        }
    }
    $scope.updateFileToDb = function () {

        galleryService.updateFile($scope.seelectedFileToUpdate)
                    .success(function (data) {
                        successHint();
                    }).error(function (data) {
                        errorHintMessage("متاسفانه بروز رسانی انجام نشد");
                    });
    }
    /*---------------File Add To gallery---------------*/

    $scope.selectedFiles = [];

    $scope.addFileToGallery = function (fileId, $event, $index) {
        var checked = $event.currentTarget.checked;
        if (checked) {

            $scope.selectedFiles.push({ fileId: fileId });
            if ($scope.selectedFiles.length > 0) {
                $scope.showImagesBoard();
            };

        } else {

            findAndRemove($scope.selectedFiles, "fileId", fileId);
        }
    }

    $scope.removeFromArray = function (fileId) {
        var array = $scope.selectedFiles;

        array.splice(fileId, 1);

    }

    $scope.saveSelectedFilesToGallery = function () {
        var $selectedId = $scope.data.selectedId[0];
        if ($selectedId == null) {
            infoHintMessage("لطفا برای ثبت یک گالری انتخاب کنید");
            //return null;
        }
        galleryService.addFileToGallery($selectedId, $scope.selectedFiles).then(function (response) {

            //remove selected files to add to gallery
            shareData.refresh();
            //rebind to files list
            shareData.getFileList();
            // close dialog box

            $.each($scope.selectedFiles, function (key, value) {
                // uncheck selected file in main list
                $('#x' + value.fileId).attr('checked', false);
            });
            $scope.selectedFiles = [];

            modalImagesBoard.close();
            modalImagesBoard = null;
            successHint();
            // imagesBoardName = "";
        },
            function (error) {
                errorHintMessage("متاسفانه انجام نشد");
            });
    }
    $scope.data = { selectedId: 'None' };
    $scope.selectedGalleryId = {};
    $scope.galleries = [];
    function bindGalleries() {

        $scope.galleries = shareGalleryDataFa.galleries;

    }

    //----------------------------------Load Data at startup
    shareData.getFileList();
    bindGalleries();

}
