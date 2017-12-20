(function () {
    "use strict";
    angular
        .module("publicApp")
        .factory("linksAdminService", LinksAdminService);

    LinksAdminService.$inject = ["$http", "$q"];

    function LinksAdminService($http, $q) {
        return {
            "something"
        };
    }
})();