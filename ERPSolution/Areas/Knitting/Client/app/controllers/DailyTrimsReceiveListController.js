(function () {
    'use strict';
    angular.module('multitex.knitting').controller('DailyTrimsReceiveListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', DailyTrimsReceiveListController]);
    function DailyTrimsReceiveListController($q, config, KnittingDataService, $stateParams, $state, $scope) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.gridOptions = {
            dataSource: new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = KnittingDataService.kFilterStr2QueryParam(params.filter)

                        KnittingDataService.getDataByFullUrl('/api/knit/DailyTrimsReceive/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            }),
            //scrollable: {
            //    virtual: true,
            //},
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains"
                    }
                }
            },
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            //sortable: true,
            columns: [
                { field: "MRR_NO", title: "MRR No", type: "string", width: "10%" },
                { field: "MRR_DT", title: "MRR Date", type: "date", template: "#= kendo.toString(kendo.parseDate(MRR_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "12%" },
                { field: "REQ_TYPE_NAME", title: "Req. Type", type: "string", width: "13%" },
                { field: "PAY_MTHD_NAME", title: "Pay Method", type: "string", width: "10%" },
                { field: "LOC_SRC_TYPE_NAME", title: "Source Type", type: "string", width: "10%" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "15%" },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="DailyTrimsReceiveView({pINV_TRMS_RCV_H_ID:dataItem.INV_TRMS_RCV_H_ID})" class="btn btn-xs green"><i class="fa fa-eye"> View</i></a> <a ui-sref="DailyTrimsReceive({pINV_TRMS_RCV_H_ID:dataItem.INV_TRMS_RCV_H_ID})" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a></a>';
                    },
                    width: "10%"
                }
            ]
        };
    }

})();