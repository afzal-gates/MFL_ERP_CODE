(function () {
    'use strict';
    angular.module('multitex.mrc').controller('YarnItemController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', '$modal', YarnItemController]);
    function YarnItemController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData, $modal) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'StyleDItemFab';
        var key = 'MC_STYLE_D_FAB_ID';
        vm.Title = $state.current.Title || '';
        vm.selectedFibCom = '';

        vm.spinType = '';
        vm.cotnType = '';
        vm.count = '';
        vm.composition = '';
        vm.mellange = '';
        vm.slub = '';

        vm.params = $stateParams;

        vm.form = formData[key] ? formData : {
            IS_ACTIVE: 'Y',
            IS_SLUB: 'N',
            IS_MELLANGE: 'N'
        };

        activate()
        vm.showSplash = true;

        function activate() {
            var promise = [getFabricCompList(), getSpinFinList(), getCotnTypeList(), getYarnCountList(),getItemCategList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        $scope.$watchGroup(['vm.form.RF_FIB_COMP_ID', 'vm.form.LK_COTN_TYPE_ID', 'vm.form.LK_SPN_PRCS_ID', 'vm.form.RF_YRN_CNT_ID', 'vm.form.IS_SLUB', 'vm.form.IS_MELLANGE', 'vm.form.INV_ITEM_ID','vm.form.FANCY_DESC'], function (newVal, oldVal) {
            if (!newVal[6]) {

                if (newVal[4] == 'Y') {
                    vm.slub = 'Slub';
                } else {
                    vm.slub = '';
                }


                if (newVal[5] == 'Y') {
                    vm.mellange = 'Mellange';
                } else {
                    vm.mellange = '';
                }
                vm.form['YR_SPEC_DESC'] = (vm.count + ' ' + vm.composition +' ' + vm.spinType + ' ' + vm.slub + ' ' + vm.mellange+''+((newVal[7]==''||newVal[7]===undefined)?'':'(' + newVal[7]+')')).replace('Cotton', vm.cotnType +' Cotton');
            } 

        });


        vm.reset = function () {
        
            
            vm.form['INV_ITEM_ID'] = null;
            vm.form['ITEM_CODE']='';
            vm.form['RF_FIB_COMP_ID'] = '';
            vm.form['RF_YRN_CNT_ID'] = '';
            vm.form['LK_SPN_PRCS_ID'] = '';
            vm.form['LK_COTN_TYPE_ID'] = '';
            vm.form['IS_SLUB'] = 'N';
            vm.form['IS_MELLANGE'] = 'N';
            vm.form['RF_YR_CAT_ID'] = '';
            vm.form['YR_SPEC_DESC'] = '';
            vm.form['YR_SPEC_DESC'] = '';
            vm.form['IS_ACTIVE'] = 'Y';

            $scope.YarnItemRegForm.$setPristine();
            
        };

        vm.edit = function (data) {
            vm.form = angular.copy(data);
        };

        function getSpinFinList() {
            return vm.SpinFinList = {
                optionLabel: "--Spin Finish--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(61).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {

                    if (this.dataItem(e.item).LOOKUP_DATA_ID) {
                        vm.spinType = this.dataItem(e.item).LK_DATA_NAME_EN;
                    } else {
                        vm.spinType = '';
                    }


                },

                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        


        function getItemCategList() {
            return vm.ItemCategList = {
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/inv/ItemCategory/Code/YR').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID"
            };
        }



        function getCotnTypeList() {
            return vm.CotnTypeList = {
                optionLabel: "--Cotton Type--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(62).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);
                    if (dataItem.LOOKUP_DATA_ID && dataItem.LOOKUP_DATA_ID != 300) {
                        vm.cotnType = dataItem.LK_DATA_NAME_EN;
                    } else {
                        vm.cotnType = '';
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }
        function getYarnCountList() {

            return vm.YarnCountList = {
                optionLabel: "--Count--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/Common/YarnCountList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });

                        }
                    }
                },
                select: function (e) {
                    if (this.dataItem(e.item).RF_YRN_CNT_ID) {
                        vm.count = this.dataItem(e.item).YR_COUNT_DESC;
                    } else {
                        vm.count = '';
                    }


                },
                dataTextField: "YR_COUNT_DESC",
                dataValueField: "RF_YRN_CNT_ID"
            };



        }
        function getYarnCategoryList() {
            return vm.YarnCategoryList = {
                optionLabel: "--Yarn Category--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/Common/YarnCategoryList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });

                        }
                    }
                },
                dataTextField: "YR_CAT_NAME",
                dataValueField: "RF_YR_CAT_ID"
            };
        }

        function getFabricCompList() {
            return vm.FabricCompList = {
                optionLabel: "--Fibre Content--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/Common/FibreCompList').then(function (res) {
                                vm.FiberCompList = res;
                                var data = [{
                                    FIB_COMP_NAME: '--New Composition--',
                                    RF_FIB_COMP_ID: -1
                                }];

                                res.forEach(function (val, key) {
                                    data.push(val);
                                })

                                e.success(data);

                            }, function (err) {
                                console.log(err);
                            });
                        }
                    },
                    requestEnd: function (e) {
                        if (vm.selectedFibCom != '') {
                            vm.form['RF_FIB_COMP_ID'] = vm.selectedFibCom;
                        }

                    }
                },
                select: function (e) {

                    if (this.dataItem(e.item).RF_FIB_COMP_ID) {
                        vm.composition = this.dataItem(e.item).FIB_COMP_NAME;
                    } else {
                        vm.composition = '';
                    }


                    if (this.dataItem(e.item).RF_FIB_COMP_ID == -1) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'FabricCompositionGeneration.html',
                            controller: function (FiberCompList, FiberTypeList, FiberCompGroup, $modalInstance, $scope, $timeout, MrcDataService) {
                                var vm = this;
                                vm.form = {};
                                vm.errors = [];
                                vm.FiberCompositionTypeList = FiberTypeList.map(function (o) {
                                    return {
                                        LOOKUP_DATA_ID: o.LOOKUP_DATA_ID,
                                        LK_DATA_NAME_EN: o.LK_DATA_NAME_EN
                                    }
                                });

                                vm.form['FIB_COMP_NAME'] = '';
                                vm.formDisabled = false;


                                vm.FeederTypeList = {
                                    optionLabel: "--Fiber Group--",
                                    filter: "contains",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                e.success(FiberCompGroup);
                                            }
                                        }
                                    },
                                    select: function (e) {
                                        var FabGroupOb = [];
                                        var FIB_TYPE_ID_LST = this.dataItem(e.item).FIB_TYPE_ID_LST;
                                        if (FIB_TYPE_ID_LST) {
                                            FIB_TYPE_ID_LST.split(',').forEach(function (val) {
                                        
                                                FabGroupOb.push({
                                                    LOOKUP_DATA_ID: parseInt(val),
                                                    LK_DATA_NAME_EN: _.filter(FiberTypeList, function (o) {
                                                        return o.LOOKUP_DATA_ID == parseInt(val)
                                                    })[0].LK_DATA_NAME_EN
                                                });
                                            });
                                        }
                                        vm.form['LK_FIB_TYPE_LST'] = FabGroupOb;
                                    },
                                    dataTextField: "RF_FIB_COMP_GRP_NAME",
                                    dataValueField: "RF_FIB_COMP_GRP_ID"
                                };



                                $scope.$watch('vm.form.LK_FIB_TYPE_LST', function (newValOri, oldVal) {
                                    if (newValOri && newValOri.length > 0) {
                                        var newVal = angular.copy(newValOri);
                                        vm.form['FIB_COMP_NAME'] = '';

                                        newVal.map(function (o) {
                                            o.PERCENT = o.PERCENT || 0;
                                            return o;
                                        });

                                        var pecentValue = _.sumBy(newVal, function (o) {
                                            return o.PERCENT;
                                        });

                                        var isFillUpData = _.every(newVal, function (o) {
                                            return o.PERCENT > 0;
                                        })


                                        if (isFillUpData) {
                                            var dataToBeCheck = [];
                                            FiberCompList.forEach(function (o) {
                                                dataToBeCheck.push(MrcDataService.parseXmlString(o.LK_FIB_TYPE_LST).map(function (ob) {
                                                    return {
                                                        LOOKUP_DATA_ID: parseInt(ob.LOOKUP_DATA_ID),
                                                        PERCENT: parseInt(ob.PERCENT)
                                                    }
                                                }));

                                            });

                                            var isAvailable = _.some(dataToBeCheck, function (o) {

                                                return angular.equals(o, newVal.map(function (ob) {
                                                    return {
                                                        LOOKUP_DATA_ID: parseInt(ob.LOOKUP_DATA_ID),
                                                        PERCENT: parseInt(ob.PERCENT)
                                                    }
                                                }));

                                            });




                                        }

                                        if (isAvailable && isFillUpData) {

                                            $timeout(function () {
                                                vm.errors = [['Duplicate Composition is not allowed. Please try another']];
                                                vm.formDisabled = true;
                                            })

                                        };

                                        if (pecentValue < 100 && isFillUpData) {
                                            vm.errors = [['Wrong Percent Value !!!. Percent Value must be 100 in total.']];
                                            vm.formDisabled = true;
                                        };


                                        if (pecentValue == 100 && isFillUpData) {

                                            vm.formDisabled = false;
                                            vm.errors = [];

                                        };

                                        if (pecentValue > 100 && isFillUpData) {
                                            vm.formDisabled = true;
                                            vm.errors = [['Wrong Percent Value !!!. Percent Value must be 100 in total.']];
                                        };


                                        newVal.forEach(function (val, key) {
                                            vm.form['FIB_COMP_NAME'] += ' ' + val.PERCENT + '% ' + val.LK_DATA_NAME_EN;
                                        });
                                        vm.form['FIB_COMP_NAME'] = vm.form['FIB_COMP_NAME'].trim();
                                    } else {
                                        vm.form['FIB_COMP_NAME'] = '';
                                    }

                                }, true);


                                vm.cancel = function (data) {
                                    $modalInstance.close(data);
                                };
                                vm.submitData = function (dataOri, token) {
                                    var data = angular.copy(dataOri);


                                    data['IS_ELA_MXD'] = (
                                        _.some(data.LK_FIB_TYPE_LST, function (o) {
                                            return o.LOOKUP_DATA_ID == 375;
                                        })
                                        ) ? 'Y' : 'N';


                                    data['LK_FIB_TYPE_LST'] = MrcDataService.xmlStringLong(
                                             data.LK_FIB_TYPE_LST.map(function (o) {
                                                 return {
                                                     LOOKUP_DATA_ID: o.LOOKUP_DATA_ID,
                                                     PERCENT: o.PERCENT
                                                 }
                                             })
                                         );

                                    return MrcDataService.saveDataByUrl(data, '/StyleDItemFab/SaveFiberComposition', token).then(function (res) {
                                        if (res.success === false) {
                                            vm.errors = res.errors;
                                        }
                                        else {
                                            res['data'] = angular.fromJson(res.jsonStr);
                                            config.appToastMsg(res.data.PMSG);

                                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                                vm.form['RF_FIB_COMP_ID'] = parseInt(res.data.V_RF_FIB_COMP_ID);
                                            }
                                        }
                                    }).catch(function (message) {
                                        exception.catcher('XHR loading Failded')(message);
                                    });

                                };


                            },
                            controllerAs: 'vm',
                            size: 'lg',
                            windowClass: 'large-Modal',
                            resolve: {
                                FiberCompList: function () {
                                    return vm.FiberCompList;
                                },
                                FiberTypeList: function (MrcDataService) {
                                    return MrcDataService.LookupListData(76);
                                },
                                FiberCompGroup: function () {
                                    return MrcDataService.getDataByUrl('/YarnSpec/FiberCompGroup');
                                }
                            }
                        });

                        modalInstance.result.then(function (result) {

                            if (result.RF_FIB_COMP_ID && result.RF_FIB_COMP_ID > 0) {
                                vm.selectedFibCom = result.RF_FIB_COMP_ID;
                                vm.composition = result.FIB_COMP_NAME;
                                $("#RF_FIB_COMP_ID").data("kendoDropDownList").dataSource.read();
                            }

                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });


                    }
                },
                dataTextField: "FIB_COMP_NAME",
                dataValueField: "RF_FIB_COMP_ID"
            };
        }

        vm.submitData = function (dataOri, token) {

            var data = angular.copy(dataOri);

            return MrcDataService.saveDataByUrl(data, '/YarnSpec/YarnItemSave', token).then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $('#kendoGrid').data("kendoGrid").dataSource.read();
                    }
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });

        }


        vm.yarnListPrint = function (data) {
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params =angular.extend( { REPORT_CODE: 'RPT-2002' },data);

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

        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return MrcDataService.getDataByUrl('/YarnSpec/YarnItemData').then(function (res) {
                          
                            vm.FiberCombDs = _.uniq(_.map(res, 'FIB_COMP_NAME'));
                            vm.YarnDescDs = _.uniq(_.map(res, 'YR_SPEC_DESC'));
                            e.success(res);
                        })
                    }
                },
                pageSize: 15
            },

            filterable: {
                extra: false, //do not show extra filters
                operators: { // redefine the string operators
                    string: {   
                        contains: "Contains",
                        startswith: "Starts With",
                        eq: "Is Equal To"
                    }
                }
            },

            filterMenuInit: function (e) {
                if (e.field === "YR_COUNT_NO") {
                    var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck")
                    filterMultiCheck.container.empty();
                    filterMultiCheck.checkSource.sort({ field: "RF_YRN_CNT_ID", dir: "asc" });
                    filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
                    filterMultiCheck.createCheckBoxes();
                }

                if (e.field === "SPN_PRCS") {
                    var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck")
                    filterMultiCheck.container.empty();
                    filterMultiCheck.checkSource.sort({ field: "LK_SPN_PRCS_ID", dir: "asc" });
                    filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
                    filterMultiCheck.createCheckBoxes();
                }
            },

            pageable: true,
            pageSize:10,
            columns: [
                 //{ field: "YR_CAT_NAME", title: "Category", type: "string", width: "50px" },
                 {
                     field: "FIB_COMP_NAME", title: "Composition", type: "string", width: "80px",
                     filterable: {
                         ui: styleFilter
                     }
                 },
                 { field: "YR_COUNT_NO", title: "Count", type: "string", width: "40px", filterable: { multi: true }},
                 { field: "SPN_PRCS", title: "Spin.", type: "string", width: "40px", filterable: { multi: true } },
                 { field: "COTN_TYPE", title: "CtnType", type: "string", width: "40px", filterable: { multi: true } },
                 {
                     field: "YR_SPEC_DESC", title: "Yarn Description", type: "string", width: "100px",
                     filterable: {
                         ui: YarnDescFilter
                     }
                 },
                 { field: "FANCY_DESC", title: "FancyDesc.", type: "string", width: "60px" },
                 { field: "IS_MELLANGE", title: "Mell.", type: "string", width: "30px", filterable: { multi: true } },
                 { field: "IS_SLUB", title: "Slub", type: "string", width: "30px", filterable: { multi: true } },
                {
                    title: "Action",
                    template: function () {
                        return "<a  class='btn btn-xs blue' ng-click='vm.edit(dataItem)'><i class='fa fa-edit'> Edit</i></a>";
                    },
                    width: "40px"
                }
            ]
        };

        function styleFilter(element) {
            element.kendoAutoComplete({
                dataSource: vm.FiberCombDs
            });
        }

        function YarnDescFilter(element) {
            element.kendoAutoComplete({
                dataSource: vm.YarnDescDs
            });
        }

    }

})();