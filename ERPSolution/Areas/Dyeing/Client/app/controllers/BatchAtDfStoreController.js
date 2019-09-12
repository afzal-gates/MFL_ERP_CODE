
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('BatchAtDfStoreController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', '$filter', 'Dialog', BatchAtDfStoreController]);
    function BatchAtDfStoreController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, $filter, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = { RECEIVE_DT: vm.today, SCM_STORE_ID: 10 };

        activate()
        vm.showSplash = true;

        function activate() {
            var promise = [GetReqSourceMainList(), GetReqSourceList(), getUserData(), getPendingSubLot()];
            //getSelectMonth(0, 0), getStyleExtList(null, null, null, null), getFabOederByOh(null, null, null, null, ''), 
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        if ($stateParams.pMC_FAB_PROD_ORD_H_ID) {
            showGrid(0, 0, 0, vm.form.FIRSTDATE, '');
        }

        function getPendingSubLot() {
            vm.QcRollLotList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/DfFinishFabric/SelectPendingLotRcv2DfStore').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    model: {
                        fields: {
                            YARN_SPEC: { editable: false },
                            GROUP_LST: { editable: false },
                            DYE_BATCH_NO: { editable: false },
                            SUB_LOT_NO: { editable: false },
                            LOT_QTY: { editable: false },
                            FINIS_QTY: { editable: false },
                            FAB_STR_POS: { editable: false },
                            ACT_NO_ROLL: { editable: true },
                            ACT_FINIS_QTY: { editable: true },
                            IS_SELECT: { editable: false },
                        }
                    }
                },
                group: [
                    { field: "GROUP_LST", dir: "asc" },
                ]
            });

        }


        vm.gridOptions = {

            pageable: false,
            filterable: false,
            height: '100%',
            scrollable: false,
            editable: true,
            groupable: true,
            columns: [
                { field: "LK_DIA_TYPE_ID", hidden: true },
                { field: "LK_FBR_GRP_ID", hidden: true },
                { field: "RF_FAB_TYPE_ID", hidden: true },
                { field: "RF_FIB_COMP_ID", hidden: true },
                //{ field: "BYR_ACC_GRP_NAME_EN", title: "Buyer", type: "string", editable: false, width: "10%", hidden: true },
                //{ field: "STYLE_NO", title: "Style", type: "string", editable: false, width: "10%", hidden: true },
                //{ field: "ORDER_NO", title: "Order", type: "string", editable: false, width: "5%", hidden: true },
                //{ field: "COLOR_NAME_EN", title: "Color", type: "string", editable: false, width: "5%", hidden: true },
                {
                    field: "GROUP_LST", title: "Order Info", type: "string", editable: false, width: "5%", hidden: true
                },
                {
                    field: "DYE_BATCH_NO", title: "Batch No", type: "string", editable: false, width: "10%",
                },
                //{ field: "RQD_GSM", title: "Req. GSM", type: "string", editable: false, width: "7%" },
                //{ field: "RQD_QTY", title: "Grey Qty.", type: "decimal", editable: false, width: "7%" },
                { field: "SUB_LOT_NO", title: "Sub-Lot", type: "string", editable: false, width: "5%" },
                { field: "YARN_SPEC", title: "Fabric Details", type: "string", editable: false, width: "30%", },
                { field: "LOT_QTY", title: "Lot Qty.", type: "decimal", editable: false, width: "10%" },
                { field: "FINIS_QTY", title: "Finish Qty", type: "decimal", editable: false, width: "10%" },
                { field: "FAB_STR_POS", title: "Fabric Positionk", type: "string", editable: false, width: "10%" },
                { field: "ACT_NO_ROLL", title: "No of Roll", type: "decimal", editable: true, width: "5%" },
                { field: "ACT_FINIS_QTY", title: "Fab. Qty", type: "decimal", editable: true, width: "5%" },
                { field: "IS_SELECT", title: "Select", width: "5%", editable: false, template: "<input type='checkbox' ng-model='dataItem.IS_SELECT' />" },

            ]
        
        };

        vm.SearchPendingSubLot = function () {
            DyeingDataService.getDataByFullUrl('/api/dye/DfFinishFabric/SelectPendingLotRcv2DfStore?pMC_BYR_ACC_GRP_ID=' + (vm.form.MC_BYR_ACC_GRP_ID || null) + '&pMC_FAB_PROD_ORD_H_ID=' + (vm.form.MC_FAB_PROD_ORD_H_ID || null) + '&pRF_FAB_PROD_CAT_ID=' + (vm.form.RF_FAB_PROD_CAT_ID || null) + '&pMONTHOF=' + (vm.form.MONTHOF || '')).then(function (res) {
                vm.QcRollLotList = res;
            });
        }

        vm.productionTypeList = {
            optionLabel: "-- Select Type --",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/common/GetFabProdCat').then(function (res) {
                            e.success(_.findByValues(res, 'RF_FAB_PROD_CAT_ID', '1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17'));
                        });
                    }
                }
            },
            change: function (e) {
                var itmTyp = this.dataItem(e.item);

                if (itmTyp.RF_FAB_PROD_CAT_ID) {
                    getSelectMonth(null, itmTyp.RF_FAB_PROD_CAT_ID);
                    getStyleExtList(null, null, null, null)
                    getFabOederByOh(null, itmTyp.RF_FAB_PROD_CAT_ID, null, null, '')
                } else {
                    getSelectMonth(0, 0);
                    getFabOederByOh(null, null, null, null, '');
                    getStyleExtList(null, null, null, null);
                }
            },
            dataTextField: "FAB_PROD_CAT_SNAME",
            dataValueField: "RF_FAB_PROD_CAT_ID"
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

        function getSelectMonth(MC_BYR_ACC_GRP_ID, RF_FAB_PROD_CAT_ID) {
            return DyeingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/' + (MC_BYR_ACC_GRP_ID || 0) + '/0/' + (RF_FAB_PROD_CAT_ID || 0)).then(function (res) {
                vm.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        };

        vm.onChangeShipMonth = function (e) {
            var itmShipMonth = e.sender.dataItem(e.sender.item);

            if (itmShipMonth.MONTHOF) {
                vm.form.FIRSTDATE = itmShipMonth.FIRSTDATE.split('T')[0];
                vm.form.LASTDATE = itmShipMonth.LASTDATE.split('T')[0];
                getStyleExtList((vm.form.MC_BYR_ACC_GRP_ID || null), (vm.form.RF_FAB_PROD_CAT_ID || null), itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE)
                getFabOederByOh(vm.form.MC_BYR_ACC_GRP_ID, (vm.form.RF_FAB_PROD_CAT_ID || null), itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE, '')


            } else {
                getStyleExtList((vm.form.MC_BYR_ACC_GRP_ID || null), (vm.form.RF_FAB_PROD_CAT_ID || null), null, null);
                vm.form.FIRSTDATE = null;
                vm.form.LASTDATE = null;
                getFabOederByOh(vm.form.MC_BYR_ACC_GRP_ID, (vm.form.RF_FAB_PROD_CAT_ID || null), null, null, '')
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



        $scope.RECEIVE_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.RECEIVE_DT_LNopened = true;
        }

        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getUserDatalist().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    vm.IsChange = false;
                },

                height: 400,
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };
        }



        vm.reset = function () {
            $state.go($state.current, {}, { inherit: false, reload: true });

        };


        function GetReqSourceList() {

            return vm.reqSourceList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=5&pSC_USER_ID=' + cur_user_id).then(function (res) {
                                var list = _.filter(res, function (x) { return x.SCM_STORE_ID == 25 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };

        };


        function GetReqSourceMainList() {
            return vm.reqSourceMainList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=5&pSC_USER_ID=' + cur_user_id).then(function (res) {

                                var list = _.filter(res, function (x) { return x.SCM_STORE_ID == 10 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                console.log(data);
                console.log(vm.QcRollLotList);

                //var iList = _.filter(vm.QcRollLotList, function (x) { return x.IS_SELECT == 'Y' });
                var iList = _.filter(vm.QcRollLotList.data(), function (x) { return x.IS_SELECT == true });

                var _isudate = $filter('date')(data.RECEIVE_DT, vm.dtFormat);
                data["RECEIVE_DT"] = _isudate;

                data["XML_LOT"] = DyeingDataService.xmlStringShort(iList.map(function (o) {
                    return {
                        DF_BT_MVM_DFIN_STR_ID: o.DF_BT_MVM_DFIN_STR_ID == null ? 0 : o.DF_BT_MVM_DFIN_STR_ID,
                        SCM_STORE_ID: vm.form.SCM_STORE_ID,
                        NEXT_SCM_STORE_ID: vm.form.pNEXT_SCM_STORE_ID,
                        DF_BT_SUB_LOT_ID: o.DF_BT_SUB_LOT_ID == null ? 0 : o.DF_BT_SUB_LOT_ID,
                        RECEIVE_DT: _isudate,
                        ISSUE_DT: _isudate,
                        FAB_STR_POS: o.FAB_STR_POS,
                        ACT_NO_ROLL: o.ACT_NO_ROLL == null ? 0 : o.ACT_NO_ROLL,
                        ACT_FINIS_QTY: o.ACT_FINIS_QTY == null ? 0 : o.ACT_FINIS_QTY,
                        CUT_STR_ISS_AUTH_BY: o.CUT_STR_ISS_AUTH_BY == null ? 1 : cur_user_id,
                        IS_FINALIZED: 'Y'
                    }
                }));

                return DyeingDataService.saveDataByUrl(data, '/DfFinishFabric/SaveDfStore').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        $state.go($state.current, {}, { inherit: false, reload: true });
                    }
                });
            }
        };
    }


})();

