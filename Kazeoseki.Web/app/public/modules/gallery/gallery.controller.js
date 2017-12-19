(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("galleryController", GalleryController);

    GalleryController.$inject = ["$scope", "galleryService", "imageFileService"];

    function GalleryController($scope, GalleryService, ImageFileService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;
        vm.galleryService = GalleryService;
        vm.imageFileService = ImageFileService;

        function _onInit() {
            console.log("GalleryController");
        }
    }
})();