public class Pedidos{
    private int nro; 
    private string obs;
    private Cliente cliente; 
    private Estados estado;

    public int Nro { get => nro; set => nro = value; }
    public string Obs { get => obs; set => obs = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public Estados Estado { get => estado; set => estado = value; }

    public Pedidos(int numero, string observacion, string nombre, string direccion, string telefono, string referencias){
        nro = numero; 
        obs = observacion; 
        Estado = Estados.Preparacion; 
        cliente = new Cliente(nombre, direccion, telefono,referencias); 
    }

    public void VerDireccionCliente(){

    }

    public void VerDatosCliente(){

    }
} 

public enum Estados {
    Preparacion, 
    EnCamino, 
    Entregado
}

