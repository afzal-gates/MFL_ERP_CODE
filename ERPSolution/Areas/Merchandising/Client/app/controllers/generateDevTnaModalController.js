(function () {
    'use strict';

    angular.module('multitex.mrc').controller('GenerateDevTnaModalController', ['$modalInstance', '$scope', 'Order', 'TnATaskList', '$q', 'config', '$filter', 'MrcDataService', 'Dialog', '$modal', 'cur_date_server', 'DeselectTnaTaskList', GenerateDevTnaModalController]);

    function GenerateDevTnaModalController($modalInstance, $scope, Order, TnATaskList, $q, config, $filter, MrcDataService, Dialog, $modal, cur_date_server, DeselectTnaTaskList) {
        $scope.Order = Order;
        $scope.OrderCopy = angular.copy(Order);
        $scope.Order['ORD_CONF_DT'] = $filter('date')(Order.ORD_CONF_DT, 'dd-MMM-yyyy');
        $scope.Order['SHIP_DT'] = $filter('date')(Order.SHIP_DT, 'dd-MMM-yyyy');
        $scope.showSplash = true;
       
        var DayMulTi = 1;

        $scope.validShipDate = true;
        $scope.validConfDate = true;

        $scope.dtFormat = config.appDateFormat;
        $scope.GridDateOpened = [];
        $scope.GridDateOpen = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.GridDateOpened[index] = true;
        };


        activate();
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                $scope.showSplash = false;
            });
        }
        $scope.TnATaskList = _.sortBy(TnATaskList, 'TA_TASK_SL');
        var reload = function () {
            return MrcDataService.getDataByUrl('/StyleH/TnAList/TnATemp/' + Order.MC_TNA_TMPLT_H_ID + '/Order/' + Order.MC_ORDER_H_ID + '/Buyer/' + Order.MC_BUYER_ID).then(function (res) {
                $scope.TnATaskList = _.sortBy(res, 'TA_TASK_SL');
            })
        }

        MrcDataService.getDataByFullUrl('/api/common/NoOfWorkingDay?pHR_COMPANY_ID=' + Order.HR_COMPANY_ID + '&pFROM_DT=' + Order.ORD_CONF_DT + '&pTO_DT=' + Order.SHIP_DT).then(function (res) {
            $scope.leadTime = parseInt(angular.fromJson(res).V_NO_OF_DAYS);
        });

        $scope.printOrderWiseTnA = function (pMC_ORDER_H_ID, pIS_EXCEL_FORMAT) {
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-2008' }, {
                MC_ORDER_H_ID: pMC_ORDER_H_ID,
                IS_EXCEL_FORMAT: pIS_EXCEL_FORMAT
            });

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
        };

        function RecursiveStDaysUpdate(data, pPRIOR_TNA_TASK_D_ID, pDAYS2BeAdj) {
            var idx = data.findIndex(function (o) { return o.MC_TNA_TASK_D_ID === pPRIOR_TNA_TASK_D_ID });
            if (idx > -1) {
                data[idx]['STD_DAYS'] = (data[idx]['STD_DAYS_ORI'] + pDAYS2BeAdj)
                if (data[idx]['PARENT_TASK_ID'] > 0) {
                    RecursiveStDaysUpdate(data, data[idx]['PARENT_TASK_ID'], pDAYS2BeAdj);
                };
            }
        }


        $scope.onChangeStdDays = function (item) {
            var DAYS2BeAdj = (item.STD_DAYS - item.STD_DAYS_ORI)
            angular.forEach($scope.TnATaskList, function (val, key) {
                if (item.MC_ORD_TNA_D_ID != val.MC_ORD_TNA_D_ID && item.PARENT_TASK_ID > 0) {
                    RecursiveStDaysUpdate($scope.TnATaskList, item.PARENT_TASK_ID, DAYS2BeAdj);
                }
            });
        };

        $scope.onChangeCheckUncheck = function (item) {
            angular.forEach($scope.TnATaskList, function (val, key) {
                if (item.MC_TNA_TASK_ID == val.MC_TNA_TASK_ID && val.MC_TNA_TASK_D_ID != item.MC_TNA_TASK_D_ID) {
                    val['IS_CHECKED'] = item.IS_CHECKED;
                }
            });
        };


        $scope.save = function (data, token) {
            var adata = [];
            angular.forEach(data, function (val, key) {
                if (val.IS_CHECKED == 'N') {
                    adata.push({
                        STD_DAYS: (val.STD_DAYS - val.STD_DAYS_ORI),
                        MC_TNA_TASK_ID: val.MC_TNA_TASK_ID,
                        MC_TNA_TASK_D_ID: val.MC_TNA_TASK_D_ID,
                        MC_ORD_TNA_D_ID: val.MC_ORD_TNA_D_ID,
                        MC_ORD_TNA_ID: val.MC_ORD_TNA_ID,
                        IS_ST_END: val.IS_ST_END
                    });
                };

            });


            if (adata.length > 0) {
                var XML = MrcDataService.xmlStringShort(adata);
                $scope.showSplash = true;
                return MrcDataService.saveDataByUrl(
                     {
                         MC_ORDER_H_ID: $scope.Order.MC_ORDER_H_ID, IS_TNA_FINALIZED: $scope.Order.IS_TNA_FINALIZED, XML: XML, XMLD: '',
                         MC_TNA_TMPLT_H_ID: $scope.Order.MC_TNA_TMPLT_H_ID
                     }, '/StyleH/SaveTnATemplateData', token).then(function (res) {
                         res['data'] = angular.fromJson(res.jsonStr);
                         if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             $scope.showSplash = false;
                             reload();
                         }
                         config.appToastMsg(res.data.PMSG);

                     }, function (err) {
                         console.log(err);
                     })
            }

        };

        $scope.reset = function (token) {

            Dialog.confirm('Reseting data from TnA Template...', 'Are you sure?', ['Yes', 'No'])
             .then(function () {
                 return MrcDataService.saveDataByUrl({ MC_ORDER_H_ID: $scope.Order.MC_ORDER_H_ID, IS_TNA_FINALIZED: $scope.Order.IS_TNA_FINALIZED, XML: '<trans></trans>' }, '/StyleH/SaveTnATemplateData', token).then(function (res) {
                     res['data'] = angular.fromJson(res.jsonStr);

                     if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                         reload();
                     }

                     config.appToastMsg('MULTI-002 Successfuly reset');

                 }, function (err) {
                     console.log(err);
                 })
             });


        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();

