var confirmQuotaCtrl = campsApp.controller('confirmQuotaCtrl', ['$scope', '$route', '$compile', '$location', 'deptService', 'periodService', 'confirmQuotaService', 'preloaderfa', 'publiceService', confirmQuotaCtrlfn]);


function confirmQuotaCtrlfn($scope, $route, $compile, $location, deptService, periodService, confirmQuotaService, preloaderfa, publiceService) {

    $scope.pagesize = 20;
    $scope.skip = 0;
    $scope.minLength = 3;

    $scope.confirmQuotaList = [];
 
 
    confirmQuotaService.watchOberver(loadData);
    confirmQuotaService.loadData();


    //------Require Common Objects---------------
    var modalNewConfirmQuota, newConfirmQuota = "newConfirmQuota", modalEditConfirmQuota, editConfirmQuota = "editConfirmQuota";

    var d = new Date();
    $scope.date1 = d.getDate();
 
    //------------Prepare to open modal for new dept
    $scope.openCancelReservationModal = function (id) {
        var modalYesNo = modalConfirmYesNo("انصراف از سهمیه", "آیا جهت انصراف از این سهمیه مطمئن هستید؟", function () {
            //if click on YES
            confirmQuotaService.refuse(id).success(function (data, status, headers) {
                $scope.reloadData();
                successHint();
            });
            //.error(function (data, status, header, config) {});
        }, function () {
            // if click on No
        });
    };
 
 
    //-----------------------Edit confirmQuota modal open---------
    $scope.editConfirmQuota = null;
    $scope.openEditModal = function (model) {
        $scope.editConfirmQuota = model;
        if (!isExistElement(editConfirmQuota)) {
            preloaderfa.push();


            modalEditConfirmQuota = modalWindows(editConfirmQuota, 'ثبت سهمیه   ', $compile("<div ng-include=\"'/Apps/Views/ConfirmQuota/EditConfirmQuota.html'\"></div>")($scope));
            modalEditConfirmQuota.resize("600px", "500px");
            modalEditConfirmQuota.control("disable", "maximize");
            modalEditConfirmQuota.on("jspanelloaded", function (event, id) {
                if (id === editConfirmQuota) {

                    preloaderfa.pop();
                  

                }
            });

        }
    };
    //-----------------------Edit Dept---------
    $scope.editFn = function (model) {

        confirmQuotaService.edit(model.id, model)
                .then(function (data) {
                  
                    $scope.reloadData();
                    successHint();
                }, function (data) {
                    errorHint();

                });

    };
    //--------------Declare dept object
    $scope.confirmQuota = {}


 
    //-------------cancel operation and close modal
    $scope.cancelEditModal = function () {
        if (modalEditConfirmQuota) {
            modalEditConfirmQuota.close();
        }
    }
    //------------Common handeler-------
    $scope.fetchData = function (skip, pageSize) {
        // return deptService.getAlls(skip,p pageSize);
        return confirmQuotaService.getPaging(skip, pageSize);

    }
    $scope.selectedfn = function (data) {
        // return deptService.getAlls(skip, pageSize);
        $scope.editConfirmQuota.passengerUserId = data.params.data.id;

    }
    $scope.reloadData = function () {
        $scope.skip = 0;
        $scope.confirmQuotaList = [];
        confirmQuotaService.loadData();

    };
 
    function loadData() {

        preloaderfa.push();
        confirmQuotaService.getPaging($scope.skip, $scope.pagesize)
                    .then(function (response) {
                        var data = response.data;
                        $scope.confirmQuotaList = data;
                        $scope.skip += $scope.pagesize;
                        preloaderfa.pop();
                        //     newconfirmQuotaScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست سهمیه  ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newconfirmQuotaScroll.refresh();
                    });
    }

};