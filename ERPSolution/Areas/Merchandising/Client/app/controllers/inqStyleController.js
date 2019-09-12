(function () {
    'use strict';
    angular.module('multitex.mrc').controller('InqStyleController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', InqStyleController]);
    function InqStyleController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'Style';
        var key = 'MC_STYLE_ID';
        vm.Title = $state.current.Title || '';
        vm.parentItemCategory = 8;


   
        vm.form = formData[key] ? formData : { MC_INQR_H_ID: $stateParams.MC_INQR_H_ID, NO_OF_ITEM: 1 };
        $scope.StyleData = formData;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getGmtItemGroupList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }



        function getStyleDataByInqId() {
            MrcDataService.getStyleDataByInqId($stateParams.MC_INQR_H_ID).then(function (res) {
                vm.styleListByInqId = res;
            }, function (err) {
                console.log(err);
            });
        }

        function getGmtItemGroupList() {
            return vm.GmtItemGroupList = {
                optionLabel: "--Select Item Group--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.invItemListByParent(vm.parentItemCategory).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID"
            };
        }



        vm.submitData = function (data, token) {

            console.log(data);
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
                        if (res.data.V_MC_STYLE_ID != 0) {
                            $state.go($state.current, { MC_STYLE_ID: res.data.V_MC_STYLE_ID });
                        }
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }

        vm.goToItemReg = function () {
            var params = angular.extend({}, $stateParams.current, {MC_STYLE_ITEM_ID:0})
            $state.go('Inquiry.Style.Item', params);
        }
    }

})();