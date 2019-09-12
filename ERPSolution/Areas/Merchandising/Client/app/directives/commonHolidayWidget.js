(function () {
    'use strict';

    angular
        .module('multitex.mrc.directives')
        .directive('mtexCommonHolidayValidator', [mtexCommonHolidayValidator]);

    function mtexCommonHolidayValidator() {
        var directive = {
            require: 'ngModel',
            link: link,
            restrict: 'A'
        };
        return directive;


        function link($scope, element, attrs, ngModel) {
            ngModel.$validators.commonHolidayCheck = function (value) {
                var status = true;
                if ((value == null||value=='') ) {
                    status = false;
                }
                return status;
            };
        }
    }
})();