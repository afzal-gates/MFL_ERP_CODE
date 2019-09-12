(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('LabdipRecipeController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'Dialog', 'DateShareService', 'formData', '$modal', LabdipRecipeController]);
    function LabdipRecipeController($q, config, DyeingDataService, $stateParams, $state, $scope, Dialog, DateShareService, formData, $modal) {

        var vm = this;
        vm.showSplash = true;
        vm.Title = $state.current.Title || '';

        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        console.log($stateParams);

        //vm.form.IS_ACTIVE = 'Y';

        vm.obGrid = [];
        vm.selectedRow = {};

        vm.form = formData.MC_BYR_ACC_ID ? formData : { IS_ACTIVE: 'Y', FAB_COM_TYPE: 'F', IS_RECHECK: null, RECHECK: false, RE_CHK_NO: 0 };

        activate();
        function activate() {
            var promise = [getBuyerAcGrpList(), buyerListByBAC(formData.MC_BYR_ACC_ID || null), fabricColorListByStyle(formData.MC_STYLE_H_ID || null), TrimsItemList(), /*styleListByBuyer((formData.MC_BYR_ACC_ID || null),(formData.MC_BUYER_ID || null)),*/ getStyleListData((formData.MC_BYR_ACC_GRP_ID || null)), getDYItemData(), getCHItemData(), getBuyerAcList(), getDyeMethodList(), geTemperatureList(), fiberCompositionList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
                if ($stateParams.pRECHECK_ID == 1) {
                    vm.form.IS_RECHECK = 1;
                    vm.form.RECHECK = true;
                    vm.form.RE_CHK_DT = vm.today;
                    if (vm.form.LD_APROVED_DT)
                        vm.form.LD_APROVED_DT = vm.today;
                }
                else if ($stateParams.pRECHECK_ID == 2) {
                    vm.form.IS_RECHECK = 0;
                    vm.form.RECHECK = false;
                    vm.form.RE_CHK_DT = "";
                    vm.form.IS_ACTIVE = 'Y';
                    if (vm.form.LD_APROVED_DT)
                        vm.form.LD_APROVED_DT = "";
                }
                else {
                    vm.form.RE_CHK_NO = 0;
                    vm.form.REASON_DESC = "";
                    vm.form.RE_CHK_DT = vm.today;

                    //if (vm.form.RE_CHK_NO > 1)
                    //    vm.form.RE_CHK_NO = vm.form.RE_CHK_NO - 1;
                    //else
                    //    vm.form.RE_CHK_NO == 0;
                }

            });
        }

        if ($stateParams.pRECHECK_ID > 0) {
            vm.form.RE_CHK_NO = vm.form.RE_CHK_NO + 1;
        }

        if (vm.form.MC_BYR_ACC_GRP_ID > 0) {
            vm.fabLotList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetProdWithLotList?pMC_COLOR_ID=' + (vm.form.MC_COLOR_ID || 0) + '&pMC_STYLE_H_ID=' + (vm.form.MC_STYLE_H_ID || 0) + '&pMC_STYLE_D_FAB_ID=' + (vm.form.MC_STYLE_D_FAB_ID || 0) + '&pRF_FAB_TYPE_ID=0&pRF_FIB_COMP_ID=0').then(function (res) {

                            e.success(res);
                        });
                    }
                }
            });
        }

        vm.recipeItem = {};
        vm.optionList = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J"];
        vm.unitList = ["%", "g/L"];


        function TrimsItemList() {
            return vm.TrimsItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetTrimsForLDByStyleID?pMC_STYLE_H_ID=' + (vm.form.MC_STYLE_H_ID || -1)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getBuyerAcGrpList() {

            return vm.buyerAcGrpList = {
                optionLabel: "--- Select Buyer Group ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.styleList.read();
                },

                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID"
            };
        }


        function getStyleListData(MC_BYR_ACC_GRP_ID) {
            var pSTYLE_NO = "";
            if (vm.form) {
                MC_BYR_ACC_GRP_ID = vm.form.MC_BYR_ACC_GRP_ID;
                pSTYLE_NO = vm.form.STYLE_NO;
            }

            vm.fabLotList = new kendo.data.DataSource({
                data: []
            });

            return vm.styleList = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);

                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0 ? vm.form.MC_BYR_ACC_GRP_ID : (MC_BYR_ACC_GRP_ID || 0))); //pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0);

                        url += DyeingDataService.kFilterStr2QueryParam(params.filter);
                        if (pSTYLE_NO)
                            url = url + '&pSTYLE_NO=' + (pSTYLE_NO || "");

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.loadLotInfo = function (e) {

            var item = e.sender.dataItem(e.item);
            vm.form.LOT_NO = item.YRN_LOT_NO;
        }

        vm.checkDuplicateRecipe = function () {

            vm.fabLotList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetProdWithLotList?pMC_COLOR_ID=' + (vm.form.MC_COLOR_ID || 0) + '&pMC_STYLE_H_ID=' + (vm.form.MC_STYLE_H_ID || 0) + '&pMC_STYLE_D_FAB_ID=' + (vm.form.MC_STYLE_D_FAB_ID || 0) + '&pRF_FAB_TYPE_ID=0&pRF_FIB_COMP_ID=0').then(function (res) {

                            e.success(res);
                        });
                    }
                }
            });

            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/checkDuplicateRecipe/' + (vm.form.MC_STYLE_H_ID || 0) + '/' + (vm.form.MC_COLOR_ID || 0) + '/' + (vm.form.MC_STYLE_D_FAB_ID || 0)).then(function (res) {
                if (res.TOTAL_REC > 0) {
                    config.appToastMsg("MULTI-005 Receipe already exists!");
                    vm.form.MC_COLOR_ID = '';
                    vm.form.MC_STYLE_D_FAB_ID = '';

                }
            });


        }

        vm.CheckDuplicateBuyerLabNo = function () {
            if (vm.form.MC_BUYER_ID > 0 && vm.form.LD_RECIPE_NO.length > 1)
                DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/CheckDuplicateBuyerLabNo/' + vm.form.MC_BUYER_ID + '/' + vm.form.LD_RECIPE_NO).then(function (res) {

                    if (res.length > 0) {
                        vm.form.LD_RECIPE_NO = "";
                        config.appToastMsg("MULTI-005 Another Buyer has Same Lab Recipe No");
                    }
                });
        }

        vm.getDyeMethodByColorGroup = function () {
            if (vm.form.MC_COLOR_GRP_ID > 0)
                DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetDyeMethodColorGroup?pMC_COLOR_GRP_ID=' + vm.form.MC_COLOR_GRP_ID).then(function (res) {
                    if (res.LK_DYE_MTHD_ID > 0)
                        vm.form.LK_DYE_MTHD_ID = res.LK_DYE_MTHD_ID;
                });
        }

        vm.getColorGroupByDyeMethod = function () {
            if (vm.form.LK_DYE_MTHD_ID > 0)
                DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetDyeMethodColorGroup?pLK_DYE_MTHD_ID=' + vm.form.LK_DYE_MTHD_ID).then(function (res) {
                    if (res.MC_COLOR_GRP_ID > 0)
                        vm.form.MC_COLOR_GRP_ID = res.MC_COLOR_GRP_ID;
                });
        }

        vm.buyerOnSelect = function (e) {

            var item = e.sender.dataItem(e.item);

            if (item.MC_BUYER_ID > 0) {
                styleListByBuyer((vm.form.MC_BYR_ACC_ID || null), (vm.form.MC_BUYER_ID || null));
            }

        };

        vm.getDataByStyleId = function (e) {
            var item = e.sender.dataItem(e.item);
            console.log(item);
            if (item.MC_STYLE_H_ID > 0) {
                vm.form.STYLE_DESC = item.STYLE_DESC;
                vm.form.STYLE_DESC = item.STYLE_DESC;

                fabricColorListByStyle((item.MC_STYLE_H_ID || null), item.MC_STYLE_H_EXT_ID);
                TrimsItemList((item.MC_STYLE_H_ID || null));
            }
        };

        vm.openlabReceipList = function () {
            if (vm.form.MC_STYLE_H_ID > 0 && vm.form.MC_COLOR_ID > 0 && vm.form.MC_BYR_ACC_GRP_ID > 0) {

                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: 'labReceipList.html',
                    controller: function ($scope, $modalInstance, DyeingDataService, idList) {

                        styleListByBuyer();
                        buyerAccGroupList();
                        colorList();

                        $scope.searchLabRecipeNo = function () {

                            if ($scope.MC_STYLE_H_ID > 0 || $scope.ORDER_NO)
                                return DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/SelectByOrderStyleColor/' + ($scope.ORDER_NO || null) + '/' + ($scope.MC_STYLE_H_ID || 0) + '/' + $scope.MC_COLOR_ID + '/' + $scope.MC_BYR_ACC_GRP_ID).then(function (res) {
                                    $scope.labReceipList = res;
                                });
                            else
                                config.appToastMsg("MULTI-005 Please select Style/Order No!");

                        };

                        $scope.cancel = function (data) {
                            $modalInstance.close(data);
                        }

                        $scope.selectLabdipReceip = function (data) {
                            $modalInstance.close(data);
                        }

                        function colorList() {
                            DyeingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll').then(function (resC) {
                                $scope.colorList = new kendo.data.DataSource({
                                    data: resC
                                })
                            });
                        }


                        function buyerList() {
                            DyeingDataService.getDataByFullUrl('/api/mrc/buyer/SelectAll').then(function (resC) {
                                $scope.buyerList = new kendo.data.DataSource({
                                    data: resC
                                })
                            });
                        }

                        function buyerAccGroupList() {
                            DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (resC) {
                                $scope.buyerAccGrpList = new kendo.data.DataSource({
                                    data: resC
                                })
                            });
                        }


                        function styleListByBuyer() {
                            var ids = idList.split(',');

                            //$scope.MC_BYR_ACC_ID = ids[0];
                            $scope.MC_COLOR_ID = ids[0];
                            $scope.MC_BYR_ACC_GRP_ID = ids[1];
                            $scope.ORDER_NO = '';
                            var pSTYLE_NO = "";

                            return $scope.styleList = new kendo.data.DataSource({
                                serverFiltering: true,
                                transport: {
                                    read: function (e) {
                                        var webapi = new kendo.data.transports.webapi({});
                                        var params = webapi.parameterMap(e.data);

                                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=' + ids[1]; //pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0);

                                        url += DyeingDataService.kFilterStr2QueryParam(params.filter);
                                        if (pSTYLE_NO)
                                            url = url + '&pSTYLE_NO=' + (pSTYLE_NO || "");

                                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                                            e.success(res);
                                        }, function (err) {
                                            console.log(err);
                                        });
                                    }
                                }
                            });

                            //DyeingDataService.getDataByFullUrl('/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=' + ids[1]).then(function (sdata) {

                            //    $scope.styleList = new kendo.data.DataSource({
                            //        data: sdata
                            //    })
                            //})
                        };

                        function getBuyerAcList() {

                            return $scope.buyerAcList = {
                                optionLabel: "--- Select Buyer A/C ---",
                                filter: "contains",
                                autoBind: true,
                                dataSource: {
                                    transport: {
                                        read: function (e) {
                                            DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
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
                        };


                    },
                    size: 'lg',
                    windowClass: 'large-Modal',
                    resolve: {
                        idList: function (DyeingDataService) {
                            var ids = vm.form.MC_COLOR_ID + "," + vm.form.MC_BYR_ACC_GRP_ID;
                            return ids;
                        }
                    }

                });

                modalInstance.result.then(function (item) {
                    vm.form.MC_ORDER_H_ID = item.MC_ORDER_H_ID;
                    vm.form.OPTION_NO = item.OPTION_NO;
                    vm.form.LK_DYE_MTHD_ID = item.LK_DYE_MTHD_ID;
                    vm.form.RQD_ROOM_TEMP_R = item.RQD_ROOM_TEMP_R;
                    vm.form.RQD_TIME_MIN_R = item.RQD_TIME_MIN_R;
                    vm.form.LQR_RATIO = item.LQR_RATIO;
                    vm.form.RQD_ROOM_TEMP_D = item.RQD_ROOM_TEMP_D;
                    vm.form.RQD_TIME_MIN_D = item.RQD_TIME_MIN_D;
                    vm.form.MC_STYLE_D_FAB_ID = item.MC_STYLE_D_FAB_ID;
                    vm.form.LD_RECIPE_NO = item.LD_RECIPE_NO;

                    vm.recipeItemList = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetPreviousRecipeItemList?pMC_LD_RECIPE_H_ID=' + (item.MC_LD_RECIPE_H_ID || 0)).then(function (res) {
                                    var list = _.filter(res, function (o) { return o.QTY_MOU_ST == "%" });
                                    var total = 0;
                                    for (var i = 0; i < list.length; i++)
                                        total = total + list[i].DOSE_QTY;
                                    vm.form.TOTAL_WT = total.toFixed(4);
                                    vm.form.COLOR_WT = total.toFixed(4);
                                    DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLDColorGroup?pPERCENT_QTY=' + (vm.form.TOTAL_WT || 0)).then(function (res) {
                                        vm.form.MC_COLOR_GRP_ID = res.MC_COLOR_GRP_ID;
                                    });

                                    e.success(res);
                                });
                            }
                        }
                    });


                }, function () {
                    console.log('Modal dismissed at: ' + new Date());
                });
            }
            else {
                config.appToastMsg("MULTI-005 Please select Style & Color");
            }
        };

        $scope.labRecipeNoAuto = function (val) {

            if (vm.form.MC_STYLE_H_ID > 0 && vm.form.MC_COLOR_ID > 0)
                return DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/SelectByReceipeStyleColor/' + val + '/' + vm.form.MC_STYLE_H_ID + '/' + vm.form.MC_COLOR_ID).then(function (res) {
                    return res;
                });

        };

        $scope.onSelectlabRecipeNo = function (item) {
            //console.log(item);
            vm.form.MC_ORDER_H_ID = item.MC_ORDER_H_ID;
            vm.form.OPTION_NO = item.OPTION_NO;
            vm.form.LK_DYE_MTHD_ID = item.LK_DYE_MTHD_ID;
            vm.form.RQD_ROOM_TEMP_R = item.RQD_ROOM_TEMP_R;
            vm.form.RQD_TIME_MIN_R = item.RQD_TIME_MIN_R;
            vm.form.LQR_RATIO = item.LQR_RATIO;
            vm.form.RQD_ROOM_TEMP_D = item.RQD_ROOM_TEMP_D;
            vm.form.RQD_TIME_MIN_D = item.RQD_TIME_MIN_D;

            vm.recipeItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetPreviousRecipeItemList?pMC_LD_RECIPE_H_ID=' + (item.MC_LD_RECIPE_H_ID || 0)).then(function (res) {
                            var list = _.filter(res, function (o) { return o.QTY_MOU_ST == "%" });
                            var total = 0;
                            for (var i = 0; i < list.length; i++)
                                total = total + list[i].DOSE_QTY;
                            vm.form.TOTAL_WT = total.toFixed(4);
                            vm.form.COLOR_WT = total.toFixed(4);
                            e.success(res);
                        });
                    }
                }
            });
        }



        function getDyeMethodList() {
            getDyeMethodListForDC();
            return vm.DyeMethodList = {
                optionLabel: "--Select Dyeing Method--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return DyeingDataService.getDataByFullUrl('/api/common/GetDyeingMethodList').then(function (res) {
                                var list = _.filter(res, function (o) { return (o.IS_S_D_PART == 'S' || o.IS_S_D_PART == 'D') });
                                e.success(list);
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

        function getDyeMethodListForDC() {
            DyeingDataService.getDataByFullUrl('/api/common/GetDyeingMethodList').then(function (res) {

                var list = _.filter(res, function (o) { return (o.IS_S_D_PART == 'S') });
                vm.DyeMethodListForDC = new kendo.data.DataSource({
                    data: list
                })
            });
        }



        function geTemperatureList() {
            return vm.temperatureList = {
                optionLabel: "-Temp(C)-",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(86).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };


        vm.colorGroupList = {
            optionLabel: "-- Colour Group--",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/mrc/ColourMaster/ColourGroupList').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        })

                    }
                }
            },
            dataTextField: "COLOR_GRP_NAME_EN",
            dataValueField: "MC_COLOR_GRP_ID"
        };

        vm.unassignValue = function (Mellange) {
            if (Mellange == true) {
                vm.form.MC_STYLE_D_FAB_ID = '';
            }
            else {
                vm.form.RF_FAB_TYPE_ID = '';
                vm.form.RF_FIB_COMP_ID = '';
            }
        }

        vm.recipeItemList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetRecipeItemList?pMC_LD_RECIPE_H_ID=' + (vm.form.MC_LD_RECIPE_H_ID || 0)).then(function (res) {
                        var list = _.filter(res, function (o) { return o.QTY_MOU_ST == "%" });
                        var total = 0;
                        for (var i = 0; i < list.length; i++)
                            total = total + list[i].DOSE_QTY;
                        vm.form.TOTAL_WT = total.toFixed(4);
                        vm.form.COLOR_WT = total.toFixed(4);
                        e.success(res);
                    });
                }
            }
        });

        vm.gridOptionsRecipeItem = {
            pageable: false,
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
            height: '200px',
            scrollable: true,
            columns: [
                {
                    field: "SL_NO",
                    title: "SL No",
                    width: "10%"
                },
                { field: "INV_ITEM_ID", hidden: true },
                { field: "MC_LD_RECIPE_D_ID", hidden: true },
                { field: "LK_DYE_MTHD_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "DYE_MTHD_NAME", title: "Dye Method", width: "20%" },
                { field: "ITEM_CAT_NAME_EN", title: "Agent Name", width: "20%" },
                { field: "ITEM_NAME_EN", title: "Item Name", width: "20%" },
                { field: "BRAND_NAME_EN", title: "Brand", width: "10%" },
                { field: "DOSE_QTY", title: "Amount", width: "10%" },
                //{ field: "QTY_MOU_ID", title: "Unit", width: "30%" },
                { field: "QTY_MOU_ST", title: "Unit", width: "10%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeRecipeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>  <a  title="Edit" ng-click="vm.editRecipeItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "10%"
                }
            ],
        };


        vm.addToGrid = function () {
            //if (fnValidateSub3() == true) {
            var LD_M_ID = vm.recipeItem.LK_DYE_MTHD_ID;
            var item = _.filter(vm.DyItemList.data(), function (o) {
                return o.INV_ITEM_ID === parseInt(vm.recipeItem.INV_ITEM_ID)
            })[0];

            if (vm.recipeItem.INV_ITEM_ID > 0 && vm.recipeItem.DOSE_QTY >= 0.00001) {
                var ITEM_NAME_EN = $("#INV_ITEM_ID").data("kendoDropDownList").text();

                var dlist = _.filter(vm.DyeMethodListForDC.data(), function (o) { return o.LK_DYE_MTHD_ID == vm.recipeItem.LK_DYE_MTHD_ID });

                vm.recipeItem.ITEM_CAT_NAME_EN = item.ITEM_CAT_NAME_EN;
                vm.recipeItem.BRAND_NAME_EN = item.BRAND_NAME_EN;
                vm.recipeItem.ITEM_NAME_EN = ITEM_NAME_EN;
                vm.recipeItem.DYE_MTHD_NAME = dlist[0].DYE_MTHD_NAME;
                vm.recipeItem.QTY_MOU_ID = "27";
                vm.recipeItem.QTY_MOU_ST = "%";
                if (vm.recipeItem.uid) {
                    // Update
                    var row = vm.recipeItemList.getByUid(vm.recipeItem.uid);
                    var count = _.filter(vm.recipeItemList.data(), function (o) {
                        return o.ITEM_NAME_EN === vm.recipeItem.ITEM_NAME_EN
                    }).length;

                    if (count <= 1) {
                        row["ITEM_NAME_EN"] = vm.recipeItem.ITEM_NAME_EN;
                        row["LK_DYE_MTHD_ID"] = vm.recipeItem.LK_DYE_MTHD_ID;
                        row["DYE_MTHD_NAME"] = vm.recipeItem.DYE_MTHD_NAME;
                        row["INV_ITEM_ID"] = vm.recipeItem.INV_ITEM_ID;
                        row["DOSE_QTY"] = vm.recipeItem.DOSE_QTY;
                        row["QTY_MOU_ID"] = vm.recipeItem.QTY_MOU_ID;
                        row["QTY_MOU_ST"] = vm.recipeItem.QTY_MOU_ST;
                        row["MC_LD_RECIPE_D_ID"] = vm.recipeItem.MC_LD_RECIPE_D_ID;
                        row["ITEM_CAT_NAME_EN"] = vm.recipeItem.ITEM_CAT_NAME_EN;
                        row["BRAND_NAME_EN"] = vm.recipeItem.BRAND_NAME_EN;

                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert
                    if (vm.recipeItemList != null) {
                        var count = _.filter(vm.recipeItemList.data(), function (o) {
                            return o.ITEM_NAME_EN === vm.recipeItem.ITEM_NAME_EN
                        }).length;

                        if (count == 0) {

                            vm.recipeItem.ITEM_CAT_NAME_EN = item.ITEM_CAT_NAME_EN;
                            vm.recipeItem.BRAND_NAME_EN = item.BRAND_NAME_EN;
                            var idx = vm.recipeItemList.data().length + 1;

                            vm.recipeItem.SL_NO = idx;
                            vm.recipeItemList.insert(idx, vm.recipeItem);
                        }
                        else {
                            config.appToastMsg("MULTI-005 Duplicate item name exists!");
                        }
                    }
                    else {
                        var idx = 1;
                        vm.recipeItemList.insert(idx, vm.recipeItem);
                    }

                }
                vm.recipeItem = { LK_DYE_MTHD_ID: LD_M_ID };
                vm.recipeItem['INV_ITEM_ID'] = "";

                var list = _.filter(vm.recipeItemList.data(), function (o) {
                    return o.QTY_MOU_ST === "%";
                })
                var sum = 0;
                for (var i = 0; i < list.length; i++)
                    sum = sum + list[i].DOSE_QTY;
                var pPERCENT_QTY = sum;
                $("#TOTAL_WT").val(sum);
                DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLDColorGroup?pPERCENT_QTY=' + (pPERCENT_QTY || 0)).then(function (res) {
                    vm.form.MC_COLOR_GRP_ID = res.MC_COLOR_GRP_ID;
                });



            } else {
                if (vm.recipeItem.INV_ITEM_ID.length > 0)
                    config.appToastMsg("MULTI-005 Please input depth of shade!");
                else
                    config.appToastMsg("MULTI-005 Please select an item!");
            }
        };

        vm.addToGridC = function () {
            //if (fnValidateSub3() == true) {
            var LD_M_ID = vm.recipeItemC.LK_DYE_MTHD_ID;

            var item = _.filter(vm.ChItemList.data(), function (o) {
                return o.INV_ITEM_ID === parseInt(vm.recipeItemC.INV_ITEM_ID)
            })[0];

            if (vm.recipeItemC.INV_ITEM_ID > 0 && vm.recipeItemC.DOSE_QTY >= 0.00001) {
                var ITEM_NAME_EN = $("#cINV_ITEM_ID").data("kendoDropDownList").text();

                var dlist = _.filter(vm.DyeMethodListForDC.data(), function (o) { return o.LK_DYE_MTHD_ID == vm.recipeItemC.LK_DYE_MTHD_ID });

                vm.recipeItemC.ITEM_CAT_NAME_EN = item.ITEM_CAT_NAME_EN;
                vm.recipeItemC.BRAND_NAME_EN = item.BRAND_NAME_EN;
                vm.recipeItemC.DYE_MTHD_NAME = dlist[0].DYE_MTHD_NAME;
                vm.recipeItemC.ITEM_NAME_EN = ITEM_NAME_EN;
                vm.recipeItemC.QTY_MOU_ID = "28";
                vm.recipeItemC.QTY_MOU_ST = "g/L";
                if (vm.recipeItemC.uid) {
                    // Update
                    var row = vm.recipeItemList.getByUid(vm.recipeItemC.uid);
                    var count = _.filter(vm.recipeItemList.data(), function (o) {
                        return o.ITEM_NAME_EN === vm.recipeItemC.ITEM_NAME_EN
                    }).length;

                    if (count <= 1) {
                        row["ITEM_NAME_EN"] = vm.recipeItemC.ITEM_NAME_EN;
                        row["INV_ITEM_ID"] = vm.recipeItemC.INV_ITEM_ID;
                        row["LK_DYE_MTHD_ID"] = vm.recipeItemC.LK_DYE_MTHD_ID;
                        row["DYE_MTHD_NAME"] = vm.recipeItemC.DYE_MTHD_NAME;
                        row["DOSE_QTY"] = vm.recipeItemC.DOSE_QTY;
                        row["QTY_MOU_ID"] = vm.recipeItemC.QTY_MOU_ID;
                        row["QTY_MOU_ST"] = vm.recipeItemC.QTY_MOU_ST;
                        row["MC_LD_RECIPE_D_ID"] = vm.recipeItemC.MC_LD_RECIPE_D_ID;
                        row["ITEM_CAT_NAME_EN"] = vm.recipeItemC.ITEM_CAT_NAME_EN;
                        row["BRAND_NAME_EN"] = vm.recipeItemC.BRAND_NAME_EN;

                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert
                    if (vm.recipeItemList != null) {
                        var count = _.filter(vm.recipeItemList.data(), function (o) {
                            return o.ITEM_NAME_EN === vm.recipeItemC.ITEM_NAME_EN
                        }).length;

                        if (count == 0) {
                            vm.recipeItemC.ITEM_CAT_NAME_EN = item.ITEM_CAT_NAME_EN;
                            vm.recipeItemC.BRAND_NAME_EN = item.BRAND_NAME_EN;
                            var idx = vm.recipeItemList.data().length + 1;
                            vm.recipeItemC.SL_NO = idx;
                            vm.recipeItemList.insert(idx, vm.recipeItemC);
                        }
                        else {
                            config.appToastMsg("MULTI-005 Duplicate item name exists!");
                        }
                    }
                    else {
                        var idx = 1;
                        vm.recipeItemList.insert(idx, vm.recipeItemC);
                    }

                }
                vm.recipeItemC = { LK_DYE_MTHD_ID: LD_M_ID };
                vm.recipeItemC['INV_ITEM_ID'] = "";

                var list = _.filter(vm.recipeItemList.data(), function (o) {
                    return o.QTY_MOU_ST === "%";
                })
                var sum = 0;
                for (var i = 0; i < list.length; i++)
                    sum = sum + list[i].DOSE_QTY;

                $("#TOTAL_WT").val(sum);
            }
            else {
                if (vm.recipeItemC.INV_ITEM_ID.length > 0)
                    config.appToastMsg("MULTI-005 Please input concentration quantity!");
                else
                    config.appToastMsg("MULTI-005 Please select an item!");
            }
        };


        vm.editRecipeItemData = function (data) {
            var item = angular.copy(data);

            if (item.QTY_MOU_ST == "%")
                vm.recipeItem = item;
            else
                vm.recipeItemC = item;
        }

        vm.removeRecipeItemData = function (data) {

            if (!data.MC_LD_RECIPE_D_ID || data.MC_LD_RECIPE_D_ID <= 0) {
                vm.recipeItemList.remove(data);

                var i = 0;
                var row = 0;

                var list = _.filter(vm.recipeItemList.data(), function (o) {
                    return o.SL_NO > 0;
                }).length;
                for (i = 0; i < list; i++) {
                    var list2 = _.filter(vm.recipeItemList.data(), function (o) {
                        return o.SL_NO > 0;
                    })[i];
                    list2.SL_NO = ++row;
                }

                var list = _.filter(vm.recipeItemList.data(), function (o) {
                    return o.QTY_MOU_ST === "%";
                })
                var sum = 0;
                for (var i = 0; i < list.length; i++)
                    sum = sum + list[i].DOSE_QTY;

                $("#TOTAL_WT").val(sum);

                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.recipeItemList.remove(data);
                     var i = 0;
                     var row = 0;

                     var list = _.filter(vm.recipeItemList.data(), function (o) {
                         return o.SL_NO > 0;
                     }).length;

                     for (i = 0; i < list; i++) {
                         var list2 = _.filter(vm.recipeItemList.data(), function (o) {
                             return o.SL_NO > 0;
                         })[i];
                         if (list2)
                             list2.SL_NO = ++row;
                     }
                 });

            //vm.recipeItemList.remove(data);


            var list = _.filter(vm.recipeItemList.data(), function (o) {
                return o.QTY_MOU_ST === "%";
            })
            var sum = 0;
            for (var i = 0; i < list.length; i++)
                sum = sum + list[i].DOSE_QTY;

            $("#TOTAL_WT").val(sum);
        }

        $scope.RecheckDate_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.RecheckDate_LNopened = true;
        }

        $scope.ApprovedDate_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ApprovedDate_LNopened = true;
        }

        function fiberCompositionList() {
            return vm.styleWiseFibCompListData = {
                optionLabel: "-- Select Fiber Composition --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return DyeingDataService.getDataByFullUrl('/api/Common/FibreCompList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "FIB_COMP_NAME",
                dataValueField: "RF_FIB_COMP_ID"
            };
        };

        function buyerListByBAC(MC_BYR_ACC_ID) {

            if (!MC_BYR_ACC_ID) {
                vm.accWiseBuyerList = new kendo.data.DataSource({
                    data: []
                })
                return;
            }

            DyeingDataService.getDataByFullUrl('/api/mrc/buyer/getBuyerDropdownListByID/' + MC_BYR_ACC_ID).then(function (res) {
                vm.accWiseBuyerList = new kendo.data.DataSource({
                    data: res
                })
            })
        };

        function styleListByBuyer(MC_BYR_ACC_ID, MC_BUYER_ID) {

            if (!MC_BYR_ACC_ID || !MC_BUYER_ID) {
                vm.styleList = new kendo.data.DataSource({
                    data: []
                })
                return;
            }

            DyeingDataService.getDataByFullUrl('/api/mrc/StyleH/BuyerWiseStyleListData/' + MC_BUYER_ID).then(function (sdata) {

                vm.styleList = new kendo.data.DataSource({
                    data: sdata
                })
            })
        };

        function fabricColorListByStyle(MC_STYLE_H_ID, MC_STYLE_H_EXT_ID) {

            if (!MC_STYLE_H_ID) {
                vm.styleWiseFabListData = new kendo.data.DataSource({
                    data: []
                });
                vm.colorList = new kendo.data.DataSource({
                    data: []
                });
                vm.styleWiseFabTypeListData = new kendo.data.DataSource({
                    data: []
                });
                return;
            }

            DyeingDataService.getDataByFullUrl('/api/mrc/StyleDItemFab/SelectFabByStyleID/' + (MC_STYLE_H_ID || 0)).then(function (res) {
                console.log(res);
                var list = _.filter(res, function (x) { x.LK_FBR_GRP_ID === 192 })

                vm.styleWiseFabListData = new kendo.data.DataSource({
                    data: res
                });

                vm.styleWiseFabTypeListData = new kendo.data.DataSource({
                    data: res
                });
            });

            DyeingDataService.getDataByFullUrl('/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + (MC_STYLE_H_ID || 0)).then(function (resC) {
                vm.colorList = new kendo.data.DataSource({
                    data: resC
                })
            });

            if (MC_STYLE_H_ID) {
                var pMC_STYLE_H_EXT_ID = (MC_STYLE_H_ID || 0);
                DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetOrderNoByStl/' + pMC_STYLE_H_EXT_ID).then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    vm.form.ORDER_NO = res.data.PORDER_NO;
                });
            }

        };

        vm.getPantonNo = function () {
            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetPantonNoByStlClr/' + (vm.form.MC_STYLE_H_ID || 0) + '/' + (vm.form.MC_COLOR_ID || 0)).then(function (res) {
                res['data'] = angular.fromJson(res.jsonStr);
                vm.form.PANTON_NO = res.data.PPANTON_NO;
            });

        }

        function getBuyerAcList() {

            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "contains",
                autoBind: true,
                select: function (e) {

                    var item = this.dataItem(e.item);
                    buyerListByBAC(item.MC_BYR_ACC_ID);
                    vm.styleList = new kendo.data.DataSource({
                        data: []
                    })
                },
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
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

        vm.DYMethodRearange = function () {
            vm.recipeItem.LK_DYE_MTHD_ID = vm.form.LK_DYE_MTHD_ID;
            vm.recipeItemC.LK_DYE_MTHD_ID = vm.form.LK_DYE_MTHD_ID;
            vm.getColorGroupByDyeMethod();
        }

        vm.DYMethodExchange = function (x) {
            if (x == 1)
                vm.recipeItemC.LK_DYE_MTHD_ID = vm.recipeItem.LK_DYE_MTHD_ID;
            else
                vm.recipeItem.LK_DYE_MTHD_ID = vm.recipeItemC.LK_DYE_MTHD_ID;

        }

        function getCHItemData() {
            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=4').then(function (res) {
                vm.ChItemList = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        function getDYItemData() {

            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=3').then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res,

                });
            });

        };

        vm.refreshUI = function () {
            $state.go($state.current, { pMC_LD_RECIPE_H_ID: 0, pRECHECK_ID: 0 }, { inherit: false, reload: true });
        }

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data["XML_LD_ITEM"] = DyeingDataService.xmlStringShort(vm.recipeItemList.data().map(function (o) {
                    var xx = new Date().getTime();
                    return {
                        MC_LD_RECIPE_D_ID: o.MC_LD_RECIPE_D_ID == null ? 0 : o.MC_LD_RECIPE_D_ID,
                        LK_DYE_MTHD_ID: o.LK_DYE_MTHD_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID == null ? 0 : o.INV_ITEM_ID,
                        DOSE_QTY: o.DOSE_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 0 : o.QTY_MOU_ID
                    }
                })
                    );

                var id = vm.form.MC_LD_RECIPE_H_ID;
                var rc = $stateParams.pRECHECK_ID;
                //alert(rc);
                return DyeingDataService.saveDataByUrl(data, '/LabdipRecipe/Save').then(function (res) {
                    console.log(res);

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        if (id > 0 && rc > 0) {
                            //config.appToastMsg("MULTI-001 Labdip recipe has been updated successfully");
                            $state.go("LabdipRecipe", { pMC_LD_RECIPE_H_ID: id, pRECHECK_ID: 0 });
                        }
                        else if (id > 0 && rc == 0) {
                            //config.appToastMsg("MULTI-001 Labdip recipe has been updated successfully");
                            $state.go("LabdipRecipe", { pMC_LD_RECIPE_H_ID: id, pRECHECK_ID: 0 });
                        }
                        else {
                            console.log(res.data.OPMC_LD_RECIPE_H_ID);
                            //config.appToastMsg('MULTI-001 Labdip recipe has been saved successfully');
                            $state.go($state.current, { pMC_LD_RECIPE_H_ID: res.data.OPMC_LD_RECIPE_H_ID, pRECHECK_ID: 0 }, { inherit: false, reload: true });
                        }
                    }
                });

            }
        };
    }
})();

