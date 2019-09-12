(function () {
    'use strict';
    angular.module('multitex.mrc').controller('BuyerListController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'BuyerList', '$modal', BuyerListController]);
    function BuyerListController($q, config, MrcDataService, $stateParams, $state, $scope, logger, BuyerList, $modal) {

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

        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(BuyerList);
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
                { field: "BUYER_CODE", title: "Buyer Code", type: "string", width: "60px" },
                { field: "BUYER_NAME_EN", title: "Buyer Name", type: "string", width: "80px" },
                { field: "BUYER_SNAME", title: "Short Name", type: "string", width: "50px" },
                { field: "ADDRESS_PI", title: "Address", type: "string", width: "180px" },

                {
                    title: "Action",
                    template: function () {
                        return "</a><a ui-sref='BuyerEntry({MC_BUYER_ID:dataItem.MC_BUYER_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a></a>&nbsp;<a ng-click='vm.openOfficeMappingModal(dataItem)' class='btn btn-xs purple'><i class='fa fa-edit'> Link to Office</i></a>";
                    },
                    width: "100px"
                }
            ]
        };


  
        vm.colRefToBuyerModal = function (data) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ColRefToBuyerModal.html',
                controller: 'ColourRefToBuyerController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    Buyer: function () {
                        return data;
                    },
                    ColourList: function (MrcDataService) {
                      return  MrcDataService.getDataByUrl('/ColourMaster/ColourListByBuyerId/' + data.MC_BUYER_ID);
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }





        vm.openOfficeMappingModal = function (data) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'OfficeMapModal.html',
                controller: 'OfficeMappingModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    buyer: function () {
                        return data;
                    },
                    AllOffice: function (MrcDataService) {
                        return MrcDataService.getAllOfficeDatas();

                    },
                    SelectedOffice: function (MrcDataService) {
                        return MrcDataService.getOfficeDatasByBuyerId(data.MC_BUYER_ID);
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };


    }

})();