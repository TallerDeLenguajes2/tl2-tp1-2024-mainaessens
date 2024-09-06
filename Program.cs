using Sistema;
using System;

class Program
{
    static void Main()
    {
        // Solicitar al usuario el tipo de acceso a datos (CSV o JSON)
        Console.WriteLine("Seleccione el tipo de acceso a datos:");
        Console.WriteLine("1. CSV");
        Console.WriteLine("2. JSON");
        int tipoAcceso;

        while (!int.TryParse(Console.ReadLine(), out tipoAcceso) || (tipoAcceso != 1 && tipoAcceso != 2))
        {
            Console.WriteLine("Opción inválida. Por favor, ingrese 1 para CSV o 2 para JSON:");
        }

        // Instanciar el objeto de acceso a datos adecuado
        AccesoADatos accesoDatos;
        string nombreArchivoCadetes, nombreArchivoCadeteria;

        if (tipoAcceso == 1)
        {
            accesoDatos = new AccesoCSV();
            nombreArchivoCadetes = "cadetes.csv";
            nombreArchivoCadeteria = "cadeteria.csv";
        }
        else
        {
            accesoDatos = new AccesoJson();
            nombreArchivoCadetes = "cadetes.json";
            nombreArchivoCadeteria = "cadeteria.json";
        }

        // Verificar si los archivos existen
        if (accesoDatos.Existe(nombreArchivoCadetes) && accesoDatos.Existe(nombreArchivoCadeteria))
        {
            int operacion;
            List<Pedidos> pedidoSinAsignar = new List<Pedidos>();
            int numeroPedido = 0;

            // Leer los datos de los archivos
            List<Cadete> cadetes = accesoDatos.LeerCadetes(nombreArchivoCadetes);
            string[] infoCadeteria = accesoDatos.LeerCadeteria(nombreArchivoCadeteria).Split(";");
            Cadeteria cadeteria = new Cadeteria(infoCadeteria[0], infoCadeteria[1], cadetes);

            do
            {
                // Mostrar el menú principal
                Menu menu = new Menu($"Cadetería {cadeteria.Nombre} -- {cadeteria.Telefono}",
                    new string[] { "Dar pedido de alta", "Asignar pedido", "Cambiar estado del pedido", "Reasignar pedido", "Mostrar jornales y envíos", "Cerrar" });
                operacion = menu.MenuDisplay();

                switch (operacion)
                {
                    case 0:
                        numeroPedido++;
                        Pedidos pedidoNuevo = Funciones.DarDeAltaPedido(numeroPedido);
                        pedidoSinAsignar.Add(pedidoNuevo);
                        Console.ReadKey();
                        break;

                    case 1:
                        if (pedidoSinAsignar.Count != 0)
                        {
                            Console.WriteLine("El pedido a asignar es el siguiente:");
                            Funciones.MostrarPedido(pedidoSinAsignar[0]);
                            cadeteria.AsignarPedido(pedidoSinAsignar[0]);
                            pedidoSinAsignar.RemoveAt(0);
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("No hay pedidos para asignar.");
                        }
                        break;

                    case 2:
                        string num;
                        int numeroIngresado;
                        do
                        {
                            Console.WriteLine("Ingrese el número de pedido cuyo estado desea modificar:");
                            num = Console.ReadLine();
                        } while (!int.TryParse(num, out numeroIngresado));
                        cadeteria.CambiarEstadoDelPedido(numeroIngresado);
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.WriteLine("Pedidos disponibles para reasignar:");
                        Funciones.MostrarPedidosSinEntregar(cadeteria);

                        string ingreso;
                        int numPedido;
                        do
                        {
                            Console.WriteLine("Ingrese el número del pedido que desea reasignar:");
                            ingreso = Console.ReadLine();
                        } while (!int.TryParse(ingreso, out numPedido));

                        cadeteria.ReasignarPedido(numPedido);
                        Console.ReadKey();
                        break;

                    case 4:
                        cadeteria.MostrarJornalesYEvios();
                        Console.ReadKey();
                        break;
                }
            } while (operacion != 5);
        }
        else
        {
            Console.WriteLine("No se encontró la información de la cadetería.");
        }
    }
}
