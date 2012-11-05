function Options(options) {
    this.Options = new Array();
    for (var i = 0; i < options.length; i++) {
        options[i].Index = i;
        options[i].SubOptions = new Array();
        this.Options.push(options[i]);
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