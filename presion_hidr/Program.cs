Console.WriteLine("Presión hidrostatica");

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
    Console.WriteLine("===== MENÚ HIDROSTÁTICO =====");
    Console.ResetColor();
    Console.WriteLine("1. Agregar medición");
    Console.WriteLine("2. Mostrar mediciones");
    Console.WriteLine("3. Eliminar medición");
    Console.WriteLine("4. Guardar archivo");
    Console.WriteLine("5. Salir");
    Console.Write("Digita tu opción: ");

    if (int.TryParse(Console.ReadLine(), out int opcion))
        return opcion;

    return 0;
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

            case 3:
                Console.Clear();
                eliminarMedicion();
                Console.ReadKey();
                Console.Clear();
                break;

            case 4:
                guardarArchivo();
                Console.ReadKey();
                Console.Clear();
                break;

            case 5:
                Console.WriteLine("Adiós...");
                break;

            default:
                Console.WriteLine("Opción inválida.");
                Console.ReadKey();
                Console.Clear();
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

