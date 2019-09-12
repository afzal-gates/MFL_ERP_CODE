(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DCIssuedLoanAdjustListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', DCIssuedLoanAdjustListController]);
    function DCIssuedLoanAdjustListController($q, config, DyeingDataService, $stateParams, $state, $scope) {

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
                        var pm = DyeingDataService.kFilterStr2QueryParam(params.filter);
                        DyeingDataService.getDataByFullUrl('/api/dye/DCReceiveableLoan/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            }),
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
            columns: [
                { field: "DYE_DC_LRT_H_ID", hidden:true },
                { field: "HR_COMPANY_ID", hidden:true },
                { field: "SCM_STORE_ID", hidden:true },
                { field: "SCM_SUPPLIER_ID", hidden:true },
                { field: "RCV_DT", hidden:true },
                { field: "ITEM_RCV_BY", hidden:true },
                { field: "CARRIER_NAME", hidden:true },
                { field: "CARRIER_NO", hidden:true },
                { field: "RF_ACTN_STATUS_ID", hidden:true },
                { field: "REMARKS", hidden:true },

                { field: "RET_CHALAN_NO", title: "Return Challan No", type: "string", width: "15%" },
                { field: "RET_CHALAN_DT", title: "Rtn. Date", type: "date", template: "#= kendo.toString(kendo.parseDate(RET_CHALAN_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "12%" },
                { field: "STORE_NAME_EN", title: "Store", type: "string", width: "13%" },
                { field: "SUP_TRD_NAME_EN", title: "Supplier Name", type: "string", width: "10%" },
                {
                    field: "ACTN_STATUS_NAME", title: "Status", type: "string", width: "15%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.IS_FINALIZED==' + "'Y'" + ',' + "'grid_ri'" + ':dataItem.IS_FINALIZED==' + "'N'" + '}">#=ACTN_STATUS_NAME #  </span>'

                },
                { field: "ITEM_RECV_BY_NAME", title: "Received By", type: "string", width: "10%" },

                {
                    title: "Action",
                    template: function () {
                        //return "</a><a ui-sref='DCIssuedLoanAdjust({pDYE_DC_LRT_H_ID:dataItem.DYE_DC_LRT_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a></a>";
                        return '</a><a ui-sref="DCIssuedLoanAdjust({pDYE_DC_LRT_H_ID:dataItem.DYE_DC_LRT_H_ID})" ng-if="dataItem.IS_FINALIZED==' + "'N'" +'" class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a> ';
                        //+'<a ui-sref="DCIssue({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ' && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a></a>';
                    },
                    width: "10%"
                }
            ]
        };
    }

})();