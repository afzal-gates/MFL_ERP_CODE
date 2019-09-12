(function () {
    'use strict';
    angular.module('multitex.mrc').controller('RateChartController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'CurrencyList', 'MOUList', 'DyeingMethod', 'ColourGrp', 'Template', RateChartController]);
    function RateChartController($q, config, MrcDataService, $stateParams, $state, $scope, CurrencyList, MOUList, DyeingMethod, ColourGrp, Template) {

        var vm = this;
        vm.errors = null;
        activate()
        vm.showSplash = true;
        vm.Title = $state.current.Title || '';
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;

            });
        }

        $scope.setTitle = function (title) {
            vm.Title = title;
        }

        $scope.currencyList = CurrencyList;
        $scope.MOUList = MOUList;
        $scope.DyeingMethod = DyeingMethod;
        $scope.ColourGrp = ColourGrp;
        $scope.Template = Template;

        vm.submitData = function (dataOri, token) {
            var data = angular.copy(dataOri);

            return MrcDataService.updateData(data, ctrl, token).then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                    }
                    config.appToastMsg(res.data.PMSG);


                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }
    }

})();

//RateChartKniting
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('RateChartKnitingController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', '$modal', RateChartKnitingController]);
    function RateChartKnitingController($q, config, MrcDataService, $stateParams, $state, $scope, $modal) {

        var vm = this;
        vm.errors = null;
        var MC_FAB_PROC_GRP_ID = 1;
       
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
                $scope.$parent.setTitle($state.current.Title);
            });
        }

        vm.CompGrp = '';
        vm.Construction = '';
        vm.ColType = '';
        vm.CompGrp = '';
        vm.YDType = '';
        vm.FederType = '';
        vm.slub = '';
        vm.isFeeder = 'N';
        vm.IS_FKNIT = false;
        vm.colCuffDes = '';
        vm.Template = angular.copy($scope.$parent.Template);



        vm.form = {
            RATE_MOU_ID: 3,
            RF_CURRENCY_ID: 1,
            MC_RATE_SPEC_KNIT_ID: -1,
            IS_SLUB: 'N',
            FIB_COMB_CONF: [],
            template: [],
            IS_ACTIVE:'Y'
        };
        var copyOfForm = angular.copy(vm.form);


        vm.FabricTypeList = {
            optionLabel: "--Fabric Type--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.getDataByFullUrl('/api/Common/FabricTypeList').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            },
            dataBound: function (e) {
                if (this.dataItem(e.item).IS_FLAT_CIR === 'F') {
                    vm.IS_FKNIT = true;
                } else {
                    vm.IS_FKNIT = false;
                    vm.form['LK_FK_DGN_TYP_ID'] = '';
                }
            },
            select: function (e) {

                if (this.dataItem(e.item).RF_FAB_TYPE_ID) {
                    vm.Construction = this.dataItem(e.item).FAB_TYPE_NAME;
                } else {
                    vm.Construction = '';
                }

                if (this.dataItem(e.item).IS_FLAT_CIR === 'F') {
                    vm.IS_FKNIT = true;
                } else {
                    vm.IS_FKNIT = false;
                    vm.form['LK_FK_DGN_TYP_ID'] = '';
                }
            },
            dataTextField: "FAB_TYPE_NAME",
            dataValueField: "RF_FAB_TYPE_ID"
        };

        vm.ColCuffDesType = {
            optionLabel: "--Design Type--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.LookupListData(78).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            },
            select: function (e) {

                if (this.dataItem(e.item).LOOKUP_DATA_ID) {
                    vm.colCuffDes = this.dataItem(e.item).LK_DATA_NAME_EN;
                } else {
                    vm.colCuffDes = '';
                }


            },

            dataTextField: "LK_DATA_NAME_EN",
            dataValueField: "LOOKUP_DATA_ID"
        };

       

        $scope.TemplateList = {
            optionLabel: "--New Template--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return MrcDataService.getDataByUrl('/YarnSpec/FibCombTemplateData?pMC_FIB_COMB_TMPLT_ID').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.error(err)
                        });
                    }
                }
            },
            select : function(e){
                var item = this.dataItem(e.item);
                if (item.MC_FIB_COMB_TMPLT_ID) {
                    vm.isFeeder = item.IS_ELA_MXD ;
                }
            },
            dataTextField: "FIB_COMB_TMPLT_NAME",
            dataValueField: "MC_FIB_COMB_TMPLT_ID"
        };


        vm.ColourTypelist = {
            optionLabel: "-- Colour Type--",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.LookupListData(74).then(function (res) {
                            console.log(res);
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            },
            select: function (e) {
                if (this.dataItem(e.item).LOOKUP_DATA_ID) {
                    vm.ColType = this.dataItem(e.item).LK_DATA_NAME_EN;

                    if (this.dataItem(e.item).LOOKUP_DATA_ID != 360) {

                        vm.form['LK_YD_TYPE_ID'] = '';
                        vm.YDType = '';
                    }

                } else {
                    vm.ColType = '';
                }
            },
            dataTextField: "LK_DATA_NAME_EN",
            dataValueField: "LOOKUP_DATA_ID"
        };


        vm.YDTypelist = {
            optionLabel: "-- Y/D Type--",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.LookupListData(71).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            },
            select: function (e) {
                if (this.dataItem(e.item).LOOKUP_DATA_ID) {
                    vm.YDType = this.dataItem(e.item).LK_DATA_NAME_EN;
                } else {
                    vm.YDType = '';
                }
            },

            dataTextField: "LK_DATA_NAME_EN",
            dataValueField: "LOOKUP_DATA_ID"
        };



        $scope.$watchGroup(['vm.form.MC_RATE_SPEC_KNIT_ID', 'vm.form.RF_FIB_COMP_GRP_ID', 'vm.form.RF_FAB_TYPE_ID', 'vm.form.LK_COL_TYPE_ID', 'vm.form.LK_YD_TYPE_ID', 'vm.form.LK_FEDER_TYPE_ID', 'vm.form.IS_SLUB', 'vm.form.LK_FK_DGN_TYP_ID'], function (newVal, oldVal) {

            if (newVal[7] === 'Y') {
                vm.slub = 'Slub';
            } else {
                vm.slub = '';
            }

            if ((newVal[0] || 0) <= 0) {
                vm.form['FAB_PROC_NAME'] = (vm.CompGrp + ' ' + vm.Construction + ' ' + vm.ColType + ' ' + vm.YDType + ' ' + vm.FederType + ' ' + vm.slub + ' ' + vm.colCuffDes).replace('  ', ' ');
            }
        }, true)


        vm.FeederTypeList = {
            optionLabel: "--Feeder Type--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.LookupListData(60).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            },
            select: function (e) {
                if (this.dataItem(e.item).LOOKUP_DATA_ID) {
                    vm.FederType = this.dataItem(e.item).LK_DATA_NAME_EN;
                } else {
                    vm.FederType = '';
                }
            },

            dataTextField: "LK_DATA_NAME_EN",
            dataValueField: "LOOKUP_DATA_ID"
        };

        vm.currencyList = {
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.$parent.currencyList);
                    }
                }
            },
            dataTextField: "CURR_CODE",
            dataValueField: "RF_CURRENCY_ID"
        };

        vm.MOUList = {
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.$parent.MOUList)
                    }
                }
            },
            dataTextField: "MOU_CODE",
            dataValueField: "RF_MOU_ID"
        };


        vm.reset = function () {
            vm.form = angular.copy(copyOfForm);
            vm.CompGrp = '';
            vm.Construction = '';
            vm.ColType = '';
            vm.CompGrp = '';
            vm.YDType = '';
            vm.FederType = '';
            vm.slub = '';
            vm.IS_FKNIT = false;

            vm.form['MC_RATE_SPEC_KNIT_ID'] = -1;
            vm.form['MC_FIB_COMB_TMPLT_ID'] = '';
            vm.form['FIB_COMB_CONF'] = [];
            vm.form['RF_FAB_TYPE_ID'] = '';
            vm.form['LK_COL_TYPE_ID'] = '';
            vm.form['LK_YD_TYPE_ID'] = '';
            vm.form['LK_FEDER_TYPE_ID'] = '';
            vm.form['LK_MAC_GG_ID'] = '';
            vm.form['IS_SLUB'] = 'N';
            vm.form['IS_ACTIVE'] = 'Y';
            vm.form['LK_FK_DGN_TYP_ID'] = '';
            $scope.KnittingChartForm.$setPristine();
        };



        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.getDataByUrl('/BudgetSheet/KnittingRateData').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.error(err);
                        })
                    }
                },
                pageSize: 6,
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
            pageable:true,
            sortable: true,

            filterMenuInit: function (e) {
                if (e.field === "FAB_TYPE_NAME" || e.field === "COL_TYPE_NAME" || e.field === "MAC_GG_NAME" || e.field === 'FIB_COMB_TMPLT_NAME') {
                    var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck")
                    filterMultiCheck.container.empty();
                    filterMultiCheck.checkSource.sort({ field: (e.field === 'FAB_TYPE_NAME' ? 'DISPLAY_ORDER' : e.field), dir: "asc" });

                    filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
                    filterMultiCheck.createCheckBoxes();
                }
            },

            columns: [
                    { field: "FAB_TYPE_NAME", title: "Fab Type", type: "string", width: "80px", filterable: { multi: true } },
                    { field: "COL_TYPE_NAME", title: "Type", type: "string", width: "40px", filterable: { multi: true } },
                    { field: "MAC_GG_NAME", title: "G", type: "string", width: "40px", filterable: { multi: true } },
                    { field: "FIB_COMB_TMPLT_NAME", title: "Fib. Combi. Template", type: "string", width: "120px", filterable: { multi: true } },
                    { field: "FAB_PROC_NAME", title: "Rate Desc.", type: "string", width: "130px" },
                    {
                        title: "Rate",
                        template: function () {
                            return "{{dataItem.PROC_RATE +' '+dataItem.CURR_NAME_EN+' /'+dataItem.RATE_MOU}}";
                        },
                        width: "50px"
                    },

                    {
                        title: "Prod.Rate",
                        template: function () {
                            return "{{dataItem.FACT_PROD_RATE +' '+dataItem.CURR_NAME_EN+' /'+dataItem.RATE_MOU}}";
                        },
                        width: "50px"
                    },
                   
                    { field: "IS_SLUB", title: "Sl?", type: "string", width: "40px" },
                     { field: "IS_ACTIVE", title: "Ac?", type: "string", width: "40px" },
                    {
                        title: ">>",
                        template: function () {
                            return "<a  class='btn btn-xs blue' ng-click='vm.edit(dataItem)'><i class='fa fa-edit'> </i></a>";
                        },
                        width: "20px"
                    }
            ]
        };


        vm.edit = function (data) {

            if (data.LK_FK_DGN_TYP_ID && data.LK_FK_DGN_TYP_ID > 0) {
                vm.IS_FKNIT = true;
            } else {
                vm.IS_FKNIT = false;
            }

            var dt = angular.copy(data);
            vm.form = dt;
        }

        vm.openFiberConfigModal = function (MC_FIB_COMB_TMPLT_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Merchandising/Mrc/_CombFiberConfig',
                controller: 'FiberConfigModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    FiberTypeList: function (MrcDataService) {
                        return MrcDataService.LookupListData(76);
                    },
                    TMPLT_H_ID: function () {
                        return MC_FIB_COMB_TMPLT_ID || null;
                    },
                    Template: function (MrcDataService) {
                        return MrcDataService.getDataByUrl('/YarnSpec/FibCombTemplateData?pMC_FIB_COMB_TMPLT_ID');
                    }
                }
            });

            modalInstance.result.then(function (data) {
                if (angular.isUndefined(data.MC_FIB_COMB_TMPLT_ID)) return;
                var ifTempIdExist = _.some(vm.Template, function (o) {
                    return o.MC_FIB_COMB_TMPLT_ID == parseInt(data.MC_FIB_COMB_TMPLT_ID || 0);
                })
                if (!ifTempIdExist) {
                    vm.Template.push({
                        MC_FIB_COMB_TMPLT_ID: data.MC_FIB_COMB_TMPLT_ID,
                        FIB_COMB_TMPLT_NAME: data.FIB_COMB_TMPLT_NAME
                    });

                    $('#MC_FIB_COMB_TMPLT_ID').data("kendoDropDownList").dataSource.read();
                    $('#MC_FIB_COMB_TMPLT_ID').data("kendoDropDownList").value(data.MC_FIB_COMB_TMPLT_ID);
                    vm.form.MC_FIB_COMB_TMPLT_ID = data.MC_FIB_COMB_TMPLT_ID;
                    vm.isFeeder = data.IS_ELA_MXD ||'N';
                } else {
                    $('#MC_FIB_COMB_TMPLT_ID').data("kendoDropDownList").dataSource.read();
                    vm.form.MC_FIB_COMB_TMPLT_ID = data.MC_FIB_COMB_TMPLT_ID;
                    vm.isFeeder = data.IS_ELA_MXD || 'N';
                }





            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };


        vm.submitData = function (dataOri, token, valid) {
            if (!valid) {
                $scope.KnittingChartForm.$submitted = true;
                return;
            }
            var data = angular.copy(dataOri);
            data['MC_FAB_PROC_GRP_ID'] = MC_FAB_PROC_GRP_ID;
            return MrcDataService.saveDataByUrl({
                MC_RATE_SPEC_KNIT_ID: data.MC_RATE_SPEC_KNIT_ID,
                MC_FAB_PROC_RATE_ID: data.MC_FAB_PROC_RATE_ID,
                RF_FAB_TYPE_ID: data.RF_FAB_TYPE_ID,
                LK_COL_TYPE_ID: data.LK_COL_TYPE_ID,
                LK_YD_TYPE_ID: data.LK_YD_TYPE_ID,
                LK_FEDER_TYPE_ID: data.LK_FEDER_TYPE_ID,
                LK_MAC_GG_ID: data.LK_MAC_GG_ID,
                IS_SLUB: data.IS_SLUB,
                LK_FK_DGN_TYP_ID: data.LK_FK_DGN_TYP_ID,
                FAB_PROC_NAME: data.FAB_PROC_NAME,
                IS_ACTIVE: data.IS_ACTIVE,
                PROC_RATE: data.PROC_RATE,
                RATE_MOU_ID: data.RATE_MOU_ID,
                RF_CURRENCY_ID: data.RF_CURRENCY_ID,
                MC_FAB_PROC_GRP_ID: data.MC_FAB_PROC_GRP_ID,
                MC_FIB_COMB_TMPLT_ID: data.MC_FIB_COMB_TMPLT_ID,
                FACT_PROD_RATE: data.FACT_PROD_RATE
            }, '/BudgetSheet/SaveKnittingRateData', token).then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $('#kendoGrid').data("kendoGrid").dataSource.read();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }
    }

})();

