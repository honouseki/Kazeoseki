(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("homeController", HomeController);

    HomeController.$inject = ["homeService"];

    function HomeController(HomeService) {
        var vm = this;
        vm.$onInit = _onInit;
        vm.homeService = HomeService;

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