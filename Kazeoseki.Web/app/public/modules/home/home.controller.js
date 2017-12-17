(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("homeController", HomeController);

    HomeController.$inject = ["$scope", "homeService"];

    function HomeController($scope, HomeService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;
        vm.homeService = HomeService;

        vm.item = {};

        function _onInit() {
            console.log("On Home Controller");
            vm.homeService.getEchoTest("testString")
                .then(success).catch(error);
            function success(res) {
                console.log(res);
            }
            function error(err) {
                console.log(err);
            }
        }
    }
})();