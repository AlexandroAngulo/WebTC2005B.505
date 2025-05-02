using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;


namespace PaginaWebOxxo.Model
{
    public class DataBaseContext{
        public string ConnectionString { get; set; }
        
        public DataBaseContext(){
            ConnectionString = "Server=127.0.0.1;Port=3306;Database=BD.TC2005B.505;Uid=root;password=;";
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        public Estadisticas ObtenerMonedasPorEmpleado(int numEmpleado)
        {
            Estadisticas estadisticas = null;
            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                string query = "SELECT Monedas FROM Estadisticas WHERE NumEmpleado = @NumEmpleado";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@NumEmpleado", numEmpleado);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            estadisticas = new Estadisticas();
                            estadisticas.Monedas = Convert.ToInt32(reader["monedas"]);
                        }
                    }   
                }
            }
            return estadisticas;
        }
        
        public Usuarios ObtenerUsuarioPorEmpleados(int numEmpleado)
        {
            Usuarios usuario = null;

            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                string query = "SELECT * FROM usuarios WHERE NumEmpleado = @NumEmpleado";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@NumEmpleado", numEmpleado);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuarios
                            {
                                NumEmpleado = Convert.ToInt32(reader["NumEmpleado"]),
                                Nombre = reader["Nombre"].ToString(),
                                ApellidoP = reader["ApellidoP"].ToString(),
                                ApellidoM = reader["ApellidoM"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                                FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                                IdGenero = Convert.ToInt32(reader["IdGenero"]),
                                IdTipoPuesto = Convert.ToInt32(reader["IdTipoPuesto"]),
                                IdEstatus = Convert.ToInt32(reader["IdEstatus"])
                            };
                        }
                    }
                }
            }
            return usuario;
        }

        public List<NivelUsuario> ObtenerProgresoPorEmpleado(int numEmpleado)
        {
            List<NivelUsuario> progresos = new List<NivelUsuario>();

            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                string query = @"SELECT NumEmpleado, IdNivel, FechaIntento, Estrellas, Puntuacion, TiempoNivel 
                                FROM nivelusuario 
                                WHERE NumEmpleado = @NumEmpleado";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@NumEmpleado", numEmpleado);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NivelUsuario nivel = new NivelUsuario
                            {
                                NumEmpleado = Convert.ToInt32(reader["NumEmpleado"]),
                                IdNivel = Convert.ToInt32(reader["IdNivel"]),
                                FechaIntento = Convert.ToDateTime(reader["FechaIntento"]),
                                Estrellas = Convert.ToInt32(reader["Estrellas"]),
                                Puntuacion = Convert.ToInt32(reader["Puntuacion"]),
                                TiempoNivel = Convert.ToInt32(reader["TiempoNivel"])
                            };
                            progresos.Add(nivel);
                        }
                    }
                }
            }
            return progresos;
        }
    }

}

