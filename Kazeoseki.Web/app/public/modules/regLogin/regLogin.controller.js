(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("regLoginController", RegLoginController);

    RegLoginController.$inject = ["$scope", "$location", "$rootScope", "$cookies", "regLoginService"];

    function RegLoginController($scope, $location, $rootScope, $cookies, RegLoginService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$rootScope = $rootScope;
        vm.$onInit = _onInit;
        vm.regLoginService = RegLoginService;

        vm.register = _register;
        vm.login = _login;

        vm.regItem = {};
        vm.logItem = {};
        vm.remember = false;

        function _onInit() {
            console.log("regLoginController");
        }

        function _register() {
            console.log(vm.regItem);
            vm.regLoginService.register(vm.regItem)
                .then(success).catch(error);
            function success(res) {
                console.log(res);
                vm.logItem = vm.regItem;
                vm.login();
            }
            function error(err) {
                console.log(err);
            }
        }

        function _login() {
            console.log(vm.logItem);
            vm.regLoginService.login(vm.logItem, vm.remember)
                .then(success).catch(error);
            function success(res) {
                console.log(res);
                if (res.data.item == true) {
                    vm.$rootScope.$broadcast("loginSuccess");
                    $location.path("/home");
                } else {
                    alert("Failed to login");
                };
            }
            function error(err) {
                console.log(err);
                alert("Failed to register");
            }
        }
    }
})();