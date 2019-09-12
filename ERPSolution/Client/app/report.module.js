(function() {
    'use strict';

    angular.module('multitex.report', [
        'xml',
        'blocks.router',
        'blocks.exception',
        'multitex.mrc',
        'multitex.knitting',
        'kendo.directives',
        'multitex.dyeing',
    ]);

    angular.module('multitex.report').config(toastrConfig);

    function toastrConfig(toastr) {
        toastr.options.timeOut = 4000;
        toastr.options.positionClass = 'toast-bottom-right';
    }

    angular.module('multitex.report').constant('toastr', toastr)


})();