(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DCRequestLoanRequisitionListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', DCRequestLoanRequisitionListController]);
    function DCRequestLoanRequisitionListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        var hubUrl = "/signalr";
        var connection = $.hubConnection(hubUrl, { useDefaultPath: true });
        var hub = connection.createHubProxy("dashboard");
        var hubStart = connection.start();

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [requisitionList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        var hub = new Hub('dashboard', {
            listeners: {
                'requisitionList': function () {
                    requisitionList();
                    $rootScope.$apply()
                },
                'newConnection': function (id) {
                    vm.connected.push(id);
                    $rootScope.$apply();
                },
                'removeConnection': function (id) {
                    $rootScope.$apply();
                }

            },
            methods: [],
            errorHandler: function (error) {
                console.error(error);
            }
        });

        vm.connected = [];

        function requisitionList() {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = DyeingDataService.kFilterStr2QueryParam(params.filter);
                        DyeingDataService.getDataByFullUrl('/api/dye/DCIssueRequisition/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pRF_REQ_TYPE_ID=11&pOption=3006&pUSER_ID=' + cur_user_id).then(function (res) {
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
            
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            sortable: true,
            columns: [
                { field: "STR_REQ_NO", title: "REQ No", type: "string", width: "10%" },
                { field: "STR_REQ_DT", title: "Req Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "10%" },
                //{ field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "10%" },
                { field: "REQ_TYPE_NAME", title: "Req. Type", type: "string", width: "10%" },
                { field: "FROM_ST_NAME", title: "From Store", type: "string", width: "10%" },
                //{ field: "TO_ST_NAME", title: "To Store", type: "string", width: "10%" },

                { field: "TTL_RQD", title: "Required Qty", type: "string", width: "10%" },
                { field: "TTL_ISS_QTY", title: "Issued Qty", type: "string", width: "10%" },
                { field: "BALANCE_QTY", title: "Balance Qty", type: "string", width: "10%" },


                { field: "REMARKS", title: "Remarks", type: "string", width: "10%" },
                {
                    field: "EVENT_NAME", title: "Status", type: "string", width: "15%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'

                },

                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="DCRequestLoanRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ') && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a> ' +
                            '<a ui-sref="DCIssue({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ' && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a></a>';
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
                            DyeingDataService.getDataByFullUrl('/api/dye/DCIssue/GetDCIssueInfoByReqID?pDYE_STR_REQ_H_ID=' + dataItem.DYE_STR_REQ_H_ID + '&pUSER_ID=' + cur_user_id).then(function (res) {

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
               {
                   field: "ISS_REF_NO", title: "Issue No", type: "string", width: "10%",
                   headerAttributes: {
                       style: "background-color: #239a84"
                   }
               },
                {
                    field: "ISS_REF_DT", title: "Issue Date", type: "date",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }, template: "#= kendo.toString(kendo.parseDate(ISS_REF_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%"
                },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                {
                    field: "COMP_NAME_EN", title: "Company", type: "string", width: "10%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                {
                    field: "REQ_TYPE_NAME", title: "Issue Type", type: "string", width: "10%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                {
                    field: "FROM_ST_NAME", title: "Store", type: "string", width: "10%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                {
                    field: "REMARKS", title: "Remarks", type: "string", width: "10%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                //{
                //    field: "EVENT_NAME", title: "Status", type: "string", width: "10%",                    
                //    headerAttributes: {
                //        style: "background-color: #239a84"
                //    }
                //},
                {
                    title: "Action",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    },
                    template: function () {
                        return "</a><a ui-sref='DCIssue({pDYE_DC_ISS_H_ID:dataItem.DYE_DC_ISS_H_ID})' ng-if='!dataItem.LAST_UPDATED_BY>0 && dataItem.PERMISSION==1' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a>";
                    },
                    width: "10%"
                },

                ]
            };
        };



    }

})();