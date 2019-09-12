////////// Start Header Controller
(function () {
    'use strict';
    angular.module('multitex.inventory').controller('StrIssRtnListController', ['logger', 'config', '$q', '$scope', '$http', '$filter', '$state', '$stateParams', 'InventoryDataService', 'cur_user_id', StrIssRtnListController]);
    function StrIssRtnListController(logger, config, $q, $scope, $http, $filter, $state, $stateParams, InventoryDataService, cur_user_id) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        
        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        vm.isAddToGrid = 'Y';
        vm.updateGridIndex = null;

        vm.today = new Date();
        vm.form = {};
        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getIssueReturnList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.STR_REQ_DT_LNopened = true;
        };


        vm.IssueReturnOption = {
            height: 400,
            sortable: true,
            pageable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains",
                        startswith: "Starts With",
                        eq: "Is Equal To"
                    }
                }
            },
            columns: [
                { field: "RTN_REF_NO", title: "Return #", type: "string", width: "14%" },
                { field: "RTN_DT", title: "Date", template: "#= kendo.toString(kendo.parseDate(RTN_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "DEPARTMENT_NAME_EN", title: "Department", width: "15%" },
                { field: "STORE_NAME_EN", title: "Store", width: "15%" },
                { field: "REMARKS", title: "Remarks", width: "8%" },
                //{ field: "TTL_ISS_QTY", title: "Issue Qty", width: "8%" },
                //{ field: "TTL_ISS_QTY", title: "Balance Qty", width: "8%" },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="StrIssRtn({pSCM_STR_GEN_ISS_RTN_H_ID:dataItem.SCM_STR_GEN_ISS_RTN_H_ID})" ng-if="dataItem.IS_FINALIZED=='+"'N'"+' " class="btn btn-xs green"><i class="fa fa-edit"> Edit</i></a>' +
                           ' <a class="btn btn-xs blue" ng-click="vm.printSlip(dataItem);"><i class="fa fa-print"> Print</i></a>';
                    },
                    width: "10%"
                }
            ]
        };


        function getIssueReturnList() {

            vm.IssueReturnDataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                schema: {
                    data: "data",
                    total: "total"
                },
                pageSize: 10,
                transport: {
                    read: function (e) {

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = InventoryDataService.kFilterStr2QueryParam(params.filter);
                        InventoryDataService.getDataByFullUrl('/api/inv/IssueReturn/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }


        vm.printSlip = function (dataItem) {
            //console.log(dataItem);

            vm.isRDLC = true;
            vm.form['REPORT_CODE'] = 'RPT-3003';

            vm.form['SCM_STR_GEN_REQ_H_ID'] = dataItem.SCM_STR_GEN_REQ_H_ID;
            var url = '/api/Inv/InvReport/PreviewReportRDLC';

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

    }

})();
////////// End Header Controller

