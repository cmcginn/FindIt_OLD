var locations = {
    country: null,
    stateprovince: null,
    city: null,
    data:{cities:null,stateprovinces:null,countries:null,selectedCountry:null,selectedStateProvince:null},
    viewmodel:null,
    
    init: function () {
        $('#location_search').hide();
        locations.allCountries(function () { locations.getStateProvinces(); });
        $('#stateprovince').change(function () { locations.getCities(); });
        $('#btn_save_cities').click(function () { locations.saveLocations(); });
        $('#cities').hide();
        $('#location_search').show();
    },
    saveLocations:function(){
        $('form').submit();
        $('#cities').hide();
        locations.data.stateprovinces = null;
        locations.data.cities = null;
        locations.allCountries(function () { locations.getStateProvinces(); });
    },
    getCities: function () {
        if ($('#stateprovince').val() != '') {
            var url = '/api/location/stateprovincecity?name=' + $('#stateprovince').val();
            locations.getLocations(url, function (data, textStatus, jqXHR) {
                locations.data.cities = data;
                locations.viewmodel = ko.mapping.fromJS(locations.data);
                ko.applyBindings(locations.viewmodel);
                $('#cities').show();
            });
        }
    },
    allCountries:function(cb){
        var url = '/api/location/allcountries';
        locations.getLocations(url, function (data, textStatus, jqXHR) {
            locations.data.countries = data;
            locations.viewmodel = ko.mapping.fromJS(locations.data);
            ko.applyBindings(locations.viewmodel);
            cb();
        });

    },
    getStateProvinces:function(){
        var url = '/api/location/countrystateprovince?name=' + $('#country').val();
        locations.getLocations(url, function (data, textStatus, jqXHR) {
            locations.data.stateprovinces = data;
            locations.viewmodel = ko.mapping.fromJS(locations.data);
            ko.applyBindings(locations.viewmodel);
        });
    },
    getCountries:function(){
        var url = '/api/location/country?name=' + $('#country').val();       
        locations.getLocations(url, function (data, textStatus, jqXHR) {
            locations.data.countries = data;
            locations.viewmodel = ko.mapping.fromJS(locations.data);
            ko.applyBindings(locations.viewmodel);
            
        });

    },
    searchLocations: function () {

        if ($('#country').val() != '') {
            locations.getCountries();
        }
        //locations.city = $('#city').val();
        //locations.stateprovince = $('#stateprovince').val();
        //if (locations.city != null)
        //    url += 'city?name=' + locations.city;
        //else if (locations.stateprovince != null)
        //    url += 'stateprovince?name=' + locations.stateprovince;
        //else if (locations.country != null)
        //    url += 'bycountry/' + locations.country;

        //if (url != null) {
           
        //}
    },
    getLocations: function (url,cb) {
        var result = null;
        $.ajax({
            url: url,
            dataType: 'json',
            success: cb
        });       
    }
};

$(function () {
    locations.init();
});
