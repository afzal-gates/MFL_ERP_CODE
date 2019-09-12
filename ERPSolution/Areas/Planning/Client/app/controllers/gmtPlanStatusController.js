(function () {
    'use strict';
    angular.module('multitex.planning').controller('GmtPlanStatusController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', GmtPlanStatusController]);
    function GmtPlanStatusController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope) {
        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        activate();
        vm.showSplash = true;
        vm.plnStatusAreas = [];

        function activate() {
            var promise = [getGmtPLanStatusArea(), getGmtPLanStatusRules()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;                
            });
        }


        function getGmtPLanStatusArea() {
            return PlanningDataService.getDataByUrl('/PlanStatus/Areas').then(function (res) {
      
                vm.plnStatusAreas =res;
            })
        }

        function getGmtPLanStatusRules() {
            return PlanningDataService.getDataByUrl('/PlanStatus/Rules').then(function (res) {
                vm.plnStatusRules = res;
            })
        }



        vm.addPlnStatusArea = function (item) {
            item.items.push({IS_ACTIVE:'Y'});
        }

        vm.delPlnStatusArea = function (item, idx) {
            item.items.splice(idx, 1);
        }

        vm.onParentStatusArea = function (itm, e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.LK_PLN_STS_GRP_ID) {
                itm['PLN_STS_AREA_NAME'] = item.LK_PLN_STS_GRP_NAME;
            } else {
                itm['PLN_STS_AREA_NAME'] = '';
            }
        }

        vm.submitPlnStatusArea = function (data, isValid) {
            if (!isValid) return;
            var data2Save = [];

            angular.forEach(data, function (val, key) {

                data2Save.push({
                    LK_PLN_STS_GRP_ID : val.LK_PLN_STS_GRP_ID,
                    ITEMS : config.xmlStringShortNoTag(val.items.map(function (o) {
                        return {
                            GMT_PLN_STS_AREA_ID: o.GMT_PLN_STS_AREA_ID,
                            PARENT_ID : o.PARENT_ID,
                            DISPLAY_ORDER: o.DISPLAY_ORDER,
                            IS_ACTIVE: o.IS_ACTIVE,
                            PLN_STS_AREA_CODE: o.PLN_STS_AREA_CODE,
                            PLN_STS_AREA_NAME: o.PLN_STS_AREA_NAME,
                            WT_FACTOR: o.WT_FACTOR
                        };
                    }))
                });
            })

            PlanningDataService.saveDataByUrl({
                XML: config.xmlStringShort(data2Save),
                Option: 1000

            }, '/PlanStatus/Save').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                    }
                    config.appToastMsg(res.data.PMSG);
                }
            });

          };

        vm.cancelPlnStatusArea = function () {
            return getGmtPLanStatusArea();
        }

        //a
        vm.addPlnStatusRules = function (item) {
            item.details.push({});
        }

        vm.delPlnStatusRules = function (item, idx) {
            item.details.splice(idx, 1);
        }

        vm.submitPlnStatusRules = function (data, isValid) {
            if (!isValid) return;
            var data2Save = [];

            angular.forEach(data, function (val, key) {

                data2Save.push({
                    LK_PLN_STS_GRP_ID: val.LK_PLN_STS_GRP_ID,
                    ITEMS: config.xmlStringShortNoTagChild(val.details.map(function (o) {
                        return {
                            GMT_PLN_STS_RULE_ID: o.GMT_PLN_STS_RULE_ID,
                            SCORE_RNG_1: o.SCORE_RNG_1,
                            SCORE_RNG_2: o.SCORE_RNG_2,
                            PLN_STS_CODE: o.PLN_STS_CODE,
                            PLN_STS_NAME: o.PLN_STS_NAME,
                            STS_COLOR_CODE: o.STS_COLOR_CODE,
                            POSTIVE_RANK: o.POSTIVE_RANK
                        };
                    }))
                });
            })

            PlanningDataService.saveDataByUrl({
                XML: config.xmlStringShort(data2Save),
                Option: 1001

            }, '/PlanStatus/Save').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                    }
                    config.appToastMsg(res.data.PMSG);
                }
            });

        };

        vm.cancelPlnStatusRules = function () {
            return getGmtPLanStatusRules();
        }


    }

})();

