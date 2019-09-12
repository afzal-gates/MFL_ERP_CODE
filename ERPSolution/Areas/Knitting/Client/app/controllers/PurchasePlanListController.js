
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('PurchasePlanListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', PurchasePlanListController]);
    function PurchasePlanListController($q, config, KnittingDataService, $stateParams, $state, $scope) {

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


        vm.printRequisition = function (dataItem, id) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-3562';

            vm.form.SCM_PURC_REQ_H_ID = dataItem.SCM_PURC_REQ_H_ID;
            vm.form.IS_EXCEL_FORMAT = id;

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
                        var pm = KnittingDataService.kFilterStr2QueryParam(params.filter)
                        KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/SelectAll/' + params.page + '/' + params.pageSize + '?pRF_REQ_SRC_ID=0' + pm).then(function (res) {
                            //KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/SelectAll/' + params.page + '/' + params.pageSize + '?pRF_REQ_SRC_ID=' + (cur_user_id == 112 ? 0 : 1) + '' + pm).then(function (res) {
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
                { field: "SCM_PURC_REQ_H_ID", hidden: true },
                { field: "PURC_REQ_NO", title: "Reqisition No", type: "string", width: "10%" },
                { field: "PURC_REQ_DT", title: "Req. Date", type: "date", template: "#= kendo.toString(kendo.parseDate(PURC_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "12%" },
                { field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "13%" },
                { field: "REQ_PRIORITY_NAME_EN", title: "Priority", type: "string", width: "10%" },
                { field: "REQ_SOURCE_NAME_EN", title: "Req. Source", type: "string", width: "10%" },
                { field: "ORDER_NO_LST", title: "Order List", type: "string", width: "15%" },
                { field: "REQ_STATUS_NAME_EN", title: "Req. Status", type: "string", width: "10%" },

                {
                    title: "Action",
                    template: function () {
                        return "</a><a ui-sref='YarnPurReq({pSCM_PURC_REQ_H_ID:dataItem.SCM_PURC_REQ_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a> " +
                            " <a ng-click='vm.printRequisition(dataItem, " + '"N"' + ")'class='btn btn-xs green'><i class='fa fa-print'> Print</i></a></a>" +
                            " <a ng-click='vm.printRequisition(dataItem, " + '"Y"' + ")' class='btn btn-xs yellow-gold'><i class='fa fa-print'> Print Excel</i></a></a>";
                    },
                    width: "10%"
                }
            ]
        };
    }

})();