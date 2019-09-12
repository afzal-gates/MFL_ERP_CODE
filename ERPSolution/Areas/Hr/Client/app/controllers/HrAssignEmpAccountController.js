(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrAssignEmpAccountController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'HrService', 'commonDataService', HrAssignEmpAccountController]);

    function HrAssignEmpAccountController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, HrService, commonDataService) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.timeFormat = config.appTimeFormat;
        vm.showSplash = true;
        
        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
            function (e) {
                e.preventDefault();
            });

        vm.insert = true;
        vm.today = new Date();
                
        vm.form = { IS_ACTIVE: 'Y', IS_EMP_ACC: 'N', IS_INT_ACC: 'N', IS_AP_ACC: 'N', IS_AR_ACC: 'N' };
        vm.formCopy = {};
                
        function reset() {
            vm.form = {
                IS_ACTIVE: 'Y', IS_EMP_ACC: 'N', IS_INT_ACC: 'N', IS_AP_ACC: 'N', IS_AR_ACC: 'N', LK_ACC_TYPE_ID: vm.formCopy.LK_ACC_TYPE_ID,
                RF_BANK_ID: vm.formCopy.RF_BANK_ID, RF_BANK_BRANCH_ID: vm.formCopy.RF_BANK_BRANCH_ID, RF_CURRENCY_ID: vm.formCopy.RF_CURRENCY_ID
            };
            $('#BK_ACC_NO').focus();
            vm.insert = true;
        };

        vm.TranCancel = function () {
            reset();
        };

        $scope.$watchGroup(['vm.form.OT_APRV_DATE'], function (newVal, oldVal) {
            //alert(newVal); 
            if (newVal[0] != null) {
                $("#otApproveGrid").empty();
                vm.isNext = false;
            }
        });
              
        // Start For Employee Search
        $scope.emoloyeeAuto = function (val) {
            //alert('a');
            return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val
                }
            }).then(function (response) {
                return response.data;
            });
        };

        $scope.onSelectEmp = function (item) {
            //console.log(item);            
            vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
            vm.form.DEPARTMENT_NAME_EN = item.DEPARTMENT_NAME_EN;
            vm.form.DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
            //vm.form.COMP_NAME_EN = item.COMP_NAME_EN;
            //vm.form.HR_COMPANY_ID = item.HR_COMPANY_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.DEPARTMENT_NAME_EN;
            vm.form.BK_ACC_TITLE = item.EMP_FULL_NAME_EN;
        }

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.HR_EMPLOYEE_ID = null;
                vm.form.EMP_FULL_NAME_EN = null;
                vm.form.DEPARTMENT_NAME_EN = null;
                vm.form.DESIGNATION_NAME_EN = null;
                //vm.form.COMP_NAME_EN = null;
                //vm.form.HR_COMPANY_ID = null;
                //vm.form.EMP_FULL_NAME_EN = null;
            }

        });
        // End For Employee Search


        // Start For Bank Account Search
        $scope.bankAccountAuto = function (val) {
            return commonDataService.getBankAccountAutoList('Y', val, vm.form.RF_BANK_ID).then(function (res) {
                return res;
            });
        };

        $scope.onSelectBnkAc = function (item) {
            //console.log(item);                        
            vm.form.ACC_BK_ACCOUNT_ID = item.ACC_BK_ACCOUNT_ID;
            vm.form.IS_EMP_ACC = item.IS_EMP_ACC;
            vm.form.BK_ACC_TITLE = item.BK_ACC_TITLE;
            vm.form.LK_ACC_TYPE_ID = item.LK_ACC_TYPE_ID;
            vm.form.RF_CURRENCY_ID = item.RF_CURRENCY_ID;
            vm.form.IS_INT_ACC = item.IS_INT_ACC;
            vm.form.IS_AP_ACC = item.IS_AP_ACC;
            vm.form.IS_AR_ACC = item.IS_AR_ACC;
            vm.form.IS_ACTIVE = item.IS_ACTIVE;

            if (item.IS_EMP_ACC == 'Y') {                
                vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
                vm.form.EMPLOYEE_CODE = item.EMPLOYEE_CODE;
                vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
                vm.form.DEPARTMENT_NAME_EN = item.DEPARTMENT_NAME_EN;
                vm.form.DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
                
                vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.DEPARTMENT_NAME_EN;                
            }
        }

        $scope.$watch('vm.form.BK_ACC_NO', function (newVal, oldVal) {            
            if (!newVal || newVal == '') {
                vm.form.ACC_BK_ACCOUNT_ID = null;
                vm.form.IS_EMP_ACC = 'N';
                vm.form.BK_ACC_TITLE = null;
            }
        });
        // End For Bank Account Search
        
        function getBankBranchListData() {
            return vm.bankListData = {
                optionLabel: "-- Select Bank --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            commonDataService.getBankListData().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "BANK_NAME_EN",
                dataValueField: "RF_BANK_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vRF_BANK_ID = dataItem.RF_BANK_ID;

                    $('#RF_BANK_BRANCH_ID').kendoDropDownList({
                        optionLabel: "-- Select Branch --",
                        filter: "startswith",
                        autoBind: true,
                        dataSource: {
                            transport: {
                                read: function (e) {                                    
                                    commonDataService.getBranchListData(vRF_BANK_ID).then(function (res) {                                        
                                        e.success(res);
                                    });
                                }
                            }
                        },
                        dataTextField: "BANK_BRANCH_NAME_EN",
                        dataValueField: "RF_BANK_BRANCH_ID"
                    });
                }
            };
        };

        function getLookupList() {
            return vm.bankAcTypeList = {
                optionLabel: "-- Select A/C Type --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            commonDataService.getLookupList(22).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };
        
        function getCurrencyList() {
            return vm.currencyList = {
                optionLabel: "-- Select Currency --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            commonDataService.getCurrencyList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "CURR_NAME_EN",
                dataValueField: "RF_CURRENCY_ID"
            };
        };


        vm.submitData = function (data, token) {            
            data.CREATED_BY = vm.CREATED_BY;
            vm.formCopy = angular.copy(data);

            return HrService.batchSaveBkAccAssigWithEmp(data, token).then(function (res) {
                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    //vm.form.MEMO_NO = res.data.PSAVE_MEMO_NO;

                    //console.log(res.data.PMSG);
                    //console.log(res.data.PMSG.substr(0, 9));

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        reset();
                        vm.isSaved = true;
                    };

                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };


        vm.otGrid = function () {

            var dataSource = new kendo.data.DataSource({
                batch: true,
                transport: {
                    read: function (e) {
                        HrService.getOtApproveData(vm.form.OT_APRV_DATE).then(function (res) {
                            e.success(res);
                        });
                    },

                    destroy: function (e) {
                        //console.log(e);
                        HrService.deleteOtApvrData(e.data.models, vm.antiForgeryToken).then(function (res) {
                            //config.appToastMsg(res.vMsg);
                            e.success();

                        });
                    },
                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return { models: kendo.stringify(options.models) };
                        }
                    }                    
                },                
                pageSize: 10,
                schema: {
                    model: {
                        id: "HR_OT_APPROVE_ID",
                        fields: {
                            EMPLOYEE_CODE: { type: "string", editable: false },
                            EMP_FULL_NAME_EN: { type: "string", editable: false },                            
                            DESIGNATION_NAME_EN: { type: "string", editable: false },
                            OT_APRV_DATE: { type: "date", editable: false },
                            OT_DATE: { type: "date", editable: false },
                            IN_TIME: { type: "string", editable: false },
                            OUT_TIME: { type: "string", editable: false },
                            OT_HR: { type: "string", editable: false }                            
                        }
                    }
                }
            });


            //HrService.getOtApproveData(vm.form.OT_APRV_DATE).then(function (res) {
                
            //    if (res.length > 0) {
            //        vm.form.OT_APRV_REASON_EN = res[0].OT_APRV_REASON_EN;
            //        vm.form.OT_APRV_REASON_BN = res[0].OT_APRV_REASON_BN;
                    $("#otApproveGrid").kendoGrid({                        
                        dataSource: dataSource,
                        navigatable: true,
                        pageable: {
                            refresh: true,
                            pageSizes: true
                        },
                        //height: 550,
                        //toolbar: ["save", "cancel"],
                        columns: [
                            { field: "EMPLOYEE_CODE", title: "Code", type: "string", width: "120px" },
                            { field: "EMP_FULL_NAME_EN", title: "Name", type: "string", width: "200px" },
                            { field: "DESIGNATION_NAME_EN", title: "Designation", type: "string", width: "200px" },
                            { field: "OT_DATE", title: "Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "100px" },
                            { field: "IN_TIME", title: "In", type: "string", width: "50px", filterable: false },
                            { field: "OUT_TIME", title: "Out", type: "string", width: "50px", filterable: false },
                            { field: "OT_HR", title: "OT Hour/Day", type: "string", width: "100px", filterable: false },
                            { command: "destroy", text: "Remove", title: "&nbsp;", width: "80px" }
                            //{ command: [{ text: "Remove", click:vm.test1() }], title: 'Actions' }
                            //{
                            //    title: "Action",
                            //    template: function () {
                            //        return "<a class='btn btn-xs blue' ng-click='vm.test1()' onclick='vm.removeRecord(dataItem)'  title='Remove'><i class='fa fa-remove'></i> Remove</a>";
                            //        //return "<a ng-click='vm.editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>";
                            //    },
                            //    width: "70px"
                            //}

                        ],
                        editable: {
                            confirmation: false, //"Do you want to remove this record?",
                            mode: "inline"
                        }
                    });
            //    }
            //});
        }

        
        function activate(){
            var promise = [getBankBranchListData(), getLookupList(), getCurrencyList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

       
            
        
    }

    

})();

