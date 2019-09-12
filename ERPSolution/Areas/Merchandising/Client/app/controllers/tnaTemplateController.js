(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TnaTemplateController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', '$modal', TnaTemplateController]);
    function TnaTemplateController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData, $modal) {

        var vm = this;
        vm.errors = null;
        var key = 'MC_TNA_TMPLT_H_ID';
        vm.Title = $state.current.Title || '';

        vm.form = formData[key] ? formData : { TempType: 2 };

        $scope.TemplateHeader = vm.form;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getBuyerListData(), getNatureOfWorkList(), getOrderTypeList(), getUserData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function getBuyerListData() {

            return vm.buyerList = {
                optionLabel: "-- Select Buyer --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.selectAllData('buyer').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "BUYER_NAME_EN",
                dataValueField: "MC_BUYER_ID"
            };
        }



        function getNatureOfWorkList() {
                return MrcDataService.LookupListData(37).then(function (res) {
                      vm.NatureOfWorkList = res;
                }, function (err) {
                    console.log(err);
                });
        }

        function getOrderTypeList() {
            return MrcDataService.LookupListData(40).then(function (res) {
                vm.OrderTypeList = res;
            }, function (err) {
                console.log(err);
            });
        }

        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select  --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getUserData().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };
        }



        $scope.$watchGroup(['vm.form.LEAD_TIME_FROM', 'vm.form.LEAD_TIME_TO'], function (newVal, oldVal) {

            if (newVal[1] < newVal[0]) {
                $scope.TnaTemplateForm['LEAD_TIME_TO'].$setValidity("range", false);
            } else {
                $scope.TnaTemplateForm['LEAD_TIME_TO'].$setValidity("range", true);
            }

        });



        $scope.$watchGroup(['vm.form.TempType', 'vm.form.MC_BUYER_ID', 'vm.form.LEAD_TIME_FROM'], function (newVal, oldVal) {

            if ($stateParams.MC_TNA_TMPLT_H_ID == 0) {
                vm.form['LEAD_TIME_TO'] = angular.copy(newVal[2]);
            }

            if (newVal[0] == 2 && newVal[1]) {

                if (parseInt(newVal[1]) != oldVal[1]) {
                    var data = $("#BuyerDropDown").data("kendoDropDownList").dataSource.data();
                } else {
                    var data = [];
                }

                angular.forEach(data, function (val, key) {
                    if (val.MC_BUYER_ID == parseInt(newVal[1])) {
                        vm.form['TNA_TMPLT_CODE'] = 'TN' + val.BUYER_CODE + (newVal[2] || '00');
                        $scope.TnaTemplateForm['MC_BUYER_ID'].$setValidity("required", true);
                    }
                });

            } else {
                vm.form['MC_BUYER_ID'] = null;
                vm.form['TNA_TMPLT_CODE'] = 'TNGEN' + (newVal[2]||'00');
            }
        });
    }

})();