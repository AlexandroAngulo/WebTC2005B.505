using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

using MySql.Data.MySqlClient; 
using System.Collections.Generic;
using System.Data;


namespace APIAngel.Controllers;
[ApiController]
[Route("[controller]")]

public class EstadisticasController : ControllerBase
{
    public string ConnectionString= "Server=mysql-a0f09b6-dario-ceda.b.aivencloud.com;Port=15915;Database=Proyecto;Uid=avnadmin;password=AVNS_x1ewxXkuSiLMKWdUhD2;";
    [HttpGet("Estadisticas/{NumEmpleado}")]

    public Estadisticas Estadisticas(int NumEmpleado)
    {
        Estadisticas miestadistica = new Estadisticas();
        MySqlConnection conexion = new MySqlConnection(ConnectionString); //abro una neuva conexion con el conectionstring
        conexion.Open();

        MySqlCommand cmd = new MySqlCommand(); //Query
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ObtenerEstadisticasPorEmpleado"; //el nombre del store prosedure de la base de datos
        cmd.Parameters.AddWithValue("@num_id", NumEmpleado);
        cmd.Connection = conexion; //sobre esta conexion

        Estadisticas Estad1 = new Estadisticas();
        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                miestadistica.NumEmpleado = Convert.ToInt32(reader["NumEmpleado"]);
                miestadistica.estrellas = Convert.ToInt32(reader["TotalEstrellas"]);
                miestadistica.Monedas = Convert.ToInt32(reader["Monedas"]);
            }

        }
        conexion.Close();

        return miestadistica;
    }
}