//RateChartDyeing

(function () {
    'use strict';
    angular.module('multitex.mrc').controller('RateChartDyeingController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', '$modal', RateChartDyeingController]);
    function RateChartDyeingController($q, config, MrcDataService, $stateParams, $state, $scope, $modal) {

        var vm = this;
        vm.errors = null;
        var MC_FAB_PROC_GRP_ID = 4;
        vm.Title = $state.current.Title || '';
        vm.Template = angular.copy($scope.$parent.Template);

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
                $scope.$parent.setTitle($state.current.Title);

            });
        }


        vm.ColGrp = '';
        vm.CompGrp = '';
        vm.DyMethod = '';

        $scope.TemplateList = {
            optionLabel: "--New Template--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(vm.Template);
                    }
                }
            },
            dataTextField: "FIB_COMB_TMPLT_NAME",
            dataValueField: "MC_FIB_COMB_TMPLT_ID"
        };


        vm.form = { RATE_MOU_ID: 3, RF_CURRENCY_ID: 1, MC_RATE_SPEC_DYE_ID: -1, FIB_COMB_CONF: [], template: [] };
        var copyOfForm = angular.copy(vm.form);

        vm.ColourGroupList = {
            optionLabel: "-- Colour Group--",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.$parent.ColourGrp);
                    }
                }
            },
            select: function (e) {
                if (this.dataItem(e.item).MC_COLOR_GRP_ID) {
                    vm.ColGrp = this.dataItem(e.item).COLOR_GRP_NAME_EN;
                } else {
                    vm.ColGrp = '';
                }
            },
            dataTextField: "COLOR_GRP_NAME_EN",
            dataValueField: "MC_COLOR_GRP_ID"
        };


        vm.FiberGroupList = {
            optionLabel: "--Composition Group--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return MrcDataService.getDataByUrl('/YarnSpec/FiberCompGroup?Short=N').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        })

                    }
                }
            },
            select: function (e) {
                if (this.dataItem(e.item).RF_FIB_COMP_GRP_ID) {
                    vm.CompGrp = this.dataItem(e.item).RF_FIB_COMP_GRP_NAME;
                } else {
                    vm.CompGrp = '';
                }
            },
            dataTextField: "RF_FIB_COMP_GRP_NAME",
            dataValueField: "RF_FIB_COMP_GRP_ID"
        };


        $scope.$watchGroup(['vm.form.MC_RATE_SPEC_DYE_ID', 'vm.form.RF_FIB_COMP_GRP_ID', 'vm.form.MC_COLOR_GRP_ID', 'vm.form.LK_DYE_MTHD_ID'], function (newVal, oldVal) {
            if ((newVal[0] || 0) <= 0) {
                vm.form['FAB_PROC_NAME'] = (vm.ColGrp + ' ' + vm.CompGrp + ' ' + vm.DyMethod).replace('  ', ' ');
            }
        });

        vm.currencyList = {
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.$parent.currencyList);
                    }
                }
            },
            dataTextField: "CURR_CODE",
            dataValueField: "RF_CURRENCY_ID"
        };

        vm.MOUList = {
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.$parent.MOUList)
                    }
                }
            },
            dataTextField: "MOU_CODE",
            dataValueField: "RF_MOU_ID"
        };


        vm.reset = function () {
            vm.form = angular.copy(copyOfForm);
            vm.form['MC_RATE_SPEC_DYE_ID'] = -1;
            vm.form['MC_FIB_COMB_TMPLT_ID'] = '';
            vm.form['MC_COLOR_GRP_ID'] = '';
            vm.form['FAB_PROC_NAME'] = '';
            vm.form['LK_DYE_MTHD_ID'] = '';
            vm.ColGrp = '';
            vm.CompGrp = '';
            vm.DyMethod = '';

            $scope.DyeingChartForm.$setPristine();
        };



        vm.DyeMethodList = {
            optionLabel: "---N/A---",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.$parent.DyeingMethod)
                    }
                }
            },
            select: function (e) {
                if (this.dataItem(e.item).LK_DYE_MTHD_ID) {
                    vm.DyMethod = this.dataItem(e.item).DYE_MTHD_NAME;
                } else {
                    vm.DyMethod = '';
                }
            },

            dataTextField: "DYE_MTHD_NAME",
            dataValueField: "LK_DYE_MTHD_ID"
        };



        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: {
                        serverPaging: true,
                        serverSorting: true,
                        pageSize: 50,
                        url: "/api/mrc/BudgetSheet/DyeingRateData"
                    }
                }
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
            height: 400,
            scrollable: {
                virtual: true
            },
            sortable: true,
            columns: [
                { field: "FIB_COMB_TMPLT_NAME", title: "Fib. Combi. Template", type: "string", width: "100px", filterable: { multi: true } },
                { field: "COLOR_GRP_NAME_EN", title: "Colour Group", type: "string", width: "70px", filterable: { multi: true } },
                { field: "DYE_MTHD_NAME", title: "Dye Method", type: "string", width: "70px", filterable: { multi: true } },



                { field: "FAB_PROC_NAME", title: "Rate Desc.", type: "string", width: "120px" },

                {
                    title: "Rate",
                    template: function () {
                        return "{{dataItem.PROC_RATE +' '+dataItem.CURR_NAME_EN+' /'+dataItem.RATE_MOU}}";
                    },
                    width: "80px"
                },

                {
                    title: "Action",
                    template: function () {
                        return "<a  class='btn btn-xs blue' ng-click='vm.edit(dataItem)'><i class='fa fa-edit'> </i></a>";
                    },
                    width: "40px"
                }
            ]
        };


        vm.edit = function (data) {
            var dt = angular.copy(data);
            vm.form = dt;
        }

        vm.openFiberConfigModal = function (MC_FIB_COMB_TMPLT_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Merchandising/Mrc/_CombFiberConfig',
                controller: 'FiberConfigModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    FiberTypeList: function (MrcDataService) {
                        return MrcDataService.LookupListData(76);
                    },
                    TMPLT_H_ID: function () {
                        return MC_FIB_COMB_TMPLT_ID || null;
                    },
                    Template: function (MrcDataService) {
                        return MrcDataService.getDataByUrl('/YarnSpec/FibCombTemplateData?pMC_FIB_COMB_TMPLT_ID');
                    }
                }
            });

            modalInstance.result.then(function (data) {
                if (angular.isUndefined(data.MC_FIB_COMB_TMPLT_ID)) return;
                var ifTempIdExist = _.some(vm.Template, function (o) {
                    return o.MC_FIB_COMB_TMPLT_ID == parseInt(data.MC_FIB_COMB_TMPLT_ID || 0);
                })
                if (!ifTempIdExist) {
                    vm.Template.push({
                        MC_FIB_COMB_TMPLT_ID: data.MC_FIB_COMB_TMPLT_ID,
                        FIB_COMB_TMPLT_NAME: data.FIB_COMB_TMPLT_NAME
                    });

                    $('#MC_FIB_COMB_TMPLT_ID').data("kendoDropDownList").dataSource.read();

                    $('#MC_FIB_COMB_TMPLT_ID').data("kendoDropDownList").value(data.MC_FIB_COMB_TMPLT_ID);
                    vm.form.MC_FIB_COMB_TMPLT_ID = data.MC_FIB_COMB_TMPLT_ID;
                } else {

                    vm.form.MC_FIB_COMB_TMPLT_ID = data.MC_FIB_COMB_TMPLT_ID;
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };
        vm.submitData = function (dataOri, token, valid) {
            if (!valid) {
                return;
            }
            var data = angular.copy(dataOri);
            data['MC_FAB_PROC_GRP_ID'] = MC_FAB_PROC_GRP_ID;
            return MrcDataService.saveDataByUrl(
                {
                    LK_DYE_MTHD_ID: data.LK_DYE_MTHD_ID,
                    MC_COLOR_GRP_ID: data.MC_COLOR_GRP_ID,
                    FAB_PROC_NAME: data.FAB_PROC_NAME,
                    MC_FAB_PROC_GRP_ID: data.MC_FAB_PROC_GRP_ID,
                    MC_FAB_PROC_RATE_ID: data.MC_FAB_PROC_RATE_ID,
                    MC_RATE_SPEC_DYE_ID: data.MC_RATE_SPEC_DYE_ID,
                    PROC_RATE: data.PROC_RATE,
                    RATE_MOU: data.RATE_MOU,
                    RATE_MOU_ID: data.RATE_MOU_ID,
                    RF_CURRENCY_ID: data.RF_CURRENCY_ID,
                    MC_FIB_COMB_TMPLT_ID: data.MC_FIB_COMB_TMPLT_ID
                }
                , '/BudgetSheet/SaveDyeingRateData', token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $('#kendoGrid').data("kendoGrid").dataSource.read();
                        }
                        config.appToastMsg(res.data.PMSG);


                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }
    }

})();

