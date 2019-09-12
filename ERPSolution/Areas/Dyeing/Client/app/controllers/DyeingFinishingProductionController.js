(function () {
    'use strict';

    angular.module('multitex.dyeing').directive('autoFocus', function ($timeout) {
        return {
            restrict: 'AC',
            link: function (_scope, _element) {
                $timeout(function () {
                    _element[0].focus();
                }, 0);
            }
        };
    });

    angular.module('multitex.dyeing').controller('DyeingFinishingProductionController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', 'Dialog', '$filter', '$ngConfirm', DyeingFinishingProductionController]);
    function DyeingFinishingProductionController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, Dialog, $filter, $ngConfirm) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        $scope.back_UI = 0;

        vm.form = { LK_DIA_TYPE_ID: 0, PROD_DT: vm.today };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getOperatorList(), getShiftList(), GetFabGroupList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.showProductionUI = function (item, proc) {

            console.log(item);
            $scope.MC_TYPE = item.DF_MC_TYPE_NAME_EN;
            vm.form.DF_PROC_TYPE_NAME = proc.DF_PROC_TYPE_NAME;
            vm.form.DF_MACHINE_NO = item.DF_MACHINE_NO;
            vm.form.MC_IMAGE = item.MC_IMAGE;

            vm.form.DF_PROC_TYPE_ID = proc.DF_PROC_TYPE_ID;
            vm.form.DF_MACHINE_ID = item.DF_MACHINE_ID;

            if ($scope.IS_TUBE == true)
                $scope.back_UI = 1;
            if ($scope.IS_OPEN == true)
                $scope.back_UI = 2;

            $scope.IS_PRODUCTION = true;
            $scope.IS_TUBE = false;
            $scope.IS_OPEN = false;
        }

        vm.backPrevious = function () {

            $scope.IS_PRODUCTION = false;
            if ($scope.back_UI == 1)
                $scope.IS_TUBE = true;
            else if ($scope.back_UI == 2)
                $scope.IS_OPEN = true;
        }
        vm.df_data = [];

        vm.getDfProcessTypeData = function (pDYE_BATCH_PLAN_ID) {

            return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDFProcessData?pDYE_BATCH_PLAN_ID=' + pDYE_BATCH_PLAN_ID).then(function (res) {
                vm.df_data = res;
            });

        };



        vm.getProcessTypeList = function () {

            var LK_DIA_TYPE_ID = 327;
            if ($scope.IS_TUBE)
                LK_DIA_TYPE_ID = 328;

            vm.form['LK_DIA_TYPE_ID'] = LK_DIA_TYPE_ID;

            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/GetDFProcessTypeByID?pLK_DIA_TYPE_ID=' + LK_DIA_TYPE_ID).then(function (res) {
                vm.ProcessTypeList = res;
            });
        }

        vm.changeDIA = function (id) {

            var LK_DIA_TYPE_ID = 327;
            if (id == 327)
                LK_DIA_TYPE_ID = 328;

            if (vm.Title.match("Open Line"))
                vm.Title = "Dyeing Finishing Production Open Line (" + angular.copy($scope.DIA_INV) + " DIA)";
            else
                vm.Title = "Dyeing Finishing Production Tube Line (" + angular.copy($scope.DIA_INV) + " DIA)";

            if ($scope.DIA_INV == 'Open') {
                $scope.DIA_INV = 'Tube';
            }
            else {
                $scope.DIA_INV = 'Open';
            }
            vm.form['LK_DIA_TYPE_ID'] = LK_DIA_TYPE_ID;
        }

        vm.getMachineList = function () {
            //var DF_MC_TYPE_ID = 1;
            //if ($scope.IS_TUBE)
            //    DF_MC_TYPE_ID = 2;

            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/GetMachineByProcessType?pDYE_BT_CARD_H_ID=' + (vm.form.DYE_BT_CARD_H_ID || null) + '&pDF_SC_PT_ISS_H_ID=' + (vm.form.DF_SC_PT_ISS_H_ID || null) + '&pDF_BT_SUB_LOT_ID=' + (vm.form.DF_BT_SUB_LOT_ID || null) + '&pLK_DIA_TYPE_ID=' + (vm.form.LK_DIA_TYPE_ID || null)).then(function (res) {
                //DyeingDataService.getDataByFullUrl('/api/Dye/DFMachine/SelectAll').then(function (res) {
                console.log(res);
                var list = _.filter(res, function (x) { return x.DF_MC_TYPE_ID === $scope.DF_MC_TYPE_ID });
                console.log(list);
                vm.machineList = list;
            });
        }


        function GetFabGroupList() {
            return vm.FabGroupList = {
                optionLabel: "-- Select Fabric Group --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(45).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };


        vm.getProductionList = function () {
            var batchNo = vm.form.DYE_BATCH_NO;
            var dia = vm.form.LK_DIA_TYPE_ID;

            var pLK_FBR_GRP_LST = !vm.form.LK_FBR_GRP_LST ? '' : vm.form.LK_FBR_GRP_LST.join(',');

            if (vm.form.DYE_BATCH_NO)
                if (vm.form.DYE_BATCH_NO.length > 9) {

                    vm.showSplash = true;
                    if (!vm.form.DYE_BATCH_NO.match('DSC')) {
                        console.log("BATCH");
                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForDF?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || "") + '&pLK_DIA_TYPE_ID=' + (vm.form.IS_CC == true ? null : (vm.form.LK_DIA_TYPE_ID || null)) + '&pLK_FBR_GRP_LST=' + (pLK_FBR_GRP_LST || '')).then(function (res) {
                            if (res.length > 0) {

                                vm.form.DYE_BT_CARD_H_ID = res[0].DYE_BT_CARD_H_ID;
                                vm.form.BUYER_NAME_EN = res[0].BUYER_NAME_EN;
                                vm.form.STYLE_NO = res[0].STYLE_NO;
                                vm.form.ORDER_NO = res[0].ORDER_NO;
                                vm.form.COLOR_NAME_EN = res[0].COLOR_NAME_EN;
                                vm.form.DYE_LOT_NO = res[0].DYE_LOT_NO;
                                vm.form.RF_FAB_TYPE_ID = res[0].RF_FAB_TYPE_ID;
                                vm.form.FAB_TYPE_NAME = res[0].FAB_TYPE_NAME;
                                vm.form.FAB_COLOR_ID = res[0].FAB_COLOR_ID;
                                vm.form.MC_FAB_PROD_ORD_H_ID = res[0].MC_FAB_PROD_ORD_H_ID;
                                vm.form.BATCH_QTY = res[0].BATCH_QTY;
                                vm.form.DIA_TYPE_NAME = res[0].DIA_TYPE_NAME;
                                vm.form.DF_BT_SUB_LOT_ID = res[0].DF_BT_SUB_LOT_ID;

                                vm.form.SPECIAL_INSTRUCTION = res[0].SPECIAL_INSTRUCTION;
                                vm.form.DIA_TYPE = $scope.MC_TYPE;
                                vm.form.DYE_BATCH_QTY = res[0].ACT_BATCH_QTY + " Kg";

                                vm.form.ACT_BATCH_QTY = res[0].ACT_BATCH_QTY;

                                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchIssueFabricByBatchNo?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "") + '&pDYE_STR_REQ_H_ID=' + (vm.form.DYE_STR_REQ_H_ID || null) + '&pOption=3006').then(function (resfab) {
                                    vm.BatchFabricList = resfab;
                                });

                                vm.getDfProcessTypeData(res[0].DYE_BATCH_PLAN_ID);
                                vm.getMachineList();
                            }
                            else {
                                config.appToastMsg("MULTI-005 Not Ready for Dyeing Finishing Production(QC Not Ready)!!!");
                                vm.form = { DYE_BATCH_NO: batchNo, PROD_DT: vm.today, LK_DIA_TYPE_ID: dia }
                                vm.machineList = [];
                                //vm.getDfProcessTypeData(0);
                                //vm.getMachineList();
                            }

                        });

                        DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/GetDFProductionByBatchNo?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || '') + '&pLK_DIA_TYPE_ID=' + (vm.form.LK_DIA_TYPE_ID || null) + '&pLK_FBR_GRP_LST=' + (pLK_FBR_GRP_LST || '')).then(function (res) {
                            vm.productionList = res;
                            vm.showSplash = false;
                        });
                    }
                    else {
                        console.log(vm.form.DYE_BATCH_NO);
                        console.log("BATCH");

                        DyeingDataService.getDataByFullUrl('/api/dye/ScPreTreatment/GetScPTIssueD1ForDF?pSC_PRG_NO=' + (vm.form.DYE_BATCH_NO || "") + '&pLK_DIA_TYPE_ID=' + (vm.form.LK_DIA_TYPE_ID || null) + '&pLK_FBR_GRP_LST=' + (pLK_FBR_GRP_LST || '')).then(function (res) {
                            if (res.length > 0) {

                                vm.form.DF_SC_PT_ISS_H_ID = res[0].DF_SC_PT_ISS_H_ID;
                                vm.form.BUYER_NAME_EN = res[0].BUYER_NAME_EN;
                                vm.form.STYLE_NO = res[0].STYLE_NO;
                                vm.form.ORDER_NO = res[0].ORDER_NO;
                                vm.form.COLOR_NAME_EN = res[0].COLOR_NAME_EN;
                                vm.form.DYE_LOT_NO = res[0].DYE_LOT_NO;
                                vm.form.RF_FAB_TYPE_ID = res[0].RF_FAB_TYPE_ID;
                                vm.form.FAB_TYPE_NAME = res[0].FAB_TYPE_NAME;
                                vm.form.FAB_COLOR_ID = res[0].FAB_COLOR_ID;
                                vm.form.MC_FAB_PROD_ORD_H_ID = res[0].MC_FAB_PROD_ORD_H_ID;
                                vm.form.DF_BT_SUB_LOT_ID = res[0].DF_BT_SUB_LOT_ID;
                                vm.form.BATCH_QTY = res[0].ISS_QTY;
                                vm.form.DIA_TYPE_NAME = res[0].DIA_TYPE_NAME;

                                vm.form.ACT_BATCH_QTY = res[0].ISS_QTY;

                                vm.form.DYE_BATCH_QTY = res[0].ISS_QTY + " Kg";

                                vm.form.SPECIAL_INSTRUCTION = res[0].DF_PROC_TYPE_NAME_LST;
                                vm.form.DIA_TYPE = $scope.MC_TYPE;


                                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchIssueFabricByBatchNo?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "") + '&pDYE_STR_REQ_H_ID=' + (vm.form.DYE_STR_REQ_H_ID || null) + '&pOption=3006').then(function (resfab) {
                                    vm.BatchFabricList = resfab;
                                });

                                //vm.getDfProcessTypeData(res[0].DF_SC_PT_ISS_H_ID);
                                vm.getMachineList();
                            }
                            else {
                                config.appToastMsg("MULTI-005 Not Ready for Dyeing Finishing Production(Pre-Treatment Not Ready)!!!");
                                vm.form = { DYE_BATCH_NO: batchNo, PROD_DT: vm.today, LK_DIA_TYPE_ID: dia }
                                vm.machineList = [];
                                //vm.getDfProcessTypeData(0);
                                //vm.getMachineList();
                            }
                        });

                        DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/GetDFProductionByBatchNo?pSC_PRG_NO=' + (vm.form.DYE_BATCH_NO || '') + '&pLK_DIA_TYPE_ID=' + (vm.form.LK_DIA_TYPE_ID || null) + '&pLK_FBR_GRP_LST=' + (pLK_FBR_GRP_LST || '')).then(function (res) {
                            vm.productionList = res;
                            vm.showSplash = false;
                        });
                    }
                }
        }


        var CurShiftData = {};

        function getShifData(res) {

            CurShiftData = _.find(res, function (o) {
                return o.IS_SCHE_ACTIVE == 'Y';
            });
            if (CurShiftData) {
                vm.form.HR_SCHEDULE_H_ID = CurShiftData.HR_SCHEDULE_H_ID;
                //if (pKNT_MACHINE_ID) {
                //    setOperatorData(pKNT_MACHINE_ID, CurShiftData.HR_SCHEDULE_H_ID);
                //}

            };
        };


        function loadShift() {
            DyeingDataService.getDataByFullUrl('/api/Knit/KnitPlan/getShiftDataByPlanId?pHR_SHIFT_PLAN_ID=2').then(function (res) {
                console.log("SSSS");
                vm.shifDataDs = new kendo.data.DataSource({
                    data: res
                });
                getShifData(res);
            });
        }

        vm.activeDIA = function (id) {
            $scope.IS_DIA = true;
            $scope.IS_MAIN = true;
            $scope.DF_MC_TYPE_ID = id;
            if (id == 1) {
                vm.Title = vm.Title + " Open Line";
            }
            else {
                vm.Title = vm.Title + " Tube Line";
            }
        }

        vm.activeOpen = function () {

            $scope.IS_DIA = false;

            $scope.IS_OPEN = true;
            $scope.IS_TUBE = false;
            $scope.IS_MAIN = true;
            $scope.DIA_INV = 'Tube';
            loadShift();
            vm.Title = vm.Title + " (Open Dia)";

            if ($scope.IS_TUBE == true)
                $scope.back_UI = 1;
            if ($scope.IS_OPEN == true)
                $scope.back_UI = 2;

            $scope.IS_PRODUCTION = true;
            //$scope.IS_TUBE = false;
            //$scope.IS_OPEN = false;

            vm.getProcessTypeList();
            //vm.getMachineList();

        }

        vm.activeTube = function () {

            $scope.IS_DIA = false;
            $scope.IS_OPEN = false;
            $scope.IS_TUBE = true;
            $scope.IS_MAIN = true;
            $scope.DIA_INV = 'Open';

            loadShift();
            vm.Title = vm.Title + " (Tube Dia)";

            if ($scope.IS_TUBE == true)
                $scope.back_UI = 1;
            if ($scope.IS_OPEN == true)
                $scope.back_UI = 2;

            $scope.IS_PRODUCTION = true;
            //$scope.IS_TUBE = false;
            //$scope.IS_OPEN = false;

            vm.getProcessTypeList();
            //vm.getMachineList();
        }



        function getOperatorList() {

            vm.operatorOption = {
                optionLabel: "-- Select Operator--",
                filter: "contains",
                autoBind: true,
                dataTextField: "EMP_FULL_NAME_EN",
                dataValueField: "HR_EMPLOYEE_ID"
            };

            return vm.operatorDataSource = new kendo.data.DataSource({
                serverFiltering: false,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/Dye/DFProduction/SelectDfQcUser?pLK_DF_EMP_TYPE_ID=' + (vm.form.LK_DF_EMP_TYPE_ID || 719);

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);
                            for (var x = 0; x < res.length; x++) {
                                var SO = res[x].EMP_FULL_NAME_EN + " (" + res[x].EMPLOYEE_CODE + ")";
                                res[x].EMP_FULL_NAME_EN = SO;
                            }
                            console.log(res);
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        function getShiftList() {

            return vm.shiftList = {
                optionLabel: "--Select Shift--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/knit/KntMachinOprAssign/GetKnitScheduleList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "SCHEDULE_NAME_EN",
                dataValueField: "HR_SCHEDULE_H_ID"
            };
        };



        vm.submitAll = function (dataOri, status) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                Dialog.confirm('Are you want to process this data!!! "' + data.DYE_BATCH_NO + '.', 'Attention', ['Yes', 'No'])
                .then(function () {
                    //var pform = angular.copy(vm.form);

                    data["IS_FINALIZED"] = status;
                    data["LK_FBR_GRP_LST"] = !data.LK_FBR_GRP_LST ? '' : data.LK_FBR_GRP_LST.join(',');

                    var _isudate = $filter('date')(data.PROD_DT, 'yyyy-MM-ddTHH:mm:ss');
                    data["PROD_DT"] = _isudate;


                    var mc = _.filter(vm.machineList, function (x) { return x.IS_SELECT === true })[0];

                    var fabList = _.filter(vm.BatchFabricList, function (x) { return x.LK_DIA_TYPE_ID == (vm.form.LK_DIA_TYPE_ID || 392) })

                    data["XML_PROD_D"] = DyeingDataService.xmlStringShort(fabList.map(function (o) {
                        return {
                            DF_BT_PROD_FAB_ID: o.DF_BT_PROD_FAB_ID == null ? 0 : o.DF_BT_PROD_FAB_ID,
                            DF_BT_PROD_ID: o.DF_BT_PROD_ID == 0 ? null : o.DF_BT_PROD_ID,
                            KNT_STYL_FAB_ITEM_H_ID: o.KNT_STYL_FAB_ITEM_H_ID == 0 ? null : o.KNT_STYL_FAB_ITEM_H_ID,
                            QTY_MOU_ID: 3,
                            FINIS_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                            RQD_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                        }
                    }));

                    data["DF_PROC_TYPE_ID"] = mc.DF_PROC_TYPE_ID;
                    data["DF_MACHINE_ID"] = mc.DF_MACHINE_ID;
                    data["BT_FIN_QTY"] = data.BATCH_QTY;
                    data["DYE_BT_STS_TYPE_ID"] = mc.DYE_BT_STS_TYPE_ID;

                    var xOPERATOR_ID = data.OPERATOR_ID;
                    var xHR_SCHEDULE_H_ID = data.HR_SCHEDULE_H_ID;
                    var xLK_DIA_TYPE_ID = data.LK_DIA_TYPE_ID;

                    console.log(mc);
                    var list = mc.NXT_BT_STS_TYPE_LST.split(',');
                    console.log(list);

                    if (list.length > 1) {
                        $ngConfirm({
                            title: 'Take Action',
                            contentUrl: 'form.html',
                            buttons: {
                                getPassCode: {
                                    text: 'OK',
                                    disabled: true,
                                    btnClass: 'btn-green',
                                    action: function (scope) {

                                        if (scope.NXT_BT_STS_TYPE_ID > 0) {

                                            data["NXT_BT_STS_TYPE_ID"] = scope.NXT_BT_STS_TYPE_ID;

                                            return DyeingDataService.saveDataByUrl(data, '/DFProduction/Save').then(function (res) {
                                                if (res.success === false) {
                                                    vm.form = { LK_DIA_TYPE_ID: xLK_DIA_TYPE_ID, PROD_DT: vm.today, HR_SCHEDULE_H_ID: xHR_SCHEDULE_H_ID, OPERATOR_ID: xOPERATOR_ID };
                                                    vm.errors = res.errors;
                                                }
                                                else {
                                                    res['data'] = angular.fromJson(res.jsonStr);
                                                    config.appToastMsg(res.data.PMSG);
                                                    //$scope.IS_MAIN = false;
                                                    //$scope.IS_PRODUCTION = false;

                                                    $scope.IS_PRODUCTION = true;
                                                    if ($scope.back_UI == 1)
                                                        $scope.IS_TUBE = true;
                                                    else if ($scope.back_UI == 2)
                                                        $scope.IS_OPEN = true;

                                                    var LK_DIA_TYPE_ID = 327;
                                                    if ($scope.IS_TUBE)
                                                        LK_DIA_TYPE_ID = 328;
                                                    $scope.IS_MAIN = true;

                                                    vm.form = { LK_DIA_TYPE_ID: xLK_DIA_TYPE_ID, PROD_DT: vm.today, HR_SCHEDULE_H_ID: xHR_SCHEDULE_H_ID, OPERATOR_ID: xOPERATOR_ID };
                                                    vm.productionList = [];
                                                    vm.machineList = [];
                                                    angular.element('#DYE_BATCH_NO').focus();
                                                }
                                            });
                                        }
                                        else {

                                            config.appToastMsg("MULTI-005 Please Select Next Status!!!");
                                            return;
                                        }
                                    }
                                },
                                later: function () {
                                    return;
                                }
                            },
                            onScopeReady: function (scope) {
                                var self = this;

                                scope.statusTypeList = {
                                    optionLabel: "-- Select Status --",
                                    filter: "contains",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchStatusType').then(function (res) {
                                                    var slist = _.filter(res, function (x) { return x.DYE_BT_STS_TYPE_ID == list[0] });
                                                    for (var i = 1; i < list.length; i++) {
                                                        var obj = _.filter(res, function (x) { return x.DYE_BT_STS_TYPE_ID == list[i] })[0];
                                                        slist.splice(i, "0", obj);
                                                    }
                                                    e.success(slist);
                                                });
                                            }
                                        }
                                    },
                                    dataTextField: "BT_STS_TYP_NAME",
                                    dataValueField: "DYE_BT_STS_TYPE_ID"
                                };

                                scope.textChange = function () {
                                    if (scope.NXT_BT_STS_TYPE_ID > 0)
                                        self.buttons.getPassCode.setDisabled(false);
                                    else
                                        self.buttons.getPassCode.setDisabled(true);
                                }
                            }
                        });
                    }
                    else {

                        return DyeingDataService.saveDataByUrl(data, '/DFProduction/Save').then(function (res) {
                            if (res.success === false) {
                                vm.form = { LK_DIA_TYPE_ID: xLK_DIA_TYPE_ID, PROD_DT: vm.today, HR_SCHEDULE_H_ID: xHR_SCHEDULE_H_ID, OPERATOR_ID: xOPERATOR_ID, LK_FBR_GRP_LST: [] };
                                vm.errors = res.errors;
                            }
                            else {
                                res['data'] = angular.fromJson(res.jsonStr);
                                config.appToastMsg(res.data.PMSG);
                                //$scope.IS_MAIN = false;
                                //$scope.IS_PRODUCTION = false;

                                $scope.IS_PRODUCTION = true;
                                if ($scope.back_UI == 1)
                                    $scope.IS_TUBE = true;
                                else if ($scope.back_UI == 2)
                                    $scope.IS_OPEN = true;

                                var LK_DIA_TYPE_ID = 327;
                                if ($scope.IS_TUBE)
                                    LK_DIA_TYPE_ID = 328;
                                $scope.IS_MAIN = true;

                                vm.form = { LK_DIA_TYPE_ID: xLK_DIA_TYPE_ID, PROD_DT: vm.today, HR_SCHEDULE_H_ID: xHR_SCHEDULE_H_ID, OPERATOR_ID: xOPERATOR_ID, LK_FBR_GRP_LST: [] };
                                vm.productionList = [];
                                vm.machineList = [];
                                angular.element('#DYE_BATCH_NO').focus();
                            }
                        });
                    }

                });
            }
        };



        vm.submitReprocess = function (dataOri, item) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);
                Dialog.confirm('This is a re-process production!!! "' + data.DYE_BATCH_NO + '.', 'Attention', ['Yes', 'No'])
                .then(function () {
                    //var pform = angular.copy(vm.form);

                    data["IS_REFINIS"] = 'Y';
                    data["IS_FINALIZED"] = 'N';

                    data["LK_FBR_GRP_LST"] = !data.LK_FBR_GRP_LST ? '' : data.LK_FBR_GRP_LST.join(',');

                    var _isudate = $filter('date')(data.PROD_DT, 'yyyy-MM-ddTHH:mm:ss');
                    data["PROD_DT"] = _isudate;


                    var mc = angular.copy(item);

                    data["DF_PROC_TYPE_ID"] = mc.DF_PROC_TYPE_ID;
                    data["DF_MACHINE_ID"] = mc.DF_MACHINE_ID;
                    data["BT_FIN_QTY"] = data.BATCH_QTY;
                    data["DYE_BT_STS_TYPE_ID"] = mc.DYE_BT_STS_TYPE_ID;

                    var xOPERATOR_ID = data.OPERATOR_ID;
                    var xHR_SCHEDULE_H_ID = data.HR_SCHEDULE_H_ID;
                    var xLK_DIA_TYPE_ID = data.LK_DIA_TYPE_ID;

                    console.log(mc);
                    var list = mc.NXT_BT_STS_TYPE_LST.split(',');
                    console.log(list);

                    if (list.length > 1) {
                        $ngConfirm({
                            title: 'Take Action',
                            contentUrl: 'form.html',
                            buttons: {
                                getPassCode: {
                                    text: 'OK',
                                    disabled: true,
                                    btnClass: 'btn-green',
                                    action: function (scope) {

                                        if (scope.NXT_BT_STS_TYPE_ID > 0) {

                                            data["NXT_BT_STS_TYPE_ID"] = scope.NXT_BT_STS_TYPE_ID;

                                            return DyeingDataService.saveDataByUrl(data, '/DFProduction/Save').then(function (res) {
                                                if (res.success === false) {
                                                    vm.errors = res.errors;
                                                    vm.form = { LK_DIA_TYPE_ID: xLK_DIA_TYPE_ID, PROD_DT: vm.today, HR_SCHEDULE_H_ID: xHR_SCHEDULE_H_ID, OPERATOR_ID: xOPERATOR_ID, LK_FBR_GRP_LST: [] };
                                                }
                                                else {
                                                    res['data'] = angular.fromJson(res.jsonStr);
                                                    config.appToastMsg(res.data.PMSG);
                                                    //$scope.IS_MAIN = false;
                                                    //$scope.IS_PRODUCTION = false;

                                                    $scope.IS_PRODUCTION = true;
                                                    if ($scope.back_UI == 1)
                                                        $scope.IS_TUBE = true;
                                                    else if ($scope.back_UI == 2)
                                                        $scope.IS_OPEN = true;

                                                    var LK_DIA_TYPE_ID = 327;
                                                    if ($scope.IS_TUBE)
                                                        LK_DIA_TYPE_ID = 328;
                                                    $scope.IS_MAIN = true;

                                                    vm.form = { LK_DIA_TYPE_ID: xLK_DIA_TYPE_ID, PROD_DT: vm.today, HR_SCHEDULE_H_ID: xHR_SCHEDULE_H_ID, OPERATOR_ID: xOPERATOR_ID, LK_FBR_GRP_LST: [] };
                                                    vm.productionList = [];
                                                    vm.machineList = [];
                                                    angular.element('#DYE_BATCH_NO').focus();
                                                }
                                            });
                                        }
                                        else {

                                            config.appToastMsg("MULTI-005 Please Select Next Status!!!");
                                            return;
                                        }
                                    }
                                },
                                later: function () {
                                    return;
                                }
                            },
                            onScopeReady: function (scope) {
                                var self = this;

                                scope.statusTypeList = {
                                    optionLabel: "-- Select Status --",
                                    filter: "contains",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchStatusType').then(function (res) {
                                                    var slist = _.filter(res, function (x) { return x.DYE_BT_STS_TYPE_ID == list[0] });
                                                    for (var i = 1; i < list.length; i++) {
                                                        var obj = _.filter(res, function (x) { return x.DYE_BT_STS_TYPE_ID == list[i] })[0];
                                                        slist.splice(i, "0", obj);
                                                    }
                                                    e.success(slist);
                                                });
                                            }
                                        }
                                    },
                                    dataTextField: "BT_STS_TYP_NAME",
                                    dataValueField: "DYE_BT_STS_TYPE_ID"
                                };

                                scope.textChange = function () {
                                    if (scope.NXT_BT_STS_TYPE_ID > 0)
                                        self.buttons.getPassCode.setDisabled(false);
                                    else
                                        self.buttons.getPassCode.setDisabled(true);
                                }
                            }
                        });
                    }
                    else {

                        return DyeingDataService.saveDataByUrl(data, '/DFProduction/Save').then(function (res) {
                            if (res.success === false) {
                                vm.form = { LK_DIA_TYPE_ID: xLK_DIA_TYPE_ID, PROD_DT: vm.today, HR_SCHEDULE_H_ID: xHR_SCHEDULE_H_ID, OPERATOR_ID: xOPERATOR_ID, LK_FBR_GRP_LST: [] };
                                vm.errors = res.errors;
                            }
                            else {
                                res['data'] = angular.fromJson(res.jsonStr);
                                config.appToastMsg(res.data.PMSG);

                                $scope.IS_PRODUCTION = true;
                                if ($scope.back_UI == 1)
                                    $scope.IS_TUBE = true;
                                else if ($scope.back_UI == 2)
                                    $scope.IS_OPEN = true;

                                var LK_DIA_TYPE_ID = 327;
                                if ($scope.IS_TUBE)
                                    LK_DIA_TYPE_ID = 328;
                                $scope.IS_MAIN = true;

                                vm.form = { LK_DIA_TYPE_ID: xLK_DIA_TYPE_ID, PROD_DT: vm.today, HR_SCHEDULE_H_ID: xHR_SCHEDULE_H_ID, OPERATOR_ID: xOPERATOR_ID, LK_FBR_GRP_LST: [] };
                                vm.productionList = [];
                                vm.machineList = [];
                                angular.element('#DYE_BATCH_NO').focus();
                            }
                        });
                    }

                });
            }
        };


    }


})();

