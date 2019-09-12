(function () {
    'use strict';

    angular.module('multitex.mrc').controller('ColourRefToBuyerController', ['$modalInstance', '$scope', 'Buyer', 'ColourList', '$state', 'config', 'MrcDataService', ColourRefToBuyerController]);

    function ColourRefToBuyerController($modalInstance, $scope, Buyer, ColourList, $state, config, MrcDataService) {



        $scope.ColourList = {
                optionLabel: "-- Select Colour --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(ColourList)
                        }
                    }
                },
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID"
            };


        $scope.save = function (token) {

            var adata = [];
            var XML = null;


            var data = $('#kendoGrid').data("kendoGrid").dataSource.data();

                angular.forEach(data, function (val, key) {
                    adata.push({ MC_BU_COL_REF_ID: val.MC_BU_COL_REF_ID, MC_COLOR_ID: val.MC_COLOR_ID, MC_BUYER_ID: val.MC_BUYER_ID, COLOR_REF: val.COLOR_REF, IS_SWATCH: val.IS_SWATCH, OTHER_REF: val.OTHER_REF });
                });
               
                if (adata.length > 0) {
                    var XML = MrcDataService.xmlStringShort(adata);

                    return MrcDataService.saveDataByUrl({ XML: XML }, '/ColourMaster/UpdateWithXML', token).then(function (res) {
                              res['data'] = angular.fromJson(res.jsonStr);
                              if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                  config.appToastMsg(res.data.PMSG);
                                  $('#kendoGrid').data("kendoGrid").dataSource.read();
                              } else {
                                  toastr.error('Please check colour and/or colour ref#', "MultiTEX");
                              }

                        }, function (err) {
                            console.log(err);
                        })

                }
               
        };

        $scope.addNew = function () {
            
            var data = { MC_BU_COL_REF_ID: -1, MC_COLOR_ID: null, COLOR_REF: '', IS_SWATCH: 'N', OTHER_REF: '', MC_BUYER_ID: Buyer.MC_BUYER_ID }
            $('#kendoGrid').data("kendoGrid").dataSource.insert(0,data);
        }

        $scope.remove = function (data) {
            $('#kendoGrid').data("kendoGrid").dataSource.remove(data);
        }

        $scope.Buyer = Buyer;

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };



        

        $scope.gridOptions = {
            autoBind: true,
            height: '400px',
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.getDataByUrl('/ColourMaster/ColourMappDataByBuyerId/' + Buyer.MC_BUYER_ID).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        })
                    }
                }
            },
            filterable: true,
            selectable: false,
            sortable: true,
            pageable: false,
            columns: [
                {
                    title: "Colour",
                    template: function () {
                        return "<select kendo-drop-down-list  options='ColourList' ng-model='dataItem.MC_COLOR_ID' name='colour-{{dataItem.uid}}' style='width:200px;' required ></select>";
                    },
                    width: "100px"
                },
                {
                    title: "Colour Ref#",
                    template: function () {
                        return "<input type='text' ng-model='dataItem.COLOR_REF' name='colour-{{dataItem.uid}}' style='width:190px;' required/>";
                    },
                    width: "100px"
                },
                {
                    title: "Swatch?",
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.IS_SWATCH' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                    },
                    width: "40px"
                },
                {
                    title: "Other Ref#",
                    template: function () {
                        return "<input type='text' ng-model='dataItem.OTHER_REF' style='width:180px;'/>";
                    },
                    width: "100px"
                },
                {
                    title: "Action",
                    template: '<a ng-click="addNew()" class="btn btn-xs blue"><i class="fa fa-plus">Add</i> </a> <a ng-click="remove(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle">Rem</i></a>',
                    width: "60px"
                }
            ]
        };

    }

})();