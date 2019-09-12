
(function () {
    'use strict';
    angular.module('multitex.garments').controller('GmtSewingOutputDefectRejectModalController', ['$q', 'config', 'GarmentsDataService', '$modalInstance', '$scope', 'BundleCaradList', 'Dialog', GmtSewingOutputDefectRejectModalController]);
    function GmtSewingOutputDefectRejectModalController($q, config, GarmentsDataService, $modalInstance, $scope, BundleCaradList, Dialog) {

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }
        $scope.SL = 1;

        $scope.selectedDefect = null;

        $scope.MAX_SL = _.maxBy(BundleCaradList, function (o) { return o.SL; }).SL;
        $scope.BundleCaradList = BundleCaradList;
        $scope.CurrentBundle = $scope.BundleCaradList[0];

        function setCurrentBundle() {
            var idx = $scope.BundleCaradList.findIndex(function (o) { return o.SL === $scope.SL });
            if (idx > -1) {
                $scope.selectedDefect = null;
                $scope.CurrentBundle = $scope.BundleCaradList[idx];
            }
        };

        $scope.setTotalPnlRej = function () {
            $scope.CurrentBundle.NO_OF_PANEL_REJ = _.sumBy($scope.CurrentBundle.defects, function (o) { return (o.NO_OF_DFCT || 0); });
        };

        $scope.onTrimDigit = function () {
            var data = Object.assign({}, $scope.selectedDefect);
            $scope.selectedDefect.NO_OF_DFCT = parseInt(data.NO_OF_DFCT.toString().substring(0, (data.NO_OF_DFCT.toString().length - 1)) || 0);
            $scope.setTotalPnlRej();
        };

        $scope.setSelected = function (itm, list) {

            angular.forEach(list, function (o) {
                o['SELECTED'] = false;
            });

            itm['SELECTED'] = true;
            $scope.selectedDefect = itm;
        };

        $scope.addOne = function (item, val) {
            if (item.SELECTED) {
                item.NO_OF_DFCT = item.NO_OF_DFCT + val;
                $scope.setTotalPnlRej();
            }
        }

        $scope.prev = function () {
            if ($scope.SL < 2) { return; }
            $scope.SL--;
            setCurrentBundle();
        }
        $scope.next = function () {
            if ($scope.SL > $scope.MAX_SL) { return; }
            $scope.SL++;
            setCurrentBundle();
        }

        $scope.save = function (data) {

            var count = data.filter(function (ob) { return (ob.REJECT_A + ob.REJECT_B + ob.REJECT_C) == 0; }).length;
            var msg = count == 0 ? 'Saving data...' : 'Still ' + count + ' data is pending to update. Do you want to save?';
            var pXML;


            Dialog.confirm('<h5 style="margin:0px;">' + msg + '</h5', 'Are You Sure?', ['Yes', ' No'])
            .then(function () {
                pXML = config.xmlStringShort(data.filter(function (ob) { return (ob.REJECT_A || 0) + (ob.REJECT_B || 0) + (ob.REJECT_C || 0) > 0; }).map(function (ob) {
                    return {
                            GMT_SEW_PROD_SCAN_ID: ob.GMT_SEW_PROD_SCAN_ID,
                            REJECT_A: ob.REJECT_A,
                            REJECT_B: ob.REJECT_B,
                            REJECT_C: ob.REJECT_C,
                            XML_D: config.xmlStringShortNoTag(ob.defects.filter(function (o) { return o.NO_OF_DFCT > 0; }).map(function (o) {
                                return {
                                    GMT_SEW_PROD_SCAN_D_ID: o.GMT_SEW_PROD_SCAN_D_ID,
                                    RF_FB_DFCT_TYPE_ID: o.RF_FB_DFCT_TYPE_ID,
                                    NO_OF_DFCT: o.NO_OF_DFCT
                                }
                            })
                            )
                        }
                    })
                 );
                return GarmentsDataService.saveDataByUrl({ XML: pXML, pOption: 1001 }, '/GmtSewProdScan/SaveGmtSewProdScan').then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $modalInstance.close();
                    }
                });

            });
        }
      
    }

})();
