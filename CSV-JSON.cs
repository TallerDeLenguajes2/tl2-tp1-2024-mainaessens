using System.Text.Json;

public class AccesoCSV : AccesoADatos{


    public AccesoCSV()
    {
       
    }

    private string getRuta(string nombreArchivo) => "csv/" + nombreArchivo + ".csv";
    public override bool Existe(string nombreArchivo){
        return File.Exists(getRuta(nombreArchivo)); 
    }

    public override List<Cadete> LeerCadetes(string nombreArchivo){
       
        if (!Existe(getRuta(nombreArchivo))) new List<Cadete>(); 

        List<Cadete> cadetes = new List<Cadete>(); 
        using (var archivoOpen = new FileStream(getRuta(nombreArchivo), FileMode.Open)){
            using(var strReader = new StreamReader(archivoOpen)){
                string linea;
                while((linea = strReader.ReadLine()) != null){
                    var datos = linea.Split(";"); 
                    var cadete = new Cadete(int.Parse(datos[0]), datos[1], datos[2], datos[3]); 
                    cadetes.Add(cadete); 
                }
            }
        }
        return cadetes; 
    }

    public override Cadeteria LeerCadeteria(string nombreArchivo){
        string informacionCadeteria; 
        Cadeteria cadeteria;
        using(var archivoOpen = new FileStream(getRuta(nombreArchivo), FileMode.Open)){
            using(var strReader = new StreamReader(archivoOpen)){
                informacionCadeteria = strReader.ReadToEnd(); 
                var datosCadeteria = informacionCadeteria.Split(";");
                cadeteria = new Cadeteria(datosCadeteria[0],datosCadeteria[1]);
                archivoOpen.Close(); 
            }
        }
        return cadeteria; 
    }

    public override void GuardarCadetes(List<Cadete> cadetes, string archivo)
    {
        using (StreamWriter writer = new StreamWriter(archivo))
        {
            writer.WriteLine("ID,Nombre,Direccion,Telefono"); // Encabezado del archivo CSV
            foreach (var cadete in cadetes)
            {
                writer.WriteLine($"{cadete.Id},{cadete.Nombre},{cadete.Direccion},{cadete.Telefono}");
            }
        }
        Console.WriteLine("Cadetes guardados exitosamente en CSV.");
    }

    public override void GuardarCadeteria(Cadeteria cadeteria, string archivo)
    {
        using (StreamWriter writer = new StreamWriter(archivo))
        {
            writer.WriteLine($"{cadeteria.Nombre};{cadeteria.Telefono}");
        }
        Console.WriteLine("Cadeteria guardada exitosamente en CSV.");

    }
}

public class AccesoJson : AccesoADatos
{
    private string getRuta(string nombreArchivo) => "json/" + nombreArchivo + ".json";

    public AccesoJson()
    {
        
    }

    public override bool Existe(string nombreArchivo)
    {
        return File.Exists(getRuta(nombreArchivo)); 
    }

    public override Cadeteria LeerCadeteria(string nombreArchivo)
    {
        string json = File.ReadAllText(getRuta(nombreArchivo)); 
        return JsonSerializer.Deserialize<Cadeteria>(json); 
    }

    public override List<Cadete> LeerCadetes(string nombreArchivo)
    {
        string json = File.ReadAllText(getRuta(nombreArchivo)); 
        return JsonSerializer.Deserialize<List<Cadete>>(json); 
    }

    public override void GuardarCadetes(List<Cadete> cadetes, string nombreArchivo)
    {
        string json = JsonSerializer.Serialize(cadetes, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(getRuta(nombreArchivo), json);
        Console.WriteLine("Cadetes guardados exitosamente en JSON.");
    }

    public override void GuardarCadeteria(Cadeteria cadeteria, string nombreArchivo)
    {
        string json = JsonSerializer.Serialize(cadeteria, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(getRuta(nombreArchivo), json);
        Console.WriteLine("Cadeteria guardada exitosamente en JSON.");
    }
}