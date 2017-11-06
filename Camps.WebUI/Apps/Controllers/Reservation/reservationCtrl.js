var reservationCtrl = campsApp.controller('reservationCtrl', ['$scope', '$route', '$compile', '$location', 'deptService', 'periodService', 'reservationService', 'preloaderfa', 'publiceService', reservationCtrlfn]);


function reservationCtrlfn($scope, $route, $compile, $location, deptService, periodService, reservationService, preloaderfa, publiceService) {

    $scope.pagesize = 20;
    $scope.skip = 0;
    $scope.minLength = 3;

    $scope.reservationList = [];
    $scope.periodList = [];
    $scope.periodEditList = [];
    $scope.deptList = [];
    $scope.deptEditList = [];
    $scope.speceficFestivalPeriod = 0;
    reservationService.watchOberver(loadData);
    reservationService.loadData();


    //------Require Common Objects---------------
    var modalNewReservation, newReservation = "newReservation", newReservationScroll, modalEditReservation, editReservation = "editReservation";

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
        if (!isExistElement(newReservation)) {
            preloaderfa.push();

            modalNewReservation = modalWindows(newReservation, 'سهمیه  جدید  ', $compile("<div ng-include=\"'/Apps/Views/Reservation/NewReservation.html'\"></div>")($scope));
            modalNewReservation.resize("600px", "500px");
            modalNewReservation.control("disable", "maximize");
            modalNewReservation.on("jspanelloaded", function (event, id) {
                if (id === newReservation) {

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

    //------------Add new Reservation function---------
    $scope.addNew = function (model) {
        reservationService.post(model)
                .success(function (data) {
                    $scope.reloadData();
                    successHintMessage(" سهمیه  جدید با موفقیت ثبت شد");
                    modalNewReservation.close();
                }).error(function (data) {
                    errorHintMessage("متاسفانه سهمیه  جدید ثبت نشد");
                    //alert("faild:" + data);
                });
    }
    //-----------------------Delete dept
    $scope.delete = function (id) {
        var modalYesNo = modalConfirmYesNo("حذف رکورد", "آیا از حذف رکورد مطمئن هستید؟", function () {
            //if click on YES
            reservationService.delete(id)
                    .then(function (data) {
                        $scope.reloadData();
                        findAndRemove($scope.reservationList, "id", id);
                        successHint();
                    }, function (data) {
                        errorHint();

                    });

        }, function () {
            // if click on No
        });

    };
    //-----------------------Edit reservation modal open---------
    $scope.editReservation = null;
    $scope.openEditModal = function (model) {
        $scope.editReservation = model;
        if (!isExistElement(editReservation)) {
            preloaderfa.push();


            modalEditReservation = modalWindows(editReservation, 'ویرایش سهمیه   ', $compile("<div ng-include=\"'/Apps/Views/Reservation/EditReservation.html'\"></div>")($scope));
            modalEditReservation.resize("600px", "500px");
            modalEditReservation.control("disable", "maximize");
            modalEditReservation.on("jspanelloaded", function (event, id) {
                if (id === editReservation) {

                    preloaderfa.pop();
                    $scope.loadEditDept();

                }
            });

        }
    };
    //-----------------------Edit Dept---------
    $scope.editFn = function (model) {

        reservationService.edit(model.id, model)
                .then(function (data) {
                    $scope.reloadData();
                    successHint();
                }, function (data) {
                    errorHint();

                });

    };
    //--------------Declare dept object
    $scope.reservation = {
        addDate: new Date(),
        deadLineTime: "",
        periodId: 0,
        departmentId: 0
    }


    //-------------cancel operation and close modal
    $scope.cancelNewModal = function () {
        if (modalNewReservation) {
            modalNewReservation.close();
        }
    }
    //-------------cancel operation and close modal
    $scope.cancelEditModal = function () {
        if (modalEditReservation) {
            modalEditReservation.close();
        }
    }
    //------------Common handeler-------
    $scope.fetchData = function (skip, pageSize) {
        // return deptService.getAlls(skip, pageSize);
        return reservationService.getPaging(skip, pageSize);

    }
    $scope.selectedfn = function (data) {
        // return deptService.getAlls(skip, pageSize);
        var item = data;

    }
    $scope.selectedPeriodfn = function (id) {

        $scope.reservation.periodId = id;


    }
    $scope.selectedEditPeriodfn = function (id) {
        $scope.editReservation.periodId = id;

    }
    $scope.loadDept = function () {
        preloaderfa.push();
        deptService.getAlls()
                    .then(function (response) {
                        var data = response.data;
                        $scope.deptList = data;
                        preloaderfa.pop();
                        return data;
                        //     newreservationScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دپارتمان ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newreservationScroll.refresh();
                    });
    }
    $scope.loadEditDept = function () {
        preloaderfa.push();
        deptService.getAlls()
                    .then(function (response) {
                        var data = response.data;
                        $scope.deptEditList = data;
                        preloaderfa.pop();

                        //     newreservationScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دپارتمان ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newreservationScroll.refresh();
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
                        //     newreservationScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دوره ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newreservationScroll.refresh();
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
                        //     newreservationScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دوره ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newreservationScroll.refresh();
                    });
    }

    $scope.selectedDeptfn = function (id) {
        $scope.reservation.departmentId = id;
    }
    $scope.selectedEditDeptfn = function (id) {

        $scope.editReservation.departmentId = id;
    }
    $scope.reloadData = function () {
        $scope.skip = 0;
        $scope.reservationList = [];
        reservationService.loadData();

    };
    $scope.convertCalendar1 = function (date) {
        if (date) {
            $scope.reservation.deadLineTime = toJalali(date);
        }
    };
    $scope.convertEditCalendar1 = function (date) {
        if (date) {
            $scope.editReservation.deadLineTime = toJalali(date);;
        }
    };

    function loadData() {

        preloaderfa.push();
        reservationService.getPaging($scope.skip, $scope.pagesize)
                    .then(function (response) {
                        var data = response.data;
                        $scope.reservationList = data;
                        $scope.skip += $scope.pagesize;
                        preloaderfa.pop();
                        //     newreservationScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست سهمیه  ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newreservationScroll.refresh();
                    });
    }

};