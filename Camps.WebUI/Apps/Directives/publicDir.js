
campsApp.directive('mySimple', function () {
    return {
        link: function (scope, elements, attrs, controller) {
            var markup = "<input type='text' ng-model='sampleData' /> {{sampleData}} <br/>";
            angular.element(elements).html(markup);
        }
    };
});
campsApp.directive('cPaginationperiod', ['$parse', 'preloaderfa', function ($parse, preloaderfa) {
    return {
        restrict: 'AC',
        require: '?ngModel',
        //replace: true,
        transclude: true,
        template: '<div class="scroller" ng-transclude></div>',
        link: function (scope, elem, attrs, ngModel) {
            // page skip size
            var skip = 0;
            // create id for element which need to IScroll
            var id = "_" + uniqId();
            elem.attr('id', id);
            //Config IScroll
            var myScroll = new IScroll("#" + id, {
                bounceEasing: 'elastic',
                bounceTime: 1200,
                scrollbars: true,
                mouseWheel: true,
                interactiveScrollbars: true,
                shrinkScrollbars: 'scale',
                fadeScrollbars: false
            });
            // When list scroll to End fetch new data from db.
            myScroll.on('scrollEnd', function () {

                var h = this.maxScrollY;
                var call = this.y - 10;
                if (call < h) {
                    preloaderfa.push();
                    scope.fetchFn()(scope.cSkip, scope.cPagesize, scope.cFestivalId)
                        .then(function (response) {
                            preloaderfa.pop();
                            var data = response.data;
                            if (data.length > 0) {
                                $.each(data, function (key, value) {
                                    scope.datasource.push(value);
                                });
                                scope.cSkip += scope.cPagesize;
                                myScroll.refresh();
                            } else {
                                infoHintMessage("داده دیگری در این لیست موجود نمی باشد");
                                myScroll.refresh();
                            }
                            //     newSuiteScroll.refresh();
                        }, function (error) {
                            preloaderfa.pop();
                            errorHintMessage("متاسفانه داده ای بارگذاری نشد");

                            //   newSuiteScroll.refresh();
                        });

                }
            });

        },
        scope: {
            datasource: "=datasource",
            cPagesize: '=cPagesize',
            cSkip: "=cSkip",
            cFestivalId: "=cFestivalid",
            fetchFn: '&'
        }
    }
}]);
campsApp.directive('cPagination', ['$parse', 'preloaderfa', function ($parse, preloaderfa) {
    return {
        restrict: 'AC',
        require: '?ngModel',
        //replace: true,
        transclude: true,
        template: '<div class="scroller" ng-transclude></div>',
        link: function (scope, elem, attrs, ngModel) {
            // page skip size
            var skip = 0;
            // create id for element which need to IScroll
            var id = "_" + uniqId();
            elem.attr('id', id);
            //Config IScroll
            var myScroll = new IScroll("#" + id, {
                bounceEasing: 'elastic',
                bounceTime: 1200,
                scrollbars: true,
                mouseWheel: true,
                interactiveScrollbars: true,
                shrinkScrollbars: 'scale',
                fadeScrollbars: false
            });
            // When list scroll to End fetch new data from db.
            myScroll.on('scrollEnd', function () {

                var h = this.maxScrollY;
                var call = this.y - 10;
                if (call < h) {
                    preloaderfa.push();
                    scope.fetchFn()(scope.cSkip, scope.cPagesize)
                        .then(function (response) {
                            preloaderfa.pop();
                            var data = response.data;
                            if (data.length > 0) {
                                $.each(data, function (key, value) {
                                    scope.datasource.push(value);
                                });
                                scope.cSkip += scope.cPagesize;
                                myScroll.refresh();
                            } else {
                                infoHintMessage("داده دیگری در این لیست موجود نمی باشد");
                                myScroll.refresh();
                            }
                            //     newSuiteScroll.refresh();
                        }, function (error) {
                            preloaderfa.pop();
                            errorHintMessage("متاسفانه داده ای بارگذاری نشد");

                            //   newSuiteScroll.refresh();
                        });

                }
            });

        },
        scope: {
            datasource: "=datasource",
            cPagesize: '=cPagesize',
            cSkip: "=cSkip",
            fetchFn: '&'
        }
    }
}]);
campsApp.directive('cIscroll', ['$parse', 'preloaderfa', function ($parse, preloaderfa) {
    return {
        restrict: 'AC',
        require: '?ngModel',
        //replace: true,
        transclude: true,
        template: '<div class="scroller" ng-transclude></div>',
        link: function (scope, elem, attrs, ngModel) {
            // page skip size
            var skip = 0;
            // create id for element which need to IScroll
            var id = "_" + uniqId();
            elem.attr('id', id);


            //Config IScroll
            var myScroll = new IScroll("#" + id, {
                bounceEasing: 'elastic',
                bounceTime: 1200,
                scrollbars: true,
                mouseWheel: true,
                interactiveScrollbars: true,
                shrinkScrollbars: 'scale',
                fadeScrollbars: false
            });
            // When list scroll to End fetch new data from db.
            myScroll.on('scrollEnd', function () {

                var h = this.maxScrollY;
                var call = this.y - 10;
                if (call < h) {

                    myScroll.refresh();
                }

            });

        },
        scope: {

            fetchFn: '&'
        }
    }
}]);
campsApp.directive('cTab', ['$parse', 'preloaderfa', function ($parse, preloaderfa) {
    return {
        restrict: 'AC',
        require: '?ngModel',
        compile: function (element, attributes) {

            return {
                pre: function (scope, element, attributes, controller, transcludeFn) {

                },
                post: function (scope, element, attributes, controller, transcludeFn) {
                    var id = createId();
                    element.attr('id', id);
                    //  add tab page 
                    $("#" + id + " a").click(function (e) {
                        e.preventDefault();
                        $(this).tab('show');
                    });

                }
            }
        },
        link: function (scope, elem, attrs, ngModel) {
        },
        scope: {


        }
    }
}]);
//creating custom directive
campsApp.directive('customSelect2', ['$parse', function ($parse) {
    return {
        restrict: 'AC',
        require: '?ngModel',
        link: function (scope, elem, attrs, ngModel) {


            var remoteDataConfig = {
                multiple: true,
                tags: true,
                data: scope.tags,
                placeholder: "عنوان را انتخاب کنید... ",

                templateResult: function (option) {

                    //     option.text = option.name;
                    return option.name;
                },
                templateSelection: function (option) {
                    return option.name;
                }
            };

            //console.log(scope);
            elem.select2(remoteDataConfig);
            elem.on("change", function (e) {
                var tb = elem.select2('data');
                var dt = [];
                $.each(tb, function (index, data) {
                    dt.push({ id: data.id, name: data.name });
                });
                scope.$apply(function () {

                    ngModel.$setViewValue(dt);
                });

            });


        },
        scope: {
            tags: '=tags'
        }
    }
}]);
campsApp.directive('remoteSelect2', ['$parse', function ($parse) {
    return {
        restrict: 'E',
        require: '?ngModel',
        transclude: true,
        replace: true,
        template: ' <select style="width:100%" ></select>',
        link: function (scope, elem, attrs, ngModel) {
            var id = createId();
            elem.attr('id', id);
            elem.attr('name', id);

            function formatState(state) {
                if (!state.id) { return state.text; }
                var $state = $(
                  '<option>' + state.text + '</option>'
                );
                return $state;
            };
            var remoteDataConfig = {
                minimumInputLength: scope.minLength,
                dir: "ltr",

                placeholder: "لطفا انتخاب کنید",
                allowClear: true,
                ajax: {
                    url: scope.ajaxUrl,
                    type: 'GET',
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        return {
                            q: params.term, // search term
                            page: params.page
                        };
                    },
                    processResults: function (data, page) {
                        return {
                            results: data
                        };
                    }
                },
                escapeMarkup: function (markup) { return markup; },
                templateResult: formatState
            };

            //console.log(scope);
            $(elem).select2(remoteDataConfig);
            elem.on("select2:select", function (e) {
                scope.selectedfn()(e);
            });


        },
        scope: {
            minLength: '=minLength',

            ajaxUrl: '=ajaxUrl',
            selectedfn: '&'
        }
    }
}]);

