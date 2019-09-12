(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeBatchFabReqController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', '$modal', '$window', 'Dialog', 'McList', 'cur_user_id', '$http', '$ngConfirm', DyeBatchFabReqController]);
    function DyeBatchFabReqController($q, config, DyeingDataService, $stateParams, $state, $scope, $modal, $window, Dialog, McList, cur_user_id, $http, $ngConfirm) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        var MC_OPEN = [];
        var OPEN_PLAN_ID = [];

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetReqSourceList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function GetReqSourceList() {
            return vm.reqSourceList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=5&pSC_USER_ID=' + cur_user_id).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 512 || x.LK_WH_TYPE_ID == 511 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };
        };

        vm.companyDs = new kendo.data.DataSource({
            data: McList
        });

        vm.onCompanyDataBound = function (e) {
            var ds = e.sender.dataSource.data();
            if (ds.length == 1) {
                e.sender.value(ds[0].HR_COMPANY_ID);
                vm.HR_COMPANY_ID = ds[0].HR_COMPANY_ID;
            } else if (ds.length > 1) {
                e.sender.value(ds[0].HR_COMPANY_ID);
                vm.HR_COMPANY_ID = ds[0].HR_COMPANY_ID;
            } else {
                vm.HR_COMPANY_ID = -1;
            }
        };



        vm.onChangeCompany = function (e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.HR_COMPANY_ID) {
                getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID, '', item.HR_COMPANY_ID);
            } else {
                getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID, '', null);
            }
        }

        vm.SchedulePlanData = {
            optionLabel: "--Select Plan--",
            filter: "contains",
            autoBind: true,
            template: '<span><h4 style="padding:0px;margin:0px;"><b>#: data.SCDL_REF_NO #</b></h4><p> #= kendo.toString(kendo.parseDate(data.START_DT),"dd/MM/yyyy HH:mm") # to #= kendo.toString(kendo.parseDate(data.END_DT),"dd/MM/yyyy HH:mm") #</p></span>',
            valueTemplate: '<span><h4 style="padding:0px;margin:0px;"><b>#: data.SCDL_REF_NO #</b><small>(#= kendo.toString(kendo.parseDate(data.START_DT),"dd/MM/yyyy HH:mm") # to #= kendo.toString(kendo.parseDate(data.END_DT),"dd/MM/yyyy HH:mm") #)</small></h4></span>',
            dataSource: {
                transport: {
                    read: function (e) {


                        var url = "/DyeBatchPlan/getSchedulePlanDatas";
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '?pSCDL_REF_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '?pSCDL_REF_NO';
                        }
                        url += '&pDYE_BATCH_SCDL_ID=' + $stateParams.pDYE_BATCH_SCDL_ID;
                        url += '&pIS_SMP_BLK=B';

                        DyeingDataService.getDataByUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true,
            },
            select: function (e) {
                var item = this.dataItem(e.item);
                if (item && item.DYE_BATCH_SCDL_ID) {
                    //getBatchProgramData(item.DYE_BATCH_SCDL_ID);
                    $state.go('DyeBatchFabReq', { pDYE_BATCH_SCDL_ID: item.DYE_BATCH_SCDL_ID }, { reload: true });
                };
            },
            dataBound: function (e) {
                var ds = e.sender.dataSource.data();

                if ($stateParams.pDYE_BATCH_SCDL_ID) {
                    var item = _.find(ds, function (o) {
                        return o.DYE_BATCH_SCDL_ID == $stateParams.pDYE_BATCH_SCDL_ID;
                    });
                } else {
                    var item = _.find(ds, function (o) {
                        return o.IS_ACTIVE == 'Y';
                    });
                }

                if (item && item.DYE_BATCH_SCDL_ID) {
                    e.sender.value(item.DYE_BATCH_SCDL_ID);
                    getBatchProgramData(item.DYE_BATCH_SCDL_ID, '', vm.HR_COMPANY_ID);
                    $state.go('DyeBatchFabReq', { pDYE_BATCH_SCDL_ID: item.DYE_BATCH_SCDL_ID }, { notify: false });

                };
            },
            dataTextField: "SCDL_REF_NO",
            dataValueField: "DYE_BATCH_SCDL_ID"
        };


        vm.onChangeQty = function (main) {
            var sum = 0;
            angular.forEach(_.groupBy(
              main.requirements
            , function (o) {
                return o.MC_FAB_PROD_ORD_H_ID;
            }), function (val, key) {

                sum = _.sumBy(val, function (oo) { return (oo.ACT_BATCH_QTY || oo.RQD_QTY_KG || 0); });

                angular.forEach(main.requirements, function (vvv, kkk) {
                    if (vvv.MC_FAB_PROD_ORD_H_ID == parseInt(key) && vvv.MC_FAB_PROD_ORD_H_SL == 1) {
                        vvv['BT_TOTAL'] = sum;
                    };
                });

            });
        };


        vm.onChangeQtyD = function (list, d) {
            d.ACT_BATCH_QTY = _.sumBy(d.non_col_cu_avail, function (o) { return (o.ASSIGN_QTY || 0); });
            vm.onChangeQty(list);
        };

        function getBatchProgramData(pDYE_BATCH_SCDL_ID, pDYE_BATCH_NO, pHR_COMPANY_ID) {
            return DyeingDataService.getDataByUrl('/DyeBatch/GetDataFabReqDyeBatch?pDYE_BATCH_SCDL_ID=' + pDYE_BATCH_SCDL_ID + '&pOption=3000&pIS_SMP_BLK=B&pDYE_BATCH_NO=' + (pDYE_BATCH_NO || '') + '&pHR_COMPANY_ID=' + (pHR_COMPANY_ID || null)).then(function (res) {
                vm.data = res.map(function (o) {
                    o['IS_OPEN'] = MC_OPEN.length > 0 ? _.some(MC_OPEN, function (oo) { return oo == o.DYE_MACHINE_ID }) : false;
                    return o;
                });
            });
        };

        vm.onRequirementClick = function (itm) {
            var url;

            if (!itm.hasOwnProperty('requirements')) {
                itm['requirements'] = [];
            }

            if (itm.IS_OPEN_REQ && itm.requirements.length == 0) {
                //if (itm.IS_OPEN_REQ && itm.requirements.length == 0) {
                url = '/DyeBatch/GetDataFabReqCal?pMC_FAB_PROD_ORD_H_LST=' + itm.MC_FAB_PROD_ORD_H_ID + (itm.LNK_ORD_H_ID_LST ? (',' + itm.LNK_ORD_H_ID_LST) : '');
                url += '&pDYE_BATCH_PLAN_ID=' + itm.DYE_BATCH_PLAN_ID;
                url += '&pDYE_BATCH_SCDL_ID=' + itm.DYE_BATCH_SCDL_ID;
                url += '&pFAB_COLOR_ID=' + itm.FAB_COLOR_ID;
                url += '&pRF_FAB_TYPE_ID=' + itm.RF_FAB_TYPE_ID;
                url += '&pRQD_PRD_QTY=' + itm.RQD_PRD_QTY;
                url += '&pINV_ITEM_CAT_ID=' + itm.INV_ITEM_CAT_ID;
                url += '&pIS_BATCH_STORE=' + (itm.IS_BATCH_STORE || "N");
                return DyeingDataService.getDataByUrl(url).then(function (res) {
                    itm['requirements'] = res;
                    //setTimeout(function () {
                    //    vm.onChangeQty(itm);
                    //}, 500);

                });
            }
        };


        vm.onIsBatchStoreClick = function (itm) {
            var url;

            if (!itm.hasOwnProperty('requirements')) {
                itm['requirements'] = [];
            }
            console.log(itm);
            url = '/DyeBatch/GetDataFabReqCal?pMC_FAB_PROD_ORD_H_LST=' + itm.MC_FAB_PROD_ORD_H_ID + (itm.LNK_ORD_H_ID_LST ? (',' + itm.LNK_ORD_H_ID_LST) : '');
            url += '&pDYE_BATCH_PLAN_ID=' + itm.DYE_BATCH_PLAN_ID;
            url += '&pDYE_BATCH_SCDL_ID=' + itm.DYE_BATCH_SCDL_ID;
            url += '&pFAB_COLOR_ID=' + itm.FAB_COLOR_ID;
            url += '&pRF_FAB_TYPE_ID=' + itm.RF_FAB_TYPE_ID;
            url += '&pRQD_PRD_QTY=' + itm.RQD_PRD_QTY;
            url += '&pINV_ITEM_CAT_ID=' + itm.INV_ITEM_CAT_ID;
            url += '&pIS_BATCH_STORE=' + (itm.IS_BATCH_STORE || "N");
            return DyeingDataService.getDataByUrl(url).then(function (res) {
                itm['requirements'] = res;
            });

            //if (itm.IS_OPEN_REQ) {
            //    //if (itm.IS_OPEN_REQ && itm.requirements.length == 0) {
            //    url = '/DyeBatch/GetDataFabReqCal?pMC_FAB_PROD_ORD_H_LST=' + itm.MC_FAB_PROD_ORD_H_ID + (itm.LNK_ORD_H_ID_LST ? (',' + itm.LNK_ORD_H_ID_LST) : '');
            //    url += '&pDYE_BATCH_PLAN_ID=' + itm.DYE_BATCH_PLAN_ID;
            //    url += '&pDYE_BATCH_SCDL_ID=' + itm.DYE_BATCH_SCDL_ID;
            //    url += '&pFAB_COLOR_ID=' + itm.FAB_COLOR_ID;
            //    url += '&pRF_FAB_TYPE_ID=' + itm.RF_FAB_TYPE_ID;
            //    url += '&pRQD_PRD_QTY=' + itm.RQD_PRD_QTY;
            //    url += '&pINV_ITEM_CAT_ID=' + itm.INV_ITEM_CAT_ID;
            //    url += '&pIS_BATCH_STORE=' + (itm.IS_BATCH_STORE || "N");
            //    return DyeingDataService.getDataByUrl(url).then(function (res) {
            //        itm['requirements'] = res;
            //        //setTimeout(function () {
            //        //    vm.onChangeQty(itm);
            //        //}, 500);

            //    });
            //}
        };

        vm.getDfProcessTypeData = function (itm) {
            if (!itm.hasOwnProperty('df_data')) {
                itm['df_data'] = [];
            }

            if (itm.IS_OPEN_DF_PRC && itm.df_data.length == 0) {

                return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDFProcessData?pDYE_BATCH_PLAN_ID=' + itm.DYE_BATCH_PLAN_ID).then(function (res) {
                    itm['df_data'] = res;
                });

            };

        };

        vm.OpenFabricBookingData = function (data) {
            var params = {
                REPORT_CODE: 'RPT-2001',
                MC_BLK_FAB_REQ_H_ID: data.MC_BLK_FAB_REQ_H_ID,
                IS_MULTI_SHIP_DT: 'N',
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


        vm.openLotDiaWiseKnitProd = function (data) {
            var params = {
                REPORT_CODE: 'RPT-3591',
                MC_BYR_ACC_GRP_ID: data.MC_BYR_ACC_GRP_ID,
                MC_STYLE_H_EXT_ID: data.MC_STYLE_H_EXT_ID,
                MC_COLOR_ID: data.FAB_COLOR_ID,
                RF_FAB_PROD_CAT_ID: data.RF_FAB_PROD_CAT_ID
            };

            console.log(params);
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReportRDLC');
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






        vm.cancel = function () {
            return getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID, '', vm.HR_COMPANY_ID);
        }

        $scope.$watch('DYE_BATCH_NO', function (newVal, oldVal) {
            if (newVal !== undefined && $stateParams.pDYE_BATCH_SCDL_ID !== undefined) {
                return getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID, newVal, vm.HR_COMPANY_ID);
            } 
        });

        vm.submitData = function (dataOri, isValid) {

            if (!isValid)
                return;

            MC_OPEN = [];
            var DfProcessData = [];
            OPEN_PLAN_ID = [];
            var data2save = [];
            angular.forEach(dataOri, function (val0, key0) {
                angular.forEach(val0.dyePrograms, function (val1, key1) {


                    if (val1.IS_SELECTED_BT) {


                        if (val1.df_data && val1.df_data.length > 0) {
                            var DfDataD = [];

                            angular.forEach(val1.df_data, function (val, key) {
                                angular.forEach(val.items, function (v, k) {
                                    if (v.IS_SELECTED == "Y") {
                                        DfDataD.push({
                                            DYE_BT_RQD_PROC_ID: v.DYE_BT_RQD_PROC_ID,
                                            DF_PROC_TYPE_ID: v.DF_PROC_TYPE_ID
                                        });
                                    }
                                })
                            });

                            DfProcessData.push({
                                DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                                DF_XML_D: config.xmlStringShortNoTag(DfDataD)

                            });


                        }



                        OPEN_PLAN_ID.push(val1.DYE_BATCH_PLAN_ID);

                        if (MC_OPEN.indexOf(val0.DYE_MACHINE_ID) < 0) {
                            MC_OPEN.push(val0.DYE_MACHINE_ID);
                        }


                        //var parent = _.filter(val1.requirements, function (o) {
                        //    return o.MC_FAB_PROD_ORD_H_ID == val1.MC_FAB_PROD_ORD_H_ID;
                        //});

                        //var child = _.filter(val1.requirements, function (o) {
                        //    return o.MC_FAB_PROD_ORD_H_ID != val1.MC_FAB_PROD_ORD_H_ID;
                        //}) || [];

                        var child = [];


                        angular.forEach(val1.requirements, function (oo) {

                            // if (oo.non_col_cu_avail.length > 0 || oo.col_cu_avail.length > 0) {

                            data2save.push({
                                DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                                ACT_LOAD_TIME: val1.LOAD_TIME,
                                ACT_UN_LOAD_TIME: val1.UN_LOAD_TIME,
                                FAB_COLOR_ID: val1.FAB_COLOR_ID,
                                LK_DYE_MTHD_ID: val1.LK_DYE_MTHD_ID,
                                RF_FAB_TYPE_ID: (oo.RF_FAB_TYPE_ID || ''),
                                IS_BATCH_STORE: (val1.IS_BATCH_STORE || 'N'),

                                IS_LOT_MIX: (val1.IS_LOT_MIX || 'N'),

                                RF_FIB_COMP_ID: (oo.RF_FIB_COMP_ID || ''),
                                MC_STYLE_H_ID: oo.MC_STYLE_H_ID,

                                INV_ITEM_CAT_ID: oo.INV_ITEM_CAT_ID,

                                ACT_BATCH_QTY: oo.INV_ITEM_CAT_ID == 34 ? oo.ACT_BATCH_QTY : (oo.RQD_QTY_KG || 0),
                                LK_DIA_TYPE_ID: (oo.LK_DIA_TYPE_ID || ''),
                                RQD_GSM: (oo.RQD_GSM || ''),


                                DYE_BATCH_NO: oo.DYE_BATCH_NO,
                                DYE_LOT_NO: _.filter(val1.requirements, function (x) { return x.DYE_LOT_NO.length > 0 && x.MC_FAB_PROD_ORD_H_ID == oo.MC_FAB_PROD_ORD_H_ID })[0].DYE_LOT_NO,
                                BT_TOTAL: _.filter(val1.requirements, function (x) { return x.BT_TOTAL > 0 && x.MC_FAB_PROD_ORD_H_ID == oo.MC_FAB_PROD_ORD_H_ID })[0].BT_TOTAL,

                                MC_FAB_PROD_ORD_H_ID: oo.MC_FAB_PROD_ORD_H_ID,
                                DYE_BT_CARD_H_ID: oo.DYE_BT_CARD_H_ID,
                                LK_FBR_GRP_ID: (oo.LK_FBR_GRP_ID || ''),
                                DYE_BT_CARD_GRP_ID: (oo.DYE_BT_CARD_GRP_ID || ''),

                                DYE_BT_CARD_D_TRMS_ID: (oo.DYE_BT_CARD_D_TRMS_ID || ''),
                                RQD_QTY_YDS: (oo.RQD_QTY_YDS || 0),
                                RQD_QTY_KG: (oo.RQD_QTY_KG || 0),

                                NON_COL_CUF: config.xmlStringShortNoTag(oo.non_col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                    return {
                                        DYE_BT_CARD_D_FAB_ID: o.DYE_BT_CARD_D_FAB_ID,

                                        INV_ORD_GFAB_STK_ID: o.INV_ORD_GFAB_STK_ID,

                                        KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID,
                                        RQD_QTY: o.RCV_ROLL_WT,
                                        QTY_MOU_ID: 3,
                                        ASSIGN_QTY: o.ASSIGN_QTY,
                                        RQD_ROLL_QTY: o.RQD_ROLL_QTY,
                                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID
                                    }
                                })),
                                COL_CUF: config.xmlStringShortNoTag(oo.col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                    return {
                                        KNT_CLCF_STYL_ITEM_ID: o.KNT_CLCF_STYL_ITEM_ID,
                                        DYE_BT_CARD_D_CLCF_ID: o.DYE_BT_CARD_D_CLCF_ID,
                                        PRD_QTY: o.PRD_QTY,
                                        QTY_MOU_ID: 1,
                                        RQD_ROLL_QTY: o.RQD_ROLL_QTY,
                                        ASSIGN_QTY: o.ASSIGN_QTY
                                    }
                                })),

                                TRIMS_ACC: config.xmlStringShortNoTag(oo.woven_trims_avai.filter(function (f) { return (f.RQD_QTY_YDS || 0) > 0 }).map(function (o) {
                                    return {
                                        MC_ORD_TRMS_ITEM_ID: o.MC_ORD_TRMS_ITEM_ID,
                                        DYE_BT_CARD_D_TRMS_ID: o.DYE_BT_CARD_D_TRMS_ID,
                                        DYE_BT_CARD_H_ID: (oo.DYE_BT_CARD_H_ID || ''),
                                        RQD_QTY_YDS: (o.RQD_QTY_YDS || 0),
                                        RQD_QTY_KG: (oo.RQD_QTY_KG || 0),
                                    }
                                })),
                                CHILD: config.xmlStringShortNoTag(child.map(function (ob) {


                                    return {
                                        DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                                        ACT_LOAD_TIME: val1.LOAD_TIME,
                                        ACT_UN_LOAD_TIME: val1.UN_LOAD_TIME,
                                        FAB_COLOR_ID: val1.FAB_COLOR_ID,
                                        LK_DYE_MTHD_ID: val1.LK_DYE_MTHD_ID,
                                        RF_FAB_TYPE_ID: (ob.RF_FAB_TYPE_ID || ''),

                                        RF_FIB_COMP_ID: (ob.RF_FIB_COMP_ID || ''),
                                        MC_STYLE_H_ID: ob.MC_STYLE_H_ID,
                                        INV_ITEM_CAT_ID: ob.INV_ITEM_CAT_ID,

                                        ACT_BATCH_QTY: ob.INV_ITEM_CAT_ID == 34 ? ob.ACT_BATCH_QTY : (ob.RQD_QTY_KG || 0),

                                        DYE_BATCH_NO: _.filter(child, function (x) { return x.DYE_BATCH_NO.length > 0 })[0].DYE_BATCH_NO,
                                        DYE_LOT_NO: _.filter(child, function (x) { return x.DYE_LOT_NO.length > 0 })[0].DYE_LOT_NO,

                                        MC_FAB_PROD_ORD_H_ID: ob.MC_FAB_PROD_ORD_H_ID,
                                        DYE_BT_CARD_H_ID: ob.DYE_BT_CARD_H_ID,
                                        LK_FBR_GRP_ID: (ob.LK_FBR_GRP_ID || ''),
                                        LK_DIA_TYPE_ID: (ob.LK_DIA_TYPE_ID || ''),
                                        RQD_GSM: (ob.RQD_GSM || ''),
                                        DYE_BT_CARD_GRP_ID: (ob.DYE_BT_CARD_GRP_ID || ''),

                                        DYE_BT_CARD_D_TRMS_ID: (ob.DYE_BT_CARD_D_TRMS_ID || ''),
                                        RQD_QTY_YDS: (ob.RQD_QTY_YDS || 0),
                                        RQD_QTY_KG: (ob.RQD_QTY_KG || 0),
                                        IS_BATCH_STORE: ob.IS_BATCH_STORE,

                                        NON_COL_CUF: config.xmlStringShortNoTagChild(ob.non_col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                            return {
                                                DYE_BT_CARD_D_FAB_ID: o.DYE_BT_CARD_D_FAB_ID,
                                                INV_ORD_GFAB_STK_ID: o.INV_ORD_GFAB_STK_ID,
                                                KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID,
                                                RQD_QTY: o.RCV_ROLL_WT,
                                                QTY_MOU_ID: 3,
                                                ASSIGN_QTY: o.ASSIGN_QTY,
                                                RQD_ROLL_QTY: o.RQD_ROLL_QTY,
                                                KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID
                                            }
                                        })),
                                        COL_CUF: config.xmlStringShortNoTagChild(ob.col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                            return {
                                                KNT_CLCF_STYL_ITEM_ID: o.KNT_CLCF_STYL_ITEM_ID,
                                                DYE_BT_CARD_D_CLCF_ID: o.DYE_BT_CARD_D_CLCF_ID,
                                                PRD_QTY: o.PRD_QTY,
                                                QTY_MOU_ID: 1,
                                                ASSIGN_QTY: o.ASSIGN_QTY,
                                                RQD_ROLL_QTY: o.RQD_ROLL_QTY
                                            }
                                        }))


                                    }

                                }))

                            });
                            // }

                        });


                    }

                });

            });

            if (data2save.length == 0) {

                console.log(data2save);
                return;
            } else {
                Dialog.confirm('Saving Batch data...<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     return DyeingDataService.saveDataByUrl({
                         XML: config.xmlStringShort(data2save),
                         XML_DF: config.xmlStringShort(DfProcessData)

                     }, '/DyeBatch/Save').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID);
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
            }




        }

        vm.createRequisition = function (dataOri) {
            var data2save = [];
            MC_OPEN = [];
            var pOTP_CODE = "";
            var pDYE_BATCH_NO = "";

            angular.forEach(dataOri, function (val0, key0) {
                angular.forEach(val0.dyePrograms, function (val1, key1) {

                    if (val1.IS_SELECTED_BT) {
                        console.log(val1);
                        pOTP_CODE = val1.OTP_CODE;
                        pDYE_BATCH_NO = val1.DYE_BATCH_NO_LST.split('(')[0];

                        if (MC_OPEN.indexOf(val0.DYE_MACHINE_ID) < 0) {
                            MC_OPEN.push(val0.DYE_MACHINE_ID);
                        }


                        if (data2save.indexOf({
                            DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID
                        }) < 0) {
                            if (val1.DYE_BATCH_NO_LST.length > 0) {
                                data2save.push({
                                    DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                                    IS_BATCH_STORE: (val1.IS_BATCH_STORE || 'N'),
                                    INV_ITEM_CAT_ID: (val1.INV_ITEM_CAT_ID || 34)
                                });
                            }
                        }
                    }
                });


            });

            console.log(data2save);

            if (data2save.length == 0) {
                config.appToastMsg('MULTI-003 No Valid Data found');
                return;
            } else {
                Dialog.confirm('Creating Grey Fabric Req for Batch?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.showSplash = true;
                     if (pOTP_CODE > 0) {
                         ////////// Start Projection Booking Approval Notification 

                         var submitData = { DYE_BATCH_NO: pDYE_BATCH_NO };
                         console.log(submitData);
                         return DyeingDataService.saveDataByUrl(submitData, '/DyeBatch/SendOTP').then(function (res) {
                             vm.showSplash = false;
                             
                             $ngConfirm({
                                 title: 'Prompt for OTP',
                                 contentUrl: 'form.html',
                                 buttons: {
                                     getPassCode: {
                                         text: 'OK',
                                         disabled: true,
                                         btnClass: 'btn-green',
                                         action: function (scope) {
                                             //$ngConfirm('Hello <strong>' + scope.passCode + '</strong>, i hope you have a great day!');

                                             if (scope.passCode == pOTP_CODE) {
                                                 return DyeingDataService.saveDataByUrl({
                                                     XML: config.xmlStringShort(data2save),
                                                     RF_REQ_TYPE_ID: 16,// 16 bulk;17 Sample
                                                     HR_COMPANY_ID: vm.HR_COMPANY_ID,
                                                     IS_BATCH_STORE: (_.filter(data2save, function (x) { return x.IS_BATCH_STORE == 'Y' })).length > 0 ? 'Y' : 'N',
                                                     PREFIX: 'RB-',
                                                     RF_ACTN_TYPE_ID: 5 // 6 Sample,5 Bulk
                                                 }, '/DyeBatch/CreateRequisition').then(function (res) {
                                                     if (res.success === false) {
                                                         vm.errors = res.errors;
                                                     }
                                                     else {
                                                         res['data'] = angular.fromJson(res.jsonStr);

                                                         if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                                             getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID);
                                                         }
                                                         config.appToastMsg(res.data.PMSG);
                                                     }
                                                 }).catch(function (message) {
                                                     exception.catcher('XHR loading Failded')(message);
                                                 });
                                             }
                                             else {

                                                 config.appToastMsg("MULTI-005 OTP does not match!!!");
                                                 return;
                                             }
                                         }
                                     },
                                     later: function () {
                                         return;
                                     }
                                 },
                                 onScopeReady: function (scope) {
                                     var self = this;
                                     scope.showPassword = false;

                                     scope.toggleShowPassword = function () {
                                         scope.showPassword = !scope.showPassword;
                                     }

                                     scope.textChange = function () {
                                         if (scope.passCode.length == 4)
                                             self.buttons.getPassCode.setDisabled(false);
                                         else
                                             self.buttons.getPassCode.setDisabled(true);
                                     }
                                 }
                             });
                         }).catch(function (message) {
                             exception.catcher('XHR loading Failded')(message);
                         });
                         
                     }
                     else {
                         return DyeingDataService.saveDataByUrl({
                             XML: config.xmlStringShort(data2save),
                             RF_REQ_TYPE_ID: 16,// 16 bulk;17 Sample
                             HR_COMPANY_ID: vm.HR_COMPANY_ID,
                             IS_BATCH_STORE: (_.filter(data2save, function (x) { return x.IS_BATCH_STORE == 'Y' })).length > 0 ? 'Y' : 'N',
                             PREFIX: 'RB-',
                             RF_ACTN_TYPE_ID: 5 // 6 Sample,5 Bulk
                         }, '/DyeBatch/CreateRequisition').then(function (res) {
                             if (res.success === false) {
                                 vm.errors = res.errors;
                             }
                             else {
                                 res['data'] = angular.fromJson(res.jsonStr);

                                 if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                     getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID);
                                 }
                                 config.appToastMsg(res.data.PMSG);
                             }
                         }).catch(function (message) {
                             exception.catcher('XHR loading Failded')(message);
                         });
                     }
                 });
            }
        }
        vm.printPendingLabdipRecepi = function (data) {
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/dye/dyereport/previewreportrdlc');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-4022' }, data);

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
        vm.remove = function (data) {

            Dialog.confirm('Removing Program ID# <b>' + data.DYE_BATCH_PLAN_ID + '</b><br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {

                     MC_OPEN = [];
                     MC_OPEN.push(data.DYE_MACHINE_ID);
                     return DyeingDataService.saveDataByUrl({
                         DYE_BATCH_PLAN_ID: data.DYE_BATCH_PLAN_ID
                     }, '/DyeBatch/Delete').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID);
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
        }

        ////////////// ======= Start For Grey Fabric Transfer ===========
        function checkPopUpClosed(win) {
           
            var timer = setInterval(function () {
                if (win.closed) {
                    clearInterval(timer);
                    $state.go($state.current.name, $stateParams, { reload: $state.current.name, inherit: true });
                }
            }, 1000);
        };

        vm.fabTransWin = function (data) {
            //console.log(data.parent());
            console.log(data);

            var url = '/Knitting/Knit/OrdWiseGreyTrnReq?a=359/#/ordWiseGreyTrnReqH/0/dtl?pLK_TRN_SRC_ID=726&pTO_MC_BYR_ACC_GRP_ID=' + data.MC_BYR_ACC_GRP_ID + '&pMC_STYLE_H_EXT_ID=' + data.MC_STYLE_H_EXT_ID + '&pRF_FAB_PROD_CAT_ID=' + data.RF_FAB_PROD_CAT_ID + '&pTO_MC_FAB_PROD_ORD_H_ID=' + data.MC_FAB_PROD_ORD_H_ID + '&pTO_MC_COLOR_ID=' + data.FAB_COLOR_ID;
            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 300) + ',scrollbars=yes,status=yes,modal=yes';
            var win = $window.open(url, "newwindow", opt);
            win.focus();
            checkPopUpClosed(win);

        };        
        ////////////// ======= End For Grey Fabric Transfer ===========

    }



})();