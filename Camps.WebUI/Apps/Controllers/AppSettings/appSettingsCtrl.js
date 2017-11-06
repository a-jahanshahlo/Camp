var appSettingsCtrl = campsApp.controller('appSettingsCtrl',['$scope', '$compile', 'appSettingsService', appSettingsCtrlfn]);



function appSettingsCtrlfn($scope,$compile, appSettingsService) {
    //  $scope.snippet = "<span style='color:red'> this is red</span>";
 
    $scope.openImageWindow = function () {


        //var body = angular.element(document).find('body');
        //console.log(body[0].offsetWidth);
        // var promiseGet = appSettingsService.getBackgroundImages();
        var modal = modalWindows('backImagelist','تصاویر پس زمینه', $compile("<div ng-include=\"'/Apps/Views/AppSettings/BackgroundImagesList.html'\"></div>")($scope));
        modal.resize("660px", "570px");
    };

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

    loadBackgroundImages();   
}
