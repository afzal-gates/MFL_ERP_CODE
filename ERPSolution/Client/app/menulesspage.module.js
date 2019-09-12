(function() {
    'use strict';

    angular.module('multitex.menulesspage', [
        'blocks.router',
        'blocks.exception',
        'ui.bootstrap',
        'ui.slimscroll',
        'ngStorage',
        'ngSanitize'
    ]);

    angular.module('multitex.menulesspage').config(toastrConfig);

    function toastrConfig(toastr) {
        toastr.options.timeOut = 4000;
        toastr.options.positionClass = 'toast-bottom-right';
    }

    angular.module('multitex.menulesspage').constant('toastr', toastr);
    angular.module('multitex.menulesspage').directive('focusMe', function ($timeout) {
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
    });
    angular.module('multitex.menulesspage').controller('ConfirmModalController', ['$modalInstance', 'data', ConfirmModalController]);
    function ConfirmModalController($modalInstance, data) {
        var vm = this;

        vm.cancel = cancel;
        vm.ok = ok;
        vm.properties = data;

        function cancel() {
            $modalInstance.dismiss();
        }

        function ok() {
            $modalInstance.close();
        }
    };

    angular.module('multitex.menulesspage').factory('Dialog', ['$modal', Dialog]);
    function Dialog($modal) {
                var self = this;
                self.confirm=function(message, title, buttons) {
                    var modalInstance = $modal.open({
                        template: "<div class='modal-header' style='padding-bottom: 3px;padding-top: 5px;background-color: #5e738b;'><h4 style='color:white;'><b>{{vm.properties.title}}</b></h4></div><div class='modal-body' ng-bind-html='vm.properties.message'></div><div class='modal-footer' style='padding-top:5px;padding-bottom:5px;height:1000px;'><button class='btn btn-primary btn-lg' ng-click='vm.ok()'>{{vm.properties.buttons[0]}}</button><button class='btn btn-default btn-sm' ng-click='vm.cancel()'>{{vm.properties.buttons[1]}}</button></div>",
                        controller: 'ConfirmModalController',
                        controllerAs: 'vm',
                        resolve: {
                            data: function () {
                                return {
                                    message: message,
                                    title: title,
                                    buttons: buttons
                                };
                            }
                        },
                        size: 'lg',
                        windowClass: 'app-modal-window',
                    });

                    return modalInstance.result;
                }

                return self;
    }


})();