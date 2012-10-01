
var profile = {
    model:{
        locations: null,
        selectedLocations:null
    },
    mapping:{
        'stateProvinces': {
            create: function (options) {
                
                return options.data;
            }
        }
    },
    viewModel:null,
    init: function () {
        profile.loadModel();
    },
    loadModel: function () {
        $.get('/api/CommonApi/GetCountries', function (result, state) {
            profile.model.locations = result;
            profile.onModelDataLoaded();

        });

    },
    onModelDataLoaded:function(){
        if (profile.model.locations != null) {
            profile.viewModel = ko.mapping.fromJS(profile.model,profile.mapping);
            ko.applyBindings(profile.viewModel);
        }
    }
}

$(function () {
    profile.init();
});

