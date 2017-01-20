var geocoder;
var map;
var directionsDisplay;
var directionsService;
var pos = { lat: 18.9473, lng: -70.4811 };
var stores = [];
var markers = [];
var iconLink = document.getElementById("link_to_store_icon").value;

function setMarker(pos, title, store) {
    var _animation = google.maps.Animation.DROP;

    var _marker = {}
    if (store) {
        _marker = new google.maps.Marker({
            map: map,
            draggable: false,
            animation: _animation,
            position: pos,
            title: title,
            icon: iconLink
        });
    } else {
        _marker = new google.maps.Marker({
            map: map,
            draggable: false,
            animation: _animation,
            position: pos,
            title: title
        });
    }
    return _marker;
}

function calculateAndDisplayRoute() {
    var _origin = new google.maps.LatLng(pos.lat, pos.lng);
    var _destination = new google.maps.LatLng(stores[0].pos.lat, stores[0].pos.lng);
    directionsService.route({
        origin: _origin,
        destination: _destination,
        travelMode: 'DRIVING'
    }, function (response, status) {
        if (status === 'OK') {
            directionsDisplay.setDirections(response);
        } else {
            window.alert('Directions request failed due to ' + status);
        }
    });
}

function showInfoWindow(info, _marker) {
    for (var c = 0; c < markers.length; c++) {
        markers[c].infoWindow.close();
    }
    info.open(map, _marker);
}

function _addListerner(_marker) {
    _marker.marker.addListener('click', function () {
        showInfoWindow(_marker.infoWindow, _marker.marker);
    });
}

function geocodeLatLng(_pos, _name, store) {
    geocoder.geocode({ 'location': _pos }, function (results, status) {
        if (status === 'OK') {
            if (results[0]) {
                var _infowindow = new google.maps.InfoWindow({
                    content: results[0].formatted_address
                });
                var _marker = {
                    marker: setMarker(_pos, name, store),
                    infoWindow: _infowindow
                }
                _addListerner(_marker);
                markers.push(_marker);
            } else {
                console.log('No se encontraron resultados.');
            }
        } else {
            console.log('Geocoder falló debido a: ' + status);
        }
    });
}

function _initMap(zoom) {
    //map.setCenter(pos);
    //map.setZoom(zoom);
    //geocodeLatLng(pos, "Tu ubicacion!!!", false);
    //for (var c = 0; c < stores.length; c++) {
    //    geocodeLatLng(stores[c].pos, stores[c].name, true);
    //}
    calculateAndDisplayRoute();
}

function initMap(showed) {
    geocoder = new google.maps.Geocoder;
    directionsService = new google.maps.DirectionsService;
    directionsDisplay = new google.maps.DirectionsRenderer;
    map = new google.maps.Map(document.getElementById('map-create'));
    directionsDisplay.setMap(map);    

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            if (showed)
                _initMap(13);
            return true;
        }, function () {
            showErrorMsgMap(true);
        });
    } else {
        showErrorMsgMap(true);
    }
}

function showErrorMsgMap(show) {
    if (show) {
        document.getElementById('not-geolocation-map-msg').className = 'row';
        document.getElementById('map-create').className = 'hidden';
    } else {
        document.getElementById('not-geolocation-map-msg').className = 'row hidden';
        document.getElementById('map-create').className = '';
    }
}

function setModal(_lat, _lng, _name) {
    stores = [];
    markers = [];
    stores.push({ pos: { lat: Number(_lat), lng: Number(_lng) }, name: _name });
    $("#maps_search").openModal({
        dismissible: true, // Modal can be dismissed by clicking outside of the modal
        opacity: .5, // Opacity of modal background
        in_duration: 300, // Transition in duration
        out_duration: 200, // Transition out duration
        ready: function (modal, trigger) { // Callback for Modal open. Modal and trigger parameters available.
            initMap(true);
        },
        complete: function () { // Callback for Modal close
            geocoder = null;
            directionsService = null;
            directionsDisplay = null;
            map = null;
            pos = { lat: 18.9473, lng: -70.4811 };
            stores = [];
            markers = [];
            $("#map-create").children("div").remove();
        }
    });

    $('#maps_search').openModal('open');
    return true;
}
