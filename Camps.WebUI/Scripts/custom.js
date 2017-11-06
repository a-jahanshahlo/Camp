$(document).ready(function () {

   
    moment().format('jYYYY/jM/jD');
    moment.loadPersian();
    //var loader = $("body").DEPreLoad({
    //    OnStep: function (percent) {
    //        console.log(percent + '%');

    //        $("#depreload .line").animate({ opacity: 1 });
    //        $("#depreload .perc").text(percent + "%");

    //        if (percent > 5) {
    //            context.clearRect(0, 0, canvas.width, canvas.height);
    //            context.beginPath();
    //            context.arc(280, 280, 260, Math.PI * 1.5, Math.PI * (1.5 + percent / 50), false);
    //            context.stroke();
    //        }
    //    },
    //    OnComplete: function () {
    //        console.log('Everything loaded!');

    //        $("#depreload .perc").text("done");
    //        $("#depreload .loading").animate({ opacity: 0 });
    //    }
    //});


    /*----------------Select 2---------------------------*/


    /*----------------blue imp---------------------------*/
    $('.gallery-button').on('click', function (event) {
        event.preventDefault();
        blueimp.Gallery($('#links ul li a'), $('#blueimp-gallery').data());
    });
    $('.gallery-button').on('click', function (event) {
        event.preventDefault();
        blueimp.Gallery($('#links ul li a'), $('#blueimp-gallery').data());
    });
});
function toJalali(georgian) {
    return moment(georgian, 'jYYYY/jM/jD HH:mm').format('YYYY-M-D HH:mm:ss');
}
function createId() {
    return "_" + uniqId();
}
function uniqId() {
    return Math.round(new Date().getTime() + (Math.random() * 100));
}
function connectionTimer() {
    var obj = this;
    var observerCallbacks = [];

    //register an observer
    obj.registerCallback = function (callback) {
        observerCallbacks.push(callback);
    };

    obj.StartTimer = function () {
        setInterval(function () {
            $.each(observerCallbacks, function (key, value) {
                value();
            });
            while (observerCallbacks.length > 0) {
                observerCallbacks.pop();
            }
        }, 20000);
    }

    return obj;
}



/*---------------------------------------------------*/
function listView($id) {
    //$('#list').on("click", function () {
    //alert("");
    //$('.prod-box').animate({ opacity: 0.0 }, function () {
    //    $('.grid').removeClass('grid-active');
    //    $('.list').addClass('list-active');
    //    $('.prod-box').attr('class', 'prod-box-list shadow');
    //    $('.prod-box-list').animate({ opacity: 1.0 });
    //});
    //});

    //$('#list').click(function(event) {

    $('.' + $id + ' [class^="item"]').addClass('list-group-item');
    $('.' + $id + ' [class^="item"]').removeClass(' list-group-item-block');
    //});
}
function gridView($id) {
    //$('#grid').on("click", function () {
    //alert("");
    //$('.prod-box-list').animate({ opacity: 0.0 }, function () {
    //    $('.list').removeClass('list-active');
    //    $('.grid').addClass('grid-active');
    //    $('.prod-box-list').attr('class', 'prod-box shadow');
    //    $('.prod-box').animate({ opacity: 1.0 });
    //});
    //});
    //$('#grid').click(function (event) {
    //    event.preventDefault();
    $('.' + $id + ' [class^="item"]').removeClass('list-group-item');
    $('.' + $id + ' [class^="item"]').addClass('grid-group-item');
    $('.' + $id + ' [class^="item"]').addClass(' list-group-item-block');
    //  });

}

function findAndRemove(array, property, value) {
    $.each(array, function (index, result) {
        if (result) {
            if (result[property] == value) {
                //Remove from array
                array.splice(index, 1);
            }
        }
    });
}
var modalWindows = function ($id, title, message) {

    var jsPn = $.jsPanel({
        id: $id,
        selector: 'body',
        position: 'center',
        controls: {
            iconfont: 'font-awesome'
        },
        content: message,
        size: { width: 600, height: 400 },
        //    position: "center",
        theme: "primary",
        title: title
    });

    jsPn.on("jspanelloaded", function (event, id) {
        if (id === $id) {
            jsPn.front();
        }
    });
    //  jsPn.front();
    return jsPn;
};
//-----------------
var modalInfoOk = function (title, message, errorcallback) {

    var arr = [
    //{
    //    item: "<div style='float:left;padding:6px 0 0 0;cursor:pointer;'>Clickable Text ...</div>",
    //    event: "click"
    //    callback: function (event) {
    //       // event.data.content.append("<p>The click happened at (" + event.pageX + ", " + event.pageY + ")</>");
    //    }
    //},
    {
        item: "<button class='...' type='button'><span class='...'></span></button>",
        event: "click",
        btnclass: "btn-sm",
        btntext: " تائید",
        callback: function (event) {
            if (errorcallback) {
                errorcallback();
            }

            event.data.close();
        }
    }
    ];


    var item = $.jsPanel({
        selector: 'body',
        title: title,
        content: message,
        paneltype: 'modal',
        toolbarFooter: arr,
        theme: 'warning',
        size: { width: 300, height: 100 },
        callback: function (panel) {

        }
    });



    // item.front();
    return item;
};




