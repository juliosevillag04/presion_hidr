/* Almacenar 15 registros de mediciones hidrostáticas */
using System;
using System.IO;

using System.Globalization; // Permite usar configuraciones culturales específicas en el programa.

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture; // Establece la cultura del programa para usar el punto como separador decimal.
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture; // Aplica la misma configuración cultural a los mensajes y formatos del programa.

Console.Clear();

Medicion[] mediciones = new Medicion[15];
int i = 0;

string rutaDescargas = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
    "Downloads",
    "mediciones_hidrostaticas.csv"
);

int menu()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("=========================================");
    Console.WriteLine("  LABORATORIO DE PRESIÓN HIDROSTÁTICA");
    Console.WriteLine("=========================================");
    Console.ResetColor();

    Console.WriteLine("Menu de opciones:");
    Console.WriteLine("1. Agregar medición");
    Console.WriteLine("2. Mostrar mediciones");
    Console.WriteLine("3. Eliminar medición");
    Console.WriteLine("4. Guardar archivo");
    Console.WriteLine("5. Salir");
    Console.Write("Digita tu opción (1-5): ");

    if (int.TryParse(Console.ReadLine(), out int opcion))
    {
        return opcion;
    }

    return 0;
}

void mostrarTitulo(string titulo) // Función reutilizable para mostrar títulos con el mismo formato visual en cada sección del programa.
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("=========================================");
    Console.WriteLine($"  {titulo}");
    Console.WriteLine("=========================================");
    Console.ResetColor();
}

void pausa() // Función reutilizable para detener la pantalla, permitir que el usuario lea la información y luego volver al menú.
{
    Console.WriteLine();
    Console.Write("Pulsa una tecla para volver al menú.");
    Console.ReadKey();
    Console.Clear();
}

void pedirDatos()
{
    if (i >= 15)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No hay más espacio. Límite de 15 mediciones alcanzado.");
        Console.ResetColor();
        return;
    }

    Console.WriteLine($"\nMedición #{i + 1} de 15");

    /*
    Datos a recopilados en laboratorio:
    Concepto           |  Letra   | Unidad de medida
    ===================================================
    Brazo de palanca   |    Iw    | mm
    Fuerza en peso     |    Fw    | N
    Nivel del agua     |    s     | mm
    */

    Console.Write("Distancia a pesas - Iw (mm): ");
    while (!double.TryParse(Console.ReadLine(), out mediciones[i].Iw) || mediciones[i].Iw < 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error. Ingresa un número positivo.");
        Console.ResetColor();
        Console.Write("Distancia a pesas - Iw (mm): ");
    }

    Console.Write("Fuerza de pesas - Fw (N): ");
    while (!double.TryParse(Console.ReadLine(), out mediciones[i].Fw) || mediciones[i].Fw < 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error. Ingresa un número positivo.");
        Console.ResetColor();
        Console.Write("Fuerza de pesas - Fw (N): ");
    }

    Console.Write("Nivel del agua - s (mm): ");
    while (!double.TryParse(Console.ReadLine(), out mediciones[i].s) || mediciones[i].s < 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error. Ingresa un número positivo.");
        Console.ResetColor();
        Console.Write("Nivel del agua - s (mm): ");
    }

    double s = mediciones[i].s;

    double rho = 1000.0; // kg/m^3
    double g = 9.81;    // m/s^2
    double b = 0.075;   // m
    double L = 0.1;     // m

    /*
    Incógnitas a encontrar:
    Concepto             |  Letra  | Unidad de medida
    ===================================================
    Fuerza sustitutiva   |    Fp   | N
    Brazo de palanca     |    Ip   | mm
    Momento hidrostático |    Mp   | N-mm
    Momento antagónico   |    Mw   | N-mm
    Error porcentual     | Error % | %
    */

    if (s <= 100)
    {
        mediciones[i].Fp = rho * g * (Math.Pow(s, 2) / 2000000.0) * b;
        mediciones[i].Ip = 200.0 - (s / 3.0);
    }
    else
    {
        mediciones[i].Fp = rho * g * ((s / 1000.0) - 0.05) * L * b;
        mediciones[i].Ip = 150.0 + ((1.0 / 12.0) * (10000.0 / (s - 50.0)));
    }

    mediciones[i].Mp = mediciones[i].Fp * mediciones[i].Ip;
    mediciones[i].Mw = mediciones[i].Fw * mediciones[i].Iw;

    if (mediciones[i].Mw != 0)
    {
        mediciones[i].error = Math.Abs((mediciones[i].Mw - mediciones[i].Mp) / mediciones[i].Mw) * 100.0;
    }
    else
    {
        mediciones[i].error = 0;
    }

    i++;

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\nDatos calculados e ingresados correctamente.");
    Console.ResetColor();
}

