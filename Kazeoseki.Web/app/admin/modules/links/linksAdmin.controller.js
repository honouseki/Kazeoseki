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

        vm.getLinks = _getLinks;
        vm.getLinksInfo = _getLinksInfo;
        vm.submit = _submit;
        vm.edit = _edit;
        vm.delete = _delete;
        vm.setLinkItem = _setLinkItem;

        vm.items = [];
        vm.linkItem = {};
        vm.linkItemCopy = {};

        function _onInit(){
            console.log("LinksAdminController");
            vm.getLinks();
        }

        function _getLinks() {
            vm.linksAdminService.selectAll()
                .then(success).catch(error);
            function success(res) {
                console.log(res);
                vm.items = res.data.items;
                for (var i = 0; i < vm.items.length; i++) {
                    var url = {};
                    url.urlString = vm.items[i].url;
                    vm.getLinksInfo(i, url);
                }
                console.log(vm.items);
            }
            function error(err) {
                console.log(err);
            }
        }

        function _getLinksInfo(index, url) {
            vm.linksAdminService.getLinkInfo(url)
                .then(success).catch(error);
            function success(res) {
                console.log(res);
                vm.items[index].linkUrlData = res.data.item;
            }
            function error(err) {
                console.log(err);
            }
        }

        function _submit() {
            console.log(vm.linkItem);
            vm.linksAdminService.insert(vm.linkItem)
                .then(success).catch(error);
            function success(res) {
                console.log(res);
                vm.linkItem = {};
                vm.linkItemCopy = {};
            }
            function error(err) {
                console.log(err);
            }
        }

        function _edit() {
            console.log(vm.linkItem);
            vm.linksAdminService.update(vm.linkItem)
                .then(success).catch(error);
            function success(res) {
                console.log(res);
                vm.linkItem = {};
                vm.linkItemCopy = {};
            }
            function error(err) {
                console.log(err);
            }
        }

        function _delete(id) {
            console.log(id);
            vm.linksAdminService.delete(id)
                .then(success).catch(error);
            function success(res) {
                console.log(res);
            }
            function error(err) {
                console.log(err);
            }
        }

        function _setLinkItem(item) {
            vm.linkItem = item;
            vm.linkItemCopy = angular.copy(vm.linkItem.id);
        }
    }
})();