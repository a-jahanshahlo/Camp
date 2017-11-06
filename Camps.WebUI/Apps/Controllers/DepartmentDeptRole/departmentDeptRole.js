var departmentDeptRoleCtrl = campsApp.controller('departmentDeptRoleCtrl', ['$scope', '$route', '$compile', '$location', 'deptRoleService', 'departmentDeptRoleService', 'deptService', 'preloaderfa', 'publiceService', departmentDeptRoleCtrlfn]);


function departmentDeptRoleCtrlfn($scope, $route, $compile, $location,deptRoleService, departmentDeptRoleService,  deptService, preloaderfa, publiceService) {

    $scope.pagesize = 20;
    $scope.skip = 0;

    $scope.pagesizeSpecific = 20;
    $scope.skipSpecific = 0;
    $scope.deptid = 0;

    $scope.departmentDeptRoleList = [];
    $scope.deptList = [];
    $scope.specificDepartmentDeptRoleList = [];
    $scope.deptRoleList = [];
    departmentDeptRoleService.watchOberver(loadData);
    departmentDeptRoleService.loadData();


    //------Require Common Objects---------------
    var modalNewDepartmentDeptRole, newDepartmentDeptRole = "newDepartmentDeptRole", modalEditDepartmentDeptRole, editDepartmentDeptRole = "editDepartmentDeptRole";
    var modalSpecificDepartmentDeptRole, specificDepartmentDeptRole = "specificDepartmentDeptRole";
 
 
    $scope.$on('getSpecificDepartmentDeptRole', function (event, args) {
        $scope.deptDepartmentDeptRoleModal(args.id);
    });
    $scope.$on('newDepartmentDeptRole', function (event) {
        $scope.newModal();
    });
    //------------Prepare to open modal for new dept
    $scope.deptDepartmentDeptRoleModal = function (id) {

        if (!isExistElement(specificDepartmentDeptRole)) {
            preloaderfa.push();
            $scope.deptid = id;
            modalSpecificDepartmentDeptRole = modalWindows(specificDepartmentDeptRole, 'پست های دپارتمان', $compile("<div ng-include=\"'/Apps/Views/DepartmentDeptRole/SpecificDepartmentDeptRole.html'\"></div>")($scope));
            modalSpecificDepartmentDeptRole.resize("750px", "500px");
            modalSpecificDepartmentDeptRole.control("disable", "maximize");
            modalSpecificDepartmentDeptRole.on("jspanelloaded", function (event, id) {
                if (id === specificDepartmentDeptRole) {

                    preloaderfa.pop();
                    $scope.reloadSpecificData();
                    $scope.specificDept();

                    //$scope.bindGalleries();
                }
            });

        }
    };
    $scope.specificDept = function () {
        preloaderfa.push();
        departmentDeptRoleService.getPaging($scope.skipSpecific, $scope.pagesizeSpecific, $scope.deptid)
                    .then(function (response) {
                        var data = response.data;
                        $scope.specificDepartmentDeptRoleList = data;
                        $scope.skipSpecific += $scope.pagesizeSpecific;
                        preloaderfa.pop();
                        //     newdepartmentDeptRoleScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست پست ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newdepartmentDeptRoleScroll.refresh();
                    });

    }

    $scope.specificDeptFetchData = function (skip, pageSize, deptid) {
        // return deptService.getAlls(skip, pageSize);
        return departmentDeptRoleService.getPaging(skip, pageSize, deptid);

    }
    //------------Prepare to open modal for new dept
    $scope.newModal = function () {
        if (!isExistElement(newDepartmentDeptRole)) {
            preloaderfa.push();

            modalNewDepartmentDeptRole = modalWindows(newDepartmentDeptRole, 'پست جدید  ', $compile("<div ng-include=\"'/Apps/Views/DepartmentDeptRole/NewDepartmentDeptRole.html'\"></div>")($scope));
            modalNewDepartmentDeptRole.resize("600px", "400px");
            modalNewDepartmentDeptRole.control("disable", "maximize");
            modalNewDepartmentDeptRole.on("jspanelloaded", function (event, id) {
                if (id === newDepartmentDeptRole) {

                    preloaderfa.pop();

                    $scope.loadDept();
                    $scope.loadDeptRole();
                    //$scope.bindGalleries();
                }
            });

        }
    };

    //------------Add new departmentDeptRole function---------
    $scope.addNew = function (model) {
        departmentDeptRoleService.post(model)
                .success(function (data) {
                    $scope.reloadData();
                    successHintMessage(" پست جدید با موفقیت ثبت شد");
                    modalNewDepartmentDeptRole.close();
                }).error(function (data) {
                    errorHintMessage("متاسفانه پست جدید ثبت نشد");
                    //alert("faild:" + data);
                });
    }

    $scope.loadDept = function () {
        preloaderfa.push();
        deptService.getPaging(0, 1000)
                    .then(function (response) {
                        var data = response.data;
                        $scope.deptList = data;

                        preloaderfa.pop();
                        //     newdepartmentDeptRoleScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دپارتمان ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newdepartmentDeptRoleScroll.refresh();
                    });
    }
    $scope.loadDeptRole = function () {
        preloaderfa.push();
        deptRoleService.getPaging(0, 1000)
                    .then(function (response) {
                        var data = response.data;
                        $scope.deptRoleList = data;

                        preloaderfa.pop();
                        //     newdepartmentDeptRoleScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست نقش ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newdepartmentDeptRoleScroll.refresh();
                    });
    }
    $scope.selectedDept = function (id) {
        $scope.departmentDeptRole.departmentId = id;
    }
    $scope.selectedDeptRole = function (id) {
        $scope.departmentDeptRole.deptRoleId = id;
    }
    //-----------------------Delete dept
    $scope.delete = function (id) {
        var modalYesNo = modalConfirmYesNo("حذف رکورد", "آیا از حذف رکورد مطمئن هستید؟", function () {
            //if click on YES
            departmentDeptRoleService.delete(id)
                    .then(function (data) {
                        $scope.reloadData();
                        findAndRemove($scope.specificDepartmentDeptRoleList, "id", id);
                        successHint();
                    }, function (data) {
                        errorHint();

                    });

        }, function () {
            // if click on No
        });

    };
    //-----------------------Edit departmentDeptRole modal open---------
    $scope.editDepartmentDeptRole = {
        departmentId: -1,
        deptRoleId: -1,
        isActive: true
    };
    $scope.openEditModal = function (model) {
        $scope.editDepartmentDeptRole = model;
        if (!isExistElement(editDepartmentDeptRole)) {
            preloaderfa.push();


            modalEditDepartmentDeptRole = modalWindows(editDepartmentDeptRole, 'ویرایش دوره ', $compile("<div ng-include=\"'/Apps/Views/DepartmentDeptRole/EditDepartmentDeptRole.html'\"></div>")($scope));
            modalEditDepartmentDeptRole.resize("600px", "500px");
            modalEditDepartmentDeptRole.control("disable", "maximize");
            modalEditDepartmentDeptRole.on("jspanelloaded", function (event, id) {
                if (id === editDepartmentDeptRole) {

                    preloaderfa.pop();
                    $scope.reloadSpecificData();
                    $scope.specificDept();
                    $scope.loadDept();
                }
            });

        }
    };
    //-----------------------Edit Dept---------
    $scope.editFn = function (model) {

        departmentDeptRoleService.edit(model.id, model)
                .then(function (data) {
                    $scope.reloadSpecificData();
                    $scope.specificDept();
                    $scope.loadDept();
                    successHint();
                }, function (data) {
                    errorHint();

                });

    };
    //--------------Declare dept object

    $scope.departmentDeptRole = {
        departmentId: -1,
        deptRoleId: -1,
        isActive: true
    }
 
    //-------------cancel operation and close modal
    $scope.cancelNewModal = function () {
        if (modalNewDepartmentDeptRole) {
            modalNewDepartmentDeptRole.close();
        }
    }
    //-------------cancel operation and close modal
    $scope.cancelEditModal = function () {
        if (modalEditDepartmentDeptRole) {
            modalEditDepartmentDeptRole.close();
        }
    }
    //------------Common handeler-------
    $scope.fetchData = function (skip, pageSize) {
        // return deptService.getAlls(skip, pageSize);
        return departmentDeptRoleService.getPaging(skip, pageSize);

    }
    $scope.reloadSpecificData = function () {
        $scope.specificDepartmentDeptRoleList = [];

        $scope.pagesizeSpecific = 20;
        $scope.skipSpecific = 0;



    };
    $scope.reloadData = function () {
        $scope.skip = 0;
        $scope.departmentDeptRoleList = [];
        departmentDeptRoleService.loadData();

    };
 
    function loadData() {

        preloaderfa.push();
        departmentDeptRoleService.getPaging($scope.skip, $scope.pagesize)
                    .then(function (response) {
                        var data = response.data;
                        $scope.departmentDeptRoleList = data;
                        $scope.skip += $scope.pagesize;
                        preloaderfa.pop();
                        //     newdepartmentDeptRoleScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دوره ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newdepartmentDeptRoleScroll.refresh();
                    });




    }

};