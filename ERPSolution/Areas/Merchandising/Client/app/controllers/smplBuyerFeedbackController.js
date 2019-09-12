////////// Start Header Controller
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('SmplBuyerFeedbackController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'MrcDataService', 'Dialog', SmplBuyerFeedbackController]);
    function SmplBuyerFeedbackController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, MrcDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        
        var key = 'MC_SMP_REQ_H_ID';
        vm.today = new Date();
        vm.form = {};
        
        activate();
        function activate() {
            var promise = [getSmpReqDtl(), getActionList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;                
            });
        }

        function getActionList() {
            return vm.actionOption = {
                optionLabel: "-- Select --",
                //filter: "startswith",
                autoBind: true,
                dataTextField: "EVENT_NAME",
                dataValueField: "MC_TNA_TASK_STATUS_ID",
                //select: function (e) {
                //    var item = this.dataItem(e.item);
                //    vm.formDtl.SMPL_TYPE_NAME = item.SMPL_TYPE_NAME;
                //}
            };
        };

        vm.actionDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    MrcDataService.GetAllOthers('/api/mrc/TnaTaskStatus/SelectApprovRejectStatus?pMC_TNA_TASK_ID=16&pIS_FB_FRM_BUYER=Y').then(function (res) {
                        e.success(res);
                    }, function (err) {
                        console.log(err);
                    });
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            }
        });

        vm.mainSmplReqGridOption = {
            height: 450,
            sortable: true,
            pageable: true,
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
            //dataBound: function () {
            //    this.expandRow(this.tbody.find("tr.k-master-row").first());
            //},
            columns: [
                { field: "ACTION_DATE", title: "Date", type: "date", format: "{0:d MMM,h:mm tt}", width: "150px" },
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", type: "string", width: "250px" },
                { field: "STYLE_NO", title: "Style#" },
                { field: "SMPL_TYPE_NAME", title: "Sample" }
            ]
        };

        function getSmpReqDtl() {

            return MrcDataService.getDataByFullUrl('/api/mrc/SampleReq/GetSmpNotifDtl4ByrFeedback/' + $stateParams.pMC_SMP_REQ_D_ID).then(function (res) {
                var data = [];
                vm.SmpProgNotif = res;

                vm.mainSmpReqDataSource = new kendo.data.DataSource({
                    data: res,
                    schema: {
                        model: {
                            fields: {
                                ACTION_DATE: { type: "date" }
                            }
                        }
                    },
                    pageSize: 6,
                    serverPaging: true,
                    serverSorting: true,
                    sort: [{ field: "ACTION_DATE", dir: "desc" }]
                });

                //vm.smpReqDataSource.read();
            });

        };

        vm.detailSmplReqGridOption = function (hdrDataItem) {
            //console.log(hdrDataItem);
            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            return MrcDataService.getDataByFullUrl("/api/mrc/SampleReq/GetSmpNotifDtl4ByrFeedback?pMC_SMP_REQ_H_ID=" + hdrDataItem.MC_SMP_REQ_H_ID).then(function (res) {
                                var data = _.map(res, function (ob) {
                                    ob['ACTION_USER_TYPE'] = hdrDataItem.ACTION_USER_TYPE;
                                    return ob;
                                });

                                e.success(data);

                            }, function (err) {
                                console.log(err);
                            });
                        }
                    },
                    serverPaging: true,
                    serverSorting: true,
                    serverFiltering: true,
                    //pageSize: 5,
                    filter: { field: "MC_SMP_REQ_H_ID", operator: "eq", value: hdrDataItem.MC_SMP_REQ_H_ID }
                },
                scrollable: false,
                sortable: true,
                //pageable: true,
                columns: [
                    { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", type: "string", width: "150px" },
                    { field: "STYLE_NO", title: "Style#" },
                    { field: "SMPL_TYPE_NAME", title: "Sample" }                    
                ]
            };
        };

      
        //vm.detailData = [];
        vm.detailColorGridOption = function (dtlDataItem) {            
            console.log(dtlDataItem);
            
            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            return MrcDataService.getDataByFullUrl("/api/mrc/SampleReq/SampReqDtl1ListByDID?pMC_SMP_REQ_D_ID=" + dtlDataItem.MC_SMP_REQ_D_ID).then(function (res) {
                                var uniques = [];
                                var data = angular.copy(res);

                                //console.log(data);
                                
                                angular.forEach(_.uniqBy(res, function (doc) { return [doc.MC_STYLE_D_ITEM_ID, doc.MC_COLOR_ID].join(); }), function (val, key) {
                                    //console.log(val);
                                    val['sizeDtl'] = _.filter(data, function (ob) {
                                        return ob.MC_STYLE_D_ITEM_ID == val.MC_STYLE_D_ITEM_ID && ob.MC_COLOR_ID == val.MC_COLOR_ID;// && ob.MC_SMP_REQ_D1_ID == val.MC_SMP_REQ_D1_ID;
                                    });
                                    uniques.push(val);
                                    //vm.detailData.push(val);
                                });

                                //console.log(uniques);
                                e.success(uniques);

                            }, function (err) {
                                console.log(err);
                            });
                        }
                    },
                    serverPaging: true,
                    serverSorting: true,
                    serverFiltering: true,
                    //pageSize: 5,
                    filter: { field: "MC_SMP_REQ_D_ID", operator: "eq", value: dtlDataItem.MC_SMP_REQ_D_ID },
                },
                scrollable: false,
                sortable: true,
                //pageable: true,
                columns: [
                    { field: "ITEM_SNAME", title: "Item", width: "150px" },
                    { field: "COLOR_NAME_EN", title: "Color", width: "100px" },
                    {
                        headerTemplate: "<i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Approve'\"></i>",
                        template: function () {
                            return "<input type='checkbox' title='All Approve?' ng-model='dataItem.IS_ALL_APPROVE' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-change='vm.changeAllEvent(dataItem, 18)' >";
                        },
                        width: "30px"
                    },
                    {
                        headerTemplate: "<i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Revise'\"></i>",
                        template: function () {
                            return "<input type='checkbox' title='All Revise?' ng-model='dataItem.IS_ALL_REVISE' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-change='vm.changeAllEvent(dataItem, 21)' >";
                        },
                        width: "30px"
                    },
                    {
                        headerTemplate: "<i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Droped'\"></i>",
                        template: function () {
                            return "<input type='checkbox' title='All Droped?' ng-model='dataItem.IS_ALL_DROPED' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-change='vm.changeAllEvent(dataItem, 22)' >";
                        },
                        width: "30px"
                    },
                    {
                        title: "Size Wise Status",                        
                        template: function () {
                            return '<table><tr>' +
                                '<td ng-repeat="i in dataItem.sizeDtl">' +
                                    '<span class="col-md-2" style="padding-left: 0px; padding-right: 0px;">' +
                                        '{{i.SIZE_CODE}}' +
                                    '</span>' +
                                    '<span class="col-md-9" style="padding-left: 0px; padding-right: 0px;">' +
                                        '<select kendo-drop-down-list k-options="vm.actionOption" k-data-source="vm.actionDataSource" ng-model="dataItem.sizeDtl[$index].MC_TNA_TASK_STATUS_ID" class="form-control" required></select>' +
                                    '</span>' +                                    
                                '</td></tr></table>';
                        }
                    },
                    {
                        title: "Comments",
                        template: function () {
                            return "<input type='text' ng-model='dataItem.COMMENTS' class='form-control' />";
                        },
                        width: "200px"
                    },
                ]
            };
        };

        vm.changeAllEvent = function (dataItem, actionID) {
            //console.log(dataItem);


            if (actionID == 18) {
                dataItem.IS_ALL_REVISE = 'N';
                dataItem.IS_ALL_DROPED = 'N';
            }
            else if (actionID == 21) {
                dataItem.IS_ALL_APPROVE = 'N';
                dataItem.IS_ALL_DROPED = 'N';
            }
            else if (actionID == 22) {
                dataItem.IS_ALL_REVISE = 'N';
                dataItem.IS_ALL_APPROVE = 'N';
            }

            angular.forEach(dataItem.sizeDtl, function (val, key) {
                val['MC_TNA_TASK_STATUS_ID'] = actionID;
            });
        }
        //<input type="checkbox" ng-model="dataItem.sizeDtl[$index].MC_TNA_TASK_STATUS_ID" ng-true-value="\'Y\'" ng-false-value="\'N\'" />
   
       

        vm.submitByrFeedbk = function (grid) {
            //console.log(grid.dataSource.data());
            //var grid = $('#smpBookingGrid').data("kendoGrid");
            //$.each(grid.tbody.find('tr'), function () {
            //    if ($stateParams.pMC_STYLE_H_EXT_ID == 0) return;
            //    var model = grid.dataItem(this);
            //    if (model.MC_STYLE_H_EXT_ID == $stateParams.pMC_STYLE_H_EXT_ID) {
            //        grid.expandRow("[data-uid='" + model.uid + "']");
            //    }
            //});

            var data = grid.dataSource.data();
            var dataList = [];

            angular.forEach(data, function (val, key) {
                if (val['ChildGrid'] != undefined) {
                    //console.log(val['ChildGrid'].dataSource.data());
                    angular.forEach(val['ChildGrid'].dataSource.data(), function (val1, key1) {
                        //console.log(val1);
                        angular.forEach(val1['sizeDtl'], function (val2, key2) {
                            val2['COMMENTS'] = val1.COMMENTS;
                            dataList.push(val2);
                        });
                    });
                }
                
            });

            //console.log('=== dataList =====');
            //console.log(dataList);
            //console.log('=== data =====');
            //console.log(data);

            var submitData = { DTL_XML: "", MC_SMP_REQ_D_ID: $stateParams.pMC_SMP_REQ_D_ID };
            submitData.DTL_XML = MrcDataService.xmlStringShort(dataList.map(function (ob) {
                return {
                    MC_SMP_FEEDBK_ID: ob.MC_SMP_FEEDBK_ID, MC_SMP_REQ_D1_ID: ob.MC_SMP_REQ_D1_ID, MC_SMP_REQ_D_ID: ob.MC_SMP_REQ_D_ID,
                    MC_TNA_TASK_STATUS_ID: ob.MC_TNA_TASK_STATUS_ID, COMMENTS: ob.COMMENTS
                };
            }));

            console.log(submitData);
            //return;

            Dialog.confirm('Do you want to submit?', 'Are you sure?', ['Yes', 'No'])
                .then(function () {
                    
                    return MrcDataService.saveDataByUrl(submitData, '/SampleReq/BatchSaveByrFeedback')
                    .then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                vm.mainSmpReqDataSource.read();
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

    }

})();
////////// End Header Controller