//-----------------
var modalConfirmYesNo = function (title, message, successcallback, errorCallback) {

    var arr = [
    //{
    //    item: "<div style='float:left;padding:6px 0 0 0;cursor:pointer;'>Clickable Text ...</div>",
    //    event: "click"
    //    callback: function (event) {
    //       // event.data.content.append("<p>The click happened at (" + event.pageX + ", " + event.pageY + ")</>");
    //    }
    //},
    {
        item: "<button class='...' type='button'><span class='...'></span></button>",
        event: "click",
        btnclass: "btn-sm",
        btntext: " خیر",
        callback: function (event) {
            errorCallback();
            event.data.close();
        }
    },
    {
        item: "<button class='...' type='button'><span class='...'></span></button>",
        event: "click",
        btnclass: "btn-sm",
        btntext: " بله",
        callback: function (event) {
            successcallback();
            event.data.close();
            // event.data.content.append("<p style='...'>And this was a click on the OK button!</p>")
        }
    }
    ];


    var item = $.jsPanel({
        selector: 'body',
        title: title,
        content: message,
        paneltype: 'modal',
        toolbarFooter: arr,
        theme: 'danger',
        size: { width: 300, height: 50 },
        callback: function (panel) {

        }
    });



    // item.front();
    return item;
};

var modalUpload = function (title, message) {

    var arr = [{
        item: "<button class='...' type='button'><span class='...'></span></button>",
        event: "click",
        btnclass: "btn-sm",
        btntext: " بستن",
        callback: function (event) { event.data.close() }
    }];


    var item = $.jsPanel({
        selector: 'body',
        title: title,
        content: message,
        paneltype: 'modal',
        toolbarFooter: arr,
        theme: 'primary',
        size: { width: 500, height: 300 },
        callback: function (panel) {

        }
    });



    // item.front();
    return item;
};
$("body").on("jspanelclosed", function closeHandler(event, id) {
    $(id).remove();
    // close handlers attached to body should be removed again
    $("body").off("jspanelclosed", closeHandler);

});
function isExistElement(id) {
    return $('#' + id).length > 0;
}
newGuid = function () {
    return 'dxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
};
/*----------------------hint message file ----------------------------*/
function infoHintMessage(message) {
    $.jsPanel({
        paneltype: 'hint',
        theme: 'primary',
        position: 'top right',
        size: { width: 360, height: 'auto' },
        content: successHintContent(message)
    });
}
function successHintContent(content) {
    var cont = "<div style='font-size:14px;z-index:10000;'>" +
    "<div style='float:left;width:auto;height:100%'>" +
    "<i class='fa fa-comment' style='font-size:18px;padding:18px;'></i>" +
    "</div>" +
    "<p style='padding:14px 0;'>" + content + "</p>" +
    "</div>";
    return cont;
}
function successHint() {
    $.jsPanel({
        paneltype: 'hint',
        theme: 'success',
        position: 'top right',
        size: { width: 360, height: 'auto' },
        content: successHintContent("با موفقیت انجام شد")
    });
}
function successHintMessage(message) {
    return $.jsPanel({
        paneltype: 'hint',
        theme: 'success',
        position: 'top right',
        size: { width: 360, height: 'auto' },
        content: successHintContent(message)
    });
}
function errorHint() {
    $.jsPanel({
        paneltype: 'hint',
        theme: 'danger',
        position: 'top right',
        size: { width: 360, height: 'auto' },
        content: successHintContent("متاسفانه درخواست شما شکست خورد")
    });
}
function errorHintMessage(message) {
    $.jsPanel({
        paneltype: 'hint',
        theme: 'danger',
        position: 'top right',
        size: { width: 360, height: 'auto' },
        content: successHintContent(message)
    });
}
/*-----------------------Full Screen----------------------------*/
function toggleFullScreen() {
    if (!document.fullscreenElement &&    // alternative standard method
        !document.mozFullScreenElement && !document.webkitFullscreenElement && !document.msFullscreenElement) {  // current working methods
        if (document.documentElement.requestFullscreen) {
            document.documentElement.requestFullscreen();
        } else if (document.documentElement.msRequestFullscreen) {
            document.documentElement.msRequestFullscreen();
        } else if (document.documentElement.mozRequestFullScreen) {
            document.documentElement.mozRequestFullScreen();
        } else if (document.documentElement.webkitRequestFullscreen) {
            document.documentElement.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
        }
    } else {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        } else if (document.msExitFullscreen) {
            document.msExitFullscreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.webkitExitFullscreen) {
            document.webkitExitFullscreen();
        }
    }
}

