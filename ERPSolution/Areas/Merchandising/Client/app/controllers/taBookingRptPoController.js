(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TaBookingPoRptController', ['$q', '$scope', '$http', '$stateParams', TaBookingPoRptController]);
    function TaBookingPoRptController($q, $scope, $http, $stateParams) {
       
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

        vm.GTTL = 0;


        $http.get('/api/mrc/TaBooking/getItemForPurchaseList?pSCM_PURC_REQ_H_ID=' + $stateParams.pSCM_PURC_REQ_H_ID)
            .then(function (result) {
                angular.forEach(result.data, function (val, key) {
                              
                    var item = {
                        ITEM_NAME_EN  : val.ITEM_NAME_EN,
                        ITEM_SPEC_LIST: [],
                        pcTRepo: {},
                        labelRepo: {},
                        sub_total: 0,
                        sub_ttl_qty: 0,
                        count: 0,
                        is_swatch: _.some(val.CONTROLS, function (o) {
                            return o.CNTRL_NAME == 'SWATCH';
                        }),
                    }

                    angular.forEach(val.CONTROLS, function (v3, k3) {

                        if (v3.CNTRL_NAME != 'REMARKS') {
                            item.labelRepo[k3 + v3.CNTRL_NAME + '_'] = v3.CNTRL_LABEL;
                            item.pcTRepo[k3 + v3.CNTRL_NAME + '_'] = v3.PCT_WIDTH;
                        }
                    });

                    angular.forEach(val.ITEM_SPEC_LIST, function (v2, k2) {
                        var f = {};

                        angular.forEach(val.CONTROLS, function (v3, k3) {
                            if (v2.hasOwnProperty(v3.CNTRL_NAME) && v3.CNTRL_NAME != 'REMARKS') {

                                f[k3 + v3.CNTRL_NAME + '_'] = v2[v3.CNTRL_NAME + '_'];

                            }

                            if (v2.hasOwnProperty('RQD_QTY') && v2.hasOwnProperty('CONF_RATE')) {
                             
                                f['PRICE'] = parseFloat((v2.RQD_QTY * v2.CONF_RATE).toFixed(3));
                                f['REMARKS'] = v2.REMARKS;
                               
                               
                            }

                        });

                        item.sub_ttl_qty += v2.RQD_QTY;
                        item.sub_total += (v2.RQD_QTY * v2.CONF_RATE);

                        angular.forEach(v2.ITEM_SPEC_LIST, function (v4, k4) {
                            if (!vm.checkKey(k4)) {
                                if (!_.isNil(v4)) {
                                    f[k4] = v4;
                                }
                            }
                        });
                        item.ITEM_SPEC_LIST.push(f);
                    });
                    item.count = (_.keys(item.ITEM_SPEC_LIST[0]).length);
                    vm.GTTL += item.sub_total;
                    vm.gridData.push(item);                    
                });

                console.log(vm.gridData);
             

            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
     

    };
})();