(function () {
    'use strict';
    angular.module('multitex.knitting').controller('DailyTrimsAccesoriesController', ['$modalInstance', '$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$modal', 'Dialog', DailyTrimsAccesoriesController]);
    function DailyTrimsAccesoriesController($modalInstance, $q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $modal, Dialog) {


        var vmS = this;

        //vm.form = {};
        console.log(formData);

        activate()
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
            });
        }

        vmS.form = formData.MC_STYLE_H_EXT_ID || formData.MC_BYR_ACC_ID ? formData : { MC_ORD_TRMS_ITEM_ID: 0, PARTICULARS_LST: [{ 'CNTRL_NAME': '', 'COL_VAL': '' }] };
        vmS.form.PARTICULARS_LST = [{ 'CNTRL_NAME': '', 'COL_VAL': '' }]
        console.log(formData);
        vmS.form['INV_ITEM_CAT_ID'] = 7;
        vmS.form['MC_ORD_TRMS_ITEM_ID'] = 0;

        //loadItemCategoryList();
        //fiberCompositionList();
        //getFabTypeList();
        GetTrimsItemList();
        getColorData(vmS.form.MC_BYR_ACC_ID, (vmS.form.MC_STYLE_H_EXT_ID || 0));

        getItemData(vmS.form.INV_ITEM_CAT_ID);
        getSizeData((vmS.form.MC_STYLE_H_ID || 0));
        getControlList();

        console.log(vmS.form);

        $scope.cancel = function () {
            $modalInstance.close($scope["TrimsItemList"].data());
        }


        $scope.loadItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);

            KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.INV_ITEM_CAT_ID || 7)).then(function (res) {
                $scope["ItemList"] = new kendo.data.DataSource({
                    data: res
                });
            });

        };


        $scope.concateItemSpec = function (items) {
            var spec = "";
            if ((items.MC_ORD_TRMS_ITEM_ID || 0) == 0) {
                if (items.INV_ITEM_ID) {
                    var item = _.filter($scope["ItemList"].data(), function (o) {
                        return o.INV_ITEM_ID === parseInt(items.INV_ITEM_ID)
                    })[0];

                    if (items.GMT_COLOR_ID > 0) {
                        var color = _.filter($scope["ColorList"].data(), function (o) {
                            return o.MC_COLOR_ID === parseInt(items.GMT_COLOR_ID)
                        })[0];

                        if (color.COLOR_NAME_EN)
                            spec = item.ITEM_NAME_EN + ' ' + color.COLOR_NAME_EN;
                    }

                    if (items.MC_SIZE_ID > 0) {
                        var fabType = _.filter($scope["SizeList"].data(), function (o) {
                            return o.MC_SIZE_ID === parseInt(items.MC_SIZE_ID)
                        })[0];

                        if (fabType.SIZE_CODE)
                            spec = item.ITEM_NAME_EN + ' ' + fabType.SIZE_CODE;
                    }
                }
                else {
                    spec = '';
                }

                var iptem = spec.replace('  ', ' ');

                items.ITEM_SPEC_AUTO = iptem.replace('  ', ' ');
            }
        }

        function loadItemCategoryList() {

            KnittingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                var filter = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 7) });
                $scope["itemCategoryList"] = new kendo.data.DataSource({
                    data: filter
                });
            });
        }

        function getItemData(INV_ITEM_CAT_ID) {

            KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (INV_ITEM_CAT_ID || 7)).then(function (res) {
                $scope["ItemList"] = new kendo.data.DataSource({
                    data: res
                });
            });
        };


        function getColorData(MC_BYR_ACC_ID, MC_STYLE_H_EXT_ID) {

            KnittingDataService.getDataByFullUrl('/api/mrc/Order/StyleExtOrByerAccWiseColorList?pMC_BYR_ACC_ID=' + (MC_BYR_ACC_ID || 0) + '&pMC_STYLE_H_EXT_ID=' + (MC_STYLE_H_EXT_ID || 0)).then(function (res) {
                $scope["ColorList"] = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        function getSizeData(MC_STYLE_H_ID) {

            var url = '/api/mrc/SizeMaster/SelectAll/';
            if ((MC_STYLE_H_ID || 0) > 0)
                url = '/api/mrc/Order/StyleWiseSizeList/' + MC_STYLE_H_ID;

            KnittingDataService.getDataByFullUrl(url).then(function (res) {
                $scope["SizeList"] = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        function GetTrimsItemList() {

            KnittingDataService.getDataByFullUrl('/api/knit/DailyTrimsReceive/GetTrimsItemListByID?pINV_ITEM_CAT_ID=' + (vmS.form.INV_ITEM_CAT_ID || 7) + '&pMC_STYLE_H_EXT_ID=' + (vmS.form.MC_STYLE_H_EXT_ID || 0) + '&pMC_BYR_ACC_ID=' + (vmS.form.MC_BYR_ACC_ID || 0)).then(function (res) {
                $scope["TrimsItemList"] = new kendo.data.DataSource({
                    data: res
                });
            });

        };


        function getControlList() {
            $scope["controlList"] = {
                optionLabel: "--Select--",
                filter: "contains",
                autoBind: false,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl("/api/mrc/TempTaBk/getTemplateControl?pMC_ACCS_PO_TMPLT_H_ID=0").then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "CNTRL_LABEL",
                dataValueField: "CNTRL_NAME"
            };
        }

        $scope.addCtrl = function (index, item) {
            var idx = index + 1;
            var nObj = { CNTRL_NAME: '', COL_VAL: '' };
            item.splice(idx, 0, nObj);
        }

        $scope.delCtrl = function (idx, item) {
            item.splice(idx, 1);
        }

        $scope["gridOptionsItem"] = {
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
            height: '300px',
            scrollable: true,
            columns: [

                { field: "MC_ORD_TRMS_ITEM_ID", hidden: true },
                { field: "INV_ITEM_ID", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },
                { field: "PARTICULARS_LST", hidden: true },
                { field: "RF_FIB_COMP_ID", hidden: true },
                { field: "ADJ_BY_ITEM_ID", hidden: true },
                { field: "IS_ITEM_ADJ", hidden: true },
                { field: "GMT_COLOR_ID", hidden: true },
                { field: "MC_BYR_ACC_ID", hidden: true },
                { field: "MC_STYLE_H_EXT_ID", hidden: true },
                { field: "MC_ORDER_H_ID", hidden: true },
                { field: "MC_FAB_PROD_ORD_H_ID", hidden: true },

                { field: "ITEM_NAME_EN", title: "Item Name", type: "string", width: "20%" },
                { field: "ITEM_SPEC_AUTO", title: "Item Specification", type: "string", width: "20%" },
                { field: "PROD_COLOR_NAME_EN", title: "Prod. Color", type: "string", width: "10%" },
                { field: "PROD_SIZE_CODE", title: "Prod. Size", type: "string", width: "10%" },
                { field: "ITEM_CODE", title: "Item Code", type: "string", width: "10%" },
                { field: "PARTICULARS_VALUE", title: "Other Particulars", type: "string", width: "20%" },
                {
                    title: "Remove",
                    template: '<a  title="Delete" ng-click="vmS.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vmS.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "10%"
                }
            ],
            //editable: true
        };

        vmS.editItemData = function (dataItem) {
            var data = angular.copy(dataItem);
            var list = KnittingDataService.parseXmlString(data.PARTICULARS);

            vmS.form = {
                MC_ORD_TRMS_ITEM_ID: data.MC_ORD_TRMS_ITEM_ID,
                MC_FAB_PROD_ORD_H_ID: data.MC_FAB_PROD_ORD_H_ID,
                INV_ITEM_CAT_ID: data.INV_ITEM_CAT_ID,
                INV_ITEM_ID: data.INV_ITEM_ID,
                ITEM_SPEC_AUTO: data.ITEM_SPEC_AUTO,
                GMT_COLOR_ID: data.GMT_COLOR_ID,
                MC_SIZE_ID: data.MC_SIZE_ID,
                QTY_MOU_ID: data.QTY_MOU_ID,
                RQD_QTY: data.RQD_QTY,
                CONF_RATE: data.CONF_RATE,
                MC_BYR_ACC_ID: data.MC_BYR_ACC_ID,
                PROD_COLOR_NAME_EN: data.PROD_COLOR_NAME_EN,
                PROD_SIZE_CODE: data.PROD_SIZE_CODE,
                ITEM_CODE: data.ITEM_CODE,
                PARTICULARS_LST: data.PARTICULARS.length > 5 ? list : data.PARTICULARS_LST,
                SCM_PURC_REQ_D_ID: data.SCM_PURC_REQ_D_ID,
                MC_STYLE_H_EXT_ID: data.MC_STYLE_H_EXT_ID
            };
            console.log(vmS.form);
        }


        vmS.removeItemData = function (dataItem) {

            var data = angular.copy(dataItem);
            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     return KnittingDataService.saveDataByFullUrl(data, '/api/knit/DailyTrimsReceive/DeleteTrimsItem').then(function (res) {

                         if (res.success === false) {
                             vmS.errors = res.errors;
                         }
                         else {
                             GetTrimsItemList();
                             res['data'] = angular.fromJson(res.jsonStr);
                             config.appToastMsg(res.data.PMSG);
                         }
                     });
                 });
        }

        $scope.resetAllData = function () {
            vmS.form['MC_ORD_TRMS_ITEM_ID'] = 0;
            vmS.form['INV_ITEM_ID'] = '';
            vmS.form['GMT_COLOR_ID'] = '';
            vmS.form['MC_SIZE_ID'] = '';
            vmS.form['PROD_COLOR_NAME_EN'] = '';
            vmS.form['PROD_SIZE_CODE'] = '';
            vmS.form['ITEM_SPEC_AUTO'] = '';
            vmS.form['ITEM_CODE'] = '';
            vmS.form['PARTICULARS_LST'] = [{ 'CNTRL_NAME': '', 'COL_VAL': '' }];
        }

        $scope.submitAll = function (dataOri) {

            var data = angular.copy(dataOri);
            var list = _.filter(data.PARTICULARS_LST, function (s) { return s.CNTRL_NAME != "--Select--" });
            data.PARTICULARS = KnittingDataService.xmlStringLong(list.map(function (x) {
                return {
                    CNTRL_NAME: x.CNTRL_NAME,
                    COL_VAL: x.COL_VAL
                }
            }))

            return KnittingDataService.saveDataByFullUrl(data, '/api/knit/DailyTrimsReceive/SaveTrimsItem').then(function (res) {

                if (res.success === false) {
                    vmS.errors = res.errors;
                }
                else {

                    GetTrimsItemList();
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                    $scope.resetAllData();
                }
            });
        }
    }


})();

