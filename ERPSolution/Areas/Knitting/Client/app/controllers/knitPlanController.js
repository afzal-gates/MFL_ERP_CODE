(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitPlanController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'KnitPlanHeader', 'Dialog', '$modal', '$modalInstance', KnitPlanController]);
    function KnitPlanController($q, config, KnittingDataService, $stateParams, $state, $scope, KnitPlanHeader, Dialog, $modal, $modalInstance) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        var YarnLotList = [];
        activate()
        vm.showSplash = true;

        console.log(KnitPlanHeader);

        function activate() {
            var promise = [getMachineGaugeList(), getYarnItemList(), getYarnPartList(), getFabricDetails(), getMcDiaList(),
                getCollarCuffData(KnitPlanHeader.MC_FAB_PROD_ORD_H_ID, KnitPlanHeader.FAB_COLOR_ID, KnitPlanHeader.IS_FBP_VISIBLE, KnitPlanHeader.IS_FLAT_CIR, KnitPlanHeader.LK_FBR_GRP_ID)
            ];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.yarnItemListDs = [null, null, null, null, null, null, null, null, null, null, null, null, null, null, null];
        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };
        vm.template = '<span><h5 style="padding:0px;margin:0px;"><b>#: data.YRN_LOT_NO #</b></h5><p> Stock: #: data.STK_BALANCE # #: data.MOU_CODE #</p></span>';
        vm.Yarntemplate = '<span><span style="padding:0px;margin:0px;">#: data.ITEM_NAME_EN # #if (data.PCT_RATIO>0)  { #<span class="label label-sm label-danger" style="padding:0px;margin:0px;">Suggested</span># } # </span>';

        vm.value_template = '<span><h5 style="padding:0px;margin:0px;"><b>#: data.YRN_LOT_NO # </b> (#: data.STK_BALANCE # #: data.MOU_CODE #)</h5></span>';

        
        $scope.cancel = function () {
            $modalInstance.dismiss();
        };

        function getMcDiaList() {
            return KnittingDataService.getDataByUrl('/KnitCommon/getMachineDiaList').then(function (res) {
                vm.machineDiaDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        function guid() {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                  .toString(16)
                  .substring(1);
            }
            return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
        }



        vm.rateTemplate = '<span><h5 style="padding:0px;margin:0px;"><b>#: data.FAB_PROC_NAME # </b> (#: data.FACT_PROD_RATE # BDT/Kg)</h5></span>';
        vm.RateDs = new kendo.data.DataSource({
            data: (KnitPlanHeader.rates||[])
        });

        vm.onRateOnBound = function (e,data) {
            var ds = e.sender.dataSource.data();
            if (ds.length == 1) {
                e.sender.value(ds[0].MC_FAB_PROC_RATE_ID);
                data['MC_FAB_PROC_RATE_ID'] = ds[0].MC_FAB_PROC_RATE_ID;
            }
        };


        vm.diaTypeList = {
            transport: {
                read: function (e) {
                    return KnittingDataService.getDataByFullUrl('/api/common/LookupListData/67').then(function (res) {
                        e.success(res);
                    });
                }

            }
        };

        vm.mouList = {
            transport: {
                read: function (e) {
                    return KnittingDataService.getDataByFullUrl('/api/common/MOUList/Y').then(function (res) {
                        e.success(_.filter(res, function (o) {
                            return (o.RF_MOU_ID == 6 || o.RF_MOU_ID == 7 || o.RF_MOU_ID == 8 || o.RF_MOU_ID == 23 || o.RF_MOU_ID == 24)
                        }));
                    });
                }

            }
        };

        $scope.data = KnitPlanHeader;
        $scope.data['IS_FINALIZED_SC'] = KnitPlanHeader.IS_FINALIZED;



        vm.MC_FAB_PROD_ORD_D_ID_SEL = KnitPlanHeader.MC_FAB_PROD_ORD_D_ID;

        vm.params = $stateParams;
        vm.dtFormat = config.appDateFormat;
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.KNT_PLAN_DT_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.KNT_PLAN_DT_OPENED = true;
        };


        function getCollarCuffData(pMC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, IS_FBP_VISIBLE, IS_FLAT_CIR, LK_FBR_GRP_ID) {

            if (/*IS_FBP_VISIBLE == 'Y' ||*/ IS_FLAT_CIR == 'F' || LK_FBR_GRP_ID != 192) {
                $scope.datas = [];
            }
            else {
                return KnittingDataService.getDataByUrl('/KnitPlan/getKnitPlanCollarCuff?pMC_FAB_PROD_ORD_H_ID=' + (pMC_FAB_PROD_ORD_H_ID || KnitPlanHeader.MC_FAB_PROD_ORD_H_ID) + '&pFAB_COLOR_ID=' + (pFAB_COLOR_ID || KnitPlanHeader.FAB_COLOR_ID)).then(function (res) {
                    $scope.datas = res.map(function (o) {
                        o['RateDs'] = new kendo.data.DataSource({
                            data: (o.rates || [])
                        });
                        return o;
                    });

                    console.log($scope.datas);
                })
            }

            

        };


        function refreshData(pMC_FAB_PROD_ORD_D_ID, pRF_FAB_PROD_CAT_ID) {
            return KnittingDataService.getDataByUrl('/KnitPlan/getKnitPlanHeaderData?pMC_FAB_PROD_ORD_D_ID=' + (pMC_FAB_PROD_ORD_D_ID || $stateParams.pMC_FAB_PROD_ORD_D_ID) + '&pRF_FAB_PROD_CAT_ID=' + (pRF_FAB_PROD_CAT_ID || $stateParams.pRF_FAB_PROD_CAT_ID)).then(function (res) {
                $scope.data = res;
                console.log($scope.data);

                $scope.data['IS_FINALIZED_SC'] = res.IS_FINALIZED;
                getCollarCuffData(res.MC_FAB_PROD_ORD_H_ID, res.FAB_COLOR_ID, res.IS_FBP_VISIBLE, res.IS_FLAT_CIR, res.LK_FBR_GRP_ID);
            })
        };







        vm.onChangesameAsFabOrd = function (e) {
            var item = e.sender.dataItem(e.sender.item);

            if (item.VALUE) {
                return KnittingDataService.getDataByUrl('/KnitPlan/getKnitPlanHeaderData?pMC_FAB_PROD_ORD_D_ID=' + item.VALUE + '&pRF_FAB_PROD_CAT_ID=' + item.RF_FAB_PROD_CAT_ID).then(function (res) {
                    $scope.data['LK_MC_GG_ID'] = res.LK_MC_GG_ID;
                    //$scope.data['KNT_PLAN_DT'] = res.KNT_PLAN_DT;
                    $scope.data['KNT_MC_DIA_ID'] = res.KNT_MC_DIA_ID;
                    $scope.data['IS_PLAN_NOTE'] = res.IS_PLAN_NOTE;

                    $scope.data['items'] = [];

                    angular.forEach(res.items, function (val, key) {
                        $scope.data.items.push({
                            KNT_PLAN_D_ID: -1,
                            KNT_PLAN_H_ID: -1,
                            KNT_YRN_LOT_ID: val.KNT_YRN_LOT_ID,
                            KNT_YRN_LOT_ID_LST: val.KNT_YRN_LOT_ID_LST,

                            KNT_YRN_LOT_STK_ID: val.KNT_YRN_LOT_STK_ID,
                            LK_YFAB_PART_ID: val.LK_YFAB_PART_ID,

                            RATIO_QTY: val.RATIO_QTY,
                            RF_BRAND_ID: val.RF_BRAND_ID,
                            RQD_YRN_QTY: 0,
                            STITCH_LEN: val.STITCH_LEN,
                            YARN_ITEM_ID: val.YARN_ITEM_ID,
                            STK_BALANCE: val.STK_BALANCE
                        });

                    });
                    //$scope.data.items = res.items;

                })
            }
        };
        vm.onChangeFabCol = function (data) {

            //if (data.IS_FLAT_CIR == 'F') {
            //    return;
            //}
            vm.MC_FAB_PROD_ORD_D_ID_SEL = data.MC_FAB_PROD_ORD_D_ID;
            $state.go('KnitPlan', { pMC_FAB_PROD_ORD_D_ID: data.MC_FAB_PROD_ORD_D_ID }, { notify: false });
            refreshData(data.MC_FAB_PROD_ORD_D_ID);
        }

        function getFabricDetails() {
            var url = '/api/knit/FabProdKnitOrder/SelectByID?pMC_FAB_PROD_ORD_H_ID=' + KnitPlanHeader.MC_FAB_PROD_ORD_H_ID;
            url += '&pLK_COL_TYPE_ID_LST=' + ($stateParams.pCT_ID_LST || '358,361,407,432') + '&pRF_FAB_TYPE_ID=' + (($stateParams.state === 'FabProdKnitOrderFBR') ? 5 : null);

            return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                vm.FabColors = res;
                vm.sameAsFabOrdDs = _.filter(res, function (o) {
                    return o.KNT_PLAN_NO.length > 0 && o.IS_FLAT_CIR == 'C';
                }).map(function (o) {
                    return {
                        TEXT: o.COLOR_NAME_EN + ' - ' + o.FAB_TYPE_NAME + ' - ' + o.FIN_DIA_N_DIA_TYPE + ' - ' + o.KNT_PLAN_NO,
                        VALUE: o.MC_FAB_PROD_ORD_D_ID,
                        RF_FAB_PROD_CAT_ID: o.RF_FAB_PROD_CAT_ID
                    }
                });
            });
        }

        function getMachineGaugeList() {
            return KnittingDataService.LookupListData(59).then(function (res) {
                vm.MachineGaugeListDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }



        function getYarnItemList() {
            var url = '/KnitPlan/getYarnItemByFabId?pMC_STYLE_D_FAB_ID=' + KnitPlanHeader.MC_STYLE_D_FAB_ID;
            url += '&pRF_FAB_PROD_CAT_ID=' + KnitPlanHeader.RF_FAB_PROD_CAT_ID;
            url += '&pKNT_SC_PRG_RCV_ID=' + KnitPlanHeader.KNT_SC_PRG_RCV_ID;
            return KnittingDataService.getDataByUrl(url).then(function (res) {

                vm.yarnItemListDs[0] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDs[1] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDs[2] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDs[3] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDs[4] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDs[5] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDs[6] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDs[7] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDs[8] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDs[9] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDs[10] = new kendo.data.DataSource({
                    data: res
                });
                vm.yarnItemListDs[11] = new kendo.data.DataSource({
                    data: res
                });
                vm.yarnItemListDs[12] = new kendo.data.DataSource({
                    data: res
                });
                vm.yarnItemListDs[13] = new kendo.data.DataSource({
                    data: res
                });
                vm.yarnItemListDs[14] = new kendo.data.DataSource({
                    data: res
                });
            });
        }
        vm.onChangeYarnItem = function (item, e, base, index) {
            var obj = e.sender.dataItem(e.sender.item);
            if (obj.YARN_ITEM_ID) {
                item['RF_YRN_CNT_ID'] = obj.RF_YRN_CNT_ID;
                KnittingDataService.getDataByFullUrl('/api/common/GetCategoryWiseBrandList/2?pOption=3003&pKNT_YRN_LOT_ID_LST=' + obj.KNT_YRN_LOT_ID_LST + '&pYARN_ITEM_ID=' + obj.YARN_ITEM_ID).then(function (res) {
                    item['brandListDs'] = new kendo.data.DataSource({
                        data: res
                    });

                    if (base) {
                        angular.forEach($scope.datas, function (val, key) {
                            angular.forEach(val.items, function (v, k) {
                                if (v.hasOwnProperty('guid') && item.hasOwnProperty('guid')) {
                                    if (v.guid === item.guid) {
                                        v['YARN_ITEM_ID'] = obj.YARN_ITEM_ID;
                                        v['brandListDs'] = res;
                                    }
                                }
                            })

                        });
                    }

                });





            } else {
                item['brandListDs'] = new kendo.data.DataSource({
                    data: []
                });

                item['yarnLotListDs'] = new kendo.data.DataSource({
                    data: []
                });
                item['RF_YRN_CNT_ID'] = '';


            }
        };
        vm.onBoundYarnItem = function (item, base, index) {
            if (item.KNT_YRN_LOT_ID_LST) {
                console.log(item);
                return KnittingDataService.getDataByFullUrl('/api/common/GetCategoryWiseBrandList/2?pOption=3003&pKNT_YRN_LOT_ID_LST=' + item.KNT_YRN_LOT_ID_LST + '&pYARN_ITEM_ID=' + item.YARN_ITEM_ID).then(function (res) {
                    item['brandListDs'] = new kendo.data.DataSource({
                        data: res
                    });

                    //if (base && $scope.datas) {
                    //    angular.forEach($scope.datas, function (val, key) {
                    //        if ($scope.datas.length > key) {
                    //            if (val['items'][index]['KNT_PLAN_D_ID'] < 0) {
                    //                val['items'][index]['YARN_ITEM_ID'] = item.YARN_ITEM_ID;
                    //                val['items'][index]['brandListDs'] = res;
                    //            }
                    //        }

                    //    });
                    //}

                });
            }
        }
        vm.onChangeBrnad = function (item, e, base, index) {
            var objBrand = e.sender.dataItem(e.sender.item);
            if (objBrand.RF_BRAND_ID) {
                return KnittingDataService.getDataByUrl('/KnitPlan/getYarnLot?pKNT_YRN_LOT_ID_LST=' + objBrand.KNT_YRN_LOT_ID_LST + '&pRF_BRAND_ID=' + objBrand.RF_BRAND_ID + '&pYARN_ITEM_ID=' + item.YARN_ITEM_ID).then(function (res) {
                    item['yarnLotListDs'] = new kendo.data.DataSource({
                        data: res
                    });

                    if (base) {

                        angular.forEach($scope.datas, function (val, key) {
                            angular.forEach(val.items, function (v, k) {
                                if (v.hasOwnProperty('guid') && item.hasOwnProperty('guid')) {
                                    if (v.guid === item.guid) {
                                        v['RF_BRAND_ID'] = objBrand.RF_BRAND_ID;
                                        v['yarnLotListDs'] = new kendo.data.DataSource({
                                            data: res
                                        });;
                                    }
                                }
                            })

                        });
                    }

                });

            } else {
                item.yarnLotListDs = new kendo.data.DataSource({
                    data: []
                });
            }
        }
        vm.onBoundBrnad = function (item, base, index) {
            if (item.YARN_ITEM_ID && item.KNT_YRN_LOT_ID_LST && item.RF_BRAND_ID) {
                return KnittingDataService.getDataByUrl('/KnitPlan/getYarnLot?pKNT_YRN_LOT_ID_LST=' + item.KNT_YRN_LOT_ID_LST + '&pRF_BRAND_ID=' + item.RF_BRAND_ID + '&pYARN_ITEM_ID=' + item.YARN_ITEM_ID).then(function (res) {
                    item['yarnLotListDs'] = new kendo.data.DataSource({
                        data: res
                    });

                    //if (base) {
                    //    angular.forEach($scope.datas, function (val, key) {
                    //        if (val['items'][index]['KNT_PLAN_D_ID'] < 0) {
                    //            val['items'][index]['RF_BRAND_ID'] = item.RF_BRAND_ID;
                    //            val['items'][index]['yarnLotListDs'] = new kendo.data.DataSource({
                    //                data: res
                    //            });
                    //        }
                    //    });
                    //}

                });
            }
        }
        vm.onChangeYarnLot = function (item, e, base, index) {
            var objLot = e.sender.dataItem(e.sender.item);
            if (objLot.KNT_YRN_LOT_ID) {
                item['STK_BALANCE'] = (objLot.STK_BALANCE || 0);
                item['KNT_YRN_LOT_STK_ID'] = objLot.KNT_YRN_LOT_STK_ID;
                if (base) {

                    angular.forEach($scope.datas, function (val, key) {
                        angular.forEach(val.items, function (v, k) {
                            if (v.hasOwnProperty('guid') && item.hasOwnProperty('guid')) {
                                if (v.guid === item.guid) {
                                    v['KNT_YRN_LOT_ID'] = objLot.KNT_YRN_LOT_ID;
                                    v['KNT_YRN_LOT_STK_ID'] = objLot.KNT_YRN_LOT_STK_ID;
                                }
                            }
                        })
                    });
                }

            }
        }
        vm.onBoundYarnLot = function (item, base, index) {
            if (item.KNT_YRN_LOT_ID && item.KNT_YRN_LOT_ID > 0) {
                item['STK_BALANCE'] = _.find(item.yarnLotListDs.data(), function (o) {
                    return o.KNT_YRN_LOT_ID == item.KNT_YRN_LOT_ID;
                }) ? _.find(item.yarnLotListDs.data(), function (o) {
                    return o.KNT_YRN_LOT_ID == item.KNT_YRN_LOT_ID;
                }).STK_BALANCE : 0;


                item['KNT_YRN_LOT_STK_ID'] = _.find(item.yarnLotListDs.data(), function (o) {
                    return o.KNT_YRN_LOT_ID == item.KNT_YRN_LOT_ID;
                }) ? _.find(item.yarnLotListDs.data(), function (o) {
                    return o.KNT_YRN_LOT_ID == item.KNT_YRN_LOT_ID;
                }).KNT_YRN_LOT_STK_ID : '';

                //if (base) {
                //    angular.forEach($scope.datas, function (val, key) {
                //        if (val['items'][index]['KNT_PLAN_D_ID'] < 0) {
                            
                //            val['items'][index]['KNT_YRN_LOT_ID'] = item.KNT_YRN_LOT_ID;
                //            val['items'][index]['KNT_YRN_LOT_STK_ID'] = item.KNT_YRN_LOT_STK_ID;
                //        }
                //    });
                //}
            }


        }


        function calculateRatioQuantity() {
            var totalRatio = _.sumBy($scope.data.items, function (o) {
                return o.OP_RATIO_QTY;
            });

            angular.forEach($scope.data.items, function (val, key) {

                val['RATIO_QTY'] = (val.RATIO_QTY > 0 && val.KNT_PLAN_D_ID > 0) ? val.RATIO_QTY : parseFloat(((val.OP_RATIO_QTY / totalRatio) * 100).toFixed(2));
                val['RQD_YRN_QTY'] = (val.RQD_YRN_QTY > 0 && val.KNT_PLAN_D_ID > 0) ? val.RQD_YRN_QTY : parseFloat(((val.OP_RATIO_QTY / totalRatio) * ($scope.data.ASSIGN_QTY || 0)).toFixed(2));

            });
        };

        function calculateRatioQuantityLycra() {
            var totalRatio = _.sumBy($scope.data.items, function (o) {
                return o.RATIO_QTY;
            });
            angular.forEach($scope.data.items, function (val, key) {
                val['RQD_YRN_QTY'] = parseFloat(((val.RATIO_QTY / totalRatio) * ($scope.data.ASSIGN_QTY || 0)).toFixed(2));
            });
        };



        vm.onChangeYarnPart = function (data, e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.LOOKUP_DATA_ID) {
                var url = '/KnitPlan/getFleeceStitchLenRatio?pRF_FAB_TYPE_ID=' + KnitPlanHeader.RF_FAB_TYPE_ID + '&pLK_YFAB_PART_ID=' + item.LOOKUP_DATA_ID + '&pRF_YRN_CNT_ID=' + data.RF_YRN_CNT_ID;
                url += '&pSTITCH_LEN=0';
                return KnittingDataService.getDataByUrl(url).then(function (res) {
                    var result = angular.fromJson(res);

                    data['STITCH_LEN'] = parseFloat(result.OP_STITCH_LEN);
                    data['OP_RATIO_QTY'] = parseFloat(result.OP_RATIO);
                    calculateRatioQuantity();
                });

            }
        }

        vm.onQtyChange = function (data) {
            if (data.IS_ELA_MXD == 'Y' || data.RF_FAB_TYPE_ID == 33) {
                calculateRatioQuantityLycra();
            }

            if (data.IS_FBP_VISIBLE == 'Y') {
                calculateRatioQuantity();
            }
        };

        vm.onBlurAssignQty = function (item, base, index) {
            var obj = angular.copy(item);

            if (base) {
                angular.forEach($scope.datas, function (val, key) {
                    //if (val['items'][index]['KNT_PLAN_D_ID'] < 0) {
                        //alert('xx');
                        var vRatioQty = ($scope.data['RQD_GFAB_QTY'] / val['RQD_GFAB_QTY']).toFixed(5);
                        var vCollarCuffYranQty = Math.ceil(obj.RQD_YRN_QTY / vRatioQty);

                        if (vCollarCuffYranQty > val['RQD_GFAB_QTY']) {
                            val['items'][index]['RQD_YRN_QTY'] = val['RQD_GFAB_QTY'];
                        }
                        else {
                            val['items'][index]['RQD_YRN_QTY'] = vCollarCuffYranQty;
                        }
                    //}
                });
            }
            
        }

        vm.onChangeSL = function (item) {
            if (KnitPlanHeader.IS_FBP_VISIBLE == 'Y' && item.STITCH_LEN > 0 && item.LK_YFAB_PART_ID) {
                var url = '/KnitPlan/getFleeceStitchLenRatio?pRF_FAB_TYPE_ID=' + KnitPlanHeader.RF_FAB_TYPE_ID + '&pLK_YFAB_PART_ID=' + item.LK_YFAB_PART_ID + '&pRF_YRN_CNT_ID=' + item.RF_YRN_CNT_ID;
                url += '&pSTITCH_LEN=' + item.STITCH_LEN;
                return KnittingDataService.getDataByUrl(url).then(function (res) {
                    var result = angular.fromJson(res);
                    item['OP_RATIO_QTY'] = parseFloat(result.OP_RATIO);
                    calculateRatioQuantity();
                });
            }
        };


        function getYarnPartList() {
            return KnittingDataService.LookupListData(79).then(function (res) {
                vm.PartListDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        vm.addToList = function (data, list) {
            var l_guid = guid();
            var d2push = {
                KNT_PLAN_D_ID: -1,
                brandListDs: angular.copy(data.brandListDs),
                YARN_ITEM_ID: data.YARN_ITEM_ID,
                RF_BRAND_ID: data.RF_BRAND_ID,
                yarnLotListDs: angular.copy(data.yarnLotListDs),
                STITCH_LEN: data.STITCH_LEN,
                RF_YRN_CNT_ID: data.RF_YRN_CNT_ID,
                IS_SOLID: 'S',
                guid: l_guid
            };
            $scope.data.items.push(angular.copy(d2push));

            angular.forEach($scope.datas, function (val, key) {
                val['items'].push(angular.copy(d2push));
            });
           
        };

        vm.removeItem = function (list, idx) {
            if (idx > -1) {
                list.splice(idx, 1);
            }
        }

        vm.edit = function (item, index) {
            item['RQD_YRN_QTY_OLD'] = item.RQD_YRN_QTY;

            item['IDX'] = index;
            setTimeout(function () {
                $scope.form = angular.copy(item);
            }, 20)
        };

        vm.reset = function () {
            return refreshData();
        }

        vm.cancel = function () {

            $scope.form = {
                IDX: -1,
                KNT_PLAN_D_ID: -1,
                KNT_YRN_LOT_ID: '',
                LK_YFAB_PART_ID: '',
                RF_BRAND_ID: '',
                YARN_ITEM_ID: '',
                IS_SKIP_LOT: 'N'
            };

            $scope.YarnDetails.$setPristine();
            $scope.YarnDetails.$setUntouched();

        };

        vm.openProgramModal = function (yyyy) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ProgramModal.html',
                controller: function ($scope, config, $modalInstance, KnittingDataService) {
                    $scope.form = {};
                    $scope.today = new Date();
                    $scope.dtFormat = config.appDateFormat;

                    $scope.dateOptions = {
                        formatYear: 'yy',
                        startingDay: 6
                    };

                    $scope.form['SC_PRG_DT'] = new Date();;

                    KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                        $scope.supplierListDs = new kendo.data.DataSource({
                            data: res
                        });
                    });


                    $scope.RevisionDate_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.RevisionDate_LNopened = true;
                    }



                    $scope.save = function (data) {
                        data['IS_YD'] = 'N';
                        return KnittingDataService.saveDataByUrl(data, '/KnitPlan/SaveScProgram').then(function (res) {
                            if (res.success === false) {
                            }
                            else {
                                res['data'] = angular.fromJson(res.jsonStr);

                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    $modalInstance.close({
                                        KNT_SC_PRG_ISS_ID: parseInt(res.data.OP_KNT_SC_PRG_ISS_ID || 0)
                                    });
                                }
                                config.appToastMsg(res.data.PMSG);
                            }
                        });
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                },
                size: 'small',
                windowClass: 'large-Modal',
                //resolve: {
                //    orderData: function () {
                //        return data;
                //    }
                //}
            });

            modalInstance.result.then(function (dta) {

                if (dta.KNT_SC_PRG_ISS_ID > 0) {
                    $state.go('KnitPlanJobCard', { pKNT_PLAN_H_ID: yyyy.KNT_PLAN_H_ID, pKNT_SC_PRG_ISS_ID: dta.KNT_SC_PRG_ISS_ID, pIS_SUB_CONTR: 'Y' });
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };


        vm.editFabrication = function (data) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Knitting/Knit/_StyleDItemFabricationKnt',
                controller: 'StyleDItemFabKntController as vm',
                resolve: {
                    formData: function () {
                        return KnittingDataService.getDataByFullUrl('/api/mrc/StyleDItemFab/Select/' + data.MC_STYLE_D_FAB_ID);
                    },
                    MC_FAB_PROD_ORD_D_ID: function () {
                        return data.MC_FAB_PROD_ORD_D_ID;
                    }
                },
                size: 'l',
                windowClass: 'large-Modal',
            });

            modalInstance.result.then(function (data) {

                if (data.SUCCESS) {
                    if ($modalInstance) {
                        refreshData(KnitPlanHeader.MC_FAB_PROD_ORD_D_ID, KnitPlanHeader.RF_FAB_PROD_CAT_ID);
                        getFabricDetails();
                    } else {
                        $state.reload();
                    }
                }
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        };

        vm.submitData = function (dataOri, valid, IS_REVISED, valid2, CollarCuffOri, valid3) {
            var CollarCuff = CollarCuffOri || [];
            if (!dataOri.LK_MC_GG_ID) {
                $scope.KnitPlanParameter.LK_MC_GG_ID.$setValidity('required', false);
                $scope.KnitPlanParameter.KNT_MC_DIA_ID.$setValidity('required', false);
                return;
            }

            if (!dataOri.KNT_MC_DIA_ID) {
                $scope.KnitPlanParameter.LK_MC_GG_ID.$setValidity('required', false);
                $scope.KnitPlanParameter.KNT_MC_DIA_ID.$setValidity('required', false);
                return;
            }

            if (!valid || !valid2) {
                return;
            }

            var data = angular.copy(dataOri);
            

            var chkUnique = [];

            angular.forEach(data.items, function (val, key) {
                chkUnique.push(_.filter(dataOri.items, function (o) {
                    return parseInt(o.YARN_ITEM_ID) == parseInt(val.YARN_ITEM_ID)
                                   && parseInt(o.RF_BRAND_ID) == parseInt(val.RF_BRAND_ID)
                                   && parseInt(o.KNT_YRN_LOT_ID) == parseInt(val.KNT_YRN_LOT_ID)
                                   && (o.LK_YFAB_PART_ID || -1) == (val.LK_YFAB_PART_ID || -1)
                }).length > 1
                );
            });

            if (_.some(chkUnique, function (o) {
                  return o == true
            })) {
                $scope.alerts.push({
                    type: 'danger', msg: 'Duplication Found'
                });
                return;
            };


            angular.forEach(data.items, function (val, key) {
                val['RQD_YRN_QTY_NEW'] = (val.KNT_PLAN_D_ID <= 0 || !val.KNT_PLAN_D_ID) ? val.RQD_YRN_QTY : (val.RQD_YRN_QTY - val.RQD_YRN_QTY_OLD);
                val['UN_ASSIGN_QTY'] =  ( val.RQD_YRN_QTY_OLD - val.RQD_YRN_QTY) < 1 ? 0:( val.RQD_YRN_QTY_OLD - val.RQD_YRN_QTY);
            });

            
            var clcfItmLotQty = 0;
            if (_.some(angular.forEach(_.map(_.groupBy(data.items, function (o) {
                 return (o.YARN_ITEM_ID + o.KNT_YRN_LOT_ID);
            }), function (g) {
                angular.forEach($scope.datas, function (val, key) {
                    var dataClcf = angular.copy(val);
                    clcfItmLotQty = 0;

                    angular.forEach(dataClcf['items'], function (val1, key1) {
                        val1['RQD_YRN_QTY_NEW'] = (val1.KNT_PLAN_D_ID <= 0 || !val1.KNT_PLAN_D_ID) ? val1.RQD_YRN_QTY : (val1.RQD_YRN_QTY - val1.RQD_YRN_QTY_OLD);
                        val1['UN_ASSIGN_QTY'] = (val1.RQD_YRN_QTY_OLD - val1.RQD_YRN_QTY) < 1 ? 0 : (val1.RQD_YRN_QTY_OLD - val1.RQD_YRN_QTY);
                        
                        if (val1['YARN_ITEM_ID'] == g[0].YARN_ITEM_ID && val1['KNT_YRN_LOT_ID'] == g[0].KNT_YRN_LOT_ID) {
                            //console.log(val1);
                            clcfItmLotQty = val1['RQD_YRN_QTY_NEW'];
                        }   
                    });
                });
                 return {
                    KNT_YRN_LOT_ID: parseInt(g[0].KNT_YRN_LOT_ID),
                    MIN_KP_ASSIGN_KGS: g[0].MIN_KP_ASSIGN_KGS,
                    RQD_YRN_QTY_NEW: _.sumBy(g, function (oo) { return oo.RQD_YRN_QTY_NEW }),
                    UN_ASSIGN_QTY: _.sumBy(g, function (oo) { return oo.UN_ASSIGN_QTY }),
                    CLCF_ITM_LOT_QTY: clcfItmLotQty, 
                    STK_BALANCE: g[0].STK_BALANCE,
                    IS_USE_WIP: (g[0].IS_USE_WIP || 'N')
                }
            }), function (val, key) {
                console.log((val.RQD_YRN_QTY_NEW + val.CLCF_ITM_LOT_QTY));
                console.log(val.STK_BALANCE);

                if ((val.STK_BALANCE - (val.RQD_YRN_QTY_NEW + val.CLCF_ITM_LOT_QTY)) < 0 && val.IS_USE_WIP == 'N' || (val.UN_ASSIGN_QTY > val.MIN_KP_ASSIGN_KGS)) {
                        val['VALID'] = false;
                }
                else {
                        val['VALID'] = true;
                }

            }), function (o) {
                 return o.VALID == false
            })) {
                $scope.alerts.push({
                    type: 'danger', msg: 'Required Yarn qty exceed stock/Unauthorized de-allocation; having requisition made. Please check and try again.'
                });
                return;
           }


            data['XML'] = KnittingDataService.xmlStringShort(data.items.map(function (o) {
                return {
                    KNT_PLAN_D_ID: o.KNT_PLAN_D_ID || -1,
                    KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID,
                    LK_YFAB_PART_ID: o.LK_YFAB_PART_ID || '',
                    RF_BRAND_ID: o.RF_BRAND_ID,
                    RQD_YRN_QTY: o.RQD_YRN_QTY,
                    STITCH_LEN: o.STITCH_LEN,
                    YARN_ITEM_ID: o.YARN_ITEM_ID,
                    YRN_NOTE: o.YRN_NOTE,
                    IS_SKIP_LOT: 'N',
                    RATIO_QTY: (o.RATIO_QTY || 100),
                    KNT_YRN_LOT_STK_ID: o.KNT_YRN_LOT_STK_ID,
                    ALOC_QTY: (o.KNT_PLAN_D_ID <= 0 || !o.KNT_PLAN_D_ID) ? o.RQD_YRN_QTY : (o.RQD_YRN_QTY - o.RQD_YRN_QTY_OLD),
                    ALOC_QTY_ED: o.ALOC_QTY,
                    IS_DBL_PLY: (o.IS_DBL_PLY || 'N'),
                    IS_USE_WIP: (o.IS_USE_WIP || 'N'),
                    IS_SOLID: (o.IS_SOLID || 'S'),
                    NO_OF_FDR: (o.NO_OF_FDR||0)
                }

            }));

            if (CollarCuff.length > 0) {
                angular.forEach(CollarCuff, function (val, key) {
                    data['COLL_CUFF_XML'] = config.xmlStringShort(CollarCuff.map(function (o) {
                        return {
                            KNT_PLAN_H_ID: o.KNT_PLAN_H_ID || -1,
                            MC_FAB_PROD_ORD_D_ID: o.MC_FAB_PROD_ORD_D_ID,
                            KNT_MC_DIA_ID: data.KNT_MC_DIA_ID,
                            LK_MC_GG_ID: data.LK_MC_GG_ID,
                            RQD_GFAB_QTY: o.RQD_GFAB_QTY,
                            QTY_MOU_ID: 3,
                            PLAN_NOTE: data.PLAN_NOTE,
                            IS_FINALIZED: data.IS_FINALIZED,

                            FIN_DIA: data.FIN_DIA,
                            DIA_MOU_ID: data.DIA_MOU_ID,
                            LK_DIA_TYPE_ID: data.LK_DIA_TYPE_ID,
                            FIN_GSM: data.FIN_GSM,
                            MC_FAB_PROC_RATE_ID: o.MC_FAB_PROC_RATE_ID,

                            ITEMS_XML: config.xmlStringShortNoTag(o.items.map(function (oo) {
                                console.log('===== Collar/Cuff =====');
                                console.log(oo);
                                
                                var vClcfAlocQty = (oo.KNT_PLAN_D_ID <= 0 || !oo.KNT_PLAN_D_ID) ? oo.RQD_YRN_QTY : (oo.RQD_YRN_QTY - oo.RQD_YRN_QTY_OLD);
                                console.log(vClcfAlocQty);

                                return {
                                    KNT_PLAN_D_ID: oo.KNT_PLAN_D_ID || -1,
                                    KNT_YRN_LOT_ID: oo.KNT_YRN_LOT_ID,
                                    LK_YFAB_PART_ID: oo.LK_YFAB_PART_ID || '',
                                    RF_BRAND_ID: oo.RF_BRAND_ID,
                                    RQD_YRN_QTY: oo.RQD_YRN_QTY,
                                    STITCH_LEN: oo.STITCH_LEN,
                                    YARN_ITEM_ID: oo.YARN_ITEM_ID,
                                    YRN_NOTE: oo.YRN_NOTE,
                                    IS_SKIP_LOT: 'N',
                                    RATIO_QTY: (oo.RATIO_QTY || 100),
                                    KNT_YRN_LOT_STK_ID: oo.KNT_YRN_LOT_STK_ID,
                                    ALOC_QTY: vClcfAlocQty, //(oo.KNT_PLAN_D_ID <= 0 || !oo.KNT_PLAN_D_ID) ? oo.RQD_YRN_QTY : (oo.RQD_YRN_QTY - oo.RQD_YRN_QTY_OLD),
                                    ALOC_QTY_ED: oo.ALOC_QTY,
                                    IS_USE_WIP: (oo.IS_USE_WIP || 'N'),
                                    IS_DBL_PLY: (oo.IS_DBL_PLY || 'N'),
                                    IS_SOLID: (oo.IS_SOLID || 'S'),
                                    NO_OF_FDR: (oo.NO_OF_FDR || 0)
                                }

                            })
                            )
                        }

                    }));
                });
            } else {
                data['COLL_CUFF_XML'] = '';
            }

            data['IS_FINALIZED'] = data.IS_FINALIZED_SC;
            data['IS_REVISED'] = IS_REVISED;

            if (IS_REVISED == 'Y') {
                Dialog.confirm('Do you really want to revise knit plan?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     return KnittingDataService.saveDataByUrl(data, '/KnitPlan/Save').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                 if ($modalInstance) {
                                     $modalInstance.close({ KNT_PLAN_H_ID: parseInt(res.data.OP_KNT_PLAN_H_ID), MC_FAB_PROD_ORD_D_ID: data.MC_FAB_PROD_ORD_D_ID, RF_FAB_PROD_CAT_ID: data.RF_FAB_PROD_CAT_ID });
                                     return;
                                 } else {
                                     refreshData();
                                     getFabricDetails();
                                 }
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });

                 }, function () {
                     return;
                 });
            } else if (IS_REVISED == 'N') {

                console.log(data);

                return KnittingDataService.saveDataByUrl(data, '/KnitPlan/Save').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                            if ($modalInstance) {
                                $modalInstance.close({ KNT_PLAN_H_ID: parseInt(res.data.OP_KNT_PLAN_H_ID), MC_FAB_PROD_ORD_D_ID: data.MC_FAB_PROD_ORD_D_ID, RF_FAB_PROD_CAT_ID: data.RF_FAB_PROD_CAT_ID });
                                return;
                            } else {
                                refreshData();
                                getFabricDetails();
                            }
                            if (((data.KNT_PLAN_H_ID || 0) < 1 && CollarCuff.length > 0) || ( (data.KNT_PLAN_H_ID || 0) < 1 && (data.KNT_PLAN_H_ID || 0) < 0 && (data.RF_FAB_TYPE_ID == 15 || data.RF_FAB_TYPE_ID == 20 || data.RF_FAB_TYPE_ID == 21))) {
                                KnittingDataService.saveDataByFullUrl({}, '/Home/sendCollarCuffKpGenMail?pKNT_PLAN_H_ID=' + res.data.OP_KNT_PLAN_H_ID).then(function (res) { });
                            }

                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }

        vm.createStoreRequisition = function (data, url) {
            var idx = _.findIndex(data.items, function (o) { return o.RQD_CONE_QTY < 1 && o.IS_USE_WIP=='N'; });
            for (var i = 0; i < data.items.length; i++) {
                data.items[i]['RQD_CONE_QTY_REQ'] = false;
            }

            if (idx > -1) {
                data.items[idx]['RQD_CONE_QTY_REQ'] = true;
                return;
            };
            
        Dialog.confirm('Do you really want to submit Yarn Requisition?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
            .then(function () {
                var xml = config.xmlStringShort([{
                    KNT_PLAN_H_ID: data.KNT_PLAN_H_ID,

                    XML: config.xmlStringShortNoTag(data.items.map(function (o) {
                        return {
                            YARN_ITEM_ID: o.YARN_ITEM_ID,
                            LK_YFAB_PART_ID: o.LK_YFAB_PART_ID || '',
                            RF_BRAND_ID: o.RF_BRAND_ID,
                            KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID,
                            RQD_YRN_QTY: o.RQD_YRN_QTY,
                            KNT_PLAN_D_ID: o.KNT_PLAN_D_ID,
                            RQSTD_NEW: o.RQSTD_NEW,
                            KNT_YRN_STR_REQ_D_ID: -1,
                            RQD_CONE_QTY: o.RQD_CONE_QTY,
                            IS_SOLID: o.IS_SOLID
                        }
                    }))
                }]);
                return KnittingDataService.saveDataByUrl({ XML: xml, KNT_YRN_STR_REQ_H_ID: -1, IS_EDIT_MODE: 'N', IS_SUBMIT: 'Y' }, '/KnitPlan/'+ url).then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        refreshData();
                        getFabricDetails();
                    }
                    config.appToastMsg(res.data.PMSG);
                });
            });





        };
    }

})();