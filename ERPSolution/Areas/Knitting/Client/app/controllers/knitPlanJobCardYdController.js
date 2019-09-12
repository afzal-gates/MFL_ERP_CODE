(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitPlanJobCardYdController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'JobCardHeader', 'Dialog', '$modal', '$window', 'YarnLotList', '$modalInstance', KnitPlanJobCardYdController]);
    function KnitPlanJobCardYdController($q, config, KnittingDataService, $stateParams, $state, $scope, JobCardHeader, Dialog, $modal, $window, YarnLotList, $modalInstance) {
        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        activate()
        vm.showSplash = true;

        console.log(JobCardHeader.details);
        console.log(YarnLotList);

        
        function activate() {
            var promise = [getKnitMachineList(), getFloorList(), runYarnDetails(), getMachineGaugeList(), getJobCardList(), getSubContractProgramData(), getMcDiaList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.state = $stateParams.state;

        function getMcDiaList() {
            return KnittingDataService.getDataByUrl('/KnitCommon/getMachineDiaList').then(function (res) {
                vm.machineDiaDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        if ($stateParams.pKNT_JOB_CRD_H_ID && $stateParams.pKNT_JOB_CRD_H_ID > 0) {
            //vm.template = '<span><h4 style="padding:0px;margin:0px;"><b>#: data.YRN_LOT_NO #</b></h4></span>';
            //vm.value_template = '<span><h5 style="padding:0px;margin:0px;"><b>#: data.YRN_LOT_NO # </b></h5></span>';

            vm.template = '<span><h4 style="padding:0px;margin:0px;"><b>#: data.YRN_LOT_NO #</b></h4><p> Stock: #: data.STK_BALANCE # #: data.MOU_CODE #</p></span>';
            vm.value_template = '<span><h5 style="padding:0px;margin:0px;"><b>#: data.YRN_LOT_NO # </b> (#: data.STK_BALANCE # #: data.MOU_CODE #)</h5></span>';

        } else {
            vm.template = '<span><h4 style="padding:0px;margin:0px;"><b>#: data.YRN_LOT_NO #</b></h4><p> Stock: #: data.STK_BALANCE # #: data.MOU_CODE #</p></span>';
            vm.value_template = '<span><h5 style="padding:0px;margin:0px;"><b>#: data.YRN_LOT_NO # </b> (#: data.STK_BALANCE # #: data.MOU_CODE #)</h5></span>';
        }


        vm.params = $stateParams;



        vm.dtFormat = "dd-MMM-yyyy HH:mm";
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.START_DT_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.START_DT_OPENED = true;
        };

        vm.END_DT_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.END_DT_OPENED = true;
        };




        function getMachineGaugeList() {
            return KnittingDataService.LookupListData(59).then(function (res) {
                vm.MachineGaugeListDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        $scope.data = JobCardHeader;
        $scope.data['IS_FINALIZED_SC'] = JobCardHeader.IS_FINALIZED;

        $scope.data['ASIGN_QTY'] = JobCardHeader.KNT_JOB_CRD_H_ID > 0 ? JobCardHeader.ASIGN_QTY : JobCardHeader.QTY_LEFT;



        function runYarnDetails() {
            angular.forEach(JobCardHeader.details, function (val, key) {
                val['yarnLotListDs'] = new kendo.data.DataSource({
                    data: _.filter(YarnLotList, function (o) {
                        return o.YARN_ITEM_ID == val.YARN_ITEM_ID && o.KNT_PLAN_D_ID == val.KNT_PLAN_D_ID;
                    })
                });

                val['RQD_YRN_QTY'] = (JobCardHeader.KNT_JOB_CRD_H_ID && JobCardHeader.KNT_JOB_CRD_H_ID > 0) ? val.RQD_YRN_QTY_JC : parseFloat(parseFloat((val.RATIO_QTY / 100) * (JobCardHeader.ASIGN_QTY || JobCardHeader.QTY_LEFT)).toFixed(2));

                //if (val['RQD_YRN_QTY'] > JobCardHeader.QTY_LEFT && !$stateParams.pKNT_JOB_CRD_H_ID) {
                //    //$scope.JobCardParams.ASIGN_QTY.$setValidity('required', false);
                //};

                //var YarnItem = _.find(YarnLotList, function (o) {
                //    return o.KNT_YRN_LOT_ID == parseInt(val.KNT_YRN_LOT_ID);
                //});


                //if (YarnItem) {

                //    if (val.RQD_YRN_QTY > YarnItem.STK_BALANCE) {
                //        val['INVALID_LOT'] = true;
                //        $scope.YarnDetailsJobCard.$valid = false;
                //    } else if (val.RQD_YRN_QTY <= YarnItem.STK_BALANCE) {
                //        val['INVALID_LOT'] = false;
                //        $scope.YarnDetailsJobCard.$valid = true;
                //    };
                //}


            })
        }


        function runYarnDetailsTrns(data) {
            var dt2ret = [];
            angular.forEach(data.details, function (val, key) {

                val['yarnLotListDs'] = new kendo.data.DataSource({
                    data: _.filter(YarnLotList, function (o) {
                        return o.YARN_ITEM_ID == val.YARN_ITEM_ID;
                    })
                });
                val['RQD_YRN_QTY'] = (data.KNT_JOB_CRD_H_ID && data.KNT_JOB_CRD_H_ID > 0) ? val.RQD_YRN_QTY_JC : parseFloat(parseFloat((val.RATIO_QTY / 100) * (data.ASIGN_QTY || data.QTY_LEFT)).toFixed(2));
                dt2ret.push(val);
            });
            return dt2ret;
        }



        function setSchedule(KNT_MACHINE_ID, DURATION) {
            return KnittingDataService.getDataByUrl('/KnitPlan/getScheduleByMachine?pKNT_MACHINE_ID=' + KNT_MACHINE_ID + '&pDURATION=' + DURATION).then(function (res) {

                $scope.data['START_DT'] = res.START_DT;
                $scope.data['END_DT'] = res.END_DT;

            });
        }

        vm.onMachineChange = function (e) {
            $scope.JobCardParams.$submitted = true;
            if (e.sender.dataItem(e.sender.item).KNT_MACHINE_ID && ($scope.data.TG_D_PROD_QTY || 0) > 0 && ($scope.data.ASIGN_QTY || 0) > 0) {
                setSchedule(e.sender.dataItem(e.sender.item).KNT_MACHINE_ID, Math.ceil($scope.data.ASIGN_QTY * (24 / ($scope.data.TG_D_PROD_QTY || 1))));
            }
        };


        vm.onFlrChange = function (e) {

            var HR_PROD_FLR_ID = e.sender.dataItem(e.sender.item).HR_PROD_FLR_ID;
            if (HR_PROD_FLR_ID) {
                getKnitMachineList(($scope.data.ACT_MC_DIA_ID || null), HR_PROD_FLR_ID);
            } else {
                getKnitMachineList(($scope.data.ACT_MC_DIA_ID || null), null);
            }
        };

        $scope.$watchGroup(['data.ASIGN_QTY', 'data.TG_D_PROD_QTY'], function (newVal, oldVal) {
            if ($scope.data.IS_FINALIZED == 'N' && $scope.data.KNT_MACHINE_ID) {
                if (!isNaN(Math.ceil(newVal[0] * (24 / (newVal[1] || 1))))) {
                    setSchedule($scope.data.KNT_MACHINE_ID, Math.ceil(newVal[0] * (24 / (newVal[1] || 1))));
                }
            }
        });


        $scope.$watch('data.ASIGN_QTY', function (newVal, oldVal) {
            if (newVal && newVal != oldVal) {
                runYarnDetails();
            }
        });
        function getSubContractProgramData() {

            if ($stateParams.pIS_SUB_CONTR && $stateParams.pKNT_SC_PRG_ISS_ID) {

                return KnittingDataService.getDataByUrl('/KnitPlan/getSubContractProgramData?pKNT_SC_PRG_ISS_ID=' + ($stateParams.pKNT_SC_PRG_ISS_ID || null)).then(function (res) {
                    console.log(res);
                    vm.TitleAdi = ' For Sub Contract Program [Ref# : ' + res.PRG_ISS_NO + ' Supplier :' + res.SUP_TRD_NAME_EN + ']';
                    vm.ScProData = res;
                });

            } else {
                vm.TitleAdi = ' For In-House';
                vm.ScProData = {
                    IS_REQS_DONE: 'N'
                };
            }


        };

        vm.onChangeYarnLot = function (itm, e) {

            console.log(itm);

            var item = e.sender.dataItem(e.sender.item);
            if (item.KNT_YRN_LOT_ID) {
                itm['RF_BRAND_ID'] = item.RF_BRAND_ID;
                itm['KNT_PLAN_D_ID'] = item.KNT_PLAN_D_ID;

                if (itm.RQD_YRN_QTY == 0) {
                    itm['RQD_YRN_QTY'] = item.STK_BALANCE;
                }
                itm['STK_BALANCE'] = item.STK_BALANCE;


            } else {
                itm['RF_BRAND_ID'] = '';
                itm['KNT_PLAN_D_ID'] = '';
                itm['RQD_YRN_QTY'] = 0;
            }
        }

        vm.yarnLotListOnBound = function (itm, e) {

            console.log(itm);

            var ds = e.sender.dataSource.data();
            var item = e.sender.dataItem(e.sender.item);
            console.log(ds);

            if (ds.length == 1 && (itm.KNT_JOB_CRD_D_ID || -1) <= 0) {
                e.sender.value(ds[0].KNT_YRN_LOT_ID);
                
                itm['KNT_YRN_LOT_ID'] = ds[0].KNT_YRN_LOT_ID;

                itm['RF_BRAND_ID'] = ds[0].RF_BRAND_ID;
                itm['KNT_PLAN_D_ID'] = ds[0].KNT_PLAN_D_ID;
                itm['RQD_YRN_QTY'] = ds[0].STK_BALANCE;
                itm['STK_BALANCE'] = ds[0].STK_BALANCE;
            }
            //else if ((itm.KNT_JOB_CRD_D_ID || -1) <= 0) {
            //    var data = _.filter(ds, function (ob) {
            //        return ob.KNT_YRN_LOT_ID == itm['KNT_YRN_LOT_ID'] && ob.YARN_ITEM_ID == itm['YARN_ITEM_ID'] && ob.KNT_PLAN_D_ID == itm['KNT_PLAN_D_ID'];
            //    });
            //    console.log(data);
                
            //    e.sender.value(data[0].KNT_YRN_LOT_ID);

            //    itm['KNT_YRN_LOT_ID'] = data[0].KNT_YRN_LOT_ID;

            //    itm['RF_BRAND_ID'] = data[0].RF_BRAND_ID;
            //    itm['KNT_PLAN_D_ID'] = data[0].KNT_PLAN_D_ID;
            //    itm['RQD_YRN_QTY'] = data[0].STK_BALANCE;
            //    itm['STK_BALANCE'] = data[0].STK_BALANCE;
            //}

            if (item.KNT_YRN_LOT_ID) {
                itm['RF_BRAND_ID'] = item.RF_BRAND_ID;
                itm['KNT_PLAN_D_ID'] = item.KNT_PLAN_D_ID;
            }
        }



        vm.createStoreRequisition = function (data) {
            Dialog.confirm('Do you really want to create Requisition? <br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {
                 KnittingDataService.saveDataByUrl({}, '/KnitPlan/CreateStoreRequisitionYdSc?pKNT_SC_PRG_ISS_ID=' + data.KNT_SC_PRG_ISS_ID).then(function (res) {
                     res['data'] = angular.fromJson(res.jsonStr);
                     if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                         getSubContractProgramData();
                     }
                     config.appToastMsg(res.data.PMSG);
                 })
             });
        };

        vm.createStoreRequisitionTrns = function () {
            Dialog.confirm('Do you really want to create Requisition & Transfer? <br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {
                 KnittingDataService.saveDataByUrl({}, '/KnitPlan/CreateStoreRequisitionYdScTrns?pKNT_SC_PRG_ISS_ID=' + $stateParams.pKNT_SC_PRG_ISS_ID).then(function (res) {
                     res['data'] = angular.fromJson(res.jsonStr);
                     if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                         $modalInstance.close({ KNT_SC_PRG_ISS_ID: $stateParams.pKNT_SC_PRG_ISS_ID });
                     }
                     config.appToastMsg(res.data.PMSG);
                 })
             });
        };

        vm.cancelModal = function () {
            $modalInstance.dismiss();
        };

        function getJobCardList() {
            if ($stateParams.pKNT_SC_PRG_ISS_ID) {
                return KnittingDataService.getDataByUrl('/KnitPlan/getJobCardList?pKNT_SC_PRG_ISS_ID=' + $stateParams.pKNT_SC_PRG_ISS_ID).then(function (res) {
                    vm.jobCardList = res;
                    vm.ENABLED_REQS = _.every(res, function (o) {
                        return o.KNT_JC_STS_TYPE_ID == 2;
                    })
                });
            } else {
                return KnittingDataService.getDataByUrl('/KnitPlan/getJobCardList?pKNT_PLAN_H_ID=' + $stateParams.pKNT_PLAN_H_ID).then(function (res) {
                    vm.ENABLED_REQS = false;
                    vm.jobCardList = res;
                });
            }

        }

        function refreshData(pKNT_JOB_CRD_H_ID) {
            return KnittingDataService.getDataByUrl('/KnitPlan/getJobCardHeaderData?pKNT_PLAN_H_ID=' + $stateParams.pKNT_PLAN_H_ID + '&pKNT_JOB_CRD_H_ID=' + (pKNT_JOB_CRD_H_ID || $stateParams.pKNT_JOB_CRD_H_ID)).then(function (res) {
                $scope.data = res;
                $scope.data['IS_FINALIZED_SC'] = res.IS_FINALIZED;
                $scope.data['ASIGN_QTY'] = res.KNT_JOB_CRD_H_ID > 0 ? res.ASIGN_QTY : res.QTY_LEFT;

                $scope.data['details'] = runYarnDetailsTrns(res);
                //setTimeout(function () {
                   
                //}, 500);
            })
        };


        function getFloorList() {
            return KnittingDataService.getDataByFullUrl('/api/hr/GetProdFloorList?pLK_PFLR_TYP_ID=527').then(function (res) {
                vm.KnitMachineFlrListDs = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        function getKnitMachineList(ACT_MC_DIA_ID, HR_PROD_FLR_ID) {
            return KnittingDataService.getDataByUrl('/KnitPlan/getKnitMachine?pACT_MC_DIA_ID=' + (ACT_MC_DIA_ID || JobCardHeader.ACT_MC_DIA_ID) + '&pHR_PROD_FLR_ID=' + (HR_PROD_FLR_ID || null)).then(function (res) {
                vm.KnitMachineListDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }


        vm.onChangeMcDia = function (e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.KNT_MC_DIA_ID) {
                getKnitMachineList(item.KNT_MC_DIA_ID);
            }
        }


        vm.cancel = function () {

            $scope.form = {
                IDX: -1,
                KNT_PLAN_D_ID: -1,
                KNT_YRN_LOT_ID: '',
                LK_YFAB_PART_ID: '',
                RF_BRAND_ID: '',
                YARN_ITEM_ID: '',
                IS_SKIP_LOT: 'N'
            };

            $scope.YarnDetails.$setPristine();
            $scope.YarnDetails.$setUntouched();

        };

        vm.submitDataTrns = function (dataOri, valid1, valid2) {
            angular.forEach(dataOri.details, function (val, key) {
                if (val.RQD_YRN_QTY > val.STK_BALANCE) {
                    val['INVALID_LOT'] = true;
                    $scope.YarnDetailsJobCard.$valid = false;
                } else if (val.RQD_YRN_QTY <= val.STK_BALANCE) {
                    val['INVALID_LOT'] = false;
                    $scope.YarnDetailsJobCard.$valid = true;
                };

            });

            var details = [];
            angular.forEach(dataOri.details, function (o, key) {
                if (parseInt(o.KNT_YRN_LOT_ID) > 0 && o.RQD_YRN_QTY > 0) {
                    details.push({
                        KNT_JOB_CRD_D_ID: o.KNT_JOB_CRD_D_ID,
                        YARN_ITEM_ID: o.YARN_ITEM_ID,
                        LK_YFAB_PART_ID: o.LK_YFAB_PART_ID || '',
                        RF_BRAND_ID: o.RF_BRAND_ID,
                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID,
                        RQD_YRN_QTY: o.RQD_YRN_QTY,
                        KNT_PLAN_D_ID: o.KNT_PLAN_D_ID,
                        STITCH_LEN: o.STITCH_LEN
                    });
                }
            });

            var validYarnLot = _.some(dataOri.details, function (o) {
                return o.INVALID_LOT == true
            });

            if (!valid1 || !valid2 || validYarnLot || details.length == 0) {
                return;
            } else {

                var data = {
                    KNT_PLAN_H_ID: $stateParams.pKNT_PLAN_H_ID,
                    KNT_JOB_CRD_H_ID: dataOri.KNT_JOB_CRD_H_ID || -1,
                    JOB_CRD_NO: dataOri.JOB_CRD_NO,
                    KNT_SC_PRG_ISS_ID: ($stateParams.pKNT_SC_PRG_ISS_ID || null),
                    HR_PROD_FLR_ID: dataOri.HR_PROD_FLR_ID,
                    ACT_MC_DIA_ID: dataOri.ACT_MC_DIA_ID,
                    ACT_FIN_DIA: dataOri.ACT_FIN_DIA,
                    ACT_FIN_GSM: dataOri.ACT_FIN_GSM,
                    ASIGN_QTY: _.sumBy(details, function (o) { return o.RQD_YRN_QTY; }),
                    TG_D_PROD_QTY: dataOri.TG_D_PROD_QTY,
                    PROD_RATE: dataOri.PROD_RATE,
                    QTY_MOU_ID: 3,
                    REMARKS: dataOri.REMARKS,
                    IS_SUB_CONTR: dataOri.IS_SUB_CONTR || 'N',
                    SCM_SUPPLIER_ID: dataOri.SCM_SUPPLIER_ID,
                    START_DT: dataOri.START_DT,
                    END_DT: dataOri.END_DT,
                    IS_FINALIZED: dataOri.IS_FINALIZED_SC,

                    MC_FAB_PROD_ORD_H_ID: dataOri.MC_FAB_PROD_ORD_H_ID,
                    MC_ORDER_H_ID_LST: dataOri.MC_ORDER_H_ID_LST,
                    MC_STYLE_H_EXT_ID: dataOri.MC_STYLE_H_EXT_ID,

                    MC_BUYER_ID: dataOri.MC_BUYER_ID,
                    MC_BYR_ACC_ID: dataOri.MC_BYR_ACC_ID,
                    MC_STYLE_H_ID: dataOri.MC_STYLE_H_ID,
                    KNT_MACHINE_ID: dataOri.KNT_MACHINE_ID,

                    XML: KnittingDataService.xmlStringShort(details),
                }

                return KnittingDataService.saveDataByUrl(data, '/KnitPlan/SaveJobCardData').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            refreshData(parseInt(res.data.OP_KNT_JOB_CRD_H_ID));
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }

        vm.submitData = function (dataOri, valid1, valid2) {
            //if (!dataOri.KNT_MACHINE_ID && !$stateParams.pIS_SUB_CONTR) {
            //    $scope.JobCardParams.KNT_MACHINE_ID.$setValidity('required', false);
            //}

            var vFormValid = "Y";

            angular.forEach(dataOri.details, function (val, key) {
                
                val['INVALID_LOT'] = false;
                val['INVALID_RQD_YRN_QTY'] = false;

                if (val.RQD_YRN_QTY == null || val.RQD_YRN_QTY < 0) {
                    val['INVALID_RQD_YRN_QTY'] = true;                  
                }
                else if (val.RQD_YRN_QTY > val.STK_BALANCE) {
                    val['INVALID_LOT'] = true;                    
                }
                else if (val.RQD_YRN_QTY <= val.STK_BALANCE) {
                    val['INVALID_LOT'] = false;                    
                };

               
                if (vFormValid == "Y" && (val['INVALID_RQD_YRN_QTY'] == true || val['INVALID_LOT'] == true)) {
                    vFormValid = "N";
                }

            });

            if (vFormValid == "N") {
                $scope.YarnDetailsJobCard.$valid = false;
            }
            else {
                $scope.YarnDetailsJobCard.$valid = true;
            }

            var details = [];
            angular.forEach(dataOri.details, function (o, key) {
                if (parseInt(o.KNT_YRN_LOT_ID) > 0 && o.RQD_YRN_QTY > 0) {
                    details.push({
                        KNT_JOB_CRD_D_ID: o.KNT_JOB_CRD_D_ID,
                        YARN_ITEM_ID: o.YARN_ITEM_ID,
                        LK_YFAB_PART_ID: o.LK_YFAB_PART_ID || '',
                        RF_BRAND_ID: o.RF_BRAND_ID,
                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID,
                        RQD_YRN_QTY: o.RQD_YRN_QTY,
                        KNT_PLAN_D_ID: o.KNT_PLAN_D_ID,
                        STITCH_LEN: o.STITCH_LEN
                    });
                }
            });




            var validYarnLot = _.some(dataOri.details, function (o) {
                return o.INVALID_LOT == true
            });
            var validYarnReqQty = _.some(dataOri.details, function (o) {
                return o.INVALID_RQD_YRN_QTY == true
            });

            if (!valid1 || !valid2 || validYarnLot || validYarnReqQty || details.length == 0) {
                return;
            } else {

                var data = {
                    KNT_PLAN_H_ID: $stateParams.pKNT_PLAN_H_ID,
                    KNT_JOB_CRD_H_ID: dataOri.KNT_JOB_CRD_H_ID || -1,
                    JOB_CRD_NO: dataOri.JOB_CRD_NO,
                    KNT_SC_PRG_ISS_ID: ($stateParams.pKNT_SC_PRG_ISS_ID || null),
                    HR_PROD_FLR_ID: dataOri.HR_PROD_FLR_ID,
                    ACT_MC_DIA_ID: dataOri.ACT_MC_DIA_ID,
                    ACT_FIN_DIA: dataOri.ACT_FIN_DIA,
                    ACT_FIN_GSM: dataOri.ACT_FIN_GSM,
                    ASIGN_QTY: _.sumBy(details, function (o) { return o.RQD_YRN_QTY; }),
                    TG_D_PROD_QTY: dataOri.TG_D_PROD_QTY,
                    PROD_RATE: dataOri.PROD_RATE,
                    QTY_MOU_ID: 3,

                    FAB_ITEM_DESC: dataOri.FAB_ITEM_DESC,
                    ADDL_NOTE: dataOri.ADDL_NOTE,

                    REMARKS: dataOri.REMARKS,
                    IS_SUB_CONTR: dataOri.IS_SUB_CONTR || 'N',
                    SCM_SUPPLIER_ID: dataOri.SCM_SUPPLIER_ID,
                    START_DT: dataOri.START_DT,
                    END_DT: dataOri.END_DT,
                    IS_FINALIZED: dataOri.IS_FINALIZED_SC,

                    MC_FAB_PROD_ORD_H_ID: dataOri.MC_FAB_PROD_ORD_H_ID,
                    MC_ORDER_H_ID_LST: dataOri.MC_ORDER_H_ID_LST,
                    MC_STYLE_H_EXT_ID: dataOri.MC_STYLE_H_EXT_ID,

                    MC_BUYER_ID: dataOri.MC_BUYER_ID,
                    MC_BYR_ACC_ID: dataOri.MC_BYR_ACC_ID,
                    MC_STYLE_H_ID: dataOri.MC_STYLE_H_ID,
                    KNT_MACHINE_ID: dataOri.KNT_MACHINE_ID,

                    XML: KnittingDataService.xmlStringShort(details),
                }

                return KnittingDataService.saveDataByUrl(data, '/KnitPlan/SaveJobCardData').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $state.go('KnitPlanJobCardYd', { pKNT_JOB_CRD_H_ID: parseInt(res.data.OP_KNT_JOB_CRD_H_ID) }, { reload: true });
                            // refreshData(parseInt(res.data.OP_KNT_JOB_CRD_H_ID));
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }

        //function openUnAssignQtyModal(itm) {

        //    var modalInstance = $modal.open({
        //        animation: true,
        //        templateUrl: 'UnAssignQtyModal.html',
        //        controller: function ($scope, KnittingDataService, $modalInstance, $q, JobCard) {


        //            $scope.form = JobCard;
        //            $scope.form['UN_ASIGN_QTY_ORI'] = (JobCard.ASIGN_QTY - JobCard.PROD_QTY);
        //            $scope.form['UN_ASIGN_QTY'] = (JobCard.ASIGN_QTY - JobCard.PROD_QTY);

        //            $scope.save = function (data, isValid) {

        //                if (!isValid) {
        //                    return;
        //                }

        //                KnittingDataService.saveDataByUrl(data, '/KnitPlan/SaveUnassignJobCardData').then(function (res) {
        //                    if (res.success === false) {
        //                    }
        //                    else {
        //                        res['data'] = angular.fromJson(res.jsonStr);

        //                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
        //                            $modalInstance.close({
        //                                success: true
        //                            });
        //                        }
        //                        config.appToastMsg(res.data.PMSG);
        //                    }
        //                });
        //            };

        //            $scope.cancel = function () {
        //                $modalInstance.dismiss();
        //            }
        //        },
        //        resolve: {
        //            JobCard: function () {
        //                return itm;
        //            }
        //        },
        //        size: 'lg',
        //        windowClass: 'large-Modal'
        //    });

        //    modalInstance.result.then(function (data) {
        //        if (data.success) {
        //            $state.go('KnitPlanJobCardYd', {}, { inherit: true, reload: true });
        //        }

        //    }, function () {
        //        console.log('Modal dismissed at: ' + new Date());
        //    });
        //}

        function openUnAssignQtyModal(itm) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Knitting/Knit/_KnitCardUnAssignQtyModal',
                controller: 'KnitCardUnAssignQtyModalController',
                resolve: {
                    JobCard: function () {
                        return KnittingDataService.getDataByUrl('/KnitPlan/getJobCardHeaderData?pKNT_PLAN_H_ID=' + itm.KNT_PLAN_H_ID + '&pKNT_JOB_CRD_H_ID=' + (itm.KNT_JOB_CRD_H_ID || null));
                    }
                },
                size: 'lg',
                windowClass: 'app-modal-window'
            });

            modalInstance.result.then(function (data) {
                if (data.success) {
                    $state.go('KnitPlanJobCardYd', {}, { inherit: true, reload: true });
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }

        vm.printKnitCard = function (item) {
            var url = '/Knitting/Knit/KnitCardRpt/#/KnitCardRpt?pKNT_JOB_CRD_H_ID=' + item.KNT_JOB_CRD_H_ID
            //var a = document.createElement('a');
            //a.href = url;
            //a.target = '_blank';
            //document.body.appendChild(a);
            //a.click();

            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 300) + ',scrollbars=yes,status=yes';
            $window.open(url, "_blank", opt);


        }


        vm.unAssignQty = function (itm) {
            Dialog.confirm('Do you really want to cancel or un-assign qty from job card?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     openUnAssignQtyModal(itm);
                 });
        }


        var NaviCfg = {
            FabProdKnitOrder: {
                RF_FAB_PROD_CAT_ID_LST: '2,4,5,6',
                DEFAULT_CAT_ID: '2',
                LK_COL_TYPE_ID_LST: '358,361,407,432'
                //358 Solid;360 YD;361 AOP, 407 Mellange, 432 Mixed
            },
            FabProdKnitOrderSample: {
                RF_FAB_PROD_CAT_ID_LST: '1,3',
                DEFAULT_CAT_ID: '1',
                LK_COL_TYPE_ID_LST: '358,361,407,432'
            },
            FabProdKnitOrderYD: {
                RF_FAB_PROD_CAT_ID_LST: '1,2,3,4,5.6',
                DEFAULT_CAT_ID: '2',
                LK_COL_TYPE_ID_LST: '360'
            }
        }



        vm.openExColourListModal = function (pMC_FAB_PROD_ORD_H_ID) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ExColourListModal.html',
                controller: 'JobCardDashboardModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    Params: function () {
                        return {

                            pMC_FAB_PROD_ORD_H_ID: pMC_FAB_PROD_ORD_H_ID,
                            pRF_FAB_PROD_CAT_ID: null,
                            LK_COL_TYPE_ID_LST:'360', //NaviCfg[$stateParams.state]['LK_COL_TYPE_ID_LST'],
                            DEFAULT_CAT_ID: '2',//NaviCfg[$stateParams.state]['DEFAULT_CAT_ID'],
                            RF_FAB_PROD_CAT_ID_LST: '1,2,3,4,5.6,8,9'//NaviCfg[$stateParams.state]['RF_FAB_PROD_CAT_ID_LST']
                        }
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if (data.KNT_PLAN_H_ID) {
                    if ($stateParams.pKNT_SC_PRG_ISS_ID) {
                        $state.go('KnitPlanJobCardYd', { pKNT_PLAN_H_ID: data.KNT_PLAN_H_ID, state: $stateParams.state, pKNT_JOB_CRD_H_ID: 0 }, { inherit: true });
                    } else {
                        $state.go('KnitPlanJobCardYd', { pKNT_PLAN_H_ID: data.KNT_PLAN_H_ID, state: $stateParams.state }, { inherit: false });
                    }
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        }



    }

})();