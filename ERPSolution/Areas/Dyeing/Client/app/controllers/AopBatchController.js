
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('AopBatchController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', 'formData', '$filter', 'Dialog', AopBatchController]);
    function AopBatchController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, formData, $filter, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = formData.DYE_BT_CARD_H_ID ? formData : { BATCH_REQ_DT: vm.today, SCM_STORE_ID: 10 };
        //vm.form = { BATCH_REQ_DT: vm.today, SCM_STORE_ID: 10 };

        activate()
        vm.showSplash = true;

        function activate() {
            var promise = [getSupplierList(), getAOPDataList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        if ($stateParams.pMC_FAB_PROD_ORD_H_ID) {
            showGrid(0, 0, 0, vm.form.FIRSTDATE, '');
        }

        vm.openChallan = function () {

            $state.go("ScProgramReceive", { pIS_AOP: 'Y' }, { inherit: false, reload: true });
        }

        function getSupplierList() {

            return vm.supplierList = {
                optionLabel: "--- Select Party ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;

                    vm.delvChlnDataSource.read();
                },
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID",
            };

        };

        function getAOPDataList() {
            vm.QcRollLotList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/Dye/AopBatch/GetAopBatchFabrics?pDYE_BT_CARD_H_ID=' + ($stateParams.pDYE_BT_CARD_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    model: {
                        fields: {
                            BYR_ACC_GRP_NAME_EN: { editable: false },
                            STYLE_NO: { editable: false },
                            ORDER_NO: { editable: false },
                            COLOR_NAME_EN: { editable: false },
                            FAB_ITEM_DESC: { editable: false },
                            //SUB_LOT_NO: { editable: false },
                            RCV_FIN_FAB_QTY: { editable: false }
                        }
                    }
                },
                //group: [
                //    { field: "DYE_BATCH_NO", dir: "asc" },
                //]
            });
        }


        vm.gridOptions = {

            pageable: false,
            filterable: false,
            height: '100%',
            scrollable: false,
            editable: true,
            //groupable: true,
            columns: [
                { field: "DF_AOP_BATCH_FAB_ID", hidden: true },
                { field: "DYE_BT_CARD_H_ID", hidden: true },
                { field: "DF_SCO_CHL_RCV_H_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "KNT_STYL_FAB_ITEM_H_ID", hidden: true },
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer", type: "string", editable: false, width: "10%", },
                { field: "STYLE_NO", title: "Style", type: "string", editable: false, width: "10%" },
                { field: "ORDER_NO", title: "Order", type: "string", editable: false, width: "10%" },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", editable: false, width: "5%" },
                //{ field: "DYE_BATCH_NO", title: "Batch No", type: "string", editable: false, hidden: true, width: "1%" },
                //{ field: "SUB_LOT_NO", title: "Lot", type: "string", editable: false, width: "5%" },
                { field: "FAB_ITEM_DESC", title: "Fabric Details", type: "string", editable: false, width: "40%", },
                { field: "RCV_FIN_FAB_QTY", title: "Rcv Qty.", type: "decimal", editable: false, width: "5%" },
                { field: "RCV_ROLL_QTY", title: "No of Roll", type: "decimal", editable: true, width: "5%" },
                { field: "BT_FAB_QTY", title: "Batch Qty", type: "decimal", editable: true, width: "5%" },
                { field: "IS_SELECT", title: "Select", width: "5%", editable: false, template: "<input type='checkbox' ng-model='dataItem.IS_SELECT' />" },
            ]
        };


        vm.delvChlnOptions = {
            height: '200px',
            scrollable: {
                virtual: true,
                //scrollable:true
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
                {
                    headerTemplate: "<input type='checkbox' ng-model='vm.isSelect' ng-click='vm.selectAll(vm.isSelect,0)'  ng-true-value='\"Y\"' ng-false-value='\"N\"' > <i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Select?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Select?' ng-model='dataItem.IS_SELECT' ng-true-value='\"Y\"' ng-false-value='\"N\"' >";
                    },
                    width: "20%"
                },
                { field: "CHALAN_DT", title: "Date", format: "{0:dd-MM-yyyy}", width: "40%", editable: false, filterable: false },
                { field: "CHALAN_NO", title: "Challan#", width: "40%", editable: false, filterable: true }

            ]
        };


        vm.delvChlnDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            pageSize: 100,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/Dye/DfScProgramBill/GetRcvList4AOP';
                    url += '?pageNo=' + params.page + '&pageSize=' + params.pageSize + '&pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0) + '&pDYE_BT_CARD_H_ID=' + (vm.form.DYE_BT_CARD_H_ID || 0);

                    url += DyeingDataService.kFilterStr2QueryParam(params.filter);

                    console.log(url);

                    DyeingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "DF_SCO_CHL_RCV_H_ID",
                    fields: {
                        CHALAN_DT: { type: "date", filterable: false },
                        CHALAN_NO: { type: "string", filterable: false }
                    }
                }
            }
        });


        vm.includeChallan = function () {
            var dataList = _.filter(vm.delvChlnDataSource.data(), function (x) { return x.IS_SELECT === 'Y'; });
            var billData = vm.QcRollLotList;
            //for (var i = 0; i < data.length; i++) {
            //    var obj = data[0];
            //    vm.QcRollLotList.insert(0, obj.fabList);
            //}
            angular.forEach(dataList, function (val, key) {

                if (val['IS_SELECT'] == 'Y') {
                    var chlnData = 0;
                    if (billData.length > 0) {
                        var chlnData = _.filter(billData, function (ob) {
                            return (ob.DF_SCO_CHL_RCV_H_ID || 0) == val['DF_SCO_CHL_RCV_H_ID'];
                        }).length;
                    }
                    if (chlnData < 1) {

                        angular.forEach(val['fabList'], function (val1, key1) {
                            console.log(val1);
                            console.log(val);

                            val1['BYR_ACC_GRP_NAME_EN'] = val1['BYR_ACC_GRP_NAME_EN'];
                            val1['STYLE_NO'] = val1['STYLE_NO'];
                            val1['ORDER_NO'] = val1['ORDER_NO'];
                            val1['COLOR_NAME_EN'] = val1['COLOR_NAME_EN'];
                            val1['FAB_ITEM_DESC'] = val1['FAB_ITEM_DESC'];

                            //val1['SUB_LOT_NO'] = val1['SUB_LOT_NO'];
                            val1['DF_SCO_CHL_RCV_H_ID'] = val1['DF_SCO_CHL_RCV_H_ID'];
                            val1['RCV_FIN_FAB_QTY'] = val1['RCV_FIN_FAB_QTY'];
                            val1['RCV_ROLL_QTY'] = val1['RCV_ROLL_QTY'];
                            val1['BT_FAB_QTY'] = val1['BT_FAB_QTY'];

                            vm.QcRollLotList.insert(0, val1);
                        });
                    }
                }
            });
        }

        vm.removeRow = function (dataItem) {
            var data = angular.copy(dataItem);

            var dataList = vm.QcRollLotList.data();
            var length = dataList.length;

            var vMsg = 'Do you want to remove items of the challan# ' + dataItem.CHALAN_NO;

            console.log(dataList);

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                var item, i;
                for (i = length - 1; i >= 0; i--) {

                    item = dataList[i];
                    if (data.CHALAN_NO == item['CHALAN_NO']) {
                        vm.QcRollLotList.remove(item);
                    }

                }
            });
        };

        function getStyleExtList(pMC_BYR_ACC_GRP_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE) {

            vm.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_GRP_ID=" + pMC_BYR_ACC_GRP_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=" + (vm.form.RF_FAB_PROD_CAT_ID || '');
                        url += "&pFIRSTDATE=" + (pFIRSTDATE || null);
                        url += "&pLASTDATE=" + (pLASTDATE || null);

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pSTYLE_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pSTYLE_NO';
                        }
                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };


        function getFabOederByOh(pMC_BYR_ACC_GRP_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE, pMC_FAB_PROD_ORD_H_ID) {
            vm.FabOederByOhDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh?pMC_BYR_ACC_GRP_ID=" + pMC_BYR_ACC_GRP_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=" + (vm.form.RF_FAB_PROD_CAT_ID || '');
                        url += "&pFIRSTDATE=" + (pFIRSTDATE || null);
                        url += "&pLASTDATE=" + (pLASTDATE || null);

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pORDER_NO_LST=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pORDER_NO_LST';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_ID=' + pMC_FAB_PROD_ORD_H_ID;

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };

        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME||"" #</p></span>';
        $scope.valueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME||"" #)</h5></span>';

        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.FAB_PROD_CAT_SNAME||"" #</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.FAB_PROD_CAT_SNAME||"" #)</h5></span>';


        vm.buyerAcGrpList = {
            optionLabel: "--- Buyer A/C Group ---",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            select: function (e) {
                var item = this.dataItem(e.item);
                if (item.MC_BYR_ACC_GRP_ID.length != 0) {
                    getStyleExtList(item.MC_BYR_ACC_GRP_ID, null);
                    getSelectMonth(item.MC_BYR_ACC_GRP_ID);
                    getFabOederByOh(item.MC_BYR_ACC_GRP_ID, null, null, null, null);
                }
            },
            dataTextField: "BYR_ACC_GRP_NAME_EN",
            dataValueField: "MC_BYR_ACC_GRP_ID"
        };



        $scope.BATCH_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.BATCH_REQ_DT_LNopened = true;
        }

        vm.reset = function () {
            $state.go($state.current, {}, { inherit: false, reload: true });
        };


        vm.printRDLC = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-4032';

            vm.form["DYE_BATCH_NO"] = dataItem.DYE_BATCH_NO;
            vm.form["DYE_BT_CARD_H_ID"] = dataItem.DYE_BT_CARD_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Dye/DyeReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = vm.form;

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

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                console.log(data);

                var iList = _.filter(vm.QcRollLotList.data(), function (x) { return x.IS_SELECT == true });

                var _isudate = $filter('date')(data.BATCH_REQ_DT, vm.dtFormat);
                data["BATCH_REQ_DT"] = _isudate;
                data["ACT_BATCH_QTY"] = 0;
                data["MC_FAB_PROD_ORD_H_ID"] = 0;
                data["XML_FAB"] = DyeingDataService.xmlStringShort(iList.map(function (o) {
                    return {
                        DF_AOP_BATCH_FAB_ID: o.DF_AOP_BATCH_FAB_ID == null ? 0 : o.DF_AOP_BATCH_FAB_ID,
                        DYE_BT_CARD_H_ID: vm.form.DYE_BT_CARD_H_ID,
                        DF_SCO_CHL_RCV_H_ID: o.DF_SCO_CHL_RCV_H_ID == null ? 0 : o.DF_SCO_CHL_RCV_H_ID,
                        KNT_STYL_FAB_ITEM_H_ID: o.KNT_STYL_FAB_ITEM_H_ID == null ? 0 : o.KNT_STYL_FAB_ITEM_H_ID,
                        BATCH_REQ_DT: _isudate,
                        RCV_ROLL_QTY: o.RCV_ROLL_QTY == null ? 0 : o.RCV_ROLL_QTY,
                        BT_FAB_QTY: o.BT_FAB_QTY == null ? 0 : o.BT_FAB_QTY,
                        QTY_MOU_ID: 3
                    }
                }));

                return DyeingDataService.saveDataByUrl(data, '/AopBatch/BatchSave').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        if (parseInt(res.data.OPDYE_BT_CARD_H_ID) > 0) {
                            $state.go($state.current, { pDYE_BT_CARD_H_ID: res.data.OPDYE_BT_CARD_H_ID }, { inherit: false, reload: true });
                        }
                    }
                });
            }
        };
    }


})();


