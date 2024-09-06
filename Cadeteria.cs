public class Cadeteria{
    private string nombre; 
    private string telefono; 
    private List<Cadete> listadoCadetes;
    private List<Pedidos> listadoPedidos; //agrego listado pedidos

    public string Nombre { get => nombre;}
    public string Telefono { get => telefono; set => telefono = value; }
    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
    public List<Pedidos> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

    public Cadeteria(string nombre, string telefono, List<Cadete> cadetes){
        this.nombre = nombre; 
        this.telefono = telefono; 
        listadoCadetes = cadetes; 
        listadoPedidos = new List<Pedidos>(); //inicializo el listado de pedidos
    }

    public void AsignarPedido(Pedidos pedido){
        listadoPedidos.Add(pedido); //agrego el pedido al listado de pedidos 
    }

    public void AsignarCadeteAPedido(int idCadete, int idPedido){
        var pedido = listadoPedidos.FirstOrDefault(p => p.Nro == idPedido); 
        var cadete = listadoCadetes.FirstOrDefault(c => c.Id == idCadete); 

        if(pedido != null && cadete != null){
            pedido.Cadete = cadete; //asigno el cadete al pedido
            Console.WriteLine($"Pedido {pedido.Nro} asignado a {cadete.Nombre}");
        }else{
            Console.WriteLine("Error en la asignación del pedido."); 
        }
    }

    public int PedidosCompletados(int idCadete)
    {
        var cadete = listadoCadetes.FirstOrDefault(c => c.Id == idCadete);
        if (cadete != null)
        {
            return listadoPedidos.Count(p => p.Cadete == cadete && p.Estado == Estados.Entregado);
        }
        return 0;
    }
    public int JornalCobrar(int idCadete){
       Cadete cadete = listadoCadetes.Find(c => c.Id == idCadete); 
       if(cadete != null){
        int pedidosEntregados = PedidosCompletados(idCadete); 
        return pedidosEntregados * 500; //monto fijo por pedido entregado
       }
       return 0; 
    }

    public void ReasignarPedido(int numero){
        var pedido = listadoPedidos.FirstOrDefault(p => p.Nro == numero); 
        if(pedido != null){
            pedido.Cadete = null; //saco el cadete del pedido
            Console.WriteLine("Pedido reasignado a ningún cadete. Puede asignarlo nuevamente"); 
        }else {
            Console.WriteLine("El numero ingresado no se corresponde con ningún pedido"); 
        }
    }

    public void CambiarEstadoDelPedido(int numero){
        var pedido = listadoPedidos.FirstOrDefault(p => p.Nro == numero);  
        if (pedido != null && pedido.Cadete != null)
        {
            Menu menuSeleccionable = new Menu("Seleccione el estado al que desea cambiar", ["En camino", "Entregado"]); 
            int seleccion = menuSeleccionable.MenuDisplay(); 
            switch (seleccion)
            {
                case 0: 
                    pedido.Estado = Estados.EnCamino;  
                    break; 
                case 1: 
                    pedido.Estado = Estados.Entregado; 
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
            float pago = JornalCobrar(cadete.Id); 
            Console.WriteLine($"{cadete.Nombre}-${pago}"); 
            totalEnvios += PedidosCompletados(cadete.Id); 
        }
        float promedioEnviosPorCadete = (float)totalEnvios/ListadoCadetes.Count; 
        Console.WriteLine($"Total-Envios: {totalEnvios}"); 
        Console.WriteLine($"Promedio de envios completado por cadete: {promedioEnviosPorCadete}"); 
    }
}