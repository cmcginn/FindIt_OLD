var mockdata = {
    mockviewmodel: new Object(),
    mocklocationsviewmodel: JSON.parse('{"AvailableStates":{"StateGroups":[{"States":[{"State":{"StateName":"Alabama","StateId":"states/alabama","StateIndex":0,"GroupIndex":0,"CityCount":5,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"Arkansas","StateId":"states/arkansas","StateIndex":1,"GroupIndex":0,"CityCount":3,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"Alaska","StateId":"states/alaska","StateIndex":2,"GroupIndex":0,"CityCount":1,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"California","StateId":"states/california","StateIndex":3,"GroupIndex":0,"CityCount":100,"AvailableCities":{"CityGroups":[]}}}]},{"States":[{"State":{"StateName":"Colorado","StateId":"states/colorado","StateIndex":0,"GroupIndex":1,"CityCount":6,"AvailableCities":{"CityGroups":[{"Cities":[{"City":{"CityName":"Barborosa","CityId":"cities/barborosa","Selected":false}},{"City":{"CityName":"Mathilda","CityId":"cities/matilda","Selected":false}},{"City":{"CityName":"Cornolio","CityId":"cities/cornolio","Selected":false}},{"City":{"CityName":"Mackwapapa","CityId":"cities/mackwapapa","Selected":true}}]},{"Cities":[{"City":{"CityName":"Atlantis","CityId":"cities/atlantis","Selected":false}},{"City":{"CityName":"Wilbur","CityId":"cities/wilbur","Selected":true}},{"City":{"CityName":"Harrington","CityId":"cities/harrington","Selected":false}},{"City":{"CityName":"Zanzabar","CityId":"cities/zanzabar","Selected":false}}]}]}}},{"State":{"StateName":"Delaware","StateId":"states/delaware","StateIndex":1,"GroupIndex":1,"CityCount":3,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"Florida","StateId":"states/florida","StateIndex":2,"GroupIndex":1,"CityCount":1,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"Georgia","StateId":"states/georgia","CityCount":100,"StateIndex":3,"GroupIndex":1,"AvailableCities":{"CityGroups":[]}}}]},{"States":[{"State":{"StateName":"Hawaii","StateId":"states/hawaii","StateIndex":0,"GroupIndex":2,"CityCount":6,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"Idaho","StateId":"states/idaho","StateIndex":1,"GroupIndex":2,"CityCount":3,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"Illinois","StateId":"states/illinois","StateIndex":2,"GroupIndex":2,"CityCount":1,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"Iowa","StateId":"states/iowa","StateIndex":3,"GroupIndex":2,"CityCount":100,"AvailableCities":{"CityGroups":[]}}}]}]},"AddLocation":[],"RemoveLocation":[]}'),
    locationOptions: JSON.parse('[{"OptionName":"Alabama","Id":"states/alabama"},{"OptionName":"Arkansas","Id":"states/arkansas"},{"OptionName":"Alaska","Id":"states/alaska"},{"OptionName":"California","Id":"states/california"},{"OptionName":"Colorado","Id":"states/colorado"}]'),
    cities: JSON.parse('{"Categories":[{"GroupIndex":0,"Groups":[{"Option":{"OptionName":"Barborosa","Id":"cities/barborosa","SubOptions":{"Categories":[]}}},{"Option":{"OptionName":"Mathilda","Id":"cities/matilda","SubOptions":{"Categories":[]}}},{"Option":{"OptionName":"Cornolio","Id":"cities/cornolio","SubOptions":{"Categories":[]}}}]},{"GroupIndex":1,"Groups":[{"Option":{"OptionName":"Atlantis","OptionIndex":0,"Id":"cities/atlantis","SubOptions":{"Categories":[]}}},{"Option":{"OptionName":"Wilbur","OptionIndex":1,"Id":"cities/wilbur","SubOptions":{"Categories":[]}}},{"Option":{"OptionName":"Harrington","OptionIndex":2,"Id":"cities/harrington","SubOptions":{"Categories":[]}}}]}]}'),
    listinggroups: JSON.parse('{"Categories":[{"GroupIndex":0,"Groups":[{"Option":{"OptionName":"Jobs","OptionIndex":0,"Id":"groups/jobs","SubOptions":{"Categories":[]}}},{"Option":{"OptionName":"Gigs","OptionIndex":1,"Id":"groups/gigs","SubOptions":{"Categories":[]}}},{"Option":{"OptionName":"Housing","OptionIndex":2,"Id":"groups/housing","SubOptions":{"Categories":[]}}}]},{"GroupIndex":1,"Groups":[{"Option":{"OptionName":"Community","OptionIndex":0,"Id":"groups/community","SubOptions":{"Categories":[]}}},{"Option":{"OptionName":"For Sale","OptionIndex":1,"Id":"groups/forsale","SubOptions":{"Categories":[]}}},{"Option":{"OptionName":"Discussions","OptionIndex":2,"Id":"groups/discusions","SubOptions":{"Categories":[]}}}]}]}'),
    listingOptions: JSON.parse('[{"OptionName":"Jobs","Id":"groups/jobs"},{"OptionName":"Gigs","Id":"groups/gigs"},{"OptionName":"Housing","Id":"groups/housing"},{"OptionName":"Community","Id":"groups/community"},{"OptionName":"For Sale","Id":"groups/forsale"},{"OptionName":"Discussions","Id":"groups/discussions"}]')
};


