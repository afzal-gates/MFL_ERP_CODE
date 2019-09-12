(function () {
    'use strict';
    angular.module('multitex.core').factory('commonDataService', ['$http', 'exception', commonDataService]);

    function commonDataService($http, exception) {
        var self = this;
        var urlBase = '/api/common';

        //Common Functionality


        //Save
        self.saveItemCategoryData = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/ItemCategory/ItemCategorySave',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        
        //Update
        self.updateItemCategoryData = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/ItemCategory/ItemCategoryUpdate',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

                
        

        //Misc Select
        self.getBankListData = function () {            
            return $http({
                method: 'get',
                url: urlBase + '/BankDataList'
            })
            .then(function (result) {                
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getBranchListData = function (pRF_BANK_ID) {            
            return $http({
                method: 'get',
                url: urlBase + '/BankBranchDataList/' + pRF_BANK_ID
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getBankAccountAutoList = function (pIS_EMP_ACC, pBK_ACC_NO, pRF_BANK_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/BankAccountAutoList',///' + pRF_BANK_ID
                params: { pIS_EMP_ACC: pIS_EMP_ACC, pBK_ACC_NO: pBK_ACC_NO, pRF_BANK_ID: pRF_BANK_ID }
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getCurrencyList = function () {
            return $http({
                method: 'get',
                url: '/api/common/CurrencyList'
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getLookupList = function (Id) {
            return $http({
                method: 'get',
                url: '/api/common/LookupListData/' + Id,
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        //Get Data with full Url
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

        //Save Data with full Url
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

        self.kFilterStr2QueryParam = function (str) {
            var v_str = '';
            if (str.length == 0) {
                return '';
            }
            var obj = str.split('~and~');
            if (obj.length > 0) {
                obj.forEach(function (val, key) {
                    val = val.replace(/'/g, '').replace(/datetime/g, '');
                    v_str += '&p' + val.split('~')[0] + '=' + val.split('~')[2];
                    if (key != obj.length - 1) {
                        v_str += '&'
                    }
                });
            }
            return v_str;
        };

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