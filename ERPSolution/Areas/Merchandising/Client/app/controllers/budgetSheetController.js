(function () {
    'use strict';
    angular.module('multitex.mrc').controller('BudgetSheetController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', '$filter', 'MouData', 'ItemData', 'Dialog', '$modal', '$window', BudgetSheetController]);
    function BudgetSheetController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData, $filter, MouData, ItemData, Dialog, $modal, $window) {
        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.TOT_HEAD_COST = 0;
        if (formData.REVISION_DT) {
            formData['REVISION_DT'] = $filter('date')(formData.REVISION_DT, config.appDateFormat);
        }

        vm.form = formData;
        vm.ItemData = [];
        vm.ItemDataFPD = [];
        var FabProcessCost = [];
        var costHeadsOri = [];
        vm.MC_STYLE_BLK_BOM_ID_LIST = [];

        activate();
        function activate() {
            var promise = [getMOUList(), buildData(ItemData)];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.getFile = function (data) {
            MrcDataService.post4pdfRes('/api/mrc/MrcReport/PreviewReport',
                {
                    PAGE_SIZE_NAME: 'A4',
                    REPORT_CODE: 'RPT-2001',
                    MC_BLK_REVISION_NO : data.BK_REVISION_NO,
                    MC_BLK_FAB_REQ_H_ID: data.MC_BLK_FAB_REQ_H_ID
                }
                ).then(function (res) {
                    var file = new Blob([res], { type: 'application/pdf' });
                    var fileURL = URL.createObjectURL(file);
                    $window.open(fileURL, "_blank", "location=yes,height=570,width=800,scrollbars=yes,status=yes");
                })
        }

        $scope.openRateDyeRateChart = function () {
            let params = {}
            let form = document.createElement('form');
            form.setAttribute('method', 'post');
            form.setAttribute('action', "/api/common/OpenPdf?pdfName=DyeingRateChart");
            form.setAttribute('target', '_blank');

            for (let i in params) {
                if (params.hasOwnProperty(i)) {
                    let input = document.createElement('input');
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

        function getMOUList() {
            vm.MOUList = {
                optionLabel: "Unt",
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

            vm.MOUListFake = {
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success([{RF_MOU_ID:5,MOU_CODE:'Doz'}]);
                        }
                    }
                },
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"
            };

        }

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        function buildData(data) {
            var MappedState = _.some(data[0].inv_items_blk, function (o) {
                return o.MC_STYLE_BLK_BOM_ID > 0;
            })

            angular.copy(data).forEach(function (val, key) {

                val['inv_items_view'] = !MappedState ? _.filter(val.inv_items_blk, function (o) {
                    if (key == 2 || key == 3, key == 4) {
                        return o.SUGGESTED == 'Y';
                    } else {
                        return o.SUGGESTED == 'Y' || o.QTY_EST > 0;
                    }
                   
                }) : _.filter(val.inv_items_blk, function (o) {
                    return o.MC_STYLE_BLK_BOM_ID > 0;
                });
                val['inv_items_rest'] = val.inv_items_blk;
                vm.ItemData.push(val);
            });
        };

        function refreshBudgetHeader() {
            return MrcDataService.getDataByUrl('/BudgetSheet/getBudgetHeaderData/' + $stateParams.pMC_BLK_FAB_REQ_H_ID).then(function (res) {
                if (res.REVISION_DT) {
                    res['REVISION_DT'] = $filter('date')(res.REVISION_DT, config.appDateFormat);
                }
                vm.form = res;
            });
        }

        vm.getFabProcessCost = function () {
            if (vm.ItemDataFPD.length == 0) {
                refreshDataFPD($stateParams.pMC_STYL_BGT_H_ID);
            }
           
        }

        function buildFabProcessCostData(data) {

            console.log(data);

            angular.forEach(data, function (v, k) {
                if (v.MC_FAB_PROC_GRP_ID == 5) {
                    angular.forEach(v.items, function (v1, k1) {
                        if (v1.LK_WASH_TYPE_ID) {
                            v1['WASH_TYPES'] = _.map(MrcDataService.parseXmlString(v1.LK_WASH_TYPE_ID), 'LK_DATA_NAME_EN');
                        }
                        if (v1.LK_FINIS_TYPE_ID) {
                            v1['FINIS_TYPES'] = _.map(MrcDataService.parseXmlString(v1.LK_FINIS_TYPE_ID), 'LK_DATA_NAME_EN');
                        }

                    })
                }
            });

            angular.forEach(data,function (val, key) {
                var MappedState = _.some(val.items, function (o) {
                    return o.MC_BLK_FAB_COST_ID > 0;
                });

                var items_view = MappedState ? _.filter(val.items, function (o) {
                    return o.MC_BLK_FAB_COST_ID > 0;
                    }) : _.filter(val.items, function (o) {
                        return o.FAB_QTY > 0;
                    });

                val['items_view'] = items_view;

                val['items_rest'] = val.items //items_rest;
                vm.ItemDataFPD.push(val);
            });
        };



        $scope.$watch('vm.ItemData', function (newVal, oldVal) {

            angular.forEach(newVal, function (val1, key1) {
                val1.inv_items_view['TOT_QTY'] = _.ceil(_.sumBy(val1.inv_items_view, function (o) {
                    return (o.ACCT_MOU_ID ==3) ? parseFloat(o.QTY_EST) : 0;
                }),3);

                val1.inv_items_view['TOT_COST'] = _.ceil(_.sumBy(val1.inv_items_view, function (o) {
                    return parseFloat(o.Cost);
                }),3);

            });

        }, true);


        $scope.$watch('vm.ItemDataFPD', function (newVal, oldVal) {

            angular.forEach(newVal, function (val1, key1) {
                val1.items_view['TOT_QTY'] = _.ceil(_.sumBy(val1.items_view, function (o) {
                    return (o.ACCT_MOU_ID == 3) ? parseFloat(o.FAB_QTY) : 0;;
                }), 3);

                val1.items_view['TOT_COST'] = _.ceil(_.sumBy(val1.items_view, function (o) {
                    return parseFloat(o.Cost);
                }), 3);

            });

        }, true);

        vm.remove = function (Source, itemToBeRemoved) {
            var index = Source.inv_items_view.indexOf(itemToBeRemoved);
            //Source.inv_items_rest.push(itemToBeRemoved);
            Source.inv_items_view.splice(index, 1);
        };


        vm.cancel = function () {
            vm.ItemData = [];
            buildData(vm.isSubmitted ? vm.refreshData($stateParams.pMC_STYL_BGT_H_ID) : ItemData);
        }


        vm.removeFPD = function (Source, itemToBeRemoved) {
            var index = Source.items_view.indexOf(itemToBeRemoved);
            //Source.items_rest.push(itemToBeRemoved);
            Source.items_view.splice(index, 1);
        };


        vm.cancelFPD = function () {
            FabProcessCost = angular.copy(vm.ItemDataFPD);
            vm.ItemDataFPD = [];
            buildFabProcessCostData(vm.isSubmittedFPD ? vm.refreshDataFPD($stateParams.pMC_STYL_BGT_H_ID) : FabProcessCost);
        }


        vm.addFPD = function (Source) {
            vm.ItemListForDDFPD = {
                optionLabel: "--Rate--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(Source.items_rest);
                        }
                    }
                },
                dataTextField: "FAB_PROC_NAME",
                dataValueField: "MC_FAB_PROC_RATE_ID"
            };

            Source.items_view.push({
                MC_BLK_FAB_COST_ID: -1
            });
        }



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
                MC_STYLE_BLK_BOM_ID: -1
            });
        }

        vm.refresh = function (data) {

            var is_edit_mode = _.every(data.inv_items_view, function (o) {
                return parseInt(o.MC_STYLE_BLK_BOM_ID || 0) > 0;
            });

            if (!is_edit_mode) {
                return false;
            }

            Dialog.confirm('Do you really want to refresh ?', 'Are you sure?', ['Yes', 'No'])
                 .then(function () {
                     angular.forEach(data.inv_items_blk, function (val, key) {
                         console.log(val);
                         if (val.MC_STYLE_BLK_BOM_ID && val.MC_STYLE_BLK_BOM_ID > 0) {
                             var LATEST_QTY = 0;                           
                             MrcDataService.getDataByUrl('/BudgetSheet/get_refreshed_oq_bom?pMC_BLK_FAB_REQ_H_ID=' + $stateParams.pMC_BLK_FAB_REQ_H_ID + '&pITEM_CAT_CODE=' + data.ITEM_CAT_CODE + '&pINV_ITEM_ID=' + val.INV_ITEM_ID + '&pLK_FAB_QTY_SRC=' + val.LK_FAB_QTY_SRC).then(function (res) {
                                 LATEST_QTY = parseInt(angular.fromJson(res.xmlString).LATEST_QTY);
                                 if (LATEST_QTY > 0) {
                                     val['QTY_EST'] = LATEST_QTY;
                                 }
                                
                             }, function (err) {
                                 console.error(err);
                             })
                         }
                         
                         
                     });
           });
        };


        vm.refreshFPD = function (data) {

            var is_edit_mode = _.every(data.items_view, function (o) {
                return parseInt(o.MC_BLK_FAB_COST_ID || 0) > 0;
            });

            if (!is_edit_mode) {
                return false;
            }

            Dialog.confirm('Do you really want to refresh ?', 'Are you sure?', ['Yes', 'No'])
                 .then(function () {


                     angular.forEach(data.items_view, function (val, key) {

                         console.log(val);
                         if (val.MC_BLK_FAB_COST_ID && val.MC_BLK_FAB_COST_ID > 0) {
                             var LATEST_QTY = 0;
                             MrcDataService.getDataByUrl('/BudgetSheet/get_oq_refresh_fab?pMC_BLK_FAB_REQ_H_ID=' + $stateParams.pMC_BLK_FAB_REQ_H_ID + '&pMC_FAB_PROC_RATE_ID=' + val.MC_FAB_PROC_RATE_ID + '&pMC_FAB_PROC_GRP_ID=' + data.MC_FAB_PROC_GRP_ID + '&pLK_WASH_TYPE_ID=' + (val.LK_WASH_TYPE_ID || '') + '&pLK_FINIS_TYPE_ID=' + (val.LK_FINIS_TYPE_ID || '')).then(function (res) {
                                  LATEST_QTY = parseInt(angular.fromJson(res.xmlString).LATEST_QTY||0);
                                 if (LATEST_QTY > 0) {
                                     val['FAB_QTY'] = LATEST_QTY;
                                 }

                             }, function (err) {
                                 console.error(err);
                             })
                         }


                     });
                 });
        };


        vm.editItem = function (Source,item) {
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


        vm.editItemFPD = function (Source, item) {

            vm.ItemListForDDFPD = {
                optionLabel: "--Rate--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(Source.items_rest);
                        }
                    }
                },
                dataTextField: "FAB_PROC_NAME",
                dataValueField: "MC_FAB_PROC_RATE_ID"
            };

            item.edit = !item.edit;
        }


        vm.onItemChange = function (data, source) {
            if (data.INV_ITEM_ID) {

                //var itemExist = _.filter(source.inv_items_view, function (o) {
                //    return o.INV_ITEM_ID == parseInt(data.INV_ITEM_ID);
                //}).length;

                //if (itemExist == 1) {
                    var itemData = _.filter(source.inv_items_rest, function (o) {
                        return o.INV_ITEM_ID == parseInt(data.INV_ITEM_ID);
                    })[0];

                    data['INV_ITEM_CAT_ID'] = itemData.INV_ITEM_CAT_ID;
                    data['PURCH_MOU_ID'] = itemData.PURCH_MOU_ID || 3;
                    data['ACCT_MOU_ID'] = itemData.ACCT_MOU_ID || 3;
                    data['PURCH_PRICE'] = itemData.PURCH_PRICE;
                    data['IMP_PRICE'] = itemData.IMP_PRICE;
                //} else {
                //    data['INV_ITEM_ID'] = '';
                //    return;
                //}



            };
        }



        vm.onItemChangeFPD = function (data, source) {

            if (data.MC_FAB_PROC_RATE_ID) {
                    var itemData = _.filter(source.items_rest, function (o) {
                        return o.MC_FAB_PROC_RATE_ID == parseInt(data.MC_FAB_PROC_RATE_ID);
                    })[0];

                    data['PROC_RATE'] = itemData.PROC_RATE;
                    data['FAB_QTY'] = itemData.FAB_QTY;
                    data['PURCH_MOU_ID'] = itemData.PURCH_MOU_ID || 3;
                    
                    data['ACCT_MOU_ID'] = itemData.ACCT_MOU_ID || 3;

                    if (itemData.ACCT_MOU_ID == 1) {
                        data['FAB_QTY'] = vm.form.TOT_ORD_QTY||0;
                    } else if (itemData.ACCT_MOU_ID == 5) {
                        data['FAB_QTY'] = parseInt((vm.form.TOT_ORD_QTY||0)/12);
                    }
            };
        }


        vm.printBudgetSheetReport = function (dataOri) {
            console.log(vm.form.IS_BGT_FINALIZED);

            var data = {
                MC_STYL_BGT_H_ID: $stateParams.pMC_STYL_BGT_H_ID,
                REVISION_NO: dataOri.REVISION_NO||0,
                IS_BGT_FINALIZED: vm.form.IS_BGT_FINALIZED || 'N'
            };

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-2003' }, data);

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


        function refreshData(pMC_STYL_BGT_H_ID) {
            vm.showSplash = true;
            vm.ItemData = [];
            return MrcDataService.getDataByUrl('/StyleH/ItemDataForStyleBlkBOM/' + $stateParams.pMC_STYLE_H_ID + '/' + $stateParams.pMC_BLK_FAB_REQ_H_ID + '/' + pMC_STYL_BGT_H_ID).then(function (res) {
                buildData(res);
                vm.refreshData = res;
                vm.showSplash = false;
            }, function (err) {

            })
        }

        function refreshDataFPD(pMC_STYL_BGT_H_ID) {
            vm.showSplash = true;
            vm.ItemDataFPD = [];
            return MrcDataService.GetAllOthers('/api/mrc/BudgetSheet/getFabProcessCost/' + $stateParams.pMC_BLK_FAB_REQ_H_ID + '/' + pMC_STYL_BGT_H_ID).then(function (res) {
                buildFabProcessCostData(res);
                vm.refreshDataFPD = res;
                vm.showSplash = false;
            }, function (err) {
                console.log(err);
            })
        }



        vm.submitData = function (dataOri, token) {

            var dataToBeSaved = [];

            angular.copy(dataOri).forEach(function (val, key) {
                val.inv_items_view.forEach(function (val1, key1) {
                    dataToBeSaved.push({
                        MC_STYLE_BLK_BOM_ID: val1.MC_STYLE_BLK_BOM_ID,
                        PARENT_ID: val1.PARENT_ID || '',
                        INV_ITEM_CAT_ID: val1.INV_ITEM_CAT_ID,
                        INV_ITEM_ID: parseInt(val1.INV_ITEM_ID),
                        QTY_EST: val1.QTY_EST || 0,
                        PCT_ADD_QTY: val1.PCT_ADD_QTY || 0,
                        RATE_EST: val1.RATE_EST || 0,
                        PURCH_MOU_ID: val1.PURCH_MOU_ID,
                        ACCT_MOU_ID: val1.ACCT_MOU_ID,
                        REMARKS: val1.REMARKS || '',
                        LK_FAB_QTY_SRC: val1.LK_FAB_QTY_SRC ||''
                    });
                });
            });

            var XML = MrcDataService.xmlStringShort(dataToBeSaved);

            return MrcDataService.saveDataByUrl({ MC_BLK_FAB_REQ_H_ID: $stateParams.pMC_BLK_FAB_REQ_H_ID, MC_STYL_BGT_H_ID: $stateParams.pMC_STYL_BGT_H_ID, XML: XML }, '/StyleH/SaveStyleBomBlkData', token).then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        refreshData(parseInt(res.data.OPMC_STYL_BGT_H_ID));
                        $state.go('BudgetSheet', { pMC_STYL_BGT_H_ID: res.data.OPMC_STYL_BGT_H_ID }, { notify: true });
                        vm.isSubmitted = true;
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });

        }


        vm.submitDataFPD = function (dataOri, token) {
            var dataToBeSaved = [];
            angular.copy(dataOri).forEach(function (val, key) {
                val.items_view.forEach(function (val1, key1) {
                    dataToBeSaved.push({
                        MC_BLK_FAB_COST_ID: val1.MC_BLK_FAB_COST_ID,
                        MC_FAB_PROC_RATE_ID: val1.MC_FAB_PROC_RATE_ID || '',
                        FAB_QTY: val1.FAB_QTY,
                        PROC_RATE: val1.PROC_RATE,
                        ACCT_MOU_ID: val1.ACCT_MOU_ID,
                        PURCH_MOU_ID: val1.PURCH_MOU_ID,
                        REMARKS: val1.REMARKS || '',
                        FINIS_TYPES: val1.FINIS_TYPES ? val1.FINIS_TYPES : 'N/A',
                        WASH_TYPES: val1.WASH_TYPES ? val1.WASH_TYPES : 'N/A'
                    });
                });
            });


            var XML = MrcDataService.xmlStringShort(dataToBeSaved);

            return MrcDataService.saveDataByUrl({ MC_BLK_FAB_REQ_H_ID: $stateParams.pMC_BLK_FAB_REQ_H_ID, MC_STYL_BGT_H_ID: $stateParams.pMC_STYL_BGT_H_ID, XML: XML }, '/BudgetSheet/SaveFabricProcessData', token).then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        refreshDataFPD(parseInt(res.data.OPMC_STYL_BGT_H_ID));
                        $state.go('BudgetSheet', { pMC_STYL_BGT_H_ID: res.data.OPMC_STYL_BGT_H_ID }, { notify: false });
                        vm.isSubmittedFPD = true;
                    }


                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        vm.cancelOverAllCost = function () {
            if (costHeadsOri.length == 0) {
                vm.refreshOverallCost();
            } else {
                vm.costHeads = costHeadsOri;
            }
        }

        vm.refreshOverallCost = function () {
            return MrcDataService.GetAllOthers('/api/mrc/BudgetSheet/FindCostHeadForBlkFab/' + $stateParams.pMC_BLK_FAB_REQ_H_ID + '/' + $stateParams.pMC_STYL_BGT_H_ID).then(function (res) {
                vm.costHeads = res;
                costHeadsOri = res;
            }, function (err) {
                console.log(err);
            });
        };


        vm.setQtyInDz = function (grandChild) {
            if (grandChild.IS_QTY == 'Y' && !grandChild.QTY_EST && (grandChild.IS_PCT == 'N' || !grandChild.IS_PCT)) {
                grandChild.QTY_EST = parseInt(vm.form.TOT_ORD_QTY / 12);
            }
        };

       
        vm.changePCT = function (data) {
            data.QTY_EST_ORI = angular.copy(data.QTY_EST);
            if (data.IS_PCT == 'Y') {
                data.QTY_EST = 0;
            } else {
                data.QTY_EST = angular.copy(data.QTY_EST_ORI);
            }
        };

        $scope.$watch('vm.costHeads', function (newVal, oldVal) {

            var allHeadCost = 0;
            var overHeadCost = {};

            if (!angular.equals(newVal, oldVal)) {
                var overAllCost = [];
                for (var i = 0, len = newVal.length; i < len; i++) {


                    if (newVal[i].HEAD_COST > 0) {
                        allHeadCost += newVal[i].HEAD_COST;
                        overAllCost.push({
                            MC_COST_HEAD_ID: newVal[i].MC_COST_HEAD_ID,
                            MC_STYL_BGT_COST_ID: newVal[i].MC_STYL_BGT_COST_ID || '',
                            IS_PCT: newVal[i].IS_PCT,
                            PCT_COST: newVal[i].PCT_COST,
                            HEAD_COST: newVal[i].HEAD_COST,
                            key1: i,
                            key2: 0,
                            key3: 0,
                            key4: 0
                        });


                        



                    };

                    //if (val.items.length > 0) {


                    for (var i1 = 0, len1 = newVal[i].items.length; i1 < len1; i1++) {

                        if (newVal[i].items[i1].PCT_COST>0 && newVal[i].items[i1].IS_PCT=='Y') {
                            newVal[i].items[i1].HEAD_COST = _.ceil(vm.form.TOT_ORD_VALUE * (newVal[i].items[i1].PCT_COST / 100), 3);
                        } else if (newVal[i].items[i1].PCT_COST==0 && newVal[i].items[i1].IS_PCT=='Y') {
                            newVal[i].items[i1].HEAD_COST = 0;
                        };

                        allHeadCost += newVal[i].items[i1].HEAD_COST;
                        overAllCost.push({
                            MC_COST_HEAD_ID: newVal[i].items[i1].MC_COST_HEAD_ID,
                            MC_STYL_BGT_COST_ID: newVal[i].items[i1].MC_STYL_BGT_COST_ID || '',
                            IS_PCT: newVal[i].items[i1].IS_PCT,
                            PCT_COST: newVal[i].items[i1].PCT_COST || 0,
                            HEAD_COST: newVal[i].items[i1].HEAD_COST || 0,
                            key1: i,
                            key2: i1,
                            key3: 0,
                            key4: 0
                        });

                        

                        for (var i2 = 0, len2 = newVal[i].items[i1].items.length; i2 < len2; i2++) {

                            if (newVal[i].items[i1].items[i2].MC_COST_HEAD_ID != 17) {
                                allHeadCost += newVal[i].items[i1].items[i2].HEAD_COST;
                            }
                            overAllCost.push({
                                MC_COST_HEAD_ID: newVal[i].items[i1].items[i2].MC_COST_HEAD_ID,
                                MC_STYL_BGT_COST_ID: newVal[i].items[i1].items[i2].MC_STYL_BGT_COST_ID || '',
                                IS_PCT: newVal[i].items[i1].items[i2].IS_PCT,
                                PCT_COST: newVal[i].items[i1].items[i2].PCT_COST,
                                HEAD_COST: newVal[i].items[i1].items[i2].HEAD_COST,
                                key1: i,
                                key2: i1,
                                key3: i2,
                                key4: 0
                            });






                            //}

                            for (var i3 = 0, len3 = newVal[i].items[i1].items[i2].items.length; i3 < len3; i3++) {
                                if (newVal[i].items[i1].items[i2].items[i3].HEAD_COST > 0) {
                                    allHeadCost += newVal[i].items[i1].items[i2].items[i3].HEAD_COST;
                                    overAllCost.push({
                                        MC_COST_HEAD_ID: newVal[i].items[i1].items[i2].items[i3].MC_COST_HEAD_ID,
                                        MC_STYL_BGT_COST_ID: newVal[i].items[i1].items[i2].items[i3].MC_STYL_BGT_COST_ID || '',
                                        IS_PCT: newVal[i].items[i1].items[i2].items[i3].IS_PCT,
                                        PCT_COST: newVal[i].items[i1].items[i2].items[i3].PCT_COST,
                                        HEAD_COST: newVal[i].items[i1].items[i2].items[i3].HEAD_COST,
                                        key1: i,
                                        key2: i1,
                                        key3: i2,
                                        key4: i3
                                    });



                                }
                            }



                        };
                        //}

                    };
                    //}



                };



                overHeadCost = _.filter(overAllCost, function (o) {
                    return o.MC_COST_HEAD_ID == 17;
                })[0];

                vm.TOT_HEAD_COST = parseFloat(allHeadCost.toFixed(3));
                vm.costHeads[overHeadCost.key1].items[overHeadCost.key2].items[overHeadCost.key3].HEAD_COST = parseFloat((formData.TOT_ORD_VALUE - allHeadCost).toFixed(3));
                vm.TOT_CM_COST = parseFloat((((formData.TOT_ORD_VALUE - allHeadCost) / formData.TOT_ORD_QTY) * 12).toFixed(2));
                //vm.BUDGETET_EXPENDITURE = parseFloat((( allHeadCost / formData.TOT_ORD_VALUE) * 100).toFixed(2));
            };

        }, true);



        vm.submitOverAllCostDara = function (data, token, valid) {

            if (!valid) {
                return;
            }
            var overAllCost = [];
            var xml;

            for (var i = 0, len = data.length; i < len; i++) {


                if (data[i].HEAD_COST > 0) {
                    overAllCost.push({
                        MC_COST_HEAD_ID: data[i].MC_COST_HEAD_ID,
                        MC_STYL_BGT_COST_ID: data[i].MC_STYL_BGT_COST_ID || '',
                        IS_PCT: data[i].IS_PCT,
                        PCT_COST: data[i].PCT_COST||0,
                        HEAD_COST: data[i].HEAD_COST||0,
                        QTY_EST: data[i].QTY_EST||0,
                        PCT_ADD_QTY: data[i].PCT_ADD_QTY || 0,
                        ACCT_MOU_ID: data[i].ACCT_MOU_ID||'',
                        RATE_EST: data[i].RATE_EST || 0,
                        PURCH_MOU_ID: data[i].PURCH_MOU_ID||''

                    })
                };

                //if (val.items.length > 0) {


                for (var i1 = 0, len1 = data[i].items.length; i1 < len1; i1++) {
                    if (data[i].items[i1].HEAD_COST > 0) {
                        overAllCost.push({
                            MC_COST_HEAD_ID: data[i].items[i1].MC_COST_HEAD_ID,
                            MC_STYL_BGT_COST_ID: data[i].items[i1].MC_STYL_BGT_COST_ID || '',
                            IS_PCT: data[i].items[i1].IS_PCT,
                            PCT_COST: data[i].items[i1].PCT_COST || 0,
                            HEAD_COST: data[i].items[i1].HEAD_COST || 0,
                            QTY_EST: data[i].items[i1].QTY_EST || 0,
                            PCT_ADD_QTY: data[i].items[i1].PCT_ADD_QTY || 0,
                            ACCT_MOU_ID: data[i].items[i1].ACCT_MOU_ID || '',
                            RATE_EST: data[i].items[i1].RATE_EST || 0,
                            PURCH_MOU_ID: data[i].items[i1].PURCH_MOU_ID || ''
                        })
                    }

                    //if (val1.items.length > 0) {

                    for (var i2 = 0, len2 = data[i].items[i1].items.length; i2 < len2; i2++) {

                        if (data[i].items[i1].items[i2].HEAD_COST > 0) {
                            overAllCost.push({
                                MC_COST_HEAD_ID: data[i].items[i1].items[i2].MC_COST_HEAD_ID,
                                MC_STYL_BGT_COST_ID: data[i].items[i1].items[i2].MC_STYL_BGT_COST_ID || '',
                                IS_PCT: data[i].items[i1].items[i2].IS_PCT,
                                PCT_COST: data[i].items[i1].items[i2].PCT_COST || 0,
                                HEAD_COST: data[i].items[i1].items[i2].HEAD_COST || 0,
                                QTY_EST: data[i].items[i1].items[i2].QTY_EST || 0,
                                PCT_ADD_QTY: data[i].items[i1].items[i2].PCT_ADD_QTY || '',
                                ACCT_MOU_ID: data[i].items[i1].items[i2].ACCT_MOU_ID || '',
                                RATE_EST: data[i].items[i1].items[i2].RATE_EST || 0,
                                PURCH_MOU_ID: data[i].items[i1].items[i2].PURCH_MOU_ID || ''
                            });

                        }

                        for (var i3 = 0, len3 = data[i].items[i1].items[i2].items.length; i3 < len3; i3++) {
                            if (data[i].items[i1].items[i2].items[i3].HEAD_COST > 0) {
                                overAllCost.push({
                                    MC_COST_HEAD_ID: data[i].items[i1].items[i2].items[i3].MC_COST_HEAD_ID,
                                    MC_STYL_BGT_COST_ID: data[i].items[i1].items[i2].items[i3].MC_STYL_BGT_COST_ID || '',
                                    IS_PCT: data[i].items[i1].items[i2].items[i3].IS_PCT,
                                    PCT_COST: data[i].items[i1].items[i2].items[i3].PCT_COST || 0,
                                    HEAD_COST: data[i].items[i1].items[i2].items[i3].HEAD_COST || 0,
                                    QTY_EST: data[i].items[i1].items[i2].items[i3].QTY_EST || 0,
                                    PCT_ADD_QTY: data[i].items[i1].items[i2].items[i3].PCT_ADD_QTY || '',
                                    ACCT_MOU_ID: data[i].items[i1].items[i2].items[i3].ACCT_MOU_ID || '',
                                    RATE_EST: data[i].items[i1].items[i2].items[i3].RATE_EST || 0,
                                    PURCH_MOU_ID: data[i].items[i1].items[i2].items[i3].PURCH_MOU_ID || ''
                                });

                            }
                        }



                    };
                    //}

                };
                //}

            };

            xml = MrcDataService.xmlStringShort(overAllCost).replace(null,0);

            return MrcDataService.saveDataByUrl({ MC_BLK_FAB_REQ_H_ID: $stateParams.pMC_BLK_FAB_REQ_H_ID, MC_STYL_BGT_H_ID: $stateParams.pMC_STYL_BGT_H_ID, XML: xml }, '/BudgetSheet/SaveOverAllCostData', token).then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.refreshOverallCost();
                    }


                   
                }

            }, function (err) {
                console.log(err);
            })

        }

        vm.setFinalizedFlag = function () {
            Dialog.confirm('Do you really want to finalize this budget sheet?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {
                 return MrcDataService.saveDataByUrl({ MC_BLK_FAB_REQ_H_ID: $stateParams.pMC_BLK_FAB_REQ_H_ID, MC_STYL_BGT_H_ID: $stateParams.pMC_STYL_BGT_H_ID, IS_BGT_FINALIZED : 'Y'}, '/BudgetSheet/SaveOverAllCostData').then(function (res) {
                     if (res.success === true) {
                         res['data'] = angular.fromJson(res.jsonStr);

                         if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             vm.form['IS_BGT_FINALIZED'] = 'Y';
                             vm.form['IS_BFB_FINALIZED'] = 'Y';
                             vm.form['MC_TNA_TASK_STATUS_ID'] = 33;
                         }

                         config.appToastMsg(res.data.PMSG);
                     }

                 });
                
             });
        };

        
        vm.lsitView = function () {
            $state.go('BulkFabBkList', { pMC_BYR_ACC_ID: vm.form.MC_BYR_ACC_ID, pMC_STYLE_H_ID: vm.form.MC_STYLE_H_ID });
        };

        vm.budgetRevisionModal = function (data) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'BudgetRevisionModal.html',
                controller: 'BudgetRevisionModalController',
                size: 'small',
                windowClass: 'large-Modal',
                resolve: {
                    orderData: function () {
                        return data;
                    }
                }
            });

            modalInstance.result.then(function (vRevisionData) {
                vm.form.REVISION_NO = vRevisionData.REVISION_NO;
                vm.form.REVISION_DT = vRevisionData.REVISION_DT;
                vm.form.REV_REASON = vRevisionData.REV_REASON;
                vm.form['IS_BGT_FINALIZED'] = 'N';
                refreshBudgetHeader();
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        vm.blkBomReset = function () {
            Dialog.confirm('Do you want to reset?', 'Attention', ['Yes', 'No'])
            .then(function () {
                return MrcDataService.saveDataByUrl({ MC_BLK_FAB_REQ_H_ID: $stateParams.pMC_BLK_FAB_REQ_H_ID, MC_STYL_BGT_H_ID: $stateParams.pMC_STYL_BGT_H_ID, XML: '<trans></trans>' }, '/StyleH/SaveStyleBomBlkData').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            refreshData($stateParams.pMC_STYL_BGT_H_ID);
                            vm.isSubmittedFPD = true;
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                });
            });
        };

        vm.FabProcessReset = function () {
            Dialog.confirm('Do you want to reset?', 'Attention', ['Yes', 'No'])
                .then(function () {
                    return MrcDataService.saveDataByUrl({ MC_BLK_FAB_REQ_H_ID: $stateParams.pMC_BLK_FAB_REQ_H_ID, MC_STYL_BGT_H_ID: $stateParams.pMC_STYL_BGT_H_ID, XML: '<trans></trans>' }, '/BudgetSheet/SaveFabricProcessData').then(function (res) {
                        if (res.success === false) {
                            vm.errors = res.errors;
                        }
                        else {
                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                refreshDataFPD($stateParams.pMC_STYL_BGT_H_ID);
                                vm.isSubmittedFPD = true;
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    });
                });
        }

        vm.INV_ITEM_ID_LIST = [];

        vm.blkBomIdListsForBooking = function (IS_BK, MC_STYLE_BLK_BOM_ID, INV_ITEM_ID) {
            if (IS_BK) {
                var idx = vm.MC_STYLE_BLK_BOM_ID_LIST.indexOf(MC_STYLE_BLK_BOM_ID);
                var idy = vm.INV_ITEM_ID_LIST.indexOf(INV_ITEM_ID);
                if (idx == -1) {
                    vm.MC_STYLE_BLK_BOM_ID_LIST.push(MC_STYLE_BLK_BOM_ID);
                }

                if (idy == -1) {
                    vm.INV_ITEM_ID_LIST.push(INV_ITEM_ID)
                }

            } else {
                var idx = vm.MC_STYLE_BLK_BOM_ID_LIST.indexOf(MC_STYLE_BLK_BOM_ID);
                if (idx >= 0) {
                    vm.MC_STYLE_BLK_BOM_ID_LIST.splice(idx, 1);
                }

                var idy = vm.INV_ITEM_ID_LIST.indexOf(INV_ITEM_ID);
                if (idy >= 0) {
                    vm.INV_ITEM_ID_LIST.splice(idy, 1);
                }
            }
        }

        vm.goToTaBookingPage = function (MC_STYLE_BLK_BOM_ID_LIST) {
            var itemList = angular.copy(MC_STYLE_BLK_BOM_ID_LIST);
            var url = '/Merchandising/Mrc/TaBooking/#/TaBk/BgtHrd/' + $stateParams.pMC_STYL_BGT_H_ID + '/PoHrd/0/Item/' + vm.INV_ITEM_ID_LIST[0] + '?pBLK_BOM_LIST=' + itemList.join(',') + '&pBLK_BOM_ACT=' + MC_STYLE_BLK_BOM_ID_LIST[0] + '&pMC_STYLE_H_ID=' + $stateParams.pMC_STYLE_H_ID;
            var a = document.createElement('a');
            a.href = url;
            a.target = '_blank';
            document.body.appendChild(a);
            a.click();
        };

    }

})();

