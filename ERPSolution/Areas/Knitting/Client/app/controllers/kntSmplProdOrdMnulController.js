//=============== Start for Fabric Color & KP =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntSmplProdOrdMnulController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modal', KntSmplProdOrdMnulController]);
    function KntSmplProdOrdMnulController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, $filter, commonDataService, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        var brandListData = [];
        var fabPartListData = [];
        var colGrpListData = [];

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';

        vm.form = { MC_FAB_PROD_ORD_H_ID: -1, RF_FAB_PROD_CAT_ID: -1, MC_BYR_ACC_ID: 0, MC_STYLE_H_ID: 0, MC_STYLE_H_EXT_ID: '', PROD_ORD_DT: $filter('date')(vm.today, vm.dtFormat) };
        vm.fabOrder = { MC_FAB_PROD_ORD_D_ID: -1, MC_FAB_PROD_ORD_H_ID: -1, FIN_DIA_N_DIA_TYPE: '', DIA_MOU_CODE: 'Inch', FIN_DIA_TYPE: 'Open', QTY_MOU_CODE: 'Kg' };

       
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getProdTypeList(), /*getBuyerAcList(),*/ getBuyerAcGrpList(), getByrAccWiseStyleExtList(), getFabricByStyleId(), getFabTypeList(), getFabricCompList(),
                getMOUList(), getDiaTypeList(), getColorList(), getMcDiaList(), getMcGgList()
            ];
            return $q.all(promise).then(function () {
                vm.showSplash = false;                
            });
        }

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.PROD_ORD_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.PROD_ORD_DT_LNopened = true;
        }

        
        $scope.$watchGroup(['vm.form.RF_FAB_PROD_CAT_ID', 'vm.form.MC_BYR_ACC_ID', 'vm.form.MC_STYLE_H_EXT_ID'], function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    vm.IS_NEXT = 'N';
                }
            }
        }, true);
        
        function getProdTypeList() {
            vm.prodTypeList = {
                optionLabel: "-- Select Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetFabProdCat').then(function (res) {
                                e.success(_.filter(res, function (ob) { return ob.FAB_PROD_CAT_CODE == 'FP01' || ob.FAB_PROD_CAT_CODE == 'FP03' || ob.FAB_PROD_CAT_CODE == 'FP05'; }));
                            });
                        }
                    }
                },                
                dataTextField: "FAB_PROD_CAT_SNAME",
                dataValueField: "RF_FAB_PROD_CAT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.LK_FAB_QTY_SRC_ID = ((item.FAB_PROD_CAT_CODE == 'FP01') ? 434 : 435);
                }//,
                //dataBound: function (e) {
                //    if ($stateParams.pRF_FAB_PROD_CAT_ID != null && $stateParams.pRF_FAB_PROD_CAT_ID > 0) {
                //        vm.form.RF_FAB_PROD_CAT_ID = $stateParams.pRF_FAB_PROD_CAT_ID;
                //    }
                //}
            };
        }

        function getBuyerAcList() {

            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    $stateParams.pMC_BYR_ACC_ID = item.MC_BYR_ACC_ID;
                    vm.form.MC_BYR_ACC_ID = item.MC_BYR_ACC_ID;
                    vm.form.MC_STYLE_H_ID = 0;

                    vm.styleExtDataSource.read();
                    vm.styleFabricDataSource.read();
                    
                },
                //dataBound: function (e) {
                //    if ($stateParams.pMC_BYR_ACC_ID != null && $stateParams.pMC_BYR_ACC_ID > 0) {
                //        vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;

                //        vm.styleExtDataSource.read();
                //        //vm.styleFabricDataSource.read();
                //        //vm.getOrderByBuyerAC($stateParams.pMC_BYR_ACC_ID);
                //    }
                //},
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }
        
        function getBuyerAcGrpList() {

            return vm.buyerAcGrpList = {
                optionLabel: "--- Select Buyer Group ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    $stateParams.pMC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.MC_STYLE_H_ID = 0;

                    vm.styleExtDataSource.read();
                    vm.styleFabricDataSource.read();

                },
                //dataBound: function (e) {
                //    if ($stateParams.pMC_BYR_ACC_ID != null && $stateParams.pMC_BYR_ACC_ID > 0) {
                //        vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;

                //        vm.styleExtDataSource.read();
                //        //vm.styleFabricDataSource.read();
                //        //vm.getOrderByBuyerAC($stateParams.pMC_BYR_ACC_ID);
                //    }
                //},
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }

        function getByrAccWiseStyleExtList() {
            vm.styleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.form.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID;

                    vm.styleFabricDataSource.read();
                }//,
                //dataBound: function (e) {
                //    if ($stateParams.pMC_STYLE_H_EXT_ID != null && $stateParams.pMC_STYLE_H_EXT_ID > 0) {
                //        vm.form.MC_STYLE_H_EXT_ID = $stateParams.pMC_STYLE_H_EXT_ID;

                //        vm.styleFabricDataSource.read();
                //    }
                //}
            };

            return vm.styleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0); //pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0);
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
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

        function getFabricByStyleId() {

            vm.styleFabricOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FABRIC_DESC",
                dataValueField: "MC_STYLE_D_FAB_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.fabOrder.FABRIC_DESC = item.FABRIC_DESC;
                    vm.fabOrder.RF_FAB_TYPE_ID = item.RF_FAB_TYPE_ID;
                    vm.fabOrder.RF_FIB_COMP_ID = item.RF_FIB_COMP_ID;
                    vm.fabOrder.FIN_GSM = item.FB_WT_MAX;
                }
            };

            return vm.styleFabricDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/mrc/StyleDItemFab/SelectFabByStyleID/' + ((vm.form.MC_STYLE_H_ID > 0) ? vm.form.MC_STYLE_H_ID : 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
                        
        };



        function getFabTypeList() {
            vm.fabTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FAB_TYPE_SNAME",
                dataValueField: "RF_FAB_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.fabOrder.FAB_TYPE_SNAME = item.FAB_TYPE_SNAME;
                }
            };

            return vm.fabTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Common/FabricTypeList?pINV_ITEM_CAT_ID=34').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getFabricCompList() {
            vm.fibCompOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FIB_COMP_NAME",
                dataValueField: "RF_FIB_COMP_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.fabOrder.FIB_COMP_NAME = item.FIB_COMP_NAME;
                    
                    if (this.dataItem(e.item).RF_FIB_COMP_ID == -1) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'FabricCompositionGeneration.html',
                            controller: function (FiberCompList, FiberTypeList, FiberCompGroup, $modalInstance, $scope, $timeout, config) {
                                var vm = this;
                                vm.form = {};
                                vm.errors = [];
                                vm.FiberCompositionTypeList = FiberTypeList.map(function (o) {
                                    return {
                                        LOOKUP_DATA_ID: o.LOOKUP_DATA_ID,
                                        LK_DATA_NAME_EN: o.LK_DATA_NAME_EN
                                    }
                                });

                                vm.form['FIB_COMP_NAME'] = '';
                                vm.formDisabled = false;


                                vm.FeederTypeList = {
                                    optionLabel: "--Fiber Group--",
                                    filter: "contains",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                e.success(FiberCompGroup);
                                            }
                                        }
                                    },
                                    select: function (e) {
                                        var FabGroupOb = [];
                                        var FIB_TYPE_ID_LST = this.dataItem(e.item).FIB_TYPE_ID_LST;
                                        if (FIB_TYPE_ID_LST) {
                                            FIB_TYPE_ID_LST.split(',').forEach(function (val) {

                                                FabGroupOb.push({
                                                    LOOKUP_DATA_ID: parseInt(val),
                                                    LK_DATA_NAME_EN: _.filter(FiberTypeList, function (o) {
                                                        return o.LOOKUP_DATA_ID == parseInt(val)
                                                    })[0].LK_DATA_NAME_EN
                                                });
                                            });
                                        }
                                        vm.form['LK_FIB_TYPE_LST'] = FabGroupOb;
                                    },
                                    dataTextField: "RF_FIB_COMP_GRP_NAME",
                                    dataValueField: "RF_FIB_COMP_GRP_ID"
                                };
                                


                                $scope.$watch('vm.form.LK_FIB_TYPE_LST', function (newValOri, oldVal) {
                                    if (newValOri && newValOri.length > 0) {
                                        var newVal = angular.copy(newValOri);
                                        vm.form['FIB_COMP_NAME'] = '';

                                        newVal.map(function (o) {
                                            o.PERCENT = o.PERCENT || 0;
                                            return o;
                                        });

                                        var pecentValue = _.sumBy(newVal, function (o) {
                                            return o.PERCENT;
                                        });

                                        var isFillUpData = _.every(newVal, function (o) {
                                            return o.PERCENT > 0;
                                        })


                                        //if (isFillUpData) {
                                        //    var dataToBeCheck = [];
                                        //    FiberCompList.forEach(function (o) {
                                        //        dataToBeCheck.push(config.parseXmlString(o.LK_FIB_TYPE_LST).map(function (ob) {
                                        //            return {
                                        //                LOOKUP_DATA_ID: parseInt(ob.LOOKUP_DATA_ID),
                                        //                PERCENT: parseInt(ob.PERCENT)
                                        //            }
                                        //        }));

                                        //    });

                                        //    var isAvailable = _.some(dataToBeCheck, function (o) {

                                        //        return angular.equals(o, newVal.map(function (ob) {
                                        //            return {
                                        //                LOOKUP_DATA_ID: parseInt(ob.LOOKUP_DATA_ID),
                                        //                PERCENT: parseInt(ob.PERCENT)
                                        //            }
                                        //        }));

                                        //    });




                                        //}

                                        //if (isAvailable && isFillUpData) {

                                        //    $timeout(function () {
                                        //        vm.errors = [['Duplicate Composition is not allowed. Please try another']];
                                        //        vm.formDisabled = true;
                                        //    })

                                        //};

                                        if (pecentValue < 100 && isFillUpData) {
                                            vm.errors = [['Wrong Percent Value !!!. Percent Value must be 100 in total.']];
                                            vm.formDisabled = true;
                                        };


                                        if (pecentValue == 100 && isFillUpData) {

                                            vm.formDisabled = false;
                                            vm.errors = [];

                                        };

                                        if (pecentValue > 100 && isFillUpData) {
                                            vm.formDisabled = true;
                                            vm.errors = [['Wrong Percent Value !!!. Percent Value must be 100 in total.']];
                                        };


                                        newVal.forEach(function (val, key) {
                                            vm.form['FIB_COMP_NAME'] += ' ' + val.PERCENT + '% ' + val.LK_DATA_NAME_EN;
                                        });
                                        vm.form['FIB_COMP_NAME'] = vm.form['FIB_COMP_NAME'].replace('null', '').trim();
                                    } else {
                                        vm.form['FIB_COMP_NAME'] = '';
                                    }

                                }, true);


                                vm.cancel = function (data) {
                                    $modalInstance.close(data);
                                };
                                vm.submitData = function (dataOri, token) {
                                    var data = angular.copy(dataOri);


                                    data['IS_ELA_MXD'] = (
                                        _.some(data.LK_FIB_TYPE_LST, function (o) {
                                            return o.LOOKUP_DATA_ID == 375;
                                        })
                                        ) ? 'Y' : 'N';


                                    data['LK_FIB_TYPE_LST'] = config.xmlStringLong(
                                             data.LK_FIB_TYPE_LST.map(function (o) {
                                                 return {
                                                     LOOKUP_DATA_ID: o.LOOKUP_DATA_ID,
                                                     PERCENT: o.PERCENT
                                                 }
                                             })
                                         );

                                    return KnittingDataService.saveDataByFullUrl(data, '/api/mrc/StyleDItemFab/SaveFiberComposition', token).then(function (res) {
                                        if (res.success === false) {
                                            vm.errors = res.errors;
                                        }
                                        else {
                                            res['data'] = angular.fromJson(res.jsonStr);
                                            config.appToastMsg(res.data.PMSG);

                                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                                vm.form['RF_FIB_COMP_ID'] = parseInt(res.data.V_RF_FIB_COMP_ID);
                                            }
                                        }
                                    }).catch(function (message) {
                                        exception.catcher('XHR loading Failded')(message);
                                    });

                                };


                            },
                            controllerAs: 'vm',
                            size: 'lg',
                            windowClass: 'large-Modal',
                            resolve: {
                                FiberCompList: function () {
                                    return vm.fibCompDataSource.data();
                                },
                                FiberTypeList: function () {
                                    return KnittingDataService.LookupListData(76);
                                },
                                FiberCompGroup: function () {
                                    return KnittingDataService.getDataByFullUrl('/api/mrc/YarnSpec/FiberCompGroup');
                                }
                            }
                        });

                        modalInstance.result.then(function (result) {

                            if (result.RF_FIB_COMP_ID && result.RF_FIB_COMP_ID > 0) {
                                vm.fibCompDataSource.read().then(function () {
                                    vm.fabOrder['RF_FIB_COMP_ID'] = result.RF_FIB_COMP_ID;
                                });
                            }

                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });


                    }

                }                            
            };

            return vm.fibCompDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Common/FibreCompList').then(function (res) {
                            e.success([{ 'FIB_COMP_NAME': '---New Composition---', RF_FIB_COMP_ID: -1 }].concat(res));
                        });
                    }

                }
            });

        };
        
        function getMOUList() {
            vm.diaMouOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {                    
                    var item = this.dataItem(e.item);
                    vm.fabOrder.DIA_MOU_CODE = item.MOU_CODE;
                },
                dataBound: function (e) {
                    vm.fabOrder['DIA_MOU_ID'] = 23;
                }
            };

            vm.qtyMouOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.fabOrder.QTY_MOU_CODE = item.MOU_CODE;
                },
                dataBound: function (e) {
                    vm.fabOrder['QTY_MOU_ID'] = 3;
                }
            };

            return KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                
                vm.diaMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });

                
                vm.qtyMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });
            });
            
        };

        function getDiaTypeList() {
            vm.diaTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {                    
                    var item = this.dataItem(e.item);
                    vm.fabOrder.FIN_DIA_TYPE = item.LK_DATA_NAME_EN;
                },
                dataBound: function (e) {
                    vm.fabOrder['LK_DIA_TYPE_ID'] = 327;
                }
            };

            return vm.diaTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/LookupListData/67').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getColorList() {
            vm.colorListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.fabOrder.COLOR_NAME_EN = item.COLOR_NAME_EN;
                    vm.fabOrder.LK_COL_TYPE_ID = item.LK_COL_TYPE_ID;
                    

                    if (item.MC_COLOR_ID < 0) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'NewColourEntryModal.html',
                            controller: 'ColourMasterModalController',
                            controllerAs: 'vm',
                            size: 'lg',
                            windowClass: 'large-Modal',                            
                            resolve: {
                                ColourList: function (KnittingDataService) {
                                    return KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll');
                                }
                            }
                        });

                        modalInstance.result.then(function (result) {
                            if (result.MC_COLOR_ID && result.MC_COLOR_ID > 0) {
                                vm.colorListDataSource.read().then(function () {
                                    vm.fabOrder['FAB_COLOR_ID'] = result.MC_COLOR_ID;
                                    vm.fabOrder['COLOR_NAME_EN'] = result.COLOR_NAME_EN;
                                });
                            }
                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });
                    }
                }
            };

            return vm.colorListDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll').then(function (res) {
                            e.success([{ 'COLOR_NAME_EN': '---New Color---', MC_COLOR_ID: -1 }].concat(res));
                        });
                    }
                }
            });
        };



        function getMcDiaList() {
            vm.mcDiaOption = {
                optionLabel: "-- Dia --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MC_DIA",
                dataValueField: "KNT_MC_DIA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.fabOrder.MC_DIA = item.MC_DIA;
                },
            };

            return vm.mcDiaDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Knit/KnitCommon/getMachineDiaList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getMcGgList() {
            vm.mcGgOption = {
                optionLabel: "-- GG --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.fabOrder.MC_GG = item.LK_DATA_NAME_EN;
                },
            };

            return vm.mcGgDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByUrlNoToken('/api/common/LookupListData/59').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.onChangeFinDia = function () {
            vm.fabOrder.FIN_DIA_N_DIA_TYPE = vm.fabOrder.FIN_DIA + ' ' + vm.fabOrder.DIA_MOU_CODE + ' ' + vm.fabOrder.FIN_DIA_TYPE;
        }

        vm.onChangeMcDiaGG = function () {
            vm.fabOrder.MC_DIA_N_GG = vm.fabOrder.MC_DIA + 'X' + vm.fabOrder.MC_GG;
        }

        vm.onChangeNetFfQty = function () {
            vm.fabOrder.NET_FFAB_QTY_NAME = vm.fabOrder.NET_FFAB_QTY + ' ' + vm.fabOrder.QTY_MOU_CODE;
        }
        
        vm.addRow = function (data) {
            var fabOrderCopy = angular.copy(data);
            
            vm.fabOrderGridDataSource.insert(0, data);

            vm.cancelToGrid();
            
            vm.isAddToGrid = 'Y';
        }
        
        function findGridIndex(index, KNT_SC_YRN_RCV_H_ID) {
            var dataList = $scope.$parent.mainYrnRcvGridDataSource.data();

            if (index > -1) {
                return index;
            } else {

                return _.findIndex(dataList, function (obj) {
                    return parseInt(obj.KNT_SC_YRN_RCV_H_ID) == parseInt(KNT_SC_YRN_RCV_H_ID);
                });
            }

        };

        vm.updateIndex = 0;
        vm.editRow = function (dataItem) {
            var index = vm.fabOrderGridDataSource.indexOf(dataItem);
            //alert('index:' + index);

            //vm.updateIndex = findGridIndex(index, dataItem.KNT_SC_YRN_RCV_H_ID);
            //alert(vm.updateHrdGridIndex);

            console.log(dataItem);
            var data = angular.copy(dataItem);
            vm.fabOrder = data;

            vm.isAddToGrid = 'N';
        };

        vm.removeRow = function (dataItem) {         
            vm.fabOrderGridDataSource.remove(dataItem);
        };

        vm.updateFabOrder = function (data) {
            var fabOrderCopy = angular.copy(data);

            var dataItem = vm.fabOrderGridDataSource.getByUid(data.uid);

            console.log(dataItem);
           
            dataItem.set('MC_STYLE_D_FAB_ID', fabOrderCopy.MC_STYLE_D_FAB_ID);
            dataItem.set('RF_FAB_TYPE_ID', fabOrderCopy.RF_FAB_TYPE_ID);
            dataItem.set('FABRIC_DESC', fabOrderCopy.FABRIC_DESC);
            dataItem.set('RF_FIB_COMP_ID', fabOrderCopy.RF_FIB_COMP_ID);
            
            dataItem.set('FAB_COLOR_ID', fabOrderCopy.FAB_COLOR_ID);
            dataItem.set('COLOR_NAME_EN', fabOrderCopy.COLOR_NAME_EN);
            dataItem.set('FIN_DIA', fabOrderCopy.FIN_DIA);
            dataItem.set('DIA_MOU_ID', fabOrderCopy.DIA_MOU_ID);
            dataItem.set('LK_DIA_TYPE_ID', fabOrderCopy.LK_DIA_TYPE_ID);
            dataItem.set('FIN_GSM', fabOrderCopy.FIN_GSM);
            dataItem.set('NET_FFAB_QTY', fabOrderCopy.NET_FFAB_QTY);
            dataItem.set('QTY_MOU_ID', fabOrderCopy.QTY_MOU_ID);
            dataItem.set('NET_FFAB_QTY_NAME', fabOrderCopy.NET_FFAB_QTY_NAME);
            dataItem.set('KNT_MC_DIA_ID', fabOrderCopy.KNT_MC_DIA_ID);
            dataItem.set('LK_MC_GG_ID', fabOrderCopy.LK_MC_GG_ID);
            
            dataItem.set('FIN_DIA_N_DIA_TYPE', fabOrderCopy.FIN_DIA_N_DIA_TYPE);
            dataItem.set('MC_DIA_N_GG', fabOrderCopy.MC_DIA_N_GG);

            vm.fabOrder.MC_FAB_PROD_ORD_D_ID = -1;
            vm.fabOrder.NET_FFAB_QTY = '';
            
            dataItem.set('editMode', false);
            
            vm.isAddToGrid = 'Y';
        }
        
        vm.cancelToGrid = function () {
            vm.fabOrder.NET_FFAB_QTY = '';

            vm.fabOrder.MC_BLK_FAB_REQ_H_ID = 0;
            vm.fabOrder.MC_BLK_FAB_REQ_D_ID = 0;
            
            vm.isAddToGrid = 'Y';
        };

        vm.fabOrderGridOption = {
            height: 220,
            sortable: true,
            columns: [                
                { field: "COLOR_NAME_EN", title: "Color", width: "20%" },
                { field: "FABRIC_DESC", title: "Fabric", width: "30%" },
                { field: "MC_DIA_N_GG", title: "MC DiaXgg", width: "15%" },
                { field: "FIN_DIA_N_DIA_TYPE", title: "Fin.Dia", width: "15%" },              
                { field: "NET_FFAB_QTY_NAME", title: "Qty", width: "15%" },
                //{ field: "SC_START_DT", title: "Start Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                //{ field: "SC_END_DT", title: "End Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)'><i class='fa fa-edit'></i></button>&nbsp;<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='dataItem.MC_FAB_PROD_ORD_D_ID>0?true:false' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "5%"
                }
            ]
        };

        vm.fabOrderGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    //e.success([]);
                    return KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/GetFabOrdListByID?pMC_FAB_PROD_ORD_H_ID=' + (vm.form.MC_FAB_PROD_ORD_H_ID || 0)).then(function (res) {

                        if (res.length > 0) {
                            //angular.forEach(res, function (val, key) {
                            //    val['SC_START_DT'] = $filter('date')(val['SC_START_DT'], vm.dtFormat);
                            //    val['SC_END_DT'] = $filter('date')(val['SC_END_DT'], vm.dtFormat);
                            //});

                            e.success(res);
                        }
                        else {
                            e.success([]);
                        }

                    }, function (err) {
                        console.log(err);
                    });
                }
            },
            schema: {
                model: {
                    id: "MC_FAB_PROD_ORD_D_ID",
                    fields: {
                        //SC_START_DT: { type: "date", editable: false },
                        //SC_END_DT: { type: "date", editable: false }
                    }
                }
            }
        });
        
        vm.next = function () {
            vm.IS_NEXT = 'Y';

            return KnittingDataService.getDataByFullUrl('/api/Knit/FabProdKnitOrder/GetFabProdOrdHdr?&pRF_FAB_PROD_CAT_ID=' + vm.form.RF_FAB_PROD_CAT_ID +
                '&pMC_STYLE_H_EXT_ID=' + vm.form.MC_STYLE_H_EXT_ID).then(function (res) {
                console.log(res);
                
                var formCopy = angular.copy(vm.form);
                res['MC_BYR_ACC_ID'] = formCopy.MC_BYR_ACC_ID;

                //vm.form = res;
                vm.form.MC_FAB_PROD_ORD_H_ID = res['MC_FAB_PROD_ORD_H_ID'];
                $stateParams.pMC_FAB_PROD_ORD_H_ID = res['MC_FAB_PROD_ORD_H_ID'];

                $state.go('KntSmplFabProdOrdMnul', { pMC_FAB_PROD_ORD_H_ID: res['MC_FAB_PROD_ORD_H_ID'], pRF_FAB_PROD_CAT_ID: vm.form.RF_FAB_PROD_CAT_ID, pMC_BYR_ACC_ID: $stateParams.pMC_BYR_ACC_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });


                vm.fabOrderGridDataSource.read();
                
            });
        }

        vm.newFabOrder = function () {
            vm.IS_NEXT = 'N';
            vm.cancelToGrid();

            $state.go('KntSmplFabProdOrdMnul', { pMC_FAB_PROD_ORD_H_ID: 0, pRF_FAB_PROD_CAT_ID: vm.form.RF_FAB_PROD_CAT_ID, pMC_BYR_ACC_ID: $stateParams.pMC_BYR_ACC_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });
        }

        vm.batchSave = function () {

            var submitData = angular.copy(vm.form);            
            submitData['FAB_PROD_D_XML'] = "";
            

            var fabOrdGridData = vm.fabOrderGridDataSource.data();
            

            //var yrnRcvHdrIndex = 0;
            //var yrnRcvDtlIndex = 0;
            //var yrnRcvHdrData = [];
            //var yrnRcvDtlData = [];

            //var fabDtlIndex = 0;
            ////var fabDtlYrnIndex = 0;
            var fabDtlData = [];
            //var fabDtlYrnData = [];


            angular.forEach(fabOrdGridData, function (val, key) {
                
                var fabData = {
                    MC_FAB_PROD_ORD_D_ID: val['MC_FAB_PROD_ORD_D_ID'], MC_FAB_PROD_ORD_H_ID: val['MC_FAB_PROD_ORD_H_ID'], MC_STYLE_D_FAB_ID: val['MC_STYLE_D_FAB_ID'],                    
                    DIA_MOU_ID: val['DIA_MOU_ID'], FIN_DIA: val['FIN_DIA'], LK_DIA_TYPE_ID: val['LK_DIA_TYPE_ID'], FAB_COLOR_ID: val['FAB_COLOR_ID'], LK_COL_TYPE_ID: val['LK_COL_TYPE_ID'],
                    RF_FAB_TYPE_ID: val['RF_FAB_TYPE_ID'], RF_FIB_COMP_ID: val['RF_FIB_COMP_ID'], FIN_GSM: val['FIN_GSM'], KNT_MC_DIA_ID: val['KNT_MC_DIA_ID'], LK_MC_GG_ID: val['LK_MC_GG_ID'],
                    NET_FFAB_QTY: val['NET_FFAB_QTY'], PROD_RATE: val['PROD_RATE'], QTY_MOU_ID: val['QTY_MOU_ID']                   
                };

                fabDtlData.push(fabData);
                                
            });

            


            submitData.FAB_PROD_D_XML = KnittingDataService.xmlStringShort(fabDtlData.map(function (ob) {
                return ob;
            }));

            

            console.log(submitData);
            console.log(fabOrdGridData);
            //return;

            Dialog.confirm('Do you want save?', 'Are you sure?', ['Yes', 'No'])
                .then(function () {

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/FabProdKnitOrder/SaveFabProdOrdMnul').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                
                                if (res.data.PMC_FAB_PROD_ORD_H_RTN != 0) {
                                    $stateParams.pMC_FAB_PROD_ORD_H_ID = res.data.PMC_FAB_PROD_ORD_H_RTN;
                                    vm.form.MC_FAB_PROD_ORD_H_ID = res.data.PMC_FAB_PROD_ORD_H_RTN;
                                    
                                    vm.fabOrderGridDataSource.read();

                                }

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };


    }
})();
//=============== End for Fabric Color & KP =================

