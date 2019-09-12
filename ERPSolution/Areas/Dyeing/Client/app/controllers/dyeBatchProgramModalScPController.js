
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeBatchProgramModalScPController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$modalInstance', '$scope', 'formData', DyeBatchProgramModalScPController]);
    function DyeBatchProgramModalScPController($q, config, DyeingDataService, $stateParams, $modalInstance, $scope, formData) {

        var ObjFromXml = [];
        getSelectMonth(0, 0);
        var cur_tab = 'KNIT';
        getDfProcessTypeData();



        $scope.formData = formData;

        function getStyleExtList(pMC_BYR_ACC_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE) {
            $scope.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=1,2,3,5,6";
                        url += "&pFIRSTDATE=" + (pFIRSTDATE||null);
                        url += "&pLASTDATE=" + (pLASTDATE|| null);

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pSTYLE_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pSTYLE_NO';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_ID=' + (formData.MC_FAB_PROD_ORD_H_ID || null);
                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };

        function getFabOederByOh(pMC_BYR_ACC_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE, pMC_FAB_PROD_ORD_H_ID) {
            $scope.FabOederByOhDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=1,2,3,5,6";
                        url += "&pFIRSTDATE=" + pFIRSTDATE;
                        url += "&pLASTDATE=" + pLASTDATE;

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

        function getDfProcessTypeData() {
            return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDFProcessData?pDYE_BATCH_PLAN_ID').then(function (res) {
                $scope.df_data = res;
            });
        };

        function getFabOederByOhMerge(pMC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, pRF_FAB_TYPE_ID, pRF_FAB_PROD_CAT_ID) {
            $scope.FabOederByOhDsMerge = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderDataOhMerge?pFAB_COLOR_ID=" + pFAB_COLOR_ID;
                        url += "&pRF_FAB_TYPE_ID=" + pRF_FAB_TYPE_ID;

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pORDER_NO_LST=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pORDER_NO_LST';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_ID=' + pMC_FAB_PROD_ORD_H_ID;
                        url += '&pRF_FAB_PROD_CAT_ID=' + pRF_FAB_PROD_CAT_ID;

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };

        getFabOederByOh(null,formData.RF_FAB_PROD_CAT_ID, null, null, null);
        getStyleExtList(null, formData.RF_FAB_PROD_CAT_ID, null, null);
        $scope.form = {
            NO_OF_BATCH: 1,
            INTERVAL: 10,
            MC_FAB_PROD_ORD_H_ID_LST: [-1],
            DYE_BATCH_PLAN_ID: -1,
            LOAD_TIME : new Date(),
            DYE_SC_PRG_ISS_ID: formData.DYE_SC_PRG_ISS_ID,
            DYE_MACHINE_ID: 25,
            IS_ADD_OTH_ORD: 'N',
            DURATION: 1
        };

        $scope.colorDs = {
            transport: {
                read: function (e) {
                    var url = "/api/mrc/ColourMaster/SelectAll?pOption=3000";
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    if (params.filter) {
                        url += '&pCOLOR_NAME_EN=' + params.filter.replace(/'/g, '').split('~')[2];
                    } else {
                        url += '&pCOLOR_NAME_EN';
                    }

                    return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            serverFiltering: true
        }

        $scope.onChangeOtherOrder = function (data) {
            if (data.IS_ADD_OTH_ORD == 'Y') {
                getFabOederByOhMerge(data.MC_FAB_PROD_ORD_H_ID, data.FAB_COLOR_ID, data.RF_FAB_TYPE_ID, formData.RF_FAB_PROD_CAT_ID);

            } else {
                $scope.form.MC_FAB_PROD_ORD_H_ID_LST = [undefined];
            }
        };

        $scope.$watch('form.MC_FAB_PROD_ORD_H_ID_LST', function (newVal, oldVal) {

            if ($scope.form.MC_FAB_PROD_ORD_H_ID) {
                refreshRequirement($scope.form.MC_FAB_PROD_ORD_H_ID + ',' + newVal.filter(function (res) { return res !== undefined }).join(','), $scope.form.FAB_COLOR_ID, $scope.form.RF_FAB_TYPE_ID, $scope.form.RQD_PRD_QTY);
            }

        }, true);

        $scope.$watch('form.RQD_PRD_QTY', function (newVal, oldVal) {
            if (newVal) {
                refreshRequirement($scope.form.MC_FAB_PROD_ORD_H_ID + ',' + $scope.form.MC_FAB_PROD_ORD_H_ID_LST.filter(function (res) { return res !== undefined }).join(','), $scope.form.FAB_COLOR_ID, $scope.form.RF_FAB_TYPE_ID, newVal);
            }

        });

        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };


        $scope.ColourTypelist = {
            optionLabel: "-- Col Type--",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                       return DyeingDataService.LookupListData(74).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "LK_DATA_NAME_EN",
            dataValueField: "LOOKUP_DATA_ID"
        };


        function getWovenFabricAvailability(pMC_FAB_PROD_ORD_H_LST, pMC_COLOR_ID, pINV_ITEM_CAT_ID, host) {
            if (!pMC_FAB_PROD_ORD_H_LST)
                return;

            var url = '/DyeBatchPlan/getProgAvailabilityWovenFabScP';
            url += '?pMC_FAB_PROD_ORD_H_LST=' + pMC_FAB_PROD_ORD_H_LST;
            url += '&pMC_COLOR_ID=' + pMC_COLOR_ID;
            url += '&pINV_ITEM_CAT_ID=' + pINV_ITEM_CAT_ID;
            DyeingDataService.getDataByUrl(url).then(function (res) {
                host['requirements'] = res;
            });

        };
        function getColorDs(pMC_FAB_PROD_ORD_H_LST, pINV_ITEM_CAT_ID, host) {
            if (!pMC_FAB_PROD_ORD_H_LST)
                return;

            var url = '/DyeBatchPlan/getColorList';
            url += '?pMC_FAB_PROD_ORD_H_LST=' + pMC_FAB_PROD_ORD_H_LST;
            url += '&pINV_ITEM_CAT_ID=' + pINV_ITEM_CAT_ID;
            DyeingDataService.getDataByUrl(url).then(function (res) {
                host['color_list_ds'] = new kendo.data.DataSource({
                    data: res
                });
            });

        };
        $scope.onChangeColor = function (e, cat_id, host) {
            var col = e.sender.dataItem(e.sender.item);
            if (!col.MC_COLOR_ID)
                return;
            getWovenFabricAvailability(host.MC_FAB_PROD_ORD_H_ID, host.FAB_COLOR_ID, cat_id, host);
        };
        $scope.onSelect = function (TabType) {

            switch (TabType) {
                case 'WOVEN':
                    cur_tab = 'WOVEN';
                    $scope.formWoven = angular.copy($scope.form);
                    getColorDs($scope.form.MC_FAB_PROD_ORD_H_ID, 35, $scope.formWoven);
                    $scope.formWoven['requirements'] = [];

                    break;
                case 'TRIMS':
                    cur_tab = 'TRIMS';
                    $scope.formTrims = angular.copy($scope.form);
                    //getWovenFabricAvailability($scope.form.MC_FAB_PROD_ORD_H_ID, $scope.form.FAB_COLOR_ID, 7, $scope.formTrims);
                    getColorDs($scope.form.MC_FAB_PROD_ORD_H_ID, 7, $scope.formTrims);
                    $scope.formTrims['requirements'] = [];

                    break;
                default:
            }
        };

        $scope.findGridData = function (dataOri) {

            var Defdata = {
                MC_BYR_ACC_ID: null,
                FIRSTDATE: null,
                LASTDATE: null,
                MC_FAB_PROD_ORD_H_IDD: null,
                LK_COL_TYPE_ID: null,
                MC_COLOR_ID : null
            }

           var data = dataOri?dataOri:Defdata;

            $scope.gridSearchResultDs = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                pageSize: 100,
                schema: {
                    data: "data",
                    total: "total",
                },
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = "/api/Knit/FabProdKnitOrder/getFabOrderDataForBatchProgram?pMC_BYR_ACC_ID=" + (data.MC_BYR_ACC_ID||null);
                        //url += "&pRF_FAB_PROD_CAT_ID=" + formData.RF_FAB_PROD_CAT_ID;
                        url += "&pRF_FAB_PROD_CAT_ID_LST=1,2,3,5,6";
                        url += "&pFIRSTDATE=" + (data.FIRSTDATE||'');
                        url += "&pLASTDATE=" + (data.LASTDATE||'');
                        url += "&pMC_FAB_PROD_ORD_H_ID=" + (data.MC_FAB_PROD_ORD_H_IDD || data.MC_FAB_PROD_ORD_H_IDDD||'');
                        url += "&pLK_COL_TYPE_ID=" + (data.LK_COL_TYPE_ID||'');

                        url += "&pMC_COLOR_ID=" + (data.MC_COLOR_ID||'');

                        url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += config.kFilterStr2QueryParam(params.filter);
                        DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                group: { field: "ORDER_NO_LIST_CON" }
            });
        };


        function refreshRequirement(pMC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, pRF_FAB_TYPE_ID,pRQD_PRD_QTY) {
            var url = '/DyeBatch/GetDataFabReqCalScP?pMC_FAB_PROD_ORD_H_LST=' + pMC_FAB_PROD_ORD_H_ID + '&pFAB_COLOR_ID=' + pFAB_COLOR_ID + '&pRF_FAB_TYPE_ID=' + pRF_FAB_TYPE_ID + '&pRQD_PRD_QTY=' + pRQD_PRD_QTY + '&pDYE_SC_PRG_ISS_ID=' + formData.DYE_SC_PRG_ISS_ID;
            return DyeingDataService.getDataByUrl(url).then(function (res) {
                $scope.form['requirements'] = res;
            });
        }





        $scope.gridSearchResultOpt = {
            height: '600px',
            scrollable: true,
            columns: [
                { field: "BYR_ACC_NAME_EN", title: "ByrAcc", width: "40px" },
                { field: "STYLE_NO", title: "Order/Style", width: "80px", template: "<span style=\"display: block;\">#=ORDER_NO_LIST #</span> <span style=\"display: block;font-style:italic;\"><small>#:STYLE_NO#</small><span>" },

                { field: "ORDER_NO_LIST_CON", hidden: true, title: "Order #", width: "100px", groupHeaderTemplate: " <b>  #= value # </b>" },
                { field: "COL_TYPE_NAME", title: "Type", width: "30px" },
                { field: "COLOR_NAME_EN", title: "Color Name", width: "110px", template: "<span style=\"display: block;\">#=COLOR_NAME_EN #</span> # if( !LD_RECIPE_NO) {#<span style=\"display: block;color:red;\">No Labdip<span># }#" },

                { field: "NET_GFAB_QTY", title: "Req. Qty", width: "30px" },
                { field: "BATCH_GFAB_QTY", title: "BT", width: "30px" },
                { field: "BAL_GFAB_QTY", title: "Balance", width: "30px" },
            ],
            change: function (e) {
                var item = e.sender.dataItem(this.select());


                $scope.form['BYR_ACC_NAME_EN'] = item.BYR_ACC_NAME_EN;
                $scope.form['COLOR_NAME_EN'] = item.COLOR_NAME_EN;
                $scope.form['DYE_MTHD_NAME'] = item.DYE_MTHD_NAME;
                $scope.form['FAB_TYPE_NAME'] = item.FAB_TYPE_NAME;
                $scope.form['ORDER_NO_LIST'] = item.ORDER_NO_LIST;
                $scope.form['STYLE_NO'] = item.STYLE_NO;

                $scope.form['FAB_COLOR_ID'] = item.FAB_COLOR_ID;
                $scope.form['LK_DYE_MTHD_ID'] = item.LK_DYE_MTHD_ID;
                $scope.form['RF_FAB_TYPE_ID'] = item.RF_FAB_TYPE_ID;
                $scope.form['MC_FAB_PROD_ORD_H_ID'] = item.MC_FAB_PROD_ORD_H_ID;

                DyeingDataService.getDataByUrl('/DyeBatch/GetDataFabReqCalProgramScP?pMC_FAB_PROD_ORD_H_LST=' + item.MC_FAB_PROD_ORD_H_ID + '&pFAB_COLOR_ID=' + item.FAB_COLOR_ID).then(function (res) {
                    $scope.form['requirements'] = res;
                });

            },
            selectable: true,
        };




        function getSelectMonth(MC_BYR_ACC_ID, RF_FAB_PROD_CAT_ID) {
            return DyeingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/' + (MC_BYR_ACC_ID || 0) + '/0/' + (RF_FAB_PROD_CAT_ID || 0)).then(function (res) {
                $scope.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        };

        $scope.onChangeShipMonth = function (e) {
            var itmShipMonth = e.sender.dataItem(e.sender.item);

            if (itmShipMonth.FIRSTDATE && itmShipMonth.LASTDATE) {
                $scope.search['FIRSTDATE'] = itmShipMonth.FIRSTDATE;
                $scope.search['LASTDATE'] = itmShipMonth.LASTDATE;

                getStyleExtList(($scope.search.MC_BYR_ACC_ID || null), formData.RF_FAB_PROD_CAT_ID, itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE)

                getFabOederByOh($scope.search.MC_BYR_ACC_ID, formData.RF_FAB_PROD_CAT_ID, itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE, null)


            } else {
                getStyleExtList(($scope.search.MC_BYR_ACC_ID || null), formData.RF_FAB_PROD_CAT_ID, null, null);
                $scope.search['FIRSTDATE'] = null;
                $scope.search['LASTDATE'] = null;

                getFabOederByOh($scope.search.MC_BYR_ACC_ID, formData.RF_FAB_PROD_CAT_ID, null, null, null)
            }
        };
        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME #</p></span>';
        $scope.valueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';

        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.FAB_PROD_CAT_SNAME #</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';

        $scope.onFabOrderChange = function (e) {
            var itm = e.sender.dataItem(e.sender.item);
            if (itm.MC_FAB_PROD_ORD_H_ID) {
                getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);

            } else {
                getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);
            }


        };

        DyeingDataService.getDataByFullUrl('/api/common/GetDyeingMethodList').then(function (res) {
            $scope.DyeingMthdDs = new kendo.data.DataSource({
                data: res
            });
        });

        $scope.buyerAcList = {
            optionLabel: "--- Buyer A/C ---",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            select: function (e) {
                var item = this.dataItem(e.item);
                if (item.MC_BYR_ACC_ID.length != 0) {
                    getStyleExtList(item.MC_BYR_ACC_ID, null);
                    getSelectMonth(item.MC_BYR_ACC_ID);
                    getFabOederByOh(item.MC_BYR_ACC_ID, null, null, null, null);
                }
            },
            dataTextField: "BYR_ACC_NAME_EN",
            dataValueField: "MC_BYR_ACC_ID"
        };

        $scope.addOtherOrder = function (data) {
            data.push(null);
        };

        $scope.remOtherOrder = function (data, idx) {
            if (data.length > 1) {
                data.splice(idx, 1);
            }

        };

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-dd"]
        };



        $scope.save = function (data, isValid) {

            var todate = new Date();

            if (!isValid) {
                return;
            }

            if (!data.LK_DYE_MTHD_ID) {
                $scope.DyeingBatchPlan.LK_DYE_MTHD_ID.$setValidity('required', false);
                return;
            }


            var DfProcessData = [];
            var data2Save = [];


            angular.forEach($scope.df_data, function (val, key) {
                angular.forEach(val.items, function (v, k) {
                    if (v.IS_SELECTED == "Y") {
                        DfProcessData.push({
                            DYE_BT_RQD_PROC_ID: v.DYE_BT_RQD_PROC_ID,
                            DF_PROC_TYPE_ID: v.DF_PROC_TYPE_ID
                        });
                    }
                })
            });


            var DfProcessXml = config.xmlStringShortNoTag(DfProcessData);


            for (var i = 0; i < data.NO_OF_BATCH; i++) {

                var finDia = [];


                var parent = _.filter(data.requirements, function (o) {
                    return o.MC_FAB_PROD_ORD_H_ID == data.MC_FAB_PROD_ORD_H_ID;
                });

                angular.forEach(parent, function (oo) {
                    finDia = finDia.concat(
                        oo.fin_dias.filter(function (f) { return (f.RQD_PRD_QTY || 0) > 0 }).map(function (o) {
                            return {
                                DYE_BT_PLAN_FIN_DIA_ID: o.DYE_BT_PLAN_FIN_DIA_ID,
                                MC_FAB_PROD_ORD_D_ID: o.MC_FAB_PROD_ORD_D_ID,
                                RQD_PRD_QTY: o.RQD_PRD_QTY
                            }
                        }))
                });

                data2Save.push({
                    DYE_BATCH_PLAN_ID: data.DYE_BATCH_PLAN_ID,
                    DYE_SC_PRG_ISS_ID: data.DYE_SC_PRG_ISS_ID,
                    DYE_MACHINE_ID: data.DYE_MACHINE_ID,

                    MC_FAB_PROD_ORD_H_ID: data.MC_FAB_PROD_ORD_H_ID,

                    FAB_COLOR_ID: data.FAB_COLOR_ID,
                    RF_FAB_TYPE_ID: data.RF_FAB_TYPE_ID,

                    LK_DYE_MTHD_ID: data.LK_DYE_MTHD_ID,
                    PRIORITY_NO: i,
                    RQD_PRD_QTY: data.RQD_PRD_QTY,
                    QTY_MOU_ID: 3,
                    REQUIREMENTS: '',
                    UN_LOAD_TIME: new Date(todate.getFullYear(),todate.getMonth()+1, todate.getDay()+1,todate.getHours(),(todate.getMinutes()+1),0,0).toISOString().substr(0, 19),
                    LOAD_TIME: new Date().toISOString().substr(0, 19),

                    MC_STYLE_D_FAB_ID: data.MC_STYLE_D_FAB_ID,
                    DF_XML: DfProcessXml,
                    FIN_DIA: config.xmlStringShortNoTag(finDia)
                });
            };

            return DyeingDataService.saveDataByUrl({
                DYE_SC_PRG_ISS_ID: formData.DYE_SC_PRG_ISS_ID,
                XML: config.xmlStringShort(data2Save),
                pOption: 1008
            }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                if (res.success === false) {
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
 
                        $scope.form['RQD_PRD_QTY'] = null;
                        //$modalInstance.close({
                        //    KNT_SC_PRG_ISS_ID: parseInt(res.data.OP_KNT_SC_PRG_ISS_ID || 0)
                        //});
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            });
        };

        $scope.saveWovenTrims = function (data, isValid, pINV_ITEM_CAT_ID) {

            var todate = new Date();

            if (!isValid) {
                return;
            }
            if (!data.LK_DYE_MTHD_ID) {
                $scope.DyeingBatchPlan.LK_DYE_MTHD_ID.$setValidity('required', false);
                return;
            }

            if (!data.FAB_COLOR_ID) {
                if (data.INV_ITEM_CAT_ID == 7) {
                    $scope.DyeingBatchPlanTrims.FAB_COLOR_ID.$setValidity('required', false);
                } else if (data.INV_ITEM_CAT_ID == 35) {
                    $scope.DyeingBatchPlanWoven.FAB_COLOR_ID.$setValidity('required', false);
                }
                return;
            }

            var RANGE_START;
            var RANGE_END;


            var startDate;
            var data2Save = [];
            for (var i = 0; i < data.NO_OF_BATCH; i++) {


                var child2Save = [];


                var parent = _.filter(data.requirements, function (o) {
                    return o.MC_FAB_PROD_ORD_H_ID == data.MC_FAB_PROD_ORD_H_ID;
                });


                angular.forEach(parent, function (oo) {

                    // if (oo.non_col_cu_avail.length > 0 || oo.col_cu_avail.length > 0) {

                    child2Save.push({

                        DYE_BATCH_NO: parent[0].DYE_BATCH_NO,
                        DYE_LOT_NO: parent[0].DYE_LOT_NO,
                        MC_FAB_PROD_ORD_H_ID: oo.MC_FAB_PROD_ORD_H_ID,
                        DYE_BT_CARD_D_TRMS_ID: oo.DYE_BT_CARD_D_TRMS_ID,
                        MC_ORD_TRMS_ITEM_ID: oo.MC_ORD_TRMS_ITEM_ID,
                        MC_STYLE_H_ID: oo.MC_STYLE_H_ID,

                        RQD_QTY_YDS: oo.RQD_QTY_YDS,
                        RQD_QTY_KG: oo.RQD_QTY_KG
                    });
                    // }

                });


                data2Save.push({
                    DYE_BATCH_PLAN_ID: data.DYE_BATCH_PLAN_ID,
                    DYE_BATCH_SCDL_ID: data.DYE_BATCH_SCDL_ID,
                    MC_FAB_PROD_ORD_H_ID: data.MC_FAB_PROD_ORD_H_ID,
                    FAB_COLOR_ID: data.FAB_COLOR_ID,
                    LK_DYE_MTHD_ID: data.LK_DYE_MTHD_ID,
                    PRIORITY_NO: i,
                    RQD_PRD_QTY: _.sumBy(child2Save, function (o) { return (o.RQD_QTY_KG ||0)}),
                    INV_ITEM_CAT_ID: pINV_ITEM_CAT_ID,
                    LNK_ORD_H_ID_LST: data.LNK_ORD_H_ID_LST,
                    UN_LOAD_TIME: new Date(todate.getFullYear(),todate.getMonth()+1, todate.getDay()+1,todate.getHours(),(todate.getMinutes()+1),0,0).toISOString().substr(0, 19),
                    LOAD_TIME: new Date().toISOString().substr(0, 19),
                    REQUIREMENTS: config.xmlStringShortNoTag(child2Save)

                });

            };



            return DyeingDataService.saveDataByUrl({
                DYE_SC_PRG_ISS_ID: formData.DYE_SC_PRG_ISS_ID,
                XML: config.xmlStringShort(data2Save),
                pOption: 1009
            }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                if (res.success === false) {
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                        
                        //$modalInstance.close({
                        //    KNT_SC_PRG_ISS_ID: parseInt(res.data.OP_KNT_SC_PRG_ISS_ID || 0)
                        //});
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            });
        };
        $scope.cancel = function () {
            $modalInstance.dismiss();
        }
    }

})();
