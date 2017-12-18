(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("galleryAdminController", GalleryAdminController);

    GalleryAdminController.$inject = ["$scope", "galleryAdminService", "imageFileService"];

    function GalleryAdminController($scope, GalleryAdminService, ImageFileService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;
        vm.galleryAdminService = GalleryAdminService;
        vm.imageFileService = ImageFileService;

        function _onInit() {
            console.log("GalleryAdminController");
        }
    }
})();