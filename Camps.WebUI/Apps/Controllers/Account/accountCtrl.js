var accountCtrl = campsApp.controller('accountCtrl', ['$scope', '$route', '$compile', '$location', 'userInDeptRoleService', 'accountService', 'preloaderfa', 'publiceService', accountCtrlfn]);


function accountCtrlfn($scope, $route, $compile, $location, userInDeptRoleService, accountService, preloaderfa, publiceService) {

    $scope.pagesize = 20;
    $scope.skip = 0;
    $scope.accountList = [];
    accountService.watchOberver(loadData);
    accountService.loadData();


    //------Require Common Objects---------------
    var modalNewAccount, newAccount = "newAccount";
    var modalEditAccount, editAccount = "editAccount";
    var modalPermissionAccount, permissionAccount = "permissionAccount";
    var modalResetPassAccount, resetPassAccount = "resetPassAccount";
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

    //------------Prepare to open modal for new permission
    $scope.selectedAccount = null;
    $scope.openPermissionModal = function (account) {
        if (!isExistElement(permissionAccount)) {
            preloaderfa.push();

            modalPermissionAccount = modalWindows(permissionAccount, 'دسترسی های کاربر ', $compile("<div ng-include=\"'/Apps/Views/Account/PermissionAccount.html'\"></div>")($scope));
            modalPermissionAccount.resize("600px", "200px");
            modalPermissionAccount.control("disable", "maximize");
            modalPermissionAccount.on("jspanelloaded", function (event, id) {
                if (id === permissionAccount) {
                    $scope.selectedAccount = account;
                    preloaderfa.pop();
                    $scope.getGenders();
                    //$scope.bindGalleries();
                }
            });

        }
    };
    $scope.resetPassAccount = null;
    $scope.openResetPassModal = function (account) {
        if (!isExistElement(resetPassAccount)) {
            preloaderfa.push();
            $scope.resetPassAccount = account;

            modalResetPassAccount = modalWindows(resetPassAccount, ' تغییر کلمه عبور کاربر ', $compile("<div ng-include=\"'/Apps/Views/Account/ResetPassword.html'\"></div>")($scope));
            modalResetPassAccount.resize("600px", "300px");
            modalResetPassAccount.control("disable", "maximize");
            modalResetPassAccount.on("jspanelloaded", function (event, id) {
                if (id === resetPassAccount) {
                   
                    preloaderfa.pop();
                    
                    
                }
            });

        }
    };
    $scope.resetPass = function (model) {
        accountService.resetPass(model)
                .success(function (data) {
             
                    successHintMessage(" کلمه عبور کاربر مورد نظر با موفقیت ثبت شد");

                }).error(function (data) {
                    errorHintMessage("متاسفانه کلمه عبور کاربر مورد نظر ثبت نشد");
                    
                });
    }
    //------------Prepare to open modal for new dept
    $scope.newModal = function () {
        if (!isExistElement(newAccount)) {
            preloaderfa.push();

            modalNewAccount = modalWindows(newAccount, 'حساب کاربری جدید  ', $compile("<div ng-include=\"'/Apps/Views/Account/NewAccount.html'\"></div>")($scope));
            modalNewAccount.resize("600px", "500px");
            modalNewAccount.control("disable", "maximize");
            modalNewAccount.on("jspanelloaded", function (event, id) {
                if (id === newAccount) {


                    preloaderfa.pop();
                    $scope.getGenders();
                    //$scope.bindGalleries();
                }
            });

        }
    };
    //------------Add new account function---------
    $scope.addNew = function (model) {
        accountService.post(model)
                .success(function (data) {
                    $scope.reloadData();
                    successHintMessage("حساب کاربری جدید با موفقیت ثبت شد");
                    modalNewAccount.close();
                }).error(function (data) {
                    errorHintMessage("متاسفانه حساب کاربری جدید ثبت نشد");
                    //alert("faild:" + data);
                });
    }
    //-----------------------Delete dept
    $scope.delete = function (id) {
        var modalYesNo = modalConfirmYesNo("حذف رکورد", "آیا از حذف رکورد مطمئن هستید؟", function () {
            //if click on YES
            accountService.delete(id)
                    .then(function (data) {
                        $scope.reloadData();
                        findAndRemove($scope.accountList, "id", id);
                        successHint();
                    }, function (data) {
                        errorHint();

                    });

        }, function () {
            // if click on No
        });

    };
    //-----------------------Edit account modal open---------
    $scope.editAccount = null;
    $scope.openEditModal = function (model) {
        $scope.editAccount = model;
        if (!isExistElement(editAccount)) {
            preloaderfa.push();


            modalEditAccount = modalWindows(editAccount, 'ویرایش عنوان پست ', $compile("<div ng-include=\"'/Apps/Views/Account/EditAccount.html'\"></div>")($scope));
            modalEditAccount.resize("300px", "120px");
            modalEditAccount.control("disable", "maximize");
            modalEditAccount.on("jspanelloaded", function (event, id) {
                if (id === editAccount) {

                    preloaderfa.pop();
                    //$scope.bindGalleries();

                }
            });

        }
    };
    //-----------------------Edit Dept---------
    $scope.editFn = function (model) {

        accountService.edit(model.id, model)
                .then(function (data) {
                    $scope.reloadData();
                    successHint();
                }, function (data) {
                    errorHint();

                });

    };
    //--------------Declare dept object
    $scope.account = {
        email: "",
        mobile: "",
        password: "",
        isActive: true,
        confirmPassword: "",
        profile: {
            phone: "",
            address: "",
            isDeleted: false,
            firstName: "",
            lastName: "",
            age: 6,
            nid: "",
            genderId: -1,
            birthday: ""
        }

    }
    //-------------cancel operation and close modal
    $scope.cancelNewModal = function () {
        if (modalNewAccount) {
            modalNewAccount.close();
        }
    }
    //-------------cancel operation and close modal
    $scope.cancelEditModal = function () {
        if (modalEditAccount) {
            modalEditAccount.close();
        }
    }
    //------------Common handeler-------
    $scope.fetchData = function (skip, pageSize) {
        // return deptService.getAlls(skip, pageSize);
        return accountService.getPaging(skip, pageSize);

    }
    $scope.reloadData = function () {
        $scope.skip = 0;
        $scope.accountList = [];
        accountService.loadData();

    };
    $scope.convertCalendar = function (date) {
        if (date.gDate) {
            $scope.account.profile.birthday = date.gDate;
        }
    };
    $scope.genders = []; 
    $scope.getGenders = function () {
        preloaderfa.push();
        publiceService.getGenders()
                      .then(function (response) {
                          var data = response.data;
                          $scope.genders = data;
                          preloaderfa.pop();
                          //     newaccountScroll.refresh();
                      }, function (error) {
                          errorHintMessage("متاسفانه لیست جنسیت بارگذاری نشد");
                          preloaderfa.pop();
                          //   newaccountScroll.refresh();
                      });

    };
    $scope.selectedRolefn = function (data) {

        accountService.addToRole($scope.selectedAccount.id, data.params.data.id)
                        .success(function (data) {

                            successHintMessage("نقش جدید با موفقیت ثبت شد");

                        }).error(function (data) {
                            errorHintMessage("متاسفانه نقش جدید ثبت نشد");
                            //alert("faild:" + data);
                        });
    };
    $scope.removedRolefn = function (data) {

        accountService.removeFromRole($scope.selectedAccount.id, data.params.data.id)
                .success(function (data) {

                    successHintMessage("نقش مورد نظر با موفقیت حذف شد");

                }).error(function (data) {
                    errorHintMessage("متاسفانه نقش مورد نظر حذف نشد");
                    //alert("faild:" + data);
                });
    };
    $scope.selectedDeptRolefn = function (data) {

        userInDeptRoleService.addToPost($scope.selectedAccount.id, data.params.data.id)
                        .success(function (data) {

                            successHintMessage(" پست سازمانی جدید با موفقیت ثبت شد");

                        }).error(function (data) {
                            errorHintMessage("متاسفانه پست سازمانی  جدید ثبت نشد");
                            //alert("faild:" + data);
                        });
    };
    $scope.removedDeptRolefn = function (data) {
        userInDeptRoleService.removeFromPost($scope.selectedAccount.id, data.params.data.id)
                             .success(function (data) {
                                 successHintMessage("پست سازمانی  مورد نظر با موفقیت حذف شد");
                             }).error(function (data) {
                                 errorHintMessage("متاسفانه پست سازمانی  مورد نظر حذف نشد");

                             });

    };
    $scope.confirmer = function ($event, id) {
        userInDeptRoleService.updateConfirmer($event.currentTarget.checked, id)
                             .success(function (data) {
                                         successHintMessage("مجوز تائید کننده با موفقیت اعمال شد");
                              });
        //;
    }
    $scope.convertCalendar = function (date) {
        if (date.gDate) {
            $scope.account.profile.birthday = date.gDate;
        }
    };
    function loadData() {

        preloaderfa.push();
        accountService.getPaging($scope.skip, $scope.pagesize)
                    .then(function (response) {
                        var data = response.data;
                        $scope.accountList = data;
                        $scope.skip += $scope.pagesize;
                        preloaderfa.pop();
                        //     newaccountScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست دپارتمان ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newaccountScroll.refresh();
                    });




    }

};