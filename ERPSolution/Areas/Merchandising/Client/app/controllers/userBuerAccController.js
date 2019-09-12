(function () {
    'use strict';
    angular.module('multitex.mrc').controller('UserBuyerAccController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', UserBuyerAccController]);
    function UserBuyerAccController($q, config, MrcDataService, $stateParams, $state, $scope, logger) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'UserBuyerAcc';
        var key = 'MC_USR_BYRACC_ID';
        vm.Title = $state.current.Title || '';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.changeUserId = function (SC_USER_ID) {
            MrcDataService.getBuyerAccListByUserId(SC_USER_ID).then(function (res) {

                var dataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                                e.success(res);
                        }
                    },
                    pageSize: 100,
                });

                $('#kendoGrid').data("kendoGrid").setDataSource(dataSource);
            }, function (err) {
                console.log(err);
            })
        }

        $scope.makeBatchSelected = function (IS_TRUE) {
            var data = $('#kendoGrid').data("kendoGrid").dataSource.data();
            angular.forEach($('#kendoGrid').data("kendoGrid").dataSource.data(), function (dataItem) {
                dataItem.set('IS_ACTIVE', IS_TRUE?'Y':'N');
            });

        }


        vm.saveGridData = function (token, CREATED_BY, SC_USER_ID) {
            var data = $('#kendoGrid').data("kendoGrid").dataSource.data();
            var dataForSave = [];
            angular.forEach(data, function (val, key) {
                val['CREATED_BY'] = CREATED_BY;
                val['SC_USER_ID'] = SC_USER_ID;
                if (val.MC_USR_BYRACC_ID > 0) {
                    dataForSave.push(val);

                } else if (val.MC_USR_BYRACC_ID < 1 && val.IS_ACTIVE == 'Y') {
                    dataForSave.push(val);
                }

            });

            if (dataForSave.length == 0) {
                return;
            } else {
                return MrcDataService.saveUserBuyerAccMapData(dataForSave,token).then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);

                }, function (err) {
                    console.log(err);
                })
            }
            console.log(dataForSave);
        }


        function getUserData() {
            return   vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h3>#: data.USER_NAME_EN #</h3><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/common/SelectAllUserData').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                height: 400,
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };

               //$("#customers").kendoDropDownList(vm.userList);
        }




        vm.submitData = function (data, token) {
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
                        if (res.data.V_MC_COLOR_ID != 0) {
                            $state.go($state.current, { MC_COLOR_ID: res.data.V_MC_COLOR_ID });
                        }
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }

        vm.gridOptions = {
            autoBind: true,
            height: '400px',
            scrollable: true,
            navigatable: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success([]);
                    }
                },
                pageSize: 100
            },
            filterable: true,
            selectable: "cell",
            sortable: true,
            pageSize: 100,
            pageable: true,
            columns: [
                {
                    headerTemplate: "<input type='checkbox' ng-model='IS_ALL_SELECT' ng-change='makeBatchSelected(IS_ALL_SELECT)'> Select All",
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                    },
                    width: "50px"
                },
                { field: "BYR_ACC_NAME_EN", title: "Buyer Acc Name", type: "string", width: "80px" },
                { field: "BYR_ACC_SNAME", title: "Acc Short Name", type: "string", width: "100px" },
                { field: "BYR_ACC_DESC", title: "Buyer Acc Description", type: "string", width: "150px" },
            ]
        };

    }

})();