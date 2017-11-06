var backgroundImagesListCtrl = campsApp.controller('backgroundImagesListCtrl', ['$scope', '$compile', 'appSettingsService', 'shareUserDataFa', backgroundImagesListCtrlfn]);



function backgroundImagesListCtrlfn($scope, $compile, appSettingsService, shareUserDataFa) {

    var data = shareUserDataFa;
    $scope.setBackground = function (image) {
        data.setImageUrl(image);
    };

    function loadBackgroundImages() {
        var promiseGet = appSettingsService.getBackgroundImages();

        promiseGet.then(function (pl) {

            $scope.imagesList = pl.data;
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
