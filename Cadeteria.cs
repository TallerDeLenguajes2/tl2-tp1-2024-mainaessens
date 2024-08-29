using System.ComponentModel.Design;

public class Cadeteria{
    private string nombre; 
    private string telefono; 
    private List<Cadete> listadoCadetes;

    public string Nombre { get => nombre;}
    public string Telefono { get => telefono; set => telefono = value; }
    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }

    public Cadeteria(string nombre, string telefono, List<Cadete> cadetes){
        this.nombre = nombre; 
        this.telefono = telefono; 
        this.ListadoCadetes = cadetes; 
    }

    public void AsignarPedido(Pedidos pedido){
        List<string> opcionesMenu = new List<string>(); 
        foreach (var cadete in listadoCadetes)
        {
            opcionesMenu.Add(cadete.Nombre); 
        }
        string[] opcionesCadetes = opcionesMenu.ToArray(); 
        Menu menuSeleccionable = new Menu("Seleccione el cadete al que asignará el pedido", opcionesCadetes); 
        int seleccion = menuSeleccionable.MenuDisplay(); 
        ListadoCadetes[seleccion].ListadoPedidos.Add(pedido); 
    }

    public void ReasignarPedido(int numero){
        var cadeteConPedido = ListadoCadetes.Where(c => c.ListadoPedidos.Any(p => p.Nro == numero)).ToList(); 
        if (cadeteConPedido.Count != 0)
        {
            var cadetesDisponibles = ListadoCadetes.Where(c => c.Nombre != cadeteConPedido[0].Nombre).ToList(); 
            List<string> opcionesMenu = new List<string>(); 
            Pedidos pedidoAReasignar = cadeteConPedido[0].DarDeBajaPedido(numero); 
            foreach (var cadete in cadetesDisponibles)
            {
                opcionesMenu.Add(cadete.Nombre); 
            }
            string[] opcionesCadetes = opcionesMenu.ToArray(); 
            Menu menuDeSeleccion= new Menu("Seleccione el cadete al que reasignará el pedido", opcionesCadetes); 
            int seleccion = menuDeSeleccion.MenuDisplay(); 
            cadetesDisponibles[seleccion].ListadoPedidos.Add(pedidoAReasignar); 
        }else{
            Console.WriteLine("El número ingresado no se corresponde con ningun pedido"); 
        }
    }

    public void CambiarEstadoDelPedido(int numero){
        var cadeteConPedido = ListadoCadetes.Where(c => c.ListadoPedidos.Any(p => p.Nro == numero)).ToList(); 
        if (cadeteConPedido.Count != 0)
        {
            Menu menuSeleccionable = new Menu("Seleccione el estado al que desea cambiar", ["En camino", "Entregado"]); 
            int seleccion = menuSeleccionable.MenuDisplay(); 
            switch (seleccion)
            {
                case 0: 
                    cadeteConPedido[0].RetirarPedido(numero); 
                    break; 
                case 1: 
                    cadeteConPedido[0].CompletarPedido(numero); 
                    break; 
            }
        }else{
            Console.WriteLine("El número ingresado no se corresponde con ningún pedido"); 
        }
    }

    public void MostrarJornalesYEvios(){
        int totalEnvios = 0; 
        foreach (var cadete in ListadoCadetes)
        {
            float pago = cadete.JornalACobrar(); 
            Console.WriteLine($"{cadete.Nombre}-${pago}"); 
            totalEnvios += cadete.CantidadDePedidosCompletados(); 
        }
        float promedioEnviosPorCadete = (float)totalEnvios/ListadoCadetes.Count; 
        Console.WriteLine($"Total-Envios: {totalEnvios}"); 
        Console.WriteLine($"Promedio de envios completado por cadete: {promedioEnviosPorCadete}"); 
    }
}