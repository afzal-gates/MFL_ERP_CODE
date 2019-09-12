(function () {
    'use strict';
    angular.module('multitex.mrc').controller('OfficeInfoController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', OfficeInfoController]);
    function OfficeInfoController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData) {

        var vm = this;
        vm.errors = null;
        vm.Title = $state.current.Title || '';

        vm.form = formData.MC_BUYER_OFF_ID ? formData : {IS_ACTIVE:'Y'};
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getOfficeType(), GetCountryList(), GetTimeZoneList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.REG_PERIODopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.REG_PERIODopened = true;
        };



        vm.submitOfficeData = function (data, token) {

            if (angular.isDefined(data.MC_BUYER_OFF_ID) && data.MC_BUYER_OFF_ID > 0) {

                return MrcDataService.updateOfficeData(data, token).then(function (res) {
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

                return MrcDataService.saveOfficeData(data, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        if (res.data.V_MC_BUYER_OFF_ID != 0) {
                            $state.go("OfficeEntry", { MC_BUYER_OFF_ID: res.data.V_MC_BUYER_OFF_ID });
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }

        }


        function getOfficeType() {
            return MrcDataService.LookupListData(39).then(function (res) {
                vm.officeType = res;
            }, function (err) {
                console.log(err);
            })
        };

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

        function GetTimeZoneList() {
            return vm.TimeZoneList = {
                optionLabel: "-- Select Time Zone --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetTimeZoneList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "TIMEZONE_TEXT",
                dataValueField: "HR_TIMEZONE_ID"
            };
        };


    }

})();