
public class GastoExtra : Gasto
{
    public DateTime Fecha { get; private set; }
    public bool Prescindible { get; set; }
   
    public GastoExtra(double cantidad, string descripcion = "", bool prescindible = true, DateTime? fecha = null) : base(cantidad, descripcion)
    {
        Prescindible = prescindible;
        Fecha = DateTime.Now;
    }
    public void SetFecha(DateTime fecha)
    {
        Fecha = fecha;
    }
    public void SetPrescindible(bool prescindible)
    {
        Prescindible = prescindible;
    }

    public override string ToString()
    {
        return $"Gasto Extra: {Cantidad:F2} € - {Descripcion} - Fecha: {Fecha.ToShortDateString()} - Prescindible: {Prescindible}";
    }

}
