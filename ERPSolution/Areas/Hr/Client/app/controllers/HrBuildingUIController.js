(function () {
    'use strict';
    angular.module('multitex.hr').controller('HrBuildingUIController', ['$modalInstance', '$q', 'config', 'HrService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$modal', HrBuildingUIController]);
    function HrBuildingUIController($modalInstance, $q, config, HrService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $modal) {


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
        
        GetCompanyList();
        GetOfficeList();
        GetBuildingList();
              
        console.log(vmS.form);

        $scope.cancel = function () {
            $modalInstance.close($scope["buildingList"].data());
        }


        function GetCompanyList() {

            return $scope["companyList"] = {
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

                { field: "HR_PROD_BLDNG_ID", hidden: true },
                { field: "HR_COMPANY_ID", hidden: true },
                { field: "HR_OFFICE_ID", hidden: true },
                //, , , , , , 
                //, , , , 

                { field: "BLDNG_NO", title: "Building No", type: "string", width: "7%" },
                { field: "BLDNG_CODE", title: "Building Code", type: "string", width: "7%" },
                { field: "BLDNG_DESC_EN", title: "BLDNG_DESC_EN", type: "string", width: "10%" },
                { field: "BLDNG_DESC_BN", title: "BLDNG_DESC_BN", type: "string", width: "10%" },
                { field: "IS_ACTIVE", title: "IS_ACTIVE", type: "string", width: "10%" },
                { field: "COMP_NAME_EN", title: "COMP_NAME_EN", type: "string", width: "10%" },
                { field: "OFFICE_NAME_EN", title: "OFFICE_NAME_EN", type: "string", width: "10%" },

                {
                    title: "Remove",
                    //template: '<a  title="Delete" ng-click="vmS.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vmS.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    template: '<a  title="Edit" ng-click="vmS.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
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

            return HrService.saveDataByUrl(data, '/api/hr/SaveBuilding').then(function (res) {

                if (res.success === false) {
                    vmS.errors = res.errors;
                }
                else {
                    GetBuildingList();
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                }
            });
        }
    }


})();

