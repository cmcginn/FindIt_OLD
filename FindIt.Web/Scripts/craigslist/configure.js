var configure = {
    /*------------------- View Models and Mapping *-------------------*/
    //keeps an count of model items for array indexing
    stateCount: 0,
    groupCount:0,
    keywordCount:0,
    viewModel: null,    
    //takes care of handling multiple callbacks constructs a model, THEN gets bound
    dataModel:{
        States: new Array(),
        Groups: new Array(),
        Keywords: new Array(),
        ChangedCities: new Array(),    
        ProfileName:null
    },
    dto:null,
    mapping: {
       
        'States': {            
            create: function (options) {
               
                var result = {
                    Index: configure.stateCount,
                    StateName: options.data[0].StateProvinceName,
                    StateCode: options.data[0].StateProvinceCode,
                    Cities: new ko.observableArray(),                    
                    AddCities: function (data) {
                        var items = new Array();
                        $(data[0].Cities).each(function (index) {
                            var item = {
                                Index: index,
                                Id:this.Id,
                                CityName: this.CityName,
                                Selected: ko.observable(false)
                            };
                            item.Selected.subscribe(function () { configure.saveCity(item) });
                            item.StateProvinceCode = data[0].StateProvinceCode;
                            items.push(item);
                        });
                        this.Cities(items);
                    },
                    Selected: function () { configure.onStateSelected(this); }
                };
                configure.stateCount++;

                return result;                
            }
        },
        'Groups': {
            create: function (options) {
                var result = {
                    Index: configure.groupCount,
                    GroupName: options.data[0].Name,
                    Id: options.data[0].Id,
                    Categories:new ko.observableArray(),
                    AddCategories:function(data){
                        var items = new Array();
                        $(data).each(function (index) {
                            var item = {
                                Id:this.Id,
                                Index: index,
                                Name: this.Name,
                                Selected: ko.observable(false)
                            }
                            item.Selected.subscribe(function () { configure.saveCategory(item) });
                            items.push(item);
                        });
                        this.Categories(items);
                    }
                };
                configure.groupCount++;
                return result;
            }
        }        
    },
    /*------------------- Event Handlers -------------------*/
    //detects when complete model has been bound
    onDataReceived: function () {
        if (configure.dataModel.States.length > 0 &&
            configure.dataModel.Groups.length > 0 && configure.dto != null) {
            configure.viewModel = ko.mapping.fromJS(configure.dataModel, configure.mapping);
            ko.applyBindings(configure.viewModel);
            configure.applyKeywordsLayout();
            configure.applyStateLayout();
            configure.applyGroupLayout();
            configure.applyConfigureLayout();
        }
    },
    onStatesReceived: function (data) {
      
        //turn into generic options with selected event
        $(data).each(function () {
            //turn int viewmodel state with select
            configure.dataModel.States.push($(this));
        });
        configure.onDataReceived();        
        
    },
    onStateSelected: function (index) {       
        //if this is the first time a state is selected get cities
        var stateProvince = configure.viewModel.States()[index];
        if (stateProvince.Cities.length == 0)
            configure.getCities(stateProvince);
    },
    onGroupSelected: function (index) {
        var group = configure.viewModel.Groups()[index];
        if (group.Categories.length == 0)
            configure.getCategories(group);
    },
    onGroupsReceived:function(data){
        $(data).each(function () {
            configure.dataModel.Groups.push($(this));
        });
        configure.onDataReceived();
    },
    onDtoReceived: function (data) {
        console.log(data);
    },
    /*------------------- Data Functions -------------------*/
    loadDto:function(){
        $.get('http://localhost:15718/api/CraigslistApi/LoadDto', function (data) {
            configure.dto = data;
            configure.onDataReceived();
        });

    },
    getStates: function () {
        $.get('http://localhost:15718/api/Location/CountryStateProvince?countryCode=US', function (data) {
            configure.onStatesReceived(data);

        })
    },
    getCities: function (stateProvince) {        
            $.get('http://localhost:15718/api/Location/StateProvinceCity?name=' + stateProvince.StateCode, function (data) {
                stateProvince.AddCities(data);
                //apply binding to just our cities node
                ko.applyBindingsToNode($('.cities')[stateProvince.Index], { template: { name: 'cities-template', data: stateProvince } });
            });        
    },
    //craigslist groups
    getGroups:function()
    {
        $.get('http://localhost:15718/api/CraigslistApi/CraigslistGroups',function(data){
            configure.onGroupsReceived(data);
        });
    },
    getCategories:function(group){
        $.get('http://localhost:15718/api/CraigslistApi/GetCategories?groupId=' + group.Id,function(data){
            group.AddCategories(data);
            //apply binding to just our craigslist group node
            ko.applyBindingsToNode($('.categories')[group.Index], { template: { name: 'categories-template', data: group } });
        });
    },
    saveCategory: function (category) {
        var index = configure.dto.SelectedCategories.indexOf(category.Id);
        if (category.Selected()) {
            if (index == -1)
                configure.dto.SelectedCategories.push(category.Id);
        } else {
            if(index > -1)
                configure.dto.SelectedCategories.splice(configure.dto.SelectedCategories.indexOf(category.Id), 1);
        }
    },
    saveCity: function (city) {
        var index = configure.dto.SelectedCities.indexOf(city.StateProvinceCode+'/'+city.CityName);
        if (city.Selected()) {
            if (index == -1)
                configure.dto.SelectedCities.push(city.StateProvinceCode + '/' + city.CityName);

        } else {
            if (index > -1) {
                configure.dto.SelectedCities.splice(configure.dto.SelectedCities.indexOf(city.StateProvinceCode + '/' + city.CityName), 1);
            }
        }
    },
    saveProfile:function(){
        configure.dto.ProfileName = configure.viewModel.ProfileName();
        $(configure.viewModel.Keywords()).each(function () {
            configure.dto.Keywords.push({ 'Key': $(this)[0].KeywordValue, 'Value': $(this)[0].KeywordScore });
        });
        $.ajax({
            type: 'POST',
            dataType:'json',
            url:'http://localhost:15718/api/CraigslistApi/StoreSession',
            data: configure.dto
        });            
    },
    addKeyword:function(){
        configure.dataModel.Keywords.push({ KeywordValue: $('#new_keyword').val(), KeywordScore: ko.observable($('#new_keyword_score').val()).extend({numeric:0}), Remove: function () { configure.removeKeyword(this) } });

        configure.viewModel.Keywords(configure.dataModel.Keywords);
        $('.keywords-remove').last().button()
        $('#new_keyword').val('');
        $('#new_keyword_score').val('');

    },
    removeKeyword: function (keyword) {
        var removeAt = configure.viewModel.Keywords.indexOf(keyword);
        configure.dataModel.Keywords = new Array();
        $(configure.viewModel.Keywords()).each(function (index) {
            if (index != removeAt)
                configure.dataModel.Keywords.push(this);
        });
        configure.viewModel.Keywords(configure.dataModel.Keywords);
        
    },
    /*------------------- UI------------------*/
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
    applyGroupLayout:function(){
        $('.group').accordion({
            collapsible: true,
            autoHeight: false,
            heightStyle: 'content',
            active: false,
            icons: { 'header': 'ui-icon-triangle-1-e' },
            beforeActivate: function (event, ui) {
                //only when expanding
                if (ui.newHeader.length == 1) {
                    configure.onGroupSelected(parseFloat($(event.currentTarget).attr('index')));
                }
            }
        });
    },
    applyKeywordsLayout:function(){
        $('.keywords-add').button().click(function (event) { configure.addKeyword(event); });
    },
    applyConfigureLayout:function(){
        $('#configure .section').accordion({
            header: '.section-header',
            collapsible: true,
            autoHeight: false,
            heightStyle: 'content',
            active: false,
        });

        $('#save').button();
        $('#save').click(function (event) {
            configure.saveProfile();
        });
        $('#configure').show();
    },
    /*------------------- Utility------------------*/
    init: function () {
        configure.getStates();
        configure.getGroups();
        configure.loadDto();
    }
}
$(function () {
    configure.init();
});