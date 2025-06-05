using APIAngel;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace APIAngel.Controllers;

[ApiController]
[Route("[controller]")]
public class AugustoController : ControllerBase
{
    public string ConnectionString = "Server=mysql-a0f09b6-dario-ceda.b.aivencloud.com;Port=15915;Database=Proyecto;Uid=avnadmin;password=AVNS_x1ewxXkuSiLMKWdUhD2;";

    [HttpGet("ObtenerPersonajes")]
    public IActionResult ObtenerPersonajes(int numEmpleado)
    {
        using var conexion = new MySqlConnection(ConnectionString);
        conexion.Open();

        string query = @"SELECT pA.IdPersonalizacion, p.NombreAspecto
                        FROM personalizacion AS p
                        JOIN PersonajeActual AS pA ON p.IdPersonalizacion = pA.IdPersonalizacion
                        WHERE pA.NumEmpleado = @NumEmpleado AND pA.IdPersonalizacion  = p.IdPersonalizacion
                        LIMIT 1";
            

        using var cmd = new MySqlCommand(query, conexion);
        cmd.Parameters.AddWithValue("@NumEmpleado", numEmpleado);

        using var reader = cmd.ExecuteReader();
        var personajes = new List<Personaje>();
        while (reader.Read())
        {
            var personaje = new Personaje
            {
                IdPersonalizacion = reader.GetInt32("IdPersonalizacion"),
                NombreAspecto = reader.GetString("NombreAspecto")
            };
            personajes.Add(personaje);
        }
        return Ok(personajes);
    }
}

[Route("[controller]")]
public class AugustoController2 : ControllerBase
{
    public string ConnectionString = "Server=mysql-a0f09b6-dario-ceda.b.aivencloud.com;Port=15915;Database=Proyecto;Uid=avnadmin;password=AVNS_x1ewxXkuSiLMKWdUhD2;";

    [HttpGet("ObtenerCanciones")]
    public IActionResult ObtenerCanciones(int numEmpleado)
    {
        using var conexion = new MySqlConnection(ConnectionString);
        conexion.Open();

        string query = @"SELECT mA.IdPersonalizacion, pm.NombreMusica
                        FROM MusicaPersonalizacion AS pm
                        JOIN MusicaActual AS mA ON pm.IdPersonalizacion = mA.IdPersonalizacion
                        WHERE mA.NumEmpleado = @NumEmpleado AND mA.IdPersonalizacion  = pm.IdPersonalizacion
                        LIMIT 1";

        using var cmd = new MySqlCommand(query, conexion);
        cmd.Parameters.AddWithValue("@NumEmpleado", numEmpleado);

        using var reader = cmd.ExecuteReader();
        var canciones = new List<Cancion>();
        while (reader.Read())
        {
            var cancion = new Cancion
            {
                IdPersonalizacionM = reader.GetInt32("IdPersonalizacion"),
                NombreAspectoM = reader.GetString("NombreMusica")
            };
            canciones.Add(cancion);
        }
        return Ok(canciones);
    }
} 