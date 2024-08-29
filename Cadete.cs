using System.Reflection; 
public class Cadete{
    private int id; 
    private string nombre; 
    private string direccion; 
    private string telefono;
    private List<Pedidos> listadoPedidos;

    public int Id { get => id;}
    public string Nombre { get => nombre;}
    public string Direccion { get => direccion;}
    public string Telefono { get => telefono;}
    public List<Pedidos> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

    public Cadete(int id, string nombre, string direccion, string telefono){
        this.id = id; 
        this.nombre = nombre; 
        this.direccion = direccion; 
        this.telefono = telefono; 
        ListadoPedidos = new List<Pedidos>(); 
    }
    public int CantidadDePedidosCompletados(){
        return ListadoPedidos.Count(p => p.Estado == Estados.Entregado); 
    }

    public int JornalACobrar(){
        return 500 * CantidadDePedidosCompletados(); 
    }

    public Pedidos DarDeBajaPedido(int numero){
        var pedidoQuitar = ListadoPedidos.Where(p => p.Nro == numero).ToList(); 
        ListadoPedidos.Remove(pedidoQuitar[0]); 
        return pedidoQuitar[0]; 
    }

    public void RetirarPedido(int numero){
        var pedidoQuitar = ListadoPedidos.Where(p => p.Nro == numero).ToList(); 
        pedidoQuitar[0].Estado = Estados.Entregado; 
    }

    public void CompletarPedido(int numero){
        var pedidoQuitar = ListadoPedidos.Where(p => p.Nro == numero).ToList(); 
        pedidoQuitar[0].Estado = Estados.Entregado; 
    }
}