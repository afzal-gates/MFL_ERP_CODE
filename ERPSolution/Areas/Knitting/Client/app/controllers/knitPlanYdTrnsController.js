(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitPlanYdTrnsController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'KnitPlanHeader', 'Dialog', '$modal', '$modalInstance', KnitPlanYdTrnsController]);
    function KnitPlanYdTrnsController($q, config, KnittingDataService, $stateParams, $state, $scope, KnitPlanHeader, Dialog, $modal, $modalInstance) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        var YarnLotList = [];
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getMachineGaugeList(), getYarnItemList(), getYarnItemListYd(), getYarnPartList(), getFabricDetails(), getMcDiaList()
            ];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.yarnItemListDs = [null, null, null, null, null, null, null, null, null, null];
        vm.yarnItemListDsYd = [null, null, null, null, null, null, null, null, null, null];

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

        vm.rateTemplate = '<span><h5 style="padding:0px;margin:0px;"><b>#: data.FAB_PROC_NAME # </b> (#: data.FACT_PROD_RATE # BDT/Kg)</h5></span>';
        vm.RateDs = new kendo.data.DataSource({
            data: (KnitPlanHeader.rates || [])
        });

        vm.onRateOnBound = function (e, data) {
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

        function refreshData(pMC_FAB_PROD_ORD_D_ID, pRF_FAB_PROD_CAT_ID) {
            return KnittingDataService.getDataByUrl('/KnitPlan/getKnitPlanHeaderData?pMC_FAB_PROD_ORD_D_ID=' + (pMC_FAB_PROD_ORD_D_ID || $stateParams.pMC_FAB_PROD_ORD_D_ID) + '&pRF_FAB_PROD_CAT_ID=' + (pRF_FAB_PROD_CAT_ID || $stateParams.pRF_FAB_PROD_CAT_ID)).then(function (res) {
                $scope.data = res;
                $scope.data['IS_FINALIZED_SC'] = res.IS_FINALIZED;
                
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
            $state.go('KnitPlanYd', { pMC_FAB_PROD_ORD_D_ID: data.MC_FAB_PROD_ORD_D_ID,pRF_FAB_PROD_CAT_ID : data.RF_FAB_PROD_CAT_ID, state : 'FabProdKnitOrderYD', pCT_ID_LST: '360' }, { notify: false });
            refreshData(data.MC_FAB_PROD_ORD_D_ID);
        }

        function getFabricDetails() {
            var url = '/api/knit/FabProdKnitOrder/SelectByID?pMC_FAB_PROD_ORD_H_ID=' + KnitPlanHeader.MC_FAB_PROD_ORD_H_ID;
            url += '&pLK_COL_TYPE_ID_LST=' + ($stateParams.pCT_ID_LST || '358,361,407,432');

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


            });
        }

        function getYarnItemListYd() {
            var url = '/KnitPlan/getDyedYarnListForKp'
            url += '?pMC_FAB_PROD_ORD_H_ID=' + KnitPlanHeader.MC_FAB_PROD_ORD_H_ID;
            url += '&pSCM_SUPPLIER_ID=' + KnitPlanHeader.SCM_SUPPLIER_ID;

            return KnittingDataService.getDataByUrl(url).then(function (res) {

                vm.yarnItemListDsYd[0] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDsYd[1] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDsYd[2] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDsYd[3] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDsYd[4] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDsYd[5] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDsYd[6] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDsYd[7] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDsYd[8] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDsYd[9] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDsYd[10] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDsYd[11] = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnItemListDsYd[12] = new kendo.data.DataSource({
                    data: res
                });
            });
        }
        vm.onChangeYarnItem = function (item, e, base, index) {
            var obj = e.sender.dataItem(e.sender.item);
            if (obj.YARN_ITEM_ID) {
                item['RF_YRN_CNT_ID'] = obj.RF_YRN_CNT_ID;
                KnittingDataService.getDataByFullUrl('/api/common/GetCategoryWiseBrandList/2?pOption=3003&pKNT_YRN_LOT_ID_LST=' + obj.KNT_YRN_LOT_ID_LST + '&pIS_SOLID=' + (item.IS_SOLID||'S')).then(function (res) {
                    item['brandListDs'] = new kendo.data.DataSource({
                        data: res
                    });
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

        var YarnItmDs;
        vm.onChangeIsSolid = function (item, idx) {
            if (item.IS_SOLID == 'S') {

                YarnItmDs = angular.copy(vm.yarnItemListDsYd[idx]);
                vm.yarnItemListDsYd[idx] = vm.yarnItemListDs[idx];

            } else {
                vm.yarnItemListDsYd[idx] = YarnItmDs;
            }

        };


        vm.onBoundYarnItem = function (item, base, index) {
            if (item.KNT_YRN_LOT_ID_LST) {
                return KnittingDataService.getDataByFullUrl('/api/common/GetCategoryWiseBrandList/2?pOption=3003&pKNT_YRN_LOT_ID_LST=' + item.KNT_YRN_LOT_ID_LST + '&pIS_SOLID=' + (item.IS_SOLID || 'S')).then(function (res) {
                    item['brandListDs'] = new kendo.data.DataSource({
                        data: res
                    });
                });
            }
        }
        vm.onChangeBrnad = function (item, e, base, index) {
            var objBrand = e.sender.dataItem(e.sender.item);
            if (objBrand.RF_BRAND_ID) {
                return KnittingDataService.getDataByUrl('/KnitPlan/getYarnLot?pKNT_YRN_LOT_ID_LST=' + objBrand.KNT_YRN_LOT_ID_LST + '&pRF_BRAND_ID=' + objBrand.RF_BRAND_ID + '&pYARN_ITEM_ID=' + item.YARN_ITEM_ID + '&pIS_SOLID=' + (item.IS_SOLID || 'S') + '&pMC_FAB_PROD_ORD_H_ID=' + (KnitPlanHeader.MC_FAB_PROD_ORD_H_ID||'')+'&pSCM_SUPPLIER_ID=' + KnitPlanHeader.SCM_SUPPLIER_ID).then(function (res) {
                    item['yarnLotListDs'] = new kendo.data.DataSource({
                        data: res
                    });
                });

            } else {
                item.yarnLotListDs = new kendo.data.DataSource({
                    data: []
                });
            }
        }
        vm.onBoundBrnad = function (item, base, index) {
            if (item.YARN_ITEM_ID && item.KNT_YRN_LOT_ID_LST && item.RF_BRAND_ID) {
                return KnittingDataService.getDataByUrl('/KnitPlan/getYarnLot?pKNT_YRN_LOT_ID_LST=' + item.KNT_YRN_LOT_ID_LST + '&pRF_BRAND_ID=' + item.RF_BRAND_ID + '&pYARN_ITEM_ID=' + item.YARN_ITEM_ID + '&pIS_SOLID=' + (item.IS_SOLID || 'S') + '&pMC_FAB_PROD_ORD_H_ID=' + (KnitPlanHeader.MC_FAB_PROD_ORD_H_ID || '') + '&pSCM_SUPPLIER_ID=' + (KnitPlanHeader.SCM_SUPPLIER_ID||'')).then(function (res) {
                    item['yarnLotListDs'] = new kendo.data.DataSource({
                        data: res
                    });
                });
            }
        }
        vm.onChangeYarnLot = function (item, e, base, index) {
            var objLot = e.sender.dataItem(e.sender.item);
            if (objLot.KNT_YRN_LOT_ID) {
                item['STK_BALANCE'] = (objLot.STK_BALANCE || 0);
                item['KNT_YRN_LOT_STK_ID'] = objLot.KNT_YRN_LOT_STK_ID;
            } else {
                item['STK_BALANCE'] = 0;
                item['KNT_YRN_LOT_STK_ID'] = '';
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

                if (base) {
                    angular.forEach($scope.datas, function (val, key) {
                        if (val['items'][index]['KNT_PLAN_D_ID'] < 0) {
                            val['items'][index]['KNT_YRN_LOT_ID'] = item.KNT_YRN_LOT_ID;
                        }
                    });
                }
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

        vm.addToListKpFeed = function (data, list) {
            var d2push = {
                KNT_YD_PLAN_FDR_ID: -1,
                SL : (list.length+1),
                SOLID_COLOR_ID: angular.copy(data.SOLID_COLOR_ID),
                NO_OF_FDR: angular.copy(data.NO_OF_FDR),
                REMARKS: angular.copy(data.REMARKS),
            };
            
            list.push(d2push);
        };

        vm.addToList = function (data, list) {
            var d2push = {
                KNT_PLAN_D_ID: -1,

                brandListDs: angular.copy(data.brandListDs),
                YARN_ITEM_ID: data.YARN_ITEM_ID,
                RF_BRAND_ID: data.RF_BRAND_ID,
                yarnLotListDs: angular.copy(data.yarnLotListDs),
                STITCH_LEN: data.STITCH_LEN,
                RF_YRN_CNT_ID: data.RF_YRN_CNT_ID,
                IS_SOLID : 'Y'
            };
            list.push(d2push);
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




        function newKnitCard(obj) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Knitting/Knit/_KnitPlanJobCardYdTrns',
                controller: 'KnitPlanJobCardYdController as vm',
                resolve: {
                    $stateParams: function(){
                        return obj;
                    },
                    JobCardHeader: function () {
                            return KnittingDataService.getDataByUrl('/KnitPlan/getJobCardHeaderData?pKNT_PLAN_H_ID=' + obj.pKNT_PLAN_H_ID + '&pKNT_JOB_CRD_H_ID=' + (obj.pKNT_JOB_CRD_H_ID || null));
                    },
                    YarnLotList: function () {
                            return KnittingDataService.getDataByUrl('/KnitPlan/getYarnLotForJobCard?pKNT_PLAN_H_ID=' + obj.pKNT_PLAN_H_ID);
                    }
                },
                size: 'lg',
                windowClass: 'app-modal-window'
            });

            modalInstance.result.then(function (data) {
                $modalInstance.close(data);

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }



        vm.openProgramModal = function (yyyy) {
            newKnitCard({ pKNT_PLAN_H_ID: yyyy.KNT_PLAN_H_ID, pKNT_SC_PRG_ISS_ID: KnitPlanHeader.KNT_SC_PRG_ISS_ID, pIS_SUB_CONTR: 'Y' });
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



        vm.submitData = function (dataOri, valid, IS_REVISED) {
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

            var v1 = 0;
            angular.forEach(dataOri.items, function (val, key) {
                    
                if ( parseInt(val.YARN_ITEM_ID||0) <=0) {
                    val['YARN_ITEM_ID_V'] = false;
                    v1 += 1;
                    
                } else {
                    val['YARN_ITEM_ID_V'] = true;
                }

                if (parseInt(val.RF_BRAND_ID||0) <= 0) {
                    v1 += 1;
                    val['RF_BRAND_ID_V'] = false;
                } else {
                    val['RF_BRAND_ID_V'] = true;
                }

                if (parseInt(val.KNT_YRN_LOT_ID||0) <= 0) {
                    v1 += 1;
                    val['KNT_YRN_LOT_ID_V'] = false;
                } else {
                    val['KNT_YRN_LOT_ID_V'] = true;
                }

            });


            angular.forEach(dataOri.kp_feeders, function (val, key) {
                if ( parseInt(val.SOLID_COLOR_ID||0) <= 0) {
                    val['SOLID_COLOR_ID_V'] = false;
                    v1 += 1;
                } else {

                    val['SOLID_COLOR_ID_V'] = true;
                }

            });


            if (!valid || v1>0) {
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
            });

            if (_.some(angular.forEach(_.map(_.groupBy(data.items, function (o) {
                 return (o.YARN_ITEM_ID+o.KNT_YRN_LOT_ID);
            }), function (g) {
                 return {
                KNT_YRN_LOT_ID: parseInt(g[0].KNT_YRN_LOT_ID),
                RQD_YRN_QTY_NEW: _.sumBy(g, function (oo) { return oo.RQD_YRN_QTY_NEW }),
                STK_BALANCE: g[0].STK_BALANCE,
                IS_USE_WIP: (g[0].IS_USE_WIP||'N')
            }
            }
             ), function (val, key) {
                 if ((val.STK_BALANCE - val.RQD_YRN_QTY_NEW) < 0 && val.IS_USE_WIP=='N') {
                        val['VALID'] = false;
                    } else {
                        val['VALID'] = true;
                    }

            }), function (o) {
                 return o.VALID == false
            })) {
                $scope.alerts.push({
                    type: 'danger', msg: 'Required Yarn qty exceed stock.Please check and try again.'
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
                    IS_SOLID: (o.IS_SOLID || 'Y')
                }
            }));

            data['XML_FEED'] = KnittingDataService.xmlStringShort(data.kp_feeders.map(function (o) {
                return {
                    KNT_YD_PLAN_FDR_ID: o.KNT_YD_PLAN_FDR_ID,
                    KNT_PLAN_H_ID: o.KNT_PLAN_H_ID,
                    SL: o.SL,
                    SOLID_COLOR_ID: o.SOLID_COLOR_ID,
                    NO_OF_FDR: o.NO_OF_FDR,
                    PCT_VALUE: o.PCT_VALUE,
                    REMARKS: o.REMARKS,
                    LK_YFAB_PART_ID: (o.LK_YFAB_PART_ID || ''),
                    IS_SOLID_COL: (o.IS_SOLID || 'Y')
                }

            }));

            data['XML_TIMES'] = KnittingDataService.xmlStringShort(data.yd_repeats.map(function (o) {
                return {
                    KNT_YD_FDR_REPEAT_ID: o.KNT_YD_FDR_REPEAT_ID,
                    SL_LIST: o.SL_LIST,
                    NO_OF_REPEAT: o.NO_OF_REPEAT
                }

            }));




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
                return KnittingDataService.saveDataByUrl(data, '/KnitPlan/Save').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            refreshData(data.MC_FAB_PROD_ORD_D_ID, data.RF_FAB_PROD_CAT_ID);
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }



        function CalTotalFeeder(kp_feeders, yd_repeats) {
            $scope.ttl_feeder = 0;
            kp_feeders.forEach(function (ob) {
                var rptOb = yd_repeats.find(function (o) {
                    return o.SL_LIST ? o.SL_LIST.indexOf(ob.SL) > -1 : false;
                });
                if (rptOb && Object.keys(rptOb).length > 0) {
                    $scope.ttl_feeder += ((ob.NO_OF_FDR||0) * (rptOb.NO_OF_REPEAT||1));
                } else
                    $scope.ttl_feeder += (ob.NO_OF_FDR||0);

            });
        };

        $scope.$watch('data.kp_feeders', function (newVal, oldVal) {
            CalTotalFeeder(newVal, $scope.data.yd_repeats);
        }, true);

        $scope.$watch('data.yd_repeats', function (newVal, oldVal) {
            CalTotalFeeder($scope.data.kp_feeders,newVal);
        }, true);
    }

})();