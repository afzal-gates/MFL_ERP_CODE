﻿(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnIssueRequisitionListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'cur_user_id', YarnIssueRequisitionListController]);
    function YarnIssueRequisitionListController($q, config, KnittingDataService, $stateParams, $state, $scope, cur_user_id) {

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

        vm.printRequisition = function (item) {
            vm.form = item;
            vm.form.REPORT_CODE = 'RPT-3513';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = vm.form;

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
        }

        vm.printReq = function (item) {
            console.log(item);
            vm.form = item;

            if (vm.form.RF_REQ_TYPE_ID == 5 || vm.form.RF_REQ_TYPE_ID == 6) {  //For Sample, Development
                vm.form.REPORT_CODE = 'RPT-3554';
            }
            else {
                vm.form.REPORT_CODE = 'RPT-3554';
            }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = vm.form;

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

                        KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
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
            //sortable: true,
            columns: [
                { field: "KNT_YRN_STR_REQ_H_ID", hidden: true },
                { field: "KNT_JOB_CRD_H_ID", hidden: true },
                { field: "KNT_SC_PRG_ISS_ID", hidden: true },
                { field: "RF_REQ_TYPE_ID", hidden: true },
                { field: "STR_REQ_BY", hidden: true },
                { field: "STR_REQ_TO", hidden: true },
                { field: "STYLE_NO", title: "Style No", type: "string", width: "10%", filterable: false },
                { field: "ORDER_NO", title: "Order No", type: "string", width: "10%", filterable: false },

                { field: "STR_REQ_NO", title: "Requisition No", type: "string", width: "10%" },
                { field: "STR_REQ_DT", title: "Requisition Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "REQ_TYPE_NAME", title: "Requisition Type", type: "string", width: "20%" },
                //{ field: "USER_NAME_EN", title: "Create By", type: "string", width: "10%" },
                { field: "TTL_REQ_QTY", title: "TTL Req. Qty", type: "string", width: "8%" },
                { field: "TTL_ISU_QTY", title: "TTL Issue Qty", type: "string", width: "8%" },
                { field: "BALANCE_QTY", title: "Balance Qty", type: "string", width: "8%" },

                {
                    field: "EVENT_NAME", title: "Status", type: "string"
                    //, template: '<span ng-class="{' + "'gride_app'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'gride_req'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'gride_isu'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + '}">#=EVENT_NAME #  </span>'
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'

                    //attributes: {
                    //    style: "color: red;"
                    //}

                    , width: "10%"
                },
                //{ field: "USER_NAME_EN", title: "Req. Status", type: "string", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        //return "</a><a ui-sref='YarnIssueReq({pKNT_YRN_STR_REQ_H_ID:dataItem.KNT_YRN_STR_REQ_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> {{dataItem.NXT_ACTION_NAME}}</i></a></a>";
                        return '</a><a ui-sref="YarnIssue({pKNT_YRN_STR_REQ_H_ID:dataItem.KNT_YRN_STR_REQ_H_ID,pRF_REQ_TYPE_ID:dataItem.RF_REQ_TYPE_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ' && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a>' +
                        '<a ui-sref="YarnIssueReq({pKNT_YRN_STR_REQ_H_ID:dataItem.KNT_YRN_STR_REQ_H_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ') && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a></a>' +
                        '<a class="btn btn-xs blue" ng-click="vm.printReq(dataItem)"><i class="fa fa-file-text"> Print Req</i></a>&nbsp;'+
                        '<a class="btn btn-xs blue" ng-click="vm.printRequisition(dataItem)"><i class="fa fa-file-text"> Print</i></a></a>';
                    },
                    width: "10%"
                }
            ]
        };

        vm.detailGridOptionsForIssue = function (dataItem) {

            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/SelectByReqID?pKNT_YRN_STR_REQ_H_ID=' + dataItem.KNT_YRN_STR_REQ_H_ID).then(function (res) {
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
                { field: "KNT_YRN_ISS_H_ID", hidden: true },
                { field: "HR_COMPANY_ID", hidden: true },
                { field: "SCM_STORE_ID", hidden: true },
                { field: "KNT_YRN_STR_REQ_H_ID", hidden: true },
                { field: "IS_FINALIZED", hidden: true },
                { field: "ITEM_ISS_BY", hidden: true },

                {
                    field: "ISS_CHALAN_NO", title: "Challan No", type: "string", width: "15%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                {
                    field: "ISS_CHALAN_DT", title: "Challan Date", type: "date", template: "#= kendo.toString(kendo.parseDate(ISS_CHALAN_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                    , width: "10%"
                },
                //{ field: "STR_REQ_NO", title: "REQ. NO", type: "string", width: "10%" },
                {
                    field: "COMP_NAME_EN", title: "Company", type: "string", width: "15%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                {
                    field: "STORE_NAME_EN", title: "Store Name", type: "string", width: "15%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                {
                    field: "REMARKS", title: "Remarks", type: "string", width: "15%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                {
                    field: "REQ_STATUS", title: "Issue Status", type: "string", width: "10%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="YarnIssue({pKNT_YRN_ISS_H_ID:dataItem.KNT_YRN_ISS_H_ID})" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs blue"><i class="fa fa-edit"> Confirm</i></a></a>';
                    },
                    width: "10%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                }

                ]
            };
        };


    }

})();