using Sistema;

string nombreArchivoCadetes = "Cadetes";
string nombreArchivoCadeteria = "Cadeteria";

Menu menuArchivos = new Menu("Elija el archivo con el que desea trabajar", ["Json", "CSV"]);
int eleccion = menuArchivos.MenuDisplay();


AccesoADatos accesoADatos;

if(eleccion == 0)
{
    accesoADatos = new AccesoJson();
}
else
{
    accesoADatos = new AccesoCSV();    
}

var cadetes = accesoADatos.LeerCadetes(nombreArchivoCadetes);
var cadeteria = accesoADatos.LeerCadeteria(nombreArchivoCadeteria);
cadeteria.Cadetes = cadetes;

int operacion;
int nroPedido = 0;
do
{         
    Menu menu = new Menu($"Cadeteria {cadeteria.Nombre}-{cadeteria.Telefono}", ["Dar pedido de alta", "Asignar pedido", "Cambiar estado del pedido", "Reasignar pedido", "Cerrar"]);
    operacion = menu.MenuDisplay();
    switch (operacion)
    {
            
        case 0:
            nroPedido++;
            Pedido pedidoNuevo = Funciones.DarDeAltaPedido(nroPedido);
            cadeteria.GuardarPedido(pedidoNuevo);
            Console.ReadKey();
            break;
        case 1:
            bool hayPedidos = Funciones.MostrarPedidosSinCadete(cadeteria);
            int numeroPedido;
            string ingresa;
            if(hayPedidos)
            {
                do
                {
                    Console.WriteLine("Ingrese el numero del pedido que desea asignar:");
                    ingresa = Console.ReadLine();
                } while (!int.TryParse(ingresa, out numeroPedido));
                int idCadete = Funciones.ElegirCadete(cadeteria.Cadetes);
                if(!cadeteria.AsignarCadeteAPedido(numeroPedido, idCadete))
                {
                    Funciones.MostrarMensajeDeError();
                }
                cadeteria.AsignarCadeteAPedido(numeroPedido, idCadete);
            }
            break;
        case 2:
            string num;
            int numIngresado;
            do
            {
                Console.WriteLine("Ingrese el numero de pedido cuyo estado desea modificar: ");
                num = Console.ReadLine();
            } while (!int.TryParse(num, out numIngresado));
            int seleccion = Funciones.ElegirEstado(); 
            if(!cadeteria.CambiarEstadoDelPedido(numIngresado, seleccion)){
                Funciones.MostrarMensajeDeError(); 
            }
            Console.ReadKey();
            break;
        case 3:
            Console.WriteLine("Pedidos disponibles para reasignar");
            string ingreso;
            int numPedido;
            Funciones.MostrarPedidosSinEntregar(cadeteria);
            do
            {
                Console.WriteLine("Ingrese el numero del pedido que desea reasignar:");
                ingreso = Console.ReadLine();
            } while (!int.TryParse(ingreso, out numPedido));
            if(!cadeteria.ReasignarPedido(numPedido))
            {
                Funciones.MostrarMensajeDeError();      
            }
            Console.ReadKey();
            break;
        case 4:
            Console.WriteLine("Final de Jornada-Informe");
            Funciones.MostrarJornalesYEnvios(cadeteria); 
            Console.ReadKey();
            break;
        }

} while (operacion != 4);
