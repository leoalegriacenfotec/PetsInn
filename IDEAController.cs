using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Test_API_IDEA_BOARD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IDEAController : ControllerBase
    {
        SqlConnection conn = new SqlConnection("Server=tcp:ideaboardsharpcoders.database.windows.net,1433;Initial Catalog=IdeaBoardSharpCoders;Persist Security Info=False;User ID=ideaboardamin;Password=Cenfo1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        [HttpGet]
        public string Get()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Dispositivo", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return "No data found!";
            }

        }

        [HttpPost]
        public string Post([FromBody] string value)
        {
            // Split the input string into an array using comma as the separator
            string[] values = value.Split(',');

            // Make sure that you have exactly three values
            if (values.Length == 3)
            {
                // Create a new SqlCommand
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Dispositivo (IdW, Humedad, Temperatura) VALUES (@IdW, @Humedad, @Temperatura)", conn))
                {
                    // Assign each value to its respective parameter
                    cmd.Parameters.AddWithValue("@IdW", values[0].Trim());
                    cmd.Parameters.AddWithValue("@Humedad", values[1].Trim());
                    cmd.Parameters.AddWithValue("@Temperatura", values[2].Trim());

                    // Open the connection
                    conn.Open();

                    // Execute the command
                    cmd.ExecuteNonQuery();

                    // Close the connection
                    conn.Close();
                }

                return "Record inserted with the value as " + value;
            }
            else
            {
                return "Invalid input format. Please provide values for IdW, Humedad, and Temperatura separated by commas.";
            }
        }


        [HttpGet]
        public Dispositivo getStringTest()
        {
            return new Dispositivo()
            {
                IdW = "W01",
                Humedad = "10%",
                Temperatura = "29C"
            };
        }

        [HttpPost]
        public Dispositivo PostTest(Dispositivo dispositivo)
        {
            dispositivo.IdW = "Recibido -->" + dispositivo.IdW;
            dispositivo.Humedad = "Recibido -->" + dispositivo.Humedad;
            dispositivo.Temperatura = "Recibido -->" + dispositivo.Temperatura;

            return dispositivo;
        }

    }
}
