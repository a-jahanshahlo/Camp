campsApp.directive('iscrollInfinite', ['$compile', 'galleryService', function ($compile, galleryService) {
    return {
        restrict: 'A',
        transclude : true,
        link: function (scope, element, attrs, controller) {
            var wrappr = $("<div class='scrollwrapper' ></div>");
            var $id = newGuid();
            $(wrappr).attr("id", $id);
         
            var scroller = $("<div class='scroller'></div>");
         var uu=   "<ul><li ng-repeat='gallery in galleries'>{{gallery.name}}</li></ul>";
         $(scroller).html(uu);
            $(wrappr).append(scroller);
            $(element).html($compile(wrappr)(scope));
            
            //var $this = $(element);
            //var $id = $this.attr('id');


            var myScroll = new IScroll('#' + $id, {
                bounceEasing: 'elastic',
                bounceTime: 1200,
                scrollbars: true,
                mouseWheel: true,
                interactiveScrollbars: true,
                shrinkScrollbars: 'scale',
                fadeScrollbars: false
                //infiniteElements: '#scroller .ttt',
                //infiniteLimit: 2000,
                //dataset: requestData,
                //dataFiller: updateContent,
                //cacheSize: 1000

            });

            myScroll.on('scrollEnd', function () {

                var h = this.maxScrollY;
                var call = this.y - 100;
                if (call < h) {
                    galleryService.getGalleries(0)
                    .then(function (response) {
                        var data = response.data;
                        $.each(data, function (key,value) {
                            alert(key+":"+value);
                        });
                        //for (var i = 0; i <= 20; i++) {
                        //    $('#' +$id+' div.scroller ul').append("<li>jnjnjnj</li>");
                        //}
                    },
                        function (error) {
                            //  $scope.error = 'failure loading ', errorPl;
                        });
                myScroll.refresh();
                }
            });

        },
        scope: {
            galleries: '=galleries',
            wrapper: '=wrapper',
            loadData:'&loadData'
        }
    }
}] );