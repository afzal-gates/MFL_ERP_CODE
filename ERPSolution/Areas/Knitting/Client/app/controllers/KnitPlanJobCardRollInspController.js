(function () {
    'use strict';
    angular.module('multitex.menulesspage').controller('KnitPlanJobCardRollInspController', ['$q', '$stateParams', '$state', '$scope', 'MenuLessPageDataService', '$filter', 'cur_user_id', '$modal', KnitPlanJobCardRollInspController]);
    function KnitPlanJobCardRollInspController($q, $stateParams, $state, $scope, MenuLessPageDataService, $filter, cur_user_id, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [defectList(), getUserData(), getShiftData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        vm.form = { 'TOTAL_PT': 0, QC_BY: cur_user_id };

        vm.ClearForm = function () {
            vm.form = { 'TOTAL_PT': 0, QC_BY: cur_user_id };
            defectList();
        }

        vm.openRollInspDtl = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'RollInspDtl.html',
                controller: function ($scope, $modalInstance, MenuLessPageDataService) {

                    $scope.formItemList = {};
                    //loadAllHold();
                    //colorList();
                    buyerAccGroupList();

                    function loadAllHold() {

                        MenuLessPageDataService.getDataByFullUrl('/api/knit/KnitPlanRollInsp/getRollDataList?pKNT_QC_STS_TYPE_ID=3&pMC_BYR_ACC_GRP_ID=' + $scope.MC_BYR_ACC_GRP_ID + '&pMC_COLOR_ID=' + $scope.MC_COLOR_ID + '&pSTYLE_ORDER_NO=' + ($scope.STYLE_ORDER_NO || "") + '&pKNT_MACHINE_NO=' + ($scope.KNT_MACHINE_NO || "")).then(function (res) {

                            $scope.formItemList = res;
                        });
                    }

                    function colorList() {
                        MenuLessPageDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll').then(function (resC) {
                            $scope.colorList = resC
                        });
                    }


                    function buyerAccGroupList() {
                        MenuLessPageDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (resC) {
                            $scope.buyerAccGrpList = resC
                        });
                    }

                    $scope.clearAll = function () {
                        $scope.MC_BYR_ACC_GRP_ID = '';
                        $scope.COLOR_NAME_EN = '';
                        $scope.STYLE_ORDER_NO = '';
                        $scope.QC_DT = '';
                    }

                    $scope.getRollDtl = function (pKNT_QC_STS_TYPE_ID) {

                        var qcDate = $filter('date')($scope.QC_DT, 'yyyy-MM-ddTHH:mm:ss');

                        MenuLessPageDataService.getDataByFullUrl('/api/knit/KnitPlanRollInsp/getRollDataList?pKNT_QC_STS_TYPE_ID=3&pMC_BYR_ACC_GRP_ID=' + $scope.MC_BYR_ACC_GRP_ID + '&pMC_COLOR_ID=' + $scope.MC_COLOR_ID + '&pSTYLE_ORDER_NO=' + ($scope.STYLE_ORDER_NO || "") + '&pCOLOR_NAME_EN=' + ($scope.COLOR_NAME_EN || "") + "&pQC_DT=" + (qcDate || "")).then(function (res) {
                            $scope.formItemList = res;
                        });
                    }

                    $scope.cancel = function (data) {
                        $modalInstance.close(data);
                    }

                    $scope.selectRollNo = function (data) {
                        $modalInstance.close(data);
                    }

                    $scope.submitData = function (dataOri, id) {
                        console.log(dataOri);
                        var data = angular.copy(dataOri);

                        if (id == 1 && data.QC_NOTE.length <= 0) {
                            alert("Please Select QC Note!!!");

                            return;
                        }
                        data.KNT_QC_STS_TYPE_ID = id;

                        return MenuLessPageDataService.saveDataByFullUrl(data, '/api/knit/KnitPlanRollInsp/Save').then(function (res) {
                            if (res.success === false) {
                                vm.errors = res.errors;
                            }
                            else {

                                res['data'] = angular.fromJson(res.jsonStr);

                                $scope.alerts = [];
                                $scope.closeAlert = function (index) {
                                    $scope.alerts.splice(index, 1);
                                };

                                var typ = "";
                                var msgtyp = res.data.PMSG.substring(0, 9);
                                var mesg = res.data.PMSG.substring(9, res.data.PMSG.length);

                                if (msgtyp == "MULTI-001")
                                    typ = "success";
                                else
                                    typ = "danger";

                                $scope.alerts.push({
                                    type: typ, msg: mesg
                                });
                                loadAllHold()

                            }
                        }).catch(function (message) {
                            exception.catcher('XHR loading Failded')(message);
                        });
                    }

                },
                size: 'lg',
                windowClass: 'app-modal-window',
                //resolve: {
                //    LoanItemList: function (DyeingDataService) {
                //        return DyeingDataService.getDataByFullUrl('/api/dye/DCLoanReturn/GetSupplierLoanDtlByID/' + (SCM_SUPPLIER_ID || 0) + '/' + vm.form.SCM_STORE_ID);
                //    }
                //}
            });

            modalInstance.result.then(function (item) {
                vm.form.FAB_ROLL_NO = item.FAB_ROLL_NO;
                if (item.FAB_ROLL_NO.length > 0) {

                    vm.loadDataByJobCardNo(item.FAB_ROLL_NO);
                }
            });

        };


        vm.loadDataByJobCardNo = function (FAB_ROLL_NO) {

            MenuLessPageDataService.getDataByFullUrl('/api/knit/KnitPlanRollInsp/RollQCDelails?pFAB_ROLL_NO=' + (FAB_ROLL_NO)).then(function (res) {
                vm.formItemList = res
                var lst = _.filter(res, function (x) { return x.DFCT_QTY > 0 })
                vm.defectList = lst;
            });

            MenuLessPageDataService.getDataByFullUrl('/api/knit/KnitPlanRollInsp/RollDelails?pFAB_ROLL_NO=' + (FAB_ROLL_NO)).then(function (res) {
                vm.form = res;
                console.log(res);
                if (!res.FAB_ROLL_NO) {
                    $scope.alerts = [];
                    $scope.closeAlert = function (index) {
                        $scope.alerts.splice(index, 1);
                    };

                    $scope.alerts.push({
                        type: "danger", msg: "QC Status already updated!!!"
                    });
                }
            });

            angular.element('#FAB_ROLL_NO').focus();
        }

        vm.addOne = function (item) {
            if (item.DFCT_QTY)
                item.DFCT_QTY += 1;
            else
                item.DFCT_QTY = 1;
            console.log(item);
            var chk = _.filter(vm.defectList, function (x) { return x.RF_FB_DFCT_TYPE_ID === item.RF_FB_DFCT_TYPE_ID }).length;
            if (chk > 0)
                return;
            else {
                console.log(item.RF_FB_DFCT_TYPE_ID);
                vm.defectList.splice(1, "0", item);
            }
        }

        vm.updateDDL = function (item) {
            if (item.DFCT_QTY > 0) {
                vm.defectList.splice(1, "0", item);
            }
            else {
                var index = vm.defectList.indexOf(item);
                vm.defectList.splice(index, 1);
            }
        }

        vm.subOne = function (item) {
            if (item.DFCT_QTY > 0)
                item.DFCT_QTY -= 1;

            if (item.DFCT_QTY == 0) {

                var index = vm.defectList.indexOf(item);
                vm.defectList.splice(index, 1);
            }
        }


        vm.ClaculateDefectResult = function () {

            var total = 0;

            for (var i = 0; i < vm.formItemList.length; i++) {
                var item = vm.formItemList[i];
                if (item.IS_CALC_APLY == 'Y')
                    total = total + (item.DFCT_QTY * item.STD_PT);
            }

            vm.form.TOTAL_PT = total.toFixed(2);

            var gsm = vm.form.FIN_GSM;
            var gsmList = vm.form.FIN_GSM.split('-')
            if (gsmList.length > 1) {
                gsm = gsmList[1];
            }

            var point = ((total * 100 * parseInt(gsm)) / (vm.form.FAB_ROLL_WT * 1000 * 1.2)).toFixed(2);
            vm.form.GRADE_PT = point

            MenuLessPageDataService.getDataByFullUrl('/api/knit/KnitPlanRollInsp/GetRollQCGRD?pGRADE_PT=' + point).then(function (res) {
                vm.form.GRADE_NO = res.QC_GRD_NAME;
                vm.form.LK_QC_GRD_ID = res.LK_QC_GRD_ID;
            });
            //vm.form.GRADE_NO = total > 70 ? 'F' : total > 50 ? 'C' : total > 30 ? 'B' : total > 0 ? 'A' : 'F';

        }

        function getUserData() {

            MenuLessPageDataService.getDataByFullUrl('/api/knit/KnitPlanRollInsp/getQCUserList').then(function (res) {
                console.log(res);
                var lst = [{
                    EMP_FULL_NAME_EN: '-- Select Operator --',
                    HR_EMPLOYEE_ID: -1
                }].concat(res);
                vm.userList = lst;
            });
        }

        function getShiftData() {

            MenuLessPageDataService.getDataByFullUrl('/api/knit/KntMachinOprAssign/GetKnitScheduleList').then(function (res) {
                var lst = [{
                    SCHEDULE_NAME_EN: '-- Select Shift --',
                    HR_SCHEDULE_H_ID: -1
                }].concat(res);
                vm.shiftList = lst;
            });
        }

        function defectList() {
            MenuLessPageDataService.getDataByFullUrl('/api/security/UserMap/GetFabDfctTypeMapByID?pLK_FAB_INSP_TYPE_ID=707&pOption=3002').then(function (res) {
                //MenuLessPageDataService.getDataByFullUrl('/api/knit/KnitPlanRollInsp/DefectTypeList').then(function (res) {
                //var list = _.filter(res, function (x) { return x.RF_RESP_DEPT_ID == 4 });
                //console.log("AAAAA");
                //console.log(res);
                //console.log("AAAAA");
                vm.formItemList = res;
                vm.defectList = _.filter(res, function (x) { return x.RF_FB_DFCT_TYPE_ID == 0 });
            });
        }

        vm.loadDefectValue = function (data) {
            var item = _.filter(vm.formItemList, function (x) { return x.DISPLAY_ORDER === data });
            console.log(vm.formItemList)
        }

        vm.submitData = function (dataOri, id) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                data.KNT_QC_STS_TYPE_ID = id;

                if (id > 1)
                    if (!data.HOLD_DFCT_TYPE_ID) {
                        alert("Please Select Defect Type!!!")
                        return;
                    }

                var list = _.filter(vm.formItemList, function (x) { return x.DFCT_QTY > 0 });

                data["XML_QC_D"] = MenuLessPageDataService.xmlStringShort(list.map(function (o) {
                    return {
                        KNT_ROLL_QC_PT_ID: o.KNT_ROLL_QC_PT_ID == null ? 0 : o.KNT_ROLL_QC_PT_ID,
                        KNT_FAB_ROLL_ID: vm.form.KNT_FAB_ROLL_ID == null ? 0 : vm.form.KNT_FAB_ROLL_ID,
                        RF_FB_DFCT_TYPE_ID: o.RF_FB_DFCT_TYPE_ID,
                        DFCT_QTY: o.DFCT_QTY == null ? 3 : o.DFCT_QTY,
                        EARN_PT: o.STD_PT * o.DFCT_QTY,
                    }
                }));

                return MenuLessPageDataService.saveDataByFullUrl(data, '/api/knit/KnitPlanRollInsp/Save').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        $scope.alerts = [];
                        $scope.closeAlert = function (index) {
                            $scope.alerts.splice(index, 1);
                        };

                        var typ = "";
                        var msgtyp = res.data.PMSG.substring(0, 9);
                        var mesg = res.data.PMSG.substring(9, res.data.PMSG.length);

                        if (msgtyp == "MULTI-001")
                            typ = "success";
                        else
                            typ = "danger";

                        $scope.alerts.push({
                            type: typ, msg: mesg
                        });

                        vm.form = {};
                        defectList();

                        angular.element('#FAB_ROLL_NO').focus();

                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }


    }


})();