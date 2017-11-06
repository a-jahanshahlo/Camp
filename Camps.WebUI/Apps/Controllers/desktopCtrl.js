


var desktop = campsApp.controller('desktopCtrl', ['$scope', 'userServices','preloaderfa', 'shareUserDataFa', desktopCtrlfn]);

//appCtrl.filter('unsafe', function ($sce) {
//    return function (val) {
//        return $sce.trustAsHtml(val);
//    };
//});

function desktopCtrlfn($scope, userServices,preloaderfa, shareUserDataFa) {
    preloaderfa.pushToCenter();
    var data = shareUserDataFa;
    $scope.imageUrl = data.imageUrl;
    $scope.shareUserData = { shareUserData: { imageUrl: "" } };
    var updateData = function () {
        $scope.imageUrl = data.imageUrl;
    };
    data.registerObserverCallback(updateData);
    var tt = data.getImageUrl();

    //$scope.shareUserData = shareUserDataFa.Data;// "url('/content/DesktopImage/2.jpg')"; //"url('" +pl.data+"')";
    $scope.dataLoaded = false;

}

