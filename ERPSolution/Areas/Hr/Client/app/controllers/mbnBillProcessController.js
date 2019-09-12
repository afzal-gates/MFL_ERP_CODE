//============== Start for MbnBillProcessController
(function () {
    'use strict';
    angular.module('multitex.hr').controller('MbnBillProcessController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', '$state', '$stateParams', '$filter', 'Dialog', MbnBillProcessController]);

    function MbnBillProcessController(logger, config, HrService, $q, $scope, $http, $location, $state, $stateParams, $filter, Dialog) {
                        
        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        var key = 'ACC_PAY_PERIOD_ID';
        
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.today = new Date();
        vm.periodType = 3;
        vm.form = {}; //formData[key] ? formData : { ACC_PAY_PERIOD_ID: 0, IS_SHOW4_RPT: 'Y', IS_CLOSED: 'N' };

        
        
        activate();

        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        vm.START_DATE_Open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.START_DATE_Opened = true;
        }
        
        vm.END_DATE_Open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.END_DATE_Opened = true;
        }

        $scope.emoloyeeAuto = function (val) {

            return HrService.GetDataByUrl('/api/Hr/MbnBill/GetEmpAutoSearch?pEMPLOYEE_CODE=' + val).then(function (res) {
                return res;
            }, function (err) {
                console.log(err);
            });            
        };

        $scope.onSelectItem = function (item) {
            //console.log(item);            
            vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
            vm.form.HR_LEAVE_TRANS_ID = item.HR_LEAVE_TRANS_ID;

            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
            vm.form.DEPARTMENT_NAME_EN = item.DEPARTMENT_NAME_EN;
            vm.form.DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
            vm.form.SECTION_NAME = item.SECTION_NAME;

            vm.form.EMP_PROFILE = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.SECTION_NAME;

            vm.form.LEAVE_REF_NO = item.LEAVE_REF_NO;
            vm.form.FROM_DATE = item.FROM_DATE;
            vm.form.TO_DATE = item.TO_DATE;
            vm.form.EDD_DT = item.EDD_DT;
            vm.form.NO_DAYS_APPL = item.NO_DAYS_APPL;
            vm.form.HR_MBN_BILL_H_ID = item.HR_MBN_BILL_H_ID;

            vm.mbnBillGridDataSource.read();
        }

        function getCompanyList() {
           
            vm.compOptions = {
                optionLabel: "-- Select Company --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",                
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);
                    //return vm.compPayPeriodDataSource = new kendo.data.DataSource({
                    //    transport: {
                    //        read: function (e) {
                    //            return HrService.getDataByFullUrl("/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + dataItem.HR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodType + "&pIS_CLOSED=N").then(function (res) {
                    //                e.success(res);
                    //            });
                    //        }
                    //    }
                    //});
                },
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.GetAllOthers('/api/common/CompanyList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                }
            }                                  
        };
                
        function getPeriodType() {
            vm.periodTypeOptions = {
                optionLabel: "-- Select Type --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "PERIOD_TYPE_NAME_EN_UNIT",
                dataValueField: "HR_PERIOD_TYPE_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);                                       
                },
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getDataByFullUrl('/api/common/GetPayPeriodType').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                }
            }
         
        };

        function getFiscalYear() {
            vm.fiscalYearOptions = {
                optionLabel: "-- Select FY --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "FY_NAME_EN",
                dataValueField: "RF_FISCAL_YEAR_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);
                    vm.form.FY_START_DATE = dataItem.FY_START_DATE;

                    console.log(moment(vm.form.FY_START_DATE).get('year'));

                    if (dataItem.RF_FISCAL_YEAR_ID < 0) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'NewFYEntryModal.html',
                            controller: 'FYModalController',
                            controllerAs: 'vm',
                            size: 'lg',
                            windowClass: 'large-Modal'//,
                            //resolve: {
                            //    ColourList: function (KnittingDataService) {
                            //        return KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll');
                            //    }
                            //}
                        });

                        modalInstance.result.then(function (result) {
                            console.log(result);

                            if (result.RF_FISCAL_YEAR_ID && result.RF_FISCAL_YEAR_ID > 0) {
                                vm.fiscalYearDataSource.read().then(function () {
                                    vm.form['RF_FISCAL_YEAR_ID'] = result.RF_FISCAL_YEAR_ID;
                                    vm.form['FY_NAME_EN'] = result.FY_NAME_EN;
                                });
                            }
                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });
                    }
                }                
            }

            return vm.fiscalYearDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/common/GetPayFiscalYear?pIS_CLOSED=N').then(function (res) {
                            e.success([{ 'FY_NAME_EN': '---New FY---', RF_FISCAL_YEAR_ID: -1 }].concat(res));
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        function getMonthList() {
            vm.monthListOptions = {
                optionLabel: "-- Select Month --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "MONTH_NAME_EN",
                dataValueField: "RF_CAL_MONTH_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);

                    if (vm.form.HR_PERIOD_TYPE_ID == 3) {
                        //var lastDay = moment().dayOfMonth(); //year(year).month(month).date(day)
                        console.log(moment([moment(vm.form.FY_START_DATE).get('year'), 0, 31]).month(dataItem.MONTH_VALUE - 1).format("YYYY-MM-DD"));

                        vm.form.START_DATE = moment([moment(vm.form.FY_START_DATE).get('year'), 0, 1]).month(dataItem.MONTH_VALUE - 1).format("YYYY-MM-DD");
                        vm.form.END_DATE = moment([moment(vm.form.FY_START_DATE).get('year'), 0, 31]).month(dataItem.MONTH_VALUE - 1).format("YYYY-MM-DD");
                        vm.form.PAY_DATE = moment([moment(vm.form.FY_START_DATE).get('year'), 0, 31]).month(dataItem.MONTH_VALUE - 1).format("YYYY-MM-DD");
                    }
                },
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getDataByFullUrl('/api/common/GetMonthList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                }
            }

        };
        

        vm.mbnBillGridOption = {
            height: 300,
            sortable: true,
            scrollable: {
                virtual: true,
                scrollable:true
            },
            pageable: false,
            editable: false,
            selectable: "row",
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
            columns: [
                { field: "LEAVE_REF_NO", title: "Leave Ref#", type: "string", width: "15%" },
                { field: "EDD_DT", title: "EDD Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "10%", filterable: false },
                { field: "FROM_DATE", title: "From Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "10%", filterable: false },
                { field: "TO_DATE", title: "To Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "10%", filterable: false },
                { field: "NEXT_JOIN_DATE", title: "Next Join", type: "date", format: "{0:dd-MMM-yyyy}", width: "10%", filterable: false },
                { field: "SAL_MONTH_NAME_LST", title: "Calculated Months", type: "string", width: "35%" },
                { field: "TOTAL_PAY_AMT", title: "Total Pay", type: "string", width: "10%" }//,
                //{
                //    title: "Action",
                //    template: function () {                        
                //        return "<button class='btn btn-xs blue' ng-click='vm.editData(dataItem)' ><i class='fa fa-edit'> Edit</i></button>";
                //    },
                //    width: "10%"
                //}
            ]
        };

        vm.mbnBillGridDataSource = new kendo.data.DataSource({            
            transport: {
                read: function (e) {
                    
                    var url = '/api/hr/MbnBill/GetMbnBillHdrList?pHR_EMPLOYEE_ID=' + (vm.form.HR_EMPLOYEE_ID || 0);
                   
                    return HrService.GetDataByUrl(url).then(function (res) {
                        e.success(res);
                    }, function (err) {
                        console.log(err);
                    });                    
                }
            },
            schema: {                
                model: {
                    id: "HR_MBN_BILL_H_ID",
                    fields: {
                        COMP_NAME_EN: { type: "string", editable: false },
                        EDD_DT: { type: "date", editable: false },
                        FROM_DATE: { type: "date", editable: false },
                        TO_DATE: { type: "date", editable: false },
                        NEXT_JOIN_DATE: { type: "date", editable: false }
                    }
                }
            }
        });

                   
        vm.detailExpand = function (dtlDataItem) {

            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            return HrService.GetDataByUrl('/api/hr/MbnBill/GetMbnBillDtlList?pHR_MBN_BILL_H_ID=' + (dtlDataItem.HR_MBN_BILL_H_ID || 0)).then(function (res) {
                                e.success(res);
                            })
                        }
                    }
                },
                scrollable: true,
                editable: true,
                //selectable: "cell",
                columns: [                
                    { field: "MBN_BILL_NO", title: "Bill#", type: "string", width: "25%", editor: mbnBillNoEditor, template: '#= kendo.toString(MBN_BILL_NO) #' },
                    { field: "PHASE_NO", title: "Phase#", type: "string", width: "10%", editor: phaseNoEditor, template: '#= kendo.toString(PHASE_NO) #' },
                    //{ field: "NOTICE_DT", title: "Notice Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "15%" },
                    {
                        field: "NOTICE_DT", title: "Notice Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "15%", editor: noticeDateEditor,
                        template: '#= kendo.toString(NOTICE_DT,\'dd-MMM-yyyy\') #',
                        filterable: false
                    },
                    //{ field: "IS_EDIT_ENABLED", title: "IS_EDIT_ENABLED", width: "15%" },
                    {
                        field: "PAYMENT_DT", title: "Payment Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "15%", editor: paymentDateEditor,
                        template: '#= kendo.toString(PAYMENT_DT,\'dd-MMM-yyyy\') #',
                        filterable: false
                    },
                    { field: "PAY_AMT", title: "Pay Amount", type: "number", width: "15%", editor: payAmtEditor, template: '#= kendo.toString(PAY_AMT) #' },
                    {
                        title: "Action",
                        template: function () {
                            return "<button type='button' class='btn btn-xs blue' ng-click='vm.billPrint(dataItem)'><i class='fa fa-print'></i> Print</button>&nbsp;"                                
                                + "<button type='button' class='btn btn-xs blue' ng-click='vm.billEdit(dataItem)' ng-show='!dataItem.IS_EDIT_ENABLED'><i class='fa fa-pencil'></i> Edit</button>&nbsp;"
                                + "<button type='button' class='btn btn-xs yellow-gold' ng-click='vm.billUpdate(dataItem)' ng-show='dataItem.IS_EDIT_ENABLED'>Update</button>";
                        },
                        width: "20%"
                    }
                ],
                schema: {
                    model: {
                        id: "HR_MBN_BILL_D_ID",
                        fields: {
                            MBN_BILL_NO: { type: "string", editable: false },
                            PHASE_NO: { type: "string", editable: false },
                            PAY_AMT: { type: "number", editable: false },
                            NOTICE_DT: { type: "date", editable: true },
                            PAYMENT_DT: { type: "date", editable: true }
                        }
                    }
                }
            };
        };

        function mbnBillNoEditor(container, options) {
            console.log(options.model);

            $("<span>" + kendo.toString(options.model.MBN_BILL_NO) + "</span>").appendTo(container);
            return;

        }

        function phaseNoEditor(container, options) {
            console.log(options.model);

            $("<span>" + kendo.toString(options.model.PHASE_NO) + "</span>").appendTo(container);
            return;

        }

        function payAmtEditor(container, options) {
            console.log(options.model);

            $("<span>" + kendo.toString(options.model.PAY_AMT) + "</span>").appendTo(container);
            return;

        }

        function noticeDateEditor(container, options) {
            console.log(options.model);

            if (!options.model.IS_EDIT_ENABLED) {
                $("<span>" + kendo.toString(options.model.NOTICE_DT, 'dd-MMM-yyyy') + "</span>").appendTo(container);
                return;
            };

            $('<input type="text" data-bind="value:' + options.field + '"/>')
            .appendTo(container)
            .kendoDatePicker({
                format: "dd-MMM-yyyy",
                value: kendo.toString(new Date(options.model.NOTICE_DT), 'dd-MMM-yyyy')
            });

        }

        function paymentDateEditor(container, options) {
            console.log(options.model);

            if (!options.model.IS_EDIT_ENABLED) {
                $("<span>" + kendo.toString(options.model.PAYMENT_DT, 'dd-MMM-yyyy') + "</span>").appendTo(container);
                return;
            };

            $('<input type="text" data-bind="value:' + options.field + '"/>')
            .appendTo(container)
            .kendoDatePicker({
                format: "dd-MMM-yyyy",
                value: kendo.toString(new Date(options.model.PAYMENT_DT), 'dd-MMM-yyyy')
            });

        }

        vm.billEdit = function (dataItem) {
            dataItem.IS_EDIT_ENABLED = !dataItem.IS_EDIT_ENABLED;
        }
        
        vm.billPrint = function (dataItem) {
            
            dataItem['REPORT_CODE'] = 'RPT-1065';
            
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/Hr/HrReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.copy(dataItem);
            console.log(params);

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);

            form.submit();

            document.body.removeChild(form);
        }

        vm.billUpdate = function (dataItem) {
            var vMsg = 'Do you want to update the record?';

            var submitData = angular.copy(dataItem);
           
            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {
                submitData.NOTICE_DT = $filter('date')(submitData.NOTICE_DT, vm.dtFormat);
                submitData.PAYMENT_DT = $filter('date')(submitData.PAYMENT_DT, vm.dtFormat);

                return HrService.saveDataByUrl(submitData, '/api/hr/MbnBill/MbnBillPhaseUpdate').then(function (res) {
                    if (res.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        console.log(res);
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            dataItem.IS_EDIT_ENABLED = !dataItem.IS_EDIT_ENABLED;
                        };

                        config.appToastMsg(res.data.PMSG);

                    }
                });
            });
        }

        //vm.reasonGridOption = {
        //    height: 200,
        //    sortable: true,
        //    columns: [
        //        { field: "SFAB_RSN_TYPE_NAME", title: "Type", width: "20%" },
        //        { field: "REASON_DESC", title: "Fabric", width: "70%" },
        //        {
        //            title: "Action",
        //            template: function () {
        //                return "<button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)'><i class='fa fa-edit'></i></button>";
        //            },
        //            width: "10%"
        //        }
        //    ]
        //};


        //vm.onChangePeriodType = function () {

        //}

        vm.reset = function () {
            //vm.form = { HR_INCR_MEMO_ID: 0, IS_BY_ADMIN: 'Y', IS_FINALIZED: 'N', EFFECTIVE_DT: vm.today, MEMO_DT: vm.today };
            vm.form.HR_INCR_MEMO_ID = 0;
            vm.form.AUTH_NO = null;
            vm.form.MEMO_NO = null;
            vm.form.MEMO_DT = null;
            vm.form.REMARKS = null;
        }

        //vm.editData = function (data) {
        //    vm.form = angular.copy(data);
        //    $state.go('CompPayPeriod', { pACC_PAY_PERIOD_ID: data.ACC_PAY_PERIOD_ID }, { notify: false });
        //}


        vm.mbnBillProcess = function () {
            var vMsg = 'Do you want to process?';
            //if (pIS_CLOSED == 'Y') {
            //    submitData['IS_CLOSED'] = pIS_CLOSED;
            //    vMsg = "After closing, the period will lock & you can't any transaction into the period.</br></br> <b>Do you want to save and close the period?</b>";
            //}


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                return HrService.saveDataByUrl(vm.form, '/api/hr/MbnBill/MbnBillProcess').then(function (res) {
                    if (res.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        console.log(res);
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            //vm.reset();

                            //if (pIS_CLOSED == 'Y') {
                            //    vm.form.IS_CLOSED = pIS_CLOSED;
                            //}
                            //vm.form.ACC_PAY_PERIOD_ID = res.data.PACC_PAY_PERIOD_ID_RTN;
                            vm.mbnBillGridDataSource.read();

                            //$state.go('CompPayPeriod', { pACC_PAY_PERIOD_ID: res.data.PACC_PAY_PERIOD_ID_RTN }, { notify: false });

                        };

                        config.appToastMsg(res.data.PMSG);

                    }
                });
            });
        }


        vm.save = function (pIS_CLOSED) {
            var submitData = angular.copy(vm.form);

            submitData.START_DATE = $filter('date')(submitData.START_DATE, vm.dtFormat);
            submitData.END_DATE = $filter('date')(submitData.END_DATE, vm.dtFormat);
            submitData.PAY_DATE = $filter('date')(submitData.PAY_DATE, vm.dtFormat);
            
            var vMsg = 'Do you want to save?';
            if (pIS_CLOSED == 'Y') {
                submitData['IS_CLOSED'] = pIS_CLOSED;
                vMsg = "After closing, the period will lock & you can't any transaction into the period.</br></br> <b>Do you want to save and close the period?</b>";
            }


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                return HrService.saveDataByUrl(submitData, '/api/common/SaveCompPayPeriod').then(function (res) {
                    if (res.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        console.log(res);
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            //vm.reset();

                            if (pIS_CLOSED == 'Y') {
                                vm.form.IS_CLOSED = pIS_CLOSED;
                            }
                            vm.form.ACC_PAY_PERIOD_ID = res.data.PACC_PAY_PERIOD_ID_RTN;
                            vm.compPayPeriodGridDataSource.read();

                            $state.go('CompPayPeriod', { pACC_PAY_PERIOD_ID: res.data.PACC_PAY_PERIOD_ID_RTN }, { notify: false });

                        };

                        config.appToastMsg(res.data.PMSG);

                    }
                });
            });


        };


        vm.memoFinalize = function () {
            return HrService.saveDataByUrl(vm.form, '/api/Hr/HrIncriment/MemoFinalize').then(function (res) {
                if (res.success === false) {
                    vm.errors = data.errors;
                }
                else {
                    console.log(res);
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        //vm.reset();
                        vm.incrMemoGridDataSource.read();                        
                    };

                    config.appToastMsg(res.data.PMSG);

                }
            });
        }
            
        
    }
    
})();
//============== End for MbnBillProcessController


