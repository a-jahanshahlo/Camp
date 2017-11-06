

var navBarCtrl = campsApp.controller('navBarCtrl', ['$scope', '$compile', 'navbarService', navBarCtrlfn]);
function navBarCtrlfn($scope, $compile, navbarService) {

    loadData();

  //  var mainViewAppSettings = $( $compile("<div ng-include=\"'/Apps/Views/AppSettings/MainViewAppSettings.html'\"></div>")($scope));
    $scope.appSettings = function () {
 
        var modal = modalWindows('MainViewAppSettings', 'تنظیمات برنامه', $compile("<div ng-include=\"'/Apps/Views/AppSettings/MainViewAppSettings.html'\"></div>")($scope));


    };

    function loadData() {
        navbarService.getPaging($scope.skip, $scope.pagesize)
            .then(function (response) {
                var data = response.data;
                $scope.account = data;
 
            }, function (error) {
 
            });
    }
}
