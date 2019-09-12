(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrYearlyCalendarController', ['logger', 'config', '$q', '$scope', '$http', '$state', '$stateParams', 'HrService', '$rootScope', HrYearlyCalendarController]);

    function HrYearlyCalendarController(logger, config, $q, $scope, $http, $state, $stateParams, HrService, $rootScope) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.showSplash = true;
        vm.showCalendar = true;
                
        vm.form = {};


        function activate() {
            var promise = [getCompanyData(), getFiscalYearData()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        function getCompanyData() {
            return HrService.getCompanyData().then(function (res) {                
                vm.companyData = res;
                if ($stateParams.pHR_COMPANY_ID == null) {
                    vm.form['HR_COMPANY_ID'] = res[0]['HR_COMPANY_ID'];
                }
            });
        }

        function getFiscalYearData() {
            return HrService.getFiscalYearData().then(function (res) {
                vm.FiscalYearData = res;

                if ($stateParams.pRF_FISCAL_YEAR_ID == null) {
                    vm.form['RF_FISCAL_YEAR_ID'] = res[0]['RF_FISCAL_YEAR_ID'];
                }                
            });
        }

        vm.form.HR_COMPANY_ID = $stateParams.pHR_COMPANY_ID;
        vm.form.RF_FISCAL_YEAR_ID = $stateParams.pRF_FISCAL_YEAR_ID;

        $scope.$watchGroup(['vm.form.HR_COMPANY_ID', 'vm.form.RF_FISCAL_YEAR_ID'], function (newVal, oldval) {
            if (newVal[0] != null && newVal[1] != null ) {
                //return HrService.getLeaveBalanceByEmplType(newVal[0], newVal[1], newVal[2], newVal[3]).then(function (res) {
                //    vm.LeaveBalance = res;
                //    if (res == 0) {
                //        logger.warning('No Leave Balance');
                //    }
                //});
                //alert('t');
                //$state.go('YearlyCalendarParamView.calender');
                ////// ****************************************************************
                //vm.showCalendar = false;
                

                $("#scheduler").empty();
                
                $("#scheduler").kendoScheduler({
                    //date: new Date("2013/6/13"),
                    //startTime: new Date("2013/6/13 07:00 AM"),
                    height: 600,
                    views: [
                        { type: "month", selected: true }
                        //"day",
                        //{ type: "workWeek", selected: true },
                        //"week",
                        //"month",
                        //"agenda",
                        //{ type: "timeline", eventHeight: 50 }
                    ],
                    //timezone: "Etc/UTC",
                    dataSource: {
                        //batch: true,
                        transport: {
                            read: {
                                method:'post',
                                url: "/Hr/HrYearlyCalander/HolidayCalendarData",
                                dataType: "json",
                                data: { pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pRF_FISCAL_YEAR_ID: vm.form.RF_FISCAL_YEAR_ID },
                                headers: { "RequestVerificationToken": vm.antiForgeryToken }
                            }//,
                            //update: {
                            //    url: "http://demos.telerik.com/kendo-ui/service/tasks/update",
                            //    dataType: "jsonp"
                            //},
                            //create: {
                            //    url: "http://demos.telerik.com/kendo-ui/service/tasks/create",
                            //    dataType: "jsonp"
                            //},
                            //destroy: {
                            //    url: "http://demos.telerik.com/kendo-ui/service/tasks/destroy",
                            //    dataType: "jsonp"
                            //},
                            //parameterMap: function (options, operation) {
                            //    if (operation !== "read" && options.models) {
                            //        return { models: kendo.stringify(options.models) };
                            //    }
                            //}
                        },
                        schema: {
                            model: {
                                id: "taskId",
                                fields: {
                                    taskId: { from: "TaskID", type: "number" },
                                    title: { from: "Title", defaultValue: "No title", validation: { required: true } },
                                    start: { type: "date", from: "Start" },
                                    end: { type: "date", from: "End" },
                                    startTimezone: { from: "StartTimezone" },
                                    endTimezone: { from: "EndTimezone" },
                                    description: { from: "Description" },
                                    recurrenceId: { from: "RecurrenceID" },
                                    recurrenceRule: { from: "RecurrenceRule" },
                                    recurrenceException: { from: "RecurrenceException" },
                                    ownerId: { from: "OwnerID", defaultValue: 1 },
                                    isAllDay: { type: "boolean", from: "IsAllDay" }
                                }
                            }
                        },
                        //filter: {
                        //    logic: "or",
                        //    filters: [
                        //        { field: "ownerId", operator: "eq", value: 1 },
                        //        { field: "ownerId", operator: "eq", value: 2 }
                        //    ]
                        //}
                    },
                    resources: [
                        {
                            field: "ownerId",
                            title: "Owner",
                            dataSource: [
                                { text: "Working Day", value: 1, color: "" },
                                { text: "Weekly Off Day", value: 2, color: "#db0ddb" },
                                { text: "Company/Govt. Holiday", value: 3, color: "#FFFF00" },
                                { text: "Govt. Holiday", value: 4, color: "#6600ff" }
                            ]
                        }
                    ]
                });

                ///// ***************************************************************


                                
            }

        });

        


        //////////// Start For View Calendar /////////////////////////////////////////////////////////////////////////////////
        vm.HolidayCalendarProcess = function () {
            //alert('dsfs');
            //$http.post('/Hr/HrYearlyCalander/HolidayCalendarProcess').success(function (data, status, headers, config1) {

            $http({
                    method: 'POST',
                    url: '/Hr/HrYearlyCalander/HolidayCalendarProcess',
                    data: { pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pRF_FISCAL_YEAR_ID: vm.form.RF_FISCAL_YEAR_ID },
                    headers: { "RequestVerificationToken": vm.antiForgeryToken }
            }).success(function (data, status, headers, config1) {
                vm.errors = [];
                if (data.success === false) {
                    vm.errors = data.errors;
                }
                else {
                    //if (update) {
                    //    $state.go('LeaveTransList.data', { datas: { HR_COMPANY_ID: vm.form.HR_COMPANY_ID, RF_FISCAL_YEAR_ID: vm.form.RF_FISCAL_YEAR_ID, HR_EMPLOYEE_ID: vm.form.HR_EMPLOYEE_ID } });
                    //} else {
                    //    $state.go('LeaveTransEntry', { LeaveTrans: null }, { reload: true });
                    //}
                    config.appToastMsg(data.vMsg);
                }

            }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });
        };
              
        //////////////////////////////////////////////////////        
        vm.EditCalenderDate = function () {            
            $state.go('EditCalenderDate', { pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pRF_FISCAL_YEAR_ID: vm.form.RF_FISCAL_YEAR_ID });            
        };        
        
        vm.YearlyCalendarView = function () {
            $state.go('YearlyCalendarView', { pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pRF_FISCAL_YEAR_ID: vm.form.RF_FISCAL_YEAR_ID });
        };
        /////////////////////////////////////////////////////        
        
        
            vm.schedulerOptions= {
                editable: false,
                //date: new Date(),
                //startTime: new Date(),  
                //timezone: "Etc/UTC",
                height: 600,
                views: [                    
                        //{ type: "week" },
                        { type: "month", selected: true }
                        //{ type: "agenda" }
                ],

                messages: {
                    allDay: "daily",
                    agenda: "List View"
                },
                autoBind: true,
                //sync: function () {
                //    this.read();
                //},
                autoSync: true,
                dataSource: {

                    transport: {
                        read: {
                            url: "/Hr/HrYearlyCalander/HolidayCalendarData",
                            dataType: "json"
                        }
                    },
                    //batch: true,                
                    schema: {
                        model: {
                            id: "taskId",
                            fields: {
                                taskId: { from: "TaskID", type: "number" },
                                title: { from: "Title", defaultValue: "No title", validation: { required: true } },
                                start: { type: "date", from: "Start" },
                                end: { type: "date", from: "End" },
                                startTimezone: { from: "StartTimezone" },
                                endTimezone: { from: "EndTimezone" },
                                description: { from: "Description" },
                                recurrenceId: { from: "RecurrenceID" },
                                recurrenceRule: { from: "RecurrenceRule" },
                                recurrenceException: { from: "RecurrenceException" },
                                ownerId: { from: "OwnerID", defaultValue: 1 },
                                isAllDay: { type: "boolean", from: "IsAllDay" }
                            }
                        }
                    },
                    //filter: {
                    //    logic: "or",
                    //    filters: [
                    //        { field: "ownerId", operator: "eq", value: 1 },
                    //        { field: "ownerId", operator: "eq", value: 2 },
                    //        { field: "ownerId", operator: "eq", value: 3 }
                    //    ]
                    //}
                },           
                resources: [
                    {
                        field: "ownerId",
                        title: "Owner",
                        dataSource: [
                            { text: "Working Day", value: 1, color: "" },
                            { text: "Weekly Off Day", value: 2, color: "#db0ddb" },
                            { text: "Company/Govt. Holiday", value: 3, color: "#FFFF00" },
                            { text: "Govt. Holiday", value: 4, color: "#6600ff" }
                        ]
                    }
                ]
            };
   
        //////////// End For View Calendar /////////////////////////////////////////////////////////////////////////////////


        //////////// Start For View List /////////////////////////////////////////////////////////////////////////////////

        $scope.monthSelectorOptions = {
            start: "year",
            depth: "year"
        };
        
        //$scope.Save = function () {
            
        //    return $http.get('/Hr/HrYearlyCalander/Save?dtpFromDate=' + $scope.dtpFromDate).then(function (res) {

        //    });

        //    $scope.dtpFromDate = null;
        //    $scope.dtpToDate = null;
        //};

        //vm.ShowCalendarList = function () {

        //    //alert($scope.ShowMonth);
        //    $("#grid").kendoGrid({
        //        dataSource: {
        //            transport: {
        //                read: {
        //                    url: "/Hr/HrYearlyCalander/HolidayCalendarListData?pShowMonth=" + $scope.ShowMonth,
        //                    dataType: "json",
        //                },
        //                update: {
        //                    url: "/Hr/HrYearlyCalander/Update",
        //                    contentType: "application/json",
        //                    accepts: "application/json",
        //                    type: "put",

        //                    dataType: "json"
        //                },
        //                destroy: {
        //                    url: "/Products/Destroy",
        //                    dataType: "jsonp"
        //                },
        //                create: {
        //                    url: "/Hr/HrYearlyCalander/Save",
        //                    contentType: "application/json",
        //                    accepts: "application/json",
        //                    type: "put",

        //                    dataType: "json"
        //                },
        //                parameterMap: function (data, type) {
        //                    if (type == "update" && data) {
        //                        return JSON.stringify(data);
        //                    }
        //                    if (type == "create" && data) {
        //                        return JSON.stringify(data);
        //                    }
        //                }
        //            },
        //            pageSize: 10,
        //            schema: {
        //                model: {
        //                    id: "HR_YRLY_CALNDR_ID",
        //                    fields: {
        //                        HR_YRLY_CALNDR_ID: { editable: false, nullable: true },
        //                        HR_COMPANY_ID: {},
        //                        RF_FISCAL_YEAR_ID: {},
        //                        CALNDR_DATE: { editable: false, type: "date" },
        //                        HR_DAY_TYPE_ID: {},
        //                        HR_HOLIDAY_ID: {},
        //                        REMARKS: {}
        //                    }
        //                }
        //            }
        //        },
        //        //height: 550,
        //        //groupable: true,
        //        sortable: true,
        //        pageable: {
        //            refresh: true,
        //            pageSizes: true,
        //            buttonCount: 5
        //        },
        //        //toolbar: ["create"],
        //        columns: [
        //        //{ field: "HR_YRLY_CALNDR_ID", title: "ID" },
        //        { field: "CALNDR_DATE", title: "Date", type: "Date", format: "{0:dd/MMM/yyyy}", width: "120px" },
        //        { field: "HR_DAY_TYPE_ID", title: "Day Type", width: "180px", editor: categoryDropDownEditor, template: "#=DAY_TYPE_NAME_EN#" },
        //        { field: "HR_HOLIDAY_ID", title: "Holiday", width: "180px", editor: holidayDropDownEditor, template: "#=HOLIDAY_NAME_EN#" },
        //        { field: "REMARKS", title: "Remarks", width: "180" },
        //        { command: ["edit"/*, "destroy"*/], title: "&nbsp;", width: "180px" }],
        //        editable: "inline"
        //    });
        //};


        vm.mainGridOptions = {
            dataSource: {
                transport: {
                    read: {
                        url: "/Hr/HrYearlyCalander/HolidayCalendarListData?pShowMonth=" + $scope.ShowMonth,
                        dataType: "json"
                    },
                    update: {
                        url: "/Hr/HrYearlyCalander/Update",
                        contentType: "application/json",
                        accepts: "application/json",
                        type: "put",

                        dataType: "json"
                    },
                    destroy: {
                        url: "/Products/Destroy",
                        dataType: "jsonp"
                    },
                    create: {
                        url: "/Hr/HrYearlyCalander/Save",
                        contentType: "application/json",
                        accepts: "application/json",
                        type: "put",

                        dataType: "json"
                    },
                    parameterMap: function (data, type) {
                        if (type == "update" && data) {
                            return JSON.stringify(data);
                        }
                        if (type == "create" && data) {
                            return JSON.stringify(data);
                        }
                    }
                },
                pageSize: 10,
                schema: {
                    model: {
                        id: "HR_YRLY_CALNDR_ID",
                        fields: {
                            HR_YRLY_CALNDR_ID: { editable: false, nullable: true },
                            HR_COMPANY_ID: {},
                            RF_FISCAL_YEAR_ID: {},
                            CALNDR_DATE: { editable: false, type: "date" },
                            HR_DAY_TYPE_ID: {},
                            HR_HOLIDAY_ID: {},
                            REMARKS: {}

                        }
                    }
                }
            },
            pageable: true,
            //height: 550,
            //toolbar: ["create"],
            columns: [
                //{ field: "HR_YRLY_CALNDR_ID", title: "ID" },
                { field: "CALNDR_DATE", title: "Date", format: "{0:dd/MMM/yyyy}", width: "120px" },
                { field: "HR_DAY_TYPE_ID", title: "Day Type", width: "180px", editor: categoryDropDownEditor, template: "#=DAY_TYPE_NAME_EN#" },
                { field: "HR_HOLIDAY_ID", title: "Holiday", width: "180px", editor: holidayDropDownEditor, template: "#=HOLIDAY_NAME_EN#" },
                { field: "REMARKS", title: "Remarks", width: "180" },
                { command: ["edit", "destroy"], title: "&nbsp;", width: "180px" }],
            editable: "popup"

        };


        function holidayDropDownEditor(container, options) {
            $('<input data-text-field="HOLIDAY_NAME_EN" data-value-field="HR_HOLIDAY_ID" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    optionLabel: "Select",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: {
                                url: "/Hr/HrYearlyCalander/HolidayListData",
                                dataType: "json"
                            }
                        }
                        //data: [{ HR_DAY_TYPE_ID: 1, DAY_TYPE_NAME_EN: 'A1' }, { HR_DAY_TYPE_ID: 2, DAY_TYPE_NAME_EN: 'A3' }]
                        //type: "odata",
                        //transport: {
                        //    read: "http://demos.telerik.com/kendo-ui/service/Northwind.svc/Categories"
                        //}
                    }
                });
        }

        function categoryDropDownEditor(container, options) {
            $('<input required data-text-field="DAY_TYPE_NAME_EN" data-value-field="HR_DAY_TYPE_ID" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    optionLabel: "Select",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: {
                                url: "/Hr/HrYearlyCalander/DayTypeListData",
                                dataType: "json"
                            }
                        }
                        //data: [{ HR_DAY_TYPE_ID: 1, DAY_TYPE_NAME_EN: 'A1' }, { HR_DAY_TYPE_ID: 2, DAY_TYPE_NAME_EN: 'A3' }]
                        //type: "odata",
                        //transport: {
                        //    read: "http://demos.telerik.com/kendo-ui/service/Northwind.svc/Categories"
                        //}
                    }
                });
        }
        //////////// End For View List /////////////////////////////////////////////////////////////////////////////////


        //////////// Start For View Form /////////////////////////////////////////////////////////////////////////////////

        //$scope.onFromDateSelected = function (e) {
        //    var datePicker = e.sender;
        //    vm.fromDate = datePicker.value();
        //    //console.log("From Date: " + vm.fromDate);
        //};

        //$scope.onToDateSelected = function (e) {
        //    var datePicker = e.sender;
        //    vm.toDate = datePicker.value();
        //    //console.log("To Date: " + vm.toDate);
        //};

        //$scope.today = function () {
        //    $scope.dt = new Date();
        //};
        //$scope.today();
        
        //vm.form.FROM_DATE = '15/Mar/2015';

        //$scope.clear = function () {
        //    $scope.dt = null;
        //};

        // Disable weekend selection
        //$scope.disabled = function (date, mode) {
        //    return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
        //};

        //$scope.toggleMin = function () {
        //    $scope.minDate = $scope.minDate ? null : new Date();
        //};
        //$scope.toggleMin();

        vm.today = new Date();

        vm.fromDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            
            vm.fromDateOpened = true;
        };

        vm.toDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            
            vm.toDateOpened = true;
        };
        vm.toDateMin = function (item) {
            console.log(item);
        };

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        //$scope.formats = ['dd-MMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        //$scope.format = $scope.formats[0];

        vm.TranCancel = function () {
            vm.reset();
        };

        vm.reset = function () {            
            vm.formCopy = vm.form;
            vm.form = {};           
        };

        //vm.directive('pwCheck', function () {
        //    return {
        //        require: 'ngModel',
        //        link: function (scope, elem, attrs, ctrl) {
        //            scope.$watch(attrs.pwCheck, function (confirmPassword) {
        //                var isValid = ctrl.$viewValue === confirmPassword;
        //                ctrl.$setValidity('pwmatch', isValid);
        //            });
        //        }
        //    }
        //});


        vm.UpdateDateRange = function (isValid, form) {
            //console.log(form);
            //alert('t');
            //if (!isValid) return;
            //alert('ok');

            $http({
                headers: { "RequestVerificationToken": vm.antiForgeryToken },
                url: '/Hr/HrYearlyCalander/UpdateDateRange',
                method: 'post',
                data: form
            }).then(function (data, status, headers, config1) {
                
                vm.errors = undefined;
                
                if (data.data.success === false) {
                    vm.errors = data.data.errors;
                }
                else {
                    config.appToastMsg(data.data.vMsg);
                    vm.insert = true;
                }

            }).catch(function (message) {
                //console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
            
            return;
        };


        vm.holidayTypeList = {
            optionLabel: "Select",
            autoBind: false,
            dataSource: {
                transport: {
                    read: {
                        url: "/Hr/HrYearlyCalander/DayTypeListData",
                        dataType: "json"
                    }
                }
            },
            dataTextField: "DAY_TYPE_NAME_EN",
            dataValueField: "HR_DAY_TYPE_ID",
            select: function onSelect(e) {
                var dataItem = this.dataItem(e.item);
                
                if (vm.form.IS_GOVT_HOLIDAY == 'Y' || dataItem.HR_DAY_TYPE_ID == 3) {
                    vm.enable = true;
                } else {
                    vm.enable = false;
                };

            }
        };


        vm.govtHolidayClick = function() {
            if (vm.form.IS_GOVT_HOLIDAY == 'Y' || vm.form.HR_DAY_TYPE_ID==3) {
                vm.enable = true;
            } else {
                vm.enable = false;
            };
        }

        vm.commonHolidayList = {
            optionLabel: "Select",
            autoBind: false,
            dataSource: {
                transport: {
                    read: {
                        url: "/Hr/HrYearlyCalander/HolidayListData",
                        dataType: "json"
                    }
                }
            },
            dataTextField: "HOLIDAY_NAME_EN",
            dataValueField: "HR_HOLIDAY_ID",
        };


        //$scope.monthPickerConfig = {
        //    start: "year",
        //    depth: "year",
        //    format: "MMMM yyyy"
        //};
        $scope.datePickerConfig = {
            //start: "year",
            //depth: "year",
            format: vm.dtFormat
        };

        //vm.directive('pwCheck', function () {
        //    return {
        //        require: 'ngModel',
        //        link: function (scope, elem, attrs, ctrl) {
        //            scope.$watch(attrs.pwCheck, function (confirmPassword) {
        //                var isValid = ctrl.$viewValue === confirmPassword;
        //                ctrl.$setValidity('pwmatch', isValid);
        //            });
        //        }
        //    }
        //});


        $scope.validate = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                $scope.validationMessage = "Hooray! Your tickets has been booked!";
                $scope.validationClass = "valid";
            } else {
                $scope.validationMessage = "Oops! There is invalid data in the form.";
                $scope.validationClass = "invalid";
            }
        }

        //////////// End For View Form /////////////////////////////////////////////////////////////////////////////////






        //vm.tabs = [
        //{ title: 'Entry', content: 'Dynamic content 1' },
        //{ title: 'List', content: 'Dynamic content 2' }
        ////{ title: 'Dynamic Title 3', content: 'Dynamic content 3' }
        //];


        

        //function getData(){
        //     return AdminService.getData().then(function (data) {
        //         //vm.schedulerOptions.dataSource.data = data;
        //         //return vm.schedulerOptions.dataSource.data;
           
        //    });
        //}

 
        

            
        
    }

    

})();