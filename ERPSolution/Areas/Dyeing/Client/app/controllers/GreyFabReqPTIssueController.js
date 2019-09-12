/// <reference path="greyFabReqIssueController.js" />
(function () {
    'use strict';

    angular.module('multitex.dyeing').directive('checkRollWhileIssue', function (DyeingDataService, $q) {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {

                ngModel.$asyncValidators.invalidRoll = function (modelValue, viewValue) {
                    if (viewValue.length == 13) {

                        console.log(modelValue);
                        return DyeingDataService.getDataByUrl('/GreyFabReq/CheckIsRollIssuable?pFAB_ROLL_NO=' + modelValue + '&pDYE_BT_CARD_GRP_ID=' + scope.pDYE_BT_CARD_GRP_ID + '&pOption=3006')
                            .then(
                             function (res) {
                                 if (res.length == 0) {
                                     scope['KNT_FAB_ROLL_ID'] = null;
                                     return $q.reject();
                                 } else if (res.length == 1) {
                                     scope['KNT_FAB_ROLL_ID'] = res[0].KNT_STYL_FAB_ITEM_ID;
                                     return true;
                                 }
                             }
                        );
                    }
                    else
                        return false;
                };
            }
        };
    });


    angular.module('multitex.dyeing').controller('GreyFabReqPTIssueController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'data', '$location', GreyFabReqPTIssueController]);
    function GreyFabReqPTIssueController($q, config, DyeingDataService, $stateParams, $state, $scope, data, $location) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetFabGrpList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function GetFabGrpList() {
            return vm.FabGrpList = {
                optionLabel: "-- Select Fabric Group --",
                filter: "contains",
                autoBind: true,
                dataSource: data.groups,
                dataTextField: "DYE_FAB_GRP_NAME",
                dataValueField: "LK_FBR_GRP_ID"
            };
        };

        $scope.params = $stateParams;
        $scope.RF_ACTN_STATUS_ID_ISS = data.RF_ACTN_STATUS_ID_ISS;
        $scope.CurState = $stateParams.state;

        console.log($location.search());

        if ($location.search().hasOwnProperty('pKNT_STYL_FAB_ITEM_ID')) {
            data.groups.forEach(function (o) {
                if (o.KNT_STYL_FAB_ITEM_ID == parseInt($location.search().pKNT_STYL_FAB_ITEM_ID)) {
                    o['IS_TAB_ACT'] = true;
                } else {
                    o['IS_TAB_ACT'] = false;
                }
            });

            setTimeout(function () {
                vm.data = data;
                $state.go('GreyFabReqPTIssue.groups', { pKNT_STYL_FAB_ITEM_ID: $location.search().pKNT_STYL_FAB_ITEM_ID }, { reload: 'GreyFabReqPTIssue.groups' });
            }, 200);


        }
        else {

            vm.data = data;
            console.log(data.groups[0]);
            if (data.groups[0].KNT_STYL_FAB_ITEM_ID > 0) {
                $state.go('GreyFabReqPTIssue.groups', { pKNT_STYL_FAB_ITEM_ID: data.groups[0].KNT_STYL_FAB_ITEM_ID }, { reload: true });
            } else {
                vm.data = data;
            }
        }



        vm.onBack2List = function (DYE_GSTR_REQ_H_ID) {
            $state.go($stateParams.state, { pDYE_GSTR_REQ_H_ID: DYE_GSTR_REQ_H_ID, page: $stateParams.page });
        };

        vm.onFinalize = function () {
            var data = {

                DF_SC_PT_ISS_D1_ID: $scope.$parent.params.pDF_SC_PT_ISS_D1_ID,
                KNT_FAB_ROLL_ID: -1,
                TYPE: 'finalize'
            };

            return DyeingDataService.saveDataByUrl(data, '/GreyFabReq/SaveRollForPreTreatmentIssue').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $state.go('GreyFabReqPTIssue.groups', { pKNT_STYL_FAB_ITEM_ID: $location.search().pKNT_STYL_FAB_ITEM_ID }, { reload: 'GreyFabReqPTIssue.groups' });
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        vm.onSelect = function (groups) {
            var actTab = _.find(groups, function (o) {
                return o.IS_TAB_ACT == true;
            });
            console.log(actTab);

            if (actTab && actTab.KNT_STYL_FAB_ITEM_ID && $location.search().hasOwnProperty('pKNT_STYL_FAB_ITEM_ID')) {

                if ($location.search().hasOwnProperty('pKNT_STYL_FAB_ITEM_ID') != actTab.KNT_STYL_FAB_ITEM_ID) {
                    $state.go('GreyFabReqPTIssue.groups', { pKNT_STYL_FAB_ITEM_ID: actTab.KNT_STYL_FAB_ITEM_ID }, { reload: 'GreyFabReqPTIssue.groups' });
                }
            }
        }

        vm.form = {};

        vm.printBatchCard = function (dataItem) {

            vm.form.REPORT_CODE = 'RPT-6007';

            vm.form.SC_PRG_NO = dataItem.SC_PRG_NO;
            vm.form.DF_SC_PT_ISS_H_ID = (dataItem.DF_SC_PT_ISS_H_ID || null);
            //vm.form.KNT_SC_PRG_ISS_ID = dataItem.KNT_SC_PRG_ISS_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Dye/DyeReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = vm.form;

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);

            form.submit();

            document.body.removeChild(form);
        }


        vm.printChallan = function (dataItem) {
            console.log(dataItem);
            vm.form["REPORT_CODE"] = 'RPT-6005';

            vm.form["DF_SC_PT_ISS_H_ID"] = dataItem.DF_SC_PT_ISS_H_ID;
            vm.form["DYE_GSTR_REQ_H_ID"] = dataItem.DYE_GSTR_REQ_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Dye/DyeReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = vm.form;

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);

            form.submit();

            document.body.removeChild(form);
        }


        vm.printCollarCuffTrimsSlip = function (dataItem) {
            console.log(dataItem);
            vm.form["REPORT_CODE"] = 'RPT-6008';

            vm.form["DYE_GSTR_REQ_H_ID"] = dataItem.DYE_GSTR_REQ_H_ID;
            vm.form["DYE_BT_CARD_H_ID"] = dataItem.DYE_BT_CARD_H_ID;
            //vm.form.KNT_SC_PRG_ISS_ID = dataItem.KNT_SC_PRG_ISS_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Dye/DyeReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = vm.form;

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);

            form.submit();

            document.body.removeChild(form);
        }
    }

})();

