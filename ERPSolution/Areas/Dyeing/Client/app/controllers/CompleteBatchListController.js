(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('CompleteBatchListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$modal', CompleteBatchListController]);
    function CompleteBatchListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        var hubUrl = "/signalr";
        var connection = $.hubConnection(hubUrl, { useDefaultPath: true });
        var hub = connection.createHubProxy("dashboard");
        var hubStart = connection.start();

        vm.form = { REPORT_CODE: '' };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [CompleteBatchList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        var hub = new Hub('dashboard', {
            listeners: {
                'CompleteBatchList': function () {
                    CompleteBatchList();
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


        function CompleteBatchList() {

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
                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/CompleteBatchProgram/' + params.page + '/' + params.pageSize + '?' + pm + '&pRF_REQ_TYPE_ID=7&pOption=3007&pUSER_ID=' + cur_user_id).then(function (res) {
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
            sortable: true,
            scrollable: false,
            columns: [
                { field: "DYE_STR_REQ_H_ID", hidden: true },
                { field: "STR_REQ_NO", title: "REQ No", type: "string", width: "10%" },
                { field: "STR_REQ_DT", title: "Req Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "10%" },
                //{ field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "10%" },
                { field: "REQ_TYPE_NAME", title: "Re-process Type", type: "string", width: "10%" },
                { field: "FROM_ST_NAME", title: "Req. Store", type: "string", width: "10%" },
                { field: "DYE_BATCH_NO", title: "Batch No", type: "string", width: "10%" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "10%" },
                {
                    field: "COLOR_NAME_EN", title: "Color", type: "string", width: "10%"
                },
                {
                    title: "Action Back",
                    template: '<a  title="Open QC" ng-if="dataItem.LK_RP_SRC_TYPE_ID==544" ng-click="vm.openDfOnlineQcModal(dataItem)" class="btn btn-xs green"><i class="fa fa-recycle"> {{dataItem.SRC_OF_RP_ACTION}}</i></a>' +
                        '<a  title="Open Sub-Lot" ng-if="dataItem.LK_RP_SRC_TYPE_ID==545" ng-click="vm.openDfModal(dataItem)" class="btn btn-xs red"><i class="fa fa-recycle"> {{dataItem.SRC_OF_RP_ACTION}}</i></a>' +
                        '<a  title="Open Cutting" ng-if="dataItem.LK_RP_SRC_TYPE_ID==736" ng-click="vm.openCuttingModal(dataItem)" class="btn btn-xs yellow-gold"><i class="fa fa-recycle"> {{dataItem.SRC_OF_RP_ACTION}}</i></a>' +
                        '<a  title="Open Rib Shade" ng-if="dataItem.LK_RP_SRC_TYPE_ID==802" ng-click="vm.openRibModal(dataItem)" class="btn btn-xs blue"><i class="fa fa-recycle"> {{dataItem.SRC_OF_RP_ACTION}}</i></a>'+
                        '<ul kendo-menu k-orientation="menuOrientation" k-rebind="menuOrientation" k-on-select="onSelect(kendoEvent)">' +
                                  '<li><span style="color:black;">{{dataItem.SRC_OF_RP_ACTION}}</span>' +
                                      '<ul style="width:150px;">' +
                                           '<li style="padding:5px;"><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:3})" style="color:black"><i class="fa fa-edit"> Re-Matching</i></a> </li>' +
                                           '<li style="padding:5px;"><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:4})" style="color:black"><i class="fa fa-edit"> Re-Wash</i></a> </li>' +
                                           '<li style="padding:5px;"><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:5})" style="color:black"><i class="fa fa-edit"> Re-Dyeing</i></a> </li>' +

                                      '</ul>' +
                                  '</li></ul> ',
                    width: "10%"
                },                
                //{
                //    title: "Action",
                //    template: function () {
                //        return '<ul kendo-menu k-orientation="menuOrientation" k-rebind="menuOrientation" ng-if="dataItem.LK_RP_SRC_TYPE_ID==543" k-on-select="onSelect(kendoEvent)">' +
                //                  '<li><span style="color:black;">Navigate</span>' +
                //                      '<ul style="width:150px;">' +
                //                           '<li style="padding:5px;"><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:3})" style="color:black"><i class="fa fa-edit"> Re-Matching</i></a> </li>' +
                //                           '<li style="padding:5px;"><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:4})" style="color:black"><i class="fa fa-edit"> Re-Wash</i></a> </li>' +
                //                           '<li style="padding:5px;"><a ui-sref="DCBatchProgramRequisition({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID,pADDITION_FLAG:5})" style="color:black"><i class="fa fa-edit"> Re-Dyeing</i></a> </li>' +

                //                      '</ul>' +
                //                  '</li></ul> ';
                //        //return '<a ng-click="vm.openCheckRollModal(dataItem)" class="btn btn-xs green"><i class="fa fa-print"> Check Roll</i></a> <a ng-click="vm.printBatchCardAddition(dataItem)"  class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a> </a>';
                //    },
                //    width: "10%"
                //}
            ]
        };


        vm.openDfOnlineQcModal = function (item) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'DyeingFinishingOnlineQcDtl.html',
                controller: function ($scope, $modalInstance, DyeingDataService, formData) {

                    var vmDF = this;
                    $scope.vmDF = formData;
                    console.log(formData);
                    $scope.dtFormat = config.appDateFormat;
                    $scope.TimeFormat = config.appTimeFormat;
                    $scope.today = new Date();

                    $scope.datePickerOptions = {
                        parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
                    };

                    $scope.CHK_ROLL_DT_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.CHK_ROLL_DT_LNopened = true;
                    }
                    loadFabricInfo();
                    loadSubLotInfo();
                    $scope.checkBatchQty = function () {
                        if (parseFloat($scope.vmDF.ACT_BATCH_QTY) < parseFloat($scope.vmDF.LOT_QTY)) {
                            config.appToastMsg("MULTI-005 Value Exceed Orginal Qty!!!");
                            $scope.vmDF.LOT_QTY = '';
                        }
                    }
                    $scope.searchBatchInfo = function (BatchNo) {

                        if (BatchNo.length > 9) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForProduction?pDYE_BATCH_NO=' + (BatchNo || "")).then(function (res) {

                                if (res.batch.length > 0) {

                                    $scope.vmDF.BatchFabricList = res.fab;

                                    $scope.vmDF.DYE_STR_REQ_H_ID = res.batch[0].DYE_STR_REQ_H_ID;
                                    $scope.vmDF.DYE_BT_CARD_H_ID = res.batch[0].DYE_BT_CARD_H_ID;
                                    $scope.vmDF.STR_REQ_NO = res.batch[0].STR_REQ_NO;
                                    $scope.vmDF.STR_REQ_DT = res.batch[0].STR_REQ_DT;
                                    $scope.vmDF.DYE_MACHINE_ID = res.batch[0].DYE_MACHINE_ID;
                                    $scope.vmDF.BUYER_NAME_EN = res.batch[0].BUYER_NAME_EN;
                                    $scope.vmDF.STYLE_NO = res.batch[0].STYLE_NO;
                                    $scope.vmDF.ORDER_NO = res.batch[0].ORDER_NO;
                                    $scope.vmDF.COLOR_NAME_EN = res.batch[0].COLOR_NAME_EN;
                                    $scope.vmDF.LD_RECIPE_NO = res.batch[0].LD_RECIPE_NO;
                                    $scope.vmDF.COLOR_GRP_NAME_EN = res.batch[0].COLOR_GRP_NAME_EN;
                                    $scope.vmDF.MC_LD_RECIPE_H_ID = res.batch[0].MC_LD_RECIPE_H_ID;
                                    $scope.vmDF.DYE_RE_PROC_TYPE_ID = res.batch[0].DYE_RE_PROC_TYPE_ID;
                                    $scope.vmDF.REQ_RE_PROC_TYPE_ID = res.batch[0].REQ_RE_PROC_TYPE_ID;
                                    $scope.vmDF.LQR_RATIO = "1:" + res.batch[0].LQR_RATIO;
                                    $scope.vmDF.DYE_MTHD_NAME = res.batch[0].DYE_MTHD_NAME;
                                    $scope.vmDF.ACTN_ROLE_FLAG = res.batch[0].ACTN_ROLE_FLAG;
                                    $scope.vmDF.DYE_BATCH_NO_LST = res.batch[0].DYE_BATCH_NO.length > 11 ? res.batch[0].DYE_BATCH_NO.substring(0, 10) : res.batch[0].DYE_BATCH_NO;
                                    $scope.vmDF.ACT_BATCH_QTY_LST = res.batch[0].ACT_BATCH_QTY + " Kg";
                                    $scope.vmDF.ACT_BATCH_QTY = res.batch[0].ACT_BATCH_QTY;
                                }
                            });

                            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/SelectForBatchReProcessByID?pDYE_BATCH_NO=' + (BatchNo.trim() || "")).then(function (res) {
                                $scope.vmDF.subLotBatchList = res;
                            });
                        }
                    }

                    $scope.calculateLotQty = function () {
                        var total = 0;
                        var ids = "";
                        var list = _.filter($scope.vmDF.subLotBatchList, function (x) { return x.IS_SELECT == "Y" });
                        console.log(list);
                        for (var i = 0; i < list.length; i++) {
                            total = total + parseInt(list[i].BATCH_RP_QTY);
                            if (i == 0) {
                                ids = list[i].DF_PROC_QC_RPT_H_ID;
                            }
                            else
                                ids = ids + ',' + list[i].DF_PROC_QC_RPT_H_ID;
                        }

                        $scope.vmDF.SUB_LOT_NO = list.length;
                        $scope.vmDF.LOT_QTY = total;
                        $scope.vmDF.LOT_ID = ids;
                    }

                    function loadFabricInfo() {

                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForProduction?pDYE_BATCH_NO=' + ($scope.vmDF.DYE_BATCH_NO || "")).then(function (res) {

                            if (res.batch.length > 0) {

                                $scope.vmDF.BatchFabricList = res.fab;

                                $scope.vmDF.DYE_STR_REQ_H_ID = res.batch[0].DYE_STR_REQ_H_ID;
                                $scope.vmDF.DYE_BT_CARD_H_ID = res.batch[0].DYE_BT_CARD_H_ID;
                                $scope.vmDF.STR_REQ_NO = res.batch[0].STR_REQ_NO;
                                $scope.vmDF.STR_REQ_DT = res.batch[0].STR_REQ_DT;
                                $scope.vmDF.DYE_MACHINE_ID = res.batch[0].DYE_MACHINE_ID;
                                $scope.vmDF.BUYER_NAME_EN = res.batch[0].BUYER_NAME_EN;
                                $scope.vmDF.STYLE_NO = res.batch[0].STYLE_NO;
                                $scope.vmDF.ORDER_NO = res.batch[0].ORDER_NO;
                                $scope.vmDF.COLOR_NAME_EN = res.batch[0].COLOR_NAME_EN;
                                $scope.vmDF.LD_RECIPE_NO = res.batch[0].LD_RECIPE_NO;
                                $scope.vmDF.COLOR_GRP_NAME_EN = res.batch[0].COLOR_GRP_NAME_EN;
                                $scope.vmDF.MC_LD_RECIPE_H_ID = res.batch[0].MC_LD_RECIPE_H_ID;
                                $scope.vmDF.DYE_RE_PROC_TYPE_ID = res.batch[0].DYE_RE_PROC_TYPE_ID;
                                $scope.vmDF.REQ_RE_PROC_TYPE_ID = res.batch[0].REQ_RE_PROC_TYPE_ID;
                                $scope.vmDF.LQR_RATIO = "1:" + res.batch[0].LQR_RATIO;
                                $scope.vmDF.DYE_MTHD_NAME = res.batch[0].DYE_MTHD_NAME;
                                $scope.vmDF.ACTN_ROLE_FLAG = res.batch[0].ACTN_ROLE_FLAG;
                                $scope.vmDF.DYE_BATCH_NO_LST = res.batch[0].DYE_BATCH_NO;
                                $scope.vmDF.ACT_BATCH_QTY_LST = res.batch[0].ACT_BATCH_QTY + " Kg";
                                $scope.vmDF.ACT_BATCH_QTY = res.batch[0].ACT_BATCH_QTY;
                            }
                        });

                    }

                    function loadSubLotInfo() {

                        DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/SelectForBatchReProcessByID?pDYE_BATCH_NO=' + (formData.DYE_BATCH_NO_LST || "")).then(function (res) {
                            $scope.vmDF.subLotBatchList = res;
                        });
                    }


                    $scope.clearAll = function () {
                        $scope.MC_BYR_ACC_GRP_ID = '';
                        $scope.COLOR_NAME_EN = '';
                        $scope.STYLE_ORDER_NO = '';
                        $scope.QC_DT = '';
                    }

                    $scope.cancel = function (data) {
                        $modalInstance.close(data);
                    }

                    $scope.submitAll = function (dataOri) {

                        $modalInstance.close([]);
                        var url = '/dyeing/Dye/CompleteBatchList?a=217/#/DCBatchProgramRequisition?pDYE_STR_REQ_H_ID=' + ($scope.vmDF.DYE_STR_REQ_H_ID || 0) + '&pADDITION_FLAG=3&pON_LINE_QC_ID=' + $scope.vmDF.LOT_ID;
                        var win = window.open(url, '_blank');
                        win.focus();
                        //$state.go("DCBatchProgramRequisition", { pDYE_STR_REQ_H_ID: ($scope.vmDF.DYE_STR_REQ_H_ID || 0), pADDITION_FLAG: '3', pON_LINE_QC_ID: $scope.vmDF.LOT_ID }, { reload: true });
                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    formData: function (DyeingDataService) {
                        item["CHK_ROLL_DT"] = "";
                        item["CHECK_BY"] = cur_user_id;
                        item["REQ_RE_PROC_TYPE_ID"] = 3;
                        console.log(item);
                        return item;
                    }
                }
            });

            modalInstance.result.then(function (item) {

                //if (item.DYE_BATCH_NO.length > 0) {
                //    var lst = item.DYE_BATCH_NO.split('/');
                //    if (lst.length > 0)
                //        vm.form.DYE_BATCH_NO = lst[0];
                //    else
                //        vm.form.DYE_BATCH_NO = item.DYE_BATCH_NO;

                //    vm.searchBatchInfo(1);
                //}
            });

        };


        vm.openRibModal = function (item) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'DyeingFinishingRibQcDtl.html',
                controller: function ($scope, $modalInstance, DyeingDataService, formData) {

                    var vmDF = this;
                    $scope.vmDF = formData;
                    console.log(formData);
                    $scope.dtFormat = config.appDateFormat;
                    $scope.TimeFormat = config.appTimeFormat;
                    $scope.today = new Date();

                    $scope.datePickerOptions = {
                        parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
                    };

                    $scope.CHK_ROLL_DT_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.CHK_ROLL_DT_LNopened = true;
                    }
                    loadBatchInfo();
                    loadRibShadeInfo();
                    $scope.checkBatchQty = function () {
                        if (parseFloat($scope.vmDF.ACT_BATCH_QTY) < parseFloat($scope.vmDF.LOT_QTY)) {
                            config.appToastMsg("MULTI-005 Value Exceed Orginal Qty!!!");
                            $scope.vmDF.LOT_QTY = '';
                        }
                    }
                    $scope.searchBatchInfo = function (BatchNo) {

                        if (BatchNo.length > 9) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForProduction?pDYE_BATCH_NO=' + (BatchNo || "")).then(function (res) {

                                if (res.batch.length > 0) {

                                    $scope.vmDF.BatchFabricList = res.fab;

                                    $scope.vmDF.DYE_STR_REQ_H_ID = res.batch[0].DYE_STR_REQ_H_ID;
                                    $scope.vmDF.DYE_BT_CARD_H_ID = res.batch[0].DYE_BT_CARD_H_ID;
                                    $scope.vmDF.STR_REQ_NO = res.batch[0].STR_REQ_NO;
                                    $scope.vmDF.STR_REQ_DT = res.batch[0].STR_REQ_DT;
                                    $scope.vmDF.DYE_MACHINE_ID = res.batch[0].DYE_MACHINE_ID;
                                    $scope.vmDF.BUYER_NAME_EN = res.batch[0].BUYER_NAME_EN;
                                    $scope.vmDF.STYLE_NO = res.batch[0].STYLE_NO;
                                    $scope.vmDF.ORDER_NO = res.batch[0].ORDER_NO;
                                    $scope.vmDF.COLOR_NAME_EN = res.batch[0].COLOR_NAME_EN;
                                    $scope.vmDF.LD_RECIPE_NO = res.batch[0].LD_RECIPE_NO;
                                    $scope.vmDF.COLOR_GRP_NAME_EN = res.batch[0].COLOR_GRP_NAME_EN;
                                    $scope.vmDF.MC_LD_RECIPE_H_ID = res.batch[0].MC_LD_RECIPE_H_ID;
                                    $scope.vmDF.DYE_RE_PROC_TYPE_ID = res.batch[0].DYE_RE_PROC_TYPE_ID;
                                    $scope.vmDF.REQ_RE_PROC_TYPE_ID = res.batch[0].REQ_RE_PROC_TYPE_ID;
                                    $scope.vmDF.LQR_RATIO = "1:" + res.batch[0].LQR_RATIO;
                                    $scope.vmDF.DYE_MTHD_NAME = res.batch[0].DYE_MTHD_NAME;
                                    $scope.vmDF.ACTN_ROLE_FLAG = res.batch[0].ACTN_ROLE_FLAG;
                                    $scope.vmDF.DYE_BATCH_NO_LST = res.batch[0].DYE_BATCH_NO.length > 11 ? res.batch[0].DYE_BATCH_NO.substring(0, 10) : res.batch[0].DYE_BATCH_NO;
                                    $scope.vmDF.ACT_BATCH_QTY_LST = res.batch[0].ACT_BATCH_QTY + " Kg";
                                    $scope.vmDF.ACT_BATCH_QTY = res.batch[0].ACT_BATCH_QTY;
                                }
                            });

                            DyeingDataService.getDataByFullUrl('/api/Dye/OtherCheckRoll/SelectForRibRequisition?pDYE_BATCH_NO=' + (BatchNo.trim() || "")).then(function (res) {
                                $scope.vmDF.ribBatchList = res;
                            });
                        }
                    }

                    $scope.calculateLotQty = function () {
                        var total = 0;
                        var ids = "";
                        var list = _.filter($scope.vmDF.ribBatchList, function (x) { return x.IS_SELECT == "Y" });
                        console.log(list);
                        for (var i = 0; i < list.length; i++) {
                            total = total + parseInt(list[i].BATCH_RP_QTY);
                            if (i == 0) {
                                ids = list[i].DF_RIB_SHADE_RPT_H_ID;
                            }
                            else
                                ids = ids + ',' + list[i].DF_RIB_SHADE_RPT_H_ID;
                        }

                        $scope.vmDF.SUB_LOT_NO = list.length;
                        $scope.vmDF.LOT_QTY = total;
                        $scope.vmDF.LOT_ID = ids;
                    }

                    function loadBatchInfo() {

                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForProduction?pDYE_BATCH_NO=' + ($scope.vmDF.DYE_BATCH_NO || "")).then(function (res) {

                            if (res.batch.length > 0) {
                                console.log(res.batch[0]);
                                $scope.vmDF.BatchFabricList = res.fab;

                                var batList = res.batch[0].DYE_BATCH_NO.split('-');
                                var batch = batList.length > 1 ? batList[0] + '-' + batList[1] : res.batch[0].DYE_BATCH_NO;

                                $scope.vmDF.DYE_STR_REQ_H_ID = res.batch[0].DYE_STR_REQ_H_ID;
                                $scope.vmDF.DYE_BT_CARD_H_ID = res.batch[0].DYE_BT_CARD_H_ID;
                                $scope.vmDF.STR_REQ_NO = res.batch[0].STR_REQ_NO;
                                $scope.vmDF.STR_REQ_DT = res.batch[0].STR_REQ_DT;
                                $scope.vmDF.DYE_MACHINE_ID = res.batch[0].DYE_MACHINE_ID;
                                $scope.vmDF.BUYER_NAME_EN = res.batch[0].BUYER_NAME_EN;
                                $scope.vmDF.STYLE_NO = res.batch[0].STYLE_NO;
                                $scope.vmDF.ORDER_NO = res.batch[0].ORDER_NO;
                                $scope.vmDF.COLOR_NAME_EN = res.batch[0].COLOR_NAME_EN;
                                $scope.vmDF.LD_RECIPE_NO = res.batch[0].LD_RECIPE_NO;
                                $scope.vmDF.COLOR_GRP_NAME_EN = res.batch[0].COLOR_GRP_NAME_EN;
                                $scope.vmDF.MC_LD_RECIPE_H_ID = res.batch[0].MC_LD_RECIPE_H_ID;
                                $scope.vmDF.DYE_RE_PROC_TYPE_ID = res.batch[0].DYE_RE_PROC_TYPE_ID;
                                $scope.vmDF.REQ_RE_PROC_TYPE_ID = res.batch[0].REQ_RE_PROC_TYPE_ID;
                                $scope.vmDF.LQR_RATIO = "1:" + res.batch[0].LQR_RATIO;
                                $scope.vmDF.DYE_MTHD_NAME = res.batch[0].DYE_MTHD_NAME;
                                $scope.vmDF.ACTN_ROLE_FLAG = res.batch[0].ACTN_ROLE_FLAG;
                                $scope.vmDF.DYE_BATCH_NO_LST = batch;
                                $scope.vmDF.ACT_BATCH_QTY_LST = res.batch[0].ACT_BATCH_QTY + " Kg";
                                $scope.vmDF.ACT_BATCH_QTY = res.batch[0].ACT_BATCH_QTY;
                            }
                        });

                    }

                    function loadRibShadeInfo() {
                        DyeingDataService.getDataByFullUrl('/api/Dye/OtherCheckRoll/SelectForRibRequisition?pDYE_BATCH_NO=' + (formData.DYE_BATCH_NO || "")).then(function (res) {
                            $scope.vmDF.ribBatchList = res;
                        });
                    }


                    $scope.clearAll = function () {
                        $scope.MC_BYR_ACC_GRP_ID = '';
                        $scope.COLOR_NAME_EN = '';
                        $scope.STYLE_ORDER_NO = '';
                        $scope.QC_DT = '';
                    }

                    $scope.cancel = function (data) {
                        $modalInstance.close(data);
                    }

                    $scope.submitAll = function (dataOri) {
                        $modalInstance.close([]);
                        var url = '/dyeing/Dye/CompleteBatchList?a=217/#/DCBatchProgramRequisition?pDYE_STR_REQ_H_ID=' + ($scope.vmDF.DYE_STR_REQ_H_ID || 0) + '&pADDITION_FLAG=3&pRIB_ID=' + $scope.vmDF.LOT_ID;
                        var win = window.open(url, '_blank');
                        win.focus();

                        //$state.go("DCBatchProgramRequisition", { pDYE_STR_REQ_H_ID: ($scope.vmDF.DYE_STR_REQ_H_ID || 0), pADDITION_FLAG: '3', pON_LINE_QC_ID: $scope.vmDF.LOT_ID }, { reload: true });
                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    formData: function (DyeingDataService) {
                        item["CHK_ROLL_DT"] = "";
                        item["CHECK_BY"] = cur_user_id;
                        item["REQ_RE_PROC_TYPE_ID"] = 3;
                        console.log(item);
                        return item;
                    }
                }
            });

            modalInstance.result.then(function (item) {
            });
        };



        vm.openDfModal = function (item) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'DyeingFinishingRejectDtl.html',
                controller: function ($scope, $modalInstance, DyeingDataService, formData) {

                    var vmDF = this;
                    $scope.vmDF = formData;

                    $scope.dtFormat = config.appDateFormat;
                    $scope.TimeFormat = config.appTimeFormat;
                    $scope.today = new Date();

                    $scope.datePickerOptions = {
                        parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
                    };

                    $scope.CHK_ROLL_DT_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.CHK_ROLL_DT_LNopened = true;
                    }
                    loadSubLotInfo();
                    loadFabricInfo();
                    $scope.checkBatchQty = function () {
                        if (parseFloat($scope.vmDF.ACT_BATCH_QTY) < parseFloat($scope.vmDF.LOT_QTY)) {
                            config.appToastMsg("MULTI-005 Value Exceed Orginal Qty!!!");
                            $scope.vmDF.LOT_QTY = '';
                        }
                    }
                    $scope.searchBatchInfo = function (BatchNo) {

                        if (BatchNo.length > 9) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForProduction?pDYE_BATCH_NO=' + (BatchNo || "")).then(function (res) {

                                if (res.batch.length > 0) {

                                    $scope.vmDF.BatchFabricList = res.fab;

                                    $scope.vmDF.DYE_STR_REQ_H_ID = res.batch[0].DYE_STR_REQ_H_ID;
                                    $scope.vmDF.DYE_BT_CARD_H_ID = res.batch[0].DYE_BT_CARD_H_ID;
                                    $scope.vmDF.STR_REQ_NO = res.batch[0].STR_REQ_NO;
                                    $scope.vmDF.STR_REQ_DT = res.batch[0].STR_REQ_DT;
                                    $scope.vmDF.DYE_MACHINE_ID = res.batch[0].DYE_MACHINE_ID;
                                    $scope.vmDF.BUYER_NAME_EN = res.batch[0].BUYER_NAME_EN;
                                    $scope.vmDF.STYLE_NO = res.batch[0].STYLE_NO;
                                    $scope.vmDF.ORDER_NO = res.batch[0].ORDER_NO;
                                    $scope.vmDF.COLOR_NAME_EN = res.batch[0].COLOR_NAME_EN;
                                    $scope.vmDF.LD_RECIPE_NO = res.batch[0].LD_RECIPE_NO;
                                    $scope.vmDF.COLOR_GRP_NAME_EN = res.batch[0].COLOR_GRP_NAME_EN;
                                    $scope.vmDF.MC_LD_RECIPE_H_ID = res.batch[0].MC_LD_RECIPE_H_ID;
                                    $scope.vmDF.DYE_RE_PROC_TYPE_ID = res.batch[0].DYE_RE_PROC_TYPE_ID;
                                    $scope.vmDF.REQ_RE_PROC_TYPE_ID = res.batch[0].REQ_RE_PROC_TYPE_ID;
                                    $scope.vmDF.LQR_RATIO = "1:" + res.batch[0].LQR_RATIO;
                                    $scope.vmDF.DYE_MTHD_NAME = res.batch[0].DYE_MTHD_NAME;
                                    $scope.vmDF.ACTN_ROLE_FLAG = res.batch[0].ACTN_ROLE_FLAG;
                                    $scope.vmDF.DYE_BATCH_NO_LST = res.batch[0].DYE_BATCH_NO.length > 11 ? res.batch[0].DYE_BATCH_NO.substring(0, 10) : res.batch[0].DYE_BATCH_NO;
                                    $scope.vmDF.ACT_BATCH_QTY_LST = res.batch[0].ACT_BATCH_QTY + " Kg";
                                    $scope.vmDF.ACT_BATCH_QTY = res.batch[0].ACT_BATCH_QTY;
                                }
                            });

                            DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQCRptByBatchNo?pDYE_BATCH_NO=' + (BatchNo.trim() || "")).then(function (res) {
                                $scope.vmDF.subLotBatchList = res;
                            });
                        }
                    }

                    $scope.calculateLotQty = function () {
                        var total = 0;
                        var ids = "";
                        var list = _.filter($scope.vmDF.subLotBatchList, function (x) { return x.IS_SELECT == "Y" });
                        console.log(list);
                        for (var i = 0; i < list.length; i++) {
                            total = total + parseInt(list[i].LOT_QTY);
                            if (i == 0) {
                                ids = list[i].DF_BT_SUB_LOT_ID;
                            }
                            else
                                ids = ids + ',' + list[i].DF_BT_SUB_LOT_ID;
                        }

                        $scope.vmDF.SUB_LOT_NO = list.length;
                        $scope.vmDF.LOT_QTY = total;
                        $scope.vmDF.LOT_ID = ids;
                    }

                    function loadFabricInfo() {

                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForProduction?pDYE_BATCH_NO=' + ($scope.vmDF.DYE_BATCH_NO || "")).then(function (res) {

                            if (res.batch.length > 0) {

                                $scope.vmDF.BatchFabricList = res.fab;

                                $scope.vmDF.DYE_STR_REQ_H_ID = res.batch[0].DYE_STR_REQ_H_ID;
                                $scope.vmDF.DYE_BT_CARD_H_ID = res.batch[0].DYE_BT_CARD_H_ID;
                                $scope.vmDF.STR_REQ_NO = res.batch[0].STR_REQ_NO;
                                $scope.vmDF.STR_REQ_DT = res.batch[0].STR_REQ_DT;
                                $scope.vmDF.DYE_MACHINE_ID = res.batch[0].DYE_MACHINE_ID;
                                $scope.vmDF.BUYER_NAME_EN = res.batch[0].BUYER_NAME_EN;
                                $scope.vmDF.STYLE_NO = res.batch[0].STYLE_NO;
                                $scope.vmDF.ORDER_NO = res.batch[0].ORDER_NO;
                                $scope.vmDF.COLOR_NAME_EN = res.batch[0].COLOR_NAME_EN;
                                $scope.vmDF.LD_RECIPE_NO = res.batch[0].LD_RECIPE_NO;
                                $scope.vmDF.COLOR_GRP_NAME_EN = res.batch[0].COLOR_GRP_NAME_EN;
                                $scope.vmDF.MC_LD_RECIPE_H_ID = res.batch[0].MC_LD_RECIPE_H_ID;
                                $scope.vmDF.DYE_RE_PROC_TYPE_ID = res.batch[0].DYE_RE_PROC_TYPE_ID;
                                $scope.vmDF.REQ_RE_PROC_TYPE_ID = res.batch[0].REQ_RE_PROC_TYPE_ID;
                                $scope.vmDF.LQR_RATIO = "1:" + res.batch[0].LQR_RATIO;
                                $scope.vmDF.DYE_MTHD_NAME = res.batch[0].DYE_MTHD_NAME;
                                $scope.vmDF.ACTN_ROLE_FLAG = res.batch[0].ACTN_ROLE_FLAG;
                                $scope.vmDF.DYE_BATCH_NO_LST = res.batch[0].DYE_BATCH_NO;
                                $scope.vmDF.ACT_BATCH_QTY_LST = res.batch[0].ACT_BATCH_QTY + " Kg";
                                $scope.vmDF.ACT_BATCH_QTY = res.batch[0].ACT_BATCH_QTY;
                            }
                        });

                    }

                    function loadSubLotInfo() {

                        DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQCRptByBatchNo?pDYE_BATCH_NO=' + ($scope.vmDF.DYE_BATCH_NO.trim() || "")).then(function (res) {
                            $scope.vmDF.subLotBatchList = res;
                        });
                    }


                    $scope.clearAll = function () {
                        $scope.MC_BYR_ACC_GRP_ID = '';
                        $scope.COLOR_NAME_EN = '';
                        $scope.STYLE_ORDER_NO = '';
                        $scope.QC_DT = '';
                    }

                    $scope.cancel = function (data) {
                        $modalInstance.close(data);
                    }

                    $scope.submitAll = function (dataOri) {

                        $modalInstance.close([]);
                        var url = '/dyeing/Dye/CompleteBatchList?a=217/#/DCBatchProgramRequisition?pDYE_STR_REQ_H_ID=' + ($scope.vmDF.DYE_STR_REQ_H_ID || 0) + '&pADDITION_FLAG=3&pLOT_ID=' + $scope.vmDF.LOT_ID;
                        var win = window.open(url, '_blank');
                        win.focus();
                        //$state.go("DCBatchProgramRequisition", { pDYE_STR_REQ_H_ID: ($scope.vmDF.DYE_STR_REQ_H_ID || 0), pADDITION_FLAG: '3', pLOT_ID: $scope.vmDF.LOT_ID }, { reload: true });
                    }
                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    formData: function (DyeingDataService) {
                        item["CHK_ROLL_DT"] = "";
                        item["CHECK_BY"] = cur_user_id;
                        item["REQ_RE_PROC_TYPE_ID"] = 3;
                        console.log(item);
                        return item;
                    }
                }
            });

            modalInstance.result.then(function (item) {

                //if (item.DYE_BATCH_NO.length > 0) {
                //    var lst = item.DYE_BATCH_NO.split('/');
                //    if (lst.length > 0)
                //        vm.form.DYE_BATCH_NO = lst[0];
                //    else
                //        vm.form.DYE_BATCH_NO = item.DYE_BATCH_NO;

                //    vm.searchBatchInfo(1);
                //}
            });

        };




    }

})();