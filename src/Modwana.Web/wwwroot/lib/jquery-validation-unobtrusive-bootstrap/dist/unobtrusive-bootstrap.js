(function ($) {
    if ($.validator && $.validator.unobtrusive) {
        var defaultOptions = {
            validClass: 'is-valid',
            errorClass: 'is-invalid',
            highlight: function (element, errorClass, validClass) {

                var form = $(element).closest('form');

                $(element)
                    .removeClass(validClass)
                    .addClass(errorClass);

                var labelName = $(element).attr('name');

                $("label[for='" + labelName + "']", form)
                    .removeClass('text-success')
                    .addClass('text-danger');

            },
            unhighlight: function (element, errorClass, validClass) {

                var form = $(element).closest('form');

                $(element)
                    .removeClass(errorClass)
                    .addClass(validClass);

                var labelName = $(element).attr('name');

                $("label[for='" + labelName + "']", form)
                    .removeClass('text-danger')
                    .addClass('text-success');
            }
        };

        $.validator.setDefaults(defaultOptions);

        $.validator.unobtrusive.options = {
            errorClass: defaultOptions.errorClass,
            validClass: defaultOptions.validClass,
            errorElement: 'span',
            errorPlacement: function (error, element) {
                error.addClass('invalid-feedback');
            }
        };
    }
    else {
        console.warn('$.validator is not defined. Please load this library **after** loading jquery.validate.js and jquery.validate.unobtrusive.js');
    }
})(jQuery);
