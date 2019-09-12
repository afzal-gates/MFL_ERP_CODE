(function () {
    'use strict';
    angular.module('multitex.hr').factory('HrService', ['$http', 'exception', '$q', '$filter', 'access_token', HrService]);

    function HrService($http, exception, $q, $filter, access_token) {
        var self = this;

        self.getIndexByKeyValue = function (arraytosearch, key, valuetosearch) {

            for (var i = 0; i < arraytosearch.length; i++) {
                if (arraytosearch[i][key] == valuetosearch) {
                    return i;
                }
            }
            return null;
        }

        self.kFilterStr2QueryParam = function (str) {
            var v_str = '';
            if (str.length == 0) {
                return '';
            }
            var obj = str.split('~and~');
            if (obj.length > 0) {
                obj.forEach(function (val, key) {

                    if (val.search('datetime') < 0) {
                        val = val.replace(/'/g, '').replace(/datetime/g, '');
                        v_str += '&p' + val.split('~')[0] + '=' + val.split('~')[2];
                        if (key != obj.length - 1) {
                            v_str += '&'
                        }
                    } else {
                        val = val.replace(/'/g, '').replace(/datetime/g, '');
                        v_str += '&p' + val.split('~')[0] + '=' + moment(val.split('~')[2], "YYYY-MM-DD").format("DD-MMM-YYYY");
                        if (key != obj.length - 1) {
                            v_str += '&'
                        }
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


        self.batchSaveBkAccAssigWithEmp = function (data, token) {
            return $http({
                method: 'post',
                url: '/api/hr/BatchSaveBkAccAssigWithEmp',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        self.getAttenDataList = function (pData) {
            return $http({
                method: 'get',
                url: '/Hr/HrOtApprove/AttenDataList',
                params: pData
            })            
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }
        
        self.getEmployeeList = function (deptID, jobStatus, empCode, empOldCode) {
            return $http({
                method: 'post',
                url: '/Hr/HrEmployee/EmployeeListData',
                data: { pHR_DEPARTMENT_ID: deptID, pLK_JOB_STATUS_ID: jobStatus, pEMPLOYEE_CODE: empCode, pOLD_EMP_CODE: empOldCode }

            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.EmployeeAutoDataList = function (pEMPLOYEE_CODE, pLK_JOB_STATUS_ID, pHR_COMPANY_ID) {
            return $http({
                method: 'post',
                url: '/Hr/HrEmployee/EmployeeAutoData',
                data: { pEMPLOYEE_CODE: pEMPLOYEE_CODE, pLK_JOB_STATUS_ID: pLK_JOB_STATUS_ID, pHR_COMPANY_ID: pHR_COMPANY_ID }
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getManagementTypeData = function () {
            return $http({
                method: 'get',
                url: '/Hr/Admin/HrDesignation/ManagementTypeListData'
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getEmployeeTypeData = function () {
            return $http({
                method: 'get',
                url: '/Hr/Admin/HrDesignation/EmployeeTypeListData'
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }


        self.saveDataByUrl = function (data, url) {
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
        };



        //Misc Select
        self.GetAllOthers = function (url) {
            return $http({
                method: 'get',
                url: url                
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.GetDataByUrl = function (url) {
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


        // =========== Start For Salary =================
        self.getAttenSummeryData = function (pACC_PAY_PERIOD_ID, pHR_EMPLOYEE_ID) {
            return $http({
                method: 'get',
                url: '/Hr/HrSalaryTran/AttenSummeryData',
                params: { pACC_PAY_PERIOD_ID: pACC_PAY_PERIOD_ID, pHR_EMPLOYEE_ID: pHR_EMPLOYEE_ID }
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getSalaryTranHdrData = function (pACC_PAY_PERIOD_ID, pHR_EMPLOYEE_ID) {
            return $http({
                method: 'get',
                url: '/Hr/HrSalaryTran/SalaryTranHdrData',
                params: { pACC_PAY_PERIOD_ID: pACC_PAY_PERIOD_ID, pHR_EMPLOYEE_ID: pHR_EMPLOYEE_ID }
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getSalaryPayListData = function (pHR_SAL_TRAN_H_ID) {
            return $http({
                method: 'get',
                url: '/Hr/HrSalaryTran/SalaryPayListData',
                params: { pHR_SAL_TRAN_H_ID: pHR_SAL_TRAN_H_ID }
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getPayElementListData = function (pIS_ACTIVE, pIS_CORE_SAL_PART, pLK_PAY_ELM_TYPE_ID) {
            return $http({
                method: 'get',
                url: '/Hr/HrEmployee/PayElementListData?pIS_ACTIVE=' + pIS_ACTIVE + '&pIS_CORE_SAL_PART=' + pIS_CORE_SAL_PART + '&pLK_PAY_ELM_TYPE_ID=' + pLK_PAY_ELM_TYPE_ID
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getEmpPayListData = function (pHR_EMPLOYEE_ID) {
            return $http({
                method: 'get',
                url: '/Hr/HrEmployee/EmpPayListData',
                params: { pHR_EMPLOYEE_ID: pHR_EMPLOYEE_ID }
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getDesigPayListData = function (pHR_DEPARTMENT_ID, pHR_PAY_ELEMENT_ID) {
            return $http({
                method: 'get',
                url: '/Hr/HrDesigPay/DesigPayListData',
                params: { pHR_DEPARTMENT_ID: pHR_DEPARTMENT_ID, pHR_PAY_ELEMENT_ID: pHR_PAY_ELEMENT_ID }
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getSalaryBreakupListData = function (pGROSS_SALARY) {            
            return $http({
                method: 'get',
                url: '/Hr/HrEmployee/SalaryBreakupListData',
                params: { pGROSS_SALARY: pGROSS_SALARY }
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }
        // =========== End For Salary =================

        self.getOtTeamListData = function () {
            return $http({
                method: 'get',
                url: '/Hr/HrOtEntitled/OtTeamListData'
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }


        self.getRejoinListData = function (pHR_COMPANY_ID, pACC_PAY_PERIOD_ID) {
            return $http({
                method: 'get',
                url: '/Hr/HrEmployee/RejoinListData',
                params: { pHR_COMPANY_ID: pHR_COMPANY_ID, pACC_PAY_PERIOD_ID: pACC_PAY_PERIOD_ID }
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.deleteRejoinData = function (data, token) {            
            return $http({
                headers: { "RequestVerificationToken": token },
                method: 'post',
                url: '/Hr/HrEmployee/EmployeeRejoinDelete',
                data: { ob: data[0] }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getDesigData = function (pDeptID) {
            //alert(pDeptID);
            return $http({
                method: 'get',
                url: '/Hr/HrEmployee/DesigListData',
                params: { pOrganoDeptId: pDeptID }
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getNonEntitledDesigListData = function () {
            return $http({
                method: 'get',
                url: '/Hr/HrOtEntitled/NonEntitledDesigListData'                
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getEntitledDesigListData = function (pHR_OT_TEAM_ID) {
            return $http({
                method: 'get',
                url: '/Hr/HrOtEntitled/EntitledDesigListData',
                params: { pHR_OT_TEAM_ID: pHR_OT_TEAM_ID }
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

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
        
        self.getPunchData = function () {
            return $http.get('/Hr/HrTaRawData/PunchListData')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }

        self.getDeviceData = function (deviceType) {
          return $http({
                    method: 'get',
                    url: '/Hr/HrTaRawData/DeviceListData',
                    params: { pLK_TA_DEV_TYPE_ID: deviceType }
                   })
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }


        self.deleteOtApvrData = function (data, token) {            
            return $http({
                headers: { "RequestVerificationToken": token },
                method: 'post',
                url: '/Hr/HrOtApprove/DeleteBatch',
                data: { obList: data }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        self.getBillTypeListData = function () {
            return $http.get('/Hr/Admin/HrPayElement/BillTypeListData')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }

        self.getOtTypeData = function () {
            return $http.get('/Hr/HrPayBill/OtTypeListData')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }


        self.getOtApproveData = function (pOT_APRV_DATE) {
            return $http({
                method: 'get',
                url: '/Hr/HrOtApprove/OtApproveListData',
                params: { pOT_APRV_DATE: pOT_APRV_DATE }
            })
           .then(function (result) {
               return result.data;
           }).catch(function (message) {
               exception.catcher('XHR loading Failded')(message);
           });
        }

        self.getOtApproveSearchListData = function (pOT_FROM_DATE, pOT_TO_DATE, pHR_EMPLOYEE_ID) {            
            return $http({
                method: 'get',
                url: '/Hr/HrOtApprove/OtApproveSearchListData',
                params: { pOT_FROM_DATE: pOT_FROM_DATE, pOT_TO_DATE: pOT_TO_DATE, pHR_EMPLOYEE_ID: pHR_EMPLOYEE_ID }
            })
           .then(function (result) {
               return result.data;
           }).catch(function (message) {
               exception.catcher('XHR loading Failded')(message);
           });
        }


        self.getLeaveTypeData = function () {
            return $http.get('/hr/hrleavetrans/listdata')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }

        self.ProcessableLeaveType= function () {
            return $http.get('/hr/admin/HrLeaveType/ProcessableLeaveType')
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

        self.getFiscalYearData = function () {
            return $http.get('/hr/hrleavetrans/FiscalYearData')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }

        self.getLeaveDataById = function (HR_LEAVE_TRANS_ID, HR_LEAVE_APRVL_ID) {
            return $http({
                method: 'get',
                url: '/hr/hrleavetrans/LeaveDataById',
                params: { HR_LEAVE_TRANS_ID: HR_LEAVE_TRANS_ID, HR_LEAVE_APRVL_ID: HR_LEAVE_APRVL_ID }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        self.getEmployeeData = function () {
            return $http.get('/Hr/HrEmployee/employeelistdata')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }


        self.getMonthListData = function () {
            return $http.get('/Hr/HrTaProcessDataCorr/MonthListData')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }


        //This function will be deleted after checking its dependency
        //self.getEmployeeDataById = function () {
        //    return $http.get('/hr/hrleavetrans/EmployeeDataByUserId')
        //           .then(function (result) {
        //               return result.data;
        //           }).catch(function (message) {
        //               exception.catcher('XHR loading Failded')(message);
        //           });
        //}


        self.getEmployeeDataByUserId = function () {
            return $http.get('/Hr/HrSalAdvance/getEmployeeByUserId')
                   .then(function (result) {
                       return result.data[0];
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }


        self.getLeaveBalance = function (data) {

            return $http({
                method: 'get',
                url: '/hr/hrleavetrans/LeaveBalance',
                params: { HR_COMPANY_ID: data.HR_COMPANY_ID, RF_FISCAL_YEAR_ID: data.RF_FISCAL_YEAR_ID, HR_EMPLOYEE_ID: data.HR_EMPLOYEE_ID }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getAttendanceStatus = function (data) {
            return $http({
                method: 'get',
                url: '/hr/hrleavetrans/AttendanceStatus',
                params: { HR_EMPLOYEE_ID: data.HR_EMPLOYEE_ID, FROM_DATE: data.FROM_DATE, TO_DATE: data.TO_DATE }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getOffDayLeaveData = function (data) {
            console.log(data);
            return $http({
                method: 'post',
                url: '/hr/hrleavetrans/OffDayLeaveData',
                data: { ob: data }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        

        self.processLeaveReset = function (data) {
            return $http({
                method: 'post',
                url: '/hr/hrleavetrans/ProcessLeaveReset',
                params: { HR_COMPANY_ID: data.HR_COMPANY_ID, RF_FISCAL_YEAR_ID: data.RF_FISCAL_YEAR_ID, HR_LEAVE_TYPE_ID: data.HR_LEAVE_TYPE_ID }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.loadLeaveTransData = function (data) {
            return $http({
                method: 'post',
                url: '/hr/hrleavetrans/loadLeaveTransData',
                data: { ob: data}
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }


        self.leaveBalanceByType = function (data) {
            return $http({
                method: 'get',
                url: '/hr/hrleavetrans/LeaveBalanceByType',
                params: { HR_COMPANY_ID: data.HR_COMPANY_ID, RF_FISCAL_YEAR_ID: data.RF_FISCAL_YEAR_ID, HR_LEAVE_TYPE_ID: data.HR_LEAVE_TYPE_ID }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }




        self.getLookupListData = function (Id) {

            return $http({
                method: 'get',
                url: '/hr/hrleavetrans/LookupListData',
                params: { id: Id }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        self.getEmployeeDataByDept = function (data) {
            return $http({
                method: 'get',
                url: '/hr/OffDayRoaster/getEmployeeDataByDept',
                params: { HR_COMPANY_ID: data.HR_COMPANY_ID||null, HR_DEPARTMENT_ID: data.HR_DEPARTMENT_ID||null, HR_EMPLOYEE_ID: data.HR_EMPLOYEE_ID||null}
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.loadTaData = function (data) {
            return $http({
                method: 'post',
                url: '/Hr/HrTaProcessDataCorr/LoadTaData',
                data: { ob: data }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        

        self.postTaCorrData = function (data) {
            console.log(data);
            return $http({
                method: 'post',
                url: '/Hr/HrTaProcessDataCorr/UupdateAttnCorrData',
                data: { obList: data }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        self.saveBatchData = function (data) {
            return $http({
                method: 'post',
                url: '/Hr/HrTaProcessDataCorr/saveBatchData',
                data: { obList: data }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        






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


        //self.getData = function(){
        //    return $http.get('/Admin/HrYearlyCalander/HolidayCalendarData')
        //           .then(function (result) {
        //           return result.data;
        //           }).catch(function (message) {
        //                          exception.catcher('XHR loading Failded')(message);
        //                  });
        //} 
        
        //self.getDataList = function () {
        //    return $http.get('/Admin/HrYearlyCalander/HolidayCalendarListData')
        //           .then(function (result) {
        //               return result.data;
        //           }).catch(function (message) {
        //               exception.catcher('XHR loading Failded')(message);
        //           });
        //}

        self.OnPageTransition=function() {
            var deferred = $q.defer();
            deferred.resolve({ data: true });
            return deferred.promise;
        }


        self.getLeaveBalanceByEmplType = function (HR_COMPANY_ID,RF_FISCAL_YEAR_ID,HR_EMPLOYEE_ID, HR_LEAVE_TYPE_ID) {
            return $http({
                method: 'get',
                url: '/hr/hrleavetrans/LeaveBalanceByTypeEmp',
                params: { HR_COMPANY_ID: HR_COMPANY_ID, RF_FISCAL_YEAR_ID: RF_FISCAL_YEAR_ID, HR_EMPLOYEE_ID: HR_EMPLOYEE_ID, HR_LEAVE_TYPE_ID: HR_LEAVE_TYPE_ID }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }


        self.submitDataOffdayRoaster = function (data, pOption) {
            return $http({
                method: 'post',
                url: '/hr/OffDayRoaster/Submit',
                data: { obList: data, pOption: pOption }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }




        self.findNextWorkingDay = function (HR_COMPANY_ID, RF_FISCAL_YEAR_ID, TO_DATE, HR_EMPLOYEE_ID) {
            return $http({
                method: 'get',
                url: '/hr/hrleavetrans/findNextWorkingDay',
                params: { HR_COMPANY_ID: HR_COMPANY_ID, RF_FISCAL_YEAR_ID: RF_FISCAL_YEAR_ID, TO_DATE: TO_DATE, HR_EMPLOYEE_ID: HR_EMPLOYEE_ID }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.DateDiff = function (HR_COMPANY_ID, RF_FISCAL_YEAR_ID, HR_LEAVE_TYPE_ID, FROM_DATE, TO_DATE, HR_EMPLOYEE_ID) {
            console.log(HR_COMPANY_ID);
            return $http({
                method: 'get',
                url: '/hr/hrleavetrans/DateDiff',
                params: { HR_COMPANY_ID: HR_COMPANY_ID, RF_FISCAL_YEAR_ID: RF_FISCAL_YEAR_ID, HR_LEAVE_TYPE_ID: HR_LEAVE_TYPE_ID, FROM_DATE: FROM_DATE, TO_DATE: TO_DATE, HR_EMPLOYEE_ID: HR_EMPLOYEE_ID }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }

        ///////Start of Salary Advance
        self.saveData = function (data, token) {
            return $http({
                method: 'post',
                url: '/hr/HrSalAdvance/SaveAdvData',
                data: data,
                headers: { "RequestVerificationToken": token }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.updateData = function (data, token) {
            return $http({
                method: 'post',
                url: '/hr/HrSalAdvance/Update',
                data: data,
                headers: { "RequestVerificationToken": token }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.submitData = function (data, token) {
            return $http({
                method: 'post',
                url: '/hr/HrSalAdvance/submitData',
                data: data,
                headers: { "RequestVerificationToken": token }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.saveDataForSectionHead = function (data, token) {
            return $http({
                method: 'post',
                url: '/hr/HrSalAdvance/saveDataForSectionHead',
                data: { ob: data },
                headers: { "RequestVerificationToken": token }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }


        //self.submitDataForSectionHead = function (data, token) {
        //    console.log(data);
        //    return $http({
        //        method: 'post',
        //        url: '/hr/HrSalAdvance/submitDataForSectionHead',
        //        data: { ob: data },
        //        headers: { "RequestVerificationToken": token }
        //    }).then(function (res) {
        //        return res.data;
        //    }).catch(function (message) {
        //        console.log(message);
        //        exception.catcher('XHR loading Failded')(message);
        //    });
        //}


        //self.ApprovedByHr = function (data, token) {
        //    console.log(data);
        //    return $http({
        //        method: 'post',
        //        url: '/hr/HrSalAdvance/ApprovedByHr',
        //        data: { ob: data },
        //        headers: { "RequestVerificationToken": token }
        //    }).then(function (res) {
        //        return res.data;
        //    }).catch(function (message) {
        //        console.log(message);
        //        exception.catcher('XHR loading Failded')(message);
        //    });
        //}


        //self.AccSave = function (data, token) {
        //    console.log(data);
        //    return $http({
        //        method: 'post',
        //        url: '/hr/HrSalAdvance/AccSave',
        //        data: { ob: data },
        //        headers: { "RequestVerificationToken": token }
        //    }).then(function (res) {
        //        return res.data;
        //    }).catch(function (message) {
        //        console.log(message);
        //        exception.catcher('XHR loading Failded')(message);
        //    });
        //}

        self.fileClose = function (data) {
            return $http({
                method: 'post',
                url: '/hr/HrSalAdvance/fileClose',
                data: data
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }

          



        self.getBoardOfDirectors = function () {
            return $http.get('/Hr/HrSalAdvance/getBoardOfDirectors')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }


        


        


        self.getSalaryAdvDataById = function (HR_SAL_ADVANCE_ID, HR_SAL_ADV_APRVL_ID) {
            return $http({
                method: 'get',
                url: '/hr/HrSalAdvance/SalaryAdvDataById',
                params: { HR_SAL_ADVANCE_ID: HR_SAL_ADVANCE_ID, HR_SAL_ADV_APRVL_ID: HR_SAL_ADV_APRVL_ID }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.submitHrData = function (data) {
            //console.log(data);
            return $http({
                method: 'post',
                url: '/hr/HrSalAdvance/submitHrData',
                data: { SAL_ADV: data }
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }






        ///////End of Salary Advance

        return self;
    }


})();