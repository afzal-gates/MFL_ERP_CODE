

////////// Start List Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('McnNeedleReqListController', ['logger', 'config', '$q', '$scope', '$http', 'Hub', '$rootScope', 'exception', '$filter', '$state', '$stateParams', '$location', 'KnittingDataService', 'Dialog', 'cur_user_id', McnNeedleReqListController]);
    function McnNeedleReqListController(logger, config, $q, $scope, $http, Hub, $rootScope, exception, $filter, $state, $stateParams, $location, KnittingDataService, Dialog, cur_user_id) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        var hubUrl = "/signalr";
        var connection = $.hubConnection(hubUrl, { useDefaultPath: true });
        var hub = connection.createHubProxy("dashboard");
        var hubStart = connection.start();

        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.LIST_FROM = $stateParams.pLIST_FROM;

        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        var key = 'SCM_STR_OIL_REQ_H_ID';
        vm.today = new Date();
        vm.form = {};

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getMcOilReq4SubStrList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }

        var hub = new Hub('dashboard', {
            listeners: {
                'newConnection': function (id) {
                    vm.connected.push(id);
                    $rootScope.$apply();
                },
                'removeConnection': function (id) {
                    $rootScope.$apply();
                },
                'broadcastMcOilReq4SubStrList': function () {
                    getMcOilReq4SubStrList();
                    $rootScope.$apply()
                },

            },
            methods: [],
            errorHandler: function (error) {
                console.error(error);
            }
        });

        vm.connected = [];


        vm.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.STR_REQ_DT_LNopened = true;
        };


        vm.mcOilReq4SubStrListOptions = {
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
                { field: "SCM_STR_NDL_REQ_H_ID", hidden: true },
                { field: "STR_REQ_NO", title: "Requisition#", type: "string", width: "14%" },
                { field: "STR_REQ_DT", title: "Requisition Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "FROM_STORE_NAME_EN", title: "From", width: "15%" },
                { field: "TO_STORE_NAME_EN", title: "To", width: "15%" },
                { field: "REASON_FOR", title: "Reason", width: "10%" },                
                { field: "RQD_QTY", title: "Rqd.Qty", width: "8%" },
                { field: "ISS_QTY", title: "Iss.Qty", width: "8%" },
                { field: "MC_TYPE_NAME_EN", title: "M/C Type", width: "15%" },
                

                {
                    field: "EVENT_NAME", title: "Status", type: "string"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'
                    , width: "10%"
                },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="McnNeedleReq({pSCM_STR_NDL_REQ_H_ID:dataItem.SCM_STR_NDL_REQ_H_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ' && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a>' +
                        '<a ui-sref="McnNeedleReq({pSCM_STR_NDL_REQ_H_ID:dataItem.SCM_STR_NDL_REQ_H_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ') && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a></a>' +
                        ' <a class="btn btn-xs blue" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'RA'" + '" ng-click="vm.printRequisition(dataItem)"><i class="fa fa-file-text"> Print</i></a></a>';
                    },
                    width: "10%"
                }
                //{
                //    title: "Action",
                //    template: function () {
                //        return "<a type='button' class='btn btn-xs blue' ng-click='vm.editRequsition(dataItem)' ng-if='dataItem.RF_ACTN_STATUS_ID==44'><i class='fa fa-edit'></i></a>&nbsp;" +
                //            "<a type='button' class='btn btn-xs blue' ng-click='vm.editRequsition(dataItem)' ng-if='dataItem.RF_ACTN_STATUS_ID>44'>View</a>&nbsp;" +
                //            "<a type='button' class='btn btn-xs blue' ng-click='vm.submitRequsition(dataItem)' ng-if='dataItem.RF_ACTN_STATUS_ID==44'>Submit</a>&nbsp;" +
                //            //"<a type='button' class='btn btn-xs blue' ng-click='vm.verifyRequsition(dataItem)' ng-if='(dataItem.RF_ACTN_STATUS_ID==45 && dataItem.USER_VERIFIER_NAME==\"VERIFIER\")'>Verify</a>&nbsp;" +
                //            "<a type='button' class='btn btn-xs blue' ng-click='vm.issueRequsition(dataItem)' ng-if='(dataItem.RF_ACTN_STATUS_ID==45 && dataItem.USER_ISSUER_NAME==\"ISSUER\")'>Issue</a>&nbsp;";
                //    },
                //    width: "15%"
                //}
            ]
        };


        function getMcOilReq4SubStrList() {

            vm.mcOilReq4SubStrListDataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                schema: {
                    data: "data",
                    total: "total",
                    model: {
                        id: "SCM_STR_NDL_REQ_H_ID",
                        fields: {
                            STR_REQ_DT: { type: "date", editable: false }
                        }
                    }
                },
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/McnNeedleReq/SelectAll/';
                        url += '/' + params.page + '/' + params.pageSize;

                        console.log(url);

                        KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }



        vm.addNew = function () {
            return $state.go('McnNeedleReq', { pSCM_STR_NDL_REQ_H_ID: 0 });
        };

        vm.editRequsition = function (dataItem) {
            return $state.go('McnNeedleReq', { pSCM_STR_NDL_REQ_H_ID: dataItem.SCM_STR_NDL_REQ_H_ID });
        };

        vm.issueRequsition = function (dataItem) {
            return $state.go('McnNeedleIssue', { pSCM_STR_NDL_REQ_H_ID: dataItem.SCM_STR_NDL_REQ_H_ID });
        };
        
        vm.printRequisition = function (dataItem) {
            //console.log(dataItem);

            vm.isRDLC = true;
            vm.form.REPORT_CODE = 'RPT-3553';

            vm.form.SCM_STR_NDL_REQ_H_ID = dataItem.SCM_STR_NDL_REQ_H_ID;

            var url = '/api/Knit/KntReport/PreviewReportRDLC';

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
////////// End List Controller

