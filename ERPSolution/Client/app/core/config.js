(function () {
    'use strict';

    var core = angular.module('multitex.core');
    core.config(toastrConfig);

    /* @ngInject */
    function toastrConfig(toastr) {
        toastr.options.timeOut = 4000;
        toastr.options.positionClass = 'toast-bottom-right';
    }
    
    var config = {
        appErrorPrefix: '[MultiTex Error] ', //Configure the exceptionHandler decorator
        appTitle: 'MultiTex',
        //imageBasePath: '/images/photos/',
        //unknownPersonImageSource: 'unknown_person.jpg',

        appDateFormat: 'dd-MMM-yyyy',
        appDateTimeFormat: 'dd-MMM-yyyy HH:mm:ss',
        appTimeFormat: 'HH:mm',
        appExRateUsdBdtApi: 'https://www.exchangerate-api.com/USD/BDT/1?k=8abba360d9d7e7a974e2fb83',
        appExRateBdtUsdApi: 'https://www.exchangerate-api.com/BDT/USD/1?k=8abba360d9d7e7a974e2fb83',


        appToastMsg: function (vMsg) {
                       
            //console.log(vMsg);
            if (vMsg != '') {

                var vMsgOrg = vMsg;
                var vMsgPrefix = vMsg.substr(0, 9);
                var vMsg = vMsg.substr(9);

                if (vMsgPrefix == 'MULTI-001') {
                    toastr.success(vMsg, "MultiTEX");
                    //vMsgSuccess = true;
                }
                else if (vMsgPrefix == 'MULTI-002') {
                    toastr.info(vMsg, "MultiTEX");
                }
                else if (vMsgPrefix == 'MULTI-003') {
                    toastr.warning(vMsg, "MultiTEX");
                }
                else if (vMsgPrefix == 'MULTI-005') {
                    toastr.error(vMsg, "MultiTEX");
                }
                else
                {
                    toastr.info(vMsgOrg, "MultiTEX");
                }

            }
        },

        ToastInfoMsg: function (vMsg) {
            toastr.info(vMsg, "MultiTEX");
        },
        ToastErrorMsg: function (vMsg) {
            toastr.info(vMsg, "MultiTEX");
        },
        ToastWarningMsg: function (vMsg) {
            toastr.info(vMsg, "MultiTEX");
        },

        kFilterStr2QueryParam : function (str) {
            var v_str = '';
            if (str.length == 0) {
                return '';
            }
            var obj = str.split('~and~');
            if (obj.length > 0) {
                obj.forEach(function (val, key) {

                    if (val.search('datetime') < 0) {
                        val = val.replace(/'/g, '').replace(/datetime/g, '');
                        v_str += '&p' + val.split('~')[0] + '=' + val.split('~')[2];
                        if (key != obj.length - 1) {
                            v_str += '&'
                        }
                    } else {
                        val = val.replace(/'/g, '').replace(/datetime/g, '');
                        v_str += '&p' + val.split('~')[0] + '=' + moment(val.split('~')[2], "YYYY-MM-DD").format("DD-MMM-YYYY");
                        if (key != obj.length - 1) {
                            v_str += '&'
                        }
                    }
                });
            }
            return v_str;
        },
        xmlStringShort : function (data) {
        var xml = '';
        if (angular.isArray(data)) {
            xml += '<trans>';
            angular.forEach(data, function (val, key) {
                xml += ' <row ';
                angular.forEach(val, function (Value, Field) {
                    if (Field !== '$$hashKey') {
                        xml += " " + Field + "=\"" + (_.isNil(Value) ? '' : Value )+ "\"";
                    }
                });
                xml += ' />';
            });
            xml += '</trans>';
        }
        return xml;
    },

     xmlStringShortNoTag : function (data) {
        var xml = '';
        if (data.length > 0 && angular.isArray(data)) {
            xml += '#trans$';
            angular.forEach(data, function (val, key) {
                xml += '#row ';
                angular.forEach(val, function (Value, Field) {
                    if (Field !== '$$hashKey') {
                        xml += " " + Field + "=*" + (_.isNil(Value) ? '' : Value) + "*";
                    }
                });
                xml += '/$';
            });
            xml += '#/trans$';
        }
        return xml;
    },

    xmlStringShortNoTagChild : function (data) {
        var xml = '';
        if (data.length > 0 && angular.isArray(data)) {
            xml += '%trans@';
            angular.forEach(data, function (val, key) {
                xml += '%row';
                angular.forEach(val, function (Value, Field) {
                    if (Field !== '$$hashKey') {
                        xml += " " + Field + "=~" + (_.isNil(Value) ? '' : Value) + "~";
                    }
                });
                xml += '/@';
            });
            xml += '%/trans@';
        }
        return xml;
    },
    xmlStringLong : function (data) {
        var xml = '';
        if (angular.isArray(data) && data.length > 0) {
            xml += '<trans>';
            angular.forEach(data, function (val, key) {
                xml += '<row> ';
                angular.forEach(val, function (Value, Field) {
                    if (Field != '$$hashKey') {
                        xml += "<" + Field + ">" + (_.isNil(Value) ? '' : Value) + "</" + Field + ">"
                    }
                });
                xml += '</row>';
            });
            xml += '</trans>';
        }
        return xml;
    }        
    };


    


    core.value('config', config);

    core.config(configure);

    configure.$inject = ['$compileProvider', '$logProvider',
          'exceptionHandlerProvider', 'routerHelperProvider'];
    /* @ngInject */
    function configure($compileProvider, $logProvider,
          exceptionHandlerProvider, routerHelperProvider) {

      

        $compileProvider.debugInfoEnabled(false);

        // turn debugging off/on (no info or warn)
        if ($logProvider.debugEnabled) {
            $logProvider.debugEnabled(true);
        }
        exceptionHandlerProvider.configure(config.appErrorPrefix);
        configureStateHelper();

        ////////////////

        function configureStateHelper() {
            var resolveAlways = { /* @ngInject */
                //ready: function (dataservice) {
                //    return dataservice.ready();
                //}
              
            };

            routerHelperProvider.configure({
                docTitle: 'Gulp: ',
                resolveAlways: resolveAlways
            });
        }
    }
})();


//(function() {
//    'use strict';

//    var core = angular.module('multitex.core');

//    core.config(toastrConfig);

//    /* @ngInject */
//    function toastrConfig(toastr) {
//        toastr.options.timeOut = 4000;
//        toastr.options.positionClass = 'toast-bottom-right';
//    }

//    var config = {
//        appErrorPrefix: '[multitex ERP] ', //Configure the exceptionHandler decorator
//        appTitle: 'MultiTex ERP',
//        version: '1.0.0',

//        appDateFormat: 'dd-MMM-yyyy'
//    };
    

//    core.value('config', config);

//    core.config(configure);

//    /* @ngInject */
       
//    function configure ($logProvider, $routeProvider, routehelperConfigProvider, exceptionHandlerProvider) {
//        // turn debugging off/on (no info or warn)
//        if ($logProvider.debugEnabled) {
//            $logProvider.debugEnabled(true);
//        }

//        // Configure the common route provider
//        routehelperConfigProvider.config.$routeProvider = $routeProvider;
//        routehelperConfigProvider.config.docTitle = 'MultiTex ERP: ';
//        //var resolveAlways = { /* @ngInject */
//        //    ready: function(dataservice) {
//        //        return dataservice.ready();
//        //    }
//            // ready: ['dataservice', function (dataservice) {
//            //    return dataservice.ready();
//            // }]
//        //};
//        //routehelperConfigProvider.config.resolveAlways = resolveAlways;

//        // Configure the common exception handler
//        exceptionHandlerProvider.configure(config.appErrorPrefix);
//    }
//})();
