(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnIssueListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'cur_user_id', '$sessionStorage', '$filter', YarnIssueListController]);
    function YarnIssueListController($q, config, KnittingDataService, $stateParams, $state, $scope, cur_user_id, $sessionStorage, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.loading = 0;

        vm.form = $sessionStorage.SessionMessage ? $sessionStorage.SessionMessage : { PAGE_NO: 1 };

        activate()
        vm.showSplash = true;
        
        if (!vm.form) {
            vm.form["PAGE_NO"] = 1;
        }

        function activate() {

            var promise = [dailyIssueList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        $scope.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_REQ_DT_LNopened = true;
        }


        
        vm.searchData = function () {
            vm.showSplash = true;
            vm.loading = 0;
            var data = {
                STR_REQ_NO: vm.form.STR_REQ_NO,
                STR_REQ_DT: vm.form.STR_REQ_DT,
                REQ_TYPE_NAME: vm.form.REQ_TYPE_NAME,
                //PAY_MTHD_NAME: vm.form.PAY_MTHD_NAME,
                //LOC_SRC_TYPE_NAME: vm.form.LOC_SRC_TYPE_NAME,
                //CHALAN_NO: vm.form.CHALAN_NO,
                //IMP_LC_NO: vm.form.IMP_LC_NO,
                PAGE_NO: 1
            };
            vm.form.PAGE_NO = 1;
            $sessionStorage.SessionMessage = angular.copy(data);

            dailyIssueList();
        }

        vm.printIssueChallan = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-3502';

         ///   vm.form.IS_EXCEL_FORMAT = 'Y';

            //vm.form.KNT_YRN_STR_REQ_H_ID = dataItem.KNT_YRN_STR_REQ_H_ID;
            vm.form.IS_YD = "N";
            vm.form.ISS_CHALAN_NO = dataItem.ISS_CHALAN_NO;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReportRDLC');
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
            vm.form.REPORT_CODE = 'RPT-3513';

            vm.form.KNT_YRN_STR_REQ_H_ID = dataItem.KNT_YRN_STR_REQ_H_ID;
            vm.form.KNT_YRN_ISS_H_ID = dataItem.KNT_YRN_ISS_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReportRDLC');
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

        vm.checkBlance = function (item) {

            var qty = (100 - (((item.TTL_REQ_QTY - item.TTL_ISU_QTY) / item.TTL_REQ_QTY) * 100));

            if (qty > 94 && item.CHK_VAL) {
                item.CHK_VAL = true;
            }
            else {
                item.CHK_VAL = false;
            }
        }


        function dailyIssueList() {

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                page: (vm.form.PAGE_NO || page),
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var data = angular.copy($sessionStorage.SessionMessage);

                        if (pm.length > 0) {
                            KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id + '&pOption=3005').then(function (res) {
                                e.success(res);
                            });
                        }
                        else {
                            if (vm.form.STR_REQ_DT) {
                                var _mrrDate = $filter('date')(vm.form.STR_REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                                vm.form.STR_REQ_DT = _mrrDate;
                            }
                            KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/SelectAll/' + (vm.loading == 0 ? (data.PAGE_NO || 1) : params.page) + '/' + params.pageSize + '?pOption=3005&pUSER_ID=' + cur_user_id + '&pSTR_REQ_NO=' + (vm.form.STR_REQ_NO || "") + '&pSTR_REQ_DT=' + (vm.form.STR_REQ_DT || "") + '&pREQ_TYPE_NAME=' + (vm.form.REQ_TYPE_NAME || "") + '&pBYR_ACC_NAME_EN=' + (vm.form.BYR_ACC_NAME_EN || "") + '&pMC_ORDER_NO_LST=' + (vm.form.MC_ORDER_NO_LST || "") + '&pYR_COUNT_NO=' + (vm.form.YR_COUNT_NO || "")).then(function (res) {
                                e.success(res);
                            });
                        }

                        $sessionStorage.SessionMessage.PAGE_NO = params.page;
                        vm.form.PAGE_NO = params.page;
                        vm.loading = 1;
                        vm.showSplash = false;
                    }
                },
                schema: {
                    data: "data",
                    total: 'total',
                    model: {
                        fields: {
                            KNT_YRN_STR_REQ_H_ID: { type: "string" },
                            STR_REQ_NO: { type: "string" },
                            STR_REQ_DT: { type: "date" },
                            REQ_TYPE_NAME: { type: "string" },
                            BUYER_NAME_EN: { type: "string" },
                            ORDER_NO: { type: "string" },
                            YR_COUNT_NO: { type: "string" },
                            TTL_REQ_QTY: { type: "number" },
                            TTL_ISU_QTY: { type: "number" },
                            BALANCE_QTY: { type: "number" },
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
            //sortable: true,
            columns: [
                { field: "KNT_YRN_STR_REQ_H_ID", hidden: true },
                { field: "KNT_JOB_CRD_H_ID", hidden: true },
                { field: "KNT_SC_PRG_ISS_ID", hidden: true },
                { field: "RF_REQ_TYPE_ID", hidden: true },
                { field: "STR_REQ_BY", hidden: true },
                { field: "STR_REQ_TO", hidden: true },

                { field: "STR_REQ_NO", title: "Requisition No", type: "string", width: "10%" },
                { field: "STR_REQ_DT", title: "Requisition Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "REQ_TYPE_NAME", title: "Requisition Type", type: "string", width: "20%" },
                { field: "BUYER_NAME_EN", title: "Buyer", type: "string", width: "10%" },
                { field: "ORDER_NO", title: "Order", type: "string", width: "10%" },
                { field: "YR_COUNT_NO", title: "Yarn Count", type: "string", width: "10%" },
                { field: "TTL_REQ_QTY", title: "TTL Req. Qty", type: "string", width: "8%" },
                { field: "TTL_ISU_QTY", title: "TTL Issue Qty", type: "string", width: "8%" },
                { field: "BALANCE_QTY", title: "Balance Qty", type: "string", width: "8%" },
                {
                    field: "EVENT_NAME", title: "Status", type: "string", width: "10%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'
                },
                //{ field: "USER_NAME_EN", title: "Req. Status", type: "string", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        //return "</a><a ui-sref='YarnIssueReq({pKNT_YRN_STR_REQ_H_ID:dataItem.KNT_YRN_STR_REQ_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> {{dataItem.NXT_ACTION_NAME}}</i></a></a>";
                        return '</a><a ui-sref="YarnIssue({pKNT_YRN_STR_REQ_H_ID:dataItem.KNT_YRN_STR_REQ_H_ID,pRF_REQ_TYPE_ID:dataItem.RF_REQ_TYPE_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ' && dataItem.PERMISSION==1  && dataItem.RF_REQ_TYPE_ID!=4" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a>' +
                            ' <a ui-sref="YarnTestIssue({pKNT_YRN_STR_REQ_H_ID:dataItem.KNT_YRN_STR_REQ_H_ID})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ' && dataItem.PERMISSION==1 && dataItem.RF_REQ_TYPE_ID==4"" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a>' +
                        //'<a ui-sref="YarnIssueReq({pKNT_YRN_STR_REQ_H_ID:dataItem.KNT_YRN_STR_REQ_H_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ') && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a>'+
                        ' <a ng-click="vm.printBatchCard(dataItem)" class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a></a>';
                    },
                    width: "10%"
                },
                {
                    title: "Close",
                    field: "CHK_VAL",
                    template: function () {
                        return '<input type="checkbox" ng-model="dataItem.CHK_VAL" ng-click="vm.checkBlance(dataItem)"></input>';
                    },
                    width: "6%"
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
                        return '</a><a ui-sref="YarnIssue({pKNT_YRN_ISS_H_ID:dataItem.KNT_YRN_ISS_H_ID})" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs blue"><i class="fa fa-edit"> Confirm</i></a>' +
                        ' <a ng-click="vm.printIssueChallan(dataItem)" class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a></a>';
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

            var data = angular.copy(dataOri);
            var List = _.filter(vm.gridOptions.dataSource.data(), function (x) { return x.CHK_VAL == true; });
            if (List.length > 0) {
                data["XML_REQ_D"] = KnittingDataService.xmlStringShort(List.map(function (o) {
                    return {
                        KNT_YRN_STR_REQ_H_ID: o.KNT_YRN_STR_REQ_H_ID == null ? 0 : o.KNT_YRN_STR_REQ_H_ID
                    }
                }));

                return KnittingDataService.saveDataByUrl(data, '/YarnIssueReq/Update').then(function (res) {

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