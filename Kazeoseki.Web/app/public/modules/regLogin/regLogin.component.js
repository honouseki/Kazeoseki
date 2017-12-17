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

    RegLoginCompController.$inject = ["$scope", "$rootScope"];

    function RegLoginCompController($scope, $rootScope) {
        var vm = this;
        vm.$scope = $scope;
        vm.$rootScope = $rootScope;
        vm.$onInit = _onInit;
        vm.onLoginSuccess = _onLoginSuccess;

        vm.isLoggedIn = false;

        function _onInit() {
            console.log("RegLoginCompController");
            vm.$scope.$on("loginSuccess", vm.onLoginSuccess);
        }

        function _onLoginSuccess() {
            console.log("Login Successful");
            vm.isLoggedIn = true;
        }
    }
})();