(function () {
    'use strict';

    angular.module('multitex.security').controller('UserProdCatMapController', ['$state', 'logger', 'config', '$q', '$scope', '$http', '$stateParams', '$filter', 'SecurityService', UserProdCatMapController]);
    function UserProdCatMapController($state, logger, config, $q, $scope, $http, $stateParams, $filter, SecurityService) {

        var vm = this;

        $scope.format = config.appDateFormat;
        vm.currentDate = $filter('date')(new Date(), config.appDateFormat);

        vm.form = {};
        vm.errors = {};

        activate();
        vm.Title = $state.current.Title || '';
        vm.showSplash = true;

        function activate() {
            var promise = [getUserData(), loadProdCateList()];
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


        vm.getProdCatUserList = function () {
            if (vm.form.SC_USER_ID) {
                vm.gridOptionsDS.read();
            }
        }


        $scope.makeBatchSelected = function (IS_TRUE) {
            var data = $('#kendoGrid').data("kendoGrid").dataSource.data();
            angular.forEach($('#kendoGrid').data("kendoGrid").dataSource.data(), function (dataItem) {
                dataItem.set('IS_ACTIVE', IS_TRUE ? 'Y' : 'N');
            });

        }


        function loadProdCateList() {

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: false,
                serverFiltering: false,
                transport: {
                    read: function (e) {
                        vm.showSplash = true;
                        console.log(vm.form.SC_USER_ID);
                        SecurityService.getDataByFullUrl('/api/security/UserMap/GetProdCatUserByID?pSC_USER_ID=' + (vm.form.SC_USER_ID || null)).then(function (res) {
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
                { field: "SC_MAP_USR_PRD_CAT_ID", hidden: true },
                { field: "SC_USER_ID", hidden: true },
                { field: "RF_FAB_PROD_CAT_ID", hidden: true },
                {
                    headerTemplate: "<input type='checkbox' ng-model='IS_ALL_SELECT' ng-change='makeBatchSelected(IS_ALL_SELECT)'> Select All",
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                    },
                    width: "10%"
                },
                { field: "FAB_PROD_CAT_CODE", title: "Code", width: "20%" },
                { field: "FAB_PROD_CAT_NAME", title: "Category Name", width: "35%" },
                { field: "FAB_PROD_CAT_SNAME", title: "Short Name", width: "35%" },
                //{ field: "IS_SUB_CONTR", title: "Sub-Contract", width: "10%" },
                //{ field: "PRD_PROD_CAT_PRFX", title: "PRD_PROD_CAT_PRFX", width: "35%" },

            ],
            //editable: true
        };

        vm.submitData = function (dataOri) {           

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                
                var list = _.filter(vm.gridOptionsDS.data(), function (x) { return x.IS_ACTIVE == "Y" });
                data["XML_MAP_D"] = SecurityService.xmlStringShort(list.map(function (o) {
                    return {
                        SC_MAP_USR_PRD_CAT_ID: o.SC_MAP_USR_PRD_CAT_ID == null ? 0 : o.SC_MAP_USR_PRD_CAT_ID,
                        SC_USER_ID: o.SC_USER_ID == null ? 0 : o.SC_USER_ID,
                        RF_FAB_PROD_CAT_ID: o.RF_FAB_PROD_CAT_ID == null ? 0 : o.RF_FAB_PROD_CAT_ID,
                    }
                }));

                return SecurityService.saveDataByUrl(data, '/UserMap/SavePCM').then(function (res) {

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