(function () {
    'use strict';
    angular.module('multitex.mrc').controller('LabdipRecipeListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', '$timeout', 'DateShareService', '$sessionStorage', LabdipRecipeListController]);
    function LabdipRecipeListController($q, config, DyeingDataService, $stateParams, $state, $scope, $timeout, DateShareService, $sessionStorage) {


        var vm = this;
        vm.Title = $state.current.Title || '';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getBuyerAccListData(), getBuyerAcGrpList(), getStyleListData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        /*
            if (!$sessionStorage.SessionMessage) {
                if ($sessionStorage.SessionMessage.length == 0) {
                    $sessionStorage.SessionMessage = [];
                    $sessionStorage.SessionMessage["labdip"] = {};
                }
            }
        */

        vm.form = $sessionStorage.SessionMessage ? $sessionStorage.SessionMessage : {};

        function getStyleListData() {
            //vm.styleList = new kendo.data.DataSource({
            //    data: []
            //});

            return vm.styleList = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0); //pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0);

                        url += DyeingDataService.kFilterStr2QueryParam(params.filter);

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
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
                    console.log(item);

                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;

                    vm.styleList.read();
                },
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID"
            };
        }


        vm.getStyleListByBA = function () {
            if (vm.form.MC_BYR_ACC_ID > 0)
                return DyeingDataService.getDataByFullUrl('/api/mrc/StyleH/BuyerAccWiseStyleListData/' + vm.form.MC_BYR_ACC_ID).then(function (res) {
                    vm.styleList = new kendo.data.DataSource({
                        data: res
                    });
                });
            else
                vm.styleList = new kendo.data.DataSource({
                    data: []
                });

        };

        function getBuyerAccListData() {
            return vm.buyerAccList = {
                optionLabel: "-- Select Buyer Account--",
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

        vm.showLabdipRecipe = function () {

            var data = {
                MC_BYR_ACC_GRP_ID: vm.form.MC_BYR_ACC_GRP_ID,
                MC_STYLE_H_ID: vm.form.MC_STYLE_H_ID,
                LD_RECIPE_NO: vm.form.LD_RECIPE_NO,
                PAGE_NO: 1
            };

            //$sessionStorage.SessionMessage["labdip"] = angular.copy(data);
            $sessionStorage.SessionMessage = angular.copy(data);

            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLabdipRecipeList?pMC_BYR_ACC_GRP_ID=' + vm.form.MC_BYR_ACC_GRP_ID + '&pMC_STYLE_H_ID=' + vm.form.MC_STYLE_H_ID + '&pLD_RECIPE_NO=' + (vm.form.LD_RECIPE_NO || "")).then(function (res) {

                var dataSource = new kendo.data.DataSource({
                    data: res,
                    pageSize: 20,
                });

                $('#labdipRecipeGrid').data("kendoGrid").setDataSource(dataSource);
                //$timeout(function () {
                //    $('#labdipRecipeGrid').data("kendoGrid").setDataSource(dataSource);
                //})
            });
        };

        var LabdipRecipeDataSource = new kendo.data.DataSource({

            batch: true,
            transport: {
                read: function (e) {

                    //var data = angular.copy($sessionStorage.SessionMessage["labdip"]);
                    var data = angular.copy($sessionStorage.SessionMessage);

                    DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLabdipRecipeList?pMC_BYR_ACC_GRP_ID=' + (data.MC_BYR_ACC_GRP_ID || 0) + '&pMC_STYLE_H_ID=' + (data.MC_STYLE_H_ID || 0) + '&pLD_RECIPE_NO=' + (data.LD_RECIPE_NO || "")).then(function (res) {

                        e.success(res);
                    });
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
            pageSize: 20
        });

        vm.gridOptions = {
            autoBind: true,
            options: "contains",
            dataSource: LabdipRecipeDataSource,
            //dataSource: {
            //    transport: {
            //        read: function (e) {
            //            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLabdipRecipeList').then(function (res) {
            //                e.success(res);
            //            });
            //        }
            //    },
            //    pageSize: 10
            //},
            filterable: true,
            selectable: "row",
            sortable: true,
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "BUYER_NAME_EN", title: "Buyer Name", type: "string", width: "15%" },
                { field: "STYLE_NO", title: "Style No", type: "string", width: "15%" },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "15%" },
                { field: "FAB_TYPE_NAME", title: "Fabric Type", type: "string", width: "15%" },
                { field: "FIB_COMP_NAME", title: "Fiber Comp", type: "string", width: "15%" },

                { field: "LD_RECIPE_NO", title: "Recipe No", type: "string", width: "12%" },
                { field: "OPTION_NO", title: "Option No", type: "string", width: "12%" },
                //{ field: "SUP_OWNER_NAME", title: "Owner Name", type: "string", width: "50px" },
                //{ field: "RQD_ROOM_TEMP", title: "ROOM TEMP", type: "string", width: "15%" },

                {
                    title: "Action",
                    template: function () {
                        return "</a><a ui-sref='LabdipRecipe({pMC_LD_RECIPE_H_ID:dataItem.MC_LD_RECIPE_H_ID, pRECHECK_ID:0})' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a>   <a ui-sref='LabdipRecipe({pMC_LD_RECIPE_H_ID:dataItem.MC_LD_RECIPE_H_ID, pRECHECK_ID:1})' class='btn btn-xs purple'><i class='fa fa-edit'> Re-check</i></a></a>";
                    },
                    width: "15%"
                }
            ]
        };
    }

})();