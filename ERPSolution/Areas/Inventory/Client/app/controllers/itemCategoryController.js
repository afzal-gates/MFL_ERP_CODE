(function () {
    'use strict';
    angular.module('multitex.inventory').controller('ItemCategoryController', ['$q', 'config', 'InventoryDataService', '$stateParams', '$state', '$scope', '$modal', 'logger', ItemCategoryController]);
    function ItemCategoryController($q, config, InventoryDataService, $stateParams, $state, $scope, $modal, logger) {

        var vm = this;
        vm.errors = null;
        vm.Title = $state.current.Title || '';

        vm.ACTION_MODE = 1;
        vm.selectedItem = {IS_LEAF:'Y'};
        vm.form = { IS_ACTIVE: 'Y', IS_LEAF: 'N' };        

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getTreeData(), getBrandMapList()];
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
        function getCategData() {
            return InventoryDataService.getItemCategTreeList().then(function (res) {
                //var dataSource = new kendo.data.HierarchicalDataSource({
                //    transport: {
                //        read: function (e) {
                //            e.success(res);
                //        }
                //    },                   
                //});

                var treeview = $("#ItemCategTreeview").data("kendoTreeView");
                var selectedNode = treeview.select();
                var item = treeview.dataItem(selectedNode);
                var bar = treeview.findByUid(item.uid);
                treeview.select(bar);
                //console.log(bar);

                treeview.setDataSource(new kendo.data.HierarchicalDataSource({
                    data: res
                }));

                //console.log(item);
                
                
                
                //$("#ItemCategTreeview").kendoTreeView({
                //    autoScroll: true,
                //    dataSource: dataSource
                //});
                //dataSource.read();

                //$('#ItemCategTreeview').data("kendoTreeView").setDataSource(dataSource);
            });
        };

        function getTreeData() {            
            return InventoryDataService.getItemCategTreeList().then(function (res) {
                return vm.thingsOptions = {
                    dataSource: res,
                    autoScroll: true,
                    select: function (e) {                                                                    
                        var dataItem = this.dataItem(e.node);
                        vm.selectedItem = dataItem;

                        getBrandMapList();
                        if (vm.ACTION_MODE == 1) {
                            reset();
                        }
                        else if (vm.ACTION_MODE == 2) {                            
                            vm.form = dataItem;                            
                        }

                        console.log(dataItem);
                        if (dataItem.PARENT_ID == 0 || dataItem.PARENT_ID == 1) {
                            
                        }
                    }
                };
            });
        };

        vm.actionModeSelection = function () {
            getBrandMapList();

            if (vm.ACTION_MODE == 1) {
                reset();                
            }
            else if (vm.ACTION_MODE == 2) {
                vm.form = vm.selectedItem;                
            }
        };

        $scope.items = [{}];
        $scope.selectedItems = [{}];
        function getBrandMapList() {
            $scope.selectedItems = [{}];
            return InventoryDataService.GetAllOthers('/api/common/GetItemBrandList').then(function (res) {
                //vm.nonEntitledDesigListData = res;
                //$scope.items = res;
                return InventoryDataService.GetAllOthers('/api/inv/ItemCategory/getCategWiseBrandList?pINV_ITEM_CAT_ID=' + vm.selectedItem.INV_ITEM_CAT_ID).then(function (res1) {
                    //vm.form.HR_DESIGNATION_ID_LIST = res;
                    $scope.selectedItems = res1;

                    //$scope.items = _.differenceBy(res, res1, 'BRAND_NAME_EN');// _.difference(res, res1);

                    angular.forEach(res, function (val, key) {
                        var itm = _.filter(res1, function (ob) {
                            if (val['BRAND_NAME_EN'] == ob.BRAND_NAME_EN) {
                                return ob;
                            }
                        });

                        //console.log(itm);
                        if (itm == null) {
                            $scope.items.push(val);
                        }
                    });

                    //console.log($scope.items);


                    $scope.demoOptions = {
                        title: '',
                        filterPlaceHolder: 'Start typing to filter the lists below.',
                        labelAll: 'Brand not yet assigned',
                        labelSelected: 'Brand assigned',
                        legendTitle: '',
                        helpMessage: 'Mapping',
                        showHelpMessage: false,                        
                        /* angular will use this to filter your lists */
                        orderProperty: 'BRAND_NAME_EN',
                        /* this contains the initial list of all items (i.e. the left side) */
                        items: $scope.items,
                        /* this list should be initialized as empty or with any pre-selected items */
                        selectedItems: $scope.selectedItems,
                        addButton: true,
                        addButtonCaption: 'New',
                        clickAdd: function () {
                            //alert(vm.selectedItem.INV_ITEM_CAT_ID);
                            var modalInstance = $modal.open({
                                animation: true,
                                templateUrl: '/GlobalUI/BrandEntry',
                                controller: function ($modalInstance, $scope, $timeout, pINV_ITEM_CAT_ID, InventoryDataService) {
                                    var vm = this;
                                    vm.form = {};
                                    vm.errors = [];
                                    BrandGetAll();
                                    function BrandGetAll() {                                        
                                        InventoryDataService.GetAllOthers('/api/common/GetItemBrandList').then(function (res) {
                                            //PurchaseDataService.getDataByFullUrl('/api/common/GetCategoryWiseBrandList/' + ((vm.brand.INV_ITEM_CAT_ID == '-- Select Item Category--' ? 0 : vm.brand.INV_ITEM_CAT_ID) || 0)).then(function (res) {
                                            vm.brandAllList = new kendo.data.DataSource({
                                                data: res
                                            });
                                        });
                                    };

                                    vm.gridBrand = {
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
                                        groupable: false,
                                        columns: [

                                            { field: "RF_BRAND_ID", hidden: true },
                                            { field: "BRAND_NAME_EN", title: "Brand Name (EN)", width: "30%" },
                                            { field: "BRAND_NAME_BN", title: "Brand Name (BN)", width: "30%" },
                                            { field: "IS_ACTIVE", title: "Is Active", width: "20%" },
                                            {
                                                title: "",
                                                template: '<a title="Edit" ng-click="vm.editBrand(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                                                width: "20%"
                                            }
                                        ],
                                        //editable: true

                                    };

                                    vm.form['BRAND_NAME_EN'] = '';
                                    vm.formDisabled = false;

                                    vm.cancel = function () {
                                        var data = vm.brandAllList.data();                                        
                                        $modalInstance.close(data);
                                    };
                                   
                                    vm.editBrand = function (data) {
                                        vm.form = angular.copy(data);
                                    }

                                    vm.removeBrand = function (data) {

                                        if (!data.RF_BRAND_ID || data.RF_BRAND_ID <= 0) {
                                            vm.brandAllList.remove(data);
                                            return;
                                        };

                                        Dialog.confirm('Removing "' + data.BRAND_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                                             .then(function () {
                                                 vm.brandAllList.remove(data);
                                             });

                                    }

                                    vm.resetBrandInfo = function () {
                                        vm.form = {};
                                    };

                                    vm.submitDataNewBrand = function (dataOri, token) {
                                        var data = angular.copy(dataOri);
                                        data['INV_ITEM_CAT_ID'] = pINV_ITEM_CAT_ID;
                                        console.log(data);
                                        //alert('ddd');
                                        //return;

                                        return InventoryDataService.saveDataByFullUrl(data, '/api/common/BrandSave', token).then(function (res) {
                                            if (res.success === false) {
                                                vm.errors = res.errors;
                                            }
                                            else {
                                                console.log(res);
                                                res['data'] = angular.fromJson(res.jsonStr);
                                                config.appToastMsg(res.data.PMSG);

                                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                                    vm.form['RF_BRAND_ID'] = parseInt(res.data.opRF_BRAND_ID);
                                                }
                                                BrandGetAll();
                                                //vm.form = {};
                                            }
                                        }).catch(function (message) {
                                            exception.catcher('XHR loading Failded')(message);
                                        });

                                    };
                                },
                                resolve: {
                                    pINV_ITEM_CAT_ID: function () {
                                        return vm.selectedItem.INV_ITEM_CAT_ID;
                                    }
                                },
                                controllerAs: 'vm',
                                size: 'lg',
                                windowClass: 'large-Modal',
                            });

                            modalInstance.result.then(function (data) {
                                console.log($scope.items);
                                $scope.demoOptions.items = data;
                                console.log($scope.items);
                            }, function () {
                                console.log('Modal dismissed at: ' + new Date());
                            });
                        }
                    };

                });
            });
            vm.isNext = true;
        };

        
        vm.submitData = function (data, token) {
            vm.errors = undefined;
            data.CREATED_BY = vm.CREATED_BY;
            if (vm.selectedItem.ITEM_CAT_PREFIX != null && vm.selectedItem.ITEM_CAT_PREFIX!="") {
                data.ITEM_CAT_PREFIX = vm.selectedItem.ITEM_CAT_PREFIX;
            }

            data.XML_STR = InventoryDataService.xmlStringShort($scope.selectedItems.map(function (ob) {
                return ob;
            }));
                       

            if (angular.isDefined(data.INV_ITEM_CAT_ID) && data.INV_ITEM_CAT_ID > 0) {               
                return InventoryDataService.updateItemCategoryData(data, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);                        
                        reset();
                        getCategData();
                        vm.selectedItem = { IS_LEAF: 'Y' };
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            } else {                
                //console.log(vm.selectedItem);
                data.PARENT_ID = vm.selectedItem.INV_ITEM_CAT_ID;

                return InventoryDataService.saveItemCategoryData(data, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        reset();
                        getCategData();
                        vm.selectedItem = { IS_LEAF: 'Y' };
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }

        }

        

    }

})();



