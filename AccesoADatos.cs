
public abstract class AccesoADatos
{
   
   public abstract bool Existe(string nombreArchivo);
   public abstract List<Cadete> LeerCadetes(string nombreArchivo);
   public abstract Cadeteria LeerCadeteria(string nombreArchivo);
   public abstract void GuardarCadetes(List<Cadete> cadetes, string archivo);
   public abstract void GuardarCadeteria(Cadeteria cadeteria, string archivo);
}