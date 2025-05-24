using System;
public class Usuarios
{
	public int NumEmpleado { get; set; }
	public string Nombre { get; set; }
	public string ApellidoP { get; set; }
	public string ApellidoM { get; set; }
	public DateTime FechaNacimiento { get; set; }
	public DateTime FechaRegistro { get; set; }
	public int IdGenero { get; set; }
	public int IdTipoPuesto { get; set; }
	public int IdEstatus { get; set; }
	
	public int Edad
    {
        get
        {
            var hoy = DateTime.Today;
            int edad = hoy.Year - FechaNacimiento.Year;
            if (FechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
            return edad;
        }
    }

}