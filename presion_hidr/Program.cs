Console.WriteLine("Presión hidrostatica");
//pruebas con datos de laboratorion y verificacionesde resultados 
void mostrarDatos()
{
    if (i == 0)
    {
        Console.WriteLine("No hay mediciones registradas.");
        return;
    }

    Console.WriteLine("{0,-4}{1,-12}{2,-12}{3,-15}{4,-15}{5,-10}",
        "N°", "Fp(N)", "Ip(mm)", "Mp(N-mm)", "Mw(N-mm)", "Error%");

    for (int cont = 0; cont < i; cont++)
    {
        Console.WriteLine("{0,-4}{1,-12:F3}{2,-12:F2}{3,-15:F2}{4,-15:F2}{5,-10:F2}",
            cont + 1,
            mediciones[cont].Fp,
            mediciones[cont].Ip,
            mediciones[cont].Mp,
            mediciones[cont].Mw,
            mediciones[cont].error);
    }
}

void eliminarMedicion()
{
    if (i == 0)
    {
        Console.WriteLine("No hay mediciones registradas para eliminar.");
        return;
    }

    mostrarDatos();

    Console.Write("\nDigita el N° de la medición que deseas eliminar: ");

    if (int.TryParse(Console.ReadLine(), out int numABorrar) &&
        numABorrar > 0 &&
        numABorrar <= i)
    {
        Console.Write("¿Seguro que deseas eliminarla? (S/N): ");
        string resp = Console.ReadLine()?.ToUpper() ?? "N";

        if (resp != "S")
        {
            Console.WriteLine("Operación cancelada.");
            return;
        }

        int indice = numABorrar - 1;

        for (int cont = indice; cont < i - 1; cont++)
        {
            mediciones[cont] = mediciones[cont + 1];
        }

        i--;
        Console.WriteLine("¡Medición eliminada correctamente!");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Número inválido.");
        Console.ResetColor();
    }
}

void guardarArchivo()
{
    try
    {
        using StreamWriter archivo = new StreamWriter(rutaDescargas);

        archivo.WriteLine("Iw;Fw;s;Fp;Ip;Mp;Mw;Error");

        for (int cont = 0; cont < i; cont++)
        {
            archivo.WriteLine(
                $"{mediciones[cont].Iw};{mediciones[cont].Fw};{mediciones[cont].s};" +
                $"{mediciones[cont].Fp};{mediciones[cont].Ip};{mediciones[cont].Mp};" +
                $"{mediciones[cont].Mw};{mediciones[cont].error}");
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Archivo guardado correctamente en:\n{rutaDescargas}");
        Console.ResetColor();
    }
    catch (Exception e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"ERROR: {e.Message}");
        Console.ResetColor();
    }
}

void leerArchivo()
{
    try
    {
        if (!File.Exists(rutaDescargas))
            return;

        using StreamReader archivo = new StreamReader(rutaDescargas);

        string? linea;

        archivo.ReadLine();

        while ((linea = archivo.ReadLine()) != null && i < 15)
        {
            string[] dato = linea.Split(';');

            if (dato.Length >= 8)
            {
                mediciones[i].Iw = double.Parse(dato[0]);
                mediciones[i].Fw = double.Parse(dato[1]);
                mediciones[i].s = double.Parse(dato[2]);
                mediciones[i].Fp = double.Parse(dato[3]);
                mediciones[i].Ip = double.Parse(dato[4]);
                mediciones[i].Mp = double.Parse(dato[5]);
                mediciones[i].Mw = double.Parse(dato[6]);
                mediciones[i].error = double.Parse(dato[7]);
                i++;
            }
        }
    }
    catch
    {
    }
}

void main()
{
    leerArchivo();

    int op;

    do
    {
        op = menu();

        switch (op)
        {
            case 1:
                Console.Clear();
                pedirDatos();
                Console.WriteLine("\nDatos ingresados correctamente.");
                Console.ReadKey();
                Console.Clear();
                break;

            case 2:
                Console.Clear();
                mostrarDatos();
                Console.ReadKey();
                Console.Clear();
                break;
