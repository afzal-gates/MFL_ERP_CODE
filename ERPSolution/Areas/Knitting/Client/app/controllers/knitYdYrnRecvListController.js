(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitYdYrnRecvListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'Dialog', '$modal', '$window', KnitYdYrnRecvListController]);
    function KnitYdYrnRecvListController($q, config, KnittingDataService, $stateParams, $state, $scope, Dialog, $modal, $window) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.NewMode = $state.current.data.NewMode;
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
            $state.go($state.current.name, {}, { notify: false, inherit: false, reload: true })
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
            var filters = [];
            angular.forEach(data,function (val, key) {
                if (val && val.length > 0) {
                    filters.push({
                        field: key, operator: "contains", value: val
                    })
                }

            });

            if (filters.length>0) {
                vm.YdProgramHeaderDs.filter(filters);
            } else {
                vm.YdProgramHeaderDs.filter({});
            }
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

        vm.printDeliveryChallan = function (dataItem) {
            if (!dataItem.KNT_SC_PRG_ISS_ID) { return; }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = {
                REPORT_CODE: 'RPT-3582',
                KNT_YD_RCV_H_ID: dataItem.KNT_YD_RCV_H_ID,
                KNT_SC_PRG_ISS_ID: dataItem.KNT_SC_PRG_ISS_ID,
                IS_YD: 'Y'
            };

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);

            form.submit();

            document.body.removeChild(form);
        };
        vm.printScProgram = function (pKNT_SC_PRG_ISS_ID) {
            if (!pKNT_SC_PRG_ISS_ID) { return; }
            var url = '/Knitting/Knit/KntScProgYd/#/KntScProgYd?pKNT_SC_PRG_ISS_ID=' + pKNT_SC_PRG_ISS_ID
            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 300) + ',scrollbars=yes,status=yes';
            $window.open(url, "_blank", opt);
        }



       var col_transfer = [
                { field: "PRG_REF_NO", title: "Y/D Program #", width: "10%", filterable: false },
                { field: "CL_WO_REF_NO", title: "Working Ref# ", width: "10%", filterable: false },
                { field: "CHALAN_NO", title: "Challan", width: "10%", filterable: false },
                { field: "KNT_SC_FACTORY", title: "TransferTo", width: "10%", filterable: false },

                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", width: "10%", filterable: false },
                { field: "STYLE_NO", title: "Style", width: "10%", filterable: false },
                { field: "ORDER_NO", title: "Order", width: "10%", filterable: false },
                { field: "SUP_TRD_NAME_EN", title: "Supplier Name", width: "10%", filterable: false },
                { field: "CHALAN_DT", title: "Challan DT", width: "10%", format: "{0:dd-MMM-yyyy}" },
                { field: "EVENT_NAME", title: "Status", width: "10%", filterable: false, template: '#if (RF_ACTN_STATUS_ID==64) {# <span style=\"color:green;\"><b>#=EVENT_NAME #</b></span> #} else {# <span style=\"color:red;\"><b>#=EVENT_NAME #</b></span> #}#' },
                {
                    title: "Action",
                    template: "<a  class='btn btn-xs blue' ng-click='vm.navigate(dataItem)' title='Edit'> <i class=\"fa fa-pencil\"></i>Link</a> #if (KNT_SC_PRG_ISS_ID) {# <a  class='btn btn-xs green-haze' ng-click='vm.printDeliveryChallan(dataItem)' title='Delivery Challan'> <i class=\"fa fa-print\"></i>Del.Challan</a>  #}#",
                    width: "20%"
                }
        ]

        var col_no_transfer = [
        { field: "PRG_REF_NO", title: "Y/D Program #", width: "10%", filterable: false },
        { field: "CL_WO_REF_NO", title: "Working Ref# ", width: "10%", filterable: false },
        { field: "CHALAN_NO", title: "Challan", width: "10%", filterable: false },
        
        { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", width: "10%", filterable: false },
        { field: "STYLE_NO", title: "Style", width: "10%", filterable: false },
        { field: "ORDER_NO", title: "Order", width: "10%", filterable: false },
        { field: "SUP_TRD_NAME_EN", title: "Supplier Name", width: "10%", filterable: false },
        { field: "CHALAN_DT", title: "Challan DT", width: "10%", format: "{0:dd-MMM-yyyy}" },
        { field: "EVENT_NAME", title: "Status", width: "10%", filterable: false, template: '#if (RF_ACTN_STATUS_ID==64) {# <span style=\"color:green;\"><b>#=EVENT_NAME #</b></span> #} else if (RF_ACTN_STATUS_ID==63){# <span style=\"color:red;\"><b>#=EVENT_NAME #</b></span> #} else {# <span style=\"color:black;\"><b>#=EVENT_NAME #</b></span> #}#' },
        {
            title: "Action",
            template: function () {
                return "<a  class='btn btn-xs blue' ng-click='vm.navigate(dataItem)' title='Edit'> <i class=\"fa fa-pencil\"></i>Link</a>";
            },
            width: "10%"
        }
        ];

        console.log($state.current.data.NewMode);


        vm.YdProgramHeaderDs = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    fields: {
                        CHALAN_DT: { type: "date" }
                    }
                }
            },
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/KnitYdProgram/getYdYrnRecvList';
                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += config.kFilterStr2QueryParam(params.filter);
                    if ($state.current.data.NewMode===true) {
                        url += '&pIS_TRANSFER=' + $state.current.data.IS_TRANSFER;
                    }

                    url += '&pRF_ACTN_STATUS_ID=' + $state.current.data.RF_ACTN_STATUS_ID;

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

        vm.navigate = function (dataItem) {
            if (dataItem && dataItem.KNT_YD_RCV_H_ID && dataItem.IS_TRANSFER == 'N') {
                $state.go($state.current.data.stateTo, { pKNT_YD_RCV_H_ID: dataItem.KNT_YD_RCV_H_ID, ConMode: $state.current.data.ConMode, stateBack: $state.current.data.stateBack });
            } else if (dataItem && dataItem.KNT_YD_RCV_H_ID && dataItem.IS_TRANSFER == 'Y') {
                $state.go($state.current.data.stateTo, { pKNT_YD_RCV_H_ID: dataItem.KNT_YD_RCV_H_ID, ConMode: $state.current.data.ConMode, stateBack: $state.current.data.stateBack });
            } else {
                $state.go($state.current.data.stateTo, { ConMode: $state.current.data.ConMode, stateBack: $state.current.data.stateBack });
            }
        }

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
            columns: ( $state.current.data.IS_TRANSFER=='N' )? col_no_transfer : col_transfer
        };







    }

})();