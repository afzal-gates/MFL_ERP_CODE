(function () {
    'use strict';
    angular.module('multitex.knitting').controller('ScProgramListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'Dialog', '$modal', '$window', 'access_token', '$http', ScProgramListController]);
    function ScProgramListController($q, config, KnittingDataService, $stateParams, $state, $scope, Dialog, $modal, $window, access_token, $http) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        vm.Title += ($state.current.data.IS_YD === 'Y') ? ' (Y/D)' : ' (Solid)';
        vm.errors = null;

        vm.scProgState = $state.current.data.SC_PROG_STATE;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getbuyerAcList(), getProdCategory(), getSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        


        vm.getScProgramHeaderList = function (pRF_FAB_PROD_CAT_ID, pSCM_SUPPLIER_ID, pMC_BYR_ACC_ID, pMC_STYLE_H_EXT_ID, pPRG_ISS_NO) {
            vm.ScProgramHeaderDs = new kendo.data.DataSource({
                    serverPaging: true,
                    serverFiltering: true,
                    schema: {
                        data: "data",
                        total: "total",
                        model: {
                            fields: {
                                SC_PRG_DT: { type: "date" },
                            }
                        }
                    },
                    transport: {
                        read: function (e) {
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);
                            var url = '/KnitPlan/ScProgramPagingData?pMC_BYR_ACC_ID=' + (pMC_BYR_ACC_ID||null);
                            url += '&pRF_FAB_PROD_CAT_ID=' + (pRF_FAB_PROD_CAT_ID || null);
                            url += '&pSCM_SUPPLIER_ID=' + (pSCM_SUPPLIER_ID || null);
                            url += '&pMC_STYLE_H_EXT_ID=' + (pMC_STYLE_H_EXT_ID || null);
                            url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                            url += '&pIS_YD=' + $state.current.data.IS_YD;
                            url += '&pRF_FAB_PROD_CAT_ID_LST=' + $state.current.data.RF_FAB_PROD_CAT_ID_LST;
                            url += '&pPRG_ISS_NO=' + (pPRG_ISS_NO||'')
                            KnittingDataService.getDataByUrl(url).then(function (res) {
                                e.success(res);
                            })
                        }
                    },
                    pageSize: 10
                });
        }

        function getStyleExtList(pMC_BYR_ACC_ID, pMC_BUYER_ID) {
            if (pMC_BYR_ACC_ID) {
                return KnittingDataService.getDataByFullUrl('/api/mrc/StyleHExt/BuyerWiseStyleHExtList/' + pMC_BYR_ACC_ID + '/' + pMC_BUYER_ID).then(function (res) {
                    vm.StyleExtDs = new kendo.data.DataSource({
                        data: res
                    });
                });
            }

        };


        function getBuyerList(pMC_BYR_ACC_ID) {
            KnittingDataService.getDataByFullUrl('/api/mrc/buyer/getBuyerDropdownList?MC_BYR_ACC_ID=' + pMC_BYR_ACC_ID).then(function (res) {
                vm.BuyerListDs = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        vm.buyerListOnBound = function (e) {
            var ds = e.sender.dataSource.data();
            if (ds.length == 1) {
                e.sender.value(ds[0].MC_BUYER_ID);
                vm.form['MC_BUYER_ID'] = ds[0].MC_BUYER_ID;
                getStyleExtList(null, ds[0].MC_BUYER_ID);
            } else {
                vm.form['MC_BUYER_ID'] = null;
                e.sender.value(null);
            }
        }

        vm.buyerListOnChange = function (e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.hasOwnProperty('MC_BUYER_ID')) {
                getStyleExtList(null, item.MC_BUYER_ID)
            }
        }

        function getbuyerAcList() {
            return vm.buyerAcList = {
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
                dataBound: function (e) {
                    var ds = this.dataSource.data();
                    if (ds.length == 1) {
                        this.value(ds[0].MC_BYR_ACC_ID);
                        vm.form['MC_BYR_ACC_ID'] = ds[0].MC_BYR_ACC_ID;
                        getBuyerList(ds[0].MC_BYR_ACC_ID);
                        getStyleExtList(ds[0].MC_BYR_ACC_ID, null);
                    }
                },
                change: function (e) {
                    var item = this.dataItem(e.item);
                    if (item.MC_BYR_ACC_ID && item.MC_BYR_ACC_ID > 0) {
                        //getBuyerList(item.MC_BYR_ACC_ID);
                        getStyleExtList(item.MC_BYR_ACC_ID, null);
                    } else {
                        vm.BuyerListDs = new kendo.data.DataSource({
                            data: []
                        });
                        vm.StyleExtDs = new kendo.data.DataSource({
                            data: []
                        });
                    };
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }

        vm.onProdCategoryDataBound = function (e) {
            var ds = e.sender.dataSource.data();
            if (ds.length == 1) {
                e.sender.value(ds[0].RF_FAB_PROD_CAT_ID);
                vm.form.RF_FAB_PROD_CAT_ID = ds[0].RF_FAB_PROD_CAT_ID;
            }            
        };

        function getProdCategory() {
            vm.productionTypeList = {
                optionLabel: "-- Select Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetFabProdCat').then(function (res) {
                                //e.success(res);
                                e.success(_.findByValues(res, 'RF_FAB_PROD_CAT_ID', $state.current.data.RF_FAB_PROD_CAT_ID_LST));
                            });
                        }
                    }
                },                
                dataTextField: "FAB_PROD_CAT_SNAME",
                dataValueField: "RF_FAB_PROD_CAT_ID"
            };
        }


        function remQueryParam() {
            $state.go($state.current , {}, { notify: false , inherit:false })
        }

        function getSupplierList() {
            KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                vm.supplierListDs = new kendo.data.DataSource({
                    data: res
                });

                vm.fromSupplierListDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }


        vm.openProgramModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ProgramModal.html',
                controller: function ($scope, config, $modalInstance, KnittingDataService, supplierListDs, fromSupplierListDs) {
                    $scope.form = {};
                    $scope.today = new Date();
                    $scope.dtFormat = config.appDateFormat;

                    $scope.dateOptions = {
                        formatYear: 'yy',
                        startingDay: 6
                    };

                    $scope.form['SC_PRG_DT'] = new Date();;
                    $scope.supplierListDs = supplierListDs;
                    $scope.fromSupplierListDs = fromSupplierListDs;


                    $scope.RevisionDate_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.RevisionDate_LNopened = true;
                    }



                    $scope.save = function (data) {

                        data['IS_YD'] = $state.current.data.IS_YD;

                        return KnittingDataService.saveDataByUrl(data, '/KnitPlan/SaveScProgram').then(function (res) {
                            if (res.success === false) {
                            }
                            else {
                                res['data'] = angular.fromJson(res.jsonStr);

                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    $modalInstance.close({
                                        KNT_SC_PRG_ISS_ID: parseInt(res.data.OP_KNT_SC_PRG_ISS_ID || 0)
                                    });
                                }
                                config.appToastMsg(res.data.PMSG);
                            }
                        });
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                },
                size: 'small',
                windowClass: 'large-Modal',
                resolve: {
                    supplierListDs : function () {
                        return vm.supplierListDs
                    },
                    fromSupplierListDs: function () {
                        return vm.fromSupplierListDs
                    }
                 }
            });

            modalInstance.result.then(function (dta) {

                if (dta.KNT_SC_PRG_ISS_ID > 0) {

                    if ($state.current.data.IS_YD == 'Y') {
                        var url = '/Knitting/Knit/KnitPlanYd/#/JobCardYd?pKNT_SC_PRG_ISS_ID=' + dta.KNT_SC_PRG_ISS_ID + '&pIS_SUB_CONTR=Y&pCT_ID_LST=360&pRF_FAB_PROD_CAT_ID=2&state=FabProdKnitOrderYD';
                    } else {
                        var url = '/Knitting/Knit/KnitPlan/#/JobCard?pKNT_SC_PRG_ISS_ID=' + dta.KNT_SC_PRG_ISS_ID + '&pIS_SUB_CONTR=Y&pRF_FAB_PROD_CAT_ID=2&pCT_ID_LST=358,361,407,432&state=FabProdKnitOrder';
                    };

                    


                    var a = document.createElement('a');
                    a.href = url;
                    a.target = '_self';
                    document.body.appendChild(a);
                    a.click();
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };



        vm.ScProgramHeaderDs = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    fields: {
                        SC_PRG_DT: { type: "date" },
                    }
                }
            },
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/KnitPlan/ScProgramPagingData';
                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += '&pIS_YD=' + $state.current.data.IS_YD;
                    url += '&pRF_FAB_PROD_CAT_ID_LST=' + $state.current.data.RF_FAB_PROD_CAT_ID_LST;
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


        if ($stateParams.pPRG_ISS_NO) {
            vm.ScProgramHeaderDs.filter({ field: "PRG_ISS_NO", operator: "contains", value: $stateParams.pPRG_ISS_NO });
        } else {
            vm.ScProgramHeaderDs.filter();
        }


        vm.createStoreRequisition = function (data) {

            Dialog.confirm('Do you really want to create yarn requisition? <br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {
                 KnittingDataService.saveDataByUrl({}, '/KnitPlan/CreateStoreRequisition?pKNT_SC_PRG_ISS_ID=' + data.KNT_SC_PRG_ISS_ID).then(function (res) {
                     res['data'] = angular.fromJson(res.jsonStr);
                     if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                         vm.ScProgramHeaderDs.read();
                     }
                     config.appToastMsg(res.data.PMSG);
                 })
             });
        };


        vm.onDelete = function (data) {
            Dialog.confirm('Deleting Program Ref# ' + data.PRG_ISS_NO + '? <br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                .then(function () {
                    KnittingDataService.saveDataByUrl({ KNT_SC_PRG_ISS_ID: data.KNT_SC_PRG_ISS_ID }, '/KnitPlan/DeleteScProgram').then(function (res) {
                        res['data'] = angular.fromJson(res.jsonStr);
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.ScProgramHeaderDs.read();
                        }
                        config.appToastMsg(res.data.PMSG);
                    })
                });
        };


        vm.sendMail = function (dataItem, rptCode) {
            //console.log(dataItem);

            vm.form.REPORT_CODE = rptCode;
            vm.form.KNT_SC_PRG_ISS_ID = dataItem.KNT_SC_PRG_ISS_ID;
            vm.form.IS_YD = $state.current.data.IS_YD;
            vm.form.IS_MAIL_WITH_ATTACH = "Y";

            return $http({                
                method: 'post',
                url: '/api/Knit/KntReport/PreviewReport',
                data: vm.form 
            }).then(function (res) {
                return res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });


        };

        vm.printReport = function (dataItem, rptCode) {
            //console.log(dataItem);

            vm.form.REPORT_CODE = rptCode;
            vm.form.KNT_SC_PRG_ISS_ID = dataItem.KNT_SC_PRG_ISS_ID;
            vm.form.IS_YD = $state.current.data.IS_YD;
          
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

        vm.printReportYd = function (dataItem) {
            var url = '/Knitting/Knit/KntScProgYd/#/KntScProgYd?pKNT_SC_PRG_ISS_ID=' + dataItem.KNT_SC_PRG_ISS_ID
            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 300) + ',scrollbars=yes,status=yes';
            $window.open(url, "_blank", opt);
        }
             

        vm.ScProgramHeaderOpt = {
            toolbar: kendo.template($("#ScProgramToolBarTemplate").html()),
            sortable: true,
            dataBound: function (e) {
                var grid = this;
                $.each(grid.tbody.find('tr'), function () {
                    if (!$stateParams.pPRG_ISS_NO) return;
                    var model = grid.dataItem(this);
                    if (model.PRG_ISS_NO === $stateParams.pPRG_ISS_NO) {
                        grid.expandRow("[data-uid='" + model.uid + "']");
                    }
                });
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
            scrollable: true,
            pageable:true,
            columns: [
                { field: "FAB_PROD_CAT_SNAME", title: "Type", width: "80px" },
                { field: "PRG_ISS_NO", title: "Program #", width: "80px" },
                { field: "SUP_TRD_NAME_EN", title: "Supplier Name", width: "90px" },
                { field: "SC_PRG_DT", title: "PO Date", width: "60px", format: "{0:dd-MMM-yyyy}" },
                { field: "ACCS_PO_SUB", title: "Status", width: "100px" },
                {
                    title: "Action",
                    template: function () {
                           //var TempStr = "<a ng-disabled='dataItem.IS_REQS_DONE==\"Y\"' class='btn btn-xs blue' title='Edit' href='/Knitting/Knit/KnitPlan/#/JobCard?pKNT_SC_PRG_ISS_ID={{dataItem.KNT_SC_PRG_ISS_ID}}&pIS_SUB_CONTR=Y'> <i class=\"fa fa-pencil\"></i> Edit</a> ";

                            if ($state.current.data.IS_YD == 'Y') {
                                var TempStr = "<a  class='btn btn-xs blue' title='Edit' href='/Knitting/Knit/KnitPlanYd/#/JobCardYd?pKNT_SC_PRG_ISS_ID={{dataItem.KNT_SC_PRG_ISS_ID}}&pIS_SUB_CONTR=Y&pCT_ID_LST=360&pRF_FAB_PROD_CAT_ID=2&state=FabProdKnitOrderYD'> <i class=\"fa fa-pencil\"></i> Edit</a> ";
                                TempStr += "<a class='btn btn-xs blue' title='Print Program' ng-click='vm.printReportYd(dataItem)'> <i class=\"fa fa-print\"></i> Program</a>&nbsp";
                                TempStr += "<a class='btn btn-xs blue' title='Print Challan' ng-click='vm.printReport(dataItem,\"RPT-3502\")'> <i class=\"fa fa-print\"></i> Challan</a>";
                                TempStr += "<a class='btn btn-xs btn-danger' ng-click='vm.onDelete(dataItem)' title='Delete' ng-if=\"dataItem.CNT_KNT_CRD==0\"> <i class=\"fa fa-ban\"></i> Del</a>";
                            } else {
                                var TempStr = "<a  class='btn btn-xs blue' title='Edit' href='/Knitting/Knit/KnitPlan/#/JobCard?pKNT_SC_PRG_ISS_ID={{dataItem.KNT_SC_PRG_ISS_ID}}&pIS_SUB_CONTR=Y&pRF_FAB_PROD_CAT_ID=2&pCT_ID_LST=358,361,407,432&state=FabProdKnitOrder&pSC_PROG_STATE={{vm.scProgState}}'> <i class=\"fa fa-pencil\"></i> Edit</a> ";
                                TempStr += "<a class='btn btn-xs blue' title='Print Program' ng-click='vm.printReport(dataItem,\"RPT-3501\")'> <i class=\"fa fa-print\"></i> Program</a>&nbsp";
                                TempStr += "<a class='btn btn-xs blue' title='Print Challan' ng-click='vm.printReport(dataItem,\"RPT-3502\")'> <i class=\"fa fa-print\"></i> Challan</a>&nbsp";
                                //TempStr += "<a class='btn btn-xs blue' title='Send Mail' ng-click='vm.sendMail(dataItem,\"RPT-3501\")'> <i class=\"fa fa-send\"></i> Send</a>";
                                TempStr += "<a class='btn btn-xs btn-danger' ng-click='vm.onDelete(dataItem)' title='Delete' ng-if=\"dataItem.CNT_KNT_CRD==0\"> <i class=\"fa fa-ban\"></i> Del</a>";
                            }


                        return TempStr;
                    },
                    width: "100px"
                },
            ]
        };






        vm.ScProgramHeaderDtl = function (Item) {
            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByUrl('/KnitPlan/getJobCardList?pKNT_SC_PRG_ISS_ID=' + Item.KNT_SC_PRG_ISS_ID).then(function (res) {
                                e.success(res);
                            })
                        }
                    }
                },
                scrollable: true,
                columns: [
                    { field: "JOB_CRD_NO", title: "Job Card#", type: "string", width: "10px" },
                    { field: "BYR_ACC_NAME_EN", title: "Buyer A/C#", type: "string", width: "10px" },
                    { field: "WORK_STYLE_NO", title: "Style#", type: "string", width: "10px" },
                    { field: "ORDER_NO_LIST", title: "Order#", type: "string", width: "20px" },
                    { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "20px" },
                    { field: "FAB_TYPE_NAME", title: "Fab Type", type: "string", width: "15px" },
                    { field: "MC_DIA", title: "Exp M/C Dia", type: "string", width: "10px" },
                    { field: "ACT_MC_DIA", title: "M/C Mia", type: "string", width: "10px" },
                    { field: "FIN_DIA_N_DIA_TYPE", title: "Exp Fin Dia", type: "string", width: "10px" },
                    { field: "ACT_FIN_DIA_N_DIA_TYPE", title: "Fin Dia", type: "string", width: "10px" },
                    { field: "MC_GAUGE_NO", title: "M/C Gauge", type: "string", width: "10px" },
                    {
                        title: "Assign Qty",
                        field: "ASIGN_QTY",
                        template: function () {
                            return " {{dataItem.ASIGN_QTY}} {{dataItem.MOU_CODE}}";
                        },
                        width: "10px"
                    },
                    { field: "REMARKS", title: "Remarks", type: "string", width: "10px" }
                ]
            };
        };

    }

})();