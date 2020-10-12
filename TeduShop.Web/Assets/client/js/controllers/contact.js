var contact = {
    init: function () {
        contact.registerEvent();
    },
    registerEvent: function () {
        contact.initMap();
    },
    initMap: function () {
        var latVal = ($('#hidLat').val()).replace(',', '.');
        var lngVal = ($('#hidLng').val()).replace(',', '.');
        console.log('lat', latVal);
        console.log('lng', lngVal);
        var uluru = { lat: Number(latVal), lng: Number(lngVal) };
        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 17,
            center: uluru
        });

        var contentString = $('#hidAddress').val();

        var infowindow = new google.maps.InfoWindow({
            content: contentString
        });

        var marker = new google.maps.Marker({
            position: uluru,
            map: map,
            title: $('#hidName').val()
        });
        infowindow.open(map, marker);
    }
}

contact.init();