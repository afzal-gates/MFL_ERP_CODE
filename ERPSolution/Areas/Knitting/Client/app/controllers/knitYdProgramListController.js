(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitYdProgramListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'Dialog', '$modal', KnitYdProgramListController]);
    function KnitYdProgramListController($q, config, KnittingDataService, $stateParams, $state, $scope, Dialog, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.form = {};

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function remQueryParam() {
            $state.go('KnitYdProgramList', {}, { notify: false, inherit: false, reload:true })
        }


        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.STYLE_NO||"" #</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.STYLE_NO||"" #)</h5></span>';

        $scope.FabOederByOhDs = {
            transport: {
                read: function (e) {
                    vm.showSplash = true;
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);

                    var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh";
                    url += "?pMC_BYR_ACC_ID=" + (vm.form.MC_BYR_ACC_ID || null);

                    if (params.filter) {
                        url += '&pORDER_NO_LST=' + params.filter.replace(/'/g, '').split('~')[2];
                    } else {
                        url += '&pORDER_NO_LST';
                    }

                    url += '&pIS_YD_ONLY=Y';
                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                        vm.showSplash = false;
                    });
                }
            },
            serverFiltering: true
        };

        $scope.buyerAcList = {
            optionLabel: "--- Buyer A/C ---",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            //dataBound: function (e) {
            //    var ds = this.dataSource.data();
            //    if (ds.length == 1) {
            //        this.value(ds[0].MC_BYR_ACC_ID);
            //        vm.form['MC_BYR_ACC_ID'] = ds[0].MC_BYR_ACC_ID;
            //    }
            //},
            change: function (e) {
                var item = this.dataItem(e.item);
                if (item.MC_BYR_ACC_ID) {
                    var multiselect = $("#ORDER_LIST").data("kendoDropDownList");
                    multiselect.dataSource.read();
                }
            },
            dataTextField: "BYR_ACC_NAME_EN",
            dataValueField: "MC_BYR_ACC_ID"
        };

        vm.onSearch = function (data) {
            var filter = [];
            angular.forEach(data,function (val,key) {
                if (val && val.length > 0) {
                    filter.push({
                        field: key, operator: "contains", value: val
                    })
                }
            });

            vm.YdProgramHeaderDs.filter(filter);
        };

        vm.supplierListDs = {
            transport: {
                read: function (e) {
                    return KnittingDataService.getDataByFullUrl('/api/purchase/SupplierProfile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=556').then(function (res) {
                        e.success(res);
                    });
                }

            }
        };

        vm.YdProgramHeaderDs = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    fields: {
                        PRG_ISS_DT: { type: "date" },
                        DELV_TGT_DT: { type: "date" }
                    }
                }
            },
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/KnitYdProgram/getProgamList';
                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += config.kFilterStr2QueryParam(params.filter);
                    if (params.filter.length == 0) {
                        remQueryParam();
                    }
                    KnittingDataService.getDataByUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            pageSize: 10
        });



        vm.YdProgramHeaderOpt = {
            sortable: true,
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
            scrollable: true,
            pageable:true,
            columns: [
                { field: "PRG_REF_NO", title: "Y/D Program #", width: "10%", filterable: false },
                { field: "FAB_PROD_CAT_SNAME", title: "Type", width: "10%", filterable: false },
                { field: "REVISION_NO", title: "Rev#", width: "5%", filterable: false },

                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", width: "10%", filterable: false },
                { field: "ORDER_NO", title: "Order (Style)", width: "20%", filterable: false },

                { field: "SUP_TRD_NAME_EN", title: "Supplier Name", width: "20%", filterable: false },
                { field: "PRG_ISS_DT", title: "Issue", width: "10%", format: "{0:dd-MMM-yyyy}" },
                { field: "DELV_TGT_DT", title: "Delivery", width: "10%", format: "{0:dd-MMM-yyyy}" },
                
                {
                    title: "Action",
                    template: function () {
                        return "<a  class='btn btn-xs blue' ui-sref='KnitYdProgram({pKNT_YD_PRG_H_ID:dataItem.KNT_YD_PRG_H_ID})' title='Edit'> <i class=\"fa fa-pencil\"></i>View/Edit</a>";
                    },
                    width: "10%"
                },
            ]
        };







    }

})();