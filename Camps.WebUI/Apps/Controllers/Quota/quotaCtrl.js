var quotaCtrl = campsApp.controller('quotaCtrl', ['$scope', '$route', '$compile', '$location', 'deptService', 'periodService', 'quotaService', 'preloaderfa', 'publiceService', quotaCtrlfn]);


function quotaCtrlfn($scope, $route, $compile, $location, deptService, periodService, quotaService, preloaderfa, publiceService) {

    $scope.pagesize = 20;
    $scope.skip = 0;
    $scope.minLength = 3;

    $scope.quotaList = [];
    $scope.periodList = [];
    $scope.periodEditList = [];
    $scope.deptList = [];
    $scope.deptEditList = [];
    $scope.speceficFestivalPeriod = 0;
    quotaService.watchOberver(loadData);
    quotaService.loadData();


    //------Require Common Objects---------------
    var modalNewQuota, newQuota = "newQuota", newQuotaScroll, modalEditQuota, editQuota = "editQuota";

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
    $scope.getSpeceficPeriod = function (id) {

        $scope.$broadcast('getSpecificPeriod', { id: id });


    }
    //------------Prepare to open modal for new dept
    $scope.newModal = function () {
        if (!isExistElement(newQuota)) {
            preloaderfa.push();

            modalNewQuota = modalWindows(newQuota, 'سهمیه  جدید  ', $compile("<div ng-include=\"'/Apps/Views/Quota/NewQuota.html'\"></div>")($scope));
            modalNewQuota.resize("600px", "500px");
            modalNewQuota.control("disable", "maximize");
            modalNewQuota.on("jspanelloaded", function (event, id) {
                if (id === newQuota) {

                    preloaderfa.pop();
                    $scope.loadDept();
                    //$scope.bindGalleries();
                }
            });

        }
    };
    $scope.newPeriodModal = function () {
        $scope.$broadcast('newPeriod');
    };

    //------------Add new Quota function---------
    $scope.addNew = function (model) {
        quotaService.post(model)
                .success(function (data) {
                    $scope.reloadData();
                    successHintMessage(" سهمیه  جدید با موفقیت ثبت شد");
                    modalNewQuota.close();
                }).error(function (data) {
                    errorHintMessage("متاسفانه سهمیه  جدید ثبت نشد");
                    //alert("faild:" + data);
                });
    }
    //-----------------------Delete dept
    $scope.delete = function (id) {
        var modalYesNo = modalConfirmYesNo("حذف رکورد", "آیا از حذف رکورد مطمئن هستید؟", function () {
            //if click on YES
            quotaService.delete(id)
                    .then(function (data) {
                        $scope.reloadData();
                        findAndRemove($scope.quotaList, "id", id);
                        successHint();
                    }, function (data) {
                        errorHint();

                    });

        }, function () {
            // if click on No
        });

    };
    //-----------------------Edit quota modal open---------
    $scope.editQuota = null;
    $scope.openEditModal = function (model) {
        $scope.editQuota = model;
        if (!isExistElement(editQuota)) {
            preloaderfa.push();


            modalEditQuota = modalWindows(editQuota, 'ویرایش سهمیه   ', $compile("<div ng-include=\"'/Apps/Views/Quota/EditQuota.html'\"></div>")($scope));
            modalEditQuota.resize("600px", "500px");
            modalEditQuota.control("disable", "maximize");
            modalEditQuota.on("jspanelloaded", function (event, id) {
                if (id === editQuota) {

                    preloaderfa.pop();
                    $scope.loadEditDept();

                }
            });

        }
    };
    //-----------------------Edit Dept---------
    $scope.editFn = function (model) {

        quotaService.edit(model.id, model)
                .then(function (data) {
                    $scope.reloadData();
                    successHint();
                }, function (data) {
                    errorHint();

                });

    };
    //--------------Declare dept object
    $scope.quota = {
        addDate: new Date(),
        deadLineTime: "",
        periodId: 0,
        departmentId: 0
    }


    //-------------cancel operation and close modal
    $scope.cancelNewModal = function () {
        if (modalNewQuota) {
            modalNewQuota.close();
        }
    }
    //-------------cancel operation and close modal
    $scope.cancelEditModal = function () {
        if (modalEditQuota) {
            modalEditQuota.close();
        }
    }
    //------------Common handeler-------
    $scope.fetchData = function (skip, pageSize) {
        // return deptService.getAlls(skip, pageSize);
        return quotaService.getPaging(skip, pageSize);

    }
    $scope.selectedfn = function (data) {
        // return deptService.getAlls(skip, pageSize);
        var item = data;

    }
    $scope.selectedPeriodfn = function (id) {

        $scope.quota.periodId = id;


    }
    $scope.selectedEditPeriodfn = function (id) {
        $scope.editQuota.periodId = id;

    }
    $scope.loadDept = function () {
        preloaderfa.push();
        deptService.getAlls()
                    .then(function (response) {
                        var data = response.data;
                        $scope.deptList = data;
                        preloaderfa.pop();
                        return data;
                        //     newquotaScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دپارتمان ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newquotaScroll.refresh();
                    });
    }
    $scope.loadEditDept = function () {
        preloaderfa.push();
        deptService.getAlls()
                    .then(function (response) {
                        var data = response.data;
                        $scope.deptEditList = data;
                        preloaderfa.pop();

                        //     newquotaScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دپارتمان ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newquotaScroll.refresh();
                    });
    }
    $scope.selectedFestivalfn = function (data) {
        // return deptService.getAlls(skip, pageSize);
        preloaderfa.push();
        periodService.getByFestival(data.params.data.id)
                    .then(function (response) {
                        var data = response.data;
                        $scope.periodList = data;

                        preloaderfa.pop();
                        //     newquotaScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دوره ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newquotaScroll.refresh();
                    });
    }
    $scope.selectedEditFestivalfn = function (data) {
        // return deptService.getAlls(skip, pageSize);
        preloaderfa.push();
        periodService.getByFestival(data.params.data.id)
                    .then(function (response) {
                        var data = response.data;
                        $scope.periodEditList = data;

                        preloaderfa.pop();
                        //     newquotaScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دوره ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newquotaScroll.refresh();
                    });
    }

    $scope.selectedDeptfn = function (id) {
        $scope.quota.departmentId = id;
    }
    $scope.selectedEditDeptfn = function (id) {

        $scope.editQuota.departmentId = id;
    }
    $scope.reloadData = function () {
        $scope.skip = 0;
        $scope.quotaList = [];
        quotaService.loadData();

    };



    function loadData() {

        preloaderfa.push();
        quotaService.getPaging($scope.skip, $scope.pagesize)
                    .then(function (response) {
                        var data = response.data;
                        $scope.quotaList = data;
                        $scope.skip += $scope.pagesize;
                        preloaderfa.pop();
                        //     newquotaScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست سهمیه  ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newquotaScroll.refresh();
                    });
    }

};