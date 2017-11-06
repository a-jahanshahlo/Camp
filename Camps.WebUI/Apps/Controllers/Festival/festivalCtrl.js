var festivalCtrl = campsApp.controller('festivalCtrl', ['$scope', '$route', '$compile', '$location', 'festivalService', 'preloaderfa', 'publiceService', festivalCtrlfn]);


function festivalCtrlfn($scope, $route, $compile, $location, festivalService, preloaderfa, publiceService) {

    $scope.pagesize = 20;
    $scope.skip = 0;
    $scope.festivalList = [];
    $scope.speceficFestivalPeriod = 0;
    festivalService.watchOberver(loadData);
    festivalService.loadData();


    //------Require Common Objects---------------
    var modalNewFestival, newFestival = "newFestival", newFestivalScroll, modalEditFestival, editFestival = "editFestival";
  
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
        if (!isExistElement(newFestival)) {
            preloaderfa.push();

            modalNewFestival = modalWindows(newFestival, 'دوره جدید  ', $compile("<div ng-include=\"'/Apps/Views/Festival/NewFestival.html'\"></div>")($scope));
            modalNewFestival.resize("600px", "500px");
            modalNewFestival.control("disable", "maximize");
            modalNewFestival.on("jspanelloaded", function (event, id) {
                if (id === newFestival) {

                    preloaderfa.pop();

                    //$scope.bindGalleries();
                }
            });

        }
    };
    $scope.newPeriodModal = function () {
        $scope.$broadcast('newPeriod');
    };
 
    //------------Add new Festival function---------
    $scope.addNew = function (model) {
        festivalService.post(model)
                .success(function (data) {
                    $scope.reloadData();
                    successHintMessage(" جشنواره جدید با موفقیت ثبت شد");
                    modalNewFestival.close();
                }).error(function (data) {
                    errorHintMessage("متاسفانه جشنواره جدید ثبت نشد");
                    //alert("faild:" + data);
                });
    }
    //-----------------------Delete dept
    $scope.delete = function (id) {
        var modalYesNo = modalConfirmYesNo("حذف رکورد", "آیا از حذف رکورد مطمئن هستید؟", function () {
            //if click on YES
            festivalService.delete(id)
                    .then(function (data) {
                        $scope.reloadData();
                        findAndRemove($scope.festivalList, "id", id);
                        successHint();
                    }, function (data) {
                        errorHint();

                    });

        }, function () {
            // if click on No
        });

    };
    //-----------------------Edit festival modal open---------
    $scope.editFestival = {
        fromDate: "",
        toDate: "",
        isActive: "",
        festivalTitle: ""
    };
    $scope.openEditModal = function (model) {
        $scope.editFestival = model;
        if (!isExistElement(editFestival)) {
            preloaderfa.push();


            modalEditFestival = modalWindows(editFestival, 'ویرایش جشنواره  ', $compile("<div ng-include=\"'/Apps/Views/Festival/EditFestival.html'\"></div>")($scope));
            modalEditFestival.resize("600px", "500px");
            modalEditFestival.control("disable", "maximize");
            modalEditFestival.on("jspanelloaded", function (event, id) {
                if (id === editFestival) {

                    preloaderfa.pop();
                    //$scope.bindGalleries();

                }
            });

        }
    };
    //-----------------------Edit Dept---------
    $scope.editFn = function (model) {

        festivalService.edit(model.id, model)
                .then(function (data) {
                    $scope.reloadData();
                    successHint();
                }, function (data) {
                    errorHint();

                });

    };
    //--------------Declare dept object
    $scope.festival = {
        fromDate: "",
        toDate: "",
        isActive: "",
        festivalTitle: ""
    }

 
    //-------------cancel operation and close modal
    $scope.cancelNewModal = function () {
        if (modalNewFestival) {
            modalNewFestival.close();
        }
    }
    //-------------cancel operation and close modal
    $scope.cancelEditModal = function () {
        if (modalEditFestival) {
            modalEditFestival.close();
        }
    }
    //------------Common handeler-------
    $scope.fetchData = function (skip, pageSize) {
        // return deptService.getAlls(skip, pageSize);
        return festivalService.getPaging(skip, pageSize);

    }
    $scope.reloadData = function () {
        $scope.skip = 0;
        $scope.festivalList = [];
        festivalService.loadData();

    };


 
    function loadData() {

        preloaderfa.push();
        festivalService.getPaging($scope.skip, $scope.pagesize)
                    .then(function (response) {
                        var data = response.data;
                        $scope.festivalList = data;
                        $scope.skip += $scope.pagesize;
                        preloaderfa.pop();
                        //     newfestivalScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دوره ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newfestivalScroll.refresh();
                    });




    }

};