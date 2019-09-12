
(function () {
    'use strict';
    angular.module('multitex.ie').controller('GmtIeNptModalController', ['$q', 'config', 'IeDataService', '$modalInstance', '$scope', 'npts_data', 'ResDept', 'npt_reasons_datas', GmtIeNptModalController]);
    function GmtIeNptModalController($q, config, IeDataService, $modalInstance, $scope, npts_data, ResDept, npt_reasons_datas) {

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }


        $scope.ResDeptDs = new kendo.data.DataSource({
            data: ResDept
        });
        $scope.NptReasonsDs = new kendo.data.DataSource({
            data: npt_reasons_datas
        });
        $scope.HourSlotDs = new kendo.data.DataSource({
            data: [
                { label: '1', value: 1 },
                { label: '2', value: 2 },
                { label: '3', value: 3 },
                { label: '4', value: 4 },
                { label: '5', value: 5 },
                { label: '6', value: 6 },
                { label: '7', value: 7 },
                { label: '8', value: 8 },
                { label: '9', value: 9 },
                { label: '10', value: 10 },
                { label: '11', value: 11 },
                { label: '12', value: 12 },
                { label: '13', value: 13 },
                { label: '14', value: 14 },
                { label: '15', value: 15 },
                { label: '16', value: 16 },
                { label: '17', value: 17 },
                { label: '18', value: 18 },
                { label: '19', value: 19 },
                { label: '20', value: 20 },
                { label: '21', value: 21 },
                { label: '22', value: 22 },
            ]
        });

        $scope.npts_data = npts_data;

        $scope.npts_data['npts'] = npts_data.npts.map(function (o) {
            o['HourSlotDs'] = $scope.HourSlotDs
            return o;
        });

        $scope.onNptCal = function (val) {
            if (val.HOUR_NO_LST && val.HOUR_NO_LST.length > 0) {
                val['NPT'] = val['HOUR_NO_LST'].length * val['AFFECTED_MP'] * 60 - val['MIN_ADJUST'];
                val['AVG_NPT_HR'] = (val['HOUR_NO_LST'].length * val['AFFECTED_MP'] * 60 - val['MIN_ADJUST']) / val['HOUR_NO_LST'].length;
            }
        };

        $scope.save = function (data, valid) {
            if (!valid) { return; };
            var data2bSave = [];
            angular.forEach(data.npts, function (val) {
                data2bSave.push(
                        {
                            GMT_IE_NPT_DATA_ID: val.GMT_IE_NPT_DATA_ID,
                            RF_RESP_DEPT_ID: val.RF_RESP_DEPT_ID,
                            HOUR_NO_LST: val.HOUR_NO_LST.join(','),
                            AFFECTED_MP: val.AFFECTED_MP,
                            MIN_ADJUST: val.MIN_ADJUST,
                            NPT: val.NPT,
                            GMT_IE_NPT_REASON_ID: val.GMT_IE_NPT_REASON_ID,
                            AVG_NPT_HR: val.AVG_NPT_HR
                        }
                    );
            });

            data['XML'] = config.xmlStringShort(data2bSave);
            return IeDataService.saveDataByUrl(data, '/GmtIeTarget/NptDataSave').then(function (res) {
                if (res.success === false) {
                    $scope.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $modalInstance.close();
                    }
                    config.appToastMsg(res.data.PMSG);
                }

            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
            
        }
    }

})();
