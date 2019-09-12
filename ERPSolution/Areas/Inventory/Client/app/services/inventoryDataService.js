(function () {
    'use strict';
    angular.module('multitex.inventory').factory('InventoryDataService', ['$http', 'exception', 'access_token', InventoryDataService]);

    function InventoryDataService($http, exception, access_token) {
        var self = this;
        var urlBase = '/api/inv';


        var stringOperators = {
            '~eq~': ' LIKE ',
            '~neq~': ' NOT LIKE ',
            '~doesnotcontain~': ' NOT LIKE ',
            '~contains~': ' LIKE ',
            '~startswith~': ' LIKE ',
            '~endswith~': ' LIKE ',
            '~and~': ' AND ',
            '~or~': ' OR '
        };

        self.kFilterStr2Sql = function (str) {
            var kFilterStr2SqlStr = null;
            angular.forEach(stringOperators, function (val, key) {
                kFilterStr2SqlStr = (kFilterStr2SqlStr || str).replace(new RegExp(key, 'g'), val);
            });
            return kFilterStr2SqlStr;
        };

        self.kFilterStr2QueryParam = function (str) {
            var v_str = '';
            if (str.length == 0) {
                return '';
            }
            var obj = str.split('~and~');
            if (obj.length > 0) {
                obj.forEach(function (val, key) {
                    val = val.replace(/'/g, '').replace(/datetime/g, '');
                    v_str += '&p' + val.split('~')[0] + '=' + val.split('~')[2];
                    if (key != obj.length - 1) {
                        v_str += '&'
                    }
                });
            }
            return v_str;
        };

        //Common Functionality
        self.getUserDatalist = function () {
            return $http({
                method: 'get',
                url: '/api/common/SelectAllUserData'
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        self.xmlStringShort = function (data) {
            var xml = '';
            if (angular.isArray(data)) {
                xml += '<trans>';
                angular.forEach(data, function (val, key) {
                    xml += ' <row ';
                    angular.forEach(val, function (Value, Field) {
                        if (angular.isDate(Value)) {
                            Value = $filter('date')(Value, 'dd-MMM-yyyy');
                        }

                        if (Field !== '$$hashKey') {
                            xml += " " + Field + "=\"" + (Value || '') + "\"";
                        }
                    });
                    xml += ' />';
                });
                xml += '</trans>';
            }
            return xml;
        }

        //Fetch External Url that does not support auth bearer.
        self.getDataByUrlNoToken = function (url) {
            return $http({
                method: 'get',
                url: url,
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        //Save
        self.saveItemCategoryData = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/ItemCategory/ItemCategorySave',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        self.batchSaveItemCatPermissionData = function (data, token) {
            return $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: urlBase + '/ItemCategory/BatchSaveItemCatPermission',
                method: 'post',
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        self.saveItemData = function (data, token) {
            return $http({
                headers: { 'Authorization': 'Bearer ' + token },                
                url: urlBase + '/Item/ItemSave',
                method: 'post',
                data: data 
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        self.batchSavePOSItem = function (data, token) {            
            return $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: urlBase + '/Item/BatchSavePOSItem',
                method: 'post',
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        self.saveDataByFullUrl = function (data, url) {
            return $http({
                method: 'post',
                url: url,
                headers: { 'Authorization': 'Bearer ' + access_token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        
        //Update
        self.updateItemCategoryData = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/ItemCategory/ItemCategoryUpdate',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        self.updateItemData = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/Item/ItemUpdate',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };
        
        

        //Misc Select
        self.GetAllOthers = function (url) {
            return $http({
                method: 'get',
                url: url,
                headers: { 'Authorization': 'Bearer ' + access_token }
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getItemCategTreeList = function () {
            //alert(pSC_USER_ID);
            return $http({
                method: 'get',
                url: '/api/inv/ItemCategory/ItemCategTreeList'
            })
            .then(function (result) {                
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getItemCategTreePermissionList = function (pSC_USER_ID) {
            alert(pSC_USER_ID);
            return $http({
                method: 'get',
                url: '/api/inv/ItemCategory/ItemCategTreeList?&pSC_USER_ID=' + pSC_USER_ID
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getLoginUserWiseItemCatTreeList = function () {
            //alert(pSC_USER_ID);
            return $http({
                method: 'get',
                url: '/api/inv/ItemCategory/LoginUserWiseItemCatTreeList'
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getItemCategList = function () {
            return $http({
                method: 'get',
                url: urlBase + '/ItemCategory/ItemCategList'
            })
            .then(function (result) {
                //var data = [];
                //data = result.data;
                //console.log(data);
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getItemList = function (pINV_ITEM_CAT_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/Item/ItemList',
                params: { pINV_ITEM_CAT_ID: pINV_ITEM_CAT_ID }
            })
            .then(function (result) {
                //var data = [];
                //data = result.data;
                //console.log(data);
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getMOUList = function () {
            return $http({
                method: 'get',
                url: '/api/common/MOUList'
            })
            .then(function (result) {               
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getCountryList = function () {
            return $http({
                method: 'get',
                url: '/api/common/GetCountryList'
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getItemBrandList = function () {
            return $http({
                method: 'get',
                url: '/api/common/GetItemBrandList'
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getCategoryWiseBrandList = function (pINV_ITEM_CORE_CAT_ID) {
            return $http({
                method: 'get',
                url: '/api/common/GetCategoryWiseBrandList/' + pINV_ITEM_CORE_CAT_ID
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getCurrencyList = function () {
            return $http({
                method: 'get',
                url: '/api/common/CurrencyList'
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getLookupListData = function (Id) {
            return $http({
                method: 'get',
                url: '/api/common/LookupListData/' + Id,
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getEmpAutoDataList = function (pEMPLOYEE_CODE, pLK_JOB_STATUS_ID, pHR_COMPANY_ID) {            
            return $http({
                method: 'get',
                url: '/api/hr/EmpAutoDataList/' + pEMPLOYEE_CODE + '/' + pLK_JOB_STATUS_ID + '/' + pHR_COMPANY_ID
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getEmpPOSVerify = function (pHR_EMPLOYEE_ID, pMEMO_DT) {           
            return $http({
                method: 'get',
                url: urlBase + '/Item/EmpPOSVerify/' + pHR_EMPLOYEE_ID + '/' + pMEMO_DT
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.getItemAutoDataList = function (pITEM_CODE) {            
            return $http({
                method: 'get',
                url: urlBase + '/Item/ItemAutoDataList/' + pITEM_CODE
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };
        
        self.LookupListData = function (ID) {
            return $http({
                method: 'get',
                url: '/api/common/LookupListData/' + ID
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getDataByUrl = function (url) {
            return $http({
                method: 'get',
                url: urlBase + url,
                headers: { 'Authorization': 'Bearer ' + access_token },
            }).then(function (result) {
                return result.data;
            }).catch(function (Message) {
                console.error(Message);
                exception.catcher('XHR loading Failded')(Message);
            });
        }

        self.getDataByFullUrl = function (url) {
            return $http({
                method: 'get',
                url: url,
                headers: { 'Authorization': 'Bearer ' + access_token }
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        //Save Data with Url
        self.saveDataByUrl = function (data, url) {
            return $http({
                method: 'post',
                url: urlBase + url,
                headers: { 'Authorization': 'Bearer ' + access_token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        //Save Data with Url
        self.saveDataByFullUrl = function (data, url) {
            return $http({
                method: 'post',
                url: url,
                headers: { 'Authorization': 'Bearer ' + access_token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        return self;
    }


})();