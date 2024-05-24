using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using Oracle.DataAccess.Client;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Opening file explorer window
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|KML (*.kml*)|*.kml*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            // display file location in text box
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fdlg.FileName;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filepath = textBox1.Text;
            DataTable dt = readCoords(filepath);
            insert_Data(dt);

        }
        public DataTable readCoords(string file_Path)
        {
            // Reading KML file

            // settings for xml reader
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;


            // creating data table for storing data
            DataTable locations = new DataTable();
            locations.Clear();
            locations.Columns.Add("Location");
            locations.Columns.Add("Longitude");
            locations.Columns.Add("Latitude");
            locations.Columns.Add("Elevation");

            string location_Name;
            string[] coordinates;
            int counter = 0;
            // reading through file
            using (XmlReader reader = XmlReader.Create(file_Path, settings))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    // find placemark tag
                    if (reader.Name == "Placemark" && reader.IsStartElement())
                    {
                        reader.ReadToDescendant("name"); // go to name tag of the placemark
                        location_Name = reader.ReadElementContentAsString();
                        // checking if the name contains char ' and replacing it with '' so sql command works correctly
                        if (location_Name.Contains("'"))
                        {
                            location_Name = location_Name.Replace("'", "''");
                            Console.WriteLine(location_Name);
                        }

                        reader.ReadToNextSibling("Point"); // go to Point tag of placemark
                        reader.ReadToDescendant("coordinates"); // go to coordinates tag in point tag
                        coordinates = reader.ReadElementContentAsString().Split(',');
                        // store a place's name and coordinates in data table
                        string[] data = { location_Name, coordinates[0], coordinates[1], coordinates[2] };
                        locations.Rows.Add(data);
                        counter++;

                    }

                }
            }
            label4.Text = counter.ToString();
            return locations;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            

        }
        public string DBConnection = "User Id = NAVIDD;  Password = Navi_2357; Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orcl11)))";
        public void insert_Data(DataTable locations)
        {
            // connecting to oracle database
            try
            {
                using (OracleConnection conn = new OracleConnection(DBConnection))
                {
                    using (OracleCommand cmd = new OracleCommand("", conn))
                    {
                        conn.Open();
                        OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        // inserting each row of data into database table PLACES
                        int counter = 0;
                        foreach (DataRow row in locations.Rows)
                        {
                            try
                            {
                                cmd.CommandText = @"INSERT INTO COORDINATES (name, x, y, z) VALUES ('" + row["Location"].ToString().Replace("'", "''") + "','" + row["Longitude"].ToString() + "','" + row["Latitude"].ToString() + "','" + row["Elevation"].ToString() + "')";
                                cmd.CommandType = CommandType.Text;
                                cmd.ExecuteNonQuery();
                                counter = counter + 1;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                break;
                            }
                        }
                        txn.Commit();
                        MessageBox.Show(counter.ToString() + " rows inserted");
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }

}
