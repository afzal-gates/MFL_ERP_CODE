(function () {
    'use strict';

    var items;
    var modalInstance;

    angular.module('multitex.security').controller('ScRoleMenuController', ['logger', 'config', '$q', '$scope', '$http', '$modal', 'SecurityService', ScRoleMenuController]);

    function ScRoleMenuController(logger, config, $q, $scope, $http, $modal, SecurityService) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;        

        activate();

        vm.treeSelecteditem = {};
        vm.isInsert = false;
        vm.IsChange = false;


        vm.TranCancel = function (myForm) {
            vm.isInsert = false;
            vm.IsChange = false;

            vm.getMenuTreeData(myForm);
            
            //vm.IsChange = true;
        };

       

        vm.submit = function (isValid, isInsert, myForm) {
            
            console.log(roleMenuNode);
            //return;

            if (!isValid) return;
            if (checkedNodes.length < 1) return;

            //alert('ok');
            //console.log(checkedNodes[0]);
            //console.log(roleMenuNode);

            //submit the data to the server
            if (isInsert) {
                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Security/ScRoleMenu/Save',
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


        vm.ModuleSelect = function () {
            //alert('change module');
            vm.isInsert = false;
            vm.IsChange = false;
        };
        
        vm.SC_SYSTEM_MODULE_ID = 0;
        vm.ModuleListData = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Security/Menu/ScSubModuleData"  //+ "&pType=" + showType
                        }).
                        success(function (data, status, headers, config) {
                            e.success(data)
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                    }
                }
            },
            select: function (e) {
                var dataItem = this.dataItem(e.item);
                if (dataItem.SC_SYSTEM_MODULE_ID > 0) {
                    vm.SC_SYSTEM_MODULE_ID = dataItem.SC_SYSTEM_MODULE_ID;
                }
                else {
                    vm.SC_SYSTEM_MODULE_ID = 0;
                }
            },
            dataTextField: "SYS_MODULE_NAME",
            dataValueField: "SC_SYSTEM_MODULE_ID"
        };

        vm.RoleSelect = function () {
            //alert('change role');
            vm.isInsert = false;
            vm.IsChange = false;
            
        };

        vm.SC_ROLE_ID = 0;
        vm.RoleListData = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Security/ScRole/RoleListData"  //+ "&pType=" + showType
                        }).
                        success(function (data, status, headers, config) {
                            e.success(data);                        
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                    }
                }
            },
            select: function(e){
                var dataItem = this.dataItem(e.item);
                if (dataItem.SC_ROLE_ID>0){
                    vm.SC_ROLE_ID = dataItem.SC_ROLE_ID;
                }
                else{
                    vm.SC_ROLE_ID = 0;
                }
            },
            dataTextField: "ROLE_NAME_EN",
            dataValueField: "SC_ROLE_ID"
        };




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


                roleMenuNode[x] = { SC_ROLE_ID: vm.myForm.SC_ROLE_ID, SC_MENU_ID: nodes[i].SC_MENU_ID, IsChecked: nodes[i].IsChecked };
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
            var treeView = $("#menuTreeview").data("kendoTreeView"),
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


        vm.reportGrid = function () {

            var dataSource = new kendo.data.DataSource({
                batch: true,
                transport: {
                    read: function (e) {
                        SecurityService.getReportListData(vm.treeSelecteditem.SC_MENU_ID).then(function (res) {
                            e.success(res.data);
                        });
                    },                    
                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return { models: kendo.stringify(options.models) };
                        }
                    }
                },
                pageSize: 10,
                schema: {
                    model: {
                        id: "RF_REPORT_ID",
                        fields: {
                            RPT_CODE: { type: "string", editable: false },
                            RPT_NAME_EN: { type: "string", editable: false },
                            IS_ACTIVE: { type: "string", editable: false }
                        }
                    }
                }
            });


            console.log(dataSource);

            $("#reportGrid").kendoGrid({
                dataSource: dataSource,
                navigatable: true,
                pageable: {
                    refresh: true,
                    pageSizes: true
                },
                //height: 550,
                //toolbar: ["save", "cancel"],
                columns: [
                    { field: "RPT_CODE", title: "Code", type: "string", width: "120px" },
                    { field: "RPT_NAME_EN", title: "Name", type: "string", width: "200px" }                    
                ]
                //editable: {
                //    confirmation: false, //"Do you want to remove this record?",
                //    mode: "inline"
                //}
            });
            //    }
            //});
        };
       

        vm.getMenuTreeData = function (myForm) {
            
            if (myForm.SC_ROLE_ID == '' || myForm.SC_ROLE_ID == null) {
                alert('Please select role.');
                return;            
            }
            else if (myForm.SC_SYSTEM_MODULE_ID == '' || myForm.SC_SYSTEM_MODULE_ID == null) {
                alert('Please select module.');
                return;
            };

            return $http.post('/Security/ScRoleMenu/ScRoleMenuData', myForm).then(function (res) {
                             
                checkedNodes = [];
                //console.log(res.data.length);
                if (res.data.length > 0) {                    
                    vm.IsChange = true;
                };

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

        

        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;

                //vm.myForm = { SC_ROLE_ID: 0, SC_SYSTEM_MODULE_ID: 0 };
            });
        }

        //function getData(){
        //     return AdminService.getData().then(function (data) {
        //         //vm.schedulerOptions.dataSource.data = data;
        //         //return vm.schedulerOptions.dataSource.data;

        //    });
        //}

        //vm.styleModelIndex = -1;
        $scope.rptPermissionModal = function () {
            
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'RptPermissionModal.html',
                controller: 'RptPermissionModalController',
                size: 'large',
                //scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    rptGrpDataList: function (SecurityService) {
                        return SecurityService.GetAllOthers('/Security/ScRoleMenu/getRptGrpDataList').then(function (res) {
                            return res;
                        }, function (err) {
                            console.log(err);
                        });
                        //if (angular.isDefined(pSC_ROLE_ID) && pSC_ROLE_ID > 0) {
                        //    return SecurityService.GetAllOthers('/ScRoleMenu/getRptGrpListData').then(function (res) {
                        //        return res;
                        //    }, function (err) {
                        //        console.log(err);
                        //    });
                        //}
                        //else {
                        //    return {};
                        //}
                    },
                    roleID: function () {
                        return vm.myForm.SC_ROLE_ID;
                    }
                }
                
            });

            modalInstance.result.then(function (data) {
                console.log(data);
                //var vGmtParts = angular.copy(data);
                //angular.forEach(vm.fabBookingData[vm.styleModelIndex].STYLE_COLORS, function (val, key) {
                //    angular.forEach(vGmtParts, function (val1, key1) {
                //        console.log(val);
                //        console.log(val1);
                //        val['GMT_PARTS_QTY'].push({
                //            MC_SMP_REQ_D2_ID: 0, FIN_DIA: val1.FIN_DIA, DIA_MOU_ID: val1.DIA_MOU_ID, MC_STYLE_D_FAB_ID: val1.MC_STYLE_D_FAB_ID,
                //            RF_GARM_PART_LST: val1.RF_GARM_PART_LST, LK_DIA_TYPE_ID: val1.LK_DIA_TYPE_ID,
                //            FIN_DIA_DESC: val1.FIN_DIA_DESC, RF_GARM_PART_NAME_LST: val1.RF_GARM_PART_NAME_LST, FAB_TYPE_SNAME: val1.FAB_TYPE_SNAME
                //        });
                //    });
                //});

                //console.log(vm.fabBookingData);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };



    };

    

})();