// Start Revision Modal Controller
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('BudgetRevisionModalController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'orderData', 'MrcDataService', 'Dialog', BudgetRevisionModalController]);
    function BudgetRevisionModalController($modalInstance, $q, $scope, $http, $state, $filter, config, orderData, MrcDataService, Dialog) {
        $scope.showSplash = true;
        activate();

        $scope.today = new Date();
        $scope.dtFormat = config.appDateFormat;
        $scope.formInvalid = false;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        $scope.REVISION_DT = $scope.today;

        if (orderData.REVISION_NO) {
            $scope.REVISION_NO = parseInt(orderData.REVISION_NO||0) + 1;
        }
        else {
            $scope.REVISION_NO = 1;
        }

        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                $scope.showSplash = false;
            });
        }

        $scope.RevisionDate_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.RevisionDate_LNopened = true;
        }






        $scope.save = function (token, valid) {
            if (!valid) return;

            var submitData = { MC_STYL_BGT_H_ID: orderData.MC_STYL_BGT_H_ID, REVISION_DT: $filter('date')($scope.REVISION_DT, $scope.dtFormat), REV_REASON: $scope.REV_REASON, REVISION_NO: $scope.REVISION_NO };

            Dialog.confirm('Do you want to revise Budget Sheet?', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     return MrcDataService.saveDataByUrl(submitData, '/BudgetSheet/reviseBudgetSheet').then(function (res) {
                         if (res.success === false) {
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PREVISION_NO_RTN != 0) {
                                 submitData['REVISION_NO'] = res.data.PREVISION_NO_RTN;
                                 $modalInstance.close(submitData);
                                 
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     });
                 });
        };


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };



    }

})();
// End Revision Modal Controller