//RateChartDFin
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('RateChartDFinController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', '$modal', RateChartDFinController]);
    function RateChartDFinController($q, config, MrcDataService, $stateParams, $state, $scope, $modal) {

        var vm = this;
        vm.errors = null;
        var dataOnInit = [];
        vm.Title = $state.current.Title || '';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getDFINRateData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
                $scope.$parent.setTitle($state.current.Title);

            });
        }

        vm.Template = angular.copy($scope.$parent.Template);

        vm.currencyList = {
            optionLabel: "---",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.$parent.currencyList);
                    }
                }
            },
            dataTextField: "CURR_CODE",
            dataValueField: "RF_CURRENCY_ID"
        };

        vm.MOUList = {
            optionLabel: "---",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.$parent.MOUList)
                    }
                }
            },
            dataTextField: "MOU_CODE",
            dataValueField: "RF_MOU_ID"
        };


        $scope.addNew = function () {

            var data = {
                MC_RATE_SPEC_DFIN_ID: -1,
                MC_FAB_PROC_RATE_ID: -1,
                FAB_PROC_NAME: '',
                PROC_RATE: '',
                RF_CURRENCY_ID: 1,
                RATE_MOU_ID: 3,
                FIB_COMB_CONF: [],
                template: []
            };

            vm.dataSource.insert(0, data);
        }

        $scope.remove = function (data) {
            vm.dataSource.remove(data);
        }

        $scope.TemplateList = {
            optionLabel: "--New Template--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(vm.Template);
                    }
                }
            },
            dataTextField: "FIB_COMB_TMPLT_NAME",
            dataValueField: "MC_FIB_COMB_TMPLT_ID"
        };



        function getDFINRateData() {
            return MrcDataService.getDataByUrl('/BudgetSheet/DFINRateData').then(function (res) {
                vm.dataSource = new kendo.data.DataSource({ data: res });
                dataOnInit = angular.copy(res);
            }, function (err) {
                console.log(err);
            });
        }

        vm.init = function (dataItem) {
            if (!dataItem.FAB_PROC_NAME) {
                dataItem.FAB_PROC_NAME = dataItem.RF_FIB_COMP_GRP_NAME;
            }
        };

        vm.gridColumns = [
        {
            title: "Fiber Combination Template",
            template: function () {
                return "<div class='input-group'><select kendo-drop-down-list options='TemplateList' id='{{dataItem.uid}}' name='MC_FIB_COMB_TMPLT_ID-{{dataItem.uid}}' ng-model='dataItem.MC_FIB_COMB_TMPLT_ID' class='form-control input-group' required></select><span class='input-group-btn'><a ng-click='vm.openFiberConfigModal(dataItem)' title='Edit' class='btn btn-xs blue'><i class='fa' ng-class='dataItem.MC_FIB_COMB_TMPLT_ID ? \"fa-pencil\":\"fa-plus\"'></i> </a></span></div>";
            },
            width: "120px"
        },

        {
            title: "Rate Description",
            template: function () {
                return "<input type='text' ng-init='vm.init(dataItem)' ng-model='dataItem.FAB_PROC_NAME'  class='form-control' ng-required=''/>";
            },
            width: "130px"
        },

        {
            title: "Rate",
            template: function () {
                return "<input type='number' min='0' ng-model='dataItem.PROC_RATE' class='form-control' ng-change='vm.rateChanged(dataItem)' ng-required='' />";
            },
            width: "30px"
        },


        {
            title: "Currency",
            template: function () {
                return "<select kendo-drop-down-list options='vm.currencyList'  ng-model='dataItem.RF_CURRENCY_ID'  name='RF_CURRENCY_ID-{{dataItem.uid}}' class='form-control' ng-required='dataItem.RF_CURRENCY_ID_REQ' ></select>";
            },
            width: "40px"
        },

        {
            title: "/MOU",
            template: function () {
                return "<select kendo-drop-down-list options='vm.MOUList' ng-model='dataItem.RATE_MOU_ID'  name='RATE_MOU_ID-{{dataItem.uid}}' class='form-control' ng-required='dataItem.RATE_MOU_ID' ></select>";
            },
            width: "40px"
        },
        {
            title: "",
            template: '</a> <a ng-click="remove(dataItem)" class="btn btn-xs red" title="Remove"><i class="fa fa-times-circle"></i></a>',
            width: "15px"
        }
        ];


        vm.rateChanged = function (dataItem) {

            if (dataItem.PROC_RATE > 0) {
                dataItem.RF_CURRENCY_ID = 1;
                dataItem.RATE_MOU_ID = 3;
                dataItem.FAB_PROC_NAME_REQ = true;
                dataItem.RF_CURRENCY_ID_REQ = true;
                dataItem.RATE_MOU_ID_REQ = true;

            } else {
                dataItem.RF_CURRENCY_ID = '';
                dataItem.RATE_MOU_ID = '';
                dataItem.FAB_PROC_NAME_REQ = false;
                dataItem.RF_CURRENCY_ID_REQ = false;
                dataItem.RATE_MOU_ID_REQ = false;

            }

        };


        vm.openFiberConfigModal = function (dataItem) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Merchandising/Mrc/_CombFiberConfig',
                controller: 'FiberConfigModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    FiberTypeList: function (MrcDataService) {
                        return MrcDataService.LookupListData(76);
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
                    return o.MC_FIB_COMB_TMPLT_ID == parseInt(data.MC_FIB_COMB_TMPLT_ID || 0);
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
                    dataItem.MC_FIB_COMB_TMPLT_ID = data.MC_FIB_COMB_TMPLT_ID;
                }
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };


        vm.submitData = function (token) {
            var dataToBeSaved = [];


            angular.forEach(vm.dataSource.data(), function (val, key) {

                console.log(val);
                if ((val.PROC_RATE && val.PROC_RATE > 0) || val.MC_RATE_SPEC_DFIN_ID > 0) {
                    dataToBeSaved.push({
                        RF_FIB_COMP_GRP_ID: val.RF_FIB_COMP_GRP_ID,
                        MC_FAB_PROC_RATE_ID: val.MC_FAB_PROC_RATE_ID,
                        MC_FIB_COMB_TMPLT_ID: val.MC_FIB_COMB_TMPLT_ID,
                        MC_RATE_SPEC_DFIN_ID: val.MC_RATE_SPEC_DFIN_ID,
                        PROC_RATE: val.PROC_RATE,
                        RATE_MOU_ID: val.RATE_MOU_ID,
                        RF_CURRENCY_ID: val.RF_CURRENCY_ID,
                        FAB_PROC_NAME: val.FAB_PROC_NAME || 'N/A'
                    });
                };
            });

            if (dataToBeSaved.length == 0) {
                return;
            }

            var xml = MrcDataService.xmlStringShort(dataToBeSaved);

            return MrcDataService.saveDataByUrl({ MC_FAB_PROC_GRP_ID: 5, XML: xml }, '/BudgetSheet/SaveDFINRateData', token).then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        getDFINRateData();
                    };

                    config.appToastMsg(res.data.PMSG);


                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        vm.cancel = function () {
            vm.dataSource = new kendo.data.DataSource({ data: dataOnInit });
        };
    }

})();


