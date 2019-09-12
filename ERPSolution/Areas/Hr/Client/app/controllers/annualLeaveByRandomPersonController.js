//================== Start AnnualLeaveByRandomPersonController =========================
(function () {
    'use strict';

    angular.module('multitex.hr').controller('AnnualLeaveByRandomPersonController', ['logger', 'config', 'HrService', '$q', '$scope', '$window', '$http', '$filter', '$state', AnnualLeaveByRandomPersonController]);
    function AnnualLeaveByRandomPersonController(logger, config, HrService, $q, $scope, $window, $http, $filter, $state) {

        var vm = this;
        
        vm.Title = $state.current.Title;
        vm.showSplash = true;
        vm.dtFormat = config.appDateFormat;
        vm.scheduleIsChange = false;
        vm.today = new Date();

        $("#EMPLOYEE_CODE").focus();

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });
        

        vm.form = { EFFECTIVE_FROM: moment(vm.today).format("DD-MMM-YYYY") };

        activate();

        function activate() {
            var promise = [getFiscalYearList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.EFFECTIVE_FROM_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.EFFECTIVE_FROM_LNopened = true;
        };

        function getFiscalYearList() {
            
            vm.fiscalYearOptions = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FY_NAME_EN",
                dataValueField: "RF_FISCAL_YEAR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);                    
                }
            };

            return vm.fiscalYearDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/hr/GetFiscalYearList?pIS_CLOSED=N').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        

        $scope.emoloyeeAuto = function (val) {           
            return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val
                }
            }).then(function (response) {
                return response.data;
            });
        };

        $scope.onSelectItem = function (item) {

            console.log(item);            
            vm.myForm.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
            vm.myForm.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
            vm.myForm.DEPARTMENT_NAME_EN = item.DEPARTMENT_NAME_EN;
            vm.myForm.DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
            vm.myForm.COMP_NAME_EN = item.COMP_NAME_EN;
            vm.myForm.HR_COMPANY_ID = item.HR_COMPANY_ID;
            vm.myForm.HR_OFFICE_ID = item.HR_OFFICE_ID;
            
            vm.myForm.EMP_PROFILE = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.DEPARTMENT_NAME_EN;                        
        }

        $scope.$watch('vm.myForm.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {                
                vm.myForm = {};
            }
        });

        vm.addNewEmp = function () {
            //vm.assignEmpListDataSource.insert(0, vm.form);
            //vm.form = {};
            if (vm.myForm.HR_EMPLOYEE_ID != null) {
                vm.assignEmpListDataSource.insert(0, vm.myForm);
                vm.myForm = {};
            }
        }

        //$("#EMPLOYEE_CODE").keypress(function (e) {
        //    alert(e.which);
        //    if (e.which == 13) {
        //        alert('x');
        //        vm.assignEmpListDataSource.insert(0, vm.myForm);
        //        vm.form = {};
        //    };
        //});

        vm.removeRow = function (dataItem) {
            vm.assignEmpListDataSource.remove(dataItem);
        }

        vm.assignEmpListOptions = {
            editable: false,
            selectable: "cell",
            navigatable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains",
                        startswith: "Starts With",
                        eq: "Is Equal To"
                    }
                }
            },
            //dataBound: function (e) {
            //    collapseAllGroups(this);
            //},
            columns: [
                { field: "EMPLOYEE_CODE", title: "Code", width: "15%" },
                { field: "EMP_FULL_NAME_EN", title: "Name", width: "25%" },
                { field: "DEPARTMENT_NAME_EN", title: "Department", width: "20%" },
                { field: "DESIGNATION_NAME_EN", title: "Designation", width: "20%" },
                { field: "COMP_NAME_EN", title: "Company", width: "15%" },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "5%"
                }
            ]
        };

        vm.assignEmpListDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    e.success([]);                   
                }
            }
        });


        vm.errors = undefined;
        vm.procLeave = function () {
            var submitData = angular.copy(vm.form);
        
            var empListGridData = vm.assignEmpListDataSource.data();
           
            console.log(empListGridData);            
           
            //$scope.$parent.errors = [];
            //angular.forEach(clcfRateGridData, function (val, key) {
            //    if (val['PROD_RATE'] == null || val['PROD_RATE'] == '') {
            //        $scope.$parent.errors.push({ key: "", error: 'Assign rate should not empty' });
            //    }
            //});

            //if ($scope.$parent.errors != undefined && $scope.$parent.errors.length > 0) {
            //    return;
            //}
            //else {
            //    $scope.$parent.errors = undefined;
            //}

            submitData.EMP_XML = HrService.xmlStringShort(empListGridData.map(function (ob) {
                return { HR_EMPLOYEE_ID: ob.HR_EMPLOYEE_ID, EMPLOYEE_CODE: ob.EMPLOYEE_CODE, HR_COMPANY_ID: ob.HR_COMPANY_ID, HR_OFFICE_ID: ob.HR_OFFICE_ID };
            }));

            console.log(submitData);
            //return;

            return HrService.saveDataByUrl(submitData, '/api/hr/Leave/AnnualLeaveByRandomPersonBatchSave').then(function (res) {
                console.log(res);

                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {                        
                        vm.assignEmpListDataSource.read();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });


        };



    }



})();
//================== End AnnualLeaveByRandomPersonController =========================