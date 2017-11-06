campsApp.service("userInDeptRoleService", function ($http, $cacheFactory) {

    var allUserInDeptRoleCache = $cacheFactory('allUserInDeptRoleCache');
    var observerCallbacks = [];

    //register an observer
    this.watchOberver = function (callback) {
        observerCallbacks.push(callback);
    };

    //call this when you know 'foo' has been changed
    this.notifyObservers = function () {
        angular.forEach(observerCallbacks, function (callback) {
            callback();
        });
    };

    var baseUrl = "/api/UserInDeptRoles/";

    function getPagedResource(controlName, skip) {
        return $http.get(controlName + skip);//.then(function (response) {
    }

    this.loadData = function () {
        allUserInDeptRoleCache.removeAll();
        this.notifyObservers();
    }
    this.updateConfirmer = function (ischecked, userid) {
        var model= {
            isConfirm: ischecked,
            userId:userid
        }
        var obj = JSON.stringify(model);
        var item = $http.post(baseUrl + "ConfirmerPost/", obj);
        return item;
    };
    this.post = function (model) {
        var obj = JSON.stringify(model);
        var post = $http.post(baseUrl + "Post/", obj);
        return post;
    };
    this.addToPost = function (userid, deptid) {
        var model = { userId: userid, deptId: deptid };
        var obj = JSON.stringify(model);
        var post = $http.post(baseUrl + "AddToPost/", obj);
        return post;
    };
    this.removeFromPost = function (userid, deptid) {
        var model = { userId: userid, deptId: deptid };
        var obj = JSON.stringify(model);
        var post = $http.post(baseUrl + "RemoveFromPost/", obj);
        return post;
    };
    this.delete = function (id) {

        return $http.delete(baseUrl + "Delete/" + id);

    };
    this.edit = function (id, model) {
        return $http.put(baseUrl + "?id=" + id, model);
    };
    this.getAlls = function () {

        return $http.get(baseUrl + "get", { cache: this.allUserInDeptRoleCache });
    };
    this.getByUserId = function (userId) {

        return $http.get(baseUrl + "GetUserDept?userId="+userId);
    };
    this.getPaging = function (skip, pageSize) {
        return $http.get(baseUrl + "?skip=" + skip + "&pageSize=" + pageSize, { cache: this.allUserInDeptRoleCache });
    };
});
