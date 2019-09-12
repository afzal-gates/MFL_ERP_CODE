(function () {
    'use strict';

    angular.module('multitex.security').controller('ModuleController', ['logger', 'config', '$q', '$scope', '$http', ModuleController]);

    function ModuleController(logger, config, $q, $scope, $http) {
        var vm = this;
        vm.title = config.appTitle;
        vm.showSplash = true;
        vm.transMode = false;


        activate(); 



        function activate() {
            var promises = [getSystemModuleTreeData()];
            return $q.all(promises).then(function () {
                vm.showSplash = false;
                //logger.success(config.appTitle + ' : Security Module loaded!', null);
            });
        }

        //function getName() {
        //   return SecurityService.getName().then(function (data) {
        //       vm.contact = data;
        //       return vm.contact;
        //    });
        //}

        //function (){

        //} 


        //////////////////// Start Menu Controller //////////////////////////

        //vm.subModuleList = {
        //    optionLabel: "Select",
        //    autoBind: true,
        //    dataSource: {
        //        transport: {
        //            read: {
        //                url: "/Security/Menu/ScSubModuleData",
        //                dataType: "json"
        //            }
        //        }
            
            //},
            //change: function getMenuTreeData() {
            //    return $http.get('/Security/Menu/MenuData').then(function (res) {
            //        return vm.menuOptions = {
            //            dataSource: res.data
            //        };
            //    });

            //},            
            //dataTextField: "SYS_MODULE_NAME",
            //dataValueField: "SC_SYSTEM_MODULE_ID"
            //,
            //select: function onSelect(e) {
            //    var dataItem = this.dataItem(e.item.index());
            //    //alert(dataItem.value);
            //    //console.log(dataItem.HR_DAY_TYPE_ID);

            //    if (dataItem.HR_DAY_TYPE_ID == 3) {
            //        vm.enable = true;
            //    } else {
            //        vm.enable = false;
            //    };
                
            //}
        //};

        //vm.securityLevelList = {
        //    optionLabel: "Select",
        //    autoBind: true,
        //    dataSource: {
        //        transport: {
        //            read: {
        //                url: "/Security/Menu/ScSecurityLevelData",
        //                dataType: "json"
        //            }
        //        }
        //    },
        //    dataTextField: "SCR_LEVEL_NAME",
        //    dataValueField: "SC_SECURITY_LEVEL_ID"
        //};

        function getSystemModuleTreeData() {
            return $http.get('/Security/Module/SystemModuleData').then(function (res) {
                return vm.thingsOptions = {
                    dataSource: res.data
                };
            });

        }

        //function getMenuTreeData() {
        //    return $http.get('/Security/Menu/MenuData').then(function (res) {
        //        return vm.menuOptions = {
        //            dataSource: res.data
        //        };
        //    });

        //}
        
        

        //$scope.show = function (dataItem) {
        //    dataItem.SYS_MODULE_NAME;
        //};

        //$scope.click = function (dataItem) {
        //    alert(dataItem.SYS_MODULE_NAME);
        //};

        //function makeItem() {
        //    var txt = kendo.toString(new Date(), "HH:mm:ss");
        //    return { text: txt };
        //};

        //$scope.frmMenuCreation = function () {
        //    if ($scope.frmMenuCreation.$valid) {
        //        // Submit as normal
        //    } else {
        //        $scope.frmMenuCreation.submitted = true;
        //    }
        //}

        //$scope.addAfter = function (item) {
        //    var array = item.parent();
        //    var index = array.indexOf(item);
        //    var newItem = makeItem();
        //    array.splice(index + 1, 0, newItem);
        //};

        //$scope.addBelow = function () {
        //    // can't get this to work by just modifying the data source
        //    // therefore we're using tree.append instead.
        //    var newItem = makeItem();
        //    $scope.tree.append(newItem, $scope.tree.select());
        //};

        //$scope.remove = function (item) {
        //    var array = item.parent();
        //    var index = array.indexOf(item);
        //    array.splice(index, 1);
        //};

        window.$scope = $scope;

    }

})();