function OptionsModel(options) {
    
    this.OptionNameProperty = options.nameProperty;
    this.OptionsIdProperty = options.idProperty;
    this.OptionsUrl = options.optionsUrl;
    this.OptionsCallback = options.parentCallback;
    this.Options = null;
    this.LoadOptions = function () {
        var cb = this.OptionsCallback;
        var np = this.OptionNameProperty;
        var ip = this.OptionsIdProperty;
        this.Options = new Array();
        var loc = this.Options;
        $.get(this.OptionsUrl, function (data) {
            //var result = { Options: new Array() };
            var loc = new Array();
            $(data).each(function (index) {
                loc.push({ OptionName: $(this)[0][np], Index: index,SubOptions:new Array() });
            });
            this.Options = loc;
            cb(this);
        });
    }

}