//RateChartYD

(function () {
    'use strict';
    angular.module('multitex.mrc').controller('RateChartYDController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', RateChartYDController]);
    function RateChartYDController($q, config, MrcDataService, $stateParams, $state, $scope) {

        var vm = this;
        vm.errors = null;
        var dataOnInit = [];
        vm.Title = $state.current.Title || '';
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getYDRateData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
                $scope.$parent.setTitle($state.current.Title);
            });
        }


        vm.currencyList = {
            optionLabel: "---",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.$parent.currencyList);
                    }
                }
            },
            dataTextField: "CURR_CODE",
            dataValueField: "RF_CURRENCY_ID"
        };

        vm.MOUList = {
            optionLabel: "---",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.$parent.MOUList)
                    }
                }
            },
            dataTextField: "MOU_CODE",
            dataValueField: "RF_MOU_ID"
        };

        $scope.DyeMethodList = {
            optionLabel: "--Dyeing Method--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.$parent.DyeingMethod)
                    }
                }
            },
            dataTextField: "DYE_MTHD_NAME",
            dataValueField: "LK_DYE_MTHD_ID"
        };


        $scope.add = function () {
            vm.dataSource.insert({
                MC_RATE_SPEC_YD_ID: -1,
                MC_FAB_PROC_RATE_ID: -1,
                RF_CURRENCY_ID: 1,
                RATE_MOU_ID: 3
            });
        };

        $scope.remove = function (dataItem) {
            vm.dataSource.remove(dataItem);
        }



        $scope.ColourGroupList = {
            optionLabel: "-- Col Group--",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.$parent.ColourGrp);
                    }
                }
            },
            select: function (e) {
                if (this.dataItem(e.item).MC_COLOR_GRP_ID) {
                    vm.ColGrp = this.dataItem(e.item).COLOR_GRP_NAME_EN;
                } else {
                    vm.ColGrp = '';
                }
            },
            dataTextField: "COLOR_GRP_NAME_EN",
            dataValueField: "MC_COLOR_GRP_ID"
        };

        vm.onChangeColGrp = function (dataItem) {
            dataItem.FAB_PROC_NAME = '';
            dataItem.FAB_PROC_NAME = _.filter($scope.$parent.ColourGrp, function (o) {
                return o.MC_COLOR_GRP_ID == parseInt(dataItem.MC_COLOR_GRP_ID);
            })[0].COLOR_GRP_NAME_EN;
        };

        vm.onChangeDyeMethod = function (dataItem) {

            dataItem.FAB_PROC_NAME = dataItem.FAB_PROC_NAME + ' ' + _.filter($scope.$parent.DyeingMethod, function (o) {
                return o.LK_DYE_MTHD_ID == parseInt(dataItem.LK_DYE_MTHD_ID);
            })[0].DYE_MTHD_NAME;
        };



        function getYDRateData() {
            return MrcDataService.getDataByUrl('/BudgetSheet/YarnDyeingRateData').then(function (res) {
                vm.dataSource = new kendo.data.DataSource({ data: res });
                dataOnInit = angular.copy(res);
            }, function (err) {
                console.log(err);
            });
        }

        vm.gridColumns = [
        {
            title: "Colour Grp",
            template: function () {
                return "<select kendo-drop-down-list options='ColourGroupList' ng-model='dataItem.MC_COLOR_GRP_ID' name='MC_COLOR_GRP_ID-{{dataItem.uid}}' class='form-control' ng-change='vm.onChangeColGrp(dataItem)' required></select>";
            },
            width: "80px"
        },
        {
            title: "Dye Method",
            template: function () {
                return "<select kendo-drop-down-list options='DyeMethodList' ng-model='dataItem.LK_DYE_MTHD_ID' name='LK_DYE_MTHD_ID-{{dataItem.uid}}' class='form-control' ng-change='vm.onChangeDyeMethod(dataItem)' required ng-disabled='!dataItem.MC_COLOR_GRP_ID'></select>";
            },
            width: "80px"
        },
        {
            title: "Rate Description",
            template: function () {
                return "<input type='text' ng-model='dataItem.FAB_PROC_NAME'  class='form-control' ng-required/ >";
            },
            width: "130px"
        },

        {
            title: "Rate",
            template: function () {
                return "<input type='number' ng-model='dataItem.PROC_RATE' class='form-control' required/ >";
            },
            width: "40px"
        },


        {
            title: "Currency",
            template: function () {
                return "<select kendo-drop-down-list options='vm.currencyList'  ng-model='dataItem.RF_CURRENCY_ID'  name='RF_CURRENCY_ID-{{dataItem.uid}}' class='form-control' ng-required='dataItem.RF_CURRENCY_ID_REQ' ></select>";
            },
            width: "60px"
        },

        {
            title: "/MOU",
            template: function () {
                return "<select kendo-drop-down-list options='vm.MOUList' ng-model='dataItem.RATE_MOU_ID'  name='RATE_MOU_ID-{{dataItem.uid}}' class='form-control'></select>";
            },
            width: "60px"
        },

        {
            headerTemplate: '<a title="Add New" ng-click="add()" class="btn btn-xs blue"><i class="fa fa-plus"></i></a>',
            template: function () {
                return '<a  title="Remove" ng-click="remove(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>';
            },
            width: "25px"
        }

        ];

        vm.submitData = function (token) {
            var dataToBeSaved = [];


            angular.forEach(vm.dataSource.data(), function (val, key) {
                if (val.PROC_RATE && val.PROC_RATE > 0) {
                    dataToBeSaved.push({
                        MC_COLOR_GRP_ID: val.MC_COLOR_GRP_ID,
                        MC_FAB_PROC_RATE_ID: val.MC_FAB_PROC_RATE_ID,
                        MC_RATE_SPEC_YD_ID: val.MC_RATE_SPEC_YD_ID,
                        LK_DYE_MTHD_ID: val.LK_DYE_MTHD_ID,
                        PROC_RATE: val.PROC_RATE,
                        RATE_MOU_ID: val.RATE_MOU_ID,
                        RF_CURRENCY_ID: val.RF_CURRENCY_ID,
                        FAB_PROC_NAME: val.FAB_PROC_NAME.replace('&', '#')
                    });
                };
            });

            if (dataToBeSaved.length == 0) {
                return;
            }

            var xml = MrcDataService.xmlStringShort(dataToBeSaved);

            return MrcDataService.saveDataByUrl({ MC_FAB_PROC_GRP_ID: 2, XML: xml }, '/BudgetSheet/SaveYDRateData', token).then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        getYDRateData();
                    };

                    config.appToastMsg(res.data.PMSG);


                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        vm.cancel = function () {
            vm.dataSource = new kendo.data.DataSource({ data: dataOnInit });
        };
    }

})();

