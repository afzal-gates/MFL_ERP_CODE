(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitPlanYarnTestReqListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'cur_user_id', '$window', KnitPlanYarnTestReqListController]);
    function KnitPlanYarnTestReqListController($q, config, KnittingDataService, $stateParams, $state, $scope, cur_user_id, $window) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.printKnitCard = function (item) {
            var url = '/Knitting/Knit/KnitCardTestRpt/#/KnitCardTestRpt?pKNT_JOB_CRD_H_ID=' + item.KNT_JOB_CRD_H_ID;
            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 300) + ',scrollbars=yes,status=yes';
            $window.open(url, "_blank", opt);
        }

        vm.gridOptions = {
            dataSource: new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = KnittingDataService.kFilterStr2QueryParam(params.filter)
                        console.log(pm);
                        KnittingDataService.getDataByFullUrl('/api/knit/KntPlanYarnTest/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pRF_REQ_TYPE_ID=4&pUSER_ID=' + cur_user_id).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            }),
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
            scrollable: false,
            columns: [
                { field: "KNT_YRN_LOT_TEST_H_ID", hidden: true },
                { field: "KNT_YRN_STR_REQ_H_ID", hidden: true },
                { field: "KNT_JOB_CRD_H_ID", hidden: true },

                { field: "TEST_WO_NO", title: "Test Order No", type: "string", width: "8%" },
                { field: "TEST_WO_DT", title: "Order Date", type: "date", template: "#= kendo.toString(kendo.parseDate(TEST_WO_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "8%" },
                //{ field: "USER_NAME_EN", title: "Create By", type: "string", width: "10%" },
                { field: "TEST_WO_BY_NAME", title: "Req. By", type: "string", width: "8%" },
                { field: "ATTN_TO", title: "Attn. To", type: "string", width: "8%" },
                { field: "YARN_ITEM_INFO", title: "Yarn Description", type: "string", width: "40%" },

                {
                    field: "EVENT_NAME", title: "Status", type: "string", width: "10%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_sr'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SR'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'

                },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="KnitPlanYarnTestReq({pKNT_YRN_LOT_TEST_H_ID:dataItem.KNT_YRN_LOT_TEST_H_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ') " class="btn btn-xs blue"><i class="fa fa-edit">{{dataItem.NXT_ACTION_NAME}}</i></a> ' +
                            ' <a ui-sref="YarnTestResultList({pKNT_YRN_LOT_TEST_H_ID:dataItem.KNT_YRN_LOT_TEST_H_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'SR'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'DA'" + ') " class="btn btn-xs yellow-gold"><i class="fa fa-edit">Yarn Test</i></a></a>';
                            //' <a class="btn btn-xs green" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'SR'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'DA'" + ') " ng-click="vm.printKnitCard(dataItem)"><i class="fa fa-file-text"> Print Knit Card</i></a></a>';
                    },
                    width: "7%"
                },
                {
                    title: "Print",
                    template: function () {
                        return '<ul id="myMenu" style="z-index:9999;" kendo-menu k-orientation="menuOrientation" k-rebind="menuOrientation" k-on-select="onSelect(kendoEvent)">' +
                                '<li><span style="color:black; z-index:9999">Knit Card #</span>' +
                                    '<ul style="width:150px;"><li class="k-item k-state-default k-first" ng-repeat="itm in dataItem.KNT_JOB_CRD_H_LST"><a class="k-link" style="color:black" ng-click="vm.printKnitCard(itm)">{{itm.JOB_CRD_NO}}</a></li></ul>' +
                                                                
                                '</li></ul>';
                    },
                    width: "10%"
                },
            ]
        };

    }

})();