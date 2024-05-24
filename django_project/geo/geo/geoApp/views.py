from django.shortcuts import render
import os
import folium

def home(request):
    shp_dir = os.path.join(os.getcwd(), 'media', 'shp')

    # folium
    m = folium.Map(location=[35.6892, 51.3890], zoom_start=9)  # Setting Tehran's coordinates

    # Collecting properties of all hotels
    hotel_properties = []
    with open(os.path.join(shp_dir, 'hotel.geojson'), encoding='utf-8') as f:
        geojson_data = f.read()
        hotels = folium.GeoJson(geojson_data)
        for feature in hotels.data['features']:
            properties = feature['properties']
            hotel_properties.append(properties)

    # Formatting properties into HTML content for the popup
    popup_content = '<h3>Hotels Information</h3><ul>'
    for hotel in hotel_properties:
        popup_content += '<li>'
        for key, value in hotel.items():
            popup_content += f'<strong>{key}:</strong> {value}<br>'
        popup_content += '</li>'
    popup_content += '</ul>'

    # Adding popup with all hotel features to the map
    popup = folium.Popup(popup_content, max_width=800)
    folium.Marker([35.6892, 51.3890], popup=popup).add_to(m)  # Marker at Tehran's coordinates

    # Style for basin and rivers
    style_basin = {'fillColor': '#228B22', 'color': '#228B22'}
    style_rivers = {'color': 'blue'}

    # Adding basin and rivers layers to the map
    folium.GeoJson(
        os.path.join(shp_dir, 'basin.geojson'), 
        name='basin', 
        style_function=lambda x: style_basin
    ).add_to(m)
    folium.GeoJson(
        os.path.join(shp_dir, 'rivers.geojson'), 
        name='rivers', 
        style_function=lambda x: style_rivers
    ).add_to(m)

    # Adding layer control to the map
    folium.LayerControl().add_to(m)

    # Exporting map to HTML
    m_html = m._repr_html_()

    # Context for rendering
    context = {'my_map': m_html}

    # Rendering template with map
    return render(request, 'geoApp/home.html', context)
