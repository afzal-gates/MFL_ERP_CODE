(function () {
    'use strict';
    angular.module('multitex.planning').controller('IeGmtItemVariationController', ['$q', 'config', 'Dialog', 'IeDataService', '$stateParams', '$state', '$scope', '$filter', '$modal', '$timeout', IeGmtItemVariationController]);
    function IeGmtItemVariationController($q, config, Dialog, IeDataService, $stateParams, $state, $scope, $filter, $modal, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        var prodTypeList = [];
        
        vm.form = {GMT_PART_GRP_CAT_ID: null, GMT_CAT_ID: null};

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };
        
              
       
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getGmtCategoryGroupList(), getGmtCategoryList()];
            return $q.all(promise).then(function () {
               
                vm.showSplash = false;                
            });
        }

        vm.ItmGroup = [
            { label: 'TOP (except POLO)', value: 234 },
            { label: 'Polo', value: 783 },
            { label: 'Bottom', value: 235 },
            { label: 'Jacket', value: 780 },
            { label: 'Romper', value: 781 },
            { label: 'Cap', value: 782 },
            { label: 'Others', value: 743 }
        ];


        vm.addItem = function (data) {
            var Obj = {};
            Obj['GMT_IE_ITM_VARIATION_ID'] = -1;
            Obj['TECH_SPEC_DESC'] = '';
            Obj['LK_ITEM_GRP_ID'] = data[data.length - 1]['LK_ITEM_GRP_ID'];

            
            Obj['REQ_OP'] = '';
            Obj['REQ_HLP'] = '';
            vm.FormData.OrdVols.forEach(function (val, key) {
                Obj[val.GMT_IE_ITM_OQVOL_ID.toString()] = {
                    CM_PER_DZ : 0,
                    GMT_IE_ITM_OQVOL_ID: val.GMT_IE_ITM_OQVOL_ID,
                    CPM : 0
                };
            });

            data.push(Obj);
         
        }
      
     
        function getGmtCategoryGroupList() {
            return vm.gmtCategoryGroupDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        IeDataService.getDataByFullUrl('/api/mrc/StyleItem/InvItemByParent/8').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });            
        };

        vm.onChangeGmtCategoryGroup = function (dataItem) {
            console.log(dataItem);
            var item = angular.copy(dataItem);
            vm.form.GMT_CAT_GRP_ID = item.INV_ITEM_CAT_ID;
            vm.gmtCategoryDataSource.read();
        };

        vm.onDataBoundGmtCategoryGroup = function (e) {
            var listView = e.sender;
            var firstItem = listView.element.children().first();
            listView.select(firstItem);
        };

        function getGmtCategoryList() {
            return vm.gmtCategoryDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        IeDataService.getDataByFullUrl('/api/inv/ItemCategory/ItemCategList4LastLevel?pPARENT_ID=' + (vm.form.GMT_CAT_GRP_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function setForm(pINV_ITEM_CAT_ID) {
            return IeDataService.getDataByUrl('/GmtItemVariation/FindGmtTechSpecVariationData?pINV_ITEM_CAT_ID=' + pINV_ITEM_CAT_ID).then(function (res) {
                vm.FormData = res;
            });
        }
        
        vm.onChangeGmtCategory = function (dataItem) {
            var item = angular.copy(dataItem);
            vm.form.GMT_CAT_ID = item.INV_ITEM_CAT_ID;
            setForm(item.INV_ITEM_CAT_ID);
        };

        vm.onDataBoundGmtCategory = function (e) {
            var listView = e.sender;
            var firstItem = listView.element.children().first();
            listView.select(firstItem);            
        };
 

        vm.gmtPartOption = {
            height: '400px',
            sortable: false,
            scrollable: true,
            pageable: false,
            editable: false,
            selectable: "row",
            columns: [
                {
                    headerTemplate: "<input type='checkbox' ng-model='vm.isSelect' ng-click='vm.selectAll(vm.isSelect,0)'  ng-true-value='\"Y\"' ng-false-value='\"N\"' > <i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Select?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Select?' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"' >";
                    },
                    width: "3%"
                },
                { field: "GARM_PART_NAME", title: "Part Name", width: "20%" }
            ]
        };
        
               

        vm.save = function (DataOri, isValid) {

           if (!isValid) { return; }

           var Data2Save = [];
           angular.forEach(DataOri, function(val, key) {
                  var q = [];
                  angular.forEach(Object.keys(val),function(vv, kk) {
                       if(!isNaN(vv)) {
                           q.push({
                               CM_PER_DZ: val[vv]['CM_PER_DZ'], GMT_IE_ITM_OQVOL_ID: val[vv]['GMT_IE_ITM_OQVOL_ID'],
                               GMT_IE_ITM_CM_ID: val[vv]['GMT_IE_ITM_CM_ID']
                           });
                       }
                  });

                Data2Save.push({
                    GMT_IE_ITM_VARIATION_ID: val.GMT_IE_ITM_VARIATION_ID,
                    LK_ITEM_GRP_ID: val.LK_ITEM_GRP_ID,
                    INV_ITEM_CAT_ID: val.INV_ITEM_CAT_ID,
                    TECH_SPEC_DESC: val.TECH_SPEC_DESC,
                    REQ_OP: val.REQ_OP,
                    REQ_HLP: val.REQ_HLP,
                    SMV: val.SMV,
                    PICK_PROD_PER_HR: val.PICK_PROD_PER_HR,
                    DAYS_TO_PICK_PROD: val.DAYS_TO_PICK_PROD,
                    GMt_IE_CM_XML: config.xmlStringShortNoTag(q)
                });
           });

            Dialog.confirm('Do you want save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    return IeDataService.saveDataByUrl({
                        INV_ITEM_CAT_ID: vm.form.GMT_CAT_ID,
                        XML: config.xmlStringShort(Data2Save)
                    }, '/GmtItemVariation/Save').then(function (res) {
                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {
                            res['data'] = angular.fromJson(res.jsonStr);
                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                setForm(vm.form.GMT_CAT_ID);
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