(function () {
    'use strict';

    var items;
    var modalInstance;

    angular.module('multitex.security').controller('ScRoleController', ['logger', 'config', '$q', '$scope', '$http', '$modal', ScRoleController]);

    function ScRoleController(logger, config, $q, $scope, $http, $modal) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;

        vm.insert = true;


        vm.TranCancel = function () {
            vm.insert = true;
            vm.reset();
        };

        vm.reset = function () {
            //$scope.frmCompany.submitted = false;

            vm.insert = true;
            vm.form = { IS_ADMIN: 'N', IS_ACTIVE: 'Y' };
        };

        vm.editRecord = function (dataItem) {
            vm.insert = false;

            vm.form = dataItem;
        };

        vm.submit = function (isValid, form, insert) {

            console.log(insert);
            console.log(form);

            if (!isValid) return;

            //alert('ok');            

            //submit the data to the server
            if (insert) {

                $http.post('/Security/ScRole/Save', vm.form).success(function (data, status, headers, config) {



                    if (data != '') {
                        if (data.substr(0, 9) == 'MULTI-001') {
                            toastr.success(data.substr(9), "MultiTEX");
                            vm.reset();
                            //vm.TRAN_MODE = 1;
                        }
                        if (data.substr(0, 9) == 'MULTI-002') {
                            toastr.info(data.substr(9), "MultiTEX");
                        }
                        if (data.substr(0, 9) == 'MULTI-003') {
                            toastr.warning(data.substr(9), "MultiTEX");
                        }
                        if (data.substr(0, 9) == 'MULTI-005') {
                            toastr.error(data.substr(9), "MultiTEX");
                        }

                    }
                }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });

            }
            else {

                $http.post('/Security/ScRole/Update', vm.form).success(function (data, status, headers, config) {

                    if (data != '') {
                        if (data.substr(0, 9) == 'MULTI-001') {
                            toastr.success(data.substr(9), "MultiTEX");
                            //$window.location.reload();
                            vm.reset();
                            //vm.TRAN_MODE = 1;
                        }
                        if (data.substr(0, 9) == 'MULTI-002') {
                            toastr.info(data.substr(9), "MultiTEX");
                        }
                        if (data.substr(0, 9) == 'MULTI-003') {
                            toastr.warning(data.substr(9), "MultiTEX");
                        }
                        if (data.substr(0, 9) == 'MULTI-005') {
                            toastr.error(data.substr(9), "MultiTEX");
                        }

                    }
                }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });

            }

            return;

        };



        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Security/ScRole/RoleListData"  //+ "&pType=" + showType
                        }).
                        success(function (data, status, headers, config) {
                            e.success(data)
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                    }
                },
                pageSize: 5
            },
            filterable: {
                mode: "row"
            },
            selectable: "row",
            sortable: true,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "ROLE_NAME_EN", title: "Name [E]", type: "string", width: "250px" },
                { field: "ROLE_SNAME", title: "Short Name", type: "string", width: "200px" },                
                //{ field: "ROLE_NAME_BN", title: "Name [B]", type: "string", width: "200px" },                
                //{ field: "ROLE_DESC", title: "Description", type: "string", width: "300px" },
                { field: "IS_ADMIN", title: "", type: "string", width: "100px" },
                { field: "IS_ACTIVE", title: "Active?", type: "string", width: "100px" },
                {
                    title: "Action",
                    template: function () {
                        return "<a ng-click='vm.editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>&nbsp; ";
                    },
                    width: "50px"
                }
            ]
        };


        //$scope.open = function (dataItem) {
            
        //    items = dataItem;

        //    modalInstance = $modal.open({
        //        //template: '<h3>Hello</h3>'
        //        templateUrl: '/Security/ScRole/ScRoleMenu', //'myModalContent.html',
        //        controller: 'ScRoleMenuController',
        //        controllerAs: 'vm',
        //        windowClass: 'large-Modal',
        //        //size: size//,
        //        resolve: {
        //            items: function () {
        //                return items;
        //            }
        //        }
        //    });
        //};



        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        //function getData(){
        //     return AdminService.getData().then(function (data) {
        //         //vm.schedulerOptions.dataSource.data = data;
        //         //return vm.schedulerOptions.dataSource.data;

        //    });
        //}






    };


    

    

})();