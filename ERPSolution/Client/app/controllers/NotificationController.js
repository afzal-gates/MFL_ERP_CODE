(function () {
    'use strict';

    angular.module('multitex').controller('NotificationController', ['logger', 'config', '$scope', '$q', 'ngNotify', 'Hub', '$rootScope', 'NotificationService', 'Dialog', NotificationController]);

    function NotificationController(logger, config, $scope, $q, ngNotify, Hub, $rootScope, NotificationService, Dialog) {

        var vm = this;

        vm.NotifCount = { ALERT: 0, EVENT: 0, MESSAGE: 0 };
        vm.test = 0;
        activate();

        function activate() {
            var promise = [getNotificationCount(), getNotification(), getMessageData()];
            return $q.all(promise).then(function () {
            });
        }


        var hub = new Hub('dashboard', {
            listeners: {
                'newConnection': function (id) {
                    //vm.connected.push(id);
                    //Notify('Title','Description','info', 1); //info,error,success,warn,grimace
                    //getNotification();
                    //$rootScope.$apply();
                },
                'updateNotifCount': function () {
                    getNotificationCount();
                    $rootScope.$apply();
                },
                'showNotification': function () {
                    getNotification();
                    $rootScope.$apply();
                },
                'getMessageData': function () {
                    getMessageData();
                    $rootScope.$apply();
                },
                'getAlertData': function () {
                    getAlertData();
                    $rootScope.$apply();
                },

                'getEventData': function () {
                    getEventData();
                    $rootScope.$apply();
                }


            },
            methods: [],
            errorHandler: function (error) {
                console.error(error);
            }
        });

        vm.connected = [];

        function Notify(title, description, type, RF_NOTIFICATION_ID) {
            ngNotify.set('<b>' + title + '</b> </span><br><small>' + description + '</small>', {
                sticky: true,
                type: type,
                theme: 'pastel',
                html: true,
                RF_NOTIFICATION_ID: RF_NOTIFICATION_ID
            });
        }

        $rootScope.dismiss = function (RF_NOTIFICATION_ID) {
            return NotificationService.seenNotification(RF_NOTIFICATION_ID).then(function () {
                console.log('Seen Notification');
            });
        }

        vm.clearData = function (LK_NOTIF_TYPE_ID) {

            Dialog.confirm('Do you really want to clear 1st 10 Message?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {
                 switch (LK_NOTIF_TYPE_ID) {
                     case 181://Alert
                         var list = _.map(alertData, 'RF_NOTIFICATION_ID').join(',');
                         return NotificationService.seenNotificationByList(list).then(function (res) {
                             console.log('Seen Notification');
                         });

                         break;
                     case 182://Message

                         var list = _.map(vm.messageData, 'RF_NOTIFICATION_ID').join(',');
                         return NotificationService.seenNotificationByList(list).then(function (res) {
                             console.log('Seen Notification');
                         });
                         break;
                     case 183://Event
                         var list = _.map(vm.eventData, 'RF_NOTIFICATION_ID').join(',');
                         return NotificationService.seenNotificationByList(list).then(function (res) {
                             console.log('Seen Notification');
                         });

                         break;
                 }
             });




        }


        function getNotificationCount() {
            return NotificationService.getNotificationCount().then(function (res) {
                vm.NotifCount = res;
            });
        }

        function getMessageData() {
            return NotificationService.getMessageData().then(function (res) {
                vm.messageData = res.map(function (o) {
                    o['CREATION_DATE'] = o.CREATION_DATE ? moment(o.CREATION_DATE).fromNow() : '';
                    return o;
                });
            });
        }

        function getAlertData() {
            return NotificationService.getAlertData().then(function (res) {
                vm.alertData = res.map(function (o) {
                    o['CREATION_DATE'] = o.CREATION_DATE ? moment(o.CREATION_DATE).fromNow() : '';
                    return o;
                });
            });
        }

        function getEventData() {
            return NotificationService.getEventData().then(function (res) {
                vm.eventData = res.map(function (o) {
                    o['CREATION_DATE'] = o.CREATION_DATE ? moment(o.CREATION_DATE).fromNow() : '';
                    return o;
                });
            });
        }


        vm.navigate = function (IS_REDIRECT, url,ID) {

            if (IS_REDIRECT=='N') {
                return;
            }

            $rootScope.dismiss(ID);

            var a = document.createElement('a');
            a.href = url;
            a.target = '_blank';
            document.body.appendChild(a);
            a.click();
        }

        function getNotification() {
            return NotificationService.getNotification().then(function (res) {
                if (res.RF_NOTIFICATION_ID && res.IS_STICKY == 'Y') {
                    Notify(res.NOTIFICATION_TITLE, res.NOTIFICATION_DESC_EN, res.COLOR_CODE, res.RF_NOTIFICATION_ID); //info,error,success,warn,grimace
                }

            });
        }

    }



})();