(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnTestController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', 'formData', 'Dialog', YarnTestController]);
    function YarnTestController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, formData, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();


        vm.form = formData.KNT_YRN_STR_REQ_H_ID ? formData : { TEST_BY: cur_user_id, TEST_DT: vm.today, RF_REQ_TYPE_ID: 4 };
        vm.form.TEST_BY = cur_user_id;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [RequisitionList(), pendingYarnList(), getUserData(), GetReqSourceList(), GetReqTypeList(), GetTestStatusList(), GetCompanyList(), GetYarnColorGroupList(), GetMOUList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }



        vm.addAllNewRecoed = function (itemData, indx) {
            var idx = indx + 1;
            itemData.IS_ACTIVE = true;
            var item = angular.copy(itemData);
            //console.log(item);
            //var item = {
            //    SL_NO: idx,
            //    INV_ITEM_ID: 0,
            //    ITEM_NAME_EN: "--Select Item--",
            //    DYE_PRD_PHASE_TYPE_ID: PhaseID,
            //    MOU_CODE: "",
            //    DOSE_QTY: "0"
            //};
            itemData['YarnList'].push(item);
            //vm.recipeItemList.sort();
        }

        vm.removeExtraRecord = function (itemData,data) {

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {

                     var index = itemData['YarnList'].indexOf(data);
                     itemData['YarnList'].splice(index, 1);
                 });
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
                                var list = _.filter(res, function (x) { return x.LOOKUP_DATA_ID != 489 && x.LOOKUP_DATA_ID != 490 })
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function pendingYarnList() {

            KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/SelectForTest?pKNT_YRN_STR_REQ_H_ID=' + (vm.form.KNT_YRN_STR_REQ_H_ID || 0)).then(function (res) {

                angular.forEach(res, function (val, key) {
                    val["YarnList"] = [];

                    vm.pendingYarnList.push(val);
                });

                //console.log(vm.pendingYarnList);
            });

            //KnittingDataService.getDataByFullUrl('/api/knit/YarnReceive/GetYarnForTest').then(function (res) {
            //    vm.pendingYarnList = res;
            //});

        };


        function RequisitionList() {
            return vm.requisitionList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/GetYarnTestReq?pRF_REQ_TYPE_ID=4').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function GetMOUList() {
            return vm.mOUList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

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

        vm.getRequisitionByType = function (e) {
            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            return vm.requisitionList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/GetYarnTestReq?pRF_REQ_TYPE_ID=' + (obj.RF_REQ_TYPE_ID || null)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        vm.getRequisitionInfo = function (e) {
            var obj = e.sender.dataItem(e.item);

            //vm.form.STR_REQ_DT = obj.STR_REQ_DT;
            //vm.form.RF_REQ_TYPE_ID = obj.RF_REQ_TYPE_ID;
            //vm.form.HR_COMPANY_ID = obj.HR_COMPANY_ID;
            //vm.form.SCM_STORE_ID = obj.SCM_STORE_ID;

            KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/SelectForTest?pKNT_YRN_STR_REQ_H_ID=' + (vm.form.KNT_YRN_STR_REQ_H_ID || 0)).then(function (res) {
                vm.pendingYarnList = _.filter(res, function (x) { return x.KNT_YRN_LOT_ID == 0 });

                angular.forEach(res, function (val, key) {
                    val["YarnList"] = [];
                    vm.pendingYarnList.push(val);
                });

                //console.log(vm.pendingYarnList);

                //vm.pendingYarnList = res;


            });
        }



        vm.resetItemData = function () {
            vm.formItem = {};
        };

        vm.reset = function () {
            $state.go('YarnTest', { pKNT_YRN_STR_REQ_H_ID: 0 });
        };



        function GetReqTypeList() {
            return vm.reqTypeList = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                //KnittingDataService.LookupListData(88).then(function (res) {
                                var list = _.filter(res, function (o) { return o.RF_REQ_TYPE_ID == 4; })
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
            };
        };

        function GetReqSourceList() {

            return vm.reqSourceList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                                //KnittingDataService.LookupListData(92).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };
        };

        function GetCompanyList() {

            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };

        function GetYarnColorGroupList() {

            return vm.yarnColorGroupList = {
                optionLabel: "-- Select Color Group --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(93).then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };

        };

        $scope.colorNameAuto = function (val) {

            return KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll?pCOLOR_NAME_EN=' + val).then(function (res) {
                return res;
            });

        };

        $scope.onSelectColor = function (item2, item) {
            //console.log(item);
            item2.MC_COLOR_ID = item.MC_COLOR_ID;
        }



        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                var list = _.filter(vm.pendingYarnList, function (x) { return x.KNT_YRN_LOT_ID = 0 });
                for (var i = 0; i < vm.pendingYarnList.length; i++)
                    for (var j = 0; j < vm.pendingYarnList[i].YarnList.length; j++)
                        list.push(vm.pendingYarnList[i].YarnList[j]);

                console.log(list);
                data["XML_ISS_D"] = KnittingDataService.xmlStringShort(list.map(function (o) {
                    return {
                        KNT_YRN_LOT_TEST_ID: o.KNT_YRN_LOT_TEST_ID == null ? 0 : o.KNT_YRN_LOT_TEST_ID,
                        YARN_ITEM_ID: o.YARN_ITEM_ID,
                        LK_YRN_TST_TYPE_ID: o.LK_YRN_TST_TYPE_ID == 0 ? null : o.LK_YRN_TST_TYPE_ID,
                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID == null ? 0 : o.KNT_YRN_LOT_ID,
                        TEST_BY: vm.form.TEST_BY,
                        TST_COLR_GRP_ID: o.LK_YRN_COLR_GRP_ID,
                        MC_COLOR_ID: o.MC_COLOR_ID,
                        TEST_NOTE: o.TEST_NOTE,
                        STATUS_NOTE: o.STATUS_NOTE,
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
                        $state.go('YarnTest', { pKNT_YRN_STR_REQ_H_ID: 0 });

                    }
                });
            }
        };
    }


})();

