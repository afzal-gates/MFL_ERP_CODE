(function () {
    'use strict';

    angular.module('multitex.admin').controller('AdminController', ['AdminService', 'logger','config','$q','$scope','$http', AdminController]);

    function AdminController(AdminService, logger, config, $q, $scope, $http) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;
        
        vm.fromDate = null;
        vm.toDate = null;

        vm.enable = false;

        vm.mainGridOptions1=[];

        vm.form = {};


        /////////// Start Department //////////////////////////////////////////

       
        function getTreeData() {
            return $http.get('/Hr/Admin/HrDepartment/DeptListData').then(function (res) {
               return vm.thingsOptions = {
                   dataSource: res.data,
                   select: onSelect
               };
            });
        }

        function onSelect(e) {
            var dataItem = $("#DeptTreeview").getKendoTreeView().dataItem(e.node);
            vm.form.PARENT_ID = dataItem.PARENT_ID;

            //alert(dataItem.PARENT_ID);
            //console.log(dataItem);
        };
             
        $scope.show = function (dataItem) {
            dataItem.DEPARTMENT_NAME_EN;
        };

        $scope.click = function (dataItem) {
            alert(dataItem.DEPARTMENT_NAME_EN);
        };

        function makeItem() {
            var txt = kendo.toString(new Date(), "HH:mm:ss");
            return { text: txt };
        };

        $scope.frmDepartment = function () {
            if ($scope.frmDepartment.$valid) {
                // Submit as normal
            } else {
                $scope.frmDepartment.submitted = true;
            }
        }

        $scope.addAfter = function (item) {
            var array = item.parent();
            var index = array.indexOf(item);
            var newItem = makeItem();
            array.splice(index + 1, 0, newItem);
        };

        $scope.addBelow = function () {
            // can't get this to work by just modifying the data source
            // therefore we're using tree.append instead.
            var newItem = makeItem();
            $scope.tree.append(newItem, $scope.tree.select());
        };

        $scope.remove = function (item) {
            var array = item.parent();
            var index = array.indexOf(item);
            array.splice(index, 1);
        };

        window.$scope = $scope;


        vm.departmentList = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: {
                        url: "/Hr/Admin/HrDepartment/SelectAll",
                        dataType: "json"
                    }
                }
            },
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID"
        };

        ////////// End Department //////////////////////////


        //activate();

        function activate(){
            var promise = getTreeData();
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        function getData(){
             return AdminService.getData().then(function (data) {
                 //vm.schedulerOptions.dataSource.data = data;
                 //return vm.schedulerOptions.dataSource.data;
           
            });
        }

 
        

            
        
    }

    

})();