(function () {
    'use strict';
    angular.module('multitex.mrc').controller('InquiryListController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', InquiryListController]);
    function InquiryListController($q, config, MrcDataService, $stateParams, $state, $scope, logger) {

        var vm = this;
        vm.Title = $state.current.Title || '';

           vm.dtFormat = config.appDateFormat;
    
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.edit = function (data) {

            if(data.MC_INQR_H_ID>0 && data.MC_STYLE_ID==0){
                $state.go('Inquiry.Style',{MC_INQR_H_ID:data.MC_INQR_H_ID,MC_STYLE_ID:0})
            } else{
                $state.go('Inquiry.Style.Item',{MC_INQR_H_ID:data.MC_INQR_H_ID,MC_STYLE_ID:data.MC_STYLE_ID,MC_STYLE_ITEM_ID:0})
            }
        }

        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.getDataByUrl('/InquiryH/InquiryDataForList').then(function (res) {
                            console.log(res);
                            e.success(res);
                            },
                            function (err) {
                                console.log(err);
                                e.success([]);
                            });
                    }
                },
                pageSize: 15,
                group: [{ field: "BUYER_NAME_EN" }],
            },
            filterable: true,
            selectable: "row",
            sortable: true,
            groupable: false,
            pageSize: 15,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "BUYER_NAME_EN", title: "Buyer", type: "string", width: "80px" },
                { field: "INQR_NO", title: "Inquiry #", type: "string", width: "80px" },
                { field: "STYLE_NO", title: "Style No #", type: "string", width: "80px" },
                { field: "STYL_EXT_NO", title: "Style Ext. #", type: "string", width: "60px" },
                { field: "STYLE_DESC", title: "Style Desc.", type: "string", width: "150px" },

                //{ field: "LK_INCOTERM", title: "Inco Term", type: "string", width: "80px" },
                //{ field: "LK_LCTERM", title: "LC Term", type: "string", width: "80px" },
                
                //{ field: "QUOTE_NO", title: "Quote #", width: "60px"},
                //{ field: "QUOTE_DT", title: "Quote No", type: "date", width: "80px", format: "{0:" + vm.dtFormat + "}" },                
                { field: "INQR_DT", title: "Inquiry DT", type: "date", width: "80px", format: "{0:" + vm.dtFormat + "}" },
                { field: "LK_INQ_STATUS", title: "Status", type: "string", width: "60px" },

                {
                    title: "Action",
                    template: function () {
                        return "</a><a ng-click='vm.edit(dataItem)' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a></a>&nbsp;<a ng-click='vm.edit(dataItem)' class='btn btn-xs purple'>Revise</a>&nbsp;<a class='btn btn-xs purple'>Send Quotation</a>&nbsp;<a  class='btn btn-xs purple'>Confirm</a>&nbsp;<a ng-click='vm.edit(dataItem)' class='btn btn-xs default'>Cancel</a>&nbsp;";
                    },
                    width: "250px"
                }
            ]
        };



   }

})();