from tkinter import filedialog as fd
import tkinter as tk
import geopandas as gpd
import matplotlib.pyplot as plt
#asking path from user
Page = tk.Tk()
file_path = fd.askopenfilename(title="Select your GeoJSON file: ",filetypes=(("all files", "*.*"),("JSON Files", "*.json"),("GeoJSON Files", "*.geojson")))
Page.destroy()

#plotting
class Reading_geojson:
    
    def __init__(self, file_path):
        self.file_path = file_path
        self.geojson_data_frame = gpd.read_file(self.file_path)
        
        self.geojson_data_frame.plot()
        plt.xlabel('horizontal')
        plt.ylabel('vertical')

Reading_geojson(file_path)    
