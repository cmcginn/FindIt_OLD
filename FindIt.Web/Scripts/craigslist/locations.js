var mockdata = {
    mocklocationsviewmodel: JSON.parse('{"AvailableStates":{"StateGroups":[{"States":[{"State":{"StateName":"Alabama","StateId":"states/alabama","StateIndex":0,"GroupIndex":0,"CityCount":5,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"Arkansas","StateId":"states/arkansas","StateIndex":1,"GroupIndex":0,"CityCount":3,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"Alaska","StateId":"states/alaska","StateIndex":2,"GroupIndex":0,"CityCount":1,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"California","StateId":"states/california","StateIndex":3,"GroupIndex":0,"CityCount":100,"AvailableCities":{"CityGroups":[]}}}]},{"States":[{"State":{"StateName":"Colorado","StateId":"states/colorado","StateIndex":0,"GroupIndex":1,"CityCount":6,"AvailableCities":{"CityGroups":[{"Cities":[{"City":{"CityName":"Barborosa","CityId":"cities/barborosa","Selected":false}},{"City":{"CityName":"Mathilda","CityId":"cities/matilda","Selected":false}},{"City":{"CityName":"Cornolio","CityId":"cities/cornolio","Selected":false}},{"City":{"CityName":"Mackwapapa","CityId":"cities/mackwapapa","Selected":true}}]},{"Cities":[{"City":{"CityName":"Atlantis","CityId":"cities/atlantis","Selected":false}},{"City":{"CityName":"Wilbur","CityId":"cities/wilbur","Selected":true}},{"City":{"CityName":"Harrington","CityId":"cities/harrington","Selected":false}},{"City":{"CityName":"Zanzabar","CityId":"cities/zanzabar","Selected":false}}]}]}}},{"State":{"StateName":"Delaware","StateId":"states/delaware","StateIndex":1,"GroupIndex":1,"CityCount":3,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"Florida","StateId":"states/florida","StateIndex":2,"GroupIndex":1,"CityCount":1,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"Georgia","StateId":"states/georgia","CityCount":100,"StateIndex":3,"GroupIndex":1,"AvailableCities":{"CityGroups":[]}}}]},{"States":[{"State":{"StateName":"Hawaii","StateId":"states/hawaii","StateIndex":0,"GroupIndex":2,"CityCount":6,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"Idaho","StateId":"states/idaho","StateIndex":1,"GroupIndex":2,"CityCount":3,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"Illinois","StateId":"states/illinois","StateIndex":2,"GroupIndex":2,"CityCount":1,"AvailableCities":{"CityGroups":[]}}},{"State":{"StateName":"Iowa","StateId":"states/iowa","StateIndex":3,"GroupIndex":2,"CityCount":100,"AvailableCities":{"CityGroups":[]}}}]}]},"AddLocation":[],"RemoveLocation":[]}'),
    states: JSON.parse('{"StateGroups":[{"States":[{"State":{"StateName":"Alabama","StateId":"states/alabama","CityCount":5}},{"State":{"StateName":"Arkansas","StateId":"states/arkansas","CityCount":3}},{"State":{"StateName":"Alaska","StateId":"states/alaska","CityCount":1}},{"State":{"StateName":"California","StateId":"states/california","CityCount":100}}]},{"States":[{"State":{"StateName":"Colorado","StateId":"states/colorado","CityCount":6}},{"State":{"StateName":"Delaware","StateId":"states/delaware","CityCount":3}},{"State":{"StateName":"Florida","StateId":"states/florida","CityCount":1}},{"State":{"StateName":"Georgia","StateId":"states/georgia","CityCount":100}}]},{"States":[{"State":{"StateName":"Hawaii","StateId":"states/hawaii","CityCount":6}},{"State":{"StateName":"Idaho","StateId":"states/idaho","CityCount":3}},{"State":{"StateName":"Illinois","StateId":"states/illinois","CityCount":1}},{"State":{"StateName":"Iowa","StateId":"states/iowa","CityCount":100}}]}]}'),
    cities: JSON.parse('{"CityGroups":[{"Cities":[{"City":{"CityName":"Barborosa","CityId":"cities/barborosa","Selected":false}},{"City":{"CityName":"Mathilda","CityId":"cities/matilda","Selected":false}},{"City":{"CityName":"Cornolio","CityId":"cities/cornolio","Selected":false}},{"City":{"CityName":"Mackwapapa","CityId":"cities/mackwapapa","Selected":true}}]},{"Cities":[{"City":{"CityName":"Atlantis","CityId":"cities/atlantis","Selected":false}},{"City":{"CityName":"Wilbur","CityId":"cities/wilbur","Selected":true}},{"City":{"CityName":"Harrington","CityId":"cities/harrington","Selected":false}},{"City":{"CityName":"Zanzabar","CityId":"cities/zanzabar","Selected":false}}]}]}'),
    categories:JSON.parse('{"Categories":[{"GroupIndex":0,"Groups":[{"Option":{"OptionName":"Jobs","OptionIndex":0,"Id":"groups/jobs","SubOptions":{"SelectionGroups":[]}}},{"Option":{"OptionName":"Gigs","OptionIndex":1,"Id":"groups/gigs","SubOptions":{"SelectionGroups":[]}}},{"Option":{"OptionName":"Housing","OptionIndex":2,"Id":"groups/housing","SubOptions":{"SelectionGroups":[]}}}]},{"GroupIndex":1,"Groups":[{"Option":{"OptionName":"Community","OptionIndex":0,"Id":"groups/community","SubOptions":{"SelectionGroups":[]}}},{"Option":{"OptionName":"For Sale","OptionIndex":1,"Id":"groups/forsale","SubOptions":{"SelectionGroups":[]}}},{"Option":{"OptionName":"Discussions","OptionIndex":2,"Id":"groups/discusions","SubOptions":{"SelectionGroups":[]}}}]}]}')
}
var locations = {
    init: function () {
        locations.getStates();
    },
    jsonData: null,
    viewmodel: null,
    getStates: function () {
        locations.viewmodel = ko.mapping.fromJS(mockdata.mockviewmodel);        
        ko.applyBindings(locations.viewmodel);
        $('.state-group').accordion({
            header:'.state-header',
            collapsible: true,
            autoHeight: false,
            heightStyle:'content',
            active: false,
            icons: { 'header': 'ui-icon-triangle-1-e' },
            beforeActivate: function (event, ui) {
                var target = $(event.currentTarget).children('.state-name');
                var groupindex = parseFloat(target.attr('groupindex'));
                var stateindex = parseFloat(target.attr('stateindex'));
                locations.getCities(groupindex,stateindex);
            }
        });
        $('.city-input').live('change', function (sender) { 
            var id = $(this).attr('data-id');
            var checked = $(this)[0].checked;
            if (checked)
                locations.viewmodel.AddLocation().push(id);
            else
                location.viewmodel.RemoveLocation().push(id);
        });
        //var url = '/api/mockapi/GetStateProvinceSearch';
        //locations.getJson(url, function (data, textStatus, jqXHR) {
        //    locations.jsonData = data;
        //    locations.viewmodel = ko.mapping.fromJS(locations.jsonData);
        //    ko.applyBindings(locations.viewmodel);
        //});
        
    },
    getCities: function (groupindex, stateindex) {
        locations.viewmodel.AvailableStates.StateGroups()[groupindex].States()[stateindex].State.AvailableCities.CityGroups(mockdata.cities.CityGroups);
    },
    getJson: function (url,cb) {
        var result = null;
        $.ajax({
            url: url,
            dataType: 'json',
            success: cb
        });       
    },

}
$(function () {  

});