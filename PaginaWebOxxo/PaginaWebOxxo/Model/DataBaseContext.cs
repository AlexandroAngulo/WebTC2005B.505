using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Mysqlx.Crud;
using System.Data;


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

        //Angel
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
                string query = "SELECT * FROM usuarios INNER JOIN genero on usuarios.IdGenero = genero.IdGenero JOIN puesto on usuarios.IdTipoPuesto = puesto.IdTipoPuesto WHERE usuarios.NumEmpleado = @NumEmpleado";
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
                                IdEstatus = Convert.ToInt32(reader["IdEstatus"]),
                                Puesto = reader["Puesto"].ToString(),
                                genero = reader["Genero"].ToString(),
                                horarioInicio = TimeSpan.Parse(reader["horarioInicio"].ToString()),
                                horarioFin = TimeSpan.Parse(reader["horarioFin"].ToString()),
                                fotoPerfil = reader["fotoPerfil"].ToString(),
                            };
                        }
                    }
                }
            }
            return usuario;
        }

        //Mateos
        public Contacto ObtenerContactoPorEmpleados(int numEmpleado)
        {
            Contacto contacto = new Contacto();

            MySqlConnection conexion = new MySqlConnection(ConnectionString);
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM contacto INNER JOIN codigopostal on contacto.CodigoPostal = codigopostal.CodigoPostal WHERE NumEmpleado = @NumEmpleado");
            cmd.Connection = conexion;
            cmd.Parameters.AddWithValue("@NumEmpleado", numEmpleado);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    contacto.NumEmpleado = Convert.ToInt32(reader["NumEmpleado"]);
                    contacto.telefono = reader["Telefono"].ToString();
                    contacto.correo = reader["Correo"].ToString();
                    contacto.codigoPostal = reader["CodigoPostal"].ToString();
                    contacto.colonia = reader["Colonia"].ToString();
                    contacto.municipio = reader["Municipio"].ToString();
                    contacto.estado = reader["Estado"].ToString();

                }
            }
            conexion.Close();
            return contacto;
        }

        public List<InstruccionVideojuego> ObtenerTodasLasInstrucciones()
        {
            List<InstruccionVideojuego> instrucciones = new List<InstruccionVideojuego>();

            using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
            {
                conexion.Open();
                string query = "SELECT id_instruccion, titulo, contenido FROM InstruccionesVideojuego";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InstruccionVideojuego instruccion = new InstruccionVideojuego
                            {
                                id_instruccion = Convert.ToInt32(reader["id_instruccion"]),
                                titulo = reader["titulo"].ToString(),
                                contenido = reader["contenido"].ToString()
                            };

                            instrucciones.Add(instruccion);
                        }
                    }
                }
            }

            return instrucciones;
        }

        public void ActualizarTelefono(int numEmpleado, string telefono)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                string query = "UPDATE contacto SET Telefono = @Telefono WHERE NumEmpleado = @NumEmpleado";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@NumEmpleado", numEmpleado);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Estadisticas ObtenerEstadisticasPorEmpleado(int numEmpleado)
        {
            Estadisticas estadisticas = null;
            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                string query = "CALL ObtenerEstadisticasPorEmpleado(@NumEmpleado)";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@NumEmpleado", numEmpleado);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            estadisticas = new Estadisticas
                            {
                                NumEmpleado = Convert.ToInt32(reader["NumEmpleado"]),
                                Monedas = Convert.ToInt32(reader["Monedas"]),
                                TotalEstrellas = Convert.ToInt32(reader["TotalEstrellas"])
                            };
                        }
                    }
                }
            }
            return estadisticas;
        }

        //Ivan
        public List<NivelUsuario> ObtenerProgresoPorEmpleado(int numEmpleado)
        {
            List<NivelUsuario> progresos = new List<NivelUsuario>();

            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                string query = @"SELECT nu.NumEmpleado, nu.IdNivel, nu.FechaIntento, nu.Estrellas, nu.Puntuacion, nu.TiempoNivel, n.NombreNivel
                                FROM nivelusuario nu JOIN niveles n ON nu.IdNivel = n.IdNivel 
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
                                TiempoNivel = Convert.ToInt32(reader["TiempoNivel"]),
                                NombreNivel = reader["NombreNivel"].ToString(),
                            };
                            progresos.Add(nivel);
                        }
                    }
                }
            }
            return progresos;
        }

        // Dario
        public List<Usuarios> ObtenerEmpleadosPorLider(int numLider)
        {
            List<Usuarios> empleados = new List<Usuarios>();
            MySqlConnection conexion = new MySqlConnection(ConnectionString);
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT u.*, p.Puesto FROM usuarios u join lider_empleados l on l.NumEmpleado = u.NumEmpleado join puesto p on p.IdTipoPuesto = u.IdTipoPuesto where l.NumLider = @NumLider");
            cmd.Parameters.AddWithValue("@NumLider", numLider);

            cmd.Connection = conexion;
            Usuarios empleado = new Usuarios();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    empleado = new Usuarios();
                    empleado.NumEmpleado = Convert.ToInt32(reader["NumEmpleado"]);
                    empleado.Nombre = reader["Nombre"].ToString();
                    empleado.ApellidoP = reader["ApellidoP"].ToString();
                    empleado.IdEstatus = Convert.ToInt32(reader["IdEstatus"]);
                    empleado.Puesto = reader["Puesto"].ToString();
                    empleado.horarioInicio = TimeSpan.Parse(reader["horarioInicio"].ToString());
                    empleado.horarioFin = TimeSpan.Parse(reader["horarioFin"].ToString());
                    empleado.fotoPerfil = reader["fotoPerfil"].ToString();
                    empleados.Add(empleado);
                }
            }
            return empleados;
        }

        public List<Usuarios> ObtenerEmpleadosNoAsignados(int numLider)
        {
            List<Usuarios> empleados = new List<Usuarios>();
            MySqlConnection conexion = new MySqlConnection(ConnectionString);
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT u.*, p.Puesto FROM usuarios u JOIN puesto p ON p.IdTipoPuesto = u.IdTipoPuesto WHERE u.NumEmpleado NOT IN ( SELECT l.NumEmpleado FROM lider_empleados l WHERE l.NumLider = @NumLider ) AND u.IdTipoPuesto NOT IN (1, 2)");
            cmd.Parameters.AddWithValue("@NumLider", numLider);

            cmd.Connection = conexion;
            Usuarios empleado = new Usuarios();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    empleado = new Usuarios();
                    empleado.NumEmpleado = Convert.ToInt32(reader["NumEmpleado"]);
                    empleado.Nombre = reader["Nombre"].ToString();
                    empleado.ApellidoP = reader["ApellidoP"].ToString();
                    empleado.IdEstatus = Convert.ToInt32(reader["IdEstatus"]);
                    empleado.Puesto = reader["Puesto"].ToString();
                    empleado.horarioInicio = TimeSpan.Parse(reader["horarioInicio"].ToString());
                    empleado.horarioFin = TimeSpan.Parse(reader["horarioFin"].ToString());
                    empleado.fotoPerfil = reader["fotoPerfil"].ToString();
                    empleados.Add(empleado);
                }
            }
            return empleados;
        }

        public void AgregarEmpleado(int numLider, int numEmpleado)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                string query = "UPDATE lider_empleados SET NumLider = @numLider WHERE NumEmpleado = @numEmpleado";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {

                    cmd.Parameters.AddWithValue("@numLider", numLider);
                    cmd.Parameters.AddWithValue("@numEmpleado", numEmpleado);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        //Login
        public Login login(int numEmpleado)
        {
            MySqlConnection conexion = new MySqlConnection(ConnectionString);
            conexion.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "VerificarContraseña";
            cmd.Parameters.AddWithValue("@p_NumEmpleado", numEmpleado);

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

        //Augusto
        public void EquiparPersonaje(int numEmpleado, int idPersonalizacion)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                string desactivar = @"DELETE FROM PersonajeActual WHERE NumEmpleado = @NumEmpleado;";
                using (MySqlCommand cmd1 = new MySqlCommand(desactivar, conexion))
                {
                    cmd1.Parameters.AddWithValue("@NumEmpleado", numEmpleado);
                    cmd1.Parameters.AddWithValue("@IdPersonalizacion", idPersonalizacion);
                    cmd1.ExecuteNonQuery();
                }

                string activar = @"INSERT INTO PersonajeActual (NumEmpleado, IdPersonalizacion) VALUES (@NumEmpleado, @IdPersonalizacion)";
                using (MySqlCommand cmd2 = new MySqlCommand(activar, conexion))
                {
                    cmd2.Parameters.AddWithValue("@NumEmpleado", numEmpleado);
                    cmd2.Parameters.AddWithValue("@IdPersonalizacion", idPersonalizacion);
                    cmd2.ExecuteNonQuery();
                }
            }
        }

        public void EquiparTrack(int numEmpleado, int idPersonalizacion)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                

                string desactivar = @"DELETE FROM MusicaActual Where NumEmpleado = @NumEmpleado;";
                using (MySqlCommand cmd1 = new MySqlCommand(desactivar, conexion))
                {
                    cmd1.Parameters.AddWithValue("@NumEmpleado", numEmpleado);
                    cmd1.Parameters.AddWithValue("@IdPersonalizacion", idPersonalizacion);

                    cmd1.ExecuteNonQuery();
                }


                string activar = @"INSERT INTO MusicaActual (NumEmpleado, IdPersonalizacion) VALUES (@NumEmpleado, @IdPersonalizacion)";
                using (MySqlCommand cmd2 = new MySqlCommand(activar, conexion))
                {
                    cmd2.Parameters.AddWithValue("@NumEmpleado", numEmpleado);
                    cmd2.Parameters.AddWithValue("@IdPersonalizacion", idPersonalizacion);
                    cmd2.ExecuteNonQuery();
                }
            }
        }

        public int ObtenerIdPersonalizacion(string nombre)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                string query = "SELECT IdPersonalizacion FROM personalizacion WHERE NombreAspecto = @Nombre;";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    object resultado = cmd.ExecuteScalar();
                    return resultado != null ? Convert.ToInt32(resultado) : 0;
                }
            }
        }

        public int ObtenerIdPersonalizacionMusica(string nombre)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                string query = "SELECT IdPersonalizacion FROM MusicaPersonalizacion WHERE NombreMusica = @Nombre";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    object resultado = cmd.ExecuteScalar();
                    return resultado != null ? Convert.ToInt32(resultado) : 0;
                }
            }
        }

        public string ObtenerNombrePersonajeEquipado(int numEmpleado)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                string query = @"SELECT p.NombreAspecto
                                FROM personalizacion AS p
                                JOIN PersonajeActual AS pA ON p.IdPersonalizacion = pA.IdPersonalizacion
                                WHERE pA.NumEmpleado = @NumEmpleado AND pA.IdPersonalizacion  = p.IdPersonalizacion
                                LIMIT 1;";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@NumEmpleado", numEmpleado);
                    object resultado = cmd.ExecuteScalar();
                    return resultado != null ? resultado.ToString() : null;
                }
            }
        }
        
        public string ObtenerNombreTrackEquipado(int numEmpleado)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                string query = @"SELECT pm.NombreMusica
                                FROM MusicaPersonalizacion AS pm
                                JOIN MusicaActual AS mA ON pm.IdPersonalizacion = mA.IdPersonalizacion
                                WHERE mA.NumEmpleado = @NumEmpleado AND mA.IdPersonalizacion  = pm.IdPersonalizacion
                                LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@NumEmpleado", numEmpleado);
                    object resultado = cmd.ExecuteScalar();
                    return resultado != null ? resultado.ToString() : null;
                }
            }
        }

    }

}

