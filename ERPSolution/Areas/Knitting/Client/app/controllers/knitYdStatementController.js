(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitYdStatementController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'Dialog', '$modal', '$window', KnitYdStatementController]);
    function KnitYdStatementController($q, config, KnittingDataService, $stateParams, $state, $scope, Dialog, $modal, $window) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.form = {};

        var page, pageSize;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getOrderShipmentMonth()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function getOrderShipmentMonth() {
            return KnittingDataService.getDataByFullUrl('/api/mrc/Order/OrderShipmentMonth?pMC_BYR_ACC_ID').then(function (res) {
                vm.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        }

        $scope.KntYdProgramDs = {
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);

                    var url = "/KnitYdProgram/getYdProgramDropDownData";

                    if (params.filter) {
                        url += '?pPRG_REF_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                    } else {
                        url += '?pPRG_REF_NO';
                        url += '&pKNT_YD_PRG_H_ID=' + (Object.keys(FormData).length > 0 ? FormData.KNT_YD_PRG_H_ID : '');
                    }

                    return KnittingDataService.getDataByUrl(url).then(function (res) {
                        e.success(res);

                    });
                }
            },
            serverFiltering: true
        };




        vm.onChangeShipMonth = function (data, e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.hasOwnProperty('MONTHOF') && item.MONTHOF) {
                data['FIRSTDATE'] = item.FIRSTDATE;
                data['LASTDATE'] = item.LASTDATE;
                data['MONTHOF'] = item.MONTHOF;

                data['FROM_DATE'] = item.FIRSTDATE;
                data['TO_DATE'] = item.LASTDATE;

            } else {

                data['FIRSTDATE'] = null;
                data['LASTDATE'] = null;
                data['MONTHOF'] = null;

                data['FROM_DATE'] = null;
                data['TO_DATE'] = null;
            }
        };



        $scope.productionTypeList = {
            optionLabel: "-- Select Type --",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/GetFabProdCat').then(function (res) {
                            e.success(_.findByValues(res, 'RF_FAB_PROD_CAT_ID', [1,2,3,5,6]));
                        });
                    }
                }
            },
            dataTextField: "FAB_PROD_CAT_SNAME",
            dataValueField: "RF_FAB_PROD_CAT_ID"
        };

        function remQueryParam() {
            $state.go('KnitYdProgramList', {}, { notify: false, inherit: false, reload: true })
        }

        vm.donePctDs = new kendo.data.DataSource({
            data: [
                {
                 LABEL: 'Not Received', CODE:'NR'
                },
                                {
                                    LABEL: 'Partial Received', CODE: 'PR'
                                },
                                                {
                                                    LABEL: 'Full Received', CODE: 'FR'
                                                }
               
            ]
        });


        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.STYLE_NO||"" #</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.STYLE_NO||"" #)</h5></span>';

        $scope.FabOederByOhDs =  new kendo.data.DataSource({
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

                    if (vm.form.RF_FAB_PROD_CAT_ID > 1) {
                        url += '&pRF_FAB_PROD_CAT_ID=' + vm.form.RF_FAB_PROD_CAT_ID
                    };

                    if (vm.form.FIRSTDATE) {
                        url += '&pFIRSTDATE=' + vm.form.FIRSTDATE + '&pLASTDATE=' + vm.form.LASTDATE;
                    };

                    url += '&pIS_YD_ONLY=Y';
                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                        vm.showSplash = false;
                    });
                }
            },
            serverFiltering: true
        });

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
            //change : function(e){
            //    $scope.FabOederByOhDs.read();
            //},
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
                    var url = '/KnitYdProgram/getKntStatementData';
                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;

                    
                    page = params.page;
                    pageSize = params.pageSize;

                    url += config.kFilterStr2QueryParam(params.filter);
                    

                    KnittingDataService.getDataByUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            filter: {
                field: 'RF_FAB_PROD_CAT_ID', operator: "contains", value: 2
            },
            pageSize: 50
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
            height: 500,
            scrollable: true,
            pageable:true,
            columns: [
                {
                    field: "BYR_ACC_NAME_EN", title: "Buyer A/C", width: "120px", filterable: false, locked: true, template: '# if (ORDH_ID_SL == 1 ) { #  <small> #=BYR_ACC_NAME_EN# </small> # } #',

                },

                { field: "STYLE_NO", title: "Style No.", width: "150px", filterable: false, locked: true, template: '# if (ORDH_ID_SL == 1 ) { #  <small> #=STYLE_NO# </small>  # } #' },
                { field: "MC_ORDER_NO_LST", title: "Order No.", width: "150px", filterable: false, locked: true, template: '# if (ORDH_ID_SL == 1 ) { # <small>  #=MC_ORDER_NO_LST# </small>  # } #' },
                { field: "SHIP_DT", title: "Shipment", width: "100px", filterable: false, locked: true, template: '# if (ORDH_ID_SL == 1 ) { # <small>  #=  kendo.toString(kendo.parseDate(SHIP_DT,"yyyy-MM-dd"),"dd-MMM-yyyy")# </small>  # } #' },
                { field: "PROD_ORD_DT", title: "Booking Rev Date", width: "80px", filterable: false, locked: true, template: '# if (ORDH_ID_SL == 1 ) { # <small>  #=kendo.toString(kendo.parseDate(PROD_ORD_DT,"yyyy-MM-dd"),"dd-MMM-yyyy")# </small>  # } #' },

                { field: "SUP_TRD_NAME_EN", title: "Supplier", width: "80px", filterable: false, locked: true, template: '# if (ORDH_ID_SUP_SL == 1 ) { # <small>  #=SUP_TRD_NAME_EN# </small>  # } #' },
                
                { field: "PRG_REF_NO", title: "Prog. Ref", width: "100px", filterable: false, template: '# if (ORDH_ID_PROG_SL == 1 ) { # <small>  #=PRG_REF_NO# </small>  # } #' },
                { field: "PRG_ISS_DT", title: "Prog. Sent Date", width: "100px", filterable: false, template: '# if (ORDH_ID_PROG_SL == 1 ) { # <small>  #=kendo.toString(kendo.parseDate(PRG_ISS_DT,"yyyy-MM-dd"),"dd-MMM-yyyy")# </small>  # } #' },

               
                { field: "YD_COLOR_NAME", title: "Color", width: "100px", filterable: false, template: '<small> #=YD_COLOR_NAME# </small>' },

                { title: 'Yarn Desc.', field: "PROD_ORD_DT", width: "200px", template: ' # for (var i = 0; i < data.yarns.length; i++) { # <span style=\"display:block;\"><small><small> #=data.yarns[i].YARN # </small></span> </span> # } #' },
                
                { field: "RQD_YD_QTY", title: "Issued", width: "80px", filterable: false, template: '<small> #=RQD_YD_QTY# Kg </small>' },
                { field: "RCV_YD_QTY", title: "Recv", width: "80px", filterable: false, template: '<small> #=RCV_YD_QTY# Kg</small>' },
                { field: "RCV_PACK_QTY", title: "Bag", width: "80px", filterable: false, template: '<small> #=RCV_PACK_QTY# #=PACK_MOU# </small>' },
                { field: "YD_BATCH_NO", title: "Batch#", width: "80px", filterable: false, template: '<small> #=YD_BATCH_NO# </small>' },
                { field: "CL_WO_REF_NO", title: "Work Order#", width: "80px", filterable: false, template: '# if (ORDH_ID_RECV_SL == 1 ) { # <small>  #=CL_WO_REF_NO# </small>  # } #' },
                { field: "CHALAN_DT", title: "Recv Date", width: "80px", filterable: false, template: '# if (ORDH_ID_RECV_SL == 1 ) { # <small>  #=kendo.toString(kendo.parseDate(CHALAN_DT,"yyyy-MM-dd"),"dd-MMM-yyyy")# </small>  # } #' }
            ],
            dataBound: function (e) {
                var rows = e.sender.tbody.children();
                for (var j = 0; j < rows.length; j++) {
                    var row = $(rows[j]);
                    var dataItem = e.sender.dataItem(row);
                    var benchmark = 96;

                    if (dataItem.YRN_COLOR_ID == 181) {
                        benchmark = 95;
                    }
                    if ((dataItem.get("RCV_YD_QTY") / (dataItem.get("RQD_YD_QTY") || 1)) * 100 > 0 && (dataItem.get("RCV_YD_QTY") / (dataItem.get("RQD_YD_QTY") || 1)) * 100 < benchmark) {
                        row.addClass("bg-y");
                    }

                    if ((dataItem.get("RCV_YD_QTY") / (dataItem.get("RQD_YD_QTY") || 1)) * 100 > benchmark) {
                        row.addClass("bg-g");
                    }
                }
            }
        };

        vm.onPrint = function (data) {
            var url = '/Knitting/Knit/KnitYdStatementRpt?a=279/#/YdStatementRpt';

            url += '?pageNo=' + page + '&pageSize=' + pageSize;

            url += '&pRF_FAB_PROD_CAT_ID=' + (data.RF_FAB_PROD_CAT_ID || null);
            url += '&pSCM_SUPPLIER_ID=' + (data.SCM_SUPPLIER_ID || null);
            url += '&pMC_FAB_PROD_ORD_H_ID=' + (data.MC_FAB_PROD_ORD_H_ID || null);
            url += '&pMC_BYR_ACC_ID=' + (data.MC_BYR_ACC_ID || null);

            url += '&pPCT_DONE_CODE=' + (data.PCT_DONE_CODE || null);

            if (data.FIRSTDATE && data.LASTDATE) {
                url += '&pFIRSTDATE=' + data.FIRSTDATE;
                url += '&pLASTDATE=' + data.LASTDATE;
                url += '&pMONTHOF=' + data.MONTHOF;
            }

            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 30) + ',scrollbars=yes,status=yes';
            var win = $window.open(url, "newwindow", opt);
            win.focus();

        };

        vm.printYdStatement = function () {
            //console.log(dataItem);

            vm.form.REPORT_CODE = 'RPT-3570';
            

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = vm.form;

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





    }

})();