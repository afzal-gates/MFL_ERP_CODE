(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnRtnToSupReqListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'cur_user_id', YarnRtnToSupReqListController]);
    function YarnRtnToSupReqListController($q, config, KnittingDataService, $stateParams, $state, $scope, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [YarnReplacementList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function YarnReplacementList() {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = KnittingDataService.kFilterStr2QueryParam(params.filter)

                        KnittingDataService.getDataByFullUrl('/api/knit/YarnReturnToSupplier/SelectAllReq/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            })
        }

        vm.gridOptions = {
            //dataSource: new kendo.data.DataSource({
            //    serverPaging: true,
            //    serverSorting: true,
            //    serverFiltering: true,
            //    pageSize: 10,
            //    transport: {
            //        read: function (e) {
            //            var webapi = new kendo.data.transports.webapi({});
            //            var params = webapi.parameterMap(e.data);
            //            var pm = KnittingDataService.kFilterStr2QueryParam(params.filter)

            //            KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReturn/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
            //                e.success(res);
            //            });
            //        }
            //    },
            //    schema: {
            //        data: "data",
            //        total: 'total'
            //    }
            //}),
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
                { field: "KNT_YRN_STR_RPL_REQ_H_ID", hidden: true },
                { field: "PARENT_ID", hidden: true },
                { field: "SCM_STORE_ID", hidden: true },

                { field: "STR_REQ_NO", title: "Requisition No", type: "string", width: "10%" },
                { field: "STR_REQ_DT", title: "Requisition Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "12%" },
                { field: "STORE_NAME_EN", title: "Store Name", type: "string", width: "13%" },
                
                { field: "SUP_TRD_NAME_EN", title: "Supplier", type: "string", width: "10%" },
                { field: "YRN_RT_TYP_NAME", title: "Return Type", type: "string", width: "15%" },
                { field: "IMP_LC_NO", title: "L/C", type: "string", width: "10%" },
                { field: "YRN_LOT_NO", title: "Lot", type: "string", width: "10%" },
                {
                    field: "EVENT_NAME", title: "Status", type: "string", width: "10%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'
                },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="YarnRtnToSupReq({pKNT_YRN_STR_RPL_REQ_H_ID:dataItem.KNT_YRN_STR_RPL_REQ_H_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ') " class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a> ' +
                           '<a ui-sref="YarnRtnToSupIssue({pKNT_YRN_STR_RPL_REQ_H_ID:dataItem.KNT_YRN_STR_RPL_REQ_H_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ' && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a> </a>';

                    },
                    width: "10%"
                }
            ]
        };
    }

})();