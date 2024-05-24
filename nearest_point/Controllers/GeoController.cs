using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Oracle.DataAccess.Client;

namespace nearest_point.Controllers
{
    public class GeoController : ApiController
    {
        public string DBConnection = "User Id = NAVIDD;  Password = Navi_2357; Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orcl11)))";
        public string find_nearest_point(double x, double y)
        {
            // connecting to oracle database
            try
            {
                using (OracleConnection conn = new OracleConnection(DBConnection))
                {
                    using (OracleCommand cmd = new OracleCommand("", conn))
                    {
                        conn.Open();
                        List<String> values = new List<string>();
                        OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);

                        // sending coordinae of user to database and getting the result
                        try
                        {
                            cmd.CommandText = @"WITH locations AS 
                                                    (SELECT Name, X, Y, 
                                                        sdo_geom.sdo_distance(
                                                            sdo_geometry(2001, 4326, sdo_point_type(X, Y, null), null, null),
                                                            sdo_geometry(2001, 4326, sdo_point_type(" + x.ToString() + ", " + y.ToString() + @", null), null, null),
                                                            0.01,
                                                            'unit=KM'
                                                        )*1000 as distance
                                                    FROM coordinates ORDER BY distance) 
                                                SELECT Name, X, Y, ROUND(distance, 3) 
                                                FROM locations 
                                                WHERE distance = (SELECT MIN(distance) FROM locations)";
                            cmd.CommandType = CommandType.Text;
                            OracleDataReader data = cmd.ExecuteReader();
                            // Parsing result from database query
                            while (data.Read())
                            {
                                values.Add(data.GetString(0));
                                values.Add(data.GetDecimal(1).ToString());
                                values.Add(data.GetDecimal(2).ToString());
                                values.Add(data.GetDecimal(3).ToString());
                            }

                            return "Name: " + values[0] + "        Longitude: " + values[1] + "        Latitude: " + values[2] + "        Distance: " + values[3] + "m";
                        }
                        catch (Exception ex)
                        {
                            return ex.Message;
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                return ex.Message + " can't connect to database";
            }

        }

        public IHttpActionResult GetNearestPoint(double x, double y)
        {
            try
            {
                // finding properties of nearest point
                string nearestpoint = find_nearest_point(x, y);
                return Ok(nearestpoint);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
