(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitPlanJobCardRollProdController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'Dialog', 'JobCardHeader', 'cur_date_server', '$localStorage', '$modal', 'cur_user_id', '$filter', KnitPlanJobCardRollProdController]);
    function KnitPlanJobCardRollProdController($q, config, KnittingDataService, $stateParams, $state, $scope, Dialog, JobCardHeader, cur_date_server, $localStorage, $modal, cur_user_id, $filter) {

        var vm = this;
        $scope.DateFilter = '';
        $scope.KNT_JOB_CRD_H_ID = '';
        vm.Title = $state.current.Title || '';
        vm.errors = null;

        vm.dtFormat = config.appDateFormat;
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getJobCardHeaderData(), getMcDiaList(), getKnitMachineList(), openPriterConfig()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.ACCS_DELV_DT_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.ACCS_DELV_DT_OPENED = true;
        };

        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        vm.cur_user_id = parseInt(cur_user_id);

        vm.openConfigModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Knitting/Knit/_LblPrinterConfig',
                controller: 'LblPrinterCfgModalController',
                size: 'md',
                windowClass: 'large-Modal'
            });

            modalInstance.result.then(function (data) {

            });


        };



        $scope.form = { KNT_FAB_ROLL_ID: -1 };
        vm.NoJobCardFound = false;
        $scope.item = {};
        var CurShiftData = {};
        var pKNT_MACHINE_ID = null;
        var pHR_SCHEDULE_H_ID = null;
        
        function getMcDiaList() {
            return KnittingDataService.getDataByUrl('/KnitCommon/getMachineDiaList').then(function (res) {
                vm.machineDiaDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        function getKnitMachineList(ACT_MC_DIA_ID, HR_PROD_FLR_ID) {
            return KnittingDataService.getDataByUrl('/KnitPlan/getKnitMachine?pACT_MC_DIA_ID=' + (ACT_MC_DIA_ID ||null) + '&pHR_PROD_FLR_ID').then(function (res) {
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



        function getJobCardHeaderData() {

            if (!$stateParams.pJOB_CRD_NO && !$stateParams.pKNT_JOB_CRD_H_ID) {
                vm.IS_SEARCH_FOCUS = true;
                return;
            } else {
                if ($stateParams.pJOB_CRD_NO) {
                    if (isNaN($stateParams.pJOB_CRD_NO)) {
                        return;
                    }

                }

            }

            return KnittingDataService.getDataByUrl('/KnitPlan/getJobCardHeaderDataRollProd?pKNT_JOB_CRD_H_ID=' + ($stateParams.pKNT_JOB_CRD_H_ID || null) + '&pJOB_CRD_NO=' + $stateParams.pJOB_CRD_NO).then(function (res) {

                if (res.KNT_JOB_CRD_H_ID == 0) {
                    $scope.item = {
                        JOB_CRD_NO : 'Not Found !!!!'
                    };
                    vm.IS_ROLL_PROD = false;
                    vm.IS_SEARCH_FOCUS = true;

                    vm.NoJobCardFound = true;
                    return;
                }

                vm.IS_ROLL_PROD = true;
                vm.IS_SEARCH_FOCUS = false;

                vm.NoJobCardFound = false;

                $scope.form['CUR_DATE_TIME'] = cur_date_server + ' ' + res.TIME_STR;
                $scope.item['CUR_DATE_TIME'] = cur_date_server + ' ' + res.TIME_STR;

                $scope.form['ACT_MC_DIA_ID_V'] = res.ACT_MC_DIA_ID;
                $scope.form['ACT_FIN_DIA'] = res.ACT_FIN_DIA;
                $scope.form['KNT_MACHINE_ID'] = res.KNT_MACHINE_ID;
                $scope.form['KNT_STYL_FAB_ITEM_ID'] = res.KNT_STYL_FAB_ITEM_ID;

                //$scope.form['KNT_SC_PRG_ISS_ID'] = res.KNT_SC_PRG_ISS_ID;

                pKNT_MACHINE_ID = res.KNT_SC_PRG_ISS_ID>0 ? null : res.KNT_MACHINE_ID;
                $scope.item = res;

                $scope.KNT_JOB_CRD_H_ID = res.KNT_JOB_CRD_H_ID;

                getRollProductionData(res.KNT_JOB_CRD_H_ID);
                KnittingDataService.getDataByUrl('/KnitPlan/getShiftDataByPlanId?pHR_SHIFT_PLAN_ID=2').then(function (res) {
                    
                    vm.shifDataDs = new kendo.data.DataSource({
                        data: res
                    });
                    getShifData(res);
                });

            });
        };



        function getShifData(res) {

            CurShiftData = _.find(res, function (o) {
                return o.IS_SCHE_ACTIVE == 'Y';
            });
            if (CurShiftData) {
                pHR_SCHEDULE_H_ID = CurShiftData.HR_SCHEDULE_H_ID;
                $scope.form['HR_SCHEDULE_H_ID'] = CurShiftData.HR_SCHEDULE_H_ID;
                if (pKNT_MACHINE_ID) {
                    setOperatorData(pKNT_MACHINE_ID, CurShiftData.HR_SCHEDULE_H_ID);
                }
                
            };
        };



        function setOperatorData(KNT_MACHINE_ID,HR_SCHEDULE_H_ID) {
            return KnittingDataService.getDataByUrl('/KnitPlan/getOperatorByMCnSchedule?pKNT_MACHINE_ID=' + KNT_MACHINE_ID + '&pHR_SCHEDULE_H_ID=' + HR_SCHEDULE_H_ID).then(function (res) {

                $scope.form['OPERATOR_ID'] = res.length>0?res[0].OPERATOR_ID:null;
                vm.operatorDataDs = new kendo.data.DataSource({
                    data: res.map(function (o) {
                        o['EMP_FULL_NAME_EN'] = o['EMP_FULL_NAME_EN'] + ' (' + o['EMPLOYEE_CODE'] + ')';
                        return o;
                    })
                });
            });
        }


        vm.onChangeShift = function (e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.HR_SCHEDULE_H_ID) {
                if (pKNT_MACHINE_ID) {
                    setOperatorData(pKNT_MACHINE_ID, item.HR_SCHEDULE_H_ID);
                }
                
            }          
        }


        vm.params = $stateParams;


     

        vm.loadDataByJobCardNo = function (JOB_CRD_NO) {
            if (!JOB_CRD_NO) {
                return;
            }

            $state.go('KnitPlanJobCardRollProd', {pJOB_CRD_NO : JOB_CRD_NO});
        }

       
        function getRollProductionData(KNT_JOB_CRD_H_ID, DateFilter) {
            var url = '/KnitPlan/RollProductionList?pKNT_JOB_CRD_H_ID=' + KNT_JOB_CRD_H_ID + '&pDateFilter=' + (DateFilter|| '');
            return KnittingDataService.getDataByUrl(url).then(function (res) {
                vm.RollProductionList = res;

                vm.ttl_rolls = res.length;
                vm.ttl_kgs = _.sumBy(res, function (o) { return o.FAB_ROLL_WT - (o.DED_ROLL_WT||0) });
                vm.ttl_kgs_ded = _.sumBy(res, function (o) { return (o.DED_ROLL_WT||0) });
            });

        };




        $scope.onChangeDate = function (DateFilter) {
            if ($scope.KNT_JOB_CRD_H_ID) {
                getRollProductionData($scope.KNT_JOB_CRD_H_ID, (DateFilter instanceof Date ? $filter('date')(DateFilter, 'dd-MMM-yyyy'): ''));
            }
        }
        



        vm.addToList = function (dataOri, valid, KNT_SC_PRG_ISS_ID) {

            console.log(dataOri);

            if ((KNT_SC_PRG_ISS_ID || 0) == 0 && dataOri.HR_SCHEDULE_H_ID.length == 0) {
                $scope.JobCardRollProd.HR_SCHEDULE_H_ID.$setValidity('required', false);
                return;
            }

            if ((KNT_SC_PRG_ISS_ID||0) == 0 && !dataOri.OPERATOR_ID && $scope.item.KNT_MACHINE_ID) {

                $scope.JobCardRollProd.OPERATOR_ID.$setValidity('required', false);
                return;
            }

            if (!dataOri.ACT_MC_DIA_ID && !dataOri.ACT_MC_DIA_ID_V) {
                $scope.JobCardRollProd.ACT_MC_DIA_ID.$setValidity('required', false);
                return;
            }

            if (!dataOri.KNT_MACHINE_ID && dataOri.KNT_SC_PRG_ISS_ID) {
                $scope.JobCardRollProd.KNT_MACHINE_ID.$setValidity('required', false);
                return;
            }



            if (dataOri.FAB_ROLL_WT > ($scope.item.QTY_LEFT + $scope.item.KNT_ROLL_MAX_EXTRA_KGS)) {
                $scope.JobCardRollProd.FAB_ROLL_WT.$setValidity('required', false);
                return;
            }

            if (dataOri.FAB_ROLL_WT > ($scope.item.MAX_ROLL_PROD_WT + $scope.item.KNT_ROLL_MAX_EXTRA_KGS)) {
                $scope.JobCardRollProd.FAB_ROLL_WT.$setValidity('required', false);
                return;
            }



            if (!valid) {
                return;
            }

            var data = angular.copy(dataOri);


            data['QTY_MOU_ID'] = $scope.item.QTY_MOU_ID;
            //data['ACT_FIN_DIA'] = $scope.item.ACT_FIN_DIA;
            data['ACT_MC_DIA_ID'] = data.ACT_MC_DIA_ID_V;
            data['ACT_FIN_GSM'] = $scope.item.FIN_GSM;
            data['KNT_PLAN_H_ID'] = $scope.item.KNT_PLAN_H_ID;
            data['JOB_CRD_NO'] = $scope.item.JOB_CRD_NO;
            data['KNT_JOB_CRD_H_ID'] = $scope.item.KNT_JOB_CRD_H_ID;

            

            if (!$localStorage.hasOwnProperty('PRNTR_ADDRESS')) {

                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: '/Knitting/Knit/_LblPrinterConfig',
                    controller: 'LblPrinterCfgModalController',
                    size: 'md',
                    windowClass: 'large-Modal'
                });

                modalInstance.result.then(function (data) {
                    data['PRNTR_ADDRESS'] = $localStorage.PRNTR_ADDRESS;

                    console.log(data);

                    return KnittingDataService.saveDataByUrl(data, '/KnitPlan/SaveRollProductionData').then(function (res) {
                        if (res.success === false) {
                            vm.errors = res.errors;
                        }
                        else {
                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                $scope.item['QTY_LEFT'] = parseFloat(res.data.OP_QTY_LEFT);
                                getRollProductionData($scope.item.KNT_JOB_CRD_H_ID);

                                if (data.KNT_FAB_ROLL_ID > 0) {
                                    $scope.form = { KNT_FAB_ROLL_ID: -1, CUR_DATE_TIME: $scope.item.CUR_DATE_TIME };
                                    vm.IS_ROLL_PROD = true;
                                    $scope.JobCardRollProd.$setPristine();
                                    $scope.JobCardRollProd.$submitted = false;
                                    $scope.JobCardRollProd.FAB_ROLL_WT.$setValidity('required', true);
                                }

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });

                });



            } else {
                data['PRNTR_ADDRESS'] = $localStorage.PRNTR_ADDRESS;

                console.log(data);

                return KnittingDataService.saveDataByUrl(data, '/KnitPlan/SaveRollProductionData').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                            $scope.item['QTY_LEFT'] = parseFloat(res.data.OP_QTY_LEFT);
                            getRollProductionData($scope.item.KNT_JOB_CRD_H_ID);

                            if (data.KNT_FAB_ROLL_ID > 0) {
                                $scope.form = { KNT_FAB_ROLL_ID: -1, CUR_DATE_TIME: $scope.item.CUR_DATE_TIME };
                                vm.IS_ROLL_PROD = true;
                                $scope.JobCardRollProd.$setPristine();
                                $scope.JobCardRollProd.$submitted = false;
                                $scope.JobCardRollProd.FAB_ROLL_WT.$setValidity('required', true);
                            }

                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }


        }


        vm.editRoll = function (itm) {
            
            if ($scope.form.KNT_FAB_ROLL_ID != itm.KNT_FAB_ROLL_ID) {
                $scope.JobCardRollProd.$setPristine();
                $scope.form = angular.copy(itm);
                $scope.item['QTY_LEFT'] += itm.FAB_ROLL_WT;
            }



        };

        vm.cancelEdit = function (itm) {
            $scope.form = { KNT_FAB_ROLL_ID: -1, CUR_DATE_TIME: $scope.item.CUR_DATE_TIME };
            vm.IS_ROLL_PROD = true;
            $scope.item['QTY_LEFT'] -= itm.FAB_ROLL_WT;

            $scope.JobCardRollProd.$setPristine();
            $scope.JobCardRollProd.$submitted = false;
            $scope.JobCardRollProd.FAB_ROLL_WT.$setValidity('required', true);
        }

        vm.removeRoll = function (itm) {
            Dialog.confirm('Do you really want to remove fabric roll? <br> <b> Roll # ' + itm.FAB_ROLL_NO + ' </b><br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {

                 return KnittingDataService.saveDataByUrl(itm, '/KnitPlan/RemoveRollProductionData').then(function (res) {
                     if (res.success === false) {
                         vm.errors = res.errors;
                     }
                     else {
                         res['data'] = angular.fromJson(res.jsonStr);

                         if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                             $scope.item['QTY_LEFT'] = parseFloat(res.data.OP_QTY_LEFT);
                             getRollProductionData($scope.item.KNT_JOB_CRD_H_ID);

                         }
                         else {
                             config.appToastMsg(res.data.PMSG);
                         }
                        // config.appToastMsg(res.data.PMSG);
                     }
                 }).catch(function (message) {
                     exception.catcher('XHR loading Failded')(message);
                 });

             });


        };





        vm.lavelPrint = function (itm) {




            Dialog.confirm('Label Printing for <b> Roll # ' + itm.FAB_ROLL_NO + ' </b><br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {

                 if (!$localStorage.hasOwnProperty('PRNTR_ADDRESS')) {

                     var modalInstance = $modal.open({
                         animation: true,
                         templateUrl: '/Knitting/Knit/_LblPrinterConfig',
                         controller: 'LblPrinterCfgModalController',
                         size: 'md',
                         windowClass: 'large-Modal'
                     });

                     modalInstance.result.then(function (data) {

                         return KnittingDataService.saveDataByUrl(itm, '/KnitPlan/LabelPrint?pKNT_FAB_ROLL_ID=' + itm.KNT_FAB_ROLL_ID + '&pPRNTR_ADDRESS=' + $localStorage.PRNTR_ADDRESS).then(function (res) {
                             if (res.success === false) {
                                 vm.errors = res.errors;
                             }
                             else {
                                 res['data'] = angular.fromJson(res.jsonStr);

                                 if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                     getRollProductionData($scope.item.KNT_JOB_CRD_H_ID);
                                 } else {
                                     config.appToastMsg(res.data.PMSG);
                                 }
                             }
                         });

                     });
                 } else {
                     return KnittingDataService.saveDataByUrl(itm, '/KnitPlan/LabelPrint?pKNT_FAB_ROLL_ID=' + itm.KNT_FAB_ROLL_ID + '&pPRNTR_ADDRESS=' + $localStorage.PRNTR_ADDRESS).then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 getRollProductionData($scope.item.KNT_JOB_CRD_H_ID);
                             } else {
                                 config.appToastMsg(res.data.PMSG);
                             }
                         }
                     });
                 }

             });


        };




        function openPriterConfig() {
            if (!$localStorage.hasOwnProperty('PRNTR_ADDRESS')) {
               
                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: '/Knitting/Knit/_LblPrinterConfig',
                    controller: 'LblPrinterCfgModalController',
                    size: 'md',
                    windowClass: 'large-Modal'
                });

                modalInstance.result.then(function (data) {

                });
            }

        }



        
        vm.printFabRcvReg = function (pJOB_CRD_NO) {
            var data = {};
            var url = '/api/Knit/KntReport/PreviewReportRDLC';
            

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            //vm.form.FROM_DATE = $filter('date')(vm.form.FROM_DATE, vm.dtFormat);
            //vm.form.TO_DATE = $filter('date')(vm.form.TO_DATE, vm.dtFormat);

            data.REPORT_CODE = "RPT-3590";
            data.JOB_CRD_NO = pJOB_CRD_NO;
            

            var params = angular.copy(data);

            console.log(params);

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