// Start Report Permission Model Controller
(function () {
    'use strict';

    angular.module('multitex').controller('RptPermissionModalController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'rptGrpDataList', 'roleID', 'SecurityService', RptPermissionModalController]);
    function RptPermissionModalController($modalInstance, $q, $scope, $http, $state, $filter, config, rptGrpDataList, roleID, SecurityService) {

        //var vm = this;
        
        $scope.showSplash = true;
        //$scope.rptGrpDataList = angular.copy(rptGrpDataList);
       
        $scope.rptGrpDataList = new kendo.data.DataSource({
            data: rptGrpDataList
        });

        //$scope.plDataList = new kendo.data.ObservableArray([]);
        console.log(rptGrpDataList[0]);

        //$("button").click(function () {
        //    var value = $("#city").val(),
        //        view = grid.dataSource.view();

        //    var rows = $.grep(view, function (item) {
        //        return item.ShipCity === value;
        //    }).map(function (item) {
        //        return grid.tbody.find("[data-uid=" + item.uid + "]");
        //    });

        //    grid.select(rows);
        //});

        //$scope.gmtPartDiaDataList = new kendo.data.ObservableArray([]);
        //$scope.gmtPartDiaDataList = new kendo.data.DataSource({
        //    data: res
        //});

        activate();

        $scope.dtFormat = config.appDateFormat;
        $scope.formInvalid = false;


        function activate() {
            var promise = [selectRptGrpGrid()];
            return $q.all(promise).then(function () {                
                $scope.showSplash = false;
            });
        };

        function selectRptGrpGrid() {
            if (rptGrpDataList.length > 0) {
                $scope.selectedGrp = rptGrpDataList[0];

                return $http({
                    method: 'get',
                    url: "/Security/ScRoleMenu/getRoleRptDataList",
                    params: { pRF_REPORT_GRP_ID: rptGrpDataList[0].RF_REPORT_GRP_ID, pSC_ROLE_ID: roleID }
                }).
                success(function (data, status, headers, config) {
                    $scope.rptDataList = new kendo.data.DataSource({
                        data: data
                    });
                }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });
            }

            //var grid = $("#rptGrpGrid").data("kendoGrid");
            //grid.select("tr:eq(1)");
        };

        $scope.gridFilterable = {
            extra: false,
            operators: {
                string: {
                    contains: "Contains",
                    startswith: "Starts With",
                    eq: "Is Equal To"
                }
            }
        };

        $scope.rptGrpColumns = [
            { field: "RF_REPORT_GRP_ID", title: "GMT Part", type: "number", hidden: true },
            { field: "SC_MENU_ID", title: "Dia", type: "number", hidden: true },
            { field: "RPT_GRP_NAME_EN", title: "Report Group", type: "string" }
        ];

        $scope.rptGrpOnChange = function (data) {
            $scope.selectedGrp = data;

            return $http({
                method: 'get',
                url: "/Security/ScRoleMenu/getRoleRptDataList",
                params: { pRF_REPORT_GRP_ID: data.RF_REPORT_GRP_ID, pSC_ROLE_ID: roleID }
            }).
            success(function (data, status, headers, config) {                
                $scope.rptDataList = new kendo.data.DataSource({
                    data: data
                });
            }).
            error(function (data, status, headers, config) {
                alert('something went wrong')
                console.log(status);
            });                   
        }

        $scope.rptColumns = [
            { field: "SC_ROLE_REPORT_ID", title: "SC_ROLE_REPORT_ID", type: "number", hidden: true },
            { field: "RF_REPORT_ID", title: "RF_REPORT_ID", type: "number", hidden: true },
            { field: "RF_REPORT_GRP_ID", title: "RF_REPORT_GRP_ID", type: "number", hidden: true },
            { field: "RPT_NAME_EN", title: "Report Name", type: "string", width: '200px' },
            {
                headerTemplate: "<input type='checkbox' ng-model='checked' ng-change='selectAll(checked)'  ng-true-value='\"Y\"' ng-false-value='\"N\"'> Assign?",
                template: function () {
                    return "<input type='checkbox' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-change='vm.changeActive(dataItem)'>";
                },
                width: "40px"
            }
        ];

        $scope.selectAll = function (checked) {
            //rptDataList            
            var data = $('#kendoGrid').data("kendoGrid").dataSource.data().map(function (obj) {
                obj.IS_ACTIVE = checked;
                
                return obj;
            });
        };

        //$scope.changeActive = function (dataItem) {
        //    if (dataItem.IS_ACTIVE == 'Y' && !dataItem.LK_SMPL_SRC_TYPE_ID) {
        //        $scope.BuyerSampleForm[dataItem.uid].$setValidity('required', false);
        //    } else {
        //        $scope.BuyerSampleForm[dataItem.uid].$setValidity('required', true);
        //    }
        //}

  
        $scope.save = function () {
           
            var obj = {};
            obj.ROLE_RPT_XML = SecurityService.xmlStringShort($scope.rptDataList._data.map(function (ob) {
                return { SC_ROLE_REPORT_ID: ob.SC_ROLE_REPORT_ID, SC_ROLE_ID: ob.SC_ROLE_ID, RF_REPORT_ID: ob.RF_REPORT_ID, IS_ACTIVE: ob.IS_ACTIVE };
            }));

            $http({
                headers: { "RequestVerificationToken": $scope.antiForgeryToken },
                url: '/Security/ScRoleMenu/SaveRoleReport',
                method: 'post',
                data: obj
            }).success(function (data, status, headers, config1) {
                $scope.errors = undefined;
                if (data.success === false) {
                    $scope.errors = data.errors;
                }
                else {
                    data['data'] = angular.fromJson(data.jsonStr);

                    if (data.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        //$scope.selectedGrp

                        $http({
                            method: 'get',
                            url: "/Security/ScRoleMenu/getRoleRptDataList",
                            params: { pRF_REPORT_GRP_ID: $scope.selectedGrp.RF_REPORT_GRP_ID, pSC_ROLE_ID: roleID }
                        }).
                        success(function (data, status, headers, config) {
                            $scope.rptDataList = new kendo.data.DataSource({
                                data: data
                            });
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                    }

                    console.log(data);
                    config.appToastMsg(data.data.PMSG);
                    //$scope.isSaved = true;
                    //$scope.cancel();
                }
            }).error(function (data, status, headers, config) {
                alert('something went wrong')
                console.log(status);
            });
                    
            //$modalInstance.close(data);
        };


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        //$scope.generate = function () {
        //    $modalInstance.close($scope.gmtPartDiaDataList);
        //};
    }

})();
// End Report Permission Model Controller