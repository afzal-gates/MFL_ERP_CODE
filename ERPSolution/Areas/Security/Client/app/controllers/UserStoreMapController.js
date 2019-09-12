(function () {
    'use strict';

    angular.module('multitex.security').controller('UserStoreMapController', ['$state', 'logger', 'config', '$q', '$scope', '$http', '$stateParams', '$filter', 'SecurityService', UserStoreMapController]);

    function UserStoreMapController($state, logger, config, $q, $scope, $http, $stateParams, $filter, SecurityService) {

        var vm = this;

        $scope.format = config.appDateFormat;
        vm.currentDate = $filter('date')(new Date(), config.appDateFormat);

        vm.form = {};
        vm.errors = {};

        activate();
        vm.Title = $state.current.Title || '';
        vm.showSplash = true;

        function activate() {
            var promise = [getUserData(), loadStoreList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }


        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #<p>(#: data.LOGIN_ID #)</p></h4><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            SecurityService.getUserDataList().then(function (res) {
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

            //$("#customers").kendoDropDownList(vm.userList);
        }


        vm.getStoreUserList = function () {
            if (vm.form.SC_USER_ID) {
                //vm.showSplash = true;
                vm.gridOptionsDS.read();
                //vm.showSplash = false;
            }
        }


        $scope.makeBatchSelected = function (IS_TRUE) {
            var data = $('#kendoGrid').data("kendoGrid").dataSource.data();
            angular.forEach($('#kendoGrid').data("kendoGrid").dataSource.data(), function (dataItem) {
                dataItem.set('IS_ACTIVE', IS_TRUE ? 'Y' : 'N');
            });

        }


        function loadStoreList() {

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: false,
                serverFiltering: false,
                transport: {
                    read: function (e) {
                        vm.showSplash = true;
                        console.log(vm.form.SC_USER_ID);
                        SecurityService.getDataByFullUrl('/api/security/UserMap/GetStoreUserByID?pSC_USER_ID=' + (vm.form.SC_USER_ID || null)).then(function (res) {
                            e.success(res);
                            vm.showSplash = false;
                        });
                    }
                },
            })
        }

        vm.gridOptions = {
            //autoBind: true,            
            sortable: false,
            pageable: false,
            columns: [
                { field: "SCM_STORE_ID", hidden: true },
                { field: "SC_MAP_STR_USR_ID", hidden: true },
                {
                    headerTemplate: "<input type='checkbox' ng-model='IS_ALL_SELECT' ng-change='makeBatchSelected(IS_ALL_SELECT)'> Select All",
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                    },
                    width: "7%"
                },
                { field: "STORE_CODE", title: "Code", width: "10%" },
                { field: "STORE_NAME_EN", title: "Name (EN)", width: "15%" },
                { field: "STORE_NAME_BN", title: "Name (BN)", width: "15%" },
                { field: "STORE_TYPE_NAME", title: "Store Type", width: "8%" },
                { field: "OFFICE_NAME_EN", title: "Office Name", width: "15%" },
                { field: "ADDRESS_EN", title: "Address", width: "15%" },
                //{ field: "INV_ITEM_CAT_LST", title: "Category", width: "8%" }
            ],
            //editable: true
        };

        vm.submitData = function (dataOri) {
            var data = angular.copy(dataOri);

            console.log(data.XML_MAP_D);

            var list = _.filter(vm.gridOptionsDS.data(), function (x) { return x.IS_ACTIVE == "Y" });
            //data["XML_MAP_D"] = "Afzal";
            data["XML_MAP_D"] = SecurityService.xmlStringShort(list.map(function (o) {
                return {
                    SC_MAP_STR_USR_ID: o.SC_MAP_STR_USR_ID == null ? 0 : o.SC_MAP_STR_USR_ID,
                    SCM_STORE_ID: o.SCM_STORE_ID == null ? 0 : o.SCM_STORE_ID,
                    SC_USER_ID: vm.form.SC_USER_ID,
                }
            }));
            console.log(data.XML_MAP_D);

            if (fnValidate() == true) {

                return SecurityService.saveDataByUrl(data, '/UserMap/SaveSM').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        vm.gridOptionsDS.read();
                    }
                });
            }
        }
    }


})();