App = {
    version: '1.0.00',
    contentType: 'application/json; charset=utf-8',
    dataType: 'json',

    init: function () {
        App.initiControls();
    },

    initiControls: function () {
        if (typeof NProgress != 'undefined') {
            $(document).ajaxStart(function () {
                NProgress.set(0.4);
                NProgress.start();
            }).ajaxStop(function () {
                NProgress.done();
            });
        }
        $('#btnDownload')
            .hide()
            .click(App.download);
        $('#tbFrom,#tbTo').datepicker({ dateFormat: 'dd.mm.yy' });
        $('#btnOK').click(App.createReport);
    },

    download: function () {
        var id = $(this).attr('fileId');
    },

    createReport: function () {
        $(this).attr('disabled', 'disabled');
        $('#btnDownload').hide();
        var postData =
        {
            from: $('#tbFrom').val(),
            to: $('#tbTo').val()
        };
        $.ajax({
            url: '/Home/CreateReport',
            method: 'POST',
            data: JSON.stringify(postData),
            contentType: App.contentType,
            dataType: App.dataType,
            async: true,
            success: function (obj) {

                $('#btnDownload')
                    .attr('fileId', obj)
                    .show();
            },
            error: function (jqXHR, exception) {
            },
            complete: function () {
                $('#btnOK').removeAttr('disabled');
            }
        });
    }
}

$(function () {
    App.init();
}); ''