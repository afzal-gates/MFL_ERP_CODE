(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DCBatchProgramCompleteListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$modal', '$filter', DCBatchProgramCompleteListController]);
    function DCBatchProgramCompleteListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $modal, $filter) {

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
                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/CompleteBatchProgram/' + params.page + '/' + params.pageSize + '?' + pm + '&pRF_REQ_TYPE_ID=7&pOption=3005&pUSER_ID=' + cur_user_id).then(function (res) {
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
            scrollable: true,
            columns: [
                { field: "DYE_STR_REQ_H_ID", hidden: true },
                { field: "STR_REQ_NO", title: "Requisition No", type: "string", width: "10%" },
                { field: "STR_REQ_DT", title: "Req Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COLOR_NAME_EN", title: "Color Name", type: "string", width: "10%" },
                //{ field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "10%" },
                { field: "REQ_TYPE_NAME", title: "Process Type", type: "string", width: "10%" },
                { field: "FROM_ST_NAME", title: "Req. Store", type: "string", width: "10%" },
                { field: "DYE_BATCH_NO", title: "Batch No", type: "string", width: "10%" },
                //{ field: "REMARKS", title: "Remarks", type: "string", width: "10%" },
                {
                    field: "EVENT_NAME", title: "Status", type: "string", width: "10%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'
                },
                {
                    title: "Print",
                    template: function () {
                        //return '<a ng-click="vm.openCheckRollModal(dataItem)" class="btn btn-xs blue"><i class="fa fa-print"> Check Roll</i></a> </a>';
                        return ' <a ng-click="vm.printBatchCardAddition(dataItem)"  class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a>' +
                        //'<a ng-click="vm.openCheckRollModal(dataItem)" class="btn btn-xs green"><i class="fa fa-print"> Check Roll</i></a> ' +
                            ' <a ng-click="vm.openDfModal(dataItem)"  class="btn btn-xs red"><i class="fa fa-edit"> Back From DF</i></a></a>';
                    },
                    width: "15%"
                }
            ]
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
                        }
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

                        if (fnValidate() == true) {
                            console.log(dataOri);
                            var data = angular.copy(dataOri);

                            return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/UpdateBack2Dyeing').then(function (res) {

                                if (res.success === false) {
                                    vm.errors = res.errors;
                                }
                                else {

                                    res['data'] = angular.fromJson(res.jsonStr);
                                    config.appToastMsg(res.data.PMSG);
                                }
                            }).catch(function (message) {
                                exception.catcher('XHR loading Failded')(message);
                            });
                        }
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




        vm.openCheckRollModal = function (item) {


            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'batchCheckRollStatus.html',
                controller: function ($scope, $modalInstance, DyeingDataService, formData, $filter) {
                    var vmS = this;
                    //vmS.today = new Date();
                    $scope.today = new Date();

                    $scope.datePickerOptions = {
                        parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
                    };

                    console.log(formData);

                    getData();
                    getUserData();
                    getDefectTypeList();

                    $scope.cancel = function () {

                        $modalInstance.close();
                    }

                    function getDefectTypeList() {
                        return $scope["defectTypeList"] = {
                            optionLabel: "-- Select Defect --",
                            filter: "contains",
                            autoBind: true,
                            dataSource: {
                                transport: {
                                    read: function (e) {
                                        DyeingDataService.getDataByFullUrl('/api/common/getDyeDfctTypeList ').then(function (res) {
                                            e.success(res);
                                        }, function (err) {
                                            console.log(err);
                                        });
                                    }
                                }
                            },
                            dataTextField: "DY_DFCT_TYPE_NAME",
                            dataValueField: "RF_DY_DFCT_TYPE_ID"
                        };
                    }

                    function getUserData() {
                        return $scope["userList"] = {
                            optionLabel: "-- Select User --",
                            filter: "startswith",
                            autoBind: true,
                            valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                            template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                                      '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                            dataSource: {
                                transport: {
                                    read: function (e) {
                                        DyeingDataService.getUserDatalist().then(function (res) {
                                            e.success(res);
                                        }, function (err) {
                                            console.log(err);
                                        });
                                    }
                                }
                            },
                            select: function (e) {
                                vm.IsChange = false;
                            },

                            height: 400,
                            dataTextField: "LOGIN_ID",
                            dataValueField: "SC_USER_ID"
                        };

                    }


                    function getData() {

                        return $scope["btStatusList"] = {
                            optionLabel: "-- Select Status --",
                            filter: "startswith",
                            autoBind: true,
                            dataSource: {
                                transport: {
                                    read: function (e) {
                                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchReprocessTypeList').then(function (res) {

                                            var list = _.filter(res, function (x) { return x.RE_PROC_FLAG == "R" });
                                            e.success(list);
                                        });
                                    }
                                }
                            },
                            dataTextField: "RE_PROC_TYPE_NAME",
                            dataValueField: "DYE_RE_PROC_TYPE_ID"
                        };
                    }

                    $scope.disableOtherType = function () {

                        console.log($scope.vmS);
                        if ($scope.vmS.IS_ROLL_OK == "Y") {
                            $scope.vmS.RF_DY_DFCT_TYPE_LST = [];
                            $scope.vmS.REQ_RE_PROC_TYPE_ID = '';
                        }
                    }

                    $scope.updateAll = function (dataOri) {

                        if (fnValidateSub2() == true) {
                            var data = angular.copy(dataOri);

                            console.log(data);
                            data['RF_DY_DFCT_TYPE_LST'] = !data.RF_DY_DFCT_TYPE_LST ? '0' : data.RF_DY_DFCT_TYPE_LST.join(',');

                            var _ldate = $filter('date')(data.CHK_ROLL_DT, 'yyyy-MM-ddTHH:mm:ss');
                            data["CHK_ROLL_DT"] = _ldate;
                            data.DYE_BT_STS_TYPE_ID = 6;

                            //if (data.IS_ROLL_OK == "Y")
                            //    data.DYE_BT_STS_TYPE_ID = 7;
                            //else
                            //    data.DYE_BT_STS_TYPE_ID = 8;

                            //var List = data.RF_DY_DFCT_TYPE_LST ? data.RF_DY_DFCT_TYPE_LST.split(',') : [];
                            //data["RF_DY_DFCT_TYPE_LST"] = List;

                            return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/UpdateBatchProd').then(function (res) {

                                if (res.success === false) {
                                    vm.errors = res.errors;
                                }
                                else {

                                    res['data'] = angular.fromJson(res.jsonStr);
                                    config.appToastMsg(res.data.PMSG);
                                }
                            });
                        }
                    }


                    $scope.confirmAll = function (dataOri) {

                        if (fnValidateSub2() == true) {
                            var data = angular.copy(dataOri);

                            console.log(data);
                            data['RF_DY_DFCT_TYPE_LST'] = !data.RF_DY_DFCT_TYPE_LST ? '0' : data.RF_DY_DFCT_TYPE_LST.join(',');

                            var _ldate = $filter('date')(data.CHK_ROLL_DT, 'yyyy-MM-ddTHH:mm:ss');
                            data["CHK_ROLL_DT"] = _ldate;

                            if (data.IS_ROLL_OK == "Y")
                                data.DYE_BT_STS_TYPE_ID = 7;
                            else
                                data.DYE_BT_STS_TYPE_ID = 8;

                            //var List = data.RF_DY_DFCT_TYPE_LST ? data.RF_DY_DFCT_TYPE_LST.split(',') : [];
                            //data["RF_DY_DFCT_TYPE_LST"] = List;

                            return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/UpdateBatchProd').then(function (res) {

                                if (res.success === false) {
                                    vm.errors = res.errors;
                                }
                                else {

                                    res['data'] = angular.fromJson(res.jsonStr);
                                    config.appToastMsg(res.data.PMSG);
                                }
                            });
                        }
                    }

                    $scope.vmS = formData;


                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    formData: function (DyeingDataService) {
                        item["CHK_ROLL_DT"] = "";
                        item["CHECK_BY"] = cur_user_id;
                        item["REQ_RE_PROC_TYPE_ID"] = 3;

                        return item;
                    }
                }


            });


            modalInstance.result.then(function (data) {
                CompleteBatchList();
                //vm.bankListData = data;
                //vm.bankInfo.RF_BANK_ID = ary.RF_BANK_ID;

                //vm.branchListData = new kendo.data.DataSource({
                //    transport: {
                //        read: function (e) {
                //            commonDataService.getBranchListData(ary.RF_BANK_ID).then(function (res) {
                //                e.success(res);
                //            });
                //        }
                //    }
                //});


            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

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

    }

})();