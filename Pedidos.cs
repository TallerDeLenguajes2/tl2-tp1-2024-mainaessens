public class Pedidos{
    private int nro; 
    private string obs;
    private Cliente cliente; 
    private Estados estado;
    private Cadete cadete; //agregamos la referencia al cadete

    public int Nro { get => nro;}
    public string Obs { get => obs;}
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public Estados Estado { get => estado; set => estado = value; }
    public Cadete Cadete { get => cadete; set => cadete = value; }

    public Pedidos(int numero, string observacion, Cliente cliente){
        nro = numero; 
        obs = observacion; 
        Estado = Estados.Preparacion; 
        cadete = null; //inicialmente, sin cadete asignado
        this.cliente = cliente; 
    }

    public void VerDireccionCliente(){
        Console.WriteLine($"Dirección de entrega: {cliente.Direccion}");
        if (cliente.DatosReferenciaDireccion != null)
        {
            Console.WriteLine($"Referencias: {cliente.DatosReferenciaDireccion}");
        }
    }

    public void VerDatosCliente(){
        Console.WriteLine($"Cliente: {cliente.Nombre}"); 
        Console.WriteLine($"Direccion: {cliente.Direccion}"); 
        Console.WriteLine($"Telefono: {cliente.Telefono}"); 

    }
} 

public enum Estados {
    Preparacion, 
    EnCamino, 
    Entregado
}


