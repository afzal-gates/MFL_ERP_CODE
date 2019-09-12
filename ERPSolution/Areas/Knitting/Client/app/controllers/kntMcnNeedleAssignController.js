(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntMcnNeedleAssignController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', 'cur_user_id', KntMcnNeedleAssignController]);
    function KntMcnNeedleAssignController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog, cur_user_id) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log(cur_user_id);
        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
        
        vm.IS_FINALIZED = 'N';
        vm.IS_NEXT = 'Y';
        vm.isAddToGrid = 'Y';
        vm.updateGridIndex = null;

        var key = 'KNT_MCN_NDL_ASSIGN_ID';
        vm.today = new Date();
        vm.form = { KNT_MCN_NDL_ASSIGN_ID: 0, MCN_NDL_ASSIGN_XML: '' };
        
        
        //$('#FAB_ROLL_NO').focus();

        //$("input[type=text]").focus(function () { $(this).select(); }).mouseup(
        //   function (e) {
        //       e.preventDefault();
        //   });


        vm.errstyle = { 'border': '1px solid #f13d3d' };
        var needleData = [];


        activate();
        function activate() {
            var promise = [getOfficeList(), getCategoryList(), getNeedleList()];
            return $q.all(promise).then(function () {
               
                vm.showSplash = false;                
            });
        }

        
                
        function getOfficeList() {
            vm.officeOptions = {
                optionLabel: "-- Select Office --",
                filter: "contains",
                autoBind: true,
                dataTextField: "OFFICE_NAME_EN",
                dataValueField: "HR_OFFICE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.officeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/OfficeList';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {                           
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });            
        };

        function getCategoryList() {
            vm.categoryOptions = {
                optionLabel: "-- Select Category --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    //vm.form.INV_ITEM_CAT_ID = item.INV_ITEM_CAT_ID;

                    //vm.itemDataSource.read();

                }
            };

            return vm.categoryDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/inv/ItemCategory/SelectAll';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);

                            var list = _.filter(res, function (o) {
                                return (o.INV_ITEM_CAT_ID === 128 || o.INV_ITEM_CAT_ID === 211)
                            });
                            e.success(list);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getNeedleList() {           
            return vm.itemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (vm.form.INV_ITEM_CAT_ID || 0);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);                            
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }
        
        $scope.$watchGroup(['vm.form.HR_OFFICE_ID', 'vm.form.INV_ITEM_CAT_ID'], function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    vm.IS_NEXT = 'N';
                }
            }
        }, true);


        function NeedleDropDownEditor(container, options) {
            //if (options.model.PROMOTED_DISABLED) {
            //    $("<span>" + options.model.get(options.field).DESIGNATION_NAME_EN + "</span>").appendTo(container);
            //    return;
            //};

            $('<input data-text-field="ITEM_NAME_EN" data-value-field="INV_ITEM_ID" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    optionLabel: "--N/A--",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {                                
                                //console.log(needleData);
                                e.success(vm.itemDataSource.data());
                            }
                        }
                    },
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        console.log(item);
                        options.model.INV_ITEM_ID = item.INV_ITEM_ID;
                       
                        options.model.INV_ITEM = { INV_ITEM_ID: item.INV_ITEM_ID, ITEM_NAME_EN: item.ITEM_NAME_EN };

                        //var grid = $("#mcNdlAssign").data("kendoGrid");
                        //grid.refresh();
                    }
                });
        }
        
        vm.mcNdlAssignOptions = {
            height: 450,
            sortable: true,
            pageable: false,
            editable: true,
            //selectable: "row",
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
                { field: "KNT_MACHINE_NO", title: "M/C No", type: "string", width: "10%", editable: false },
                { field: "MC_DIA", title: "M/C Dia", type: "string", width: "10%", filterable: false, editable: false },
                { field: "MC_GG", title: "M/C GG", type: "string", width: "10%", filterable: false, editable: false },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "15%", filterable: false, editable: false },
                { field: "MC_TYPE_NAME_EN", title: "M/C Type", type: "string", width: "15%", filterable: false, editable: false },
                { field: "ITEM_NAME_EN", title: "Item", type: "string", width: "20%", filterable: false, editor: NeedleDropDownEditor, template: "#=INV_ITEM.ITEM_NAME_EN#" },
                //{ field: "ASSIGN_QTY", title: "Assign Qty", type: "number", width: "10%", filterable: false, editable: false },
                {
                    title: "Assign Qty",
                    field: "ASSIGN_QTY",                    
                    template: function () {
                        return "<ng-form name='frmAssignQty'><input type='number' class='form-control' name='ASSIGN_QTY' ng-model='dataItem.ASSIGN_QTY' min='1' ng-disabled='dataItem.IS_FINALIZED==\"Y\"' /></ng-form>";
                    },
                    width: "10%",
                    filterable: false
                },                
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.addRow(dataItem)' ng-disabled='dataItem.IS_FINALIZED==\"Y\"'><i class='fa fa-plus'></i></button>&nbsp;" +
                            "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='dataItem.IS_FINALIZED==\"Y\"'><i class='fa fa-remove'></i></button>&nbsp;";
                    },
                    width: "10%"
                }
            ]            
        };

       
        vm.mcNdlAssignDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {                                        
                    var url = '/api/knit/McNeedleAssign/GetNeedleAssignList?pHR_OFFICE_ID=' + (vm.form.HR_OFFICE_ID || 0) + '&pINV_ITEM_CAT_ID=' + (vm.form.INV_ITEM_CAT_ID || 0);
                                        
                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        var list = _.filter(res, function (ob) {
                            return ob.IS_FINALIZED != 'Y';
                        });

                        if (list.length > 0) {
                            vm.IS_FINALIZED = 'N';
                        }
                        else {
                            vm.IS_FINALIZED = 'Y';
                        }

                        e.success(res);
                    });                    
                }               
            },
            schema: {                
                model: {
                    id: "KNT_MCN_NDL_ASSIGN_ID",
                    fields: {
                        KNT_MACHINE_NO: { type: "string", editable: false },
                        MC_DIA: { type: "string", editable: false },
                        MC_GG: { type: "string", editable: false },
                        BRAND_NAME_EN: { type: "string", editable: false },                        
                        MC_TYPE_NAME_EN: { type: "string", editable: false },
                        INV_ITEM: { defaultValue: { INV_ITEM_ID: "", ITEM_NAME_EN: "--N/A--" }, editable: true }
                    }
                }
            }
        });
        
        
        vm.addRow = function (dataItem) {
            var data = {
                KNT_MCN_NDL_ASSIGN_ID: 0, KNT_MACHINE_ID: dataItem.KNT_MACHINE_ID, INV_ITEM_ID: '', ASSIGN_QTY: null, QTY_MOU_ID: 1, IS_FINALIZED: 'N', KNT_MACHINE_NO: dataItem.KNT_MACHINE_NO, MC_DIA: dataItem.MC_DIA, MC_GG: dataItem.MC_GG,
                BRAND_NAME_EN: dataItem.BRAND_NAME_EN, INV_ITEM: { INV_ITEM_ID: "", ITEM_NAME_EN: "--N/A--" }
            };

            //data['uid'] = null;
            //data['KNT_MCN_NDL_ASSIGN_ID'] = 0;
            //data['INV_ITEM_ID'] = '';
            //data['INV_ITEM'] = { INV_ITEM_ID: "", ITEM_NAME_EN: "--N/A--" };
            //data['ASSIGN_QTY'] = null;

            vm.IS_FINALIZED = 'N';
            vm.mcNdlAssignDataSource.insert(0, data);
        }

        vm.removeRow = function (dataItem) {
            vm.mcNdlAssignDataSource.remove(dataItem);
        };
        
        vm.submitData = function (token, pIS_FINALIZED) {
            var submitRcvData = angular.copy(vm.form);
            var vMsg = 'Do you want to save?';

                                  
            if (pIS_FINALIZED == 'Y') {
                vMsg = 'Do you want to save and finalize?';
            }

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {
                
                submitRcvData['IS_FINALIZED'] = pIS_FINALIZED;
                //submitRcvData['TRAN_REF_DT'] = $filter('date')(vm.form.TRAN_REF_DT, vm.dtFormat);

                var rcvData = vm.mcNdlAssignDataSource.data();
                console.log(rcvData);
            

                submitRcvData.MCN_NDL_ASSIGN_XML = KnittingDataService.xmlStringShort(rcvData.map(function (ob) {
                    return ob;
                }));

                console.log(submitRcvData);

                return KnittingDataService.saveDataByUrl(submitRcvData, '/McNeedleAssign/BatchSave').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            
                            vm.mcNdlAssignDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                });
                                
            });
        };

        vm.backToList = function () {
            return $state.go('KntMcNeedleBrokenList');
        };

        vm.cancel = function () {
            vm.mcNdlAssignDataSource.read();
        };

        vm.loadNdlAssignList = function () {
            vm.IS_NEXT = 'Y';

            vm.itemDataSource.read();
            vm.mcNdlAssignDataSource.read();
        };
       
        vm.printChallan = function () {
            //console.log(dataItem);

            vm.isRDLC = true;
            vm.form.REPORT_CODE = 'RPT-3515';
            
            if (vm.form.SCM_STORE_ID == null || vm.form.SCM_STORE_ID == '') {
                vm.form.SCM_STORE_ID = -1;
            }

            var url;
            if (vm.isRDLC == true) {
                url = '/api/Knit/KntReport/PreviewReportRDLC';
            }
            else {
                url = '/api/Knit/KntReport/PreviewReport';
            }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.FROM_DATE = $filter('date')(vm.form.RCV_DT, vm.dtFormat);
            
            var params = angular.copy(vm.form);

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

       

        vm.issueRequsition = function () {
            return $state.go('KntMcOilReqIss4SubStr', { pSCM_STR_OIL_REQ_H_ID: vm.form.SCM_STR_OIL_REQ_H_ID });
        };

    }

})();

