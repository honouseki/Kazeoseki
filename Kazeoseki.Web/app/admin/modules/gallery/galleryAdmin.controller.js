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
        vm.separateImageInfo = _separateImageInfo;
        vm.edit = _edit;
        vm.delete = _delete;
        vm.setImageItem = _setImageItem;

        // Set to get gallery images by image type id (1 = Gallery)
        vm.imageType = 1;
        vm.fileBaseUrl = "http://kazeoseki.s3.amazonaws.com/images/";
        vm.item = [];
        vm.images = [];
        vm.imageItem = {};
        vm.imageItemCopy = {};
        vm.imgUploadItem = {};
        vm.$scope.currentImg = null;
        vm.$scope.currentCroppedImg = null;

        var handleFileSelect = function (evt) {
            var file = evt.currentTarget.files[0];
            var reader = new FileReader();
            reader.onload = function (evt) {
                $scope.$apply(function ($scope) {
                    $scope.currentImg = evt.target.result;
                });
            };
            reader.readAsDataURL(file);
        }
        angular.element(document.querySelector("#imgFileInput")).on("change", handleFileSelect);

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
                for (var j = 0; j < vm.images.length; j++) {
                    if (vm.item[i].fileId == vm.images[j].fileId) {
                        var sfn = vm.images[j].systemFileName;
                        vm.item[i].displayImg = vm.fileBaseUrl + sfn;
                    }
                }
            }
        }
        function _upload() {
            // Uploads the image file
            vm.separateImageInfo(vm.$scope.currentCroppedImg);
            vm.galleryAdminService.insertFile(vm.imgUploadItem)
                .then(success).catch(error);
            function success(res) {
                console.log(res.data);
                insertGallery(res.data.item);
            }
            function error(err) {
                console.log(err);
            }
            // Uploads into the gallery
            function insertGallery(id) {
                vm.imageItem.FileId = id;
                vm.galleryAdminService.insert(vm.imageItem)
                    .then(success).catch(success);
                function success(res) {
                    console.log(res);
                    vm.imageItem = {};
                    vm.imageItemCopy = {};
                    vm.imgUploadItem = {};
                }
                function error(err) {
                    console.log(err);
                }
            }
        }
        function _separateImageInfo(img) {
            // Extracts 64base string and image extension of given image file
            var imageInfo = img.split(",");
            var getExtension = imageInfo[0].split("/");
            var extension = getExtension[1].split(";");
            vm.imgUploadItem.encodedImageFile = imageInfo[1];
            vm.imgUploadItem.fileExtension = "." + extension[0];
        }
        function _edit() {
            // Current Copy
            console.log(vm.imageItem);
            // Original Copy
            console.log(vm.imageItemCopy);
            vm.separateImageInfo(vm.$scope.currentCroppedImg);
            console.log(vm.imgUploadItem);
            // Deletes original image from AmazonS3
            vm.imageFileService.delete(vm.imageItemCopy.fileId)
                .then(success).catch(error);
            function success(res) {
                console.log(res);
                reUpload();
            }
            function error(err) {
                console.log(err);
            }
            // Re-uploads image from encoded image
            function reUpload() {
                vm.galleryAdminService.insertFile(vm.imgUploadItem)
                    .then(success).catch(error);
                function success(res) {
                    console.log(res.data);
                    updateGallery(res.data.item);
                }
                function error(err) {
                    console.log(err);
                }
            }
            // Updates Gallery
            function updateGallery(fileId) {
                vm.imageItem.FileId = fileId;
                vm.galleryAdminService.update(vm.imageItem.id, vm.imageItem)
                    .then(success).catch(error);
                function success(res) {
                    console.log(res);
                    vm.imageItem = {};
                    vm.imageItemCopy = {};
                    vm.imgUploadItem = {};
                }
                function error(err) {
                    console.log(err);
                }
            }
        }
        function _delete() {
            console.log(vm.imageItem);
            // Deletes from gallery
            vm.galleryAdminService.delete(vm.imageItem.id)
                .then(success).catch(error);
            function success(res) {
                console.log(res);
                deleteFile(vm.imageItem.fileId);
            }
            function error(err) {
                console.log(err);
            }
            // Deletes from AmazonS3
            function deleteFile(id) {
                vm.imageFileService.delete(id)
                    .then(success).catch(error);
                function success(res) {
                    console.log(res);
                    vm.imageItem = {};
                }
                function error(err) {
                    console.log(err);
                }
            }
        }
        function _setImageItem(item) {
            vm.imageItem = item;
            vm.imageItemCopy = angular.copy(vm.imageItem);
        }
    }
})();