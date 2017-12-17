(function () {
    "use strict";
    angular
        .module("publicApp")
        .component("regLoginDetails", {
            templateUrl: "/app/public/modules/reglogin/regLoginDetails.html",
            controller: "regLoginController"
        });
})();

(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("regLoginController", RegLoginController);

    RegLoginController.$inject = ["$scope"];

    function RegLoginController($scope) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;

        function _onInit() {
            console.log("RegLoginController");
        }
    }
})();