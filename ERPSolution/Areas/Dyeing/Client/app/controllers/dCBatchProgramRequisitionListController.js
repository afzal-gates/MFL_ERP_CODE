(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DCBatchProgramRequisitionListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$filter', 'Dialog', DCBatchProgramRequisitionListController]);
    function DCBatchProgramRequisitionListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $filter, Dialog) {

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
            var promise = [batchRequisitionList()];
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
                'batchRequisitionList': function () {
                    batchRequisitionList();
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
            if (vm.form.COLOR_NAME_EN)
                pm = pm + "&pCOLOR_NAME_EN=" + vm.form.COLOR_NAME_EN;
            if (vm.form.REQ_TYPE_NAME)
                pm = pm + "&pREQ_TYPE_NAME=" + vm.form.REQ_TYPE_NAME;
            if (vm.form.DYE_BATCH_NO)
                pm = pm + "&pDYE_BATCH_NO=" + vm.form.DYE_BATCH_NO;
            if (vm.form.DYE_MACHINE_NO)
                pm = pm + "&pDYE_MACHINE_NO=" + vm.form.DYE_MACHINE_NO;

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
                        DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pRF_REQ_TYPE_ID=7&pUSER_ID=' + cur_user_id).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total',
                    model: {
                        fields: {
                            DYE_STR_REQ_H_ID: { type: "string" },
                            STR_REQ_NO: { type: "string" },
                            STR_REQ_DT: { type: "date" },
                            COMP_NAME_EN: { type: "string" },
                            REQ_TYPE_NAME: { type: "string" },
                            //FROM_ST_NAME: { type: "string" },
                            DYE_BATCH_NO: { type: "string" },
                            DYE_MACHINE_NO: { type: "string" },
                            EVENT_NAME: { type: "string" }
                        }
                    }
                }
            })
        }


        function batchRequisitionList() {
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
                        e.success([]);
                        //DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pRF_REQ_TYPE_ID=7&pUSER_ID=' + cur_user_id).then(function (res) {
                        //    e.success(res);
                        //});
                    }
                },
                schema: {
                    data: "data",
                    total: 'total',
                    model: {
                        fields: {
                            DYE_STR_REQ_H_ID: { type: "string" },
                            STR_REQ_NO: { type: "string" },
                            STR_REQ_DT: { type: "date" },
                            COMP_NAME_EN: { type: "string" },
                            REQ_TYPE_NAME: { type: "string" },
                            //FROM_ST_NAME: { type: "string" },
                            DYE_BATCH_NO: { type: "string" },
                            DYE_MACHINE_NO: { type: "string" },

                            //TTL_RQD: { type: "decimal" },
                            //TTL_ISS_QTY: { type: "decimal" },
                            //BALANCE_QTY: { type: "decimal" },
                            //REMARKS: { type: "string" },
                            EVENT_NAME: { type: "string" }
                        }
                    }
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
            sortable: true,
            scrollable: true,
            height: 600,
            columns: [
                { field: "DYE_STR_REQ_H_ID", hidden: true },
                { field: "STR_REQ_NO", title: "REQ No", type: "string", width: "10%" },
                { field: "STR_REQ_DT", title: "Req Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "10%" },
                //{ field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "10%" },
                { field: "REQ_TYPE_NAME", title: "Process Type", type: "string", width: "10%" },
                //{ field: "FROM_ST_NAME", title: "Req. Store", type: "string", width: "10%" },
                { field: "DYE_BATCH_NO", title: "Batch No", type: "string", width: "10%" },
                { field: "DYE_MACHINE_NO", title: "M/C No", type: "string", width: "10%" },
                //{ field: "REMARKS", title: "Remarks", type: "string", width: "5%" },
                {
                    field: "EVENT_NAME", title: "Status", type: "string", width: "10%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'
                },

                {
                    title: "Action",
                    template: function () {
                        return '<ul kendo-menu k-orientation="menuOrientation" k-rebind="menuOrientation" k-on-select="onSelect(kendoEvent)">' +
                               '<li><span style="color:black;">Navigate</span>' +
                                   '<ul style="width:150px;">' +
                                       '<li ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ') && dataItem.PERMISSION==1" style="padding:5px;"><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" style="color:black" ><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a></li>' +

                                        //'<li ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ' || dataItem.BALANCE_QTY>0) && !dataItem.IS_ADDITION>0 && !dataItem.UN_LOAD_TIME" style="padding:5px;"><a ui-sref="DCBatchProgramAddRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:2})" style="color:black"><i class="fa fa-edit"> Addition</i></a> </li>' +
                                        //'<li ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ' || dataItem.BALANCE_QTY>0) && dataItem.IS_ADDITION>0 && !dataItem.UN_LOAD_TIME" style="padding:5px;"><a ui-sref="DCBatchProgramAddRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" style="color:black"><i class="fa fa-edit"> Addition Edit</i></a> </li>' +
                                        '<li ng-if="dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ' && dataItem.UN_LOAD_TIME && !dataItem.IS_REPROCESS>0" style="padding:5px;"><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:3})" style="color:black"><i class="fa fa-edit"> Re-Match</i></a> </li>' +
                                        '<li ng-if="dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ' && dataItem.UN_LOAD_TIME && !dataItem.IS_REPROCESS>0" style="padding:5px;"><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:4})" style="color:black"><i class="fa fa-edit" > Re-Wash</i></a> </li>' +
                                        '<li ng-if="dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ' && dataItem.UN_LOAD_TIME && !dataItem.IS_REPROCESS>0" style="padding:5px;"><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:5})"  style="color:black"><i class="fa fa-edit"> Re-Dyeing</i></a> </li>' +
                                        '<li ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ') && 9==' + cur_user_id + '" style="padding:5px;"><a  ng-click="vm.deleteBatchCard(dataItem)" style="color:red" ><i class="fa fa-remove">Delete</i></a></li>' +
                                        '<li ng-if="dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ' && 9==' + cur_user_id + '" style="padding:5px;"><a  ng-click="vm.deleteIssue(dataItem)" style="color:red" ><i class="fa fa-remove">Issue Delete</i></a></li>' +
                                       '<li ng-if="9==' + cur_user_id + '" style="padding:5px;"><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" style="color:black" ><i class="fa fa-edit"> Edit</i></a></li>' +

                                   '</ul>' +
                               '</li></ul> ';
                    },
                    width: "10%"
                },
                {
                    title: "Print",
                    template: function () {
                        //return '<a ng-click="vm.printBatchCard(dataItem)" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'DN'" + '" class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a> </a> <a ng-click="vm.printBatchCardAddition(dataItem)" ng-if="dataItem.HAS_ADDITION>=1" class="btn btn-xs blue"><i class="fa fa-print"> Print Full</i></a> </a>';
                        return '<a ng-click="vm.printBatchCard(dataItem)" class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a> </a> <a ng-click="vm.printBatchCardAddition(dataItem)" ng-if="dataItem.HAS_ADDITION>=1" class="btn btn-xs blue"><i class="fa fa-print"> Print Full</i></a> <a ng-click="vm.printBatchCardExcel(dataItem)" class="btn btn-xs blue"><i class="fa fa-print"> Excel</i></a></a>';
                    },
                    width: "10%"
                }
            ]
        };

        vm.detailGridOptions = function (dataItem) {

            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/SelectAllBatchDtl/1/20?&pDYE_STR_REQ_H_ID=' + dataItem.DYE_STR_REQ_H_ID + '&pRF_REQ_TYPE_ID=7&pUSER_ID=' + cur_user_id).then(function (res) {

                                e.success(res);
                            });
                        }
                    },

                    schema: {
                        model: {
                            fields: {
                                DYE_STR_REQ_H_ID: { type: "string" },
                                STR_REQ_NO: { type: "string" },
                                STR_REQ_DT: { type: "date" },
                                COMP_NAME_EN: { type: "string" },
                                REQ_TYPE_NAME: { type: "string" },
                                //FROM_ST_NAME: { type: "string" },
                                DYE_BATCH_NO: { type: "string" },
                                DYE_MACHINE_NO: { type: "string" },

                                //REMARKS: { type: "string" },
                                EVENT_NAME: { type: "string" }
                            }
                        }
                    }
                },
                scrollable: false,
                sortable: true,
                pageable: false,
                //groupable: true,
                columns: [
                    { field: "DYE_STR_REQ_H_ID", hidden: true },
                    { field: "STR_REQ_NO", title: "REQ No", type: "string", width: "15%" },
                    { field: "STR_REQ_DT", title: "Req Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                    //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                    { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "10%" },
                    //{ field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "10%" },
                    { field: "REQ_TYPE_NAME", title: "Process Type", type: "string", width: "10%" },
                    //{ field: "FROM_ST_NAME", title: "From Store", type: "string", width: "10%" },
                    { field: "DYE_BATCH_NO", title: "Batch No", type: "string", width: "10%" },
                    { field: "DYE_MACHINE_NO", title: "M/C No", type: "string", width: "10%" },
                    //{ field: "REMARKS", title: "Remarks", type: "string", width: "5%" },
                    { field: "EVENT_NAME", title: "Status", type: "string", width: "10%" },
                    {
                        title: "Action",
                        template: function () {
                            return '<ul kendo-menu k-orientation="menuOrientation" k-rebind="menuOrientation" k-on-select="onSelect(kendoEvent)">' +
                                   '<li><span style="color:black;">Navigate</span>' +
                                       '<ul style="width:150px;">' +
                                           '<li ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ') && dataItem.PERMISSION==1" style="padding:5px;"><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" ng-if="dataItem.DYE_RE_PROC_TYPE_ID!=2" style="color:black" ><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a> <a ui-sref="DCBatchProgramAddRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" ng-if="dataItem.DYE_RE_PROC_TYPE_ID==2" style="color:black" ><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a> </li>' +
                                            '<a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" ng-if="dataItem.DYE_RE_PROC_TYPE_ID>2 && dataItem.IS_FINISHED==0 && 9==' + cur_user_id + '" style="color:black" ><i class="fa fa-edit"> Update</i></a> </li>' +

                                            //'<li ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'DN'" + '|| dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ')' + ' && !dataItem.IS_ADDITION>0 && dataItem.DYE_RE_PROC_TYPE_ID!=2 &&!dataItem.UN_LOAD_TIME" style="padding:5px;"><a ui-sref="DCBatchProgramAddRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:2})" style="color:black"><i class="fa fa-edit"> Addition</i></a> </li>' +
                                            '<li ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ') && dataItem.PERMISSION==1" style="padding:5px;"><a  ng-click="vm.deleteBatchCard(dataItem)" style="color:red" ><i class="fa fa-remove">Delete</i></a></li>' +
                                            '<li ng-if="dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ' && 9==' + cur_user_id + '" style="padding:5px;"><a  ng-click="vm.deleteIssue(dataItem)" style="color:red" ><i class="fa fa-remove">Issue Delete</i></a></li>' +
                                            '<li ng-if="9==' + cur_user_id + '" style="padding:5px;"><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" style="color:black" ><i class="fa fa-edit"> Edit</i></a></li>' +

                                       '</ul>' +
                                   '</li></ul> ';
                        },
                        width: "10%"
                    },
                    {
                        title: "Print",
                        template: function () {
                            return '<a ng-click="vm.printBatchCard(dataItem)" class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a> </a> <a ng-click="vm.printBatchCardAddition(dataItem)" ng-if="dataItem.HAS_ADDITION>=1" class="btn btn-xs blue"><i class="fa fa-print"> Print Full</i></a> </a>';
                        },
                        width: "5%"
                    },
                    //{
                    //    title: "Action",
                    //    template: function () {
                    //        return "</a><a ui-sref='DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})' ng-if='dataItem.RF_ACTN_STATUS_ID<=4' class='btn btn-xs blue'><i class='fa fa-edit'> {{dataItem.NXT_ACTION_NAME}}</i></a> " +
                    //        "<a ui-sref='DCIssueBatchProgram({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})' ng-if='dataItem.RF_ACTN_STATUS_ID>=5 && dataItem.BALANCE_QTY>0' class='btn btn-xs blue'><i class='fa fa-edit'> Issue</i></a> " +
                    //        "<a ui-sref='DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:2})' ng-if='dataItem.RF_ACTN_STATUS_ID==6 && !dataItem.IS_ADDITION>0 && dataItem.DYE_RE_PROC_TYPE_ID>2' class='btn btn-xs blue'><i class='fa fa-edit'> Addition</i></a> " +
                    //        //"<a ui-sref='DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:2})' ng-if='dataItem.RF_ACTN_STATUS_ID>5 && !dataItem.DYE_RE_PROC_TYPE_ID==2' class='btn btn-xs blue'><i class='fa fa-edit'> Addition</i></a> " +
                    //        "<a ng-click='vm.printBatchCard(dataItem)' class='btn btn-xs blue'><i class='fa fa-print'> Print</i></a> </a>" +
                    //        "<a ng-click='vm.printBatchCardAddition(dataItem)' ng-if='dataItem.HAS_ADDITION>=1' class='btn btn-xs blue'><i class='fa fa-print'> Print Full</i></a> </a>";
                    //    },
                    //    width: "10%"
                    //}

                ]
            };
        };

        vm.deleteBatchCard = function (dataItem) {

            var data = angular.copy(dataItem);

            Dialog.confirm('Removing "' + data.STR_REQ_NO + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.showSplash = true;

                     return DyeingDataService.saveDataByUrl(data, '/DCBatchProgramRequisition/Delete').then(function (res) {

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

                     return DyeingDataService.saveDataByUrl(data, '/DCBatchProgramRequisition/IssueDelete').then(function (res) {

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
            vm.form.REPORT_CODE = 'RPT-4001';

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


        vm.printBatchCardExcel = function (dataItem) {
            console.log(dataItem);
            //if (dataItem.DYE_RE_PROC_TYPE_ID == 2)
            //    vm.form.REPORT_CODE = 'RPT-4001';
            //else
            //    vm.form.REPORT_CODE = 'RPT-4002';
            vm.form.REPORT_CODE = 'RPT-4001';

            vm.form.REQ_TYPE_NAME = dataItem.REQ_TYPE_NAME;
            vm.form["IS_EXCEL_FORMAT"] = "Y";
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