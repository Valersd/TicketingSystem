(function ($) {
    if ($.validator && $.validator.unobtrusive) {

        $.validator.addMethod('donotcontain', function (value, element, params) {
            //return value.toLowerCase().indexOf(params.toLowerCase()) < 0;  --> for single parameter
            return value.toLowerCase().indexOf(params.forbidden.toLowerCase()) < 0;  // --> for several parameters
        });

        //$.validator.unobtrusive.adapters.addSingleVal('donotcontain', 'forbidden');  --> for single parameter

        $.validator.unobtrusive.adapters.add('donotcontain', ['forbidden'], function (options) {  // --> for several parameters
            options.rules['donotcontain'] = {
                forbidden:options.params.forbidden
            }
            options.messages['donotcontain'] = options.message;
        })

    }
}(jQuery));
