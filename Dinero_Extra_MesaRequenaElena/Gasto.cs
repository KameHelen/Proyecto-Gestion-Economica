public class Gasto : Dinero
{
    public Gasto(double cantidad, string descripcion = "") : base(cantidad, descripcion)
    {
        if (cantidad <= 0)
            throw new ArgumentException("El gasto debe ser un número positivo.");
    }

    public override string ToString()
    {
        return $"Gasto: {Cantidad:F2} € - {Descripcion}";
    }
}