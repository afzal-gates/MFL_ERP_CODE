(function () {
    'use strict';
    angular.module('multitex.mrc').controller('BuyerSampleMappingController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'SizeList', BuyerSampleMappingController]);
    function BuyerSampleMappingController($q, config, MrcDataService, $stateParams, $state, $scope, SizeList) {

        var vm = this;
        vm.errors = null;
        vm.SampleSourceData = [];

        vm.Title = $state.current.Title || '';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getMappedBuyerList(), getBuyerListData(), getSampleSourceData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getBuyerListData() {
            return vm.buyerList = {
                optionLabel: "-- Buyer --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.BuyerByUserList().then(function (res) {
                                e.success(_.map(_.groupBy(res, function (o) {
                                    return o.MC_BUYER_ID;
                                }), function (grouped) {
                                    return grouped[0];
                                }));
                            });


                        }
                    }
                },
                dataBound: function (e) {
                    var MC_BYR_ACC_ID = this.dataItem(e.item).MC_BYR_ACC_ID;
                    if (MC_BYR_ACC_ID) {
                        vm.form['MC_BYR_ACC_ID'] = MC_BYR_ACC_ID;
                    }
                },
                dataTextField: "BUYER_NAME_EN",
                dataValueField: "MC_BUYER_ID"
            };
        }


        vm.getSampleListByBuyer = function (MC_BUYER_ID) {
            return MrcDataService.getDataByUrl('/buyer/SampleListByBuyer/' + MC_BUYER_ID).then(function (res) {
                var dataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res.map(function (o) {
                                o.DEFA_SIZE_LST = o.DEFA_SIZE_LST ? o.DEFA_SIZE_LST.split(',') : [];
                                return o;
                            }));
                        }
                    }
                });
                $('#kendoGrid').data("kendoGrid").setDataSource(dataSource);
            }, function (err) {
                console.log(err);
            })

        }



        function getMappedBuyerList() {
            return MrcDataService.getDataByUrl('/buyer/MappedBuyerList').then(function (res) {
                vm.dataSource = new kendo.data.DataSource({
                    data: res,
                    pageSize: 15
                });
            }, function (err) {
                console.log(err);
            });
        }


        function getSavableData(data) {
            return _.reject(data, function (obj) {
                return obj.MC_BU_SMPL_CFG_ID < 0 && obj.IS_ACTIVE == 'N'
            });
        }

        vm.saveGridData = function (MC_BUYER_ID, token) {
            var data = angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
            var dataToBeSave = getSavableData(data).map(function (obj) {
                return {
                    MC_BU_SMPL_CFG_ID: obj.MC_BU_SMPL_CFG_ID,
                    RF_SMPL_TYPE_ID: obj.RF_SMPL_TYPE_ID,
                    LK_SMPL_SRC_TYPE_ID: obj.LK_SMPL_SRC_TYPE_ID,
                    SMPL_ORDER: obj.SMPL_ORDER,
                    IS_ACTIVE: obj.IS_ACTIVE,
                    DEFA_SIZE_LST: obj.DEFA_SIZE_LST.length > 0 ? obj.DEFA_SIZE_LST.join(',') : '',
                    SZ_DEFA_QTY: obj.SZ_DEFA_QTY
                }
            });

            if (dataToBeSave.length == 0) {
                return;
            } else {
                var xml = MrcDataService.xmlStringShort(dataToBeSave);


                return MrcDataService.saveDataByUrl({ XML: xml, MC_BUYER_ID: MC_BUYER_ID }, '/buyer/SaveSampleMappedData', token).then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.getSampleListByBuyer(MC_BUYER_ID);
                        getMappedBuyerList();
                    }
                    config.appToastMsg(res.data.PMSG);

                }, function (err) {
                    console.log(err);
                })
            }

        }

        vm.SizeList = {
            placeholder: "--Size List--",
            valuePrimitive: true,
            autoBind: false,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(SizeList);
                    }
                }
            },
            dataTextField: "SIZE_CODE",
            dataValueField: "MC_SIZE_ID"
        };



        vm.up = function (dataItem) {
            var grid = $('#kendoGrid').data("kendoGrid");
            var index = grid.dataSource.indexOf(dataItem);



            var newIndex = Math.max(0, index - 1);

            if (newIndex != index) {
                grid.dataSource.data()[index].set('SMPL_ORDER', dataItem.SMPL_ORDER - 1);
                grid.dataSource.data()[newIndex].set('SMPL_ORDER', dataItem.SMPL_ORDER + 1);
                grid.dataSource.remove(dataItem);
                grid.dataSource.insert(newIndex, dataItem);
            }

            return false;
        }

        vm.down = function (dataItem) {
            var grid = $('#kendoGrid').data("kendoGrid");
            var index = grid.dataSource.indexOf(dataItem);

            var newIndex = Math.min(grid.dataSource.total() - 1, index + 1);
            if (newIndex != index) {
                grid.dataSource.data()[index].set('SMPL_ORDER', dataItem.SMPL_ORDER + 1);
                grid.dataSource.data()[newIndex].set('SMPL_ORDER', dataItem.SMPL_ORDER - 1);
                grid.dataSource.remove(dataItem);
                grid.dataSource.insert(newIndex, dataItem);
            }

            return false;
        }


        vm.selectAll = function (checked) {
            var data = $('#kendoGrid').data("kendoGrid").dataSource.data().map(function (obj) {
                obj.IS_ACTIVE = checked;
                if (obj.IS_ACTIVE == 'Y') {
                    obj.LK_SMPL_SRC_TYPE_ID = obj.LK_SMPL_SRC_TYPE_ID > 0 ? obj.LK_SMPL_SRC_TYPE_ID : 367
                } else if (obj.IS_ACTIVE == 'N') {
                    obj.LK_SMPL_SRC_TYPE_ID = obj.LK_SMPL_SRC_TYPE_ID > 0 ? obj.LK_SMPL_SRC_TYPE_ID : ''
                }

                return obj;
            });
        };


        function getSampleSourceData() {
            MrcDataService.LookupListData(75).then(function (res) {
                vm.SampleSourceData = res;
            }, function (err) {
                console.log(err);
            });
        }

        vm.SampleSource = {
            optionLabel: "-- Select --",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(vm.SampleSourceData);
                    }
                }
            },
            dataTextField: "LK_DATA_NAME_EN",
            dataValueField: "LOOKUP_DATA_ID"
        };

        vm.changeActive = function (dataItem) {


            if (dataItem.IS_ACTIVE == 'Y' && !dataItem.LK_SMPL_SRC_TYPE_ID) {
                $scope.BuyerSampleForm[dataItem.uid].$setValidity('required', false);
            } else {
                $scope.BuyerSampleForm[dataItem.uid].$setValidity('required', true);
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
            //dataBound: function () {
            //    var grid = this;
            //    grid.table.kendoDraggable({
            //        filter: "tbody > tr",
            //        cursorOffset: {
            //            top: 30,
            //            left: 20
            //        },
            //        group: "gridGroup",
            //        hint: function (e) {
            //            return $('<div class="k-grid k-widget"><table><tbody><tr>' + e.html() + '</tr></tbody></table></div>');
            //        }
            //    });

            //    grid.table.kendoDropTarget({
            //        group: "gridGroup",
            //        drop: function (e) {
            //            var target = grid.dataSource.getByUid($(e.draggable.currentTarget).data("uid")),
            //                dest = $(e.target);

            //            if (dest.is("th") || dest.is("thead") || dest.is("span") || dest.parent().is("th")) {
            //                return;
            //            }

            //            //Destination is image
            //            if (dest.is("img")) {
            //                dest = grid.dataSource.getByUid(dest.parent().parent().data("uid"));
            //            } else {
            //                dest = grid.dataSource.getByUid(dest.parent().data("uid"));
            //            }

            //            if (!dest) {
            //                return;
            //            }

            //            //not on same item
            //            if (target.MC_BU_SMPL_CFG_ID !== dest.MC_BU_SMPL_CFG_ID) {
            //                //reorder the items
            //                var tmp = target.get("SMPL_ORDER");
            //                target.set("SMPL_ORDER", dest.get("SMPL_ORDER"));
            //                dest.set("SMPL_ORDER", tmp);
            //                grid.dataSource.sort({ field: "SMPL_ORDER", dir: "asc" });
            //            }
            //        }
            //    })
            //},

            columns: [

                {
                    headerTemplate: "<input type='checkbox' ng-model='vm.checked' ng-change='vm.selectAll(vm.checked)'  ng-true-value='\"Y\"' ng-false-value='\"N\"'>",
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-change='vm.changeActive(dataItem)'>";
                    },
                    width: "15px"
                },
                {
                    title: "SL#", width: "15px",
                    template: function () {
                        return "<input type='number'  ng-model='dataItem.SMPL_ORDER' class='form-control'>";
                    }
                },

                { field: "SMPL_TYPE_NAME", title: "Sample Type", type: "string", width: "70px" },
                {
                    title: "Source",
                    template: function () {
                        return "<select kendo-drop-down-list  options='vm.SampleSource' name='{{dataItem.uid}}' ng-model='dataItem.LK_SMPL_SRC_TYPE_ID' class='form-control' required></select>";
                    },
                    width: "40px"
                },
                {
                    title: "Default Size",
                    template: function () {
                        return "<select kendo-multi-select options='vm.SizeList' style='border: 1px solid #898383;' ng-model='dataItem.DEFA_SIZE_LST' class='form-control'></select>";
                    },
                    width: "100px"
                },
                {
                    title: "Qty",
                    template: function () {
                        return "<input type='number' num='0' ng-model='dataItem.SZ_DEFA_QTY' class='form-control'>";
                    },
                    width: "30px"
                },


                {
                    title: "Action",
                    template: '<a ng-click="vm.up(dataItem)" class="btn btn-xs blue"><i class="fa fa-angle-up"></i> </a> <a ng-click="vm.down(dataItem)" class="btn btn-xs blue"><i class="fa fa-angle-down"></i></a>',
                    width: "20px"
                }
            ]
        };

    }

})();