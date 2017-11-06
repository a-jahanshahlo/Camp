campsApp.service("galleryService", function ($http) {
    function getPagedResource(controlName, skip) {
        return $http.get(controlName + skip);//.then(function (response) {

        //return {
        //    totalRecords: parseInt(response.headers('X-InlineCount')),
        //    results: custs
        //};
        // });
    }
    var baseUrl = "/api/gallery/";
    var pageSkip = 0;
    this.resetPagesize = function () {
        pageSkip = 0;
    };
    this.getAllGalleries = function () {
       
        return getPagedResource(baseUrl + "getAllGalleries/", null);
    };
    this.getGalleries = function (skip) {
        var sk = skip ? skip : pageSkip;
        return getPagedResource(baseUrl + "getgalleries/", sk);
    };
    this.newGalleriy = function (name) {
        var request =   $http.post(baseUrl + "NewGalleriy", "'" + name + "'");
        return request;
     
    };
    this.deleteGalleryById = function (id) {
        var deleteitem = $http.delete(baseUrl +"?id="+ id);
        return deleteitem;

    };
    this.getGalleryById = function (id) {

        return getPagedResource(baseUrl + "getgallerybyid/", id);
    };
    this.getFiles = function (skip) {

        return getPagedResource(baseUrl + "GetFileList/?skip=", skip);
    };
    this.addFileToGallery = function (id,files) {
       
        var xsrf =JSON.stringify({ id: id, files: files });
        var post = $http.post(baseUrl + "AddToGallery/", xsrf );
        return post;
    };
    //-----------rename selected gallary title
    this.renameGallery = function (id, galleryName) {

        var xsrf = JSON.stringify({ id: id, galleryName: galleryName });
        var post = $http.post(baseUrl + "renameGallery/", xsrf);
        return post;
    };
    //----------delete selected file
    this.deleteFile = function (guid) {
        //var deleteitem = $http.delete(baseUrl + "?id=" + id);
        //return deleteitem;
        return $http.delete("/api/Files/DeleteFile/" + guid);
       
    };
    //----------download selected file
    this.downloadFile = function (guid) {
        window.open("/api/Files/Download/" +guid, '_blank', '');
       
        return ;
    };

    //----------delete selected file
    this.getTotalFiles = function (guid) {
        var get = $http.get("/api/Files/" + "DeleteFile/", guid);
        return get;
    };
    //---------update selected file
    this.updateFile = function (file) {

        var data = JSON.stringify(file);
        var post = $http.post("/api/Files/" + "UpdateFile/", data);
        return post;
    };
});
