(function () {
    'use strict';
    angular.module('multitex.mrc').controller('ColourMasterController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', '$modal', 'ColourList', ColourMasterController]);
    function ColourMasterController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData, $modal, ColourList) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'ColourMaster';
        var key = 'MC_COLOR_ID';
        
        vm.AopBaseColName ='';
        vm.Title = $state.current.Title || '';

        vm.ColourList = _.filter(ColourList, { 'LK_COL_TYPE_ID': 358 }).map(function (obj) {
            return {
                MC_COLOR_ID: obj.MC_COLOR_ID,
                COLOR_NAME_EN: obj.COLOR_NAME_EN
            }
        });


        vm.form = formData[key] ? formData : { IS_ACTIVE: 'Y', IS_SWATCH: 'N', PNT_VERSION_NO: 1 };


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getColourTypelist(), getAllColorListForDropDown()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
      
            });
        }


        if (formData[key]) {
            formData['YD_COL_LST'] = formData.YD_COL_LST ? MrcDataService.parseXmlString(formData.YD_COL_LST) : [];
        }

        if (formData[key]) {
            formData['AOP_PRNT_COL_LST'] = formData.AOP_PRNT_COL_LST ? MrcDataService.parseXmlString(formData.AOP_PRNT_COL_LST) : [];
        }

        $scope.$watchGroup(['vm.form.LK_COL_TYPE_ID', 'vm.form.AOP_BASE_COL_ID', 'vm.form.YD_COL_LST', 'vm.form.MC_COLOR_ID'], function (newVal, oldVal) {

            if (!newVal[3]) {

                if (newVal[0] && parseInt(newVal[0]) == 361) {
                    vm.form['COLOR_NAME_EN'] = vm.AopBaseColName + ' AOP';
                    vm.form['YD_COL_LST'] = '';

                } else if (newVal[0] && parseInt(newVal[0]) == 360) {
                    vm.form['COLOR_NAME_EN'] = _.map(newVal[2], 'COLOR_NAME_EN').join(' / ') + ' (Y/D)';
                    vm.form['AOP_BASE_COL_ID'] = '';
                    vm.form['AOP_PRNT_COL_LST'] = '';


                } else if (newVal[0] && parseInt(newVal[0]) == 432) {
                vm.form['COLOR_NAME_EN'] = _.map(newVal[2], 'COLOR_NAME_EN').join(' / ') + ' (Mixed)';
                vm.form['AOP_BASE_COL_ID'] = '';
                vm.form['AOP_PRNT_COL_LST'] = '';
               }
               else {
                    vm.form['COLOR_NAME_EN'] = '';
                    vm.form['AOP_BASE_COL_ID'] = '';
                    vm.form['YD_COL_LST'] = '';
                    vm.form['AOP_PRNT_COL_LST'] = '';
              }
            }
        });

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
            dataTextField: "COLOR_GRP_NAME_EN",
            dataValueField: "MC_COLOR_GRP_ID"
        };



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
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var data = this.dataItem(e.item);
                    
                    $scope.ColourMasterForm.LK_COL_TYPE_ID.$setValidity('min', true);
                },
            };
        };

        

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

            if (angular.isDefined(data[key]) && data[key] > 0) {

                return MrcDataService.updateData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                            data['YD_COL_LST'] = data.YD_COL_LST ? MrcDataService.parseXmlString(data.YD_COL_LST) : [];
                            data['AOP_PRNT_COL_LST'] = data.AOP_PRNT_COL_LST ? MrcDataService.parseXmlString(data.AOP_PRNT_COL_LST) : [];
                          
                            vm.form = data;

                        }
                        config.appToastMsg(res.data.PMSG);
                        
                        
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            } else {

                return MrcDataService.saveData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        
                        if (res.data.V_MC_COLOR_ID != 0 && res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $state.go($state.current, { MC_COLOR_ID: res.data.V_MC_COLOR_ID });
                        }

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }

        vm.edit = function (data) {

            data['YD_COL_LST'] = data.YD_COL_LST ? MrcDataService.parseXmlString(data.YD_COL_LST) : [];
            data['AOP_PRNT_COL_LST'] = data.AOP_PRNT_COL_LST ? MrcDataService.parseXmlString(data.AOP_PRNT_COL_LST) : [];
            vm.form = data;
        };

        vm.gridColumns = [
            
                { field: "COL_TYPE_NAME", title: "Colour Type", type: "string", width: "80px" },
                { field: "COLOR_CODE", title: "Colour Code", type: "string", width: "80px" },
                { field: "COLOR_NAME_EN", title: "Colour Name", type: "string", width: "150px" },
                //{ field: "COLOR_SNAME", title: "Colour Name(Short)", type: "string", width: "90px" },
                { field: "PANTON_NO", title: "Panton #", type: "string", width: "60px" },
                { field: "PNT_VERSION_NO", title: "Version #", type: "string", width: "60px" },
                {
                    title: "Action",
                    template: function () {
                        return "</a><a ng-click='vm.edit(dataItem)' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a>";
                    },
                    width: "50px"
                }
        ];

        //vm.dataSource = new kendo.data.ObservableArray(ColourList);

        vm.dataSource = new kendo.data.DataSource({
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
            data: ColourList,
            pageSize: 10
        });



    }

})();