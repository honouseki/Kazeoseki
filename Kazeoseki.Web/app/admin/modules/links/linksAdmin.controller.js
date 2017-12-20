(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("linksAdminController", LinksAdminController);

    LinksAdminController.$inject = ["$scope", "linksAdminService"];

    function LinksAdminController($scope, LinksAdminService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;
        vm.linksAdminService = LinksAdminService;

        vm.

        vm.items = [];
        vm.linkItem = {};
        vm.linkItemCopy = {};

        function _onInit(){
            console.log("LinksAdminController");
        }
    }
})();