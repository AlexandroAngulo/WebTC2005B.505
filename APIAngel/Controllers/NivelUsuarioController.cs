using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace APIAngel.Controllers;

[ApiController]
[Route("[controller]")]

public class NivelUsuarioController : ControllerBase
{
    public string ConnectionString = "Server=mysql-a0f09b6-dario-ceda.b.aivencloud.com;Port=15915;Database=Proyecto;Uid=avnadmin;password=AVNS_x1ewxXkuSiLMKWdUhD2;";

    [HttpPut("ActualizarIntento")]
    public IActionResult Update([FromBody] NivelUsuario intento)
    {
        using var conexion = new MySqlConnection(ConnectionString);
        conexion.Open();

        string query = @"UPDATE nivelusuario SET FechaIntento = @FechaIntento, Estrellas = @Estrellas, Puntuacion = @Puntuacion, TiempoNivel = @TiempoNivel WHERE NumEmpleado = @NumEmpleado AND IdNivel = @IdNivel AND @Puntuacion > Puntuacion";

        MySqlCommand cmd = new MySqlCommand(query, conexion);
        cmd.Parameters.AddWithValue("@FechaIntento", DateTime.Now);
        cmd.Parameters.AddWithValue("@Estrellas", intento.Estrellas);
        cmd.Parameters.AddWithValue("@Puntuacion", intento.Puntuacion);
        cmd.Parameters.AddWithValue("@TiempoNivel", intento.TiempoNivel);
        cmd.Parameters.AddWithValue("@NumEmpleado", intento.NumEmpleado);
        cmd.Parameters.AddWithValue("@IdNivel", intento.IdNivel);

        int filasAfectadas = cmd.ExecuteNonQuery();

        if (filasAfectadas > 0)
        {
            return Ok(new { mensaje = "Se actualizo el registro." });
        }
            
        else
        {
            return NotFound(new { mensaje = "No se pudo actualizar el registro" });
        }
    }
}