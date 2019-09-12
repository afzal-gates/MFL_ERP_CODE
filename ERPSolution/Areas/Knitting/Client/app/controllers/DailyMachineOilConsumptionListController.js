(function () {
    'use strict';
    angular.module('multitex.knitting').controller('DailyMachineOilConsumptionListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'cur_user_id', DailyMachineOilConsumptionListController]);
    function DailyMachineOilConsumptionListController($q, config, KnittingDataService, $stateParams, $state, $scope, cur_user_id) {

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
                        var pm = KnittingDataService.kFilterStr2QueryParam(params.filter)

                        KnittingDataService.getDataByFullUrl('/api/knit/MacOilCons/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
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

                { field: "ISS_REF_NO", title: "Requisition No", type: "string", width: "10%" },
                { field: "ISS_REF_DT", title: "Requisition Date", type: "date", template: "#= kendo.toString(kendo.parseDate(ISS_REF_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "10%" },
                { field: "OFFICE_NAME_EN", title: "Office", type: "string", width: "10%" },
                { field: "STORE_NAME_EN", title: "Store", type: "string", width: "10%" },
                { field: "SCHEDULE_NAME_EN", title: "Shift", type: "string", width: "10%" },
                { field: "LOGIN_ID", title: "Issue By", type: "string", width: "8%" },
                                
                {
                    title: "Action",
                    template: function () {
                        //return "</a><a ui-sref='YarnIssueReq({pKNT_YRN_STR_REQ_H_ID:dataItem.KNT_YRN_STR_REQ_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> {{dataItem.NXT_ACTION_NAME}}</i></a></a>";
                        return '</a><a ui-sref="DailyMachineOilConsumption({pKNT_MCN_OIL_ISS_H_ID:dataItem.KNT_MCN_OIL_ISS_H_ID})" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a>';
                        
                        
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