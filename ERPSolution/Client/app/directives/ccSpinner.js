//(function() {
//    'use strict';

//    angular
//        .module('multitex.widgets')
//        .directive('ccSpinner', ccSpinner);

//    // ccSpinner.$inject = ['$window'];

//    /* @ngInject */
//    function ccSpinner ($window) {
//        // Description:
//        //  Creates a new Spinner and sets its options
//        // Usage:
//        //  <div data-cc-spinner="vm.spinnerOptions"></div>
//        var directive = {
//            link: link,
//            restrict: 'A'
//        };
//        return directive;

//        function link(scope, element, attrs) {
//            scope.spinner = null;
//            scope.$watch(attrs.ccSpinner, function(options) {
//                if (scope.spinner) {
//                    scope.spinner.stop();
//                }
//                scope.spinner = new $window.Spinner(options);
//                scope.spinner.spin(element[0]);
//            }, true);
//        }
//    }
//})();


/**
 * dirPagination - AngularJS module for paginating (almost) anything.
 *
 *
 * Credits
 * =======
 *
 * Daniel Tabuenca: https://groups.google.com/d/msg/angular/an9QpzqIYiM/r8v-3W1X5vcJ
 * for the idea on how to dynamically invoke the ng-repeat directive.
 *
 * I borrowed a couple of lines and a few attribute names from the AngularUI Bootstrap project:
 * https://github.com/angular-ui/bootstrap/blob/master/src/pagination/pagination.js
 *
 * Copyright 2014 Michael Bromley <michael@michaelbromley.co.uk>
 */
