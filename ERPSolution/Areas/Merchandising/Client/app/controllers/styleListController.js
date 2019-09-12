(function () {
    'use strict';
    angular.module('multitex.mrc').controller('StyleListController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', '$modal', StyleListController]);
    function StyleListController($q, config, MrcDataService, $stateParams, $state, $scope, logger, $modal) {

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
                        var tmpl = "</a><a ui-sref='StyleH.Item({MC_STYLE_H_ID:dataItem.MC_STYLE_H_ID,MC_STYLE_D_ITEM_ID:dataItem.MC_STYLE_D_ITEM_ID})' class='btn btn-xs blue'><i class='fa fa-edit'></i></a></a>";
                        var tmplDev = "</a><a ui-sref='StyleHDev.ItemDev({MC_STYLE_H_ID:dataItem.MC_STYLE_H_ID,MC_STYLE_D_ITEM_ID:dataItem.MC_STYLE_D_ITEM_ID})' class='btn btn-xs blue'><i class='fa fa-edit'></i></a></a>";
                        return $state.current.LK_STYL_DEV_ID == 265 ? tmplDev : tmpl;
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