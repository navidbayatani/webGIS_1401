using System.Windows.Forms;

namespace get_tiles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string Tile_Col = TileColumnTB.Text;
            string Tile_Row = TileRowTB.Text;
            string Image_URl = "http://localhost:8080/geoserver/gis_class/gwc/service/wmts?layer=gis_class%3Aqomm&style=&tilematrixset=EPSG%3A4326&Service=WMTS&Request=GetTile&Version=1.0.0&Format=image%2Fjpeg&TileMatrix=EPSG%3A4326%3A7&TileCol=" + Tile_Col + "&TileRow=" + Tile_Row;
            try
            {
                pictureBox1.Load(Image_URl);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}