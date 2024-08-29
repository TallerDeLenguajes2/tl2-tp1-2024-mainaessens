public class Pedidos{
    private int nro; 
    private string obs;
    private Cliente cliente; 
    private Estados estado;

    public int Nro { get => nro;}
    public string Obs { get => obs;}
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public Estados Estado { get => estado; set => estado = value; }

    public Pedidos(int numero, string observacion, string nombre, string direccion, string telefono, string referencias){
        nro = numero; 
        obs = observacion; 
        Estado = Estados.Preparacion; 
        cliente = new Cliente(nombre, direccion, telefono,referencias); 
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

