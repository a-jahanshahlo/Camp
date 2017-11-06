var appCtrl = campsApp.controller('appCtrl', ['$scope', 'eventData', HelloWorldCtrl]);
appCtrl.filter('unsafe', function ($sce) {
    	    return function(val) {
        	        return $sce.trustAsHtml(val);
        	    };
  	});
function HelloWorldCtrl($scope,eventData) {
    $scope.snippet = "<span style='color:red'> this is red</span>";
    $scope.helloMessage = "Hello World!";
    $scope.myStyle = { color: 'red' }

    $scope.switchOn = true;
    $scope.switchOff = false;

    $scope.sortorder = "Phone";
      eventData.getEvent(function(event) {
        $scope.tt=event
    });

    $scope.upVote=function(session) {
        session.Vote++;
    }
    $scope.downVote = function (session) {
        session.Vote--;
    }
}