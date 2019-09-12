(function () {
    'use strict';
    angular.module('multitex.knitting').controller('ExcessFabReqRptController', ['$q', '$scope', '$http', '$stateParams', 'KnittingDataService', 'rptData', ExcessFabReqRptController]);
    function ExcessFabReqRptController($q, $scope, $http, $stateParams, KnittingDataService, rptData) {

        var vm = this;
        vm.errors = null;        
        vm.showSplash = true;
        vm.today = new Date();
        vm.dtFormat = "dd/MMM/yyyy"; //config.appDateFormat;

        console.log(rptData);
        vm.data = rptData.fabList;
        vm.dataClcf = rptData.clcfList;
        vm.dataRsn = rptData.rsnList;
        vm.dataResp = rptData.respList;

        vm.collarLength = 0;
        vm.cuffLength = 0;

        if (vm.dataClcf.length > 0) {
            vm.collarLength = _.filter(vm.dataClcf[0].itemsDtl, function (ob) {
                return ob.RF_GARM_PART_ID == 10;
            }).length;
            vm.cuffLength = _.filter(vm.dataClcf[0].itemsDtl, function (ob) {
                return ob.RF_GARM_PART_ID == 12;
            }).length;
        }

        console.log(vm.collarLength);
        console.log(vm.cuffLength);

        activate();

        function activate() {
            var promise = [/*getDataFormServer()*/];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        
        function getDataFormServer() {
            //alert('x');
            if (!$stateParams.pKNT_SRT_FAB_REQ_H_ID)
                return;

            KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/GetSrtFabReqRpt?pKNT_SRT_FAB_REQ_H_ID=' + $stateParams.pKNT_SRT_FAB_REQ_H_ID)
                .then(function (res) {
                    vm.data = res;

                    console.log(res);
                })
                
                .catch(function (message) {
                    console.error(message);
                });
        }


        vm.diaWiseSumQty = function (itm) {

            var vRtn = 0;

            angular.forEach(vm.data, function (val, key) {
                angular.forEach(val['itemsDiaList'], function (val1, key1) {
                    if (val1['DIA_INDEX'] == itm.DIA_INDEX) {
                        vRtn = vRtn + val1['RQD_FAB_QTY'];
                    }
                });
            });

            console.log(vRtn);
            return vRtn;                                   
        }

        vm.grandTotSumQty = function () {

            var vRtn = 0;

            angular.forEach(vm.data, function (val, key) {
                angular.forEach(val['itemsDiaList'], function (val1, key1) {
                    vRtn = vRtn + val1['RQD_FAB_QTY'];
                });
            });

            console.log(vRtn);
            return vRtn;
        }

        vm.measWiseSumQty = function (itm) {
            var vRtn = 0;

            angular.forEach(vm.dataClcf, function (val, key) {
                angular.forEach(val['itemsDtl'], function (val1, key1) {
                    if (val1['MESUREMENT_INDEX'] == itm.MESUREMENT_INDEX) {
                        vRtn = vRtn + val1['RQD_PC_QTY'];
                    }
                });
            });

            console.log(vRtn);
            return vRtn;
        }

        vm.grandTotClcfSumQty = function () {
            var vRtn = 0;

            angular.forEach(vm.dataClcf, function (val, key) {
                angular.forEach(val['itemsDtl'], function (val1, key1) {
                    vRtn = vRtn + val1['RQD_PC_QTY'];
                });
            });

            console.log(vRtn);
            return vRtn;
        }
    
    };
})();