(function () {
    'use strict';
    angular.module('multitex.menulesspage').factory('MenuLessPageDataService', ['$http', '$filter', 'exception', 'access_token', MenuLessPageDataService]);

    function MenuLessPageDataService($http, $filter, exception, access_token) {
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

        //Save Data with Url
        self.saveDataByUrl = function (data, url) {
            return $http({
                method: 'post',
                url: urlBase + url,
                headers: { 'Authorization': 'Bearer ' + access_token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        //Save Data with Url
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
                     
        self.xmlStringShort = function (data) {
            var xml = '';
            if (angular.isArray(data)) {
                xml += '<trans>';
                angular.forEach(data, function (val, key) {
                    xml += ' <row ';
                    angular.forEach(val, function (Value, Field) {
                        if (angular.isDate(Value)) {
                            Value = $filter('date')(Value, 'dd-MMM-yyyy');
                        }

                        if (Field !== '$$hashKey') {
                            xml += " " + Field + "=\"" + (_.isNil(Value) ? '' : Value) + "\"";
                        }
                    });
                    xml += ' />';
                });
                xml += '</trans>';
            }
            return xml;
        }

        return self;
    }


})();