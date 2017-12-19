(function () {
    "use strict";
    angular
        .module("publicApp")
        .factory("galleryAdminService", GalleryAdminService);

    GalleryAdminService.$inject = ["$http", "$q"];

    function GalleryAdminService($http, $q) {
        return {
            insert: _insert,
            insertFile: _insertFile,
            selectAll: _selectAll,
            update: _update,
            delete: _delete
        };

        function _insert(data) {
            return $http.post("/api/gallery", data, { withCredentials: true })
                .then(success).catch(error);
        }

        function _insertFile(data) {
            return $http.post("/api/gallery/file", data, { withCredentials: true })
                .then(success).catch(error);
        }

        function _selectAll() {
            return $http.get("/api/gallery", { withCredentials: true })
                .then(success).catch(error);
        }

        function _update(id, data) {
            return $http.put("/api/gallery/" + id, data, { withCredentials: true })
                .then(success).catch(error);
        }

        function _delete(id) {
            return $http.delete("/api/gallery/" + id, { withCredentials: true })
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