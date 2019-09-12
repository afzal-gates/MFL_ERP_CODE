
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeBatchProgramModalController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$modalInstance', '$scope', 'formData', 'McDataDs', DyeBatchProgramModalController]);
    function DyeBatchProgramModalController($q, config, DyeingDataService, $stateParams, $modalInstance, $scope, formData, McDataDs) {

        var ObjFromXml = [];
        var cur_tab = 'KNIT';
        getSelectMonth(0, 0);
        getDfProcessTypeData();



        $scope.removeFabric = function (item) {
            var idx = $scope.form.requirements.indexOf(item);
            console.log(idx);
            $scope.form.requirements.splice(idx, 1);
        }


        function getStyleExtList(pMC_BYR_ACC_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE) {
            $scope.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=2,5,6,8,9";
                        url += "&pFIRSTDATE=" + pFIRSTDATE;
                        url += "&pLASTDATE=" + pLASTDATE;

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

        function getDfProcessTypeData() {
            return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDFProcessData?pDYE_BATCH_PLAN_ID').then(function (res) {
                $scope.df_data = res;
            });
        };

        function getFabOederByOh(pMC_BYR_ACC_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE, pMC_FAB_PROD_ORD_H_ID) {
            $scope.FabOederByOhDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=2,5,6,8,9";
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
                            console.log("JAHID");
                            console.log(res);
                            console.log("JAHID");

                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };




        function getFabOederByOhMerge(pMC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, pRF_FAB_TYPE_ID, pRF_FAB_PROD_CAT_ID, pINV_ITEM_CAT_ID) {
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
                        url += '&pINV_ITEM_CAT_ID=' + pINV_ITEM_CAT_ID;

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            console.log("JAHID");
                            console.log(res);
                            console.log("JAHID");
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };

        getFabOederByOh(null, formData.RF_FAB_PROD_CAT_ID, null, null, null);
        getStyleExtList(null, formData.RF_FAB_PROD_CAT_ID, null, null);
        $scope.form = {
            NO_OF_BATCH: 1,
            INTERVAL: 10,
            MC_FAB_PROD_ORD_H_ID_LST: [-1],
            DYE_BATCH_PLAN_ID: formData.DYE_BATCH_PLAN_ID,
            DYE_BATCH_SCDL_ID: formData.DYE_BATCH_SCDL_ID,
            DYE_MACHINE_ID: formData.DYE_MACHINE_ID,
            END_DT: formData.END_DT,
            LOAD_TIME: formData.LOAD_TIME,
            START_DT: formData.START_DT,
            UN_LOAD_TIME: formData.UN_LOAD_TIME,
            IS_ADD_OTH_ORD: 'N',
            DURATION: formData.DURATION,
            MAX_LOAD: formData.MAX_LOAD,
            PCT_EFFCNCY: formData.PCT_EFFCNCY
        };

        function refreshLastEvtDate(pDYE_MACHINE_ID, host) {
            return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID=null&pIS_SMP_BLK&pDYE_BATCH_SCDL_ID=' + formData.DYE_BATCH_SCDL_ID + '&pDYE_MACHINE_ID=' + pDYE_MACHINE_ID).then(function (res) {
                if (res.length == 1) {
                    host['DYE_BATCH_PLAN_ID'] = -1;
                    host['LOAD_TIME'] = res[0].LAST_EVT_END;
                    host['RQD_PRD_QTY'] = 0;
                    host['UN_LOAD_TIME'] = moment(res[0].LAST_EVT_END).add($scope.form.DURATION, 'h')._d;
                    getDfProcessTypeData();
                }
            });
        }

        $scope.onChangeTrims = function (data, pINV_ITEM_CAT_ID) {
            if (data.IS_WITH_TRMS == 'Y') {
                var addiOrders = ','+(data.MC_FAB_PROD_ORD_H_ID_LST || []).join(",");
                return DyeingDataService.getDataByUrl('/DyeBatch/GetDataFabReqCalProgramWithTrims?pMC_FAB_PROD_ORD_H_LST=' + data.MC_FAB_PROD_ORD_H_ID + addiOrders + '&pFAB_COLOR_ID=' + data.FAB_COLOR_ID).then(function (res) {
                    $scope.form['requirements'] = res;
                });

            }
        };


        $scope.onChangeOtherOrder = function (data, pINV_ITEM_CAT_ID) {
            if (data.IS_ADD_OTH_ORD == 'Y') {
                getFabOederByOhMerge(data.MC_FAB_PROD_ORD_H_ID, data.FAB_COLOR_ID, data.RF_FAB_TYPE_ID, formData.RF_FAB_PROD_CAT_ID, pINV_ITEM_CAT_ID);

            } else {
                $scope.form.MC_FAB_PROD_ORD_H_ID_LST = [undefined];
            }
        };

        $scope.$watch('form.MC_FAB_PROD_ORD_H_ID_LST', function (newVal, oldVal) {
            if ($scope.form.MC_FAB_PROD_ORD_H_ID) {
                refreshRequirement($scope.form.MC_FAB_PROD_ORD_H_ID + ',' + newVal.filter(function (res) { return res !== undefined }).join(','), $scope.form.FAB_COLOR_ID);
            }

        }, true);


        $scope.McDataDs = new kendo.data.DataSource({
            data: McDataDs
        });



        function getWovenFabricAvailability(pMC_FAB_PROD_ORD_H_LST, pMC_COLOR_ID, pINV_ITEM_CAT_ID, host) {
            if (!pMC_FAB_PROD_ORD_H_LST)
                return;

            var url = '/DyeBatchPlan/getProgAvailabilityWovenFab';
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
                    DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID&pIS_SMP_BLK&pDYE_BATCH_SCDL_ID=' + formData.DYE_BATCH_SCDL_ID).then(function (res) {
                        $scope.McDataDs = new kendo.data.DataSource({
                            data: res
                        });

                    });

                    getColorDs($scope.form.MC_FAB_PROD_ORD_H_ID, 5, $scope.formWoven);
                    $scope.formWoven['requirements'] = [];

                    break;
                case 'TRIMS':
                    cur_tab = 'TRIMS';
                    $scope.formTrims = angular.copy($scope.form);
                    DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID&pIS_SMP_BLK&pDYE_BATCH_SCDL_ID=' + formData.DYE_BATCH_SCDL_ID).then(function (res) {
                        $scope.McDataDs = new kendo.data.DataSource({
                            data: res
                        });
                    });
                    //getWovenFabricAvailability($scope.form.MC_FAB_PROD_ORD_H_ID, $scope.form.FAB_COLOR_ID, 7, $scope.formTrims);
                    getColorDs($scope.form.MC_FAB_PROD_ORD_H_ID, 7, $scope.formTrims);
                    $scope.formTrims['requirements'] = [];

                    break;
                default:
                    $scope.McDataDs = new kendo.data.DataSource({
                        data: McDataDs
                    });
            }
        };

        $scope.onBoundDyeMc = function (e) {
            // e.sender.value(formData.DYE_MACHINE_ID);
        };


        $scope.onChangeDyeMc = function (e, host) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.id) {
                host['NO_OF_BATCH'] = 1;
                host['INTERVAL'] = 10;
                host['MC_FAB_PROD_ORD_H_ID_LST'] = [-1];
                host['DYE_BATCH_PLAN_ID'] = -1;
                host['DYE_BATCH_SCDL_ID'] = formData.DYE_BATCH_SCDL_ID;
                host['DYE_MACHINE_ID'] = item.id;
                host['END_DT'] = item.END_DT;

                DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID=null&pIS_SMP_BLK=' + formData.IS_SMP_BLK + '&pDYE_BATCH_SCDL_ID=' + formData.DYE_BATCH_SCDL_ID + '&pDYE_MACHINE_ID=' + item.id).then(function (res) {
                    if (res.length == 1) {
                        host['LOAD_TIME'] = res[0].LAST_EVT_END;
                        host['MAX_LOAD'] = res[0].MAX_LOAD;
                        host['PCT_EFFCNCY'] = res[0].PCT_EFFCNCY;
                    }
                });

                host['START_DT'] = item.START_DT;
                host['UN_LOAD_TIME'] = '';
                host['IS_ADD_OTH_ORD'] = 'N';
                host['DURATION'] = '';


            }
        };
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

        $scope.findGridData = function (dataOri) {

            var Defdata = {
                MC_BYR_ACC_ID: null,
                FIRSTDATE: null,
                LASTDATE: null,
                MC_FAB_PROD_ORD_H_IDD: null,
                LK_COL_TYPE_ID: null
            }

            var data = dataOri ? dataOri : Defdata;

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
                        var url = "/api/Knit/FabProdKnitOrder/getFabOrderDataForBatchProgram?pMC_BYR_ACC_ID=" + data.MC_BYR_ACC_ID;
                        //url += "&pRF_FAB_PROD_CAT_ID=" + formData.RF_FAB_PROD_CAT_ID;
                        url += "&pFIRSTDATE=" + data.FIRSTDATE;
                        url += "&pRF_FAB_PROD_CAT_ID_LST=2,5,6,8,9";
                        url += "&pLASTDATE=" + data.LASTDATE;
                        url += "&pMC_FAB_PROD_ORD_H_ID=" + (data.MC_FAB_PROD_ORD_H_IDDD || data.MC_FAB_PROD_ORD_H_IDD);
                        url += "&pLK_COL_TYPE_ID=" + data.LK_COL_TYPE_ID;
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


        function refreshRequirement(pMC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID) {
            return DyeingDataService.getDataByUrl('/DyeBatch/GetDataFabReqCalProgram?pMC_FAB_PROD_ORD_H_LST=' + pMC_FAB_PROD_ORD_H_ID + '&pFAB_COLOR_ID=' + pFAB_COLOR_ID).then(function (res) {
                $scope.form['requirements'] = res;
            });
        }


        $scope.OpenFabricBookingData = function (data) {
                   var params = {
                            REPORT_CODE : 'RPT-2001',
                            MC_BLK_FAB_REQ_H_ID: data.MC_BLK_FAB_REQ_H_ID,
                            IS_MULTI_SHIP_DT : 'N',
                            PAGE_SIZE_NAME: 'A4',
                            MC_BLK_REVISION_NO: data.MC_BLK_REVISION_NO
                    };

                    var form = document.createElement("form");
                    form.setAttribute("method", "post");
                    form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
                    form.setAttribute("target", '_blank');



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


        $scope.gridSearchResultOpt = {
            height: '600px',
            scrollable: true,
            columns: [
                { field: "BYR_ACC_NAME_EN", title: "ByrAcc", width: "50px", template: "<span style=\"display: block;\">#=BYR_ACC_NAME_EN #</span> <span style=\"display: block;font-style:italic;color:FUCHSIA;\"><small>(#:FAB_PROD_CAT_NAME#)</small><span>" },
                { field: "STYLE_NO", title: "Order/Style", width: "50px", template: "<span style=\"display: block;\">#=ORDER_NO_LIST #</span> <span style=\"display: block;font-style:italic;\"><small>#:STYLE_NO#</small><span>" },

                { field: "ORDER_NO_LIST_CON", hidden: true, title: "Order #", width: "80px", groupHeaderTemplate: " <b>  #= value # </b>" },
                { field: "COL_TYPE_NAME", title: "Type", width: "30px" },
                { field: "COLOR_NAME_EN", title: "Color Name", width: "80px", template: "<span style=\"display: block;\">#=COLOR_NAME_EN #</span> # if( !LD_RECIPE_NO) {#<span style=\"display: block;color:red;\">No Labdip<span># }#" },

                {
                    field: "NET_GFAB_QTY", title: "Req. Qty", width: "40px",
                    template: "<span style=\"display: block;\">#=NET_GFAB_QTY #</span> <span title=\"Open Fabric Booking\" ng-click=\"OpenFabricBookingData(dataItem)\" style=\"display: block;font-style:italic;cursor: pointer;\"><i class=\"fa fa fa-file-pdf-o\"></i><span>"
                },
                { field: "KNT_FAB_QTY", title: "Grey Qty", width: "40px" },
                { field: "BATCH_GFAB_QTY", title: "BT", width: "40px" },
                { field: "BAL_GFAB_QTY", title: "Balance", width: "40px" },
            ],
            change: function (e) {
                var item = e.sender.dataItem(this.select());

                console.log(item);


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
                $scope.form['FAB_PROD_CAT_NAME'] = item.FAB_PROD_CAT_NAME;

                $scope.form['RF_FAB_PROD_CAT_ID'] = item.RF_FAB_PROD_CAT_ID;
                $scope.form['MC_STYLE_H_EXT_ID'] = item.MC_STYLE_H_EXT_ID;
                $scope.form['MC_BYR_ACC_GRP_ID'] = item.MC_BYR_ACC_GRP_ID;
                $scope.form['IS_WITH_TRMS'] = 'N';

                DyeingDataService.getDataByUrl('/DyeBatch/GetDataFabReqCalProgram?pMC_FAB_PROD_ORD_H_LST=' + item.MC_FAB_PROD_ORD_H_ID + '&pFAB_COLOR_ID=' + item.FAB_COLOR_ID).then(function (res) {
                    $scope.form['requirements'] = res;
                });

                if (cur_tab != 'KNIT') {
                    $scope.onSelect(cur_tab);
                }



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

        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p style="font-size:x-small; color:red;"> #: data.FAB_PROD_CAT_SNAME # (#: data.STYLE_NO #)</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #/#: data.ORDER_NO_LST # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';

        $scope.onFabOrderChange = function (e) {
            var itm = e.sender.dataItem(e.sender.item);
            if (itm.MC_FAB_PROD_ORD_H_ID) {
                getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);

            } else {
                getFabOederByOh(null, null, null, null, null);
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


        $scope.onChangeDuration = function (DURATION, LOAD_TIME, host) {
            if (LOAD_TIME && DURATION) {
                host['UN_LOAD_TIME'] = moment(LOAD_TIME).add(DURATION, 'h')._d;
            }

        };

        $scope.onChangeDate = function (LOAD_TIME, UN_LOAD_TIME, host) {
            host['DURATION'] = moment(new Date(UN_LOAD_TIME)).diff(moment(new Date(LOAD_TIME)), 'hours');
        };

        $scope.onChangeDateTrims = function (LOAD_TIME, UN_LOAD_TIME) {
            $scope.formTrims['DURATION'] = moment(new Date(UN_LOAD_TIME)).diff(moment(new Date(LOAD_TIME)), 'hours');
        }

        $scope.$watch('form.UN_LOAD_TIME', function (newVal, oldVal) {
            console.log($scope.form.LOAD_TIME);
            if (newVal !== oldVal && $scope.form.LOAD_TIME) {
                $scope.form['DURATION'] = moment(newVal).diff(moment($scope.form.LOAD_TIME), 'hours');
            }

        });


        $scope.addOtherOrder = function (data) {
            data.push(null);
        };

        $scope.remOtherOrder = function (data, idx) {
            if (data.length > 1) {
                data.splice(idx, 1);
            }

        };

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
        };



        $scope.save = function (data, isValid) {

            if (!isValid) {
                return;
            }
            var RANGE_START;
            var RANGE_END;

            var DfProcessData = [];

            var ORD_LIST = [];

            angular.forEach(data.MC_FAB_PROD_ORD_H_ID_LST, function (val) {
                if (val && val > 0) {
                    if (ORD_LIST.indexOf(val) < 0) {
                        ORD_LIST.push(val);
                    }
                }
            });

            if (data.IS_ADD_OTH_ORD == 'Y') {
                data['LNK_ORD_H_ID_LST'] = ORD_LIST.join(',');
            } else {
                data['LNK_ORD_H_ID_LST'] = null;
            }
            var startDate;
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

                if (i == 0) {
                    data2Save = [];
                    startDate = new DayPilot.Date(data.LOAD_TIME).addMinutes(0);
                    RANGE_START = startDate;
                } else if (i > 0 && data2Save.length > 0) {
                    startDate = new DayPilot.Date(data2Save[i - 1]['UN_LOAD_TIME']).addMinutes(data.INTERVAL);
                }


                data2Save.push({
                    DYE_BATCH_PLAN_ID: data.DYE_BATCH_PLAN_ID,
                    DYE_BATCH_SCDL_ID: data.DYE_BATCH_SCDL_ID,
                    DYE_MACHINE_ID: data.DYE_MACHINE_ID,
                    MC_FAB_PROD_ORD_H_ID: data.MC_FAB_PROD_ORD_H_ID,
                    FAB_COLOR_ID: data.FAB_COLOR_ID,
                    RF_FAB_TYPE_ID: data.RF_FAB_TYPE_ID,
                    LK_DYE_MTHD_ID: data.LK_DYE_MTHD_ID,
                    INV_ITEM_CAT_ID: 34,
                    INV_ITEM_CAT_LST: data.IS_WITH_TRMS == "Y" ? "34,7" : "34",
                    PRIORITY_NO: i,
                    RQD_PRD_QTY: data.RQD_PRD_QTY,
                    QTY_MOU_ID: 3,
                    IS_WITH_TRMS: data.IS_WITH_TRMS,
                    LNK_ORD_H_ID_LST: data.LNK_ORD_H_ID_LST,
                    LOAD_TIME: startDate.toString(),
                    UN_LOAD_TIME: startDate.addHours(data.DURATION).toString(),
                    DF_XML: DfProcessXml
                });

                RANGE_END = startDate.addHours(data.DURATION);

            };


            // ****************************************** Start **************************************
            
            //if (data.IS_WITH_TRMS == "Y")
            //    data2Save.push({
            //        DYE_BATCH_PLAN_ID: data.DYE_BATCH_PLAN_ID,
            //        DYE_BATCH_SCDL_ID: data.DYE_BATCH_SCDL_ID,
            //        DYE_MACHINE_ID: data.DYE_MACHINE_ID,
            //        MC_FAB_PROD_ORD_H_ID: data.MC_FAB_PROD_ORD_H_ID,
            //        FAB_COLOR_ID: data.FAB_COLOR_ID,
            //        RF_FAB_TYPE_ID: data.RF_FAB_TYPE_ID,
            //        LK_DYE_MTHD_ID: data.LK_DYE_MTHD_ID,
            //        INV_ITEM_CAT_ID: 7,
            //        PRIORITY_NO: i,
            //        RQD_PRD_QTY: data.RQD_PRD_QTY,
            //        QTY_MOU_ID: 3,
            //        IS_WITH_TRMS: data.IS_WITH_TRMS,
            //        LNK_ORD_H_ID_LST: data.LNK_ORD_H_ID_LST,
            //        LOAD_TIME: startDate.toString(),
            //        UN_LOAD_TIME: startDate.addHours(data.DURATION).toString(),
            //        DF_XML: DfProcessXml
            //    });

            console.log(data2Save);

            //return;


            // ***************************************** END ***************************************

            return DyeingDataService.saveDataByUrl({
                DYE_BATCH_SCDL_ID: formData.DYE_BATCH_SCDL_ID,
                XML: config.xmlStringShort(data2Save),
                pOption: 1001,
                IS_SMP_BLK: 'B'
            }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                if (res.success === false) {
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        refreshLastEvtDate(data.DYE_MACHINE_ID, data);
                        //$modalInstance.close({
                        //    KNT_SC_PRG_ISS_ID: parseInt(res.data.OP_KNT_SC_PRG_ISS_ID || 0)
                        //});
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            });
        };

        $scope.saveWovenTrims = function (data, isValid, pINV_ITEM_CAT_ID) {
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
                } else if (data.INV_ITEM_CAT_ID == 5) {
                    $scope.DyeingBatchPlanWoven.FAB_COLOR_ID.$setValidity('required', false);
                }
                return;
            }

            var RANGE_START;
            var RANGE_END;


            var ORD_LIST = [];

            angular.forEach(data.MC_FAB_PROD_ORD_H_ID_LST, function (val) {
                if (val && val > 0) {
                    if (ORD_LIST.indexOf(val) < 0) {
                        ORD_LIST.push(val);
                    }
                }
            });

            if (data.IS_ADD_OTH_ORD == 'Y') {
                data['LNK_ORD_H_ID_LST'] = ORD_LIST.join(',');
            } else {
                data['LNK_ORD_H_ID_LST'] = null;
            }
            var startDate;
            var data2Save = [];

            for (var i = 0; i < data.NO_OF_BATCH; i++) {
                if (i == 0) {
                    data2Save = [];
                    startDate = new DayPilot.Date(data.LOAD_TIME).addMinutes(0);
                    RANGE_START = startDate;
                } else if (i > 0 && data2Save.length > 0) {
                    startDate = new DayPilot.Date(data2Save[i - 1]['UN_LOAD_TIME']).addMinutes(data.INTERVAL);
                }
                var child2Save = [];

                var parent = _.filter(data.requirements, function (o) {
                    return o.MC_FAB_PROD_ORD_H_ID == data.MC_FAB_PROD_ORD_H_ID;
                });

                var child = _.filter(data.requirements, function (o) {
                    return o.MC_FAB_PROD_ORD_H_ID != data.MC_FAB_PROD_ORD_H_ID;
                }) || [];


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
                        RQD_QTY_KG: oo.RQD_QTY_KG,

                        CHILD: config.xmlStringShortNoTagChild(child.map(function (ob) {
                            return {

                                DYE_BATCH_NO: child[0].DYE_BATCH_NO,
                                DYE_LOT_NO: child[0].DYE_LOT_NO,
                                MC_FAB_PROD_ORD_H_ID: ob.MC_FAB_PROD_ORD_H_ID,
                                DYE_BT_CARD_D_TRMS_ID: ob.DYE_BT_CARD_D_TRMS_ID,
                                MC_ORD_TRMS_ITEM_ID: ob.MC_ORD_TRMS_ITEM_ID,
                                MC_STYLE_H_ID: ob.MC_STYLE_H_ID,


                                RQD_QTY_YDS: ob.RQD_QTY_YDS,
                                RQD_QTY_KG: ob.RQD_QTY_KG
                            }

                        }))

                    });
                    // }

                });


                data2Save.push({
                    DYE_BATCH_PLAN_ID: data.DYE_BATCH_PLAN_ID,
                    DYE_BATCH_SCDL_ID: data.DYE_BATCH_SCDL_ID,
                    DYE_MACHINE_ID: data.DYE_MACHINE_ID,
                    MC_FAB_PROD_ORD_H_ID: data.MC_FAB_PROD_ORD_H_ID,
                    FAB_COLOR_ID: data.FAB_COLOR_ID,
                    LK_DYE_MTHD_ID: data.LK_DYE_MTHD_ID,
                    PRIORITY_NO: i,
                    RQD_PRD_QTY: data.RQD_PRD_QTY,
                    INV_ITEM_CAT_ID: pINV_ITEM_CAT_ID,
                    INV_ITEM_CAT_LST: data.IS_WITH_TRMS == "Y" ? "34,7" : pINV_ITEM_CAT_ID,
                    LNK_ORD_H_ID_LST: data.LNK_ORD_H_ID_LST,
                    LOAD_TIME: startDate.toString(),
                    UN_LOAD_TIME: startDate.addHours(data.DURATION).toString(),
                    REQUIREMENTS: config.xmlStringShortNoTag(child2Save)

                });

                RANGE_END = startDate.addHours(data.DURATION);

            };

            return DyeingDataService.saveDataByUrl({
                DYE_BATCH_SCDL_ID: formData.DYE_BATCH_SCDL_ID,
                XML: config.xmlStringShort(data2Save),
                pOption: 1007,
                IS_SMP_BLK: 'B'
            }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                if (res.success === false) {
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        refreshLastEvtDate(data.DYE_MACHINE_ID, data);

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
