(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DCDosingTempleteController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', '$filter', '$http', 'commonDataService', 'Dialog', DCDosingTempleteController]);
    function DCDosingTempleteController($q, config, DyeingDataService, $stateParams, $state, $scope, $filter, $http, commonDataService, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = $filter('date')(new Date(), config.appDateFormat);
        vm.KNT_MACHINE_ID = 0;

        var key = 'KNT_MACHN_OPR_ID';
        vm.form = { EFFECT_FROM: vm.today, KNT_MACHINE_ID: null };
        vm.formItem = { STEP_NO: 1 };

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getDyePhaseTypeList(), getDyeMethodList(), GetItemCategoryList(), ItemList(), getDyeProcessTypeList(), getLabdipMasterList(), GetMOUList(), GetTemplateTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });


        }


        function GetTemplateTypeList() {

            return vm.TemplateTypeList = {
                optionLabel: "-- Select Template Type --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(97).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.EFFECT_FROM_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.EFFECT_FROM_LNopened = true;
        }

        vm.EXPIRED_ON_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.EXPIRED_ON_LNopened = true;
        }

        vm.DYMethodRearange = function () {
            if (vm.form.LK_DYE_MTHD_ID && vm.formItem) {
                var count = _.filter(vm.DyeMethodListForDC.data(), function (o) { return o.LK_DYE_MTHD_ID == vm.form.LK_DYE_MTHD_ID }).length;
                if (count > 0)
                    vm.formItem.LK_DYE_MTHD_ID = vm.form.LK_DYE_MTHD_ID;
                else
                    vm.formItem.LK_DYE_MTHD_ID = 0;
            }
        }

        vm.colorGroupList = {
            optionLabel: "-- Colour Group--",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/mrc/ColourMaster/ColourGroupList').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        })
                    }
                }
            },
            dataTextField: "COLOR_GRP_NAME_EN",
            dataValueField: "MC_COLOR_GRP_ID"
        };


        function getDyeMethodList() {
            getDyeMethodListForDC();
            return vm.DyeMethodList = {
                optionLabel: "--Select Dyeing Method--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return DyeingDataService.getDataByFullUrl('/api/common/GetDyeingMethodList').then(function (res) {
                                var list = _.filter(res, function (o) { return (o.IS_S_D_PART == 'S' || o.IS_S_D_PART == 'D') });
                                e.success(list);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "DYE_MTHD_NAME",
                dataValueField: "LK_DYE_MTHD_ID"
            };
        }

        function getDyeMethodListForDC() {
            DyeingDataService.getDataByFullUrl('/api/common/GetDyeingMethodList').then(function (res) {

                var list = _.filter(res, function (o) { return (o.IS_S_D_PART == 'S') });
                vm.DyeMethodListForDC = new kendo.data.DataSource({
                    data: list
                })
            });
        }

        function getLabdipMasterList() {
            return vm.labdipMasterList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLDDosingTemplete').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 15
            });
        };

        function getDyePhaseTypeList() {

            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/DyePhaseType').then(function (res) {
                console.log(res);
                return vm.dyePhaseTypeList = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        function getDyeProcessTypeList() {

            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/DyeProcessType').then(function (res) {
                console.log(res);
                return vm.dyeProcessTypeList = new kendo.data.DataSource({
                    data: res
                });
            });
        };


        function ItemList() {
            return vm.DyItemList = new kendo.data.DataSource({
                data: []
            });
        };

        function GetItemCategoryList() {
            return vm.itemCategoryList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                            var list = _.filter(res, function (o) { return ((o.INV_ITEM_CAT_ID === 3 || o.INV_ITEM_CAT_ID === 4 || o.PARENT_ID === 3 || o.PARENT_ID === 4)) });
                            e.success(list);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }


        vm.getItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);
            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.INV_ITEM_CAT_ID || 3)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });

        };

        function GetMOUList() {
            return vm.mOUList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                            var list = _.filter(res, function (o) { return (o.MOU_CODE.indexOf("%") != -1 || o.MOU_CODE.indexOf("g/L") != -1) })
                            e.success(list);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        vm.onChangeMachine = function (dataItem) {

            var item = angular.copy(dataItem);

            console.log(item);

            var List = item.MC_COLOR_GRP_LST ? item.MC_COLOR_GRP_LST.split(',') : [];
            vm.form['DYE_DOSE_TMPLT_H_ID'] = item.DYE_DOSE_TMPLT_H_ID;

            vm.form['DOSE_TMPLT_CODE'] = item.DOSE_TMPLT_CODE;
            vm.form['DOSE_TMPLT_NAME_EN'] = item.DOSE_TMPLT_NAME_EN;
            vm.form['DOSE_TMPLT_NAME_BN'] = item.DOSE_TMPLT_NAME_BN;
            vm.form['DOSE_TMPLT_SNAME'] = item.DOSE_TMPLT_SNAME;
            vm.form['LK_DYE_MTHD_ID'] = item.LK_DYE_MTHD_ID;
            vm.form['IS_FINALIZED'] = item.IS_FINALIZED;
            vm.form['LK_DC_TMPLT_TYPE_ID'] = item.LK_DC_TMPLT_TYPE_ID;
            vm.form['MC_COLOR_GRP_LST'] = List;
            vm.formItem = { STEP_NO: ''};
            vm.recipeItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLDDosingTempleteDtl?pDYE_DOSE_TMPLT_H_ID=' + item.DYE_DOSE_TMPLT_H_ID).then(function (res) {

                            e.success(res);
                        });
                    }
                }
            });
            //DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLDDosingTempleteDtl?pDYE_DOSE_TMPLT_H_ID=' + item.DYE_DOSE_TMPLT_H_ID).then(function (res) {
            //    console.log(res);
            //    return vm.dyePhaseTypeList = new kendo.data.DataSource({
            //        data: res
            //    });
            //});
        };

        vm.onChangeOperator = function (dataItem) {

            var item = dataItem;

            vm.form.HR_EMPLOYEE_ID = dataItem.HR_EMPLOYEE_ID;
            vm.form.EMPLOYEE_CODE = dataItem.EMPLOYEE_CODE;
            vm.form.EMP_FULL_NAME_EN = dataItem.EMP_FULL_NAME_EN;
        };


        vm.addToGridC = function () {
            //if (fnValidateSub3() == true) {
            var LD_M_ID = vm.formItem.LK_DYE_MTHD_ID;
            var PHASE_TYPE_ID = vm.formItem.DYE_PRD_PHASE_TYPE_ID
            var StepNo = "";
            var item = _.filter(vm.itemCategoryList.data(), function (o) { return o.INV_ITEM_CAT_ID == vm.formItem.DC_AGENT_ID });
            var dlist = _.filter(vm.DyeMethodListForDC.data(), function (o) { return o.LK_DYE_MTHD_ID == vm.formItem.LK_DYE_MTHD_ID });
            var dphlist = _.filter(vm.dyePhaseTypeList.data(), function (o) { return o.DYE_PRD_PHASE_TYPE_ID == vm.formItem.DYE_PRD_PHASE_TYPE_ID });
            var dprlist = _.filter(vm.dyeProcessTypeList.data(), function (o) { return o.DYE_PROC_OPR_TYPE_ID == vm.formItem.DYE_PROC_OPR_TYPE_ID });
            var mouList = _.filter(vm.mOUList.data(), function (o) { return o.RF_MOU_ID == vm.formItem.DOSE_MOU_ID });
            //console.log(item);
            vm.formItem.DC_AGENT_NAME = item[0].ITEM_CAT_NAME_EN;
            vm.formItem.ALT_AGENT_NAME = item[0].ITEM_CAT_NAME_EN;

            vm.formItem.PROC_OPR_NAME = dprlist[0].PROC_OPR_NAME;
            vm.formItem.PRD_PHASE_NAME = dphlist[0].PRD_PHASE_NAME;
            vm.formItem.DYE_MTHD_NAME = dlist[0].DYE_MTHD_NAME;
            vm.formItem.MOU_CODE = mouList[0].MOU_CODE;

            if (vm.formItem.uid) {
                // Update
                var row = vm.recipeItemList.getByUid(vm.formItem.uid);
                //alert(vm.formItem.STEP_NO);
                row["STEP_NO"] = vm.formItem.STEP_NO;
                row["DYE_DOSE_TMPLT_D_ID"] = vm.formItem.DYE_DOSE_TMPLT_D_ID;
                row["DYE_PRD_PHASE_TYPE_ID"] = vm.formItem.DYE_PRD_PHASE_TYPE_ID;
                row["LK_DYE_MTHD_ID"] = vm.formItem.LK_DYE_MTHD_ID;
                row["DYE_PROC_OPR_TYPE_ID"] = vm.formItem.DYE_PROC_OPR_TYPE_ID;
                row["DC_AGENT_ID"] = vm.formItem.DC_AGENT_ID;
                row["ALT_AGENT_ID"] = vm.formItem.ALT_AGENT_ID;
                row["DOSE_QTY"] = vm.formItem.DOSE_QTY;
                row["DOSE_MOU_ID"] = vm.formItem.DOSE_MOU_ID;
                row["IS_LABDIP"] = vm.formItem.IS_LABDIP;
                row["IS_LN_BRK"] = vm.formItem.IS_LN_BRK;

                row["PRD_PHASE_NAME"] = vm.formItem.PRD_PHASE_NAME;

                row["DYE_MTHD_NAME"] = vm.formItem.DYE_MTHD_NAME;
                row["PROC_OPR_NAME"] = vm.formItem.PROC_OPR_NAME;

                row["DC_AGENT_NAME"] = vm.formItem.DC_AGENT_NAME;
                row["ALT_AGENT_NAME"] = vm.formItem.ALT_AGENT_NAME;
                row["MOU_CODE"] = vm.formItem.MOU_CODE;
                
            } else {
                StepNo = vm.formItem.STEP_NO + 1;
                //insert
                if (vm.recipeItemList != null) {

                    var idx = vm.recipeItemList.data().length + 1;
                    vm.formItem.SL_NO = idx;
                    vm.recipeItemList.insert(idx, vm.formItem);
                }
                else {
                    var idx = 1;
                    vm.recipeItemList.insert(idx, vm.formItem);
                }

            }
            vm.formItem = { DYE_PRD_PHASE_TYPE_ID: PHASE_TYPE_ID, LK_DYE_MTHD_ID: LD_M_ID, DYE_PROC_OPR_TYPE_ID: '', DC_AGENT_ID: '', DOSE_MOU_ID: '', STEP_NO: StepNo };

        };

        vm.gridOptionsRecipeItem = {
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
            height: '200px',
            scrollable: true,
            columns: [
                {
                    field: "STEP_NO",
                    title: "Step No",
                    width: "10%"
                },

                { field: "DYE_DOSE_TMPLT_D_ID", hidden: true },
                { field: "DYE_DOSE_TMPLT_H_ID", hidden: true },
                { field: "DYE_PRD_PHASE_TYPE_ID", hidden: true },
                { field: "LK_DYE_MTHD_ID", hidden: true },
                { field: "DYE_PROC_OPR_TYPE_ID", hidden: true },
                { field: "DC_AGENT_ID", hidden: true },
                { field: "ALT_AGENT_ID", hidden: true },
                { field: "DOSE_MOU_ID", hidden: true },
                { field: "ALT_AGENT_NAME", hidden: true },


                //{ field: "PRD_PHASE_NAME", title: "Phase Name", width: "15%" },
                { field: "DYE_MTHD_NAME", title: "Dye Method", width: "15%" },
                { field: "PRD_PHASE_NAME", title: "Phase Name", width: "15%" },
                { field: "PROC_OPR_NAME", title: "Process Name", width: "15%" },
                { field: "DC_AGENT_NAME", title: "Agent Name", width: "20%" },
                { field: "IS_LABDIP", title: "Lab-dip", width: "10%" },
                { field: "IS_LN_BRK", title: "Line Brk", width: "10%" },
                
                { field: "DOSE_QTY", title: "DOSE QTY", width: "10%" },
                { field: "MOU_CODE", title: "Unit", width: "10%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>  <a  title="Edit" ng-click="vm.editRecipeItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "10%"
                }
            ],
        };

        vm.editRecipeItemData = function (data) {
            vm.formItem = angular.copy(data);
        }

        vm.removeItemData = function (data) {

            if (!data.DYE_DOSE_TMPLT_D_ID || data.DYE_DOSE_TMPLT_D_ID <= 0) {
                vm.recipeItemList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.DC_AGENT_NAME + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.recipeItemList.remove(data);
                 });

        }

        vm.submitAll = function (dataOri) {

            var data = angular.copy(dataOri);

            data['MC_COLOR_GRP_LST'] = !data.MC_COLOR_GRP_LST ? '0' : data.MC_COLOR_GRP_LST.join(',');

            data["XML_DOS_D"] = DyeingDataService.xmlStringShort(vm.recipeItemList.data().map(function (o) {
                return {
                    DYE_DOSE_TMPLT_D_ID: o.DYE_DOSE_TMPLT_D_ID == null ? 0 : o.DYE_DOSE_TMPLT_D_ID,
                    DYE_PRD_PHASE_TYPE_ID: o.DYE_PRD_PHASE_TYPE_ID == null ? 0 : o.DYE_PRD_PHASE_TYPE_ID,
                    LK_DYE_MTHD_ID: o.LK_DYE_MTHD_ID,
                    DYE_PROC_OPR_TYPE_ID: o.DYE_PROC_OPR_TYPE_ID == null ? 0 : o.DYE_PROC_OPR_TYPE_ID,
                    DC_AGENT_ID: o.DC_AGENT_ID == null ? 0 : o.DC_AGENT_ID,
                    ALT_AGENT_ID: o.ALT_AGENT_ID == null ? 0 : o.ALT_AGENT_ID,
                    STEP_NO: o.STEP_NO == null ? 0 : o.STEP_NO,
                    DOSE_QTY: o.DOSE_QTY == null ? 0 : o.DOSE_QTY,
                    IS_LABDIP: o.IS_LABDIP == null ? "N" : o.IS_LABDIP,
                    IS_LN_BRK: o.IS_LN_BRK == null ? "N" : o.IS_LN_BRK,
                    DOSE_MOU_ID: o.DOSE_MOU_ID == null ? 0 : o.DOSE_MOU_ID
                }
            }));

            return DyeingDataService.saveDataByUrl(data, '/LabdipRecipe/DosSave').then(function (res) {

                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                    }
                    config.appToastMsg(res.data.PMSG);
                    $state.go('DosingTemplate', {}, { reload: true });
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });


        };



        vm.reset = function () {
            vm.form.EFFECT_FROM = vm.today;
            vm.form.KNT_MACHINE_ID = null;
            vm.form.KNT_MACHINE_NO = null;
            vm.form.HR_EMPLOYEE_ID = null;
            vm.form.EMPLOYEE_CODE = null;
            vm.form.EMP_FULL_NAME_EN = null;
        }

        vm.recipeItemList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLDDosingTempleteDtl?pDYE_DOSE_TMPLT_H_ID=' + (vm.form.DYE_DOSE_TMPLT_H_ID || 0)).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });

    }


})();
