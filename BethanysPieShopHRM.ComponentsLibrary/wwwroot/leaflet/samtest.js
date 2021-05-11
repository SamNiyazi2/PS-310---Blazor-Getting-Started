// Original code didn't work.  Mangled map.
// Added this. It worked but not with app data.

// http://harrywood.co.uk/maps/examples/openlayers/simple-marker.view.html
window.ssnMapTest = function (lonlat) {

    console.log("%cssnMapTesst", 'color:yellow');
    console.log(lonlat);
    console.log(lonlat[0]);
    console.log(lonlat[0].y);
    console.log(lonlat[0].x);

    console.log(parseFloat(lonlat[0].y));
    console.log(parseFloat(lonlat[0].x));


    map = new OpenLayers.Map("mapdiv");
    map.addLayer(new OpenLayers.Layer.OSM());

    var lonLat = new OpenLayers.LonLat(-0.1279688, 51.5077286)
   // var lonLat = new OpenLayers.LonLat(parseFloat( lonlat[0].y), parseFloat(lonlat[0].x))
// Taking this section out didn't work either.
        .transform(
            new OpenLayers.Projection("EPSG:4326"), // transform from WGS 1984
            map.getProjectionObject() // to Spherical Mercator Projection
    )
        ;

    var zoom = 16;

    var markers = new OpenLayers.Layer.Markers("Markers");
    map.addLayer(markers);

    markers.addMarker(new OpenLayers.Marker(lonLat));


    map.setCenter(lonLat, zoom);

}

