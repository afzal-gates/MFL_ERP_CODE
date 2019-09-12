
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('ShadeCheckRollInspectionController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', 'formData', 'Dialog', '$filter', '$modal', ShadeCheckRollInspectionController]);
    function ShadeCheckRollInspectionController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, formData, Dialog, $filter, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.DateTimeFormat = config.appDateTimeFormat;
        vm.TimeFormat = config.appTimeFormat;
        vm.ACTION_DIS = 0;
        vm.REPROCESS_FLAG = 0;
        vm.today = new Date();
        //vm.form = {};

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
        };

        vm.form = formData.DF_RIB_SHADE_RPT_H_ID ? formData : { RP_SL_NO: 1, RPT_BY: cur_user_id, SHD_APRV_DT: vm.today, RCV_DT_FRM_QC: vm.today, IS_FINALIZED: 'N', DYE_BATCH_NO: '' };

        angular.element('#DYE_BATCH_NO').focus();
        //vm.form.IS_FINALIZED = "Y";

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetDefectList(), getUserData(), loadQcRptDtlList(), getQcReturnList(), getResDeptList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        if (formData.DF_RIB_SHADE_RPT_H_ID > 0) {

            DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForProduction?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "")).then(function (res) {

                var prdList = _.filter(res.prd, function (x) { return x.DYE_BT_STS_TYPE_ID > 5 })

                if (prdList.length > 0) {

                    vm.form.DYE_BT_PROD_ID = prdList[0].DYE_BT_PROD_ID;
                    vm.form.REMARKS = prdList[0].REMARKS;

                    vm.BatchProgramList = prdList;
                    vm.BatchFabricList = _.filter(res.fab, function (x) { return x.LK_FBR_GRP_ID < 194 });
                    if (res.batch.length > 0) {
                        console.log(res.batch);
                        vm.form.DYE_STR_REQ_H_ID = res.batch[0].DYE_STR_REQ_H_ID;
                        vm.form.STR_REQ_NO = res.batch[0].STR_REQ_NO;
                        vm.form.STR_REQ_DT = res.batch[0].STR_REQ_DT;
                        vm.form.DYE_MACHINE_ID = res.batch[0].DYE_MACHINE_ID;
                        vm.form.BUYER_NAME_EN = res.batch[0].BUYER_NAME_EN;
                        vm.form.STYLE_NO = res.batch[0].STYLE_NO;
                        vm.form.ORDER_NO = res.batch[0].ORDER_NO;
                        vm.form.COLOR_NAME_EN = res.batch[0].COLOR_NAME_EN;
                        vm.form.LD_RECIPE_NO = res.batch[0].LD_RECIPE_NO;
                        vm.form.COLOR_GRP_NAME_EN = res.batch[0].COLOR_GRP_NAME_EN;
                        vm.form.MC_LD_RECIPE_H_ID = res.batch[0].MC_LD_RECIPE_H_ID;
                        vm.form.LQR_RATIO = "1:" + res.batch[0].LQR_RATIO;
                        vm.form.DYE_MTHD_NAME = res.batch[0].DYE_MTHD_NAME;
                        vm.form.UN_HOLD_DT = res.batch[0].UN_HOLD_DT;
                        vm.form.CHK_ROLL_DT = res.batch[0].CHK_ROLL_DT;

                        vm.form.DF_PROC_TYP_LST = res.batch[0].DF_PROC_TYP_LST ? res.batch[0].DF_PROC_TYP_LST.split(',') : [];
                        vm.form.CK_ROLL_RCV_DT = (res.batch[0].CK_ROLL_RCV_DT || null);
                        vm.form.CK_ROLL_FIN_DT = (res.batch[0].CK_ROLL_FIN_DT || null);
                        vm.form.LK_CHQ_RL_STS_ID = res.batch[0].LK_CHQ_RL_STS_ID;
                        //vm.form.DYE_BATCH_NO_LST = res.batch[0].DYE_LOT_NO == null ? res.batch[0].DYE_BATCH_NO : res.batch[0].DYE_BATCH_NO + "-" + res.batch[0].DYE_LOT_NO;
                        vm.form.DYE_BATCH_NO_LST = res.batch[0].DYE_BATCH_NO;
                        vm.form.DYE_BT_CARD_H_ID = res.batch[0].DYE_BT_CARD_H_ID;
                        vm.form.ACT_BATCH_QTY_LST = res.batch[0].ACT_BATCH_QTY + " Kg";
                        console.log(vm.form);
                        loadQcRptDtlList();
                    }
                    else {

                        config.appToastMsg("MULTI-005 Batch not found!");
                        vm.form = {}
                        angular.element('#DYE_BATCH_NO').focus();
                    }
                }
                else {

                    config.appToastMsg("MULTI-005 Not Ready for Check-Roll Process!");
                    vm.form = {}
                    angular.element('#DYE_BATCH_NO').focus();
                }
            });
        }

        vm.loadFabric = function (item) {
            console.log(item);
        }

        $scope.expandAll = function (expanded) {
            // $scope is required here, hence the injection above, even though we're using "controller as" syntax
            $scope.$broadcast('onExpandAll', {
                expanded: expanded
            });
        }

        vm.clearData = function () {
            $state.go('CheckRollInspection', { pDYE_BATCH_NO: '' }, { reload: true });
        }

        $scope.RCV_DT_FRM_QC_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.RCV_DT_FRM_QC_LNopened = true;
        }

        $scope.SHD_APRV_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.SHD_APRV_DT_LNopened = true;
        }

        vm.selectdeselect = function (item, e) {
            var rtnQty = 0;
            var list = _.filter(vm.QcParamList, function (x) { return x.LK_PARAM_QC_STS_ID == 712 });
            if (list.length > 0) {
                for (var i = 0; i < list.length; i++) {
                    var obj = list[i].KNIT_ITEM_LST;
                    for (var j = 0; j < obj.length; j++) {
                        rtnQty += obj[j].BATCH_QTY;
                    }
                }
            }

            vm.form.BATCH_RP_QTY = rtnQty;
            if (rtnQty > 0) {
                vm.form.MAJ_DFCT_TYPE_ID = 42;
                vm.form.RF_RESP_DEPT_ID = 3;
                vm.form.LK_RP_RTN_TYPE_ID = 680;
            }
            else {
                vm.form.MAJ_DFCT_TYPE_ID = 0;
                vm.form.RF_RESP_DEPT_ID = 0;
                vm.form.LK_RP_RTN_TYPE_ID = 0;
            }
        }

        function GetDefectList() {

            DyeingDataService.getDataByFullUrl('/api/security/UserMap/GetFabDfctTypeMapByID?pLK_FAB_INSP_TYPE_ID=708&pOption=3002').then(function (res) {
                //DyeingDataService.getDataByFullUrl('/api/common/getDyeDfctTypeList').then(function (res) {
                console.log(res);
                vm.defectList = res
            });
        };

        function getUserData() {
            return vm.userList = {
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

            //$("#customers").kendoDropDownList(vm.userList);
        }

        function loadQcRptDtlList() {

            if ($stateParams.pDF_RIB_SHADE_RPT_H_ID > 0) {
                DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfBTQcParamList?pDYE_BT_CARD_H_ID=' + (vm.form.DYE_BT_CARD_H_ID || 0) + '&pDF_RIB_SHADE_RPT_H_ID=' + ($stateParams.pDF_RIB_SHADE_RPT_H_ID || 0)).then(function (res) {
                    if (res.length == 0) {
                        DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQcParamTypeList').then(function (resD) {
                            var prmList = _.filter(resD, function (x) { return x.RF_RESP_DEPT_ID == 3 && x.RF_DF_QC_PARAM_TYPE_ID != 3 })
                            vm.QcParamList = prmList;
                        });
                    }
                    else {
                        var prmList = _.filter(res, function (x) { return x.RF_RESP_DEPT_ID == 3 && x.RF_DF_QC_PARAM_TYPE_ID != 3 })
                        vm.QcParamList = prmList;
                    }
                });
            }
            else {
                DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfBTQcParamList?pDYE_BT_CARD_H_ID=' + (vm.form.DYE_BT_CARD_H_ID || 0)).then(function (res) {
                    if (res.length == 0) {
                        DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQcParamTypeList').then(function (resD) {
                            var prmList = _.filter(resD, function (x) { return x.RF_RESP_DEPT_ID == 3 && x.RF_DF_QC_PARAM_TYPE_ID != 3 })
                            vm.QcParamList = prmList;
                        });
                    }
                    else {
                        var prmList = _.filter(res, function (x) { return x.RF_RESP_DEPT_ID == 3 && x.RF_DF_QC_PARAM_TYPE_ID != 3 })
                        vm.QcParamList = prmList;
                    }
                });
            }
            DyeingDataService.LookupListData(145).then(function (res) {
                var qcList = _.filter(res, function (x) { return x.LOOKUP_DATA_ID == 711 || x.LOOKUP_DATA_ID == 712 || x.LOOKUP_DATA_ID == 713 })
                vm.qcParamStatusList = qcList;
            });

        }


        function getQcReturnList() {

            return vm.qcReturnList = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(139).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };



        function getResDeptList() {

            return vm.resDptList = {
                optionLabel: "-- Select Department --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetRespDeptListByID?pMULTI_DEPT_LST=3,5').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "RESP_DEPT_NAME",
                dataValueField: "RF_RESP_DEPT_ID"
            };
        };



        function GetMultipleDeptList() {
            return vm.getMultipleDeptData = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        var MULTI_DEPT_LST = "";
                        var lst = _.filter(vm.defectList, function (x) { return x.LAST_UPDATED_BY > 0 && x.IS_MULTI_DEPT_ALLOW == 'Y' })

                        for (var i = 0; i < lst.length; i++) {
                            if (i > 0)
                                MULTI_DEPT_LST = RF_FB_DFCT_TYPE_LST + "," + lst[i].MULTI_DEPT_LST;
                            else
                                MULTI_DEPT_LST = lst[i].MULTI_DEPT_LST;
                        }

                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetRespDeptListByID?pMULTI_DEPT_LST=' + (MULTI_DEPT_LST || '')).then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.getMultipleDept = function () {
            vm.getMultipleDeptData.read();
        }

        vm.searchBatchInfo = function (ScanValue) {
            if ($stateParams.pDF_RIB_SHADE_RPT_H_ID == 0) {
                if (ScanValue == "1")
                    ScanValue = (vm.form.DYE_BATCH_NO || vm.form.FAB_ROLL_NO);
                if (ScanValue)
                    if (ScanValue.length > 9) {

                        ScanValue = ScanValue.toUpperCase();
                        console.log(ScanValue);
                        vm.showSplash = true;

                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForProduction?pDYE_BATCH_NO=' + (ScanValue.trim() || "")).then(function (res) {
                            
                            var prdList = _.filter(res.prd, function (x) { return x.DYE_BT_STS_TYPE_ID > 5 })

                            if (prdList.length > 0) {

                                vm.form.DYE_BT_PROD_ID = prdList[0].DYE_BT_PROD_ID;
                                vm.form.REMARKS = prdList[0].REMARKS;

                                vm.BatchProgramList = prdList;
                               
                                if (res.batch.length > 0) {
                                    console.log(res.batch);
                                    vm.form.DYE_STR_REQ_H_ID = res.batch[0].DYE_STR_REQ_H_ID;
                                    vm.form.STR_REQ_NO = res.batch[0].STR_REQ_NO;
                                    vm.form.STR_REQ_DT = res.batch[0].STR_REQ_DT;
                                    vm.form.DYE_MACHINE_ID = res.batch[0].DYE_MACHINE_ID;
                                    vm.form.BUYER_NAME_EN = res.batch[0].BUYER_NAME_EN;
                                    vm.form.STYLE_NO = res.batch[0].STYLE_NO;
                                    vm.form.ORDER_NO = res.batch[0].ORDER_NO;
                                    vm.form.COLOR_NAME_EN = res.batch[0].COLOR_NAME_EN;
                                    vm.form.LD_RECIPE_NO = res.batch[0].LD_RECIPE_NO;
                                    vm.form.COLOR_GRP_NAME_EN = res.batch[0].COLOR_GRP_NAME_EN;
                                    vm.form.MC_LD_RECIPE_H_ID = res.batch[0].MC_LD_RECIPE_H_ID;
                                    vm.form.LQR_RATIO = "1:" + res.batch[0].LQR_RATIO;
                                    vm.form.DYE_MTHD_NAME = res.batch[0].DYE_MTHD_NAME;
                                    vm.form.UN_HOLD_DT = res.batch[0].UN_HOLD_DT;
                                    vm.form.CHK_ROLL_DT = res.batch[0].CHK_ROLL_DT;
                                    vm.form.RP_SL_NO = res.batch[0].RP_SL_NO;

                                    vm.form.DF_PROC_TYP_LST = res.batch[0].DF_PROC_TYP_LST ? res.batch[0].DF_PROC_TYP_LST.split(',') : [];
                                    vm.form.CK_ROLL_RCV_DT = (res.batch[0].CK_ROLL_RCV_DT || null);
                                    vm.form.CK_ROLL_FIN_DT = (res.batch[0].CK_ROLL_FIN_DT || null);
                                    vm.form.LK_CHQ_RL_STS_ID = res.batch[0].LK_CHQ_RL_STS_ID;
                                    //vm.form.DYE_BATCH_NO_LST = res.batch[0].DYE_LOT_NO == null ? res.batch[0].DYE_BATCH_NO : res.batch[0].DYE_BATCH_NO + "-" + res.batch[0].DYE_LOT_NO;
                                    vm.form.DYE_BATCH_NO_LST = res.batch[0].DYE_BATCH_NO;
                                    vm.form.DYE_BT_CARD_H_ID = res.batch[0].DYE_BT_CARD_H_ID;
                                    vm.form.ACT_BATCH_QTY_LST = res.batch[0].ACT_BATCH_QTY + " Kg";
                                    console.log(vm.form);
                                    loadQcRptDtlList();

                                    DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchFabricForProduction?pDYE_BATCH_NO=' + (ScanValue.trim() || "") + '&pOption=3011').then(function (res) {

                                        vm.BatchFabricList = _.filter(res, function (x) { return x.LK_FBR_GRP_ID < 193 });
                                    });

                                }
                                else {

                                    config.appToastMsg("MULTI-005 Batch not found!");
                                    vm.form = {}
                                    angular.element('#DYE_BATCH_NO').focus();
                                }
                            }
                            else {

                                config.appToastMsg("MULTI-005 Not Ready for Check-Roll Process!");
                                vm.form = {}
                                angular.element('#DYE_BATCH_NO').focus();
                            }
                            vm.showSplash = false;
                        });
                    }
            }
        }

        function getRollStatusList() {

            DyeingDataService.LookupListData(111).then(function (res) {
                vm.rollStatusList = res
            });
        };



        function getDfProcessTypeData() {
            return vm.processTypeList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        //DyeingDataService.getDataByFullUrl('/api/security/UserMap/GetFabDfctTypeMapByID?pLK_FAB_INSP_TYPE_ID=709&pOption=3002').then(function (res) {
                        DyeingDataService.getDataByUrl('/DFProcessType/SelectAll').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.reset = function () {

            $state.go($state.current, { pDF_RIB_SHADE_RPT_H_ID: 0 }, { inherit: false, reload: true });
        };



        vm.gridOptionsFabric = {
            pageable: false,
            scrollable: false,
            columns: [
                { field: "SL_NO", title: "SL No", type: "string", width: "10%", template: "<span class='row-number'></span>" },
                { field: "FABRIC_DESC", title: "Fabric Description", type: "string", width: "100%" },
            ],
            dataBound: function () {
                var rows = this.items();
                $(rows).each(function () {
                    var index = $(this).index() + 1;
                    var rowLabel = $(this).find(".row-number");
                    $(rowLabel).html(index);

                    var rowLabel = $(this).find(".row-unit");
                    $(rowLabel).html("Kg");
                });
            }
        };

        vm.gridOptionsBatch = {
            pageable: false,
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
            height: '200px',
            scrollable: true,
            columns: [
                { field: "SL_NO", title: "SL No", type: "string", width: "7%", template: "<span class='row-number'></span>" },

                { field: "DYE_MACHINE_NO", title: "M/C No", type: "string", width: "10%", },
                //{ field: "CK_ROLL_RCV_DT", title: "M/C No", type: "string", width: "10%", template: "#if(CK_ROLL_FIN_DT === null) {#<div class='customClass'>#:null#</div>#} else{##:kendo.toString(new Date(CK_ROLL_FIN_DT), 'dd-MM-yy hh:mmtt')##}#" },
                { field: "LOAD_TIME", title: "Load Time", type: "date", template: "#= kendo.toString(new Date(LOAD_TIME), 'dd-MM-yy hh:mmtt') #", width: "10%" },
                { field: "UN_LOAD_TIME", title: "Unload Time", type: "date", template: "#= kendo.toString(new Date(UN_LOAD_TIME), 'dd-MM-yy hh:mmtt') #", width: "10%" },
                { field: "CK_ROLL_RCV_DT", title: "BT. Rcv", type: "date", template: "#if(CK_ROLL_RCV_DT === null) {#<div class='customClass'>#:null#</div>#} else{##:kendo.toString(new Date(CK_ROLL_RCV_DT), 'dd-MM-yy hh:mmtt')##}#", width: "10%" },
                { field: "CK_ROLL_FIN_DT", title: "DF. Rcv", type: "date", template: "#if(CK_ROLL_FIN_DT === null) {#<div class='customClass'>#:null#</div>#} else{##:kendo.toString(new Date(CK_ROLL_FIN_DT), 'dd-MM-yy hh:mmtt')##}#", width: "10%" },

                { field: "TIME_DIFF", title: "Duration", type: "string", width: "10%", },
                { field: "RE_PROC_TYPE_NAME", title: "Type", type: "string", width: "10%", },
                { field: "BT_STS_TYP_NAME", title: "Status", type: "string", width: "15%", },

            ],
            dataBound: function () {
                var rows = this.items();
                $(rows).each(function () {
                    var index = $(this).index() + 1;
                    var rowLabel = $(this).find(".row-number");
                    $(rowLabel).html(index);

                    var rowLabel = $(this).find(".row-unit");
                    $(rowLabel).html("Kg");
                });
            }
            //editable: true
        };

        vm.rowIndex = 0;

        vm.submitAll = function (dataOri, id) {

            Dialog.confirm('Are you want to process this action!!!".', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     if (vm.form.DYE_BT_PROD_ID > 0) {
                         var data = angular.copy(dataOri);

                         var _rcvdate = $filter('date')(data.RCV_DT_FRM_QC, 'yyyy-MM-ddTHH:mm:ss');
                         data["RCV_DT_FRM_QC"] = _rcvdate;

                         var _ldate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                         data["SHD_APRV_DT"] = _ldate;

                         data["IS_FINALIZED"] = id;

                         var iList = _.filter(vm.QcParamList, function (x) { return x.KNIT_ITEM_LST.length > 0 });
                         var prmList = _.filter(vm.QcParamList, function (x) { return x.LK_PARAM_QC_STS_ID > 0 });
                         var fablst = [];
                         if (iList) {
                             fablst = iList[0].KNIT_ITEM_LST;

                             for (var i = 1; i < iList.length; i++) {
                                 for (var j = 0; j < iList[i].KNIT_ITEM_LST.length; j++) {
                                     fablst.push(iList[i].KNIT_ITEM_LST[j]);
                                 }
                             }
                         }

                         var finalQC = _.filter(prmList, function (x) { return x.LK_PARAM_QC_STS_ID == 712 });
                         var waitList = _.filter(prmList, function (x) { return x.LK_PARAM_QC_STS_ID == 713 });

                         if (finalQC.length > 0) {

                             data["RE_PROC_TYPE_ID"] = 3;
                             data["KNT_QC_STS_TYPE_ID"] = 3;

                         }
                         else {
                             if (waitList.length > 0) {
                                 data["KNT_QC_STS_TYPE_ID"] = 3;
                                 data["RE_PROC_TYPE_ID"] = 3;
                             }
                             else
                                 data["KNT_QC_STS_TYPE_ID"] = 1;
                         }

                         if (id == 'Y' && waitList.length > 0) {
                             config.appToastMsg("MULTI-005 Before Finalize You Must Update Wait Status!!!");
                             return;
                         }

                         console.log(fablst);

                         data["XML_RPT_D"] = DyeingDataService.xmlStringShort(fablst.map(function (o) {
                             return {
                                 DF_RIB_SHADE_RPT_D_ID: o.DF_RIB_SHADE_RPT_D_ID == null ? 0 : o.DF_RIB_SHADE_RPT_D_ID,
                                 DF_RIB_SHADE_RPT_H_ID: o.DF_RIB_SHADE_RPT_H_ID == null ? 0 : o.DF_RIB_SHADE_RPT_H_ID,
                                 KNT_STYL_FAB_ITEM_H_ID: o.KNT_STYL_FAB_ITEM_H_ID == null ? 0 : o.KNT_STYL_FAB_ITEM_H_ID,
                                 BATCH_QTY: o.BATCH_QTY,
                                 FINIS_QTY: o.FINIS_QTY == null ? 0 : o.FINIS_QTY,
                                 ROLL_QTY: o.ROLL_QTY == null ? 0 : o.ROLL_QTY,
                                 QTY_MOU_ID: 3
                             }
                         }));

                         data["XML_PARAM"] = DyeingDataService.xmlStringShort(prmList.map(function (o) {
                             return {
                                 DF_BT_QC_CHQ_LST_ID: o.DF_BT_QC_CHQ_LST_ID == null ? 0 : o.DF_BT_QC_CHQ_LST_ID,
                                 DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID == null ? 0 : o.DYE_BT_CARD_H_ID,
                                 RF_DF_QC_PARAM_TYPE_ID: o.RF_DF_QC_PARAM_TYPE_ID == null ? 0 : o.RF_DF_QC_PARAM_TYPE_ID,
                                 LK_PARAM_QC_STS_ID: o.LK_PARAM_QC_STS_ID,
                                 CHQ_IGNR_RSN_DESC: o.CHQ_IGNR_RSN_DESC
                             }
                         }));

                         return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/OtherCheckRoll/Save').then(function (res) {

                             if (res.success === false) {
                                 vm.errors = res.errors;
                             }
                             else {
                                 res['data'] = angular.fromJson(res.jsonStr);
                                 config.appToastMsg(res.data.PMSG);
                                 $state.go($state.current, { pDF_RIB_SHADE_RPT_H_ID: (res.data.OPDF_RIB_SHADE_RPT_H_ID || 0) }, { inherit: false, reload: true });
                             }
                         });
                     }
                 });
        }

    }


})();

