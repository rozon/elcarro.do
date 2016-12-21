var geocoder;
var infowindow;
var map;
var marker;
var pos = { lat: 18.9473, lng: -70.4811 };

function setMarker(pos, title) {
    var _animation = google.maps.Animation.DROP;
    if (marker) {
        marker.setMap(null);
        _animation = null;
    }

    var draggable = true;
    if (document.getElementById("no-drageble"))
        draggable = false;

    marker = new google.maps.Marker({
        map: map,
        draggable: draggable,
        animation: _animation,
        position: pos,
        title: title
    });

    marker.addListener('dragend', function () {
        var _pos = { lat: this.position.lat(), lng: this.position.lng() };
        geocodeLatLng(_pos);
    });

    marker.setMap(map);
}

function setValuesModel(pos) {
    document.getElementById("latitude").value = pos.lat;
    document.getElementById("longitude").value = pos.lng;
}

function geocodeLatLng(pos) {
    geocoder.geocode({ 'location': pos }, function (results, status) {
        setMarker(pos, "Tu ubicación!!!");
        setValuesModel(pos);
        if (status === 'OK') {
            if (results[0]) {
                infowindow.setContent(results[0].formatted_address);
                infowindow.open(map, marker);
            } else {
                console.log('No se encontraron resultados.');
            }
        } else {
            console.log('Geocoder falló debido a: ' + status);
        }
    });
}

function _initMap(zoom){
    map.setCenter(pos);
    map.setZoom(zoom);
    geocodeLatLng(pos);
}

function getPosition() {
    var lat = document.getElementById("latitude").value;
    var lng = document.getElementById("longitude").value;
    if(lat && lng){
        pos = { lat: Number(lat), lng: Number(lng) }
        return true;
    }else{
        return false;
    }
}

function codeAddress() {
    var address = document.getElementById('address-map').value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == 'OK') {
            map.setCenter(results[0].geometry.location);
            var loc = results[0].geometry.location;
            pos = { lat: loc.lat(), lng: loc.lng() };
            _initMap(16);
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

function initMap() {
    geocoder = new google.maps.Geocoder;
    infowindow = new google.maps.InfoWindow;
    map = new google.maps.Map(document.getElementById('map-create'), {
        center: pos,
        zoom: 6
    });

    if (getPosition()) {
        _initMap(16);
    }
    // Try HTML5 geolocation.
    else if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            _initMap(16);
        }, function () {
            _initMap(6);
        });
    } else {
        _initMap(6);
    }
}
