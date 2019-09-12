(function() {
    'use strict';
    angular.module('multitex.directives', [
        'multitex.security.directives', 'multitex.admin.directives', 'multitex.hr.directives',
        'multitex.mrc.directives', 'multitex.inventory.directives', 'multitex.purchase.directives', 'multitex.knitting.directives']);


    angular.module('multitex.directives').directive('focusMe', function ($timeout) {
        return {
            scope: { trigger: '=focusMe' },
            link: function (scope, element) {
                scope.$watch('trigger', function (value) {
                    if (value === true) {
                        element[0].focus();
                        scope.trigger = false;
                    }
                });
            }
        };
    }).directive('clickAnywhereButHere', function ($document) {
        return {
            restrict: 'A',
            link: function (scope, elem, attr, ctrl) {
                elem.bind('click', function (e) {
                    // this part keeps it from firing the click on the document.
                    e.stopPropagation();
                });
                $document.bind('click', function () {
                    // magic here.
                    scope.$apply(attr.clickAnywhereButHere);
                })
            }
        }
    });


})();