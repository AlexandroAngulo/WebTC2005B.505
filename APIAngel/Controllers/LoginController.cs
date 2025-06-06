using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace APIAngel.Controllers;

[ApiController]
[Route("[controller]")]

public class LoginController : ControllerBase
{
    public string ConnectionString = "Server=mysql-a0f09b6-dario-ceda.b.aivencloud.com;Port=15915;Database=Proyecto;Uid=avnadmin;password=AVNS_x1ewxXkuSiLMKWdUhD2;";


    [HttpGet("login/{NumEmpleado}")]
    public Login login(int NumEmpleado)
    {
        MySqlConnection conexion = new MySqlConnection(ConnectionString);
        conexion.Open();

        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "VerificarContraseña";
        cmd.Parameters.AddWithValue("@p_NumEmpleado", NumEmpleado);

        cmd.Connection = conexion;
        Login login = new Login();
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                login.NumEmpleado = Convert.ToInt32(reader["NumEmpleado"]);
                login.Contraseña = reader["Contraseña"].ToString();
            }
        }
        conexion.Close();
        return login;
    }
}

