////////// Start KntScoYdTrChalnHController Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScoYdTrChalnHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', 'ScoYdTrChalnHdrData', KntScoYdTrChalnHController]);
    function KntScoYdTrChalnHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog, ScoYdTrChalnHdrData) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
        
        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        var key = 'KNT_SCO_YD_TR_CHL_H_ID';
        vm.today = new Date();

        vm.form = ScoYdTrChalnHdrData[key] ? ScoYdTrChalnHdrData : {
            KNT_SCO_YD_TR_CHL_H_ID: 0, SCM_SUPPLIER_ID: 0, CHALAN_NO: null, CHALAN_DT: $filter('date')(vm.today, vm.dtFormat),
            LNK_RCV_CHLN_LST: ''
        };
                         

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getSupplierList(), getScoIssProgList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;                
            });
        }


        vm.chlnDtlDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {

                    var url = '/api/Knit/KntScoYdTrChaln/GetScoYdTrChalnDtl?pKNT_SCO_YD_TR_CHL_H_ID=' + (vm.form.KNT_SCO_YD_TR_CHL_H_ID || $stateParams.pKNT_SCO_YD_TR_CHL_H_ID || 0);

                    console.log(url);

                    //e.success([]);

                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            aggregate: [                
                { field: "PACK_TR_QTY", aggregate: "sum" },
                { field: "TR_QTY", aggregate: "sum" }
            ],
            schema: {
                model: {
                    id: "KNT_YD_RCV_CHL_D_ID",
                    fields: {                        
                        PACK_TR_QTY: { type: "number", filterable: false },
                        TR_QTY: { type: "number", filterable: false }
                    }
                }
            }
        });

        vm.ydRecvChalnDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {

                    var url = '/api/Knit/KntYdRecvChaln/GetYdRecvChalnList4TrChaln?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0);

                    console.log(url);

                    //e.success([]);

                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            schema: {                
                model: {
                    id: "KNT_YD_RCV_CHL_H_ID",
                    fields: {
                        CHALAN_DT: { type: "date", filterable: false },
                        RCV_YD_QTY: { type: "number", filterable: false }
                    }
                }
            }
        });

       
                
        vm.chlnDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.chlnDateOpened = true;
        };

        function getSupplierList() {
            vm.supplierOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                   
                    vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;

                    vm.scoIssProgDataSource.read();
                    vm.ydRecvChalnDataSource.read();
                },
                dataBound: function (e) {
                    vm.scoIssProgDataSource.read();
                }
            };

            return vm.supplierDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };
        
        function getScoIssProgList() {
            //vm.scoIssProgOption = {
            //    optionLabel: "-- Knitting Program --",
            //    filter: "contains",
            //    autoBind: true,
            //    dataTextField: "PRG_ISS_NO",
            //    dataValueField: "KNT_SC_PRG_ISS_ID"
            //};
            
            return vm.scoIssProgDataSource = new kendo.data.DataSource({
                //serverFiltering: true,
                transport: {
                    read: function (e) {
                        //var webapi = new kendo.data.transports.webapi({});
                        //var params = webapi.parameterMap(e.data);

                        var url = '/api/knit/KntScoYdTrChaln/GetScoYdTrChlPrgList?pSCM_SUPPLIER_ID=' + (ScoYdTrChalnHdrData['SCM_SUPPLIER_ID'] || vm.form.SCM_SUPPLIER_ID || 0);
                        //url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        //var paramsData = params.filter.replace(/'/g, '').split('~');
                        //console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };
        
        //$scope.$watch('vm.form', function (newVal, oldVal) {
        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            $scope.form = vm.form;
        //        }
        //    }
        //}, true);

        vm.ydRecvChalnOptions = {
            height: 380,
            scrollable: {
                virtual: true,
                //scrollable:true
            },
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
                {
                    headerTemplate: "<input type='checkbox' ng-model='vm.isSelect' ng-click='vm.selectAll(vm.isSelect,0)'  ng-true-value='\"Y\"' ng-false-value='\"N\"' > <i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Select?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Select?' ng-model='dataItem.IS_SELECT' ng-true-value='\"Y\"' ng-false-value='\"N\"' >";
                    },
                    width: "50px"
                },
                { field: "CHALAN_DT", title: "Date", type: "string", format: "{0:dd-MM-yyyy}", width: "100px", editable: false, filterable: false },
                { field: "CHALAN_NO", title: "Challan#", type: "string", width: "200px", editable: false, filterable: false },
                { field: "RCV_YD_QTY", title: "Qty (Kg)", type: "string", width: "100px", editable: false, filterable: false }
            ]
        };

        vm.selectAll = function (v, index) {
            var data = vm.ydRecvChalnDataSource.data(); //angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
            console.log(data);

            angular.forEach(data, function (val, key) {
                val['IS_SELECT'] = v;
            });
        };

        vm.includeChaln = function () {
            
            //angular.forEach(vm.chlnDtlDataSource.data(), function (val, key) {
            //    var dataItem = vm.chlnDtlDataSource.at(key);
            //    vm.chlnDtlDataSource.remove(dataItem);
            //    console.log(key);
            //});
            $("#chlnDtlGrid").data('kendoGrid').dataSource.data([]);

            var data = vm.ydRecvChalnDataSource.data();
                        
            console.log(data);
            var vCount = 0;
            vm.form.LNK_RCV_CHLN_LST = "";

            angular.forEach(data, function (val, key) {
                if (val['IS_SELECT'] == 'Y') {
                    var vComma = '';
                    if (vCount > 0) {
                        vComma = ',';
                    }

                    vm.form.LNK_RCV_CHLN_LST = vm.form.LNK_RCV_CHLN_LST + vComma + val['KNT_YD_RCV_CHL_H_ID'];

                    vCount = vCount + 1;
                }
            });
            

            var url = '/api/Knit/KntYdRecvChaln/GetYdList4TrChaln?pKNT_YD_RCV_CHL_H_LST=' + (vm.form.LNK_RCV_CHLN_LST || '');
            console.log(url);
            console.log(vm.form);

            KnittingDataService.getDataByFullUrl(url).then(function (res) {
                                
                angular.forEach(res, function (val, key) {

                    //var includeList = _.filter(vm.chlnDtlDataSource.data(), function (o) {
                    //    return (val['KNT_YDP_D_COL_ID'] == o.KNT_YDP_D_COL_ID && val['YARN_ITEM_ID'] == o.YARN_ITEM_ID && val['KNT_YRN_LOT_ID'] == o.KNT_YRN_LOT_ID);
                    //});

                    val['PACK_TR_QTY'] = val['RCV_PACK_QTY'];
                    val['TR_QTY'] = val['RCV_YD_QTY'];

                    val['ScoIssProgList'] = vm.scoIssProgDataSource.data();

                    //if (includeList.length < 1) {
                        vm.chlnDtlDataSource.insert(0, val);
                    //}
                });
            });            
        }

        vm.removeRow = function (dataItem) {
            var data = angular.copy(dataItem);

            var dataList = vm.billDtlDataSource.data();
            var length = dataList.length;

            var vMsg = 'Do you want to remove items of the challan# ' + dataItem.CHALAN_NO;

            console.log(dataList);

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                var item, i;
                for (i = length - 1; i >= 0; i--) {

                    item = dataList[i];
                    if (data.CHALAN_NO == item['CHALAN_NO']) {
                        vm.billDtlDataSource.remove(item);
                    }

                }
            });
        };

        function DesigDropDownEditor(container, options) {
            //if (options.model.PROMOTED_DISABLED) {
            //    $("<span>" + options.model.get(options.field).DESIGNATION_NAME_EN + "</span>").appendTo(container);
            //    return;
            //};

            $('<input data-text-field="PRG_ISS_NO" data-value-field="KNT_SC_PRG_ISS_ID" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoMultiSelect({
                    filter: "contains",
                    //optionLabel: "--N/A--",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {                                
                                e.success(vm.scoIssProgDataSource.data());
                            }
                        }
                    }//,
                    //change: function (e) {
                    //    var item = this.dataItem(e.item);
                    //    console.log(item);
                    //    options.model.HR_DESIGNATION_ID = item.HR_DESIGNATION_ID;
                        
                    //}
                });
        }

        vm.chlnDtlOptions = {
            height: 350,
            sortable: true,
            pageable: false,
            //editable: true,
            //selectable: "row",
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
                //{ field: "KNT_SCO_YD_TR_CHL_D_ID", title: "KNT_SCO_YD_TR_CHL_D_ID", type: "string", width: "10%", editable: false, filterable: false },
                //{ field: "KNT_YDP_D_COL_ID", title: "KNT_YDP_D_COL_ID", type: "string", width: "10%", editable: false, filterable: false },
                //{ field: "YARN_ITEM_ID", title: "YARN_ITEM_ID", type: "string", width: "10%", editable: false, filterable: false },
                //{ field: "KNT_YRN_LOT_ID", title: "KNT_YRN_LOT_ID", type: "string", width: "10%", editable: false, filterable: false },
                {
                    title: "Kntting Prog#",
                    field: "KNT_SC_PRG_ISS_LIST",
                    template: function () {
                        return "<ng-form name='frmKNT_SC_PRG_ISS_LIST'><div class='k-widget k-multiselect k-header form-control' ng-style='frmKNT_SC_PRG_ISS_LIST.KNT_SC_PRG_ISS_LIST.$error.required? vm.errstyle:\"\"'> <select class='form-control' kendo-multi-select name='KNT_SC_PRG_ISS_LIST' k-placeholder='\"Search...\"' k-data-text-field='\"PRG_ISS_NO\"' k-data-value-field='\"KNT_SC_PRG_ISS_ID\"' k-data-source='dataItem.ScoIssProgList' ng-model='dataItem.KNT_SC_PRG_ISS_LIST' ng-disabled='vm.form.IS_FINALIZED==\"Y\"' required ></select></div></ng-form>";
                    },
                    width: "20%",
                    filterable: false
                },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "15%", editable: false, filterable: false },
                { field: "YR_SPEC_DESC", title: "Yarn Item", type: "string", width: "25%", editable: false, filterable: false },
                { field: "YRN_LOT_NO", title: "Lot#", type: "string", width: "10%", editable: false, filterable: false },
                //{ field: "ADV_RCV_QTY", title: "Adv Recv Qty(Kg)", type: "number", width: "11%", editable: false, filterable: false, footerTemplate: "#=kendo.toString(sum, '0.00')#" },
                //{ field: "YD_BATCH_NO", title: "Batch#", type: "string", width: "10%", editable: false, filterable: false },               
                { field: "PACK_TR_QTY", title: "Pack Qty (Bag)", type: "number", width: "10%", editable: false, filterable: false, footerTemplate: "#=kendo.toString(sum, '0.00')#" },
                //{ field: "RCV_CONE_QTY", title: "Cone Qty", type: "number", width: "8%", editable: false, filterable: false, footerTemplate: "#=kendo.toString(sum, '0.00')#" },
                { field: "TR_QTY", title: "Yrn Qty(Kg)", type: "number", width: "10%", editable: false, filterable: false, footerTemplate: "#=kendo.toString(sum, '0.00')#" },
                {
                    title: "Remarks",
                    field: "SP_NOTE",
                    template: function () {
                        return "<ng-form name='frmSP_NOTE'><input type='text' class='form-control' name='SP_NOTE' ng-model='dataItem.SP_NOTE' ng-disabled='vm.form.IS_FINALIZED==\"Y\"' /></ng-form>";
                    },
                    width: "10%",
                    filterable: false
                }
            ]
        };


        vm.submitData = function (token, pIS_FINALIZED) {
            var submitData = angular.copy(vm.form);
            var vMsg = 'Do you want to save?';
            if (pIS_FINALIZED == 'Y') {
                vMsg = 'Do you want to save and finalize?';
            }


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                submitData['IS_FINALIZED'] = pIS_FINALIZED;
                submitData['CHALAN_DT'] = $filter('date')(vm.form.CHALAN_DT, vm.dtFormat);
                

                var chlnDtlData = vm.chlnDtlDataSource.data();
                console.log(chlnDtlData);


                submitData.KNT_SCO_YD_TR_CHL_D_XML = KnittingDataService.xmlStringShort(chlnDtlData.map(function (ob) {
                    return {
                        KNT_SCO_YD_TR_CHL_D_ID: (ob.KNT_SCO_YD_TR_CHL_D_ID||0), KNT_SCO_YD_TR_CHL_H_ID: ob.KNT_SCO_YD_TR_CHL_H_ID, KNT_YDP_D_COL_ID: ob.KNT_YDP_D_COL_ID,
                        YARN_ITEM_ID: ob.YARN_ITEM_ID, KNT_YRN_LOT_ID: ob.KNT_YRN_LOT_ID, PACK_TR_QTY: ob.PACK_TR_QTY, PACK_MOU_ID: 29, TR_QTY: ob.TR_QTY,
                        QTY_MOU_ID: 3, SP_NOTE: ob.SP_NOTE, KNT_SC_PRG_ISS_LST: ob.KNT_SC_PRG_ISS_LIST.join(",")
                    };
                }));

                console.log(submitData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + token },
                    url: '/api/Knit/KntScoYdTrChaln/BatchSave',
                    method: 'post',
                    data: submitData
                })
                .then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.isSaved = true;

                            vm.form.KNT_SCO_YD_TR_CHL_H_ID = res['data'].PKNT_SCO_YD_TR_CHL_H_ID_RTN;
                            vm.form.CHALAN_NO = res['data'].PCHALAN_NO_RTN;
                            vm.form.GATE_PASS_NO = res['data'].PGATE_PASS_NO_RTN;

                            if (pIS_FINALIZED == 'Y') {
                                vm.form.IS_FINALIZED = 'Y';
                            }

                            vm.chlnDtlDataSource.read();

                            $state.go('ScoYdTrChalnH', { pKNT_SCO_YD_TR_CHL_H_ID: res['data'].PKNT_SCO_YD_TR_CHL_H_ID_RTN }, { notify: false });
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

        vm.printDeliveryChallan = function (pKNT_SCO_YD_TR_CHL_H_ID) {
            if (!pKNT_SCO_YD_TR_CHL_H_ID) { return; }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = {
                REPORT_CODE: 'RPT-3582',
                KNT_SCO_YD_TR_CHL_H_ID: pKNT_SCO_YD_TR_CHL_H_ID
            };

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
        };

    }

})();
////////// End KntScoYdTrChalnHController Controller










