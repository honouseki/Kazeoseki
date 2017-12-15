﻿(function () {
    "use strict";
    var app = angular.module("publicApp" + ".routes", []);

    app.config(_configureStates);

    _configureStates.$inject = ["$stateProvider", "$locationProvider", "$urlRouterProvider"];

    function _configureStates($stateProvider, $locationProvider, $urlRouterProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
        //$urlRouterProvider.otherwise('/home').;
        $stateProvider
            .state({
                name: 'home',
                url: '/home',
                templateUrl: '/app/public/modules/home/home.html',
                title: 'Home',
                controller: 'homeController as homeCtrl'
            })
            .state({
                name: 'about',
                url: '/about',
                templateUrl: '/app/public/modules/about/about.html',
                title: 'About Me'
                //,controller: 'homeController as homeCtrl'
            });
    }
})();