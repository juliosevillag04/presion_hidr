void pedirDatos()
{
    if (i >= 15)
    {
        Console.WriteLine("No hay más espacio. Límite de 15 mediciones alcanzado.");
        return;
    }

    Console.WriteLine($"\nMedición #{i + 1} de 15");

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

    double rho = 1000.0;
    double g = 9.81;
    double b = 0.075;
    double L = 0.1;

    if (s <= 100)
    {
        mediciones[i].Fp = rho * g * (Math.Pow(s, 2) / 2000000) * b;
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
}