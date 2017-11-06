

campsApp.factory('shareUserDataFa', ['$rootScope', 'userServices', shareUserDataFafn]);



function shareUserDataFafn(userServices, $rootScope) {
var    myStyle = {

    }
var style = {
    'position':' fixed',
    'width':' 100%',
    'height':' 100%',
    'z-index':' 0',
    'filter':' blur(4px)',
    '-o-filter':' blur(4px)',
    '-ms-filter':' blur(4px)',
    '-moz-filter':' blur(4px)',
    '-webkit-filter':' blur(4px)',
    'background-size':' cover',
    '-webkit-transition': ' all 2s',
    'transition': ' all 2s'
}
    var observerCallbacks = [];
    var Data = {
        imageUrl: "",
        registerObserverCallback:null,
        getImageUrl: null, setImageUrl: null
    };
    //register an observer
    Data.registerObserverCallback = function (callback) {
        observerCallbacks.push(callback);
    };

    //call this when you know 'foo' has been changed
    var notifyObservers = function () {
        angular.forEach(observerCallbacks, function (callback) {
            callback();
        });
    };



    Data.setImageUrl = function (imagePath) {
        var setBackgroundImage = $rootScope.setBackgroundImage(imagePath);
        setBackgroundImage.success(function (data) {
          
            style["background-image"] = " url('" + data + "')";
            Data.imageUrl = style;
            notifyObservers();
        }).error(function (data) {
            Data.imageUrl = data;
        });
    }
    Data.getImageUrl = function () {
        var getBackgroundImage = $rootScope.getBackgroundImage();
        getBackgroundImage.success(function(data) {
            style["background-image"] = " url('" + data + "')";
            Data.imageUrl = style;
            notifyObservers();
        }).error(function(data) {
            Data.imageUrl = data;
        });

    }
    
    return Data;

}