(function () {
    angular.module('multitex.dyeing').filter('offset', function () {
        return function (input, start) {
            start = parseInt(start, 10);
            //console.log(input);
            if (!input || !input.length) { return; }

            return input.slice(start);
        };
    });
})();

(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('GreyFabReqPTIssueDController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'Dialog', '$modal', GreyFabReqPTIssueDController]);
    function GreyFabReqPTIssueDController($q, config, DyeingDataService, $stateParams, $state, $scope, Dialog, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.form = {};

        activate()
        vm.showSplash = true;
        vm.focusme = true;

        $scope.currentPage = 1;
        $scope.itemPerPage = 15;
        $scope.start = 0;
        $scope.maxSize = 5;

        $scope.pKNT_STYL_FAB_ITEM_ID = parseInt($stateParams.pKNT_STYL_FAB_ITEM_ID || 0);


        function activate() {
            var promise = [getRollProductionList(), getFabRollList()];

            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        $scope.setItems = function (n) {
            $scope.itemPerPage = n;
        };
        // In case you can replace ($scope.currentPage - 1) * $scope.itemPerPage in <tr> by "start"
        $scope.pageChanged = function () {
            $scope.start = ($scope.currentPage - 1) * $scope.itemPerPage;
        };

        vm.openRollModal = function () {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'FabRollSplit.html',
                controller: function ($scope, $modalInstance, DyeingDataService, formData, config) {


                    $scope.currentPage = 1;
                    $scope.itemPerPage = 15;
                    $scope.start = 0;
                    $scope.maxSize = 5;

                    $scope.pendingRollList = formData;
                    $scope.newRollList = [];

                    $scope.setItems = function (n) {
                        $scope.itemPerPage = n;
                    };
                    // In case you can replace ($scope.currentPage - 1) * $scope.itemPerPage in <tr> by "start"
                    $scope.pageChanged = function () {
                        $scope.start = ($scope.currentPage - 1) * $scope.itemPerPage;
                    };

                    $scope.clearAll = function () {
                        $scope.MC_BYR_ACC_GRP_ID = '';
                        $scope.COLOR_NAME_EN = '';
                        $scope.STYLE_ORDER_NO = '';
                        $scope.QC_DT = '';
                    }

                    $scope.checkMaxWeight = function () {

                        if ($scope.FAB_ROLL_WT <= $scope.S_FAB_ROLL_WT) {
                            $scope.S_FAB_ROLL_WT = '';
                            $scope.B_FAB_ROLL_WT = '';
                        }
                        else {
                            $scope.B_FAB_ROLL_WT = $scope.FAB_ROLL_WT - $scope.S_FAB_ROLL_WT;
                        }
                    }

                    $scope.checkRollStatus = function (ctrl) {

                        if (ctrl.length == 13) {
                            var list = angular.copy($scope.pendingRollList);
                            var obj = _.filter(list, function (x) { return x.FAB_ROLL_NO == ctrl });
                            if (obj.length > 0) {
                                $scope.KNT_FAB_ROLL_ID = angular.copy(obj[0].KNT_FAB_ROLL_ID);
                                $scope.FAB_ROLL_WT = angular.copy(obj[0].FAB_ROLL_WT);
                            }
                        }
                        else
                            return false;
                    };


                    $scope.cancel = function (data) {
                        $modalInstance.close(data);
                    }

                    $scope.selectRollNo = function (data) {
                        $modalInstance.close(data);
                    }

                    $scope.submitData = function () {

                        if (fnValidateSub() == true) {
                            var data = {
                                FAB_ROLL_WT: $scope.S_FAB_ROLL_WT,
                                KNT_FAB_ROLL_ID: $scope.KNT_FAB_ROLL_ID
                            };

                            return DyeingDataService.saveDataByFullUrl(data, '/api/knit/KnitPlan/SplitRoll').then(function (res) {

                                if (res.success === false) {
                                    vm.errors = res.errors;
                                }
                                else {

                                    res['data'] = angular.fromJson(res.jsonStr);
                                    config.appToastMsg(res.data.PMSG);
                                    var idx = $scope.newRollList.length;
                                    var item = {
                                        FAB_ROLL_NO: res.data.OPFAB_ROLL_NO,
                                        FAB_ROLL_WT: angular.copy(data.FAB_ROLL_WT)
                                    };
                                    $scope.newRollList.splice(idx, "0", item);
                                }
                            }).catch(function (message) {
                                exception.catcher('XHR loading Failded')(message);
                            });
                        }
                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    formData: function (DyeingDataService) {
                        var list = angular.copy(vm.pendingRollList);
                        return list;
                    }
                }
            });

            modalInstance.result.then(function (item) {
                getFabRollList();
            });

        };


        $scope.getTotal = function () {
            var total = 0;
            if (vm.pendingRollList)
                for (var i = 0; i < vm.pendingRollList.length; i++) {
                    var obj = vm.pendingRollList[i];
                    total += obj.FAB_ROLL_WT;
                }
            return total;
        }

        $scope.playError = function () {
            var audio = new Audio('../../Content/sounds/errorSound.mp3');
            audio.play();
        };


        $scope.playOk = function () {
            var audio = new Audio('../../Content/sounds/OkSound.mp3');
            audio.play();
        };

        vm.checkRollStatus = function (ctrl) {
            vm.errors = undefined;
            console.log($scope);
            if (ctrl.length == 13) {
                var modelValue = ctrl;
                console.log(modelValue);

                return DyeingDataService.getDataByUrl('/GreyFabReq/CheckIsRollIssuable?pFAB_ROLL_NO=' + modelValue + '&pDYE_GSTR_REQ_H_ID=' + $stateParams.pDYE_GSTR_REQ_H_ID + '&pKNT_STYL_FAB_ITEM_ID=' + $scope.pKNT_STYL_FAB_ITEM_ID + '&pOption=3006')
                    .then(
                     function (res) {
                         //var listRoll = _.filter(vm.RollProductionList, function (x) { return x.FAB_ROLL_NO == modelValue });
                         //if (listRoll.length == 0)
                         if (res[0].KNT_STYL_FAB_ITEM_ID == 0) {
                             $scope.playError();
                             config.appToastMsg(res[0].MC_ORDER_NO_LST);
                             $scope.$parent['KNT_FAB_ROLL_ID'] = null;

                             $scope.$parent['KNT_FAB_ROLL_ID'] = '';
                             $scope.$parent.FAB_ROLL_NO = '';

                             return $q.reject();
                         } else if (res[0].KNT_STYL_FAB_ITEM_ID > 0) {
                             $scope.playOk();
                             $scope['KNT_FAB_ROLL_ID'] = res[0].KNT_STYL_FAB_ITEM_ID;
                             $scope.$parent['KNT_FAB_ROLL_ID'] = res[0].KNT_STYL_FAB_ITEM_ID;
                         }
                     }
                ).then(function (res2) {

                    var data = {
                        DF_SC_PT_ISS_D1_ID: $scope.$parent.tab.DF_SC_PT_ISS_D1_ID,
                        KNT_FAB_ROLL_ID: $scope.KNT_FAB_ROLL_ID,
                        TYPE: 'insert'
                    };

                    $scope.$parent['KNT_FAB_ROLL_ID'] = '';
                    $scope.$parent.FAB_ROLL_NO = '';

                    return DyeingDataService.saveDataByUrl(data, '/GreyFabReq/SaveRollForPreTreatmentIssue').then(function (res3) {
                        if (res3.success === false) {
                            vm.errors = res3.errors;
                        }
                        else {
                            res3['data'] = angular.fromJson(res3.jsonStr);
                            if (res3.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                $scope.$parent['KNT_FAB_ROLL_ID'] = '';
                                $scope.$parent.FAB_ROLL_NO = '';
                                getRollProductionList();
                            }
                            config.appToastMsg(res3.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
            }
            else
                return false;
        };

        function getFabRollList() {
            //console.log($stateParams);
            //console.log($scope.$parent.params);

            DyeingDataService.getDataByUrl('/GreyFabReq/getAllRollListByKntFab?pKNT_STYL_FAB_ITEM_ID=' + $stateParams.pKNT_STYL_FAB_ITEM_ID).then(function (res) {
                vm.pendingRollList = res;
                $scope.total = vm.pendingRollList.length;
            });

        };

        function getNonColCuffItemData() {

            DyeingDataService.getDataByUrl('/GreyFabReq/GetNonPreTreatmentItemData?pKNT_STYL_FAB_ITEM_ID=' + $stateParams.pKNT_STYL_FAB_ITEM_ID + '&pDYE_GSTR_REQ_H_ID=' + ($stateParams.pDYE_GSTR_REQ_H_ID || 0)).then(function (res) {
                vm.nonColCuffData = res;
            });
        };



        vm.onChangeIssueQty = function (list) {
            vm.ttl_issue = _.sumBy(list, function (o) {
                return o.ISS_QTY;
            });
        };

        vm.onChangeIssueQtyKg = function (list) {
            vm.ttl_issueKg = _.sumBy(list, function (o) {
                return (o.ISS_QTY_KG || 0);
            });
        };


        vm.onChangeIssueQtyYds = function (list) {
            vm.ttl_issueYds = _.sumBy(list, function (o) {
                return (o.ISS_QTY_YDS || 0);
            });
        };

        vm.calculateColCuffKg = function (list) {
            var xxx = angular.copy(vm.ttl_issueKg);
            var yyy = angular.copy(vm.ttl_issue);
            for (var i = 0; i < list.length; i++) {
                list[i].ISS_QTY_KG = parseFloat(((xxx / yyy) * list[i].ISS_QTY).toFixed(2));
            }

        }
        function getRollProductionList() {
            DyeingDataService.getDataByUrl('/GreyFabReq/getIssuableRollListForPT?pKNT_STYL_FAB_ITEM_ID=' + $stateParams.pKNT_STYL_FAB_ITEM_ID + '&pDYE_GSTR_REQ_H_ID=' + ($stateParams.pDYE_GSTR_REQ_H_ID || 0)).then(function (res) {
                vm.RollProductionList = res;
                getNonColCuffItemData();
            });
        };



        $scope.getIssueTotal = function () {
            var total = 0;
            if (vm.RollProductionList)
                for (var i = 0; i < vm.RollProductionList.length; i++) {
                    var obj = vm.RollProductionList[i];
                    total += obj.FAB_ROLL_WT;
                }
            return total;
        }


        vm.removeRoll = function (pKNT_FAB_ROLL_ID) {

            Dialog.confirm('Deleting...?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {
                 var data = {
                     DF_SC_PT_ISS_D1_ID: $scope.$parent.tab.DF_SC_PT_ISS_D1_ID,
                     KNT_FAB_ROLL_ID: pKNT_FAB_ROLL_ID,
                     TYPE: 'delete'
                 };

                 return DyeingDataService.saveDataByUrl(data, '/GreyFabReq/SaveRollForPreTreatmentIssue').then(function (res) {
                     if (res.success === false) {
                         vm.errors = res.errors;
                     }
                     else {
                         res['data'] = angular.fromJson(res.jsonStr);
                         if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             getRollProductionList();
                         }
                         config.appToastMsg(res.data.PMSG);
                     }
                 }).catch(function (message) {
                     exception.catcher('XHR loading Failded')(message);
                 });
             });
        };



        vm.insertRoll = function (isValid, pKNT_FAB_ROLL_ID) {

            if (!isValid || !pKNT_FAB_ROLL_ID) {
                return;
            }
            var data = {
                DF_SC_PT_ISS_D1_ID: $scope.$parent.tab.DF_SC_PT_ISS_D1_ID,
                KNT_FAB_ROLL_ID: pKNT_FAB_ROLL_ID,
                TYPE: 'insert'
            };

            console.log($scope.$parent.tab.DF_SC_PT_ISS_D1_ID);

            return DyeingDataService.saveDataByUrl(data, '/GreyFabReq/SaveRollForPreTreatmentIssue').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $scope.KNT_FAB_ROLL_ID = '';
                        $scope.FAB_ROLL_NO = '';

                        getRollProductionList();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };


        vm.printBatchCard = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-4032';

            vm.form.DYE_BATCH_NO = dataItem.DYE_BATCH_NO;
            vm.form.DYE_BT_CARD_H_ID = dataItem.DYE_BT_CARD_H_ID;
            //vm.form.KNT_SC_PRG_ISS_ID = dataItem.KNT_SC_PRG_ISS_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Dye/DyeReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = vm.form;

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);

            form.submit();

            document.body.removeChild(form);
        }


        vm.saveTrimsData = function (datas) {
            var data = {
                DYE_BT_CARD_H_ID: $stateParams.pDYE_BT_CARD_H_ID,

                DYE_GSTR_ISS_H_ID: $stateParams.pDYE_GSTR_ISS_H_ID,

                XML: DyeingDataService.xmlStringShort(datas.map(function (o) {
                    return {
                        DYE_GSTR_BT_ISS_D3_ID: o.DYE_GSTR_BT_ISS_D3_ID,
                        ISS_QTY: o.ISS_QTY,
                        ISS_QTY_YDS: o.ISS_QTY_YDS,
                        DYE_BT_CARD_D_TRMS_ID: o.DYE_BT_CARD_D_TRMS_ID,
                        MC_ORD_TRMS_ITEM_ID: o.MC_ORD_TRMS_ITEM_ID
                    };
                })
                )
            };

            return DyeingDataService.saveDataByUrl(data, '/GreyFabReq/SaveTrimWovenFab').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        getTrimWovenData();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };




        vm.saveCollarCuffData = function (DataOri) {

            var data2save = [];

            angular.forEach(DataOri, function (val, key) {
                if (val.ISS_QTY > 0) {
                    data2save.push({
                        DYE_BT_CARD_D_CLCF_ID: val.DYE_BT_CARD_D_CLCF_ID,
                        KNT_CLCF_STYL_ITEM_ID: val.KNT_CLCF_STYL_ITEM_ID,
                        ISS_QTY: val.ISS_QTY,
                        ISS_QTY_KG: (val.ISS_QTY_KG || 0),
                        QTY_MOU_ID: 3,
                        DYE_GSTR_BT_ISS_D2_ID: val.DYE_GSTR_BT_ISS_D2_ID,
                        NO_OF_ROLL: (val.NO_OF_ROLL || 0)
                    });
                };

            });

            var data = {
                DYE_BT_CARD_H_ID: $scope.$parent.params.pDYE_BT_CARD_H_ID,
                LK_FBR_GRP_ID: $stateParams.pLK_FBR_GRP_ID,
                XML: DyeingDataService.xmlStringShort(data2save),
                DYE_GSTR_ISS_H_ID: $stateParams.pDYE_GSTR_ISS_H_ID
            };

            return DyeingDataService.saveDataByUrl(data, '/GreyFabReq/SaveColCuffIssueData').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        getColCuffItemData();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };
    }

})();

