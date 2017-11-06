campsApp.directive('map', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs, controller) {
            $(element).click(function () {
                var mapContainer = $("<div></div>");
                //var myLatlng = new google.maps.LatLng(40.65, -73.95);
                //var myOptions = {
                //    zoom: 13,
                //    center: myLatlng,
                //    mapTypeId: google.maps.MapTypeId.ROADMAP
                //};width: 100px;
                //  height: 100px;
                //var map = new google.maps.Map(mapContainer[0], myOptions);
                var gmap = new GMaps({
                    div: mapContainer[0],
                    lat: scope.lat,
                    lng: scope.lng
                });
                gmap.addMarker({
                    lat: scope.lat,
                    lng: scope.lng,
                    title: scope.gTip,
                    click: function (e) {
                       
                    }
                });
                var modal = modalWindows(' نقشه', mapContainer);
                //var w = $(modal.content).width();
                //var h = $(modal.content).height();
                $(mapContainer).width("100%");
                $(mapContainer).height("100%");
             //   modal.front();
                gmap.refresh();
                //  modal.
                //  alert(scope.lat + "  " + scope.lng + " " + scope.gTip);
            });
        },
        scope: {
            lat: '=lat',
            lng: '=lng',
            gTip: '@gTip'
        }
    }
});