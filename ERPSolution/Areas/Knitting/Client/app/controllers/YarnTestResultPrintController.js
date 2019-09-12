(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnTestResultPrintController', ['$q','KnittingDataService', '$stateParams', '$state', '$scope', 'formData', YarnTestResultPrintController]);
    function YarnTestResultPrintController($q, KnittingDataService, $stateParams, $state, $scope, formData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.today = new Date();
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
        
        vm.resultList = ["Minor", "Mazor"];

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

        function getDefectTypeList() {

            KnittingDataService.getDataByFullUrl('/api/common/getDyeDfctTypeList ').then(function (res) {
                var list = _.filter(res, function (x) { return x.DISPLAY_ORDER > 8 });
                vm.defectTypeList = list;
            });
        }
        
    }


})();



