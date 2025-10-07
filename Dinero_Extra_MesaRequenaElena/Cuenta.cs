
public class Cuenta
{
    public Usuario Usuario { get; private set; }
    public List<Dinero> Movimientos { get; private set; }

    public Cuenta(Usuario usuario)
    {
        Usuario = usuario;
        Movimientos = new List<Dinero>();
    }

    public void AddIngreso(Ingreso ingreso)
    {
        if (ingreso == null)
            throw new ArgumentNullException(nameof(ingreso));
        Movimientos.Add(ingreso);
    }

    public void AddGastoBasico(string descripcion, double cantidad)
    {
        var gasto = new GastoBasico(cantidad, descripcion);
        gasto.SetFecha(DateTime.Now);
        Movimientos.Add(gasto);
    }

    public void AddGastoExtra(string descripcion, double cantidad, bool prescindible = false)
    {
        var gasto = new GastoExtra(cantidad, descripcion, prescindible);
        gasto.SetFecha(DateTime.Now);
        Movimientos.Add(gasto);
    }

    //Metodo para filtrar por tipo y mes

    public List<GastoBasico> GetGastosBasicos(bool esteMes = false)
    {
        var lista = Movimientos.OfType<GastoBasico>().ToList();
        if (esteMes)
        {
            var hoy = DateTime.Today;
            int mesActual = hoy.Month;
            int añoActual = hoy.Year;
            lista = lista.Where(g => g.Fecha.Month == mesActual && g.Fecha.Year == añoActual).ToList();
        }
        return lista;
    }

    public List<GastoExtra> GetGastosExtras(bool esteMes = false)
    {
        List<GastoExtra> lista = Movimientos.OfType<GastoExtra>().ToList();
        if (esteMes)
        {
            var hoy = DateTime.Today;
            int mesActual = hoy.Month;
            int añoActual = hoy.Year;
            lista = lista.Where(g => g.Fecha.Month == mesActual && g.Fecha.Year == añoActual).ToList();
        }
        return lista;
    }

    // Calcular ahorro entre dos fechas
    public double GetAhorro(DateTime fechaInicio, DateTime fechaFin)
    {
        double totalIngresos = Movimientos
            .OfType<Ingreso>()
            .Where(i => i.Fecha >= fechaInicio && i.Fecha <= fechaFin)
            .Sum(i => i.Cantidad);

        double totalGastos = Movimientos
            .OfType<Gasto>()
            .Where(g => (g is GastoBasico gb && gb.Fecha >= fechaInicio && gb.Fecha <= fechaFin) ||
                        (g is GastoExtra ge && ge.Fecha >= fechaInicio && ge.Fecha <= fechaFin))
            .Sum(g => g.Cantidad);

        return totalIngresos - totalGastos;
    }

    // Gastos imprescindibles: básicos + extras NO prescindibles
    public double GetGastosImprescindibles(DateTime fechaInicio, DateTime fechaFin)
    {
        double basicos = GetGastosBasicos(false)
            .Where(g => g.Fecha >= fechaInicio && g.Fecha <= fechaFin)
            .Sum(g => g.Cantidad);

        double extrasNoPrescindibles = GetGastosExtras(false)
            .Where(g => !g.Prescindible && g.Fecha >= fechaInicio && g.Fecha <= fechaFin)
            .Sum(g => g.Cantidad);

        return basicos + extrasNoPrescindibles;
    }

    // Posibles ahorros del mes pasado: gastos extras prescindibles del mes anterior
    public double GetPosiblesAhorrosMesPasado()
    {
        var hoy = DateTime.Today;
        var mesPasado = hoy.AddMonths(-1);
        int mes = mesPasado.Month;
        int año = mesPasado.Year;

        var gastosExtraMesPasado = GetGastosExtras(false)
            .Where(g => g.Fecha.Month == mes && g.Fecha.Year == año && g.Prescindible)
            .Sum(g => g.Cantidad);

        return gastosExtraMesPasado;
    }

    // Calcular saldo actual
    public double CalcularSaldo()
    {
        double saldo = 0;
        foreach (var movimiento in Movimientos)
        {
            if (movimiento is Ingreso ingreso)
                saldo += ingreso.Cantidad;
            else if (movimiento is Gasto gasto)
                saldo -= gasto.Cantidad;
        }
        return saldo;
    }

    public override string ToString()
    {
        return $"Cuenta de {Usuario.Nombre} - Saldo: {CalcularSaldo():F2} €";
    }

    public List<Dinero> GetAllMovimientos()
    {
        return Movimientos.ToList();
    }
}