(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TnAFollowupTaskController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', '$modal', TnAFollowupTaskController]);
    function TnAFollowupTaskController($q, config, MrcDataService, $stateParams, $state, $scope, $modal) {

        var vm = this;
        vm.errors = null;
        vm.Title = $state.current.Title || '';

        activate()

        vm.showSplash = true;
        var OLD_MC_BYR_ACC_ID = 0;

        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        $scope.filterTna = ['N', 'N', 'N', 'N', 'N'];
        $scope.filterTnaName = [':Today', ': Tomorrow', ': Day After Tomorrow', ': Next 7 Days', ': Next 30 Days'];
        $scope.filterRules = [
            '0',
            '1',
            '2',
            '0,1,2,3,4,5,6',
            '0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29'
        ];


        $scope.$watch('filterTna', function (newVal, oldVal) {
            $scope.FilterName = '';
            if (!_.isEqual(newVal, oldVal)) {
                var index = newVal.indexOf('Y');
                if (index >= 0) {
                    $scope.FilterName = $scope.filterTnaName[index];
                    vm.dataSource = new kendo.data.DataSource({
                        serverPaging: true,
                        serverFiltering: true,
                        serverSorting: false,
                        schema: {
                            data: 'data',
                            total: 'total',
                            model: {
                                fields: {
                                    TASK_ORDER: { type: 'number' },
                                    PLAN_START_DT: { type: 'date' },
                                    ACT_START_DT: { type: 'date' }
                                }
                            }
                        },
                        transport: {
                            read: function (e) {
                                var webapi = new kendo.data.transports.webapi({});
                                var params = webapi.parameterMap(e.data);
                                var url = '/api/common/OrderTnADataTask?pMC_ORDER_H_ID=' + $stateParams.MC_ORDER_H_ID;
                                url += '&pageNumber=1&pageSize=20&DpD2GList=' + $scope.filterRules[index];
                                url += MrcDataService.kFilterStr2QueryParam(params.filter);

                                return MrcDataService.getDataByFullUrl(url).then(function (res) {
                                    e.success({
                                        total: res.total,
                                        data: res.data.map(function (ob) {
                                            ob['U'] = _.some(ob.CRUD_FLAG.split(''), function (val) {
                                                return val == 'U'
                                            });
                                            return ob;
                                        })
                                    });
                                }, function (err) {
                                    console.log(err);
                                })
                            }
                        },
                        //sort: { field: "TASK_ORDER", dir: "asc" },
                        pageSize: 100
                    });
                } else {
                    vm.dataSource = new kendo.data.DataSource({
                        serverPaging: true,
                        serverFiltering: true,
                        serverSorting: false,
                        schema: {
                            data: 'data',
                            total: 'total',
                            model: {
                                fields: {
                                    TASK_ORDER: { type: 'number' },
                                    PLAN_START_DT: { type: 'date' },
                                    ACT_START_DT: { type: 'date' }
                                }
                            }
                        },
                        transport: {
                            read: function (e) {
                                var webapi = new kendo.data.transports.webapi({});
                                var params = webapi.parameterMap(e.data);

                                var url = '/api/common/OrderTnADataTask?pMC_ORDER_H_ID=' + $stateParams.MC_ORDER_H_ID;
                                url += MrcDataService.kFilterStr2QueryParam(params.filter);

                                return MrcDataService.getDataByFullUrl(url).then(function (res) {
                                    e.success({
                                        total: res.total,
                                        data: res.data.map(function (ob) {
                                            ob['U'] = _.some(ob.CRUD_FLAG.split(''), function (val) {
                                                return val == 'U'
                                            });
                                            return ob;
                                        })
                                    });
                                }, function (err) {
                                    console.log(err);
                                })
                            }
                        },
                        //sort: { field: "TASK_ORDER", dir: "asc" },
                        pageSize: 100
                    });

                }
            }

        }, true);


        vm.openTnaUpdateModal = function (dataItem) {
            console.log(dataItem);
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Merchandising/Mrc/_UpdateTnAModal',
                controller: 'TnaUpdateModalController',
                size: 'small',
                windowClass: 'large-Modal',
                resolve: {
                    Order: function () {
                        return $scope.$parent.Order;
                    },
                    Task: function () {
                        return dataItem;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                if (data) {
                    $("#PO_LIST_TASK").data("kendoGrid").dataSource.read();
                }
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };


        vm.dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            schema: {
                data: 'data',
                total: 'total',
                model: {
                    fields: {
                        TASK_ORDER: { type: 'number' },
                        PLAN_START_DT: { type: 'date' },
                        ACT_START_DT: { type: 'date' }
                    }
                }
            },
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    if ($stateParams.MC_ORDER_H_ID > 0) {
                        var url = '/api/common/OrderTnADataTask?pMC_ORDER_H_ID=' + $stateParams.MC_ORDER_H_ID;
                        url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += MrcDataService.kFilterStr2QueryParam(params.filter);

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
                            e.success({
                                total: res.total,
                                data: res.data.map(function (ob) {
                                    ob['U'] = _.some(ob.CRUD_FLAG.split(''), function (val) {
                                        return val == 'U'
                                    });
                                    return ob;
                                })
                            });
                        }, function (err) {
                            console.error(err);
                        })
                    } else {
                        e.success([]);
                    }

                }
            },
            //sort: { field: "TASK_ORDER", dir: "asc" },
            pageSize: 100
        });

        //vm.dataSource.sort({ field: "TASK_ORDER", dir: "asc" });

        vm.PO_LIST_OPTIONS_TASK = {
            autoBind: true,
            toolbar: kendo.template($("#TnATaskToolBarTemplate").html()),
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
            height: 500,
            //scrollable: {
            //    virtual: true
            //},

            selectable: false,
            sortable: true,
            columns: [

                //{
                //    title: "Task Name",
                //    field:'TA_TASK_NAME_EN',
                //    template: function () {
                //        return "<span class='item-status'><span class='badge badge-empty' ng-class=\"{'badge-warning':!dataItem.ACT_START_DT,'badge-success':dataItem.ACT_START_DT}\"></span> {{dataItem.TASK_ORDER}}</span>. <a class='btn btn-link btn-xs' ng-class=\"{'disabled': dataItem.IS_AUTO_UPDATE=='Y', 'disabled': dataItem.U,'disabled' : dataItem.IS_START_TASK=='Y','disabled':dataItem.SHOULD_U =='N'}\" ng-click='vm.openTnaUpdateModal(dataItem)'>{{dataItem.TA_TASK_NAME_EN}}</a>";
                //    },
                //    width: "250px",
                //    filterable: true
                //},
                {
                    title: "Task Name",
                    field: 'TA_TASK_NAME_EN',
                    template: function () {
                        return "<span class='item-status'><span class='badge badge-empty' ng-class=\"{'badge-warning':!dataItem.ACT_START_DT,'badge-success':dataItem.ACT_START_DT}\"></span> {{dataItem.TASK_ORDER}}</span>. <a class='btn  btn-xs' ng-class=\"{'disabled': dataItem.IS_AUTO_UPDATE=='Y','disabled' : dataItem.IS_START_TASK=='Y','disabled': (dataItem.ACT_START_DT && dataItem.CRUD_FLAG!=='CRUD') }\" ng-disabled='!dataItem.U' ng-click='vm.openTnaUpdateModal(dataItem)'><strong><b>{{dataItem.TA_TASK_NAME_EN}}</b></strong></a>";
                    },
                    width: "200px",
                    filterable: true
                },

                 {
                     field: "DAYS_TO_GO", title: "D2G", type: "string", width: "35px", filterable: false,
                     template: function () {
                         return "<span class='badge' ng-class=\"{'badge-warning':dataItem.DAYS_TO_GO==0 && !dataItem.ACT_START_DT}\">{{dataItem.DAYS_TO_GO}}</span>";
                     },

                 },
                 {
                     field: "PLAN_START_DT", title: "Plan", type: "date", width: "60px", format: "{0:dd-MMM-yyyy}"
                 },

                {
                    field: "ACT_START_DT", title: "Actual", type: "date", width: "60px", format: "{0:dd-MMM-yyyy}"
                },
                {
                    field: "LAG_DAYS", title: "LGD", type: "string", width: "35px", filterable: false,
                    template: function () {
                        return "<span class='badge' ng-class=\"{'badge-danger':dataItem.LAG_DAYS<0,'badge-success':!dataItem.LAG_DAYS && dataItem.ACT_START_DT}\">{{dataItem.LAG_DAYS}}</span>";
                    },
                },
                {
                    field: "REMARKS", title: "Comment", type: "string", width: "80px"
                },
            ]
        };

    }

})();