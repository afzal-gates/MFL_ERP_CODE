(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrSalaryTranController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', 'HrService', HrSalaryTranController]);

    function HrSalaryTranController(logger, config, $q, $scope, $http, exception, $filter, $state, HrService) {

        var vm = this;
        
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.timeFormat = config.appTimeFormat;
        vm.showSplash = true;
        
        vm.isNext = false;

        activate();

        vm.insert = true;
        vm.today = new Date();        

        //var currMonth=moment()
        var lastDayOfMonth = moment().daysInMonth();

        var firstDate = moment().date(1);
        var lastDate = moment().date(lastDayOfMonth);

        vm.isEmpPayShow = false;
        vm.obPayElement = {};
        vm.form = {IS_SAL_OFF: 'N', IS_ADV_DED_OFF: 'N'};
        vm.salaryPayData = [];
                
        //var dt = firstDate._d;
        //vm.form.FROM_DATE = $filter('date')(vm.today, vm.dtFormat);
        //dt = lastDate._d;
        //vm.form.TO_DATE = $filter('date')(vm.today, vm.dtFormat);                       
                        
        //vm.dateOptions = {
        //    formatYear: 'yy',
        //    startingDay: 6
        //};

        //vm.fromDateOpen = function ($event) {
        //    $event.preventDefault();
        //    $event.stopPropagation();

        //    vm.fromDateOpened = true;
        //};

        //vm.toDateOpen = function ($event) {
        //    $event.preventDefault();
        //    $event.stopPropagation();

        //    vm.toDateOpened = true;
        //};

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
            vm.isEmpPayShow = false;
            vm.isNext = false;
            vm.form['HR_EMPLOYEE_ID'] = item.HR_EMPLOYEE_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.DEPARTMENT_NAME_EN;
        }

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.errors = undefined;

                vm.isNext = false;
                vm.isAddAnother = false;
                vm.form.HR_EMPLOYEE_ID = null;
                vm.form.EMP_FULL_NAME_EN = null;
                vm.form.HR_SAL_TRAN_H_ID = null;
                vm.form.ABS_DAYS = null;
                vm.form.LEAVE_DAYS = null;
                vm.form.HOLI_DAYS = null;
                vm.form.PERIOD_DAYS = null;                
            }

        });

        vm.next = function () {
            //vm.form = { IS_SAL_OFF: 'N', IS_ADV_DED_OFF: 'N' };
            if (vm.form.HR_EMPLOYEE_ID != null) {
                vm.isEmpPayShow = false;
                vm.isNext = true;

                vm.salaryPayData = [];
                $("#salaryTranGrid").empty();

                HrService.getSalaryTranHdrData(vm.form.ACC_PAY_PERIOD_ID, vm.form.HR_EMPLOYEE_ID).then(function (res) {
                    vm.form.HR_SAL_TRAN_H_ID = res.HR_SAL_TRAN_H_ID;
                    vm.form.ABS_DAYS = res.ABS_DAYS;
                    vm.form.LEAVE_DAYS = res.LEAVE_DAYS;
                    vm.form.HOLI_DAYS = res.HOLI_DAYS;
                    vm.form.PERIOD_DAYS = res.PERIOD_DAYS;
                    vm.form.PRE_DAYS = res.PRE_DAYS;
                    vm.form.PAYABLE_DAYS = res.PAYABLE_DAYS;

                    if (res.HR_SAL_TRAN_H_ID != null && res.HR_SAL_TRAN_H_ID > 0) {
                        vm.form.IS_SAL_OFF = res.IS_SAL_OFF;
                        vm.form.IS_ADV_DED_OFF = res.IS_ADV_DED_OFF;
                        //e.success(vm.desigPayElementData);
                    }
                    else {
                        HrService.getAttenSummeryData(vm.form.ACC_PAY_PERIOD_ID, vm.form.HR_EMPLOYEE_ID).then(function (res) {                            
                            vm.form.ABS_DAYS = res.ABS_DAYS;
                            vm.form.LEAVE_DAYS = res.LEAVE_DAYS;
                            vm.form.HOLI_DAYS = res.HOLI_DAYS;
                            vm.form.PERIOD_DAYS = res.PERIOD_DAYS;
                            vm.form.PRE_DAYS = res.PRE_DAYS;
                            vm.form.PAYABLE_DAYS = res.PAYABLE_DAYS;

                            //vm.form.IS_SAL_OFF = res.IS_SAL_OFF;
                            //vm.form.IS_ADV_DED_OFF = res.IS_ADV_DED_OFF;
                            vm.form.MISSED_OUT_PUNCH_DAYS = res.MISSED_OUT_PUNCH_DAYS;
                            vm.form.LATE_DAYS = res.LATE_DAYS;
                            vm.form.BASIC_PER_DAY = res.BASIC_PER_DAY;
                        });
                    };

                    vm.salaryTranGrid();
                });

                
            }            
        };

        vm.isAddAnother = false;
        vm.addAnotherEmpPayElement = function () {
            vm.isAddAnother = true;
        };

        vm.isAddEmpPayElement = false;
        vm.addEmpPayElement = function () {
            vm.isAddEmpPayElement = true;

            vm.salaryPayData = $("#salaryTranGrid").data("kendoGrid").dataSource.data();
            
            var index = HrService.getIndexByKeyValue(vm.salaryPayData, 'HR_PAY_ELEMENT_ID', vm.form.HR_PAY_ELEMENT_ID);

            if (index == null) {
                vm.salaryTranGrid();
            }
            else {
                alert('Sorry! The selected pay element already exists.');
            }
        };

        function getPayElementListData() {
            vm.payElementData = {
                optionLabel: "-- Select Pay Element --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getPayElementListData('Y', 'N', null).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "PAY_ELEMENT_NAME_EN",
                dataValueField: "HR_PAY_ELEMENT_ID",
                select: function (e) {
                    vm.obPayElement = this.dataItem(e.item);
                    vm.obPayElement.PAY_RATE = vm.obPayElement.PAY_AMT;
                }
            };
        };

        vm.salaryTranGrid = function () {
            
            var dataSource = new kendo.data.DataSource({
                batch: true,
                transport: {
                    read: function (e) {                        
                        HrService.getSalaryPayListData(vm.form.HR_SAL_TRAN_H_ID).then(function (res) {

                            if (vm.isEmpPayShow != true) {                                
                                vm.isEmpPayShow = true;

                                for (var x = 0; x < res.length; x++) {
                                    var index = HrService.getIndexByKeyValue(vm.salaryPayData, 'HR_PAY_ELEMENT_ID', res[x].HR_PAY_ELEMENT_ID);
                                    //console.log(res);
                                    if (index != null) {
                                        vm.salaryPayData[index].PAY_ELEMENT_NAME_EN = res[x].PAY_ELEMENT_NAME_EN;
                                        vm.salaryPayData[index].PAY_ELM_TYPE_NAME_EN = res[x].PAY_ELM_TYPE_NAME_EN;
                                        vm.salaryPayData[index].PAY_QTY_ACT = res[x].PAY_QTY_ACT;
                                        vm.salaryPayData[index].PAY_QTY_EXM = res[x].PAY_QTY_EXM;
                                        vm.salaryPayData[index].PAY_QTY = res[x].PAY_QTY;
                                        vm.salaryPayData[index].PAY_RATE = res[x].PAY_RATE;
                                        vm.salaryPayData[index].PAY_AMT = res[x].PAY_AMT;
                                        vm.salaryPayData[index].PAY_UNIT_FLAG = res[x].PAY_UNIT_FLAG;
                                        vm.salaryPayData[index].IS_PROCESSED = res[x].IS_PROCESSED;
                                        
                                    }
                                    else {
                                        vm.salaryPayData.push({
                                            HR_SAL_TRAN_D_ID: res[x].HR_SAL_TRAN_D_ID, HR_SAL_TRAN_H_ID: res[x].HR_SAL_TRAN_H_ID, HR_PAY_ELEMENT_ID: res[x].HR_PAY_ELEMENT_ID, PAY_ELEMENT_NAME_EN: res[x].PAY_ELEMENT_NAME_EN,
                                            PAY_AMT: res[x].PAY_AMT, PAY_ELM_TYPE_NAME_EN: res[x].PAY_ELM_TYPE_NAME_EN, PAY_QTY: res[x].PAY_QTY,
                                            PAY_RATE: res[x].PAY_RATE, PAY_QTY_ACT: res[x].PAY_QTY_ACT, PAY_QTY_EXM: res[x].PAY_QTY_EXM, PAY_UNIT_FLAG: res[x].PAY_UNIT_FLAG,
                                            IS_PROCESSED: res[x].IS_PROCESSED
                                        });
                                    };
                                }
                            };
                            //vm.empPayElementData = res;
                            //console.log(vm.empPayElementData);
                            if (vm.isAddEmpPayElement == true && vm.obPayElement.HR_PAY_ELEMENT_ID != '' && vm.obPayElement.HR_PAY_ELEMENT_ID != null) {
                                vm.isAddEmpPayElement = false;
                                
                                vm.form.PAY_RATE = vm.obPayElement.PAY_RATE;

                                if (vm.obPayElement.PAY_ELEMENT_CODE == 'D003') {
                                    vm.form.PAY_QTY_ACT = vm.form.LATE_DAYS;
                                    vm.form.PAY_RATE = vm.form.BASIC_PER_DAY;
                                }
                                else if (vm.obPayElement.PAY_ELEMENT_CODE == 'D004') {
                                    vm.form.PAY_QTY_ACT = vm.form.MISSED_OUT_PUNCH_DAYS;
                                    vm.form.PAY_RATE = vm.form.BASIC_PER_DAY;
                                }
                                else
                                {
                                    vm.form.PAY_QTY_ACT=1;
                                }

                                vm.salaryPayData.push({
                                    HR_SAL_TRAN_H_ID: vm.form.HR_SAL_TRAN_H_ID, HR_PAY_ELEMENT_ID: vm.obPayElement.HR_PAY_ELEMENT_ID, PAY_ELEMENT_NAME_EN: vm.obPayElement.PAY_ELEMENT_NAME_EN,
                                    PAY_AMT: vm.obPayElement.PAY_AMT, PAY_ELM_TYPE_NAME_EN: vm.obPayElement.PAY_ELM_TYPE_NAME_EN, PAY_QTY: 1,
                                    PAY_RATE: vm.form.PAY_RATE, PAY_QTY_ACT: vm.form.PAY_QTY_ACT, PAY_QTY_EXM: 0, PAY_UNIT_FLAG: 'M',
                                    IS_PROCESSED: 'N'
                                });
                            };

                            e.success(vm.salaryPayData);
                        });
                    },
                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return { models: kendo.stringify(options.models) };
                        }
                    }
                },
                pageSize: 1000,
                schema: {
                    model: {
                        id: "HR_PAY_ELEMENT_ID",
                        fields: {
                            HR_PAY_ELEMENT_ID: { type: "number", editable: false },
                            PAY_ELEMENT_NAME_EN: { type: "string", editable: false },
                            PAY_ELM_TYPE_NAME_EN: { type: "string", editable: false },
                            PAY_UNIT_FLAG: { type: "string", editable: true },
                            PAY_QTY_ACT: { type: "number", editable: true },                            
                            PAY_QTY: { type: "number", editable: true },
                            PAY_QTY_EXM: { type: "number", editable: true },
                            PAY_RATE: { type: "number", editable: true },
                            PAY_AMT: { type: "number", editable: false },
                            REMARKS: { type: "string", editable: false },
                            IS_PROCESSED: { type: "string", editable: false }
                        }
                    }
                }
                //aggregate: [
                //    { field: "PAY_AMT", aggregate: "sum" }
                //]
            });

            var unitTypeList = [{ UNIT_TYPE_ID: 'Hour', UNIT_TYPE_NAME: 'Hour' }, { UNIT_TYPE_ID: 'Day', UNIT_TYPE_NAME: 'Day' }, { UNIT_TYPE_ID: 'Month', UNIT_TYPE_NAME: 'Month' }];

            $("#salaryTranGrid").kendoGrid({
                dataSource: dataSource,
                navigatable: true,
                sortable: true,
                selectable: "row",
                pageable: {
                    refresh: false,
                    pageSizes: false
                },
                height: 300,
                //toolbar: ["save", "cancel"],
                columns: [
                    { field: "PAY_ELEMENT_NAME_EN", title: "Pay Element", width: "220px" },
                    { field: "PAY_ELM_TYPE_NAME_EN", title: "Type", width: "100px" },
                    { field: "PAY_UNIT_FLAG", title: "Unit Type", width: "80px", editor: unitTypeListData },
                    { field: "PAY_QTY_ACT", title: "Act.Qty.", width: "80px", attributes: { style: "text-align:right;" } },                    
                    { field: "PAY_QTY", title: "Pay.Qty.", width: "80px", attributes: { style: "text-align:right;" }, hidden:true },
                    { field: "PAY_QTY_EXM", title: "Exm.Qty.", width: "80px", attributes: { style: "text-align:right;" } },
                    { field: "PAY_RATE", title: "Rate", width: "80px", attributes: { style: "text-align:right;" } },
                    { field: "PAY_AMT", title: "Amount", width: "80px", attributes: { style: "text-align:right;" } },
                    { field: "REMARKS", title: "Remarks", width: "100px" },
                    { field: "IS_PROCESSED", title: "Processed?", width: "100px", hidden:false }
                ],
                editable: true,
                save: function (data) {
                    //console.log(data.values.PAY_QTY_EXM);
                    //console.log(data.model);

                    if (data.values.PAY_QTY_EXM != undefined){
                        data.model.PAY_QTY_EXM = data.values.PAY_QTY_EXM;
                    }
                    else if (data.values.PAY_QTY_ACT != undefined) {
                        data.model.PAY_QTY_ACT = data.values.PAY_QTY_ACT;
                    }
                    else if (data.values.PAY_RATE != undefined) {
                        data.model.PAY_RATE = data.values.PAY_RATE;
                    }

                    data.model.PAY_AMT = (data.model.PAY_QTY_ACT - data.model.PAY_QTY_EXM) * data.model.PAY_RATE;
                    data.model.IS_PROCESSED = 'N';
                    this.refresh();
                }
            });

            function unitTypeListData(container, options) {
                $('<input required data-text-field="UNIT_TYPE_NAME" data-value-field="UNIT_TYPE_ID" data-bind="value:' + options.field + '"/>')
                    .appendTo(container)
                    .kendoDropDownList({
                        autoBind: false,
                        dataSource: unitTypeList
                    });
            };

            //function unitTypeListData(container, options) {
            //    //console.log(options.field);
            //    $('<input required data-text-field="UNIT_TYPE_NAME" data-value-field="UNIT_TYPE_ID" data-bind="value:' + options.field + '"/>')
            //        .appendTo(container)
            //        .kendoDropDownList({
            //            autoBind: false,
            //            dataSource: unitTypeList
            //        });
            //};
        };

                
        function getCompanyList() {
            return vm.companyList = {
                //optionLabel: "-- Select Company --",
                filter: "startswith",
                autoBind: true,
                //index:1,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getCompanyData().then(function (res) {
                                e.success(res);

                                $('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                                    //optionLabel: "-- Select Period --",
                                    filter: "startswith",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                $http({
                                                    method: 'post',
                                                    url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
                                                    params: {
                                                        pHR_COMPANY_ID: res[0].HR_COMPANY_ID,
                                                        pHR_PERIOD_TYPE_ID: 3
                                                    }
                                                }).
                                                success(function (data, status, headers, config) {
                                                    e.success(data);
                                                    if (data.length > 0) {
                                                        vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;
                                                    };
                                                }).
                                                error(function (data, status, headers, config) {
                                                    alert('something went wrong')
                                                    console.log(status);
                                                });
                                            }
                                        }
                                    },
                                    dataTextField: "REMARKS",
                                    dataValueField: "ACC_PAY_PERIOD_ID",
                                    select: function (e) {                                        
                                        var dataItem = this.dataItem(e.item);
                                        vm.form.ACC_PAY_PERIOD_ID = dataItem.ACC_PAY_PERIOD_ID;
                                    }
                                });

                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    //alert('a');
                    var dataItem = this.dataItem(e.item);
                    var vHR_COMPANY_ID = dataItem.HR_COMPANY_ID; //this.value();
                    vm.form.ACC_PAY_PERIOD_ID = null;

                    $('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                        //optionLabel: "-- Select Period --",
                        filter: "startswith",
                        autoBind: true,
                        dataSource: {
                            transport: {
                                read: function (e) {
                                    $http({
                                        method: 'post',
                                        url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
                                        params: {
                                            pHR_COMPANY_ID: vHR_COMPANY_ID,
                                            pHR_PERIOD_TYPE_ID: 3
                                        }
                                    }).
                                    success(function (data, status, headers, config) {
                                        e.success(data);
                                        if (data.length > 0) {
                                            vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;
                                        };
                                        
                                    }).
                                    error(function (data, status, headers, config) {
                                        alert('something went wrong')
                                        console.log(status);
                                    });
                                }
                            }
                        },
                        dataTextField: "REMARKS",
                        dataValueField: "ACC_PAY_PERIOD_ID",
                        select: function (e) {
                            var dataItem = this.dataItem(e.item);
                            vm.form.ACC_PAY_PERIOD_ID = dataItem.ACC_PAY_PERIOD_ID;
                        }
                    });
                }
                
            };

        };

              
        //vm.EmployeeDataList = {
        //    optionLabel: "-- Select Employee --",
        //    template: '<span class="text-primary">#= data.EMPLOYEE_CODE #</span>  #if(data.EMP_FULL_NAME_EN!=undefined){#<span>: #= data.EMP_FULL_NAME_EN #</span>#}#',
        //    filter: "startswith",
        //    autoBind: true,
        //    dataSource: {
        //        transport: {
        //            read: function (e) {
        //                HrService.getEmployeeData().then(function (res) {
        //                    e.success(res);
        //                });
        //            }
        //        }
        //    },
        //    dataTextField: "EMPLOYEE_CODE",
        //    dataValueField: "HR_EMPLOYEE_ID"
        //};
        
        vm.submit = function (form, insert) {
            vm.showSplash = true;

            //if (!insert) return;

            var x = confirm("Are you sure to save salary data?");
            if (x == false) {
                vm.showSplash = false;
                return;
            }

            if (insert) {
                //vm.insert = false;   
                vm.salaryPayData = $("#salaryTranGrid").data("kendoGrid").dataSource.data();
                
                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrSalaryTran/BatchSave',
                    method: 'post',
                    data: { obHDR:vm.form, obList:vm.salaryPayData }
                }).then(function (data, status, headers, config1) {

                    vm.errors = undefined;
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        //console.log(data);
                        config.appToastMsg(data.data.x.vMsg);
                        vm.form.HR_SAL_TRAN_H_ID = data.data.x.vSalTranHID;
                        vm.insert = true;                        
                    }

                    vm.showSplash = false;

                }).catch(function (message) {
                    //console.log(message);
                    exception.catcher('XHR loading Failded')(message);
                });

            }
        };

        

        function activate(){
            var promise = [getCompanyList(), getPayElementListData()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

       
            
        
    }

    

})();