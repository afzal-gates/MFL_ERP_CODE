////////// Start CuttingListController Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutTableTgtController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', CutTableTgtController]);
    function CutTableTgtController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.dtTimeFormat = config.appDateTimeFormat;

        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
                
        vm.errstyle = { 'border': '1px solid #f13d3d' };
        vm.today = new Date();

        vm.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p>(#: data.ORDER_NO #)</p></span>';
        vm.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO #)</h5></span>';

        vm.form = { CALENDAR_DT: $stateParams.pCALENDAR_DT || $filter('date')(vm.today, vm.dtFormat) };
        
        
        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getProdClndrID()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;                
            });
        }

        vm.clndrDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.clndrDateOpened = true;
        };
                     
        $scope.$watchGroup(['vm.form.CALENDAR_DT'], function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    vm.isVisible = 'N';
                    getProdClndrID();
                }
            }
        }, true);

        function getProdClndrID() {
            
            return CuttingDataService.getDataByFullUrl('/api/Cutting/CutTblTgt/GetProdPlnClndrId?pCALENDAR_DT=' + ($filter('date')(vm.form.CALENDAR_DT, vm.dtFormat) || $stateParams.pCALENDAR_DT)).then(function (res) {
                vm.GMT_PROD_PLN_CLNDR_ID = res.GMT_PROD_PLN_CLNDR_ID;

                $state.go('CutTableTgt', { pCALENDAR_DT: $filter('date')(vm.form.CALENDAR_DT, vm.dtFormat), pGMT_PROD_PLN_CLNDR_ID: res.GMT_PROD_PLN_CLNDR_ID }, { notify: false });
                return;
            });
        }
  
        vm.isVisible = 'Y';
        vm.showTarget = function () {           
            vm.isVisible = 'Y';
            vm.tblTgtGridDataSource.read();
        }


        vm.isFinalized = 'N';
        vm.finalizedAll = function (v, index) {
            var data = vm.tblTgtGridDataSource.data(); //angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
            console.log(data);
          
            angular.forEach(data, function (val, key) {
                if (val['IS_FINALIZED'] != 'Y') {
                    val['IS_SHOW_FINALIZED'] = v;
                }
            });
        };

        vm.onChangeAddHr = function (dataItem) {
            
            console.log(dataItem);

            dataItem.TOT_MP_ADD = parseInt(dataItem.H9_MP_ADD) + parseInt(dataItem.H10_MP_ADD) + parseInt(dataItem.H11_MP_ADD) + parseInt(dataItem.H12_MP_ADD) +
                parseInt(dataItem.H13_MP_ADD) + parseInt(dataItem.H14_MP_ADD) + parseInt(dataItem.H15_MP_ADD) + parseInt(dataItem.H15_P_MP_ADD);
        }
        
        vm.onChange4TrgtQty = function (dataItem) {
            dataItem.TARGET_QTY = Math.round((dataItem.STNDR_TRGT_PER_HR_PER_EMP * dataItem.TGT_WORK_HR) * dataItem.PLAN_MP_QTY);

            if (dataItem.GMT_CUT_TBL_TGT_ID < 1) {

                dataItem.H9_MP_ADD = 0;
                dataItem.H10_MP_ADD = 0;
                dataItem.H11_MP_ADD = 0;
                dataItem.H12_MP_ADD = 0;
                dataItem.H13_MP_ADD = 0;
                dataItem.H14_MP_ADD = 0;
                dataItem.H15_MP_ADD = 0;
                dataItem.H15_P_MP_ADD = 0;

                if (dataItem.TGT_WORK_HR == 9)
                    dataItem.H9_MP_ADD = dataItem.PLAN_MP_QTY;
                else if (dataItem.TGT_WORK_HR == 10)
                    dataItem.H10_MP_ADD = dataItem.PLAN_MP_QTY;
                else if (dataItem.TGT_WORK_HR == 11)
                    dataItem.H11_MP_ADD = dataItem.PLAN_MP_QTY;
                else if (dataItem.TGT_WORK_HR == 12)
                    dataItem.H12_MP_ADD = dataItem.PLAN_MP_QTY;
                else if (dataItem.TGT_WORK_HR == 13)
                    dataItem.H13_MP_ADD = dataItem.PLAN_MP_QTY;
                else if (dataItem.TGT_WORK_HR == 14)
                    dataItem.H14_MP_ADD = dataItem.PLAN_MP_QTY;
                else if (dataItem.TGT_WORK_HR == 15)
                    dataItem.H15_MP_ADD = dataItem.PLAN_MP_QTY;
                else if (dataItem.TGT_WORK_HR > 15)
                    dataItem.H15_P_MP_ADD = dataItem.PLAN_MP_QTY;
            }
        }

        vm.onChange4RevTrgtQty = function (dataItem) {
            dataItem.REVISED_TRGT = Math.round(((dataItem.PLAN_MP_QTY * 8) + (dataItem.H9_MP_ADD * 1) + (dataItem.H10_MP_ADD * 2) + (dataItem.H11_MP_ADD * 3)
                                + (dataItem.H12_MP_ADD * 4) + (dataItem.H13_MP_ADD * 5) + (dataItem.H14_MP_ADD * 6) + (dataItem.H15_MP_ADD * 7) + (dataItem.H15_P_MP_ADD * 8)) * dataItem.STNDR_TRGT_PER_HR_PER_EMP);
        }

        

        vm.tblTgtGridOption = {
            height: 350,
            scrollable: true,
            pageable: false,
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
            columns: [
                //{ field: "TOT_MP_ADD", title: "Valid?", width: "5%", filterable: false },
                {
                    headerTemplate: "<input type='checkbox' ng-model='vm.isFinalized' ng-click='vm.finalizedAll(vm.isFinalized, 0)' ng-true-value='\"Y\"' ng-false-value='\"N\"' > <i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Finalized?'\"></i>",
                    footerTemplate: "Total",
                    template: function () {
                        return "<input type='checkbox' title='Finalized?' ng-model='dataItem.IS_SHOW_FINALIZED' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='dataItem.IS_FINALIZED==\"Y\"' >";
                    },
                    width: "4%",                    
                },
                { field: "TABLE_NO", title: "Table", width: "4%", filterable: false, footerTemplate: "#=count#" },
                {
                    title: "Man power",                    
                    field: "PLAN_MP_QTY",
                    headerAttributes: { style: "overflow: visible; white-space: normal;" },
                    //footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmPlnMp'><input type='number' class='form-control' name='PLAN_MP_QTY' ng-model='dataItem.PLAN_MP_QTY' ng-min='0' ng-max='999' ng-disabled='dataItem.IS_FINALIZED==\"Y\"' ng-style='(frmPlnMp.PLAN_MP_QTY.$error.min||frmPlnMp.PLAN_MP_QTY.$error.max||(dataItem.PLAN_MP_QTY < dataItem.TOT_MP_ADD))? vm.errstyle:\"\"' ng-change='vm.onChange4TrgtQty(dataItem)' /></ng-form>";
                    },
                    width: "5%",
                    filterable: false
                },
                {
                    title: "Work Hour",
                    field: "TGT_WORK_HR",
                    headerAttributes: { style: "overflow: visible; white-space: normal;" },
                    template: function () {
                        return "<ng-form name='frmTgtWrkHr'><input type='number' class='form-control' name='TGT_WORK_HR' ng-model='dataItem.TGT_WORK_HR' ng-min='(dataItem.PLAN_MP_QTY> 0 || dataItem.TARGET_QTY>0)?1:0' max='24' ng-disabled='dataItem.IS_FINALIZED==\"Y\"' ng-style='(frmTgtWrkHr.TGT_WORK_HR.$error.min||frmTgtWrkHr.TGT_WORK_HR.$error.max)? vm.errstyle:\"\"' ng-change='vm.onChange4TrgtQty(dataItem)' /></ng-form>";
                    },
                    width: "5%",
                    filterable: false
                },
                {
                    title: "Target Qty",
                    field: "TARGET_QTY",
                    headerAttributes: { style: "overflow: visible; white-space: normal;" },
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmTgtQty'><input type='number' class='form-control' name='TARGET_QTY' ng-model='dataItem.TARGET_QTY' ng-min='0' max='9999999' ng-disabled='true' ng-style='(frmTgtQty.TARGET_QTY.$error.min||frmTgtQty.TARGET_QTY.$error.max)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "6%",
                    filterable: false
                },
                //{ field: "QTY_IN_BNDL", title: "Prod Qty", width: "5%", filterable: false },
                {
                    title: "Prod Qty",
                    field: "QTY_IN_BNDL",
                    headerAttributes: { style: "overflow: visible; white-space: normal;" },
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmQTY_IN_BNDL'><input type='number' class='form-control' name='QTY_IN_BNDL' ng-model='dataItem.QTY_IN_BNDL' ng-disabled='true' /></ng-form>";
                    },
                    width: "6%",
                    filterable: false
                },
                
                {
                    title: "Manpower Adition",
                    headerAttributes: {
                        "class": "table-header-cell",
                        style: "text-align: center; font-weight: bold"
                    },
                    columns: [
                                {
                                    title: "9th",
                                    field: "H9_MP_ADD",
                                    headerAttributes: { style: "overflow: visible; white-space: normal;" },
                                    template: function () {
                                        return "<ng-form name='frmH9_MP_ADD'><input type='number' class='form-control' name='H9_MP_ADD' ng-model='dataItem.H9_MP_ADD' min='0' max='999' ng-disabled='dataItem.IS_FINALIZED==\"N\"' ng-change='vm.onChangeAddHr(dataItem);vm.onChange4RevTrgtQty(dataItem)' /></ng-form>";
                                    },
                                    width: "5%",
                                    filterable: false
                                },
                                {
                                    title: "10th",
                                    field: "H10_MP_ADD",
                                    headerAttributes: { style: "overflow: visible; white-space: normal;" },
                                    template: function () {
                                        return "<ng-form name='frmH10_MP_ADD'><input type='number' class='form-control' name='H10_MP_ADD' ng-model='dataItem.H10_MP_ADD' min='0' max='999' ng-disabled='dataItem.IS_FINALIZED==\"N\"' ng-change='vm.onChangeAddHr(dataItem);vm.onChange4RevTrgtQty(dataItem)' /></ng-form>";
                                    },
                                    width: "5%",
                                    filterable: false
                                },
                                {
                                    title: "11th",
                                    field: "H11_MP_ADD",
                                    headerAttributes: { style: "overflow: visible; white-space: normal;" },
                                    template: function () {
                                        return "<ng-form name='frmTH11_MP_ADD'><input type='number' class='form-control' name='H11_MP_ADD' ng-model='dataItem.H11_MP_ADD' min='0' max='999' ng-disabled='dataItem.IS_FINALIZED==\"N\"' ng-change='vm.onChangeAddHr(dataItem);vm.onChange4RevTrgtQty(dataItem)' /></ng-form>";
                                    },
                                    width: "5%",
                                    filterable: false
                                },
                                {
                                    title: "12th",
                                    field: "H12_MP_ADD",
                                    headerAttributes: { style: "overflow: visible; white-space: normal;" },
                                    template: function () {
                                        return "<ng-form name='frmH12_MP_ADD'><input type='number' class='form-control' name='H12_MP_ADD' ng-model='dataItem.H12_MP_ADD' min='0' max='999' ng-disabled='dataItem.IS_FINALIZED==\"N\"' ng-change='vm.onChangeAddHr(dataItem);vm.onChange4RevTrgtQty(dataItem)' /></ng-form>";
                                    },
                                    width: "5%",
                                    filterable: false
                                },
                                {
                                    title: "13th",
                                    field: "H13_MP_ADD",
                                    headerAttributes: { style: "overflow: visible; white-space: normal;" },
                                    template: function () {
                                        return "<ng-form name='frmH13_MP_ADD'><input type='number' class='form-control' name='H13_MP_ADD' ng-model='dataItem.H13_MP_ADD' min='0' max='999' ng-disabled='dataItem.IS_FINALIZED==\"N\"' ng-change='vm.onChangeAddHr(dataItem);vm.onChange4RevTrgtQty(dataItem)' /></ng-form>";
                                    },
                                    width: "5%",
                                    filterable: false
                                },
                                {
                                    title: "14th",
                                    field: "H14_MP_ADD",
                                    headerAttributes: { style: "overflow: visible; white-space: normal;" },
                                    template: function () {
                                        return "<ng-form name='frmH14_MP_ADD'><input type='number' class='form-control' name='H14_MP_ADD' ng-model='dataItem.H14_MP_ADD' min='0' max='999' ng-disabled='dataItem.IS_FINALIZED==\"N\"' ng-change='vm.onChangeAddHr(dataItem);vm.onChange4RevTrgtQty(dataItem)' /></ng-form>";
                                    },
                                    width: "5%",
                                    filterable: false
                                },
                                {
                                    title: "15th",
                                    field: "H15_MP_ADD",
                                    headerAttributes: { style: "overflow: visible; white-space: normal;" },
                                    template: function () {
                                        return "<ng-form name='frmH15_MP_ADD'><input type='number' class='form-control' name='H15_MP_ADD' ng-model='dataItem.H15_MP_ADD' min='0' max='999' ng-disabled='dataItem.IS_FINALIZED==\"N\"' ng-change='vm.onChangeAddHr(dataItem);vm.onChange4RevTrgtQty(dataItem)' /></ng-form>";
                                    },
                                    width: "5%",
                                    filterable: false
                                },
                                {
                                    title: "MP>15th",
                                    field: "H15_P_MP_ADD",
                                    headerAttributes: { style: "overflow: visible; white-space: normal;" },
                                    template: function () {
                                        return "<ng-form name='frmH15_P_MP_ADD'><input type='number' class='form-control' name='H15_P_MP_ADD' ng-model='dataItem.H15_P_MP_ADD' min='0' max='999' ng-disabled='dataItem.IS_FINALIZED==\"N\"' ng-change='vm.onChangeAddHr(dataItem);vm.onChange4RevTrgtQty(dataItem)' /></ng-form>";
                                    },
                                    width: "5%",
                                    filterable: false
                                },
                                {
                                    title: "WH>15th",
                                    field: "H15_P_WH",
                                    headerAttributes: { style: "overflow: visible; white-space: normal;" },
                                    template: function () {
                                        return "<ng-form name='frmH15_P_WH'><input type='number' class='form-control' name='H15_P_WH' ng-model='dataItem.H15_P_WH' min='0' max='24' ng-disabled='dataItem.IS_FINALIZED==\"N\"' /></ng-form>";
                                    },
                                    width: "5%",
                                    filterable: false
                                }                                
                             ]
                },
                {
                    title: "Revise Trgt",
                    field: "REVISED_TRGT",
                    headerAttributes: { style: "overflow: visible; white-space: normal;" },
                    template: function () {
                        return "<ng-form name='frmREVISED_TRGT'><input type='number' class='form-control' name='REVISED_TRGT' ng-model='dataItem.REVISED_TRGT' min='0' max='99999999' ng-disabled='true' /></ng-form>";
                    },
                    width: "6%",
                    filterable: false
                },
                {
                    title: "Remarks",
                    field: "REMARKS",
                    //footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmPlnMp'><input type='text' class='form-control' name='REMARKS' ng-model='dataItem.REMARKS' ng-disabled='dataItem.IS_FINALIZED==\"N\"' /></ng-form>";
                    },
                    width: "20%",
                    filterable: false
                },
            ]
        };


        vm.tblTgtGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    
                    var url = '/api/Cutting/CutTblTgt/GetCuttingTableTergetList?pGMT_PROD_PLN_CLNDR_ID=' + ($stateParams.pGMT_PROD_PLN_CLNDR_ID||vm.GMT_PROD_PLN_CLNDR_ID||0);

                    console.log(url);

                    CuttingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            aggregate: [
                { field: "TABLE_NO", aggregate: "count" },
                { field: "TARGET_QTY", aggregate: "sum" },
                { field: "QTY_IN_BNDL", aggregate: "sum" }
            ],
        });


        vm.reset = function () {
            vm.tblTgtGridDataSource.read();
        }

        vm.batchSave = function () {
            var submitData = {};
            var isValid = 'Y';

            var dataList = vm.tblTgtGridDataSource.data();

            angular.forEach(dataList, function (val, key) {
                if (val['PLAN_MP_QTY'] < val['TOT_MP_ADD']) {
                    isValid = 'N';
                }
            });

            if (isValid == 'N') {
                return;
            }
            else {

                submitData.GMT_CUT_TBL_TGT_XML = CuttingDataService.xmlStringShort(dataList.map(function (ob) {
                    return {
                        GMT_CUT_TBL_TGT_ID: ob.GMT_CUT_TBL_TGT_ID, GMT_PROD_PLN_CLNDR_ID: vm.GMT_PROD_PLN_CLNDR_ID, GMT_CUT_TABLE_ID: ob.GMT_CUT_TABLE_ID,
                        PLAN_MP_QTY: ob.PLAN_MP_QTY, TGT_WORK_HR: ob.TGT_WORK_HR, TARGET_QTY: ob.TARGET_QTY, H9_MP_ADD: ob.H9_MP_ADD, H10_MP_ADD: ob.H10_MP_ADD,
                        H11_MP_ADD: ob.H11_MP_ADD, H12_MP_ADD: ob.H12_MP_ADD, H13_MP_ADD: ob.H13_MP_ADD, H14_MP_ADD: ob.H14_MP_ADD, H15_MP_ADD: ob.H15_MP_ADD,
                        H15_P_MP_ADD: ob.H15_P_MP_ADD, H15_P_WH: ob.H15_P_WH, REVISED_TRGT: ob.REVISED_TRGT, REMARKS: ob.REMARKS, IS_FINALIZED: (ob.IS_FINALIZED == "Y" ? "Y" : ob.IS_SHOW_FINALIZED)
                    };
                }));

                console.log(submitData);
                //return;

                Dialog.confirm('Do you want to save?', 'Confirmation', ['Yes', 'No'])
                     .then(function () {

                         $http({
                             headers: { 'Authorization': 'Bearer ' + $scope.token },
                             url: '/api/Cutting/CutTblTgt/BatchSave',
                             method: 'post',
                             data: submitData
                         }).then(function (res) {

                             console.log(res);

                             vm.errors = undefined;
                             if (res.data.success === false) {
                                 vm.errors = [];
                                 vm.errors = res.data.errors;
                             }
                             else {

                                 res['data'] = angular.fromJson(res.data.jsonStr);

                                 if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                     //$state.go('CutTableTgt', { pGMT_PROD_PLN_CLNDR_ID: vm.GMT_PROD_PLN_CLNDR_ID }, { notify: false });

                                     vm.tblTgtGridDataSource.read();
                                 }
                                 config.appToastMsg(res.data.PMSG);
                             }
                         }).catch(function (message) {
                             exception.catcher('XHR loading Failded')(message);
                         });


                     });
            }
        };
              
      

    }

})();
////////// End CutMrkr4CadController Controller