///======== Start Item Category Permission Controller
(function () {
    'use strict';
    angular.module('multitex.inventory').controller('ItemCategoryPermissionController', ['$q', 'config', 'InventoryDataService', '$stateParams', '$state', '$scope', 'logger', ItemCategoryPermissionController]);
    function ItemCategoryPermissionController($q, config, InventoryDataService, $stateParams, $state, $scope, logger) {

        var vm = this;
        vm.errors = null;
        vm.Title = $state.current.Title || '';

        vm.ACTION_MODE = 1;
        vm.selectedItem = { IS_LEAF: 'Y' };
        vm.form = { IS_ACTIVE: 'Y', IS_LEAF: 'N' };

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

        vm.userSelect = function () {
            //alert('change module');
            //vm.isInsert = false;
            vm.IsChange = false;
        };

        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h3>#: data.USER_NAME_EN #</h3><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getUserDatalist().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function(e){
                    vm.IsChange = false;
                },
                height: 400,
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };

            //$("#customers").kendoDropDownList(vm.userList);
        }

        // function that gathers IDs of checked nodes
        var x = 0;
        function checkedNodeIds(nodes, checkedNodes) {
            vm.isInsert = true;

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


                roleMenuNode[x] = {
                    SC_USER_ID: vm.form.SC_USER_ID, INV_ITEM_CAT_ID: nodes[i].INV_ITEM_CAT_ID,
                    IsChecked: nodes[i].IsChecked, IS_ACTIVE: (nodes[i].IsChecked == true ? 'Y' : 'N')
                };
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

        vm.getCatTreeData = function (pSC_USER_ID) {
            
            return InventoryDataService.getItemCategTreePermissionList(pSC_USER_ID).then(function (res) {

                checkedNodes = [];
                console.log(res);
                if (res.length > 0) {
                    vm.IsChange = true;
                };

                return vm.menuOptions = {
                    checkboxes: {
                        checkChildren: true,
                        template: "<input type='checkbox' ng-model='dataItem.IsChecked' />"
                    },
                    dataSource: res,// [{ id: 1, text: "texxt1", items: [{ id: 2, text: "texxt2", items: [] }, { id: 3, text: "texxt3", items: [] }] }]
                    //select: function (e) {
                    //    alert('test');

                    //    var tv = $('#ItemCategTreeview').data('kendoTreeView'),
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

        //var dataSource
        //dataSource.read();
        function getCategData() {
            return InventoryDataService.getItemCategTreeList().then(function (res) {
                var dataSource = new kendo.data.HierarchicalDataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    },
                });

                var treeview = $("#ItemCategTreeview").data("kendoTreeView");
                var selectedNode = treeview.select();
                var item = treeview.dataItem(selectedNode);
                var bar = treeview.findByUid(item.uid);
                treeview.select(bar);
                //console.log(bar);

                treeview.setDataSource(new kendo.data.HierarchicalDataSource({
                    data: res
                }));

                
            });
        };

        function getTreeData() {
            return InventoryDataService.getItemCategTreeList().then(function (res) {
                return vm.thingsOptions = {
                    dataSource: res,
                    autoScroll: true,
                    select: function (e) {
                        var dataItem = this.dataItem(e.node);
                        vm.selectedItem = dataItem;

                        if (vm.ACTION_MODE == 1) {
                            reset();
                        }
                        else if (vm.ACTION_MODE == 2) {
                            vm.form = dataItem;
                        }
                    }
                };
            });
        };

        vm.actionModeSelection = function () {
            if (vm.ACTION_MODE == 1) {
                reset();
            }
            else if (vm.ACTION_MODE == 2) {
                vm.form = vm.selectedItem;
            }
        };

        vm.saveData = function (token) {
            vm.errors = [];

            if (vm.errors != undefined && vm.errors.length > 0) {
                return;
            }
            else {
                vm.errors = undefined;
            }

            console.log(roleMenuNode);           
            
            var obj = {};
            obj.XML_STR = InventoryDataService.xmlStringShort(roleMenuNode.map(function (ob) {
                return ob;
            }));
            
            return InventoryDataService.batchSaveItemCatPermissionData(obj, token).then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    //reset();
                    //getCategData();
                    //vm.selectedItem = { IS_LEAF: 'Y' };
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });


        }

    }

})();