var deptRoleCtrl = campsApp.controller('deptRoleCtrl', ['$scope', '$route', '$compile', '$location', 'deptRoleService', 'preloaderfa', deptCtrlfn]);


function deptCtrlfn($scope, $route, $compile, $location, deptRoleService, preloaderfa) {

    $scope.pagesize = 20;
    $scope.skip = 0;
    $scope.deptRoleList = [];
    deptRoleService.watchOberver(loadData);
    deptRoleService.loadData();


    //------Require Common Objects---------------
    var modalNewDeptRole, newDeptRole = "newDeptRole", newDeptRoleScroll, modalEditDeptRole, editDeptRole = "editDeptRole";




    //------------Prepare to open modal for new dept
    $scope.newModal = function () {
        if (!isExistElement(newDeptRole)) {
            preloaderfa.push();

            modalNewDeptRole = modalWindows(newDeptRole, 'پست های ', $compile("<div ng-include=\"'/Apps/Views/DeptRole/NewDeptRole.html'\"></div>")($scope));
            modalNewDeptRole.resize("300px", "120px");
            modalNewDeptRole.control("disable", "maximize");
            modalNewDeptRole.on("jspanelloaded", function (event, id) {
                if (id === newDeptRole) {

                    newDeptRoleScroll = new IScroll("#scrollnewdeptroleframe", {
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
    $scope.addNew = function (model) {
        deptRoleService.post(model)
                .success(function (data) {
                    $scope.reloadData();
                    successHintMessage("پست جدید با موفقیت ثبت شد");
                    modalNewDeptRole.close();
                }).error(function (data) {
                    errorHintMessage("متاسفانه پست جدید ثبت نشد");
                    //alert("faild:" + data);
                });
    }
    //-----------------------Delete dept
    $scope.delete = function (id) {
        var modalYesNo = modalConfirmYesNo("حذف رکورد", "آیا از حذف رکورد مطمئن هستید؟", function () {
            //if click on YES
            deptRoleService.delete(id)
                    .then(function (data) {
                        $scope.reloadData();
                        findAndRemove($scope.deptRoleList, "id", id);
                        successHint();
                    }, function (data) {
                        errorHint();

                    });

        }, function () {
            // if click on No
        });

    };
    //-----------------------Edit suite modal open---------
    $scope.editDeptRole = null;
    $scope.openEditModal = function (model) {
        $scope.editDeptRole = model;
        if (!isExistElement(editDeptRole)) {
            preloaderfa.push();


            modalEditDeptRole = modalWindows(editDeptRole, 'ویرایش عنوان پست ', $compile("<div ng-include=\"'/Apps/Views/DeptRole/EditDeptRole.html'\"></div>")($scope));
            modalEditDeptRole.resize("300px", "120px");
            modalEditDeptRole.control("disable", "maximize");
            modalEditDeptRole.on("jspanelloaded", function (event, id) {
                if (id === editDeptRole) {

                    preloaderfa.pop();
                    //$scope.bindGalleries();

                }
            });

        }
    };
    //-----------------------Edit Dept---------
    $scope.editFn = function (model) {

        deptRoleService.edit(model.id, model)
                .then(function (data) {
                    $scope.reloadData();
                    successHint();
                }, function (data) {
                    errorHint();

                });

    };
    //--------------Declare dept object
    $scope.deptRole = {
        RoleTitle: ""
    }
    //-------------cancel operation and close modal
    $scope.cancelNewModal = function () {
        if (modalNewDeptRole) {
            modalNewDeptRole.close();
        }
    }
    //-------------cancel operation and close modal
    $scope.cancelEditModal = function () {
        if (modalEditDeptRole) {
            modalEditDeptRole.close();
        }
    }
    //------------Common handeler-------
    $scope.fetchData = function (skip, pageSize) {
        // return deptService.getAlls(skip, pageSize);
        return deptRoleService.getPaging(skip, pageSize);

    }
    $scope.reloadData = function () {
        $scope.skip = 0;
        $scope.deptRoleList = [];
        deptRoleService.loadData();

    };
    function loadData() {

        preloaderfa.push();
        deptRoleService.getPaging($scope.skip, $scope.pagesize)
                    .then(function (response) {
                        var data = response.data;
                        $scope.deptRoleList = data;
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