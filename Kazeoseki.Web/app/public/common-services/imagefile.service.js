(function () {
    "use strict";
    angular
        .module("publicApp")
        .factory("imageFileService", ImageFileService);

    ImageFileService.$inject = ["$http", "$q"];

    function ImageFileService($http, $q) {
        return {
            selectById: _selectById,
            selectByImageType: _selectByImageType,
            delete: _delete
        }

        function _selectById(id) {
            return $http.get("/api/imagefile/" + id, { withCredentials: true })
                .then(success).catch(error);
        }

        function _selectByImageType(typeId) {
            return $http.get("/api/imagefile/type/" + typeId, { withCredentials: true })
                .then(success).catch(error);
        }

        function _delete(id) {
            return $http.delete("/api/imagefile/" + id, { withCredentials: true })
                .then(success).catch(error);
        }

        function success(res) {
            return res;
        }

        function error(err) {
            return $q.reject(err);
        }
    }
})();