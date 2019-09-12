/// <reference path="../../../Views/Mrc/_OfficeList.cshtml" />
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('labdipSubmissionController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'MrcDataService', '$modal', '$window', 'CompositionList', 'ColGrpList', 'DyeingMethod', '$timeout', 'Dialog', labdipSubmissionController]);
    function labdipSubmissionController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, MrcDataService, $modal, $window, CompositionList, ColGrpList, DyeingMethod, $timeout, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.hideFeedBackBtn = false;
        vm.today = new Date();
        vm.form = { IS_MCR: 'N' };
        vm.obGrid = [];
        vm.selectedRow = {};

        var vStatusLD_REQ_NO = localStorage.getItem("LsStatusLD_REQ_NO") || null;
        var vStatusSTYLE_NO = localStorage.getItem("LsStatusSTYLE_NO") || null;
        var vLD_REQ_TO = localStorage.getItem("LsLD_REQ_TO");
        var vLD_REQ_BY = localStorage.getItem("LsLD_REQ_BY");
        var vMC_BYR_ACC_ID = localStorage.getItem("LsMC_BYR_ACC_ID");


        $scope.msgPosition = { top: '50%', left: ($window.innerWidth / 2) - 200, pinned: true }

        activate();
        function activate() {
            var promise = [getBuyerAccListData(), getRfActionListData(), editShowLabSub({ MC_STYLE_H_ID: vStatusSTYLE_NO, MC_LD_REQ_H_ID: vStatusLD_REQ_NO, LD_REQ_TO: vLD_REQ_TO, LD_REQ_BY: vLD_REQ_BY })];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        $scope.SUBMIT_DTopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.SUBMIT_DT_LNopened = true;
        };
        $scope.COMMENTS_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.COMMENTS_DT_LNopened = true;
        };

        function editShowLabSub(dataItem) {

            if (!dataItem.MC_STYLE_H_ID && !dataItem.MC_LD_REQ_H_ID) {
                return;
            }

            $('#btnSave').attr('disabled', true);
            $('#btnSubmitInHouse').attr('disabled', true);

            vm.MC_LD_REQ_H_ID = dataItem.MC_LD_REQ_H_ID;

            MrcDataService.GetAllOthers('/api/mrc/Labdip/LabdipColorStyleWise/' + dataItem.MC_STYLE_H_ID + '/' + dataItem.MC_LD_REQ_H_ID).then(function (res) {
                console.log(res);


                vm.hideFeedBackBtn = _.every(_.map(res, 'MC_TNA_TASK_STATUS_ID'), function (val) {
                    return val == 10 || val == 4 || val == 5 || val == 8 || val == 11;
                });



                vm.hideSubmitInHouse = _.every(_.map(res, 'MC_TNA_TASK_STATUS_ID'), function (val) {
                    return val == 10 || val == 5;
                });


                var dataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            angular.forEach(res, function (val, key) {
                                val['SUBMIT_DT'] = $filter('date')(val['SUBMIT_DT'], vm.dtFormat);
                                val['COMMENTS_DT'] = $filter('date')(val['COMMENTS_DT'], vm.dtFormat);
                                val['STYLE_NO'] = dataItem.STYLE_NO;
                                val['MC_LD_REQ_H_ID'] = dataItem.MC_LD_REQ_H_ID;


                            });
                            e.success(res);
                        }
                    },
                    pageSize: 10,
                });


                if (vm.form.CREATED_BY == dataItem.LD_REQ_TO) {
                    console.log('Lab Tech');
                    $('#divLabSubTech').show();
                    $('#labSubGridTech').data("kendoGrid").setDataSource(dataSource);
                }
                if (vm.form.CREATED_BY == dataItem.LD_REQ_BY) {
                    $('#divLabSubMerch').show();
                    $('#labSubGridMerch').data("kendoGrid").setDataSource(dataSource);
                }

            });
        };


        vm.printLapDipReport = function (dataOri) {

            var data = {
                MC_LD_REQ_H_ID: dataOri.MC_LD_REQ_H_ID
            };

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-2005' }, data);

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


        vm.onChangeFibComp = function (dataItem) {
            if (dataItem.RF_FIB_COMP_ID == -1) {
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
                            return CompositionList;
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

                        CompositionList.push(result);



                        $('#FibComp-' + dataItem.uid).kendoDropDownList({
                            autoBind: false,
                            dataSource: {
                                transport: {
                                    read: function (e) {
                                        e.success([{
                                            FIB_COMP_NAME: '--New Composition--',
                                            RF_FIB_COMP_ID: -1
                                        }].concat(CompositionList));
                                    }
                                }
                            },
                            dataTextField: "FIB_COMP_NAME",
                            dataValueField: "RF_FIB_COMP_ID"
                        });





                        $timeout(function () {
                            $('#FibComp-' + dataItem.uid).data("kendoDropDownList").value(result.RF_FIB_COMP_ID);
                        }, 150);

                        $timeout(function () {
                            dataItem.RF_FIB_COMP_ID = result.RF_FIB_COMP_ID;
                        }, 100);

                    }

                }, function () {
                    console.log('Modal dismissed at: ' + new Date());
                });


            }
        };

        $scope.FabricCompList = {
            optionLabel: "--Composition--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success([{
                            FIB_COMP_NAME: '--New Composition--',
                            RF_FIB_COMP_ID: -1
                        }].concat(CompositionList));
                    }
                }
            },
            dataTextField: "FIB_COMP_NAME",
            dataValueField: "RF_FIB_COMP_ID"
        };

        $scope.ColourGroupList = {
            optionLabel: "-- Col Grp--",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(ColGrpList);
                    }
                }
            },
            dataTextField: "COLOR_GRP_NAME_EN",
            dataValueField: "MC_COLOR_GRP_ID"
        };

        $scope.DyeingMethod = {
            optionLabel: "-- Dye Method--",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(DyeingMethod);
                    }
                }
            },
            dataTextField: "DYE_MTHD_NAME",
            dataValueField: "LK_DYE_MTHD_ID"
        };

        function getBuyerAccListData() {
            return vm.buyerAccList = {
                optionLabel: "-- Select Buyer Account--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        };
        function getRfActionListData() {
            return vm.statusList = {
                optionLabel: "-- Select TNA Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/TnaTaskStatus/SelectApprovRejectStatus?pMC_TNA_TASK_ID=7&pIS_FB_FRM_BUYER=Y').then(function (res) {
                                res[0].disabled = true;
                                $scope.data = res;
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                template: kendo.template($("#template").html()),
                dataBound: function (e) {
                    $(".tbd").parent().click(false);
                },
                dataTextField: "EVENT_NAME",
                dataValueField: "MC_TNA_TASK_STATUS_ID"
            };
        };
        function getApprovedList() {
            return MrcDataService.LookupListData(40).then(function (res) {
                vm.ApprovedList = res;
                //console.log(res);
                vm.form.LK_ORD_TYPE_ID = res[0].LOOKUP_DATA_ID;
            }, function (err) {
                console.log(err);
            });
        }

        vm.showLabdip = function () {
            return MrcDataService.GetAllOthers('/api/mrc/Labdip/LabdipSubListByBuyerAcc/' + vm.form.MC_BYR_ACC_ID + '/null').then(function (res) {
                var dataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            angular.forEach(res, function (val, key) {
                                val['LD_REQ_DT'] = $filter('date')(val['LD_REQ_DT'], vm.dtFormat);
                                val['TARGET_DT'] = $filter('date')(val['TARGET_DT'], vm.dtFormat);
                            });
                            e.success(res);
                        }
                    },
                    pageSize: 50,
                });
                $('#labdipGrid').data("kendoGrid").setDataSource(dataSource);
            });
        };



        vm.editShowLabSub = function (dataItem) {

            if (!dataItem.MC_STYLE_H_ID && !dataItem.MC_LD_REQ_H_ID) {
                return;
            }

            $('#btnSave').attr('disabled', true);
            $('#btnSubmitInHouse').attr('disabled', true);

            vm.MC_LD_REQ_H_ID = dataItem.MC_LD_REQ_H_ID;

            MrcDataService.GetAllOthers('/api/mrc/Labdip/LabdipColorStyleWise/' + dataItem.MC_STYLE_H_ID + '/' + dataItem.MC_LD_REQ_H_ID).then(function (res) {

                vm.hideFeedBackBtn = _.every(_.map(res, 'MC_TNA_TASK_STATUS_ID'), function (val) {
                    return val == 10 || val == 4 || val == 5 || val == 8 || val == 11;
                });



                vm.hideSubmitInHouse = _.every(_.map(res, 'MC_TNA_TASK_STATUS_ID'), function (val) {
                    return val == 10 || val == 5;
                });


                var dataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            angular.forEach(res, function (val, key) {
                                val['SUBMIT_DT'] = $filter('date')(val['SUBMIT_DT'], vm.dtFormat);
                                val['COMMENTS_DT'] = $filter('date')(val['COMMENTS_DT'], vm.dtFormat);
                                val['STYLE_NO'] = dataItem.STYLE_NO;
                                val['MC_LD_REQ_H_ID'] = dataItem.MC_LD_REQ_H_ID;


                            });
                            e.success(res);
                        }
                    },
                    pageSize: 10,
                });


                if (vm.form.CREATED_BY == dataItem.LD_REQ_TO) {
                    console.log('Lab Tech');
                    $('#divLabSubTech').show();
                    $('#labSubGridTech').data("kendoGrid").setDataSource(dataSource);
                }
                if (vm.form.CREATED_BY == dataItem.LD_REQ_BY) {
                    $('#divLabSubMerch').show();
                    $('#labSubGridMerch').data("kendoGrid").setDataSource(dataSource);
                }

            });

        };

        vm.ShowLabSubHistory = function (dataItem) {
            return MrcDataService.GetAllOthers('/api/mrc/Labdip/LabdipColorStyleWiseHistory/' + dataItem.STYLE_NO + '/' + dataItem.MC_LD_REQ_H_ID).then(function (res) {
                var dataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            angular.forEach(res, function (val, key) {
                                val['SUBMIT_DT'] = $filter('date')(val['SUBMIT_DT'], vm.dtFormat);
                                val['COMMENTS_DT'] = $filter('date')(val['COMMENTS_DT'], vm.dtFormat);
                            });
                            e.success(res);
                        }
                    },
                    pageSize: 20,
                });
                $('#divLabSubStatus').show();
                $('#labStatusGrid').data("kendoGrid").setDataSource(dataSource);
            });
        };

        var LabdipDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    e.success([]);
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
            pageSize: 5
        });

        vm.gridOptionsLabdip = {
            autoBind: true,
            scrollable: true,
            navigatable: true,
            dataSource: LabdipDataSource,
            filterable: true,
            selectable: false,
            sortable: true,
            pageSize: 5,
            pageable: true,
            refresh: true,
            columns: [
                { field: "STYLE_NO", title: "Style No.", type: "string", width: "130px" },
                { field: "ORDER_NO", title: "Order No.", type: "string", width: "130px" },
                { field: "LD_REQ_NO", title: "Program No.", type: "string", width: "130px" },
                { field: "LD_REQ_DT", title: "Req. Date", type: "date", width: "70px", format: "{0:dd-MMM-yyyy}" },
                { field: "TARGET_DT", title: "Target Date", type: "date", width: "70px", format: "{0:dd-MMM-yyyy}" },
                {
                    title: "Action",
                    template: function () {
                        return "<a ng-click='vm.editShowLabSub(dataItem)' class='btn btn-xs blue'>Show</a>";

                        //return "<a ng-click='vm.editShowLabSub(dataItem)' class='btn btn-xs blue'>Show</a>&nbsp;&nbsp;&nbsp;<a ng-click='vm.ShowLabSubHistory(dataItem)' class='btn btn-xs blue'>History</a>";
                    },
                    width: "80px"
                }
            ]
        };

        vm.changeEventT1 = function (dataItem) {
            if ((dataItem.LD_REF1 != '' && dataItem.LD_REF2 != '' && dataItem.LD_REF3 != '') || (dataItem.IS_REPEAT === 'Y' && dataItem.LD_REF1 != '')) {
                dataItem.IS_ACTIVE = "Y";
                $('#btnSave').attr('disabled', false);
            }
            else {
                dataItem.IS_ACTIVE = "N";
                $('#btnSave').attr('disabled', true);
            }
        }
        vm.changeEventT2 = function (dataItem) {
            if (dataItem.LD_REF1 != '' && dataItem.LD_REF2 != '' && dataItem.LD_REF3 != '') {
                dataItem.IS_ACTIVE = "Y";
                $('#btnSave').attr('disabled', false);
            }
            else {
                dataItem.IS_ACTIVE = "N";
                $('#btnSave').attr('disabled', true);
            }
        }
        vm.changeEventT3 = function (dataItem) {
            if (dataItem.LD_REF1 != '' && dataItem.LD_REF2 != '' && dataItem.LD_REF3 != '') {
                dataItem.IS_ACTIVE = "Y";
                $('#btnSave').attr('disabled', false);
            }
            else {
                dataItem.IS_ACTIVE = "N";
                $('#btnSave').attr('disabled', true);
            }
        }



        vm.openLDOptionModal = function (dataItem) {
                
                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: 'LDOptionModal.html',
                    controller: function ($scope, $modalInstance) {

                        //$scope.ld_ref_list = dataItem.ld_ref_list;
                        $scope.ld_ref_list = dataItem.ld_ref_list.map(function (o) {
                            return {
                                MC_LD_SUBMIT_D_ID: o.MC_LD_SUBMIT_D_ID,
                                OPTION_NO: o.OPTION_NO,
                                LD_REF: o.LD_REF
                            }
                        });

                        $scope.incrementChar = function (idx, op) {
                            if (op) {
                                return op;
                            } else {
                                return String.fromCharCode('A'.charCodeAt() + idx);
                            }
                        }

                        $scope.add = function () {
                            $scope.ld_ref_list.push({});
                        },
                        $scope.remove = function (idx) {
                            $scope.ld_ref_list.splice(idx,1);
                        };

                        $scope.save = function (data,valid) {
                            if (!valid) {
                                return;
                            }
                            dataItem.ld_ref_list = data;
                            $modalInstance.close();
                        }

                        $scope.cancel = function (data) {
                            $modalInstance.dismiss();
                        }

                    },
                    size: 'md',
                    windowClass: 'large-Modal'
                });

                modalInstance.result.then(function (item) {

                }, function () {
                    console.log('Modal dismissed at: ' + new Date());
                });

           
        };




        vm.onRepeatChanged = function (dataItem) {
            if (dataItem.IS_REPEAT == 'Y') {

                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: 'RepeatModal.html',
                    controller: function ($scope, $modalInstance) {
                        $scope.item = {
                            LK_DYE_MTHD_ID: '',
                            MC_COLOR_GRP_ID: '',
                            APRV_LD_REF: ''
                        };
                        $scope.APRV_LD_REF = dataItem.APRV_LD_REF;
                        $scope.save = function (APRV_LD_REF) {
                            $modalInstance.close($scope.item);
                        };

                        $scope.cancel = function (data) {
                            $modalInstance.close($scope.item);

                        }

                        $scope.styleSearchAuto = function (StyleNo) {
                            return MrcDataService.getDataByUrl('/StyleH/StyleNoLabdipRefAuto/' + StyleNo + '/' + parseInt(dataItem.MC_COLOR_ID)).then(function (res) {
                                return res;
                            }, function (err) {
                                console.log(err);
                            });
                        }

                        $scope.onSelectItem = function (item) {
                            $scope.item = item;
                        }



                    },
                    size: 'md',
                    windowClass: 'large-Modal'
                });

                modalInstance.result.then(function (item) {
                    if (item.APRV_LD_REF) {
                        dataItem['APRV_LD_REF'] = item.APRV_LD_REF;
                        dataItem['LD_REF1'] = item.APRV_LD_REF;
                        dataItem['IS_LD_REF1'] = 'Y';
                        dataItem['IS_ACTIVE'] = 'Y';
                        dataItem['LK_DYE_MTHD_ID'] = item.LK_DYE_MTHD_ID || '';
                        dataItem['MC_COLOR_GRP_ID'] = item.MC_COLOR_GRP_ID || '';
                        dataItem['OLD_MC_STYLE_H_EXT_ID'] = item.MC_STYLE_H_EXT_ID || '';

                    } else {
                        dataItem['IS_REPEAT'] = 'N';
                        dataItem['APRV_LD_REF'] = '';
                        dataItem['IS_LD_REF1'] = 'N';
                        dataItem['LD_REF1'] = '';
                        dataItem['IS_ACTIVE'] = 'N';
                        dataItem['LK_DYE_MTHD_ID'] = '';
                        dataItem['MC_COLOR_GRP_ID'] = '';
                        dataItem['OLD_MC_STYLE_H_EXT_ID'] = '';
                    }

                }, function () {
                    console.log('Modal dismissed at: ' + new Date());
                });

            } else {

            }
        };


        vm.onRepeatCheckBoxChange = function (dataItem) {
            if (dataItem.IS_REPEAT === 'N') {
                Dialog.confirm('Unchecking...', 'Are you sure?', ['Yes', 'No'])
                    .then(function () {
                        dataItem['APRV_LD_REF'] = '';
                        dataItem['IS_LD_REF1'] = 'N';
                        dataItem['LD_REF1'] = '';
                        dataItem['LK_DYE_MTHD_ID'] = '';
                        dataItem['MC_COLOR_GRP_ID'] = '';
                        dataItem['OLD_MC_STYLE_H_EXT_ID'] = '';
                        dataItem['IS_ACTIVE'] = 'N';
                        dataItem.IS_REPEAT = 'N';
                        return;
                    });
                dataItem.IS_REPEAT = 'Y';
            }
        };

        vm.gridOptionsLabSubTech = {
            autoBind: true,
            height: '500px',
            scrollable: true,
            dataSource: LabdipSubDataSource,
            filterable: true,
            selectable: false,
            sortable: true,
            //pageSize: 10,
            //pageable: true,
            refresh: true,
            columns: [
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "150px" },
                { field: "FABRIC_DESC", title: "Fabric", type: "string", width: "250px" },
                {
                    title: "Revi#",
                    template: function () {
                        return "{{dataItem.REVISION_NO==0?'':dataItem.REVISION_NO}}";
                    },
                    width: "70px"
                },

                {
                    title: "Composition",
                    template: function () {
                        return "<select kendo-drop-down-list id='FibComp-{{dataItem.uid}}'  options='FabricCompList' ng-model='dataItem.RF_FIB_COMP_ID' class='form-control' ng-change='vm.onChangeFibComp(dataItem)'></select>";
                    },
                    width: "250px"
                },
                {
                    title: "Col Grp",
                    template: function () {
                        return "<select kendo-drop-down-list   options='ColourGroupList' ng-model='dataItem.MC_COLOR_GRP_ID' class='form-control'></select>";
                    },
                    width: "150px"
                },
                {
                    title: "DyeMthd",
                    template: function () {
                        return "<select kendo-drop-down-list  options='DyeingMethod' ng-model='dataItem.LK_DYE_MTHD_ID' class='form-control'></select>";
                    },
                    width: "130px"
                },
                {
                    title: "Repeat?",
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.IS_REPEAT' ng-change='vm.onRepeatCheckBoxChange(dataItem)' ng-true-value='\"Y\"' ng-false-value='\"N\"'  style='width:50%;'><a  title='Search Previous Labdip' ng-click='vm.onRepeatChanged(dataItem)'  class='btn btn-xs blue'><i class='fa fa-search'></i> </a>";
                    },
                    width: "70px"
                },
                {
                    title: "Labdip Ref No.",
                    template: function () {
                        return "<span ng-repeat='itm in dataItem.ld_ref_list' ng-if='itm.LD_REF'>{{itm.OPTION_NO}}:<span class=\"label label-success\" style='padding-left:5px;'><b><span style='font-size:14;'>{{itm.LD_REF}}</span></b></span> &nbsp </span>  <a  title='Add LD Reference' ng-click='vm.openLDOptionModal(dataItem)'  class='btn btn-xs blue'><i class='fa fa-plus'></i> </a>";
                    },
                    width: "400px"

                    //columns: [
                    //{
                    //    title: "Option-1",
                    //    template: function () {
                    //        return "<input type='text' ng-disabled='dataItem.MC_TNA_TASK_STATUS_ID==10||(dataItem.MC_TNA_TASK_STATUS_ID==7&& dataItem.REVISION_NO==0)?true:false' ng-change='vm.changeEventT1(dataItem)' ng-model='dataItem.LD_REF1' style='width:90px;color:Black;'/>&nbsp;&nbsp;<input ng-disabled='true' type='checkbox' ng-model='dataItem.IS_LD_REF1' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                    //    },
                    //    width: "135px"
                    //},
                    //{
                    //    title: "Option-2",
                    //    template: function () {
                    //        return "<input type='text' ng-disabled='dataItem.MC_TNA_TASK_STATUS_ID==10||(dataItem.MC_TNA_TASK_STATUS_ID==7&& dataItem.REVISION_NO==0)?true:false' ng-change='vm.changeEventT2(dataItem)' ng-model='dataItem.LD_REF2' style='width:90px;color:Black;'/>&nbsp;&nbsp;<input ng-disabled='true' type='checkbox' ng-model='dataItem.IS_LD_REF2' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                    //    },
                    //    width: "135px"
                    //},
                     //{
                     //    title: "Option-3",
                     //    template: function () {
                     //        return "<input type='text' ng-disabled='dataItem.MC_TNA_TASK_STATUS_ID==10||(dataItem.MC_TNA_TASK_STATUS_ID==7&& dataItem.REVISION_NO==0)?true:false' ng-change='vm.changeEventT3(dataItem)' ng-model='dataItem.LD_REF3' style='width:90px;color:Black;'/>&nbsp;&nbsp;<input ng-disabled='true' type='checkbox' ng-model='dataItem.IS_LD_REF3' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                     //    },
                     //    width: "135px"
                     //}]
                },
                 {
                     title: "In House Submit Date",
                     template: function () {
                         return "<input type='text' ng-disabled='true'  ng-model='dataItem.SUBMIT_DT' format= '{0:yyyy-MM-dd HH:mm}', placeholder='mm/dd/yyyy' style='width:80px;color:Black;' name='SUBMIT_DT' readonly />";
                     },
                     width: "100px"
                 },
                 {
                     title: "Select",
                     template: function () {
                         return "<input type='checkbox' ng-disabled='true' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                     },
                     width: "70px"
                 }
            ]
        };

        var LabdipSubDataSource = new kendo.data.DataSource({
            schema: {
                model: {
                    uid: "uid"
                }
            },
            batch: true,
            transport: {
                read: function (e) {
                    e.success([]);
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
            pageSize: 10
        });

        vm.changeEvent1 = function (dataItem) {
            dataItem.IS_LD_REF2 = "N";
            dataItem.IS_LD_REF3 = "N";
            if (dataItem.IS_LD_REF1 == "N") {
                dataItem.MC_TNA_TASK_STATUS_ID = 1;
            }
            if (dataItem.IS_LD_REF1 == "Y") {
                dataItem.MC_TNA_TASK_STATUS_ID = 10;
            }
            //var data = $('#labSubGridMerch').data("kendoGrid").dataSource.pushUpdate([{ uid: dataItem.uid, IS_LD_REF1: "Y" }]);
        }
        vm.changeEvent2 = function (dataItem) {
            dataItem.IS_LD_REF1 = "N";
            dataItem.IS_LD_REF3 = "N";
            if (dataItem.IS_LD_REF2 == "N") {
                dataItem.MC_TNA_TASK_STATUS_ID = 1;
            }
            else {
                dataItem.MC_TNA_TASK_STATUS_ID = 10;
            }
        }
        vm.changeEvent3 = function (dataItem) {
            dataItem.IS_LD_REF1 = "N";
            dataItem.IS_LD_REF2 = "N";
            if (dataItem.IS_LD_REF3 == "N") {
                dataItem.MC_TNA_TASK_STATUS_ID = 1;
            }
            else {
                dataItem.MC_TNA_TASK_STATUS_ID = 10;
            }
        }

        vm.onSelectLdRef = function (list, idx,dataItem) {
            for (var i = 0; i < list.length; i++) {
                if (i == idx) {
                    list[i].IS_APROVED = list[i].IS_APROVED == 'N' ? 'Y' : (list[i].IS_APROVED == 'Y' ? 'N' : 'Y');
                    if (list[i].IS_APROVED == 'Y') {
                        dataItem.MC_TNA_TASK_STATUS_ID = 10;
                    } else if (list[i].IS_APROVED == 'N') {
                        dataItem.MC_TNA_TASK_STATUS_ID = 1;
                    }
                   
                } else {
                    list[i].IS_APROVED = 'N';
                }
                
            }

        };

        vm.gridOptionsLabSubMerch = {
            autoBind: true,
            height: '600px',
            scrollable: true,
            navigatable: true,
            dataSource: LabdipSubDataSource,
            filterable: true,
            selectable: false,
            sortable: true,
            //pageSize: 10,
            //pageable: true,
            refresh: true,
            columns: [
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "80px" },
                { field: "FABRIC_DESC", title: "Fabric", type: "string", width: "250px" },
                {
                    title: "Revi#",
                    template: function () {
                        return "{{dataItem.REVISION_NO==0?'':dataItem.REVISION_NO}}";
                    },
                    width: "70px"
                },
                {
                    title: "Composition",
                    template: function () {
                        return "<select kendo-drop-down-list id='FibComp-{{dataItem.uid}}'  options='FabricCompList' ng-model='dataItem.RF_FIB_COMP_ID' class='form-control' readonly></select>";
                    },
                    width: "250px"
                },
                {
                    title: "Col Grp",
                    template: function () {
                        return "<select kendo-drop-down-list   options='ColourGroupList' ng-model='dataItem.MC_COLOR_GRP_ID' class='form-control' readonly></select>";
                    },
                    width: "150px"
                },
                {
                    title: "DyeMthd",
                    template: function () {
                        return "<select kendo-drop-down-list  options='DyeingMethod' ng-model='dataItem.LK_DYE_MTHD_ID' class='form-control' readonly></select>";
                    },
                    width: "130px"
                },
                {
                    title: "Repeat?",
                    template: function () {
                        return "<input type='checkbox'  ng-model='dataItem.IS_REPEAT'  ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='true'>";
                    },
                    width: "70px"
                },
                {

                    title: "Labdip Ref No.",
                    template: function () {
                        return "<span ng-repeat='(key,itm) in dataItem.ld_ref_list' ng-if='itm.LD_REF'>{{itm.OPTION_NO}}:<span class=\"label\" ng-click='vm.onSelectLdRef(dataItem.ld_ref_list,key,dataItem)' ng-class='{ \"label label-success\" : itm.IS_APROVED==\"Y\",\"label label-default\" : itm.IS_APROVED==\"N\" }' style='cursor:pointer;'><b><span style='font-size:14;'>{{itm.LD_REF}}</span></b></span> &nbsp </span>";
                    },
                    width: "400px"
                },

                //{
                //    title: "Labdip Ref No.",
                //    columns: [
                //    {
                //        title: "Option-1",
                //        template: function () {
                //            return "<input type='text' readonly ng-model='dataItem.LD_REF1' style='width:90px;color:Black;'/>&nbsp;&nbsp;<input type='checkbox' ng-disabled='dataItem.IS_REPEAT==\"Y\"' ng-change='vm.changeEvent1(dataItem)' ng-model='dataItem.IS_LD_REF1' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                //        },
                //        width: "135px"
                //    },
                //    {
                //        title: "Option-2",
                //        template: function () {
                //            return "<input type='text' readonly ng-model='dataItem.LD_REF2' style='width:90px;color:Black;'/>&nbsp;&nbsp;<input type='checkbox' ng-disabled='dataItem.IS_REPEAT==\"Y\"' ng-change='vm.changeEvent2(dataItem)' ng-model='dataItem.IS_LD_REF2' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                //        },
                //        width: "135px"
                //    },
                //     {
                //         title: "Option-3",
                //         template: function () {
                //             return "<input type='text' readonly ng-model='dataItem.LD_REF3' style='width:90px;color:Black;'/>&nbsp;&nbsp;<input type='checkbox' ng-disabled='dataItem.IS_REPEAT==\"Y\"' ng-change='vm.changeEvent3(dataItem)' ng-model='dataItem.IS_LD_REF3' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                //         },
                //         width: "135px"
                //     }]
                //},
                 {
                     title: "In House Submit Date",
                     template: function () {
                         return "<input type='text' readonly ng-model='dataItem.SUBMIT_DT' format= '{0:yyyy-MM-dd HH:mm}', placeholder='mm/dd/yyyy' style='width:80px;color:Black;' name='SUBMIT_DT' />";
                     },
                     width: "100px"
                 },
                 {
                     title: "Select",
                     template: function () {
                         return "<input type='checkbox' ng-disabled='true' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                     },
                     width: "70px"
                 },
                 {
                     title: "Status",
                     template: function () {
                         return "<select kendo-drop-down-list ng-disabled='dataItem.MC_TNA_TASK_STATUS_ID==10?true:false' options='vm.statusList' ng-model='dataItem.MC_TNA_TASK_STATUS_ID' style='width:195px;'></select>";
                     },
                     width: "200px"
                 },
                 {
                     title: "Comments Date",
                     template: function () {
                         return "<input type='text'  ng-model='dataItem.COMMENTS_DT' format= '{0:yyyy-MM-dd HH:mm}', placeholder='mm/dd/yyyy' style='width:80px;color:Black;' name='COMMENTS_DT' />";
                     },
                     width: "100px"
                 },
                {
                    title: "Comments",
                    template: function () {
                        return "<input type='text' ng-model='dataItem.COMMENTS' style='width:330px;color:Black;' />";
                    },
                    width: "350px"
                }
            ]
        };

        vm.saveData = function (token, CREATED_BY) {

            var DataForSave = [];
            var data = $('#labSubGridTech').data("kendoGrid").dataSource.data();

            angular.forEach(data, function (val, key) {
                // && !_.every(val.ld_ref_list, function (o) { return o.LD_REF==='' })

                if ( val.MC_COLOR_GRP_ID && val.LK_DYE_MTHD_ID && val.ld_ref_list && val.ld_ref_list.length > 0) {
                    val['CREATED_BY'] = CREATED_BY;
                    val['XML'] = config.xmlStringShort(val.ld_ref_list)
                    val['IS_ACTIVE'] = 'Y';
                    DataForSave.push(val);
                }
            });

            if (DataForSave.length == 0) {
                return;
            } else {

                return MrcDataService.submitLabdipData(DataForSave, token, '/api/mrc/LabdipSubmit/saveLabdipData').then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    vm.form.MC_LD_SUBMIT_ID = res.data.V_MC_LD_SUBMIT_ID;
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        config.appToastMsg(res.data.PMSG);
                        vm.editShowLabSub(DataForSave[0]);
                        $('#btnSubmitInHouse').attr('disabled', false);
                    }

                }, function (err) {
                    console.log(err);
                })
            }

        };

        vm.submitData = function (token, CREATED_BY) {

            var DataForSave = [];
            var data = $('#labSubGridTech').data("kendoGrid").dataSource.data();

            angular.forEach(data, function (val, key) {
                if (val.MC_COLOR_GRP_ID && val.LK_DYE_MTHD_ID && val.ld_ref_list && val.ld_ref_list.length > 0) {
                    val['CREATED_BY'] = CREATED_BY;
                    val['IS_ACTIVE'] = 'Y';
                    val['XML'] = config.xmlStringShort(val.ld_ref_list)
                    DataForSave.push(val);
                }
            });


            if (DataForSave.length == 0) {
                return;
            } else {
                return MrcDataService.submitLabdipData(DataForSave, token, '/api/mrc/LabdipSubmit/submitLabdipData').then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    vm.form.MC_LD_SUBMIT_ID = res.data.V_MC_LD_SUBMIT_ID;
                    if (vm.form.MC_LD_SUBMIT_ID) {
                        $('#btnSave').hide();
                        $('#btnUpdate').show();
                    }

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.editShowLabSub(DataForSave[0]);
                        config.appToastMsg(res.data.PMSG);
                    }

                }, function (err) {
                    console.log(err);
                })
            }

        };

        vm.UpdateData = function (token, CREATED_BY, MC_TNA_TASK_STATUS_ID) {

            var DataForSave = [];
            var data = $('#labSubGridTech').data("kendoGrid").dataSource.data();

            angular.forEach(data, function (val, key) {
                val['CREATED_BY'] = CREATED_BY;
                if (MC_TNA_TASK_STATUS_ID == 'Resubmit') {
                    val['MC_TNA_TASK_STATUS_ID'] = 8;
                }
                if (val['APRV_LD_REF'] == '') {
                    DataForSave.push(val);
                }
            });

            if (DataForSave.length == 0) {

                return;
            } else {
                return MrcDataService.updateLabdipData(DataForSave, token, '/api/mrc/LabdipSubmit/updateLabdipData').then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        config.appToastMsg(res.data.PMSG);
                        vm.editShowLabSub(DataForSave[0]);
                        $('#btnSubmitInHouse').attr('disabled', false);
                    }

                }, function (err) {
                    console.log(err);
                })
            }
        };

        vm.FeedbackData = function (token, CREATED_BY) {

            var DataForSave = [];
            var data = $('#labSubGridMerch').data("kendoGrid").dataSource.data();
            var vcount = 0;

            angular.forEach(data, function (val, key) {

                if ( val.ld_ref_list && val.ld_ref_list.length > 0) {
                    val['CREATED_BY'] = CREATED_BY;
                    val['XML'] = config.xmlStringShort(val.ld_ref_list.map(function (o) {
                        return {
                            LD_REF: (o.LD_REF || ''),
                            MC_LD_SUBMIT_D_ID: o.MC_LD_SUBMIT_D_ID,
                            OPTION_NO: o.OPTION_NO,
                            IS_APROVED: o.IS_APROVED
                        }

                    }));

                    val['APRV_LD_REF'] = _.find(val.ld_ref_list, function (o) {
                        return o.IS_APROVED == 'Y'
                    }) ? _.find(val.ld_ref_list, function (o) {
                        return o.IS_APROVED == 'Y'
                    }).LD_REF : null;

                    val['IS_ACTIVE'] = 'Y';

                    if (val.APRV_LD_REF) {
                        DataForSave.push(val);
                    }
                }
            });

            if (DataForSave.length == 0) {
                $scope.kNotification.show("Please select status", "error");
                return;
            } else {
                return MrcDataService.updateLabdipData(DataForSave, token, '/api/mrc/LabdipSubmit/updateLabdipData').then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        config.appToastMsg(res.data.PMSG);
                        vm.hideFeedBackBtn = true;
                        vm.editShowLabSub(DataForSave[0]);
                    }
                });
            }
        };

        var LabdipStatusDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    e.success([]);
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
            pageSize: 10
        });

        vm.gridOptionsLabStatus = {
            autoBind: true,
            height: '250px',
            scrollable: true,
            navigatable: true,
            dataSource: LabdipStatusDataSource,
            filterable: true,
            selectable: false,
            sortable: true,
            //pageSize: 10,
            //pageable: true,
            refresh: true,
            columns: [
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "100px" },
                { field: "FABRIC_DESC", title: "Fabric", type: "string", width: "250px" },
                { field: "SUBMIT_DT", title: "Submit Date", type: "string", width: "100px" },
                { field: "COMMENTS_DT", title: "Comments Date", type: "string", width: "120px" },
                { field: "COMMENTS", title: "Comments", type: "string", width: "250px" },
                { field: "APRV_LD_REF", title: "Approv Ref#", type: "string", width: "100px" },
                { field: "TASK_STATUS_NAME", title: "Status", type: "string", width: "250px" },
            ]
        };

        fneditShowLabSub(vStatusLD_REQ_NO, vStatusSTYLE_NO);
        function fneditShowLabSub(vStatusLD_REQ_NO, vStatusSTYLE_NO) {
            return MrcDataService.GetAllOthers('/api/mrc/Labdip/LabdipColorStyleWise/' + vStatusSTYLE_NO + '/' + vStatusLD_REQ_NO).then(function (res) {
                var dataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            angular.forEach(res, function (val, key) {
                                val['SUBMIT_DT'] = $filter('date')(val['SUBMIT_DT'], vm.dtFormat);
                                val['COMMENTS_DT'] = $filter('date')(val['COMMENTS_DT'], vm.dtFormat);

                                //if (val['MC_TNA_TASK_STATUS_ID'] != 6) {
                                //    if (val['MC_TNA_TASK_STATUS_ID'] == 2 || val['MC_TNA_TASK_STATUS_ID'] == 7 || val['MC_TNA_TASK_STATUS_ID'] == 9) {
                                //        //$('#lblErrormessage').text('Please accept from Dashboard.');
                                //    }
                                //    if (val['MC_TNA_TASK_STATUS_ID'] == 4) {
                                //        //$('#lblErrormessage').text('Please Send to buyer from Dashboard.');
                                //    }
                                //    if (val['LD_REF1'] != '' && val['LD_REF2'] != '' && val['LD_REF3'] != '' && val['MC_TNA_TASK_STATUS_ID'] == 3) {
                                //        $('#btnUpdate').attr('disabled', false);
                                //        $('#btnSubmitInHouse').attr('disabled', false);
                                //        $('#btnSave').attr('disabled', true);
                                //        $('#btnSave').hide();
                                //    }
                                //    if (val['LD_REF1'] == '' && val['LD_REF2'] == '' && val['LD_REF3'] == '' && val['MC_TNA_TASK_STATUS_ID'] == 3) {
                                //        $('#btnSave').attr('disabled', false);
                                //    }
                                //    if (val['MC_TNA_TASK_STATUS_ID'] == 5) {
                                //        $('#btnFeedback').attr('disabled', false);
                                //        $('#btnUpdate').attr('disabled', true);
                                //        $('#btnSubmitInHouse').attr('disabled', true);
                                //        $('#btnSave').attr('disabled', true);
                                //    }
                                //    if (val['MC_TNA_TASK_STATUS_ID'] == 8) {
                                //        $('#btnReSubmitInHouse').attr('disabled', false);
                                //        $('#btnSave').hide();
                                //    }
                                //    if (val['MC_TNA_TASK_STATUS_ID'] == 9) {
                                //        $('#btnFeedback').attr('disabled', false);
                                //    }
                                //}

                            });
                            e.success(res);
                        }
                    },
                    pageSize: 10,
                });
                if (vm.form.CREATED_BY == vLD_REQ_TO) {
                    $('#divLabSubTech').show();
                    $('#labSubGridTech').data("kendoGrid").setDataSource(dataSource);
                }
                if (vm.form.CREATED_BY == vLD_REQ_BY) {
                    $('#divLabSubMerch').show();
                    $('#labSubGridMerch').data("kendoGrid").setDataSource(dataSource);
                }
            });
        };

        fnLabStatus(vStatusLD_REQ_NO, vStatusSTYLE_NO);
        function fnLabStatus(vStatusLD_REQ_NO, vStatusSTYLE_NO) {
            if (vStatusLD_REQ_NO != null && vStatusSTYLE_NO != null) {
                return MrcDataService.GetAllOthers('/api/mrc/Labdip/LabdipColorStyleWiseHistory/' + vStatusSTYLE_NO + '/' + vStatusLD_REQ_NO).then(function (res) {
                    var dataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                angular.forEach(res, function (val, key) {
                                    val['SUBMIT_DT'] = $filter('date')(val['SUBMIT_DT'], vm.dtFormat);
                                    val['COMMENTS_DT'] = $filter('date')(val['COMMENTS_DT'], vm.dtFormat);
                                });
                                e.success(res);
                            }
                        },
                        pageSize: 20,
                    });
                    $('#divLabSubStatus').show();
                    //$('#labStatusGrid').data("kendoGrid").setDataSource(dataSource);
                });
            }
        }

        fnshowLabdip(vStatusLD_REQ_NO, vMC_BYR_ACC_ID);
        function fnshowLabdip(vStatusLD_REQ_NO, vMC_BYR_ACC_ID) {
            return MrcDataService.GetAllOthers('/api/mrc/Labdip/LabdipSubListByBuyerAcc/' + vMC_BYR_ACC_ID + '/' + vStatusLD_REQ_NO).then(function (res) {
                var dataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            angular.forEach(res, function (val, key) {
                                val['LD_REQ_DT'] = $filter('date')(val['LD_REQ_DT'], vm.dtFormat);
                                val['TARGET_DT'] = $filter('date')(val['TARGET_DT'], vm.dtFormat);
                            });
                            e.success(res);
                        }
                    },
                    pageSize: 50,
                });
                $('#labdipGrid').data("kendoGrid").setDataSource(dataSource);
            });
        };

    }

})();