void mostrarDatos()
{
    if (i == 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No hay mediciones registradas.");
        Console.ResetColor();
        return;
    }

    Console.WriteLine("N° | Fp(N)  | Ip(mm) | Mp(N-mm) | Mw(N-mm) | %Error");
    Console.WriteLine("----------------------------------------------------------");

    for (int cont = 0; cont < i; cont++)
    {
        Console.WriteLine(
            $"{cont + 1} | " +
            $"{mediciones[cont].Fp,6:F3} | " +
            $"{mediciones[cont].Ip,6:F2} | " +
            $"{mediciones[cont].Mp,8:F2} | " +
            $"{mediciones[cont].Mw,8:F2} | " +
            $"{mediciones[cont].error,6:F2}%"
        );
    }
}

void eliminarMedicion()
{
    if (i == 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No hay mediciones registradas para eliminar.");
        Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Operación cancelada.");
            Console.ResetColor();
            return;
        }

        int indice = numABorrar - 1;

        for (int cont = indice; cont < i - 1; cont++)
        {
            mediciones[cont] = mediciones[cont + 1];
        }

        i--;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n¡Medición eliminada correctamente de la memoria!");
        Console.ResetColor();
        Console.WriteLine("Nota: Recuerda seleccionar 'Guardar archivo' para actualizar el CSV.");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nError. Número de medición inválido.");
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
                $"{mediciones[cont].Mw};{mediciones[cont].error}"
            );
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Registro guardado exitosamente en:\n{rutaDescargas}");
        Console.ResetColor();
    }
    catch (Exception e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"ERROR al guardar el archivo: {e.Message}");
        Console.ResetColor();
    }
}

void leerArchivo()
{
    try
    {
        if (!File.Exists(rutaDescargas))
        {
            return;
        }

        using StreamReader archivo = new StreamReader(rutaDescargas);

        string? linea = archivo.ReadLine();

        if (linea != null && !linea.StartsWith("Iw;"))
        {
            procesarLineaArchivo(linea);
        }

        while ((linea = archivo.ReadLine()) != null && i < 15)
        {
            procesarLineaArchivo(linea);
        }
    }
    catch
    {
        // Si el archivo está vacío, dañado o con formato incorrecto, se ignora para no detener el programa.
    }
}

void procesarLineaArchivo(string linea)
{
    if (i >= 15)
    {
        return;
    }

    string[] dato = linea.Split(';');

    if (dato.Length >= 8 &&
        double.TryParse(dato[0], out mediciones[i].Iw) &&
        double.TryParse(dato[1], out mediciones[i].Fw) &&
        double.TryParse(dato[2], out mediciones[i].s) &&
        double.TryParse(dato[3], out mediciones[i].Fp) &&
        double.TryParse(dato[4], out mediciones[i].Ip) &&
        double.TryParse(dato[5], out mediciones[i].Mp) &&
        double.TryParse(dato[6], out mediciones[i].Mw) &&
        double.TryParse(dato[7], out mediciones[i].error))
    {
        i++;
    }
}

void mostrarCreditos()
{
    Console.Clear();

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("=========================================");
    Console.WriteLine("  Elaborado por:");
    Console.WriteLine("=========================================");
    Console.ResetColor();

    Console.WriteLine("Ronaldo Jeshua Castillo Cuadra");
    Console.WriteLine("Mariela Fernanda Hurtado Duarte");
    Console.WriteLine("Nijeri Iveth Jarquín García");
    Console.WriteLine("Julio Javier Sevilla Gallegos");
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
                mostrarTitulo("INGRESAR DATOS");
                pedirDatos();
                pausa();
                break;

            case 2:
                Console.Clear();
                mostrarTitulo("DATOS INGRESADOS");
                mostrarDatos();
                pausa();
                break;

            case 3:
                Console.Clear();
                mostrarTitulo("ELIMINAR MEDICIÓN");
                eliminarMedicion();
                pausa();
                break;

            case 4:
                Console.Clear();
                mostrarTitulo("GUARDAR ARCHIVO");
                guardarArchivo();
                pausa();
                break;

            case 5:
                mostrarCreditos();
                break;

            default:
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opción inválida.");
                Console.ResetColor();
                pausa();
                break;
        }

    } while (op != 5);
}

main();

struct Medicion
{
    public double Iw;
    public double Fw;
    public double s;
    public double Fp;
    public double Ip;
    public double Mp;
    public double Mw;
    public double error;
}

