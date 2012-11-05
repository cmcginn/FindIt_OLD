
var Selections = function (options) {
    selectionParent = options.selectionParent;
    selector=options.selector;
    mockParentData = options.mockParentData;
    this.getSelectionGroups = function () {        
        selectionParent(mockParentData);        
    }
    getSubSelections = function (groupindex,itemindex) {
        selectionParent().SelectionGroups[groupindex].Groups[itemindex].SelectionGroups(options.mockParentData)
    }
    this.applyLayout = function () {
            $(selector).first('.selection-group').accordion({
                header: '.selection-group-header',
                collapsible: true,
                autoHeight: false,
                heightStyle: 'content',
                active: false,
                icons: { 'header': 'ui-icon-triangle-1-e' },
                beforeActivate: function (event, ui) {
                    var target = $(event.currentTarget).children('.group-name:first');
                    var groupindex = parseFloat(target.attr('groupindex'));
                    var itemindex = parseFloat(target.attr('itemindex'));
                    getSubSelections(groupindex, itemindex);                 
                }
            });
        }
    }

$(function () {
    //categories.init();
});