//=============== Start for KntYdRecvChalnListController List =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScoYdTrChalnListController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', KntScoYdTrChalnListController]);
    function KntScoYdTrChalnListController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, commonDataService) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.form = {};

        

        //vm.fabOrder = { FIN_DIA_N_DIA_TYPE: '', DIA_MOU_ID: 23, LK_DIA_TYPE_ID: 327, DIA_MOU_CODE: 'Open', QTY_MOU_ID: 3, QTY_MOU_CODE: 'Kg' };
        //vm.fabOrder = { MC_FAB_PROD_ORD_D_ID: -1, MC_FAB_PROD_ORD_H_ID: -1, FIN_DIA_N_DIA_TYPE: '', DIA_MOU_CODE: 'Open', QTY_MOU_CODE: 'Kg' };


        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [GetSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function GetSupplierList() {
            vm.supplierOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    $stateParams.pSCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                },
                dataBound: function (e) {
                    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                        vm.form.SCM_SUPPLIER_ID = $stateParams.pSCM_SUPPLIER_ID;
                    }
                }
            };

            return vm.supplierDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.ydRecvChalnGridOption = {
            height: 400,
            sortable: true,
            pageable: true,
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
                { field: "CHALAN_DT", title: "Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                { field: "CHALAN_NO", title: "Challan#", type: "string", width: "15%" },                
                //{ field: "CL_WO_REF_NO", title: "WO Ref#", type: "string", width: "10%" },
                { field: "GATE_PASS_NO", title: "Gate Pass#", type: "string", width: "20%" },
                { field: "REMARKS", title: "Remarks", width: "23%", filterable: false },
                { field: "SUP_TRD_NAME_EN", title: "Party", width: "15%", filterable: false },
                { field: "IS_FINALIZED", title: "Finalized?", width: "10%", filterable: false },
                //{ field: "IS_TRANSFER", title: "Transfer?", width: "10%" },
                //{ field: "SUP_TRD_NAME_EN", title: "Tran Party", width: "15%", filterable: false },
                //{
                //    field: "EVENT_NAME",
                //    title: "Status",
                //    template: function () {
                //        return "<label class='label label-sm label-warning' ng-show='dataItem.RF_ACTN_STATUS_ID<75 && dataItem.RF_ACTN_STATUS_ID!=15'>{{dataItem.EVENT_NAME}}</label> <label class='label label-sm label-danger' ng-show='dataItem.RF_ACTN_STATUS_ID==15'>{{dataItem.EVENT_NAME}}</label> <label class='label label-sm label-success' ng-show='dataItem.RF_ACTN_STATUS_ID==75'>{{dataItem.EVENT_NAME}}</label>";
                //    },
                //    width: "10%"
                //},
                {
                    title: "Action",
                    template: function () {                        
                        return "<button type='button' class='btn btn-xs blue' title='Edit/View' ng-click='vm.editChaln(dataItem)'><i class='fa fa-edit'></i></button>&nbsp;<button type='button' class='btn btn-xs blue' title='Print Challan' ng-click='vm.printDeliveryChallan(dataItem.KNT_SCO_YD_TR_CHL_H_ID)'><i class='fa fa-print'></i></button>";
                    },
                    width: "7%"
                }
            ]
        };

        vm.ydRecvChalnGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "KNT_SCO_YD_TR_CHL_H_ID",
                    fields: {
                        CHALAN_DT: { type: "date", editable: false }
                    }
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/knit/KntScoYdTrChaln/GetScoYdTrChalnList?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || $stateParams.pSCM_SUPPLIER_ID || '');

                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        console.log(res);
                        e.success(res);
                    });
                }
            },
            sort: [{ field: 'CHALAN_DT', dir: 'desc' }]
        });


        vm.getChalnList = function () {
            vm.ydRecvChalnGridDataSource.read();

            $state.go('ScoYdTrChalnList', { pSCM_SUPPLIER_ID: (vm.form.SCM_SUPPLIER_ID || 0) }, { notify: false });
        };
        
        vm.editChaln = function (dataItem) {
            $state.go('ScoYdTrChalnH', { pKNT_SCO_YD_TR_CHL_H_ID: dataItem.KNT_SCO_YD_TR_CHL_H_ID });
        };
        
        vm.printDeliveryChallan = function (pKNT_SCO_YD_TR_CHL_H_ID) {
            if (!pKNT_SCO_YD_TR_CHL_H_ID) { return; }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = {
                REPORT_CODE: 'RPT-3582',
                KNT_SCO_YD_TR_CHL_H_ID: pKNT_SCO_YD_TR_CHL_H_ID
            };

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
        };
        
    }
})();
//=============== End for KntYdRecvChalnListController List =================