(function () {
    'use strict';
    angular.module('multitex.mrc').controller('GsmYarnConfigController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'Constructions', 'Counts', 'Gauges', 'CountPostions', '$modal', 'FiberTypeList', 'Template', '$timeout', GsmYarnConfigController]);
    function GsmYarnConfigController($q, config, MrcDataService, $stateParams, $state, $scope, logger, Constructions, Counts, Gauges, CountPostions, $modal, FiberTypeList, Template, $timeout) {

        var vm = this;
        vm.errors = null;
        vm.CountList = [];
        vm.CompositionList = [];
        vm.FiberTypeList_2 = [];
        vm.Template = angular.copy(Template);
        var ConstructionsOri = angular.copy(Constructions);

        vm.Title = $state.current.Title || '';
        vm.form = {
            RF_FIB_COMP_ID: '',
            RF_FAB_TYPE_ID: ''
        };


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getYarnCountList(), getFabricTypeList_k(), getMachineGaugeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getFabricCompList_k() {
            return vm.FabricCompList = {
                optionLabel: "---Select---",
                filter: "contains",
                autoBind: false,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(Compositions);
                        }
                    }
                },
                dataTextField: "FIB_COMP_NAME",
                dataValueField: "RF_FIB_COMP_ID"
            };
        }


        function getFabricTypeList_k() {
            return vm.FabricTypeList = {
                optionLabel: "---Select---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(ConstructionsOri);
                        }
                    }
                },
                dataTextField: "FAB_TYPE_NAME",
                dataValueField: "RF_FAB_TYPE_ID"
            };

        }


        function getMachineGaugeList() {
            return vm.MachineGaugeList = {
                optionLabel: "-Select--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(Gauges);
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }



        function getYarnCountList() {
            return vm.YarnCountList = {
                optionLabel: "---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(Counts);
                        }
                    }
                },
                dataTextField: "YR_COUNT_NO",
                dataValueField: "RF_YRN_CNT_ID"
            };
        }

        vm.yarnCountConfigData = function (data) {
            var qParams = '?';
            if (data.RF_FAB_TYPE_ID) {
                qParams += 'pRF_FAB_TYPE_ID=' + data.RF_FAB_TYPE_ID;
            } else {
                qParams += 'pRF_FAB_TYPE_ID';
            }
            if (data.MC_FIB_COMB_TMPLT_ID) {
                qParams += '&pMC_FIB_COMB_TMPLT_ID=' + data.MC_FIB_COMB_TMPLT_ID;
            } else {
                qParams += '&pMC_FIB_COMB_TMPLT_ID';
            }

            if (data.LK_MAC_GG_ID) {
                qParams += '&pLK_MAC_GG_ID=' + data.LK_MAC_GG_ID;
            } else {
                qParams += '&pLK_MAC_GG_ID';
            }

            return MrcDataService.getDataByUrl('/YarnSpec/CountConfigData' + qParams).then(function (res) {
                var dataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res.map(function (o) {
                                var LK_YFAB_PART_LST = '';
                                o['isBackDisabled'] = true;
                                var IS_FBP_VISIBLE = 'N';
                                o.RF_YRN_CNT_LST = o.RF_YRN_CNT_LST.length == 0 ? [
                                    { PART_ID: 426 },
                                    { PART_ID: 427 },
                                    { PART_ID: 428 },
                                ] : o.counts;


                                LK_YFAB_PART_LST = _.filter(ConstructionsOri, function (ob) {
                                    return ob.RF_FAB_TYPE_ID == parseInt(o.RF_FAB_TYPE_ID);
                                })[0].LK_YFAB_PART_LST;

                                IS_FBP_VISIBLE = _.filter(Constructions, function (ob) {
                                    return ob.RF_FAB_TYPE_ID == parseInt(o.RF_FAB_TYPE_ID);
                                })[0].IS_FBP_VISIBLE;



                                if (LK_YFAB_PART_LST.length > 0) {
                                    if (LK_YFAB_PART_LST.split(',').length == 3) {
                                        o['isLoopDisabled'] = false;

                                        if (IS_FBP_VISIBLE === 'Y') {
                                            o['isBackDisabled'] = false;
                                            console.log(IS_FBP_VISIBLE);
                                        } else {
                                            o['isBackDisabled'] = true;
                                        }

                                    } else {
                                        o['isLoopDisabled'] = true;
                                        o['RF_YRN_CNT_LST'][1]['CNT_ID'] = '';

                                    }
                                }

                                return o;
                            }));
                        }
                    }
                });
                $('#kendoGrid').data("kendoGrid").setDataSource(dataSource);
            }, function (err) {
                console.error(err);
            })


        }

        vm.saveGridData = function (formData, token) {
            var xml;
            var data = angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
            if (data.length == 0) {
                xml = '<trans></trans>';
            } else {
                var dataToBeSave = data.map(function (obj) {
                    return {
                        MC_YRN_CNT_CFG_ID: obj.MC_YRN_CNT_CFG_ID,
                        RF_FIB_COMP_ID: obj.RF_FIB_COMP_ID || '',
                        RF_FAB_TYPE_ID: obj.RF_FAB_TYPE_ID,
                        FB_WT_MIN: obj.FB_WT_MIN,
                        FB_WT_MAX: obj.FB_WT_MAX,
                        RF_YRN_CNT_LST: MrcDataService.xmlStringShortNoTag(obj.RF_YRN_CNT_LST.map(function (o) {
                            return {
                                CNT_ID: (o.CNT_ID || ''),
                                PART_ID: o.PART_ID
                            }
                        })),
                        IS_ACTIVE: obj.IS_ACTIVE,
                        LK_MAC_GG_ID: obj.LK_MAC_GG_ID,
                        MC_FIB_COMB_TMPLT_ID: obj.MC_FIB_COMB_TMPLT_ID
                    }
                });

                xml = MrcDataService.xmlStringShort(dataToBeSave.reverse());
            }



            
            return MrcDataService.saveDataByUrl({ MC_FIB_COMB_TMPLT_ID: formData.MC_FIB_COMB_TMPLT_ID, RF_FAB_TYPE_ID: formData.RF_FAB_TYPE_ID, LK_MAC_GG_ID: formData.LK_MAC_GG_ID, XML: xml }, '/YarnSpec/BatchSave', token).then(function (res) {
                res['data'] = angular.fromJson(res.jsonStr);
                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                    vm.yarnCountConfigData(vm.form);
                }
                config.appToastMsg(res.data.PMSG);

            }, function (err) {
                console.log(err);
            })


        }

       $scope.TemplateList = {
            optionLabel: "--Fib. Template--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(vm.Template)
                    }
                }
            },
            dataTextField: "FIB_COMB_TMPLT_NAME",
            dataValueField: "MC_FIB_COMB_TMPLT_ID"
        };





       $scope.addNew = function (dataOri) {
           var data = {};
           
           var LK_YFAB_PART_LST = '';
           data['isBackDisabled'] = true;
           var IS_FBP_VISIBLE = 'N';
           data['RF_YRN_CNT_LST'] = [
                   { PART_ID: 426 },
                   { PART_ID: 427 },
                   { PART_ID: 428 },
           ];


           LK_YFAB_PART_LST = dataOri.RF_FAB_TYPE_ID ? _.filter(ConstructionsOri, function (ob) {
               return ob.RF_FAB_TYPE_ID == parseInt(dataOri.RF_FAB_TYPE_ID);
           })[0].LK_YFAB_PART_LST:'';

           IS_FBP_VISIBLE =dataOri.RF_FAB_TYPE_ID ? _.filter(Constructions, function (ob) {
               return ob.RF_FAB_TYPE_ID == parseInt(dataOri.RF_FAB_TYPE_ID);
           })[0].IS_FBP_VISIBLE :[];



           if (LK_YFAB_PART_LST.length > 0) {
               if (LK_YFAB_PART_LST.split(',').length == 3) {
                   data['isLoopDisabled'] = false;

                   if (IS_FBP_VISIBLE === 'Y') {
                       data['isBackDisabled'] = false;
                   } else {
                       data['isBackDisabled'] = true;
                   }

               } else {
                   data['isLoopDisabled'] = true;
                   data['RF_YRN_CNT_LST'][1]['CNT_ID'] = '';

               }
           };
           data['MC_YRN_CNT_CFG_ID'] = -1;
           data['RF_FAB_TYPE_ID'] = dataOri.RF_FAB_TYPE_ID || '';
           data['LK_MAC_GG_ID'] = dataOri.LK_MAC_GG_ID || '';
           data['MC_FIB_COMB_TMPLT_ID'] = dataOri.MC_FIB_COMB_TMPLT_ID || '';

            $('#kendoGrid').data("kendoGrid").dataSource.insert(0, data);
        }

        $scope.copy = function (dataOri) {
            var dataCopy = angular.copy(dataOri); 
            var dataTbAdd = {
                MC_YRN_CNT_CFG_ID: -1,
                LK_MAC_GG_ID: dataCopy.LK_MAC_GG_ID || '',
                RF_FAB_TYPE_ID: dataCopy.RF_FAB_TYPE_ID || '',
                MC_FIB_COMB_TMPLT_ID: dataCopy.MC_FIB_COMB_TMPLT_ID,
                IS_ACTIVE: 'Y',
                isBackDisabled: dataCopy.isBackDisabled,
                isLoopDisabled: dataCopy.isLoopDisabled,
                FB_WT_MIN: 0,
                FB_WT_MAX: 0,
                RF_YRN_CNT_LST: dataCopy.RF_YRN_CNT_LST
            };
            $('#kendoGrid').data("kendoGrid").dataSource.insert(0, dataTbAdd);
        }


        $scope.remove = function (data) {
            $('#kendoGrid').data("kendoGrid").dataSource.remove(data);
        }


        $scope.makeEditMode = function (data) {
            data['editMode'] = !data['editMode'];
        }

        vm.openFiberConfigModal = function (dataItem) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Merchandising/Mrc/_CombFiberConfig',
                controller: 'FiberConfigModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    FiberTypeList: function () {
                        return FiberTypeList;
                    },
                    TMPLT_H_ID: function () {
                        return dataItem ? dataItem.MC_FIB_COMB_TMPLT_ID : null;
                    },
                    Template: function (MrcDataService) {
                        return MrcDataService.getDataByUrl('/YarnSpec/FibCombTemplateData?pMC_FIB_COMB_TMPLT_ID');
                    }
                }
            });

            modalInstance.result.then(function (data) {
               

                if (angular.isUndefined(data.MC_FIB_COMB_TMPLT_ID)) return;


                var ifTempIdExist = _.some(vm.Template, function (o) {
                    return o.MC_FIB_COMB_TMPLT_ID == parseInt(data.MC_FIB_COMB_TMPLT_ID||0);
                })

                if (!ifTempIdExist) {
                    vm.Template.push({
                        MC_FIB_COMB_TMPLT_ID: data.MC_FIB_COMB_TMPLT_ID,
                        FIB_COMB_TMPLT_NAME: data.FIB_COMB_TMPLT_NAME
                    });

                    $('#' + dataItem.uid).data("kendoDropDownList").dataSource.read();
                    $('#' + dataItem.uid).data("kendoDropDownList").value(data.MC_FIB_COMB_TMPLT_ID);
                    dataItem['MC_FIB_COMB_TMPLT_ID'] = data.MC_FIB_COMB_TMPLT_ID;
                    
                } else {

                    dataItem['MC_FIB_COMB_TMPLT_ID'] = data.MC_FIB_COMB_TMPLT_ID;
                }



                

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };


        vm.reset = function () {
            vm.form = {
                RF_FIB_COMP_ID: '',
                RF_FAB_TYPE_ID: ''
            };

            $('#kendoGrid').data("kendoGrid").dataSource.read();

        };

          vm.FabTypeChanged = function (dataItem) {
            if (!dataItem.RF_FAB_TYPE_ID) {
                return;
            }

            var LK_YFAB_PART_LST = '';
            var IS_FBP_VISIBLE = 'N';
            dataItem['isBackDisabled'] = true;

            LK_YFAB_PART_LST = _.filter(Constructions, function (o) {
                return o.RF_FAB_TYPE_ID == parseInt(dataItem.RF_FAB_TYPE_ID);
            })[0].LK_YFAB_PART_LST;


            IS_FBP_VISIBLE = _.filter(Constructions, function (o) {
                return o.RF_FAB_TYPE_ID == parseInt(dataItem.RF_FAB_TYPE_ID);
            })[0].IS_FBP_VISIBLE;

            if (LK_YFAB_PART_LST.length > 0) {

                if (LK_YFAB_PART_LST.split(',').length == 3) {
                    dataItem['isLoopDisabled'] = false;
                    if (IS_FBP_VISIBLE === 'Y') {
                        dataItem['isBackDisabled'] = false;
                    } else {
                        dataItem['isBackDisabled'] = true;
                    }

                } else {
                    dataItem['isLoopDisabled'] = true;
                    dataItem['RF_YRN_CNT_LST'][1]['CNT_ID'] = '';

                }
            }
        }

        vm.onFirstCntChanged = function (dataItem) {

            if (dataItem.isLoopDisabled) {
                dataItem.RF_YRN_CNT_LST[2].CNT_ID = dataItem.RF_YRN_CNT_LST[0].CNT_ID;
            } else {
                dataItem.RF_YRN_CNT_LST[1].CNT_ID = dataItem.RF_YRN_CNT_LST[0].CNT_ID;
                dataItem.RF_YRN_CNT_LST[2].CNT_ID = dataItem.RF_YRN_CNT_LST[0].CNT_ID;
            }


        }



        vm.gridOptions = {
            autoBind: true,
            height: '400px',
            scrollable: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success([]);
                    }
                }
            },
            filterable: true,
            pageable: false,
            dataBinding: function(e) {
                console.log("dataBinding");
            },
            columns: [
                {
                    title: "Construction",
                    template: function () {
                        return "<select kendo-drop-down-list   options='vm.FabricTypeList' ng-change='vm.FabTypeChanged(dataItem)' ng-model='dataItem.RF_FAB_TYPE_ID' name='RF_FAB_TYPE_ID-{{dataItem.uid}}' class='form-control' required></select>";
                    },
                    width: "100px"
                },
                {
                    title: "Fiber Combination Template",
                    template: function () {
                        return "<div class='input-group'><select kendo-drop-down-list options='TemplateList' id='{{dataItem.uid}}' name='MC_FIB_COMB_TMPLT_ID-{{dataItem.uid}}' ng-model='dataItem.MC_FIB_COMB_TMPLT_ID' class='form-control input-group'></select><span class='input-group-btn'><a ng-click='vm.openFiberConfigModal(dataItem)' title='Edit' class='btn btn-xs blue'><i class='fa' ng-class='dataItem.MC_FIB_COMB_TMPLT_ID ? \"fa-pencil\":\"fa-plus\"'></i> </a></span></div>";
                    },
                    width: "100px"
                },

                {
                    title: "Gauge",
                    template: function () {
                        return "<select kendo-drop-down-list options='vm.MachineGaugeList' ng-model='dataItem.LK_MAC_GG_ID' name='LK_MAC_GG_ID-{{dataItem.uid}}' class='form-control'></select>";
                    },
                    width: "40px"
                },

                {
                    title: "GSM",
                    columns: [
                       {
                           title: "Min",
                           template: function () {
                               return "<input type='number' ng-model='dataItem.FB_WT_MIN' min='1' name='FB_WT_MIN-{{dataItem.uid}}' ng-change='dataItem.FB_WT_MAX=dataItem.FB_WT_MIN' class='form-control'/>";
                           },
                           width: "30px"
                       },

                       {
                           title: "Max",
                           template: function () {
                               return "<input type='number' min='dataItem.FB_WT_MIN' ng-model='dataItem.FB_WT_MAX' name='FB_WT_MAX-{{dataItem.uid}}' class='form-control'/>";
                           },
                           width: "30px"
                       }
                    ]
                },
                {
                    title: "Count",
                    columns: [
                        {
                            title: "Top",
                            template: function () {
                                return "<select kendo-drop-down-list options='vm.YarnCountList' ng-model='dataItem.RF_YRN_CNT_LST[0].CNT_ID' ng-change='vm.onFirstCntChanged(dataItem)'  class='form-control' ng-required></select>";
                            },
                            width: "30px"
                        },

                        {
                            title: "Binding",
                            template: function () {
                                return "<select kendo-drop-down-list options='vm.YarnCountList' ng-model='dataItem.RF_YRN_CNT_LST[1].CNT_ID' class='form-control' ng-disabled='dataItem.isLoopDisabled'></select>";
                            },
                            width: "30px"
                        },

                       {
                           title: "Back",
                           template: function () {
                               return "<select kendo-drop-down-list options='vm.YarnCountList' ng-disabled='dataItem.isBackDisabled' ng-model='dataItem.RF_YRN_CNT_LST[2].CNT_ID' class='form-control' ng-required></select>";
                           },
                           width: "30px"
                       }
                    ]
                },
                {
                    title: "X",
                    template: '<a ng-click="remove(dataItem)" class="btn btn-xs red" title="Remove"><i class="fa fa-times-circle"></i></a>&nbsp;<a ng-click="copy(dataItem)" class="btn btn-xs blue" title="Copy"><i class="fa fa-copy"></i></a>',
                    width: "25px"
                }
            ]
        };

    }

})();