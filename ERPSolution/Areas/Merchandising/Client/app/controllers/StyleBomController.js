(function () {
    'use strict';
    angular.module('multitex.mrc').controller('StyleBomController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'MouData', StyleBomController]);
    function StyleBomController($q, config, MrcDataService, $stateParams, $state, $scope, logger, MouData) {

        var vm = this;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.ItemData = [];
        vm.itemIntoView = [];
        vm.ItemOfRest = [];
        vm.refreshData = [];
        vm.isSubmitted = false;
        vm.ItemDataOri = [];


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [buildData(), getMOUList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
      
            });
        }


        function buildData(params) {

            if (!params) {
                return MrcDataService.getDataByUrl('/StyleH/ItemDataForStyleBOM/' + $stateParams.MC_STYLE_H_ID + '?MC_BUYER_ID=' + $scope.$parent.StyleData.MC_BUYER_ID + '&pMC_BYR_ACC_ID=' + $scope.$parent.StyleData.MC_BYR_ACC_ID).then(function (data) {
                    vm.ItemDataOri = angular.copy(data);

                    angular.copy(data).forEach(function (val, key) {
                        var MappedState = _.some(data[0].inv_items, function (o) {
                            return o.MC_STYLE_D_BOM_ID > 0;
                        });

                        val.IS_PCT = _.some(val.inv_items, function (o) {
                            return o.IS_PCT === 'Y';
                        }) ? 'Y' : 'N';

                        val['inv_items_view'] = _.filter(val.inv_items, function (o) {
                            return (o.IS_COMMON == 'Y' && o.MC_STYLE_D_BOM_ID < 0 && !MappedState) || (o.MC_STYLE_D_BOM_ID > 0);
                        });

                        val['inv_items_rest'] = _.filter(val.inv_items, function (o) {
                            return !((o.IS_COMMON == 'Y' && o.MC_STYLE_D_BOM_ID < 0 && !MappedState) || (o.MC_STYLE_D_BOM_ID > 0));
                        });

                        vm.ItemData.push(val);
                    });

                }, function (err) {
                    console.error(err);
                });


            } else {

                vm.ItemDataOri = angular.copy(params);
                angular.copy(params).forEach(function (val, key) {

                    var MappedState = _.some(params[0].inv_items, function (o) {
                        return o.MC_STYLE_D_BOM_ID > 0;
                    });

                    val.IS_PCT = _.some(val.inv_items, function (o) {
                        return o.IS_PCT === 'Y';
                    }) ? 'Y' : 'N';

                    val['inv_items_view'] = _.filter(val.inv_items, function (o) {
                        return (o.IS_COMMON == 'Y' && o.MC_STYLE_D_BOM_ID < 0 && !MappedState) || (o.MC_STYLE_D_BOM_ID > 0);
                    });

                    val['inv_items_rest'] = _.filter(val.inv_items, function (o) {
                        return !((o.IS_COMMON == 'Y' && o.MC_STYLE_D_BOM_ID < 0 && !MappedState) || (o.MC_STYLE_D_BOM_ID > 0));
                    });
                    vm.ItemData.push(val);
                });
            }




        };

        vm.cancel = function () {
            vm.ItemData = [];
            buildData(vm.isSubmitted ? vm.refreshData : vm.ItemDataOri);
        }

        vm.setPctChecked = function(data, IS_PCT){
            angular.forEach(data.inv_items_view, function (val, key) {
                val.IS_PCT = IS_PCT;
            })
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


        vm.add = function (Source) {

            vm.ItemListForDD = {
                optionLabel: "--Item--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            //var webapi = new kendo.data.transports.webapi({});
                            //var params = webapi.parameterMap(e.data);
                            //console.log(params);

                            e.success(Source.inv_items_rest);
                        }
                    },
                    //serverFiltering: true
                },
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "INV_ITEM_ID"
            };



            Source.inv_items_view.push({
                MC_STYLE_D_BOM_ID: -1,
                UNIT_QTY_REQ: 1
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
                            //var webapi = new kendo.data.transports.webapi({});
                            //var params = webapi.parameterMap(e.data);
                            //console.log(params);
                            e.success(Source.inv_items_rest);
                        }
                    },
                    //serverFiltering: true
                },
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "INV_ITEM_ID"
            };


        }


        vm.onItemChange = function (data,source) {
            if (data.INV_ITEM_ID) {

                var itemExist = _.filter(source.inv_items_view, function (o) {
                    return o.INV_ITEM_ID == parseInt(data.INV_ITEM_ID);
                }).length;

                if (itemExist==1) {
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


        function refreshData() {
            vm.showSplash = true;
            vm.ItemData = [];
            return MrcDataService.getDataByUrl('/StyleH/ItemDataForStyleBOM/' + $stateParams.MC_STYLE_H_ID + '?MC_BUYER_ID=' + $scope.$parent.StyleData.MC_BUYER_ID + '&pMC_BYR_ACC_ID=' + $scope.$parent.StyleData.MC_BYR_ACC_ID).then(function (res) {
                buildData(res);
                vm.refreshData = res;
                vm.showSplash = false;
            }, function (err) {
                console.error(err);
            })
        }

        vm.submitData = function (dataOri, token) {
            
            var dataToBeSaved = [];
       
            angular.copy(dataOri).forEach(function (val, key) {
                val.inv_items_view.forEach(function (val1, key1) {
                    dataToBeSaved.push({
                        CONS_MOU_ID: val1.CONS_MOU_ID,
                        INV_ITEM_CAT_ID: val1.INV_ITEM_CAT_ID,
                        MC_STYLE_D_BOM_ID: val1.MC_STYLE_D_BOM_ID,
                        INV_ITEM_ID: parseInt(val1.INV_ITEM_ID),
                        POSITION_DESC: val1.POSITION_DESC||'',
                        PURCH_MOU_ID: val1.PURCH_MOU_ID,
                        REMARKS: val1.REMARKS||'',
                        UNIT_QTY_REQ: val1.UNIT_QTY_REQ,
                        RATE_EST: val1.RATE_EST||0,
                        PCT_ADD_QTY: val1.PCT_ADD_QTY||0,
                        IS_PCT: val1.IS_PCT ||'N'
                    });
                });
            });

            var XML = MrcDataService.xmlStringShort(dataToBeSaved);

            return MrcDataService.saveDataByUrl({ MC_STYLE_H_ID: $stateParams.MC_STYLE_H_ID, XML: XML }, '/StyleH/SaveStyleBomData', token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            refreshData();
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