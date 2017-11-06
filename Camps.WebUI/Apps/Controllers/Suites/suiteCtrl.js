var suiteCtrl = campsApp.controller('suiteCtrl', ['$scope', '$location', '$compile', 'suiteService', 'campsService', 'galleryService', 'preloaderfa', 'shareGalleryDataFa', suiteCtrlfn]);
//appCtrl.filter('unsafe', function ($sce) {
//    return function (val) {
//        return $sce.trustAsHtml(val);
//    };
//});
function suiteCtrlfn($scope, $location, $compile, suiteService, campsService, galleryService, preloaderfa, shareGalleryDataFa) {

    // pagination size variable. must declare in top
    $scope.pagesize = 20;
    $scope.skip = 0;

    var shareGallery = shareGalleryDataFa;
    shareGallery.registerObserverCallback(loadData);
    suiteService.watchOberver(loadSuites);
    shareGallery.getAllGallery();
    suiteService.loadSuites();




    var newSuite = "newSuite";
    var editSuite = "editSuite";
    var detailsSuite = "detailsSuite";
    var modalNewSuite;
    var modalEditSuite;
    var modalDetailsSuite;
    var newSuiteScroll;
    //---------------------Refresh List---------------
    $scope.reloadData = function () {
        $scope.skip = 0;
        $scope.suiteList = [];
        suiteService.loadSuites();

    };
    //---------------------get suite grade------------
    function getSuiteGrade() {
        suiteService.getSuiteGrade()
        .then(function (response) {
            $scope.suiteGrades = response.data;

        }, function (error) {
            errorHintMessage("لیست درجه سوئیت بارگذاری نشد");
        });
    };
    //--------------selectedt item from grade combobox-------------
    $scope.selectedGrade = function (item) {
        console.log(item);
        $scope.suite.SuiteGradeId = item;
    }
    //-------------get all exist camps in short --------------------
    function getAllCamp() {
        campsService.getAllCamps()
        .then(function (response) {
            $scope.allCamps = response.data;

        }, function (error) {
            errorHintMessage("لیست اردوگاه ها بارگذاری نشد");
        });
    };
    //--------------selectedt item from grade combobox-------------
    $scope.selectedCamp = function (item) {
        console.log(item);
        $scope.suite.CampId = item;
    }
    //-------------get all exist camps in short --------------------
    function getAllSuiteOwner() {
        suiteService.getSuiteOwner()
        .then(function (response) {
            $scope.allSuiteOwners = response.data;

        }, function (error) {
            errorHintMessage("لیست  مالکان سوئیت بارگذاری نشد");
        });
    };
    //--------------selected item from owner combobox-------------
    $scope.selectedOwner = function (item) {
        console.log(item);
        $scope.suite.SuiteOwnerId = item;
    }
    //-------------get all exist camps in short --------------------
    function getSuiteTypes() {
        suiteService.getSuiteTypes()
        .then(function (response) {
            $scope.suiteTypes = response.data;

        }, function (error) {
            errorHintMessage("لیست  نوع سوئیت بارگذاری نشد");
        });
    };
    //--------------selected item from owner combobox-------------
    $scope.selectedSuiteType = function (item) {
        console.log(item);
        $scope.suite.SuiteTypeId = item;
    }
    //-------------Suite Entity------------------------------------
    $scope.suite = {
        phones: [{ phoneNumber: "" }],
        SuiteName: "",
        SuiteNumber: "",
        Description: "",
        SuiteTypeId: "",
        CampId: "",
        SuiteOwnerId: "",
        GalleryId: null,
        SuiteGradeId: -1
    };
    //-------------make new suite with modal--------------------------
    $scope.newSuite = function () {
        if (!isExistElement(newSuite)) {
            preloaderfa.push();
            getAllSuiteOwner();
            getSuiteGrade();
            getAllCamp();
            getSuiteTypes();

            modalNewSuite = modalWindows(newSuite, 'سوئیت جدید', $compile("<div ng-include=\"'/Apps/Views/Suites/NewSuite.html'\"></div>")($scope));
            modalNewSuite.resize("700px", "500px");
            modalNewSuite.on("jspanelloaded", function (event, id) {
                if (id === newSuite) {

                    newSuiteScroll = new IScroll("#scrollnewsuiteframe", {
                        bounceEasing: 'elastic',
                        bounceTime: 1200,
                        scrollbars: true,
                        mouseWheel: true,
                        interactiveScrollbars: true,
                        shrinkScrollbars: 'scale',
                        fadeScrollbars: false
                    });

                    preloaderfa.pop();
                    //$scope.bindGalleries();
                }
            });





        }
    };
    //------------Add new suite function---------
    $scope.addNewSuite = function (model) {
        suiteService.postNewSuite(model)
                .success(function (data) {
                    $scope.reloadData();
                    successHintMessage("سوئیت جدید با موفقیت ثبت شد");
                    modalNewSuite.close();
                }).error(function (data) {
                    errorHintMessage("متاسفانه سوئیت جدید ثبت نشد");
                    //alert("faild:" + data);
                });
    }
    //-----------------------
    $scope.deleteSuite = function (id) {
        var modalYesNo = modalConfirmYesNo("حذف رکورد", "آیا از حذف رکورد مطمئن هستید؟", function () {
            //if click on YES
            suiteService.deleteSuite(id)
                    .then(function (data) {
                        $scope.reloadData();
                        findAndRemove($scope.suiteList, "id", id);
                        successHint();
                    }, function (data) {
                        errorHint();

                    });

        }, function () {
            // if click on No
        });

    };
    //-----------------------Details suite modal open---------
    $scope.openDetailsSuiteModal = function (model) {
        $scope.suiteDetails = model;

        if (!isExistElement(detailsSuite)) {
            preloaderfa.push();
            getAllSuiteOwner();
            getSuiteGrade();
            getAllCamp();
            getSuiteTypes();

            modalDetailsSuite = modalWindows(detailsSuite, 'جزئیات سوئیت ', $compile("<div ng-include=\"'/Apps/Views/Suites/DetailsSuite.html'\"></div>")($scope));
            modalDetailsSuite.resize("700px", "400px");
            modalDetailsSuite.on("jspanelloaded", function (event, id) {
                if (id === detailsSuite) {

                    preloaderfa.pop();
                    galleryService.getGalleryById(model.gallery.id)
                                    .then(function (response) {
                                        $scope.suiteDetails.gallery = response.data;
                                    }, function (response) {
                                        errorHintMessage("گالری تصاویر سوئیت انتخاب شده بارگذاری نشد");
                                    });

                }
            });

        }
    };
    //-----------------------Edit suite modal open---------
    $scope.editSuiteModel = null;
    $scope.openEditSuiteModal = function (model) {
        $scope.suite = model;
        if (!isExistElement(editSuite)) {
            preloaderfa.push();
            getAllSuiteOwner();
            getSuiteGrade();
            getAllCamp();
            getSuiteTypes();

            modalEditSuite = modalWindows(editSuite, 'ویرایش سوئیت ', $compile("<div ng-include=\"'/Apps/Views/Suites/EditSuite.html'\"></div>")($scope));
            modalEditSuite.resize("700px", "500px");
            modalEditSuite.on("jspanelloaded", function (event, id) {
                if (id === editSuite) {

                    preloaderfa.pop();
                    //$scope.bindGalleries();

                }
            });

        }
    };
    //-----------------------Edit suite---------
    $scope.editSuite = function (model) {

        suiteService.editSuite(model.id, model)
                .then(function (data) {
                    $scope.reloadData();
                    successHint();
                }, function (data) {
                    errorHint();

                });

    };
    //-------------
    $scope.showGalleryDetails = function (id) {

        $location.url('/galleryDetails');
    }
    //-------------New model--------------------
    $scope.addPhone = function () {
        if ($scope.suite.phones.length > 5) {
            modalInfoOk("هشدار", "تعداد شماره تلفن وارد شده زیاد است", null);
            return;
        }
        $scope.suite.phones.push({ phoneNumber: "" });
        newSuiteScroll.refresh();
    }
    $scope.minusPhone = function ($index) {
        $scope.suite.phones.splice($index, 1);
        newSuiteScroll.refresh();
    }
    //-----------------Edit mode
    $scope.addPhoneEdit = function () {
        if ($scope.suiteDetails.phoneViewModels.length > 5) {
            modalInfoOk("هشدار", "تعداد شماره تلفن وارد شده زیاد است", null);
            return;
        }
        $scope.suiteDetails.phoneViewModels.push({ phoneNumber: "" });
 
    }
    $scope.minusPhoneEdit = function ($index) {
        $scope.campDetails.phoneViewModels.splice($index, 1);
 
    }

    //--------------------------------
    $scope.selectedGallery = function (item) {

        $scope.suite.GalleryId = item;
    }
    //----------------Remove suite gallery----------------
    $scope.removeGallery = function (suite) {
        var modalYesNo = modalConfirmYesNo("حذف گالری", "آیا از حذف گالری مطمئن هستید؟", function () {
            //if click on YES
            suiteService.removeGallery(suite.id)
                                .then(function (data) {
                                    $scope.reloadData();
                                    suite.gallery = null;
                                    successHint();
                                }, function (data) {
                                    errorHint();

                                });
        }, function () {
            // if click on No
        });

    }
    /*------------------------------*/
    function loadData() {
        $scope.galleriesList = [];
        $scope.galleriesList = shareGallery.galleries;


    }
    $scope.fetchData = function (skip, pageSize) {
        return suiteService.getSuits(skip, pageSize);
    }

    function loadSuites() {
        preloaderfa.push();
        suiteService.getSuits($scope.skip, $scope.pagesize)
                    .then(function (response) {
                        var data = response.data;
                        $scope.suiteList = data;
                        $scope.skip += $scope.pagesize;
                        preloaderfa.pop();
                        //     newSuiteScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست سوئیت ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newSuiteScroll.refresh();
                    });
    }

}