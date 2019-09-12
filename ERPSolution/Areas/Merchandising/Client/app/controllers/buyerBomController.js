(function () {
    'use strict';
    angular.module('multitex.mrc').controller('BuyerBomController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'MouData', '$modal', BuyerBomController]);
    function BuyerBomController($q, config, MrcDataService, $stateParams, $state, $scope, logger, MouData, $modal) {

        var vm = this;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.ItemData = [];
        vm.itemIntoView = [];
        vm.ItemOfRest = [];
        vm.refreshData = [];
        vm.isSubmitted = false;
        vm.ItemDataOri = [];
        var BuyerList = [];

        vm.form = {};

        vm.BuyerAccDs = new kendo.data.DataSource({
            data: []
        });


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getMappedBuyerList(), getMOUList(), getBuyerListData(), getConsList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;

            });
        }



        vm.openSuplierModal = function (data) {
            console.log(data);
            data.MC_BYR_ACC_ID = vm.form.MC_BYR_ACC_ID;

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'NominatedSupplierView.html',
                controller: function ($scope, $modalInstance, MrcDataService, formData) {

                    console.log(formData);
                    //formData['SUP_NAME_LIST'] = [];
                    $scope.SupplierRateList = formData.NOM_SUP_LST;
                    GetSupplierList();
                    $scope.IS_EDIT = 0;

                    $scope.form = { MC_BYR_NOM_SUP_ID: 0, MC_BYR_ACC_ID: formData.MC_BYR_ACC_ID, SCM_SUPPLIER_ID: 0, INV_ITEM_ID: formData.INV_ITEM_ID, RATE: '' };


                    function GetSupplierList() {
                        return $scope.supplierList = new kendo.data.DataSource({
                            transport: {
                                read: function (e) {
                                    MrcDataService.getDataByFullUrl('/api/purchase/supplierprofile/SelectAll').then(function (res) {
                                        e.success(res);

                                    }, function (err) {
                                        console.log(err);
                                    });
                                }
                            }
                        });
                    };

                    $scope.editItem = function (item) {
                        $scope.form = angular.copy(item);
                        $scope.IS_EDIT = 1;
                    }

                    $scope.removeItem = function (item) {
                        var idx = $scope.SupplierRateList.indexOf(item);
                        console.log(idx);
                        $scope.SupplierRateList.splice(idx,1);

                    }



                    $scope.save = function (data) {

                        var obj = angular.copy(data);
                        var idx = $scope.SupplierRateList.length + 1;
                        var rateList = _.filter($scope.SupplierRateList, function (x) { return x.SCM_SUPPLIER_ID == $scope.form.SCM_SUPPLIER_ID });
                        console.log(rateList);
                        if (rateList.length == 0) {
                            var sup = _.filter($scope.supplierList.data(), function (x) { return x.SCM_SUPPLIER_ID == $scope.form.SCM_SUPPLIER_ID });
                            obj['SUP_TRD_NAME_EN'] = sup[0].SUP_TRD_NAME_EN;

                            $scope.SupplierRateList.splice(idx, 0, obj);
                        }
                        else {
                            for (var i = 0; i < $scope.SupplierRateList.length; i++) {
                                if($scope.SupplierRateList[i].SCM_SUPPLIER_ID==$scope.form.SCM_SUPPLIER_ID)
                                    $scope.SupplierRateList[i].RATE = obj.RATE;
                            }
                        }
                        $scope.IS_EDIT = 0;
                        $scope.form = { MC_BYR_NOM_SUP_ID: 0, MC_BYR_ACC_ID: formData.MC_BYR_ACC_ID, SCM_SUPPLIER_ID: 0, INV_ITEM_ID: formData.INV_ITEM_ID, RATE: '' };
                    };

                    $scope.cancel = function (data) {
                        $modalInstance.dismiss();
                    }
                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        return data;
                    }
                }
            });

            modalInstance.result.then(function (dataItem) {
                data.NOM_SUP_LST = dataItem;
                console.log(dataItem);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }



        function getMappedBuyerList() {
            return MrcDataService.getDataByUrl('/buyer/MappedBuyerList?pOption=3006').then(function (res) {
                vm.dataSource = new kendo.data.DataSource({
                    data: res,
                    pageSize: 10
                });
            });
        }

        function getBuyerListData() {
            return vm.buyerList = {
                optionLabel: "-- Buyer --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.BuyerByUserList().then(function (res) {
                                BuyerList = res;
                                e.success(_.map(_.groupBy(res, function (o) {
                                    return o.MC_BUYER_ID;
                                }), function (grouped) {
                                    return grouped[0];
                                }));
                            });


                        }
                    }
                },
                change: function (e) {
                    var MC_BUYER_ID = this.dataItem(e.item).MC_BUYER_ID;

                    var buyerAccData = _.filter(BuyerList, function (o) {
                        return o.MC_BUYER_ID === MC_BUYER_ID;
                    });

                    if (MC_BUYER_ID) {
                        vm.BuyerAcc = new kendo.data.DataSource({
                            data: buyerAccData
                        });
                    }
                },
                dataTextField: "BUYER_NAME_EN",
                dataValueField: "MC_BUYER_ID"
            };
        }



        vm.buyerAccListOnBound = function (e) {
            var ds = e.sender.dataSource.data();
            if (ds.length == 1) {
                e.sender.value(ds[0].MC_BYR_ACC_ID);
                vm.form['MC_BYR_ACC_ID'] = ds[0].MC_BYR_ACC_ID;
            } else {
                e.sender.value();
                vm.form['MC_BYR_ACC_ID'] = '';
            }
        };


        vm.setCommonChecked = function (data, IS_COMMON) {
            angular.forEach(data.inv_items_view, function (val, key) {
                val.IS_COMMON = IS_COMMON;
            })
        }


        vm.getItemData = function (MC_BUYER_ID, params, MC_BYR_ACC_ID) {

            var ItemData = [];
            if (!MC_BUYER_ID) {
                return;
            }


            if (!params) {
                return MrcDataService.getDataByUrl('/StyleH/ItemDataForBuyerBOM/' + MC_BUYER_ID + '?pMC_BYR_ACC_ID=' + MC_BYR_ACC_ID).then(function (data) {


                    var MappedState = _.some(data[0].inv_itm_byr_bom, function (o) {
                        return o.MC_BUYER_BOM_ID > 0;
                    });

                    angular.copy(data).forEach(function (val, key) {


                        val['inv_items_view'] = _.filter(val.inv_itm_byr_bom, function (o) {
                            return (o.IS_COMMON == 'Y' && o.MC_BUYER_BOM_ID < 0 && !MappedState) || (o.MC_BUYER_BOM_ID > 0);
                        });

                        val['inv_items_rest'] = _.filter(val.inv_itm_byr_bom, function (o) {
                            return !((o.IS_COMMON == 'Y' && o.MC_BUYER_BOM_ID < 0 && !MappedState) || (o.MC_BUYER_BOM_ID > 0));
                        });

                        ItemData.push(val);


                    });
                    vm.ItemData = ItemData;
                    vm.ItemDataOri = angular.copy(vm.ItemData);

                }, function (err) {
                    console.error(err);
                });


            } else {
                vm.ItemData = params;
                vm.ItemDataOri = angular.copy(vm.ItemData)
            }
        };

        vm.cancel = function (MC_BUYER_ID, MC_BYR_ACC_ID) {

            if (vm.ItemDataOri && vm.ItemDataOri.length > 0) {
                vm.getItemData(MC_BUYER_ID, vm.ItemDataOri, MC_BYR_ACC_ID)
            } else {
                vm.getItemData(MC_BUYER_ID, null, MC_BYR_ACC_ID)
            }
        }

        function getMOUList() {
            return vm.MOUList = {
                optionLabel: "--Unit--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(MouData);
                        }
                    }
                },
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"
            };
        }

        vm.remove = function (Source, itemToBeRemoved) {
            var index = Source.inv_items_view.indexOf(itemToBeRemoved);
            Source.inv_items_rest.push(itemToBeRemoved);
            Source.inv_items_view.splice(index, 1);
        };

        vm.onSelectMapedBuyer = function (dataItem) {
            var buyerAccData = _.filter(BuyerList, function (o) {
                return o.MC_BUYER_ID === dataItem.MC_BUYER_ID;
            });

            vm.BuyerAcc = new kendo.data.DataSource({
                data: buyerAccData
            });

            vm.form = {
                MC_BUYER_ID: dataItem.MC_BUYER_ID,
                MC_BYR_ACC_ID: dataItem.MC_BYR_ACC_ID
            };
        };


        vm.add = function (Source) {

            vm.ItemListForDD = {
                optionLabel: "--Item--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(Source.inv_items_rest);
                        }
                    }
                },
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "INV_ITEM_ID"
            };



            Source.inv_items_view.push({
                MC_BUYER_BOM_ID: -1,
                NOM_SUP_LST: []
            });
        }


        vm.editItem = function (Source, item) {
            item.edit = !item.edit;

            if (item.edit) {
                Source.inv_items_rest.push(item);
            } else {
                Source.inv_items_rest.splice(Source.inv_items_rest.indexOf(item), 1);
            }



            vm.ItemListForDD = {
                optionLabel: "--Item--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(Source.inv_items_rest);
                        }
                    }
                },
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "INV_ITEM_ID"
            };


        }


        vm.onItemChange = function (data, source) {
            if (data.INV_ITEM_ID) {

                var itemExist = _.filter(source.inv_items_view, function (o) {
                    return o.INV_ITEM_ID == parseInt(data.INV_ITEM_ID);
                }).length;

                if (itemExist == 1) {
                    var itemData = _.filter(source.inv_items_rest, function (o) {
                        return o.INV_ITEM_ID == parseInt(data.INV_ITEM_ID);
                    })[0];

                    data['INV_ITEM_CAT_ID'] = itemData.INV_ITEM_CAT_ID;
                    data['PURCH_MOU_ID'] = itemData.PURCH_MOU_ID;
                    data['CONS_MOU_ID'] = itemData.CONS_MOU_ID;
                } else {
                    data['INV_ITEM_ID'] = '';
                    return;
                }



            };
        }


        function getConsList() {
            return vm.ConsList = {
                optionLabel: "--N/A--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(155).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        vm.submitData = function (MC_BUYER_ID, dataOri, token, MC_BYR_ACC_ID) {
            var dataToBeSaved = [];
            console.log(vm.ItemDataOri);
            angular.copy(dataOri).forEach(function (val, key) {
                val.inv_items_view.forEach(function (val1, key1) {
                    dataToBeSaved.push({
                        CONS_MOU_ID: val1.CONS_MOU_ID,
                        PURCH_MOU_ID: val1.PURCH_MOU_ID,
                        MC_BUYER_BOM_ID: val1.MC_BUYER_BOM_ID,
                        INV_ITEM_ID: parseInt(val1.INV_ITEM_ID),
                        REMARKS: val1.REMARKS || '',
                        RATE_EST: val1.RATE_EST || 0,
                        IS_COMMON: val1.IS_COMMON || 'N',
                        IS_SUPL_NOM: val1.IS_SUPL_NOM,
                        LK_ACS_CONS_TYPE_ID: val1.LK_ACS_CONS_TYPE_ID,
                        IS_UPDATE: 'Y',
                        NOM_SUP_LST: config.xmlStringShortNoTag(val1.NOM_SUP_LST.map(function (o) {
                            return {
                                MC_BYR_NOM_SUP_ID: o.MC_BYR_NOM_SUP_ID,
                                MC_BYR_ACC_ID: o.MC_BYR_ACC_ID,
                                SCM_SUPPLIER_ID: o.SCM_SUPPLIER_ID,
                                INV_ITEM_ID: o.INV_ITEM_ID,
                                RATE: o.RATE
                            }
                        })),
                    });
                });
            });

            var XML = MrcDataService.xmlStringShort(dataToBeSaved);
            return MrcDataService.saveDataByUrl({ MC_BUYER_ID: MC_BUYER_ID, MC_BYR_ACC_ID: MC_BYR_ACC_ID, XML: XML }, '/StyleH/SaveBuyerBomData', token).then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.getItemData(MC_BUYER_ID, null, MC_BYR_ACC_ID);
                        getMappedBuyerList()
                        vm.isSubmitted = true;
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });

        }

    }

})();