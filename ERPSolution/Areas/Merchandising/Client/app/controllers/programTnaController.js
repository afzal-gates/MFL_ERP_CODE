(function () {
    'use strict';
    angular.module('multitex.mrc').controller('ColourMasterModalController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', ColourMasterModalController]);
    function ColourMasterModalController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData) {
        var vm = this;
        vm.errors = null;
        var ctrl = 'ColourMaster';
        var key = 'MC_COLOR_ID';


        vm.param = $stateParams;      
        vm.Title = $state.current.Title || '';
        vm.form = formData[key] ? formData : { IS_ACTIVE: 'Y' };


        activate()

        vm.showSplash = true;

        function activate() {
                var promise = [];
                return $q.all(promise).then(function () {
                    vm.showSplash = false;
      
                });
        }

        vm.submitData = function (dataOri, token) {
            var data = angular.copy(dataOri);

            if (angular.isDefined(data[key]) && data[key] > 0) {

                return MrcDataService.updateData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                          
                        }
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            } else {

                return MrcDataService.saveData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);                      
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        }
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }
    }

})();