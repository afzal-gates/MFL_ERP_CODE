(function () {
    'use strict';

    angular.module('multitex').controller('BuyerAccModalController', ['$modalInstance', '$scope', 'Buyer', '$state', 'config', 'MrcDataService', BuyerAccModalController]);

    function BuyerAccModalController($modalInstance, $scope, Buyer, $state, config, MrcDataService) {


        console.log(Buyer);

        $scope.save = function (token, CREATED_BY) {

            var saveForData = [];

            var data = $('#kendoGrid').data("kendoGrid").dataSource.data();

                angular.forEach(data, function (val, key) {
                    if (val.BYR_ACC_NAME_EN && val.BYR_ACC_SNAME) {
                        val['CREATED_BY'] = CREATED_BY;
                        saveForData.push(val);
                    }
                });

                if (saveForData.length == 0) {
                    return;
                } else {
                    return MrcDataService.submitBuyerAcc(saveForData, token).then(function (res) {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        $('#kendoGrid').data("kendoGrid").dataSource.read();
                    }, function (err) {
                        console.log(err);
                    })
                }
                   


        };

        $scope.addNew = function (CREATED_BY) {
            
            var data = { IS_ACTIVE: 'Y', CREATED_BY: CREATED_BY, MC_BYR_ACC_ID: -1, BYR_ACC_DESC: '', BYR_ACC_NAME_EN: '', BYR_ACC_SNAME: '', DISPLAY_ORDER: '', MC_BUYER_OFF_ID: Buyer.MC_BUYER_OFF_ID }
            $('#kendoGrid').data("kendoGrid").dataSource.insert(0,data);
        }

        $scope.remove = function (data) {
            $('#kendoGrid').data("kendoGrid").dataSource.remove(data);
        }

        $scope.Buyer = Buyer;

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        $scope.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {

                        MrcDataService.getBuyerAccDataByByerId($scope.Buyer.MC_BUYER_OFF_ID).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        })
                    }
                }
            },
            filterable: true,
            selectable: false,
            sortable: true,
            pageSize: 10,
            pageable: false,
            columns: [
                {
                    title: "Buyer Acc Name",
                    template: function () {
                        return "<input type='text' ng-model='dataItem.BYR_ACC_NAME_EN' style='width:190px;'/>";
                    },
                    width: "90px"
                },
                {
                    title: "Short Name",
                    template: function () {
                        return "<input type='text' ng-model='dataItem.BYR_ACC_SNAME' style='width:95px;'/>";
                    },
                    width: "50px"
                },
                {
                    title: "Description",
                    template: function () {
                        return "<input type='text' ng-model='dataItem.BYR_ACC_DESC' style='width:250px;'/>";
                    },
                    width: "120px"
                },
                {
                    title: "Order",
                    template: function () {
                        return "<input type='text' ng-model='dataItem.DISPLAY_ORDER' style='width:40px;'/>";
                    },
                    width: "30px"
                },
                {
                    title: "Action",
                    template: '<a ng-click="addNew()" class="btn btn-xs blue"><i class="fa fa-plus">Add</i> </a> <a ng-click="remove(dataItem)" ng-disabled="dataItem.MC_BYR_ACC_ID>0" class="btn btn-xs red"><i class="fa fa-times-circle"> Rem</i></a>',
                    width: "60px"
                }
            ]
        };

    }

})();