

var userProfile = campsApp.controller('userProfileCtrl', ['$scope', 'userServices', userProfileCtrl]);

//appCtrl.filter('unsafe', function ($sce) {
//    return function (val) {
//        return $sce.trustAsHtml(val);
//    };
//});

function userProfileCtrl($scope, userServices) {
      //  $scope.snippet = "<span style='color:red'> this is red</span>";
 
    loadRecords();

    //Function to Load all Employees Records.   
    function loadRecords() {
        var promiseGetCurrentUser = userServices.getCurrentUser();

        promiseGetCurrentUser.then(function(pl) {

                var data = pl.data;
                $scope.AliasName = data.userInfo.aliasName;
                $scope.FirstName = data.userInfo.firstName;
                $scope.LastName = data.userInfo.lastName;
                $scope.UserName = data.userName;
                $scope.Phone = data.userInfo.phone;
                $scope.Email = data.userInfo.email;
            },
            function (errorPl) {
                  $scope.error = 'failure loading user', errorPl;
              });
    }



    }
