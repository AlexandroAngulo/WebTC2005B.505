using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace APIAngel.Controllers;

[ApiController]
[Route("[controller]")]

public class LeaderboardController : ControllerBase
{
    public string ConnectionString = "Server=mysql-a0f09b6-dario-ceda.b.aivencloud.com;Port=15915;Database=Proyecto;Uid=avnadmin;password=AVNS_x1ewxXkuSiLMKWdUhD2;";

    [HttpGet("Top10")]
    public IActionResult ObtenerTop10()
    {
        var lista = new List<Leaderboard>();

        using var conexion = new MySqlConnection(ConnectionString);
        conexion.Open();

        string query = @"SELECT NumEmpleado, SUM(Puntuacion) AS PuntuacionTotal FROM nivelusuario GROUP BY NumEmpleado ORDER BY PuntuacionTotal DESC LIMIT 10;";

        using var cmd = new MySqlCommand(query, conexion);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var usuario = new Leaderboard
            {
                NumEmpleado = reader.GetInt32("NumEmpleado"),
                PuntuacionTotal = reader.GetInt32("PuntuacionTotal")
            };

            lista.Add(usuario);
        }
        conexion.Close();

        return Ok(lista);
    }
}