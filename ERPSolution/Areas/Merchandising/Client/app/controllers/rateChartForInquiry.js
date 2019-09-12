(function () {
    'use strict';
    angular.module('multitex.mrc').controller('RateChartForInquiry', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'Dialog', RateChartForInquiry]);
    function RateChartForInquiry($q, config, MrcDataService, $stateParams, $state, $scope, Dialog) {
        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getDataForChart()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }




        function getDataForChart() {
            return MrcDataService.getDataByFullUrl('/api/inquiry/GetMktRateChartData').then(function (res) {
                vm.RateChart = res;
            });
        }


        vm.submitData = function (DataOri) {
            var Data2Save = [];
            angular.forEach(DataOri, function (val, key) {
                var q = [];
                angular.forEach(Object.keys(val), function (vv, kk) {
                    if (!isNaN(vv)) {
                        if ((val[vv]['RATE'] || 0) > 0) {
                            q.push({
                                RATE: val[vv]['RATE'],
                                MC_BYR_ACC_GRP_ID: val[vv]['MC_BYR_ACC_GRP_ID'],
                                MKT_FAB_RATE_TMPLT_ID: val[vv]['MKT_FAB_RATE_TMPLT_ID']
                            });
                        }

                    }
                });

                Data2Save.push({
                    LK_RATE_DATA_ID: val.LK_RATE_DATA_ID,
                    XML_D: config.xmlStringShortNoTag(q)
                });
            });


            return MrcDataService.saveDataByFullUrl({
                XML: config.xmlStringShort(Data2Save)
                }, '/api/inquiry/UpdateRateForInq').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        getDataForChart();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

    }



})();