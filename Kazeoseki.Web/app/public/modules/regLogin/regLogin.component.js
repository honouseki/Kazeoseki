(function () {
    "use strict";
    angular
        .module("publicApp")
        .component("regLoginDetails", {
            templateUrl: "/app/public/modules/reglogin/regLoginDetails.html",
            controller: "regLoginCompController"
        });
})();

(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("regLoginCompController", RegLoginCompController);

    RegLoginCompController.$inject = ["$scope"];

    function RegLoginCompController($scope) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;

        function _onInit() {
            console.log("RegLoginCompController");
        }
    }
})();