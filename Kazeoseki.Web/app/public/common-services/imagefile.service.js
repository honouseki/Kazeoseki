(function () {
    "use strict";
    angular
        .module("publicApp")
        .factory("imageFileService", ImageFileService);

    ImageFileService.$inject = ["$http", "$q"];

    function ImageFileService($http, $q) {
        return {
            image: _image
        }
        function _image(){}
    }
})();