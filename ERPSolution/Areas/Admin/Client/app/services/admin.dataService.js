(function () {
    'use strict';
    angular.module('multitex.admin').factory('AdminService', ['$http', 'exception', AdminService]);

    function AdminService($http, exception) {
        var self = this;

        //self.Sum = function (a, b) {
        //    return a + b;
        //}

        //self.getName = function () {
        //    return $http.get('/Security/Users/SignUp')
        //          .then(geComplete).catch(function (message) {
        //            exception.catcher('XHR loading Failded')(message);
        //          });

        //        function geComplete(data, status, headers, config) {
        //            return data.data;
        //        }
        //}

        self.getSubDepartmentData = function (pDeptID) {
            return $http({
                method: 'get',
                url: '/Hr/HrEmployee/DeptListData',
                params: { pPARENT_ID: pDeptID }
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }


        self.getCompanyData = function () {
            return $http.get('/hr/hrleavetrans/CompanyData')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }


        self.getData = function(){
            return $http.get('/Hr/Admin/HrYearlyCalander/HolidayCalendarData')
                   .then(function (result) {
                   return result.data;
                   }).catch(function (message) {
                                  exception.catcher('XHR loading Failded')(message);
                          });
        }

        self.getPeriodData = function () {
            return $http.get('/Hr/Admin/HrLeaveType/periodtypedata')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }

        self.getLeaveTypeData = function (Id) {
            console.log(Id);
                return $http({
                    method: 'get',
                    url: '/Hr/Admin/HrLeaveType/LeaveTypeData',
                    params: { Id: Id }
                }).then(function (res) {
                    return res.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getActionConfigData = function (RF_ACTION_CFG_H_ID) {
            return $http({
                method: 'get',
                url: '/Hr/Admin/ActionConfig/ActionConfigData',
                params: { RF_ACTION_CFG_H_ID: RF_ACTION_CFG_H_ID }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.saveConfigData = function (data, url, token) {
            return $http({
                method: 'post',
                url: url,
                data: data,
                headers: { "RequestVerificationToken": token }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.SubmitLvlData = function (data, url, token) {
            return $http({
                method: 'post',
                url: url,
                data: data,
                headers: { "RequestVerificationToken": token }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getLvlData = function (RF_ACTION_CFG_H_ID) {
            return $http({
                method: 'get',
                url: '/Hr/Admin/ActionConfig/getLvlData',
                params: { RF_ACTION_CFG_H_ID: RF_ACTION_CFG_H_ID }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getApproverLvlData = function (RF_ACTION_CFG_H_ID, RF_ACTION_CFG_D1_ID) {
            return $http({
                method: 'get',
                url: '/Hr/Admin/ActionConfig/getApproverLvlData',
                params: { RF_ACTION_CFG_H_ID: RF_ACTION_CFG_H_ID, RF_ACTION_CFG_D1_ID: RF_ACTION_CFG_D1_ID }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.submitLvlData = function (data) {
            return $http({
                method: 'post',
                url: '/Hr/Admin/ActionConfig/submitLvlData',
                data: data
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        
        //self.getDataList = function () {
        //    return $http.get('/Admin/HrYearlyCalander/HolidayCalendarListData')
        //           .then(function (result) {
        //               return result.data;
        //           }).catch(function (message) {
        //               exception.catcher('XHR loading Failded')(message);
        //           });
        //}


        return self;
    }


})();