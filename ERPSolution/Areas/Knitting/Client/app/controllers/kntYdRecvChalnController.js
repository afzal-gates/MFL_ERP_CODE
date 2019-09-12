////////// Start KntYdRecvChalnHController Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntYdRecvChalnHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', 'ydRecvChalnHdrData', KntYdRecvChalnHController]);
    function KntYdRecvChalnHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog, ydRecvChalnHdrData) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
        
        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        var key = 'KNT_YD_RCV_CHL_H_ID';
        vm.today = new Date();

        vm.form = ydRecvChalnHdrData[key] ? ydRecvChalnHdrData : {
            KNT_YD_RCV_CHL_H_ID: 0, KNT_YD_PRG_H_ID: null, MC_FAB_PROD_ORD_H_ID: null, CHALAN_DT: $filter('date')(vm.today, vm.dtFormat), IS_TRANSFER: 'N'
        };
                         

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getStoreList(), getSupplierList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;                
            });
        }


        vm.chlnDtlDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {

                    var url = '/api/Knit/KntYdRecvChaln/GetYdRecvChalnDtl?pKNT_YD_PRG_H_ID=' + (vm.form.KNT_YD_PRG_H_ID || '') + '&pMC_FAB_PROD_ORD_H_ID=' + (vm.form.MC_FAB_PROD_ORD_H_ID || '') + '&pKNT_YD_RCV_CHL_H_ID=' + (vm.form.KNT_YD_RCV_CHL_H_ID || $stateParams.pKNT_YD_RCV_CHL_H_ID || 0);

                    console.log(url);

                    //e.success([]);

                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            aggregate: [
                { field: "ADV_RCV_QTY", aggregate: "sum" },
                { field: "TOT_RCV_YD_QTY", aggregate: "sum" },
                { field: "RCV_YD_QTY", aggregate: "sum" },
                { field: "RCV_CONE_QTY", aggregate: "sum" },
                { field: "RCV_PACK_QTY", aggregate: "sum" },
                { field: "RCV_GYRN_QTY", aggregate: "sum" }
            ],
            schema: {
                model: {
                    id: "KNT_YD_RCV_CHL_D_ID",
                    fields: {
                        ADV_RCV_QTY: { type: "number", filterable: false },
                        TOT_RCV_YD_QTY: { type: "number", filterable: false },
                        RCV_YD_QTY: { type: "number", filterable: false },
                        RCV_CONE_QTY: { type: "number", filterable: false },
                        RCV_PACK_QTY: { type: "number", filterable: false },
                        RCV_GYRN_QTY: { type: "number", filterable: false }
                    }
                }
            }
        });

       
        vm.fabProdOrderDs = {
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);

                    var url = "/KnitYdProgram/getOrderListByProgram?pMC_FAB_PROD_ORD_H_LST=" + (vm.form.MC_FAB_PROD_ORD_H_ID||0);                   

                    return KnittingDataService.getDataByUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            }            
        };

        vm.onFabProdOrderBound = function (item, e) {
            var ds = e.sender.dataSource.data();
            if (ds.length == 1) {
                e.sender.value(ds[0].MC_FAB_PROD_ORD_H_ID);
                item['MC_FAB_PROD_ORD_H_ID'] = ds[0].MC_FAB_PROD_ORD_H_ID;

                vm.chlnDtlDataSource.read();               
            }           
        };

        vm.onFabProdOrderChange = function (item, e) {
            var ordRef = e.sender.dataItem(e.sender.item);
                        
            vm.form.MC_FAB_PROD_ORD_H_ID = ordRef.MC_FAB_PROD_ORD_H_ID;
            vm.chlnDtlDataSource.read();

        };

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
                dataValueField: "SCM_SUPPLIER_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);
                //    console.log(item);
                   
                //    vm.form.TR_PARTY_ID = item.SCM_SUPPLIER_ID;
                //},
                //dataBound: function (e) {
                //    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                //        vm.form.TR_PARTY_ID = $stateParams.pSCM_SUPPLIER_ID;
                //    }
                //}
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
        
        function getStoreList() {
            vm.storeOption = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,                
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };

            return vm.storeDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=2';
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        $scope.KntYdProgramDs = {
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);

                    var url = "/KnitYdProgram/getYdProgramDropDownData";

                    if (params.filter) {
                        url += '?pPRG_REF_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                    } else {
                        url += '?pPRG_REF_NO';
                        if (ydRecvChalnHdrData.KNT_YD_PRG_H_ID) {
                            url += '&pKNT_YD_PRG_H_ID=' + (Object.keys(ydRecvChalnHdrData).length > 0 ? ydRecvChalnHdrData.KNT_YD_PRG_H_ID : '');
                        }
                    }

                    return KnittingDataService.getDataByUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            serverFiltering: true
        };

        vm.onChaneProgramRef = function (e) {
            var PrgRef = e.sender.dataItem(e.sender.item);
            if (PrgRef.KNT_YD_PRG_H_ID) {
                vm.form['SCM_SUPPLIER_ID'] = PrgRef.SCM_SUPPLIER_ID;
                vm.form['SUP_TRD_NAME_EN'] = PrgRef.SUP_TRD_NAME_EN;

                vm.chlnDtlDataSource.read();

                //KnittingDataService.getDataByUrl('/KnitYdProgram/getOrderListByProgram?pMC_FAB_PROD_ORD_H_LST=' + PrgRef.MC_FAB_PROD_ORD_H_LST).then(function (res) {
                //    vm.fabProdOrderDs = new kendo.data.DataSource({
                //        data: res
                //    });
                //});

            }
            else {
                vm.form['SCM_SUPPLIER_ID'] = '';
                vm.form['SUP_TRD_NAME_EN'] = '';
                vm.fabProdOrderDs = new kendo.data.DataSource({
                    data: []
                });
            }


        };

        //$scope.$watch('vm.form', function (newVal, oldVal) {
        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            $scope.form = vm.form;
        //        }
        //    }
        //}, true);

        vm.chlnDtlOptions = {
            height: 350,
            sortable: true,
            pageable: false,
            editable: false,
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
                { field: "MC_ORDER_NO_LST", title: "Order#", type: "string", width: "15%", editable: false, filterable: false },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "15%", editable: false, filterable: false },
                { field: "YR_SPEC_DESC", title: "Yarn Item", type: "string", width: "15%", editable: false, filterable: false },
                { field: "YRN_LOT_NO", title: "Lot#", type: "string", width: "10%", editable: false, filterable: false },
                { field: "", title: "Issue Qty", type: "number", width: "6%", editable: false, filterable: false },
                //{ field: "ADV_RCV_QTY", title: "Adv Recv Qty(Kg)", type: "number", width: "11%", editable: false, filterable: false, footerTemplate: "#=kendo.toString(sum, '0.00')#" },
                { field: "TOT_RCV_YD_QTY", title: "Tot Rcv Qty(Kg)", type: "number", width: "7%", editable: false, filterable: false, footerTemplate: "#=kendo.toString(sum, '0.00')#" },
                {
                    title: "Batch#",
                    field: "YD_BATCH_NO",
                    template: function () {
                        return "<ng-form name='frmYD_BATCH_NO'><input type='text' class='form-control' name='YD_BATCH_NO' ng-model='dataItem.YD_BATCH_NO' ng-disabled='vm.form.IS_FINALIZED==\"Y\"' ng-required='dataItem.RCV_YD_QTY>0' ng-style='(frmYD_BATCH_NO.YD_BATCH_NO.$error.required)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "8%",
                    filterable: false
                },
                {
                    title: "Pack Qty (Bag)",
                    field: "RCV_PACK_QTY",
                    template: function () {
                        return "<ng-form name='frmRCV_PACK_QTY'><input type='number' class='form-control' name='RCV_PACK_QTY' ng-model='dataItem.RCV_PACK_QTY' min='0' ng-disabled='vm.form.IS_FINALIZED==\"Y\"' ng-style='(frmRCV_PACK_QTY.RCV_PACK_QTY.$error.min)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "7%",
                    filterable: false,
                    footerTemplate: "#=kendo.toString(sum, '0.00')#"
                },
                {
                    title: "Cone Qty",
                    field: "RCV_CONE_QTY",
                    template: function () {
                        return "<ng-form name='frmRCV_CONE_QTY'><input type='number' class='form-control' name='RCV_CONE_QTY' ng-model='dataItem.RCV_CONE_QTY' min='0' ng-disabled='vm.form.IS_FINALIZED==\"Y\"' ng-style='(frmRCV_CONE_QTY.RCV_CONE_QTY.$error.min)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "8%",
                    filterable: false,
                    footerTemplate: "#=kendo.toString(sum, '0.00')#"
                },
                {
                    title: "Recv Qty(Kg)",
                    field: "RCV_YD_QTY",
                    template: function () {
                        return "<ng-form name='frmRCV_YD_QTY'><input type='number' class='form-control' name='RCV_YD_QTY' ng-model='dataItem.RCV_YD_QTY' min='0' ng-disabled='vm.form.IS_FINALIZED==\"Y\"' ng-style='(frmRCV_YD_QTY.RCV_YD_QTY.$error.min)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "8%",
                    filterable: false,
                    footerTemplate: "#=kendo.toString(sum, '0.00')#"
                },
                {
                    title: "GYrn Qty(Kg)",
                    field: "RCV_GYRN_QTY",
                    template: function () {
                        return "<ng-form name='frmRCV_GYRN_QTY'><input type='number' class='form-control' name='RCV_GYRN_QTY' ng-model='dataItem.RCV_GYRN_QTY' min='0' ng-disabled='vm.form.IS_FINALIZED==\"Y\"' ng-style='(frmRCV_GYRN_QTY.RCV_GYRN_QTY.$error.min)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "8%",
                    filterable: false,
                    footerTemplate: "#=kendo.toString(sum, '0.00')#"
                }
                //{
                //    title: "Remarks",
                //    field: "REMARKS",
                //    template: function () {
                //        return "<ng-form name='frmREMARKS'><input type='text' class='form-control' name='REMARKS' ng-model='dataItem.REMARKS' ng-disabled='vm.form.IS_FINALIZED==\"Y\"' /></ng-form>";
                //    },
                //    width: "10%",
                //    filterable: false
                //}
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


                submitData.KNT_YD_RCV_CHL_D_XML = KnittingDataService.xmlStringShort(chlnDtlData.map(function (ob) {
                    return {
                        KNT_YD_RCV_CHL_D_ID: ob.KNT_YD_RCV_CHL_D_ID, KNT_YD_RCV_CHL_H_ID: ob.KNT_YD_RCV_CHL_H_ID, MC_FAB_PROD_ORD_H_ID: ob.MC_FAB_PROD_ORD_H_ID, KNT_YDP_D_COL_ID: ob.KNT_YDP_D_COL_ID,
                        YARN_ITEM_ID: ob.YARN_ITEM_ID, KNT_YRN_LOT_ID: ob.KNT_YRN_LOT_ID, RCV_GYRN_QTY: ob.RCV_GYRN_QTY, RCV_YD_QTY: ob.RCV_YD_QTY, YD_BATCH_NO: ob.YD_BATCH_NO,
                        RCV_CONE_QTY: ob.RCV_CONE_QTY, RCV_PACK_QTY: ob.RCV_PACK_QTY, REMARKS: ob.REMARKS
                    };
                }));

                console.log(submitData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + token },
                    url: '/api/Knit/KntYdRecvChaln/BatchSave',
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

                            vm.form.KNT_YD_RCV_CHL_H_ID = res['data'].PKNT_YD_RCV_CHL_H_ID_RTN;
                            if (pIS_FINALIZED == 'Y') {
                                vm.form.IS_FINALIZED = 'Y';
                            }

                            vm.chlnDtlDataSource.read();

                            $state.go('YdRecvChalnH', { pKNT_YD_RCV_CHL_H_ID: res['data'].PKNT_YD_RCV_CHL_H_ID_RTN }, { notify: false });
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

    }

})();
////////// End KntYdRecvChalnHController Controller










//=============== Start for KntYdRecvChalnListController List =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntYdRecvChalnListController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', KntYdRecvChalnListController]);
    function KntYdRecvChalnListController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, commonDataService) {

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
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=556').then(function (res) {
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
                { field: "CHALAN_NO", title: "Challan#", type: "string", width: "10%" },                
                { field: "CL_WO_REF_NO", title: "WO Ref#", type: "string", width: "10%" },
                { field: "GATE_PASS_NO", title: "Gate Pass#", type: "string", width: "10%" },
                { field: "REMARKS", title: "Remarks", width: "18%", filterable: false },
                { field: "SUP_TRD_NAME_EN", title: "Party", width: "15%", filterable: false },
                { field: "IS_FINALIZED", title: "Finalized?", width: "10%", filterable: false },
                { field: "IS_TRANSFER", title: "Transfer?", width: "10%" },
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
                        return "<button type='button' class='btn btn-xs blue' title='Edit/View' ng-click='vm.editChaln(dataItem)'><i class='fa fa-edit'></i></button>";
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
                    id: "KNT_YD_RCV_CHL_H_ID",
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
                    var url = '/api/knit/KntYdRecvChaln/GetYdRecvChalnList?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || $stateParams.pSCM_SUPPLIER_ID || 0);

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

            $state.go('YdRecvChalnList', { pSCM_SUPPLIER_ID: (vm.form.SCM_SUPPLIER_ID || 0) }, { notify: false });
        };
        
        vm.editChaln = function (dataItem) {
            $state.go('YdRecvChalnH', { pKNT_YD_RCV_CHL_H_ID: dataItem.KNT_YD_RCV_CHL_H_ID });
        };
        
        
    }
})();
//=============== End for KntYdRecvChalnListController List =================