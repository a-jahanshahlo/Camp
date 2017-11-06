var passengerCtrl = campsApp.controller('passengerCtrl', ['$scope', 'userServices', 'passengerService', 'preloaderfa', passengerCtrlfn]);

function passengerCtrlfn($scope, userServices, passengerService, preloaderfa) {
    // pagination size variable. must declare in top
    $scope.pagesize = 20;
    $scope.skip = 0;

    passengerService.watchOberver(loadPassengers);

    passengerService.loadPassengers();






    function loadPassengers() {
        preloaderfa.push();
        passengerService.getPassengers($scope.skip, $scope.pagesize)
                    .then(function (response) {
                        var data = response.data;
                        $scope.suiteList = data;
                        $scope.skip += $scope.pagesize;
                        preloaderfa.pop();
                        //     newSuiteScroll.refresh();
                    }, function (error) {
                        errorHintMessage("متاسفانه لیست سوئیت ها بارگذاری نشد");
                        preloaderfa.pop();
                        //   newSuiteScroll.refresh();
                    });
    }
}