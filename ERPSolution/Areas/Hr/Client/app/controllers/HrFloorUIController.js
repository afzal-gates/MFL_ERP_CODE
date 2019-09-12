(function () {
    'use strict';
    angular.module('multitex.hr').controller('HrFloorUIController', ['$modalInstance', '$q', 'config', 'HrService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$modal', HrFloorUIController]);
    function HrFloorUIController($modalInstance, $q, config, HrService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $modal) {


        var vmS = this;

        //vm.form = {};
        console.log(formData);

        activate()
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
            });
        }

        vmS.form = formData.HR_OFFICE_ID ? formData : {};

        GetFloorList();
        GetOfficeList();
        GetBuildingList();
        getLKFloorList();
        getPrdFloorTypeList();

        console.log(vmS.form);

        $scope.cancel = function () {
            $modalInstance.close($scope["floorList"].data());
        }


        function GetOfficeList() {

            return $scope["officeList"] = {
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

            return $scope["buildingList"] = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/hr/GetBuildingList?pHR_OFFICE_ID=' + (vmS.form.HR_OFFICE_ID || null)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        function GetFloorList() {

            return $scope["floorList"] = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/hr/GetFloorList?pHR_PROD_BLDNG_ID=' + (vmS.form.HR_PROD_BLDNG_ID || null)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        function getLKFloorList() {
            return $scope["LKFloorList"] = {
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
            return $scope["prdFloorTypeList"] = {
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

        $scope["gridOptionsItem"] = {
            pageable: false,
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
            height: '300px',
            scrollable: true,
            columns: [

                { field: "HR_PROD_FLR_ID", hidden: true },
                { field: "HR_PROD_BLDNG_ID", hidden: true },
                { field: "LK_FLOOR_ID", hidden: true },
                { field: "FLOOR_DESC_BN", hidden: true },

                //, , , , , , , 
                { field: "FLOOR_NO", title: "FLOOR NO", type: "string", width: "7%" },
                { field: "FLOOR_CODE", title: "FLOOR CODE", type: "string", width: "7%" },
                { field: "FLOOR_DESC_EN", title: "FLOOR DESC EN", type: "string", width: "10%" },
                //{ field: "FLOOR_DESC_BN", title: "FLOOR DESC BN", type: "string", width: "10%" },
                { field: "BLDNG_DESC_EN", title: "BLDNG DESC EN", type: "string", width: "20%" },
                { field: "OFFICE_NAME_EN", title: "OFFICE NAME EN", type: "string", width: "10%" },
                //{ field: "OFFICE_SNAME", title: "OFFICE NAME", type: "string", width: "10%" },
                { field: "IS_ACTIVE", title: "ACTIVE", type: "string", width: "7%" },

                {
                    title: "Remove",
                    //template: '<a  title="Delete" ng-click="vmS.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vmS.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    template: '<a  title="Edit" ng-click="vmS.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "10%"
                }
            ],
            //editable: true
        };

        vmS.editItemData = function (dataItem) {

            var data = angular.copy(dataItem);
            vmS.form = data;
        }


        vmS.removeItemData = function (dataItem) {

            var data = angular.copy(dataItem);
            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     return HrService.saveDataByFullUrl(data, '/api/knit/DailyTrimsReceive/DeleteTrimsItem').then(function (res) {

                         if (res.success === false) {
                             vmS.errors = res.errors;
                         }
                         else {
                             GetTrimsItemList();
                             res['data'] = angular.fromJson(res.jsonStr);
                             config.appToastMsg(res.data.PMSG);
                         }
                     });
                 });
        }

        $scope.submitAll = function (dataOri) {

            var data = angular.copy(dataOri);

            return HrService.saveDataByUrl(data, '/api/hr/SaveFloor').then(function (res) {

                if (res.success === false) {
                    vmS.errors = res.errors;
                }
                else {
                    GetFloorList();
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                }
            });
        }
    }


})();

