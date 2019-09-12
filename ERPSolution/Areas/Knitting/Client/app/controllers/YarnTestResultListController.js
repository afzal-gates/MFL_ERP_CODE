(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnTestResultListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$modal', '$filter', '$window', YarnTestResultListController]);
    function YarnTestResultListController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $modal, $filter, $window) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};
        console.log(formData);
        vm.form = formData.KNT_YRN_LOT_TEST_H_ID ? formData : { RF_ACTN_VIEW: 0 };
        vm.formItem = {};
        vm.form["SHOW_VIEW"] = 0;


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [RequisitionList(), getUserData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.updateTestResult = function (item) {
            console.log(item);
            vm.form.SHOW_VIEW = 2;
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Knitting/Knit/_KnitYarnTestRpt',
                controller: 'KnitYarnTestRptController',
                controllerAs: 'vm',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    formData: function (KnittingDataService) {
                        return item;
                    }
                }
            });
            modalInstance.result.then(function (dataItem) {
                console.log(dataItem);
                vm.form.SHOW_VIEW = 0;

            }, function () {
                vm.form.SHOW_VIEW = 0;
                console.log('Modal dismissed at: ' + new Date());
            });
        }



        function RequisitionList() {
            return vm.RequisitionItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/KntPlanYarnTest/GetYarnTestDtl?pKNT_YRN_LOT_TEST_H_ID=' + ($stateParams.pKNT_YRN_LOT_TEST_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getUserDatalist().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    vm.IsChange = false;
                },

                height: 400,
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };

            //$("#customers").kendoDropDownList(vm.userList);
        }

        vm.form.INV_ITEM_CAT_ID = 2;

        $scope.TEST_WO_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TEST_WO_DT_LNopened = true;
        }


        $scope.FROM_DATE_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.FROM_DATE_LNopened = true;
        }
        $scope.TO_DATE_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TO_DATE_LNopened = true;
        }

        vm.reset = function () {

            $state.go('YarnTestReq', { pKNT_YRN_STR_REQ_H_ID: 0 });

        };


        vm.printReport = function (dataItem) {
            var url = '/Knitting/Knit/YarnTestResultPrint/#/YarnTestResultPrint?pKNT_YRN_LOT_TEST_H_ID=' + dataItem.KNT_YRN_LOT_TEST_H_ID + '&pKNT_YRN_LOT_TEST_D1_ID=' + dataItem.KNT_YRN_LOT_TEST_D1_ID
            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 300) + ',scrollbars=yes,status=yes';
            $window.open(url, "_blank", opt);
        }


        vm.gridOptionsItem = {
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
                { field: "YARN_ITEM_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "LK_DIA_TYPE_ID", hidden: true },
                { field: "KNT_YRN_LOT_ID", hidden: true },
                { field: "RF_YRN_CNT_ID", hidden: true },

                { field: "REQ_DOC_NO", hidden: true },
                { field: "MC_COLOR_ID", hidden: true },
                { field: "RF_FAB_TYPE_ID", hidden: true },
                { field: "RF_FIB_COMP_ID", hidden: true },
                { field: "FIN_GSM", hidden: true },
                { field: "FIN_DIA", hidden: true },
                { field: "SCM_STORE_ID", hidden: true },
                { field: "HAS_YRN_FAB", hidden: true },
                { field: "STITCH_LEN", hidden: true },
                { field: "LK_COLR_GRP_LST", hidden: true },
                { field: "COLOR_NAME_EN", hidden: true },

                { field: "ITEM_NAME_EN", title: "Yarn Item", type: "string", width: "15%" },
                { field: "YRN_COLR_GRP", title: "Color Group", type: "string", width: "10%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "10%" },
                { field: "YRN_LOT_NO", title: "Lot#", type: "string", width: "10%" },
                { field: "RQD_QTY", title: "Reqd Qty", type: "string", width: "10%" },
                { field: "CONE_QTY", title: "Reqd Cone", type: "string", width: "10%" },

                //{ field: "SP_NOTE", title: "Note", width: "10%" },
                {
                    title: "",
                    template: function () {
                        return '<a  title="Edit" ng-click="vm.updateTestResult(dataItem)" class="btn btn-xs green"><i class="fa fa-edit">Update Result</i></a>' +
                            ' <a  class="btn btn-xs blue" title="Edit" ng-click="vm.printReport(dataItem)"> <i class=\'fa fa-print\'></i> Print</a> ';
                    },
                    width: "15%"
                }
            ],
            //editable: true
        };

    }


})();



