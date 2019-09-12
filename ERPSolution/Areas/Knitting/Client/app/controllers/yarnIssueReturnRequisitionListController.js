(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnIssueReturnRequisitionListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'cur_user_id', YarnIssueReturnRequisitionListController]);
    function YarnIssueReturnRequisitionListController($q, config, KnittingDataService, $stateParams, $state, $scope, cur_user_id) {

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


        vm.printRtnChallan = function (item) {
            vm.form = item;
            vm.form.REPORT_CODE = 'RPT-3510';

            vm.form.KNT_YRN_STR_REQ_H_ID = item.KNT_YRN_STR_REQ_H_ID;
            vm.form.KNT_YRN_ISS_RET_H_ID = item.KNT_YRN_ISS_RET_H_ID;

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

                        KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReturn/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
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
            sortable: true,
            columns: [
                { field: "KNT_YRN_ISS_RET_H_ID", hidden: true },
                { field: "HR_COMPANY_ID", hidden: true },
                { field: "SCM_STORE_ID", hidden: true },

                { field: "RET_CHALAN_NO", title: "Challan No", type: "string", width: "10%" },
                { field: "RET_CHALAN_DT", title: "Challan Date", type: "date", template: "#= kendo.toString(kendo.parseDate(RET_CHALAN_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "12%" },
                { field: "STORE_NAME_EN", title: "Store Name", type: "string", width: "13%" },
                
                { field: "USER_NAME_EN", title: "Return By", type: "string", width: "10%" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "15%" },
                { field: "REQ_STATUS", title: "Ret. Status", type: "string", width: "10%" },
                {
                    field: "EVENT_NAME", title: "Status", type: "string", width: "10%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'
                },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="YarnIssueRtnReq({pKNT_YRN_ISS_RET_H_ID:dataItem.KNT_YRN_ISS_RET_H_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ') && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a> ' +
                           '<a ui-sref="YarnIssueRtn({pKNT_YRN_ISS_RET_H_ID:dataItem.KNT_YRN_ISS_RET_H_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SR'" + ' && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a> '+
                           ' <a class="btn btn-xs blue" ng-click="vm.printRtnChallan(dataItem)"><i class="fa fa-file-text"> Print</i></a></a>';


                        //return '</a><a ui-sref="YarnIssueRtnReq({pKNT_YRN_ISS_RET_H_ID:dataItem.KNT_YRN_ISS_RET_H_ID})" ng-if="dataItem.RF_ACTN_STATUS_ID==17 && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit">A {{dataItem.NXT_ACTION_NAME}}</i></a> ' +
                        //    '<a ui-sref="YarnIssueRtn({pKNT_YRN_ISS_RET_H_ID:dataItem.KNT_YRN_ISS_RET_H_ID})" ng-if="dataItem.RF_ACTN_STATUS_ID<=15 && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit">B {{dataItem.NXT_ACTION_NAME}}</i></a> </a>';

                    },
                    width: "10%"
                }
            ]
        };
    }

})();