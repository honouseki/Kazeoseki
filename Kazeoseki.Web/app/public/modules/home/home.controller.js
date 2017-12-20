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

            // Initiates twitch-embed object
            new Twitch.Embed("twitch-embed", {
                width: 900,
                height: 480,
                channel: "carrotnubby",
                theme: "dark"
            });
        }
    }
})();