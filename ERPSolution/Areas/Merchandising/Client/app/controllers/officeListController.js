(function () {
    'use strict';
    angular.module('multitex.mrc').controller('OfficeListController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'OfficeList', '$modal', OfficeListController]);
    function OfficeListController($q, config, MrcDataService, $stateParams, $state, $scope, logger, OfficeList, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
    
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.openBuyerAccModal = function (data) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'BuyerAccModal.html',
                controller: 'BuyerAccModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    Buyer: function () {
                        return data;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }


        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(OfficeList);
                    }
                },
                pageSize: 10
            },
            filterable: true,
            selectable: "row",
            sortable: true,
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "BOFF_NAME_EN", title: "Office Name", type: "string", width: "150px" },
                { field: "LK_BOFF_TYPE", title: "Office Type", type: "string", width: "80px" },
                { field: "ADDRESS_EN", title: "Address", type: "string", width: "250px" },
                { field: "COUNTRY_NAME_EN", title: "Country", type: "string", width: "80px" },

                {
                    title: "Action",
                    template: function () {
                        return "</a><a ui-sref='OfficeEntry({MC_BUYER_OFF_ID:dataItem.MC_BUYER_OFF_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a>&nbsp;<a ng-click='vm.openBuyerAccModal(dataItem)' class='btn btn-xs purple'><i class='fa fa-edit' ng-disabled='dataItem.LK_BOFF_TYPE_ID==189'> Buyer Acc</i></a>";
                    },
                    width: "100px"
                }
            ]
        };
    }

})();