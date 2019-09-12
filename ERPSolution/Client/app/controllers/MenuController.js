(function () {
    'use strict';

    angular.module('multitex').controller('ActiveLinkController', ['logger', 'config', '$scope', '$q', '$sessionStorage', 'Hub', '$state', '$rootScope', '$window', 'DashBoardService', '$timeout', '$ngConfirm', ActiveLinkController]);

    function ActiveLinkController(logger, config, $scope, $q, $sessionStorage, Hub, $state, $rootScope, $window, DashBoardService, $timeout, $ngConfirm) {

        var vm = this;

        vm.IS_SESSION = 0;
        vm.today = new Date();
        activate();
        showMessageLoading();
        showBatchListMessage();
        function activate() {
        }
        var timer2 = $timeout(function refresh() {
            showBatchListMessage();
            timer2 = $timeout(refresh, 30000);
        }, 30000);


        var timer3 = $timeout(function refresh() {
            checkLogin();
            timer3 = $timeout(refresh, 10000);
        }, 10000);

        $scope.dog = "onfocus";
        var onFocus = function () {
            $scope.dog = 'onfocus';
            $scope.$apply();
        }
        var onBlur = function () {
            $scope.dog = 'onblur';
            $scope.$apply();
        }
        $window.onfocus = onFocus;
        $window.onblur = onBlur;

        function checkLogin() {
            console.log($scope.dog);
            if (vm.IS_SESSION == 0 && $scope.dog != "onblur")
                DashBoardService.getDataByFullUrl('/api/security/GlobalNotify/CheckUserSession').then(function (res) {
                    if (!res) {
                        config.appToastMsg("MULTI-005 Server Connection Lost!!!");
                    }
                    else {
                        if (res.SC_USER_ID == 0) {
                            config.appToastMsg("MULTI-005 Session Out!!!");
                            $scope.dog = "fred";
                            vm.IS_SESSION = 1;
                            $ngConfirm({
                                title: 'Prompt for Password',
                                contentUrl: 'PasswordPrompt.html',
                                buttons: {
                                    getPassCode: {
                                        text: 'Login',
                                        disabled: true,
                                        btnClass: 'btn-green',
                                        action: function (scope) {

                                            DashBoardService.saveDataByFullUrl({
                                                LOGIN_ID: scope.userName,
                                                PASSWORD_HASH: scope.passCode,
                                                CAPTCHA_VALUE: 10
                                            }, '/Security/ScUser/PromptSignIn').then(function (res) {
                                                console.log(res);
                                                if (res == "Success") {
                                                    vm.IS_SESSION = 0;
                                                    config.appToastMsg("MULTI-001 Login Success");
                                                    return
                                                }
                                                else {
                                                    vm.IS_SESSION = 0;
                                                    config.appToastMsg("MULTI-005 Worng Input!!!");
                                                }
                                            }).catch(function (message) {
                                                vm.IS_SESSION = 0;
                                                exception.catcher('XHR loading Failded')(message);
                                            });
                                        }
                                    },
                                    later: function () {
                                        vm.IS_SESSION = 0;
                                        return;
                                    }
                                },
                                onScopeReady: function (scope) {
                                    var self = this;
                                    scope.showPassword = false;

                                    scope.toggleShowPassword = function () {
                                        scope.showPassword = !scope.showPassword;
                                    }

                                    scope.textChange = function () {
                                        if (scope.passCode.length > 0)
                                            self.buttons.getPassCode.setDisabled(false);
                                        else
                                            self.buttons.getPassCode.setDisabled(true);
                                    }
                                }
                            });
                        }
                    }

                });
        }

        function showBatchListMessage() {

            if (vm.IS_SESSION == 0) {
                var data = {
                    alertType: 'info',
                    title: 'নিম্নলিখিত ব্যাচগুলির বিপরীতে ইতিমধ্যে ডাইজ/কেমিক্যাল রিকুইজিশন ইস্যু সম্পন্ন হয়েছে তবে আপনি প্রোডাকশন লোড করেন নি। সুতরাং, এই ব্যাচগুলি উল্লিখিত সময়ের পরে প্রোডাকশন লোড নিষ্ক্রিয় হবে/হয়েছে।',
                    vMsg: 'Hi I am Afzal multiServerCurrDate',
                };

                DashBoardService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchWaitingList').then(function (res) {
                    if (res)
                        if (res.length > 0) {
                            $("#alert-placeholder").empty();
                            var alertHtml = '<div class="alert alert-' + htmlEncode(data.alertType) + ' " style="color:red; opacity: 0.95;" alert-dismissible" role="alert"> <i class="fa fa-' + htmlEncode(data.alertType) + '" ></i> <button type="button" class="close" data-dismiss="alert"><span>×</span></button><strong>' +
                                htmlEncode(data.title) + '</strong>';
                            var pList = _.filter(res, function (x) { return x.IS_LOCKED == 'N' });
                            alertHtml = alertHtml + '<div><table style="width:100%">';
                            if (pList.length > 0) {
                                alertHtml = alertHtml + '<tr><td style="width:30%;border: 1px solid black;"><b>Pending Batches</b></td><td style="width:70%;border: 1px solid black;">';
                                alertHtml = alertHtml + '<table style="width:100%"><thead><tr style="border: 1px solid black;"><th style="width:50%;border: 1px solid black;">Batch No</th><th style="width:50%;border: 1px solid black;">Time</th></tr></thead><tbody>';
                                for (var i = 0; i < pList.length; i++) {
                                    alertHtml = alertHtml + '<tr style="border: 1px solid black; text-align:left;"><td style="border: 1px solid black;">' + htmlEncode(pList[i].DYE_BATCH_NO + " (" + pList[i].BATCH_QTY + " Kg)") + '</td><td style="border: 1px solid black;">' + htmlEncode(pList[i].BATCH_LOCK_TIME) + "</td>";
                                    //alertHtml = alertHtml + '<tr><td>' + htmlEncode(res[i].DYE_BATCH_NO + " (" + res[i].BATCH_QTY + " Kg) </td>" + (res[i].IS_LOCKED == 'Y' ? "is locked from " : "will Lock at ") + res[i].BATCH_LOCK_TIME)
                                    alertHtml = alertHtml + '</tr>';
                                }
                                alertHtml = alertHtml + '</tbody></table>';
                                alertHtml = alertHtml + '</td></tr>';
                            }

                            var cList = _.filter(res, function (x) { return x.IS_LOCKED == 'Y' });
                            if (cList.length > 0) {
                                alertHtml = alertHtml + '<tr><td style="width:30%;border: 1px solid black;"><b>Locked Batches</b></td><td style="width:70%;border: 1px solid black;">';
                                alertHtml = alertHtml + '<table style="width:100%"><thead><tr style="border: 1px solid black;"><th style="width:50%;border: 1px solid black;">Batch No</th><th style="width:50%;border: 1px solid black;">Time</th></tr></thead><tbody>';
                                for (var i = 0; i < cList.length; i++) {
                                    alertHtml = alertHtml + '<tr style="border: 1px solid black; text-align:left;"><td style="border: 1px solid black;">' + htmlEncode(cList[i].DYE_BATCH_NO + " (" + cList[i].BATCH_QTY + " Kg)") + '</td><td style="border: 1px solid black;">' + htmlEncode(cList[i].BATCH_LOCK_TIME) + "</td>";
                                    alertHtml = alertHtml + '</tr>';
                                }
                                alertHtml = alertHtml + '</tbody></table>';
                                alertHtml = alertHtml + '</td></tr>';
                            }

                            alertHtml = alertHtml + '</table></div></div>';

                            $(alertHtml)
                              .hide()                           //hide the newly created element (this is required for fadeIn to work)
                              .appendTo('#alert-placeholder')   //add it to the palceholder in the page
                              .fadeIn(1500);                      //little flair to grab user attention
                          
                            //setTimeout(function () {
                            //    $("#alert-placeholder").empty();
                            //}, 10000);
                        }
                });
            }
        }



        function toastrConfig() {

            //vm.toastrNotify = angular.copy(toastr);

            //vm.toastrNotify.options = {
            //    "closeButton": true,
            //    "debug": false,
            //    "positionClass": "toast-bottom-full-width",
            //    "onclick": null,
            //    "progressBar": true,
            //    "preventDuplicates": true,
            //    "showDuration": "300",
            //    "hideDuration": "1000",
            //    "timeOut": "600000",
            //    "extendedTimeOut": "600000",
            //    "showEasing": "linear",
            //    "hideEasing": "linear",
            //    "showMethod": "fadeIn",
            //    "hideMethod": "fadeOut"
            //}

            //console.log(vm.toastrNotify);
        }

        var hubUrl = "/signalr";
        var connection = $.hubConnection(hubUrl, { useDefaultPath: true });
        var hub = connection.createHubProxy("dashboard");
        var hubStart = connection.start();

        vm.connected = [];

        var hub = new Hub('dashboard', {
            listeners: {
                'GlobalNotificationMsg': function (a, b, c) {
                    showMessage(a, b, c);
                    $rootScope.$apply()
                },
                'newConnection': function (id) {
                    vm.connected.push(id);
                    $rootScope.$apply();
                },
                'removeConnection': function (id) {
                    $rootScope.$apply();
                }

            },
            methods: [],
            errorHandler: function (error) {
                console.error(error);
            }
        });

        function htmlEncode(value) {
            return $('<div />').text(value).html();
        }

        function showMessage(alertType, title, vMsg) {

            var currentDate = new Date();
            var msgTime = new Date(currentDate.getTime() + (10 * 60 * 1000));

            var arrayMsg = {
                alertType: alertType,
                title: title,
                vMsg: vMsg,
                msgTimeOut: angular.copy(msgTime)
            };

            if (sessionStorage.NotificationMsg)
                sessionStorage.NotificationMsg.clear();

            $sessionStorage.NotificationMsg = angular.copy(arrayMsg);

            $("#alert-placeholder").empty();
            var alertHtml = '<div class="alert alert-' + htmlEncode(alertType) + ' " style="color:red; opacity: 0.95;" alert-dismissible" role="alert"> <i class="fa fa-' + htmlEncode(alertType) + '" ></i> <button type="button" class="close" data-dismiss="alert"><span>×</span></button><strong>' + htmlEncode(title) + '</strong> <br />' + htmlEncode(vMsg) + ' </div>';

            $(alertHtml)
              .hide()                           //hide the newly created element (this is required for fadeIn to work)
              .appendTo('#alert-placeholder')   //add it to the palceholder in the page
              .fadeIn(1500);                      //little flair to grab user attention

            setTimeout(function () {
                $("#alert-placeholder").empty();
            }, 600000);
        }



        function showMessageLoading() {
            DashBoardService.getDataByUrl('/api/security/GlobalNotify/GetMsg').then(function (data) {
                if (data.page > 0) {
                    if (Date.parse(data.msgTimeOut) > Date.parse(vm.today)) {
                        $("#alert-placeholder").empty();
                        var alertHtml = '<div class="alert alert-' + htmlEncode(data.alertType) + ' " style="color:red; opacity: 0.95;" alert-dismissible" role="alert"> <i class="fa fa-' + htmlEncode(data.alertType) + '" ></i> <button type="button" class="close" data-dismiss="alert"><span>×</span></button><strong>' + htmlEncode(data.title) + '</strong> <br />' + htmlEncode(data.vMsg) + '</div>';

                        $(alertHtml)
                          .hide()                           //hide the newly created element (this is required for fadeIn to work)
                          .appendTo('#alert-placeholder')   //add it to the palceholder in the page
                          .fadeIn(1500);                      //little flair to grab user attention

                        setTimeout(function () {
                            $("#alert-placeholder").empty();
                        }, 600000);
                    }
                }
                else {
                    $window.location.href = '/Security/Security/serverMaintenance/#/serverMaintenance';
                    //var url = '/Security/Security/serverMaintenance/#/serverMaintenance';
                    //var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 300) + ',scrollbars=yes,status=yes';
                    //$window.open(url, "_blank", opt);
                    //$state.go("#/serverMaintenance", { reload: true });
                }
            });
        }



        vm.printKnitCard = function (item) {

        }

        function activate() {
            var promise = [getCurrentLevels()];
            return $q.all(promise).then(function () {
            });
        }
        //vm.datas = $localStorage.obj;

        function getCurrentLevels() {
            $scope.levels = $sessionStorage.levels;
        }

        //MultitexSocket.on('error', function (ev, data) {
        //    console.log(data);
        //});    

        //MultitexSocket.on('socket:someEvent', function (data) {
        //    console.log(data);
        //    console.log($scope);
        //});



        //MenuDataService.getMenuData().then(function (res) {
        //    vm.datas = res;
        //    $localStorage.obj = res;
        //    return vm.datas;
        //});
        vm.clicked = function (level1, level2, level3) {


            var levels = [level1, level2, level3];
            console.log(levels);
            delete $sessionStorage.levels;
            $sessionStorage.levels = levels;
            //$scope.$apply();
        }


    }



})();