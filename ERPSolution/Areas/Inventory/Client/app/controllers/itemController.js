(function () {
    'use strict';
    angular.module('multitex.inventory').controller('ItemListController', ['$q', 'config', 'InventoryDataService', '$stateParams', '$state', '$scope', 'logger', ItemListController]);
    function ItemListController($q, config, InventoryDataService, $stateParams, $state, $scope, logger) {

        var vm = this;
        vm.errors = null;
        vm.Title = $state.current.Title || '';

        vm.form = { PACK_MOU_ID: null, PCT_ADDL_PRICE: 0, IS_WT_MACHINE: 'N', IS_COMMON: 'N' };
        vm.formCopy = {};

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getTreeData(), getMOUList(), getCountryList(), getSourceTypeList(), getItemBrandList(), getCurrencyList(), getItemStatusList(), getItemCategList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        };
        
        function reset() {
            console.log(vm.formCopy);
            var selectDataItem = {
                INV_ITEM_CAT_ID: vm.formCopy.INV_ITEM_CAT_ID, IS_WT_MACHINE: 'N', IS_COMMON: 'N', IS_LEAF: vm.formCopy.IS_LEAF, RF_BRAND_ID: vm.formCopy.RF_BRAND_ID, HR_COUNTRY_ID: vm.formCopy.HR_COUNTRY_ID,
                PURCH_MOU_ID: vm.formCopy.PURCH_MOU_ID, PACK_MOU_ID: vm.formCopy.PACK_MOU_ID, CONS_MOU_ID: vm.formCopy.CONS_MOU_ID, PCT_ADDL_PRICE: 0,
                LK_ITEM_STATUS_ID: vm.formCopy.LK_ITEM_STATUS_ID, RF_CURRENCY_ID: vm.formCopy.RF_CURRENCY_ID, GRS_MOU_ID: vm.formCopy.GRS_MOU_ID, NET_MOU_ID: vm.formCopy.NET_MOU_ID,
                PACK_RATIO_MOU_ID: vm.formCopy.PACK_RATIO_MOU_ID, LK_LOC_SRC_TYPE_ID: vm.formCopy.LK_LOC_SRC_TYPE_ID
            };
            vm.form = selectDataItem;
        };

        vm.TranCancel = function () {
            reset();
        };

        function getGridData() {
            return InventoryDataService.getItemList(vm.form.INV_ITEM_CAT_ID).then(function (res) {
                var dataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    },
                    pageSize: 50,
                });

                $('#itemGrid').data("kendoGrid").setDataSource(dataSource);
            });
        };

        //var createNewObj = function (data) {

        //    if (data[0].items.length > 0) {
        //        angular.forEach(data[0].items, function (val, key) {
        //            if (val.items.length > 0) {
        //                createNewObj([val])
        //            } else {
        //                data[0].items.splice(data[0].items.indexOf(val), 1);
        //            }

        //        })

        //    } else {
        //        data.splice(0, 1);
        //    }
        //    return data;
        //}

        function getTreeData() {
            return InventoryDataService.getLoginUserWiseItemCatTreeList().then(function (res) {
                //console.log('------------------');
                //console.log(res);

                //console.log(createNewObj(res));


                return vm.thingsOptions = {
                    dataSource: res,
                    autoScroll: true,
                    select: function (e) {
                        reset();
                        var dataItem = this.dataItem(e.node);
                        vm.selectedItem = dataItem;
                        vm.form.INV_ITEM_CAT_ID = dataItem.INV_ITEM_CAT_ID;
                        vm.form.IS_LEAF = dataItem.IS_LEAF;
                        vm.form.INV_ITEM_CORE_CAT_ID = dataItem.INV_ITEM_CORE_CAT_ID;

                        vm.brandDataSource = new kendo.data.DataSource({
                            transport: {
                                read: function (e) {
                                    InventoryDataService.getCategoryWiseBrandList(vm.form.INV_ITEM_CORE_CAT_ID).then(function (res) {
                                        e.success(res);
                                    });
                                }
                            }
                        });
                        //$("#RF_BRAND_ID").kendoDropDownList({
                        //    optionLabel: "-- Select Brand --",
                        //    filter: "startswith",
                        //    autoBind: true,
                        //    dataSource: dataSource,
                        //    dataTextField: "BRAND_NAME_EN",
                        //    dataValueField: "RF_BRAND_ID"
                        //});

                        getGridData();                        
                    }
                };
            });
        };

        function getMOUList() {
            return vm.MOUList = {
                optionLabel: "-- Select Unit --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getMOUList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"
            };
        };

        
        function getItemCategList() {
            return vm.ItemCategoryList = {
                optionLabel: "-- Select Category --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID"
            };
        };


        function getCountryList() {
            return vm.OrginCountryList = {
                optionLabel: "-- Select Country --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getCountryList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COUNTRY_NAME_EN",
                dataValueField: "HR_COUNTRY_ID"
            };
        };

        function getSourceTypeList() {

            return vm.sourceTypeList = {
                optionLabel: "-- Select Source --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getLookupListData(84).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getItemBrandList() {
            return vm.ItemBrandList = {
                optionLabel: "-- Select Brand --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getItemBrandList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "BRAND_NAME_EN",
                dataValueField: "RF_BRAND_ID"
            };
        };

        function getCurrencyList() {
            return vm.CurrencyList = {
                optionLabel: "-- Select Currency --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getCurrencyList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "CURR_NAME_EN",
                dataValueField: "RF_CURRENCY_ID"
            };
        };

        function getItemStatusList() {
            return vm.ItemStatusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getLookupListData(41).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        vm.gridOptions = {
            autoBind: true,
            height: '350px',
            scrollable: true,
            navigatable: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success([]);
                    }
                },
                pageSize: 50
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
            //pageSize: 10,
            //pageable: true,
            refresh: true,
            columns: [
                //{
                //    title: "Add/Remove",
                //    template: function () {
                //        return "<input type='checkbox' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                //    },
                //    width: "50px"
                //},
                { field: "ITEM_CODE", title: "Code", type: "string", width: "80px" },
                { field: "ITEM_NAME_EN", title: "Name", type: "string", width: "150px" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "100px" },
                { field: "MOU_CODE", title: "Con.Unit", type: "string", width: "100px" }                
            ],
            change: function (e) {
                var grid = $("#itemGrid").data("kendoGrid");
                //grid.select("tr:eq(1)");
                var row = grid.select();
                vm.form = grid.dataItem(row);
                $scope.$apply();                
            }
        };

        vm.getCostPrice = function () {
            vm.form.COST_PRICE = (((parseFloat(vm.form.PURCH_PRICE) * parseFloat(vm.form.PCT_ADDL_PRICE)) / 100) + parseFloat(vm.form.PURCH_PRICE)).toFixed(2);
        };

        vm.submitData = function (data, token) {
            vm.errors = undefined;
            //data.PACK_MOU_ID = vm.form.PURCH_MOU_ID;
            data.CREATED_BY = vm.CREATED_BY;
            vm.formCopy = angular.copy(data);

            if (angular.isDefined(data.INV_ITEM_ID) && data.INV_ITEM_ID > 0) {
                return InventoryDataService.updateItemData(data, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        reset();
                        //$("#itemGrid").data("kendoGrid").refresh();
                        getGridData();
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            } else {
                
                return InventoryDataService.saveItemData(data, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        reset();
                        getGridData();
                        //$("#itemGrid").data("kendoGrid").dataSource.read();
                        //grid.refresh();
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }

        };

        vm.exportItemList = function () {
            var data = { IS_EXCEL_FORMAT: 'Y' };
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/Inventory/InvReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-3000' }, data);

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




//=============== Item Sales POS =====================
(function () {
    'use strict';
    angular.module('multitex.inventory').controller('ItemSalesController', ['$q', 'config', 'InventoryDataService', '$stateParams', '$state', '$scope', '$window', 'logger', 'cur_pos_store_id', ItemSalesController]);
    function ItemSalesController($q, config, InventoryDataService, $stateParams, $state, $scope, $window, logger, cur_pos_store_id) {

        var vm = this;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.today = new Date();
        vm.isVarify = false;
        vm.isLoad = false;
        vm.isSaved = false;

        activate();


        vm.form = { LK_CUST_TYPE_ID: 590, REPORT_CODE: 'RPT-1041' };
        
        vm.form.LK_CUST_TYPE_ID = 590;
        vm.form.REPORT_CODE = 'RPT-1041';

        vm.isSendRequest = false;
        //vm.MEMO_DT = moment().format("DD-MMM-YYYY");
        $('#EMPLOYEE_CODE').focus();

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        
        vm.showSplash = true;
        function activate() {
            var promise = [getCustTypeListData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        };

        ///////// Item Auto Search
        $scope.itemAuto = function (val) {
            return InventoryDataService.getItemAutoDataList(val).then(function (res) {
                return res;
            });
        };

        $scope.onSelectItem = function (item) {
            console.log(item);
            
            vm.formDtl.ITEM_CODE = item.ITEM_CODE;
            vm.formDtl.INV_ITEM_ID = item.INV_ITEM_ID;
            vm.formDtl.ITEM_NAME_EN = item.ITEM_NAME_EN;
            vm.formDtl.SOLD_QTY = 1;
            vm.formDtl.QTY_MOU_ID = item.CONS_MOU_ID;
            vm.formDtl.CONS_MOU_CODE = item.CONS_MOU_CODE;

            vm.formDtl.UNIT_PRICE = item.SALES_PRICE;
            vm.formDtl.TOT_SOLD_AMT = item.SALES_PRICE * 1;                   
        };

       
        //////// End Item Auto Search

        $("#SOLD_QTY").blur(function () {            
            $("#TOT_SOLD_AMT").val($("#UNIT_PRICE").val() * $("#SOLD_QTY").val());            
            return;
        });

        $("#UNIT_PRICE").blur(function () {
            $("#TOT_SOLD_AMT").val($("#UNIT_PRICE").val() * $("#SOLD_QTY").val());            
            return;
        });

        //======== Start Set Focus ===========
        $("#ITEM_CODE").keypress(function (e) {
            if (e.which == 13) {
                $("#SOLD_QTY").focus();
            };
        });

        $("#SOLD_QTY").keypress(function (e) {            
            if (e.which == 13) {
                $("#UNIT_PRICE").focus();                
            };
        });

        $("#UNIT_PRICE").keypress(function (e) {
            if (e.which == 13) {
                $("#ADD_ITEM").focus();
            };
        });
        //======== End Set Focus ===========

        //========= Start Hot Key =========
        //$(document).on('keydown', null, 'ctrl+a', fn);        
        $(document).ready(function () {

            $(window).bind('keyup', function (e) {
                //$('body').append('focus window <br />');
                //console.log(e);
                if (e.which == 67 && e.altKey == true) /*Key:Alt+C*/ {
                    $(e.target).blur();
                    $('#EMPLOYEE_CODE').focus();
                }
                else if (e.which == 73 && e.altKey==true) /*Key:Alt+I*/ {
                    $(e.target).blur();
                    $('#ITEM_CODE').focus();
                }
                else if (e.which == 113) /*Key:F2*/ {
                    $(e.target).blur();

                    if (vm.formDtl.ITEM_CODE == 'FS0170') { //Eid Ul Azha-2018 Food Package-1
                        vm.formDtl.TOT_SOLD_AMT = (vm.formDtl.UNIT_PRICE * vm.formDtl.SOLD_QTY) - Math.round(((vm.formDtl.UNIT_PRICE * vm.formDtl.SOLD_QTY) * .25));
                    }                    
                    //else if (vm.formDtl.ITEM_CODE == 'FS0053') { //Milk 
                    //    vm.formDtl.TOT_SOLD_AMT = (vm.formDtl.UNIT_PRICE * vm.formDtl.SOLD_QTY) - Math.round(((vm.formDtl.UNIT_PRICE * vm.formDtl.SOLD_QTY) * .1428571428571429));
                    //}
                    else {
                        vm.formDtl.TOT_SOLD_AMT = vm.formDtl.UNIT_PRICE * vm.formDtl.SOLD_QTY;
                    }

                    $scope.$apply();
                    vm.salesItemAdd();
                    $scope.$apply();
                    $('#TOT_SOLD_AMT').val(0);
                    $('#ITEM_CODE').focus();
                }
                else if (e.which == 118) /*Key:F7*/ {
                    if (vm.isSaved == false) {
                        $(e.target).blur();
                        $scope.$apply();
                        if (vm.isSendRequest == false) {
                            vm.submitData(vm.form, $scope.token);
                        }
                        $scope.$apply();
                        //$('#EMPLOYEE_CODE').focus();
                    }
                    else {
                        vm.errors = [{ error: "Sorry! Memo already saved" }];
                        $scope.$apply();
                    }
                }
                else if (e.which == 120) /*Key:F9*/ {
                    $(e.target).blur();
                    reset();
                    $scope.$apply();
                    $('#EMPLOYEE_CODE').focus();
                }
                else if (e.which == 121) /*Key:F10*/ {
                    $(e.target).blur();
                    memoPrint();
                    $scope.$apply();
                    //$('#EMPLOYEE_CODE').focus();
                }

                //alert(e.which);                                
            });

            //$(document).bind('keydown', 'f9', function () {
            //    vm.form = {};
            //    vm.formDtl = {};
            //    $scope.$apply();                
            //    $('#EMPLOYEE_CODE').focus();
            //});
        });
        
        //========= End Hot Key =========

        $scope.emoloyeeAuto = function (val) {
            return InventoryDataService.getEmpAutoDataList(val, 30, null).then(function (res) {
                return res;
            });
        };

        $scope.onSelectEmp = function (item) {
            //console.log(item); 
            vm.errors = undefined;
            vm.form.MSG_NOT_ELLIGABLE = "";
            vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
            vm.form.CORE_DEPARTMENT_NAME = item.CORE_DEPARTMENT_NAME;
            vm.form.DEPARTMENT_NAME_EN = item.DEPARTMENT_NAME_EN;
            vm.form.DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
            vm.form.LK_JOB_STATUS_ID = item.LK_JOB_STATUS_ID;
            vm.form.GROSS_SALARY = item.GROSS_SALARY;
            //vm.form.COMP_NAME_EN = item.COMP_NAME_EN;
            //vm.form.HR_COMPANY_ID = item.HR_COMPANY_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.CORE_DEPARTMENT_NAME + '-' + item.DEPARTMENT_NAME_EN;

            vm.verify();
        };

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {
            if (!newVal || newVal == '') {                
                reset();
            }
        });

        vm.DISC_PCT = 0;
        vm.VAT_PCT = 0;
        vm.verify = function () {
            vm.form.DISC_PCT = 0;

            InventoryDataService.getEmpPOSVerify((vm.form.HR_EMPLOYEE_ID || 0), vm.MEMO_DT).then(function (res) {

                //if (res.length > 0) {
                    vm.form.MSG_NOT_ELLIGABLE = "";
                    vm.form.DAYS_PRESENT = res.DAYS_PRESENT;
                    vm.form.DAYS_ABSENT = res.DAYS_ABSENT;

                    vm.form.CRD_LMT_EARNED = res.CRD_LMT_EARNED;
                    vm.form.ALREADY_SOLD_AMT = res.ALREADY_SOLD_AMT;
                    vm.form.CREDIT_BALANCE_AMT = res.CREDIT_BALANCE_AMT;
                    vm.form.VAT_PCT = res.VAT_PCT;
                    vm.form.DISC_PCT = res.DISC_PCT;
                    if (vm.DISC_PCT == 0) {
                        vm.DISC_PCT = res.DISC_PCT;
                    }
                    if (vm.VAT_PCT == 0) {
                        vm.VAT_PCT = res.VAT_PCT;
                    }

                    if (cur_pos_store_id == 1) {
                        vm.form.VAT_PCT = 0;
                        vm.VAT_PCT = 0;
                    }

                    if (vm.form.EMPLOYEE_CODE) {
                        if (vm.form.CRD_LMT_EARNED > 0 && vm.form.DAYS_ABSENT <= 3) {
                            vm.isVarify = true;
                        }
                        else if (vm.form.CRD_LMT_EARNED<1) {
                            vm.form.MSG_NOT_ELLIGABLE = "Sorry! You don't have enough credit balance to buy.";
                        }
                        else if (vm.form.DAYS_ABSENT>3) {
                            vm.form.MSG_NOT_ELLIGABLE = "Sorry! You are continious absent.";
                        }                    
                    }
                    else {
                        vm.isVarify = true;
                    }
                //}
            });
        };

        function reset() {
            vm.isVarify = false;
            vm.isSaved = false;

            var data = angular.copy(vm.form);
            vm.form = {};

            vm.form.LK_CUST_TYPE_ID = data.LK_CUST_TYPE_ID;
            vm.form.REPORT_CODE = 'RPT-1041'; 
            vm.form.DAYS_PRESENT = 0,
            vm.form.CRD_LMT_EARNED = 0;
            vm.form.ALREADY_SOLD_AMT = 0;
            vm.form.CREDIT_BALANCE_AMT = 0;
            vm.form.DISC_PCT = 0;
            vm.form.VAT_PCT = 0;

            vm.formDtl = {};            
            vm.verify();

            var dataSource = new kendo.data.DataSource({
                batch: true,
                transport: {
                    read: function (e) {
                        e.success([]);
                    },

                    destroy: function (e) {
                        //console.log(e);
                        vm.salesTotal();
                        $scope.$apply();

                    },
                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return { models: kendo.stringify(options.models) };
                        }
                    }
                },
                pageSize: 10,
                schema: {
                    model: {
                        id: "INV_ITEM_ID",
                        fields: {
                            ITEM_CODE: { type: "string", editable: false },
                            ITEM_NAME_EN: { type: "string", editable: false },
                            SOLD_QTY: { type: "string", editable: false },
                            UNIT_PRICE: { type: "number", editable: false },
                            TOT_SOLD_AMT: { type: "string", editable: true }
                        }
                    }
                }
            });

            if (vm.isLoad == true) {                
                $('#salesItemGrid').data("kendoGrid").setDataSource(dataSource);                
            }            
            vm.isLoad = true;
        };

        vm.TranCancel = function () {
            reset();
        };
                     
        function getCustTypeListData() {
            return InventoryDataService.getLookupListData(118).then(function (res) {
                vm.custTypeList = res;
            });            
        };

        function getJobStatusListData() {
            return vm.jobStatusListData = {
                optionLabel: "-- Select Job Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getLookupListData(11).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getItemStatusList() {
            return vm.ItemStatusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getLookupListData(41).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };           
        };

        function getItemCategList() {
            return vm.ItemCategoryList = {
                optionLabel: "-- Select Category --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getItemCategList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID"
            };
        };

        vm.salesItemAdd = function () {
            if (vm.formDtl.ITEM_CODE == null) {
                alert("Please select item.");
                return;
            }
            else if ($("#SOLD_QTY").val() == null || $("#SOLD_QTY").val() <= 0) {
                alert("Please input sales quantity.");
                return;
            }
            else if ($("#TOT_SOLD_AMT").val() == null || $("#TOT_SOLD_AMT").val() <= 0) {
                alert("Sorry! Sales total amount must be grater than zero(0).");
                return;
            }

            if (vm.formDtl.ITEM_CODE == 'FS0044') { // Sola
                vm.formDtl.TOT_SOLD_AMT = (vm.formDtl.UNIT_PRICE * vm.formDtl.SOLD_QTY)-Math.round(((vm.formDtl.UNIT_PRICE * vm.formDtl.SOLD_QTY) * .25));
            }
            else if (vm.formDtl.ITEM_CODE == 'FS0045') { //Eid Ul Fitr-2017 Food Package-1
                vm.formDtl.TOT_SOLD_AMT = (vm.formDtl.UNIT_PRICE * vm.formDtl.SOLD_QTY) - Math.round(((vm.formDtl.UNIT_PRICE * vm.formDtl.SOLD_QTY) * .25));
            }
            else if (vm.formDtl.ITEM_CODE == 'FS0050') { // Eid Package
                vm.formDtl.TOT_SOLD_AMT = (vm.formDtl.UNIT_PRICE * vm.formDtl.SOLD_QTY) - Math.round(((vm.formDtl.UNIT_PRICE * vm.formDtl.SOLD_QTY) * .50));
            }
            //else if (vm.formDtl.ITEM_CODE == 'FS0053') { //Milk 
            //    vm.formDtl.TOT_SOLD_AMT = (vm.formDtl.UNIT_PRICE * vm.formDtl.SOLD_QTY) - Math.round(((vm.formDtl.UNIT_PRICE * vm.formDtl.SOLD_QTY) * .1428571428571429));
            //}
            else {
                vm.formDtl.TOT_SOLD_AMT = vm.formDtl.UNIT_PRICE * vm.formDtl.SOLD_QTY;
            }
            
            if (vm.formDtl.ITEM_CODE != null && $("#SOLD_QTY").val() != null && $("#TOT_SOLD_AMT").val() != null) {
                $('#salesItemGrid').data("kendoGrid").dataSource.insert(0, vm.formDtl);//  .setDataSource(dataSource);
                vm.formDtl = {};
                $('#ITEM_CODE').focus();
            }

            vm.salesTotal();
        };

        vm.salesTotal = function () {
            vm.form.TOT_AMT = 0;
            vm.form.SOLD_AMT = 0;
            vm.form.G_TOT_REST_AMT = 0;
            var gridData = $("#salesItemGrid").data("kendoGrid").dataSource.data();
            angular.forEach(gridData, function (val, key) {
                vm.form.TOT_AMT = vm.form.TOT_AMT + val.TOT_SOLD_AMT;
            });
            vm.form.SalesDtlItem = gridData;
            //console.log(vm.form.SalesDtlItem);
            var vDiscAmt = Math.round(vm.form.TOT_AMT * vm.form.DISC_PCT);
            vm.form.DISC_AMT = vDiscAmt;
            vm.form.SOLD_AMT = vm.form.TOT_AMT - vDiscAmt;
            
            var vVatAmt = Math.round(vm.form.SOLD_AMT * vm.form.VAT_PCT);
            vm.form.VAT_AMT = vVatAmt;
            vm.form.TOT_SOLD_WITH_VAT_AMT = vm.form.SOLD_AMT + vVatAmt;

            vm.form.G_TOT_REST_AMT = vm.form.CRD_LMT_EARNED - (vm.form.ALREADY_SOLD_AMT + vm.form.TOT_SOLD_WITH_VAT_AMT);
        };

        vm.onChangeCustType = function (item) {
            if (item.LK_CUST_TYPE_ID == 590) {
                vm.form.CUST_NAME = '';
                vm.form.CUST_MOB = '';
                vm.form.EMPLOYEE_CODE = '';
            }
            else {
                vm.form.EMPLOYEE_CODE = '';
            }
        }

        var dataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    e.success([]);
                },

                destroy: function (e) {
                    //console.log(e);
                    vm.salesTotal();
                    $scope.$apply();
                    
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
            pageSize: 10,
            schema: {
                model: {
                    id: "INV_ITEM_ID",
                    fields: {
                        ITEM_CODE: { type: "string", editable: false },
                        ITEM_NAME_EN: { type: "string", editable: false },
                        SOLD_QTY: { type: "string", editable: false },
                        UNIT_PRICE: { type: "number", editable: false },
                        TOT_SOLD_AMT: { type: "string", editable: true }
                    }
                }
            }
        });

        

        vm.gridOptions = {
            autoBind: true,
            height: '250px',
            scrollable: true,
            navigatable: true,
            dataSource: dataSource,            
            filterable: true,
            selectable: "row",
            sortable: true,
            //pageSize: 10,
            //pageable: true,
            refresh: true,
            columns: [
                { field: "INV_ITEM_ID", title: "INV_ITEM_ID", type: "string", hidden:true },
                { field: "ITEM_CODE", title: "Code", type: "string", headerAttributes: { "class": "col-md-2" }, attributes: { "class": "col-md-2" }, filterable: false },
                { field: "ITEM_NAME_EN", title: "Name", type: "string", headerAttributes: { "class": "col-md-4" }, attributes: { "class": "col-md-4" }, filterable: false },
                { field: "SOLD_QTY", title: "Qty", type: "string", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" }, filterable: false },
                { field: "CONS_MOU_CODE", title: "MoU", type: "string", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" }, filterable: false },
                { field: "UNIT_PRICE", title: "Rate", type: "string", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" }, filterable: false },
                { field: "TOT_SOLD_AMT", title: "Total", type: "string", headerAttributes: { "class": "col-md-2" }, attributes: { "class": "col-md-2" }, filterable: false },
                { command: [{ name: "destroy", text: "r" }], headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" } }
            ],
            editable: {
                confirmation: false, //"Do you want to remove this record?",
                mode: "inline"
            }
        };
        
        vm.submitData = function (data, token) {
            vm.isSendRequest = true;

            data.PS_COUNTR_ID = vm.PS_COUNTR_ID;
            data.CREATED_BY = vm.CREATED_BY;
            data.SOLD_TO = data.HR_EMPLOYEE_ID;
            data.SOLD_BY = vm.CREATED_BY;
            data.MEMO_DT = vm.MEMO_DT;

            return InventoryDataService.batchSavePOSItem(data, token).then(function (res) {
                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;

                    vm.isSendRequest = false;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    vm.form.MEMO_NO = res.data.PSAVE_MEMO_NO;

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {                        
                        vm.isSaved = true;
                    };

                    config.appToastMsg(res.data.PMSG);
                    vm.isSendRequest = false;
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        
        function memoPrint() {            
            vm.form.REPORT_CODE = 'RPT-1041';
           
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/Hr/HrReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params=vm.form;
            $scope.$apply();

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


        
        vm.memoPrint = function () {
            vm.form.REPORT_CODE = 'RPT-1041';
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/Hr/HrReport/PreviewReport');
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