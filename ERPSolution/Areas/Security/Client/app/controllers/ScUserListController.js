(function () {
    'use strict';

    angular.module('multitex.security').controller('ScUserListController', ['$state', 'logger', 'config', '$q', '$scope', '$http', '$stateParams', '$filter', '$modal', 'SecurityService', ScUserListController]);

    function ScUserListController($state, logger, config, $q, $scope, $http, $stateParams, $filter, $modal, SecurityService) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;

        function activate(){
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        vm.editRecord = function (data) {
            console.log(data);
            $state.go("UserEntry", { data: data });
        }

        $scope.open = function (data) {
 
            //SecurityService.getRolesByUserId(data.SC_USER_ID).then(function (res) {
            //    $scope.roles = res;
            //});

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'myModalContent.html',
                controller: 'ModalRoleAssignController',
                windowClass: 'large-Modal',
                resolve: {
                    items: function () {
                        return data;
                    },
                    roles: function (SecurityService) {
                        return SecurityService.getRolesByUserId(data.SC_USER_ID);
                    },
                }
            });

            modalInstance.result.then(function (data) {

                //if (data.SC_USER_ROLE_ID) {
                //    return SecurityService.updateRoleData(data).then(function (res) {
                //        config.appToastMsg(res.data.vMsg);
                //        $state.go("UserList", {}, {reload:true});
                //    });
                //} else {
                //    return SecurityService.saveRoleData(data).then(function (res) {
                //        config.appToastMsg(res.data.vMsg);
                //        $state.go("UserList", {}, { reload: true });
                //    });
                //}




            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };




        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        SecurityService.getUserDataList().then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 10
            },
            filterable: {
                mode: "row"
            },
            selectable: "row",
            sortable: true,
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "USER_TYPE_DISPLAY", title: "User Type", type: "string", width: "80px" },
                { field: "LOGIN_ID", title: "User ID", type: "string", width: "100px" },
                { field: "USER_NAME_EN", title: "User Full Name", type: "string", width: "150px" },
                { field: "USER_EMAIL", title: "Mail", type: "string", width: "150px" },

                { field: "EMPLOYEE_CODE", title: "Emp.Code", type: "string", width: "100px" },
                
                { field: "ROLE_NAME_EN", title: "Role", type: "string", width: "150px" },
                { field: "USER_STATUS_NAME", title: "Status", type: "string", width: "100px" },
                {
                    title: "Action",
                    template: function () {
                        return "</a><a ng-click='vm.editRecord(dataItem)' class='btn btn-xs blue' ng-disabled=dataItem.USER_TYPE=='B'><i class='fa fa-edit'> Edit</i></a></a> &nbsp;&nbsp;&nbsp;<a ng-click='open(dataItem)' class='btn btn-xs green-meadow' ng-disabled=dataItem.USER_TYPE=='B'><i  class='fa' ng-class=dataItem.SC_ROLE_ID?'':'fa-plus'>{{dataItem.SC_ROLE_ID?'Show Role(s)':'Assign Role'}}</i></a>";
                    },
                    width: "100px"
                }
            ]
        };


    }

    

})();