var configure = {
    viewModel: null,
    dataModel: {
        Profile: {
            ProfileInfo: { Title: null },
            Keywords: null,
            Categories: null,
            Locations: null
        }
    },
    removeKeyword: function (keyword) {
        var newCollection = new Array();
        $(configure.viewModel.Profile.Keywords()).each(function (index) {
            if ($(this)[0].Keyword.KeywordValue != keyword)
                newCollection.push($(this)[0]);
        });
        configure.viewModel.Profile.Keywords(newCollection);
    },
    addKeyword: function (event) {
        configure.viewModel.Profile.Keywords.push({ 'Keyword': { 'KeywordValue': $('#new_keyword').val(), 'KeywordScore': $('#new_keyword_score').val(), 'remove': function () { configure.removeKeyword($(this)[0].Keyword.KeywordValue); } } });
        $('.keywords-remove').last().button()
        $('#new_keyword').val('');
        $('#new_keyword_score').val('');
    },
    getLocations:function(callback){
        var locations = new OptionsModel({
            optionsUrl: 'http://localhost:15718/api/Location/CountryStateProvince?countryCode=US',
            nameProperty: 'StateProvinceName',
            idProperty: 'StateProvinceCode',
            parentCallback: function (result) {
                configure.dataModel.Profile.Locations = result;
                callback();
            }
        });
        locations.LoadOptions();
    },
    createViewModel:function()
    {
        configure.getLocations(function () {
            configure.viewModel = ko.mapping.fromJS(configure.dataModel);
            ko.applyBindings(configure.viewModel);
        });
        
        
        //configure.dataModel.Profile.Keywords = new Array();
        //configure.dataModel.Profile.Locations = new Options(mockdata.locationOptions);
       // configure.viewModel = ko.mapping.fromJS(configure.dataModel);
        
    },
    layout:function(){
        $('.keywords-add').button().click(function (event) { configure.addKeyword(event); });

        applyLayout('#categories', function (itemindex) {
            var mockSuboptions = new Options(mockdata.listingOptions).Options;
            configure.viewModel.Profile.Categories.Options()[itemindex].SubOptions(mockSuboptions);
        });

        applyLayout('#locations', function (itemindex) {
            var mockSuboptions = new Options(mockdata.locationOptions).Options;
            configure.viewModel.Profile.Locations.Options()[itemindex].SubOptions(mockSuboptions);
        });
        $("#sections").accordion(
        {
            collapsible: true,
            autoHeight: false,
            heightStyle: 'content',
            active: false,
            header: '.section-header'

        });

        $('#configure').show();
    },
    init: function () {
        configure.createViewModel();
        //configure.layout();       
    }
}
$(function () {
    configure.init();
});