campsApp.directive('multiSelect2', ['$parse', function ($parse) {
    return {
        restrict: 'E',
        require: '?ngModel',
        transclude: true,
        replace: true,
        template: ' <select style="width:100%" multiple="multiple"></select>',
        link: function (scope, elem, attrs, ngModel) {
            var id = createId();
            elem.attr('id', id);
            elem.attr('name', id);

            function formatState(state) {
                if (!state.id) { return state.text; }
                var $state = $(
                  '<option>' + state.text + '</option>'
                );
                return $state;
            };
            var remoteDataConfig = {

                dir: "ltr",
                minimumInputLength: scope.minLength,
                placeholder: "لطفا انتخاب کنید",
                allowClear: true,
                ajax: {
                    url: scope.ajaxUrl,
                    type: 'GET',
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        return {
                            q: params.term, // search term
                            page: params.page
                        };
                    },
                    processResults: function (data, page) {
                        return {
                            results: data
                        };
                    }
                },
                escapeMarkup: function (markup) { return markup; },
                templateResult: formatState
            };

            //console.log(scope);
            $(elem).select2(remoteDataConfig);
            elem.on("select2:select", function (e) {
                e.params.data.userId = scope.userId;
                scope.selectedfn()(e);
            });
            elem.on('select2:unselect', function (e) {
                e.params.data.userId = scope.userId;
                scope.removedfn()(e);
            });

        },
        scope: {
            userId: '=userId',
            minLength: '=minLength',
            roleData: '=roleData',
            permissionData: '=permissionData',
            ajaxUrl: '=ajaxUrl',
            selectedfn: '&',
            removedfn: '&'
        }
    }
}]);

