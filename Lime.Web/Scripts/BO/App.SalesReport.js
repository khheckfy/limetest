App.SalesReport = {
    init: function () {
        $('#tbFrom,#tbTo').datepicker();
        $('#btnOK').click(App.SalesReport.createReport);
    },

    ///Создание и отправка отчета
    createReport: function () {
        if (!App.SalesReport.validateForm()) {
            return;
        }
        $(this).attr('disabled', 'disabled');
        $('#btnDownload').hide();
        var postData =
        {
            From: $('#tbFrom').val(),
            To: $('#tbTo').val(),
            Email: $('#tbEmail').val()
        };
        $.ajax({
            url: '/Home/CreateReport',
            method: 'POST',
            data: JSON.stringify(postData),
            contentType: App.contentType,
            dataType: App.dataType,
            async: true,
            success: function (obj) {
                if (obj.error) {
                    alert(obj.error);
                }
                else {
                    $('#fileId').val(obj.fileId);
                }
            },
            error: function (jqXHR, exception) {
            },
            complete: function () {
                $('#btnOK').removeAttr('disabled');
            }
        });
    },

    validateForm: function () {
        var isValid = true;

        $('[required]').each(function (obj) {
            if ($(this).val())
                $(this).parent().removeClass('has-warning');
            else {
                isValid = false;
                $(this)
                    .focus()
                    .parent()
                    .addClass('has-warning');
            }
        });

        if (!App.isValidEmailAddress($('#tbEmail').val())) {
            isValid = false;
            $('#tbEmail')
                   .focus()
                   .parent()
                   .addClass('has-warning');
        }

        return isValid;
    }
}

$(function () {
    App.SalesReport.init();
});