// Start Revision Modal Controller
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TnaRevisionModalController', ['$modalInstance', '$q', '$scope', '$filter', 'config', 'orderData', 'MrcDataService', 'Dialog', TnaRevisionModalController]);
    function TnaRevisionModalController($modalInstance, $q, $scope, $filter, config, orderData, MrcDataService, Dialog) {
        $scope.showSplash = true;
        activate();

        $scope.today = new Date();
        $scope.dtFormat = config.appDateFormat;
        $scope.formInvalid = false;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        $scope.REVISION_DT = $scope.today;

        if (orderData.TNA_REVISION_NO) {
            $scope.REVISION_NO = parseInt(orderData.TNA_REVISION_NO || 0) + 1;
        }
        else {
            $scope.REVISION_NO = 1;
        }

        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                $scope.showSplash = false;
            });
        }

        $scope.RevisionDate_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.RevisionDate_LNopened = true;
        }



        $scope.save = function (token, valid) {
            if (!valid) return;
            var url = '/StyleH/TnAModal/TnaRevise?MC_ORDER_H_ID=' + orderData.MC_ORDER_H_ID + '&REVISION_NO=' + $scope.REVISION_NO + '&REVISION_DT=' + $filter('date')($scope.REVISION_DT, $scope.dtFormat) + '&REV_REASON=' + $scope.REV_REASON;
            Dialog.confirm('Do you want to revise Time & Action Calendar (TNA)?', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     return MrcDataService.getDataByUrl(url).then(function (res) {
                         var data = angular.fromJson(res);
                         if (res) {
                             if (data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 $modalInstance.close({ IS_TNA_FINALIZED: 'N' });
                             }
                             config.appToastMsg(data.PMSG);
                         }
                     });
                 });
        };


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };



    }

})();
// End Revision Modal Controller