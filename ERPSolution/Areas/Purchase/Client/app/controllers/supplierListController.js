(function () {
    'use strict';
    angular.module('multitex.purchase').controller('SupplierListController', ['$q', 'config', 'PurchaseDataService', '$stateParams', '$state', '$scope', SupplierListController]);
    function SupplierListController($q, config, PurchaseDataService, $stateParams, $state, $scope) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getSupplierList() {
            PurchaseDataService.getDataByFullUrl('/api/purchase/SupplierProfile/SelectAll').then(function (res) {
                vm.supplierList = new kendo.data.DataSource({
                    data: res,
                    pageSize: 10
                });
            }, function (err) {
                console.log(err);
            });

        }

        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        PurchaseDataService.getDataByFullUrl('/api/purchase/SupplierProfile/SelectAll').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 10
            },
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
            selectable: "row",
            sortable: true,
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "SUP_CODE", title: "Supplier Code", type: "string", width: "15%" },
                { field: "SUP_TRD_NAME_EN", title: "Supplier Name", type: "string", width: "20%" },
                //{ field: "SUP_OWNER_NAME", title: "Owner Name", type: "string", width: "50px" },
                { field: "SUP_SNAME", title: "Short Name", type: "string", width: "10%" },
                { field: "ADDRESS_DEFA", title: "Office Address", type: "string", width: "15%" },
                { field: "SUPPLIER_GROUP", title: "Supplier Group", type: "string", width: "15%", filterable: { ui: FilterOptionSuppGroup}},
                
                { field: "INV_ITEM_CAT_ID_LST", title: "Goods/Service", type: "string", width: "15%" },
                
                {
                    title: "Action",
                    template: function () {
                        return "</a><a ui-sref='SupplierMaster({pSCM_SUPPLIER_ID:dataItem.SCM_SUPPLIER_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a></a>";
                    },
                    width: "10%"
                }
            ]
        };
        function FilterOptionSuppName(element) {

            var result = [];
            vm.supplierList.data().forEach(function (x) { if (!result.includes(x.SUPPLIER_GROUP)) result.push(x.SUPPLIER_GROUP) })

            element.kendoDropDownList({
                dataSource: result,
                optionLabel: "--Select Name--"
            })
        }

        function FilterOptionSuppGroup(element) {

            var result = [];
            vm.supplierList.data().forEach(function (x) { if (!result.includes(x.SUPPLIER_GROUP)) result.push(x.SUPPLIER_GROUP) })

            element.kendoDropDownList({
                dataSource: result,
                optionLabel: "--Select Group--"
            })
        }
    }

})();