(function () {
    'use strict';
    angular.module('multitex.knitting').controller('McnNeedleIssueListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'cur_user_id', McnNeedleIssueListController]);
    function McnNeedleIssueListController($q, config, KnittingDataService, $stateParams, $state, $scope, cur_user_id) {

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

        vm.form = {};


        vm.printIssueChallan = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-3502';

            //vm.form.KNT_YRN_STR_REQ_H_ID = dataItem.KNT_YRN_STR_REQ_H_ID;
            vm.form.ISS_CHALAN_NO = dataItem.ISS_CHALAN_NO;

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

        vm.checkBlance = function (item) {

            //var qty = (100 - (((item.TTL_REQ_QTY - item.TTL_ISU_QTY) / item.TTL_REQ_QTY) * 100));

            //if (qty > 94 && item.CHK_VAL) {
            //    item.CHK_VAL = true;
            //}
            //else {
            //    item.CHK_VAL = false;
            //}
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

                        KnittingDataService.getDataByFullUrl('/api/knit/McnNeedleReq/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pOption=3002').then(function (res) {
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
                { field: "SCM_STR_NDL_REQ_H_ID", hidden: true },
                { field: "STR_REQ_NO", title: "Requisition#", type: "string", width: "14%" },
                { field: "STR_REQ_DT", title: "Requisition Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "FROM_STORE_NAME_EN", title: "From", width: "15%" },
                { field: "TO_STORE_NAME_EN", title: "To", width: "15%" },
                { field: "REASON_FOR", title: "Reason", width: "15%" },
                { field: "RQD_QTY", title: "Rqd.Qty", width: "8%" },
                { field: "ISS_QTY", title: "Iss.Qty", width: "8%" },

                {
                    field: "EVENT_NAME", title: "Status", type: "string"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'
                    , width: "10%"
                },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="McnNeedleIssue({pSCM_STR_NDL_REQ_H_ID:dataItem.SCM_STR_NDL_REQ_H_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ' && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a>' +
                        '<a ui-sref="McnNeedleIssue({pSCM_STR_NDL_REQ_H_ID:dataItem.SCM_STR_NDL_REQ_H_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ') && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a></a>' +
                        ' <a class="btn btn-xs blue" ng-click="vm.printRequisition(dataItem)"><i class="fa fa-file-text"> Print</i></a></a>';
                    },
                    width: "12%"
                },                
                {
                    title: "Close",
                    field: "CHK_VAL",
                    template: function () {
                        return '<input type="checkbox" ng-model="dataItem.CHK_VAL" ng-click="vm.checkBlance(dataItem)"></input>';
                    },
                    width: "5%"
                }
            ]
        };

        vm.detailGridOptionsForIssue = function (dataItem) {

            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/knit/McnNeedleReq/GetNeedleIssueByID?pSCM_STR_NDL_REQ_H_ID=' + dataItem.SCM_STR_NDL_REQ_H_ID).then(function (res) {
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
                { field: "SCM_STR_NDL_ISS_H_ID", hidden: true },
                { field: "SCM_STR_NDL_REQ_H_ID", hidden: true },
                { field: "IS_FINALIZED", hidden: true },
                { field: "ISSUE_BY", hidden: true },

                {
                    field: "ISS_REF_NO", title: "Issue No", type: "string", width: "15%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                {
                    field: "STR_ISS_DT", title: "Issue Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_ISS_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                    , width: "10%"
                },
                
                {
                    field: "FROM_STORE_NAME_EN", title: "Issuing Store", type: "string", width: "15%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                {
                    field: "TO_STORE_NAME_EN", title: "Receiving Store", type: "string", width: "15%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                {
                    field: "IS_FINALIZED", title: "Finalized", type: "string", width: "10%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                {
                    field: "IS_CLOSED", title: "Close", type: "string", width: "10%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="McnNeedleIssue({pSCM_STR_NDL_ISS_H_ID:dataItem.SCM_STR_NDL_ISS_H_ID})" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs blue"><i class="fa fa-edit"> Confirm</i></a>' +
                        ' <a ng-click="vm.printRequisition(dataItem)" class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a></a>';
                    },
                    width: "10%",
                    headerAttributes: {
                        style: "background-color: #239a84"
                    }
                }

                ]
            };
        };

        vm.submitAll = function (dataOri) {
            console.log(dataOri);
            var data = angular.copy(dataOri);
            var List = _.filter(vm.gridOptions.dataSource.data(), function (x) { return x.CHK_VAL == true; });
            if (List.length > 0) {
                data["XML_ISS_H"] = KnittingDataService.xmlStringShort(List.map(function (o) {
                    return {
                        SCM_STR_NDL_REQ_H_ID: o.SCM_STR_NDL_REQ_H_ID == null ? 0 : o.SCM_STR_NDL_REQ_H_ID
                    }
                }));

                return KnittingDataService.saveDataByUrl(data, '/McnNeedleReq/Close').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        vm.gridOptions.dataSource.read();
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                    }
                });
            }
        };






    }

})();