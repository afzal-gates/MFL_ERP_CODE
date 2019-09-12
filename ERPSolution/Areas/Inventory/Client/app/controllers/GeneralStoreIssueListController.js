(function () {
    'use strict';

    angular.module('multitex.inventory').controller('GeneralStoreIssueListController', ['$q', 'config', 'InventoryDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$filter', GeneralStoreIssueListController]);
    function GeneralStoreIssueListController($q, config, InventoryDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $filter) {
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

        $scope.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_REQ_DT_LNopened = true;
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

        vm.searchData = function () {

            var pm = "";
            if (vm.form.STR_REQ_NO)
                pm = pm + "&pSTR_REQ_NO=" + vm.form.STR_REQ_NO;
            if (vm.form.STR_REQ_DT) {
                var _isudate = $filter('date')(vm.form.STR_REQ_DT, 'dd-MMM-yyyy');
                pm = pm + "&pSTR_REQ_DT=" + _isudate;
            }
            if (vm.form.COMP_NAME_EN)
                pm = pm + "&pCOMP_NAME_EN=" + vm.form.COMP_NAME_EN;
            if (vm.form.REQ_TYPE_NAME)
                pm = pm + "&pREQ_TYPE_NAME=" + vm.form.REQ_TYPE_NAME;
            //if (vm.form.DYE_MACHINE_NO)
            //    pm = pm + "&pDYE_MACHINE_NO=" + vm.form.DYE_MACHINE_NO;

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        //var pm = InventoryDataService.kFilterStr2QueryParam(params.filter);
                        InventoryDataService.getDataByFullUrl('/api/inv/GenStrReq/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
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
                        var pm = InventoryDataService.kFilterStr2QueryParam(params.filter);
                        InventoryDataService.getDataByFullUrl('/api/inv/GenStrReq/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
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
                { field: "STR_REQ_NO", title: "REQ No", type: "string", width: "10%" },
                { field: "STR_REQ_DT", title: "Req Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
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
                    field: "EVENT_NAME", title: "Status", type: "string", width: "10%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'
                },

                {
                    title: "Action",
                    template: function () {
                        return "</a>"+
                            '<a ui-sref="ScmGenStrIssue({pSCM_STR_GEN_REQ_H_ID:dataItem.SCM_STR_GEN_REQ_H_ID})" ng-if="dataItem.APROVER_LVL_NO==4 && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a></a>' +
                            '<a class="btn btn-xs blue" ng-click="vm.printSlip(dataItem);"><i class="fa fa-print"> Print</i></a>';
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
                            InventoryDataService.getDataByFullUrl('/api/Inv/GenStrReq/GetGenStrIssueInfoByReqID?pSCM_STR_GEN_REQ_H_ID=' + dataItem.SCM_STR_GEN_REQ_H_ID).then(function (res) {

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
                {
                    field: "EVENT_NAME    ", title: "Status", type: "string", width: "10%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                {
                    title: "Action",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    },
                    template: function () {
                        return '</a><a ui-sref="ScmGenStrIssue({pSCM_STR_GEN_ISS_H_ID:dataItem.SCM_STR_GEN_ISS_H_ID})" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '"  class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a>'+
                            '<a class="btn btn-xs blue" ng-click="vm.printSlip(dataItem);"><i class="fa fa-print"> Print</i></a>';
                    },
                    width: "10%"
                },

                ]
            };
        };


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