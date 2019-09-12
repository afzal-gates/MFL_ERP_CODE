
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

    angular.module('multitex.dyeing').filter('groupBy', function () {
        return _.memoize(function (items, field) {
            return _.groupBy(items, field);
        }
        );
    });

    angular.module('multitex.dyeing').directive('expand', function () {
        function link(scope, element, attrs) {
            scope.$on('onExpandAll', function (event, args) {
                scope.expanded = args.expanded;
            });
        }
        return {
            link: link
        };
    });

    angular.module('multitex.dyeing').controller('FabricInspectionReportController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'formData', 'cur_user_id', '$modal', 'Dialog', '$filter', FabricInspectionReportController]);
    function FabricInspectionReportController($q, config, DyeingDataService, $stateParams, $state, $scope, formData, cur_user_id, $modal, Dialog, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        $scope.Showed = true;
        vm.IS_DISABLE = false;

        vm.form = formData.DF_FAB_QC_RPT_ID ? formData : { DF_FAB_QC_RPT_ID: 0, CREATED_BY: cur_user_id, QC_DT: vm.today, ISS_REF_DT: vm.today, ACTION_DT: vm.today, QC_BY: -1, SHFT_IN_CHRGE: -1, DYE_BATCH_NO: '' };

        vm.formItem = { KNIT_ITEM_LST: [], QC_DT: vm.today }

        vm.dyeBatchLot = [];
        vm.subLotList = [];

        vm.form['CREATED_BY'] = (cur_user_id || -1);
        vm.form['ACTION_DT'] = vm.today;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [loadQcRollList(), loadQcRollLotList(), loadQcRptDtlList(), GetDefectList(), getQcStatusList(), getQcShadeList(),
                GetDfQcParamTypeList(), getQcReturnList(), getQcParamStatusList(), getUserData(), getShiftData(), getQcUserData(), loadShift()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        $scope.expandAll = function (expanded) {
            // $scope is required here, hence the injection above, even though we're using "controller as" syntax
            $scope.$broadcast('onExpandAll', {
                expanded: expanded
            });
        }

        vm.autoLoadFinishQty = function () {
            if (vm.formItem.KNT_QC_STS_TYPE_ID == 1 || vm.formItem.KNT_QC_STS_TYPE_ID == 5 || vm.formItem.KNT_QC_STS_TYPE_ID == 6)
                vm.formItem.FINIS_QTY = vm.formItem.LOT_QTY;
            else
                vm.formItem.FINIS_QTY = '';
        }

        let lastChecked = null;
        vm.MajQcParamList = [];

        vm.selectdeselect = function (item, event) {
            if (event.target.value === lastChecked) {
                delete item.LK_PARAM_QC_STS_ID
                lastChecked = null
            } else {
                lastChecked = event.target.value
            }

            var list = _.filter(vm.QcParamList, function (x) { return x.LK_PARAM_QC_STS_ID == 713 || x.LK_PARAM_QC_STS_ID == 712 });
            if (list.length > 0) {
                vm.MajQcParamList = list;
                if (list.length == 1)
                    vm.form.MAJ_QC_PARAM_TYPE_ID = list[0].RF_DF_QC_PARAM_TYPE_ID;
                else
                    vm.form.MAJ_QC_PARAM_TYPE_ID = 0;
            }
        }

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

                    $scope.majorDfctList = [];

                    $scope['MAJ_DFCT_TYPE_ID'] = 0;
                    var pMAJ_DFCT_TYPE_ID = 0;

                    $scope.expandAll = function (expanded) {
                        // $scope is required here, hence the injection above, even though we're using "controller as" syntax
                        $scope.$broadcast('onExpandAll', {
                            expanded: expanded
                        });
                    }

                    GetDefectList();

                    function GetDefectList() {
                        DyeingDataService.getDataByFullUrl('/api/security/UserMap/GetFabDfctTypeMapByID?pLK_FAB_INSP_TYPE_ID=709&pOption=3002').then(function (res) {
                            console.log(res);

                            var list = _.groupBy(res, function (x) { return x.RESP_DEPT_NAME; });
                            $scope.defectList = list;

                            //$scope.defectList = res;
                            //pLK_FAB_INSP_TYPE_ID=709,708,707&

                        });
                    };

                    $scope.getMultipleDept = function (item) {
                        console.log(item);

                        if (item.LAST_UPDATED_BY > 0) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetRespDeptListByID?pMULTI_DEPT_LST=' + (item.MULTI_DEPT_LST || (item.RF_RESP_DEPT_ID_OLD || ''))).then(function (res) {
                                item["resDeptList"] = res;
                                if (res.length == 1) {
                                    item['RF_RESP_DEPT_ID'] = res[0].RF_RESP_DEPT_ID;
                                }
                                else {
                                    item['RF_RESP_DEPT_ID'] = 0;
                                }
                            });
                        }
                        else {
                            item.LAST_UPDATED_BY = 0;
                            item["resDeptList"] = [];
                        }


                        var grpList = _.filter($scope.defectList, function (x) { return x.RESP_DEPT_NAME != "Afzal" });
                        var row = parseInt(grpList.length);

                        var list = _.filter(grpList[0], function (x) { return x.LAST_UPDATED_BY > 0 });
                        for (var i = 1; i < row; i++) {
                            var itemData = _.filter(grpList[i], function (x) { return x.LAST_UPDATED_BY > 0 });
                            for (var j = 0; j < itemData.length; j++)
                                list.push(itemData[j]);
                        }

                        //var list = _.filter($scope.defectList, function (x) { return x.LAST_UPDATED_BY > 0 });
                        $scope.majorDfctList = list;
                        if (list.length == 1) {
                            $scope.MAJ_DFCT_TYPE_ID = list[0].RF_FB_DFCT_TYPE_ID;
                            pMAJ_DFCT_TYPE_ID = list[0].RF_FB_DFCT_TYPE_ID;
                        }
                        else {
                            $scope.MAJ_DFCT_TYPE_ID = 0;
                            pMAJ_DFCT_TYPE_ID = 0;
                        }

                    }

                    $scope.clearAll = function () {
                        $scope.MC_BYR_ACC_GRP_ID = '';
                        $scope.COLOR_NAME_EN = '';
                        $scope.STYLE_ORDER_NO = '';
                        $scope.QC_DT = vm.today;
                    }

                    $scope.cancel = function (data) {
                        $modalInstance.close(data);
                    }

                    $scope.submitAll = function () {
                        console.log($scope);
                        console.log(pMAJ_DFCT_TYPE_ID);
                        var grpList = _.filter($scope.defectList, function (x) { return x.RESP_DEPT_NAME != "Afzal" });
                        var row = parseInt(grpList.length);

                        var list = _.filter(grpList[0], function (x) { return x.LAST_UPDATED_BY > 0 });
                        for (var i = 1; i < row; i++) {
                            var itemData = _.filter(grpList[i], function (x) { return x.LAST_UPDATED_BY > 0 });
                            for (var j = 0; j < itemData.length; j++)
                                list.push(itemData[j]);
                        }

                        var dataOri = {
                            'DFCT_LST': list,
                            'MAJ_DFCT_TYPE_ID': ($scope.MAJ_DFCT_TYPE_ID || pMAJ_DFCT_TYPE_ID)
                        }

                        $modalInstance.close(dataOri);

                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    formData: function (DyeingDataService) {
                        console.log(item);
                        return item;
                    }
                }
            });

            modalInstance.result.then(function (item) {
                console.log(item);

                if (item) {
                    vm.formItem.MAJ_DFCT_TYPE_ID = item.MAJ_DFCT_TYPE_ID;
                    vm.formItem['OTH_DFCT_TYPE_LST'] = item.DFCT_LST;
                }
            });

        };


        vm.openFlatKnitModal = function (item) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'DyeingFinishingFlatKnitDtl.html',
                controller: function ($scope, $modalInstance, DyeingDataService, formData) {

                    var vmDF = this;
                    $scope.vmDF = formData;
                    console.log(formData);

                    $scope.cancel = function (data) {
                        $modalInstance.close(data);
                    }

                    $scope.checkReqQty = function (data, id) {
                        if (data.RQD_GREY_PCS < (data.ACT_FIN_PCS + data.REJECT_PCS))
                            if (id == 0) {
                                data.ACT_FIN_PCS = '';
                                config.appToastMsg("MULTI-005 Please check required qty!!!");
                            }
                            else {
                                data.REJECT_PCS = '';
                                config.appToastMsg("MULTI-005 Please check required qty!!!");
                            }
                    }


                    $scope.concateMesurement = function (data) {
                        data.ACT_FIN_MEAS = data.MEAS_WIDTH + "x" + data.MEAS_HEIGHT;
                    }


                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    formData: function (DyeingDataService) {
                        console.log(item);
                        return item;
                    }
                }
            });

            modalInstance.result.then(function (objItem) {
                console.log(objItem);

                if (objItem) {
                    vm.formItem['FLAT_KNIT_ITEM_LIST'] = objItem;
                }
            });

        };


        var CurShiftData = {};

        function getShifData(res) {

            CurShiftData = _.find(res, function (o) {
                return o.IS_SCHE_ACTIVE == 'Y';
            });
            if (CurShiftData) {
                vm.formItem.HR_SCHEDULE_H_ID = CurShiftData.HR_SCHEDULE_H_ID;
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

        function loadQcRptDtlList() {
            DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfBTQcParamList?pDYE_BT_CARD_H_ID=' + vm.form.DYE_BT_CARD_H_ID).then(function (res) {
                if (res.length == 0) {
                    DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQcParamTypeList').then(function (resD) {
                        vm.QcParamList = resD;
                    });
                }
                else {
                    vm.QcParamList = res;
                    var list = _.filter(res, function (x) { return x.LK_PARAM_QC_STS_ID == 713 || x.LK_PARAM_QC_STS_ID == 712 });
                    if (list.length > 0) {
                        vm.MajQcParamList = list;

                    }
                }

            });

            DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfBTQcStatusInfo?pDYE_BT_CARD_H_ID=' + vm.form.DYE_BT_CARD_H_ID).then(function (res) {
                if (res) {
                    vm.form['DF_BT_QC_STATUS_ID'] = res.DF_BT_QC_STATUS_ID;
                    vm.form['KNT_QC_STS_TYPE_ID'] = res.KNT_QC_STS_TYPE_ID;
                    vm.form['ACTION_DT'] = vm.today;
                    vm.form['FINAL_NOTE'] = res.FINAL_NOTE;
                    vm.form['CREATED_BY'] = (res.CREATED_BY || cur_user_id);
                }
            });

            DyeingDataService.LookupListData(145).then(function (res) {
                vm.qcParamStatusList = res;
            });

            DyeingDataService.getDataByFullUrl('/api/knit/KntScChlnRcv/GetQcStatusTypeList').then(function (res) {
                vm.finalQcStatusList = res;
            });

            if ($stateParams.pDF_FAB_QC_RPT_ID > 0) {
                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchIssueFabricByBatchNo?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "")).then(function (res) {
                    vm.BatchFabricList = res;
                    DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQCRptDtlByID?pDF_FAB_QC_RPT_ID=' + $stateParams.pDF_FAB_QC_RPT_ID).then(function (res) {
                        vm.QcRollList = res;
                        var ttl_qty = 0;
                        for (var j = 0; j < res.length; j++) {
                            ttl_qty = ttl_qty + res[j].LOT_QTY;
                        }
                        vm.form.LOT_QTY = ttl_qty;
                    });
                });

                DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQCRpt?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || '')).then(function (res) {
                    var qlist = _.filter(res, function (x) { return x.DF_FAB_QC_RPT_ID != $stateParams.pDF_FAB_QC_RPT_ID })
                    vm.inspectionList = qlist;
                });
            }
            else {
                vm.QcRollList = [];
                vm.inspectionList = [];
            }

        }


        $scope.QC_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.QC_DT_LNopened = true;
        }


        $scope.ACTION_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ACTION_DT_LNopened = true;
        }

        vm.PreQcRollList = [];

        function getQcUserData() {

            return vm.QcUserList = {
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

        vm.checkLotQty = function (obj) {
            if (obj.LOT_QTY > obj.RQD_QTY) {
                obj.LOT_QTY = '';

                config.appToastMsg("MULTI-003 Please write valid lot quantity!!!");
            }
        }

        vm.inputIntoQcList = function (dataItem) {

            if (vm.form.LK_DIA_TYPE_ID == dataItem.LK_DIA_TYPE_ID) {
                var data = angular.copy(dataItem);
                data['uid'] = "";
                data['IS_RP_RQD'] = "N";
                data['IS_RP_DONE'] = "N";
                data['DF_BT_SUB_LOT_ID'] = 0;
                var lst = angular.copy(_.filter(vm.BatchFabricList, function (x) { return x.LK_DIA_TYPE_ID == vm.form.LK_DIA_TYPE_ID && x.IS_SELECT == true; }));

                data['KNIT_ITEM_LST'] = lst;

                var shift = angular.copy(vm.formItem.HR_SCHEDULE_H_ID);

                vm.formItem = {
                    OTH_DFCT_TYPE_LST: data.OTH_DFCT_TYPE_LST,
                    SHADE_GRP: data.SHADE_GRP,
                    KNT_QC_STS_TYPE_ID: data.KNT_QC_STS_TYPE_ID,
                    LK_RTN_TYP_ID: data.LK_RTN_TYP_ID,
                    LK_DIA_TYPE_ID: data.LK_DIA_TYPE_ID,
                    SUB_LOT_NO: data.SUB_LOT_NO,
                    LOT_QTY: data.LOT_QTY,
                    NO_OF_ROLL: data.NO_OF_ROLL,
                    FIN_DIA: data.FIN_DIA,
                    IS_RP_RQD: 'N',
                    FINIS_QTY: data.FINIS_QTY,
                    IS_RP_DONE: 'N',
                    MAJ_DFCT_TYPE_ID: data.MAJ_DFCT_TYPE_ID,
                    LK_SUB_LOT_GRP_ID: data.LK_SUB_LOT_GRP_ID,
                    FAB_STR_POS: data.FAB_STR_POS,
                    KNIT_ITEM_LST: data.KNIT_ITEM_LST,
                    HR_SCHEDULE_H_ID: shift,
                    QC_DT: vm.today,
                    //QC_BY: data.QC_BY,
                    //SHFT_IN_CHRGE: data.SHFT_IN_CHRGE
                };
            }
            else {
                dataItem.IS_SELECT = false;
            }
        }

        vm.clearData = function () {
            var shift = angular.copy(vm.formItem.HR_SCHEDULE_H_ID);
            vm.formItem = {
                OTH_DFCT_TYPE_LST: '',
                SHADE_GRP: '',
                KNT_QC_STS_TYPE_ID: '',
                LK_RTN_TYP_ID: '',
                SUB_LOT_NO: '',
                LOT_QTY: '',
                NO_OF_ROLL: '',
                FIN_DIA: '',
                IS_RP_RQD: 'N',
                FINIS_QTY: '',
                IS_RP_DONE: 'N',
                MAJ_DFCT_TYPE_ID: '',
                LK_SUB_LOT_GRP_ID: '',
                FAB_STR_POS: '',
                KNIT_ITEM_LST: [],
                HR_SCHEDULE_H_ID: shift,
                QC_DT: vm.today,
                QC_BY: '',
                SHFT_IN_CHRGE: ''
            };
            angular.element('#DYE_BATCH_NO').focus();
        }

        vm.addData = function (data) {

            vm.IS_DISABLE = true;

            if (fnValidateSub() == true) {
                var ttl = 0;
                var ttl_qty = 0;
                for (var i = 0; i < data.KNIT_ITEM_LST.length; i++) {
                    ttl = ttl + parseFloat(data.KNIT_ITEM_LST[i].LOT_QTY);
                }
                data.LOT_QTY = ttl;
                var item = angular.copy(data);
                var idx = 0
                if (vm.QcRollList)
                    idx = vm.QcRollList.length;
                var index = vm.QcRollList.indexOf(item);

                console.log(item);
                vm.QcRollList.splice(idx, "0", item);
                vm.clearData();
                for (var x = 0; x < vm.BatchFabricList.length; x++) {
                    vm.BatchFabricList[x].IS_SELECT = false;
                }
                for (var j = 0; j < vm.QcRollList.length; j++) {
                    ttl_qty = ttl_qty + vm.QcRollList[j].LOT_QTY;
                }

                vm.form.LOT_QTY = ttl_qty;
            }
        }


        vm.updateData = function (data) {
            vm.IS_DISABLE = true;
            if (fnValidateSub() == true) {
                var ttl = 0;
                for (var i = 0; i < data.KNIT_ITEM_LST.length; i++) {
                    ttl = ttl + parseFloat(data.KNIT_ITEM_LST[i].LOT_QTY);
                }
                data.LOT_QTY = ttl;
                var item = angular.copy(data);

                var index = vm.QcRollList.indexOf(item);
                vm.QcRollList.splice(index, 1);
                var idx = vm.QcRollList.length;
                vm.QcRollList.splice(idx, "0", item);
                vm.clearData();
            }
        }

        vm.removeGrid = function (idx) {

            var item = vm.QcRollList[idx];
            if (item.DF_BT_SUB_LOT_ID > 0) {
                Dialog.confirm('Removing "' + item.SUB_LOT_NO + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {

                     vm.QcRollList.splice(idx, 1);
                 });
            }
            else {
                vm.QcRollList.splice(idx, 1);
            }
        }

        vm.editGrid = function (item) {
            var data = angular.copy(item);
            console.log(data);
            vm.formItem = data;
        }


        vm.clearParamData = function () {
            vm.formPram = { RF_DF_QC_PARAM_TYPE_ID: '', LK_PARAM_QC_STS_ID: '' };
        }

        vm.addParamData = function (data) {
            var item = angular.copy(data);
            var idx = vm.QcParamList.length;
            var index = vm.QcParamList.indexOf(item);
            vm.QcParamList.splice(idx, "0", item);
            vm.clearParamData();
        }

        vm.updateParamData = function (data) {
            var item = angular.copy(data);

            var index = vm.QcParamList.indexOf(item);
            vm.QcParamList.splice(index, 1);
            var idx = vm.QcParamList.length;
            vm.QcParamList.splice(idx, "0", item);
            vm.clearData();
        }

        vm.removeParamGrid = function (idx) {

            vm.QcParamList.splice(idx, 1);
        }

        vm.editParamGrid = function (item) {
            var data = angular.copy(item);
            console.log(data);
            vm.formPram = data;
        }

        function getUserData() {

            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/SelectDfQcUser?pLK_DF_EMP_TYPE_ID=' + (vm.form.LK_DF_EMP_TYPE_ID || 721)).then(function (res) {
                console.log(res);
                //var incharge = [{
                //    EMP_FULL_NAME_EN: '-- Select In-Charge --',
                //    HR_EMPLOYEE_ID: -1
                //}].concat(res);
                vm.userList = res;
            });


            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/SelectDfQcUser?pLK_DF_EMP_TYPE_ID=' + (vm.form.LK_DF_EMP_TYPE_ID || 720)).then(function (res) {
                console.log(res);
                //var inspector = [{
                //    EMP_FULL_NAME_EN: '-- Select Inspector --',
                //    HR_EMPLOYEE_ID: -1
                //}].concat(res);
                vm.userInspList = res;
            });
        }

        function getShiftData() {

            DyeingDataService.getDataByFullUrl('/api/knit/KntMachinOprAssign/GetKnitScheduleList').then(function (res) {
                var lst = [{
                    SCHEDULE_NAME_EN: '-- Select Shift --',
                    HR_SCHEDULE_H_ID: -1
                }].concat(res);
                vm.shiftList = lst;
            });
        }

        function GetDefectList() {
            return vm.defectList = {
                optionLabel: "-- Select Defect --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/security/UserMap/GetFabDfctTypeMapByID?pLK_FAB_INSP_TYPE_ID=0&pOption=3002').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "FB_DFCT_TYPE_NAME",
                dataValueField: "RF_FB_DFCT_TYPE_ID"
            };

        };

        function GetDfQcParamTypeList() {
            return vm.DfQcParamTypeList = {
                optionLabel: "-- Select QC Param --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQcParamTypeList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "CQ_PARAM_EN_NAME",
                dataValueField: "RF_DF_QC_PARAM_TYPE_ID"
            };

        };

        function getQcStatusList() {

            return vm.qcStatusList = {
                optionLabel: "-- Select Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/knit/KntScChlnRcv/GetQcStatusTypeList').then(function (res) {
                                var list = _.filter(res, function (x) { return x.KNT_QC_STS_TYPE_ID != 2 && x.KNT_QC_STS_TYPE_ID != 5 });
                                e.success(list);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "QC_STS_TYP_NAME",
                dataValueField: "KNT_QC_STS_TYPE_ID"
            };
        };

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

        function getQcParamStatusList() {

            //return vm.qcParamStatusList = {
            //    optionLabel: "-- Select Status --",
            //    filter: "contains",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                DyeingDataService.LookupListData(145).then(function (res) {
            //                    e.success(res);
            //                }, function (err) {
            //                    console.log(err);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "LK_DATA_NAME_EN",
            //    dataValueField: "LOOKUP_DATA_ID"
            //};
        };

        function getQcShadeList() {
            var list = [{ SHADE_GRP: 'A' }, { SHADE_GRP: 'B' }, { SHADE_GRP: 'C' }, { SHADE_GRP: 'D' }];
            return vm.qcShadeList = {
                optionLabel: "-- Select Shade --",
                filter: "contains",
                autoBind: true,
                dataSource: list,
                dataTextField: "SHADE_GRP",
                dataValueField: "SHADE_GRP"
            };
        };

        function loadQcRollList() {
            //DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQCRollByID?pDF_FAB_QC_RPT_ID=' + $stateParams.pDF_FAB_QC_RPT_ID).then(function (res) {
            //    vm.QcRollList = res;
            //    console.log(vm.QcRollList);
            //});

        }

        function loadQcRollLotList() {
            DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQCRollLotByID?pDF_FAB_QC_RPT_ID=' + $stateParams.pDF_FAB_QC_RPT_ID).then(function (res) {
                var list = _.filter(res, function (x) { return x.KNT_QC_STS_TYPE_ID == 3 });
                vm.QcRollLotList = list;
                console.log("AAAA");
                console.log(vm.QcRollLotList);
                console.log("AAAA");

            });

        }

        vm.searchBatchInfo = function (pPageNo) {
            if ((vm.form.DYE_BATCH_NO && vm.form.DYE_BATCH_NO.length > 0) || (vm.form.FAB_ROLL_NO && vm.form.FAB_ROLL_NO.length > 0)) {
                vm.showSplash = true;

                DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQCRpt?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || '')).then(function (res) {
                    var qlist = _.filter(res, function (x) { return x.DF_FAB_QC_RPT_ID != (vm.form.pDF_FAB_QC_RPT_ID || 0) })
                    vm.inspectionList = qlist;
                });

                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchSubLotByID?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || "") + '&pLK_DIA_TYPE_ID=' + (vm.form.LK_DIA_TYPE_ID || null)).then(function (res_b) {
                    vm.dyeBatchLot = res_b;
                    if (res_b.length == 1) {
                        DyeingDataService.getDataByUrl('/DFProduction/GetBatchForDFQC?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "") + '&pLK_DIA_TYPE_ID=' + (vm.form.LK_DIA_TYPE_ID || null)).then(function (res) {
                            console.log(res);


                            vm.form.DYE_STR_REQ_H_ID = res[0].DYE_STR_REQ_H_ID;
                            vm.form.STR_REQ_NO = res[0].STR_REQ_NO;
                            vm.form.STR_REQ_DT = res[0].STR_REQ_DT;
                            vm.form.DYE_MACHINE_ID = res[0].DYE_MACHINE_ID;
                            vm.form.BUYER_NAME_EN = res[0].BUYER_NAME_EN;
                            vm.form.STYLE_NO = res[0].STYLE_NO;
                            vm.form.ORDER_NO = res[0].ORDER_NO;
                            vm.form.COLOR_NAME_EN = res[0].COLOR_NAME_EN;
                            vm.form.LD_RECIPE_NO = res[0].LD_RECIPE_NO;
                            vm.form.COLOR_GRP_NAME_EN = res[0].COLOR_GRP_NAME_EN;
                            vm.form.MC_LD_RECIPE_H_ID = res[0].MC_LD_RECIPE_H_ID;
                            vm.form.LQR_RATIO = "1:" + res[0].LQR_RATIO;
                            vm.form.DYE_MTHD_NAME = res[0].DYE_MTHD_NAME;
                            vm.form.UN_HOLD_DT = res[0].UN_HOLD_DT;
                            vm.form.CHK_ROLL_DT = res[0].CHK_ROLL_DT;
                            vm.form.CK_ROLL_RCV_DT = res[0].CK_ROLL_RCV_DT;
                            vm.form.CK_ROLL_FIN_DT = res[0].CK_ROLL_FIN_DT;
                            vm.form.LK_CHQ_RL_STS_ID = res[0].LK_CHQ_RL_STS_ID;
                            vm.form.DYE_BATCH_NO_LST = res[0].DYE_BATCH_NO;
                            vm.form.DYE_LOT_NO = res[0].DYE_LOT_NO;
                            vm.form.FAB_COLOR_ID = res[0].FAB_COLOR_ID;
                            vm.form.DYE_BT_CARD_H_ID = res[0].DYE_BT_CARD_H_ID;
                            vm.form.ACT_BATCH_QTY = res[0].ACT_BATCH_QTY;

                            vm.form.BATCH_QTY = res[0].BATCH_QTY;
                            vm.form.DIA_TYPE_NAME = res[0].DIA_TYPE_NAME;

                            DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchIssueFabricByBatchNo?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "")).then(function (res) {
                                vm.BatchFabricList = res;
                            });

                            DyeingDataService.getDataByUrl('/GreyFabReq/getFinishFabricInsRollByBatchID?pDYE_BT_CARD_H_ID=' + res[0].DYE_BT_CARD_H_ID).then(function (res) {
                                vm.RollProductionList = res;
                            });
                            vm.showSplash = false;
                        });
                    }
                    else {
                        vm.form = { QC_DT: vm.today, DYE_BATCH_NO: vm.form.DYE_BATCH_NO, DYE_STR_REQ_H_ID_LOT: '' };
                        vm.BatchFabricList = [];
                        config.appToastMsg("MULTI-005 Please Select Working Batch Sub-Lot!!!");
                    }
                });
            }
        }

        vm.searchBatchInfoBySubLot = function (e) {

            var item = e.sender.dataItem(e.item);
            //console.log(item);
            if (item.DYE_STR_REQ_H_ID > 0) {

                vm.showSplash = true;
                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForProduction?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || "") + '&pDYE_STR_REQ_H_ID=' + (item.DYE_STR_REQ_H_ID || null)).then(function (res) {

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
                        vm.form.FAB_COLOR_ID = res.batch[0].FAB_COLOR_ID;
                        vm.form.LD_RECIPE_NO = res.batch[0].LD_RECIPE_NO;
                        vm.form.COLOR_GRP_NAME_EN = res.batch[0].COLOR_GRP_NAME_EN;
                        vm.form.MC_LD_RECIPE_H_ID = res.batch[0].MC_LD_RECIPE_H_ID;
                        vm.form.DYE_RE_PROC_TYPE_ID = res.batch[0].DYE_RE_PROC_TYPE_ID;
                        vm.form.REQ_RE_PROC_TYPE_ID = res.batch[0].REQ_RE_PROC_TYPE_ID;
                        vm.form.LQR_RATIO = "1:" + res.batch[0].LQR_RATIO;
                        vm.form.DYE_MTHD_NAME = res.batch[0].DYE_MTHD_NAME;
                        vm.form.ACTN_ROLE_FLAG = res.batch[0].ACTN_ROLE_FLAG;
                        vm.form.DYE_BATCH_NO_LST = res.batch[0].DYE_BATCH_NO;
                        vm.form.DYE_BT_CARD_H_ID = res.batch[0].DYE_BT_CARD_H_ID;
                        vm.form.DYE_LOT_NO = item.DYE_LOT_NO;
                        vm.form.ACT_BATCH_QTY_LST = res.batch[0].ACT_BATCH_QTY + " Kg";
                        vm.form.ACT_BATCH_QTY = res.batch[0].ACT_BATCH_QTY;

                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchIssueFabricByBatchNo?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "") + '&pDYE_STR_REQ_H_ID=' + (item.DYE_STR_REQ_H_ID || null) + '&pOption=3006').then(function (res) {
                            vm.BatchFabricList = res;
                        });

                    }
                    else {
                        config.appToastMsg("MULTI-005 Please select sub-lot!");
                        vm.BatchFabricList = [];
                    }
                    vm.showSplash = false;
                });

            }
        }

        var record = 0;

        vm.gridOptionsSubLot = {
            dataSource: {
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQCRptByBatchNo?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || '')).then(function (res) {
                            e.success(res);
                        })
                    }
                },
            },
            //height: '400px',
            scrollable: true,
            pageable: false,
            columns: [
                { field: "SUB_LOT_NO", title: "Sub-Lot", type: "string", width: "7%" },
                //{ field: "FABRIC_GROUP_NAME", title: "Fab Grp", type: "string", width: "5%", },
                { field: "YARN_SPEC", title: "Fabric Details", type: "string", width: "40%" },
                //{ field: "FIB_COMP_NAME", title: "Fiber Comp.", type: "string", width: "15%" },
                //{ field: "RQD_GSM", title: "Req. GSM", type: "string", width: "7%" },
                //{ field: "FIN_DIA", title: "Fin. DIA", type: "string", width: "5%" },
                { field: "LK_DIA_TYPE_NAME", title: "Dia Type", type: "string", width: "5%" },
                { field: "RQD_QTY", title: "Grey Qty.", type: "decimal", width: "7%" },
                { field: "LOT_QTY", title: "Lot Qty.", type: "decimal", width: "7%" },
                { field: "FINIS_QTY", title: "Finish Qty", type: "decimal", width: "7%" },
                { field: "FAB_STR_POS", title: "Fabric Positionk", type: "string", width: "10%" },
                { field: "QC_STS_TYP_NAME", title: "QC Status", type: "string", width: "10%" },
                { field: "SUB_LOT_GRP_NAME", title: "Sub Lot Group", type: "string", width: "10%" },
            ],
        }

        vm.gridOptionsFabric = {
            pageable: false,
            scrollable: true,
            columns: [
                { field: "LK_DIA_TYPE_ID", hidden: true },
                { field: "LK_FBR_GRP_ID", hidden: true },
                { field: "RF_FAB_TYPE_ID", hidden: true },
                { field: "RF_FIB_COMP_ID", hidden: true },
                { field: "IS_SELECT", title: "+/-", type: "string", width: "4%", template: "<input type='checkbox' ng-click='vm.inputIntoQcList(dataItem);' ng-model='dataItem.IS_SELECT' />" },
                { field: "FABRIC_GROUP_NAME", title: "Fab Grp", type: "string", width: "10%", },
                { field: "FAB_TYPE_SNAME", title: "Fabric Type", type: "string", width: "10%" },
                { field: "FIB_COMP_NAME", title: "Fiber Comp.", type: "string", width: "15%" },
                { field: "RQD_GSM", title: "Req. GSM", type: "string", width: "5%" },
                { field: "FIN_DIA", title: "Fin. DIA", type: "string", width: "5%" },
                { field: "LK_DIA_TYPE_NAME", title: "Dia Type", type: "string", width: "5%" },
                //{ field: "KNT_YRN_LOT", title: "Yarn Lot", type: "string", width: "35%" },
                { field: "FULL_RQD_QTY", title: "Batch Req Qty.", type: "decimal", width: "10%" },
                { field: "RQD_QTY", title: "Lot Qty.", type: "decimal", width: "10%" },
                { field: "FULL_FINIS_QTY", title: "Batch Fin Qty.", type: "decimal", width: "10%" },
                //{ field: "FIN_QTY", title: "Finish Qty", type: "decimal", width: "10%" },
                //{ field: "RQD_QTY", title: "Blance Type", type: "decimal", width: "10%" },

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

        vm.gridOptionsFinalFabric = {

            dataSource: {
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQCRptByBatchNo?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "")).then(function (res) {
                            e.success(res);
                        })
                    }
                },
                group: [
                    { field: "LK_DIA_TYPE_NAME", dir: "asc" },
                ]
            },
            pageable: false,
            //height: '400px',
            scrollable: true,
            groupable: true,
            columns: [
                { field: "LK_DIA_TYPE_ID", hidden: true },
                { field: "LK_FBR_GRP_ID", hidden: true },
                { field: "RF_FAB_TYPE_ID", hidden: true },
                { field: "RF_FIB_COMP_ID", hidden: true },
                { field: "KNT_STYL_FAB_ITEM_H_ID", hidden: true },
                //{ field: "FABRIC_GROUP_NAME", title: "Fab Grp", type: "string", width: "5%", },
                { field: "YARN_SPEC", title: "Fabric Details", type: "string", width: "50%" },
                //{ field: "FIB_COMP_NAME", title: "Fiber Comp.", type: "string", width: "15%" },
                { field: "LK_DIA_TYPE_NAME", title: "Dia Type", type: "string", width: "5%" },
                //{ field: "RQD_GSM", title: "Req. GSM", type: "string", width: "7%" },
                { field: "RQD_QTY", title: "Grey Qty.", type: "decimal", width: "7%" },
                { field: "SUB_LOT_NO", title: "Sub-Lot", type: "string", width: "7%" },
                { field: "LOT_QTY", title: "Lot Qty.", type: "decimal", width: "7%" },
                { field: "FINIS_QTY", title: "Finish Qty", type: "decimal", width: "7%" },
                { field: "FAB_STR_POS", title: "Fabric Position", type: "string", width: "10%" },
                { field: "QC_STS_TYP_NAME", title: "QC Status", type: "string", width: "10%" },
                { field: "SUB_LOT_GRP_NAME", title: "Sub Lot Group", type: "string", width: "10%" },

            ]
        };


        vm.printFabricInspection = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-6006';

            vm.form.DF_FAB_QC_RPT_ID = dataItem.DF_FAB_QC_RPT_ID;
            vm.form.SUB_LOT_NO = dataItem.SUB_LOT_NO;

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
        }


        vm.printSubBatchCard = function (dataItem) {
            console.log(dataItem);
            var Object = {
                REPORT_CODE: 'RPT-4041',
                DF_FAB_QC_RPT_ID: dataItem.DF_FAB_QC_RPT_ID,
                DF_BT_SUB_LOT_ID: dataItem.DF_BT_SUB_LOT_ID,
                SUB_BATCH_NO: dataItem.SUB_BATCH_NO
            }
            //vm.form.REPORT_CODE = 'RPT-4041';

            //vm.form.DF_FAB_QC_RPT_ID = dataItem.DF_FAB_QC_RPT_ID;
            //vm.form.DF_BT_SUB_LOT_ID = dataItem.DF_BT_SUB_LOT_ID;
            //vm.form.SUB_BATCH_NO = dataItem.SUB_BATCH_NO;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Dye/DyeReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = Object;

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
        }

        vm.submitQcAll = function (dataOri) {

            if (fnValidateSub2() == true) {
                var data = angular.copy(dataOri);

                var pList = _.filter(vm.QcParamList, function (x) { return x.LK_PARAM_QC_STS_ID == 712 || x.LK_PARAM_QC_STS_ID == 713 });
                var List = _.filter(vm.QcParamList, function (x) { return x.LK_PARAM_QC_STS_ID > 0 });

                if (List.length == 0) {
                    config.appToastMsg("MULTI-005 There is no check list condition!!!");
                    return;
                }

                if (pList.length > 0 && (data.KNT_QC_STS_TYPE_ID == 1 || data.KNT_QC_STS_TYPE_ID == 5 || data.KNT_QC_STS_TYPE_ID == 6)) {
                    config.appToastMsg("MULTI-005 You Can not Pass this Batch. Please Approvr Check List Condition!!!");
                    return;
                }

                data["XML_PARAM"] = DyeingDataService.xmlStringShort(vm.QcParamList.map(function (o) {
                    return {
                        DF_BT_QC_CHQ_LST_ID: o.DF_BT_QC_CHQ_LST_ID == null ? 0 : o.DF_BT_QC_CHQ_LST_ID,
                        DYE_BT_CARD_H_ID: data.DYE_BT_CARD_H_ID,
                        RF_DF_QC_PARAM_TYPE_ID: o.RF_DF_QC_PARAM_TYPE_ID == null ? 0 : o.RF_DF_QC_PARAM_TYPE_ID,
                        CHQ_IGNR_RSN_DESC: o.CHQ_IGNR_RSN_DESC,
                        LK_PARAM_QC_STS_ID: o.LK_PARAM_QC_STS_ID == null ? 1 : o.LK_PARAM_QC_STS_ID,
                    }
                }));


                return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DfFabInsp/SaveQC').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        if (res.data.OPDF_FAB_QC_RPT_ID > 0) {
                            vm.form = { QC_DT: vm.today };
                            $state.go($state.current, { pDF_FAB_QC_RPT_ID: res.data.OPDF_FAB_QC_RPT_ID }, { reload: true });
                        }
                    }
                });
            }
        }



        vm.submitAll = function (dataOri, id) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                data["IS_FINALIZED"] = id;
                data["HR_SCHEDULE_H_ID"] = (vm.QcRollList[0].HR_SCHEDULE_H_ID || 1);

                data["XML_QC_D"] = DyeingDataService.xmlStringShort(vm.QcRollList.map(function (o) {
                    return {
                        DF_BT_SUB_LOT_ID: o.DF_BT_SUB_LOT_ID == null ? 0 : o.DF_BT_SUB_LOT_ID,
                        DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID == null ? 0 : o.DYE_BT_CARD_H_ID,
                        DF_FAB_QC_RPT_D_ID: o.DF_FAB_QC_RPT_D_ID == null ? 0 : o.DF_FAB_QC_RPT_D_ID,
                        //SHADE_GRP: o.SHADE_GRP,
                        //IS_BATCH_SPLT: o.IS_BATCH_SPLT,
                        LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID == 0 ? data.LK_DIA_TYPE_ID : o.LK_DIA_TYPE_ID,
                        LOT_QTY: o.LOT_QTY,
                        NO_OF_ROLL: o.NO_OF_ROLL,
                        FINIS_QTY: o.FINIS_QTY,
                        QTY_MOU_ID: 3,
                        FAB_STR_POS: o.FAB_STR_POS,
                        LK_SUB_LOT_GRP_ID: o.LK_SUB_LOT_GRP_ID == 0 ? null : o.LK_SUB_LOT_GRP_ID,
                        IS_RP_RQD: o.IS_RP_RQD,
                        MAJ_DFCT_TYPE_ID: o.MAJ_DFCT_TYPE_ID,
                        HR_SCHEDULE_H_ID: o.HR_SCHEDULE_H_ID == 0 ? null : o.HR_SCHEDULE_H_ID,
                        QC_DT: $filter('date')(o.QC_DT, 'yyyy-MM-dd'),
                        QC_BY: o.QC_BY == 0 ? null : o.QC_BY,
                        SHFT_IN_CHRGE: o.SHFT_IN_CHRGE == 0 ? null : o.SHFT_IN_CHRGE,
                        OTH_DFCT_TYPE_LST: o.OTH_DFCT_TYPE_LST == null ? "" : config.xmlStringShortNoTag(o.OTH_DFCT_TYPE_LST.map(function (o) {
                            return {
                                DF_BT_SUB_LOT_DFCT_ID: o.DF_BT_SUB_LOT_DFCT_ID,
                                DF_BT_SUB_LOT_ID: o.DF_BT_SUB_LOT_ID,
                                RF_FB_DFCT_TYPE_ID: o.RF_FB_DFCT_TYPE_ID,
                                RF_RESP_DEPT_ID: o.RF_RESP_DEPT_ID
                            }
                        })),
                        KNIT_ITEM_LST: o.KNIT_ITEM_LST == null ? "" : config.xmlStringShortNoTag(o.KNIT_ITEM_LST.map(function (o) {
                            return {
                                DF_BT_SUB_LOT_FAB_ID: o.DF_BT_SUB_LOT_FAB_ID,
                                DF_BT_SUB_LOT_ID: o.DF_BT_SUB_LOT_ID,
                                KNT_STYL_FAB_ITEM_H_ID: o.KNT_STYL_FAB_ITEM_H_ID,
                                LOT_QTY: o.LOT_QTY,
                                FINIS_QTY: o.FINIS_QTY,
                                QTY_MOU_ID: 3,
                                NO_OF_ROLL: o.NO_OF_ROLL,
                                ACT_NO_ROLL: o.ACT_NO_ROLL,
                                ACT_FINIS_QTY: o.ACT_FINIS_QTY,
                                REMARKS: o.REMARKS,
                                FLAT_KNIT_ITEM_LIST: o.FLAT_KNIT_ITEM_LIST == null ? "" : config.xmlStringShortNoTagChild(o.FLAT_KNIT_ITEM_LIST.map(function (o) {
                                    return {
                                        DF_BT_SUB_LOT_CLCF_ID: o.DF_BT_SUB_LOT_CLCF_ID,
                                        DF_BT_SUB_LOT_ID: o.DF_BT_SUB_LOT_ID,
                                        KNT_CLCF_STYL_ITEM_ID: o.KNT_CLCF_STYL_ITEM_ID,
                                        RQD_GREY_MEAS: o.RQD_GREY_MEAS,
                                        ACT_FIN_MEAS: (o.ACT_FIN_MEAS || o.RQD_GREY_MEAS),
                                        RQD_GREY_PCS: o.RQD_GREY_PCS,
                                        ACT_FIN_PCS: o.ACT_FIN_PCS,
                                        REJECT_PCS: o.REJECT_PCS
                                    }
                                })),
                            }
                        })),
                        IS_RP_DONE: o.IS_RP_DONE,
                        PARENT_ID: o.PARENT_ID,
                        LK_RTN_TYP_ID: o.LK_RTN_TYP_ID == 0 ? null : o.LK_SUB_LOT_GRP_ID,
                        SUB_LOT_NO: o.SUB_LOT_NO == null ? data.DYE_LOT_NO : o.DF_BT_SUB_LOT_ID > 0 ? o.SUB_LOT_NO : (data.DYE_LOT_NO + '/' + o.SUB_LOT_NO),
                        KNT_QC_STS_TYPE_ID: o.KNT_QC_STS_TYPE_ID == null ? 1 : o.KNT_QC_STS_TYPE_ID,
                    }
                }));

                console.log(data);

                return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DfFabInsp/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        if (res.data.OPDF_FAB_QC_RPT_ID > 0) {
                            vm.form = { QC_DT: vm.today };
                            $state.go($state.current, { pDF_FAB_QC_RPT_ID: res.data.OPDF_FAB_QC_RPT_ID }, { reload: true });
                        }

                    }
                });
            }
        }
    }

})();