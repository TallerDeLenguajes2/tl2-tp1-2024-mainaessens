namespace Sistema
{
    public class Funciones
    {
        public static Pedidos DarDeAltaPedido(int nroPedido)
        {
            string observacionPedido;
            string nombreCliente;
            string direccCliente;
            string telefonoCliente;
            string referenciasCliente;

            do
            {
                Console.WriteLine("Ingrese la observación del pedido: ");
                observacionPedido = Console.ReadLine();
                Console.WriteLine("Ingrese el nombre del cliente: ");
                nombreCliente = Console.ReadLine();
                Console.WriteLine("Ingrese la dirección del cliente: ");
                direccCliente = Console.ReadLine();
                Console.WriteLine("Ingrese el teléfono del cliente: ");
                telefonoCliente = Console.ReadLine();
                Console.WriteLine("Ingrese una referencia de la dirección (Opcional): ");
                referenciasCliente = Console.ReadLine();

                if (observacionPedido.Length == 0 && nombreCliente.Length == 0 && direccCliente.Length == 0 && telefonoCliente.Length == 0)
                {
                    Console.WriteLine("Debe rellenar correctamente los campos.");
                }
            } while (string.IsNullOrWhiteSpace(observacionPedido) || string.IsNullOrWhiteSpace(nombreCliente) || string.IsNullOrWhiteSpace(direccCliente) || string.IsNullOrWhiteSpace(telefonoCliente));

            Cliente nuevoCliente = new Cliente(nombreCliente, direccCliente, telefonoCliente, referenciasCliente);
            Pedidos pedidoNuevo = new Pedidos(nroPedido, observacionPedido, nuevoCliente);
            return pedidoNuevo;
        }

        public static void MostrarPedido(Pedidos pedido)
        {
            if (pedido != null)
            {
                Console.WriteLine($"Pedido Nro: {pedido.Nro}");
                Console.WriteLine($"Observaciones: {pedido.Obs}");
                Console.WriteLine($"Estado: {pedido.Estado}");
                pedido.VerDatosCliente();  // Mostrar datos del cliente asociados al pedido
                pedido.VerDireccionCliente();  // Mostrar dirección del cliente
            }
            else
            {
                Console.WriteLine("El pedido no existe.");
            }
        }

        public static void MostrarPedidosSinEntregar(Cadeteria cadeteria)
        {
            foreach (var pedido in cadeteria.ListadoPedidos)
            {
                if (pedido.Estado != Estados.Entregado)
                {
                    MostrarPedido(pedido);
                }
                else
                {
                    Console.WriteLine("No hay pedidos sin entregar.");
                }
            }
        }
    }
}
