public class Ingreso : Dinero
{

    public DateTime Fecha { get; private set; }
    public Ingreso(double cantidad, string descripcion = "") : base(cantidad, descripcion)
    {
        if (cantidad <= 0)
            throw new ArgumentException("El ingreso debe ser un número positivo.");
    }
    public void SetFecha(DateTime fecha)
    {
        Fecha = fecha;
    }

    public override string ToString()
    {
        return $"Ingreso: {Cantidad:F2} € - {Descripcion}";
    }
}