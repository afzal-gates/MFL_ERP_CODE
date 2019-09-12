(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('MachineWashRequisitionListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$filter', 'Dialog', MachineWashRequisitionListController]);
    function MachineWashRequisitionListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $filter, Dialog) {

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
                var _isudate = $filter('date')(vm.form.STR_REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                pm = pm + "&pSTR_REQ_DT=" + _isudate;
            }
            if (vm.form.COMP_NAME_EN)
                pm = pm + "&pCOMP_NAME_EN=" + vm.form.COMP_NAME_EN;
            if (vm.form.REQ_TYPE_NAME)
                pm = pm + "&pREQ_TYPE_NAME=" + vm.form.REQ_TYPE_NAME;
            if (vm.form.DYE_MACHINE_NO)
                pm = pm + "&pDYE_MACHINE_NO=" + vm.form.DYE_MACHINE_NO;

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        //var pm = DyeingDataService.kFilterStr2QueryParam(params.filter);
                        DyeingDataService.getDataByFullUrl('/api/dye/DCIssueRequisition/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pRF_REQ_TYPE_ID=8' + '&pUSER_ID=' + cur_user_id).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total',
                    model: {
                        fields: {
                            STR_REQ_NO: { type: "string" },
                            STR_REQ_DT: { type: "date" },
                            COMP_NAME_EN: { type: "string" },
                            REQ_TYPE_NAME: { type: "string" },
                            FROM_ST_NAME: { type: "string" },
                            DYE_MACHINE_NO: { type: "string" },

                            TTL_RQD: { type: "decimal" },
                            TTL_ISS_QTY: { type: "decimal" },
                            BALANCE_QTY: { type: "decimal" },
                            REMARKS: { type: "string" },
                            EVENT_NAME: { type: "string" }
                        }
                    }
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
                        var pm = DyeingDataService.kFilterStr2QueryParam(params.filter);
                        DyeingDataService.getDataByFullUrl('/api/dye/DCIssueRequisition/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pRF_REQ_TYPE_ID=8&pUSER_ID=' + cur_user_id).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total',
                    model: {
                        fields: {
                            STR_REQ_NO: { type: "string" },
                            STR_REQ_DT: { type: "date" },
                            COMP_NAME_EN: { type: "string" },
                            REQ_TYPE_NAME: { type: "string" },
                            FROM_ST_NAME: { type: "string" },
                            DYE_MACHINE_NO: { type: "string" },

                            TTL_RQD: { type: "decimal" },
                            TTL_ISS_QTY: { type: "decimal" },
                            BALANCE_QTY: { type: "decimal" },
                            REMARKS: { type: "string" },
                            EVENT_NAME: { type: "string" }
                        }
                    }
                }
            })
        }


        vm.printRDLC = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-4010';
            vm.form.DYE_STR_REQ_H_ID = dataItem.DYE_STR_REQ_H_ID;

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

        vm.deleteReq = function (dataItem) {

            var data = angular.copy(dataItem);

            Dialog.confirm('Are you want to delete all issue of "' + data.STR_REQ_NO + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.showSplash = true;

                     return DyeingDataService.saveDataByUrl(data, '/DCIssueRequisition/DeleteGenRequisition').then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.jsonStr);

                             config.appToastMsg(res.data.PMSG);
                             $state.go($state.current, { reload: true });
                         }
                         vm.showSplash = false;
                     });
                 });
        }

        vm.deleteIssue = function (dataItem) {

            var data = angular.copy(dataItem);

            Dialog.confirm('Are you want to delete all issue of "' + data.STR_REQ_NO + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.showSplash = true;

                     return DyeingDataService.saveDataByUrl(data, '/DCIssueRequisition/DeleteGenReqIssue').then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.jsonStr);

                             config.appToastMsg(res.data.PMSG);
                             $state.go($state.current, { reload: true });
                         }
                         vm.showSplash = false;
                     });
                 });
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
            sortable: true,
            columns: [
                { field: "STR_REQ_NO", title: "REQ No", type: "string", width: "10%" },
                { field: "STR_REQ_DT", title: "Req Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%", filter: true },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "10%" },
                //{ field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "10%" },
                { field: "REQ_TYPE_NAME", title: "Req. Type", type: "string", width: "10%" },
                { field: "FROM_ST_NAME", title: "From Store", type: "string", width: "10%" },
                { field: "DYE_MACHINE_NO", title: "M/C No", type: "string", width: "10%" },

                { field: "TTL_RQD", title: "Required Qty", type: "string", width: "10%" },
                { field: "TTL_ISS_QTY", title: "Issued Qty", type: "string", width: "10%" },
                { field: "BALANCE_QTY", title: "Balance Qty", type: "string", width: "10%" },

                { field: "REMARKS", title: "Remarks", type: "string", width: "10%" },
                {
                    field: "EVENT_NAME", title: "Status", type: "string", width: "10%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SR'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'

                },

                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="MachineWashRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ') && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a>' +
                            ' <a ui-sref="DCIssue({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ' && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a>' +
                            ' <a ui-sref="MachineWashRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ' && (dataItem.LOAD_TIME>=' + "'2017-01-01'" + ' || dataItem.UN_LOAD_TIME>=' + "'2017-01-01'" + ')" class="btn btn-xs blue"><i class="fa fa-edit"> Complete</i></a>' +
                            '<a  class="btn btn-xs red" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ' && 9==' + cur_user_id + '" ng-click="vm.deleteIssue(dataItem)"><i class="fa fa-remove">Issue Delete</i></a>' +
                            '<a  class="btn btn-xs red" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + '|| dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ') && 9==' + cur_user_id + '" ng-click="vm.deleteReq(dataItem)"><i class="fa fa-remove">Delete</i></a>' +
                            ' <a ng-click="vm.printRDLC(dataItem);" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + '" class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a></a>';
                    },
                    width: "15%"
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
                        return "</a><a ui-sref='DCIssue({pDYE_DC_ISS_H_ID:dataItem.DYE_DC_ISS_H_ID})' ng-if='!dataItem.LAST_UPDATED_BY>0 && dataItem.PERMISSION==1' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a>";
                    },
                    width: "10%"
                },

                ]
            };
        };



    }

})();