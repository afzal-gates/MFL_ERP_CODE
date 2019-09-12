(function () {
    'use strict';
    angular.module('multitex.hr').controller('HrTaProcessDataCorrController', ['$q', 'config', 'HrService', '$http', '$stateParams', '$state', '$scope', '$filter','logger', HrTaProcessDataCorrController]);
    function HrTaProcessDataCorrController($q, config, HrService, $http, $stateParams, $state, $scope, $filter, logger) {

        var vm = this;
        vm.form = {};
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getCompanyData(), getLookupListData(), getFoorData(), shiftTeamList(), lineList(), getMonthListData(), getFiscalYearData(), parentDepartmentList(), getSubdepartmentListData(), getDayTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.form['FROM_DATE'] = $filter('date')(moment()._d, config.appDateFormat);
        vm.form['TO_DATE'] = $filter('date')(moment()._d, config.appDateFormat);
        $scope.$watch('vm.form.MONTH_VALUE', function (newVal, oldVal) {
            vm.maxDate = $filter('date')(moment({ year: moment().year(), month: (newVal - 1), day: 1 }).endOf("month")._d, config.appDateFormat);
            vm.minDate = $filter('date')(moment({ year: moment().year(), month: (newVal - 1), day: 1 })._d, config.appDateFormat);
        });

        $scope.$watch('vm.form.employeeSelect', function (newVal, oldVal) {
            if (newVal == 'Batch') {
                vm.form['CORRECTED_STATUS'] = 'P,L';
            } else {
                vm.form['CORRECTED_STATUS'] = 'P,L,A,W,H,F';
            }
        });

        vm.resetME = function () {
            vm.ME = [];
            vm.form['HR_EMPLOYEE_IDS'] = null;
        }

        vm.officeListData = {
            optionLabel: "-- Select Office --",
         
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrOffice/OfficeListData"
                        }).
                        success(function (data, status, headers, config) {
                            e.success(data)
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                    }
                }
            },
            dataTextField: "OFFICE_NAME_EN",
            dataValueField: "HR_OFFICE_ID"
        };

        function parentDepartmentList() {
            vm.parentDepartmentListData = {
                filter: "contains",
                autoBind: true,
                optionLabel: "-- Select Department --",
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/ParentDeptListData"  //+ "&pType=" + showType
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }

                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vParentID = dataItem.HR_DEPARTMENT_ID; //this.value();                    

                    if (vParentID != null) {
                        HrService.getSubDepartmentData(vParentID).then(function (res) {
                            $("#HR_DEPARTMENT_ID").kendoDropDownList({
                                optionLabel: "-- Select Section --",
                                dataTextField: "DEPARTMENT_NAME_EN",
                                dataValueField: "HR_DEPARTMENT_ID",
                                dataSource: res,
                                filter: "startswith"
                            });

                        });
                    }

                }
            };
        };


        function getSubdepartmentListData() {
            return vm.subdepartmentListData = {
                optionLabel: "-- Select Section --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getSubDepartmentData().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID"
            };
        };

        vm.form['MONTH_VALUE'] = moment().month()+1;

        $scope.format = config.appDateFormat;

        $scope.FROM_DATEopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.FROM_DATEopened = true;
        };

        $scope.TO_DATEopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TO_DATEopened = true;
        };




        function getFiscalYearData() {
            return HrService.getFiscalYearData().then(function (res) {
                console.log(res);
                vm.FiscalYearData = res;
                return vm.form['RF_FISCAL_YEAR_ID'] = res[0]['RF_FISCAL_YEAR_ID'];
            });
        }


        function getCompanyData() {
            return HrService.getCompanyData().then(function (res) {
                vm.form['HR_COMPANY_ID'] = res[0]['HR_COMPANY_ID'];
                vm.companyData = res;
                console.log(res);
            });
        }

        function getLookupListData() {
            return HrService.getLookupListData(20).then(function (res) {
                return vm.corrReason = res;
            });
        }

        function getDayTypeList() {
            return $http({
                method: 'get',
                url: '/hr/HrYearlyCalander/DayTypeListData'
            }).then(function (res) {
                vm.dayTypeList =res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }




        function getFoorData() {
            return HrService.getLookupListData(18).then(function (res) {
                return vm.floorDatas = res;
            });
        }

        function shiftTeamList() {
            return $http({
                url: '/Hr/HrEmployee/ShiftTeamListData',
                method: 'get',
                params: { pHR_SHIFT_PLAN_ID: null }
            })
                .then(function (result) {
                    vm.shiftTeamListData = result.data;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };


        function lineList() {
            vm.lineListData = [{ LINE_NO: 0 }];

            for (var i = 0; i < 50; i++) {
                vm.lineListData[i] = { LINE_NO: i + 1 };
            }
        };

        function getMonthListData() {
            HrService.getMonthListData().then(function (res) {
                vm.monthListData = res;
                return vm.monthListData;
            });
        }

       $scope.emoloyeeAutoMulti = function (val) {
           return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
               params: {
                   pEMPLOYEE_CODE: val
               }
           }).then(function (response) {
               return response.data;
           });
       };


       //$scope.onSelectItem = function (item) {
       //    vm.form['HR_EMPLOYEE_ID'] = item.HR_EMPLOYEE_ID;
        //}


       $scope.$watch('vm.form.IS_CORRECTION_REASON_OTHER', function (newVal, oldVal) {
           if (newVal=='Y') {
               vm.form['LK_PN_COR_REASON_ID'] = null;
           }
       });

       vm.submitData = function () {
           var Ids = [];
           angular.forEach(vm.ME, function (val,key) {
               Ids.push(val.HR_EMPLOYEE_ID);
           });

           vm.form['HR_EMPLOYEE_IDS'] = Ids;
           if (vm.form.employeeSelect == 'Random' && angular.isDefined(vm.form.HR_EMPLOYEE_IDS) && angular.isArray(vm.form.HR_EMPLOYEE_IDS)) {
               vm.form['HR_EMPLOYEE_IDS'] = vm.form.HR_EMPLOYEE_IDS.join(',');
               vm.form['HR_EMPLOYEE_ID']=null;
           }

           if (vm.form.employeeSelect == 'Batch') {
               vm.form['HR_EMPLOYEE_ID'] = null;
               vm.form['HR_EMPLOYEE_IDS'] = null;
           }

           if (vm.form.employeeSelect == 'Individual' && vm.SE) {
               delete vm.form['HR_EMPLOYEE_IDS'];
               vm.form['HR_EMPLOYEE_ID'] = vm.SE.HR_EMPLOYEE_ID;
           }

           if (moment(vm.form.OT_HR, "HH:mm").isValid()) {
               vm.form['OT_HR'] = moment(vm.form.OT_HR, "HH:mm");
           } else {
               vm.form['OT_HR'] = null;
           }

           var IN_TIME_WT = {};
           var OUT_TIME_WT = {};

           if (vm.form.PUNCH_TYPE == 105) {
                   IN_TIME_WT = { type: "date" };
                   OUT_TIME_WT = { type: "date",editable:false };
           } else if (vm.form.PUNCH_TYPE == 106) {
                   IN_TIME_WT = { type: "date", editable: false };
                   OUT_TIME_WT = { type: "date"};
           } else if (vm.form.PUNCH_TYPE == 107) {
                  IN_TIME_WT = { type: "date" };
                  OUT_TIME_WT = { type: "date" };
           } else if (vm.form.PUNCH_TYPE == 437 || vm.form.PUNCH_TYPE == 629) {
               IN_TIME_WT = { type: "date", editable: false };
               OUT_TIME_WT = { type: "date", editable: false };
           }

           var dataSource = new kendo.data.DataSource({
               batch: true,
                transport: {
                       read: function (e) {
                           HrService.loadTaData(vm.form).then(function (res) {
                               vm.form['OT_HR'] = null;

                               e.success(res.map(function (o) {
                                   o['HR_DAY_TYPE'] = {
                                       HR_DAY_TYPE_ID: o.HR_DAY_TYPE_ID || '',
                                       DAY_TYPE_NAME_EN: o.HR_DAY_TYPE_ID ? _.find(vm.dayTypeList, function (ob) {
                                           return ob.HR_DAY_TYPE_ID == o.HR_DAY_TYPE_ID
                                       }).DAY_TYPE_NAME_EN : ''
                                   }

                                   return o;

                               }));
                              
                           });
                       },

                       update: function (e) {
                           var data = [];
                           angular.forEach(e.data.models, function (val, key) {
                               if (moment(val.IN_TIME_WT).get('hour') == 0 && moment(val.IN_TIME_WT).get('minute') == 0) {
                                   val['IN_TIME_WT'] = null;
                               }
                               if (moment(val.OUT_TIME_WT).get('hour') == 0 && moment(val.OUT_TIME_WT).get('minute') == 0) {
                                   val['OUT_TIME_WT'] = null;
                               }
                               val['HR_DAY_TYPE_ID'] = val.HR_DAY_TYPE.HR_DAY_TYPE_ID;
                               data.push(val);
                           })
                           if (data.length > 0) {
                               HrService.postTaCorrData(data).then(function (res) {
                                   console.log(res);
                                   config.appToastMsg(res.vMsg);
                                   e.success();
                               });
                           }

                          
                       },
                       parameterMap: function (options, operation) {
                           if (operation !== "read" && options.models) {
                               return { models: kendo.stringify(options.models) };
                           }
                       }
                   },
                   pageSize: 2000,
                   schema: {
                       model: {
                           id:"HR_TA_PROCESS_DATA_ID",
                           fields: {
                               EMPLOYEE_CODE: { type: "string", editable: false },
                               EMP_FULL_NAME_EN: { type: "string", editable: false},
                               DEPARTMENT_NAME_EN: { type: "string", editable: false },
                               DESIGNATION_NAME_EN: { type: "string", editable: false },
                               ATTEN_DATE: { type: "date", editable: false },
                               IN_TIME_WT: IN_TIME_WT,
                               OUT_TIME_WT: OUT_TIME_WT,
                               OT_HR_STRING: { type: "string", editable: false },
                               TA_FLAG: { type: "string", editable: false },
                               LK_PN_CORR_STATUS: { type: "string", editable: false },
                               HR_DAY_TYPE: { defaultValue: { HR_DAY_TYPE_ID: "", DAY_TYPE_NAME_EN: "N/A" }, editable: true },
                               
                           }
                       }
                   }
           });
              
           $state.go('HrTaProcessDataCorr.Dtl', { data: dataSource, formData: vm.form });
       
       }


    }

})();
