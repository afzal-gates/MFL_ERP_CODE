(function () {
    'use strict';
    angular.module('multitex.mrc').controller('BuyerInfoController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData','$modal', BuyerInfoController]);
    function BuyerInfoController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData,$modal) {

        var vm = this;
        vm.errors = null;
        vm.Title = $state.current.Title || '';

        vm.form = formData.MC_BUYER_ID ? formData : {IS_ACTIVE : 'Y'};

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetCountryList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.REG_PERIODopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.REG_PERIODopened = true;
        };

        vm.submitBuyerData = function (data, token) {

            if (angular.isDefined(data.MC_BUYER_ID) && data.MC_BUYER_ID > 0) {

                return MrcDataService.updateBuyerData(data, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        $state.go($state.current, $stateParams.current, { reload: true });
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            } else {
                return MrcDataService.saveBuyerData(data, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.V_MC_BUYER_ID!=0){
                            $state.go("BuyerEntry", { MC_BUYER_ID: res.data.V_MC_BUYER_ID });
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }

        }

        //  DropdownList

        function GetCountryList() {
            return vm.CountryList = {
                optionLabel: "-- Select Country --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetCountryList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COUNTRY_NAME_EN",
                dataValueField: "HR_COUNTRY_ID"
            };
        };


        /*
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
        */

    }

})();