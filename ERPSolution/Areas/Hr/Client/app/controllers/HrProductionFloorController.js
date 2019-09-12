(function () {
    'use strict';
    angular.module('multitex.hr').controller('HrProductionFloorController', ['$q', 'config', 'HrService', '$filter', '$http', '$stateParams', '$state', '$scope', '$modal', 'logger', HrProductionFloorController]);
    function HrProductionFloorController($q, config, HrService, $filter, $http, $stateParams, $state, $scope, $modal, logger) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.form = { IS_ACTIVE: 'Y' };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetCompanyList(), GetBuildingList(), GetOfficeList(), GetFloorList(), getLKFloorList(), getPrdFloorTypeList(), BuildingFloorList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function GetCompanyList() {

            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };

        function GetOfficeList() {

            return vm.officeList = {
                optionLabel: "-- Select Office --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getDataByFullUrl('/api/hr/GetOfficeList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "OFFICE_NAME_EN",
                dataValueField: "HR_OFFICE_ID"
            };
        };


        function GetBuildingList() {

            return vm.buildingList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/hr/GetBuildingList?pHR_OFFICE_ID=' + (vm.form.HR_OFFICE_ID || null)).then(function (res) {
                            e.success([{
                                BLDNG_DESC_EN: '--New Building--',
                                HR_PROD_BLDNG_ID: -1
                            }].concat(res));
                        });
                    }
                }
            });

        };

        function GetFloorList() {

            return vm.floorList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/hr/GetFloorList?pHR_PROD_BLDNG_ID=' + (vm.form.HR_PROD_BLDNG_ID || null)).then(function (res) {
                            e.success([{
                                FLOOR_DESC_EN: '--New Floor--',
                                HR_PROD_FLR_ID: -1
                            }].concat(res));
                        });
                    }
                }
            });

        };

        vm.OfficeChange = function (items) {
            if (vm.form.HR_OFFICE_ID > 0) {
                vm.buildingList.read();
                vm.buildingFloorList.read();

            }
        }

        vm.newBuildingAdd = function (items) {

            if (vm.form.HR_PROD_BLDNG_ID == -1) {
                var item = angular.copy(items);

                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: '/HR/HR/BuildingUI',
                    controller: 'HrBuildingUIController',
                    controllerAs: 'vmS',
                    size: 'lg',
                    windowClass: 'large-Modal',
                    resolve: {
                        formData: function (HrService) {
                            item["HR_OFFICE_ID"] = items.HR_OFFICE_ID;
                            item["HR_COMPANY_ID"] = items.HR_COMPANY_ID;
                            return item;
                        }
                    }
                });
                modalInstance.result.then(function (dataItem) {
                    console.log(dataItem);
                    vm.buildingList.read();
                    //vm.DyItemList = new kendo.data.DataSource({
                    //    data: dataItem
                    //});

                }, function () {
                    console.log('Modal dismissed at: ' + new Date());
                });
            }
            else if (vm.form.HR_PROD_BLDNG_ID > 0) {

                vm.floorList.read();
                vm.buildingFloorList.read();

            }
        }

        vm.newFloorAdd = function (items) {

            if (vm.form.HR_PROD_FLR_ID == -1) {
                var item = angular.copy(items);

                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: '/HR/HR/FloorUI',
                    controller: 'HrFloorUIController',
                    controllerAs: 'vmS',
                    size: 'lg',
                    windowClass: 'large-Modal',
                    resolve: {
                        formData: function (HrService) {
                            item["HR_OFFICE_ID"] = items.HR_OFFICE_ID;
                            item["HR_PROD_BLDNG_ID"] = items.HR_PROD_BLDNG_ID;
                            return item;
                        }
                    }
                });
                modalInstance.result.then(function (dataItem) {
                    console.log(dataItem);
                    vm.floorList.read();

                }, function () {
                    console.log('Modal dismissed at: ' + new Date());
                });
            }
            else if (vm.form.HR_PROD_FLR_ID > 0) {
                vm.buildingFloorList.read();
            }
        }

        vm.editItemData = function (data) {
            console.log(data);
            vm.form = angular.copy(data);
        }

        vm.reset = function () {
            vm.form = { HR_OFFICE_ID: '', IS_ACTIVE: 'Y', HR_PROD_BLDNG_ID: '', HR_PROD_FLR_ID: '', LK_GARM_TYPE_ID: '', LK_FLOOR_ID: '' };
        }

        function getLKFloorList() {
            return vm.LKFloorList = {
                optionLabel: "-- Select Floor --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getLookupListData(18).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function getPrdFloorTypeList() {
            return vm.prdFloorTypeList = {
                optionLabel: "-- Select Floor Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getLookupListData(104).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function BuildingFloorList() {
            vm.buildingFloorList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.GetDataByUrl('/api/hr/GetPrdLineList?pHR_PROD_LINE_ID=' + (vm.form.HR_PROD_LINE_ID || null) + '&pHR_PROD_FLR_ID=' + (vm.form.HR_PROD_FLR_ID || null) + '&pHR_PROD_BLDNG_ID=' + (vm.form.HR_PROD_BLDNG_ID || null) + '&pHR_OFFICE_ID=' + (vm.form.HR_OFFICE_ID || null)).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 10
            });
        };

        vm.gridOptions = {
            pageable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains",
                        startswith: "Starts With",
                        eq: "Is Equal To"
                    }
                }
            },
            height: '400px',
            scrollable: true,
            sortable: true,
            columns: [
                { field: "HR_PROD_LINE_ID", hidden: true },
                { field: "HR_PROD_FLR_ID", hidden: true },
                { field: "LK_GARM_TYPE_ID", hidden: true },
                { field: "LK_FLOOR_ID", hidden: true },
                { field: "HR_OFFICE_ID", hidden: true },
                { field: "HR_COMPANY_ID", hidden: true },
                { field: "HR_PROD_BLDNG_ID", hidden: true },

                { field: "LINE_NO", title: "Line No", type: "string", width: "7%" },
                { field: "LINE_CODE", title: "Line Code", type: "string", width: "7%" },
                { field: "LINE_DESC_EN", title: "Line DESC (EN)", type: "string", width: "10%" },
                { field: "LINE_DESC_BN", title: "Line DESC (BN)", type: "string", width: "10%" },
                { field: "IS_ACTIVE", title: "Active", type: "string", width: "7%" },
                { field: "FLOOR_DESC_EN", title: "Floor DESC (EN)", type: "string", width: "15%" },
                { field: "BLDNG_DESC_EN", title: "Building DESC (EN)", type: "string", width: "15%" },
                { field: "OFFICE_NAME_EN", title: "Office Name", type: "string", width: "15%" },

                {
                    title: "",
                    template: '<a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ],
            //editable: true
            //, , , , , , , , , 
            //FLOOR_CODE, , FLOOR_NO, BLDNG_CODE, , BLDNG_NO, COMP_CODE, COMP_DESC, , COMP_SNAME
        };

        vm.submitAll = function (data) {

            return HrService.saveDataByUrl(data, '/api/hr/SaveLine').then(function (res) {

                res['data'] = angular.fromJson(res.jsonStr);
                config.appToastMsg(res.data.PMSG);
                if (res.data.OPHR_PROD_LINE_ID > 0) {
                    vm.form = { HR_OFFICE_ID: '', IS_ACTIVE: 'Y', HR_PROD_BLDNG_ID: '', HR_PROD_FLR_ID: '', LK_GARM_TYPE_ID: '', LK_FLOOR_ID: '' };
                    BuildingFloorList();
                }

            });
        }

    }

})();