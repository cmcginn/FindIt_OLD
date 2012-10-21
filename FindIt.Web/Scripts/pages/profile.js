
var profile = {

    viewModel: null,
    init: function () {
        $('#btn_search').click(function () { profile.searchLocations(); });
    },
    searchLocations: function () {
      
        $.ajax({
            url: 'http://api.geonames.org/searchJSON?q=' + $('#location_search').val() + '&featureClass=A&maxRows=10&username=demo121',
            dataType: 'json',
            success: function (data, textStatus, jqXHR) {
                profile.viewModel = ko.mapping.fromJS(data);
                ko.applyBindings(profile.viewModel);
            }
        });

    }
}

$(function () {
    profile.init();
});

