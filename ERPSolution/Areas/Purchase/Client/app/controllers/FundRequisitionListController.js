(function () {
    'use strict';
    angular.module('multitex.purchase').controller('FundRequisitionListController', ['$q', 'config', 'PurchaseDataService', '$stateParams', '$state', '$scope', FundRequisitionListController]);
    function FundRequisitionListController($q, config, PurchaseDataService, $stateParams, $state, $scope) {

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

        vm.printRequisition = function (dataItem) {

            vm.form.REPORT_CODE = 'RPT-8002';

            vm.form.SCM_FUND_REQ_H_ID = dataItem.SCM_FUND_REQ_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Pur/PurReport/PreviewReportRDLC');
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
        };


        vm.printRequisitionPO = function (dataItem) {

            vm.form.REPORT_CODE = 'RPT-8003';

            vm.form.SCM_FUND_REQ_H_ID = dataItem.SCM_FUND_REQ_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Pur/PurReport/PreviewReportRDLC');
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
                        var pm = PurchaseDataService.kFilterStr2QueryParam(params.filter)
                        PurchaseDataService.getDataByFullUrl('/api/Purchase/fund/SelectAll/' + params.page + '/' + params.pageSize + pm).then(function (res) {
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
                { field: "REQ_NO", title: "Reqisition No", type: "string", width: "10%" },
                { field: "REQ_DT", title: "Req. Date", type: "date", template: "#= kendo.toString(kendo.parseDate(REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "15%" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "15%" },
                //{ field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "13%" },
                //{ field: "REQ_PRIORITY_NAME_EN", title: "Priority", type: "string", width: "10%" },
                //{ field: "REQ_SOURCE_NAME_EN", title: "Req. Source", type: "string", width: "10%" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "10%" },
                //{ field: "ACTN_STATUS_NAME", title: "Req. Status", type: "string", width: "10%" },

                {
                    field: "EVENT_NAME", title: "Status", type: "string"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'
                                        , width: "10%"
                },
                {
                    title: "Action",
                    template: function () {
                        return "</a><a ui-sref='FundRequisition({pSCM_FUND_REQ_H_ID:dataItem.SCM_FUND_REQ_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> {{dataItem.NXT_ACTION_NAME}}</i></a> <a ng-click='vm.printRequisition(dataItem)' class='btn btn-xs green'><i class='fa fa-print'> Print</i></a> "
                            + " <a ng-click='vm.printRequisitionPO(dataItem)' class='btn btn-xs yellow-gold'><i class='fa fa-print'> Print PO</i></a></a>";
                    },
                    width: "15%"
                }
            ]
        };
    }

})();