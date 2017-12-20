(function () {
    "use strict";
    var app = angular.module("publicApp" + ".routes", []);

    app.config(_configureStates);

    _configureStates.$inject = ["$stateProvider", "$locationProvider", "$urlRouterProvider"];

    function _configureStates($stateProvider, $locationProvider, $urlRouterProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
        $urlRouterProvider.otherwise('/home');
        $stateProvider
            .state({
                name: 'home',
                url: '/home',
                templateUrl: '/app/public/modules/home/home.html',
                title: 'Home',
                controller: 'homeController as homeCtrl'
            })
            .state({
                name: 'regLogin',
                url: '/register_login',
                templateUrl: '/app/public/modules/regLogin/regLogin.html',
                title: 'Register/Login',
                controller: 'regLoginController as regLogCtrl'
            })
            .state({
                name: 'about',
                url: '/about',
                templateUrl: '/app/public/modules/about/about.html',
                title: 'About Me'
            })
            .state({
                name: 'gallery',
                //url: '/gallery/{category}/{subcategory}'
                url: '/gallery',
                templateUrl: '/app/public/modules/gallery/gallery.html',
                title: 'Gallery',
                controller: 'galleryController as galleryCtrl'
            })


            // Admin tab routes
            .state({
                name: 'galleryAdmin',
                url: '/galleryAdmin',
                templateUrl: '/app/admin/modules/gallery/galleryAdmin.html',
                title: 'Gallery Management',
                controller: 'galleryAdminController as aGalleryCtrl'
            })
            .state({
                name: 'linksAdmin',
                url: '/linksAdmin',
                templateUrl: '/app/admin/modules/links/linksAdmin.html',
                title: 'Inspiration Links Management',
                controller: 'linksAdminController as aLinksCtrl'
            });
    }
})();