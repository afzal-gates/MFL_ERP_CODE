(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrDisciplinActionController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'HrService', 'entityService', HrDisciplinActionController]);

    function HrDisciplinActionController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, HrService, entityService) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.timeFormat = config.appTimeFormat;
        vm.showSplash = true;
        
        vm.insert = true;
        vm.today = new Date();

        // vm.selectedDates = [new Date().setHours(0, 0, 0, 0)];
        vm.selectedDates = [];
        //vm.activeDate;

        //this.type = 'individual';
        //this.identity = angular.identity;

        vm.isEffectSalaryFine = false;
        vm.isEffectSalaryAtten = false;
        vm.isTotalFineEnable = false;

        //var currMonth=moment()
        var lastDayOfMonth = moment().daysInMonth();

        var firstDate = moment().date(1);
        var lastDate = moment().date(lastDayOfMonth);


        vm.form = {IS_DEDUCT_SALARY4FS:'Y'};
        var dt = firstDate._d;
        //vm.form.FROM_DT = $filter('date')(dt, vm.dtFormat);
        dt = lastDate._d;
        //vm.form.TO_DT = $filter('date')(dt, vm.dtFormat);                             
           
        vm.form.PROPOSED_DT = $filter('date')(vm.today,vm.dtFormat);

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.effectDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.effectDateOpened = true;
        };

        vm.proposeDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.proposeDateOpened = true;
        };

        vm.backToList = function () {
            return $state.go('DisciplinList');
        };

        vm.reset = function () {
            //vm.form = {};
            //vm.isEffectSalaryFine = false;
            //vm.isEffectSalaryAtten = false;
            //vm.form.PROPOSED_DT = $filter('date')(vm.today, vm.dtFormat);
            vm.form.EMPLOYEE_CODE = "";
            vm.form.IS_DEDUCT_SALARY4FS = 'Y';

            $('#EMPLOYEE_CODE').focus();
            vm.insert = true;
        };

        vm.addNew = function () {
            vm.form = {};
            vm.isEffectSalaryFine = false;
            vm.isEffectSalaryAtten = false;
            vm.form.PROPOSED_DT = $filter('date')(vm.today, vm.dtFormat);
            vm.insert = true;
        };

        /// ================= Start For Edit ====================
        if ($stateParams.pDisciplinActionObj != null) {
            //console.log($stateParams.pDisciplinActionObj);
            vm.formCopy = $stateParams.pDisciplinActionObj;
            vm.form = $stateParams.pDisciplinActionObj;

            var dt = moment($stateParams.pDisciplinActionObj.PROPOSED_DT)._d;
            vm.form['PROPOSED_DT'] = $stateParams.pDisciplinActionObj.PROPOSED_DT == null ? '' : $filter('date')(dt, vm.dtFormat);

            dt = moment($stateParams.pDisciplinActionObj.EFFECTIVE_DT)._d;
            vm.form['EFFECTIVE_DT'] = $stateParams.pDisciplinActionObj.EFFECTIVE_DT == null ? '' : $filter('date')(dt, vm.dtFormat);

            if (vm.form.ABSENT_DAYS != null && vm.form.ABSENT_DAYS != undefined) {
                vm.selectedDates = vm.form.ABSENT_DAYS.split(',').map(Number);
            }

            if (vm.form.IS_EFFECT_SALARY == 'Y' && vm.form.HR_DSPLN_ACT_TYPE_ID == 1) {
                vm.isEffectSalaryFine = true;
                vm.isEffectSalaryAtten = false;
                vm.isTotalFineDisable = false;
            }
            else if (vm.form.IS_EFFECT_SALARY == 'Y' && vm.form.HR_DSPLN_ACT_TYPE_ID == 2) {
                vm.isEffectSalaryFine = false;
                vm.isEffectSalaryAtten = true;
                vm.isTotalFineDisable = true;
            }
            else {
                vm.isEffectSalaryFine = false;
                vm.isEffectSalaryAtten = false;
            }

            vm.insert = false;
        };

        /// ================= End For Edit ====================

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

        $scope.onSelectItem = function (item) {
            //console.log(item);            
            vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
            vm.form.DEPARTMENT_NAME_EN = item.DEPARTMENT_NAME_EN;
            vm.form.DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
            vm.form.COMP_NAME_EN = item.COMP_NAME_EN;
            if (item.HR_COMPANY_ID != vm.form.HR_COMPANY_ID) {
                vm.form.HR_COMPANY_ID = item.HR_COMPANY_ID;
            };
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.DEPARTMENT_NAME_EN;

            vm.form.IS_DEDUCT_SALARY4FS = item.IS_DEDUCT_SALARY4FS;
        }

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.HR_EMPLOYEE_ID = null;
                vm.form.EMP_FULL_NAME_EN = null;
                vm.form.DEPARTMENT_NAME_EN = null;
                vm.form.DESIGNATION_NAME_EN = null;
                vm.form.COMP_NAME_EN = null;
                //vm.form.HR_COMPANY_ID = null;
                //vm.form.EMP_FULL_NAME_EN = null;
            }

        });


        $scope.onSelectProposeBy = function (item) {
            //console.log(item);
            vm.form.ACTION_BY_ID = item.HR_EMPLOYEE_ID;
            vm.form.PROPOSED_EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
            vm.form.PROPOSED_DEPARTMENT_NAME_EN = item.DEPARTMENT_NAME_EN;
            vm.form.PROPOSED_DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
            //vm.form.COMP_NAME_EN = item.COMP_NAME_EN;
            vm.form.PROPOSED_EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.DEPARTMENT_NAME_EN;
        }

        $scope.$watch('vm.form.PROPOSED_EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.ACTION_BY_ID = null;
                vm.form.PROPOSED_EMP_FULL_NAME_EN = null;
                vm.form.PROPOSED_DEPARTMENT_NAME_EN = null;
                vm.form.PROPOSED_DESIGNATION_NAME_EN = null;
                vm.form.COMP_NAME_EN = null;                
            }

        });
        

        $scope.$watch('vm.form.HR_DSPLN_ACT_TYPE_ID', function (newVal, oldVal) {
            
            if (newVal != oldVal) {
                //vm.form.RF_FISCAL_YEAR_ID = null;
                vm.form.IS_B_OR_G = null;
                vm.form.IS_DAY_OR_AMT = null;
                vm.form.NO_DAYS = null;
                vm.form.DEDU_AMT = null;
            }
        });

        vm.actionTypeListData = {
            optionLabel: "-- Select Action Type --",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrDisciplinAction/ActionTypeListData"  //+ "&pType=" + showType
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
            select: function(e){
                var dataItem = this.dataItem(e.item);
                //alert(dataItem.IS_EFFECT_SALARY);
                if (dataItem.IS_EFFECT_SALARY == 'Y' && dataItem.HR_DSPLN_ACT_TYPE_ID == 1) {                    
                    vm.isEffectSalaryFine = true;
                    vm.isEffectSalaryAtten = false;
                    vm.isTotalFineDisable = false;
                }
                else if (dataItem.IS_EFFECT_SALARY == 'Y' && dataItem.HR_DSPLN_ACT_TYPE_ID == 2) {                    
                    vm.isEffectSalaryFine = false;
                    vm.isEffectSalaryAtten = true;
                    vm.isTotalFineDisable = true;
                }
                else {                    
                    vm.isEffectSalaryFine = false;
                    vm.isEffectSalaryAtten = false;
                }
            },
            dataTextField: "DA_TYPE_NAME_EN",
            dataValueField: "HR_DSPLN_ACT_TYPE_ID"
        };

        vm.reasonTypeListData = {
            optionLabel: "-- Select Action Reason --",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrDisciplinAction/LookupListData",  //+ "&pType=" + showType
                            params: {
                                pLookupTableId: 25,
                            }
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
            dataTextField: "LK_DATA_NAME_EN",
            dataValueField: "LOOKUP_DATA_ID"
        };
        
        vm.deductionOnListData = {
            optionLabel: "-- Select --",
            filter: "startswith",
            autoBind: true,
            dataSource: [{ IS_B_OR_G: 'B', BASE_ON_NAME: 'Basic' }, { IS_B_OR_G: 'G', BASE_ON_NAME: 'Gross' }],
            dataTextField: "BASE_ON_NAME",
            dataValueField: "IS_B_OR_G",            
        };

        vm.deductionBaseListData = {
            optionLabel: "-- Select --",
            filter: "startswith",
            autoBind: true,
            dataSource: [{ IS_DAY_OR_AMT: 'D', DEDUCTION_ON_NAME: 'Day' }, { IS_DAY_OR_AMT: 'A', DEDUCTION_ON_NAME: 'Amount' }],
            dataTextField: "DEDUCTION_ON_NAME",
            dataValueField: "IS_DAY_OR_AMT",
            select: function (e) {
                var dataItem = this.dataItem(e.item);
                if (dataItem.IS_DAY_OR_AMT == 'A') {
                    vm.isTotalFineDisable = false;                    
                }
                else {
                    vm.isTotalFineDisable = true;
                    vm.form.DEDU_AMT = null;
                }
            }
        };

        vm.openPeriodListData = {
            optionLabel: "-- Select --",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
                            params: {
                                pHR_COMPANY_ID: vm.form.HR_COMPANY_ID,
                                pHR_PERIOD_TYPE_ID:3
                            }
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
            dataTextField: "MONTH_YEAR_NAME",
            dataValueField: "RF_FISCAL_YEAR_ID",
            //index:0,
            //template: ' #if(data.FY_NAME_EN!=undefined){#<span> #= data.MONTH_NAME_EN + "-" + data.FY_NAME_EN #</span>#}#' // '<span>#= data.MONTH_NAME_EN + "-" + data.FY_NAME_EN #</span>'
            select: function (e) {
                var dataItem = this.dataItem(e.item);
                var dt = moment(dataItem.END_DATE)._d;
                vm.form.EFFECTIVE_DT = $filter('date')(dt, vm.dtFormat);
                vm.form.RF_CAL_MONTH_ID = dataItem.RF_CAL_MONTH_ID;
            }
        };


        vm.amountCalculate = function () {

            if (vm.form.HR_DSPLN_ACT_TYPE_ID == 2) {
                vm.form.NO_DAYS = vm.selectedDates.length;
            }

            $http({
                method: 'post',
                url: "/Hr/HrDisciplinAction/DeductAmountData",  //+ "&pType=" + showType
                data: vm.form
            }).success(function (data, status, headers, config) {
                //e.success(data);

                //console.log(data);
                vm.form.DEDU_AMT = data.deductAmount;

            }).error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
            });             
        };


        vm.submit = function (isValid, form, insert) {
            
            form.NO_DAYS = vm.selectedDates.length;
            vm.form.ABSENT_DAYS = vm.selectedDates;
            
            if (vm.form.ABSENT_DAYS != null && vm.form.ABSENT_DAYS != undefined) {
                vm.form['ABSENT_DAYS'] = form.ABSENT_DAYS.join(',');
            }

            //if (!isValid) return;
            

            if (insert) {

                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrDisciplinAction/Save',
                    method: 'post',
                    data: form
                }).success(function (data, status, headers, config1) {
                    vm.errors = [];
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        if (data.vMsg.substr(0, 9) == 'MULTI-001') {
                            vm.reset();
                        };
                        config.appToastMsg(data.vMsg);

                    }
                }).error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });
            }
            else {
                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrDisciplinAction/Update',
                    method: 'post',
                    data: form
                }).success(function (data, status, headers, config1) {
                    vm.errors = [];
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        if (data.vMsg.substr(0, 9) == 'MULTI-001') {
                            vm.reset();
                        };
                        config.appToastMsg(data.vMsg);

                    }
                }).error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });
            }

        };

        

        function activate(){
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

       
            
        
    }

    

})();