﻿$.validator.addMethod('password', function (value, element, params) {
    var str = value;

    var counts = {};
    var ch, index, len, count;

    for (index = 0, len = str.length; index < len; ++index) {

        ch = str.charAt(index);

        count = counts[ch];

        counts[ch] = count ? count + 1 : 1;
    }

    if (counts.length >= 6)
    {
        return true;
    }

    return false;
});

$.validator.unobtrusive.adapters.add('password', ['type'], function (options) {
    var element = $(options.form).find('#uniquecharacters')[0];

    options.rules['password'] = [element, options.params['type']];
    options.messages['password'] = options.message;
});