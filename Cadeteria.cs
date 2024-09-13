using System.Data;
using System.Runtime.Intrinsics.X86;
using System.Text.Json.Serialization;
using Sistema;

public class Cadeteria
{
    private string nombre;

    private string telefono;
    private List<Cadete> cadetes;

    private List<Pedido> pedidos;


    public string Nombre { get => nombre;}

    public string Telefono {get => telefono;}
    public List<Cadete> Cadetes { get => cadetes; set => cadetes = value;}
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        pedidos = new List<Pedido>();
    }

    public void GuardarPedido(Pedido pedido)
    {
        Pedidos.Add(pedido);
    }
    public bool AsignarCadeteAPedido(int numPedido, int idCadete)
    {
       var cadeteElegido = cadetes.Where(c => c.Id == idCadete).ToList();
       var pedidoElegido = pedidos.Where(p => p.Numero == numPedido).ToList();
       if(cadeteElegido != null && pedidoElegido != null)
       {
            pedidoElegido[0].CadeteAsignado = cadeteElegido[0];
            return true; 
       }else
       {
            return false; 
       }

    }

    public bool ReasignarPedido(int numero)
    {
        var pedidoAReasignar = Pedidos.Where(p => p.Numero == numero).ToList();
        if (pedidoAReasignar.Count != 0)
        {
            var cadetesDisponibles = cadetes.Where(c => c.Nombre != pedidoAReasignar[0].CadeteAsignado.Nombre).ToList();
            int seleccion = Funciones.ElegirCadete(cadetesDisponibles);
            pedidoAReasignar[0].CadeteAsignado = cadetesDisponibles[seleccion];
            return true; 
        }else{
            return false; 
        }
    }

    public bool CambiarEstadoDelPedido(int numero, int seleccion)
    {
        var pedidoAModificar = pedidos.Where(p => p.Numero == numero).ToList();
        if (pedidoAModificar.Count != 0)
        {
            switch (seleccion)
            {
                case 0:
                    pedidoAModificar[0].Estado = Estados.EnCamino;
                    break;
                case 1:
                    pedidoAModificar[0].Estado = Estados.Entregado;
                    break;
            }
            return true; 
        }else{
            return false; 
        }

    }

    public int CalculoPedidosCompletados(int idCadete)
    {
        var pedidosEntregados = pedidos.Where(p => p.Estado == Estados.Entregado).ToList();
        var numPedidosCompletados = pedidosEntregados.Count(p => p.CadeteAsignado.Id == idCadete);
        return numPedidosCompletados;
    }

    public float JornalACobrar(int pedidosCompletados)
    {
        return 500*pedidosCompletados;
    }

}