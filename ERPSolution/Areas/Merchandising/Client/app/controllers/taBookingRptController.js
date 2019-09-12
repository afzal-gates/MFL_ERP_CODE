(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TaBookingRptController', ['$q', '$scope', '$http','$stateParams', TaBookingRptController]);
    function TaBookingRptController($q, $scope, $http, $stateParams) {

        var vm = this;
        vm.errors = null;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.gridData = [];

        vm.checkKey = function (key) {
            return _.endsWith(key, '_');
        }

        vm.getPCTValue = function(key) {
            var obj = _.find(vm.controlList, function (o) {
                return o.CNTRL_NAME === key;
            });

            if (obj) {
                return obj.PCT_WIDTH;
            }
            return 20;
        }


        $http.get('/api/mrc/TaBooking/getItemForPurchaseList?pSCM_PURC_REQ_H_ID=' + $stateParams.pSCM_PURC_REQ_H_ID)
            .then(function (result) {

                angular.forEach(result.data, function (val, key) {

                    console.log(val);
                    var item = {
                        ITEM_NAME_EN  : val.ITEM_NAME_EN,
                        ITEM_SPEC_LIST: [],
                        pcTRepo: {},
                        labelRepo: {}
                    }

                    angular.forEach(val.CONTROLS, function (v3, k3) {

                        item.labelRepo[k3 + v3.CNTRL_NAME + '_'] = v3.CNTRL_LABEL;
                        item.pcTRepo[k3 + v3.CNTRL_NAME + '_'] = v3.PCT_WIDTH;
                    });

                    angular.forEach(val.ITEM_SPEC_LIST, function (v2, k2) {
                        var f = {};

                        angular.forEach(val.CONTROLS, function (v3, k3) {
                            if (v2.hasOwnProperty(v3.CNTRL_NAME)) {
                                f[k3+ v3.CNTRL_NAME + '_'] = v2[v3.CNTRL_NAME + '_'];
                            }
                        });


                        angular.forEach(v2.ITEM_SPEC_LIST, function (v4, k4) {
                            if (!vm.checkKey(k4)) {
                                if (!_.isNil(v4)) {
                                    f[k4] = v4;
                                }
                            }
                        });
                        item.ITEM_SPEC_LIST.push(f);
                    });
                    vm.gridData.push(item);                    
                });

    

            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        function getPoSpecDetail(pSCM_PURC_REQ_D_ID, ControlList) {
            
            
            angular.forEach(ControlList, function (val, key) {
                vm.labelRepo[val.CNTRL_NAME + '_'] = val.CNTRL_LABEL;
                vm.pcTRepo[val.CNTRL_NAME + '_'] = val.PCT_WIDTH;
            });


            MrcDataService.getDataByUrl('/TaBooking/getPoDetail?pSCM_PURC_REQ_D_ID=' + pSCM_PURC_REQ_D_ID).then(function (res) {
                angular.forEach(res, function (val, key) {
                    var f = {};
                    angular.forEach(vm.controlList, function (v, k) {
                        if (val.hasOwnProperty(v.CNTRL_NAME)) {
                            f[v.CNTRL_NAME + '_'] = val[v.CNTRL_NAME + '_'];
                        }
                        vm.labelRepo[v.CNTRL_NAME + '_'] = v.CNTRL_LABEL;
                        vm.pcTRepo[v.CNTRL_NAME + '_'] = v.PCT_WIDTH;
                    });
                    angular.forEach(val, function (v, k) {
                        if (!vm.checkKey(k)) {
                            if (!_.isNil(v)) {
                                f[k] = v;
                            }
                        }
                    });


                    $stateParams.itemObj.ITEM_SPEC_LIST.push(f);
                });
            });
        }

    };
})();