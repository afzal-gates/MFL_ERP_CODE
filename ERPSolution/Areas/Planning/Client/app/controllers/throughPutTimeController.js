//=============== Start for ThroughPutTimeController =================
(function () {
    'use strict';
    angular.module('multitex.planning').controller('ThroughPutTimeController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$filter', '$modal', '$timeout', ThroughPutTimeController]);
    function ThroughPutTimeController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $filter, $modal, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        var prodTypeList = [];
        
        vm.form = {};
        
        //$scope.form = formData[key] ? formData : {
        //    GMT_SW_MCN_SPEC_ID: 0, SW_MCN_SPEC: ''
        //};

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };
        
              
       
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getProdTypeList(), getCpmEffList()];
            return $q.all(promise).then(function () {
               
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
        
     
        function getProdTypeList() {
            //vm.prodTypeOption = {
            //    optionLabel: "-- Select --",
            //    filter: "contains",
            //    autoBind: true,
            //    dataTextField: "PRODUCT_TYP_NAME_EN",
            //    dataValueField: "GMT_PRODUCT_TYP_ID",
            //    select: function (e) {
            //        var item = this.dataItem(e.item);

            //        //vm.form.GMT_PRODUCT_TYP_ID = item.GMT_PRODUCT_TYP_ID;
            //        //$stateParams.pGMT_PRODUCT_TYP_ID = item.GMT_PRODUCT_TYP_ID;
            //    },
            //    dataBound: function (e) {
            //        //if ($stateParams.pGMT_PRODUCT_TYP_ID) {
            //        //    vm.form.GMT_PRODUCT_TYP_ID = $stateParams.pGMT_PRODUCT_TYP_ID;
            //        //}
            //    }
            //};

            //return vm.prodTypeDataSource = new kendo.data.DataSource({
            //    transport: {
            //        read: function (e) {
            //            PlanningDataService.getDataByFullUrl('/api/pln/ProdCapcti/GetProdTypeList').then(function (res) {
            //                e.success(res);
            //            });
            //        }
            //    }
            //});

            return PlanningDataService.getDataByFullUrl('/api/pln/ProdCapcti/GetProdTypeList').then(function (res) {
                prodTypeList = res;
            });
        };
        
        function FrmProdTypeDropDownEditor(container, options) {
            //if (options.model.PROMOTED_DISABLED) {
            //    $("<span>" + options.model.get(options.field).DESIGNATION_NAME_EN + "</span>").appendTo(container);
            //    return;
            //};
            
            $('<input data-text-field="PRODUCT_TYP_NAME_EN" data-value-field="GMT_PRODUCT_TYP_ID" data-bind="value:' + options.field + '"  />')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    optionLabel: "--N/A--",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {                                
                                //console.log(prodTypeList);
                                e.success(prodTypeList);
                            }
                        }
                    },
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        console.log(item);

                        options.model.FRM_PROD_TYP_ID = item.GMT_PRODUCT_TYP_ID;
                        options.model.FRM_PRODUCT_TYP = { FRM_PROD_TYP_ID: item.GMT_PRODUCT_TYP_ID, FROM_PRODUCT_TYP_NAME_EN: item.PRODUCT_TYP_NAME_EN };
                        
                        var grid = $("#thrghPutTimeGrid").data("kendoGrid");
                        grid.refresh();
                    }
                });
        }

        function ToProdTypeDropDownEditor(container, options) {
            //if (options.model.PROMOTED_DISABLED) {
            //    $("<span>" + options.model.get(options.field).DESIGNATION_NAME_EN + "</span>").appendTo(container);
            //    return;
            //};

            $('<input data-text-field="PRODUCT_TYP_NAME_EN" data-value-field="GMT_PRODUCT_TYP_ID" data-bind="value:' + options.field + '"  />')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    optionLabel: "--N/A--",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                //console.log(prodTypeList);
                                e.success(prodTypeList);
                            }
                        }
                    },
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        console.log(item);

                        options.model.TO_PROD_TYP_ID = item.GMT_PRODUCT_TYP_ID;
                        options.model.TO_PRODUCT_TYP = { TO_PROD_TYP_ID: item.GMT_PRODUCT_TYP_ID, TO_PRODUCT_TYP_NAME_EN: item.PRODUCT_TYP_NAME_EN };

                        var grid = $("#thrghPutTimeGrid").data("kendoGrid");
                        grid.refresh();
                    }
                });
        }
        
        vm.thrghPutTimeGridOption = {
            height: 420,
            sortable: false,
            scrollable: true,
            pageable: false,
            editable: true,
            //selectable: "cell",
            columns: [                                
                //{
                //    title: "Type ID",
                //    field: "GMT_PRODUCT_TYP_ID",
                //    template: function () {
                //        return "<ng-form name='frmGMT_PRODUCT_TYP_ID'> <input type='number' class='form-control' name='GMT_PRODUCT_TYP_ID' ng-model='dataItem.GMT_PRODUCT_TYP_ID' required min='0' ng-style='frmGMT_PRODUCT_TYP_ID.GMT_PRODUCT_TYP_ID.$error.required||frmGMT_PRODUCT_TYP_ID.GMT_PRODUCT_TYP_ID.$error.min? vm.errstyle:\"\"'  /> </ng-form>";
                //    },
                //    width: "15%",
                //    filterable: false,
                //    hidden: true
                //},
                {
                    field: "FRM_PRODUCT_TYP", title: "From Prod. Type", width: "15%", filterable: false, editor: FrmProdTypeDropDownEditor, template: "#=FRM_PRODUCT_TYP.FROM_PRODUCT_TYP_NAME_EN#",
                    attributes: {
                        "ng-style": "{{this.dataItem.FRM_PROD_TYP_ID<1 ? vm.errstyle:''}}",
                        "ng-required": "true"
                    }
                },
                {
                    field: "TO_PRODUCT_TYP", title: "To Prod. Type", width: "15%", filterable: false, editor: ToProdTypeDropDownEditor, template: "#=TO_PRODUCT_TYP.TO_PRODUCT_TYP_NAME_EN#",
                    attributes: {
                        "ng-style": "{{this.dataItem.TO_PROD_TYP_ID<1 ? vm.errstyle:''}}",
                        "ng-required": "true"
                    }
                },
                {
                    title: "Layout Time New",
                    headerAttributes: {
                        "class": "table-header-cell",
                        style: "text-align: center;"
                    }, 
                    columns: [
                        {
                            title: "Hour",
                            field: "THRPT_HR_FRS",
                            template: function () {
                                return "<ng-form name='frmTHRPT_HR_FRS'> <input type='number' class='form-control' name='THRPT_HR_FRS' ng-model='dataItem.THRPT_HR_FRS' required min='0' ng-style='frmTHRPT_HR_FRS.THRPT_HR_FRS.$error.required||frmTHRPT_HR_FRS.THRPT_HR_FRS.$error.min? vm.errstyle:\"\"'  /> </ng-form>";
                            },
                            width: "15%",
                            filterable: false
                        },
                        {
                            title: "Minute",
                            field: "THRPT_MIN_FRS",
                            template: function () {
                                return "<ng-form name='frmTHRPT_MIN_FRS'> <input type='number' class='form-control' name='THRPT_MIN_FRS' ng-model='dataItem.THRPT_MIN_FRS' required min='0' max='59' ng-style='frmTHRPT_MIN_FRS.THRPT_MIN_FRS.$error.required||frmTHRPT_MIN_FRS.THRPT_MIN_FRS.$error.min||frmTHRPT_MIN_FRS.THRPT_MIN_FRS.$error.max? vm.errstyle:\"\"'  /> </ng-form>";
                            },
                            width: "15%",
                            filterable: false
                        }
                    ]
                },
                {
                    title: "Layout Time Old",
                    headerAttributes: {
                        "class": "table-header-cell",
                        style: "text-align: center;"
                    },
                    columns: [
                        {
                            title: "Hour",
                            field: "THRPT_HR_OLD",
                            template: function () {
                                return "<ng-form name='frmTHRPT_HR_OLD'> <input type='number' class='form-control' name='THRPT_HR_OLD' ng-model='dataItem.THRPT_HR_OLD' required min='0' ng-style='frmTHRPT_HR_OLD.THRPT_HR_OLD.$error.required||frmTHRPT_HR_OLD.THRPT_HR_OLD.$error.min? vm.errstyle:\"\"'  /> </ng-form>";
                            },
                            width: "15%",
                            filterable: false
                        },
                        {
                            title: "Minute",
                            field: "THRPT_MIN_OLD",
                            template: function () {
                                return "<ng-form name='frmTHRPT_MIN_OLD'> <input type='number' class='form-control' name='THRPT_MIN_OLD' ng-model='dataItem.THRPT_MIN_OLD' required min='0' max='59' ng-style='frmTHRPT_MIN_OLD.THRPT_MIN_OLD.$error.required||frmTHRPT_MIN_OLD.THRPT_MIN_OLD.$error.min||frmTHRPT_MIN_OLD.THRPT_MIN_OLD.$error.max? vm.errstyle:\"\"'  /> </ng-form>";
                            },
                            width: "15%",
                            filterable: false
                        }
                    ]
                },
                {
                    title: "StyleChangeOver (Hrs)",
                    field: "SCO_HRS",
                    template: function () {
                        return "<ng-form name='frmSCO_HRS'> <input type='number' class='form-control' name='SCO_HRS' ng-model='dataItem.SCO_HRS' required min='0' max='100' ng-style='frmSCO_HRS.SCO_HRS.$error.required||frmSCO_HRS.SCO_HRS.$error.min||frmSCO_HRS.SCO_HRS.$error.max? vm.errstyle:\"\"'  /> </ng-form>";
                    },
                    width: "15%",
                    filterable: false
                },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.addRow(dataItem)' ><i class='fa fa-plus'></i></button>&nbsp;" +
                            "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='(vm.thrghPutTimeGridDataSource.data().length==1)' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "10%"
                }
            ]
        };
        

        function getCpmEffList() {
            vm.thrghPutTimeGridDataSource = new kendo.data.DataSource({
                batch: true,
                transport: {
                    read: function (e) {
                        //e.success([]);
                        return PlanningDataService.getDataByFullUrl('/api/pln/PlnThrghputTime/GetThrghputTimeList').then(function (res) {

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
                        id: "GMT_THRPT_TIME_ID",
                        fields: {                            
                            FRM_PRODUCT_TYP: { defaultValue: { FRM_PROD_TYP_ID: 0, FROM_PRODUCT_TYP_NAME_EN: "--N/A--" }, editable: true },
                            TO_PRODUCT_TYP: { defaultValue: { TO_PROD_TYP_ID: 0, TO_PRODUCT_TYP_NAME_EN: "--N/A--" }, editable: true }
                        }
                    }
                }
            });
        }

        vm.addRow = function (dataItem) {
            console.log(dataItem);
            //var dataCopy = angular.copy(data);
            
            var data = {
                GMT_THRPT_TIME_ID: 0, FRM_PROD_TYP_ID: dataItem.FRM_PROD_TYP_ID, FROM_PRODUCT_TYP_NAME_EN: dataItem.FROM_PRODUCT_TYP_NAME_EN,
                TO_PROD_TYP_ID: dataItem.TO_PROD_TYP_ID, TO_PRODUCT_TYP_NAME_EN: dataItem.TO_PRODUCT_TYP_NAME_EN,
                FRM_PRODUCT_TYP: { FRM_PROD_TYP_ID: dataItem.FRM_PROD_TYP_ID, FROM_PRODUCT_TYP_NAME_EN: dataItem.FROM_PRODUCT_TYP_NAME_EN },
                TO_PRODUCT_TYP: { TO_PROD_TYP_ID: dataItem.TO_PROD_TYP_ID, TO_PRODUCT_TYP_NAME_EN: dataItem.TO_PRODUCT_TYP_NAME_EN },
                THRPT_HR_FRS: null, THRPT_MIN_FRS: null, THRPT_HR_OLD: null, THRPT_MIN_OLD: null, SCO_HRS: 0
            };

            vm.thrghPutTimeGridDataSource.insert(0, data);
        }

        vm.removeRow = function (dataItem) {
            vm.thrghPutTimeGridDataSource.remove(dataItem);
        };
        

        vm.save = function () {

            var submitData = angular.copy(vm.form);            
            
            var dataList = vm.thrghPutTimeGridDataSource.data();
            submitData['GMT_THRPT_TIME_XML'] = PlanningDataService.xmlStringShort(dataList.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            console.log(submitData);


            Dialog.confirm('Do you want save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return PlanningDataService.saveDataByFullUrl(submitData, '/api/pln/PlnThrghputTime/BatchSave').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                
                                vm.thrghPutTimeGridDataSource.read();
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

    }
})();
//=============== End for ThroughPutTimeController =================
