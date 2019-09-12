(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeMcMaintenanceListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$filter', DyeMcMaintenanceListController]);
    function DyeMcMaintenanceListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $filter) {
        var vm = this;
        vm.Title = $state.current.Title || '';

        var hubUrl = "/signalr";
        var connection = $.hubConnection(hubUrl, { useDefaultPath: true });
        var hub = connection.createHubProxy("dashboard");
        var hubStart = connection.start();

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [dyeMcMaintenanceList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm['OPTION'] = $state.current.pOPTION;

        console.log($state.current.pOPTION);

        var hub = new Hub('dashboard', {
            listeners: {
                'dyeMcMaintenanceList': function () {
                    dyeMcMaintenanceList();
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
                var _isudate = $filter('date')(vm.form.STR_REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                pm = pm + "&pSTR_REQ_DT=" + _isudate;
            }
            if (vm.form.COMP_NAME_EN)
                pm = pm + "&pCOMP_NAME_EN=" + vm.form.COMP_NAME_EN;
            if (vm.form.REQ_TYPE_NAME)
                pm = pm + "&pREQ_TYPE_NAME=" + vm.form.REQ_TYPE_NAME;

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        DyeingDataService.getDataByFullUrl('/api/dye/DyeMcMaintenance/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pRF_REQ_TYPE_ID=' + '&pUSER_ID=' + cur_user_id + '&pOption=3000').then(function (res) {
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

        function dyeMcMaintenanceList() {
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
                        DyeingDataService.getDataByFullUrl('/api/Dye/DyeMcMaintenance/SelectAll/' + params.page + '/' + params.pageSize).then(function (res) {
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
                { field: "DYE_MACHINE_NO", title: "Dyeing M/C", type: "string", width: "7%" },
                { field: "TRAN_DATE", title: "Date", type: "date", template: "#= kendo.toString(kendo.parseDate(TRAN_DATE,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "7%" },
                { field: "REPORT_TIME", title: "Report Time", type: "date", template: "#= kendo.toString(kendo.parseDate(REPORT_TIME),'dd-MMM-yyyy hh:mm tt') #", width: "12%" },
                { field: "PRB_ST_TIME", title: "Problem Time", type: "date", template: "#= kendo.toString(kendo.parseDate(PRB_ST_TIME),'dd-MMM-yyyy hh:mm tt') #", width: "12%" },
                { field: "PRB_END_TIME", title: "Resolved Time", type: "date", template: "#= kendo.toString(kendo.parseDate(PRB_END_TIME),'dd-MMM-yyyy hh:mm tt') #", width: "12%" },
                { field: "DFCT_TYPE_NAME_EN", title: "Defect Type", type: "string", width: "8%" },
                { field: "REPORT_BY_NAME", title: "Report By", type: "string", width: "8%" },
                { field: "MAINT_BY_NAME", title: "Service By", type: "string", width: "8%" },
                {
                    field: "EVENT_NAME", title: "Status", type: "string", width: "14%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'
                },
                {
                    title: "Action",
                    template: function () {
                        return "</a>" +
                            '<a ui-sref="DyeMcMaintenance({pDYE_MCN_STOP_TRAN_ID:dataItem.DYE_MCN_STOP_TRAN_ID,pOPTION:' + $state.current.pOPTION + '})" ng-if="dataItem.ACTN_ROLE_FLAG!=' + "'DN'" + ' && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a></a>';
                    },
                    width: "10%"
                }
            ]
        };



    }

})();