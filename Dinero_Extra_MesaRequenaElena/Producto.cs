public class Producto
{
    public string Nombre { get; private set; }
    public double Precio { get; private set; }
    public string Enlace { get; private set; }

    public Producto(string nombre, double precio, string enlace)
    {
        Nombre = nombre;
        Precio = precio;
        Enlace = enlace;
    }

    public override string ToString()
    {
        return $"{Nombre} - {Precio:F2} € ({Enlace})";
    }
}
