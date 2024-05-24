var mapView = new ol.View({
    center: ol.proj.fromLonLat([52, 35]),
    zoom: 8,
});

var map = new ol.Map({
    target: 'map',
    view: mapView,
});

var noneTile = new ol.layer.Tile({
    title: 'None',
    type: 'base',
    visible: false,
})

var osmTile = new ol.layer.Tile({
    title: 'open street map',
    visible: true,
    source: new ol.source.OSM(),
})

map.addLayer(osmTile);
var baseGroup = new ol.layer.Tile({
    title: 'Base Maps',
    layers: [osmTile, noneTile],
})

var qomTile = new ol.layer.Tile({
    title: 'qomRoads',
    source: new ol.source.TileWMS({
        url: 'http://localhost:8080/geoserver/gis_class/wms',
        params: { 'LAYERS': 'gis_class:qom', 'TILED': true },
        serverType: 'geoserver',
        visible: true
    })
})

map.addLayer(qomTile);

//

function toggleLayer(eve) {
    var lyrname = eve.target.value;
    var checkedStatus = eve.target.checked;
    var lyrlist = map.getLayers();

    lyrlist.forEach(function (element) {
        if (lyrname == element.get('title')) {
            element.setVisible(checkedStatus);
        }
    });
}

var container = document.getElementById('popup');
var content = document.getElementById('popup-content');
var closer = document.getElementById('popup-closer');

var popup = new ol.Overlay({
    element: container,
    autoPan: true,
    autoPanAnimation: {
        duration: 250,

    },
});

map.addOverlay(popup);

closer.onclick = function () {
    popup.setPosition(undefined);
    closer.blur();
    return false;
};

map.on('singleclick', function (evt) {
    content.innerHTML = '';
    var resolution = mapView.getResolution();

    var url = qomTile.getSource().getFeatureInfoUrl(evt.coordinate, resolution, mapView.getProjection(), {
        'INFO_FORMAT': 'application/json',
        'propertyName': 'osm_id,name,fclass',
    });
    if (url) {
        $.getJSON(url, function (data) {
            var feature = data.features[0];
            var props = feature.properties;
            console.log(feature)
            content.innerHTML = "<h3>Name : </h3> <p>" + props.name.toUpperCase() + "</p> <br> <h3> fclass : </h3> <p> " +
                props.fclass.toUpperCase() + "</p>" + "<br> <h3>OSM_ID : </h3> <p>" + props.osm_id.toUpperCase() + "</p>";
            popup.setPosition(evt.coordinate);
        });
    } else {
        popup.setPosition(undefined);
    }
});
