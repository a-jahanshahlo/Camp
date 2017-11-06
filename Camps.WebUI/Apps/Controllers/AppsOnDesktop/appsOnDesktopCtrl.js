var appsOnDesktopCtrl = campsApp.controller('appsOnDesktopCtrl', ['$scope', '$compile', 'appSettingsService', 'shareGalleryDataFa', 'accountService', appsOnDesktopCtrlfn]);



function appsOnDesktopCtrlfn($scope, $compile, appSettingsService, shareGalleryDataFa, accountService) {
    //  $scope.snippet = "<span style='color:red'> this is red</span>";
    shareGalleryDataFa.registerObserverCallbackSelectedGallery(selectedGallery);
    var mainGallerys = null;
    var modalCamps = null;
    var modalPassangers = null;
    var mainCamp = 'mainCamp', mainGalleryName = 'mainGallery', mainPassenger = "mainPassenger";
    var modalSuite , mainSuite = "mainSuite";
    var modalDept, mainDept = "mainDept";
    var modalRole, mainRole = "mainRole";
    var modalAccount, mainAccount = "mainAccount";
    var modalFestival, mainFestival = "mainFestival";
    var modalQuota, mainQuota = "mainQuota";
    var modalReservation, mainReservation = "mainReservation";
    var modalConfirmQuota, mainConfirmQuota = "mainConfirmQuota";

    $scope.admin = false;
    $scope.agent = false;
    $scope.passenger = false;
    $scope.boss = false;
    accountService.getCurrentUserRoles()
                    .then(function (response) {
                        var data = response.data;
 
                        $.each(data, function (key, value) {
                            switch (value.role) {
                                case 'Admin':
                                    $scope.admin = true;
                                    break;
                                case 'Agent':
                                    $scope.agent = true;
                                    break;
                                case 'Passenger':
                                    $scope.passenger = true;
                                    break;
                                case 'Boss':
                                    $scope.boss = true;
                                    break;
                            default:
                            }
                        });
                        //     newaccountScroll.refresh();
                    }, function (error) {

                        //   newaccountScroll.refresh();
                    });


    $scope.openCampsWindow = function () {
        if (!isExistElement(mainCamp)) {
            modalCamps = modalWindows(mainCamp, 'اردوگاه ها', $compile("<div ng-include=\"'/Apps/Views/Camps/Camps.html'\"></div>")($scope));
            modalCamps.resize("750px", "500px");
            modalCamps.on("jspanelloaded", function (event, id) {
                if (id === mainCamp) {
                    modalCamps.front();
                }
            });
        }
    };
    $scope.openSuiteWindow = function () {
        if (!isExistElement(mainSuite)) {
            modalSuite = modalWindows(mainSuite, 'سوئیت', $compile("<div ng-include=\"'/Apps/Views/Suites/MainSuite.html'\"></div>")($scope));
            modalSuite.resize("700px", "500px");
            modalSuite.control("disable", "maximize");
        }
    };
    $scope.openGalleryWindow = function () {
        if (!isExistElement(mainGalleryName)) {
            mainGallerys = modalWindows(mainGalleryName, 'گالری', $compile("<div ng-include=\"'/Apps/Views/Gallery/MainGallery.html'\"></div>")($scope));
            mainGallerys.resize("800px", "500px");
            mainGallerys.control("disable", "maximize");

        }
    };
    $scope.openPassengerWindow = function () {
        if (!isExistElement(mainPassenger)) {
            modalPassangers = modalWindows(mainPassenger, 'میهمان', $compile("<div ng-include=\"'/Apps/Views/Passenger/MainPassenger.html'\"></div>")($scope));
            modalPassangers.resize("700px", "500px");
            modalPassangers.control("disable", "maximize");
        }
    };
    $scope.openDeptWindow = function () {
        if (!isExistElement(mainDept)) {
            modalDept = modalWindows(mainDept, 'دپارتمان', $compile("<div ng-include=\"'/Apps/Views/Department/MainDept.html'\"></div>")($scope));
            modalDept.resize("700px", "500px");
            modalDept.control("disable", "maximize");
        }
    };
    $scope.openRoleWindow = function () {
        if (!isExistElement(mainRole)) {
            modalRole = modalWindows(mainRole, 'نقش ها / پست ها', $compile("<div ng-include=\"'/Apps/Views/DeptRole/MainDeptRole.html'\"></div>")($scope));
            modalRole.resize("700px", "500px");
            modalRole.control("disable", "maximize");
        }
    };

    $scope.openAccountWindow = function () {
        if (!isExistElement(mainAccount)) {
            modalAccount = modalWindows(mainAccount, 'کاربران', $compile("<div ng-include=\"'/Apps/Views/Account/MainAccount.html'\"></div>")($scope));
            modalAccount.resize("700px", "500px");
            modalAccount.control("disable", "maximize");
        }
    };
    $scope.openFestivalwindow = function () {
        if (!isExistElement(mainFestival)) {
            modalFestival = modalWindows(mainFestival, 'جشنواره/دوره', $compile("<div ng-include=\"'/Apps/Views/Festival/MainFestival.html'\"></div>")($scope));
            modalFestival.resize("700px", "500px");
            modalFestival.control("disable", "maximize");
        }
    };
    $scope.openQuotawindow = function () {
        if (!isExistElement(mainQuota)) {
            modalQuota = modalWindows(mainQuota, 'سهمیه بندی', $compile("<div ng-include=\"'/Apps/Views/Quota/MainQuota.html'\"></div>")($scope));
            modalQuota.resize("800px", "500px");
            modalQuota.control("disable", "maximize");
        }
    };
    $scope.openReservationWindow = function () {
        if (!isExistElement(mainReservation)) {
            modalReservation = modalWindows(mainReservation, 'رزرواسیون ', $compile("<div ng-include=\"'/Apps/Views/Reservation/MainReservation.html'\"></div>")($scope));
            modalReservation.resize("800px", "500px");
            modalReservation.on("jspanelloaded", function (event, id) {
                if (id === mainReservation) {
                    var dt = new Date();
                    $('#calendar').fullCalendar({
                        monthNames: ['فروردین', 'اردیبهشت', 'خرداد', 'تیر', 'مرداد', 'شهریور', 'مهر', 'آبان', 'آذر', 'دی', 'بهمن', 'اسفند'],
                        isRTL: true,
                        lang: 'fa',
                        schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives',
                        resourceAreaWidth: 230,
                        defaultDate: '2016-01-06',
                        editable: true,
                      
                        contentHeight: 400,
                        scrollTime: '00:00',
                        header: {
                            left: 'promptResource today prev,next',
                            center: 'title',
                            right: 'timelineDay,timelineThreeDays,agendaWeek,month'
                        },

                        defaultView: 'timelineDay',
                        views: {
                            timelineThreeDays: {
                                type: 'timeline',
                                duration: { days: 15 }
                            }
                        },
                        resourceLabelText: 'سوئیت ها',
                        resources: [
                            { id: 'a', title: 'سوئیت A' },
                            { id: 'b', title: 'سوئیت B', eventColor: 'green' },
                            { id: 'c', title: 'سوئیت C', eventColor: 'orange' },
                            {
                                id: 'd', title: 'سوئیت D', children: [
                                  { id: 'd1', title: 'Room D1' },
                                  { id: 'd2', title: 'Room D2' }
                                ]
                            },
                            { id: 'e', title: 'سوئیت E' },
                            { id: 'f', title: 'سوئیت F', eventColor: 'red' },
                            { id: 'g', title: 'سوئیت G' },
                            { id: 'h', title: 'سوئیت H' },
                            { id: 'i', title: 'سوئیت I' },
                            { id: 'j', title: 'سوئیت J' },
                            { id: 'k', title: 'سوئیت K' },
                            { id: 'l', title: 'سوئیت L' },
                            { id: 'm', title: 'سوئیت M' },
                            { id: 'n', title: 'سوئیت N' },
                            { id: 'o', title: 'سوئیت O' },
                            { id: 'p', title: 'سوئیت P' },
                            { id: 'q', title: 'سوئیت Q' },
                            { id: 'r', title: 'سوئیت R' },
                            { id: 's', title: 'سوئیت S' },
                            { id: 't', title: 'سوئیت T' },
                            { id: 'u', title: 'سوئیت U' },
                            { id: 'v', title: 'سوئیت V' },
                            { id: 'w', title: 'سوئیت W' },
                            { id: 'x', title: 'سوئیت X' },
                            { id: 'y', title: 'سوئیت Y' },
                            { id: 'z', title: 'سوئیت Z' }
                        ],
                        events: [
                            { id: '1', resourceId: 'b', start: '2016-01-07T02:00:00', end: '2016-01-07T07:00:00', title: 'event 1' },
                            { id: '2', resourceId: 'c', start: '2016-01-07T05:00:00', end: '2016-01-07T22:00:00', title: 'event 2' },
                            { id: '3', resourceId: 'd', start: '2016-01-06', end: '2016-01-08', title: 'event 3' },
                            { id: '4', resourceId: 'e', start: '2016-01-07T03:00:00', end: '2016-01-07T08:00:00', title: 'event 4' },
                            { id: '5', resourceId: 'f', start: '2016-01-07T00:30:00', end: '2016-01-07T02:30:00', title: 'event 5' }
                        ]
                    });
                }
            });
        }
    };
    $scope.openConfirmQuotaWindow = function () {
        if (!isExistElement(mainConfirmQuota)) {
            modalConfirmQuota = modalWindows(mainConfirmQuota, 'سهمیه واحد ', $compile("<div ng-include=\"'/Apps/Views/ConfirmQuota/MainConfirmQuota.html'\"></div>")($scope));
            modalConfirmQuota.resize("800px", "500px");
            modalConfirmQuota.control("disable", "maximize");
        }
    };
    function selectedGallery() {
        var tt = isExistElement(mainGalleryName);
        if (isExistElement(mainGalleryName)) {
            mainGallerys.title('گالری' + ' : ' + shareGalleryDataFa.gallery.name);
        }
    }
    function loadBackgroundImages() {
        var promiseGet = appSettingsService.getBackgroundImages();

        promiseGet.then(function (pl) {

            //var data = pl.data;
            //$scope.AliasName = data.userInfo.aliasName;
            //$scope.FirstName = data.userInfo.firstName;
            //$scope.LastName = data.userInfo.lastName;
            //$scope.UserName = data.userName;
            //$scope.Phone = data.userInfo.phone;
            //$scope.Email = data.userInfo.email;
        },
            function (errorPl) {
                //  $scope.error = 'failure loading ', errorPl;
            });
    }

    function sortableIcons() {
        $("#desktopicons").sortable({
 
            placeholder: "ui-state-highlight"
        });

        $("#desktopicons").disableSelection();
    }

    sortableIcons();
    loadBackgroundImages();
}
