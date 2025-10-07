public abstract class Dinero
{
    public double Cantidad { get; protected set; }
    public string Descripcion { get; protected set; }

    protected Dinero(double cantidad, string descripcion = "")
    {
        Cantidad = cantidad;
        Descripcion = descripcion;
    }


    public abstract override string ToString();
}