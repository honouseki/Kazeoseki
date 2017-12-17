(function () {
    "use strict";
    angular
        .module("publicApp")
        .factory("regLoginService", RegLoginService);

    RegLoginService.$inject = ["$http", "$q"];

    function RegLoginService($http, $q) {
        return {
            register: _register,
            login: _login
        };

        function _register(data) {
            return $http.post("api/user", data, { withCredentials: true })
                .then(success).catch(error);
        }

        function _login(data, remember) {
            return $http.post("api/user/login/" + remember, data, { withCredentials: true })
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