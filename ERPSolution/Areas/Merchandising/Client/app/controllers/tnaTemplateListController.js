(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TnaTemplateListController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', TnaTemplateListController]);
    function TnaTemplateListController($q, config, MrcDataService, $stateParams, $state, $scope, logger) {

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



        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.selectAllData('TnaTemplate').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        })
                    }
                },
                pageSize: 10
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
            selectable: "row",
            sortable: true,
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "TNA_TMPLT_CODE", title: "Template Code", type: "string", width: "80px" },
                { field: "BUYER_NAME_EN", title: "Buyer", type: "string", width: "80px", filterable: { multi: true } },
                { field: "LK_ORDER_TYPE", title: "Order Type", type: "string", width: "50px", filterable: { multi: true } },
                { field: "LK_GARM_TYPE", title: "Gmt. Type", type: "string", width: "50px", filterable: { multi: true } },
                {
                    title: "Lead Time",
                    columns: [
                      { field: "LEAD_TIME_FROM", title: "From", type: "number", width: "40px",filterable: false  },
                      { field: "LEAD_TIME_TO", title: "To", type: "number", width: "40px", filterable: false }
                    ]
                },
                 { field: "REMARKS", title: "Remarks", type: "string", width: "120px", filterable: false },

                {
                    title: "Action",
                    template: function () {
                        return "</a><a ui-sref='TnaTemplate.Dtl({MC_TNA_TMPLT_H_ID:dataItem.MC_TNA_TMPLT_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a>&nbsp;</a> <a ui-sref='TnaTemplate.Dtl({MC_TNA_TMPLT_H_ID: 0, CPY_TNA_TMPLT_ID : dataItem.MC_TNA_TMPLT_H_ID})' class='btn btn-xs purple'><i class='fa fa-copy'>Copy</i></a>";
                    },
                    width: "60px"
                }
            ]
        };
    }

})();