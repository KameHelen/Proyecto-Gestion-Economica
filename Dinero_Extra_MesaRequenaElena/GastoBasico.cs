

public class GastoBasico : Gasto
{
   
    public DateTime Fecha { get; private set; }
    public GastoBasico(double cantidad, string descripcion = "", DateTime? fecha = null) : base(cantidad, descripcion)
    {
        Fecha = DateTime.Now;
    }

    public void SetFecha(DateTime fecha)
    {
        Fecha = fecha;
    }



    public override string ToString()
    {
        return $"Gasto Básico: {Cantidad:F2} € - {Descripcion} - Fecha: {Fecha.ToShortDateString()}";
    }

}
