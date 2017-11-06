var campsCtrl = campsApp.controller('deptCtrl', ['$scope', '$route', '$compile', '$location', 'deptService','preloaderfa', deptCtrlfn]);


function deptCtrlfn($scope, $route, $compile, $location, deptService,preloaderfa) {

    $scope.pagesize = 20;
    $scope.skip = 0;
    $scope.deptList = [];
    deptService.watchOberver(loadData);
    deptService.loadData();


    //------Require Common Objects---------------
    var modalNewDept, newDept = "newDept", newDeptScroll, modalEditDept, editDept = "editDept";


    $scope.getSpecificDepartmentDeptRole = function (id) {

        $scope.$broadcast('getSpecificDepartmentDeptRole', { id: id });


    }
    $scope.newDepartmentDeptRoleModal = function () {
        $scope.$broadcast('newDepartmentDeptRole');
    };
    //------------Prepare to open modal for new dept
    $scope.newDeptModal = function () {
        if (!isExistElement(newDept)) {
            preloaderfa.push();
 
            modalNewDept = modalWindows(newDept, 'دپارتمان جدید', $compile("<div ng-include=\"'/Apps/Views/Department/NewDept.html'\"></div>")($scope));
            modalNewDept.resize("300px", "120px");
            modalNewDept.control("disable", "maximize");
            modalNewDept.on("jspanelloaded", function (event, id) {
                if (id === newDept) {

                    newDeptScroll = new IScroll("#scrollnewdeptframe", {
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
    $scope.addNewDept = function (model) {
        deptService.postNewDept(model)
                .success(function (data) {
                    $scope.reloadData();
                    successHintMessage("دپارتمان جدید با موفقیت ثبت شد");
                    modalNewDept.close();
                }).error(function (data) {
                    errorHintMessage("متاسفانه دپارتمان جدید ثبت نشد");
                    //alert("faild:" + data);
                });
    }
    //-----------------------Delete dept
    $scope.deleteDept = function (id) {
        var modalYesNo = modalConfirmYesNo("حذف رکورد", "آیا از حذف رکورد مطمئن هستید؟", function () {
            //if click on YES
            deptService.deleteDept(id)
                    .then(function (data) {
                        $scope.reloadData();
                        findAndRemove($scope.deptList, "id", id);
                        successHint();
                    }, function (data) {
                        errorHint();

                    });

        }, function () {
            // if click on No
        });

    };
    //-----------------------Edit suite modal open---------
    $scope.editDept = null;
    $scope.openEditDeptModal = function (model) {
        $scope.editDept = model;
        if (!isExistElement(editDept)) {
            preloaderfa.push();
  

            modalEditDept = modalWindows(editDept, 'ویرایش دپارتمان ', $compile("<div ng-include=\"'/Apps/Views/Department/EditDept.html'\"></div>")($scope));
            modalEditDept.resize("300px", "120px");
            modalEditDept.control("disable", "maximize");
            modalEditDept.on("jspanelloaded", function (event, id) {
                if (id === editDept) {

                    preloaderfa.pop();
                    //$scope.bindGalleries();

                }
            });

        }
    };
    //-----------------------Edit Dept---------
    $scope.editDeptFn = function (model) {

        deptService.editDept(model.id, model)
                .then(function (data) {
                    $scope.reloadData();
                    successHint();
                }, function (data) {
                    errorHint();

                });

    };
    //--------------Declare dept object
    $scope.dept = {
        DepTitle: ""
    }
    //-------------cancel operation and close modal
    $scope.cancelNewDeptModal=function() {
        if (modalNewDept) {
            modalNewDept.close();
        }
    }
    //-------------cancel operation and close modal
    $scope.cancelEditDeptModal=function() {
        if (modalEditDept) {
            modalEditDept.close();
        }
    }
    //------------Common handeler-------
    $scope.fetchData = function (skip, pageSize) {
        // return deptService.getAlls(skip, pageSize);
        return deptService.getDepts(skip, pageSize);

    }
    $scope.reloadData = function () {
        $scope.skip = 0;
        $scope.deptList = [];
        deptService.loadData();

    };
    function loadData() {

        preloaderfa.push();
        deptService.getDepts($scope.skip, $scope.pagesize)
                    .then(function (response) {
                        var data = response.data;
                        $scope.deptList = data;
                        $scope.skip += $scope.pagesize;
                        preloaderfa.pop();
                        //     newSuiteScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دپارتمان ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newSuiteScroll.refresh();
                    });




    }

};