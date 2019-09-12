(function () {
    'use strict';
    angular.module('multitex.planning').controller('PlnCritProcessTempController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', 'TnaTaskDs', 'BuyerList', PlnCritProcessTempController]);
    function PlnCritProcessTempController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, TnaTaskDs, BuyerList) {
        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.form = {
            MC_BUYER_ID: ($stateParams.pMC_BUYER_ID || null),
            MC_BUYER_ID_COPY: null,
            MC_TNA_TMPLT_H_LST: ($stateParams.pMC_TNA_TMPLT_H_LST || '').split(',')
        };


        activate();
        function activate() {
            var promise = [getDetailData($stateParams.pMC_BUYER_ID), getDetailDataTask($stateParams.pMC_BUYER_ID, null, $stateParams.pMC_TNA_TMPLT_H_LST)];
            return $q.all(promise).then(function () {
               
            });
        }

        vm.tnaTaskOptions = [{MC_TNA_TASK_D_ID : '', TA_TASK_NAME_EN: 'N/A'}].concat(TnaTaskDs);

        function getDetailData(pMC_BUYER_ID, pPARENT_ID) {
            vm.templateDs = [{ MC_TNA_TMPLT_H_ID: null, TEMPLATE_NAME: 'N/A' }];
            vm.showSplash = true;
            return PlanningDataService.getDataByUrl('/CritProcess/getGmtPlanCpm?pMC_BUYER_ID=' + (pMC_BUYER_ID || null)).then(function (res) {
                vm.showSplash = false;
                vm.templateDs = vm.templateDs.concat(res.templates.map(function (o) {
                    return {
                        MC_TNA_TMPLT_H_ID: o.MC_TNA_TMPLT_H_ID,
                        TEMPLATE_NAME: o.LK_ORDER_TYPE + ' ||' + o.TNA_TMPLT_CODE
                    }
                }));
                vm.TnaTemplatesDs = new kendo.data.DataSource({
                    data: res.templates.map(function (o) {
                        return {
                            MC_TNA_TMPLT_H_ID: o.MC_TNA_TMPLT_H_ID,
                            TEMPLATE_NAME: o.LK_ORDER_TYPE + ' ||' + o.TNA_TMPLT_CODE
                        }
                    })
                });
            });
           
        }

        function getDetailDataTask(pMC_BUYER_ID, pPARENT_ID, pMC_TNA_TMPLT_H_LST) {
            vm.showSplash = true;
            return PlanningDataService.getDataByUrl('/CritProcess/getGmtPlanCpm?pMC_BUYER_ID=' + (pMC_BUYER_ID || null) + '&pPARENT_ID=' + (pPARENT_ID || null) + '&pMC_TNA_TMPLT_H_LST=' + pMC_TNA_TMPLT_H_LST).then(function (res) {
                vm.items = res;
                vm.showSplash = false;
            });
        }


        vm.onChangeTnaSelect = function (e, data) {
            if (e.sender.value().length > 0) {
                vm.form.MC_BUYER_ID_COPY = null;
                $state.go('PlnCritProcessTemp', { pMC_BUYER_ID: data.MC_BUYER_ID, pMC_TNA_TMPLT_H_LST: e.sender.value().join(',') }, { notify: false });
                getDetailDataTask(data.MC_BUYER_ID, null, e.sender.value().join(','))
            } else {
                vm.items = [];
            }
        }

        vm.onFindtemplateDs = function (item) {
            return vm.templateDs.filter(function (o) {
                return o.MC_TNA_TMPLT_H_ID != item.MC_TNA_TMPLT_H_ID;
            })
        };

        vm.onTemplateChange = function (item, templates) {
            var idx = -1;

            if (item.MC_TNA_TMPLT_H_ID_CPY) {
                item['tasks_cpy'] = angular.copy(item.tasks);
                idx = _.findIndex(templates, function (o) { return o.MC_TNA_TMPLT_H_ID == item.MC_TNA_TMPLT_H_ID_CPY });

                if (idx > -1) {
                    item['tasks'] = angular.copy(templates[idx]['tasks'].map(function (o) {
                        o['GMT_PLN_TNA_TMPLT_ID'] = -1;
                        return o;
                    }));
                };
            } else {
                item['tasks'] = angular.copy(item.tasks_cpy) || [];
            }

        };

        vm.buyerListDs = {
            optionLabel: "-- Select Buyer --",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(BuyerList);
                    }
                }
            },
            change: function (e) {
                var dataItem = this.dataItem(e.item);
                vm.form.MC_BUYER_ID_COPY = null;
                getDetailData(dataItem.MC_BUYER_ID);
            },
            dataTextField: "BUYER_NAME_EN",
            dataValueField: "MC_BUYER_ID"
        };

        vm.buyerListDsCopy = {
            optionLabel: "-- Select Buyer --",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(BuyerList);
                    }
                }
            },
            change: function (e) {
                var dataItem = this.dataItem(e.item);
                getDetailDataTask($stateParams.pMC_BUYER_ID, dataItem.MC_BUYER_ID, $stateParams.pMC_TNA_TMPLT_H_LST);
            },
            dataTextField: "BUYER_NAME_EN",
            dataValueField: "MC_BUYER_ID"
        };

        vm.onDeleteTemp = function (list, idx) {
            list.splice(idx, 1);
        };

        vm.onCancel = function (pMC_BUYER_ID) {
            vm.form.MC_BUYER_ID_COPY = null;
            return getDetailData(pMC_BUYER_ID);
        };

    

        vm.submitData = function (data, isValid) {

            if (!isValid) return;
            var data2Save = [];
            if (fnValidate() == true) {
                angular.forEach(data.templates, function (val, key) {

                    var list = _.filter(val.tasks, function (x) { return x.STD_DAYS > 0 });

                    if (list.length > 0) {

                        data2Save.push({
                            MC_TNA_TMPLT_H_ID: val.MC_TNA_TMPLT_H_ID,
                            TASKS: config.xmlStringShortNoTag(list.map(function (o) {
                                return {
                                    A: o.GMT_PLN_TNA_TMPLT_ID,
                                   // MAX_DLAY_ALOW: o.MAX_DLAY_ALOW,
                                    B: o.MC_TNA_TASK_D_ID,
                                   // PARENT_TNA_TASK_D_ID: 97,//Sewing Start
                                    C: o.PRIOR_TNA_TASK_D_ID,
                                    D: o.DISPLAY_ORDER,
                                    E: o.STD_DAYS,
                                    F: o.IS_CRITICAL_PROC
                                };
                            }))
                        });
                    }
                })

                PlanningDataService.saveDataByUrl({
                    XML: config.xmlStringShort(data2Save),
                    Option: 1000

                }, '/CritProcess/Save').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.form.MC_BUYER_ID_COPY = null;
                            getDetailDataTask($stateParams.pMC_BUYER_ID, null, $stateParams.pMC_TNA_TMPLT_H_LST);
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                });
            }
        };

    }

})();

