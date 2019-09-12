(function () {
    'use strict';

    angular.module('multitex.security').controller('UserOfficeMapController', ['$state', 'logger', 'config', '$q', '$scope', '$http', '$stateParams', '$filter', 'SecurityService', UserOfficeMapController]);
    function UserOfficeMapController($state, logger, config, $q, $scope, $http, $stateParams, $filter, SecurityService) {

        var vm = this;

        $scope.format = config.appDateFormat;
        vm.currentDate = $filter('date')(new Date(), config.appDateFormat);

        vm.form = {};
        vm.errors = {};

        activate();
        vm.Title = $state.current.Title || '';
        vm.showSplash = true;

        function activate() {
            var promise = [getUserData(), loadOfficeList()];
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
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            SecurityService.getUserDatalistByApi().then(function (res) {
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


        vm.getOfficeUserList = function () {
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


        function loadOfficeList() {

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: false,
                serverFiltering: false,
                transport: {
                    read: function (e) {
                        vm.showSplash = true;
                        console.log(vm.form.SC_USER_ID);
                        SecurityService.getDataByFullUrl('/api/security/UserMap/GetOfficeUserByID?pSC_USER_ID=' + (vm.form.SC_USER_ID || null)).then(function (res) {
                            e.success(res);
                            vm.showSplash = false;
                        });
                    }
                },
            })
        }


        function loadCompanyList() {

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: false,
                serverFiltering: false,
                transport: {
                    read: function (e) {
                        vm.showSplash = true;
                        console.log(vm.form.SC_USER_ID);
                        SecurityService.getDataByFullUrl('/api/security/UserMap/GetOfficeUserByID?pSC_USER_ID=' + (vm.form.SC_USER_ID || null)).then(function (res) {
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
                { field: "HR_COMPANY_ID", hidden: true },
                { field: "HR_OFFICE_ID", hidden: true },
                { field: "SC_MAP_STR_USR_ID", hidden: true },
                {
                    headerTemplate: "<input type='checkbox' ng-model='IS_ALL_SELECT' ng-change='makeBatchSelected(IS_ALL_SELECT)'> Select All",
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                    },
                    width: "8%"
                },
                //{ field: "STORE_CODE", title: "Code", width: "10%" },
                { field: "COMP_NAME_EN", title: "Company Name (EN)", width: "35%" },
                //{ field: "STORE_NAME_BN", title: "Name (BN)", width: "15%" },
                //{ field: "STORE_TYPE_NAME", title: "Store Type", width: "8%" },
                { field: "OFFICE_NAME_EN", title: "Office Name", width: "20%" },
                { field: "ADDRESS_EN", title: "Address", width: "35%" },
                //{ field: "INV_ITEM_CAT_LST", title: "Category", width: "8%" }
            ],
            //editable: true
        };

        vm.submitData = function (dataOri) {           

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                
                var list = _.filter(vm.gridOptionsDS.data(), function (x) { return x.IS_ACTIVE == "Y" });
                data["XML_MAP_D"] = SecurityService.xmlStringShort(list.map(function (o) {
                    return {
                        KNT_MAP_USR_COMP_ID: o.KNT_MAP_USR_COMP_ID == null ? 0 : o.KNT_MAP_USR_COMP_ID,
                        HR_COMPANY_ID: o.HR_COMPANY_ID == null ? 0 : o.HR_COMPANY_ID,
                        HR_OFFICE_ID: o.HR_OFFICE_ID == null ? 0 : o.HR_OFFICE_ID,
                    }
                }));

                return SecurityService.saveDataByUrl(data, '/UserMap/SaveOM').then(function (res) {

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