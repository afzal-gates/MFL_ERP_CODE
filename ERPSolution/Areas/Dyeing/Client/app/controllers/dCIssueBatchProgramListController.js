(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DCIssueBatchProgramListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$filter', DCIssueBatchProgramListController]);
    function DCIssueBatchProgramListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        var hubUrl = "/signalr";
        var connection = $.hubConnection(hubUrl, { useDefaultPath: true });
        var hub = connection.createHubProxy("dashboard");
        var hubStart = connection.start();

        vm.form = { REPORT_CODE: '' };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [batchIssueRequisitionList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_REQ_DT_LNopened = true;
        }

        $scope.FROM_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.FROM_DT_LNopened = true;
        }

        $scope.TO_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TO_DT_LNopened = true;
        }



        var hub = new Hub('dashboard', {
            listeners: {
                'batchIssueRequisitionList': function () {
                    batchIssueRequisitionList();
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


        vm.getMachineData = function (MC_BYR_ACC_ID) {
            vm.MachineDataSource = [];


            var dataSource = new kendo.data.DataSource({
                serverPaging: false,
                serverFiltering: false,
                schema: {
                    data: "data",
                    total: "total"
                },
                transport: {

                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/Dye/DCIssueRequisition/GetMachineInfoByTypeID?pDYE_MC_TYPE_ID=0'
                        MrcDataService.getDataByUrl(url).then(function (res) {
                            e.success(res);
                            vm.MachineDataSource = _.uniq(_.map(res.data, 'DYE_MACHINE_NO'));
                        }, function (err) {
                            console.log(err);
                        })
                    }
                },
                pageSize: 100,
                group: [{ field: 'DYE_MACHINE_NO' }]
            });

            $('#kendoGrid').data("kendoGrid").setDataSource(dataSource);

        }


        vm.gridOptionsMachine = {
            autoBind: true,
            dataSource: {
                serverPaging: false,
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetMachineInfoByTypeID?pDYE_MC_TYPE_ID=0').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.error(err);
                        });
                    }
                },
                //pageSize: 100,
                //group: [{ field: 'STYLE_NO' }],
                //dataBound: function (e) {
                //    collapseAllGroups(this);
                //}
            },
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
            selectable: false,
            //dataBound: function (e) {
            //    collapseAllGroups(this);
            //},
            sortable: true,
            pageable: false,
            columns: [
                { field: "DYE_MACHINE_ID", hidden: true },
                { field: "DYE_MACHINE_NO", title: "Machine No", type: "string", width: "100%" },
            ]
        };


        vm.searchData = function () {

            var pm = "?";
            if (vm.form.STR_REQ_NO)
                pm = pm + "pSTR_REQ_NO=" + vm.form.STR_REQ_NO + "&";
            if (vm.form.STR_REQ_DT) {
                var _isudate = $filter('date')(vm.form.STR_REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                pm = pm + "pSTR_REQ_DT=" + _isudate + "&";
            }
            if (vm.form.COMP_NAME_EN)
                pm = pm + "pCOMP_NAME_EN=" + vm.form.COMP_NAME_EN + "&";
            if (vm.form.REQ_TYPE_NAME)
                pm = pm + "pREQ_TYPE_NAME=" + vm.form.REQ_TYPE_NAME + "&";
            if (vm.form.DYE_BATCH_NO)
                pm = pm + "pDYE_BATCH_NO=" + vm.form.DYE_BATCH_NO + "&";
            if (vm.form.DYE_MACHINE_NO)
                pm = pm + "pDYE_MACHINE_NO=" + vm.form.DYE_MACHINE_NO + "&";

            console.log(pm);
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/SelectAll/' + (params.page || 1) + '/' + (params.pageSize || 10) + pm + 'pRF_REQ_TYPE_ID=7&pUSER_ID=' + cur_user_id + '&pOption=3004').then(function (res) {
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


        function batchIssueRequisitionList() {
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

                        DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pRF_REQ_TYPE_ID=7&pUSER_ID=' + cur_user_id + '&pOption=3004').then(function (res) {
                            //DyeingDataService.getDataByFullUrl('/api/dye/DCIssueRequisition/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pRF_REQ_TYPE_ID=7&pUSER_ID=' + cur_user_id).then(function (res) {
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

                        DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pRF_REQ_TYPE_ID=7&pUSER_ID=' + cur_user_id + '&pOption=3004').then(function (res) {
                            //DyeingDataService.getDataByFullUrl('/api/dye/DCIssueRequisition/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pRF_REQ_TYPE_ID=7&pUSER_ID=' + cur_user_id).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            }),
            scrollable: {
                virtual: true,
            },
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
            groupbale: true,
            sortable: true,
            columns: [
                { field: "STR_REQ_NO", title: "REQ No", type: "string", width: "10%" },
                { field: "STR_REQ_DT", title: "Req Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "10%" },
                //{ field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "10%" },
                { field: "REQ_TYPE_NAME", title: "Process Type", type: "string", width: "10%" },
                //{ field: "FROM_ST_NAME", title: "Req. Store", type: "string", width: "10%" },
                { field: "DYE_BATCH_NO", title: "Batch No", type: "string", width: "10%" },
                { field: "DYE_MACHINE_NO", title: "M/C No", type: "string", width: "10%" },
                {
                    field: "EVENT_NAME", title: "Status", type: "string", width: "10%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'
                },

                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ') && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a> ' +
                            '<a ui-sref="DCIssueBatchProgram({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pDYE_BT_DC_ISS_H_ID:dataItem.DYE_BT_DC_ISS_H_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ' && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a> ' +
                            //'<a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:2})" ng-if="(dataItem.RF_ACTN_STATUS_ID>=5 || dataItem.BALANCE_QTY>0) && !dataItem.IS_ADDITION>0 && !dataItem.UN_LOAD_TIME" class="btn btn-xs blue"><i class="fa fa-edit"> Addition</i></a> ' +
                            ' <a ui-sref="DCBatchProgramAddRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:2})" ng-if="dataItem.TTL_ISS_QTY>0 && dataItem.IS_REQ_DONE==0 && dataItem.DYE_RE_PROC_TYPE_ID!=2" class="btn btn-xs blue"><i class="fa fa-edit"> Addition</i></a>' +
                            ' <a ng-click="vm.printBatchCard(dataItem)" class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a> </a>' +
                            ' <a ng-click="vm.printBatchCardAddition(dataItem)" class="btn btn-xs blue"><i class="fa fa-print"> Print Full</i></a> </a>';
                    },
                    width: "20%"
                }
            ]
        };


        vm.detailGridOptionsForReq = function (dataItem) {
            var pm = "";
            if (vm.form.STR_REQ_NO)
                pm = pm + "&pSTR_REQ_NO=" + vm.form.STR_REQ_NO;
            if (vm.form.STR_REQ_DT) {
                var _isudate = $filter('date')((vm.form.STR_REQ_DT || new Date()), 'yyyy-MM-ddTHH:mm:ss');
                pm = pm + "&pSTR_REQ_DT=" + _isudate;
            }
            else {
                var _isudate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                pm = pm + "&pSTR_REQ_DT=" + _isudate;
            }

            if (vm.form.COLOR_NAME_EN) {
                var _todate = $filter('date')((vm.form.COLOR_NAME_EN || new Date()), 'yyyy-MM-ddTHH:mm:ss');
                pm = pm + "&pCOLOR_NAME_EN=" + _todate;
            }
            else {
                var _todate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                pm = pm + "&pCOLOR_NAME_EN=" + _todate;
            }
            if (vm.form.REQ_TYPE_NAME)
                pm = pm + "&pREQ_TYPE_NAME=" + vm.form.REQ_TYPE_NAME;
            if (vm.form.DYE_BATCH_NO)
                pm = pm + "&pDYE_BATCH_NO=" + vm.form.DYE_BATCH_NO;
            if (dataItem.DYE_MACHINE_NO)
                pm = pm + "&pDYE_MACHINE_NO=" + dataItem.DYE_MACHINE_NO;

            return {
                dataSource: {
                    serverPaging: true,
                    serverSorting: true,
                    serverFiltering: true,
                    pageSize: 10,
                    transport: {
                        read: function (e) {
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);
                            DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/SelectAll/' + params.page + '/' + params.pageSize + '?pRF_REQ_TYPE_ID=7&pUSER_ID=' + cur_user_id + '&pOption=3004' + pm).then(function (res) {
                                e.success(res);
                            });
                        }
                    },
                    schema: {
                        data: "data",
                        total: 'total'
                    }
                },
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
                groupbale: true,
                sortable: true,
                columns: [
                { field: "STR_REQ_NO", title: "REQ No", type: "string", width: "10%" },
                { field: "STR_REQ_DT", title: "Req Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "10%" },
                //{ field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "10%" },
                { field: "REQ_TYPE_NAME", title: "Process Type", type: "string", width: "10%" },
                //{ field: "FROM_ST_NAME", title: "Req. Store", type: "string", width: "10%" },
                { field: "DYE_BATCH_NO", title: "Batch No", type: "string", width: "10%" },
                { field: "DYE_MACHINE_NO", title: "M/C No", type: "string", width: "10%" },
                {
                    field: "EVENT_NAME", title: "Status", type: "string", width: "10%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'
                },

                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ') && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a> ' +
                            '<a ui-sref="DCIssueBatchProgram({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pDYE_BT_DC_ISS_H_ID:dataItem.DYE_BT_DC_ISS_H_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ' && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a> ' +
                            '<a ng-click="vm.printBatchCard(dataItem)" class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a> </a>' +
                            '<a ng-click="vm.printBatchCardAddition(dataItem)" ng-if="dataItem.HAS_ADDITION>=1" class="btn btn-xs blue"><i class="fa fa-print"> Print Full</i></a> </a>';
                    },
                    width: "20%"
                }
                ]
            };
        };


        vm.detailGridOptionsForIssue = function (dataItem) {

            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueBatchProgram/GetDCBatchProgramIssueInfo?pDYE_STR_REQ_H_ID=' + dataItem.DYE_STR_REQ_H_ID).then(function (res) {

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
                    field: "LOGIN_ID", title: "Issue By", type: "string", width: "10%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                //{
                //    field: "STORE_NAME_EN", title: "Store", type: "string", width: "10%",
                //    headerAttributes: {
                //        style: "background-color: #239a84"
                //    }
                //},
                {
                    field: "REMARKS", title: "Remarks", type: "string", width: "10%",
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
                        return '</a><a ui-sref="DCIssueBatchProgram({pDYE_BT_DC_ISS_H_ID:dataItem.DYE_BT_DC_ISS_H_ID})" ng-if="dataItem.RF_ACTN_STATUS_ID>=5 && dataItem.BALANCE_QTY>0" class="btn btn-xs blue"><i class="fa fa-edit"> Issue</i></a> ' +
                                   '<a ui-sref="DCIssueBatchProgram({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID, pDYE_BT_DC_ISS_H_ID:dataItem.DYE_BT_DC_ISS_H_ID})" ng-if="9==' + cur_user_id + '" class="btn btn-xs red"><i class="fa fa-edit"> Delete</i></a> </a>';
                            
                    },
                    width: "10%"
                },

                ]
            };
        };

        vm.printBatchCardAddition = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-4002';

            vm.form.REQ_TYPE_NAME = dataItem.REQ_TYPE_NAME;

            vm.form.DYE_STR_REQ_H_ID = dataItem.DYE_STR_REQ_H_ID;
            //vm.form.KNT_SC_PRG_ISS_ID = dataItem.KNT_SC_PRG_ISS_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Dye/DyeReport/PreviewReportRDLC');
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


        vm.printBatchCard = function (dataItem) {
            console.log(dataItem);
            //if (dataItem.DYE_RE_PROC_TYPE_ID == 2)
            //    vm.form.REPORT_CODE = 'RPT-4001';
            //else
            //    vm.form.REPORT_CODE = 'RPT-4002';
            vm.form.REPORT_CODE = 'RPT-4015';

            vm.form.REQ_TYPE_NAME = dataItem.REQ_TYPE_NAME;

            vm.form.DYE_STR_REQ_H_ID = dataItem.DYE_STR_REQ_H_ID;
            //vm.form.KNT_SC_PRG_ISS_ID = dataItem.KNT_SC_PRG_ISS_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Dye/DyeReport/PreviewReportRDLC');
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


    }

})();