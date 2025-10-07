using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        try
        {
            Run();
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("\nPrograma interrumpido por el usuario.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }
    }

    static double PedirNumero(string mensaje, bool positivo = false)
    {
        while (true)
        {
            Console.Write(mensaje);
            string input = Console.ReadLine();
            if (double.TryParse(input, out double valor))
            {
                if (positivo && valor <= 0)
                {
                    Console.WriteLine("El valor debe ser un número positivo.");
                    continue;
                }
                return valor;
            }
            Console.WriteLine("Entrada no válida. Intenta de nuevo.");
        }
    }

    static int PedirEntero(string mensaje, bool positivo = false)
    {
        while (true)
        {
            Console.Write(mensaje);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int valor))
            {
                if (positivo && valor <= 0)
                {
                    Console.WriteLine("El valor debe ser un número positivo.");
                    continue;
                }
                return valor;
            }
            Console.WriteLine("Entrada no válida. Intenta de nuevo.");
        }
    }

    static void MostrarMovimientos<T>(Cuenta cuenta, string titulo) where T : Dinero
    {
        Console.WriteLine($"\n--- {titulo} ---");
        var movimientos = cuenta.GetAllMovimientos().OfType<T>().ToList();
        if (movimientos.Count > 0)
        {
            foreach (var m in movimientos)
            {
                Console.WriteLine(m);
            }
        }
        else
        {
            Console.WriteLine($"No hay {titulo.ToLower()} registrados.");
        }
    }

    static void MostrarGastosTotales(Cuenta cuenta)
    {
        var gastos = cuenta.GetAllMovimientos().OfType<Gasto>().ToList();
        Console.WriteLine("\n--- Todos los Gastos ---");
        if (gastos.Count > 0)
        {
            foreach (var g in gastos)
            {
                Console.WriteLine(g);
            }
        }
        else
        {
            Console.WriteLine("No hay gastos registrados.");
        }
    }

    static void MostrarGastosBasicos(Cuenta cuenta, bool esteMes = false)
    {
        var gastos = cuenta.GetGastosBasicos(esteMes);
        Console.WriteLine($"\n--- Gastos Básicos{(esteMes ? " (este mes)" : "")} ---");
        if (gastos.Count > 0)
        {
            foreach (var g in gastos)
            {
                Console.WriteLine(g);
            }
        }
        else
        {
            Console.WriteLine("No hay gastos básicos registrados.");
        }
    }

    static void MostrarGastosExtras(Cuenta cuenta, bool esteMes = false)
    {
        var gastos = cuenta.GetGastosExtras(esteMes);
        Console.WriteLine($"\n--- Gastos Extras{(esteMes ? " (este mes)" : "")} ---");
        if (gastos.Count > 0)
        {
            foreach (var g in gastos)
            {
                Console.WriteLine(g);
            }
        }
        else
        {
            Console.WriteLine("No hay gastos extras registrados.");
        }
    }

    static void Run()
    {
        // Crear el usuario
        Console.Write("Introduce tu nombre: ");
        string nombre = Console.ReadLine();
        int edad = PedirEntero("Introduce tu edad: ", positivo: true);
        Usuario usuario = new Usuario(nombre, edad);

        // Validar DNI
        string dni;
        do
        {
            Console.Write("Introduce tu DNI: ");
            dni = Console.ReadLine()?.ToUpper();
            if (string.IsNullOrWhiteSpace(dni))
            {
                Console.WriteLine("DNI no válido, intenta de nuevo.");
                continue;
            }
        } while (!usuario.SetDni(dni));

        // Crear la cuenta
        Cuenta cuenta = new Cuenta(usuario);

        // Crear Wishlist
        Wishlist wishlist = new Wishlist("Mis deseos", usuario);

        while (true)
        {
            Console.WriteLine("\n===================================");
            Console.WriteLine("   REALIZA UNA NUEVA ACCIÓN");
            Console.WriteLine("===================================");
            Console.WriteLine("1. Introduce un nuevo gasto básico");
            Console.WriteLine("2. Introduce un nuevo gasto extra");
            Console.WriteLine("3. Introduce un nuevo ingreso");
            Console.WriteLine("4. Mostrar gastos");
            Console.WriteLine("5. Mostrar ingresos");
            Console.WriteLine("6. Mostrar saldo");
            Console.WriteLine("7. Mostrar ahorro de un período");
            Console.WriteLine("8. Mostrar gastos imprescindibles");
            Console.WriteLine("9. Mostrar posibles ahorros del mes pasado");
            Console.WriteLine("10. Mostrar lista de deseos");
            Console.WriteLine("11. Mostrar productos que podemos comprar");
            Console.WriteLine("12. Añadir producto a la lista de deseos");
            Console.WriteLine("0. Salir");
            Console.Write("Selecciona una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    double cantBasico = PedirNumero("Cantidad del gasto básico: ", positivo: true);
                    Console.Write("Descripción: ");
                    string descBasico = Console.ReadLine() ?? "";
                    cuenta.AddGastoBasico(descBasico, cantBasico);
                    Console.WriteLine("Gasto básico añadido.");
                    break;

                case "2":
                    double cantExtra = PedirNumero("Cantidad del gasto extra: ", positivo: true);
                    Console.Write("Descripción: ");
                    string descExtra = Console.ReadLine() ?? "";
                    Console.Write("¿Es prescindible? (s/n): ");
                    bool prescindible = Console.ReadLine().ToLower().StartsWith("s");
                    cuenta.AddGastoExtra(descExtra, cantExtra, prescindible);
                    Console.WriteLine("Gasto extra añadido.");
                    break;

                case "3":
                    double cantIngreso = PedirNumero("Cantidad del ingreso: ", positivo: true);
                    Console.Write("Descripción: ");
                    string descIngreso = Console.ReadLine() ?? "";
                    cuenta.AddIngreso(new Ingreso(cantIngreso, descIngreso)); // ✅ CORREGIDO
                    Console.WriteLine("Ingreso añadido.");
                    break;

                case "4":
                    Console.WriteLine("¿Qué quieres ver?");
                    Console.WriteLine("1. Todos los gastos");
                    Console.WriteLine("2. Gastos básicos");
                    Console.WriteLine("3. Gastos extras");
                    Console.Write("Opción: ");
                    string subOpcion = Console.ReadLine();
                    switch (subOpcion)
                    {
                        case "1":
                            MostrarGastosTotales(cuenta);
                            break;
                        case "2":
                            Console.Write("¿Solo este mes? (s/n): ");
                            bool esteMesBasico = Console.ReadLine().ToLower().StartsWith("s");
                            MostrarGastosBasicos(cuenta, esteMesBasico);
                            break;
                        case "3":
                            Console.Write("¿Solo este mes? (s/n): ");
                            bool esteMesExtra = Console.ReadLine().ToLower().StartsWith("s");
                            MostrarGastosExtras(cuenta, esteMesExtra);
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                    break;

                case "5":
                    MostrarMovimientos<Ingreso>(cuenta, "Lista de Ingresos");
                    break;

                case "6":
                    Console.WriteLine(cuenta);
                    break;

                case "7":
                    Console.Write("Fecha inicio (dd/MM/yyyy): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime inicio))
                    {
                        Console.WriteLine("Fecha no válida.");
                        break;
                    }
                    Console.Write("Fecha fin (dd/MM/yyyy): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime fin))
                    {
                        Console.WriteLine("Fecha no válida.");
                        break;
                    }
                    double ahorro = cuenta.GetAhorro(inicio, fin);
                    Console.WriteLine($"Ahorro entre {inicio:dd/MM/yyyy} y {fin:dd/MM/yyyy}: {ahorro:F2} €");
                    break;

                case "8":
                    Console.Write("Fecha inicio (dd/MM/yyyy): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime inicio8))
                    {
                        Console.WriteLine("Fecha no válida.");
                        break;
                    }
                    Console.Write("Fecha fin (dd/MM/yyyy): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime fin8))
                    {
                        Console.WriteLine("Fecha no válida.");
                        break;
                    }
                    double imprescindibles = cuenta.GetGastosImprescindibles(inicio8, fin8);
                    Console.WriteLine($"Gastos imprescindibles entre {inicio8:dd/MM/yyyy} y {fin8:dd/MM/yyyy}: {imprescindibles:F2} €");
                    break;

                case "9":
                    double posiblesAhorros = cuenta.GetPosiblesAhorrosMesPasado();
                    Console.WriteLine($"Posibles ahorros del mes pasado (gastos extras prescindibles): {posiblesAhorros:F2} €");
                    break;

                case "10":
                    Console.WriteLine(wishlist);
                    Console.WriteLine("Productos:");
                    foreach (var p in wishlist.GetProductos())
                    {
                        Console.WriteLine(p);
                    }
                    break;

                case "11":
                    var comprables = wishlist.GetProductosParaComprar(cuenta);
                    Console.WriteLine("\n--- Productos que puedes comprar ---");
                    if (comprables.Count > 0)
                    {
                        foreach (var p in comprables)
                        {
                            Console.WriteLine(p);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No puedes comprar ningún producto con tu saldo actual.");
                    }
                    break;

                case "12":
                    Console.Write("Nombre del producto: ");
                    string nombreProd = Console.ReadLine() ?? "";
                    double precioProd = PedirNumero("Precio del producto: ", positivo: true);
                    Console.Write("Enlace del producto (opcional): ");
                    string enlaceProd = Console.ReadLine() ?? "";
                    wishlist.AgregarProducto(nombreProd, precioProd, enlaceProd);
                    Console.WriteLine("Producto añadido a la lista de deseos.");
                    break;

                case "0":
                    Console.WriteLine("Fin del programa. Gracias por utilizar la aplicación.");
                    return;

                default:
                    Console.WriteLine("Opción no válida, intenta otra vez.");
                    break;
            }
        }
    }
}