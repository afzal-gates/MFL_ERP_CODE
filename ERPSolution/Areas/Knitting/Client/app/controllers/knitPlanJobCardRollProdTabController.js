(function () {
    'use strict';
    angular.module('multitex.menulesspage').controller('KnitPlanJobCardRollProdTabController', ['$q', '$stateParams', '$state', '$scope', 'MenuLessPageDataService', '$modal', '$localStorage', KnitPlanJobCardRollProdTabController]);
    function KnitPlanJobCardRollProdTabController($q, $stateParams, $state, $scope, MenuLessPageDataService, $modal, $localStorage) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;

        vm.openConfigModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'FloorConfigModal.html',
                controller: function ($scope, MenuLessPageDataService, $modalInstance, MC_LIST, $q) {
                    activate()
                    $scope.showSplash = true;
                    function activate() {
                        var promise = [getMcData()];
                        return $q.all(promise).then(function () {
                            $scope.showSplash = false;
                        });
                    }


                    $scope.MC_LIST = MC_LIST || [];

                    $scope.alerts = [];
                    $scope.closeAlert = function (index) {
                        $scope.alerts.splice(index, 1);
                    };

                    

                    function getMcData() {
                        return MenuLessPageDataService.getDataByFullUrl('/api/knit/KnitPlan/getKnitMachine').then(function (res) {
                            $scope.mcList = res.map(function (o) {
                                o['IS_SELECT_MC'] = _.some($scope.MC_LIST, function (b) {
                                    return b == o.KNT_MACHINE_ID;
                                });
                                return o;
                            });
                        });
                    }



                    $scope.FloorChange = function (RF_LOCATION_ID, IS_SELECT_FLR) {
                        angular.forEach($scope.mcList, function (val, key) {
                            if (val.RF_LOCATION_ID == RF_LOCATION_ID) {
                                val['IS_SELECT_MC'] = IS_SELECT_FLR;
                            }
                        });
                    };
                    //$scope.knitCardList = knitCardList;

                    //$scope.remove = function (kntcard, idx) {
                    //    return Dialog.confirm('Removing knit card ref# ' + kntcard + '?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                    //        .then(function () {
                    //            $scope.knitCardList.splice(idx, 1);
                    //        });

                    //};
                    $scope.save = function (data) {
                        $scope.alerts = [];
                        var data = _.map(_.filter(data, function (o) {
                            return o.IS_SELECT_MC == true;
                        }), 'KNT_MACHINE_ID');
                        if (data.length == 0) {
                            $scope.alerts.push({
                                type: 'danger', msg: 'Please select atleast one Machine'
                            });
                            return;
                        }
                        $scope.MC_LIST = data;

                        $scope.alerts.push({
                            type: 'success', msg: 'Save Successful.'
                        });

                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }

                    $scope.close = function () {
                        console.log($scope.MC_LIST);
                        $modalInstance.close($scope.MC_LIST);
                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    MC_LIST: function () {
                        return $localStorage.MC_LIST;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                $localStorage.MC_LIST = data;
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });


        };

        function openRollProductionErrorModal(error) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'RollProductionError.html',
                controller: function ($scope, $modalInstance) {
                    $scope.errorText = error;
                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }

                },
                size: 'lg',
                windowClass: 'app-modal-window'
            });

            modalInstance.result.then(function (data) {
                if (data.SUCCESS) {

                };

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });


        };

        function openRollProductionModal (OP,KC ) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'RollProductionModal.html',
                controller: function ($scope, $modalInstance, OP, Dialog, KnitCard) {

                    $scope.alerts = [];
                    $scope.hideEntry = false;

                    $scope.CONFIRM_DISABLED = false;

                    $scope.KnitCard = KnitCard;


                    $scope.closeAlert = function (index) {
                        $scope.alerts.splice(index, 1);
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }


                    $scope.CONFIRM_DISABLED = true;
                    $scope.$watch('KnitCard.FAB_ROLL_WT', function (newVal, oldVal) {
                        if (newVal) {
                            
                            $scope.CONFIRM_DISABLED = false;                            

                            if (parseFloat(newVal)>(KnitCard.ASIGN_QTY - KnitCard.PROD_QTY)) {
                                $scope.CONFIRM_DISABLED = true;
                            }
                            else if (parseFloat(newVal)>KnitCard.MAX_ROLL_PROD_WT) {
                                $scope.CONFIRM_DISABLED = true;
                            }
                            
                        }
                        
                    });

                    $scope.save = function (data) {

                        Dialog.confirm('<h3 style="margin:0px;"> Knit Card #: <b> ' + data.JOB_CRD_NO + ' </b> &nbsp;  Weight # : <b> ' + data.FAB_ROLL_WT + ' kg </b> </h3', 'Is correct following Info?', ['Yes, Print Label', ' No'])
                        .then(function () {

                            var data2Save = {
                                ACT_FIN_DIA: data.ACT_FIN_DIA,
                                ACT_FIN_GSM: data.FIN_GSM,
                                CUR_DATE_TIME: data.CUR_DATE_TIME,


                                FAB_ROLL_WT: data.FAB_ROLL_WT,
                                HR_SCHEDULE_H_ID: OP.HR_SCHEDULE_H_ID,
                                JOB_CRD_NO: data.JOB_CRD_NO,
                                KNT_FAB_ROLL_ID: -1,
                                KNT_JOB_CRD_H_ID: data.KNT_JOB_CRD_H_ID,
                                KNT_PLAN_H_ID: data.KNT_PLAN_H_ID,
                                OPERATOR_ID: OP.OPERATOR_ID,
                                QTY_MOU_ID: 3,
                                PRNTR_ADDRESS: $localStorage.PRNTR_ADDRESS
                            };
                            return MenuLessPageDataService.saveDataByFullUrl(data2Save, '/api/knit/KnitPlan/SaveRollProductionData').then(function (res) {
                                if (res.success === false) {
                                    vm.errors = res.errors;
                                }
                                else {
                                    res['data'] = angular.fromJson(res.jsonStr);
                                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                        var OP_KNT_FAB_ROLL_ID = res.data.OP_KNT_FAB_ROLL_ID;
                                        $scope.hideEntry = true ;


                                        return MenuLessPageDataService.saveDataByFullUrl({}, '/api/knit/KnitPlan/LabelPrint?pKNT_FAB_ROLL_ID=' + OP_KNT_FAB_ROLL_ID + '&pPRNTR_ADDRESS=' + $localStorage.PRNTR_ADDRESS).then(function (res) {
                                            $modalInstance.close({ SUCCESS: true });
                                            //Dialog.confirm('<h3 style="margin:0px;"> <b> Is Label Print OK ? </b> </h3', 'Confirmation !!!', ['Yes, Got Label', ' No, Print Again'])
                                            //.then(function () {
                                            //    $modalInstance.close({ SUCCESS: true });
                                            //}, function () {
                                            //    return MenuLessPageDataService.saveDataByFullUrl({}, '/api/knit/KnitPlan/LabelPrint?pKNT_FAB_ROLL_ID=' + OP_KNT_FAB_ROLL_ID + '&pPRNTR_ADDRESS=' + $localStorage.PRNTR_ADDRESS).then(function () {
                                            //        Dialog.confirm('<h3 style="margin:0px;"> <b> Is Label Print OK ? </b> </h3', 'Confirmation !!!', ['Yes, Got Label', ' No, Print Again'])
                                            //           .then(function () {
                                            //               $modalInstance.close({ SUCCESS: true });
                                            //           }, function () {
                                            //               return MenuLessPageDataService.saveDataByFullUrl({}, '/api/knit/KnitPlan/LabelPrint?pKNT_FAB_ROLL_ID=' +OP_KNT_FAB_ROLL_ID + '&pPRNTR_ADDRESS=' + $localStorage.PRNTR_ADDRESS).then(function () {
                                            //                   Dialog.confirm('<h3 style="margin:0px;"> <b> Is Label Print OK ? </b> </h3', 'Confirmation !!!', ['Yes, Got Label', ' No, Print Again'])
                                            //                       .then(function () {
                                            //                           $modalInstance.close({ SUCCESS: true });
                                            //                       }, function () {
                                            //                           return MenuLessPageDataService.saveDataByFullUrl({}, '/api/knit/KnitPlan/LabelPrint?pKNT_FAB_ROLL_ID=' + OP_KNT_FAB_ROLL_ID + '&pPRNTR_ADDRESS=' + $localStorage.PRNTR_ADDRESS).then(function () {
                                            //                               Dialog.confirm('<h3 style="margin:0px;"> <b> Is Label Print OK ? </b> </h3', 'Confirmation !!!', ['Yes, Got Label', ' No, Print Again'])
                                            //                                   .then(function () {
                                            //                                       $modalInstance.close({ SUCCESS: true });
                                            //                                   }, function () {
                                            //                                       return MenuLessPageDataService.saveDataByFullUrl({}, '/api/knit/KnitPlan/LabelPrint?pKNT_FAB_ROLL_ID=' + OP_KNT_FAB_ROLL_ID + '&pPRNTR_ADDRESS=' + $localStorage.PRNTR_ADDRESS).then(function () {
                                            //                                           $modalInstance.close({ SUCCESS: true });
                                            //                                       });

                                            //                                   });
                                            //                           });

                                            //                       });
                                            //               });

                                            //           })
                                            //    });

                                            //})
                                            
                                        });
                                    }
                                }
                            }).catch(function (message) {
                                exception.catcher('XHR loading Failded')(message);
                            });
                        });
                    };

                },
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    OP: function () {
                        return OP;
                    },
                    KnitCard: function () {
                        return KC;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                if (data.SUCCESS) {

                };

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });


        };

        function McSelectionModal (OP) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'McSelectionModal.html',
                controller: function ($scope, $modalInstance, OP) {

                    $scope.OP = OP;
                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                    $scope.save = function (item) {

                        $scope.OP['KNT_MACHN_OPR_ID'] = item.KNT_MACHN_OPR_ID;
                        $scope.OP['KNT_MACHINE_ID'] = item.KNT_MACHINE_ID;
                        $scope.OP['KNT_MACHINE_NO'] = item.KNT_MACHINE_NO;

                        $modalInstance.close({ SUCCESS: true, OP: $scope.OP });
                    };

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    OP: function () {
                        return OP;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                if (data.SUCCESS) {

                    return MenuLessPageDataService.getDataByFullUrl('/api/knit/KnitPlan/getJobCardDataByMcId?pKNT_MACHINE_ID=' + data.OP.KNT_MACHINE_ID).then(function (res) {
                        if (res && res.KNT_JOB_CRD_H_ID > 0 && res.LAST_PROD_MIN>=15) {
                            return openRollProductionModal(data.OP, res);
                        } else {
                            if (res.KNT_JOB_CRD_H_ID > 0 && res.LAST_PROD_MIN < 60) {
                                openRollProductionErrorModal("Sorry!!! Need at least 1 hour to produce next roll");
                            } else {
                                openRollProductionErrorModal("Sorry!!! Knit Card Not Found.");
                            }
                        }

                    });

                   

                };

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });


        };
        vm.pinCodeModal = function (OP) {
            if (!$localStorage.hasOwnProperty('PRNTR_ADDRESS')) {
                return openPriterConfig();
            }

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'PinCodeModal.html',
                controller: function ($scope, $modalInstance, OP) {

                    $scope.alerts = [];

                    $scope.closeAlert = function (index) {
                        $scope.alerts.splice(index, 1);
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                    $scope.save = function (PIN_CODE) {
                        //if (parseInt(PIN_CODE) != OP.KNT_PIN_CODE) {
                        //    $scope.alerts.push({
                        //        type: 'danger',
                        //        msg: 'PIN Code does not match. Please try again.'
                        //    });
                        //    return;
                        //}

                        $modalInstance.close({SUCCESS:true,OP:OP});
                    };

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    OP: function () {
                        return OP;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                if (data.SUCCESS) {
                    if (data.OP.mc_list.length == 1) {

                        data.OP['KNT_MACHN_OPR_ID'] = data.OP.mc_list[0].KNT_MACHN_OPR_ID;
                        data.OP['KNT_MACHINE_ID'] = data.OP.mc_list[0].KNT_MACHINE_ID;
                        data.OP['KNT_MACHINE_NO'] = data.OP.mc_list[0].KNT_MACHINE_NO;

                        return MenuLessPageDataService.getDataByFullUrl('/api/knit/KnitPlan/getJobCardDataByMcId?pKNT_MACHINE_ID=' + data.OP.mc_list[0].KNT_MACHINE_ID).then(function (res) {
                            if (res && res.KNT_JOB_CRD_H_ID > 0) {
                                return openRollProductionModal(data.OP, res);

                            } else {
                                openRollProductionErrorModal("Sorry!!! Knit Card Not Found.");
                            }

                        });
                    } else {

                        return McSelectionModal(data.OP);

                    }
                };

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });


        };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getOperatorData(), openPriterConfig()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
                if (!$localStorage.MC_LIST) {
                    return vm.openConfigModal();
                };
            });
        };

        vm.openPrinterConfigModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Knitting/Knit/_LblPrinterConfig',
                controller: function ($modalInstance, $scope, MenuLessPageDataService, $localStorage) {
                    $scope.PRNTR_ADDRESS = ($localStorage.PRNTR_ADDRESS || null);

                    MenuLessPageDataService.getDataByFullUrl('/api/common/getLabelPrinter').then(function (res) {
                        $scope.PrinterList = res;
                    });

                    $scope.save = function (pPRNTR_ADDRESS) {
                        $localStorage.PRNTR_ADDRESS = pPRNTR_ADDRESS;
                        $modalInstance.close(true);
                    };
                    $scope.close = function () {
                        $modalInstance.dismiss('cancel');
                    };
                },
                size: 'md',
                windowClass: 'large-Modal'
            });

            modalInstance.result.then(function (data) {

            });
        };

        function getOperatorData() {
            if (!$localStorage.MC_LIST) {
                return;
            } else {
                return MenuLessPageDataService.getDataByFullUrl('/api/knit/KnitPlan/getOperatorByCurrentSchedule?pKNT_MACHINE_ID_LST=' + $localStorage.MC_LIST.join(',')).then(function (res) {
                    $scope.OperatorList = res;
                });
            }
        };

        var autoRefresh = function () {
            getOperatorData();
        };

        if (true) {
           setInterval(autoRefresh, 10 * 60 * 1000);
        }

        function openPriterConfig() {
            if (!$localStorage.hasOwnProperty('PRNTR_ADDRESS')) {

                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: '/Knitting/Knit/_LblPrinterConfig',
                    controller: function ($modalInstance, $scope, MenuLessPageDataService, $localStorage) {
                        $scope.PRNTR_ADDRESS = ($localStorage.PRNTR_ADDRESS || null);

                        MenuLessPageDataService.getDataByFullUrl('/api/common/getLabelPrinter').then(function (res) {
                            $scope.PrinterList = res;
                        });

                        $scope.save = function (pPRNTR_ADDRESS) {
                            $localStorage.PRNTR_ADDRESS = pPRNTR_ADDRESS;
                            $modalInstance.close(true);
                        };
                        $scope.close = function () {
                            $modalInstance.dismiss('cancel');
                        };
                    },
                    size: 'md',
                    windowClass: 'large-Modal'
                });

                modalInstance.result.then(function (data) {

                });
            }

        }


  
    }

})();