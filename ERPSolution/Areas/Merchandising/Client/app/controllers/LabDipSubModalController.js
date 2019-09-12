(function () {
    'use strict';

    angular.module('multitex').controller('LabDipSubModalController', ['$modalInstance', '$scope', '$state', 'config', '$filter', 'MrcDataService', LabDipSubModalController]);

    function LabDipSubModalController($modalInstance, $scope, $state, config, $filter, MrcDataService) {


        $scope.save = function (token, CREATED_BY, RF_ACTN_STATUS_ID, APRV_LD_REF, COMMENTS_DT, COMMENTS) {

            var dtFormat = config.appDateFormat;
            COMMENTS_DT = $filter('date')(COMMENTS_DT, dtFormat);
            //console.log(data);

            var DataForSave = [];
            var data = $('#labSubGrid').data("kendoGrid").dataSource.data();
            //var allData = $('#labSubGrid').data("kendoGrid");
            //var SelectRow = allData.dataItem(allData.select());

           
       
            var TestData = {
                MC_LD_SUBMIT_ID: localStorage.getItem("vMC_LD_SUBMIT_ID"),
                MC_LD_REQ_D_ID: localStorage.getItem("vMC_LD_REQ_D_ID"),
                SUBMIT_DT: localStorage.getItem("vSUBMIT_DT"),
                LD_REF1: localStorage.getItem("vLD_REF1"),
                LD_REF2: localStorage.getItem("vLD_REF2"),
                LD_REF3: localStorage.getItem("vLD_REF3"),
                RF_ACTN_STATUS_ID: RF_ACTN_STATUS_ID,
                APRV_LD_REF: APRV_LD_REF,
                COMMENTS_DT: COMMENTS_DT,
                COMMENTS: COMMENTS,
                CREATION_DATE: localStorage.getItem("vCREATION_DATE"),
                CREATED_BY: localStorage.getItem("vCREATED_BY"),
                LAST_UPDATE_DATE: localStorage.getItem("vLAST_UPDATE_DATE"),
                LAST_UPDATED_BY: localStorage.getItem("vLAST_UPDATED_BY"),
                COLOR_NAME_EN: localStorage.getItem("vCOLOR_NAME_EN"),
                FABRIC_DESC: localStorage.getItem("vFABRIC_DESC"),
                IS_ACTIVE: localStorage.getItem("vIS_ACTIVE"),
                ACTN_STATUS_NAME: localStorage.getItem("vACTN_STATUS_NAME"),
            };

            return MrcDataService.updateLabdipData(TestData, token, '/api/mrc/LabdipSubmit/Update').then(function (res) {
                res['data'] = angular.fromJson(res.jsonStr);
                config.appToastMsg(res.data.PMSG);
                $('#labSubGrid').data("kendoGrid").dataSource.read();
                $('#labStatusGrid').data("kendoGrid").dataSource.read();
            }, function (err) {
                console.log(err);
            });

        };

        $scope.COLOR_NAME_EN = localStorage.getItem("vColor");
        $scope.LD_REF1 = localStorage.getItem("vLD_REF1");
        $scope.LD_REF2 = localStorage.getItem("vLD_REF2");
        $scope.LD_REF3 = localStorage.getItem("vLD_REF3");
        //$scope.COMMENTS_DT = localStorage.getItem("vCOMMENTS_DT");

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

    }

})();