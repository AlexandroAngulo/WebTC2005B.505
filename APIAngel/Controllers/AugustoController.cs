using AugustoOrozcoAPI;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace AugustoOrozcoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AugustoOrozcoController : ControllerBase
{
    public string ConnectionString = "Server=mysql-a0f09b6-dario-ceda.b.aivencloud.com;Port=15915;Database=Proyecto;Uid=avnadmin;password=AVNS_x1ewxXkuSiLMKWdUhD2;";

    [HttpGet("ObtenerPersonajes")]
    public IActionResult ObtenerPersonajes()
    {
        using var conexion = new MySqlConnection(ConnectionString);
        conexion.Open();

        string query = @"SELECT up.IdPersonalizacion, p.NombreAspecto
                        FROM usuariopersonalizacion up
                        JOIN personalizacion p ON up.IdPersonalizacion = p.IdPersonalizacion
                        WHERE up.Equipado = 1;";

        MySqlCommand cmd = new MySqlCommand(query, conexion);
        using (var reader = cmd.ExecuteReader())
        {
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
} 

[Route("[controller]")]
public class AugustoOrozcoController2 : ControllerBase
{
    public string ConnectionString = "Server=mysql-a0f09b6-dario-ceda.b.aivencloud.com;Port=15915;Database=Proyecto;Uid=avnadmin;password=AVNS_x1ewxXkuSiLMKWdUhD2;";

    [HttpGet("ObtenerCanciones")]
    public IActionResult ObtenerCanciones()
    {
        using var conexion = new MySqlConnection(ConnectionString);
        conexion.Open();

        string query = @"SELECT upm.IdPersonalizacion, pm.NombreAspectoM
                        FROM usuariopersonalizacionM upm
                        JOIN personalizacionM pm ON upm.IdPersonalizacion = pm.IdPersonalizacion
                        WHERE upm.EquipadoM = 1";
                                  
        MySqlCommand cmd = new MySqlCommand(query, conexion);
        using (var reader = cmd.ExecuteReader())
        {
            var canciones = new List<Cancion>();
            while (reader.Read())
            {
                var cancion = new Cancion
                {
                    IdPersonalizacionM = reader.GetInt32("IdPersonalizacion"),
                    NombreAspectoM = reader.GetString("NombreAspectoM")
                };
                canciones.Add(cancion);
            }
            return Ok(canciones);
        }
    }
} 