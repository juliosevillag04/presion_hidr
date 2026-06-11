void generarReporte()
{
    if (i == 0)
    {
        Console.WriteLine("No hay mediciones registradas para generar el reporte.");
        return;
    }

    double sumaFp = 0;
    double sumaMp = 0;
    double sumaMw = 0;
    double sumaError = 0;

    double mayorError = mediciones[0].error;
    double menorError = mediciones[0].error;

    int medicionMayorError = 1;
    int medicionMenorError = 1;

    for (int cont = 0; cont < i; cont++)
    {
        sumaFp += mediciones[cont].Fp;
        sumaMp += mediciones[cont].Mp;
        sumaMw += mediciones[cont].Mw;
        sumaError += mediciones[cont].error;

        if (mediciones[cont].error > mayorError)
        {
            mayorError = mediciones[cont].error;
            medicionMayorError = cont + 1;
        }

        if (mediciones[cont].error < menorError)
        {
            menorError = mediciones[cont].error;
            medicionMenorError = cont + 1;
        }
    }

    double promedioFp = sumaFp / i;
    double promedioMp = sumaMp / i;
    double promedioMw = sumaMw / i;
    double promedioError = sumaError / i;

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("===== REPORTE TÉCNICO DE MEDICIONES =====");
    Console.ResetColor();

    Console.WriteLine($"Cantidad de mediciones registradas: {i}");
    Console.WriteLine($"Promedio de fuerza sustitutiva Fp: {promedioFp:F3} N");
    Console.WriteLine($"Promedio de momento hidrostático Mp: {promedioMp:F2} N-mm");
    Console.WriteLine($"Promedio de momento antagónico Mw: {promedioMw:F2} N-mm");
    Console.WriteLine($"Promedio de error porcentual: {promedioError:F2}%");

    Console.WriteLine();

    Console.WriteLine($"Mayor error registrado: {mayorError:F2}% en la medición #{medicionMayorError}");
    Console.WriteLine($"Menor error registrado: {menorError:F2}% en la medición #{medicionMenorError}");

    Console.WriteLine();

    if (promedioError <= 5)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Interpretación: Las mediciones presentan una buena aproximación entre el momento hidrostático y el momento antagónico.");
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Interpretación: Las mediciones presentan una diferencia considerable. Se recomienda revisar los datos ingresados o el procedimiento experimental.");
        Console.ResetColor();
    }
}