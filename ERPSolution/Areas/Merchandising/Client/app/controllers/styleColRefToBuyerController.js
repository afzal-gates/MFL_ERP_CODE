(function () {
    'use strict';

    angular.module('multitex.mrc').controller('StyleColRefToBuyerController', ['$scope', 'Style', '$state', 'config', 'MrcDataService', '$modal', '$timeout', 'ColGrpList', 'DyeingMethod', 'Dialog', StyleColRefToBuyerController]);

    function StyleColRefToBuyerController($scope, Style, $state, config, MrcDataService, $modal, $timeout, ColGrpList, DyeingMethod, Dialog) {

        getColourDataByBuyer();
        getTnaTaskNavigationData();
        function getTnaTaskNavigationData() {
            return MrcDataService.getDataByUrl("/TnaTask/SelectAll?IS_ORDER_EXEC=Y").then(function (res) {
                $scope.MC_TNA_TASK_ID = res[0].MC_TNA_TASK_ID;
                $scope.tnaTaskList = res;
            });
        }

        $scope.save = function (token) {

            var adata = [];
            var XML = null;

            var data = $scope.dataSource.data();
                angular.forEach(data, function (val, key) {
                    adata.push({
                        MC_BU_COL_REF_ID: val.MC_BU_COL_REF_ID,
                        MC_COLOR_ID: val.hasOwnProperty('OB_COLOR') ? (val.OB_COLOR ? val.OB_COLOR.MC_COLOR_ID : '') : '',
                        RF_FIB_COMP_ID: val.hasOwnProperty('OB_FIBER_COMP') ? (val.OB_FIBER_COMP ? val.OB_FIBER_COMP.RF_FIB_COMP_ID : '') : '',

                        COLOR_NAME_EN: val.OB_COLOR.COLOR_NAME_EN,

                        MC_COLOR_GRP_ID: val.hasOwnProperty('OB_COLOUR_GROUP') ? (val.OB_COLOUR_GROUP ? val.OB_COLOUR_GROUP.MC_COLOR_GRP_ID : null) : null,
                        LK_DYE_MTHD_ID: val.hasOwnProperty('OB_DYE_METHOD') ? (val.OB_DYE_METHOD ? val.OB_DYE_METHOD.LK_DYE_MTHD_ID : null) : null,

                        PANTON_NO: (val.PANTON_NO||''),
                        COLOR_REF: (val.COLOR_REF || ''),

                        OTHER_REF: val.OTHER_REF || '',//Shade Ref #
                        APRV_LD_REF: val.APRV_LD_REF || '',
                        MC_STYLE_D_ITEM_LST: val.MC_STYLE_D_ITEM_LST,
                        IS_GMT_COL: val.IS_GMT_COL,
                        IS_ACTIVE: val.IS_ACTIVE,
                        IS_SWATCH: val.IS_SWATCH || 'N',
                        IS_ADD_SD_COL: val.IS_ADD_SD_COL || 'N',
                        UNIQ_COL: (val.hasOwnProperty('OB_COLOR') ? (val.OB_COLOR.MC_COLOR_ID ? val.OB_COLOR.MC_COLOR_ID : '') : '') + (val.OTHER_REF||'')
                    });
                });

                if ($state.current.LK_STYL_DEV_ID != 265) {
                    if (_.filter(adata, function (o) {
                     return !o.MC_STYLE_D_ITEM_LST
                    }).length > 0) {
                        toastr.error('Please check Colour Wise Item List. Some color are missing Item.', "MultiTEX");
                        return;
                    };

                    if (_.filter(adata, function (o) {
                           return (!o.MC_COLOR_GRP_ID || !o.LK_DYE_MTHD_ID);
                    }).length > 0) {
                        toastr.info('Color Group or Dyeing Method is missing.', "MultiTEX");
                        return;
                    };
                }
               

              
 
                if( _.map(_.groupBy(adata, function (o) {
                    return o.UNIQ_COL;
                }), function (g) {
                    return g[0];
                }).length != adata.length) {

                    var duplicatedRow = _.filter(_.map(_.groupBy(adata, function (o) {
                        return o.UNIQ_COL;
                    }), function (g) {
                        return {
                            COLOR_NAME_EN: g[0].COLOR_NAME_EN,
                            COUNT: g.length
                        }
                    }), function (o) {
                        return o.COUNT > 1;
                    });
                    
                    toastr.error('Color  duplication is not allowed. Hint :color [' + duplicatedRow[0].COLOR_NAME_EN + ']', "MultiTEX");

                } else {
                    if (adata.length > 0) {
                       XML = MrcDataService.xmlStringShort(adata.reverse());
                    } else {
                        XML = '<trans></trans>';
                    }

                    return MrcDataService.saveDataByUrl({ MC_STYLE_H_ID: Style.MC_STYLE_H_ID, XML: XML }, '/ColourMaster/UpdateWithXML', token).then(function (res) {
                                res['data'] = angular.fromJson(res.jsonStr);
                                config.appToastMsg(res.data.PMSG);
                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    return getColourDataByBuyer();
                                } else {
                                    toastr.error('Colour duplication is not allowed', "MultiTEX");
                                }
                    })
                }


                
               
        };

        $scope.addNew = function () {
            
            var data = {
                MC_BU_COL_REF_ID: -1,

                OB_COLOR: { MC_COLOR_ID: '', COLOR_NAME_EN: '--Select--' },
                OB_COLOUR_GROUP:{ MC_COLOR_GRP_ID:'', COLOR_GRP_NAME_EN: '--select--' },
                OB_DYE_METHOD : { LK_DYE_MTHD_ID: '', DYE_MTHD_NAME: '--Select--' },
                OB_FIBER_COMP: { RF_FIB_COMP_ID: '', FIB_COMP_NAME: '--Select--' },


                COLOR_REF: '',
                OTHER_REF: '',
                LK_DYE_MTHD_ID: 1,
                MC_COLOR_GRP_ID: 0,
                MC_STYLE_D_ITEM_LST: $scope.$parent.StyleData.ALL_ITEM_LIST,
                IS_GMT_COL: 'Y',
                IS_ACTIVE: 'Y',
                IS_SWATCH: 'N',
                IS_USED: 'N'
            }
            $scope.dataSource.insert(0,data);
        }


       $scope.showItemList = function (MC_STYLE_H_ID, dataItemOri) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ItemListWithColour.html',
                controller: function ($scope, $modalInstance, ItemList) {

                    $scope.valid = true;
                    $scope.ItemList = angular.copy(ItemList);
                     var dataItem = angular.copy(dataItemOri)
                     
                    var List = _.map(dataItem.MC_STYLE_D_ITEM_LST.split(','), function (v) {
                        return parseInt(v);
                    });

                    angular.forEach($scope.ItemList, function (val, key) {
                        val.IS_MAPPED = _.some(List, function (v) {
                            return v == val.MC_STYLE_D_ITEM_ID;
                        });

                        if (val.CHILD_ITEMS.length > 0) {
                            angular.forEach(val.CHILD_ITEMS, function (val1, key1) {
                                val1.IS_MAPPED = _.some(List, function (v1) {
                                    return v1 == val1.MC_STYLE_D_ITEM_ID;
                                });
                            });
                        }
                    });

                    $scope.selectOrDeselect = function (IS) {
                        angular.forEach($scope.ItemList, function (val, key) {

                            if (val.CHILD_ITEMS.length == 0) {
                                val.IS_MAPPED = IS;
                            }; 

                            if (val.CHILD_ITEMS.length > 0) {
                                angular.forEach(val.CHILD_ITEMS, function (val1, key1) {
                                    val1.IS_MAPPED = IS;
                                });
                            }
                        });
                    }

                    $scope.save = function () {
                        var FITEM = [];
                        angular.forEach($scope.ItemList, function (val, key) {
                            if (val.CHILD_ITEMS.length == 0) {
                                if (val.IS_MAPPED) {
                                    FITEM.push({ MC_STYLE_D_ITEM_ID: val.MC_STYLE_D_ITEM_ID });
                                }
                            };
                            if (val.CHILD_ITEMS.length > 0) {
                                angular.forEach(val.CHILD_ITEMS, function (val1, key1) {
                                    if (val1.IS_MAPPED) {
                                        FITEM.push({ MC_STYLE_D_ITEM_ID: val1.MC_STYLE_D_ITEM_ID });
                                    }                                    
                                });
                            }
                        });

                        if (FITEM.length == 0) {
                            $scope.valid = false;
                            return;
                        }

                        $scope.valid = true;
                        $modalInstance.close(_.map(FITEM, 'MC_STYLE_D_ITEM_ID').join(','));
                    };

                    $scope.cancel = function (data) {
                        $modalInstance.close(null);
                    }



                },
                size: 'md',
                windowClass: 'large-Modal',
                resolve: {
                    ItemList: function (MrcDataService) {
                        return MrcDataService.getDataByUrl('/StyleDItem/StyleDtlItemList/' + MC_STYLE_H_ID + '/N');
                    }
                }
            });

            modalInstance.result.then(function (data) {
                if (data) {
                    dataItemOri.MC_STYLE_D_ITEM_LST = data;
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        };


       $scope.remove = function (data) {

           if (data.MC_BU_COL_REF_ID < 0) {
               $scope.dataSource.remove(data);
               return;
           };

           Dialog.confirm('Removing "' + data.COLOR_NAME_EN + '" Colour', 'Are you sure?', ['Yes', 'No'])
                .then(function () {
                    $scope.dataSource.remove(data);
            });
            
        }

        $scope.makeEditMode = function (data) {

            data['editMode'] = !data['editMode'];
        }

        $scope.cancel = function()
        {
           return getColourDataByBuyer();
        }

        $scope.Style = Style;
        
        function ColourChanged(data, dataItem) {

            console.log(data);

            if (data.MC_COLOR_ID) {
                if (data.MC_COLOR_ID < 0) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'NewColourEntryModal.html',
                            controller: 'ColourMasterModalController',
                            controllerAs:'vm',
                            size: 'lg',
                            windowClass: 'large-Modal',
                            resolve:{
                                    ColourList: function (MrcDataService) {
                                        return MrcDataService.selectAllData('ColourMaster');
                                    }
                                }
                        });

                        modalInstance.result.then(function (result) {
                            if (result.MC_COLOR_ID && result.MC_COLOR_ID > 0) {
                                
                                dataItem['COL_TYPE_NAME'] = result.COL_TYPE_NAME;
                                dataItem['PANTON_NO'] = result.PANTON_NO;
                                dataItem['COLOR_REF'] = result.COLOR_REF;

                                dataItem['OB_COLOR'] = { MC_COLOR_ID: result.MC_COLOR_ID, COLOR_NAME_EN: result.COLOR_NAME_EN };
                                dataItem['OB_COLOUR_GROUP'] = { MC_COLOR_GRP_ID: result.MC_COLOR_GRP_ID, COLOR_GRP_NAME_EN: result.COLOR_GRP_NAME_EN };
                                dataItem['OB_DYE_METHOD'] = { LK_DYE_MTHD_ID: '', DYE_MTHD_NAME: '--Select--' };
                                dataItem['OB_FIBER_COMP'] = { RF_FIB_COMP_ID: '', FIB_COMP_NAME: '--Select--' };

                                dataItem['MC_STYLE_D_ITEM_LST'] = $scope.$parent.StyleData.ALL_ITEM_LIST;
                                dataItem['IS_GMT_COL'] = 'Y';
                                dataItem['IS_ACTIVE'] = 'Y';
                                $scope.dataSource.sync();

                            }
                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });


                    $scope.ColourDetails.$valid = false;

                } else {

                    if (data.IS_DUMMY_COLOR === 'Y') {
        
                        dataItem['OB_COLOUR_GROUP'] = { MC_COLOR_GRP_ID: 10, COLOR_GRP_NAME_EN: 'N/A' };
                        dataItem['OB_DYE_METHOD'] = { LK_DYE_MTHD_ID: 7, DYE_MTHD_NAME: _.find(DyeingMethod, function (o) { return o.LK_DYE_MTHD_ID == 7; }).DYE_MTHD_NAME };

                        dataItem['COL_TYPE_NAME'] = data.COL_TYPE_NAME;

                        dataItem['COLOR_EQUIV'] = data.COLOR_EQUIV;
                        dataItem['disabledPanton'] = data.PANTON_NO ? true : false;

                        dataItem['PANTON_NO'] = data.PANTON_NO;
                        dataItem['MC_STYLE_D_ITEM_LST'] = ($scope.$parent.StyleData.ALL_ITEM_LIST || -1);
                        dataItem['IS_GMT_COL'] = 'Y';
                        dataItem['IS_ACTIVE'] = 'Y';
                        dataItem['IS_SWATCH'] = 'Y';
                        dataItem['MC_COLOR_GRP_ID_REQ'] = true;
                    } else {
                        dataItem['COL_TYPE_NAME'] = data.COL_TYPE_NAME;

                        dataItem['COLOR_EQUIV'] = data.COLOR_EQUIV;
                        dataItem['disabledPanton'] = data.PANTON_NO ? true : false;

                        dataItem['PANTON_NO'] = data.PANTON_NO;
                        dataItem['MC_STYLE_D_ITEM_LST'] = ($scope.$parent.StyleData.ALL_ITEM_LIST || -1);
                        dataItem['IS_GMT_COL'] = 'Y';
                        dataItem['IS_ACTIVE'] = 'Y';
                        dataItem['MC_COLOR_GRP_ID_REQ'] = true;
                    }

                    $scope.dataSource.sync();

                }


            } else {
                dataItem['COLOR_EQUIV'] = '';
                dataItem['PANTON_NO'] = '';

            }
        }

        function onChangeColGroup(data,dataItem) {
            if (data.MC_COLOR_GRP_ID == 1) {
                dataItem['OB_DYE_METHOD'] = { LK_DYE_MTHD_ID: 5, DYE_MTHD_NAME: _.find(DyeingMethod, function (o) { return o.LK_DYE_MTHD_ID == 5; }).DYE_MTHD_NAME };
            } else if (data.MC_COLOR_GRP_ID == 4) {
                dataItem['OB_DYE_METHOD'] = { LK_DYE_MTHD_ID: 6, DYE_MTHD_NAME: _.find(DyeingMethod, function (o) { return o.LK_DYE_MTHD_ID == 6; }).DYE_MTHD_NAME };
            } else if (data.MC_COLOR_GRP_ID == 10) {
                dataItem['OB_DYE_METHOD'] = { LK_DYE_MTHD_ID: 7, DYE_MTHD_NAME: _.find(DyeingMethod, function (o) { return o.LK_DYE_MTHD_ID == 7; }).DYE_MTHD_NAME };
            }else {
                dataItem['OB_DYE_METHOD'] = { LK_DYE_MTHD_ID: 1, DYE_MTHD_NAME: _.find(DyeingMethod, function (o) { return o.LK_DYE_MTHD_ID == 1; }).DYE_MTHD_NAME };
            }
            $scope.dataSource.sync();
        }

        var gridColumnsNor = [
                
                 { field: "OB_COLOR", title: "Color", width: "100px", editor: colorDropDownEditor, template: "#=OB_COLOR.COLOR_NAME_EN#" },
                 {
                     title: "Type",
                     field: 'COL_TYPE_NAME',
                     width: "40px",
                     sortable: { multi: true, search: true }
                 },
                 { field: "OB_COLOUR_GROUP", title: "Color Group", width: "60px", editor: colorGroupDropDownEditor, template: "#=OB_COLOUR_GROUP.COLOR_GRP_NAME_EN#" },

                 { field: "OB_DYE_METHOD", title: "Dye Method", width: "60px", editor: lkDyeMethodDropDownEditor, template: "#=OB_DYE_METHOD.DYE_MTHD_NAME#" },

                 { field: "OB_FIBER_COMP", title: "Composition", width: "100px", editor: fiberCompDropDownEditor, template: "#=OB_FIBER_COMP.FIB_COMP_NAME#" },

                {
                    title: "ColourRef #",
                    template: function () {
                        return "<input type='text' ng-model='dataItem.COLOR_REF' name='colour-{{dataItem.uid}}' class='form-control' ng-disabled='dataItem.editMode'/>";
                    },
                    width: "60px"
                },

                {
                    title: "Shade #",
                    template: function () {
                        return "<input type='text' ng-model='dataItem.OTHER_REF'  class='form-control' ng-disabled='dataItem.editMode'/>";
                    },
                    width: "50px"
                },
                {
                    //headerTemplate: "<i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'Is Swatch?'\"></i>",
                    title: "Swatch?",
                    template: function () {
                        return "<input type='checkbox' title='Is Swatch?' ng-model='dataItem.IS_SWATCH' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-change='onWwatchChanged(dataItem)'>";
                    },
                    width: "40px"
                },

                {
                    title: "Panton #",
                    template: function () {
                        return "<input type='text' ng-model='dataItem.PANTON_NO'  class='form-control'  ng-required='dataItem.IS_SWATCH==\"N\"'/>";
                    },
                    width: "80px"
                },
                {
                    headerTemplate: "<i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'Should add Solid Colour?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Should add Solid Colour?' ng-model='dataItem.IS_ADD_SD_COL' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='dataItem.LK_COL_TYPE_ID ==358 ||dataItem.LK_COL_TYPE_ID == 407'>";
                    },
                    width: "20px"
                },

                {
                    headerTemplate: "<i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'Is it Garment Colour?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Is it Garment Colour?' ng-model='dataItem.IS_GMT_COL' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='dataItem.editMode'>";
                    },
                    width: "20px"
                },
                {
                    headerTemplate: "<i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'Is Active?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Is it Active?' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='dataItem.editMode'>";
                    },
                    width: "20px"
                },
                {
                    title: "Action",
                    //template: '<a  title="Already Used" ng-if="dataItem.IS_USED==\'Y\'" class="btn btn-xs red"><i class="fa fa-lock"></i> </a> <a  title="Edit" ng-click="makeEditMode(dataItem)" ng-if="dataItem.IS_USED==\'N\'" class="btn btn-xs blue" ng-disabled="!dataItem.MC_BU_COL_REF_ID"><i class="fa fa-pencil"></i> </a><a  title="Delete"  ng-if="dataItem.IS_USED==\'N\'" ng-click="remove(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>',
                    template: '<a  title="Already Used" ng-if="dataItem.IS_USED==\'Y\'" class="btn btn-xs red"><i class="fa fa-lock"></i> </a> <a  title="Edit" ng-click="makeEditMode(dataItem)" ng-if="dataItem.IS_USED==\'N\'" class="btn btn-xs blue" ng-disabled="!dataItem.MC_BU_COL_REF_ID"><i class="fa fa-pencil"></i> </a>  <a  title="Show Item List" ng-click="showItemList(Style.MC_STYLE_H_ID,dataItem)" class="btn btn-xs yellow"><i class="fa fa-bars"></i></a> <a  title="Delete"  ng-if="dataItem.IS_USED==\'N\'" ng-click="remove(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>',
                    width: "50px"
                }
        ];

        var gridColumnsDev = [

                { field: "OB_COLOR", title: "Color", width: "100px", editor: colorDropDownEditor, template: "#=OB_COLOR.COLOR_NAME_EN#" },
                {
                    title: "Type",
                    field: 'COL_TYPE_NAME',
                    width: "40px",
                    sortable: { multi: true, search: true }
                },
                { field: "OB_COLOUR_GROUP", title: "Color Group", width: "60px", editor: colorGroupDropDownEditor, template: "#=OB_COLOUR_GROUP.COLOR_GRP_NAME_EN#" },
                { field: "OB_DYE_METHOD", title: "Dye Method", width: "60px", editor: lkDyeMethodDropDownEditor, template: "#=OB_DYE_METHOD.DYE_MTHD_NAME#" },
                { field: "OB_FIBER_COMP", title: "Composition", width: "100px", editor: fiberCompDropDownEditor, template: "#=OB_FIBER_COMP.FIB_COMP_NAME#" },

                {
                    title: "ColourRef #",
                    template: function () {
                        return "<input type='text' ng-model='dataItem.COLOR_REF' name='colour-{{dataItem.uid}}' class='form-control' ng-disabled='dataItem.editMode'/>";
                    },
                    width: "60px"
                },

                {
                    title: "Shade #",
                    template: function () {
                        return "<input type='text' ng-model='dataItem.OTHER_REF'  class='form-control' ng-disabled='dataItem.editMode'/>";
                    },
                    width: "50px"
                },
                {
                    //headerTemplate: "<i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'Is Swatch?'\"></i>",
                    title: "Swatch?",
                    template: function () {
                        return "<input type='checkbox' title='Is Swatch?' ng-model='dataItem.IS_SWATCH' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-change='onWwatchChanged(dataItem)'  ng-disabled='dataItem.editMode'>";
                    },
                    width: "40px"
                },

                {
                    title: "Panton #",
                    template: function () {
                        return "<input type='text' ng-model='dataItem.PANTON_NO'  class='form-control'  ng-required='dataItem.IS_SWATCH==\"N\"'/>";
                    },
                    width: "80px"
                },
              
                {
                    title: "Add Solid Col?",
                    template: function () {
                        return "<input type='checkbox' title='Should add Solid Colour?' ng-model='dataItem.IS_ADD_SD_COL' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='dataItem.LK_COL_TYPE_ID ==358 ||dataItem.LK_COL_TYPE_ID == 407'>";
                    },
                    width: "20px"
                },

                {
                    title:'Is it Gmt Col?',
                    template: function () {
                        return "<input type='checkbox' title='Is it Garment Colour?' ng-model='dataItem.IS_GMT_COL' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='dataItem.editMode'>";
                    },
                    width: "20px"
                },
                {
                    title:'Active?',
                    template: function () {
                        return "<input type='checkbox' title='Is it Active?' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='dataItem.editMode'>";
                    },
                    width: "20px"
                },
                {
                    title: "Action",
                  //  template: '<a  title="Already Used" ng-if=dataItem.IS_USED == \'Y\'" class="btn btn-xs red"><i class="fa fa-lock"></i> </a>  <a  title="Edit" ng-click="makeEditMode(dataItem)" ng-if="dataItem.IS_USED==\'N\'" class="btn btn-xs blue" ng-disabled="!dataItem.MC_BU_COL_REF_ID"><i class="fa fa-pencil"></i> </a><a ng-if="dataItem.IS_USED==\'N\'"  title="Delete" ng-click="remove(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>',
                    template: '<a  title="Already Used" ng-if=dataItem.IS_USED == \'Y\'" class="btn btn-xs red"><i class="fa fa-lock"></i> </a>  <a  title="Edit" ng-click="makeEditMode(dataItem)" ng-if="dataItem.IS_USED==\'N\'" class="btn btn-xs blue" ng-disabled="!dataItem.MC_BU_COL_REF_ID"><i class="fa fa-pencil"></i> </a>  <a  title="Show Item List" ng-click="showItemList(Style.MC_STYLE_H_ID,dataItem)" class="btn btn-xs yellow"><i class="fa fa-bars"></i></a> <a ng-if="dataItem.IS_USED==\'N\'"  title="Delete" ng-click="remove(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>',
                    width: "50px"
                }
        ];


        $scope.gridColumns = $state.current.LK_STYL_DEV_ID == 265 ? gridColumnsDev : gridColumnsNor;

        function colorGroupDropDownEditor(container, options) {
            if (options.model.editMode) {
                $("<span>" + options.model.get(options.field).COLOR_GRP_NAME_EN + "</span>").appendTo(container);
                return;
            }
            

            $('<input data-text-field="COLOR_GRP_NAME_EN" data-value-field="MC_COLOR_GRP_ID" data-bind="value:' + options.field + '" required/>')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                e.success(ColGrpList);
                            }
                        }
                    },
                    change: function (e) {
                        
                        onChangeColGroup(this.dataItem(e.item), options.model);
         
                    }
                });
        }

        function colorDropDownEditor(container, options) {
            if (options.model.editMode) {
                $("<span>" + options.model.get(options.field).COLOR_NAME_EN + "</span>").appendTo(container);
                return;
            }

            $('<input data-text-field="COLOR_NAME_EN" data-value-field="MC_COLOR_ID" data-bind="value:' + options.field + '" required/>')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                var url = "/ColourMaster/SelectAll?pOption=3010";
                                var webapi = new kendo.data.transports.webapi({});
                                var params = webapi.parameterMap(e.data);
                                if (params.filter) {
                                    url += '&pCOLOR_NAME_EN=' + params.filter.replace(/'/g, '').split('~')[2];
                                }
                                return MrcDataService.getDataByUrl(url).then(function (res) {
                                    e.success([{ MC_COLOR_ID: -1, COLOR_NAME_EN: '--New Colour--' }].concat(res));
                                });
                            }
                        },
                        serverFiltering: true
                    },
                    change: function (e) {
                        ColourChanged(this.dataItem(e.item), options.model);
                    }
                });
        }

        function fiberCompDropDownEditor(container, options) {
            if (options.model.editMode) {
                $("<span>" + options.model.get(options.field).FIB_COMP_NAME + "</span>").appendTo(container);
                return;
            }

            $('<input data-text-field="FIB_COMP_NAME" data-value-field="RF_FIB_COMP_ID" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    optionLabel:'N/A',
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                var url = "/api/Common/FibreCompList?pOption=3002";
                                var webapi = new kendo.data.transports.webapi({});
                                var params = webapi.parameterMap(e.data);
                                if (params.filter) {
                                    url += '&pFIB_COMP_NAME=' + params.filter.replace(/'/g, '').split('~')[2];
                                }
                               
                                return MrcDataService.getDataByFullUrl(url).then(function (res) {
                                    e.success(res);
                                });
                            },
                            serverFiltering: true
                        }
                    }
                });
        }
        function lkDyeMethodDropDownEditor(container, options) {

            if (options.model.editMode) {
                $("<span>" + options.model.get(options.field).DYE_MTHD_NAME + "</span>").appendTo(container);
                return;
            }

            $('<input data-text-field="DYE_MTHD_NAME" data-value-field="LK_DYE_MTHD_ID" data-bind="value:' + options.field + '" required/>')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                e.success(DyeingMethod);
                            }
                        }
                    },
                    //change: function (e) {
                    //    options.model.INV_ITEM_TYPE = {
                    //        INV_ITEM_ID: '',
                    //        ITEM_NAME_EN: '--N/A--'
                    //    }
                    //    vm.dsHeaderD.sync();
                    //}
                });
        }
        function getColourDataByBuyer() {
            return MrcDataService.getDataByUrl('/ColourMaster/ColourMappDataByBuyerId/' + Style.MC_STYLE_H_ID + '?pLK_COL_TYPE_LIST').then(function (res) {
                angular.forEach(res, function (val, key) {
                    val.disabledPanton = val.PANTON_NO ? true : false;
                    val.editMode = val.MC_BU_COL_REF_ID ? true : false;
                });
                $scope.dataSource = new kendo.data.DataSource({
                    data: res,
                    schema: {
                        model: {
                            id: "MC_BU_COL_REF_ID",
                            fields: {
                                OB_COLOUR_GROUP: { defaultValue: { MC_COLOR_GRP_ID: "", COLOR_GRP_NAME_EN: "--Select--" }, editable: true },
                                OB_DYE_METHOD: { defaultValue: { LK_DYE_MTHD_ID: "", DYE_MTHD_NAME: "--Select--" }, editable: true },
                                OB_FIBER_COMP: { defaultValue: { RF_FIB_COMP_ID: "", FIB_COMP_NAME: "--Select--" }, editable: true },
                                OB_COLOR: { defaultValue: { MC_COLOR_ID: "", COLOR_NAME_EN: "--Select--" }, editable: true }
                            }
                        }
                    }
                });
            })
        };


        $scope.openNavigateLabdip = function () {
            var url = '/Merchandising/Mrc/LabdipInfo?a=86/#/LabdipEntry?bAcId=' + $scope.$parent.StyleData.MC_BYR_ACC_ID + '&pMC_BUYER_ID=' + $scope.$parent.StyleData.MC_BUYER_ID;
            url += '&pMC_STYLE_H_ID=' + Style.MC_STYLE_H_ID;
            var a = document.createElement('a');
            a.href = url;
            a.target = '_blank';
            document.body.appendChild(a);
            a.click();
        };

        $scope.openNavigateSampleProg = function () {
            var url = '/Merchandising/Mrc/SmplFabBook?a=88/#/smplfabbookentry/0/dtl?pLK_STYL_DEV_TYP_ID=541&bAcId=' + $scope.$parent.StyleData.MC_BYR_ACC_ID + '&bid=' + $scope.$parent.StyleData.MC_BUYER_ID;
            var a = document.createElement('a');
            a.href = url;
            a.target = '_blank';
            document.body.appendChild(a);
            a.click();
        };


        $scope.goToFabricProjectionBooking = function () {
            var url = '/Merchandising/Mrc/ProjectionOrderList?a=341/#/ProjectionOrder?pMC_PROV_FAB_BK_H_ID=-1&pIS_VIEW=N&pMC_BYR_ACC_ID=' + $scope.$parent.StyleData.MC_BYR_ACC_ID + '&pMC_BUYER_ID=' + $scope.$parent.StyleData.MC_BUYER_ID + '&pMC_STYLE_H_ID=' + $scope.$parent.StyleData.MC_STYLE_H_ID + '&pSTYLE_NO=' + $scope.$parent.StyleData.STYLE_NO;
            var a = document.createElement('a');
            a.href = url;
            a.target = '_self';
            document.body.appendChild(a);
            a.click();
        }
    }

})();