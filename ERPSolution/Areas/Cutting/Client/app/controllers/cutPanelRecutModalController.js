
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutPanelRecutModalController', ['$q', 'config', 'CuttingDataService', '$modalInstance', '$scope', 'dataList', 'V_GMT_PROD_PLN_CLNDR_ID', 'Dialog', CutPanelRecutModalController]);
    function CutPanelRecutModalController($q, config, CuttingDataService, $modalInstance, $scope, dataList, V_GMT_PROD_PLN_CLNDR_ID, Dialog) {


        $scope.onChange = function (item) {
            item.IS_DATA_CHANGE = true;
            item['ALLOWED_SHRT_QTY'] = _.max(item.recuts.map(function (x) { return x.NO_OF_PANEL_REJ - x.NO_OF_PNL_RECUT }));

        };
        $scope.cancel = function () {
            $modalInstance.dismiss();
        }
        $scope.SL = 1;
        $scope.dataList = dataList;
        $scope.MAX_SL = _.maxBy(dataList, function (o) { return o.SL; }).SL;

        $scope.item = $scope.dataList[0];
        $scope.onChange(dataList[0]);

        function SetCurrentBundleCard() {
            var idx = $scope.dataList.findIndex(function (o) { return o.SL === $scope.SL });
            if (idx > -1) {
                $scope.item = $scope.dataList[idx];
                $scope.onChange($scope.dataList[idx]);
            }
        };

        $scope.prev = function () {
            if ($scope.SL < 2) { return; }
            $scope.SL--;
            SetCurrentBundleCard();
        }
        $scope.next = function () {
            if ($scope.SL > $scope.MAX_SL) { return; }
            $scope.SL++;
            SetCurrentBundleCard();
        }




        $scope.save = function (data) {

            var count = data.filter(function (ob) { return ob.IS_DATA_CHANGE === false; }).length;
            var msg = count == 0 ? 'Saving data...' : 'Still ' + count + ' data is pending to update. Do you want to save?';
            var pXML;


            Dialog.confirm('<h5 style="margin:0px;">' + msg + '</h5', 'Are You Sure?', ['Yes', ' No'])
            .then(function () {
                pXML = config.xmlStringShort(data.filter(function (ob) { return ob.IS_DATA_CHANGE; }).map(function (ob) {
                    return {
                        GMT_BNDL_CRD_PDATA_ID: ob.GMT_BNDL_CRD_PDATA_ID,
                        ALLOWED_SHRT_QTY : ob.ALLOWED_SHRT_QTY,
                        XML_D: config.xmlStringShortNoTag(ob.recuts.map(function (o) {
                            return {
                                GMT_CUT_PNL_RECUT_ID: o.GMT_CUT_PNL_RECUT_ID,
                                RF_GARM_PART_ID: o.RF_GARM_PART_ID,
                                NO_OF_PNL_RECUT: o.NO_OF_PNL_RECUT,
                                ALLOWED_SHRT_QTY: ob.ALLOWED_SHRT_QTY
                            }
                        })
                        )
                    }
                })
                    );

                return CuttingDataService.saveDataByUrl({ pXML: pXML, pOption: 1006, GMT_PROD_PLN_CLNDR_ID: V_GMT_PROD_PLN_CLNDR_ID }, '/CutCutPnlInspect/SaveDefectData').then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $modalInstance.close();
                    }
                });

            });
        }


    }

})();
