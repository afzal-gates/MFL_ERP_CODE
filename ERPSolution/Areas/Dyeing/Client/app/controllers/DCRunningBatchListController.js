(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DCRunningBatchListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$modal', DCRunningBatchListController]);
    function DCRunningBatchListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $modal) {

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
            var promise = [DCRunningBatchList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        var hub = new Hub('dashboard', {
            listeners: {
                'DCRunningBatchList': function () {
                    DCRunningBatchList();
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


        function DCRunningBatchList() {

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
                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/RunningBatchList/' + params.page + '/' + params.pageSize + '?pOption=3010' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
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
                { field: "STR_REQ_NO", title: "REQ No", type: "string", width: "10%" },
                { field: "STR_REQ_DT", title: "Req Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COLOR_NAME_EN", title: "Color Name", type: "string", width: "10%" },
                //{ field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "10%" },
                { field: "REQ_TYPE_NAME", title: "Process Type", type: "string", width: "10%" },
                { field: "FROM_ST_NAME", title: "Req. Store", type: "string", width: "10%" },
                { field: "DYE_BATCH_NO", title: "Batch No", type: "string", width: "10%" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "10%" },
                {
                    field: "EVENT_NAME", title: "Status", type: "string", width: "10%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'
                },
                {
                    title: "Print",
                    template: function () {

                        return '<a ng-if="dataItem.PERMISSION==1" ui-sref="RunningBatchEdit({pDYE_STR_REQ_H_ID:dataItem.DYE_STR_REQ_H_ID})" style="color:black" class="btn btn-xs yellow-gold"><i class="fa fa-edit"> Edit</i></a> <a ng-click="vm.printBatchCardAddition(dataItem)"  class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a> </a>';
                    },
                    width: "10%"
                }
            ]
        };


        vm.openCheckRollModal = function (item) {


            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'batchCheckRollStatus.html',
                controller: function ($scope, $modalInstance, DyeingDataService, formData) {
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