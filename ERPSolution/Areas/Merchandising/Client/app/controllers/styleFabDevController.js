//============= Start Style Master Controller ==============
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('StyleMasterFabDevController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'formData', '$filter', '$modal', StyleMasterFabDevController]);
    function StyleMasterFabDevController($q, config, MrcDataService, $stateParams, $state, $scope, formData, $filter, $modal) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'StyleH';
        var key = 'MC_STYLE_H_ID';
        $scope.format = config.appDateFormat;
        vm.toDay = $filter('date')(new Date(), config.appDateFormat);
        vm.Title = $state.current.Title || '';
        var sizeList = '';
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getbuyerAcList(), getBuyerList(), getDevelopedByList(), getCountryList(), getCompanyList(), getInquiryListData(), getSeasonList(),
                getStyleRefListData(), getMOUList(), getSizeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.form = { REVISION_DT: vm.toDay, IS_ACTIVE: 'Y', ORIGIN_ID: 1, MANUF_ID: 1, LK_STYL_DEV_ID: $state.current.LK_STYL_DEV_ID, items: [], QTY_MOU_ID: 1, HAS_SET: 'N', HAS_COMBO: 'N', HAS_MODEL: 'N', HAS_MULTI_COL_PACK: 'N' };

        if (formData[key]) {
            if (!formData['HAS_SET']) {
                formData['HAS_SET'] = 'N';
            }
            if (!formData['HAS_COMBO']) {
                formData['HAS_COMBO'] = 'N';
            }
            if (formData['MC_SIZE_LST']) {
                formData['MC_SIZE_LST'] = formData.MC_SIZE_LST.split(',');
            }
            vm.form = formData;
            $scope.StyleData = formData;
        }


        
        vm.fabState = ($state.current.name === 'StyleHFabDev.Fab') ? true : false;
        vm.fabBookState = ($state.current.name === 'StyleHFabDev.FabrBook') ? true : false;

        //vm.orderConState = ($state.current.name === 'StyleH.OrderCon' || $state.current.name === 'StyleH.OrderCon.Detail' || $state.current.name === 'StyleHDev.OrderConDev' || $state.current.name === 'StyleHDev.OrderConDev.DetailDev') ? true : false;
        //vm.colRefState = ($state.current.name === 'StyleH.ColRef' || $state.current.name === 'StyleHDev.ColRefDev') ? true : false;
        //vm.StyleBomState = ($state.current.name === 'StyleH.StyleBom') ? true : false;
        //vm.devFabBookingState = ($state.current.name === 'StyleHDev.DevFabBooking') ? true : false;


        $scope.addSetItem = function (data) {
            vm.form['items'] = data;
        };


        $scope.$watch('vm.form.HAS_SET', function (newVal, oldVal) {
            if (newVal && newVal != oldVal) {
                if (newVal == 'Y') {
                    vm.form['QTY_MOU_ID'] = 2;
                } else {
                    vm.form['QTY_MOU_ID'] = 1;
                }
            }
        });


        vm.SetItemList = function (data) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'SetItemEntryModal.html',
                controller: 'SetItemEntryModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                scope: $scope,
                resolve: {
                    Style: function () {
                        return data;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };







        $scope.REVISION_DTopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.REVISION_DTopened = true;
        };

        function getSizeList() {
            return vm.SizeList = {
                placeholder: "--Size List--",
                valuePrimitive: true,
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {

                            //var webapi = new kendo.data.transports.webapi({});
                            //var params = webapi.parameterMap(e.data);
                            //console.log(params);
                            MrcDataService.selectAllData('SizeMaster').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.error(err);
                            });
                        }
                    },
                    //serverFiltering: true,
                },
                change: function (e) {
                    var value = this.value();
                    if (value.length == 0) {
                        sizeList = '';
                    } else {
                        sizeList = value.join(',');
                    }
                },

                dataBound: function (e) {
                    var value = this.value();
                    if (value.length == 0) {
                        sizeList = '';
                    } else {
                        sizeList = value.join(',');
                    }
                },
                dataTextField: "SIZE_CODE",
                dataValueField: "MC_SIZE_ID"
            };
        }

        
        function getbuyerAcList() {
            return vm.buyerAcList = {
                optionLabel: "--- Buyer A/C ---",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var MC_BYR_ACC_ID = this.dataItem(e.item).MC_BYR_ACC_ID;
                    if (MC_BYR_ACC_ID) {

                        var ds = new kendo.data.DataSource({
                            transport: {
                                read: function (e) {
                                    return MrcDataService.getDataByUrl('/buyer/getBuyerDropdownList?MC_BYR_ACC_ID=' + MC_BYR_ACC_ID).then(function (res) {
                                        e.success(res);
                                    }, function (err) {
                                        console.log(err);
                                    });
                                }
                            }
                        });
                        $("#MC_BUYER_ID").data("kendoDropDownList").setDataSource(ds);

                    } else {
                        $("#MC_BUYER_ID").data("kendoDropDownList").dataSource.read();
                    }
                },

                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }


        function getBuyerList() {
            return vm.buyerList = {
                optionLabel: "-- Buyer --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return MrcDataService.getDataByUrl('/buyer/getBuyerDropdownList?MC_BYR_ACC_ID=' + (formData.MC_BYR_ACC_ID || '')).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var MC_BUYER_ID = this.dataItem(e.item).MC_BUYER_ID;
                    if (MC_BUYER_ID) {
                        var dataSource1 = new kendo.data.DataSource({
                            transport: {
                                read: function (e) {
                                    return MrcDataService.getDataByUrl('/StyleH/getStyleDropDown?pSTYLE_NO&pMC_BUYER_ID=' + MC_BUYER_ID + '&pMC_STYLE_H_ID').then(function (res) {
                                        e.success(res);
                                    }, function (err) {
                                        console.log(err);
                                    });
                                }
                            }
                        });

                        var dataSource2 = new kendo.data.DataSource({
                            data: _.filter(vm.inquiryListData, { 'MC_BUYER_ID': MC_BUYER_ID })
                        });

                        //$("#PARENT_ID").data("kendoDropDownList").setDataSource(dataSource1);
                        $("#MC_INQR_H_ID").data("kendoDropDownList").setDataSource(dataSource2);
                    } else {
                        //$("#PARENT_ID").data("kendoDropDownList").dataSource.read();
                        $("#MC_INQR_H_ID").data("kendoDropDownList").dataSource.read();
                    }
                },
                dataTextField: "BUYER_NAME_EN",
                dataValueField: "MC_BUYER_ID"
            };
        }

        function getInquiryListData() {
            return vm.inquiryList = {
                optionLabel: "-- Inquiry # --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.selectAllData('InquiryH').then(function (res) {
                                vm.inquiryListData = res;
                                e.success(_.filter(res, function (o) {
                                    return formData[key] ? o.MC_BUYER_ID == formData.MC_BUYER_ID : true;
                                }));
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "INQR_NO",
                dataValueField: "MC_INQR_H_ID"
            };
        }


        function getStyleRefListData() {
            return vm.StyleRefList = {
                optionLabel: "--  Prev. Style--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);


                            if (params.filter) {
                                var qmPrevStyle = '?pSTYLE_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                            } else {
                                var qmPrevStyle = '?pSTYLE_NO';
                            }

                            if (formData[key]) {
                                qmPrevStyle += '&pMC_BUYER_ID=' + formData.MC_BUYER_ID;
                                qmPrevStyle += '&pMC_STYLE_H_ID=' + formData.PARENT_ID;
                            } else {
                                if (MC_BUYER_ID) {
                                    qmPrevStyle += '&pMC_BUYER_ID=' + MC_BUYER_ID + '&pMC_STYLE_H_ID';
                                } else {
                                    qmPrevStyle += '&pMC_BUYER_ID&pMC_STYLE_H_ID';
                                }
                            };

                            return MrcDataService.getDataByUrl('/StyleH/getStyleDropDown' + qmPrevStyle).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    },
                    serverFiltering: true,
                },


                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_ID"
            };
        }



        function getSeasonList() {
            return vm.seasonList = {
                optionLabel: "--Season--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(52).then(function (res) {
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


        function getMOUList() {
            return vm.MOUList = {
                optionLabel: "--Qty Unit--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getMOUList().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"
            };
        }


        function getDevelopedByList() {
            return vm.DevelopedByList = {
                optionLabel: "--Status--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return MrcDataService.LookupListData(53).then(function (res) {
                                e.success(res);
                                //e.success(res.filter(function (o) {
                                //    return $state.current.LK_STYL_DEV_ID == 265 ? o.LOOKUP_DATA_ID == $state.current.LK_STYL_DEV_ID : true;
                                //}));
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getCountryList() {
            return vm.CountryList = {
                optionLabel: "-- Select Origin --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return MrcDataService.GetCountryList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COUNTRY_NAME_EN",
                dataValueField: "HR_COUNTRY_ID"
            };
        }

        function getCompanyList() {
            return vm.CompanyList = {
                optionLabel: "-- Supplier --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return MrcDataService.getCompanyData().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        }

        vm.submitData = function (dataOri, token) {

            var data = angular.copy(dataOri);

            data['MC_SIZE_LST'] = sizeList;
            if (angular.isDefined(data[key]) && data[key] > 0) {

                if (data.items.length > 0) {
                    if (data.HAS_SET == 'Y' && data.HAS_MODEL == 'N') {
                        data['items'][0]['ITEM_NAME_EN'] = data.STYLE_DESC;
                    }
                } else if (data.items.length == 0) {
                    if (data.HAS_SET == 'Y' && data.HAS_COMBO == 'Y') {
                        data['items'] = [{ MC_STYLE_D_ITEM_ID: -1, COMBO_NO: data.items[0].COMBO_NO, MODEL_NO: 'Set', ITEM_NAME_EN: data.STYLE_DESC, SEGMENT_FLAG: 'I', HAS_MODEL: data.HAS_MODEL }];
                    } else if (data.HAS_SET == 'Y' && data.HAS_COMBO == 'N' && data.HAS_MODEL == 'N') {
                        data['items'] = [{ MC_STYLE_D_ITEM_ID: -1, COMBO_NO: '', MODEL_NO: 'Set', ITEM_NAME_EN: data.STYLE_DESC, SEGMENT_FLAG: 'I', HAS_MODEL: data.HAS_MODEL }];
                    }
                }

                return MrcDataService.updateData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $state.go($state.current, $stateParams.current, { reload: 'StyleHFabDev' });
                        }

                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            } else {

                if (data.HAS_SET == 'Y' && data.HAS_COMBO == 'Y') {
                    data['items'] = [{ MC_STYLE_D_ITEM_ID: -1, COMBO_NO: data.items[0].COMBO_NO, MODEL_NO: 'Set', ITEM_NAME_EN: data.STYLE_DESC, SEGMENT_FLAG: 'I', HAS_MODEL: data.HAS_MODEL }];
                } else if (data.HAS_SET == 'Y' && data.HAS_COMBO == 'N' && data.HAS_MODEL == 'N') {
                    data['items'] = [{ MC_STYLE_D_ITEM_ID: -1, COMBO_NO: '', MODEL_NO: 'Set', ITEM_NAME_EN: data.STYLE_DESC, SEGMENT_FLAG: 'I', HAS_MODEL: data.HAS_MODEL }];
                }
                return MrcDataService.saveData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);


                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001' && res.data.V_MC_STYLE_H_ID != 0) {
                            $state.go($state.current, { MC_STYLE_H_ID: res.data.V_MC_STYLE_H_ID });
                        }

                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }

        vm.goToItemReg = function () {
            var params = angular.extend({}, $stateParams.current, { MC_STYLE_ITEM_ID: 0 })
            $state.go('Inquiry.Style.Item', params);
        }
    }

})();
//============= Start Style Master Controller ==============




//============= Start Fab. Dev. Fabrication Controller ================
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('StyleDItemFabricationFabDevController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', '$modal', 'FiberTypeList', 'YrnCount', 'Dialog', StyleDItemFabricationFabDevController]);
    function StyleDItemFabricationFabDevController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData, $modal, FiberTypeList, YrnCount, Dialog) {

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
            LK_FBR_GRP_ID: 192,
            IS_AS_SWATCH_FIB_COMP: 'N',
            IS_AS_SWATCH_FAB_TYPE: 'N',
            IS_AS_SWATCH_YRN_CNT: 'N',
            FB_WT_MIN: 0,
            FB_WT_MAX: 0
        };
        vm.IS_FEEDER_TYPE = false;

        vm.countFiltered = false;

        activate()
        vm.showSplash = true;
        function activate() {

            var promise = [getFabricCategoryList(), getFabricTypeList(), getFabricCompList(), getCotnTypeList(), getFabList(), getWashTypeList(), getFinishTypeList(),
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
            if (!$stateParams.MC_STYLE_H_ID) return;
            return MrcDataService.getDataByUrl('/StyleDItemFab/getFabricationYarn?pMC_STYLE_H_ID=' + $stateParams.MC_STYLE_H_ID).then(function (res) {
                vm.fabYarnList = res;
            });

        }

        function getSuggestedCount(RF_FIB_COMP_ID, RF_FAB_TYPE_ID, FB_WT_MIN, FB_WT_MAX) {
            if (RF_FIB_COMP_ID && RF_FAB_TYPE_ID && FB_WT_MIN && FB_WT_MAX) {
                return MrcDataService.getDataByUrl('/YarnSpec/SuggestedCount/Composition/' + RF_FIB_COMP_ID + '/Construction/' + RF_FAB_TYPE_ID + '/GsmFrom/' + FB_WT_MIN + '/GsmTo/' + FB_WT_MAX).then(function (res) {
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
                     { PART_ID: 426, RATIO: 50, CNT_ID: '' },
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
            })) {
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
                        vm.isWovenFab = false;
                    }
                    //FB02 : Woven Fabric
                    if (dataItem.ITEM_CAT_CODE == 'FB02') {
                        vm.mouUnits = [{ RF_MOU_ID: 24, MOU_NAME_EN: 'CM' }, { RF_MOU_ID: 23, MOU_NAME_EN: 'Inch' }];
                        vm.isWovenFab = true;
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
                getSuggestedCount(data.RF_FIB_COMP_ID, data.RF_FAB_TYPE_ID, data.FB_WT_MAX, data.FB_WT_MAX);
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

            if (newVal[7] < 0) {

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
                vm.Fab['FABRIC_DESC'] = (vm.yrc + ' ' + vm.composition + ' ' + vm.feedertyp + ' ' + vm.elabrand + ' ' + vm.spinType + ' ' + vm.construction + ' ' + vm.slub + ' ' + vm.mellange + ' ' + vm.range + ' ' + vm.washType + ' ' + vm.finishType + ' ' + vm.colCuffDes).replace('  ', ' ').replace('   ', ' ').replace('    ', ' ').replace('Cotton', vm.cotnType + ' Cotton');;
            }
        });


        function getFabGroupList() {
            return MrcDataService.LookupListData(45).then(function (res) {
                vm.FabGroup = res;
            });
        }


        vm.deleteFabData = function (MC_STYLE_D_FAB_ID, token, FABRIC_DESC) {
            Dialog.confirm('Removing Fabrication :' + FABRIC_DESC, 'Are you sure?', ['Yes', 'No'])
             .then(function () {
                 return MrcDataService.delDataByUrl(
                     '/StyleDItemFab/Delete/' + MC_STYLE_D_FAB_ID,
                      {},
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
                                vm.CotnTypeListData = angular.copy(res);
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
            select: function (e) {
                if (this.dataItem(e.item).RF_YRN_CNT_ID!=34) {
                    vm.Fab.IS_AS_SWATCH_YRN_CNT = 'N';
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

                    if (data.RF_FAB_TYPE_ID != 40) {
                        vm.Fab.IS_AS_SWATCH_FAB_TYPE = 'N';
                    }

                    vm.isRatioHidden = (data.IS_FBP_VISIBLE === 'N') ? true : false;
                    vm.isLoopDisabled = data.LK_YFAB_PART_LST.split(',').length == 3 ? false : true;

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
                        vm.construction = '';
                        vm.Fab.LK_FBR_GRP_ID = 192;
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

                        vm.Fab['YR_FIB_COMP_ID'] = this.dataItem(e.item).RF_FIB_COMP_ID;

                        vm.Fab['RF_ELA_CNT_ID'] = null;
                        vm.Fab['LK_ELA_BRAND_ID'] = null;
                        vm.elabrand = '';
                        vm.feedertyp = '';
                    }

                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(this.dataItem(e.item).IS_ELA_MXD);
                    
                    
                    if (item.RF_FIB_COMP_ID != 360) {
                        vm.Fab.IS_AS_SWATCH_FIB_COMP = 'N';
                    }

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

                        return MrcDataService.getDataByUrl('/YarnSpec/YrnParamForFabri?RF_FIB_COMP_ID=' + vm.Fab['YR_FIB_COMP_ID']).then(
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
                //console.log('===============');
                //console.log(data);

                //alert('X');

                return MrcDataService.updateData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            if ($state.current.LK_STYL_DEV_ID == 555) {
                                $state.go($state.current, $stateParams.current, { reload: 'StyleHFabDev.Fab' });

                            }

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

                            if ($state.current.LK_STYL_DEV_ID == 555) {
                                $state.go($state.current, $stateParams.current, { reload: 'StyleHFabDev.Fab' });
                            }

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

        vm.onCountChange = function (RF_YRN_CNT_LST, ID) {
            if (vm.isLoopDisabled) {
                RF_YRN_CNT_LST[0].RATIO = 50;
                RF_YRN_CNT_LST[2].RATIO = 50;
                RF_YRN_CNT_LST[1].RATIO = 0;
                RF_YRN_CNT_LST[1].CNT_ID = '';
                RF_YRN_CNT_LST[0].CNT_ID = RF_YRN_CNT_LST[0].CNT_ID;
                RF_YRN_CNT_LST[2].CNT_ID = RF_YRN_CNT_LST[0].CNT_ID;

                if (vm.isRatioHidden) {
                    RF_YRN_CNT_LST[2].CNT_ID = RF_YRN_CNT_LST[0].CNT_ID;
                }

            } else {
                RF_YRN_CNT_LST[0].RATIO = 33.33;
                RF_YRN_CNT_LST[1].RATIO = 33.33;
                RF_YRN_CNT_LST[2].RATIO = 33.33;

                RF_YRN_CNT_LST[1].CNT_ID = RF_YRN_CNT_LST[0].CNT_ID;
                RF_YRN_CNT_LST[2].CNT_ID = RF_YRN_CNT_LST[0].CNT_ID;
            }
        }

        vm.onChangeSwtchYrnCtn = function () {
            if (vm.Fab.IS_AS_SWATCH_YRN_CNT == 'Y') {
                vm.Fab.RF_YRN_CNT_LST[0].CNT_ID = 34;
                vm.Fab.RF_YRN_CNT_LST[2].CNT_ID = 34;
            }
            else {
                vm.Fab.RF_YRN_CNT_LST[0].CNT_ID = '';
                vm.Fab.RF_YRN_CNT_LST[2].CNT_ID = '';
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
//============= End Fab. Dev. Fabrication Controller ================




//=============== Start for Fabric Color & KP =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('StyleDFabricBookingFabDevController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'formData', 'commonDataService', '$modal', StyleDFabricBookingFabDevController]);
    function StyleDFabricBookingFabDevController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, $filter, formData, commonDataService, $modal) {
        
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

        console.log('===========');
        console.log(formData);
        var key = 'MC_FAB_PROD_ORD_H_ID';

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';

        if (formData[key]) {
            $state.go('StyleHFabDev.FabrBook', { MC_FAB_PROD_ORD_H_ID: formData[key] }, { notify: false });
        }

        vm.form = formData[key] ? formData: { MC_FAB_PROD_ORD_H_ID: -1, RF_FAB_PROD_CAT_ID: -1, MC_BYR_ACC_ID: 0, MC_STYLE_H_ID: 0, MC_STYLE_H_EXT_ID: '', PROD_ORD_DT: $filter('date')(vm.today, vm.dtFormat) };
        vm.fabOrder = formData[key] ? formData : { MC_FAB_PROD_ORD_D_ID: -1, MC_FAB_PROD_ORD_H_ID: -1, FIN_DIA_N_DIA_TYPE: '', DIA_MOU_CODE: 'Inch', FIN_DIA_TYPE: 'Open', QTY_MOU_CODE: 'Kg' };


        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getProdTypeList(), /*getBuyerAcList(),*/ getBuyerAcGrpList(), getByrAccWiseStyleExtList(), getFabricByStyleId(), getFabTypeList(), getFabricCompList(),
                getMOUList(), getDiaTypeList(), getColorList(), getMcDiaList(), getMcGgList()
            ];
            return $q.all(promise).then(function () {
                //vm.fabOrderGridDataSource.read();

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
                                e.success(_.filter(res, function (ob) { return ob.FAB_PROD_CAT_CODE == 'FP03'; }));
                            });
                        }
                    }
                },
                dataTextField: "FAB_PROD_CAT_SNAME",
                dataValueField: "RF_FAB_PROD_CAT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.LK_FAB_QTY_SRC_ID = 435;//((item.FAB_PROD_CAT_CODE == 'FP01') ? 434 : 435);
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
                        KnittingDataService.getDataByFullUrl('/api/mrc/StyleDItemFab/SelectFabByStyleID/' + (($stateParams.MC_STYLE_H_ID > 0) ? $stateParams.MC_STYLE_H_ID : 0)).then(function (res) {
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
                            controller: function (ColourList, $modalInstance, $scope) {
                                var vm = this;
                                vm.errors = null;
                                vm.AopBaseColName = '';
                                vm.colTypeName = '';
                                vm.Title = $state.current.Title || '';

                                vm.ColourList = _.filter(ColourList, { 'LK_COL_TYPE_ID': 358 }).map(function (obj) {
                                    return {
                                        MC_COLOR_ID: obj.MC_COLOR_ID,
                                        COLOR_NAME_EN: obj.COLOR_NAME_EN
                                    }
                                });


                                vm.form = { IS_ACTIVE: 'Y', IS_SWATCH: 'N', PNT_VERSION_NO: 1, LK_COL_TYPE_ID: 358 };


                                activate()
                                vm.showSplash = true;
                                function activate() {
                                    var promise = [getColourTypelist(), getAllColorListForDropDown()];
                                    return $q.all(promise).then(function () {
                                        vm.showSplash = false;

                                    });
                                }

                                $scope.$watchGroup(['vm.form.LK_COL_TYPE_ID', 'vm.form.AOP_BASE_COL_ID', 'vm.form.YD_COL_LST', 'vm.form.MC_COLOR_ID'], function (newVal, oldVal) {
                                    if (!newVal[3]) {
                                        if (newVal[0] && parseInt(newVal[0]) == 361) {
                                            vm.form['COLOR_NAME_EN'] = vm.AopBaseColName + ' AOP';
                                            vm.form['YD_COL_LST'] = '';

                                        } else if (newVal[0] && parseInt(newVal[0]) == 360) {
                                            vm.form['COLOR_NAME_EN'] = _.map(newVal[2], 'COLOR_NAME_EN').join(' / ') + ' (Y/D)';
                                            vm.form['AOP_BASE_COL_ID'] = '';
                                            vm.form['AOP_PRNT_COL_LST'] = '';
                                        } else if (newVal[0] && parseInt(newVal[0]) == 432) {
                                            vm.form['COLOR_NAME_EN'] = _.map(newVal[2], 'COLOR_NAME_EN').join(' / ') + ' (Mixed)';
                                            vm.form['AOP_BASE_COL_ID'] = '';
                                            vm.form['AOP_PRNT_COL_LST'] = '';

                                        } else {
                                            vm.form['COLOR_NAME_EN'] = '';
                                            vm.form['AOP_BASE_COL_ID'] = '';
                                            vm.form['YD_COL_LST'] = '';
                                            vm.form['AOP_PRNT_COL_LST'] = '';

                                        }
                                    }

                                });

                                function getAllColorListForDropDown() {
                                    return vm.AllColourlist = {
                                        optionLabel: "-- Colour--",
                                        filter: "startswith",
                                        autoBind: false,
                                        dataSource: {
                                            transport: {
                                                read: function (e) {
                                                    e.success(vm.ColourList)
                                                }
                                            }
                                        },

                                        select: function (e) {
                                            vm.AopBaseColName = this.dataItem(e.item).COLOR_NAME_EN;
                                        },
                                        dataBound: function (e) {
                                            vm.AopBaseColName = this.dataItem(e.item).COLOR_NAME_EN;
                                        },
                                        dataTextField: "COLOR_NAME_EN",
                                        dataValueField: "MC_COLOR_ID"
                                    };
                                }
                                function getColourTypelist() {
                                    return vm.ColourTypelist = {
                                        optionLabel: "-- Colour Type--",
                                        filter: "startswith",
                                        autoBind: true,
                                        dataSource: {
                                            transport: {
                                                read: function (e) {
                                                    KnittingDataService.LookupListData(74).then(function (res) {
                                                        e.success(res);
                                                    }, function (err) {
                                                        console.log(err);
                                                    });
                                                }
                                            }
                                        },
                                        dataBound: function (e) {
                                            var data = this.dataItem(e.item);
                                            vm.colTypeName = data.LK_DATA_NAME_EN;
                                        },
                                        select: function (e) {
                                            var data = this.dataItem(e.item);
                                            vm.colTypeName = data.LK_DATA_NAME_EN;
                                        },
                                        dataTextField: "LK_DATA_NAME_EN",
                                        dataValueField: "LOOKUP_DATA_ID"
                                    };
                                };

                                vm.submitData = function (dataOri, token) {
                                    var data = angular.copy(dataOri);
                                    data['AOP_PRNT_COL_LST'] = config.xmlStringLong(data.AOP_PRNT_COL_LST);
                                    data['YD_COL_LST'] = config.xmlStringLong(data.YD_COL_LST);

                                    return KnittingDataService.saveDataByFullUrl(data, '/api/mrc/ColourMaster/Save', token).then(function (res) {
                                        if (res.success === false) {
                                            vm.errors = res.errors;
                                        }
                                        else {
                                            res['data'] = angular.fromJson(res.jsonStr);
                                            if (res.data.V_MC_COLOR_ID != 0 && res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                                data['MC_COLOR_ID'] = parseInt(res.data.V_MC_COLOR_ID);
                                                $modalInstance.close(data);
                                            }
                                        }
                                    }).catch(function (message) {
                                        exception.catcher('XHR loading Failded')(message);
                                    });
                                }

                                $scope.cancel = function (data) {
                                    data['COL_TYPE_NAME'] = vm.colTypeName;
                                    $modalInstance.close(data);
                                };
                            },
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
                { field: "MC_DIA_N_GG", title: "MC DiaXgg", width: "15%", hidden: true },
                { field: "FIN_DIA_N_DIA_TYPE", title: "Fin.Dia", width: "15%", hidden: true },
                { field: "FIN_GSM", title: "Fin.GSM", width: "15%" },
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
            submitData['MC_STYLE_H_ID'] = $stateParams.MC_STYLE_H_ID;
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

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/FabProdKnitOrder/SaveFabDevKnitOrd').then(function (res) {
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
                                    $stateParams.MC_FAB_PROD_ORD_H_ID = res.data.PMC_FAB_PROD_ORD_H_RTN;
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





//============= Start Fab. Dev. Style List Controller ================
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('StyleListFabDevController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', '$modal', StyleListFabDevController]);
    function StyleListFabDevController($q, config, MrcDataService, $stateParams, $state, $scope, logger, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        activate()
        vm.showSplash = true;

        function activate() {
            var promise = [getBuyerAccListByUserId()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
                vm.form.MC_BYR_ACC_ID = (angular.isDefined($stateParams.BAC) && $stateParams.BAC > 0) ? $stateParams.BAC : '';
            });
        }

        vm.getStyleData = function (MC_BYR_ACC_ID) {
            vm.StyleDataSource = [];

            var dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                schema: {
                    data: "data",
                    total: "total"
                },
                transport: {

                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/StyleH/BuyerWiseStyleList?pMC_BYR_ACC_ID=' + MC_BYR_ACC_ID;
                        url += '&pLK_STYL_DEV_ID=' + $state.current.LK_STYL_DEV_ID;
                        url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += MrcDataService.kFilterStr2QueryParam(params.filter);
                        MrcDataService.getDataByUrl(url).then(function (res) {
                            e.success(res);
                            vm.StyleDataSource = _.uniq(_.map(res.data, 'STYLE_NO'));
                        }, function (err) {
                            console.log(err);
                        })
                    }
                },
                pageSize: 20,
                group: [{ field: 'STYLE_NO' }]
            });

            $('#kendoGrid').data("kendoGrid").setDataSource(dataSource);

        }


        function getBuyerAccListByUserId() {
            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }

        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                serverPaging: true,
                schema: {
                    data: "data",
                    total: "total"
                },
                transport: {
                    read: function (e) {
                        if (angular.isDefined($stateParams.BAC) && $stateParams.BAC > 0) {
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);
                            var url = '/StyleH/BuyerWiseStyleList?pMC_BYR_ACC_ID=' + $stateParams.BAC;
                            url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                            url += '&pLK_STYL_DEV_ID=' + $state.current.LK_STYL_DEV_ID;
                            url += MrcDataService.kFilterStr2QueryParam(params.filter);

                            return MrcDataService.getDataByUrl(url).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.error(err);
                            })
                        } else {
                            e.success([]);
                        }

                    }
                },
                pageSize: 20,
                group: [{ field: 'STYLE_NO' }],
                dataBound: function (e) {
                    collapseAllGroups(this);
                }
            },
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
            selectable: false,
            dataBound: function (e) {
                collapseAllGroups(this);
            },
            sortable: true,
            pageable: true,
            columns: [
                {
                    title: '',
                    template: function () {                        
                        var tmplFabDev = "</a><a ui-sref='StyleHFabDev.Fab({MC_STYLE_H_ID:dataItem.MC_STYLE_H_ID, MC_STYLE_D_FAB_ID:0})' class='btn btn-xs blue'><i class='fa fa-edit'></i></a></a>";
                        return tmplFabDev;
                    },
                    width: "40px"
                },
                { title: 'Image', template: '<img ng-click="vm.openShowImageModal(dataItem.STYL_KEY_IMG)" data-ng-src="data:image/png;base64,#:data.STYL_KEY_IMG#" title="Click for Image Enlarge" alt="No Photo" style="border:1px solid black; width:45px" />', width: "60px" },
                {
                    field: "STYLE_NO", title: "Style No.", type: "string", width: "100px", filterable: {
                        ui: styleFilter
                    }
                },
                { field: "ITEM_CAT_NAME_EN", title: "Item Category", type: "string", width: "100px", filterable: true },
                { field: "GARM_DEPT_NAME", title: "Division", type: "string", width: "80px", filterable: true },
                { field: "ITEM_NAME_EN", title: "Item Name", type: "string", width: "250px", filterable: false },
                { field: "MODEL_NO", title: "Model.", type: "string", width: "60px", filterable: false },
                { field: "COMBO_NO", title: "Combo", type: "string", width: "60px", filterable: false },
                { field: "SEASON", title: "Season", type: "number", width: "100px", filterable: true },
                { field: "GARM_TYPE_NAME", title: "Nature Of Work", type: "string", width: "150px", filterable: true }
            ]
        };

        function styleFilter(element) {
            element.kendoAutoComplete({
                dataSource: vm.StyleDataSource
            });
        }

        function collapseAllGroups(grid) {
            grid.table.find(".k-grouping-row").each(function () {
                grid.collapseGroup(this);
            });
        }


        //vm.openShowImageModal = function (image) {
        //    var modalInstance = $modal.open({
        //        animation: true,
        //        templateUrl: 'ImageShowup',
        //        controller: function ($scope, $modalInstance) {
        //            $scope.image = image;
        //            $scope.cancel = function () {
        //                $modalInstance.dismiss();
        //            }
        //        },
        //        windowClass: 'large-Modal',
        //        size: 'lg'
        //    });
        //};

        vm.openShowImageModal = function (image) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'StyleItemImageStyleG.html',
                controller: function ($scope, $modalInstance, image) {
                    $scope.image = image;
                    $scope.cancel = function (data) {
                        $modalInstance.dismiss();
                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    image: function () {
                        return image;
                    }
                }
            });
        }

    }

})();
//============= End Fab. Dev. Style List Controller ================
