campsApp.filter('setDecimal', function ($filter) {
    return function (input, places) {
        if (isNaN(input)) return input;
        // If we want 1 decimal place, we want to mult/div by 10
        // If we want 2 decimal places, we want to mult/div by 100, etc
        // So use the following to create that factor
        var factor = "1" + Array(+(places > 0 && places + 1)).join("0");
        return Math.round(input * factor) / factor;
    };
});
campsApp.filter('changeToPersian', function () {
    return function (x) {

        return moment(x, 'YYYY-M-D HH:mm:ss').format('jYYYY/jM/jD');

    };
});
campsApp.filter('setFaBoolean', function ($sce) {
    return function (x, length) {

        if (x === true) {
            return     $sce.trustAsHtml("<span class='bg-success'>" + "فعال" + "</span>");
           
        }
        return $sce.trustAsHtml("<span class='bg-danger'>" + "غیر فعال" + "</span>");
    };
});
campsApp.filter('setYesNoBoolean', function ($sce) {
    return function (x, length) {

        if (x === true) {
            return $sce.trustAsHtml("<span class='bg-danger'>" + "بله" + "</span>");

        }
        return $sce.trustAsHtml("<span class='bg-success'>" + "خیر" + "</span>");
    };
});
campsApp.filter('setIsConfirm', function ($sce) {
    return function (x, length) {

        if (x === 0) {
            return $sce.trustAsHtml("<span class='bg-info'>" + "در انتظار تائید" + "</span>");

        }
        return $sce.trustAsHtml("<span class='bg-success'>" + "تائید شده" + "</span>");
    };
});