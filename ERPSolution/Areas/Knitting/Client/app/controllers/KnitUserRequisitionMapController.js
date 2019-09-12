(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitUserRequisitionMapController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', '$filter', KnitUserRequisitionMapController]);
    function KnitUserRequisitionMapController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, $filter) {

        var vm = this;

        $scope.format = config.appDateFormat;
        vm.currentDate = $filter('date')(new Date(), config.appDateFormat);

        vm.form = {};
        vm.errors = {};

        activate();
        vm.Title = $state.current.Title || '';
        vm.showSplash = true;

        function activate() {
            var promise = [getUserData(), loadRequisitionList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }


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
        }


        vm.loadRequisitionTypeList = function () {
            if (vm.form.SC_USER_ID) {
                vm.gridOptionsDS.read();
            }
        }


        $scope.makeBatchSelected = function (IS_TRUE) {
            var data = $('#kendoGrid').data("kendoGrid").dataSource.data();
            angular.forEach($('#kendoGrid').data("kendoGrid").dataSource.data(), function (dataItem) {
                dataItem.set('IS_ACTIVE', IS_TRUE ? 'Y' : 'N');
            });

        }


        function loadRequisitionList() {

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: false,
                serverFiltering: false,
                transport: {
                    read: function (e) {
                        vm.showSplash = true;
                        console.log(vm.form.SC_USER_ID);
                        KnittingDataService.getDataByFullUrl('/api/knit/KnitUserReqMap/GetMppingInfoByUserID?pSC_USER_ID=' + (vm.form.SC_USER_ID || null)).then(function (res) {
                            e.success(res);
                            vm.showSplash = false;
                        });
                    }
                },
            })
        }




        vm.gridOptions = {
            //autoBind: true,            
            sortable: false,
            pageable: false,
            columns: [
                { field: "RF_REQ_TYPE_ID", hidden: true },
                { field: "SC_MAP_USR_RQSTYP_ID", hidden: true },
                { field: "SC_USER_ID", hidden: true },
                {
                    headerTemplate: "<input type='checkbox' ng-model='IS_ALL_SELECT' ng-change='makeBatchSelected(IS_ALL_SELECT)'> Select All",
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                    },
                    width: "15%"
                },
                { field: "REQ_TYPE_CODE", title: "Code", width: "35%" },
                { field: "REQ_TYPE_NAME", title: "Requisition Type Name", width: "40%" },
            ],
            //editable: true
        };

        vm.submitData = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var list = _.filter(vm.gridOptionsDS.data(), function (x) { return x.IS_ACTIVE == "Y" });
                data["XML_REQ"] = KnittingDataService.xmlStringShort(list.map(function (o) {
                    return {
                        SC_MAP_USR_RQSTYP_ID: o.SC_MAP_USR_RQSTYP_ID == null ? 0 : o.SC_MAP_USR_RQSTYP_ID,
                        RF_REQ_TYPE_ID: o.RF_REQ_TYPE_ID == null ? 0 : o.RF_REQ_TYPE_ID,
                        SC_USER_ID: vm.form.SC_USER_ID,
                    }
                }));

                return KnittingDataService.saveDataByUrl(data, '/KnitUserReqMap/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        vm.gridOptionsDS.read();
                    }
                });
            }
        }
    }


})();