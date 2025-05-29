using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace APIAngel.Controllers;

[ApiController]
[Route("[controller]")]



public class UsuarioController : ControllerBase
{
    public string ConnectionString = "Server=mysql-a0f09b6-dario-ceda.b.aivencloud.com;Port=15915;Database=Proyecto;Uid=avnadmin;password=AVNS_x1ewxXkuSiLMKWdUhD2;";


    [HttpGet("GetUsuarioById/{NumEmpleado}")] 

    public Usuarios Get(int NumEmpleado)
    {
        Usuarios miUsuario = new Usuarios();
        MySqlConnection conexion = new MySqlConnection(ConnectionString);

        conexion.Open();
        MySqlCommand cmd = new MySqlCommand("select * from usuarios where NumEmpleado = "+ NumEmpleado, conexion);
        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                miUsuario.NumEmpleado = Convert.ToInt32(reader["NumEmpleado"]);
                miUsuario.Nombre = reader["Nombre"].ToString();
                miUsuario.ApellidoP = reader["ApellidoP"].ToString();
                miUsuario.ApellidoM = reader["ApellidoM"].ToString();
                miUsuario.FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]);
                miUsuario.FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]);
                miUsuario.IdGenero = Convert.ToInt32(reader["IdGenero"]);
                miUsuario.IdTipoPuesto = Convert.ToInt32(reader["IdTipoPuesto"]);
                miUsuario.IdEstatus = Convert.ToInt32(reader["IdEstatus"]);
            }
        }
        conexion.Close();
        return miUsuario;
    }
}

