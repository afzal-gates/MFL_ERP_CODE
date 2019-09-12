/* global toastr:false, moment:false */
(function() {
    'use strict';

    angular
        .module('multitex.core')
        .constant('toastr', toastr)
        .constant('moment', moment);
})();
