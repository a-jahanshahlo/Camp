

var campsCtrl = campsApp.controller('campsCtrl', ['$scope', '$route', '$compile', '$location', 'preloaderfa', 'campsService', 'galleryService', 'locationServices', campsCtrlfn]);


function campsCtrlfn($scope, $route, $compile, $location, preloaderfa, campsService, galleryService, locationServices) {
    var modalNewCamps;
    campsService.regObvCallback(loadData);
    //  $scope.addNewCamps = "<span style='color:red'> this is red</span>";
    // varibale
    $scope.isEdit = false;
    var newCamp = 'newCamp';
    var newCampScroll;
    function getProvinces() {
        locationServices.getProvinces()
        .then(function (response) {
            $scope.provinces = response.data;
        }, function (error) {

        });
    };


    $scope.provinces = [];
    $scope.cities = [];
    $scope.camp = {
        phones: [{ phoneNumber: "" }],
        areaSize: "",
        name: "",
        address: {
            fullAddress: "",
            state: "",
            zip: "",
            longitude: "51.342274",
            latitude: "35.691985",
            city: {
                "id": -1, "name": "" 
            }
        },
        galleries: []
    };

    $scope.setMaps = function (lng, lat) {
        var map = new GMaps({
            div: '#map1',
            lat: lat,
            lng: lng,
            zoom: 10,
            click: function (e) {
                $scope.$apply(function () {
                    $scope.camp.address.latitude = e.latLng.lat();
                    $scope.camp.address.longitude = e.latLng.lng();
                    if ($scope.campDetails) {
                        $scope.campDetails.addressViewModel.latitude = e.latLng.lat();
                        $scope.campDetails.addressViewModel.longitude = e.latLng.lng();
                    }


                    successHintMessage("عرض جغرافیایی:" + e.latLng.lat() + "<br/>" + "طول جغرافیایی :" + e.latLng.lng());

                });


            }
        });

    };
    //----------Show map location -------------
    $scope.showMaps = function (lng, lat) {
        var geoCamp = "geoCamp";
        if (!isExistElement(geoCamp)) {
            preloaderfa.push();
            var modalGeoCamp = modalWindows(geoCamp, 'موقعیت اردوگاه', " <div id='showCamp' style='width:700px;height:500px; '></div> ");
            //  

            modalGeoCamp.on("jspanelloaded", function (event, id) {
                if (id === geoCamp) {

                    var map = new GMaps({
                        div: '#showCamp',
                        lat: lat,
                        lng: lng,
                        zoom: 10

                    });
                    modalGeoCamp.resize("700px", "500px");
                    modalGeoCamp.reposition("center");
                    modalGeoCamp.front();
                    preloaderfa.pop();
                }
            });


        }

    };
    loadData();

    $scope.refreshScrollbar = function () {
        newCampScroll.refresh();
    }
    //-------------Modify phone model
    $scope.addPhone = function () {
        if ($scope.camp.phones.length > 10) {
            modalInfoOk("هشدار", "تعداد شماره تلفن وارد شده زیاد است", null);
            return;
        }
        $scope.camp.phones.push({ phoneNumber: "" });
        newCampScroll.refresh();
    }
    $scope.minusPhone = function ($index) {
        $scope.camp.phones.splice($index, 1);
        newCampScroll.refresh();
    }
    //-----------------Edit mode
    $scope.addPhoneEdit = function () {
        if ($scope.campDetails.phoneViewModels.length > 10) {
            modalInfoOk("هشدار", "تعداد شماره تلفن وارد شده زیاد است", null);
            return;
        }
        $scope.campDetails.phoneViewModels.push({ phoneNumber: "" });
        editCampScroll.refresh();
    }
    $scope.minusPhoneEdit = function ($index) {
        $scope.campDetails.phoneViewModels.splice($index, 1);
        editCampScroll.refresh();
    }
    $scope.getCities = function (id) {
        if (id) {
            locationServices.getCitiesByProvince(id)
             .then(function (response) {
                 $scope.cities = response.data;
             }, function (error) {

             });
        }

    }
    $scope.deleteCamp = function (id) {
        var modalYesNo = modalConfirmYesNo("حذف رکورد", "آیا از حذف رکورد مطمئن هستید؟", function () {
            //if click on YES
            campsService.deleteCamp(id)
                    .success(function (data) {
                        campsService.notifyObservers();
                        successHint();
                    }).error(function (data) {
                        errorHint();
                        //alert("faild:" + data);
                    });

        }, function () {
            // if click on No
        });

    };

    $scope.editModeCamp = function () {
        $scope.isEdit = true;
    };

    $scope.cancelEditModeCamp = function () {
        //  $scope.isEdit = false;
        var data = $("#remoteGalleries").select2('data');
        var tt = $scope.camp;
        modalNewCamps.close();
    };
    //----------------------Edit Camp---------------
    var editCamp = "editCamp";
    var modalEditCamps;
    var editCampScroll;
    $scope.editCamp = function (id) {
        var campid = id;
        if (!isExistElement(editCamp)) {
            getProvinces();
            preloaderfa.push();
            campsService.findCamp(campid)
                    .then(function (response) {
                        var data = response.data;
                        $scope.campDetails = data;

                        modalEditCamps = modalWindows(editCamp, 'ویرایش اردوگاه ', $compile("<div ng-include=\"'/Apps/Views/Camps/EditCamp.html'\"></div>")($scope));
                        modalEditCamps.resize("700px", "500px");
                        modalEditCamps.on("jspanelloaded", function (event, id) {
                            if (id === editCamp) {

                                editCampScroll = new IScroll("#editCampScroll", {
                                    bounceEasing: 'elastic',
                                    bounceTime: 1200,
                                    scrollbars: true,
                                    mouseWheel: true,
                                    interactiveScrollbars: true,
                                    shrinkScrollbars: 'scale',
                                    fadeScrollbars: false
                                });

                                preloaderfa.pop();
                                $scope.setMaps(
                                                $scope.campDetails.addressViewModel.longitude,
                                                $scope.campDetails.addressViewModel.latitude);

                            }
                        });



                    }, function (error) {
                        errorHintMessage("متاسفانه رکورد بارگذاری نشد");
                        preloaderfa.pop();

                    });


        }
    };

    //------------------------New Camp---------------
    $scope.tagsList = [];
    $scope.newCamp = function () {
        if (!isExistElement(newCamp)) {
            getProvinces();
            preloaderfa.push();

            galleryService.getAllGalleries()
                .then(function (response) {
                    $scope.tagsList = response.data;
                    modalNewCamps = modalWindows(newCamp, 'اردوگاه جدید', $compile("<div ng-include=\"'/Apps/Views/Camps/NewCamp.html'\"></div>")($scope));
                    modalNewCamps.resize("700px", "500px");
                    modalNewCamps.on("jspanelloaded", function (event, id) {
                        if (id === newCamp) {

                            newCampScroll = new IScroll("#scrollframe", {
                                bounceEasing: 'elastic',
                                bounceTime: 1200,
                                scrollbars: true,
                                mouseWheel: true,
                                interactiveScrollbars: true,
                                shrinkScrollbars: 'scale',
                                fadeScrollbars: false
                            });



                            $scope.setMaps(51.378936767578125, 35.71529801212529);
                            preloaderfa.pop();
                            //$scope.bindGalleries();

                        }
                    });

                }, function (error) {
                    preloaderfa.pop();
                    errorHintMessage("متاسفانه لیست گالری بارگذاری نشد");
                });




        }
    };
    $scope.reload = function () {
        campsService.notifyObservers();
    };
    //-------------------------   
    $scope.updateCamp = function (id, camp) {
        campsService.putCamp(id, camp)
        .success(function (data) {
            campsService.notifyObservers();
            successHint();
        }).error(function (data) {
            errorHintMessage("متاسفانه رکورد مورد نظر بروز رسانی نشد");
            //alert("faild:" + data);
        });
    };
    //-----------------------
    $scope.addNewCamp = function (camp) {
        campsService.postCamp(camp)
        .success(function (data) {
            campsService.notifyObservers();
            successHint();
        }).error(function (data) {
            errorHint();
            //alert("faild:" + data);
        });
    };
    //---------------------Route camp-------
    $scope.campGallery = function () {

        $location.url('/campGallery');
 
    }
    $scope.campGalleryDetails = function () {

        $location.url('/campGalleryDetails');
 
    }
    //---------------------details camp-------
 
    $scope.findCamp = function (id) {
        preloaderfa.push();
        campsService.findCamp(id)
                    .then(function (response) {
                        var data = response.data;
                        $scope.campDetails = data;
                        preloaderfa.pop();


                    }, function (error) {
                        errorHint();
                        preloaderfa.pop();

                    });
    }
    $scope.selectCategory = function (gallery) {
        $scope.gallery = gallery;
        $location.url('/campGalleryDetails');
    }
    $scope.detailCamp = function (id) {
        var campid = id;
        var detailsCamp = "detailsCamp";
        if (!isExistElement(detailsCamp)) {
            var modaldetailCamp = modalWindows(detailsCamp, ' اطلاعات کامل اردوگاه', $compile("<div ng-include=\"'/Apps/Views/Camps/detailsCamp.html'\"></div>")($scope));
            modaldetailCamp.resize("700px", "400px");
            modaldetailCamp.on("jspanelloaded", function (event, id) {
                if (id === detailsCamp) {
                    preloaderfa.push();
                    campsService.findCamp(campid)
                                .then(function (response) {
                                    var data = response.data;
                                    $scope.campDetails = data;
                                    preloaderfa.pop();

                                }, function (error) {
                                    errorHint();
                                    preloaderfa.pop();

                                });

                }
            });

        }
    };

    var thisCampScroll = new IScroll("#thisCampScroll", {
        bounceEasing: 'elastic',
        bounceTime: 1200,
        scrollbars: true,
        mouseWheel: true,
        interactiveScrollbars: true,
        shrinkScrollbars: 'scale',
        fadeScrollbars: false
    });

    //-------------bind galleries to exit element after form open-----

    $scope.tagsList = [];


    $scope.galleryOnChange = function () {
        newCampScroll.refresh();
    }

    //------------------------
    function loadData() {

        preloaderfa.push();


        var promisegetCamps = campsService.getCamps(0);
        promisegetCamps
            .then(function (response) {
                var data = response.data;
                $scope.campsList = data;
                preloaderfa.pop();
                thisCampScroll.refresh();
            }, function (error) {
                errorHint();
                preloaderfa.pop();
                thisCampScroll.refresh();
            });




    }
}

