(function () {
    'use strict';
    angular.module('multitex.knitting').controller('AdvPurReqListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', AdvPurReqListController]);
    function AdvPurReqListController($q, config, KnittingDataService, $stateParams, $state, $scope) {

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
                        KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/SelectAll/' + params.page + '/' + params.pageSize + '?pRF_REQ_SRC_ID=2' + pm).then(function (res) {
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
            sortable: true,
            columns: [
                { field: "PURC_REQ_NO", title: "Reqisition No", type: "string", width: "10%" },
                { field: "PURC_REQ_DT", title: "Req. Date", type: "date", template: "#= kendo.toString(kendo.parseDate(PURC_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "12%" },
                { field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "13%" },
                { field: "REQ_PRIORITY_NAME_EN", title: "Priority", type: "string", width: "10%" },
                { field: "REQ_SOURCE_NAME_EN", title: "Req. Source", type: "string", width: "10%" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "15%" },
                { field: "REQ_STATUS_NAME_EN", title: "Req. Status", type: "string", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return "</a><a ui-sref='GenBuffYarnRequisition({pSCM_PURC_REQ_H_ID:dataItem.SCM_PURC_REQ_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a></a>";
                    },
                    width: "10%"
                }
            ]
        };
    }

})();