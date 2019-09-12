(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeChemicalStoreTransferListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'cur_user_id', DyeChemicalStoreTransferListController]);
    function DyeChemicalStoreTransferListController($q, config, DyeingDataService, $stateParams, $state, $scope, cur_user_id) {

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


        vm.printChallan = function (dataItem) {
            //console.log(dataItem);

            vm.isRDLC = true;
            vm.form.REPORT_CODE = 'RPT-4008';
            vm.form.DYE_STR_TR_ISS_H_ID = dataItem.DYE_STR_TR_ISS_H_ID;
            vm.form.DYE_STR_TR_REQ_H_ID = dataItem.DYE_STR_TR_REQ_H_ID;
            
            var url = '/api/dye/dyeReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');
            
            var params = angular.copy(vm.form);

            console.log(params);

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        };

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
                        DyeingDataService.getDataByFullUrl('/api/dye/DyeChemicalStoreTransfer/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
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
                        return '</a><a ui-sref="DyeChemicalStoreTransfer({pDYE_STR_TR_REQ_H_ID:dataItem.DYE_STR_TR_REQ_H_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ')  && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a>' +
                            '<a ui-sref="DyeChemicalStoreIssue({pDYE_STR_TR_REQ_H_ID:dataItem.DYE_STR_TR_REQ_H_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + '  && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a>' +
                            '<a class="btn btn-xs yellow-gold" ng-click="vm.printChallan(dataItem);"><i class="fa fa-print"></i> Print</a>';
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
                            DyeingDataService.getDataByFullUrl('/api/Dye/DyeChemicalStoreTransfer/GetDyeStrTrIssue?pDYE_STR_TR_REQ_H_ID=' + dataItem.DYE_STR_TR_REQ_H_ID).then(function (res) {
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
                { field: "DYE_STR_TR_ISS_H_ID", hidden: true },
                { field: "DYE_STR_TR_REQ_H_ID", hidden: true },
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
                        return "</a><a ui-sref='DyeChemicalStoreIssue({pDYE_STR_TR_ISS_H_ID:dataItem.DYE_STR_TR_ISS_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'>{{dataItem.LAST_UPDATED_BY>0?'Issue':'Edit'}}</i></a>"+
                        '<a class="btn btn-xs yellow-gold" ng-click="vm.printChallan(dataItem);"><i class="fa fa-print"></i> Print</a>';
                        //return "</a><a href='/DyeChemicalStoreIssue?pDYE_STR_TR_REQ_H_ID?pMC_FAB_PROD_ORD_D_ID={{dataItem.MC_FAB_PROD_ORD_D_ID}}&pRF_FAB_PROD_CAT_ID={{dataItem.RF_FAB_PROD_CAT_ID}}' class='btn btn-xs blue'><i class='fa fa-edit'>{{dataItemLAST_UPDATED_BY>0?'Issue':'Edit'}}</i></a></a>";
                    },
                    width: "10%"
                }

                ]
            };
        };
    }

})();