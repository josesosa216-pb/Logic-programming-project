using System;
using System.Collections.Generic;

namespace SistemaPedidos
{
    public class Pedido
    {
        public decimal Monto { get; set; }
        public string Ciudad { get; set; }
        public string TipoCliente { get; set; }
        public int CantidadItems { get; set; }
        public string CategoriaDespacho { get; set; }
        public decimal CostoEnvio { get; set; }
    }

    internal class Program
    {
        private static List<Pedido> pedidos = new List<Pedido>();

        static void Main()
        {
            EjecutarSistema();
        }
        static void EjecutarSistema()
        {
            int opcion;

            do
            {
                MostrarMenu();
                opcion = LeerOpcionMenu();

                switch (opcion)
                {
                    case 1:
                        RegistrarPedido();
                        break;

                    case 2:
                        MostrarRegistros();
                        break;

                    case 0:
                        Console.WriteLine("\nSaliendo del sistema...");
                        break;
                }

            } while (opcion != 0);
        }
        static void MostrarMenu()
        {
            Console.WriteLine("\n=== SISTEMA DE CLASIFICACIÓN DE PEDIDOS ===");
            Console.WriteLine("1. Registrar pedido");
            Console.WriteLine("2. Ver registros");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
        }

        static int LeerOpcionMenu()
        {
            int opcion;

            while (!int.TryParse(Console.ReadLine(), out opcion)
                   || opcion < 0
                   || opcion > 2)
            {
                Console.Write("Ingrese una opción válida: ");
            }

            return opcion;
        }

        static void RegistrarPedido()
        {
            decimal monto = LeerMonto();
            string ciudad = LeerCiudad();
            string tipoCliente = LeerTipoCliente();
            int cantidadItems = LeerCantidadItems();

            (string categoria, decimal costo) =
                CalcularDespacho(monto, ciudad, tipoCliente, cantidadItems);

            Pedido pedido = new Pedido
            {
                Monto = monto,
                Ciudad = ciudad,
                TipoCliente = tipoCliente,
                CantidadItems = cantidadItems,
                CategoriaDespacho = categoria,
                CostoEnvio = costo
            };

            pedidos.Add(pedido);

            MostrarResultado(pedido);
        }

       
        static decimal LeerMonto()
        {
            decimal monto;

            do
            {
                Console.Write("Monto del pedido: ");
            }
            while (!decimal.TryParse(Console.ReadLine(), out monto) || monto < 0);

            return monto;
        }

        static string LeerCiudad()
        {
            string ciudad;

            do
            {
                Console.Write("Ciudad destino (nacional/exterior): ");
                ciudad = Console.ReadLine()?.Trim().ToLower();

            } while (ciudad != "nacional" && ciudad != "exterior");

            return ciudad;
        }

        static string LeerTipoCliente()
        {
            string tipo;

            do
            {
                Console.Write("Tipo cliente (nuevo/recurrente): ");
                tipo = Console.ReadLine()?.Trim().ToLower();

            } while (tipo != "nuevo" && tipo != "recurrente");

            return tipo;
        }

      
        static int LeerCantidadItems()
        {
            int cantidad;

            do
            {
                Console.Write("Cantidad de ítems: ");
            }
            while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad <= 0);

            return cantidad;
        }

        static (string, decimal) CalcularDespacho(
            decimal monto,
            string ciudad,
            string tipoCliente,
            int cantidadItems)
        {
            string categoria;
            decimal costo;

            if (monto >= 150000 && tipoCliente == "recurrente")
            {
                categoria = "Envío Gratis";
                costo = 0;
            }
            else if (cantidadItems >= 5 || monto >= 300000)
            {
                categoria = "Envío Express";
                costo = 20000;
            }
            else
            {
                categoria = "Envío Estándar";
                costo = 10000;
            }

            if (ciudad == "exterior")
            {
                costo += 15000;
            }

            return (categoria, costo);
        }

        static void MostrarResultado(Pedido pedido)
        {
            Console.WriteLine("\n=== RESULTADO ===");
            Console.WriteLine($"Categoría: {pedido.CategoriaDespacho}");
            Console.WriteLine($"Costo envío: ${pedido.CostoEnvio:N0}");
        }

        static void MostrarRegistros()
        {
            Console.WriteLine("\n=== REGISTRO DE PEDIDOS ===");

            if (pedidos.Count == 0)
            {
                Console.WriteLine("No hay pedidos registrados.");
                return;
            }

            decimal sumaMontos = 0;

            for (int i = 0; i < pedidos.Count; i++)
            {
                Pedido p = pedidos[i];

                Console.WriteLine($"\nPedido #{i + 1}");
                Console.WriteLine($"Monto: ${p.Monto:N0}");
                Console.WriteLine($"Ciudad: {p.Ciudad}");
                Console.WriteLine($"Cliente: {p.TipoCliente}");
                Console.WriteLine($"Items: {p.CantidadItems}");
                Console.WriteLine($"Categoría: {p.CategoriaDespacho}");
                Console.WriteLine($"Costo envío: ${p.CostoEnvio:N0}");

                sumaMontos += p.Monto;
            }

            Console.WriteLine($"\nTotal pedidos: {pedidos.Count}");
            Console.WriteLine($"Promedio monto: ${(sumaMontos / pedidos.Count):N0}");
        }
    }
}