function OptionsModel(options) {
    
    this.OptionNameProperty = options.nameProperty;
    this.OptionsIdProperty = options.idProperty;
    this.OptionsUrl = options.optionsUrl;
    this.OptionsCallback = options.parentCallback;
    
    this.LoadOptions = function () {
        var cb = this.OptionsCallback;
        var np = this.OptionNameProperty;
        var ip = this.OptionsIdProperty;
        $.get(this.OptionsUrl, function (data) {
            var result = { Options: new Array() };
            $(data).each(function (index) {
                result.Options.push({ OptionName: $(this)[0][np], Index: $(this)[0][ip],SubOptions:new Array() });
            });          
            cb(result);
        });
    }

}

function applyLayout(selector,selectionCallback) {
    $(selector).first('.options-group').accordion({
        header: '.options-group-header',
        collapsible: true,
        autoHeight: false,
        heightStyle: 'content',
        active: false,
        icons: { 'header': 'ui-icon-triangle-1-e' },
        beforeActivate: function (event, ui) {
            var target = $(event.currentTarget).children('.options-name:first');            
            var itemindex = parseFloat(target.attr('optionIndex'));
            selectionCallback(itemindex);
        }
    });
}