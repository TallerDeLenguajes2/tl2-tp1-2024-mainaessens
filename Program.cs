using Sistema; 
string nombreArchivoCadetes= "cadetes.csv"; 
string nombreArchivoCadeteria = "cadeteria.csv"; 
CSV  csv = new CSV(); 

if(csv.Existe(nombreArchivoCadetes) && csv.Existe(nombreArchivoCadeteria)){
    int operacion; 
    List<Pedidos> pedidoSinAsignar = new List<Pedidos>(); 
    int numeroPedido = 0;  
    List<Cadete> cadetes = csv.LeerCadetes(nombreArchivoCadetes); 
    string[] infoCadeteria = csv.LeerCadeteria(nombreArchivoCadeteria).Split(";"); 
    Cadeteria cadeteria  = new Cadeteria(infoCadeteria[0], infoCadeteria[1], cadetes); 

    do{
        Menu menu = new Menu($"Cadeteria {cadeteria.Nombre}--{cadeteria.Telefono}", ["Dar pedido de alta", "Asignar pedido", "Cambiar estado del pedido", "Reasignar pedido", "cerrar"]); 
        operacion = menu.MenuDisplay(); 
        switch(operacion){
            case 0: 
                numeroPedido++;  
                Pedidos pedidoNuevo = Funciones.DarDeAltaPedido(numeroPedido); 
                pedidoSinAsignar.Add(pedidoNuevo); 
                Console.ReadKey(); 
                break; 
            case 1: 
                if(pedidoSinAsignar.Count != 0){ 
                    Console.WriteLine("El pedido a asignar es el siguiente: "); 
                    Funciones.MostrarPedido(pedidoSinAsignar[0]); 
                    Console.ReadKey(); 
                    cadeteria.AsignarPedido(pedidoSinAsignar[0]); 
                    pedidoSinAsignar.RemoveAt(0); 
                }else{
                    Console.WriteLine("No hay pedidos para asignar"); 
                }
                break; 
            case 2: 
                string num; 
                int numeroIngresado; 
                do{
                    Console.WriteLine("Ingrese el numero de pedido cuyo estado desea modificar: "); 
                    num = Console.ReadLine(); 
                }while(!int.TryParse(num, out numeroIngresado)); 
                cadeteria.CambiarEstadoDelPedido(numeroIngresado); 
                Console.ReadKey(); 
                break; 
            case 3: 
                Console.WriteLine("Pedidos disponibles para reasignar"); 
                string ingreso; 
                int numPedido;
                Funciones.MostrarPedidosSinEntregar(cadeteria); 
                do{
                    Console.WriteLine("Ingrese el numero del pedido que desea reasignar: "); 
                    ingreso = Console.ReadLine(); 
                }while(!int.TryParse(ingreso, out numPedido)); 
                cadeteria.ReasignarPedido(numPedido); 
                Console.ReadKey();
                break;  
        }
    }while(operacion != 4); 
}else{
    Console.WriteLine("No se encontró la información de la cadetería"); 
}

