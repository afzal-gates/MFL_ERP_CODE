(function () {
    'use strict';

    angular.module('multitex.menulesspage').controller('serverMaintenanceController', ['logger', '$scope', '$q', '$http', 'fromData', 'MenuLessPageDataService', '$window', '$location', '$timeout', serverMaintenanceController]);

    function serverMaintenanceController(logger, $scope, $q, $http, fromData, MenuLessPageDataService, $window, $location, $timeout) {

        var vm = this;
        vm.today = new Date();
        vm.form = fromData;
        showMessageLoading();

        var timer = $timeout(function refresh() {
            showMessageLoading();
            timer = $timeout(refresh, 10000);
        }, 10000);


        function showMessageLoading() {
            MenuLessPageDataService.getDataByFullUrl('/api/security/GlobalNotify/GetMsg').then(function (data) {
                if (data.page > 0) {
                    if (Date.parse(data.msgTimeOut) > Date.parse(vm.today)) {
                        $("#alert-placeholder").empty();
                        var alertHtml = '<div class="alert alert-' + htmlEncode(data.alertType) + ' " style="color:red; opacity: 0.95;" alert-dismissible" role="alert"> <i class="fa fa-' + htmlEncode(data.alertType) + '" ></i> <button type="button" class="close" data-dismiss="alert"><span>×</span></button><strong>' + htmlEncode(data.title) + '</strong> <br />' + htmlEncode(data.vMsg) + '</div>';

                        $(alertHtml)
                          .hide()                           //hide the newly created element (this is required for fadeIn to work)
                          .appendTo('#alert-placeholder')   //add it to the palceholder in the page
                          .fadeIn(1500);                      //little flair to grab user attention

                        setTimeout(function () {
                            $("#alert-placeholder").empty();
                        }, 600000);
                    }
                    var url = $location.absUrl();
                    if (url.indexOf('serverMaintenance'))
                        $window.location.href = '/Security/ScUser/SignIn';
                }
            });
        }

    }

})();