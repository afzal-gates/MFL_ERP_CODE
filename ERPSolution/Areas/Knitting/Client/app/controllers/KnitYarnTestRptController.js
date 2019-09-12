(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitYarnTestRptController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$modal', '$filter', '$modalInstance', KnitYarnTestRptController]);
    function KnitYarnTestRptController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $modal, $filter, $modalInstance) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};
        console.log(formData);
        vm.form = formData.KNT_YRN_LOT_TEST_D1_ID ? formData : { KNT_YRN_LOT_TEST_D1_ID: '0' };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetYarnTestParamList(), getDefectTypeList(), GetYarnTestColorList(), GetColorGRPList(), GetTestStatusList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        
        vm.resultList = ["Minor", "Major", "N/A"];

        function GetYarnTestParamList() {

            KnittingDataService.getDataByFullUrl('/api/knit/KntPlanYarnTest/GetYarnTestParam?pKNT_YRN_LOT_TEST_D1_ID=' + vm.form.KNT_YRN_LOT_TEST_D1_ID).then(function (res) {
                vm.YarnTestParamList = res;
            });
        }

        function GetYarnTestColorList() {

            KnittingDataService.getDataByFullUrl('/api/knit/KntPlanYarnTest/SelectForDefectType?pKNT_YRN_LOT_TEST_D1_ID=' + vm.form.KNT_YRN_LOT_TEST_D1_ID).then(function (res) {
                vm.YarnTestColorList = res;
            });
        }

        function GetColorGRPList() {

            KnittingDataService.getDataByFullUrl('/api/knit/KntPlanYarnTest/GetYarnTestDtl2?pKNT_YRN_LOT_TEST_D1_ID=' + vm.form.KNT_YRN_LOT_TEST_D1_ID).then(function (res) {
                vm.colorGrpList = res;
                vm.colorGrpStatusList = res;
            });
        }

        function GetTestStatusList() {

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(95).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LOOKUP_DATA_ID != 489 && x.LOOKUP_DATA_ID != 490 })
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        vm.putDefault = function (item) {
            if (!item.TST_PARAM_VAL) {
                item.TST_PARAM_VAL = 0;
            }
        }

        vm.printReportYT = function () {
            var url = '/Knitting/Knit/KntScProgYd/#/KntScProgYd?pKNT_SC_PRG_ISS_ID=';
            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 300) + ',scrollbars=yes,status=yes';
            $window.open(url, "_blank", opt);
        }


        vm.submitAllYT = function () {
            console.log(vm.form.YarnTestParamList)

            if (fnValidate() == true) {

                var data = angular.copy(vm.form)
                data["XML_TEST_R1"] = KnittingDataService.xmlStringShort(vm.YarnTestParamList.map(function (o) {
                    return {
                        KNT_YRN_LOT_TEST_R1_ID: o.KNT_YRN_LOT_TEST_R1_ID == null ? 0 : o.KNT_YRN_LOT_TEST_R1_ID,
                        KNT_YRN_LOT_TEST_D1_ID: vm.form.KNT_YRN_LOT_TEST_D1_ID,
                        RF_YRN_TST_PARAM_ID: o.RF_YRN_TST_PARAM_ID,
                        TST_PARAM_VAL: o.TST_PARAM_VAL
                    }
                }));

                return KnittingDataService.saveDataByUrl(data, '/KntPlanYarnTest/SaveResultYT').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        config.appToastMsg(res.data.PMSG);
                    }
                });
            }
        }

        vm.submitAllDF = function () {
            console.log(vm.form.YarnTestColorList)

            if (fnValidateSub() == true) {

                var data = angular.copy(vm.form)
                data["XML_TEST_R2"] = KnittingDataService.xmlStringShort(vm.YarnTestColorList.map(function (o) {
                    return {
                        KNT_YRN_LOT_TEST_R2_ID: o.KNT_YRN_LOT_TEST_R2_ID == null ? 0 : o.KNT_YRN_LOT_TEST_R2_ID,
                        KNT_YRN_LOT_TEST_D11_ID: o.KNT_YRN_LOT_TEST_D11_ID,
                        RF_FB_DFCT_TYPE_ID: o.RF_DY_DFCT_TYPE_ID,
                        IS_MN_MJ: 'MJ',
                        COMMENTS: o.COMMENTS
                    }
                }));

                data["XML_TEST_S"] = KnittingDataService.xmlStringShort(vm.colorGrpStatusList.map(function (o) {
                    return {
                        KNT_YRN_LOT_TEST_D11_ID: o.KNT_YRN_LOT_TEST_D11_ID,
                        YARN_ITEM_ID: vm.form.YARN_ITEM_ID,
                        KNT_YRN_LOT_ID: vm.form.KNT_YRN_LOT_ID,
                        LK_COLR_GRP_ID: o.LK_COLR_GRP_ID,
                        MC_COLOR_ID: o.MC_COLOR_ID,
                        LK_YRN_TST_STS_ID: o.LK_YRN_TST_STS_ID,
                        TEST_NOTE: o.TEST_NOTE
                    }
                }));

                return KnittingDataService.saveDataByUrl(data, '/KntPlanYarnTest/SaveResultDF').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        config.appToastMsg(res.data.PMSG);
                    }
                });
            }
        }

        function getDefectTypeList() {

            KnittingDataService.getDataByFullUrl('/api/common/getDyeDfctTypeList ').then(function (res) {
                var list = _.filter(res, function (x) { return x.DISPLAY_ORDER > 8 });
                vm.defectTypeList = list;
            });
        }

        $scope.cancel = function (data) {
            $modalInstance.close(data);
        }



    }


})();



