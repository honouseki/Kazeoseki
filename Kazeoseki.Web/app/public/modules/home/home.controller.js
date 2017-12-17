(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("homeController", HomeController);

    HomeController.$inject = ["$scope", "$location", "homeService"];

    function HomeController($scope, $location, HomeService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;
        vm.homeService = HomeService;

        vm.item = {};

        function _onInit() {
            console.log("On Home Controller");
            $location.hash("topOfPage");
            vm.homeService.getEchoTest("On the Home Controller through API")
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