﻿(function () {
    'use strict';
    angular.module('multitex.mrc').controller('AddFabBkingRptController', ['$q', '$scope', '$http', '$stateParams', 'addFabBkRptData', AddFabBkingRptController]);
    function AddFabBkingRptController($q, $scope, $http, $stateParams, addFabBkRptData) {

        var vm = this;
        vm.errors = null;        
        vm.showSplash = true;
        vm.today = new Date();
        vm.dtFormat = "dd/MMM/yyyy"; //config.appDateFormat;

        console.log(addFabBkRptData);
        vm.finFabdata = addFabBkRptData.finFabList;
        vm.greyFabdata = addFabBkRptData.greyFabList;
        vm.dataClcf = addFabBkRptData.clcfList;


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
            //if (!$stateParams.pMC_BLK_ADFB_REQ_H_ID)
            //    return;

            return $http({
                method: 'get',
                url: '/api/mrc/AddFabBk/GetAddFabBkingRpt?pMC_BLK_ADFB_REQ_H_ID=' + $stateParams.pMC_BLK_ADFB_REQ_H_ID//,
                //headers: { 'Authorization': 'Bearer ' + access_token }
            }).then(function (res) {

                vm.finFabdata = res.finFabList;
                vm.greyFabdata = res.greyFabList;
                vm.dataClcf = res.clcfList;

                if (vm.dataClcf.length > 0) {
                    vm.collarLength = _.filter(vm.dataClcf[0].itemsDtl, function (ob) {
                        return ob.RF_GARM_PART_ID == 10;
                    }).length;
                    vm.cuffLength = _.filter(vm.dataClcf[0].itemsDtl, function (ob) {
                        return ob.RF_GARM_PART_ID == 12;
                    }).length;
                }


            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });

            //return commonDataService.getDataByFullUrl('/api/mrc/AddFabBk/GetAddFabBkingRpt?pMC_BLK_ADFB_REQ_H_ID=' + $stateParams.pMC_BLK_ADFB_REQ_H_ID)
            //    .then(function (res) {
                    
            //        vm.finFabdata = res.finFabList;
            //        vm.greyFabdata = res.greyFabList;
            //        vm.dataClcf = res.clcfList;

            //        if (vm.dataClcf.length > 0) {
            //            vm.collarLength = _.filter(vm.dataClcf[0].itemsDtl, function (ob) {
            //                return ob.RF_GARM_PART_ID == 10;
            //            }).length;
            //            vm.cuffLength = _.filter(vm.dataClcf[0].itemsDtl, function (ob) {
            //                return ob.RF_GARM_PART_ID == 12;
            //            }).length;
            //        }

            //        console.log(res);
            //    })
                
            //    .catch(function (message) {
            //        console.error(message);
            //    });
        }


        vm.diaWiseSumQty = function (itm, isFinFab) {

            var vRtn = 0;

            angular.forEach(vm.finFabdata, function (val, key) {
                angular.forEach(val['itemsDiaList'], function (val1, key1) {
                    if (val1['DIA_INDEX'] == itm.DIA_INDEX) {
                        if (isFinFab == 'Y') {
                            vRtn = vRtn + val1['RQD_FFAB_QTY'];
                        }
                        else {
                            vRtn = vRtn + val1['RQD_GFAB_QTY'];
                        }
                    }
                });
            });

            console.log(vRtn);
            return vRtn;                                   
        }

        vm.grandTotSumQty = function (isFinFab) {

            var vRtn = 0;

            angular.forEach(vm.finFabdata, function (val, key) {
                angular.forEach(val['itemsDiaList'], function (val1, key1) {
                    if (isFinFab == 'Y') {
                        vRtn = vRtn + val1['RQD_FFAB_QTY'];
                    }
                    else {
                        vRtn = vRtn + val1['RQD_GFAB_QTY'];
                    }
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