(function () {
    'use strict';
    angular.module('multitex.inventory').controller('GeneralStoreTransferListController', ['$q', 'config', 'InventoryDataService', '$stateParams', '$state', '$scope', 'cur_user_id', GeneralStoreTransferListController]);
    function GeneralStoreTransferListController($q, config, InventoryDataService, $stateParams, $state, $scope, cur_user_id) {

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
                        var pm = InventoryDataService.kFilterStr2QueryParam(params.filter);
                        InventoryDataService.getDataByFullUrl('/api/inv/StoreTransfer/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total',
                    model: {
                        fields: {
                            DYE_DC_RCV_H_ID: { type: "string" },
                            STR_TR_REQ_NO: { type: "string" },
                            CHALAN_NO: { type: "string" },                            
                            STR_TR_REQ_DT: { type: "date" },
                            REQ_TYPE_NAME: { type: "string" },
                            FROM_ST_NAME: { type: "string" },
                            TO_ST_NAME: { type: "string" },

                            TTL_RQD: { type: "decimal" },
                            TTL_ISS_QTY: { type: "decimal" },
                            IS_ISSUED: { type: "decimal" },
                            EVENT_NAME: { type: "string" }
                        }
                    }

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
                { field: "STR_TR_REQ_NO", title: "REQ No", type: "string", width: "10%" },
                { field: "CHALAN_NO", title: "Gate Pass", type: "string", width: "10%" },
                { field: "STR_TR_REQ_DT", title: "Req Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_TR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                //{ field: "COMP_NAME_EN", title: "Company", type: "string", width: "10%" },
                //{ field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "10%" },
                { field: "REQ_TYPE_NAME", title: "Req. Type", type: "string", width: "10%" },
                { field: "INV_STR_TR_REQ_H_ID", title: "Req. Type", type: "string", width: "10%" },
                { field: "FROM_ST_NAME", title: "From Store", type: "string", width: "10%" },
                { field: "TO_ST_NAME", title: "To Store", type: "string", width: "10%" },
                { field: "TTL_RQD", title: "Required (Kg)", type: "string", width: "10%" },
                { field: "TTL_ISS_QTY", title: "Issued( Kg)", type: "string", width: "10%" },
                { field: "IS_ISSUED", title: "Balance (Kg)", type: "string", width: "10%" },
                //{ field: "REQ_REMARKS", title: "Remarks", type: "string", width: "10%" },
                {
                    field: "EVENT_NAME", title: "Status", type: "string", width: "15%"
                   , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'

                },

                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="GeneralStoreTransfer({pINV_STR_TR_REQ_H_ID:dataItem.INV_STR_TR_REQ_H_ID,pLK_STR_TRN_TYP_ID:dataItem.LK_STR_TRN_TYP_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ')  && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a>' +
                            '<a ui-sref="GeneralStoreTransferIssue({pINV_STR_TR_REQ_H_ID:dataItem.INV_STR_TR_REQ_H_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + '  && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a></a>';

                        //"<a ui-sref='YarnTestIssue({pKNT_YRN_STR_REQ_H_ID:dataItem.KNT_YRN_STR_REQ_H_ID})' ng-if='dataItem.RF_ACTN_STATUS_ID==9 && dataItem.PERMISSION==1' class='btn btn-xs blue'><i class='fa fa-edit'> {{dataItem.NXT_ACTION_NAME}}</i></a>" +
                        //"<a ui-sref='YarnTestReq({pKNT_YRN_STR_REQ_H_ID:dataItem.KNT_YRN_STR_REQ_H_ID})' ng-if='dataItem.RF_ACTN_STATUS_ID<=8 && dataItem.PERMISSION==1' class='btn btn-xs blue'><i class='fa fa-edit'> {{dataItem.NXT_ACTION_NAME}}</i></a></a>";
                    },
                    width: "10%"
                }
            ]
        };

        vm.detailGridOptionsForST = function (dataItem) {
            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/inv/StoreTransfer/GetInvStrTrIssue?pINV_STR_TR_REQ_H_ID=' + dataItem.INV_STR_TR_REQ_H_ID).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                scrollable: false,
                sortable: true,
                pageable: false,
                //groupable: true,
                columns: [
                { field: "INV_STR_TR_ISS_H_ID", hidden: true },
                { field: "INV_STR_TR_REQ_H_ID", hidden: true },
                { field: "ITEM_ISS_BY", hidden: true },
                { field: "ISS_REF_NO", title: "Issue Ref No", width: "15%" },
                { field: "ISS_REF_DT", title: "Issue Date", width: "10%", template: "#= kendo.toString(kendo.parseDate(ISS_REF_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #" },
                //{ field: "COLOR_GRP_NAME_EN", title: "Yarn Color Group", width: "7%" },
                { field: "GATE_PASS_NO", title: "Gate Pass No", width: "10%" },
                { field: "CARRIER_NO", title: "Carrier No", width: "10%" },
                { field: "CARRIER_NAME", title: "Carrier Name", width: "15%" },
                { field: "REMARKS", title: "Remarks", width: "15%" },
                { field: "USER_NAME_EN", title: "Created By", width: "15%" },
                {
                    title: "Action",
                    template: function () {
                        return "</a><a ui-sref='GeneralStoreTransferIssue({pINV_STR_TR_ISS_H_ID:dataItem.INV_STR_TR_ISS_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'>{{dataItem.LAST_UPDATED_BY>0?'Issue':'Edit'}}</i></a>";

                        //return "</a><a href='/DyeChemicalStoreIssue?pINV_STR_TR_REQ_H_ID?pMC_FAB_PROD_ORD_D_ID={{dataItem.MC_FAB_PROD_ORD_D_ID}}&pRF_FAB_PROD_CAT_ID={{dataItem.RF_FAB_PROD_CAT_ID}}' class='btn btn-xs blue'><i class='fa fa-edit'>{{dataItemLAST_UPDATED_BY>0?'Issue':'Edit'}}</i></a></a>";
                    },
                    width: "10%"
                }

                ]
            };
        };
    }

})();