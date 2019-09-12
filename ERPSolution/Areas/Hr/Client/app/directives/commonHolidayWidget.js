(function () {
    'use strict';

    angular
        .module('multitex.hr.directives')
        .directive('commonHolidayValidator', [commonHolidayValidator]);

    // ccSpinner.$inject = ['$window'];

    /* @ngInject */
    function commonHolidayValidator() {

        var directive = {
            require: 'ngModel',
            link: link,
            restrict: 'A'
        };
        return directive;

        function link($scope, element, attrs, ngModel) {
            //console.log(ngModel);
           // alert(ngModel);
            ngModel.$validators.commonHolidayCheck = function (value) {
                //console.log(value);
                var status = true;
                if ((value == null||value=='') ) {
                    status = false;
                }
                return status;
            };
        }
    }
})();