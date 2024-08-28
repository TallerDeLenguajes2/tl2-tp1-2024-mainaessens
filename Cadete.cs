using System.Reflection; 
public class Cadete{
    private int id; 
    private string nombre; 
    private string direccion; 
    private long telefono;
    private List<Pedidos> ListadoPedidos;

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public long Telefono { get => telefono; set => telefono = value; }
    public List<Pedidos> ListadoPedidos1 { get => ListadoPedidos; set => ListadoPedidos = value; }

    public int CantidadDePedidosCompletados(){
        return ListadoPedidos.Count(p => p.Estado == Estados.Entregado); 
    }

    public int JornalACobrar(){
        return 500 * CantidadDePedidosCompletados(); 
    }

    public Pedidos DarDeBajaPedido(int numero){
        var pedidoQuitar = Pedidos.Where(p => p.numero == numero).ToList(); 
        return pedidoQuitar[0]; 
    }
}