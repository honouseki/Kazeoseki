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

        vm.getGallery = _getGallery;
        vm.getGalleryImages = _getGalleryImages;
        vm.setImages = _setImages;
        vm.upload = _upload;
        vm.edit = _edit;
        vm.delete = _delete;

        // Set to get gallery images by image type id (1 = Gallery)
        vm.imageType = 1;
        vm.fileBaseUrl = "http://kazeoseki.s3.amazonaws.com/images/";
        vm.item = [];
        vm.images = [];
        vm.imageItem = {};

        function _onInit() {
            console.log("GalleryAdminController");
            vm.getGallery();
        }

        function _getGallery() {
            vm.galleryAdminService.selectAll()
                .then(success).catch(error);
            function success(res) {
                console.log(res);
                vm.item = res.data.items;
                vm.getGalleryImages();
            }
            function error(err) {
                console.log(err);
            }
        }

        function _getGalleryImages() {
            vm.imageFileService.selectByImageType(vm.imageType)
                .then(success).catch(error);
            function success(res) {
                console.log(res);
                vm.images = res.data.items;
                vm.setImages();
            }
            function error(err) {
                console.log(err);
            }
        }

        function _setImages() {
            for (var i = 0; i < vm.item.length; i++) {
                if (vm.item[i].fileId == vm.images[i].fileId) {
                    var sfn = vm.images[i].systemFileName;
                    vm.item[i].displayImg = vm.fileBaseUrl + sfn;
                }
            }
        }

        function _upload() {
            console.log(vm.imageItem);
            //first > upload image
            // second > upload into gallery upon success 

            // (functions within functions)
        }
        function _edit() {
            //delete current image (current item fileId), store that,
            // [re]upload image
            // update gallery with new, returned id
        }
        function _delete() {
            // delete from table
            // delete from amazon
        }
    }
})();