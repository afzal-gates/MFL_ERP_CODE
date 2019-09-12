(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TnAFollowupController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', '$modal', TnAFollowupController]);
    function TnAFollowupController($q, config, MrcDataService, $stateParams, $state, $scope, $modal) {

        var vm = this;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        activate()

        vm.showSplash = true;
        var OLD_MC_BYR_ACC_ID = 0;
        vm.form = {};


        function activate() {
            var promise = [getbuyerAcList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
                vm.form['MC_BYR_ACC_ID'] = (angular.isDefined($stateParams.MC_BYR_ACC_ID) && $stateParams.MC_BYR_ACC_ID > 0) ? $stateParams.MC_BYR_ACC_ID : '';
            });
        }


        function getbuyerAcList() {
            return vm.buyerAcList = {
                optionLabel: "--- Buyer A/C ---",
                filter: "contains",
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
                dataBound: function (e) {
                    var ds = this.dataSource.data();
                    if (ds.length == 1) {
                        this.value(ds[0].MC_BYR_ACC_ID);
                        vm.form['MC_BYR_ACC_ID'] = ds[0].MC_BYR_ACC_ID;
                        vm.BuyerWiseOrderList(ds[0].MC_BYR_ACC_ID);
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }

        vm.dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total"
            },
            transport: {
                read: function (e) {
                    if ($stateParams.MC_BYR_ACC_ID > 0) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/common/OrderTnAData?pMC_BYR_ACC_ID=' + $stateParams.MC_BYR_ACC_ID + '&pMC_ORDER_H_ID=' + $stateParams.MC_ORDER_H_ID;
                        url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += MrcDataService.kFilterStr2QueryParam(params.filter);

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.error(err);
                        })
                    } else {
                        e.success([]);
                    }

                }
            },
            pageSize: 14
        });


        vm.BuyerWiseOrderList = function (NEW_MC_BYR_ACC_ID) {
            if (!NEW_MC_BYR_ACC_ID) return;

            if (OLD_MC_BYR_ACC_ID != NEW_MC_BYR_ACC_ID) {
                OLD_MC_BYR_ACC_ID = angular.copy(NEW_MC_BYR_ACC_ID);

                $scope.Order = undefined;
                $state.go('TnAFollowup.TnaTask', { MC_BYR_ACC_ID: NEW_MC_BYR_ACC_ID, MC_ORDER_H_ID: 0 }, { reload: 'TnAFollowup.TnaTask' });


                vm.dataSource = new kendo.data.DataSource({
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
                            var url = '/api/common/OrderTnAData?pMC_BYR_ACC_ID=' + NEW_MC_BYR_ACC_ID;
                            url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                            url += MrcDataService.kFilterStr2QueryParam(params.filter);
                            MrcDataService.getDataByFullUrl(url).then(function (res) {
                                e.success(res);
                            });
                        }
                    },
                    pageSize: 14
                });


            }
        }



        vm.PO_LIST_OPTIONS = {
            autoBind: true,
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
            dataBound: function () {
                var grid = this;
                $.each(grid.tbody.find('tr'), function () {
                    
                    if ($stateParams.MC_ORDER_H_ID == 0) return;
                    var model = grid.dataItem(this);
                    if (model.MC_ORDER_H_ID == $stateParams.MC_ORDER_H_ID) {
                        $scope.Order = model;
                        $state.go('TnAFollowup.TnaTask', { MC_ORDER_H_ID: model.MC_ORDER_H_ID }, { reload: 'TnAFollowup.TnaTask' });
                        $('[data-uid=' + model.uid + ']').addClass('k-state-selected');
                    }
                });
            },
            change: function (e) {
                var item = e.sender.dataItem(this.select());
                $scope.Order = item;
                $state.go('TnAFollowup.TnaTask', { MC_ORDER_H_ID: item.MC_ORDER_H_ID }, { reload: 'TnAFollowup.TnaTask' });
            },
            selectable: true,
            sortable: true,
            scrollable: true,
            pageable: true,
            //height: 480,
            //scrollable: {
            //    virtual: true
            //},
            columns: [
                { field: "WORK_STYLE_NO", title: "Style No", type: "string", width: "150px", filterable: true },
                { field: "ORDER_NO", title: "Order No", type: "string", width: "150px", filterable: true },
                { field: "ORD_TYPE_NAME", title: "Order Type", type: "string", width: "100px", filterable: { ui: orderTypeFilter } },
                //{ field: "JOB_TRAC_NO", title: "Job #", type: "string", width: "150px", filterable: true }               
            ]
        };

        function orderTypeFilter(element) {

            MrcDataService.LookupListData(40).then(function (res) {
                //kendoDropDownList
                //kendoAutoComplete
                element.kendoDropDownList({
                    dataSource: _.map(res, 'LK_DATA_NAME_EN'),
                    optionLabel: "--Order Type--"
                });
            });

        }


    }

})();