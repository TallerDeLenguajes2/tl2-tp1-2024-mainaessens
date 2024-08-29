public class Cliente {
    private string nombre; 
    private string direccion; 
    private string telefono; 
    private string datosReferenciaDireccion;

    public string Nombre { get => nombre;}
    public string Direccion { get => direccion;}
    public string Telefono { get => telefono;}
    public string DatosReferenciaDireccion { get => datosReferenciaDireccion;}

    public Cliente(string nombre, string direccion, string telefono, string referencias)
    {
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        datosReferenciaDireccion = referencias;
    }
}