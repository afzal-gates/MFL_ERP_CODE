(function () {
    'use strict';
    angular.module('multitex.mrc').controller('InquiryController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', InquiryController]);
    function InquiryController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'InquiryH';
        var key = 'MC_INQR_H_ID';
        vm.Title = $state.current.Title || '';
        $scope.format = config.appDateFormat;

        vm.disableInqForm = $state.includes("Inquiry.Style") || $state.includes("Inquiry.Style.Item") || $state.includes("Inquiry.Style.ItemList");


        
        vm.form = formData[key] ? formData : { LK_LCTERM_ID: 213, LK_INCOTERM_ID: 228, RF_CURRENCY_ID: 2, WT_MOU_ID: 3, QTY_MOU_ID: 5, CONS_MOU_ID: 3, LK_INQ_STATUS_ID: 229 };
        $scope.InquiryData = formData;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getBuyerListData(), tradeTermList(), lcTermList(), getMOUList(), getCurrencyList(), getMcLDPortList()];
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


        function tradeTermList() {
            return vm.tradeTermList = {
                optionLabel: "----",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(42).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function lcTermList() {
            return vm.lcTermList = {
                optionLabel: "----",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(43).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function getMOUList() {
            return vm.MOUList = {
                optionLabel: "----",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getMOUList().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"
            };
        }


        function getCurrencyList() {
            return vm.CurrencyList = {
                optionLabel: "----",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getCurrencyList().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataBound: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.form['CURR_CONV_TK'] = dataItem.EXCHANGE_RATE || 0;
                },

                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.form['CURR_CONV_TK'] = dataItem.EXCHANGE_RATE || 0;
                },

                dataTextField: "CURR_NAME_EN",
                dataValueField: "RF_CURRENCY_ID"
            };
        }


        function getMcLDPortList() {
            return vm.McLDPortList = {
                optionLabel: "--Loading Port--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.selectAllData('McLDPort').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "MC_LD_PORT_NAME_EN",
                dataValueField: "MC_LD_PORT_ID"
            };
        }



        

        

        $scope.SHIPMENT_DTopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.SHIPMENT_DTopened = true;
        };

        $scope.OPD_DEAD_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.OPD_DEAD_LNopened = true;
        }

        


        vm.submitData = function (data, token) {

            if (data.QTY_MOU_ID == 1) { //Pcs
                data['QTY_PER_MOU'] = 1;
            } else if (data.QTY_MOU_ID == 2) { //Set
                data['QTY_PER_MOU'] = 1;
            } else if (data.QTY_MOU_ID == 12) { // Pack
                data['QTY_PER_MOU'] = data.PCS_PER_PACK;
            } else if (data.QTY_MOU_ID == 5) { // Dz
                data['QTY_PER_MOU'] = 12;
            }
 

            if (angular.isDefined(data[key]) && data[key] > 0) {

                return MrcDataService.updateDataWithFile(data, ctrl, token).then(function (res) {
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

                return MrcDataService.saveDataWithFile(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        if (res.data.MC_INQR_H_ID != 0) {
                            $state.go($state.current, { MC_INQR_H_ID: res.data.V_MC_INQR_H_ID });
                        }
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }
    }

})();