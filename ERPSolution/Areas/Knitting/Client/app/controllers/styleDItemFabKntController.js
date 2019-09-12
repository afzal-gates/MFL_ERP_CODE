(function () {
    'use strict';
    angular.module('multitex.knitting').controller('StyleDItemFabKntController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', 'Dialog', '$modalInstance', 'MC_FAB_PROD_ORD_D_ID', StyleDItemFabKntController]);
    function StyleDItemFabKntController($q, config, KnittingDataService, $stateParams, $state, $scope, logger, formData, Dialog, $modalInstance, MC_FAB_PROD_ORD_D_ID) {

        var vm = this;
        vm.errors = null;
        vm.Fab = formData
        vm.IS_FEEDER_TYPE = false;
        vm.IS_FKNIT = false;
        vm.IS_DRP_NDL = false;

        vm.countFiltered = false;

        activate()
        vm.showSplash = true;
        function activate() {

            var promise = [getFabricTypeList(), getFabricCompList(), getCotnTypeList(),
                            getFeederTypeList(), getElaBrandList(), getCollarCuffDesignType(), getSpinFinList(), getFabGroupList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        $scope.MC_FAB_PROD_ORD_D_ID = MC_FAB_PROD_ORD_D_ID;

        function getCollarCuffDesignType() {
            return vm.ColCuffDesType = {
                optionLabel: "--Design Type--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(78).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {

                    if (this.dataItem(e.item).LOOKUP_DATA_ID) {
                        vm.colCuffDes = this.dataItem(e.item).LK_DATA_NAME_EN;
                    } else {
                        vm.colCuffDes = '';
                    }
                },

                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function getFabGroupList() {
            return KnittingDataService.LookupListData(45).then(function (res) {
                vm.FabGroup = res;
            });
        }

        function getElaBrandList() {
            return vm.ElaBrandList = {
                optionLabel: "--Brand--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(73).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }
        function getDyeMethodList() {
            return vm.DyeMethodList = {
                optionLabel: "--Dyeing Method--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return KnittingDataService.getDataByFullUrl('/api/common/GetDyeingMethodList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DYE_MTHD_NAME",
                dataValueField: "LK_DYE_MTHD_ID"
            };
        }

        function getFeederTypeList() {
            return vm.FeederTypeList = {
                optionLabel: "--Feeder Type--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(60).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                select: function (e) {
                    if (this.dataItem(e.item).LOOKUP_DATA_ID) {
                        vm.feedertyp = this.dataItem(e.item).LK_DATA_NAME_EN;
                    } else {
                        vm.feedertyp = '';
                    }
                },

                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function getCotnTypeList() {
            return vm.CotnTypeList = {
                optionLabel: "--N/A--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return KnittingDataService.LookupListData(62).then(function (res) {
                                vm.CotnTypeListData =angular.copy(res);
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        vm.mouUnits = [{ RF_MOU_ID: 22, MOU_NAME_EN: 'GSM' }];


        function getFabricTypeList() {
            return vm.FabricTypeList = {
                optionLabel: "--Fabric Type--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {                         
                            return KnittingDataService.getDataByFullUrl('/api/Common/FabricTypeList?pINV_ITEM_CAT_ID=' + (formData.INV_ITEM_CAT_ID || 34)).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.error(err);
                            });
                        }
                    }
                },
                dataBound: function (e) {
                    var data = this.dataItem(e.item);
                    if (!data.RF_FAB_TYPE_ID) {
                        return;
                    }

                    if (data.IS_FLAT_CIR === 'F') {
                        vm.IS_FKNIT = true;
                    } else {
                        vm.IS_FKNIT = false;
                        vm.Fab['LK_FK_DGN_TYP_ID'] = '';
                    }

                    if (data.IS_DRP_NDL === 'Y') {
                        vm.IS_DRP_NDL = true;
                    } else {
                        vm.IS_DRP_NDL = false;
                    }

                },
                select: function (e) {
                    var data = this.dataItem(e.item);

                    if (data.RF_FAB_TYPE_ID) {
                        vm.construction = data.FAB_TYPE_NAME;

                        switch (parseInt(data.RF_FAB_TYPE_ID)) {
                            case 7: // Any Drop Needle RIB
                                vm.Fab.LK_FBR_GRP_ID = 193;
                                break;
                            case 20: // Cuff Set
                                vm.Fab.LK_FBR_GRP_ID = 195;
                                break;
                            case 21: // Collar & Cuff set
                                vm.Fab.LK_FBR_GRP_ID = 196;
                                break;
                            case 15: // Collar Set
                                vm.Fab.LK_FBR_GRP_ID = 194;
                                break;                                
                            default:
                                vm.Fab.LK_FBR_GRP_ID = 192;
                        }

                    } else {
                      
                        vm.Fab.LK_FBR_GRP_ID = 192;
                    }

                    if (data.IS_FLAT_CIR === 'F') {
                        vm.IS_FKNIT = true;
                    } else {
                        vm.IS_FKNIT = false;
                        vm.Fab['LK_FK_DGN_TYP_ID'] = '';
                    }



                    if (data.IS_DRP_NDL === 'Y') {
                        vm.IS_DRP_NDL = true;
                    } else {
                        vm.IS_DRP_NDL = false;
                        vm.Fab['DROP_NDL_DESC'] = '';
                    }

                },
                dataTextField: "FAB_TYPE_NAME",
                dataValueField: "RF_FAB_TYPE_ID"
            };
        }
        function getFabricCompList() {
            return vm.FabricCompList = {
                optionLabel: "--Fibre Content--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/Common/FibreCompList').then(function (res) {

                                vm.FiberCompList = res;
                                e.success([{
                                    FIB_COMP_NAME: '--New Composition--',
                                    RF_FIB_COMP_ID: -1
                                }].concat(res));

                            }, function (err) {
                                console.log(err);
                            });
                        }
                    },
                    requestEnd: function (e) {
                        if (vm.selectedFibCom != '') {
                            vm.Fab['RF_FIB_COMP_ID'] = vm.selectedFibCom;
                        }
                    }
                },
                dataBound: function (e) {

                    if (this.dataItem(e.item).IS_ELA_MXD == 'Y') {
                        vm.IS_FEEDER_TYPE = true;
                    } else {
                        vm.IS_FEEDER_TYPE = false;
                    }

                },
                select: function (e) {
                    console.log(this.dataItem(e.item).IS_ELA_MXD);

                    vm.composition = this.dataItem(e.item).FIB_COMP_NAME;
                  
                    if (this.dataItem(e.item).IS_ELA_MXD == 'Y') {
                        vm.IS_FEEDER_TYPE = true;
                        vm.Fab['LK_FEDER_TYPE_ID'] = 297;
                        vm.Fab['LK_ELA_BRAND_ID'] = 346;

                    } else {
                        vm.IS_FEEDER_TYPE = false;
                        vm.Fab['LK_FEDER_TYPE_ID'] = null;
                        vm.Fab['RF_ELA_CNT_ID'] = null;
                        vm.Fab['LK_ELA_BRAND_ID'] = null;
                    }

                    
                },
                dataTextField: "FIB_COMP_NAME",
                dataValueField: "RF_FIB_COMP_ID"
            };
        }

        vm.submitData = function (dataOri,isValid) {
            if (!isValid)
                return;

            var data = {
                RF_FIB_COMP_ID: dataOri.RF_FIB_COMP_ID,
                LK_FEDER_TYPE_ID: dataOri.LK_FEDER_TYPE_ID,
                LK_ELA_BRAND_ID: dataOri.LK_ELA_BRAND_ID,
                LK_SPN_PRCS_ID: dataOri.LK_SPN_PRCS_ID,
                LK_COTN_TYPE_ID: dataOri.LK_COTN_TYPE_ID,
                IS_SLUB: dataOri.IS_SLUB,
                IS_MELLANGE: dataOri.IS_MELLANGE,
                RF_FAB_TYPE_ID: dataOri.RF_FAB_TYPE_ID,
                LK_FK_DGN_TYP_ID: dataOri.LK_FK_DGN_TYP_ID,
                DROP_NDL_DESC: dataOri.DROP_NDL_DESC,
                LK_FBR_GRP_ID: dataOri.LK_FBR_GRP_ID,
                FB_WT_MIN: dataOri.FB_WT_MIN,
                FB_WT_MAX: dataOri.FB_WT_MAX,
                WT_MOU_ID: dataOri.WT_MOU_ID,
                FABRIC_DESC: dataOri.FABRIC_DESC,
                MC_STYLE_D_FAB_ID: dataOri.MC_STYLE_D_FAB_ID,
                MC_FAB_PROD_ORD_D_ID: MC_FAB_PROD_ORD_D_ID
            };


            return KnittingDataService.saveDataByFullUrl(data,'/api/mrc/StyleDItemFab/UpdateFromKnitting').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $modalInstance.close({
                            SUCCESS: true
                        })
                    }
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        function getSpinFinList() {
            return vm.SpinFinList = {
                optionLabel: "--N/A--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            if (vm.isCtnFibExit && !vm.isPolyFibExit) {
                                var url = '/api/common/GetSpinProcesListByFibTyp?LK_FIB_TYPE_ID=369'
                            }
                            if (!vm.isCtnFibExit && vm.isPolyFibExit) {
                                var url = '/api/common/GetSpinProcesListByFibTyp?LK_FIB_TYPE_ID=373'
                            }

                            if ((vm.isCtnFibExit && vm.isPolyFibExit) || (!vm.isCtnFibExit && !vm.isPolyFibExit)) {
                                var url = '/api/common/GetSpinProcesListByFibTyp'
                            }

                            return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                                vm.SpinFinListData = angular.copy(res);
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {

                    if (this.dataItem(e.item).LOOKUP_DATA_ID) {
                        vm.spinType = this.dataItem(e.item).LK_DATA_NAME_EN;
                    } else {
                        vm.spinType = '';
                    }


                },

                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        vm.cancel = function () {
            $modalInstance.dismiss();
        };

    }

})();