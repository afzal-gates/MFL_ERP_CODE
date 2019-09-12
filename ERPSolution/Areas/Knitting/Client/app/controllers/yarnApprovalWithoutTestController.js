(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnApprovalWithoutTestController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', YarnApprovalWithoutTestController]);
    function YarnApprovalWithoutTestController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();


        vm.form = { TEST_BY: cur_user_id, TEST_DT: vm.today };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getYarnCountList(), getPendingYarnList(), getUserData(), GetTestStatusList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.categoryBrandList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                        var list = _.filter(res, { 'INV_ITEM_CAT_ID': 2 });
                        e.success(list);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        function getYarnCountList() {

            return vm.YarnCountList = {
                optionLabel: "--Count--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/Common/YarnCountList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });

                        }
                    }
                },
                select: function (e) {
                    if (this.dataItem(e.item).RF_YRN_CNT_ID) {
                        vm.count = this.dataItem(e.item).YR_COUNT_DESC;
                    } else {
                        vm.count = '';
                    }
                },
                dataTextField: "YR_COUNT_DESC",
                dataValueField: "RF_YRN_CNT_ID"
            };
        }

        function GetTestStatusList() {

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(95).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LOOKUP_DATA_ID ==489 || x.LOOKUP_DATA_ID ==490})
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        vm.pendingYarnListByID = function () {
            if (vm.form.RF_BRAND_ID || vm.form.RF_YRN_CNT_ID) {
                vm.showSplash = true;
                //if (vm.form.RF_YRN_CNT_ID > 0 || vm.form.RF_BRAND_ID > 0)
                KnittingDataService.getDataByFullUrl('/api/knit/YarnReceive/GetYarnForTest/' + (vm.form.RF_BRAND_ID || null) + '/' + (vm.form.RF_YRN_CNT_ID || null)).then(function (res) {
                    vm.pendingYarnList = res;

                    vm.showSplash = false;
                });
            }

        };

        function getPendingYarnList() {

            vm.showSplash = true;
            KnittingDataService.getDataByFullUrl('/api/knit/YarnReceive/GetYarnForTest/' + vm.form.RF_BRAND_ID + '/' + vm.form.RF_YRN_CNT_ID).then(function (res) {
                vm.pendingYarnList = res;

                vm.showSplash = false;
            });

        };

        vm.checkUncheckAll = function () {
            for (var i = 0; i < vm.pendingYarnList.length; i++)
                vm.pendingYarnList[i].SELECT_ITEM = vm.form.SELECT_ITEM_ALL;
        }

        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getUserDatalist().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    vm.IsChange = false;
                },

                height: 400,
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };

        }


        $scope.TEST_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TEST_DT_LNopened = true;
        }


        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {
                vm.showSplash = true;
                var data = angular.copy(dataOri);

                var list = _.filter(vm.pendingYarnList, function (o) { return o.SELECT_ITEM == true });

                data["XML_ISS_D"] = KnittingDataService.xmlStringShort(list.map(function (o) {
                    return {
                        KNT_YRN_LOT_TEST_ID: o.KNT_YRN_LOT_TEST_ID == null ? 0 : o.KNT_YRN_LOT_TEST_ID,
                        YARN_ITEM_ID: o.YARN_ITEM_ID,
                        LK_YRN_TST_TYPE_ID: o.LK_YRN_TST_TYPE_ID == 0 ? null : o.LK_YRN_TST_TYPE_ID,
                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID == null ? 0 : o.KNT_YRN_LOT_ID,
                        TEST_BY: vm.form.TEST_BY,
                        TEST_NOTE: o.TEST_NOTE,
                        TEST_DT: vm.form.TEST_DT
                    }
                }));

                return KnittingDataService.saveDataByUrl(data, '/YarnIssue/YarnTestResult').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);

                        config.appToastMsg(res.data.PMSG);
                        $state.go('YarnApprovalWithoutTest');

                    }
                    vm.showSplash = false;
                });
            }
        };
    }


})();

