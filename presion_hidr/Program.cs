using System;

//Guia de trabajo

const int cantidad_maxima = 15;

Medicion[] mediciones = new Medicion[cantidad_maxima];
int cantidad_mediciones = 0;

string ruta_archivo = System.IO.Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
    "Downloads",
    "mediciones_hidrostaticas.csv"
);

leer_archivo();

int opcion;

do
{
    Console.Clear();
    opcion = menu();

    switch (opcion)
    {
        case 1:
            Console.Clear();
            agregar_medicion();
            pausar();
            break;

        case 2:
            Console.Clear();
            mostrar_mediciones();
            pausar();
            break;

        case 3:
            Console.Clear();
            eliminar_medicion();
            pausar();
            break;

        case 4:
            Console.Clear();
            guardar_archivo();
            pausar();
            break;

        case 5:
            Console.Clear();
            Console.WriteLine("Saliendo del sistema...");
            break;
    }

} while (opcion != 5);

int menu()
{
    int opcion_menu;

    Console.WriteLine("===== MENÚ HIDROSTÁTICO =====");
    Console.WriteLine("1. Agregar medición");
    Console.WriteLine("2. Mostrar mediciones");
    Console.WriteLine("3. Eliminar medición");
    Console.WriteLine("4. Guardar archivo");
    Console.WriteLine("5. Salir");
    Console.Write("Digite una opción: ");

    string entrada = Console.ReadLine() ?? "";

    while (!int.TryParse(entrada, out opcion_menu) || opcion_menu < 1 || opcion_menu > 5)
    {
        Console.WriteLine("Error. Debe ingresar una opción entre 1 y 5.");
        Console.Write("Digite una opción: ");
        entrada = Console.ReadLine() ?? "";
    }

    return opcion_menu;
}

// =======================================================
// INTEGRANTE 2: JULIO JAVIER SEVILLA GALLEGOS
// Rama sugerida: feature/julio-agregar
// Función: registrar Iw, Fw y s con validaciones.
// =======================================================

void agregar_medicion()
{
    Console.WriteLine("===== AGREGAR MEDICIÓN =====");

    if (cantidad_mediciones >= cantidad_maxima)
    {
        Console.WriteLine("No hay espacio disponible. El límite es de 15 mediciones.");
        return;
    }

    Console.WriteLine("Módulo pendiente de desarrollar por Julio.");
    Console.WriteLine("Aquí se deben pedir los datos: Iw, Fw y s.");
    Console.WriteLine("Luego se debe llamar a calcular_resultados().");

    /*
    Ejemplo de lo que debe implementar Julio:

    Medicion nueva_medicion = new Medicion();

    nueva_medicion.Iw = leer_double_positivo("Digite Iw en mm: ");
    nueva_medicion.Fw = leer_double_positivo("Digite Fw en N: ");
    nueva_medicion.s = leer_double_positivo("Digite s en mm: ");

    calcular_resultados(ref nueva_medicion);

    mediciones[cantidad_mediciones] = nueva_medicion;
    cantidad_mediciones++;

    Console.WriteLine("Medición agregada correctamente.");
    */
}

double leer_double_positivo(string mensaje)
{
    double valor;

    Console.Write(mensaje);
    string entrada = Console.ReadLine() ?? "";

    while (!double.TryParse(entrada, out valor) || valor < 0)
    {
        Console.WriteLine("Error. Debe ingresar un número positivo.");
        Console.Write(mensaje);
        entrada = Console.ReadLine() ?? "";
    }

    return valor;
}

// =======================================================
// INTEGRANTE 3: RONALD JESHUA CASTILLO CUADRA
// Rama sugerida: feature/ronald-calculos
// Función: aplicar las fórmulas del laboratorio.
// =======================================================

void calcular_resultados(ref Medicion medicion)
{
    Console.WriteLine("Módulo pendiente de desarrollar por Ronald.");

    /*
    Aquí se deben calcular:

    Fp
    Ip
    Mp
    Mw
    error

    Constantes:
    rho = 1000 kg/m³
    g = 9.81 m/s²
    b = 0.075 m
    L = 0.1 m

    Si s <= 100:
        Fp = rho * g * (s^2 / 2000000) * b
        Ip = 200 - (s / 3)

    Si s > 100:
        Fp = rho * g * ((s / 1000) - 0.05) * (L * b)
        Ip = 150 + (1 / 12) * (10000 / (s - 50))

    Mp = Fp * Ip
    Mw = Fw * Iw
    error = Math.Abs((Mw - Mp) / Mw) * 100
    */
}

// =======================================================
// INTEGRANTE 1: MARIELA FERNANDA HURTADO DUARTE
// Rama sugerida: feature/mariela-mostrar
// Función: mostrar los datos en una tabla clara.
// =======================================================

void mostrar_mediciones()
{
    Console.WriteLine("===== MEDICIONES REGISTRADAS =====");

    if (cantidad_mediciones == 0)
    {
        Console.WriteLine("No hay mediciones registradas.");
        return;
    }

    Console.WriteLine("Módulo pendiente de desarrollar por Mariela.");

    /*
    Aquí se debe mostrar una tabla con:

    No.
    Iw
    Fw
    s
    Fp
    Ip
    Mp
    Mw
    Error %

    Usar un ciclo for desde 0 hasta cantidad_mediciones.
    */
}

// =======================================================
// INTEGRANTE 4: NIJERI IVETH JARQUÍN GARCÍA
// Rama sugerida: feature/nijeri-archivo-eliminar
// Función: eliminar mediciones y trabajar con archivo CSV.
// =======================================================

void eliminar_medicion()
{
    Console.WriteLine("===== ELIMINAR MEDICIÓN =====");

    if (cantidad_mediciones == 0)
    {
        Console.WriteLine("No hay mediciones para eliminar.");
        return;
    }

    Console.WriteLine("Módulo pendiente de desarrollar por Nijeri.");

    /*
    Aquí se debe:

    1. Mostrar las mediciones.
    2. Pedir el número de medición a eliminar.
    3. Validar que exista.
    4. Mover los registros del arreglo una posición hacia atrás.
    5. Disminuir cantidad_mediciones.
    */
}

void guardar_archivo()
{
    Console.WriteLine("===== GUARDAR ARCHIVO =====");
    Console.WriteLine("Módulo pendiente de desarrollar por Nijeri.");

    /*
    Aquí se debe guardar en CSV usando ruta_archivo.

    El formato sugerido es:

    Iw;Fw;s;Fp;Ip;Mp;Mw;error

    Se debe recorrer el arreglo desde 0 hasta cantidad_mediciones.
    */
}

void leer_archivo()
{
    /*
    Este módulo debe cargar el archivo CSV si ya existe.

    Nijeri puede implementarlo después.

    Por ahora queda vacío para que el programa no dé error
    cuando se ejecute desde develop.
    */
}

void pausar()
{
    Console.WriteLine();
    Console.Write("Presione una tecla para continuar...");
    Console.ReadKey();
}

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