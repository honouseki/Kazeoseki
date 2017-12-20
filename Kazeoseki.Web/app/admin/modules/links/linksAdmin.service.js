(function () {
    "use strict";
    angular
        .module("publicApp")
        .factory("linksAdminService", LinksAdminService);

    LinksAdminService.$inject = ["$http", "$q"];

    function LinksAdminService($http, $q) {
        return {
            getLinkInfo: _getLinkInfo,
            insert: _insert,
            selectAll: _selectAll,
            selectById: _selectById,
            update: _update,
            delete: _delete
        };

        function _getLinkInfo(data) {
            return $http.post("/api/linkurldata", data, { withCredentials: true })
                .then(success).catch(error);
        }

        function _insert(data) {
            return $http.post("/api/link", data, { withCredentials: true })
                .then(success).catch(error);
        }

        function _selectAll() {
            return $http.get("/api/link", { withCredentials: true })
                .then(success).catch(error);
        }

        function _selectById(id) {
            return $http.get("/api/link/" + id, { withCredentials: true })
                .then(success).catch(error);
        }

        function _update(data) {
            return $http.put("/api/link/" + data.id, data, { withCredentials: true })
                .then(success).catch(error);
        }

        function _delete(id) {
            return $http.delete("/api/link/" + id, { withCredentials: true })
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