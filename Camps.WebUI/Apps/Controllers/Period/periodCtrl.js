var periodCtrl = campsApp.controller('periodCtrl', ['$scope', '$route', '$compile', '$location', 'periodService', 'campsService', 'festivalService', 'preloaderfa', 'publiceService', periodCtrlfn]);


function periodCtrlfn($scope, $route, $compile, $location, periodService, campsService,festivalService, preloaderfa, publiceService) {

    $scope.pagesize = 20;
    $scope.skip = 0;

    $scope.pagesizeSpecific = 20;
    $scope.skipSpecific = 0;
    $scope.festivalid = 0;

    $scope.periodList = [];
    $scope.festivalList = [];
    $scope.specificPeriodList = [];
    $scope.campList = [];
    periodService.watchOberver(loadData);
    periodService.loadData();


    //------Require Common Objects---------------
    var modalNewPeriod, newPeriod = "newPeriod", newPeriodScroll, modalEditPeriod, editPeriod = "editPeriod";
    var modalSpecificPeriod, specificPeriod = "specificPeriod", specificPeriodScroll;
    var d = new Date();
    $scope.date1 = d.getDate();
    $scope.date_details =
    {
        formated: "2015/12/15",
        gDate: d.getDate(),
        //gDate is Date format of selected date in Gregorian calendar
        unix: 1450197600000,
        year: 2015,
        month: 12,
        day: 15,
        hour: 20,
        minute: 10,
        minDate: null,
        maxDate: null,
        calType: "gregorian",
        format: "YYYY/MM/DD"
    }
    $scope.$on('getSpecificPeriod', function (event, args) {
        $scope.festivalPeriodModal(args.id);
    });
    $scope.$on('newPeriod', function (event) {
        $scope.newModal();
    });
    //------------Prepare to open modal for new dept
    $scope.festivalPeriodModal = function (id) {
       
        if (!isExistElement(specificPeriod)) {
            preloaderfa.push();
 $scope.festivalid = id;
            modalSpecificPeriod = modalWindows(specificPeriod, 'دوره های جشنواره', $compile("<div ng-include=\"'/Apps/Views/Period/SpecificFestival.html'\"></div>")($scope));
            modalSpecificPeriod.resize("750px", "500px");
            modalSpecificPeriod.control("disable", "maximize");
            modalSpecificPeriod.on("jspanelloaded", function (event, id) {
                if (id === specificPeriod) {

                    preloaderfa.pop();
                    $scope.reloadSpecificData();
                    $scope.specificFestival();
                 
                    //$scope.bindGalleries();
                }
            });

        }
    };
    $scope.specificFestival = function () {
        preloaderfa.push();
        periodService.getPaging($scope.skipSpecific, $scope.pagesizeSpecific, $scope.festivalid)
                    .then(function (response) {
                        var data = response.data;
                        $scope.specificPeriodList = data;
                        $scope.skipSpecific += $scope.pagesizeSpecific;
                        preloaderfa.pop();
                        //     newperiodScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دوره ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newperiodScroll.refresh();
                    });

    }

    $scope.specificFestFetchData = function (skip, pageSize, festivalid) {
        // return deptService.getAlls(skip, pageSize);
        return periodService.getPaging(skip, pageSize, festivalid);

    }
    //------------Prepare to open modal for new dept
    $scope.newModal = function () {
        if (!isExistElement(newPeriod)) {
            preloaderfa.push();

            modalNewPeriod = modalWindows(newPeriod, 'دوره جدید  ', $compile("<div ng-include=\"'/Apps/Views/Period/NewPeriod.html'\"></div>")($scope));
            modalNewPeriod.resize("600px", "500px");
            modalNewPeriod.control("disable", "maximize");
            modalNewPeriod.on("jspanelloaded", function (event, id) {
                if (id === newPeriod) {

                    preloaderfa.pop();
                   
                    $scope.loadFestival();
                    $scope.loadCamp();
                    //$scope.bindGalleries();
                }
            });

        }
    };

    //------------Add new period function---------
    $scope.addNew = function (model) {
        periodService.post(model)
                .success(function (data) {
                    $scope.reloadData();
                    successHintMessage(" دوره جدید با موفقیت ثبت شد");
                    modalNewPeriod.close();
                }).error(function (data) {
                    errorHintMessage("متاسفانه دوره جدید ثبت نشد");
                    //alert("faild:" + data);
                });
    }
    $scope.loadCamp = function () {
        preloaderfa.push();
        campsService.getAllCamps()
                    .then(function (response) {
                        var data = response.data;
                        $scope.campList = data;
                        preloaderfa.pop();
                        //     newperiodScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست اردوگاه ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newperiodScroll.refresh();
                    });

    }
    $scope.loadFestival=function() {
        preloaderfa.push();
        festivalService.getPaging(0, 1000)
                    .then(function (response) {
                        var data = response.data;
                        $scope.festivalList = data;
                   
                        preloaderfa.pop();
                        //     newperiodScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست جشنواره ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newperiodScroll.refresh();
                    });
    }
    $scope.selectedFestival=function(id) {
        $scope.period.festivalId = id;
    }
    $scope.selectedCamp = function (id) {
        $scope.period.campId = id;
    }
    //-----------------------Delete dept
    $scope.delete = function (id) {
        var modalYesNo = modalConfirmYesNo("حذف رکورد", "آیا از حذف رکورد مطمئن هستید؟", function () {
            //if click on YES
            periodService.delete(id)
                    .then(function (data) {
                        $scope.reloadData();
                        findAndRemove($scope.specificPeriodList, "id", id);
                        successHint();
                    }, function (data) {
                        errorHint();

                    });

        }, function () {
            // if click on No
        });

    };
    //-----------------------Edit period modal open---------
    $scope.editPeriod = {
        fromDate: "",
        toDate: "",
        festivalId: 0,
        periodTitle: "",
        campId:0
    };
    $scope.openEditModal = function (model) {
        $scope.editPeriod = model;
        if (!isExistElement(editPeriod)) {
            preloaderfa.push();


            modalEditPeriod = modalWindows(editPeriod, 'ویرایش دوره ', $compile("<div ng-include=\"'/Apps/Views/Period/EditPeriod.html'\"></div>")($scope));
            modalEditPeriod.resize("600px", "500px");
            modalEditPeriod.control("disable", "maximize");
            modalEditPeriod.on("jspanelloaded", function (event, id) {
                if (id === editPeriod) {

                    preloaderfa.pop();
                    $scope.reloadSpecificData();
                    $scope.specificFestival();
                    $scope.loadFestival();
                }
            });

        }
    };
    //-----------------------Edit Dept---------
    $scope.editFn = function (model) {

        periodService.edit(model.id, model)
                .then(function (data) {
                    $scope.reloadSpecificData();
                    $scope.specificFestival();
                    $scope.loadFestival();
                    successHint();
                }, function (data) {
                    errorHint();

                });

    };
    //--------------Declare dept object
 
    $scope.period = {
        fromDate: "",
        toDate: "",
        festivalId: 0,
        periodTitle: "",
        campId:0
    }
    $scope.fromDate = null;
    $scope.toDate = null;
    //-------------cancel operation and close modal
    $scope.cancelNewModal = function () {
        if (modalNewPeriod) {
            modalNewPeriod.close();
        }
    }
    //-------------cancel operation and close modal
    $scope.cancelEditModal = function () {
        if (modalEditPeriod) {
            modalEditPeriod.close();
        }
    }
    //------------Common handeler-------
    $scope.fetchData = function (skip, pageSize) {
        // return deptService.getAlls(skip, pageSize);
        return periodService.getPaging(skip, pageSize);

    }
    $scope.reloadSpecificData = function () {
        $scope.specificPeriodList = [];
 
        $scope.pagesizeSpecific = 20;
        $scope.skipSpecific = 0;

        

    };
    $scope.reloadData = function () {
        $scope.skip = 0;
        $scope.periodList = [];
        periodService.loadData();

    };


    function loadData() {

        preloaderfa.push();
        periodService.getPaging($scope.skip, $scope.pagesize)
                    .then(function (response) {
                        var data = response.data;
                        $scope.periodList = data;
                        $scope.skip += $scope.pagesize;
                        preloaderfa.pop();
                        //     newperiodScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دوره ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newperiodScroll.refresh();
                    });




    }

};