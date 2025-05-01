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

    }
    
}

