
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DfOnlineQcListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'cur_user_id', DfOnlineQcListController]);
    function DfOnlineQcListController($q, config, DyeingDataService, $stateParams, $state, $scope, cur_user_id) {

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
                        DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/SelectOnlineQCList').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                //schema: {
                //    data: "data",
                //    total: 'total'

                //}
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
            sortable: true,
            columns: [
                { field: "DF_PROC_QC_RPT_H_ID", hidden: true },
                { field: "DYE_BT_CARD_H_ID", hidden: true },
                { field: "RPT_SL_NO", hidden: true },
                { field: "BUYER_NAME_EN", title: "Buyer", type: "string", width: "10%" },
                { field: "STYLE_NO", title: "Style", type: "string", width: "10%" },
                { field: "QC_DT", title: "QC Date", type: "date", template: "#= kendo.toString(kendo.parseDate(QC_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "ORDER_NO", title: "Order", type: "string", width: "10%" },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "10%" },
                { field: "DYE_BATCH_NO", title: "Batch", type: "string", width: "10%" },
                { field: "DYE_LOT_NO", title: "Lot", type: "string", width: "5%" },
                { field: "BT_STS_TYP_NAME", title: "Current Status", type: "string", width: "10%" },
                { field: "COMP_SNAME", title: "Company", type: "string", width: "5%" },
                { field: "IS_QC_OK", title: "QC OK", type: "string", width: "5%" },
                { field: "IS_SELF_QC", title: "QC OK", type: "string", width: "5%" },
                { field: "BATCH_RP_QTY", title: "QC Qty", type: "string", width: "5%" },
                
                //COMP_NAME_EN, ACTN_ROLE_FLAG, ACTN_STATUS_NAME
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="DfOnlineQc({pDF_PROC_QC_RPT_H_ID:dataItem.DF_PROC_QC_RPT_H_ID})" class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a>';
                    },
                    width: "10%"
                }

            ]
        };

    }

})();