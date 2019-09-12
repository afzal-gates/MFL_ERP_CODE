(function () {
    'use strict';

    angular.module('multitex').controller('ItemCostHeadController', ['$modalInstance', '$scope', 'Inquiry', '$state', '$stateParams', 'config', 'MrcDataService', 'item', ItemCostHeadController]);

    function ItemCostHeadController($modalInstance, $scope, Inquiry, $state, $stateParams, config, MrcDataService, item) {

        var vm = this;

        vm.Inquiry = Inquiry;
        $scope.list = [6, 7, 13, 17];
        vm.changeView = function (more) {            
            if (more) {
                $scope.list = [6, 7, 13, 14, 15, 16, 17, 18, 19, 20, 23, 24, 25];
            } else {
                $scope.list = [6, 7, 13, 17];
            }

        };

        MrcDataService.CostHeadList(item.MC_STYLE_ITEM_ID, item.pOption).then(function (res) {
            vm.CostHeadList = res;
        }, function (err) {
            console.log(err);
        })

        $scope.$watch('vm.CostHeadList', function (newVal, oldVal) {
           
            angular.forEach(vm.CostHeadList, function (val, key) {
                var subTotal = 0;
                angular.forEach(val.items, function (val1, key1) {
                    if (val1.MC_COST_HEAD_ID != 26) {
                        subTotal = Number(subTotal) + Number(val1.HEAD_COST);
                    }

                });

                val['SUB_TTL_1'] = subTotal.toFixed(2);
                val['SUB_TTL_2'] = (subTotal + (subTotal * ((val.items[10].PCT_COST || 0) / 100))).toFixed(2);


                val['FOB'] = (Number(val.SUB_TTL_2) + (Number(val.SUB_TTL_2) * ((val.items[11].PCT_COST || 0) / 100))).toFixed(2);
                val['CAL_PRICE'] = (Number(val.FOB) + Number((val.items[15].HEAD_COST || 0))).toFixed(2);

            });



        }, true);
      
        vm.saveGridData = function (data, token, CREATED_BY) {
            var copiedData = angular.copy(data);
            angular.forEach(copiedData, function (val, key) {
                val['CREATED_BY'] = CREATED_BY;
                angular.forEach(val.items, function (val1, key1) {
                    if ((val1.PCT_COST == null || val1.PCT_COST=='') && (val1.HEAD_COST == null||val1.HEAD_COST =='') && val1.MC_STYL_ITM_COST_ID == -1) {
                        val.items.splice(key1, 1);
                    } else if ((val1.PCT_COST == null || val1.PCT_COST == '') && (val1.HEAD_COST == null || val1.HEAD_COST == '') && val1.MC_STYL_ITM_COST_ID > 0) {
                        val.items[key1]['REMOVE'] = -2;
                    }

                });
            });


            MrcDataService.saveCostHeadMappingData(copiedData,token).then(function (res) {
                toastr.success('Successfully Updated', "MultiTEX");
            }, function (err) {
                console.log(err);
            });
        }

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

    }

})();