//RateChartAOP

(function () {
    'use strict';
    angular.module('multitex.mrc').controller('RateChartAOPController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', RateChartAOPController]);
    function RateChartAOPController($q, config, MrcDataService, $stateParams, $state, $scope) {

        var vm = this;
        vm.errors = null;
        var dataOnInit = [];
        vm.Title = $state.current.Title || '';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getAOPRateData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
                $scope.$parent.setTitle($state.current.Title);

            });
        }


        vm.currencyList = {
            optionLabel: "---",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.$parent.currencyList);
                    }
                }
            },
            dataTextField: "CURR_CODE",
            dataValueField: "RF_CURRENCY_ID"
        };

        vm.MOUList = {
            optionLabel: "---",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success($scope.$parent.MOUList)
                    }
                }
            },
            dataTextField: "MOU_CODE",
            dataValueField: "RF_MOU_ID"
        };


        function getAOPRateData() {
            return MrcDataService.getDataByUrl('/BudgetSheet/AOPRateData').then(function (res) {

                console.log(res);

                vm.dataSource = new kendo.data.DataSource({ data: res });
                dataOnInit = angular.copy(res);
            }, function (err) {
                console.log(err);
            });
        }

        vm.init = function (dataItem) {

            if (!dataItem.FAB_PROC_NAME) {
                dataItem.FAB_PROC_NAME = dataItem.AOP_TYPE_NAME;
            }

        }

        vm.gridColumns = [

        {
            title: "AOP Type",
            field: 'AOP_TYPE_NAME',
            width: "80px",
            sortable: { multi: true, search: true }
        },

        {
            title: "Rate Description",
            template: function () {
                return "<input type='text' ng-init='vm.init(dataItem)' ng-model='dataItem.FAB_PROC_NAME'  class='form-control' ng-required='dataItem.FAB_PROC_NAME_REQ'/ >";
            },
            width: "130px"
        },

        {
            title: "Rate",
            template: function () {
                return "<input type='number' min='0' ng-model='dataItem.PROC_RATE' class='form-control' ng-change='vm.rateChanged(dataItem)'/ >";
            },
            width: "40px"
        },


        {
            title: "Currency",
            template: function () {
                return "<select kendo-drop-down-list options='vm.currencyList'  ng-model='dataItem.RF_CURRENCY_ID'  name='RF_CURRENCY_ID-{{dataItem.uid}}' class='form-control' ng-required='dataItem.RF_CURRENCY_ID_REQ' ></select>";
            },
            width: "60px"
        },

        {
            title: "/MOU",
            template: function () {
                return "<select kendo-drop-down-list options='vm.MOUList' ng-model='dataItem.RATE_MOU_ID'  name='RATE_MOU_ID-{{dataItem.uid}}' class='form-control' ng-required='dataItem.RATE_MOU_ID' ></select>";
            },
            width: "60px"
        },
        ];


        vm.rateChanged = function (dataItem) {

            if (dataItem.PROC_RATE > 0) {
                dataItem.RF_CURRENCY_ID = 1;
                dataItem.RATE_MOU_ID = 3;
                dataItem.FAB_PROC_NAME_REQ = true;
                dataItem.RF_CURRENCY_ID_REQ = true;
                dataItem.RATE_MOU_ID_REQ = true;

            } else {
                dataItem.RF_CURRENCY_ID = '';
                dataItem.RATE_MOU_ID = '';
                dataItem.FAB_PROC_NAME_REQ = false;
                dataItem.RF_CURRENCY_ID_REQ = false;
                dataItem.RATE_MOU_ID_REQ = false;

            }

        };

        vm.submitData = function (token) {
            var dataToBeSaved = [];


            angular.forEach(vm.dataSource.data(), function (val, key) {
                if (val.PROC_RATE && val.PROC_RATE > 0) {
                    dataToBeSaved.push({
                        LK_AOP_TYPE_ID: val.LK_AOP_TYPE_ID,
                        MC_FAB_PROC_RATE_ID: val.MC_FAB_PROC_RATE_ID,
                        MC_RATE_SPEC_AOP_ID: val.MC_RATE_SPEC_AOP_ID,
                        PROC_RATE: val.PROC_RATE,
                        RATE_MOU_ID: val.RATE_MOU_ID,
                        RF_CURRENCY_ID: val.RF_CURRENCY_ID,
                        FAB_PROC_NAME: val.FAB_PROC_NAME
                    });
                };
            });

            if (dataToBeSaved.length == 0) {
                return;
            }

            var xml = MrcDataService.xmlStringShort(dataToBeSaved);

            return MrcDataService.saveDataByUrl({ MC_FAB_PROC_GRP_ID: 3, XML: xml }, '/BudgetSheet/SaveAOPRateData', token).then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        getAOPRateData();
                    };

                    config.appToastMsg(res.data.PMSG);


                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        vm.cancel = function () {
            vm.dataSource = new kendo.data.DataSource({ data: dataOnInit });
        };
    }

})();
