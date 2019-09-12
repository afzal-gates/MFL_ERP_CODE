(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrOtApproveController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'HrService', HrOtApproveController]);

    function HrOtApproveController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, HrService) {

        var vm = this;
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
        
        //var currMonth=moment()
        var lastDayOfMonth = moment().daysInMonth();

        var firstDate = moment().date(1);
        var lastDate = moment().date(lastDayOfMonth);


        vm.form = {};
        vm.form.IS_ATTEN_CORRECTION = 'N';
        var dt = firstDate._d;
        //vm.form.FROM_DT = $filter('date')(dt, vm.dtFormat);
        dt = lastDate._d;
        //vm.form.TO_DT = $filter('date')(dt, vm.dtFormat);                             
           
        vm.form.OT_APRV_DATE = $filter('date')(vm.today, vm.dtFormat);
        vm.form.OT_DATE = $filter('date')(vm.today, vm.dtFormat);
        vm.form.OT_DATE = "";

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.approveDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.approveDateOpened = true;
        };

        vm.otDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.otDateOpened = true;
        };

        vm.otFromDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.otFromDateOpened = true;
        };

        vm.otToDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.otToDateOpened = true;
        };

        //$scope.$watch('vm.form.OT_DATE', function (newVal, oldVal) {

        //    if (!newVal || newVal == '') {
        //        var strMonYr = moment(vm.today, "MMM-YYYY");
        //        alert(strMonYr);
        //    }

        //});
        $("#OT_DATE").blur(function () {
            var strMonYr = moment(vm.form.FROM_DATE).format("MMM-YYYY");  //moment().format("MMM-YYYY");
            var strDay = $("#OT_DATE").val();
            var vOtDate = moment(strDay + '-' + strMonYr);            
            
            $("#OT_DATE").val(moment(vOtDate).format('DD-MMM-YYYY'));
            vm.form.OT_DATE = moment(vOtDate).format('DD-MMM-YYYY');
            return;
        });

        $("#OT_HR").blur(function () {           
            var strOtVal = $("#OT_HR").val();
            if (strOtVal.length <= 2) {
                strOtVal = strOtVal.length == 2 ? strOtVal + ":00" : ('0' + strOtVal + ":00");
            }

            //strOtVal = "2000-Jan-01 " + strOtVal;
            $("#OT_HR").val(strOtVal);
            vm.form.OT_HR = strOtVal;
            return;
        });

        //======== Start Set Focus ===========
        $("#EMPLOYEE_CODE").keypress(function (e) {
            if (e.which == 13 && vm.form.EMPLOYEE_TYPE_ID == 2) { //For Staff
                $("#BTN_APPROVE").focus();
            }
            else if (e.which == 13 && vm.form.EMPLOYEE_TYPE_ID == 8) { //For Worker
                $("#OT_HR").focus();
            };
        });

        $("#OT_HR").keypress(function (e) {
            if (e.which == 13) {
                $("#BTN_APPROVE").focus();
            };
        });
        //======== End Set Focus ===========

        vm.isNext = false;
        
        vm.reset = function () {
            //vm.form = {};
            //vm.form.focus();
            //vm.form.OT_DATE = "";
            vm.form.EMPLOYEE_CODE = "";
            vm.form.IS_CLOCK_START = "N";
            $('#EMPLOYEE_CODE').focus();
            //$('#OT_DATE').select();
            vm.insert = true;
        };

        vm.next = function () {
            vm.isNext = true;
            vm.otGrid();
        };        

        $scope.$watchGroup(['vm.form.OT_APRV_DATE'], function (newVal, oldVal) {
            //alert(newVal); 
            if (newVal[0] != null) {
                $("#otApproveGrid").empty();
                vm.isNext = false;
            }
        });

        vm.addNew = function () {
            //vm.form = {};
            $state.go('OtApprove');
        };      

        //vm.getAttenData = function () {
        //    alert('test');
        //    vm.form.FROM_DATE = vm.form.OT_DATE;
        //    vm.form.TO_DATE = vm.form.OT_DATE;            
        //};


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
            //vm.form.COMP_NAME_EN = item.COMP_NAME_EN;
            //vm.form.HR_COMPANY_ID = item.HR_COMPANY_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.DEPARTMENT_NAME_EN;
            vm.form.EMPLOYEE_TYPE_ID = item.EMPLOYEE_TYPE_ID;
            

            vm.form.ATTEN_DATE = vm.form.OT_DATE;
            HrService.getAttenDataList(vm.form).then(function (res) {                
                if (res.length > 0) {
                    vm.form.HR_TA_PROCESS_DATA_ID = res[0].HR_TA_PROCESS_DATA_ID;
                    vm.form.TA_FLAG = res[0].TA_FLAG;

                    var clkInTime = moment(res[0].CLK_IN_WT);
                    vm.form.CLK_IN_WT = moment(res[0].ATTEN_DATE).format('YYYY-MMM-DD') + ' ' + moment(clkInTime).format(config.appTimeFormat);
                    var clkOutTime = moment(res[0].CLK_OUT_WT);
                    vm.form.CLK_OUT_WT = moment(res[0].ATTEN_DATE).format('YYYY-MMM-DD') + ' ' + moment(clkOutTime).format(config.appTimeFormat);

                    var inTime = moment(res[0].IN_TIME_WT);
                    vm.form.IN_TIME_WT_DISP = moment(inTime).format(config.appTimeFormat); //$filter('date')(moment()._d, config.appDateFormat);
                    vm.form.IN_TIME_WT = moment(res[0].ATTEN_DATE).format('YYYY-MMM-DD') + ' ' + moment(inTime).format(config.appTimeFormat);
                    var outTime = moment(res[0].OUT_TIME_WT);
                    vm.form.OUT_TIME_WT_DISP = moment(outTime).format(config.appTimeFormat);
                    vm.form.OUT_TIME_WT = moment(res[0].ATTEN_DATE).format('YYYY-MMM-DD') + ' ' + moment(outTime).format(config.appTimeFormat);
                    var otTime = moment(res[0].OT_HR);
                    vm.form.OT_HR = moment(otTime).format(config.appTimeFormat);                    
                };
            });
        }

        
        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.HR_TA_PROCESS_DATA_ID = null;
                vm.form.HR_EMPLOYEE_ID = null;
                vm.form.EMP_FULL_NAME_EN = null;
                vm.form.DEPARTMENT_NAME_EN = null;
                vm.form.DESIGNATION_NAME_EN = null;
                //vm.form.COMP_NAME_EN = null;
                //vm.form.HR_COMPANY_ID = null;
                //vm.form.EMP_FULL_NAME_EN = null;
                vm.form.IN_TIME_WT = "";
                vm.form.OUT_TIME_WT = "";
                vm.form.IN_TIME_WT_DISP = "";
                vm.form.OUT_TIME_WT_DISP = "";
                vm.form.OT_HR = "";
            }

        });

        $scope.$watch('vm.form.EMPLOYEE_CODE_SEARCH', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.HR_TA_PROCESS_DATA_ID = null;
                vm.form.HR_EMPLOYEE_ID = null;
                vm.form.EMP_FULL_NAME_EN = null;
                vm.form.DEPARTMENT_NAME_EN = null;
                vm.form.DESIGNATION_NAME_EN = null;
                //vm.form.COMP_NAME_EN = null;
                //vm.form.HR_COMPANY_ID = null;
                //vm.form.EMP_FULL_NAME_EN = null;
                vm.form.IN_TIME_WT = "";
                vm.form.OUT_TIME_WT = "";
                vm.form.IN_TIME_WT_DISP = "";
                vm.form.OUT_TIME_WT_DISP = "";
            }

        });
        
        function getSubdepartmentListData() {
            return vm.subdepartmentListData = {
                //optionLabel: "-- Select --",
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


        function getCompanyList() {
            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "startswith",
                autoBind: true,
                //index:1,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getCompanyData().then(function (res) {
                                e.success(res);

                                //$('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                                //    //optionLabel: "-- Select Period --",
                                //    filter: "startswith",
                                //    autoBind: true,
                                //    dataSource: {
                                //        transport: {
                                //            read: function (e) {
                                //                $http({
                                //                    method: 'post',
                                //                    url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
                                //                    params: {
                                //                        pHR_COMPANY_ID: res[0].HR_COMPANY_ID,
                                //                        pHR_PERIOD_TYPE_ID: 3
                                //                    }
                                //                }).
                                //                success(function (data, status, headers, config) {
                                //                    e.success(data);
                                //                    vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;
                                //                }).
                                //                error(function (data, status, headers, config) {
                                //                    alert('something went wrong')
                                //                    console.log(status);
                                //                });
                                //            }
                                //        }
                                //    },
                                //    dataTextField: "REMARKS",
                                //    dataValueField: "ACC_PAY_PERIOD_ID"
                                //});

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

                    $('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                        optionLabel: "-- Select Period --",
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
                                        //if (data.length > 0) {
                                        //    //vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;

                                        //    dt = moment(data[0].START_DATE)._d;
                                        //    vm.form.FROM_DATE = $filter('date')(dt, vm.dtFormat);

                                        //    dt = moment(data[0].END_DATE)._d;
                                        //    vm.form.TO_DATE = $filter('date')(dt, vm.dtFormat);
                                        //    alert(vm.form.FROM_DATE);
                                        //    vm.form.NO_OF_DAYS = (vm.form.TO_DATE - vm.form.FROM_DATE);
                                        //};
                                    }).
                                    error(function (data, status, headers, config) {
                                        alert('something went wrong')
                                        console.log(status);
                                    });
                                }
                            }
                        },
                        dataTextField: "MONTH_YEAR_NAME",
                        dataValueField: "ACC_PAY_PERIOD_ID",
                        select: function (e) {
                            var dataItem = this.dataItem(e.item);

                            dt = moment(dataItem.START_DATE)._d;
                            vm.form.FROM_DATE = $filter('date')(dt, vm.dtFormat);

                            var dt1 = moment(dataItem.END_DATE)._d;
                            vm.form.TO_DATE = $filter('date')(dt, vm.dtFormat);

                            vm.form.NO_OF_DAYS = moment(dt1).diff(dt, 'days') + 1;
                            //console.log(dataItem);

                        }
                    });
                }

            };

        };

        vm.submit = function (isValid, form, insert) {
            
            //if (!isValid) return;

            //alert('ok');
            //return;
            //console.log(form);

            var submitOb = { 
                HR_OT_APPROVE_ID: form.HR_OT_APPROVE_ID, ACC_PAY_PERIOD_ID: form.ACC_PAY_PERIOD_ID, HR_COMPANY_ID: form.HR_COMPANY_ID,
                OT_APRV_DATE: form.OT_APRV_DATE, OT_DATE: form.OT_DATE, IS_ACTIVE: form.IS_ACTIVE, IS_ATTEN_CORRECTION: form.IS_ATTEN_CORRECTION,
                EMPLOYEE_CODE: form.EMPLOYEE_CODE, HR_EMPLOYEE_ID: form.HR_EMPLOYEE_ID
            };
            var submitOb1 = angular.copy(form);
            //submitOb1['OT_HR'] = "01-Jan-2000 " + submitOb1['OT_HR'];
            console.log(submitOb1);

            if (insert) {

                if (vm.form.IS_CLOCK_START == 'Y') {
                    vm.form.IN_TIME_WT = vm.form.CLK_IN_WT;
                    submitOb1.IN_TIME_WT = vm.form.CLK_IN_WT;
                }
                vm.form.OUT_TIME_WT = vm.form.CLK_OUT_WT;
                submitOb1.OUT_TIME_WT = vm.form.CLK_OUT_WT;
                //vm.form.OUT_TIME_WT = moment(vm.form.ATTEN_DATE).format('YYYY-MMM-DD') + ' ' + vm.form.OUT_TIME_WT_DISP;

                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrOtApprove/Save',
                    method: 'post',
                    data: { ob: submitOb, ob1: submitOb1 }
                }).success(function (data, status, headers, config1) {
                    vm.errors = [];
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        //alert(data.vMsg.substr(0, 9));
                        if (data.vMsg.substr(0, 9) == 'MULTI-001') {                            
                            vm.reset();
                            vm.otGrid();
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
                    url: '/Hr/HrOtApprove/Update',
                    method: 'post',
                    data: submitData
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

        vm.otGridList = function () {            
            var dataSource1 = new kendo.data.DataSource({
                batch: true,
                transport: {
                    read: function (e) {
                        HrService.getOtApproveSearchListData(vm.form.OT_FROM_DATE, vm.form.OT_TO_DATE, vm.form.HR_EMPLOYEE_ID).then(function (res) {
                            //console.log(res);
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
            $("#otApproveListGrid").kendoGrid({
                dataSource: dataSource1,
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


        vm.backToList = function () {
            $state.go('OtApproveList');
        };
        
        function activate(){
            var promise = [getCompanyList(), getSubdepartmentListData()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

       
            
        
    }

    

})();

