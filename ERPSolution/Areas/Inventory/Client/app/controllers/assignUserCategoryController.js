(function () {
    'use strict';
    angular.module('multitex.inventory').controller('AssignUserCategoryController', ['$q', 'config', 'InventoryDataService', '$stateParams', '$http', '$state', '$scope', 'logger', AssignUserCategoryController]);
    function AssignUserCategoryController($q, config, InventoryDataService, $stateParams, $http, $state, $scope, logger) {

        var vm = this;
        vm.errors = null;
        vm.Title = $state.current.Title || '';

        vm.ACTION_MODE = 1;
        vm.selectedItem = {};
        vm.form = { IS_ACTIVE: 'Y', IS_LEAF: 'N' };


        vm.treeSelecteditem = {};
        vm.IsChange = false;
        vm.TranCancel = function (myForm) {
            vm.IsChange = false;
            vm.getMenuTreeData(myForm);
        };

        vm.RoleSelect = function () {
            //vm.isInsert = false;
            vm.IsChange = false;
        };


        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        };

        function reset() {
            vm.ACTION_MODE = 1;
            vm.form = { IS_ACTIVE: 'Y', IS_LEAF: 'N', ITEM_CAT_LEVEL: vm.selectedItem.ITEM_CAT_LEVEL + 1, CREATED_BY: vm.form.CREATED_BY };
        };

        vm.TranCancel = function () {
            reset();
        };

        //var dataSource
        //dataSource.read();

        // function that gathers IDs of checked nodes
        var x = 0;
        function checkedNodeIds(nodes, checkedNodes) {

            for (var i = 0; i < nodes.length; i++) {

                if (nodes[i].checked == true) {
                    //checkedNodes.push(nodes[i].id, nodes[i].SC_MENU_ID, nodes[i].MENU_NAME_EN, nodes[i].IsChecked);
                    nodes[i].IsChecked = nodes[i].checked;
                    //checkedNodes[x] = nodes[i];
                    //x++;
                }
                else if (nodes[i].checked == false) {
                    nodes[i].IsChecked = false;
                }


                roleMenuNode[x] = { SC_USER_ID: vm.form.SC_USER_ID, INV_ITEM_CAT_ID: nodes[i].INV_ITEM_CAT_ID, IsChecked: nodes[i].IsChecked };
                checkedNodes[x] = nodes[i];
                x++;

                if (nodes[i].hasChildren) {
                    checkedNodeIds(nodes[i].children.view(), checkedNodes);
                }
            }
        };


        // show checked node IDs on datasource change 
        var checkedNodes = [];
        var roleMenuNode = [];
        function onCheck() {
            var treeView = $("#ItemCategTreeview").data("kendoTreeView"),
                message, selected = treeView.select(), treeSelecteditem = treeView.dataItem(selected);

            vm.treeSelecteditem = treeSelecteditem;
            //console.log(vm.treeSelecteditem.IS_REPORT);

            //if (vm.treeSelecteditem.IS_REPORT == 'Y') {
            //    $("#reportGrid").empty();
            //    vm.reportGrid();
            //};


            vm.isInsert = true;
            //checkedNodes = treeView.dataSource._data;

            //checkedNodes = [];
            x = 0;
            checkedNodeIds(treeView.dataSource.view(), checkedNodes);
            //console.log(roleMenuNode);

            //if (checkedNodes.length > 0) {
            //    message = "IDs of checked nodes: " + checkedNodes.join(",");
            //} else {
            //    message = "No nodes checked.";
            //}


            //$("#result").html(message);
        };
        
        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h3>#: data.USER_NAME_EN #</h3><p>#: data.DEPARTMENT_NAME_EN #</p><p>#: data.DESIGNATION_NAME_EN #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getUserDatalist().then(function (res) {
                                e.success(res);
                                //console.log(res);

                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };
            //$("#customers").kendoDropDownList(vm.userList);
        };


        vm.actionModeSelection = function () {
            if (vm.ACTION_MODE == 1) {
                reset();
            }
            else if (vm.ACTION_MODE == 2) {
                vm.form = vm.selectedItem;
            }
        };

        

        vm.isInsert = true;
        vm.getMenuTreeData = function (myForm) {
            if (myForm.SC_USER_ID == '' || myForm.SC_USER_ID == null) {
                //alert('Please select User.');
                return;
            }

            return $http.post('/Inventory/ScRoleCategory/ScRoleMenuData', myForm).then(function (res) {

                checkedNodes = [];
                //console.log(res.data.length);
                if (res.data.length > 0) {
                    vm.IsChange = true;
                }


                return vm.menuOptions = {
                    checkboxes: {
                        checkChildren: true,
                        template: "<input type='checkbox' ng-model='dataItem.IsChecked' />"
                    },
                    dataSource: res.data,// [{ id: 1, text: "texxt1", items: [{ id: 2, text: "texxt2", items: [] }, { id: 3, text: "texxt3", items: [] }] }]
                    //select: function (e) {
                    //    alert('test');

                    //    var tv = $('#menuTreeview').data('kendoTreeView'),
                    //        selected = tv.select(),
                    //        item = tv.dataItem(selected);
                    //    //console.log("Change", this.select());
                    //    console.log(item);
                    //},
                    check: onCheck
                    //change: function (e) {
                    //    alert('t');
                    //    console.log(selectedItem)
                    //    console.log("Change", this.select());
                    //}
                };

            });
        };


        vm.submit = function (isInsert, myForm) {
            //if (!isValid) return;
            if (checkedNodes.length < 1) return;

            //alert('ok');
            //console.log(checkedNodes[0]);
            //console.log(roleMenuNode);

            //submit the data to the server
            if (isInsert) {
                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Inventory/ScRoleCategory/Save',
                    method: 'post',
                    data: { roleMenuNode: roleMenuNode }
                }).success(function (data, status, headers, config1) {
                    vm.errors = [];
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        config.appToastMsg(data.vMsg);

                    }
                }).error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });
            }

            return;
        };


    }

})();