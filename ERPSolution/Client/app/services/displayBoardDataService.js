(function () {
    'use strict';
    angular.module('multitex.displayboard').factory('DisplayboardDataService', ['$http', 'exception', 'access_token', DisplayboardDataService]);

    function DisplayboardDataService($http, exception, access_token) {
        var self = this;
        //Save Data with Full Url
        self.saveDataByFullUrl = function (data, url) {
            return $http({
                method: 'post',
                url: url,
                headers: { 'Authorization': 'Bearer ' + access_token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }
        //Get Data By Full Url
        self.getDataByFullUrl = function (url) {
            return $http({
                method: 'get',
                url: url,
                headers: { 'Authorization': 'Bearer ' + access_token }
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }
     
        return self;
    }


})();