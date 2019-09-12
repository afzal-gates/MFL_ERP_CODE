(function () {
    'use strict';
    angular.module('multitex.mrc').controller('ColourMasterModalController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', '$modal', 'ColourList', '$modalInstance', ColourMasterModalController]);
    function ColourMasterModalController($q, config, MrcDataService, $stateParams, $state, $scope, logger, $modal, ColourList, $modalInstance) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'ColourMaster';
        var key = 'MC_COLOR_ID';
        
        vm.AopBaseColName = '';
        vm.colTypeName = '';
        vm.Title = $state.current.Title || '';

        vm.ColourList = _.filter(ColourList, { 'LK_COL_TYPE_ID': 358 }).map(function (obj) {
                return {
                    MC_COLOR_ID: obj.MC_COLOR_ID,
                    COLOR_NAME_EN: obj.COLOR_NAME_EN
                }
        });

  
        vm.form = { IS_ACTIVE: 'Y', IS_SWATCH: 'N', PNT_VERSION_NO: 1 };


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getColourTypelist(), getAllColorListForDropDown()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
      
            });
        }

        $scope.$watchGroup(['vm.form.LK_COL_TYPE_ID', 'vm.form.AOP_BASE_COL_ID', 'vm.form.YD_COL_LST','vm.form.MC_COLOR_ID'], function (newVal, oldVal) {

        if(!newVal[3]){
                if (newVal[0] && parseInt(newVal[0]) == 361) {
                    vm.form['COLOR_NAME_EN'] = vm.AopBaseColName + ' AOP';
                    vm.form['YD_COL_LST'] = '';

                } else if (newVal[0] && parseInt(newVal[0]) == 360) {
                    vm.form['COLOR_NAME_EN'] = _.map(newVal[2], 'COLOR_NAME_EN').join(' / ')+' (Y/D)';
                    vm.form['AOP_BASE_COL_ID'] = '';
                    vm.form['AOP_PRNT_COL_LST'] = '';
                } else if (newVal[0] && parseInt(newVal[0]) == 432) {
                    vm.form['COLOR_NAME_EN'] = _.map(newVal[2], 'COLOR_NAME_EN').join(' / ') + ' (Mixed)';
                    vm.form['AOP_BASE_COL_ID'] = '';
                    vm.form['AOP_PRNT_COL_LST'] = '';

                }else {
                    vm.form['COLOR_NAME_EN'] = '';
                    vm.form['AOP_BASE_COL_ID'] = '';
                    vm.form['YD_COL_LST'] = '';
                    vm.form['AOP_PRNT_COL_LST'] = '';

                }
          }
             
        });

        function getAllColorListForDropDown() {
         return   vm.AllColourlist = {
                optionLabel: "-- Colour--",
                filter: "startswith",
                autoBind: false,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(vm.ColourList)
                        }
                    }
                },

                select:function(e){
                    vm.AopBaseColName = this.dataItem(e.item).COLOR_NAME_EN;
                },
                dataBound:function(e){
                    vm.AopBaseColName = this.dataItem(e.item).COLOR_NAME_EN;
                },
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID"
            };
        }
        function getColourTypelist() {
            return vm.ColourTypelist = {
                optionLabel: "-- Colour Type--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(74).then(function (res) {
                                console.log(res);
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataBound:function(e){
                    var data = this.dataItem(e.item);
                    vm.colTypeName = data.LK_DATA_NAME_EN;
                },
                select:function(e){
                    var data = this.dataItem(e.item);
                    vm.colTypeName = data.LK_DATA_NAME_EN;

                    $scope.ColourMasterForm.LK_COL_TYPE_ID.$setValidity('min', true);
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };


        vm.ColourGroupList = {
            optionLabel: "-- Colour Group--",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return MrcDataService.getDataByUrl('/ColourMaster/ColourGroupList').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        })

                    }
                }
            },
            select: function (e) {

                var item = this.dataItem(e.item);

                if (item.MC_COLOR_GRP_ID == 1) {
                    vm.form.LK_DYE_MTHD_ID = 5;
                } else if (item.MC_COLOR_GRP_ID == 4) {
                    vm.form.LK_DYE_MTHD_ID = 6;
                } else if (item.MC_COLOR_GRP_ID == 10) {
                    item.LK_DYE_MTHD_ID = 7;
                } else {
                    vm.form.LK_DYE_MTHD_ID = 1;
                }

                vm.form['COLOR_GRP_NAME_EN'] = item.COLOR_GRP_NAME_EN;
            },
            dataTextField: "COLOR_GRP_NAME_EN",
            dataValueField: "MC_COLOR_GRP_ID"
        };



        vm.loadDatatoForm = function (MC_COLOR_ID, COLOR_REF) {
            return MrcDataService.selectData('ColourMaster', MC_COLOR_ID).then(function (data) {
                data['YD_COL_LST'] = data.YD_COL_LST ? MrcDataService.parseXmlString(data.YD_COL_LST) : [];
                data['AOP_PRNT_COL_LST'] = data.AOP_PRNT_COL_LST ? MrcDataService.parseXmlString(data.AOP_PRNT_COL_LST) : [];
                data['COLOR_REF'] = COLOR_REF;
                vm.form = data;

            }, function (err) {
                console.log(err);
            })
        }

        vm.submitData = function (dataOri, token) {

            var text = ["YD", "Y/D"]
            var str = dataOri.COLOR_NAME_EN.toUpperCase();
            console.log(str);
            
            var result;
            for (var x = 0; x < text.length; x++) {
                if (str.indexOf(text[x]) > -1) {
                    result = text[x];
                    break;
                }
            }

            if ((result == "YD" || result == "Y/D") && dataOri.LK_COL_TYPE_ID != 360) {
                $scope.ColourMasterForm.LK_COL_TYPE_ID.$setValidity('min', false);
               
                return;
            }
            
            

            var data = angular.copy(dataOri);
            
            data['AOP_PRNT_COL_LST'] = MrcDataService.xmlStringLong(data.AOP_PRNT_COL_LST);
            data['YD_COL_LST'] = MrcDataService.xmlStringLong(data.YD_COL_LST);

            return MrcDataService.saveData(data, ctrl, token).then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                   
                    if (res.data.V_MC_COLOR_ID != 0 && res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        data['MC_COLOR_ID'] = parseInt(res.data.V_MC_COLOR_ID);
                        $modalInstance.close(data);
                    }

                    config.appToastMsg(res.data.PMSG);

                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        $scope.cancel = function (data) {
            data['COL_TYPE_NAME'] = vm.colTypeName;
            
            $modalInstance.close(data);
        };


    }

})();