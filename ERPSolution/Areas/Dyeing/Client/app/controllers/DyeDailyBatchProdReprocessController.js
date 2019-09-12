(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeDailyBatchProdReprocessController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', 'Dialog', '$modal', '$filter', 'moment', DyeDailyBatchProdReprocessController]);
    function DyeDailyBatchProdReprocessController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, Dialog, $modal, $filter, moment) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.DateTimeFormat = config.appDateTimeFormat;
        vm.TimeFormat = config.appTimeFormat;

        vm.today = new Date();
        //vm.today = kendo.toString(new Date(), "dd/MMM/yyyy hh:mm tt");

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
        };

        vm.form = {};

        vm.form.totalItems = 0;
        vm.form.currentPage = 1;

        vm.setPage = function (pageNo) {
            vm.form.currentPage = pageNo;
            vm.searchBatchInfo(pageNo);
        };

        vm.pageChanged = function (pageNo) {
            vm.form.currentPage = pageNo;
            vm.searchBatchInfo(vm.form.currentPage);
        };



        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetStatusList(), getBuyerAcList(), GetMachineList(), getRollStatusList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function GetMachineList() {

            return vm.machineList = {
                optionLabel: "-- M/C No --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetMachineInfoByTypeID?pDYE_MC_TYPE_ID=0').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "DYE_MACHINE_NO",
                dataValueField: "DYE_MACHINE_ID"
            };

        }

        function getRollStatusList() {
            return vm.rollStatusList = {
                optionLabel: "--Select Roll Status-",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(111).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        $scope.timediff = function (item) {

            if (item.UN_LOAD_TIME) {
                var dif = moment.utc(moment(item.UN_LOAD_TIME).diff(moment(item.LOAD_TIME))).format("HH:mm");
                item.TIME_DIFF = dif;
            }
            else {
                item.TIME_DIFF = "";
            }
        }

        $scope.LOAD_TIME_LNopen = function ($event, item) {

            $event.preventDefault();
            $event.stopPropagation();
            $scope.LOAD_TIME_LNopened = true;
        }

        $scope.UNLOAD_TIME_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.UNLOAD_TIME_LNopened = true;
        }

        vm.durationCalc = function (item) {
            var tm = item.UN_LOAD_TIME - item.LOAD_TIME;
            item["SECONDS"] = tm;
            item["HOURS"] = parseInt(tm / 3600);
        }

        $scope.PROD_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.PROD_DT_LNopened = true;
        }


        vm.openCheckRollModal = function (item) {

            if (!item.UN_LOAD_TIME) {
                config.appToastMsg("MULTI-005 Please provide unload time");
                return;
            }

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

                    getRollStatusList();
                    getData();
                    getUserData();
                    getDefectTypeList();

                    $scope.cancel = function () {

                        $modalInstance.close();
                    }

                    function getRollStatusList() {
                        return $scope["rollStatusList"] = {
                            optionLabel: "--Select Roll Status-",
                            filter: "startswith",
                            autoBind: true,
                            dataSource: {
                                transport: {
                                    read: function (e) {
                                        DyeingDataService.LookupListData(111).then(function (res) {
                                            e.success(res);
                                        });
                                    }
                                }
                            },
                            dataTextField: "LK_DATA_NAME_EN",
                            dataValueField: "LOOKUP_DATA_ID"
                        };
                    };

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

                    $scope.buPassOtherType = function () {

                        $scope.vmS.IS_ROLL_OK = null;

                    }



                    $scope.disableOtherType = function () {

                        console.log($scope.vmS);
                        if ($scope.vmS.IS_ROLL_OK == "Y") {
                            $scope.vmS.RF_DY_DFCT_TYPE_LST = [];
                            $scope.vmS.REQ_RE_PROC_TYPE_ID = '';
                        }

                        $scope.vmS.BY_PASS = "N"

                    }

                    $scope.updateAll = function (dataOri) {
                        //if (!dataOri.IS_ROLL_OK) {
                        //    config.appToastMsg("MULTI-005 Please select roll status");
                        //    return;
                        //}
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
                        //if (!dataOri.IS_ROLL_OK) {
                        //    config.appToastMsg("MULTI-005 Please select roll status");
                        //    return;
                        //}
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
                                    $scope.cancel();
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
                        item["BY_PASS"] = true;
                        return item;
                    }
                }

            });


            modalInstance.result.then(function (data) {
                vm.searchBatchInfo();
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


        function getBuyerAcList() {

            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }


        vm.checkAllGrid = function () {
            vm.form.TOTAL_WATER = vm.form.TOTAL_WEIGHT * vm.form.LQR_RATIO;

            for (var i = 0; i < vm.recipeItemList.length; i++) {
                vm.PreItemCalcRecord(vm.recipeItemList[i], 1);
            }

        }

        vm.searchBatchInfo = function (pPageNo) {
            vm.showSplash = true;
            DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForStatusUpdate?pPageNo=' + (pPageNo || 1) + '&pMC_BYR_ACC_ID=' + vm.form.MC_BYR_ACC_ID + '&pSTYLE_ORDER_NO=' + (vm.form.STYLE_ORDER_NO || "") + '&pDYE_MACHINE_NO=' + (vm.form.DYE_MACHINE_NO || "") + '&pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || "") + '&pDYE_BT_STS_TYPE_ID=' + vm.form.DYE_BT_STS_TYPE_ID + '&pRF_FAB_PROD_CAT_ID=' + ($state.current.data.RF_FAB_PROD_CAT_ID || null) + '&pLK_CHQ_RL_STS_ID=' + vm.form.LK_CHQ_RL_STS_ID).then(function (res) {
                //if (res.length > 0)
                //    vm.form.totalItems = res[0].TOTAL_REC;

                vm.BatchProgramList = res;
                vm.showSplash = false;
            });
        }

        function GetStatusList() {

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchStatusType').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "BT_STS_TYP_NAME",
                dataValueField: "DYE_BT_STS_TYPE_ID"
            };
        };

        function GetBatchList() {
            DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForStatusUpdate?pPageNo=1&pRF_FAB_PROD_CAT_ID=' + ($state.current.data.RF_FAB_PROD_CAT_ID || null)).then(function (res) {
                //if (res.length > 0)
                //    vm.form.totalItems = res[0].TOTAL_REC;
                vm.BatchProgramList = res
            });
        }


        vm.copyThis = function (obj, indx) {
            var nObj = angular.copy(obj);

            //nObj.LOAD_TIME = $filter('date')("1990-02-02T00:00:01", 'yyyy-MM-ddTHH:mm:ss');
            nObj.LOAD_TIME = new Date();
            nObj.UN_LOAD_TIME = null;
            nObj.IS_EDT_LOCKED = 'N';
            nObj.DYE_BT_STS_TYPE_ID = 14;
            var idx = indx + 1;
            vm.BatchProgramList.splice(idx, "0", nObj);
        }

        vm.removeExtraRecord = function (data) {
            var index = vm.BatchProgramList.indexOf(data);
            vm.BatchProgramList.splice(index, 1);
            config.appToastMsg("MULTI-001 " + data.DYE_BATCH_NO + " has been removed");
        }

        vm.updateThis = function (dataOri) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);
                //data["UN_LOAD_TIME"] = Date.(data.UN_LOAD_TIME);
                console.log(data);

                var ld = data["LOAD_TIME"];
                var uld = data["UN_LOAD_TIME"];

                if (!ld || !uld) {

                    config.appToastMsg("MULTI-005 Please select load & unload time");
                    return;
                }
                vm.showSplash = true;

                if (parseInt(data["REQ_RE_PROC_TYPE_ID"]) == 0)
                    data["REQ_RE_PROC_TYPE_ID"] = "";

                var _ldate = $filter('date')(data.LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss');
                data["LOAD_TIME"] = _ldate;

                var _date = $filter('date')(data.UN_LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss');
                data["UN_LOAD_TIME"] = _date;

                var pdate = moment(vm.form.PROD_DT).add(10, 'h')._d;
                console.log(pdate);

                var _fdate = $filter('date')(pdate, 'yyyy-MM-ddTHH:mm:ss');
                //var _fdate = $filter('date')(vm.form.PROD_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["PROD_DT"] = _fdate;

                return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/UpdateBatchProd').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        vm.searchBatchInfo();
                        vm.showSplash = false;
                    }
                });
            }
        }


        vm.pauseThis = function (dataOri) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);
                //data["UN_LOAD_TIME"] = Date.(data.UN_LOAD_TIME);
                console.log(data);

                var ld = data["LOAD_TIME"];
                var uld = data["UN_LOAD_TIME"];

                if (!ld || !uld) {

                    config.appToastMsg("MULTI-005 Please select load & unload time");
                    return;
                }
                vm.showSplash = true;
                data["DYE_BT_STS_TYPE_ID"] = 5;

                if (parseInt(data["REQ_RE_PROC_TYPE_ID"]) == 0)
                    data["REQ_RE_PROC_TYPE_ID"] = "";

                var _ldate = $filter('date')(data.LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss');
                data["LOAD_TIME"] = _ldate;

                var _date = $filter('date')(data.UN_LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss');
                data["UN_LOAD_TIME"] = _date;

                var pdate = moment(vm.form.PROD_DT).add(10, 'h')._d;
                console.log("AAAA");
                var _fdate = $filter('date')(pdate, 'yyyy-MM-ddTHH:mm:ss');
                data["PROD_DT"] = _fdate;

                return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/UpdateBatchProd').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        vm.searchBatchInfo();
                        vm.showSplash = false;
                    }
                });
            }
        }

        vm.resumeThis = function (dataOri) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                console.log(data);

                var ld = data["LOAD_TIME"];
                var uld = data["UN_LOAD_TIME"];

                if (!ld) {
                    config.appToastMsg("MULTI-005 Please select load time");
                    return;
                }

                vm.showSplash = true;

                data["DYE_BT_STS_TYPE_ID"] = 4;
                data["IS_EDT_LOCKED"] = 'Y';

                var _ldate = $filter('date')(data.LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss');
                data["LOAD_TIME"] = _ldate;

                var _date = $filter('date')(data.UN_LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss');
                data["UN_LOAD_TIME"] = _date;

                if (parseInt(data["REQ_RE_PROC_TYPE_ID"]) == 0)
                    data["REQ_RE_PROC_TYPE_ID"] = "";

                var pdate = moment(vm.form.PROD_DT).add(10, 'h')._d;
                console.log("AAAA");
                var _fdate = $filter('date')(pdate, 'yyyy-MM-ddTHH:mm:ss');
                data["PROD_DT"] = _fdate;

                return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/UpdateBatchProd').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        vm.searchBatchInfo(); fnValidate
                        vm.showSplash = false;
                    }
                });
            }
        }

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                console.log(data);

                var ld = data["LOAD_TIME"];
                var uld = data["UN_LOAD_TIME"];

                if (!ld || !uld) {

                    config.appToastMsg("MULTI-005 Please select load & unload time");
                    return;
                }

                vm.showSplash = true;

                data["DYE_BT_STS_TYPE_ID"] = 6;

                if (parseInt(data["REQ_RE_PROC_TYPE_ID"]) == 0)
                    data["REQ_RE_PROC_TYPE_ID"] = "";

                var _ldate = $filter('date')(data.LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss');
                data["LOAD_TIME"] = _ldate;

                var _date = $filter('date')(data.UN_LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss');
                data["UN_LOAD_TIME"] = _date;

                var pdate = moment(vm.form.PROD_DT).add(10, 'h')._d;
                console.log("AAAA");
                var _fdate = $filter('date')(pdate, 'yyyy-MM-ddTHH:mm:ss');
                //var _fdate = $filter('date')(vm.form.PROD_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["PROD_DT"] = _fdate;

                return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/UpdateBatchProd').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        vm.searchBatchInfo();
                        vm.showSplash = false;
                    }
                });
            }
        };


    }


})();


