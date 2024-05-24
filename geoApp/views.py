from django.shortcuts import render
import os
import folium
import json

def home(request):
    base_dir = 'C:/Users/USER/documents/geo'
    shp_dir = os.path.join(base_dir, 'media', 'shp')

    # Initialize the folium map
    m = folium.Map(location=[35.68, 51.41], zoom_start=9)

    # Define styles
    style_basin = {'fillColor': '#228B22', 'color': '#228B22'}
    style_rivers = {'color': 'blue'}
    style_hotel = {'fillColor': '#FF0000', 'color': '#FF0000'}  # Example style for hotels

    # Add GeoJSON layers
    folium.GeoJson(os.path.join(shp_dir, 'basin.geojson'), name='basin', style_function=lambda x: style_basin).add_to(m)
    folium.GeoJson(os.path.join(shp_dir, 'rivers.geojson'), name='rivers', style_function=lambda x: style_rivers).add_to(m)

    # Read hotel.geojson with UTF-8 encoding
    with open(os.path.join(shp_dir, 'hotel.geojson'), 'r', encoding='utf-8') as f:
        hotel_geojson = json.load(f)

    # Function to create popup content from feature properties
    def popup_html(properties):
        popup_content = '<table border="1" style="width:100%;">'
        for key, value in properties.items():
            popup_content += f'<tr><th style="background-color:#f2f2f2;">{key}</th><td>{value}</td></tr>'
        popup_content += '</table>'
        return popup_content

    # Add hotel GeoJSON layer with popups
    for feature in hotel_geojson['features']:
        popup_content = popup_html(feature['properties'])
        folium.GeoJson(
            feature,
            name='hotel',
            style_function=lambda x: style_hotel,
            popup=folium.Popup(popup_content, max_width=300)
        ).add_to(m)

    # Add layer control
    folium.LayerControl().add_to(m)

    # Export the map as HTML
    m = m._repr_html_()
    context = {'my_map': m}

    # Render the template with the map
    return render(request, 'geoApp/home.html', context)
