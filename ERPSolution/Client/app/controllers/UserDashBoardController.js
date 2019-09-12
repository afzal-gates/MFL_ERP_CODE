(function () {
    'use strict';

    angular.module('multitex').controller('UserDashBoardController', ['logger', 'config', '$scope', '$q', 'DashBoardService', '$modal', '$http', 'Hub', '$rootScope', '$filter', '$state', '$stateParams', 'Dialog', '$window', 'cur_user_id', 'login_emp_id', UserDashBoardController]);

    function UserDashBoardController(logger, config, $scope, $q, DashBoardService, $modal, $http, Hub, $rootScope, $filter, $state, $stateParams, Dialog, $window, cur_user_id, login_emp_id) {

        var vm = this;
        vm.showSplash = true;
        vm.today = new Date();
        var isOpenReqCount = -1;
        vm.todayStr = moment(vm.today).format("YYYY-MMM-DD");

        vm.SmpProgNotifLength = 1;
        vm.SmpProgNotifSearch = 'N';

        vm.BfbkBudgetNotifLength = 1;
        vm.BfbProgNotifSearch = 'N';

        vm.reqPendingCountDataLen = 1;

        vm.labdipFollowupDataLen = 1;

        vm.tnaFollowupDataLen = 1;

        vm.PurchaseRequisitionListlength = 1;

        vm.isBlkFbMaximize = false;
        vm.isSmplFbMaximize = false;



        $scope.OrderTnaDataCopy = [];
        $scope.tnaActiveIndex;

        var hubUrl = "/signalr";
        var connection = $.hubConnection(hubUrl, { useDefaultPath: true });
        var hub = connection.createHubProxy("dashboard");
        var hubStart = connection.start();
        //vm.smpReqDataSource = new kendo.data.DataSource({
        //    data: []            
        //});


        $scope.LdSearchParams = '';

        activate();

        function activate() {
            var promise = [SalaryAdvReqesterNotif(), SalaryAdvApproverNotif(), OnlineLeaveReqesterNotif(), OnlineLeaveApproverNotif(), LabdipRequestProgramNotif(),
                broadcastSmpReqNotif(), broadcastBfbkBudgetNotif(), loadTaBookingPoHrdDs(), getTnADataListQuery(), getReqPendingCountData(), getPurchasePlanData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.jobCardPrevMonth = function (pIsPrevMonth) {
            
            vm.frmJobCard = { REPORT_CODE: "RPT-1000", HR_EMPLOYEE_ID: login_emp_id, IS_PREVIOUS_MONTH: pIsPrevMonth };

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", "/Hr/HrReport/PreviewReport");
            form.setAttribute("target", '_blank');
            for (var i in vm.frmJobCard) {
                if (vm.frmJobCard.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = (vm.frmJobCard[i] instanceof Date) ? vm.frmJobCard[i].toISOString() : vm.frmJobCard[i];
                    form.appendChild(input);
                }
            }
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);

        };

        var hub = new Hub('dashboard', {
            listeners: {
                'newConnection': function (id) {
                    vm.connected.push(id);
                    $rootScope.$apply();
                },
                'removeConnection': function (id) {
                    $rootScope.$apply();
                },
                'SalaryAdvApproverNotif': function () {
                    SalaryAdvApproverNotif();
                    $rootScope.$apply()
                },
                'OnlineLeaveApproverNotif': function () {
                    OnlineLeaveApproverNotif();
                    $rootScope.$apply()
                },
                'OnlineLeaveReqesterNotif': function () {
                    OnlineLeaveReqesterNotif();
                    $rootScope.$apply()
                },
                'SalaryAdvReqesterNotif': function () {
                    SalaryAdvReqesterNotif();
                    $rootScope.$apply()
                },
                'LabdipRequestProgramNotif': function () {
                    LabdipRequestProgramNotif();
                    $rootScope.$apply()
                },
                'broadcastSmpReqNotif': function () {
                    broadcastSmpReqNotif();
                    $rootScope.$apply()
                },
                'broadcastBfbkBudgetNotif': function () {
                    broadcastBfbkBudgetNotif();
                    $rootScope.$apply()
                },
                'RefreshRequisitionFollowup': function () {
                    getReqPendingCountData();
                    $rootScope.$apply()
                },
                'RefreshPurchasePlanList': function () {
                    getPurchasePlanData();
                    $rootScope.$apply()
                }
            },
            methods: [],
            errorHandler: function (error) {
                console.error(error);
            }
        });

        vm.connected = [];


        vm.reqPendingCountHOpt = {
            height: 250,
            sortable: true,
            scrollable: {
                virtual: true,
            },
            pageable: false,
            filterable: false,
            dataBound: function (e) {
                e.sender.thead.hide();
                var grid = this;
                grid.tbody.find("tr.k-master-row").each(function () {
                    var model = grid.dataItem(this);
                    if (model.RF_REQ_TYPE_ID == isOpenReqCount) {
                        grid.expandRow($(this));
                    }
                })
            },
            detailExpand: function (e) {
                e.sender.collapseRow(e.sender.tbody.find(' > tr.k-master-row').not(e.masterRow));
                isOpenReqCount = e.sender.dataItem(e.masterRow).RF_REQ_TYPE_ID;
            },
            columns: [
                { field: "REQ_TYPE_NAME", title: "Req Type", type: "string", width: "100%", template: "#=REQ_TYPE_NAME # &nbsp;&nbsp;&nbsp;<span class='label label-sm label-danger' style='font-weight:600;font-size:13px;'> #=CNT_PENDING #</span>" },
            ]
        };

        function getReqPendingCountData() {
            return DashBoardService.getDataByUrl('/api/common/GetPendingReqCountH').then(function (res) {
                vm.reqPendingCountDataLen = res.length;
                vm.reqPendingCountHDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        vm.onRefReqPendingCount = function () {
            getReqPendingCountData();
        }

        vm.reqPendingCountHDOpt = function (dataItem) {
            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            return DashBoardService.getDataByUrl('/api/common/GetPendingReqCountHD?pRF_REQ_TYPE_ID=' + dataItem.RF_REQ_TYPE_ID).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                scrollable: true,
                sortable: false,
                dataBound: function (e) {
                    e.sender.thead.hide();
                },
                columns: [
                    { field: "NXT_ACTION_NAME", title: "Action Name", type: "string", width: "100%", template: "#=NXT_ACTION_NAME # &nbsp;&nbsp;&nbsp;<span class='label label-sm label-danger' style='font-weight:600;font-size:13px;'> #=CNT_PENDING #</span>       <span class='label label-sm label-info' style='cursor:pointer;' ng-click='vm.navigatetoReqFollowup(dataItem)'>Navigate <i class='fa fa-share'></i> </span>" },
                ]
            };
        };





        ////////// Start Sample Notification
        
        vm.mainSmplReqGridOption = {
            //height: 250,
            sortable: true,
            scrollable: {
                virtual: true,
                //scrollable:true
            },
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
            //dataSource: vm.smpReqDataSource,
            //dataBound: function () {
            //    this.expandRow(this.tbody.find("tr.k-master-row").first());
            //},
            filterMenuInit: function (e) {
                if (e.field === "SMP_REQ_TYPE") {
                    var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck")
                    filterMultiCheck.container.empty();
                    filterMultiCheck.checkSource.sort({ field: "SMP_REQ_TYPE", dir: "asc" });
                    filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
                    filterMultiCheck.createCheckBoxes();
                }
            },
            columns: [
                {
                    title: "",
                    template: function () {
                        return '<ul kendo-menu k-orientation="menuOrientation" k-rebind="menuOrientation" k-on-select="onSelect(kendoEvent)">' +
                                '<li class="fa fa-share">' +
                                    '<ul style="width:150px;">' +
                                        '<li ng-show="dataItem.ACTION_USER_TYPE==\'SENDER\'&&dataItem.MC_TNA_TASK_STATUS_ID==13"><a style="color:black" ng-click="vm.sendToSample(dataItem)"><i class="fa fa-send"> Send to Sample</i></a></li>' +
                                        '<li ng-show="dataItem.ACTION_USER_TYPE==\'RECEIVER\'&&dataItem.MC_TNA_TASK_STATUS_ID==14"><a style="color:black" ng-click="vm.acceptProgram(dataItem)"> Accept</a></li>' +
                                        '<li disabled="disabled" style="padding:0px"><hr style="margin:0px;border-top: 1px solid grey;" /></li>' +
                                        //'<li><a style="color:black" ng-click="vm.printSampleBooking(dataItem)"><i class="fa fa-print"> Print</i></a></li>' +
                                        '<li><i class="fa fa-print"> Booking Print</i>' +
                                            '<ul style="width:150px;">' +
                                                '<li class="k-item k-state-default k-first" ng-repeat="itm in dataItem.fabBkingRpt"><a class="k-link" style="color:black" ng-click="vm.printSampleBooking(itm, dataItem)">{{itm.REVISION_NO_NAME}}</a></li>' +
                                            '</ul>' +
                                        '</li>' +
                                    '</ul>' +
                                '</li></ul>';
                    },
                    width: "30px"
                },
                {
                    headerTemplate: "<i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'Download to excel?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Download to excel?' ng-model='dataItem.IS_EXCEL_FORMAT' ng-true-value='\"Y\"' ng-false-value='\"N\"' >";
                    },
                    width: "30px"
                },
                //{ field: "SMP_REQ_DT", title: "Prg/Rev Date", type: "date", format: "{0:d MMM,h:mmtt}", width: "120px" },
                {
                    field: "SMP_REQ_DT",
                    title: "Prg/Rev Date",
                    template: function () {
                        return "<span>{{dataItem.SMP_REQ_DT|date: \'d MMM,h:mma\'}}<label class='label label-sm label-danger' ng-show='dataItem.REVISION_DT'>Rev-{{dataItem.REVISION_NO}}</label></span>";
                    },
                    width: "140px",
                    filterable: true

                },
                //{ field: "REVISION_DT", title: "Rev Date", type: "date", format: "{0:d MMM,h:mmtt}", width: "120px" },
                { field: "ACTION_DATE", title: "Action Date", type: "date", format: "{0:d MMM,h:mmtt}", width: "120px", filterable: false },
                {
                    field: "SMP_REQ_TYPE", title: "Type", type: "string", width: "80px",
                    filterable: { multi: true }
                    //filterable: {
                    //    ui: smpReqTypeFilter
                    //}
                },
                { field: "BYR_ACC_NAME_EN_LST", title: "Buyer A/C", type: "string", width: "150px" },
                { field: "SMP_REQ_NO", title: "Ref#", type: "string", hidden: true },
                { field: "STYLE_NO_LST", title: "Style", type: "string", width: "100px" },
                //{ field: "TASK_STATUS_NAME", title: "Status", type: "string", width: "150px" },
                {
                    field: "TASK_STATUS_NAME",
                    title: "Status",
                    template: function () {
                        return "<label style='font-size: 12px' ng-class='{\"label label-sm label-warning\":dataItem.MC_TNA_TASK_STATUS_ID>=15}'>{{dataItem.TASK_STATUS_NAME}}</label>";
                    },
                    width: "150px",
                    filterable: false
                }
            ]
        };

        function smpReqTypeFilter(element) {
            element.kendoAutoComplete({
                dataSource: vm.smpReqTypeDs
            });
        }

        vm.detailSmplReqGridOption = function (hdrDataItem) {
            //console.log(hdrDataItem);
            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            return DashBoardService.getDataByUrl("/api/mrc/SampleReq/getSmpReqNotifDtl?pMC_SMP_REQ_H_ID=" + hdrDataItem.MC_SMP_REQ_H_ID).then(function (res) {
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
                scrollable: true,
                sortable: true,
                //pageable: true,
                columns: [
                    { field: "ACTION_DATE", title: "Date", type: "date", format: "{0:d MMM,h:mmtt}", width: "100px" },
                    { field: "STYLE_NO", title: "Style#", width: "130px" },
                    { field: "SMPL_TYPE_NAME", title: "Sample", width: "100px" },
                    //{ field: "ACTION_USER_TYPE", title: "Sample", width: "200px" },
                    { field: "RQD_QTY", title: "R.Qty", width: "50px" },
                    { field: "DELV_QTY", title: "D.Qty", width: "50px" },
                    //{ field: "QTY_MOU_CODE", title: "Unit" },
                    {
                        title: "Status",
                        field: "TASK_STATUS_NAME",
                        template: function () {
                            return '<span>{{dataItem.TASK_STATUS_NAME}}</span>&nbsp;<a style="color:blue" ng-show="dataItem.IS_BYR_FEEDBK==\'Y\'" ng-click="byrFeedBackReport(dataItem)">[Rcvd Feedback]</a>';
                        },
                        width: "180px"
                    },
                    {
                        title: "Action",
                        template: function () {
                            return '<a class="btn btn-xs blue" ng-show="dataItem.MC_TNA_TASK_STATUS_ID==15&&dataItem.ACTION_USER_TYPE==\'RECEIVER\'" href="/Merchandising/Mrc/SmplFabBook?a=110/#/smplprodentry/{{dataItem.MC_SMP_REQ_H_ID}}/{{dataItem.MC_BYR_ACC_ID}}/{{dataItem.MC_STYLE_H_EXT_ID}}/{{dataItem.RF_SMPL_TYPE_ID}}/{{vm.todayStr}}/1/dtl">Prod.</a>' +
                                    '<a class="btn btn-xs blue" ng-show="dataItem.MC_TNA_TASK_STATUS_ID==16&&dataItem.ACTION_USER_TYPE==\'SENDER\'" ng-click="vm.sendToBuyer(dataItem)">Submit</a>' +
                                    '<a class="btn btn-xs blue" ng-show="dataItem.MC_TNA_TASK_STATUS_ID==17&&dataItem.ACTION_USER_TYPE==\'SENDER\'" href="/Merchandising/Mrc/SmplFabBook?a=88/#/smpByrFeedback/{{dataItem.MC_SMP_REQ_D_ID}}">Feedback</a>';
                            //'&nbsp;&nbsp;<button class="btn btn-xs blue-hoki" ng-show="dataItem.IS_BYR_FEEDBK==\'Y\'" ng-click="byrFeedBackReport(dataItem)">Detail</button>';
                        },
                        width: "80px"
                    }
                ]
            };
        };


        function broadcastSmpReqNotif() {

            vm.smpReqDataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                schema: {
                    model: {
                        fields: {
                            SMP_BK_DT: { type: "date" },
                            SMP_REQ_DT: { type: "date" },
                            ACTION_DATE: { type: "date" }
                        }
                    },
                    data: "data",
                    total: "total"
                },
                transport: {
                    read: function (e) {

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/SampleReq/BroadcastSmpReqNotif' + '?';
                        url += '&pageNumber=' + (params.page || 1) + '&pageSize=' + (params.pageSize || 10);
                        url += config.kFilterStr2QueryParam(params.filter);

                        return DashBoardService.getDataByUrl(url).then(function (res) {

                            //angular.forEach(res.data, function (val, key) {
                            //    //console.log(val);
                            //    if (val['REVISION_DT'] != null) {
                            //        val['SMP_REQ_DT'] = val['REVISION_DT'];
                            //    }
                            //});

                            if (vm.SmpProgNotifSearch == 'N' && vm.SmpProgNotifLength == 1) {
                                //alert('sss');
                                vm.SmpProgNotifLength = res.total;
                            }

                            vm.smpReqTypeDs = _.uniq(_.map(res.data, 'SMP_REQ_TYPE'));

                            e.success({
                                total: res.total,
                                data: res.data
                            });
                        }, function (err) {
                            console.error(err);
                        })

                    }
                },
                pageSize: 10,
                sort: [{ field: "SMP_REQ_DT", dir: "desc" }]
            });

        };

        vm.searchSmplProg = function (SearchTerm) {

            if (SearchTerm.length == 0) {
                vm.SmpProgNotifSearch = 'N';
                vm.smpReqDataSource.query({});
                vm.smpReqDataSource.sort({ field: "SMP_REQ_DT", dir: "desc" });

            } else if (SearchTerm.length >= 3) {
                vm.SmpProgNotifSearch = 'Y';
                var filter = [
                        { field: 'STYLE_NO_LST', operator: "contains", value: SearchTerm },
                ];
                vm.smpReqDataSource.query({ filter: filter });
                vm.smpReqDataSource.sort({ field: "SMP_REQ_DT", dir: "desc" });
            }


        };

        vm.printSampleBooking = function (itm, dataItem) {

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = { REPORT_CODE: 'RPT-2000', MC_SMP_REQ_H_ID: itm.FAB_BKING_ID, IS_EXCEL_FORMAT: dataItem.IS_EXCEL_FORMAT };

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

        vm.sendToSample = function (data, token) {

            Dialog.confirm('Do you want to submit?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     return DashBoardService.saveDataByUrl(data, '/api/mrc/SampleReq/SendToSample', token)
                     .then(function (res) {

                         vm.errors = undefined;
                         if (res.success === false) {
                             vm.errors = [];
                             vm.errors = res.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.jsonStr);

                             //if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             //    $scope.$parent.form['MC_TNA_TASK_STATUS_ID'] = 14;
                             //};

                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
        };

        vm.acceptProgram = function (data, token) {

            Dialog.confirm('Do you want to accept?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     return DashBoardService.saveDataByUrl(data, '/api/mrc/SampleReq/AcceptSample', token)
                     .then(function (res) {
                         console.log(res);

                         vm.errors = undefined;
                         if (res.success === false) {
                             vm.errors = [];
                             vm.errors = res.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.jsonStr);

                             //if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             //    $scope.$parent.form['MC_TNA_TASK_STATUS_ID'] = 15;
                             //};

                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
        };

        vm.sendToBuyer = function (data) {
            //console.log(data);
            //return;

            Dialog.confirm('Do you want to submit?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     return DashBoardService.saveDataByUrl(data, '/api/mrc/SampleReq/SampleSendToBuyer', $scope.token)
                     .then(function (res) {
                         console.log(res);

                         vm.errors = undefined;
                         if (res.success === false) {
                             vm.errors = [];
                             vm.errors = res.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.jsonStr);

                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
        }

        $scope.byrFeedBackReport = function (data) {
            console.log(data);

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'SmplByrFeedbackModal.html',
                controller: 'SmplByrFeedbackModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    smplByrFeedbackList: function (DashBoardService) {
                        return DashBoardService.getDataByUrl('/api/mrc/SampleReq/SmplByrFeedbackStatus?pMC_SMP_REQ_D_ID=' + data.MC_SMP_REQ_D_ID);
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        }
        ////////// End Sample Notification




        ////////// Start For Bulk Fabric & Budget Approval Notification       
        vm.aprvlProcess1 = function (token, statusId) {
              
            //var approveDataList = [];
            var gridData = angular.copy(_.filter(vm.blkFbReqDataSource.data(), function (ob) {

                return ob.IS_SELECT == "Y";

                //if (statusId == 33) {
                //    return ob.IS_SELECT == "Y" && ob.ACTION_USER_TYPE == "RECEIVER";
                //}
                //else if (statusId == 35) {
                //    if (ob.IS_SELECT == "Y" && ob.ACTION_USER_TYPE == "RECEIVER" && vm.blkBookingResendAlerts.length < 1 && ob.BFBK_STATUS_REMARKS.trim() == '') {
                       
                //        vm.blkBookingResendAlerts.push({
                //            type: 'danger', msg: 'Please write your remarks for resend every booking'
                //        });
                //    }

                //    return ob.IS_SELECT == "Y" && ob.ACTION_USER_TYPE == "RECEIVER";
                //}
                //else if (statusId == 36) {
                //    return ob.IS_SELECT == "Y" && ob.ACTION_USER_TYPE == "RECEIVER";
                //}
                //else if (statusId == 0) {
                //    return ob.IS_SELECT == "Y" && ob.ACTION_USER_TYPE == "SENDER";
                //}
            }));

            var approveDataList = angular.copy(gridData);
            console.log(gridData);
            //return;
                       
            angular.forEach(approveDataList, function (val, key) {

                var submitData = angular.copy(val);

                if (val['FAB_PROD_CAT_SNAME'] == 'Bulk') {
                    submitData['REPORT_CODE'] = 'RPT-2001';
                    submitData['PAGE_SIZE_NAME'] = 'Legal';
                    submitData['IS_EXPORT_TO_DISK'] = 'Y';
                    submitData['MC_BLK_REVISION_NO'] = submitData['REVISION_NO'];

                    return $http({
                        method: 'post',
                        url: '/api/mrc/MrcReport/PreviewReport',
                        data: submitData
                    }).then(function (res) {
                        config.appToastMsg(res.data.PMSG);
                        return res.data;
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                }
                else if (val['FAB_PROD_CAT_SNAME'] == 'Additional') {
                    return $http({
                        url: '/Merchandising/mrc/AddFabBkFireMail',
                        method: 'get',
                        params: { pMC_BLK_ADFB_REQ_H_ID: submitData['MC_BLK_FAB_REQ_H_ID'] }
                    }).then(function (res) {
                        config.appToastMsg(res.data.PMSG);
                        return res.data;
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                }

            });
            
        };        

        vm.blkBookingResendAlerts = [];
        vm.aprvlProcess = function (token, statusId) {
            var msgText;
            vm.blkBookingResendAlerts = [];

            //==== Start For update pending mail ==========
            DashBoardService.getDataByUrl('/api/mrc/BulkFabBk/GetMailSendPendingListBulk').then(function (res) {

                angular.forEach(res, function (val, key) {

                    var submitData = val; 

                    submitData['REPORT_CODE'] = 'RPT-2001';
                    submitData['PAGE_SIZE_NAME'] = 'Legal';
                    submitData['IS_EXPORT_TO_DISK'] = 'Y';
                    
                    submitData['PAGE_SIZE_NAME'] = 'Legal';

                    return $http({
                        method: 'post',
                        url: '/api/mrc/MrcReport/PreviewReport',
                        data: submitData
                    }).then(function (res) {
                        return res.data;
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
            });
            //==== End For update pending mail ==========



            if (statusId == 35) {
                msgText = '<b>Are you sure, resend selected request?</b>';
            }
            else if (statusId == 33) {
                msgText = '<b>Are you sure, recommend selected request?</b>';
            }
            else if (statusId == 36) {
                msgText = '<b>Are you sure, approve selected request?</b>';
            }
            else if (statusId == 0) {
                msgText = '<b>Are you sure, clear selected request from dashboard?</b>';
            }

            Dialog.confirm(msgText, 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     var gridData = angular.copy(_.filter(vm.blkFbReqDataSource.data(), function (ob) {
                         if (statusId == 33) {
                             return ob.IS_SELECT == "Y" && ob.ACTION_USER_TYPE == "RECEIVER";
                         }
                         else if (statusId == 35) {
                             if (ob.IS_SELECT == "Y" && ob.ACTION_USER_TYPE == "RECEIVER" && vm.blkBookingResendAlerts.length < 1 && ob.BFBK_STATUS_REMARKS.trim() == '') {
                                 //alert('x');
                                 vm.blkBookingResendAlerts.push({
                                     type: 'danger', msg: 'Please write your remarks for resend every booking'
                                 });
                             }

                             return ob.IS_SELECT == "Y" && ob.ACTION_USER_TYPE == "RECEIVER";
                         }
                         else if (statusId == 36) {
                             return ob.IS_SELECT == "Y" && ob.ACTION_USER_TYPE == "RECEIVER";
                         }
                         else if (statusId == 0) {
                             return ob.IS_SELECT == "Y" && ob.ACTION_USER_TYPE == "SENDER";
                         }
                     }));

                     var approveDataList = angular.copy(gridData);
                     console.log(gridData);

                     if (vm.blkBookingResendAlerts.length > 0) {
                         return;
                     }
                     else {
                         var obXML = DashBoardService.xmlStringShort(gridData.map(function (ob) {
                             return {
                                 FAB_PROD_CAT_SNAME: ob.FAB_PROD_CAT_SNAME,
                                 MC_BLK_FAB_REQ_H_ID: ob.MC_BLK_FAB_REQ_H_ID, MC_STYL_BGT_H_ID: ob.MC_STYL_BGT_H_ID, STATUS_ORDER: vm.empLevelData.STATUS_ORDER,
                                 MC_TNA_TASK_STATUS_ID: ob.MC_TNA_TASK_STATUS_ID, IS_SELECT: ob.IS_SELECT, BFBK_STATUS_REMARKS: ob.BFBK_STATUS_REMARKS
                             };
                         }));


                         console.log(obXML);
                         console.log(statusId);

                         //return;

                         $http({
                             headers: { 'Authorization': 'Bearer ' + $scope.token },
                             url: '/api/mrc/BulkFabBk/Send4BulkBudgetAprvl',
                             method: 'post',
                             params: { pXML: obXML, pLK_BFBK_STATUS_ID: statusId }
                         })
                         .then(function (res) {
                             //$scope.$parent.errors = undefined;
                             if (res.data.success === false) {
                                 //$scope.$parent.errors = [];
                                 //$scope.$parent.errors = res.data.errors;
                             }
                             else {

                                 res['data'] = angular.fromJson(res.data.jsonStr);

                                 if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                     vm.blkFbReqDataSource.read();                                     

                                     if (statusId == 36) {

                                         var rptCode = 'RPT-2001';
                                         var pageSize = 'Legal';

                                         angular.forEach(approveDataList, function (val, key) {

                                             var submitData = angular.copy(val);

                                             if (val['FAB_PROD_CAT_SNAME'] == 'Bulk') {
                                                 submitData['REPORT_CODE'] = 'RPT-2001';
                                                 submitData['PAGE_SIZE_NAME'] = 'Legal';
                                                 submitData['IS_EXPORT_TO_DISK'] = 'Y';
                                                 submitData['MC_BLK_REVISION_NO'] = submitData['REVISION_NO'];
                                                 
                                                 return $http({
                                                     method: 'post',
                                                     url: '/api/mrc/MrcReport/PreviewReport',
                                                     data: submitData
                                                 }).then(function (res) {
                                                     config.appToastMsg(res.data.PMSG);
                                                     return res.data;
                                                 }).catch(function (message) {
                                                     exception.catcher('XHR loading Failded')(message);
                                                 });
                                             }
                                             else if (val['FAB_PROD_CAT_SNAME'] == 'Additional') {
                                                 return $http({
                                                     url: '/Merchandising/mrc/AddFabBkFireMail',
                                                     method: 'get',
                                                     params: { pMC_BLK_ADFB_REQ_H_ID: submitData['MC_BLK_FAB_REQ_H_ID'] }
                                                 }).then(function (res) {
                                                     config.appToastMsg(res.data.PMSG);
                                                     return res.data;
                                                 }).catch(function (message) {
                                                     exception.catcher('XHR loading Failded')(message);
                                                 });
                                             }

                                         });
                                     }

                                 };

                                 if (res.data.PMSG.substr(0, 9) != 'MULTI-001') {
                                     config.appToastMsg(res.data.PMSG);
                                 }
                             }
                         }).catch(function (message) {
                             exception.catcher('XHR loading Failded')(message);
                         });
                     }
                 });

        };

        vm.navigateAction = function (dataItem, navigateId, pREVISION_NO) {
            if (navigateId == 2) {
                vm.printBookingRecord(dataItem, pREVISION_NO);
            }
            else if (navigateId == 7) {
                vm.printBudgetSheetReport(dataItem);
            }
            else if (navigateId == 8) {
                vm.uploadDoc(dataItem);
            }
        };

        vm.uploadDoc = function (dataItem) {
            console.log(dataItem);

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/GlobalUI/UploadOtherDocs',
                controller: 'UploadOtherDocsController',
                controllerAs: 'vm',
                size: 'lg',
                windowClass: 'app-modal-window', //'large-Modal', //'app-modal-window',
                resolve: {
                    orderData: function () {
                        return dataItem;
                    },
                    pageAccess: function () {
                        return { IS_ONLY_VIEW: 'Y' };
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }

        vm.printBookingRecord = function (dataItem, pREVISION_NO) {
            var data = {
                MC_BLK_FAB_REQ_H_ID: dataItem.MC_BLK_FAB_REQ_H_ID,
                IS_MULTI_SHIP_DT: dataItem.IS_MULTI_SHIP_DT,
                MC_BLK_REVISION_NO: pREVISION_NO
            };
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-2001' }, data);

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

        vm.printBudgetSheetReport = function (dataOri) {
            var data = {
                MC_STYL_BGT_H_ID: dataOri.MC_STYL_BGT_H_ID,
                REVISION_NO: dataOri.BGT_REVISION_NO
            };
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-2003' }, data);

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

        vm.blkFbBudgetselectAll = function (v, index) {
            var data = vm.blkFbReqDataSource.data(); //angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());

            angular.forEach(data, function (val, key) {
                val['IS_SELECT'] = v;
            });
        };
        
        vm.resizeGrid = function (grid, size, fixed, minheight, minsizeheight, maxheight, maxsizheight) {
                                    
            if (size === null || size === undefined) { size = 0.6; }
            if (minheight === null || minheight === undefined) { minheight = 600; minsizeheight = 150; }
            if (maxheight === null || maxheight === undefined) { maxheight = 800; maxsizheight = 600; }

            var windowheight = $(window).height();
            if (!fixed) {
                windowheight = windowheight * size;
            }
            else {
                windowheight = size;
            }

            if ($(window).height() < minheight) { windowheight = minsizeheight; }
            if ($(window).height() > maxheight) { windowheight = maxsizheight; }

            var gridcontent = $("#" + grid + " div.k-grid-content");
            var lockedcontent = $("#" + grid + " div.k-grid-content-locked");
            gridcontent.height(windowheight);

            if (lockedcontent !== null && lockedcontent !== undefined) { lockedcontent.height(windowheight); }
        }     

        vm.blkFbReqGridOption = {
            //min-height:  
            //height: 220,
            //editable: true,
            //selectable: "cell",
            navigatable: true,
            scrollable: {
                virtual: true,
                //scrollable:true
            },
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
            columns: [
                {
                    title: "",
                    template: function () {
                        return '<ul kendo-menu k-orientation="menuOrientation" k-rebind="menuOrientation" k-on-select="onSelect(kendoEvent)"><i class=""></i>' +
                                '<li class="fa fa-share">' +
                                    '<ul style="width:150px;">' +
                                        '<li ng-show="dataItem.FAB_PROD_CAT_SNAME==\'Bulk\' && dataItem.ACTION_USER_TYPE==\'SENDER\' && (dataItem.MC_TNA_TASK_STATUS_ID<33||dataItem.MC_TNA_TASK_STATUS_ID==35)"><a style="color:black" href="/Merchandising/Mrc/BulkFabBkEntry?a=93/#/bulkFabBkEntry/{{dataItem.MC_BLK_FAB_REQ_H_ID}}/dtl" ><i class="fa fa-edit"> Booking Edit</i></a></li>' +
                                        '<li ng-show="dataItem.FAB_PROD_CAT_SNAME==\'Additional\' && dataItem.ACTION_USER_TYPE==\'SENDER\' && dataItem.MC_TNA_TASK_STATUS_ID<88"><a style="color:black" href="/Merchandising/Mrc/AddFabBking?a=360/#/addFabBkingH/5/dtl?pMC_BLK_ADFB_REQ_H_ID={{dataItem.MC_BLK_FAB_REQ_H_ID}}" ><i class="fa fa-edit"> Booking Edit</i></a></li>' +
                                        '<li><i class="fa fa-print"> Booking Print</i>' +
                                            '<ul style="width:150px;">'+
                                                '<li class="k-item k-state-default k-first" ng-show="dataItem.FAB_PROD_CAT_SNAME==\'Bulk\'" ng-repeat="itm in dataItem.REVISION_LIST"><a class="k-link" style="color:black" ng-click="vm.navigateAction(dataItem,2,itm.REVISION_NO)">{{itm.REV_REASON}}</a></li>' +
                                                '<li class="k-item k-state-default k-first" ng-show="dataItem.FAB_PROD_CAT_SNAME==\'Additional\'" ><a class="k-link" style="color:black" href="/Merchandising/Mrc/AddFabBkingRpt?a=360/#/addFabBkingRpt?pMC_BLK_ADFB_REQ_H_ID={{dataItem.MC_BLK_FAB_REQ_H_ID}}" target="_blank">Orginal</a></li>' +
                                            '</ul>' +
                                        '</li>' +
                                        '<li ng-show="dataItem.ACTION_USER_TYPE==\'SENDER\' && dataItem.MC_TNA_TASK_STATUS_ID<33"><a style="color:black" href="/Merchandising/Mrc/BudgetSheet?a=93/#/BudgetSheet/{{dataItem.MC_BLK_FAB_REQ_H_ID}}/{{dataItem.MC_STYLE_H_ID}}/{{dataItem.MC_STYL_BGT_H_ID}}" ><i class="fa fa-edit"> Budget Edit</i></a></li>' +
                                        '<li><a style="color:black" ng-click="vm.navigateAction(dataItem,7)" ><i class="fa fa-print"> Budget Print</i></a></li>' +
                                        '<li><a style="color:black" ng-click="vm.navigateAction(dataItem,8)" ><i class="fa fa-file"> View Document</i></a></li>'
                                    '</ul>' +
                                '</li></ul>';
                    },
                    width: "30px"
                },
                {
                    headerTemplate: "", //"<input type='checkbox' ng-model='vm.isSelect' ng-click='vm.blkFbBudgetselectAll(vm.isSelect,0)'  ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='vm.empLevelData.STATUS_ORDER==1'> All",  
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.IS_SELECT' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='(dataItem.ACTION_USER_TYPE==\"SENDER\" && dataItem.MC_TNA_TASK_STATUS_ID!=36) && (dataItem.ACTION_USER_TYPE==\"SENDER\" && dataItem.MC_TNA_TASK_STATUS_ID!=90)'>";
                    },
                    width: "50px"
                },
                //{ field: "ACTION_USER_TYPE", title: "ACTION_USER_TYPE", type: "string", width: "100px" },
                {
                    field: "BLK_FAB_REQ_DT",
                    title: "Prg/Rev Date",
                    template: function () {
                        return "<span>{{dataItem.BLK_FAB_REQ_DT|date: \'d MMM,h:mma\'}}<label class='label label-sm label-danger' ng-show='dataItem.REVISION_DT'>Rev-{{dataItem.REVISION_NO}}</label></span>";
                    },
                    width: "140px",
                    filterable: true

                },
                //{ field: "REVISION_DT", title: "Rev.Date", type: "date", format: "{0:d MMM,h:mmtt}", width: "120px", filterable: false },
                //{ field: "ACTION_DATE", title: "Action Date", type: "date", format: "{0:d MMM,h:mmtt}", width: "120px", filterable: false },
                { field: "STYLE_NO", title: "Style#", type: "string", hidden: true },
                { field: "FAB_PROD_CAT_SNAME", title: "Type", type: "string", width: "100px" },
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", type: "string", width: "100px" },
                //{ field: "BLK_FAB_REQ_DT_STR", title: "Booking Date", type: "date", format: "{0:dd-MMM-yyyy}", hidden: true },
                //{ field: "BLK_FAB_REQ_DT", title: "Booking Date", type: "date", format: "{0:dd-MMM-yyyy}", hidden: true },
                { field: "WORK_STYLE_NO_LST", title: "Work Style#", type: "string", width: "100px" },
                { field: "ORDER_NO_LST", title: "Order#", type: "string", width: "100px" },
                { field: "BLK_FAB_REQ_NO", title: "Booking Ref#", type: "string", hidden: true },
                //{ field: "TASK_STATUS_NAME", title: "Status", type: "string", width: "150px" },
                {
                    field: "TASK_STATUS_NAME",
                    title: "Status",
                    template: function () {
                        return "<label style='font-size: 12px' ng-class='{\"label label-sm label-default\":dataItem.MC_TNA_TASK_STATUS_ID==32||dataItem.MC_TNA_TASK_STATUS_ID==16}' ng-show='dataItem.MC_TNA_TASK_STATUS_ID==32||dataItem.MC_TNA_TASK_STATUS_ID==16'>{{dataItem.TASK_STATUS_NAME}}</label>" +
                            "<label style='font-size: 12px' ng-class='{\"label label-sm label-warning\":dataItem.MC_TNA_TASK_STATUS_ID==33}' ng-show='dataItem.MC_TNA_TASK_STATUS_ID==33'>{{dataItem.TASK_STATUS_NAME}}</label>" +
                            "<label style='font-size: 12px' ng-class='{\"label label-sm label-info\":dataItem.MC_TNA_TASK_STATUS_ID==34||dataItem.MC_TNA_TASK_STATUS_ID==88}' ng-show='dataItem.MC_TNA_TASK_STATUS_ID==34||dataItem.MC_TNA_TASK_STATUS_ID==88'>{{dataItem.TASK_STATUS_NAME}}</label>" +
                            "<label style='font-size: 12px' ng-class='{\"label label-sm label-danger\":dataItem.MC_TNA_TASK_STATUS_ID==35||dataItem.MC_TNA_TASK_STATUS_ID==89}' ng-show='dataItem.MC_TNA_TASK_STATUS_ID==35||dataItem.MC_TNA_TASK_STATUS_ID==89'>{{dataItem.TASK_STATUS_NAME}}</label>" +
                            "<label style='font-size: 12px' ng-class='{\"label label-sm label-success\":dataItem.MC_TNA_TASK_STATUS_ID==36||dataItem.MC_TNA_TASK_STATUS_ID==90}' ng-show='dataItem.MC_TNA_TASK_STATUS_ID==36||dataItem.MC_TNA_TASK_STATUS_ID==90'>{{dataItem.TASK_STATUS_NAME}}</label>";
                    },
                    width: "180px",
                    filterable: false
                },
                { field: "ACTION_DATE", title: "Action Date", type: "date", format: "{0:d MMM,h:mmtt}", width: "120px", filterable: false },
                {
                    title: "Remarks",
                    template: function () {
                        return "<textarea class='form-control' name='BFBK_STATUS_REMARKS' ng-model='dataItem.BFBK_STATUS_REMARKS' ng-disabled='vm.empLevelData.STATUS_ORDER==1' rows='1'></textarea>";
                    },
                    width: "300px"//,
                    //hidden: true
                }                
            ]
        };

        vm.searchBulkProg = function (SearchTerm) {
            //ng-change="vm.machineListDataSource.filter({logic: 'or', filters: [{ field:'KNT_MACHINE_NO', operator :'contains', value: q },{ field:'FLOOR_DESC_EN', operator :'contains', value: q }]})"
            if (SearchTerm.length == 0) {
                vm.BfbProgNotifSearch = 'N';
                vm.blkFbReqDataSource.query({});
                vm.blkFbReqDataSource.sort({ field: "BLK_FAB_REQ_DT", dir: "desc" });

            } else if (SearchTerm.length >= 3) {
                vm.BfbProgNotifSearch = 'Y';
                var filter = [
                        { field: 'STYLE_NO', operator: "contains", value: SearchTerm }
                ];
                vm.blkFbReqDataSource.query({ filter: filter });                
                vm.blkFbReqDataSource.sort({ field: "BLK_FAB_REQ_DT", dir: "desc" });
            }

        };

        function broadcastBfbkBudgetNotif() {
            //alert('xx');

            vm.blkFbReqDataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                schema: {
                    model: {
                        fields: {
                            BLK_FAB_REQ_DT: { type: "date" },
                            ACTION_DATE: { type: "date" },
                            MC_STYLE_H_EXT_ID: { type: "number" },
                            MC_BYR_ACC_ID: { type: "number" }
                        }
                    },
                    data: "data",
                    total: "total"
                },
                transport: {
                    read: function (e) {

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/BulkFabBk/BroadcastBfbkBudgetNotif' + '?';
                        url += '&pageNumber=' + (params.page || 1) + '&pageSize=' + (params.pageSize || 10);
                        url += config.kFilterStr2QueryParam(params.filter);

                        return DashBoardService.getDataByUrl(url).then(function (res) {
                            console.log(res);

                            if (res.obAprvr.length > 0) {
                                vm.empLevelData = res.obAprvr[0];
                            }
                            else {
                                vm.empLevelData = { SC_USER_ID: 0, MC_TNA_TASK_STATUS_ID: 32, STATUS_ORDER: 1, ACTION_USER_TYPE: '' };
                            }

                            angular.forEach(res.obj.data, function (val, key) {
                                //console.log(val);
                                if (val['REVISION_DT'] != null) {
                                    val['BLK_FAB_REQ_DT'] = val['REVISION_DT'];
                                }
                            });
                            
                            if (vm.BfbProgNotifSearch == 'N' && vm.BfbkBudgetNotifLength == 1) {
                                //alert(res.obj.total);
                                vm.BfbkBudgetNotifLength = res.obj.total;
                            }

                            //vm.smpReqTypeDs = _.uniq(_.map(res.data, 'SMP_REQ_TYPE'));

                            e.success({
                                total: res.obj.total,
                                data: res.obj.data
                            });
                        }, function (err) {
                            console.error(err);
                        })

                    }
                },
                pageSize: 10,
                sort: [{ field: "BLK_FAB_REQ_DT", dir: "desc" }]
            });

            //return DashBoardService.getDataByUrl('/api/mrc/BulkFabBk/BroadcastBfbkBudgetNotif').then(function (res) {
            //    var data = [];
            //    vm.BfbkBudgetNotif = res.obList;
            //    if (res.obAprvr.length > 0) {
            //        vm.empLevelData = res.obAprvr[0];
            //    }
            //    else {
            //        vm.empLevelData = { SC_USER_ID: 0, MC_TNA_TASK_STATUS_ID: 32, STATUS_ORDER: 1, ACTION_USER_TYPE: '' };
            //    }

            //    vm.blkFbReqDataSource = new kendo.data.DataSource({
            //        data: res.obList,
            //        schema: {
            //            model: {
            //                fields: {
            //                    ACTION_DATE: { type: "date" },
            //                    BLK_FAB_REQ_DT: { type: "date" }
            //                }
            //            }
            //        },
            //        pageSize: 6,
            //        serverPaging: true,
            //        serverFiltering: true,
            //        sort: [{ field: "ACTION_DATE", dir: "desc" }]
            //    });
            //});
        }
        ////////// End For Bulk Fabric & Budget Approval Notification




        function SalaryAdvReqesterNotif() {
            return DashBoardService.SalaryAdvReqesterNotif().then(function (res) {
                var data = [];
                angular.forEach(res, function (val, key) {
                    val['LAST_UPDATE_DATE'] = moment(val.LAST_UPDATE_DATE).fromNow();
                    data.push(val)
                });
                vm.SalaryAdvReqesterNotif = data;
                return vm.SalaryAdvReqesterNotif;
            })
        }

        function SalaryAdvApproverNotif() {
            return DashBoardService.SalaryAdvApproverNotif().then(function (res) {
                console.log(res);
                var data = [];
                angular.forEach(res, function (val, key) {
                    val['CREATION_DT_FRM_NW'] = moment(val.CREATION_DATE).fromNow();
                    val['DEDU_ST_DT'] = $filter('date')(moment(val.DEDU_ST_DT)._d, config.appDateFormat);
                    data.push(val)
                });
                vm.SalaryAdvApproverNotif = data;
                return vm.SalaryAdvApproverNotif;
            })
        }

        function OnlineLeaveReqesterNotif() {
            return DashBoardService.OnlineLeaveReqesterNotif().then(function (res) {
                var data = [];
                angular.forEach(res, function (val, key) {
                    val['LAST_UPDATE_DATE'] = moment(val.CREATION_DATE).fromNow();
                    //val['CREATION_DATE'] = moment(val.CREATION_DATE).fromNow();
                    data.push(val)
                });
                vm.OnlineLeaveReqesterNotif = data;
                console.log(data);
                return vm.OnlineLeaveReqesterNotif;
            })
        }

        function OnlineLeaveApproverNotif() {
            return DashBoardService.OnlineLeaveApproverNotif().then(function (res) {
                angular.forEach(res, function (val, key) {
                    val['CREATION_DATE_FRM_NOW'] = moment(val.CREATION_DATE).fromNow();
                    val['FROM_DATE'] = $filter('date')(moment(val.FROM_DATE)._d, config.appDateFormat);
                    val['TO_DATE'] = $filter('date')(moment(val.TO_DATE)._d, config.appDateFormat);
                    val['LAST_LV_FROM_DATE'] = val.LAST_LV_FROM_DATE ? $filter('date')(moment(val.LAST_LV_FROM_DATE)._d, config.appDateFormat) : '';
                    val['LAST_LV_TO_DATE'] = val.LAST_LV_TO_DATE ? $filter('date')(moment(val.LAST_LV_TO_DATE)._d, config.appDateFormat) : '';
                });
                vm.OnlineLeaveApproverNotif = res;
            })
        }


        vm.OnLapdipSearch = function (val) {
            if (val.length == 0) {
                LabdipRequestProgramNotif('')
            } else if (val && val.length > 0) {
                LabdipRequestProgramNotif(val)
            }

        };

        function LabdipRequestProgramNotif(LdSearchParams) {

            DashBoardService.LabdipRequestProgramNotifStyleWise(LdSearchParams || $scope.LdSearchParams || '').then(function (res) {

                vm.labdipFollowupDataLen = res.length;
                vm.LabdipRequestProgramNotifStyleWise = res.map(function (obj) {
                    return {
                        CREATION_DATE: moment(obj.CREATION_DATE).format("D MMM,h:mm a"),
                        MC_STYLE_H_ID: obj.MC_STYLE_H_ID,
                        MC_BYR_ACC_ID: obj.MC_BYR_ACC_ID,
                        MC_LD_REQ_H_ID: obj.MC_LD_REQ_H_ID,
                        IS_ACT_ACTIVE: obj.IS_ACT_ACTIVE,
                        LD_REQ_NO: obj.LD_REQ_NO,
                        BYR_ACC_NAME_EN: obj.BYR_ACC_NAME_EN,
                        STYLE_NO: obj.STYLE_NO,
                        TASK_STATUS_NAME: obj.TASK_STATUS_NAME,
                        MC_TNA_TASK_STATUS_ID: obj.MC_TNA_TASK_STATUS_ID,
                        LD_REQ_TO: obj.LD_REQ_TO,
                        LD_REQ_BY: obj.LD_REQ_BY,
                        MC_BUYER_ID: obj.MC_BUYER_ID,
                        HAS_EXT: obj.HAS_EXT,
                        MC_STYLE_H_EXT_LST: obj.MC_STYLE_H_EXT_LST,
                        approved_lds: obj.approved_lds
                    };
                });
            })

        }


        vm.navigatetoReqFollowup = function (data) {
            var url = data.NAV_URL;
            url += '?pRF_ACTN_STATUS_ID=' + data.RF_ACTN_STATUS_ID;
            url += '&pRF_REQ_TYPE_ID=' + data.RF_REQ_TYPE_ID;
            var a = document.createElement('a');
            a.href = url;
            a.target = '_blank';
            document.body.appendChild(a);
            a.click();
        }


        vm.navigateLabdip = function (params) {
            var url = '/Merchandising/Mrc/LabdipInfo?a=86/#/LabdipEntry?ID=' + params.MC_LD_REQ_H_ID + '&bAcId=' + params.MC_BYR_ACC_ID + '&pMC_BUYER_ID=' + params.MC_BUYER_ID + '&pMC_STYLE_H_EXT_LST=' + params.MC_STYLE_H_EXT_LST;
            url += '&pMC_STYLE_H_ID=' + params.MC_STYLE_H_ID + '&pHAS_EXT=' + params.HAS_EXT;
            var a = document.createElement('a');
            a.href = url;
            a.target = '_blank';
            document.body.appendChild(a);
            a.click();
        }

        $scope.open = function (data) {
            console.log(data);
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'leaveHistoryModal.html',
                controller: 'LeaveHistroryModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    item: function () {
                        return data;
                    },
                    notifications: function (DashBoardService) {
                        return DashBoardService.getNotifications(data.HR_LEAVE_TRANS_ID);
                    },
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };




        $scope.openSalAdvModal = function (data) {
            console.log(data);
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'SalAdvHistoryModal.html',
                controller: 'SalAdvHistroryModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    item: function () {
                        return data;
                    },
                    notifications: function (DashBoardService) {
                        return DashBoardService.getSalAdvNotif(data.HR_SAL_ADVANCE_ID);
                    },
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };



        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
        vm.deleteDraft = function (LeaveRef, HR_LEAVE_TRANS_ID, key) {
            Dialog.confirm('You are going to delete a draft leave request ref# ' + LeaveRef, 'Are you sure?', ['Yes', 'No'])
            .then(function () {

                $http({
                    method: 'post',
                    url: '/Hr/HrLeaveTrans/saveDataForEleave',
                    data: { ob: { HR_LEAVE_TRANS_ID: HR_LEAVE_TRANS_ID }, pOption: 2013 },
                    headers: { "RequestVerificationToken": key }
                }).success(function (data, status, headers, config1) {
                    config.appToastMsg(data.MSG);
                }).
                error(function (data, status, headers, config) {
                    console.log(status);
                });
            });
        }

        vm.reApply = function (LeaveRef, HR_LEAVE_TRANS_ID, key) {
            Dialog.confirm('You are going to re-apply a leave request ref# ' + LeaveRef, 'Are you sure?', ['Yes', 'No'])
            .then(function () {

                $http({
                    method: 'post',
                    url: '/Hr/HrLeaveTrans/saveDataForEleave',
                    data: { ob: { HR_LEAVE_TRANS_ID: HR_LEAVE_TRANS_ID }, pOption: 2014 },
                    headers: { "RequestVerificationToken": key }
                }).success(function (data, status, headers, config1) {
                    config.appToastMsg(data.MSG);
                }).
                error(function (data, status, headers, config) {
                    console.log(status);
                });
            });
        }


        vm.advApprove = function (ADV_REF, data, key, ACTION, ADV_TYPE, LK_ADV_STATUS_ID) {
            Dialog.confirm('You are going to ' + ACTION + ' a ' + ADV_TYPE + '  request ref# ' + ADV_REF, 'Are you sure?', ['Yes', 'No'])
            .then(function () {
                data['CREATION_DATE'] = new Date();
                data['LAST_UPDATE_DATE'] = new Date();
                data['LK_ADV_STATUS_ID'] = LK_ADV_STATUS_ID;

                $http({
                    method: 'post',
                    url: '/hr/HrSalAdvance/submitData',
                    data: data,
                    headers: { "RequestVerificationToken": key }
                }).success(function (data, status, headers, config1) {
                    config.appToastMsg(data.MSG);
                }).
                error(function (data, status, headers, config) {
                    console.log(status);
                });
            });
        }

        vm.advApprove = function (ADV_REF, data, key, ACTION, ADV_TYPE, LK_ADV_STATUS_ID) {
            Dialog.confirm('You are going to ' + ACTION + ' a ' + ADV_TYPE + '  request ref# ' + ADV_REF, 'Are you sure?', ['Yes', 'No'])
            .then(function () {
                data['CREATION_DATE'] = new Date();
                data['LAST_UPDATE_DATE'] = new Date();
                data['LK_ADV_STATUS_ID'] = LK_ADV_STATUS_ID;

                $http({
                    method: 'post',
                    url: '/hr/HrSalAdvance/submitData',
                    data: data,
                    headers: { "RequestVerificationToken": key }
                }).success(function (data, status, headers, config1) {
                    config.appToastMsg(data.MSG);
                }).
                error(function (data, status, headers, config) {
                    console.log(status);
                });
            });
        }

        vm.recomend2 = function (applicant, data, key, status, action) {
            Dialog.confirm('You are going to ' + action + ' leave request of ' + applicant + '.', 'Are you sure?', ['Yes', 'No'])
            .then(function () {
                data['LK_LV_STATUS_ID'] = status;
                $http({
                    method: 'post',
                    url: '/Hr/HrLeaveTrans/saveDataForEleave',
                    data: { ob: data, pOption: 2005 },
                    headers: { "RequestVerificationToken": key }
                }).success(function (data, status, headers, config1) {
                    config.appToastMsg(data.MSG);
                    //$state.go('UserDashBoard');

                }).
                error(function (data, status, headers, config) {
                    console.log(status);
                });
            });
        }

        vm.clearFromDashboard = function (url, type, ref, data, key, pOption) {
            Dialog.confirm('You are going to remove a ' + type + '  request ref# ' + ref + ' from dashboard.', 'Are you sure?', ['Yes', 'No'])
            .then(function () {
                $http({
                    method: 'post',
                    url: url,
                    data: { obj: data, pOption: pOption },
                    headers: { "RequestVerificationToken": key }
                }).success(function (data, status, headers, config1) {
                    console.log(data);
                }).
                error(function (data, status, headers, config) {
                    console.log(status);
                });
            });
        }

        vm.approve4 = function (applicant, data, key, status, action) {
            Dialog.confirm('You are going to' + action + ' leave request of ' + applicant + '.', 'Are you sure?', ['Yes', 'No'])
            .then(function () {
                data['LK_LV_STATUS_ID'] = status;
                $http({
                    method: 'post',
                    url: '/Hr/HrLeaveTrans/saveDataForEleave',
                    data: { ob: data, pOption: 2006 },
                    headers: { "RequestVerificationToken": key }
                }).success(function (data, status, headers, config1) {
                    config.appToastMsg(data.MSG);
                }).
                error(function (data, status, headers, config) {
                    console.log(status);
                });
            });
        }



        $scope.openTnaUpdateModal = function (Order, Task) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'TnAUpdateModal.html',
                controller: 'TnaUpdateModalController',
                size: 'small',
                scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    Order: function () {
                        return Order;
                    },
                    Task: function () {
                        return Task;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        vm.UpdateLDReqStatus = function (pMC_LD_REQ_H_ID, pRF_ACTN_STATUS_ID, msg, MC_LD_REQ_D_ID) {

            Dialog.confirm(msg, 'Are you sure?', ['Yes', 'No'])
           .then(function () {

               return DashBoardService.UpdateLDReqStatus(pMC_LD_REQ_H_ID, pRF_ACTN_STATUS_ID, MC_LD_REQ_D_ID || null).then(function (res) {

               })

           });
        }

        $scope.openReportParamModal = function (reportCode) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'HrReportModal.html',
                controller: 'HrReportParamModalController',
                size: 'small',
                scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    reportCode: function () {
                        return reportCode;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        $scope.openCapacityBookingRpt = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'PlanningReportModal.html',
                controller: function ($scope, $modalInstance, DashBoardService) {
                    $scope.form = { REPORT_CODE: 'RPT-5003',  FROM_MONTH : new Date(), IS_EXCEL_FORMAT: 'N' };
                    $scope.cancel = function () {
                        $modalInstance.dismiss('cancel');
                    };
                    $scope.FROM_MONTH_OPEN = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.FROM_MONTH_OPENED = true;
                    };

                    $scope.TO_MONTH_OPEN = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.TO_MONTH_OPENED = true;
                    };

                    $scope.compOptions = {
                        optionLabel: "-- Select Company --",
                        filter: "contains",
                        autoBind: true,
                        dataTextField: "COMP_NAME_EN",
                        dataValueField: "HR_COMPANY_ID",
                        dataSource: {
                            transport: {
                                read: function (e) {
                                    DashBoardService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {
                                        e.success(res);
                                    }, function (err) {
                                        console.log(err);
                                    });
                                }
                            }
                        },
                        change: function (e) {
                            var dataItem = this.dataItem(e.item);
                            $scope.form['COMP_NAME_EN'] = dataItem.COMP_NAME_EN;
                            return $scope.officeDataSource = new kendo.data.DataSource({
                                transport: {
                                    read: function (e) {
                                        DashBoardService.getDataByFullUrl('/api/common/GetOfficeList?pHR_COMPANY_ID=' + dataItem.HR_COMPANY_ID + '&pOption=3004').then(function (res) {
                                            e.success(res);
                                        });
                                    }
                                }
                            });
                        }
                    }

                    $scope.officeOption = {
                        optionLabel: "-- Select --",
                        filter: "contains",
                        autoBind: true,
                        dataTextField: "OFFICE_NAME_EN",
                        dataValueField: "HR_OFFICE_ID",
                        change: function (e) {
                            var ditm = this.dataItem(e.item);
                            $scope.form['OFFICE_NAME_EN'] = ditm.OFFICE_NAME_EN;
                            return $scope.sectionDataSource = new kendo.data.DataSource({
                                transport: {
                                    read: function (e) {
                                        DashBoardService.getDataByFullUrl('/Hr/OffDayRoaster/getDeptByCompanyOffice?pHR_COMPANY_ID=' + vm.form.HR_COMPANY_ID + '&pHR_OFFICE_ID=' + ditm.HR_OFFICE_ID).then(function (res) {
                                            e.success(res);
                                        });
                                    }
                                }
                            });
                        }
                    }


                    $scope.officeDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                DashBoardService.getDataByFullUrl('/api/common/GetOfficeList?pHR_COMPANY_ID&pOption=3004').then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    });



                    $scope.reportPrint = function () {
                        var form = document.createElement("form");
                        form.setAttribute("method", "post");
                        form.setAttribute("action", "/api/pln/PlnReport/PreviewReportRDLC");
                        form.setAttribute("target", '_blank');
                        for (var i in $scope.form) {
                            if ($scope.form.hasOwnProperty(i)) {

                                var input = document.createElement('input');
                                input.type = 'hidden';
                                input.name = i;
                                input.value = ($scope.form[i] instanceof Date) ? $scope.form[i].toISOString() : $scope.form[i];
                                form.appendChild(input);
                            }
                        }
                        document.body.appendChild(form);
                        form.submit();
                        document.body.removeChild(form);
                    };


                },
                size: 'small',
                scope: $scope,
                windowClass: 'large-Modal'
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        vm.hr = {};
        vm.hrReportPrint = function (reportCode) {
            //alert('test');
            vm.hr.REPORT_CODE = angular.copy(reportCode);


            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/Hr/HrReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = vm.hr;

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


        vm.GotoLabdipStatus = function (pLD_REQ_NO, pSTYLE_NO, pLD_REQ_TO, pLD_REQ_BY, pMC_BYR_ACC_ID) {
            localStorage.setItem("LsStatusLD_REQ_NO", pLD_REQ_NO);
            localStorage.setItem("LsStatusSTYLE_NO", pSTYLE_NO);
            localStorage.setItem("LsLD_REQ_TO", pLD_REQ_TO);
            localStorage.setItem("LsLD_REQ_BY", pLD_REQ_BY);
            localStorage.setItem("LsMC_BYR_ACC_ID", pMC_BYR_ACC_ID);
            window.location = "/Merchandising/Mrc/LabdipSubmission?a=87/#/LabdipSubmission";
        }




        function getTnADataListQuery() {
            vm.PO_LIST_OPTIONS_TASK_DS = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                schema: {
                    data: "data",
                    total: "total",
                    model: {
                        fields: {
                            PLAN_START_DT: { type: "date" },
                        }
                    }
                },
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/common/OrderTnADataTaskDashBord';
                        url += '?pageNumber=' + (params.page || 1) + '&pageSize=' + (params.pageSize || 10);
                        url += config.kFilterStr2QueryParam(params.filter);

                        return DashBoardService.getDataByUrl(url).then(function (res) {

                            vm.tnaFollowupDataLen = res.data.length;
                            e.success({
                                total: res.total,
                                data: res.data
                            });
                        }, function (err) {
                            console.error(err);
                        })
                    }
                },
                pageSize: 10
            });
        }

        vm.openTnaUpdateModal = function (dataItem) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Merchandising/Mrc/_UpdateTnAModal',
                controller: 'TnaUpdateModalController',
                size: 'small',
                windowClass: 'large-Modal',
                resolve: {
                    Order: function () {
                        return dataItem;
                    },
                    Task: function () {
                        return dataItem;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                if (data) {
                    $("#PO_LIST_TASK").data("kendoGrid").dataSource.read();
                }
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        vm.searchTnaTask = function (SearchTerm) {

            if (SearchTerm.length == 0) {
                vm.PO_LIST_OPTIONS_TASK_DS.query({});

            } else if (SearchTerm.length >= 3) {
                var filter = [
                        { field: 'TA_TASK_NAME_EN', operator: "contains", value: SearchTerm },
                ];
                vm.PO_LIST_OPTIONS_TASK_DS.query({ filter: filter });
            }

        };

        vm.tnaTaskFollowupScreen = function (data) {

            var url = '/Merchandising/Mrc/TnAFollowup?a=101/#/main/' + data.MC_BYR_ACC_ID + '/TnaTask/' + data.MC_ORDER_H_ID + '/list';
            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 10) + ',scrollbars=yes,status=yes';
            $window.open(url, "_blank", opt);
        };


        vm.PO_LIST_OPTIONS_TASK = {
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
            height: 250,
            scrollable: {
                virtual: true
            },

            selectable: false,
            sortable: true,
            columns: [
                {
                    title: "Style",
                    field: 'WORK_STYLE',
                    width: "80px",
                    filterable: true,
                    template: function () {
                        return "<a ng-click='vm.tnaTaskFollowupScreen(dataItem)'>{{dataItem.WORK_STYLE}}</a>";
                    },
                },
                {
                    title: "Order",
                    field: 'ORDER_NO',
                    width: "80px",
                    filterable: true
                },
                {
                    title: "Task Name",
                    field: 'TA_TASK_NAME_EN',
                    template: function () {
                        return "<span class='item-status'><span class='badge badge-empty' ng-class=\"{'badge-warning':!dataItem.ACT_START_DT,'badge-success':dataItem.ACT_START_DT}\"></span> {{dataItem.TASK_ORDER}}</span>. <a class='btn btn-link btn-xs' ng-class=\"{'disabled': dataItem.IS_AUTO_UPDATE=='Y', 'disabled': dataItem.U,'disabled' : dataItem.IS_START_TASK=='Y'}\" ng-click='vm.openTnaUpdateModal(dataItem)'>{{dataItem.TA_TASK_NAME_EN}}</a>";
                    },
                    width: "180px",
                    filterable: true
                },
                 {
                     field: "DAYS_TO_GO", title: "D2G", type: "string", width: "35px", filterable: false,
                     template: function () {
                         return "<span class='badge' ng-class=\"{'badge-warning':dataItem.DAYS_TO_GO==0 && !dataItem.ACT_START_DT}\">{{dataItem.DAYS_TO_GO}}</span>";
                     },

                 },
                 {
                     field: "PLAN_START_DT", title: "Plan", type: "date", width: "60px", format: "{0:dd-MMM-yyyy}"
                 }
            ]
        };


        vm.taBookingPoHrdDs = new kendo.data.DataSource({
            serverPaging: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    fields: {
                        ACCS_PO_DT: { type: "date" },
                        ACCS_DELV_DT: { type: "date" }
                    }
                }
            },
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/mrc/TaBooking/AccPoHeaderList';
                    url += '?pMC_TNA_TASK_STATUS_ID=27';
                    url += '&pSC_USER_ID=' + cur_user_id;
                    //url += '&pMC_BUYER_ID=' + (pMC_BUYER_ID || null);
                    //url += '&pMC_STYLE_H_EXT_ID=' + (pMC_STYLE_H_EXT_ID || null);
                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += config.kFilterStr2QueryParam(params.filter);
                    DashBoardService.getDataByUrl(url).then(function (res) {
                        e.success(res);
                    }, function (err) {
                        console.log(err);
                    })
                }
            },
            pageSize: 50
        });

        vm.BookingPoHrdDsLength = 0;

        function loadTaBookingPoHrdDs() {
            var url = '/api/mrc/TaBooking/AccPoHeaderList';
            url += '?pMC_TNA_TASK_STATUS_ID=27';
            url += '&pSC_USER_ID=' + cur_user_id;
            url += '&pageNumber=1&pageSize=1';
            DashBoardService.getDataByUrl(url).then(function (res) {
                vm.BookingPoHrdDsLength = res.data.length;
            });
        };

        vm.taBookingPoHrdOpt = {
            sortable: true,
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
            height: 250,

            scrollable: {
                virtual: true
            },
            columns: [
                { field: "STYLE", title: "Style", width: "60px" },
                { field: "ORDER_NO_LIST", title: "Order#", width: "60px" },
                //{
                //    title: "PO #",
                //    field: "ACCS_PO_NO",
                //    template: function () {
                //        return "<a ui-sref='TaBooking.item({pMC_STYL_BGT_H_ID: dataItem.MC_STYL_BGT_H_ID, pMC_ACCS_PO_H_ID : dataItem.MC_ACCS_PO_H_ID,pBLK_BOM_LIST:dataItem.BLK_BOM_LIST,pBLK_BOM_ACT: dataItem.BLK_BOM_ACT, pINV_ITEM_ID :  dataItem.ITEM_ACT})' class='btn btn-xs btn-link' title='Edit PO #'> {{dataItem.ACCS_PO_NO}}</a>";
                //    },
                //    width: "60px"
                //},
                { field: "ACCS_PO_SUB", title: "Subject of PO", width: "120px" },
                {
                    headerTemplate: "<span style='background-color:green;'><input type='checkbox' ng-model='vm.IS_ALL_APPROVED' ng-change='vm.selectForTABookingAll(vm.IS_ALL_APPROVED,\"APPROVED\");vm.IS_ALL_REJECT=false;vm.IS_ALL_RESEND=false;'></span>",
                    template: function () {
                        return "<span style='background-color:green;'><input type='checkbox' ng-model='dataItem.APPROVED' ng-click='dataItem.RESEND=false;dataItem.REJECT=false'></span>";
                    },
                    width: "10px"
                },
                {
                    headerTemplate: "<span style='background-color:powderblue;'><input type='checkbox' ng-model='vm.IS_ALL_RESEND' ng-change='vm.selectForTABookingAll(vm.IS_ALL_RESEND,\"RESEND\",vm.IS_ALL_APPROVED,vm.IS_ALL_REJECT);vm.IS_ALL_APPROVED=false;vm.REJECT=false;'></span>",
                    template: function () {
                        return "<span style='background-color:powderblue;'><input type='checkbox' ng-model='dataItem.RESEND' ng-click='dataItem.REJECT=false;dataItem.APPROVED=false'></span>";
                    },
                    width: "10px"
                },
                {
                    headerTemplate: "<span style='background-color:red;'><input type='checkbox' ng-model='vm.IS_ALL_REJECT' ng-change='vm.selectForTABookingAll(vm.IS_ALL_REJECT,\"REJECT\",vm.IS_ALL_APPROVED,vm.IS_ALL_RESEND);vm.IS_ALL_APPROVED=false;vm.IS_ALL_RESEND=false;'></span>",
                    template: function () {
                        return "<span style='background-color:red;'><input type='checkbox' ng-model='dataItem.REJECT' ng-click='dataItem.RESEND=false;dataItem.APPROVED=false'></span>";
                    },
                    width: "10px"
                },


                //{
                //    title: ">>>",
                //    template: function () {
                //        return '<ul kendo-menu k-orientation="vertical">' +
                //                '<li>More' +
                //                    '<ul style="width:150px;">' +
                //                        '<li ng-show="vm.empLevelData.APROVER_LVL_NO==0"><a style="color:black" href="/Merchandising/Mrc/BulkFabBkEntry?a=93/#/bulkFabBkEntry/{{dataItem.MC_BLK_FAB_REQ_H_ID}}/dtl" ><i class="fa fa-edit"> Booking Edit</i></a></li>' +
                //                        '<li><i class="fa fa-print"> Booking Print</i>' +
                //                            '<ul style="width:150px;"><li class="k-item k-state-default k-first" ng-repeat="itm in dataItem.REVISION_LIST"><a class="k-link" style="color:black" ng-click="vm.navigateAction(dataItem,2,itm.REVISION_NO)">{{itm.REV_REASON}}</a></li></ul>' +
                //                        '</li>' +
                //                        '<li ng-show="vm.empLevelData.APROVER_LVL_NO==0"><a style="color:black" href="/Merchandising/Mrc/BudgetSheet?a=93/#/BudgetSheet/{{dataItem.MC_BLK_FAB_REQ_H_ID}}/{{dataItem.MC_STYLE_H_ID}}/{{dataItem.MC_STYL_BGT_H_ID}}" ><i class="fa fa-edit"> Budget Edit</i></a></li>' +
                //                        '<li><a style="color:black" ng-click="vm.navigateAction(dataItem,7)" ><i class="fa fa-print"> Budget Print</i></a></li>' +
                //                    '</ul>' +
                //                '</li></ul>';
                //    },
                //    width: "42px"
                //},
            ]
        };


        var taPOStatus = {
            'APPROVED': 28,
            'REJECT': 29,
            'RESEND': 30
        };

        vm.selectForTABookingAll = function (IS_TRUE, col) {
            angular.forEach(vm.taBookingPoHrdDs.data(), function (dataItem) {
                if (col === 'APPROVED') {
                    dataItem.set(col, IS_TRUE);

                    dataItem.set('REJECT', false);
                    dataItem.set('RESEND', false);

                } else if (col === 'REJECT') {
                    dataItem.set(col, IS_TRUE);

                    dataItem.set('APPROVED', false);
                    dataItem.set('RESEND', false);

                } else if (col === 'RESEND') {
                    dataItem.set(col, IS_TRUE);

                    dataItem.set('APPROVED', false);
                    dataItem.set('REJECT', false);
                }
            });

        }

        vm.saveTAAccessoriesPo = function () {

            var data2BSave = [];

            angular.forEach(vm.taBookingPoHrdDs.data(), function (dataItem) {

                if ((dataItem.APPROVED ? taPOStatus['APPROVED'] : (dataItem.REJECT ? taPOStatus['REJECT'] : (dataItem.RESEND ? taPOStatus['RESEND'] : 0))) != 0) {
                    data2BSave.push({
                        MC_ACCS_PO_H_ID: dataItem.MC_ACCS_PO_H_ID,
                        MC_TNA_TASK_STATUS_ID: dataItem.APPROVED ? taPOStatus['APPROVED'] : (dataItem.REJECT ? taPOStatus['REJECT'] : (dataItem.RESEND ? taPOStatus['RESEND'] : 0))
                    });


                }
            });

            if (data2BSave.length == 0) {
                config.ToastInfoMsg('Nothing to update. Please select atleast one PO');
                return;
            }

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'TaBookingPOApprovalModal.html',
                controller: function ($scope, data) {
                    $scope.data = data;

                    $scope.form = {
                        REASON_OF_REJECT: '',
                        REASON_OF_RESEND: ''
                    };

                    $scope.cancel = function () {
                        modalInstance.dismiss();
                    }

                    $scope.save = function () {





                        modalInstance.close({
                            REASON: $scope.form,
                            MainData: data.data
                        });
                    }
                },
                size: 'm',
                windowClass: 'large-Modal',
                resolve: {
                    data: function () {
                        return {
                            data: data2BSave,
                            IS_RESEND: _.some(data2BSave, function (o) { return o.MC_TNA_TASK_STATUS_ID == taPOStatus['RESEND'] }),
                            IS_REJECT: _.some(data2BSave, function (o) { return o.MC_TNA_TASK_STATUS_ID == taPOStatus['REJECT'] })
                        }
                    }
                }
            });

            modalInstance.result.then(function (data) {
                if (data) {
                    angular.forEach(data.MainData, function (val, key) {
                        if (val.MC_TNA_TASK_STATUS_ID == taPOStatus['RESEND']) {
                            val['REMARKS'] = data.REASON.REASON_OF_RESEND;
                        } else if (val.MC_TNA_TASK_STATUS_ID == taPOStatus['REJECT']) {
                            val['REMARKS'] = data.REASON.REASON_OF_REJECT;
                        } else {
                            val['REMARKS'] = '';
                        }

                    });

                    var obXML = DashBoardService.xmlStringShort(data.MainData);

                    return DashBoardService.getDataByUrl('/api/mrc/TaBooking/DashboardApprovalData?XML=' + obXML).then(function (res) {
                        var result = angular.fromJson(res);
                        if (result.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.taBookingPoHrdDs.read();
                            loadTaBookingPoHrdDs();
                        };

                        config.appToastMsg(result.PMSG);

                    });
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });



        }

        function getPurchasePlanData() {
            vm.PurchaseRequisitionList = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = config.kFilterStr2QueryParam(params.filter);
                        DashBoardService.getDataByUrl('/api/knit/KnitYarnReq/SelectAll/' + params.page + '/' + params.pageSize + '?pRF_REQ_SRC_ID=0' + pm).then(function (res) {
                            e.success(res);
                            vm.PurchaseRequisitionListlength = res.total;
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            });
        }


        vm.gridPurchaseRequisition = {
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains"
                    }
                }
            },
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            sortable: true,
            columns: [
                { field: "PURC_REQ_NO", title: "Reqisition No", type: "string", width: "15%" },
                { field: "PURC_REQ_DT", title: "Req. Date", type: "date", template: "#= kendo.toString(kendo.parseDate(PURC_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "12%" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "15%" },
                {
                    title: "Action",
                    template: function () {
                        return "</a><a href='/Knitting/Knit/ComparativeStatement?a=308/#/ComparativeStatement?pSCM_PURC_REQ_H_ID={{dataItem.SCM_PURC_REQ_H_ID}}' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a></a>";
                    },
                    width: "10%"
                }
            ]
        };


    }



})();