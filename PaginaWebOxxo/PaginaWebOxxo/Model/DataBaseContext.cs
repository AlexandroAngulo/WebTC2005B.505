using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;


namespace PaginaWebOxxo.Model
{
    public class DataBaseContext
    {
        public string ConnectionString { get; set; }

        public DataBaseContext()
        {
            ConnectionString = "Server=mysql-a0f09b6-dario-ceda.b.aivencloud.com;Port=15915;Database=Proyecto;Uid=avnadmin;password=AVNS_x1ewxXkuSiLMKWdUhD2;";
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
                string query = "SELECT Monedas FROM estadisticas WHERE NumEmpleado = @NumEmpleado";
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

        //Mateo
           public Contacto ObtenerContactoPorEmpleados(int numEmpleado)
        {
            Contacto usuario = null;

            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                string query = "SELECT * FROM contacto WHERE NumEmpleado = @NumEmpleado";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@NumEmpleado", numEmpleado);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Contacto
                            {
                                NumEmpleado = Convert.ToInt32(reader["NumEmpleado"]),
                                telefono = Convert.ToInt32(reader["Telefono"]),
                                correo = reader["Correo"].ToString(),
                                codigoP = reader["CodigoPostal"].ToString(),

                            };
                        }
                    }
                }
            }
            return usuario;
        }
           public Codigopostal ObtenerCodigoPorEmpleados(int codigoPostal)
        {
            Codigopostal usuario = null;

            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                string query = "SELECT * FROM codigopostal WHERE CodigoPostal = @CodigoPostal";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@CodigoPostal", codigoPostal);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Codigopostal
                            {
                                CodigoPostal = Convert.ToInt32(reader["CodigoPostal"]),
                                colonia = reader["Colonia"].ToString(),
                                municipio = reader["Municipio"].ToString(),
                                estado = reader["Estado"].ToString(),

                            };
                        }
                    }
                }
            }
            return usuario;
        }

        //Ivan
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
        
        // Querys de dario
        public List<Empleados> ObtenerEmpleadosPorLider(int numLider)
        {
            var empleados = new List<Empleados>();

            using (var conexion = GetConnection())
            {
                conexion.Open();
                string query = @"SELECT e.NumEmpleado, e.Nombre, e.ApellidoP, e.IdEstatus, p.Puesto
                                FROM empleados e
                                JOIN puesto p ON p.IdTipoPuesto = e.IdTipoPuesto
                                WHERE e.NumLider = @NumLider";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@NumLider", numLider);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            empleados.Add(new Empleados
                            {
                                NumEmpleado = Convert.ToInt32(reader["NumEmpleado"]),
                                Nombre = reader["Nombre"].ToString(),
                                ApellidoP = reader["ApellidoP"].ToString(),
                                IdEstatus = Convert.ToInt32(reader["IdEstatus"]),
                                Puesto = reader["Puesto"].ToString()
                            });
                        }
                    }
                }
            }

            return empleados;
        }
    }

}

