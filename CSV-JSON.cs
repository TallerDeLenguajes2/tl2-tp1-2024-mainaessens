using System.Text.Json;

public class AccesoCSV : AccesoADatos{
    public override bool Existe(string nombreArchivo){
        string ruta = "csv/"+nombreArchivo; 
        return File.Exists(ruta); 
    }

    public override List<Cadete> LeerCadetes(string nombreArchivo){
        string ruta = "csv/"+nombreArchivo; 
        List<Cadete> cadetes = new List<Cadete>(); 
        using (var archivoOpen = new FileStream(ruta, FileMode.Open)){
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

    public override string LeerCadeteria(string nombreArchivo){
        string ruta = "csv/"+nombreArchivo; 
        string informacionCadeteria; 
        using(var archivoOpen = new FileStream(ruta, FileMode.Open)){
            using(var strReader = new StreamReader(archivoOpen)){
                informacionCadeteria = strReader.ReadToEnd(); 
                archivoOpen.Close(); 
            }
        }
        return informacionCadeteria; 
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
    public override bool Existe(string nombreArchivo)
    {
        string ruta = "json/"+nombreArchivo + ".json"; 
        return File.Exists(ruta); 
    }

    public override string LeerCadeteria(string nombreArchivo)
    {
        string json = File.ReadAllText(nombreArchivo); 
        return json; 
    }

    public override List<Cadete> LeerCadetes(string nombreArchivo)
    {
        string ruta = "json/" + nombreArchivo;
        string json = File.ReadAllText(ruta); 
        return JsonSerializer.Deserialize<List<Cadete>>(json); 
    }

    public override void GuardarCadetes(List<Cadete> cadetes, string nombreArchivo)
    {
        string ruta = "json/" + nombreArchivo;
        string json = JsonSerializer.Serialize(cadetes, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ruta, json);
        Console.WriteLine("Cadetes guardados exitosamente en JSON.");
    }

    public override void GuardarCadeteria(Cadeteria cadeteria, string nombreArchivo)
    {
        string ruta = "json/" + nombreArchivo;
        string json = JsonSerializer.Serialize(cadeteria, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ruta, json);
        Console.WriteLine("Cadeteria guardada exitosamente en JSON.");
    }
}