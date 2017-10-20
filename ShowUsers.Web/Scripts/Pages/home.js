/**
Home Index
*/

!function($) {
    "use strict";

    var Home = function() {
        this.$dataTable = $('#appuserstbl');
    };

    Home.prototype.loadAppUsers = function() {
        var $this = this;
        var dataTable = this.$dataTable.DataTable({
            "responsive": true,
            "bDestroy": true,
            "iDisplayLength": 10,
            "aaSorting": [[1, "asc"]],
            "bJQueryUI": false,
            "bServerSide": true,
            "bAutoWidth": false,
            "scrollX": false,
            "bLengthChange": false,
            "sAjaxSource": url.getAppUsers,
            "fnServerData": function(sSource, aoData, fnCallback, oSettings) {
                oSettings.jqXHR = $.ajax({
                    beforeSend: function(xhr) {
                        xhr.setRequestHeader("__RequestVerificationToken", $this.$token);
                    },
                    "dataType": 'json',
                    "type": 'POST',
                    "url": sSource,
                    "data": aoData,
                    "success": fnCallback,
                    "error": function(xhr, textStatus, error) {
                        alert(xhr.resonseText);
                    },
                    "complete": function() {

                    },
                });
            },
            "fnServerParams": function(aoData) {
            },
            "bProcessing": false,
            "bFilter": false,
            "sPaginationType": "full_numbers",
            "fnRowCallback": function(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                return nRow;
            },
            "fnPreDrawCallback": function() {
            },
            "fnDrawCallback": function(oSettings) {
                // button to Edit the tt
                $('[data-id="btnstatus"]').on('click', function(e) {
                    var me = $(this);
                    e.preventDefault();
                    var appUserEmail = $(this).attr('data-appuseremail');
                    var appUserStatus = $(this).attr('data-appuserstatus');
                    //alert(appUserId + appUserStatus);
                    $this.updateUserStatus(appUserEmail, appUserStatus);
                });
            },
            "aoColumns": [
                { "sName": "Id", "visible": false, "bSortable": false, "sClass": "dt-body-center" },
                { "sName": "UserName", "bSortable": true, "sClass": "dt-body-center" },
                { "sName": "Password", "bSortable": true, "sClass": "dt-body-center" },
                { "sName": "Email", "bSortable": true, "sClass": "dt-body-center" },
                { "sName": "Gender", "bSortable": true, "sClass": "dt-body-center" },
                { "sName": "Active", "bSortable": true, "sClass": "dt-body-center" },
                {
                    "bSortable": false,
                    "mRender": function(data, type, row) {
                        var res = "";
                        if (row[5] == 'True') {
                            res = '<a href="#" data-id="btnstatus"  style="width:100px" class="btn btn-xs btn-danger waves-effect waves-light m-l-10" data-appuseremail="' + row[3] + '" data-appuserstatus="' + row[5] + '"> Deactivate </a>';
                        } else {
                            res = '<a href="#" data-id="btnstatus" style="width:100px" class="btn btn-xs btn-success waves-effect waves-light m-l-10" data-appuseremail="' + row[3] + '" data-appuserstatus="' + row[5] + '"> Activate </a>';
                        }
                        return res;
                    }
                }
            ]
        });
    },

        Home.prototype.updateUserStatus = function(email, status) {
            var $this = this;
            $.ajax({
                url: url.updateUserStatus,
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ email: email, status: status }),
                cache: false,
                success: function(data, textStatus, jqXHR) {
                    $('#appuserstbl tr:contains("' + email + '") >td').last().prev().html(data.newStatus);
                    if (data.newStatus == 'True') {
                        $('#appuserstbl tr:contains("' + email + '") >td').last().find('a').removeClass('btn-success');
                        $('#appuserstbl tr:contains("' + email + '") >td').last().find('a').addClass('btn-danger');
                        $('#appuserstbl tr:contains("' + email + '") >td').last().find('a').html("Deactivate");
                    }
                    else {
                        $('#appuserstbl tr:contains("' + email + '") >td').last().find('a').removeClass('btn-danger');
                        $('#appuserstbl tr:contains("' + email + '") >td').last().find('a').addClass('btn-success');
                        $('#appuserstbl tr:contains("' + email + '") >td').last().find('a').html("Activate");
                    }
                    $('#appuserstbl tr:contains("' + email + '") >td').last().find('a').attr('data-appuserstatus', data.newStatus);
                },
                error: function(jqXHR, textStatus, errorThrown) {

                },
                complete: function() {

                }
            });
        },

        Home.prototype.start = function() {
            var $this = this;
            $this.loadAppUsers();
        }

    //init
    $.Home = new Home, $.Home.Constructor = Home
}(window.jQuery),

    //initializing
    function($) {
        "use strict";
        $.Home.start();
    }(window.jQuery);