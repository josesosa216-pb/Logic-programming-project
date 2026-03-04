
internal class Program
{
    private static void Main(string[] args)
    {
        /*
IPO - Sistema de Clasificación de Pedidos

ENTRADAS:
- monto: decimal
- ciudad: string
- tipoCliente: string
- cantItems: int

PROCESO:
1. Si monto >= 150000 Y tipoCliente == "recurrente" → Envío Gratis.
2. Si cantItems >= 5 O monto >= 300000 → Envío Express.
3. En otros casos → Envío Estándar.
4. Si ciudad == "exterior" → agregar costo adicional.

SALIDAS:
- categoria: string
- costoEnvio: decimal
- mensaje informativo al usuario.
*/
       
        decimal monto;
        string ciudad;
        string tipoCliente;
        int cantItems;

        decimal costoEnvio = 0;
        string categoria = "";

        Console.WriteLine("Ingrese el monto del pedido:");
        monto = Convert.ToDecimal(Console.ReadLine());

        Console.WriteLine("¿El envío es nacional o exterior?");
        string tipoEnvio = Console.ReadLine().ToLower();

        Console.WriteLine("Ingrese el tipo de cliente (nuevo/recurrente):");
        tipoCliente = Console.ReadLine().ToLower();

        Console.WriteLine("Ingrese la cantidad de items:");
        cantItems = Convert.ToInt32(Console.ReadLine());

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


        Console.WriteLine("\n--- RESUMEN DEL PEDIDO ---");
        Console.WriteLine("Monto del pedido: $" + monto);
        Console.WriteLine("Tipo de cliente: " + tipoCliente);
        Console.WriteLine("Ciudad destino: " + tipoEnvio);
        Console.WriteLine("Cantidad de items: " + cantItems);
        Console.WriteLine("Categoría de envío: " + categoria);
        Console.WriteLine("Costo de envío: $" + costoEnvio);
    }
    }

