////////// Start Collar Cuff Header Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScoCollarCuffProgHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'formData', 'Dialog', KntScoCollarCuffProgHController]);
    function KntScoCollarCuffProgHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, formData, Dialog) {

        var vm = this;
        vm.showSplash = true;
        $scope.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.form = {MC_ORDER_H_ID_LST:''};// { PROD_DT: moment(vm.today).format("DD-MMM-YYYY") };

        var key = 'KNT_SCO_CLCF_PRG_H_ID';
        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
                
        vm.today = new Date();
        vm.form = formData[key] ? formData : {
            KNT_SCO_CLCF_PRG_H_ID: 0, MC_FAB_PROD_ORD_H_ID: $stateParams.pMC_FAB_PROD_ORD_H_ID, SCO_PRG_DT: moment(vm.today).format("DD-MMM-YYYY"), LK_SC_PRG_STATUS_ID: 537,
            MC_ORDER_NO_LST: $stateParams.pORDER_NO, RF_GARM_PART_LST: ''
        };
        $scope.form = formData[key] ? formData : {
            KNT_SCO_CLCF_PRG_H_ID: 0, MC_FAB_PROD_ORD_H_ID: $stateParams.pMC_FAB_PROD_ORD_H_ID, SCO_PRG_DT: moment(vm.today).format("DD-MMM-YYYY"), LK_SC_PRG_STATUS_ID: 537,
            MC_ORDER_NO_LST: $stateParams.pORDER_NO, RF_GARM_PART_LST: ''
        };

        vm.errstyle = { 'border': '1px solid #f13d3d' };
        
        activate();
        function activate() {
            var promise = [getSupplierList(), getProgStatus(), getFabricTypeList(), getFabProcRate()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.SCO_PRG_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.SCO_PRG_DT_LNopened = true;
        };

       
        //vm.yarnListDataSource = new kendo.data.DataSource({
        //    transport: {
        //        read: function (e) {
        //            return KnittingDataService.getDataByFullUrl('/api/knit/KntCollarCuff/GetCollarCuffYarnList?pMC_FAB_PROD_ORD_H_ID=' + $stateParams.pMC_FAB_PROD_ORD_H_ID).then(function (res) {
        //                e.success(res);
        //            }, function (err) {
        //                console.log(err);
        //            });
        //        }
        //    }
        //});

        
        function YarnDropDownEditor(container, options) {
            console.log(options);

            if ($scope.form.LK_SC_PRG_STATUS_ID == 538) {
                $("<span>" + options.model.get(options.field).KNT_YRN_LOT_LST + "</span>").appendTo(container);
                return;
            };

            $('<input data-text-field="KNT_YRN_LOT_LST" data-value-field="KNT_PLAN_D_ID" data-bind="value:' + options.field + '" required />')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    optionLabel: "--N/A--",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/KntCollarCuff/GetCollarCuffYarnList?pMC_FAB_PROD_ORD_H_ID=' + $stateParams.pMC_FAB_PROD_ORD_H_ID + '&pMC_COLOR_ID=' + options.model.MC_COLOR_ID).then(function (res) {
                                    e.success(res);
                                    //options.model.KNT_PLAN_D_ID = res[0].KNT_PLAN_D_ID;
                                }, function (err) {
                                    console.log(err);
                                });
                            }
                        }
                    },
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        console.log(item);
                        options.model.KNT_PLAN_D_ID = item.KNT_PLAN_D_ID;
                        //vm.incrEmpListGridDataSource.sync();
                    }
                });
        }

        function getSupplierList() {
            vm.supplierOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);                    
                    //vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                    //vm.form.KNT_SC_PRG_RCV_ID = null;

                    //vm.scProgDataSource.read();
                }
            };

            return vm.supplierDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };
     
        function getProgStatus() {
            vm.progStatusOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);                    
                    //vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                    //vm.form.KNT_SC_PRG_RCV_ID = null;

                    //vm.scProgDataSource.read();
                }
            };

            return vm.progStatusDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.LookupListData(105).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getFabricTypeList() {
            vm.fkFabTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FAB_TYPE_NAME",
                dataValueField: "RF_FAB_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.RF_GARM_PART_LST = item.RF_GARM_PART_LST;
                    $scope.assignRateDtlDataSource.read();
                    $scope.progDtlDataSource.read();
                },
                dataBound: function (e) {
                    if (formData[key] != null && formData[key] > 0) {
                        vm.form.RF_FAB_TYPE_ID = formData['RF_FAB_TYPE_ID'];
                    }
                }
            };

            return vm.fkFabTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Common/FabricTypeList?pIS_FLAT_CIR=F&pINV_ITEM_CAT_ID=' + (formData.INV_ITEM_CAT_ID || 34)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
            
        }

        function getFkDesignType() {
            vm.fkDesigTypOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    //vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                    //vm.form.KNT_SC_PRG_RCV_ID = null;

                    //vm.scProgDataSource.read();
                }
            };

            return vm.fkDesigTypDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.LookupListData(78).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getFabProcRate() {
            vm.fabProcRateOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    //vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                    //vm.form.KNT_SC_PRG_RCV_ID = null;

                    //vm.scProgDataSource.read();
                }
            };

            //'api/mrc/BudgetSheet/getRate'
            return vm.fabProcRateDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.LookupListData(78).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        

        vm.getOrderColor = function (pMC_ORDER_H_ID_LST) {
            return vm.orderColorDataSource = new kendo.data.DataSource({                
                transport: {
                    read: function (e) {                        
                        var url = '/api/mrc/Order/MultiOrderWiseColorList/' + pMC_ORDER_H_ID_LST;
                        
                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };
        
        $scope.$watch('vm.form', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    $scope.form = vm.form;
                }
            }
        }, true);

        $scope.$watchGroup(['vm.form.MC_BYR_ACC_ID', 'vm.form.MC_STYLE_H_EXT_ID', 'vm.form.MC_COLOR_ID'], function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    //vm.IS_NEXT = 'N';                    
                }
            }
        }, true);

        
        //$scope.$watchGroup(['vm.form.RF_FAB_TYPE_ID', 'vm.form.LK_FK_DGN_TYP_ID'], function (newVal, oldVal) {

        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {                    
        //            $scope.assignRateDtlDataSource.read();
        //        }
        //    }
        //}, true);

        function DesignTypeDropDownEditor(container, options) {
           
            $('<input data-text-field="LK_DATA_NAME_EN" data-value-field="LOOKUP_DATA_ID" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    optionLabel: "--N/A--",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                return KnittingDataService.LookupListData(78).then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    },
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        console.log(item);
                        options.model.LK_FK_DGN_TYP_ID = item.LOOKUP_DATA_ID;
                        options.model.FK_DESIGN_TYPE = { LK_FK_DGN_TYP_ID: item.LOOKUP_DATA_ID, FK_DESIGN_TYPE_NAME: item.LK_DATA_NAME_EN };
                        //vm.incrEmpListGridDataSource.sync();
                    }
                });
        }

        function RateDescDropDownEditor(container, options) {
         
            $('<input data-text-field="FAB_PROC_NAME" data-value-field="MC_FAB_PROC_RATE_ID" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    optionLabel: "--N/A--",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                return KnittingDataService.getDataByFullUrl('/api/Knit/KnitCommon/GetProcRateListByParty?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0) + '&pRF_FAB_TYPE_ID=' + (vm.form.RF_FAB_TYPE_ID || 0) + '&pLK_FK_DGN_TYP_ID=' + options.model.LK_FK_DGN_TYP_ID).then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    },
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        console.log(item);

                        FAB_PROC_RATE = { MC_FAB_PROC_RATE_ID: item.MC_FAB_PROC_RATE_ID, FAB_PROC_NAME: item.FAB_PROC_NAME };
                        options.model.MC_FAB_PROC_RATE_ID = item.MC_FAB_PROC_RATE_ID;
                        options.model.PROD_RATE = item.PROC_RATE;
                        //vm.incrEmpListGridDataSource.sync();
                        var grid = $("#assignRateGrid").data("kendoGrid");
                        grid.refresh();
                    }
                });
        }


        $scope.assignRateDtlOptions = {
            editable: true,
            selectable: "cell",
            navigatable: true,
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
            //dataBound: function (e) {
            //    collapseAllGroups(this);
            //},
            columns: [
                { field: "GARM_PART_NAME", title: "Part", type: "string", width: "15%" },                
                { field: "FK_DESIGN_TYPE", title: "Design Type", width: "15%", filterable: false, editor: DesignTypeDropDownEditor, template: "#=FK_DESIGN_TYPE.FK_DESIGN_TYPE_NAME#" },
                { field: "FAB_PROC_RATE", title: "Rate Desc", width: "50%", filterable: false, editor: RateDescDropDownEditor, template: "#=FAB_PROC_RATE.FAB_PROC_NAME#" },
                {
                    title: "Rate",
                    field: "PROD_RATE",
                    template: function () {
                        return "<ng-form name='frmClcfRate'><input type='number' class='form-control' name='PROD_RATE' ng-model='dataItem.PROD_RATE' min='1' ng-style='frmClcfRate.PROD_RATE.$error.required? vm.errstyle:\"\"||frmClcfRate.PROD_RATE.$error.min? vm.errstyle:\"\"' required /></ng-form>";
                    },
                    width: "20%"
                }
                //{ field: "PROD_RATE", title: "Rate", type: "number", width: "20%" }
            ]
        };

        $scope.assignRateDtlDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    return KnittingDataService.getDataByFullUrl('/api/knit/ScoCollarCuff/GetScoClCfProgRateAssign?pRF_GARM_PART_LST=' + (vm.form.RF_GARM_PART_LST || 0) +
                        '&pMC_FAB_PROD_ORD_H_ID=' + ($stateParams.pMC_FAB_PROD_ORD_H_ID || 0) + '&pKNT_SCO_CLCF_PRG_H_ID=' + ($stateParams.pKNT_SCO_CLCF_PRG_H_ID || 0)).then(function (res) {
                        //angular.forEach(res, function (val, key) {
                        //    val['LK_FK_DGN_TYP_ID'] = vm.form['LK_FK_DGN_TYP_ID'];
                        //    val['FK_DESIGN_TYPE_NAME'] = vm.form['FK_DESIGN_TYPE_NAME'];

                        //    val['FK_DESIGN_TYPE'] = { FK_DESIGN_TYPE: vm.form['LK_FK_DGN_TYP_ID'], FK_DESIGN_TYPE_NAME: vm.form['FK_DESIGN_TYPE_NAME'] };
                        //});
                        console.log(res);
                        
                        e.success(res);
                    });
                }
            },
            schema: {               
                model: {
                    //id: "HR_YR_INCR_D_ID",
                    fields: {
                        GARM_PART_NAME: { type: "string", editable: false },
                        PROD_RATE: { type: "number" },
                        FK_DESIGN_TYPE: { defaultValue: { LOOKUP_DATA_ID: "", LK_DATA_NAME_EN: "--N/A--" }, editable: true },
                        FAB_PROC_RATE: { defaultValue: { MC_FAB_PROC_RATE_ID: "", FAB_PROC_NAME: "--N/A--" }, editable: true }
                    }
                }
            }
        });


        $scope.progDtlDataSource = new kendo.data.DataSource({
            schema: {
                model: {
                    id: "KNT_SCO_CLCF_PRG_D_ID",
                    fields: {
                        GARM_PART_NAME: { type: "string", editable: false },
                        COLOR_NAME_EN: { type: "string", editable: false },
                        SIZE_CODE: { type: "string", editable: false },
                        MESUREMENT: { type: "string", editable: false },
                        LEFT_REQ_QTY: { type: "number", editable: false },
                        RQD_QTY: { type: "number", editable: false },
                        CANCEL_QTY: { type: "number", editable: false },
                        YRN_DETAILS: { defaultValue: { KNT_PLAN_D_ID: "", KNT_YRN_LOT_LST: "--N/A--" }, editable: true },
                    }
                }
            },
            aggregate: [
                { field: "YRN_DETAILS", aggregate: "count" },
                { field: "RQD_QTY", aggregate: "sum" },
                { field: "CANCEL_QTY", aggregate: "sum" }
            ],
            transport: {
                read: function (e) {
                    var url = '/api/Knit/ScoCollarCuff/GetScoCollarCuffDtl?&pKNT_SCO_CLCF_PRG_H_ID=' + ($stateParams.pKNT_SCO_CLCF_PRG_H_ID || 0) + '&pMC_FAB_PROD_ORD_H_ID=' + ($stateParams.pMC_FAB_PROD_ORD_H_ID || 0);
                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        vm.colorAutoCompleteDataSource = [];

                        if (res.length > 0) {
                            vm.form['LK_FK_DGN_TYP_ID'] = res[0]['LK_FK_DGN_TYP_ID'];
                            vm.form['FK_DESIGN_TYPE_NAME'] = res[0]['FK_DESIGN_TYPE_NAME'];
                        }
                        e.success(res);

                        vm.colorAutoCompleteDataSource = _.uniq(_.map(res, 'COLOR_NAME_EN'));
                        console.log(res);
                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        function colorFilter(element) {
            element.kendoAutoComplete({
                dataSource: vm.colorAutoCompleteDataSource
            });
        }

        $scope.progDtlOptions = {
            height: 300,
            scrollable: true,
            pageable: false,
            editable: true,
            //selectable: "cell",
            navigatable: true,
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
            //dataBound: function (e) {
            //    collapseAllGroups(this);
            //},
            columns: [
                { field: "GARM_PART_NAME", title: "Part", type: "string", width: "5%" },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "15%", filterable: { multi: true } }, //======== filterable: { ui: colorFilter }                
                { field: "YRN_DETAILS", title: "Yarn", width: "24%", filterable: false, editor: YarnDropDownEditor, template: "#=YRN_DETAILS.KNT_YRN_LOT_LST#", footerTemplate: "Total: #=count#" },
                //{
                //    title: "Yarn",
                //    field: "YRN_DETAILS",
                //    editor: YarnDropDownEditor,
                //    template: function () {
                //        return "<ng-form name='frmClcfStylItm'><input type='text' class='form-control' name='KNT_YRN_LOT_LST' ng-model='dataItem.KNT_YRN_LOT_LST' ng-style='(frmClcfStylItm.KNT_YRN_LOT_LST.$error.required)? vm.errstyle:\"\"' readonly /></ng-form>";
                //    },
                //    footerTemplate: "Total: #=count#",
                //    width: "10%",
                //    filterable: false
                //},
                { field: "SIZE_CODE", title: "Size", type: "string", width: "10%" },
                { field: "LEFT_REQ_QTY", title: "Left Qty", type: "number", width: "7%" },
                { field: "MESUREMENT", title: "F.Meas", type: "string", width: "6%" },
                {
                    title: "G.Meas",
                    field: "GREY_MEAS",
                    template: function () {
                        return "<ng-form name='frmGreyMeas'><input type='text' class='form-control' name='GREY_MEAS' ng-model='dataItem.GREY_MEAS' ng-disabled='$parent.form.LK_SC_PRG_STATUS_ID==538' /></ng-form>";
                    },
                    width: "10%",
                    filterable: false
                },
                {
                    title: "Req. Qty",
                    field: "RQD_QTY",
                    template: function () {
                        return "<ng-form name='frmReqPcs'><input type='number' class='form-control' name='RQD_QTY' ng-model='dataItem.RQD_QTY' ng-disabled='$parent.form.LK_SC_PRG_STATUS_ID==538' required min='0' ng-style='(frmReqPcs.RQD_QTY.$error.required||frmReqPcs.RQD_QTY.$error.min||dataItem.RQD_QTY>dataItem.LEFT_REQ_QTY)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    footerTemplate: "#=sum#",
                    width: "7%",
                    filterable: false
                },
                {
                    title: "Rate",
                    field: "PROD_RATE",
                    template: function () {
                        return "<ng-form name='frmRate'><input type='number' class='form-control' name='PROD_RATE' ng-model='dataItem.PROD_RATE' ng-disabled='$parent.form.LK_SC_PRG_STATUS_ID==538' required min='0' ng-style='(frmRate.PROD_RATE.$error.required||frmRate.PROD_RATE.$error.min)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "5%",
                    filterable: false,
                    hidden: false
                },
                {
                    title: "B.Ln",
                    field: "BED_LN",
                    template: function () {
                        return "<input type='number' class='form-control' name='BED_LN' ng-model='dataItem.BED_LN' ng-disabled='$parent.form.LK_SC_PRG_STATUS_ID==538' />";
                    },
                    width: "5%",
                    filterable: false
                },
                {
                    title: "Cncl Qty",
                    field: "CANCEL_QTY",
                    template: function () {
                        return "<ng-form name='frmCANCEL_QTY'><input type='number' class='form-control' name='CANCEL_QTY' ng-model='dataItem.CANCEL_QTY' ng-disabled='$parent.form.LK_SC_PRG_STATUS_ID<538' min='0' max='{{dataItem.RQD_QTY}}' ng-style='(frmCANCEL_QTY.CANCEL_QTY.$error.min||frmCANCEL_QTY.CANCEL_QTY.$error.max)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    footerTemplate: "#=sum#",
                    width: "6%",
                    filterable: false
                }
                //{
                //    title: "Action",
                //    template: function () {
                //        return "<a class='btn btn-xs blue' ui-sref='ScoCollarCuffProgH.CollarCuffDtl({pKNT_SCO_CLCF_PRG_H_ID:dataItem.KNT_SCO_CLCF_PRG_H_ID, pSCM_SUPPLIER_ID:dataItem.SCM_SUPPLIER_ID })' ><i class='fa fa-edit'> Edit</i></a>";
                //    },
                //    width: "250px"
                //}
            ]
        };
                        
    }

})();
////////// End Collar Cuff Header Controller




////////// Start Collar Cuff Detail Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScoCollarCuffProgDController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', '$modal', KntScoCollarCuffProgDController]);
    function KntScoCollarCuffProgDController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog, $modal) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.errstyle = { 'border': '1px solid #f13d3d' };

        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        $scope.Showed = false;

        console.log($scope.$parent.clcfDtl);

        //var key = 'KNT_SC_GFAB_DLV_H_ID';
        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        //vm.form = $stateParams.pSCM_SUPPLIER_ID > 0 ? { SCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID } : { SCM_SUPPLIER_ID: 0 };

        activate();
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.prodDateOpened = [];
        vm.prodDateOpen = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.prodDateOpened[index] = true;
        };

       
        
        $scope.$parent.errors = undefined;
        vm.Save = function () {
            var submitData = angular.copy($scope.$parent.form);
               
            var clcfRateGridData = $scope.$parent.assignRateDtlDataSource.data();
            var clcfGridData = $scope.$parent.progDtlDataSource.data();
            
            angular.forEach(clcfRateGridData, function (val, key) {
                angular.forEach(clcfGridData, function (val1, key1) {
                    if (val['GARM_PART_NAME'] == val1['GARM_PART_NAME']) {
                        val1['PROD_RATE'] = val['PROD_RATE'];
                        val1['MC_FAB_PROC_RATE_ID'] = val['MC_FAB_PROC_RATE_ID'];
                    }
                });
            });

            //console.log(clcfRateGridData);
            //console.log(clcfGridData);
            //return; 

            $scope.$parent.errors = [];
            angular.forEach(clcfRateGridData, function (val, key) {
                if (val['PROD_RATE'] == null || val['PROD_RATE'] == '') {
                    $scope.$parent.errors.push({ key: "", error: 'Assign rate should not empty' });
                }
            });

            if ($scope.$parent.errors != undefined && $scope.$parent.errors.length > 0) {                
                return;
            }
            else {
                $scope.$parent.errors = undefined;
            }

            

            submitData.SCO_CLCF_PRG_D_XML = KnittingDataService.xmlStringShort(clcfGridData.map(function (ob) {
                return ob;
            }));

            console.log(submitData);
            //return;

            return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/ScoCollarCuff/ScoCollarCuffBatchSave').then(function (res) {
                console.log(res);

                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $state.go('ScoCollarCuffProgH.CollarCuffDtl', {
                            pKNT_SCO_CLCF_PRG_H_ID: res['data'].PKNT_SCO_CLCF_PRG_H_ID_RTN, pMC_FAB_PROD_ORD_H_ID: $stateParams.pMC_FAB_PROD_ORD_H_ID, pMC_STYLE_H_ID: $stateParams.pMC_STYLE_H_ID, pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID
                        }, { notift: false });

                        $scope.$parent.progDtlDataSource.read();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });


        };

        vm.back = function () {
            $state.go('ScoCollarCuffProgList', { pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID || $scope.$parent.SCM_SUPPLIER_ID || 0 });
        }

        vm.cancel = function () {
            $state.go('KntCollarCuffOrdProdH.dtl', {
                pMC_BYR_ACC_ID: $stateParams.pMC_BYR_ACC_ID, pMC_STYLE_H_EXT_ID: $stateParams.pMC_STYLE_H_EXT_ID, pMC_COLOR_ID: $stateParams.pMC_COLOR_ID,
                pMC_ORDER_H_ID_LST: $stateParams.pMC_ORDER_H_ID_LST
            }, { reload: 'KntCollarCuffOrdProdH.dtl' });
        }

        vm.printProgram = function () {
            var rptCode = 'RPT-3541';

            var data = angular.copy($scope.$parent.form);

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            //var params = angular.copy($scope.form);
            var params = angular.extend({ REPORT_CODE: rptCode }, data);
            console.log(params);

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

        vm.createStoreRequisition = function (data) {
            Dialog.confirm('Do you really want to create yarn requisition? <br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {
                 KnittingDataService.saveDataByUrl({}, '/KnitPlan/CreateStoreRequisition?pKNT_SC_PRG_ISS_ID=' + data.KNT_SC_PRG_ISS_ID).then(function (res) {
                     res['data'] = angular.fromJson(res.jsonStr);
                     if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                         return getSubContractProgramData();
                     }
                     config.appToastMsg(res.data.PMSG);
                 })
             });
        };

        vm.reqGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    return KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/GetCollarCuffScoReqList?&pKNT_SCO_CLCF_PRG_H_ID=' + ($stateParams.pKNT_SCO_CLCF_PRG_H_ID || 0)).then(function (res) {
                           
                            console.log(res);

                            e.success(res);
                        });
                }
            }
        });

        vm.reqGridOptions = {
            height: 100,
            scrollable: true,
            pageable: false,
            editable: false,
            selectable: "cell",
            navigatable: true,
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
            columns: [                
                { field: "STR_REQ_NO", title: "Requisition No", type: "string", width: "10%" },
                { field: "STR_REQ_DT", title: "Requisition Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "REQ_TYPE_NAME", title: "Requisition Type", type: "string", width: "20%" },
                { field: "USER_NAME_EN", title: "Create By", type: "string", width: "10%" },
                { field: "TTL_REQ_QTY", title: "TTL Req. Qty", type: "string", width: "8%" },                
                { field: "EVENT_NAME", title: "Status", type: "string", width: "10%" },                
                {
                    title: "Action",
                    template: function () {
                        return "<a href='/Knitting/Knit/KnitPlan/#/JobCardCollarCuffSco?a=185&pMC_FAB_PROD_ORD_H_ID={{dataItem.MC_FAB_PROD_ORD_H_ID}}&pKNT_SCO_CLCF_PRG_H_ID={{dataItem.KNT_SCO_CLCF_PRG_H_ID}}&pKNT_YRN_STR_REQ_H_ID={{dataItem.KNT_YRN_STR_REQ_H_ID}}' class='btn btn-xs blue'> view</a>&nbsp;" +
                            '<a class="btn btn-xs blue" ng-click="vm.printReq(dataItem)"><i class="fa fa-print"> Print</i></a>';
                    },
                    width: "10%"
                }
            ]
        };

        vm.printReq = function (item) {
            console.log(item);
            vm.form = item;

            if (vm.form.RF_REQ_TYPE_ID == 5 || vm.form.RF_REQ_TYPE_ID == 6) {  //For Sample, Development
                vm.form.REPORT_CODE = 'RPT-3554';
            }
            else {
                vm.form.REPORT_CODE = 'RPT-3554';
            }

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
        }

        vm.finalizeProgram = function () {
            var submitData = angular.copy($scope.$parent.form);
            console.log(submitData);

            Dialog.confirm('Do you want to save and finalize?', 'Confirmation', ['Yes', 'No']).then(function () {

                return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/ScoCollarCuff/ScoCollarCuffProgFinalize').then(function (res) {
                    console.log(res);

                    vm.errors = undefined;
                    if (res.success === false) {
                        vm.errors = [];
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $scope.$parent.form.LK_SC_PRG_STATUS_ID = 538;                            
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });

        }

        vm.cancelProgram = function () {
            var clcfGridData = $scope.$parent.progDtlDataSource.data();
            var submitData = angular.copy($scope.$parent.form);

            submitData.SCO_CLCF_PRG_D_XML = KnittingDataService.xmlStringShort(clcfGridData.map(function (ob) {
                return ob;
            }));

            console.log(submitData);

            Dialog.confirm('Do you want to full/partial cancel the program?', 'Confirmation', ['Yes', 'No']).then(function () {

                return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/ScoCollarCuff/ScoCollarCuffProgCancel').then(function (res) {
                    console.log(res);

                    vm.errors = undefined;
                    if (res.success === false) {
                        vm.errors = [];
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $scope.$parent.form.LK_SC_PRG_STATUS_ID = res.data.PLK_SC_PRG_STATUS_ID_RTN;
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });

        }

        //function getKpData(pMC_FAB_PROD_ORD_H_ID, pMC_FAB_PROD_ORD_D_ID) {
        //    alert(pMC_FAB_PROD_ORD_H_ID+'-'+pMC_FAB_PROD_ORD_D_ID)
        //    return KnittingDataService.getDataByUrl('/FabProdKnitOrder/SelectByID?pMC_FAB_PROD_ORD_H_ID=' + pMC_FAB_PROD_ORD_H_ID + '&pOption=3003&pLK_COL_TYPE_ID_LST=358,361,407,432&pMC_FAB_PROD_ORD_D_ID=' + (pMC_FAB_PROD_ORD_D_ID || '')).then(function (res) {
        //        vm.IS_EDIT_MODE = false;
        //        $scope.datas = res;
        //    })
        //};

        //vm.openExColourListModal = function (pMC_FAB_PROD_ORD_H_ID) {
        //    alert(pMC_FAB_PROD_ORD_H_ID);

        //    var modalInstance = $modal.open({
        //        animation: true,
        //        templateUrl: 'ExColourListModal.html',
        //        controller: 'JobCardDashboardModalController',
        //        size: 'lg',
        //        windowClass: 'app-modal-window',
        //        resolve: {
        //            Params: function () {
        //                return {
        //                    pMC_FAB_PROD_ORD_H_ID: pMC_FAB_PROD_ORD_H_ID,
        //                    pRF_FAB_PROD_CAT_ID: null,
        //                    pHAS_COL_CUFF: 'Y',
        //                    IS_REQUISITION: true,
        //                    RF_FAB_PROD_CAT_ID_LST: '1,2,3,4,5,6',
        //                    LK_COL_TYPE_ID_LST: '358,361,407,432', //($state.current.data.LK_COL_TYPE_ID_LST || '358,361,407,432'),
        //                    DEFAULT_CAT_ID: 2,
        //                    ORDER_WISE_SELECT: true

        //                }
        //            }
        //        }
        //    });

        //    modalInstance.result.then(function (data) {
        //        if (data.MC_FAB_PROD_ORD_H_ID) {
        //            if (data.hasOwnProperty('MC_FAB_PROD_ORD_D_ID')) {
        //                getKpData(data.MC_FAB_PROD_ORD_H_ID, data.MC_FAB_PROD_ORD_D_ID);
        //            } else {
        //                getKpData(data.MC_FAB_PROD_ORD_H_ID, null);
        //            }


        //        }

        //    }, function () {
        //        console.log('Modal dismissed at: ' + new Date());
        //    });

        //}
    }

})();
////////// End Collar Cuff Detail Controller





