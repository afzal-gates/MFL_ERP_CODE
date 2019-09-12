 (function() {
    'use strict';

    angular.module('multitex.core', [
        /*
         * Angular modules
         */
         'ngSanitize', 'ui.router', 'ngAnimate', 'ngMessages', 'ui.utils', 'kendo.directives', 'daypilot',
        /*
         * Our reusable cross app code modules
         */
        'blocks.exception', 'blocks.logger', 'blocks.router', 'multitex.directives', 'ui.bootstrap.datetimepicker',
        /*
         * 3rd Party modules
         */
         'ngplus', 'ui.utils.masks', 'ngStorage','jkuri.slimscroll', 'ui.slimscroll', 'dualmultiselect', 'gm.datepickerMultiSelect',
         'SignalR', 'ngNotify', 'naif.base64', 'xml', 'oi.select', 'anguFixedHeaderTable'
    ]);
})();