//=============== Start for OrdVlumWiseCpmEffController =================
(function () {
    'use strict';
    angular.module('multitex.planning').controller('OrdVlumWiseCpmEffController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$filter', '$modal', '$timeout', OrdVlumWiseCpmEffController]);
    function OrdVlumWiseCpmEffController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $filter, $modal, $timeout) {

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
        
        function ProdTypeDropDownEditor(container, options) {
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

                        options.model.GMT_PRODUCT_TYP_ID = item.GMT_PRODUCT_TYP_ID;
                        options.model.PRODUCT_TYP = { GMT_PRODUCT_TYP_ID: item.GMT_PRODUCT_TYP_ID, PRODUCT_TYP_NAME_EN: item.PRODUCT_TYP_NAME_EN };
                        
                        var grid = $("#cpmEffGrid").data("kendoGrid");
                        grid.refresh();
                    }
                });
        }

        
        vm.cpmEffGridOption = {
            height: 420,
            sortable: false,
            scrollable: true,
            pageable: false,
            editable: true,
            //selectable: "cell",
            columns: [                                
                {
                    title: "Type ID",
                    field: "GMT_PRODUCT_TYP_ID",
                    template: function () {
                        return "<ng-form name='frmGMT_PRODUCT_TYP_ID'> <input type='number' class='form-control' name='GMT_PRODUCT_TYP_ID' ng-model='dataItem.GMT_PRODUCT_TYP_ID' required min='0' ng-style='frmGMT_PRODUCT_TYP_ID.GMT_PRODUCT_TYP_ID.$error.required||frmGMT_PRODUCT_TYP_ID.GMT_PRODUCT_TYP_ID.$error.min? vm.errstyle:\"\"'  /> </ng-form>";
                    },
                    width: "15%",
                    filterable: false,
                    hidden: true
                },
                {
                    field: "PRODUCT_TYP", title: "Prod. Type", width: "25%", filterable: false, editor: ProdTypeDropDownEditor, template: "#=PRODUCT_TYP.PRODUCT_TYP_NAME_EN#",
                    attributes: {
                        "ng-style": "{{this.dataItem.GMT_PRODUCT_TYP_ID<1 ? vm.errstyle:''}}",
                        "ng-required": "true"
                    }
                },
                {
                    title: "Order Volume Range",
                    headerAttributes: {
                        "class": "table-header-cell",
                        style: "text-align: center;"
                    }, 
                    columns: [
                        {
                            title: "From",
                            field: "ORDER_VOL_1",
                            template: function () {
                                return "<ng-form name='frmORDER_VOL_1'> <input type='number' class='form-control' name='ORDER_VOL_1' ng-model='dataItem.ORDER_VOL_1' required min='0' ng-style='frmORDER_VOL_1.ORDER_VOL_1.$error.required||frmORDER_VOL_1.ORDER_VOL_1.$error.min? vm.errstyle:\"\"'  /> </ng-form>";
                            },
                            width: "15%",
                            filterable: false
                        },
                        {
                            title: "To",
                            field: "ORDER_VOL_2",
                            template: function () {
                                return "<ng-form name='frmORDER_VOL_2'> <input type='number' class='form-control' name='ORDER_VOL_2' ng-model='dataItem.ORDER_VOL_2' required min='0' ng-style='frmORDER_VOL_2.ORDER_VOL_2.$error.required||frmORDER_VOL_2.ORDER_VOL_2.$error.min? vm.errstyle:\"\"'  /> </ng-form>";
                            },
                            width: "15%",
                            filterable: false
                        }
                    ]
                },
                {
                    title: "Efficiency",
                    field: "TOP_EFICNCY",
                    template: function () {
                        return "<ng-form name='frmTOP_EFICNCY'> <input type='number' class='form-control' name='TOP_EFICNCY' ng-model='dataItem.TOP_EFICNCY' required min='0' max='100' ng-style='frmTOP_EFICNCY.TOP_EFICNCY.$error.required||frmTOP_EFICNCY.TOP_EFICNCY.$error.min||frmTOP_EFICNCY.TOP_EFICNCY.$error.max? vm.errstyle:\"\"' /> </ng-form>";
                    },
                    width: "15%",
                    filterable: false
                },
                {
                    title: "CPM",
                    field: "COST_PER_MIN",
                    template: function () {
                        return "<ng-form name='frmCOST_PER_MIN'> <input type='number' class='form-control' name='COST_PER_MIN' ng-model='dataItem.COST_PER_MIN' required min='0' max='99' ng-style='frmCOST_PER_MIN.COST_PER_MIN.$error.required||frmCOST_PER_MIN.COST_PER_MIN.$error.min||frmCOST_PER_MIN.COST_PER_MIN.$error.max? vm.errstyle:\"\"'  /> </ng-form>";
                    },
                    width: "15%",
                    filterable: false
                },                
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.addRow(dataItem)' ><i class='fa fa-plus'></i></button>&nbsp;" +
                            "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='(vm.cpmEffGridDataSource.data().length==1)' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "15%"
                }
            ]
        };
        

        function getCpmEffList() {
            vm.cpmEffGridDataSource = new kendo.data.DataSource({
                batch: true,
                transport: {
                    read: function (e) {
                        //e.success([]);
                        return PlanningDataService.getDataByFullUrl('/api/pln/PlnOrdVlumCpmEff/GetOrdCpmEffList').then(function (res) {

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
                        id: "GMT_CPM_EFF_ORD_VOL_ID",
                        fields: {                            
                            PRODUCT_TYP: { defaultValue: { GMT_PRODUCT_TYP_ID: 0, PRODUCT_TYP_NAME_EN: "--N/A--" }, editable: true }
                        }
                    }
                }
            });
        }

        vm.addRow = function (dataItem) {
            console.log(dataItem);
            //var dataCopy = angular.copy(data);
            
            var data = {
                GMT_CPM_EFF_ORD_VOL_ID: 0, GMT_PRODUCT_TYP_ID: dataItem.GMT_PRODUCT_TYP_ID, PRODUCT_TYP_NAME_EN: dataItem.PRODUCT_TYP_NAME_EN,
                PRODUCT_TYP: { GMT_PRODUCT_TYP_ID: dataItem.GMT_PRODUCT_TYP_ID, PRODUCT_TYP_NAME_EN: dataItem.PRODUCT_TYP_NAME_EN },
                ORDER_VOL_1: null, ORDER_VOL_2: null, TOP_EFICNCY: null, COST_PER_MIN: null
            };

            vm.cpmEffGridDataSource.insert(0, data);           
        }

        vm.removeRow = function (dataItem) {
            vm.cpmEffGridDataSource.remove(dataItem);
        };
        

        vm.save = function () {

            var submitData = angular.copy(vm.form);            
            
            var dataList = vm.cpmEffGridDataSource.data();
            submitData['GMT_CPM_EFF_ORD_VOL_XML'] = PlanningDataService.xmlStringShort(dataList.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            console.log(submitData);


            Dialog.confirm('Do you want save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return PlanningDataService.saveDataByFullUrl(submitData, '/api/pln/PlnOrdVlumCpmEff/BatchSave').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                
                                vm.cpmEffGridDataSource.read();
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
//=============== End for OrdVlumWiseCpmEffController =================
