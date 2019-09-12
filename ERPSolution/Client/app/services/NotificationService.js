(function () {
    'use strict';
    angular.module('multitex.core').factory('NotificationService', ['$http', 'exception', '$q', '$filter', NotificationService]);

    function NotificationService($http, exception, $q, $filter) {
        var self = this;


        self.getNotificationCount = function () {
            return $http.get('/Home/getNotificationCount')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }

        self.getMessageData = function () {
            return $http.get('/Home/getMessageData')
               .then(function (result) {
                   return result.data;
               }).catch(function (message) {
                   exception.catcher('XHR loading Failded')(message);
               });
        }

        self.getAlertData = function () {
            return $http.get('/Home/getAlertData')
               .then(function (result) {
                   return result.data;
               }).catch(function (message) {
                   exception.catcher('XHR loading Failded')(message);
               });
        }

        self.getEventData = function () {
            return $http.get('/Home/getEventData')
               .then(function (result) {
                   return result.data;
               }).catch(function (message) {
                   exception.catcher('XHR loading Failded')(message);
               });
        }

        self.seenNotification = function (RF_NOTIFICATION_ID) {
            return $http({
                method: 'post',
                url: '/Home/seenNotification',
                params: { RF_NOTIFICATION_ID: RF_NOTIFICATION_ID }
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.seenNotificationByList = function (RF_NOTIFICATION_ID_LIST) {
            return $http({
                method: 'post',
                url: '/Home/seenNotificationByList',
                params: { RF_NOTIFICATION_ID_LIST: RF_NOTIFICATION_ID_LIST }
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        self.getNotification = function () {
            return $http.get('/Home/getNotification')
              .then(function (result) {
                  return result.data;
              }).catch(function (message) {
                  exception.catcher('XHR loading Failded')(message);
              });
        }



        return self;
    }


})();