from django.shortcuts import render
import os
import folium

def home(request):
    shp_dir = os.path.join(os.getcwd(), 'media', 'shp')

    # folium
    m = folium.Map(location=[35.6892, 51.3890], zoom_start=9)  # Setting Tehran's coordinates

    # Style for hotel
    style_hotel = {'fillColor': '#FF0000', 'color': '#FF0000'}

    # Adding hotel layer to the map
    with open(os.path.join(shp_dir, 'hotel.geojson'), encoding='utf-8') as f:
        geojson_data = f.read()
        folium.GeoJson(
            geojson_data,
            name='hotels',
            style_function=lambda x: style_hotel,
            popup=folium.GeoJsonPopup(fields=['name', 'tourism'], aliases=['Name', 'Type'])
        ).add_to(m)

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
