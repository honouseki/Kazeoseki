(function () {
    "use strict";
    angular
        .module("publicApp")
        .factory("galleryService", GalleryService);

    GalleryService.$inject = ["$http", "$q"];

    function GalleryService($http, $q) {
        return {
            gallery: _gallery
        };
        function _gallery(){}
    }
})();