//=============== Start for GmtSwMcnSpecController =================
(function () {
    'use strict';
    angular.module('multitex.garments').controller('GmtSwMcnSpecController', ['$q', 'config', 'Dialog', 'GarmentsDataService', '$stateParams', '$state', '$scope', '$filter', '$modal', 'formData', '$timeout', GmtSwMcnSpecController]);
    function GmtSwMcnSpecController($q, config, Dialog, GarmentsDataService, $stateParams, $state, $scope, $filter, $modal, formData, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        var key = 'GMT_SW_MCN_SPEC_ID';
        vm.form = formData[key] ? formData : {
            GMT_SW_MCN_SPEC_ID: 0, SW_MCN_SPEC: '', SW_MCN_SPEC_SHORT: ''
        };
        vm.thrdConsForm = { GMT_SM_THRD_CONS_ID: 0, CONS_MOU_ID: 24 };
        vm.mcGuideForm = { GMT_SW_MCN_GUIDE_ID: 0 };
        vm.mcProfileForm = { GMT_SW_MACHINE_ID: 0, IS_HIRED: 'N', LK_MAC_STATUS_ID: 607, MCN_STATUS_NAME: 'Active', REMARKS: '' };

        //$scope.form = formData[key] ? formData : {
        //    GMT_SW_MCN_SPEC_ID: 0, SW_MCN_SPEC: ''
        //};
        
              
       
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [
                getMachineSpecList(), getSwMcnTypeList(), getSwMcnBedTypeList(), getStitchTypeList(), getSwAppTypeList(), getThrdTypeList(), getMOUList(),
                getMcnGuideTypeList(), getSupplierList(), getBrandList(), getCountryList(), getProdFloorList(), getMcnStatusList()
            ];
            return $q.all(promise).then(function () {
                //$timeout(function () {
                //    if (formData[key]) {
                //        vm.form = formData[key] ? formData : {
                //            GMT_SW_MCN_SPEC_ID: 0, SW_MCN_SPEC: '', SW_MCN_SPEC_SHORT: ''
                //        };

                //        vm.thrdConsForm = { GMT_SM_THRD_CONS_ID: 0, CONS_MOU_ID: 24 };
                //        vm.mcGuideForm = { GMT_SW_MCN_GUIDE_ID: 0 };
                //        vm.mcProfileForm = { GMT_SW_MACHINE_ID: 0, IS_HIRED: 'N', LK_MAC_STATUS_ID: 607, MCN_STATUS_NAME: 'Active', REMARKS: '' };
                //    }
                //}, 1000);

                vm.showSplash = false;                
            });
        }

                    

        //$scope.$watch('vm.form', function (newVal, oldVal) {
        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            $scope.form = vm.form;
        //        }
        //    }
        //}, true);


        //$scope.$watchGroup(['vm.form.RF_FAB_PROD_CAT_ID', 'vm.form.MC_BYR_ACC_ID', 'vm.form.MC_STYLE_H_EXT_ID'], function (newVal, oldVal) {

        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            vm.IS_NEXT = 'N';
        //        }
        //    }
        //}, true);
        
        function getMachineSpecList() {
            vm.machineSpecGridOption = {
                height: 550,
                sortable: true,
                scrollable: true,
                selectable: "row",
                columns: [
                    { field: "SW_MCN_SPEC", title: "M/C Spec", width: "200px" },
                    { field: "SW_MCN_TYP_NAME_EN", title: "M/C Type", width: "150px" },
                    { field: "SM_BED_TYP_NAME_EN", title: "Bed Type", width: "150px" },
                    { field: "STCH_TYP_NAME_EN", title: "Stitch Type", width: "150px" },
                    { field: "SW_APP_TYP_NAME_EN", title: "App Type", width: "150px" },
                    { field: "NDL_COUNT", title: "Count", width: "150px" },
                    { field: "STD_RPM", title: "Cons", width: "150px" },
                    { field: "STD_SPI", title: "MoU", width: "150px" }                    
                ],
                change: function (e) {
                    var grid = $("#mcnSpecGrid").data("kendoGrid");
                    //grid.select("tr:eq(1)");
                    var row = grid.select();
                    vm.form = grid.dataItem(row);
                    $stateParams.pGMT_SW_MCN_SPEC_ID = grid.dataItem(row).GMT_SW_MCN_SPEC_ID;

                    console.log(grid.dataItem(row));

                    $scope.$apply();

                    $state.go('SwMcnSpec', { pGMT_SW_MCN_SPEC_ID: grid.dataItem(row).GMT_SW_MCN_SPEC_ID }, { notify: false });
                    vm.thrdConsGridDataSource.read();
                    vm.mcnGuideGridDataSource.read();
                    vm.mcnProfileGridDataSource.read();
                }
            };

            return vm.machineSpecListDataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                schema: {
                    data: "data",
                    total: "total",
                    model: {
                        id: "GMT_SW_MCN_SPEC_ID",
                        fields: {
                            SW_MCN_SPEC: { type: "string", editable: false }
                        }
                    }
                },
                pageSize: 100,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/gmt/GmtMcn/GetSwMcnSpecList?pSW_MCN_SPEC=' + (vm.form.SW_MCN_SPEC);
                        url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;

                        console.log(url);

                        return GarmentsDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });                       
                    }
                }
            });
        };

        //vm.onChangeMachineSpecList = function (dataItem) {

        //    var item = angular.copy(dataItem);
        //    vm.form = item;
           
        //    console.log(dataItem);
        //};

        function getSwMcnTypeList() {

            vm.mcTypeOption = {
                optionLabel: "--- Select ---",
                filter: "contains",
                autoBind: true,                
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);                   
                },
                dataBound: function (e) {
                    if (formData[key]) {
                        //alert(formData['GMT_SW_MCN_TYP_ID']);
                        vm.form.GMT_SW_MCN_TYP_ID = formData['GMT_SW_MCN_TYP_ID'];
                    }
                },
                dataTextField: "SW_MCN_TYP_NAME_EN",
                dataValueField: "GMT_SW_MCN_TYP_ID"
            };

            return GarmentsDataService.getDataByFullUrl('/api/gmt/GmtMcn/GetSwMcnType').then(function (res) {
                vm.mcTypeDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });
            });
        }
        
        function getSwMcnBedTypeList() {

            vm.mcBedTypeOption = {
                optionLabel: "--- Select ---",
                filter: "contains",
                autoBind: true,
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                },
                //dataBound: function (e) {                    
                //},
                dataTextField: "SM_BED_TYP_NAME_EN",
                dataValueField: "GMT_SM_BED_TYP_ID"
            };

            return GarmentsDataService.getDataByFullUrl('/api/gmt/GmtMcn/GetSwMcnBedType').then(function (res) {
                vm.mcBedTypeDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });
            });
        }
        
        function getStitchTypeList() {

            vm.mcStitchTypeOption = {
                optionLabel: "--- Select ---",
                filter: "contains",
                autoBind: true,
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                },
                //dataBound: function (e) {                    
                //},
                dataTextField: "STCH_TYP_NAME_EN",
                dataValueField: "GMT_STCH_TYP_ID"
            };

            return GarmentsDataService.getDataByFullUrl('/api/gmt/GmtMcn/GetGmtStitchType').then(function (res) {
                vm.mcStitchTypeDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });
            });
        }
     
        function getSwAppTypeList() {

            vm.mcSwAppTypeOption = {
                optionLabel: "--- Select ---",
                filter: "contains",
                autoBind: true,
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                },
                //dataBound: function (e) {                    
                //},
                dataTextField: "SW_APP_TYP_NAME_EN",
                dataValueField: "GMT_SW_APP_TYP_ID"
            };

            return GarmentsDataService.getDataByFullUrl('/api/gmt/GmtMcn/GetSwAppType').then(function (res) {
                vm.mcSwAppTypeDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });
            });
        }






        //=========== Start Thread Consumetion Detail ===========
        function getThrdTypeList() {

            vm.mcThrdTypeOption = {
                optionLabel: "--- Select ---",
                filter: "contains",
                autoBind: true,
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.thrdConsForm.THRD_TYPE_NAME = item.LK_DATA_NAME_EN;
                },
                //dataBound: function (e) {                    
                //},
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };

            return GarmentsDataService.LookupListData(121).then(function (res) {
                vm.mcThrdTypeDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });
            });
        }

        function getMOUList() {
            vm.thrdConsMouOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.thrdConsForm.DIA_MOU_CODE = item.MOU_CODE;
                }//,
                //dataBound: function (e) {
                //    vm.thrdConsForm['DIA_MOU_ID'] = 23;
                //}
            };

            
            return GarmentsDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {

                vm.thrdConsMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });                
            });

        };

        vm.thrdConsReset = function () {
            var data = angular.copy(vm.thrdConsForm);
            vm.thrdConsForm = { GMT_SM_THRD_CONS_ID: 0, GMT_SW_MCN_SPEC_ID: data.GMT_SW_MCN_SPEC_ID, LK_THRD_TYPE_ID: data.LK_THRD_TYPE_ID, CONS_MOU_ID: data.CONS_MOU_ID };
        }

        vm.addThrdConsRow = function (data) {
            vm.thrdConsGridDataSource.insert(0, data);
            vm.thrdConsReset();
        }

        vm.editThrdConsRow = function (data) {
            vm.thrdConsForm = angular.copy(data);
        }

        vm.updateThrdConsRow = function (data) {
            
            var dataItem = vm.thrdConsGridDataSource.getByUid(data.uid);

            console.log(dataItem);

            dataItem.set('GMT_SM_THRD_CONS_ID', vm.thrdConsForm.GMT_SM_THRD_CONS_ID);
            dataItem.set('GMT_SW_MCN_SPEC_ID', vm.thrdConsForm.GMT_SW_MCN_SPEC_ID);
            dataItem.set('LK_THRD_TYPE_ID', vm.thrdConsForm.LK_THRD_TYPE_ID);
            dataItem.set('THRD_COUNT', vm.thrdConsForm.THRD_COUNT);
            dataItem.set('THRD_CONS', vm.thrdConsForm.THRD_CONS);
            dataItem.set('CONS_MOU_ID', vm.thrdConsForm.CONS_MOU_ID);
            dataItem.set('PCT_CONS', vm.thrdConsForm.PCT_CONS);

            vm.thrdConsReset();
        }

        vm.removeThrdConsRow = function (dataItem) {
            vm.thrdConsGridDataSource.remove(dataItem);
        }

        vm.thrdConsGridOption = {
            height: 160,
            sortable: true,
            columns: [
                { field: "THRD_TYPE_NAME", title: "Type", width: "25%" },
                { field: "THRD_COUNT", title: "No of Needle", width: "15%" },
                { field: "THRD_CONS", title: "Cons", width: "15%" },
                { field: "CONS_MOU_CODE", title: "MoU", width: "15%" },
                { field: "PCT_CONS", title: "Cons Pct", width: "15%" },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editThrdConsRow(dataItem)'><i class='fa fa-edit'></i></button>&nbsp;" +
                            "<button type='button' class='btn btn-xs red' ng-click='vm.removeThrdConsRow(dataItem)'><i class='fa fa-remove'></i></button>";
                    },
                    width: "15%"
                }
            ]
        };

        vm.thrdConsGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    //e.success([]);
                    return GarmentsDataService.getDataByFullUrl('/api/gmt/GmtMcn/GetThrdConsList?pGMT_SW_MCN_SPEC_ID=' + ($stateParams.pGMT_SW_MCN_SPEC_ID || vm.form.GMT_SW_MCN_SPEC_ID || 0)).then(function (res) {

                        if (res.length > 0) {                            
                            e.success(res);
                        }
                        else {
                            e.success([]);
                        }

                    }, function (err) {
                        console.log(err);
                    });
                }
            },
            schema: {
                model: {
                    id: "GMT_SM_THRD_CONS_ID",
                    fields: {
                        //SC_START_DT: { type: "date", editable: false },
                        //SC_END_DT: { type: "date", editable: false }
                    }
                }
            }
        });
        //=========== End Thread Consumetion Detail ===========




        // ============= Start Machine Guide Detail =========== 
        function getMcnGuideTypeList() {

            vm.mcGuideTypeOption = {
                optionLabel: "--- Select ---",
                filter: "contains",
                autoBind: true,
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.mcGuideForm.SM_GUIDE_TYP_NAME_EN = item.SM_GUIDE_TYP_NAME_EN;
                },
                //dataBound: function (e) {                    
                //},
                dataTextField: "SM_GUIDE_TYP_NAME_EN",
                dataValueField: "GMT_SM_GUIDE_TYP_ID"
            };

            return GarmentsDataService.getDataByFullUrl('/api/gmt/GmtMcn/GetSwMcnGuideType').then(function (res) {
                vm.mcGuideTypeDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });
            });
        }
       
        vm.mcnGuideReset = function () {
            var data = angular.copy(vm.mcGuideForm);
            vm.mcGuideForm = { GMT_SW_MCN_GUIDE_ID: 0, MCN_GUIDE_DESC: '', GMT_SM_GUIDE_TYP_ID: data.GMT_SM_GUIDE_TYP_ID };
        }

        vm.addMcnGuideRow = function (data) {
            vm.mcnGuideGridDataSource.insert(0, data);
            vm.mcnGuideReset();
        }

        vm.editMcnGuideRow = function (data) {
            vm.mcGuideForm = angular.copy(data);
        }

        vm.updateMcnGuideRow = function (data) {

            var dataItem = vm.mcnGuideGridDataSource.getByUid(data.uid);

            console.log(dataItem);

            dataItem.set('GMT_SW_MCN_GUIDE_ID', vm.mcGuideForm.GMT_SW_MCN_GUIDE_ID);
            dataItem.set('GMT_SW_MCN_SPEC_ID', vm.mcGuideForm.GMT_SW_MCN_SPEC_ID);
            dataItem.set('MCN_GUIDE_DESC', vm.mcGuideForm.MCN_GUIDE_DESC);
            dataItem.set('GMT_SM_GUIDE_TYP_ID', vm.mcGuideForm.GMT_SM_GUIDE_TYP_ID);
            dataItem.set('SM_GUIDE_TYP_NAME_EN', vm.mcGuideForm.SM_GUIDE_TYP_NAME_EN);
            
            vm.mcnGuideReset();
        }

        vm.removeMcnGuideRow = function (dataItem) {
            vm.mcnGuideGridDataSource.remove(dataItem);
        }

        vm.mcnGuideGridOption = {
            height: 160,
            sortable: true,
            columns: [
                { field: "SM_GUIDE_TYP_NAME_EN", title: "Type", width: "20%" },
                { field: "MCN_GUIDE_DESC", title: "Description", width: "65%" },                
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editMcnGuideRow(dataItem)'><i class='fa fa-edit'></i></button>&nbsp;" +
                            "<button type='button' class='btn btn-xs red' ng-click='vm.removeMcnGuideRow(dataItem)'><i class='fa fa-remove'></i></button>";
                    },
                    width: "15%"
                }
            ]
        };

        vm.mcnGuideGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    //e.success([]);
                    return GarmentsDataService.getDataByFullUrl('/api/gmt/GmtMcn/GetMcnGuideList?pGMT_SW_MCN_SPEC_ID=' + ($stateParams.pGMT_SW_MCN_SPEC_ID || vm.form.GMT_SW_MCN_SPEC_ID || 0)).then(function (res) {

                        if (res.length > 0) {
                            e.success(res);
                        }
                        else {
                            e.success([]);
                        }

                    }, function (err) {
                        console.log(err);
                    });
                }
            },
            schema: {
                model: {
                    id: "GMT_SW_MCN_GUIDE_ID",
                    fields: {
                        //SC_START_DT: { type: "date", editable: false },
                        //SC_END_DT: { type: "date", editable: false }
                    }
                }
            }
        });
        // ============= End Machine Guide Detail =========== 




        // ============= Start Machine Profile Detail ===========
        vm.regDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.regDateOpened = true;
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
                    vm.mcProfileForm.SUP_TRD_NAME_EN = item.SUP_TRD_NAME_EN;
                }
            };

            return vm.supplierDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        GarmentsDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=447').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getBrandList() {
            vm.brandOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "BRAND_NAME_EN",
                dataValueField: "RF_BRAND_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.mcProfileForm.BRAND_NAME_EN = item.BRAND_NAME_EN;
                }
            };

            return vm.brandDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        GarmentsDataService.getDataByUrlNoToken('/api/common/GetItemBrandList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getCountryList() {
            vm.countryOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COUNTRY_NAME_EN",
                dataValueField: "HR_COUNTRY_ID"
            };

            return vm.countryDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        GarmentsDataService.getDataByUrlNoToken('/api/common/GetCountryList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getProdFloorList() {
            vm.prodFloorOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FLOOR_DESC_EN",
                dataValueField: "HR_PROD_FLR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.mcProfileForm.FLOOR_DESC_EN = item.FLOOR_DESC_EN;
                }
            };

            return vm.prodFloorDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        GarmentsDataService.getDataByFullUrl('/api/hr/GetProdFloorList?pLK_PFLR_TYP_ID=532').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                group: { field: "BLDNG_DESC_EN" },
                sort: [{ field: 'HR_PROD_BLDNG_ID', dir: 'asc' }]
            });
        }

        function getMcnStatusList() {

            vm.mcnStatusOption = {
                optionLabel: "--- Select ---",
                filter: "contains",
                autoBind: true,
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.mcProfileForm.MCN_STATUS_NAME = item.LK_DATA_NAME_EN;
                },
                //dataBound: function (e) {                    
                //},
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };

            return GarmentsDataService.LookupListData(122).then(function (res) {
                vm.mcnStatusDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });
            });
        }        

        vm.mcnProfileReset = function () {
            var data = angular.copy(vm.mcProfileForm);
            vm.mcProfileForm = { 
                GMT_SW_MACHINE_ID: 0, MC_CODE_NO: '', SCM_SUPPLIER_ID: data.SCM_SUPPLIER_ID, RF_BRAND_ID: data.RF_BRAND_ID, C_ORIGIN_ID: data.C_ORIGIN_ID,
                HR_PROD_FLR_ID: data.HR_PROD_FLR_ID, MC_SERIAL_NO: '', MODEL_NO: '', REG_DT: '', IS_HIRED: 'N', REMARKS: '', LK_MAC_STATUS_ID: 607,
                SUP_TRD_NAME_EN: '', BRAND_NAME_EN: '', FLOOR_DESC_EN: ''
            };
        }

        vm.addMcnProfileRow = function (data) {
            vm.mcnProfileGridDataSource.insert(0, data);
            vm.mcnProfileReset();
        }

        vm.editMcnProfileRow = function (data) {
            vm.mcProfileForm = angular.copy(data);
        }

        vm.updateMcnProfileRow = function (data) {

            var dataItem = vm.mcnProfileGridDataSource.getByUid(data.uid);

            console.log(dataItem);

            dataItem.set('GMT_SW_MACHINE_ID', vm.mcProfileForm.GMT_SW_MACHINE_ID);
            dataItem.set('GMT_SW_MCN_SPEC_ID', vm.mcProfileForm.GMT_SW_MCN_SPEC_ID);
            dataItem.set('MC_CODE_NO', vm.mcProfileForm.MC_CODE_NO);
            dataItem.set('SCM_SUPPLIER_ID', vm.mcProfileForm.SCM_SUPPLIER_ID);
            dataItem.set('RF_BRAND_ID', vm.mcProfileForm.RF_BRAND_ID);

            dataItem.set('MC_SERIAL_NO', vm.mcProfileForm.MC_SERIAL_NO);
            dataItem.set('MODEL_NO', vm.mcProfileForm.MODEL_NO);
            dataItem.set('C_ORIGIN_ID', vm.mcProfileForm.C_ORIGIN_ID);
            dataItem.set('REG_DT', vm.mcProfileForm.REG_DT);
            dataItem.set('HR_PROD_FLR_ID', vm.mcProfileForm.HR_PROD_FLR_ID);

            dataItem.set('IS_HIRED', vm.mcProfileForm.IS_HIRED);
            dataItem.set('REMARKS', vm.mcProfileForm.REMARKS);
            dataItem.set('LK_MAC_STATUS_ID', vm.mcProfileForm.LK_MAC_STATUS_ID);

            dataItem.set('SUP_TRD_NAME_EN', vm.mcProfileForm.SUP_TRD_NAME_EN);
            dataItem.set('BRAND_NAME_EN', vm.mcProfileForm.BRAND_NAME_EN);
            dataItem.set('FLOOR_DESC_EN', vm.mcProfileForm.FLOOR_DESC_EN);

            vm.mcnProfileReset();
        }

        vm.removeMcnProfileRow = function (dataItem) {
            vm.mcnProfileGridDataSource.remove(dataItem);
        }

        vm.mcnProfileGridOption = {
            height: 200,
            sortable: true,
            columns: [
                { field: "MC_CODE_NO", title: "M/C Code#", width: "15%" },
                { field: "SUP_TRD_NAME_EN", title: "Supplier", width: "25%" },
                { field: "BRAND_NAME_EN", title: "Brand", width: "15%" },
                { field: "FLOOR_DESC_EN", title: "Floor", width: "10%" },
                { field: "MC_SERIAL_NO", title: "Serial#", width: "0%", hidden: true },
                { field: "MODEL_NO", title: "Model", width: "0%", hidden: true },
                { field: "REMARKS", title: "Remarks", width: "25%" },                
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editMcnProfileRow(dataItem)'><i class='fa fa-edit'></i></button>&nbsp;" +
                            "<button type='button' class='btn btn-xs red' ng-click='vm.removeMcnProfileRow(dataItem)'><i class='fa fa-remove'></i></button>";
                    },
                    width: "10%"
                }
            ]
        };

        vm.mcnProfileGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    //e.success([]);
                    return GarmentsDataService.getDataByFullUrl('/api/gmt/GmtMcn/GetMcnProfileList?pGMT_SW_MCN_SPEC_ID=' + ($stateParams.pGMT_SW_MCN_SPEC_ID || vm.form.GMT_SW_MCN_SPEC_ID || 0)).then(function (res) {

                        if (res.length > 0) {
                            e.success(res);
                        }
                        else {
                            e.success([]);
                        }

                    }, function (err) {
                        console.log(err);
                    });
                }
            },
            schema: {
                model: {
                    id: "GMT_SW_MACHINE_ID",
                    fields: {                        
                        REG_DT: { type: "date", editable: false }
                    }
                }
            }
        });
        // ============= End Machine Profile Detail ===========


        vm.save = function () {

            var submitData = angular.copy(vm.form);
            

            var thrdConsData = vm.thrdConsGridDataSource.data();
            var mcnGuideData = vm.mcnGuideGridDataSource.data();
            var mcnProfileData = vm.mcnProfileGridDataSource.data();
                        
            submitData.THRD_CONS_DTL_XML = GarmentsDataService.xmlStringShort(thrdConsData.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            submitData.MCN_GUIDE_DTL_XML = GarmentsDataService.xmlStringShort(mcnGuideData.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            submitData.MCN_PROFILE_DTL_XML = GarmentsDataService.xmlStringShort(mcnProfileData.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            
            console.log(submitData);
            //return;

            Dialog.confirm('Do you want save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    //console.log(submitData);

                    return GarmentsDataService.saveDataByFullUrl(submitData, '/api/gmt/GmtMcn/SaveMcnSpec').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                
                                if (res.data.PGMT_SW_MCN_SPEC_ID_RTN != 0) {
                                    $stateParams.pGMT_SW_MCN_SPEC_ID = res.data.PGMT_SW_MCN_SPEC_ID_RTN;

                                    vm.form.GMT_SW_MCN_SPEC_ID = res.data.PGMT_SW_MCN_SPEC_ID_RTN;

                                    $state.go('SwMcnSpec', { pGMT_SW_MCN_SPEC_ID: res.data.PGMT_SW_MCN_SPEC_ID_RTN }, { notify: false });

                                    vm.thrdConsGridDataSource.read();

                                    if (submitData['GMT_SW_MCN_SPEC_ID'] < 1) {
                                        vm.machineSpecListDataSource.read();
                                    }
                                    vm.mcnGuideGridDataSource.read();
                                    vm.mcnProfileGridDataSource.read();
                                }

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

        vm.reset = function () {
            var data = angular.copy(vm.form);

            vm.form = {
                GMT_SW_MCN_SPEC_ID: 0, SW_MCN_SPEC: '', SW_MCN_SPEC_SHORT: '', GMT_SW_MCN_TYP_ID: data.GMT_SW_MCN_TYP_ID, GMT_SM_BED_TYP_ID: data.GMT_SM_BED_TYP_ID,
                GMT_STCH_TYP_ID: data.GMT_STCH_TYP_ID, GMT_SW_APP_TYP_ID: data.GMT_SW_APP_TYP_ID
            };
            $stateParams.pGMT_SW_MCN_SPEC_ID = 0;
            $state.go('SwMcnSpec', { pGMT_SW_MCN_SPEC_ID: 0 }, { notify: false });

            vm.thrdConsGridDataSource.read();            
            vm.mcnGuideGridDataSource.read();
            vm.mcnProfileGridDataSource.read();
        }



    }
})();
//=============== End for GmtSwMcnSpecController =================

