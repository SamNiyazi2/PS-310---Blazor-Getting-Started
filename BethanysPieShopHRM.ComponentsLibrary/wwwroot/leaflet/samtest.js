// Original code didn't work.  Mangled map.
// Added this. It worked but not with app data.

// http://harrywood.co.uk/maps/examples/openlayers/simple-marker.view.html
window.ssnMapTest = function (lonlat) {

    console.log("%c ssnMapTest", 'color:yellow');
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

};


 
// 04/06/2022 09:23 pm - firstCall for form load.

function ssnSetFocus( firstRender) {

    console.log(`%c Calling ssnSetFocus `, 'color:yellow');
 
    if (firstRender) {

        console.log(`%c firstRender`, 'color:yellow');

        const objsWithAutoFocus = document.querySelectorAll('[autoFocus]');

        // 04/05/2022 02:52 am - SSN - Not functioning as expected

        if (objsWithAutoFocus.length > 0) {
            objsWithAutoFocus[0].focus();
            // Works.
            // objsWithAutoFocus[0].style.backgroundColor = 'yellow';
        }
        console.dir(objsWithAutoFocus);

    } else {
        const objsWithError = document.querySelectorAll('div.validation-message');

        console.log('%c'+ 'Validating-20220406-2158-J', 'color:blue;font-size:12pt;');
        console.dir(objsWithError);

        if (objsWithError.length > 0) {

            objsWithError[0].parentElement.scrollIntoView({ behavior: 'smooth' });


        } else {

            const objsWithError = document.querySelectorAll('.alert-danger');
            if (objsWithError.length > 0) {

                objsWithError[0].scrollIntoView({ behavior: 'smooth' });


            }
        }
    }


}

