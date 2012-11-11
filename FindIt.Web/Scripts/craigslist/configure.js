var configure = {
    stateCount:0,
    viewModel: null,
    
    //takes care of handling multiple callbacks constructs a model, THEN gets bound
    dataModel:{
        States: new Array()
       
    },
    mapping: {
       
        'States': {            
            create: function (options) {
                var self = this;
                var result = {
                    Index: configure.stateCount,
                    StateName: options.data[0].StateProvinceName,
                    StateCode: options.data[0].StateProvinceCode,
                    Cities: new ko.observableArray(),                    
                    AddCities: function (data) {
                        var items = new Array();
                        $(data[0].Cities).each(function (index) {
                            items.push({
                                Index: index,
                                CityName: this.CityName,
                                Selected: false
                            });
                        });
                        this.Cities(items);
                    },
                    Selected: function () { configure.onStateSelected(this); }
                };
                configure.stateCount++;
                return result;                
            }
        }
    },
    onCitiesRendered:function(){
        console.log('hide');
    },
    onStateSelected: function (index) {
        //configure.viewModel.States()[0]
        //get city data and bind to selected state
        var sp = configure.viewModel.States()[index];
        if (sp.Cities.length == 0) {
            $.get('http://localhost:15718/api/Location/StateProvinceCity?name=' + sp.StateCode, function (data) {
                sp.AddCities(data);
                ko.applyBindingsToNode($('.cities')[index], { template: { name: 'cities-template', data: sp } });
            });
        }
    },
    //apply parent options accordion layout to states
    applyStateLayout:function(){
        $('.state').accordion({            
            collapsible: true,
            autoHeight: false,
            heightStyle: 'content',
            active: false,
            icons: { 'header': 'ui-icon-triangle-1-e' },
            beforeActivate: function (event, ui) {
                //only when expanding
                if (ui.newHeader.length == 1) {                                    
                    configure.onStateSelected(parseFloat($(event.currentTarget).attr('index')));
                }
            }
        });
    },
    //detects when complete model has been bound
    onDataReceived:function()
    {
        if (configure.dataModel.States.length > 0) {
            configure.viewModel = ko.mapping.fromJS(configure.dataModel, configure.mapping);
            ko.applyBindings(configure.viewModel);
            configure.applyStateLayout();
        }
    },
    //event handlers

    onStatesReceived: function (data) {
      
        //turn into generic options with selected event
        $(data).each(function (index) {
            //turn int viewmodel state with select
            configure.dataModel.States.push($(this));
        });
        configure.onDataReceived();        
        
    },
    getStates: function () {
        $.get('http://localhost:15718/api/Location/CountryStateProvince?countryCode=US', function (data) {
            configure.onStatesReceived(data);

        })
    },
    init: function () {
        configure.getStates();
    }
}
$(function () {
    configure.init();
});