toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-full-width",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

function showAlert(alert, alertDiv) {

    if (!alert)
        return;

    if (alert.isAutoHide) {

        switch (alert.alertType) {
            case 1: //Warning

                toastr.warning(alert.message);
                break;

            case 2: //Info

                toastr.info(alert.message);
                break;

            case 3: //Success

                toastr.success(alert.message);
                break;

            case 4: //Error

                toastr.error(alert.message);
                break;

            case 5: //Hide
                break;

            default:
        }
    }
    else {

        var option = {
            container: "",
            message: alert.message,
            close: alert.close,
            type: "success",
            icon: "",
        };

        if (alertDiv)
            option.container = alertDiv;

        switch (alert.alertType) {
            case 1: //Warning

                option.icon = "flaticon-warning";
                option.type = "warning";

                break;

            case 2: //Info

                option.icon = "question-circle";
                option.type = "info";

                break;

            case 3: //Success

                option.icon = "la la-check";
                option.type = "success";

                break;

            case 4: //Error
                option.icon = "fa fa-exclamation-triangle";
                option.type = "danger";

                break;

            case 5: //Hide
                break;

            default:
        }

        setAlert(option);
    }
}


function setAlert(options) {

    options = $.extend(true, {
        container: "", // alerts parent container(by default placed after the page breadcrumbs)
        place: "append", // "append" or "prepend" in container 
        type: 'success', // alert's type
        message: "", // alert's message
        reset: true, // close all previouse alerts first
        focus: true, // auto scroll to the alert after shown
        closeInSeconds: 0, // auto close after defined seconds
        icon: "" // put icon before the message
    }, options);

    var html = '<div class="alert alert-' + options.type + (options.close ? ' alert-dismissible' : '') + ' fade show" role="alert" ">' +
        (options.icon !== "" ? '<i class="' + options.icon + ' "></i> ' : '') +
         options.message  +
        (options.close ? '<button type="button" class="close" data-dismiss="alert" aria-label="Close"> <span aria-hidden="true">&times;</span></button>' : '');

    if (!options.container) {

        $('#alert').html(html);

    }
    else {

        $(options.container).html(html);
    }

    if (options.closeInSeconds > 0) {
        setTimeout(function () {
            $('#' + id).remove();
        }, options.closeInSeconds * 1000);
    }

}