////////// Start List Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScoCollarCuffProgListController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', KntScoCollarCuffProgListController]);
    function KntScoCollarCuffProgListController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

       
        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.today = new Date();
        vm.form = { KNT_SCO_CLCF_PRG_H_ID: 0 }; //formData[key] ? formData : { KNT_SC_GFAB_DLV_H_ID: 0, SCM_SUPPLIER_ID: 0, CHALAN_NO: null, CHALAN_DT: vm.today, GT_PASS_NO: '', GFAB_DLV_DTL_XML: '', IS_FINALIZE: 'N' };


        activate();
        function activate() {
            var promise = [getSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.scRcvDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.scRcvDateOpened = true;
        };

        vm.chlnDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.chlnDateOpened = true;
        };

        

        function getSupplierList() {
            vm.supplierOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                    vm.form.KNT_SC_PRG_RCV_ID = null;

                    //vm.scProgDataSource.read();
                }
            };

            return vm.supplierDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        

        vm.getPartyChallanList = function () {

            vm.partyChallanDataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                schema: {
                    data: "data",
                    total: "total",
                    model: {
                        fields: {
                            SCO_PRG_DT: { type: "date" }
                        }
                    }
                },
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/ScoCollarCuff/GetScoCollarCuffProgList?pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID;
                        url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);
                        console.log(params.filter);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            //angular.forEach(res.data, function (val, key) {
                            //    val['BLK_FAB_REQ_DT_STR'] = $filter('date')(val['BLK_FAB_REQ_DT'], vm.dtFormat);
                            //});
                            e.success(res);
                            console.log(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                },
                pageSize: 10,
                //scrollable: {
                //    virtual: true
                //    //scrollable:true
                //},
                //group: [{ field: 'STYLE_NO' }],
                sort: [{ field: 'SCO_PRG_DT', dir: 'desc' }]
            });
        };

        vm.partyChallanOptions = {
            height: '500',
            pageable: true,
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
            //dataBound: function (e) {
            //    collapseAllGroups(this);
            //},
            columns: [
                { field: "SCO_PRG_NO", title: "Program#", type: "string" },
                { field: "SCO_PRG_DT", title: "Date", type: "date", format: "{0:dd-MMM-yyyy}" },
                { field: "CPI", title: "CPI", type: "string" },
                { field: "NO_PLY", title: "No of Ply", type: "string" },                
                { field: "REMARKS", title: "Remarks", type: "string" },
                {
                    title: "Action",
                    template: function () {
                        return "<a class='btn btn-xs blue' ui-sref='ScoCollarCuffProgH.CollarCuffDtl({pKNT_SCO_CLCF_PRG_H_ID:dataItem.KNT_SCO_CLCF_PRG_H_ID, pMC_FAB_PROD_ORD_H_ID:dataItem.MC_FAB_PROD_ORD_H_ID, pSCM_SUPPLIER_ID:dataItem.SCM_SUPPLIER_ID })' ><i class='fa fa-edit'> Edit</i></a>&nbsp;<button type='button' class='btn btn-xs blue' ng-click='vm.printProgram(dataItem)' ><i class='fa fa-print'> Print</i></button>";
                    },
                    width: "250px"
                }
            ]
        };



        vm.newProgram = function () {
            $state.go('ScoCollarCuffProgH.CollarCuffDtl', { pKNT_SCO_CLCF_PRG_H_ID: 0, pSCM_SUPPLIER_ID: vm.form.SCM_SUPPLIER_ID });
        }

        vm.printProgram = function (dataItem) {
            var rptCode = 'RPT-3541';

            var data = angular.copy(dataItem);

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            //var params = angular.copy($scope.form);
            var params = angular.extend({ REPORT_CODE: rptCode }, data);
            console.log(params);

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
////////// End List Controller