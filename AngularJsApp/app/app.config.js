'use strict';

angular.
    module('demo').
    config(['$routeProvider',
        function config($routeProvider) {
            $routeProvider.
                when('/requests', {
                    template: '<phone-list></phone-list>'
                });
        }
    ]);