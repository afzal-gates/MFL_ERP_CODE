(function () {
    'use strict';

    angular.module('multitex').controller('BuyerMappingModalController', ['$modalInstance', '$scope', 'colour', 'AllBuyer', '$state', 'config', 'MrcDataService', BuyerMappingModalController]);

    function BuyerMappingModalController($modalInstance, $scope, colour, AllBuyer, $state, config, MrcDataService) {

        $scope.save = function (token, CREATED_BY) {

            var data = $('#YourGridName').data("kendoGrid").dataSource.data();
            var dataForSave = [];
            if (data.length > 0) {
                angular.forEach(data, function (val, key) {
                    if (val.MC_BU_COL_REF_ID > 0 && val.COLOR_REF == '') {
                        dataForSave.push({ MC_BU_COL_REF_ID: -2, MC_COLOR_ID: $scope.colour.MC_COLOR_ID, MC_BUYER_ID: val.MC_BUYER_ID, COLOR_REF: val.COLOR_REF, CREATED_BY: CREATED_BY });
                    } if (val.MC_BU_COL_REF_ID > 0 && val.COLOR_REF != '') {
                        dataForSave.push({ MC_BU_COL_REF_ID: val.MC_BU_COL_REF_ID, MC_COLOR_ID: $scope.colour.MC_COLOR_ID, MC_BUYER_ID: val.MC_BUYER_ID, COLOR_REF: val.COLOR_REF, CREATED_BY: CREATED_BY });
                    } else if (val.MC_BU_COL_REF_ID == -1 && val.COLOR_REF != '') {
                        dataForSave.push({ MC_BU_COL_REF_ID: -1, MC_COLOR_ID: $scope.colour.MC_COLOR_ID, MC_BUYER_ID: val.MC_BUYER_ID, COLOR_REF: val.COLOR_REF, CREATED_BY: CREATED_BY });
                    }
                });

                return MrcDataService.BuyerColourMapData(dataForSave, token).then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                },function (err) {
                    console.log(err);
                })
            }
        };

        $scope.colour = colour;
        $scope.AllBuyer = angular.copy(AllBuyer);


        $scope.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.AllBuyer);
                    }
                },
                pageSize: 10
            },
            filterable: true,
            selectable: false,
            sortable: true,
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "BUYER_CODE", title: "BuyerCode", type: "string", width: "50px" },
                { field: "BUYER_NAME_EN", title: "Buyer Name", type: "string", width: "50px" },
                { field: "BUYER_SNAME", title: "Short Name", type: "string", width: "50px" },
                { field: "COUNTRY_NAME_EN", title: "Country", type: "string", width: "50px" },
                {
                    title: "Colour Ref#",
                    template: function () {
                        return "<input type='text' ng-model='dataItem.COLOR_REF' style='width:100px;'/>";
                    },
                    width: "50px"
                }
            ]
        };


        $scope.cancel = function () {

            $modalInstance.dismiss('cancel');
            $state.go('ColourMaster', { MC_COLOR_ID: $scope.colour.MC_COLOR_ID }, { reload: true });
        };
    }

})();