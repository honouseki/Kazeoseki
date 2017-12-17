(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("regLoginController", RegLoginController);

    RegLoginController.$inject = ["$scope", "regLoginService"];

    function RegLoginController($scope, RegLoginService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;
        vm.regLoginService = RegLoginService;

        vm.register = _register;
        vm.login = _login;

        vm.item = {};

        function _onInit() {
            console.log("regLoginController");
        }

        function _register() {
            console.log(vm.item);
            //vm.regLoginService.register(vm.item)
            //    .then(success).catch(error);
            function success(res) {
                console.log(res);
                vm.item = {};
            }
            function error(err) {
                console.log(err);
            }
        }

        function _login() {
            console.log(vm.item);
            //vm.regLoginService.login(vm.item)
            //    .then(success).catch(error);
            function success(res) {
                console.log(res);
                vm.item = {};
            }
            function error(err) {
                console.log(err);
            }
        }
    }
})();