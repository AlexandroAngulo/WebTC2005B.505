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

        string query = @"SELECT u.Nombre, u.ApellidoP, SUM(nu.Puntuacion) AS PuntuacionTotal, SUM(nu.Estrellas) AS TotalEstrellas FROM nivelusuario nu JOIN usuarios u ON u.NumEmpleado = nu.NumEmpleado GROUP BY nu.NumEmpleado ORDER BY PuntuacionTotal DESC LIMIT 10;";

        using var cmd = new MySqlCommand(query, conexion);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var usuario = new Leaderboard
            {
                Nombre = reader.GetString("Nombre"),
                ApellidoP = reader.GetString("ApellidoP"),
                PuntuacionTotal = reader.GetInt32("PuntuacionTotal"),
                TotalEstrellas = reader.GetInt32("TotalEstrellas")
            };

            lista.Add(usuario);
        }
        conexion.Close();

        return Ok(lista);
    }
}