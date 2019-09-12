(function () {
    'use strict';
    angular.module('multitex.mrc').controller('InqComponentController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', InqComponentController]);
    function InqComponentController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'ColourMaster';
        var key = 'MC_COLOR_ID';
        vm.Title = $state.current.Title || '';



        vm.form = formData[key] ? formData : { IS_ACTIVE: 'Y', IS_SWATCH: 'N', PNT_VERSION_NO: 1 };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.submitData = function (data, token) {
            if (angular.isDefined(data[key]) && data[key] > 0) {

                return MrcDataService.updateDataWithFile(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        $state.go($state.current, $stateParams.current, { reload: true });
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            } else {

                return MrcDataService.saveDataWithFile(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        if (res.data.V_MC_COLOR_ID != 0) {
                            $state.go($state.current, { MC_COLOR_ID: res.data.V_MC_COLOR_ID });
                        }
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }
    }

})();