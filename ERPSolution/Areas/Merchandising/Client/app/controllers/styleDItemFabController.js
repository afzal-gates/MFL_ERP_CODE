(function () {
    'use strict';
    angular.module('multitex.mrc').controller('StyleDItemFabController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', '$modal', 'FiberTypeList', 'YrnCount', 'Dialog', StyleDItemFabController]);
    function StyleDItemFabController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData, $modal, FiberTypeList, YrnCount, Dialog) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'StyleDItemFab';
        var key = 'MC_STYLE_D_FAB_ID';
        vm.HAS_SET = ($scope.$parent.StyleData.HAS_SET) || 'N';
        vm.parentItemCategory = 5;
        vm.Title = $state.current.Title || '';
        vm.selectedFibCom = '';
        vm.mouUnits = [];
        vm.isWovenFab = false;
        vm.IS_FKNIT = false;
        vm.IS_DRP_NDL = false;
        vm.FabTypeListFull = [];
        vm.currentFab = '';
        vm.composition = '';
        vm.construction = '';
        vm.cotnType = '';
        vm.spinType = '';
        vm.washType = '';
        vm.finishType = '';
        vm.mellange = '';
        vm.yrc = '';
        vm.feedertyp = '';
        vm.elabrand = '';
        vm.colCuffDes = '';
        vm.FiberCompList = [];
        vm.itemSubCategory = [];
        vm.isRatioHidden = true;
        vm.params = $stateParams;
        vm.isCtnFibExit = false;
        vm.isPolyFibExit = false;
        vm.ElaPercent = 0;
        vm.MachineGaugeListData = [];
        vm.SpinFinListData = [];
        vm.CotnTypeListData = [];
        vm.isRatioHidden = true;
        vm.isLoopDisabled = true;

        var CountIni = [{ PART_ID: 426, RATIO: 50 }, { PART_ID: 427, RATIO: 0 }, { PART_ID: 428, RATIO: 50 }];

        vm.Fab = formData[key] ? formData : {
            IS_MELLANGE: 'N', FIB_COMP_NAME: '', LK_DYE_MTHD_ID: 1, INV_ITEM_CAT_ID: 34, IS_M_P: 'M',
            RF_YRN_CNT_LST: CountIni,
            IS_SLUB: 'N', WT_MOU_ID: 22,
            MC_STYLE_D_FAB_ID: -1,
            IS_SP_WASH: 'N',
            IS_SP_FINISH: 'N',
            LK_FBR_GRP_ID :''
        };
        vm.IS_FEEDER_TYPE = false;

        vm.countFiltered = false;

        activate()
        vm.showSplash = true;
        function activate() {

            var promise = [getFabricCategoryList(), getFabricTypeList(), getFabricCompList(), getCotnTypeList(), getFabList(),getWashTypeList(), getFinishTypeList(),
                            getFeederTypeList(), getElaBrandList(), getItemSubCategory(formData[key] ? formData.INV_ITEM_CAT_ID : vm.Fab.INV_ITEM_CAT_ID), getCollarCuffDesignType(), getFabricationYarn(), getSpinFinList(), getFabGroupList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        

        if (formData[key]) {
            if (formData.STYLE_D_ITEM_LST) {
                vm.Fab['STYLE_D_ITEM_LST'] = MrcDataService.parseXmlString(formData.STYLE_D_ITEM_LST);
            }
        }

        if (formData[key]) {
            console.log(formData);
            vm.Fab['RF_YRN_CNT_LST'] = formData.YRN_CNT_LST.length == 0 ? CountIni : formData.YRN_CNT_LST;
        }


        if (formData[key]) {
            if (formData.LK_WASH_TYPE_ID) {
                vm.Fab['LK_WASH_TYPE_ID'] = MrcDataService.parseXmlString(formData.LK_WASH_TYPE_ID);
            }
        }

        if (formData[key]) {
            if (formData.LK_FINIS_TYPE_ID) {
                vm.Fab['LK_FINIS_TYPE_ID'] = MrcDataService.parseXmlString(formData.LK_FINIS_TYPE_ID);
            }
        }


        function getFabricationYarn() {
            if (! $stateParams.MC_STYLE_H_ID) return;
            return MrcDataService.getDataByUrl('/StyleDItemFab/getFabricationYarn?pMC_STYLE_H_ID=' + $stateParams.MC_STYLE_H_ID).then(function (res) {
                vm.fabYarnList = res;
            });
            
        }

        function getSuggestedCount(RF_FIB_COMP_ID, RF_FAB_TYPE_ID, FB_WT_MIN, FB_WT_MAX) {
            if (RF_FIB_COMP_ID && RF_FAB_TYPE_ID && FB_WT_MIN && FB_WT_MAX) {
                return MrcDataService.getDataByUrl('/YarnSpec/SuggestedCount/Composition/' + RF_FIB_COMP_ID + '/Construction/' + RF_FAB_TYPE_ID + '/GsmFrom/' + FB_WT_MIN + '/GsmTo/' +FB_WT_MAX).then(function (res) {
                    var data = angular.copy(res);
                    if (angular.isArray(data) && data.length > 1) {
                        vm.countFiltered = true;
                        var sum = _.sumBy(data, function (o) {
                            return o.RATIO || 0;
                        });

                        var divider = sum == 0 ? 1 : sum;
                        data = angular.forEach(data, function (val, key) {
                            val.RATIO = parseFloat(((val.RATIO / divider) * 100).toFixed(2));
                            if (val.RATIO == 0) {
                                if (vm.isLoopDisabled && val.PART_ID != 427) {
                                    val.RATIO = 50;
                                } else if (vm.isLoopDisabled && val.PART_ID == 427) {
                                    val.RATIO = 0;
                                } else {
                                    val.RATIO = 33.33;
                                }
                            };
                        });
                        vm.Fab['RF_YRN_CNT_LST'] = data;
                    } else {
                        vm.Fab['RF_YRN_CNT_LST'] = [
                             { PART_ID: 426, RATIO: 50, CNT_ID: '' },
                             { PART_ID: 427, RATIO: 0, CNT_ID: '' },
                             { PART_ID: 428, RATIO: 50, CNT_ID: '' }
                        ];
                        vm.countFiltered = false;
                    }

                }, function (err) {
                    console.log(err);
                })
            } else {
                vm.Fab['RF_YRN_CNT_LST'] = [
                     { PART_ID: 426, RATIO: 50,CNT_ID:'' },
                     { PART_ID: 427, RATIO: 0, CNT_ID: '' },
                     { PART_ID: 428, RATIO: 50, CNT_ID: '' }
                ];
            }
        };



        function getWashTypeList() {
            return MrcDataService.LookupListData(57).then(function (res) {
                vm.WashTypeList = res.map(function (o) {
                    return { LOOKUP_DATA_ID: o.LOOKUP_DATA_ID, LK_DATA_NAME_EN: o.LK_DATA_NAME_EN };
                });
            }, function (err) {
                console.error(err);
            });
        }
        function getFinishTypeList() {
            return MrcDataService.LookupListData(58).then(function (res) {
                vm.FinishTypeList = res.map(function (o) {
                    return { LOOKUP_DATA_ID: o.LOOKUP_DATA_ID, LK_DATA_NAME_EN: o.LK_DATA_NAME_EN };
                });
            }, function (err) {
                console.error(err);
            });
        }
        function getCollarCuffDesignType() {
            return vm.ColCuffDesType = {
                optionLabel: "--Design Type--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(78).then(function (res) {
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



        function getFabList() {
            MrcDataService.getDataByUrl('/StyleDItemFab/FabDataByItemId/' + $stateParams.MC_STYLE_H_ID).then(function (res) {
                vm.FabList = res;
            }, function (err) {
                console.log(err);
            })
        };

        function getItemSubCategory(INV_ITEM_CAT_ID) {
            return MrcDataService.invItemListByParent(INV_ITEM_CAT_ID).then(function (res) {
                vm.itemSubCategoryOri = angular.copy(res);
                vm.itemSubCategory = _.filter(vm.itemSubCategoryOri, function (o) {
                    return o.IS_M_P === (formData[key] ? formData.IS_M_P : 'M');
                });

            }, function (err) {
                console.log(err);
            });
        }


        $scope.$watch('vm.Fab.IS_M_P', function (newVal, oldVal) {

            vm.itemSubCategory = _.filter(vm.itemSubCategoryOri, function (o) {
                return o.IS_M_P === newVal;
            });
        });

        $scope.$watchGroup(['vm.Fab.FB_WT_MAX', 'vm.Fab.RF_FIB_COMP_ID', 'vm.Fab.RF_FAB_TYPE_ID', 'vm.Fab.MC_STYLE_D_FAB_ID'], function (newVal, oldVal) {

            if (newVal[3] > 0) {
                return;
            }

            if (_.some(newVal, function (o) {
                return _.isUndefined(o);
            })){
                return;
            } else {
                getSuggestedCount(newVal[1], newVal[2], newVal[0], newVal[0]);
            }
        });

        function getFabricCategoryList() {
            return vm.FabricCategoryList = {
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.invItemListByParent(vm.parentItemCategory).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataBound: function (e) {
                    var dataItem = this.dataItem(e.item);
                    //FB01 : Knit Fabric
                    if (dataItem.ITEM_CAT_CODE == 'FB01') {
                        vm.mouUnits = [{ RF_MOU_ID: 22, MOU_NAME_EN: 'GSM' }];
                        vm.Fab['WT_MOU_ID'] = 22;

                    }
                    //FB02 : Woven Fabric
                    if (dataItem.ITEM_CAT_CODE == 'FB02') {
                        vm.mouUnits = [{ RF_MOU_ID: 24, MOU_NAME_EN: 'CM' }, { RF_MOU_ID: 23, MOU_NAME_EN: 'Inch' }];
                    }
                }
                ,
                select: function (e) {
                    var dataItem = this.dataItem(e.item);

                    if (vm.currentFab != dataItem.ITEM_CAT_CODE) {
                        vm.Fab['RF_FAB_TYPE_ID'] = '';
                        vm.Fab['RF_FIB_COMP_ID'] = '';
                        vm.Fab['LK_SPN_PRCS_ID'] = '';
                        vm.Fab['LK_COTN_TYPE_ID'] = '';
                        vm.Fab['IS_SLUB'] = 'N';
                        vm.Fab['LK_WASH_TYPE_ID'] = '';
                        vm.Fab['LK_FINIS_TYPE_ID'] = '';
                        vm.Fab['LK_DYE_MTHD_ID'] = '';
                        vm.Fab['FABRIC_DESC'] = '';
                        vm.Fab['LK_FEDER_TYPE_ID'] = '';
                        vm.Fab['LK_ELA_BRAND_ID'] = '';
                        vm.Fab['IS_MELLANGE'] = 'N';
                        $scope.FabricDetails.$setPristine();

                        vm.currentFab = dataItem.ITEM_CAT_CODE;
                    }

                    if (dataItem.INV_ITEM_CAT_ID) {
                        var ds = {
                            transport: {
                                read: function (e) {
                                    MrcDataService.getDataByFullUrl('/api/Common/FabricTypeList?pINV_ITEM_CAT_ID=' + (dataItem.INV_ITEM_CAT_ID || '')).then(function (res) {
                                        e.success(res);
                                    }, function (err) {
                                        console.log(err);
                                    });
                                }
                            }
                        };

                        $("#RF_FAB_TYPE_ID").data("kendoDropDownList").setDataSource(ds);

                    }

                    //FB01 : Knit Fabric
                    if (dataItem.ITEM_CAT_CODE == 'FB01') {
                        vm.mouUnits = [{ RF_MOU_ID: 22, MOU_NAME_EN: 'GSM' }];
                        vm.Fab['WT_MOU_ID'] = 22;
                        vm.isWovenFab = false;
                        vm.Fab['IS_M_P'] = 'M';

                        
                    }
                    //FB02 : Woven Fabric
                    if (dataItem.ITEM_CAT_CODE == 'FB02') {
                        vm.mouUnits = [{ RF_MOU_ID: 24, MOU_NAME_EN: 'CM' }, { RF_MOU_ID: 23, MOU_NAME_EN: 'Inch' }];
                        vm.Fab['WT_MOU_ID'] = 23;
                        vm.isWovenFab = true;
                        vm.Fab['IS_M_P'] = 'P';
                    }


                },
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID"
            };
        }

        vm.getSuggestedCount = function (data) {
            if (data.FB_WT_MAX && data.RF_FIB_COMP_ID && data.RF_FAB_TYPE_ID) {
                getSuggestedCount(data.RF_FIB_COMP_ID, data.RF_FAB_TYPE_ID,data.FB_WT_MAX,data.FB_WT_MAX);
            }
        };



        vm.changed = function (d) {
            vm.Fab['FB_WT_MAX'] = d;
        };

        $scope.$watchGroup(['vm.Fab.RF_FIB_COMP_ID',
                            'vm.Fab.RF_FAB_TYPE_ID',
                            'vm.Fab.FB_WT_MIN',
                            'vm.Fab.FB_WT_MAX',
                            'vm.Fab.LK_COTN_TYPE_ID',
                            'vm.Fab.LK_SPN_PRCS_ID',
                            'vm.Fab.IS_SLUB',
                            'vm.Fab.MC_STYLE_D_FAB_ID',
                            'vm.Fab.LK_WASH_TYPE_ID',
                            'vm.Fab.LK_FINIS_TYPE_ID',
                            'vm.Fab.IS_MELLANGE',
                            'vm.Fab.FIB_CMP_DESC',
                            'vm.Fab.LK_FEDER_TYPE_ID',
                            'vm.Fab.LK_ELA_BRAND_ID',
                            'vm.Fab.LK_FK_DGN_TYP_ID',
                            'vm.Fab.RF_YRN_CNT_LST[0].CNT_ID',
                            'vm.Fab.RF_YRN_CNT_LST[1].CNT_ID',
                            'vm.Fab.RF_YRN_CNT_LST[2].CNT_ID',
                            'vm.Fab.WT_MOU_ID'

        ], function (newVal, oldVal) {
            if (newVal[2] && newVal[3]) {

                if (newVal[3] < newVal[2]) {
                    $scope.FabricDetails['FB_WT_MAX'].$setValidity('required', false);
                } else {
                    $scope.FabricDetails['FB_WT_MAX'].$setValidity('required', true);
                }
            }

            if (newVal[7] < 0 ) {

                var YrnCountLc = [];
                if (newVal[8]) {
                    vm.washType = _.map(newVal[8], 'LK_DATA_NAME_EN').join(', ') + ' Wash';
                } else {
                    vm.washType = '';
                }

                if (newVal[9]) {
                    vm.finishType = _.map(newVal[9], 'LK_DATA_NAME_EN').join(', ') + ' Finish';
                } else {
                    vm.finishType = '';
                }



                if (newVal[6] == 'Y') {
                    vm.slub = 'Slub';
                } else {
                    vm.slub = '';
                }

                if (newVal[10] == 'Y') {
                    vm.mellange = 'Mellange';
                } else {
                    vm.mellange = '';
                }

                YrnCountLc = _.map(_.groupBy(vm.Fab.RF_YRN_CNT_LST, function (o) {
                    return o.CNT_ID;
                }), function (g) {
                    return g[0];
                });

                if (angular.isArray(YrnCountLc) && YrnCountLc.length > 0) {
                    vm.yrc = '';
                    angular.forEach(YrnCountLc, function (val, key) {
                        if (val.CNT_ID) {
                            vm.yrc += _.find(YrnCount, function (o) {
                                return o.RF_YRN_CNT_ID == parseInt(val.CNT_ID);
                            }).YR_COUNT_NO + ' ';
                        }

                    });
                }


                if (newVal[2] && newVal[3]) {
                    if (newVal[2] == newVal[3]) {
                        if (newVal[18] == 22) {
                            vm.range = newVal[2] + ' ' + vm.mouUnits[0].MOU_NAME_EN;
                        } else if (newVal[18] == 23) {
                            vm.range = newVal[2] + ' ' + vm.mouUnits[1].MOU_NAME_EN;
                        } else if (newVal[18] == 24) {
                            vm.range = newVal[2] + ' ' + vm.mouUnits[0].MOU_NAME_EN;
                        }

                    } else {
                        if (newVal[18] == 22) {
                            vm.range = newVal[2] + ' - ' + newVal[3] + ' ' + vm.mouUnits[0].MOU_NAME_EN;
                        } else if (newVal[18] == 23) {
                            vm.range = newVal[2] + ' - ' + newVal[3] + ' ' + vm.mouUnits[1].MOU_NAME_EN;
                        } else if (newVal[18] == 24) {
                            vm.range = newVal[2] + ' - ' + newVal[3] + ' ' + vm.mouUnits[0].MOU_NAME_EN;
                        }
                    }
                } else {
                    vm.range = '';
                }
                vm.Fab['FABRIC_DESC'] = (vm.yrc + ' ' + vm.composition + ' ' + vm.feedertyp + ' ' + vm.elabrand + ' ' + vm.spinType + ' ' + vm.construction + ' ' + vm.slub + ' ' + vm.mellange + ' ' + vm.range + ' ' + vm.washType + ' ' + vm.finishType + ' ' + vm.colCuffDes).replace('  ', ' ').replace('   ', ' ').replace('    ', ' ').replace('Cotton', vm.cotnType + ' Cotton');
            }
        });


        function getFabGroupList() {
            return MrcDataService.LookupListData(45).then(function (res) {
                vm.FabGroup = res.filter(function (u) {
                    return u.LOOKUP_DATA_ID !== 666; 
                });
            });
        }


        vm.deleteFabData = function (MC_STYLE_D_FAB_ID, token, FABRIC_DESC) {
            Dialog.confirm('Removing Fabrication :' + FABRIC_DESC  , 'Are you sure?', ['Yes', 'No'])
             .then(function () {
                 return MrcDataService.delDataByUrl(
                     '/StyleDItemFab/Delete/' + MC_STYLE_D_FAB_ID,
                      {  } ,
                      token
                     ).then(function (res) {
                     if (res.success === false) {
                         vm.errors = res.errors;
                     }
                     else {
                         res['data'] = angular.fromJson(res.jsonStr);
                         config.appToastMsg(res.data.PMSG);
                         if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             $state.go('StyleH.Fab', { MC_STYLE_D_FAB_ID: 0 }, { reload: 'StyleH.Fab' });
                         }
                     }
                 }, function (err) {
                     console.error(err)
                 })
            });


        }

        vm.makeActiveToggle = function (fab, token) {
            var str = (fab.IS_ACTIVE == 'Y' ? 'Disabling ' : 'Enabling ') + fab.FABRIC_DESC + '.';
            Dialog.confirm(str, 'Are you sure?', ['Yes', 'No'])
             .then(function () {
                 return MrcDataService.delDataByUrl(
                     '/StyleDItemFab/makeActiveToggle/' + fab.MC_STYLE_D_FAB_ID,
                      {},
                      token
                     ).then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 $state.go('StyleH.Fab', { MC_STYLE_D_FAB_ID: fab.MC_STYLE_D_FAB_ID }, { reload: 'StyleH.Fab' });
                             }
                         }
                     }, function (err) {
                         console.error(err)
                     })
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
                            MrcDataService.LookupListData(73).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    if (this.dataItem(e.item).LOOKUP_DATA_ID) {
                        vm.elabrand = this.dataItem(e.item).LK_DATA_NAME_EN;
                    } else {
                        vm.elabrand = '';
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
                            return MrcDataService.getDataByFullUrl('/api/common/GetDyeingMethodList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
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
                            MrcDataService.LookupListData(60).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
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
                optionLabel: "--Cotton Type--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return MrcDataService.LookupListData(62).then(function (res) {
                                vm.CotnTypeListData =angular.copy(res);
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                //dataBound:function(e){
                //    var ds = this.dataSource.data();
                //    if (ds.length == 1) {
                //        this.value(ds[0].LOOKUP_DATA_ID);
                //        vm.Fab['LK_COTN_TYPE_ID'] = ds[0].LOOKUP_DATA_ID;
                //    }
                //},
                select: function (e) {
                    if (this.dataItem(e.item).LOOKUP_DATA_ID) {
                        vm.cotnType = this.dataItem(e.item).LK_DATA_NAME_EN;
                    } else {
                        vm.cotnType = '';
                    }
                },

                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }


        vm.YarnCountList = {
            optionLabel: "--Select--",
            filter: "contains",
            autoBind: false,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(YrnCount);
                    }
                }
            },
            dataTextField: "YR_COUNT_DESC",
            dataValueField: "RF_YRN_CNT_ID"
        };

        vm.ElaCountList = {
            optionLabel: "--Elas.Count--",
            placeholder: "",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.getDataByFullUrl('/api/Common/YarnCountList').then(function (res) {
                            e.success(_.partition(res, function (o) {
                                return o.YR_CNT_SYS === 'D';
                            })[0]);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            },
            dataTextField: "YR_COUNT_DESC",
            dataValueField: "RF_YRN_CNT_ID"
        };



        function getFabricTypeList() {
            return vm.FabricTypeList = {
                optionLabel: "--Fabric Type--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {                         
                            return MrcDataService.getDataByFullUrl('/api/Common/FabricTypeList?pINV_ITEM_CAT_ID=' + (formData.INV_ITEM_CAT_ID || 34)).then(function (res) {
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

                    console.log(data.LK_YFAB_PART_LST);

                    vm.isRatioHidden = (data.IS_FBP_VISIBLE === 'N') ? true : false;
                    vm.isLoopDisabled = data.LK_YFAB_PART_LST.split(',').length == 3 ? false : true;

                    if (data.IS_FBP_VISIBLE) {
                        vm.isRatioHidden = (data.IS_FBP_VISIBLE === 'N') ? true : false;
                    }
                    if (data.LK_YFAB_PART_LST) {
                        vm.isLoopDisabled = data.LK_YFAB_PART_LST.split(',').length == 3 ? false : true;
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
                select: function (e) {
                    var data = this.dataItem(e.item);

                    vm.isRatioHidden = (data.IS_FBP_VISIBLE === 'N') ? true : false;
                    vm.isLoopDisabled = data.LK_YFAB_PART_LST.split(',').length == 3 ? false : true;

                    if (data.RF_FAB_TYPE_ID) {
                        vm.construction = data.FAB_TYPE_NAME;

                        //switch (parseInt(data.RF_FAB_TYPE_ID)) {
                        //    case 7: // Any Drop Needle RIB
                        //        vm.Fab.LK_FBR_GRP_ID = 193;
                        //        break;
                        //    case 20: // Cuff Set
                        //        vm.Fab.LK_FBR_GRP_ID = 195;
                        //        break;
                        //    case 21: // Collar & Cuff set
                        //        vm.Fab.LK_FBR_GRP_ID = 196;
                        //        break;
                        //    case 15: // Collar Set
                        //        vm.Fab.LK_FBR_GRP_ID = 194;
                        //        break;                                
                        //    default:
                        //        vm.Fab.LK_FBR_GRP_ID = 192;
                        //}

                    } else {
                        vm.construction = '';
                        //vm.Fab.LK_FBR_GRP_ID = 192;
                    }




                    angular.forEach(vm.Fab.RF_YRN_CNT_LST, function (val, key) {

                        if (vm.isLoopDisabled) {
                            vm.Fab.RF_YRN_CNT_LST[0].RATIO = 50;
                            vm.Fab.RF_YRN_CNT_LST[2].RATIO = 50;
                            vm.Fab.RF_YRN_CNT_LST[1].RATIO = 0;
                            vm.Fab.RF_YRN_CNT_LST[1].CNT_ID = '';

                            if (vm.isRatioHidden) {
                                vm.Fab.RF_YRN_CNT_LST[2].CNT_ID = vm.Fab.RF_YRN_CNT_LST[0].CNT_ID;
                            }

                        } else {
                            vm.Fab.RF_YRN_CNT_LST[0].RATIO = 33.33;
                            vm.Fab.RF_YRN_CNT_LST[1].RATIO = 33.33;
                            vm.Fab.RF_YRN_CNT_LST[2].RATIO = 33.33;
                        }
                    });

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
                            MrcDataService.getDataByFullUrl('/api/Common/FibreCompList').then(function (res) {

                                vm.FiberCompList = res;
                                e.success([{
                                    FIB_COMP_NAME: '--New Composition--',
                                    RF_FIB_COMP_ID: -1
                                }].concat(res));

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

                    //if (!this.dataItem(e.item).YR_FIB_COMP_ID) {
                    //    return;
                    //}

                    var CompData = MrcDataService.parseXmlString(this.dataItem(e.item).LK_FIB_TYPE_LST);
                    if (_.isArray(CompData)) {
                        vm.isCtnFibExit = _.some(CompData, function (o) {
                            return parseInt(o.LOOKUP_DATA_ID) == 369;
                        });

                        vm.isPolyFibExit = _.some(CompData, function (o) {
                            return parseInt(o.LOOKUP_DATA_ID) == 373;
                        });                      
                    }

                    if (this.dataItem(e.item).IS_ELA_MXD == 'Y') {
                        vm.IS_FEEDER_TYPE = true;
                        var FibComp = _.partition(MrcDataService.parseXmlString(this.dataItem(e.item).LK_FIB_TYPE_LST), function (o) {
                            return parseInt(o.LOOKUP_DATA_ID) == 375;
                        });

                        vm.ElaPercent = parseInt(FibComp[0][0].PERCENT);

                        var newComposition = [];

                        var RestPerct = (100 - parseInt(FibComp[0][0].PERCENT));
                        FibComp[1].forEach(function (obj) {
                            obj.PERCENT =Math.round(parseInt(obj.PERCENT) * (100 / RestPerct));
                            newComposition.push(obj);
                        });

                        var FibComForYarn = _.filter(vm.FiberCompList, function (o) {
                            return o.LK_FIB_TYPE_LST == MrcDataService.xmlStringLong(newComposition);
                        });

                        if (FibComForYarn.length == 1) {


                            vm.Fab['YR_FIB_COMP_ID'] = FibComForYarn[0].RF_FIB_COMP_ID;

                        } else {

                            newComposition.forEach(function (val, key) {
                                vm.Fab['FIB_COMP_NAME'] += ' ' + val.PERCENT + '% ' + (
                                                                         _.filter(FiberTypeList, function (o) {
                                                                             return parseInt(o.LOOKUP_DATA_ID) == parseInt(val.LOOKUP_DATA_ID);
                                                                         })[0].LK_DATA_NAME_EN
                                         );

                            });

                            vm.Fab['FIB_COMP_NAME'] = vm.Fab['FIB_COMP_NAME'].replace('null','').trim();

                            vm.Fab['LK_FIB_TYPE_LST'] = MrcDataService.xmlStringLong(newComposition);


                        }

                    } else {
                        vm.IS_FEEDER_TYPE = false;
                        vm.Fab['LK_FEDER_TYPE_ID'] = null;

                        vm.Fab['YR_FIB_COMP_ID'] = this.dataItem(e.item).RF_FIB_COMP_ID;

                        vm.Fab['RF_ELA_CNT_ID'] = null;
                        vm.Fab['LK_ELA_BRAND_ID'] = null;
                        vm.elabrand = '';
                        vm.feedertyp = '';
                    }

                },
                select: function (e) {
                    console.log(this.dataItem(e.item).IS_ELA_MXD);

                    vm.composition = this.dataItem(e.item).FIB_COMP_NAME;
                    var CompData12 = MrcDataService.parseXmlString(this.dataItem(e.item).LK_FIB_TYPE_LST);
                    if (_.isArray(CompData12)) {
                        vm.isCtnFibExit = _.some(CompData12, function (o) {
                            return parseInt(o.LOOKUP_DATA_ID) == 369;
                        });
                        vm.isPolyFibExit = _.some(CompData12, function (o) {
                            return parseInt(o.LOOKUP_DATA_ID) == 373;
                        });

                        if (vm.isCtnFibExit || vm.isPolyFibExit) {
                            $("#LK_SPN_PRCS_ID").data("kendoDropDownList").dataSource.read();
                        }

                    };
                    if (this.dataItem(e.item).RF_FIB_COMP_ID == -1) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'FabricCompositionGeneration.html',
                            controller: function (FiberCompList, FiberTypeList, FiberCompGroup, $modalInstance, $scope, $timeout, MrcDataService) {
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


                                        if (isFillUpData) {
                                            var dataToBeCheck = [];
                                            FiberCompList.forEach(function (o) {
                                                dataToBeCheck.push(MrcDataService.parseXmlString(o.LK_FIB_TYPE_LST).map(function (ob) {
                                                    return {
                                                        LOOKUP_DATA_ID: parseInt(ob.LOOKUP_DATA_ID),
                                                        PERCENT: parseInt(ob.PERCENT)
                                                    }
                                                }));

                                            });

                                            var isAvailable = _.some(dataToBeCheck, function (o) {

                                                return angular.equals(o, newVal.map(function (ob) {
                                                    return {
                                                        LOOKUP_DATA_ID: parseInt(ob.LOOKUP_DATA_ID),
                                                        PERCENT: parseInt(ob.PERCENT)
                                                    }
                                                }));

                                            });




                                        }

                                        if (isAvailable && isFillUpData) {

                                            $timeout(function () {
                                                vm.errors = [['Duplicate Composition is not allowed. Please try another']];
                                                vm.formDisabled = true;
                                            })

                                        };

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


                                    data['LK_FIB_TYPE_LST'] = MrcDataService.xmlStringLong(
                                             data.LK_FIB_TYPE_LST.map(function (o) {
                                                 return {
                                                     LOOKUP_DATA_ID: o.LOOKUP_DATA_ID,
                                                     PERCENT: o.PERCENT
                                                 }
                                             })
                                         );

                                    return MrcDataService.saveDataByUrl(data, '/StyleDItemFab/SaveFiberComposition', token).then(function (res) {
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
                                    return vm.FiberCompList;
                                },
                                FiberTypeList: function () {
                                    return FiberTypeList;
                                },
                                FiberCompGroup: function () {
                                    return MrcDataService.getDataByUrl('/YarnSpec/FiberCompGroup');
                                }
                            }
                        });

                        modalInstance.result.then(function (result) {

                            if (result.RF_FIB_COMP_ID && result.RF_FIB_COMP_ID > 0) {
                                vm.selectedFibCom = result.RF_FIB_COMP_ID;
                                vm.composition = result.FIB_COMP_NAME;
                                $("#RF_FIB_COMP_ID").data("kendoDropDownList").dataSource.read();
                            }

                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });


                    }


                    
                    if (this.dataItem(e.item).IS_ELA_MXD == 'Y') {
                        vm.IS_FEEDER_TYPE = true;

                        vm.Fab['LK_FEDER_TYPE_ID'] = 297;
                        vm.Fab['LK_ELA_BRAND_ID'] = 346;

                        var FibComp = _.partition(MrcDataService.parseXmlString(this.dataItem(e.item).LK_FIB_TYPE_LST), function (o) {
                            return parseInt(o.LOOKUP_DATA_ID) == 375;
                        });

                        var newComposition = [];
                        vm.ElaPercent = parseInt(FibComp[0][0].PERCENT);
                        var RestPerct = (100 - parseInt(FibComp[0][0].PERCENT));
                        FibComp[1].forEach(function (obj) {
                            obj.PERCENT = Math.round(parseInt(obj.PERCENT) * (100 / RestPerct));
                            newComposition.push(obj);
                        });

                        var FibComForYarn = _.filter(vm.FiberCompList, function (o) {
                            return o.LK_FIB_TYPE_LST == MrcDataService.xmlStringLong(newComposition);
                        });

                        if (FibComForYarn.length == 1) {


                            vm.Fab['YR_FIB_COMP_ID'] = FibComForYarn[0].RF_FIB_COMP_ID;

                        } else {

                            newComposition.forEach(function (val, key) {
                                vm.Fab['FIB_COMP_NAME'] += ' ' + val.PERCENT + '% ' + (
                                                                         _.filter(FiberTypeList, function (o) {
                                                                             return parseInt(o.LOOKUP_DATA_ID) == parseInt(val.LOOKUP_DATA_ID);
                                                                         })[0].LK_DATA_NAME_EN
                                         );

                            });

                            
                            vm.Fab['FIB_COMP_NAME'] = vm.Fab['FIB_COMP_NAME'].replace('null', '').trim();

                            vm.Fab['LK_FIB_TYPE_LST'] = MrcDataService.xmlStringLong(newComposition);

                        }


                    } else {
                        vm.IS_FEEDER_TYPE = false;
                        vm.Fab['LK_FEDER_TYPE_ID'] = null;
                        vm.Fab['RF_ELA_CNT_ID'] = null;
                        vm.Fab['LK_ELA_BRAND_ID'] = null;

                        vm.Fab['YR_FIB_COMP_ID'] = this.dataItem(e.item).RF_FIB_COMP_ID;
                        vm.elabrand = '';
                        vm.feedertyp = '';

                    }

                    if (vm.Fab['YR_FIB_COMP_ID'] && vm.isCtnFibExit) {

                      return  MrcDataService.getDataByUrl('/YarnSpec/YrnParamForFabri?RF_FIB_COMP_ID=' + vm.Fab['YR_FIB_COMP_ID']).then(
                            function (res) {
                                var obj = angular.fromJson(res);
                                var cottonTypDs = _.findByValues(vm.CotnTypeListData, 'LOOKUP_DATA_ID', obj.OPLK_COTN_TYPE_ID_LST.split(',').map(function (o) {
                                    return parseInt(o);
                                }));

                                var spinProcessDs = _.findByValues(vm.SpinFinListData, 'LOOKUP_DATA_ID', obj.OPLK_SPN_PRCS_ID_LST.split(',').map(function (o) {
                                    return parseInt(o);
                                }));


                                if (spinProcessDs.length == 1) {
                                    $("#LK_SPN_PRCS_ID").data("kendoDropDownList").select(function (o) {
                                        return o.LK_SPN_PRCS_ID === spinProcessDs[0].LK_SPN_PRCS_ID;
                                    });
                                    vm.Fab['LK_SPN_PRCS_ID'] = spinProcessDs[0].LK_SPN_PRCS_ID;
                                } else {
                                    vm.Fab['LK_SPN_PRCS_ID'] = '';
                                }

                                if (cottonTypDs.length == 1) {
                                    $("#LK_COTN_TYPE_ID").data("kendoDropDownList").select(function (o) {
                                        return o.LK_COTN_TYPE_ID === cottonTypDs[0].LK_COTN_TYPE_ID;
                                    });
                                    vm.Fab['LK_COTN_TYPE_ID'] = spinProcessDs[0].LK_COTN_TYPE_ID;
                                } else {
                                    vm.Fab['LK_COTN_TYPE_ID'] = '';
                                }

                                vm.Fab.IS_SLUB = obj.OPIS_SLUB_LST.split(',').length == 1 ? obj.OPIS_SLUB_LST : '';
                                vm.Fab.IS_MELLANGE = obj.OPIS_MELLANGE_LST.split(',').length == 1 ? obj.OPIS_MELLANGE_LST : '';

                            }, function (err) { console.error(err) });
                    } else {
                        vm.Fab['LK_SPN_PRCS_ID'] = '';
                        vm.Fab['LK_COTN_TYPE_ID'] = '';
                    }

                    if (this.dataItem(e.item).RF_FIB_COMP_ID) {
                        vm.composition = this.dataItem(e.item).FIB_COMP_NAME;
                    } else {
                        vm.composition = '';
                    }


                    
                },
                dataTextField: "FIB_COMP_NAME",
                dataValueField: "RF_FIB_COMP_ID"
            };
        }

        vm.submitData = function (dataOri, token, isValid) {

            $scope.FabricDetails.$submitted = true;
            if (!isValid)
                return;


            var data = angular.copy(dataOri);
            var RF_YRN_CNT_LST_ORI = [];
            var RF_YRN_CNT_LST_FIN = [];

            var elaCount = {
                PART_ID: 430,
                CNT_ID: data.RF_ELA_CNT_ID,
                RATIO: vm.ElaPercent,
                YR_FIB_COMP_ID: data.YR_FIB_COMP_ID,
                LK_SPN_PRCS_ID: '',
                LK_COTN_TYPE_ID: '',
                IS_SLUB: 'N',
                IS_MELLANGE: 'N'
            };

            angular.forEach(data.RF_YRN_CNT_LST, function (val, key) {
                val['YR_FIB_COMP_ID'] = data.YR_FIB_COMP_ID;
                val['LK_SPN_PRCS_ID'] = data.LK_SPN_PRCS_ID;
                val['LK_COTN_TYPE_ID'] = data.LK_COTN_TYPE_ID;
                val['IS_SLUB'] = data.IS_SLUB || 'N';
                val['IS_MELLANGE'] = data.IS_MELLANGE || 'N';
                RF_YRN_CNT_LST_FIN.push(val);

            });


            if (data.RF_ELA_CNT_ID) {
                RF_YRN_CNT_LST_ORI = angular.copy(data.RF_YRN_CNT_LST);
                RF_YRN_CNT_LST_FIN.push(elaCount);
            } else {
                data.RF_YRN_CNT_LST = data.RF_YRN_CNT_LST;
                RF_YRN_CNT_LST_FIN = data.RF_YRN_CNT_LST;
            }

            delete data['YRN_CNT_LST'];

            data['MC_STYLE_H_ID'] = $stateParams.MC_STYLE_H_ID;
            data['STYLE_D_ITEM_LST'] = '';
            data['LK_WASH_TYPE_ID'] = MrcDataService.xmlStringLong(data.LK_WASH_TYPE_ID);
            data['LK_FINIS_TYPE_ID'] = MrcDataService.xmlStringLong(data.LK_FINIS_TYPE_ID);
            data['RF_YRN_CNT_LST'] = MrcDataService.xmlStringShort(data.RF_YRN_CNT_LST.map(function (o) {
                return {
                    PART_ID: o.PART_ID,
                    CNT_ID: o.CNT_ID,
                    RATIO: o.RATIO
                }
            }));

            data['RF_YRN_CNT_LST_FULL'] = MrcDataService.xmlStringShort(RF_YRN_CNT_LST_FIN);
            data['IS_MSG_GEN'] = data.MC_STYLE_D_FAB_ID ? 'N' : 'Y';


            if (angular.isDefined(data[key]) && data[key] > 0) {
                console.log('===============');
                console.log(data);

                //alert('X');

                return MrcDataService.updateData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            getFabList();
                            getFabricationYarn();
                            //if ($state.current.LK_STYL_DEV_ID == 265) {
                            //    $state.go($state.current, $stateParams.current, { reload: 'StyleHDev.FabDev' });
                              
                            //} else {
                            //    $state.go($state.current, $stateParams.current, { reload: 'StyleH.Fab' });
                            //}
                            

                        }

                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            } else {

                return MrcDataService.saveData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001' && res.data.V_MC_STYLE_D_FAB_ID != 0) {
                            getFabList();
                            getFabricationYarn();
                            $scope.FabricDetails.$submitted = false;
                            vm.Fab = angular.extend(dataOri, 
                                {
                                MC_STYLE_D_FAB_ID: -1,
                                LK_FBR_GRP_ID: null,
                                RF_FAB_TYPE_ID: null,
                                FB_WT_MIN: null,
                                FB_WT_MAX: null,
                            });

                            $scope.FabricDetails.LK_FBR_GRP_ID.$setPristine();
                            $scope.FabricDetails.RF_FAB_TYPE_ID.$setPristine();
                            $scope.FabricDetails.LK_WASH_TYPE_ID.$setPristine();
                            $scope.FabricDetails.LK_FINIS_TYPE_ID.$setPristine();
                        }
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }

        function getItemList() {
            var data = [];
            MrcDataService.getDataByUrl('/StyleDItem/StyleDtlItemList/' + $stateParams.MC_STYLE_H_ID + '/' + vm.HAS_SET).then(function (res) {
                angular.forEach(res, function (val, key) {
                    data.push({ ITEM_NAME_EN: val.ITEM_SNAME, MC_STYLE_D_ITEM_ID: val.MC_STYLE_D_ITEM_ID });
                });
                vm.itemList = data;

            }, function (err) {
                console.log(err);
            })
        };

        vm.onCountChange = function (RF_YRN_CNT_LST,ID) {
            if (vm.isLoopDisabled) {
                RF_YRN_CNT_LST[0].RATIO =  50;
                RF_YRN_CNT_LST[2].RATIO =  50;
                RF_YRN_CNT_LST[1].RATIO = 0;
                RF_YRN_CNT_LST[1].CNT_ID = '';
                RF_YRN_CNT_LST[0].CNT_ID = RF_YRN_CNT_LST[0].CNT_ID;
                RF_YRN_CNT_LST[2].CNT_ID = RF_YRN_CNT_LST[0].CNT_ID;

                if (vm.isRatioHidden) {
                    RF_YRN_CNT_LST[2].CNT_ID = RF_YRN_CNT_LST[0].CNT_ID;
                }

            } else {
                RF_YRN_CNT_LST[0].RATIO = 34;
                RF_YRN_CNT_LST[1].RATIO = 33;
                RF_YRN_CNT_LST[2].RATIO = 33;

                RF_YRN_CNT_LST[1].CNT_ID = RF_YRN_CNT_LST[0].CNT_ID;
                RF_YRN_CNT_LST[2].CNT_ID = RF_YRN_CNT_LST[0].CNT_ID;
            }
        }

        function getSpinFinList() {
            return vm.SpinFinList = {
                optionLabel: "--Spin Finish--",
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

                            return MrcDataService.getDataByFullUrl(url).then(function (res) {
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






    }

})();