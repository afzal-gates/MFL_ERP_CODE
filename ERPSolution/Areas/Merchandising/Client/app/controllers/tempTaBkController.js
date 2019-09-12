(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TempTaBkController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'Dialog', '$modal', TempTaBkController]);
    function TempTaBkController($q, config, MrcDataService, $stateParams, $state, $scope, Dialog, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;

        vm.params = $stateParams;

        vm.TempHeaderList = null;
        var itemData = [];
        var itemCategoryData = [];
        var buyerData = [];
        var consData = [];
        var templateData = {};

        vm.form = {};

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getTempHeaderList(), getItemNcategoryList(), getBuyerList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;

            });
        }


        function getItemNcategoryList() {
            return MrcDataService.getDataByUrl('/TempTaBk/TrimAccItemList').then(function (res) {
                itemCategoryData = _.uniqBy(res, 'INV_ITEM_CAT_ID');
                itemData = res;
            });
        };

        function getBuyerList() {
            return MrcDataService.getDataByUrl('/buyer/SelectAll').then(function (res) {
                buyerData = res;
            });
        }




        vm.gridOptions = {
            pageable: false,
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
            dataBound: function (e) {
                $('#IDHeaderD td').on('dblclick', function (e) {
                    if ($(".k-grid-edit-row").length <= 1) {
                        $("#IDHeaderD").data("kendoGrid").editCell($(this));
                        $(this).focusout(function () {
                            if (!$("#IDHeaderD").data("kendoGrid").select().hasClass("k-state-selected"))
                                if (!$(".k-grid-edit-row input").hasClass("k-invalid"))
                                    $("#IDHeaderD").data("kendoGrid").closeCell();
                        });
                    }
                });
            },
            height: '200px',
            scrollable: true,
            columns: [
                { field: "SL", title: "SL#", width: "20px", filterable: false },
                { field: "INV_ITEM_CAT_TYPE", title: "Item Category", width: "70px", editor: ItemCatDropDownEditor, template: "#=INV_ITEM_CAT_TYPE.ITEM_CAT_NAME_EN#" },
                { field: "INV_ITEM_TYPE", title: "Trim & Accessories", width: "70px", editor: ItemDropDownEditor, template: "#=INV_ITEM_TYPE.ITEM_NAME_EN#" },
                { field: "BUYER_TYPE", title: "Buyer", width: "70px", editor: BuyerDropDownEditor, template: "#=BUYER_TYPE.BUYER_NAME_EN#" },
             
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.remove(dataItem)" class="btn btn-xs btn-warning"><i class="fa fa-times-circle"></i></a>',
                    width: "20px"
                }
            ],
            //editable: true
        };

        function BuyerDropDownEditor(container, options) {
            $('<input data-text-field="BUYER_NAME_EN" data-value-field="MC_BUYER_ID" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    optionLabel: "--N/A--",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                e.success(buyerData);
                            }
                        }
                    }
                });
        }


        function ItemCatDropDownEditor(container, options) {

            $('<input data-text-field="ITEM_CAT_NAME_EN" data-value-field="INV_ITEM_CAT_ID" data-bind="value:' + options.field + '" required/>')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                e.success(itemCategoryData);
                            }
                        }
                    },
                    change: function (e) {
                        options.model.INV_ITEM_TYPE = {
                            INV_ITEM_ID: '',
                            ITEM_NAME_EN: '--N/A--'
                        }
                        vm.dsHeaderD.sync();
                    }
                });
        }

        function ItemDropDownEditor(container, options) {
            $('<input data-text-field="ITEM_NAME_EN" data-value-field="INV_ITEM_ID" data-bind="value:' + options.field + '" required/>')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                e.success(_.filter(itemData, function (o) {
                                    return options.model.INV_ITEM_CAT_TYPE.INV_ITEM_CAT_ID ? (o.INV_ITEM_CAT_ID == options.model.INV_ITEM_CAT_TYPE.INV_ITEM_CAT_ID) : true;
                                }));
                            }
                        }
                    },
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        options.model.INV_ITEM_CAT_TYPE = {
                            INV_ITEM_CAT_ID: item.INV_ITEM_CAT_ID,
                            ITEM_CAT_NAME_EN: item.ITEM_CAT_NAME_EN
                        }

                        vm.dsHeaderD.sync();
                    }

                });
        }

        vm.remove = function (data) {

            if (!data.MC_ACCS_PO_TMPLT_D_ID || data.MC_ACCS_PO_TMPLT_D_ID <= 0) {
                vm.dsHeaderD.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.dsHeaderD.remove(data);
                 });

        }


        vm.onDataBound = function (e) {
            var listView = e.sender;
            var item = e.sender.dataSource.get($stateParams.pMC_ACCS_PO_TMPLT_H_ID || 0);

            if (!item) {
                return;
            }
            listView.select(
                listView.element.children("[data-uid='" + item.uid + "']")
            );
        }

        function getTempHeaderList() {
            return MrcDataService.getDataByUrl('/TempTaBk/getTempHeaderList').then(function (res) {
                vm.dataSource = new kendo.data.DataSource({
                    data: res,
                    pageSize: 50,
                    schema: {
                        model: {
                            id: "MC_ACCS_PO_TMPLT_H_ID"
                        }
                    }
                });
            });
        }

        vm.OptionsForControlCfg = {
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
            dataBound: function () {
                var grid = this;
                grid.table.kendoDraggable({
                    filter: "tbody > tr",
                    cursorOffset: {
                        top: 60,
                        left: 50
                    },
                    group: "gridGroup",
                    hint: function (e) {
                        return $('<div class="k-grid k-widget"><table><tbody><tr>' + e.html() + '</tr></tbody></table></div>');
                    }
                });

                grid.table.kendoDropTarget({
                    group: "gridGroup",
                    drop: function (e) {
                        var target = grid.dataSource.getByUid($(e.draggable.currentTarget).data("uid")),
                            dest = $(e.target);

                        if (dest.is("th") || dest.is("thead") || dest.is("span") || dest.parent().is("th")) {
                            return;
                        }
                        //Destination is image
                        if (dest.is("img")) {
                            dest = grid.dataSource.getByUid(dest.parent().parent().data("uid"));
                        } else {
                            dest = grid.dataSource.getByUid(dest.parent().data("uid"));
                        }

                        if (!dest) {
                            return;
                        }

                        //not on same item
                        if (target.MC_ACCS_PO_TMPLT_CFG_ID !== dest.MC_ACCS_PO_TMPLT_CFG_ID) {
                            //reorder the items
                            var tmp = target.get("DISPLAY_ORDER");
                            target.set("DISPLAY_ORDER", dest.get("DISPLAY_ORDER"));
                            dest.set("DISPLAY_ORDER", tmp);
                            grid.dataSource.sort({ field: "DISPLAY_ORDER", dir: "asc" });
                        }
                    }
                })
            },

            scrollable: true,
            //change: function() {
            //    var cell = this.select(),
            //        cellIndex = cell.index(),
            //        column = this.columns[cellIndex],
            //        dataSource = this.dataSource,
            //        dataItem = dataSource.view()[cell.closest("tr").index()];

            //        if (!dataItem[column.field]) {
            //            dataItem.IS_CHECKED = dataItem.IS_CHECKED == 'Y' ? 'N' : 'Y';
            //            vm.dsControlCfg.sync();
            //        }                 

            //},
            columns: [
                {
                    title: "+/-",
                    template: "<input type='checkbox' title='Add' ng-model='dataItem.IS_CHECKED' ng-true-value='\"Y\"' ng-false-value='\"N\"'>",
                    width: "20px"
                },
                { field: "CNTRL_LABEL", title: "Control Label", type: "string", width: "80px", editor: nonEditor },
                { field: "CNTRL_NAME", title: "Column Name", type: "string", width: "80px", editor: nonEditor },
                { field: "CNTRL_TYPE", title: "Control Type", type: "string", width: "50px", editor: nonEditor },
                { field: "PCT_WIDTH", title: "% Width", type: "number", width: "50px", filterable: false },
            ],
            editable: true
        };


        function nonEditor(container, options) {
            container.text(options.model[options.field]);
        }


        vm.onItemChange = function (data) {

            if (!$stateParams.pMC_ACCS_PO_TMPLT_H_ID || $stateParams.pMC_ACCS_PO_TMPLT_H_ID == 0 || data.MC_ACCS_PO_TMPLT_H_ID !== $stateParams.pMC_ACCS_PO_TMPLT_H_ID) {
                $stateParams['pMC_ACCS_PO_TMPLT_H_ID'] = data.MC_ACCS_PO_TMPLT_H_ID;
                $state.go('TempTaBk', { pMC_ACCS_PO_TMPLT_H_ID: data.MC_ACCS_PO_TMPLT_H_ID }, { notify: false });
            }

            vm.form = { MC_ACCS_PO_TMPLT_H_ID: data.MC_ACCS_PO_TMPLT_H_ID, ACCS_PO_TMPLT_NAME: data.ACCS_PO_TMPLT_NAME };
            templateData = { MC_ACCS_PO_TMPLT_H_ID: data.MC_ACCS_PO_TMPLT_H_ID, ACCS_PO_TMPLT_NAME: data.ACCS_PO_TMPLT_NAME };

            MrcDataService.getDataByUrl('/TempTaBk/getTempHeader?pMC_ACCS_PO_TMPLT_H=' + data.MC_ACCS_PO_TMPLT_H_ID).then(function (res) {
                vm.dsHeaderD = new kendo.data.DataSource({
                    data: res.PO_TMPLT_D,
                    schema: {
                        model: {
                            id: "MC_ACCS_PO_TMPLT_D_ID",
                            fields: {
                                INV_ITEM_CAT_TYPE: { defaultValue: { INV_ITEM_CAT_ID: "", ITEM_CAT_NAME_EN: "--Select Category--" }, editable: true },
                                INV_ITEM_TYPE: { defaultValue: { INV_ITEM_ID: "", ITEM_NAME_EN: "--Select Item--" }, editable: true },
                                BUYER_TYPE: { defaultValue: { MC_BUYER_ID: "", BUYER_NAME_EN: "--N/A--" }, editable: true }
                            }
                        }
                    }
                });

                vm.dsControlCfg = new kendo.data.DataSource({
                    data: res.PO_TMPLT_CFG,
                    schema: {
                        model: {
                            id: "MC_ACCS_PO_TMPLT_CFG_ID",
                            CNTRL_LABEL: { editable: false },
                            PCT_WIDTH: { type: "number", validation: { required: true, min: 1, max: 99 } },
                        }
                    }
                });
            });
        }

        vm.addTempHeaderD = function () {
            var idx = vm.dsHeaderD.data().length + 1;
            vm.dsHeaderD.insert(idx, {
                SL: idx,
                BUYER_TYPE: { MC_BUYER_ID: "", BUYER_NAME_EN: "--N/A--" },
                INV_ITEM_TYPE: { INV_ITEM_ID: "", ITEM_NAME_EN: "--Select Item--" },
                INV_ITEM_CAT_TYPE: { INV_ITEM_CAT_ID: "", ITEM_CAT_NAME_EN: "--Select Category--" },
            });
        }

        vm.submitData = function () {
            var HrdDData = [];

            angular.forEach(vm.dsHeaderD.data(), function (o, key) {
                if (o.INV_ITEM_CAT_TYPE.INV_ITEM_CAT_ID) {
                    HrdDData.push({
                        MC_ACCS_PO_TMPLT_D_ID: o.MC_ACCS_PO_TMPLT_D_ID || -1,
                        INV_ITEM_ID: o.hasOwnProperty('INV_ITEM_TYPE') ? (o.INV_ITEM_TYPE ? o.INV_ITEM_TYPE.INV_ITEM_ID : '') : '',
                        MC_BUYER_ID: o.hasOwnProperty('BUYER_TYPE') ? (o.BUYER_TYPE ? o.BUYER_TYPE.MC_BUYER_ID : '') : '',
                        MC_BYR_ACC_ID: o.hasOwnProperty('BUYER_ACC') ? (o.MC_BYR_ACC_ID ? o.BUYER_TYPE.MC_BYR_ACC_ID : '') : '',
                        INV_ITEM_CAT_ID: o.hasOwnProperty('INV_ITEM_CAT_TYPE') ? (o.INV_ITEM_CAT_TYPE ? o.INV_ITEM_CAT_TYPE.INV_ITEM_CAT_ID : '') : ''
                    });
                }

            });

            var CntrlConfData = [];
            angular.forEach(
               _.sortBy(vm.dsControlCfg.data(), 'DISPLAY_ORDER')
                , function (val, key) {
                    if (val.IS_CHECKED === 'Y' && val.PCT_WIDTH > 0) {
                        CntrlConfData.push({
                            //CNTRL_LABEL: val.CNTRL_LABEL,
                            //CNTRL_NAME : val.CNTRL_NAME,
                            //CNTRL_TYPE :val.CNTRL_TYPE,
                            //COL_VAL:val.COL_VAL,
                            DISPLAY_ORDER: key + 1,
                            //FETCH_URL:val.FETCH_URL,
                            //FIELD_SIZE :val.FIELD_SIZE,
                            //FIELD_TYPE:val.FIELD_TYPE,
                            //IS_CHECKED:val.IS_CHECKED,
                            //IS_DATA_FETCH:val.IS_DATA_FETCH,
                            //IS_REQUIRED:val.IS_REQUIRED,
                            //MAXLENGTH:val.MAXLENGTH,
                            //MAX_VAL:val.MAX_VAL,
                            MC_ACCS_PO_TMPLT_CFG_ID: val.MC_ACCS_PO_TMPLT_CFG_ID,
                            //MC_ACCS_PO_TMPLT_H_ID:val.MC_ACCS_PO_TMPLT_H_ID,
                            MC_MAP_ACCS_PO_TMPLT_ID: val.MC_MAP_ACCS_PO_TMPLT_ID,
                            //MINLENGTH:val.MINLENGTH,
                            //MIN_VAL:val.MIN_VAL,
                            //OPTION_VAL:val.OPTION_VAL,
                            PCT_WIDTH: val.PCT_WIDTH,
                        });
                    }
                });


            if (HrdDData.length == 0) {
                config.appToastMsg('MULTI-002 No data found in Trim & Accessories List');
                return;
            }

            if (vm.dsHeaderD.data().length < HrdDData.length) {
                config.appToastMsg('MULTI-002 Incorrect data found in Trim & Accessories List. Please check & then try again.');
                return;
            }

            if (CntrlConfData.length == 0) {
                config.appToastMsg('MULTI-002 No data found in Control Specification');
                return;
            }

            if (vm.dsControlCfg.data().length < CntrlConfData.length) {
                config.appToastMsg('MULTI-002 Incorrect information found in Control Specification. Please check & then try again.');
                return;
            }







            return MrcDataService.saveDataByUrl(
            {
                MC_ACCS_PO_TMPLT_H_ID: vm.form.MC_ACCS_PO_TMPLT_H_ID,
                ACCS_PO_TMPLT_NAME: vm.form.ACCS_PO_TMPLT_NAME,

                PO_TMPLT_D_XML: MrcDataService.xmlStringShort(HrdDData),
                PO_TMPLT_CFG_XML: MrcDataService.xmlStringShort(CntrlConfData)
            },
            '/TempTaBk/Save'
            ).then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        if (templateData.ACCS_PO_TMPLT_NAME != vm.form.ACCS_PO_TMPLT_NAME) {
                            getTempHeaderList();
                        }

                        vm.onItemChange(vm.form);

                    }
                    config.appToastMsg(res.data.PMSG);

                }

            });


        }

        vm.addItemTempName = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'NewControlTempBooking.html',
                controller: function ($scope, $modalInstance, MrcDataService) {
                    $scope.save = function (data) {
                        return MrcDataService.saveDataByUrl(
                                {
                                    MC_ACCS_PO_TMPLT_H_ID: data.MC_ACCS_PO_TMPLT_H_ID || -1,
                                    ACCS_PO_TMPLT_NAME: data.ACCS_PO_TMPLT_NAME
                                },
                                '/TempTaBk/SaveHrdData'
                                ).then(function (res) {

                                    if (res.success === false) {
                                        vm.errors = res.errors;
                                    }
                                    else {
                                        res['data'] = angular.fromJson(res.jsonStr);

                                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                            $modalInstance.close({ MC_ACCS_PO_TMPLT_H_ID: parseInt(res.data.OPMC_ACCS_PO_TMPLT_H_ID) });
                                        }
                                        config.appToastMsg(res.data.PMSG);

                                    }

                                });
                    };

                    $scope.cancel = function (data) {
                        $modalInstance.dismiss();
                    }
                },
                size: 'md',
                windowClass: 'large-Modal'
            });

            modalInstance.result.then(function (data) {
                $state.go('TempTaBk', { pMC_ACCS_PO_TMPLT_H_ID: data.MC_ACCS_PO_TMPLT_H_ID });
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        };
    }

})();