(function () {
    'use strict';
    angular.module('multitex.core').factory('DashBoardService', ['$http', 'exception', '$q', '$filter', 'access_token', DashBoardService]);

    function DashBoardService($http, exception, $q, $filter, access_token) {
        var self = this;


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

        self.SalaryAdvReqesterNotif = function () {
            return $http.get('/Home/SalaryAdvReqesterNotif')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }

        self.SalaryAdvApproverNotif = function () {
            return $http.get('/Home/SalaryAdvApproverNotif')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }

        self.OnlineLeaveReqesterNotif = function () {
            return $http.get('/Home/OnlineLeaveReqesterNotif')
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.OnlineLeaveApproverNotif = function () {
            return $http.get('/Home/OnlineLeaveApproverNotif')
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.LabdipRequestProgramNotif = function () {
            return $http.get('/api/mrc/Labdip/LabdipProgramForDB')
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.LabdipRequestProgramNotifStyleWise = function (SearchParams) {
            return $http.get('/api/mrc/Labdip/LabdipProgramForDBStyleWise?pSearchParams=' + SearchParams)
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.UpdateLDReqStatus = function (pMC_LD_REQ_H_ID, pRF_ACTN_STATUS_ID, MC_LD_REQ_D_ID) {
            return $http.get('/api/mrc/Labdip/BatchUpdateLabdip/' + pMC_LD_REQ_H_ID + '/' + pRF_ACTN_STATUS_ID + '/' + MC_LD_REQ_D_ID).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getNotifications = function (HR_LEAVE_TRANS_ID) {
            var data = [];
            return $http({
                method: 'get',
                url: '/hr/hrleavetrans/Notifications',
                params: { HR_LEAVE_TRANS_ID: HR_LEAVE_TRANS_ID}
            }).then(function (res) {
                angular.forEach(res.data, function (val, key) {
                    val['ACTION_DATE'] = $filter('date')(moment(val.ACTION_DATE)._d, 'medium');
                    val['CREATION_DATE'] = $filter('date')(moment(val.CREATION_DATE)._d, 'medium');
                    
                    data.push(val);
                });

                console.log(data);
                return data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }
        
        self.getSalAdvNotif = function (HR_SAL_ADVANCE_ID) {
            var data = [];
            return $http({
                method: 'get',
                url: '/hr/HrSalAdvance/SalAdvNotif',
                params: { HR_SAL_ADVANCE_ID: HR_SAL_ADVANCE_ID }
            }).then(function (res) {
                angular.forEach(res.data, function (val, key) {
                    val['ACTION_DATE'] = $filter('date')(moment(val.ACTION_DATE)._d, 'medium');
                    val['CREATION_DATE'] = $filter('date')(moment(val.CREATION_DATE)._d, 'medium');

                    data.push(val);
                });
                return data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getDataByUrl = function (url) {
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

        self.saveDataByUrl = function (data, url, token) {
            console.log('============');
            console.log(data);
            console.log(token);

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
        
        return self;
    }


})();