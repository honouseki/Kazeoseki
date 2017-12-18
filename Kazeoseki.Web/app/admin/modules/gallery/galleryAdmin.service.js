(function () {
    "use strict";
    angular
        .module("publicApp")
        .factory("galleryAdminService", GalleryAdminService);

    GalleryAdminService.$inject = ["$http", "$q"];

    function GalleryAdminService($http, $q) {
        return {
            gallery: _gallery
        };
        function _gallery(){}
    }
})();