/*-----------------------End Full Screen----------------------------*/

/*-----------------------Declare Loader ----------------------------*/
function Preloader() {

   
    var db = [];

    var Data = {
        isVisible: false,
        threadid: 0,
        resetCnn:0

    };

    //add preloader
    Data.topleft = function () {

        $("body").append(
  "<div id='depreload' class='table corner-preloader' >" +
       "<div class='table-cell wrapper'>" +
           "<div class='circle'>" +
               "<canvas class='line' width='160px' height='160px'></canvas>" +
              " <img src='/Content/Icons/logo.png' class='logo' />" +
           "</div>" +
      " </div>" +
   "</div>"
       );
    };
    //add preloader
    Data.center = function () {
        $("body").append(
  "<div id='depreload' class='table  Absolute-Center is-Responsive' >" +
       "<div class='table-cell wrapper'>" +
           "<div class='circle'>" +
               "<canvas class='line' width='160px' height='160px'></canvas>" +
              " <img src='/Content/Icons/logo.png' class='logo' />" +
           "</div>" +
      " </div>" +
   "</div>" +
 "<div class='backdrop' style='position: fixed;width: 100%;height: 100%;top: 0px;right: 0px;bottom: 0px;left: 0px;z-index: 1040;background: rgba(0,0,0,0.7);'></div>"
        
       );
    };
    //add preloader
    Data.removePreloader = function () {
        $("#depreload").remove();
        $(".backdrop").remove();
    };


    Data.push = function () {
        db.push("0");
        Data.start("topleft");
    };
    Data.pushToCenter = function () {
        db.push("0");
        Data.start("center");
    };
    //call this when you know 'foo' has been changed
    Data.pop = function () {
        db.pop();
        if (db.length < 1) {
            Data.stop();
           
        }
        
    };
    Data.start = function (position) {
        Data.stop();
        if (position === "center") {
            Data.center();
        } else if (position === "topleft") {
            Data.topleft();
        }


        var canvas = $("#depreload .line")[0],
        context = canvas.getContext("2d");
        var percent = 0;

        context.beginPath();
        context.arc(80, 80, 40, Math.PI * 1.5, Math.PI * 1.6);
        context.strokeStyle = '#fff';
        context.shadowColor = '#999';
        context.shadowBlur = 10;
        context.shadowOffsetX = 2;
        context.shadowOffsetY = 2;
        context.lineWidth = 3;
        context.stroke();


        Data.threadid = setInterval(function () {
            Data.resetCnn += 100;
            $("#depreload .wrapper").animate({ opacity: 1 });
            $("#depreload .logo").animate({ opacity: 1 });

            if (percent > 99) { percent = 1; }
            //Preloader after 20 minutes should be reset.
            if (Data.resetCnn > 10000) {
                while (db.length > 0) {
                    db.pop();
                }
                Data.stop();
             
            }

            context.clearRect(0, 0, canvas.width, canvas.height);
            context.beginPath();
            context.arc(80, 80, 40, Math.PI * 1.5, Math.PI * (1.5 + percent / 50), false);
            context.stroke();
            percent += 2;

        }, 100);


    }
    Data.stop = function () {
        if (Data.threadid) {
            clearInterval(Data.threadid);
            Data.threadid = -1;

        }
        Data.removePreloader();
        Data.resetCnn = 0;
    }

    return Data;

}

/*-----------------------End Loader on first load ----------------------------*/
window.loading_screen.finish();