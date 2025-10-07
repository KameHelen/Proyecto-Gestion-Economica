
public class Wishlist
{
    public string Nombre { get; private set; }
    public List<Producto> Productos { get; private set; }
    public Usuario Usuario { get; private set; }

    public Wishlist(string nombre, Usuario usuario)
    {
        Nombre = nombre;
        Usuario = usuario;
        Productos = new List<Producto>();
    }

    // Método existente
    public void AddProducto(Producto producto)
    {
        Productos.Add(producto);
    }
    public void AgregarProducto(string nombre, double precio, string enlace)
    {
        var producto = new Producto(nombre, precio, enlace);
        AddProducto(producto);
    }

    public List<Producto> GetProductos()
    {
        return Productos.ToList();
    }

    public Usuario GetUsuario()
    {
        return Usuario;
    }

    // Devuelve productos que podemos comprar ahora (individualmente)
    public List<Producto> GetProductosParaComprar(Cuenta cuenta)
    {
        var saldoActual = cuenta.CalcularSaldo();

        // Calcular gastos básicos del próximo mes (simplificado: sumar todos los básicos del último mes)
        var gastosBasicosUltimoMes = cuenta.GetGastosBasicos(false)
            .Where(g => g.Fecha >= DateTime.Today.AddMonths(-1))
            .Sum(g => g.Cantidad);

        // Suponemos que los gastos básicos del próximo mes serán similares
        double gastosBasicosProximoMes = gastosBasicosUltimoMes > 0 ? gastosBasicosUltimoMes : 100; // fallback

        var productosComprables = new List<Producto>();

        foreach (var producto in Productos)
        {
            if (saldoActual - producto.Precio >= gastosBasicosProximoMes)
            {
                productosComprables.Add(producto);
            }
        }

        return productosComprables;
    }

    public override string ToString()
    {
        return $"Lista de Deseos: {Nombre} ({Productos.Count} productos)";
    }
}
