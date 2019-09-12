(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('RequisitionListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', RequisitionListController]);
    function RequisitionListController($q, config, DyeingDataService, $stateParams, $state, $scope) {

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
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/PurcReq/GetRequisitionList').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 15
            },
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
                { field: "PURC_REQ_NO", title: "Reqisition No", type: "string", width: "10%" },
                { field: "PURC_REQ_DT", title: "Req. Date", type: "date", format: "{0:yyyy-MM-dd}", width: "10%" },
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
                        return "</a><a ui-sref='Requisition({pSCM_PURC_REQ_H_ID:dataItem.SCM_PURC_REQ_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a></a>";
                    },
                    width: "10%"
                }
            ]
        };
    }

})();