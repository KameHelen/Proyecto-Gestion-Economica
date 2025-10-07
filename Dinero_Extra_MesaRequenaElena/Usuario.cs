using System;
using System.Text.RegularExpressions;

public class Usuario
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Dni { get; private set; }

    public Usuario(string nombre, int edad)
    {
        Nombre = nombre;
        Edad = edad;
        Dni = null;
    }

    public bool SetDni(string dni)
    {
        dni = dni.ToUpper();
        string patron = @"^\d{8}-?[A-Z]$";
        if (Regex.IsMatch(dni, patron))
        {
            Dni = dni;
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"Usuario: {Nombre}, Edad: {Edad}, DNI: {Dni}";
    }
}