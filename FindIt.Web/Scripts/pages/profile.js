
var profile = {
    model:{
        locations: null,
        selectedLocations:null
    },
    viewModel:null,
    init: function () {
        profile.loadModel();
    },
    loadModel: function () {
        $.get('/api/Location', function (result, state) {
            profile.model.locations = result;
            //profile.model.selectedLocations = [result[0],result[1],result[20]];            
            profile.viewModel = ko.mapping.fromJS(profile.model);
            ko.applyBindings(profile.viewModel);
        });

    }
}

$(function () {
    profile.init();
});

