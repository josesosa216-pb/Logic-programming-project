using System;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        List<double> costosEnvio = new List<double>();
        bool sistemaActivo = true;

        do
        {
            Console.Clear();
            Console.WriteLine("SISTEMA DE CLASIFICACION DE PEDIDOS");
            Console.WriteLine("1. Registrar pedido");
            Console.WriteLine("2. Ver reporte estadístico");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            int opcionMenu;
            while (!int.TryParse(Console.ReadLine(), out opcionMenu) || (opcionMenu < 0 || opcionMenu > 2))
            {
                Console.Write("Opción inválida. Ingrese 0, 1 o 2: ");
            }

            switch (opcionMenu)
            {
                case 1:
                    decimal monto;
                    int cantItems;

                    Console.Write("Ingrese el monto del pedido: ");
                    while (!decimal.TryParse(Console.ReadLine(), out monto) || monto < 0)
                    {
                        Console.Write("Dato inválido. Ingrese un monto válido: ");
                    }

                    int opcionEnvio;
                    Console.WriteLine("Seleccione el tipo de envío:");
                    Console.WriteLine("1. Nacional");
                    Console.WriteLine("2. Exterior");

                    while (!int.TryParse(Console.ReadLine(), out opcionEnvio) || (opcionEnvio != 1 && opcionEnvio != 2))
                    {
                        Console.Write("Opción inválida. Ingrese 1 o 2: ");
                    }

                    string tipoEnvio = (opcionEnvio == 1) ? "nacional" : "exterior";

                    int opcionCliente;
                    Console.WriteLine("Seleccione el tipo de cliente:");
                    Console.WriteLine("1. Nuevo");
                    Console.WriteLine("2. Recurrente");

                    while (!int.TryParse(Console.ReadLine(), out opcionCliente) || (opcionCliente != 1 && opcionCliente != 2))
                    {
                        Console.Write("Opción inválida. Ingrese 1 o 2: ");
                    }

                    string tipoCliente = (opcionCliente == 1) ? "nuevo" : "recurrente";

                    Console.Write("Ingrese la cantidad de items: ");
                    while (!int.TryParse(Console.ReadLine(), out cantItems) || cantItems < 0)
                    {
                        Console.Write("Dato inválido. Ingrese una cantidad válida: ");
                    }

                    decimal costoEnvio = 0;
                    string categoria = "";

                    if (monto >= 150000 && tipoCliente == "recurrente")
                    {
                        categoria = "Envío Gratis";
                        costoEnvio = 0;
                    }
                    else if (cantItems >= 5 || monto >= 300000)
                    {
                        categoria = "Envío Express";
                        costoEnvio = 20000;
                    }
                    else
                    {
                        categoria = "Envío Estándar";
                        costoEnvio = 10000;
                    }

                    if (tipoEnvio == "exterior")
                    {
                        costoEnvio += 15000;
                    }

                    costosEnvio.Add((double)costoEnvio);

                    Console.WriteLine("\n--- PEDIDO REGISTRADO ---");
                    Console.WriteLine($"Categoría: {categoria}");
                    Console.WriteLine($"Costo de envío: ${costoEnvio}");

                    Console.WriteLine("\nPresione Enter para continuar...");
                    Console.ReadLine();
                    break;

                case 2:

                    if (costosEnvio.Count == 0)
                    {
                        Console.WriteLine("No hay pedidos registrados.");
                    }
                    else
                    {
                        double total = 0;
                        double mayor = costosEnvio[0];
                        double menor = costosEnvio[0];

                        int enviosCaros = 0;
                        const double UMBRAL = 20000;

                        for (int i = 0; i < costosEnvio.Count; i++)
                        {
                            double costo = costosEnvio[i];

                            total += costo;

                            if (costo > mayor)
                                mayor = costo;

                            if (costo < menor)
                                menor = costo;

                            if (costo >= UMBRAL)
                                enviosCaros++;
                        }

                        double promedio = total / costosEnvio.Count;

                        Console.WriteLine("REPORTE DEL DÍA");
                        Console.WriteLine($"Cantidad de pedidos: {costosEnvio.Count}");
                        Console.WriteLine($"Total costos de envío: ${total}");
                        Console.WriteLine($"Promedio: ${promedio}");
                        Console.WriteLine($"Mayor costo: ${mayor}");
                        Console.WriteLine($"Menor costo: ${menor}");
                        Console.WriteLine($"Envíos >= {UMBRAL}: {enviosCaros}");
                    }

                    Console.WriteLine("Presione Enter para continuar...");
                    Console.ReadLine();
                    break;

                case 0:
                    sistemaActivo = false;
                    break;
            }

        } while (sistemaActivo);
    }
}