campsApp.directive('userRoles', ['$parse', function ($parse) {
    return {
        restrict: 'E',
        require: '?ngModel',
        transclude: true,
        replace: true,
        template: '<span   ><i class="fa fa-user-secret"></i></span>',
        link: function (scope, elem, attrs, ngModel) {
            var id = createId();
            elem.attr('id', id);
            elem.attr('name', id);

            // elem.attr('data-original-title', '<i class="fa fa-spinner fa-pulse"></i>');

            //console.log(scope);
            //$(elem).select2();



            $(elem).mouseenter(function (e) {
                var jp = $.jsPanel({
                    rtl: {
                        rtl: true
                    },
                    ajax: {
                        method: "GET",
                        url: "/api/UserInDeptRoles/GetUserDept?userId=" + scope.userId,
                        done: function (data, textStatus, jqXHR, jsPanel) {
                            //elem.attr('data-original-title', '<p>خالی</p>');
                            if (data.length > 0) {
                                var el = $('<p>');
                                $.each(data, function (key, value) {
                                    el.append('<span>' + value.text + '</span> ');
                                });
                                jsPanel.content.append(el.html());
                                jsPanel.reloadContent();

                            }
                        },
                        fail: function (jqXHR, textStatus, errorThrown, jsPanel) {
                            jsPanel.content.append("<p>خالی است</p>");
                            jsPanel.reloadContent();
                        }
                    },
                    selector: '#' + id,
                    paneltype: {
                        type: 'tooltip',
                        mode: false,
                        position: scope.placement,
                        cornerBG: '#fff',
                        cornerOY: 12
                    },
                    offset: {
                        left: 15,
                        top: 12
                    },
                    size: { width: 250, height: 120 },
                    title: 'دسترسی های کاربر',
                    bootstrap: 'default'
                });

            });
            //elem.on("shown.bs.tooltip", function (e) {
            //    $.ajax({
            //        method: "GET",
            //        url: "/api/UserInDeptRoles/GetUserDept?userId=" + scope.userId

            //    })
            //    .done(function (data) {
            //        elem.attr('data-original-title', '<p>خالی</p>');
            //        if (data.length > 0) {
            //            var el = $('<p>');
            //            $.each(data, function (key, value) {
            //                el.append('<span>' + value.text + '</span> | <br>');
            //            });
            //            elem.attr('data-original-title', el.html());

            //        }
            //    })
            //    .fail(function (data) {
            //        elem.attr('data-original-title', '<p>خطا</p>');
            //    });
            //});

        },
        scope: {
            userId: '=userId',
            placement: '=placement'
        }
    }
}]);
campsApp.directive('findUser', ['$parse', function ($parse) {
    return {
        restrict: 'E',
        require: '?ngModel',
        transclude: true,
        replace: true,
        template: ' <select style="width:100%" ></select>',
        link: function (scope, elem, attrs, ngModel) {
            var id = createId();
            elem.attr('id', id);
            elem.attr('name', id);

            function formatState(state) {
                if (!state.id) { return state.text; }
                var $state = $(
                  '<option>' + state.text + '</option>'
                );
                return $state;
            };
            var remoteDataConfig = {
                minimumInputLength: scope.minLength,
                dir: "ltr",

                placeholder: "لطفا انتخاب کنید",
                allowClear: true,
                ajax: {
                    url: scope.ajaxUrl,
                    type: 'GET',
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        var query = {};
                        query.q = params.term;
                        if (scope.filterByDept) {
                            query.deptId = scope.filterByDept;
                        } else {
                            query.deptId = -1;
                        }
                        return query;
                    },
                    processResults: function (data, page) {
                        return {
                            results: data
                        };
                    }
                },
                escapeMarkup: function (markup) { return markup; },
                templateResult: formatState
            };

            //console.log(scope);
            $(elem).select2(remoteDataConfig);
            elem.on("select2:select", function (e) {
                scope.selectedfn()(e);
            });


        },
        scope: {
            minLength: '=minLength',
            filterByDept: '=filterByDept',
            ajaxUrl: '=ajaxUrl',
            selectedfn: '&'
        }
    }
}]);
campsApp.directive('persianCal', ['$parse', function ($parse) {
    return {
        restrict: 'E',
        require: '?ngModel',
        transclude: true,
        replace: true,
        template: '<input class="pdate"/>',
        link: function (scope, elem, attrs, ngModel) {
            var id = createId();

            elem.attr('id', id);
            elem.attr('name', id);



            var ddd = new AMIB.persianCalendar(id, {
                onchange: function (pdate) {
                    if (pdate) {
                        var dt = pdate.join('/');

                            var d = moment(dt, 'jYYYY/jMM/jDD').format('YYYY/MM/DD');
                            scope.faDate = d;
                            scope.enDate = new Date(d);
                    }
                }
            });

        },
        scope: {
            faDate: '=?faDate',
            enDate: '=?enDate',
            formatDate: '=?formatDate',
            enableTime: '=?enableTime'
        }
    }
}]);
// creating angular controller
//app.controller('Ctrl', function ($scope) {

//    // function to submit the form
//    $scope.submitForm = function () {

//        // check to make sure the form is completely valid
//        if ($scope.userForm.$valid) {
//            alert('form is submitted');
//        }
//    }
//});