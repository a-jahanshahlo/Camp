var addNewCtrl = campsApp.controller('addNewCtrl', ['$scope', HelloWorldCtrl]);
//appCtrl.filter('unsafe', function ($sce) {
//    return function (val) {
//        return $sce.trustAsHtml(val);
//    };
//});
function HelloWorldCtrl($scope) {
    $scope.snippet = "<span style='color:red'> this is red</span>";
    $scope.helloMessage = "Hello World!";
    $scope.myStyle = { color: 'red' };

    $scope.switchOn = true;
    $scope.switchOff = false;

    $scope.sortorder = "Phone";

    $scope.tt = {
        Name: "Alireza",
        Family: "Jahanshahlo",
        Tel: "12345",
        location: {
            Address: "tehran-wolfare",
            City: "tehran",
            No: "251"
        },
        imageUrl: "/images/orderedList8.png",
        sessions: [
            {
                Phone: "First Phone:1234",
                Vote: 2
            },
            {
                Phone: "First Phone:56789",
                Vote: 12
            },
            {
                Phone: "First Phone:101112",
                Vote: 32
            }
        ]

    }
    $scope.upVote = function (session) {
        session.Vote++;
    }
    $scope.downVote = function (session) {
        session.Vote--;
    }
}