using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RetoLavadora
{
    internal class Program
    {

        // Menu para elegir el tipo de ropa
        private static string MostrarMenuTipoDeRopa()
        {
            while (true)
            {
                Console.WriteLine("\n Seleccione el tipo de ropa: ");
                Console.WriteLine("\n 1.Blanca");
                Console.WriteLine("\n 2.Algodón");
                Console.WriteLine("\n 3.Lycra");
                Console.WriteLine("\n 4.Sedas");
                Console.WriteLine("\n 5.Colores");
                Console.WriteLine("\n 6.Jeans");
                Console.WriteLine("\n 7.Camperas");
                Console.WriteLine("\n 8.Toallas");
                Console.WriteLine("\n 9.Sabanas");
                Console.WriteLine("\n 10.Tennis");
                Console.WriteLine("\n Ingrese el número correspondiente a su selección: ");

                if (int.TryParse(Console.ReadLine(), out int opcion) && opcion >= 1 && opcion <= 10)
                {
                    string v = opcion switch
                    {
                        1 => "Blanca",
                        2 => "Algodón",
                        3 => "Lycra",
                        4 => "Sedas",
                        5 => "Colores",
                        6 => "Jeans",
                        7 => "Camperas",
                        8 => "Toallas",
                        9 => "Sabanas",
                        10 => "Tennis",
                        _ => throw new InvalidOperationException("Opción Invalida")
                    };
                    string TipoDeRopa = v;
                    Console.WriteLine($"\n Ha seleccionado: {TipoDeRopa}");
                    return TipoDeRopa;
                }
                else
                {
                    Console.WriteLine("\n Error: Selección no valida. Por favor, ingrese un número entre 1 y 10.");
                }
            }
        }

        // atributo para inicializar contador de usuarios
        private static int contadorUsuarios = 0;

        private static List<(String NombreCliente, DateTime FechaHora, TimeSpan DuracionCiclo, string TipoRopa, int Kilos, decimal CostoTotal, decimal ConsumoEnergia, int Estrato)> registrosLavado = new List<(string, DateTime, TimeSpan, string, int, decimal, decimal, int)>();
        static void Main(string[] args)
        {
            // Declaracion atributos
            string TipoDeRopa ;
            int kilos;
            int estrato;

            bool EjecutarOtroCiclo = true;

            // Ciclo mientras para realizar la ejecucion de otro ciclo sin salir del programa
            while (EjecutarOtroCiclo)
            {
                DateTime inicioLavado = DateTime.Now;
                Console.WriteLine("\n Has ingresado al Programa Lavactiv de Haceb \n");

                // Solicitar nombre usuario
                Console.WriteLine("Ingrese el nombre del cliente: ");
                string nombreCliente = Console.ReadLine();
                contadorUsuarios++;
                Console.WriteLine($"\n Querido {nombreCliente} eres el cliente N° {contadorUsuarios} \n");

                // Validar eleccion de ropa
                while (true)
                { 
                    TipoDeRopa = MostrarMenuTipoDeRopa();
                    if (!string.IsNullOrEmpty(TipoDeRopa))
                    {
                        break;
                    }
                    Console.WriteLine("\n Error: El tipo de ropa no puede estar vacio, por favor intente nuevamente. \n");

                }
                // Mostrar recomendacion de lavado
                var recomendacionLavado = new RecomendacionLavado(TipoDeRopa);
                recomendacionLavado.ShowRecomendation();

                // validar eleccion de kilos
                while (true)
                {
                    Console.WriteLine("\n - Ingrese por favor los kilos de ropa a lavar (mínimo 5 kg, máximo 40 kg): \n");
                    if (int.TryParse(Console.ReadLine(), out kilos) && kilos >= 5 && kilos <= 40)
                    {
                        break;
                    }
                    Console.WriteLine("\n * Error: Los kilos deben de estar entre 5 y 40. Por favor, intente nuevamente.* \n");
                }

                // validacion de ropa muy sucia
                int TiempoLavadoMinutos = 3;
                Console.WriteLine("\n ¿La ropa está muy sucia? (s/n) \n");
                if (Console.ReadLine().ToLower() == "s")
                {
                    Console.WriteLine("\n Ingrese el tiempo de lavado en minutos: \n");
                    if (!int.TryParse(Console.ReadLine(), out TiempoLavadoMinutos) || TiempoLavadoMinutos <= 0)
                    {
                        Console.WriteLine("\n Tiempo invalido. Se usara el tiempo estandar de 3 minutos. \n");
                        TiempoLavadoMinutos = 3;
                    }
                }

                // Inicializa un objeto de tipo Lavadora con los datos necesarios para realizar un ciclo de lavado. 
                Lavadora lavadora = new Lavadora(kilos, TipoDeRopa, TiempoLavadoMinutos);

                // validación de estrato
                while (true)
                {
                    Console.Write("\n Ingrese por favor el estrato de consumo (2, 3, 4 o 5): \n");
                    if (int.TryParse(Console.ReadLine(), out estrato) && (estrato >= 2 && estrato <= 5))
                    {
                        break;
                    }
                    Console.WriteLine("\n * Error: Estrato Invalido. * \n");
                }

                lavadora.CicloCompletado();
                lavadora.EjecutarCicloLavado(kilos, false, 1.5M, estrato);

                TimeSpan duracionCiclo = DateTime.Now - inicioLavado;

                registrosLavado.Add((
                    nombreCliente,
                    inicioLavado,
                    duracionCiclo,
                    TipoDeRopa,
                    kilos,
                    lavadora.ObtenerCostoTotal(),
                    lavadora.ObtenerConsumoEnergia(),
                    estrato
                    ));

                bool respuestaValida = false;
                while (!respuestaValida)
                {
                    Console.WriteLine("\n ¿Desea realizar otro ciclo de lavado? (s/n) \n");
                    string respuesta = Console.ReadLine()?.ToLower();

                    if (respuesta == "s")
                    {
                        respuestaValida = true;
                        EjecutarOtroCiclo = true;
                        Console.Clear();
                    }
                    else if (respuesta == "n")
                    {
                        respuestaValida = true;
                        EjecutarOtroCiclo = false;

                        Console.WriteLine("\n === Resumen de todos los lavados ===\n");
                        foreach (var registro in registrosLavado)
                        {
                            Console.WriteLine(
                                $"\n Resumen de lavado: " +
                                $"\n Cliente: {registro.NombreCliente}" +
                                $"\n Fecha y Hora: {registro.FechaHora:dd/MM/yyyy HH:mm:ss}" +
                                $"\n Duración: {registro.DuracionCiclo.Minutes} minutos y {registro.DuracionCiclo.Seconds} segundos" +
                                $"\n Tipo de ropa: {registro.TipoRopa}" +
                                $"\n Kilos: {registro.Kilos}" +
                                $"\n Costo total: ${registro.CostoTotal:N2}" +
                                $"\n Consumo de energia: {registro.ConsumoEnergia:N2} Kwh" +
                                $"\n Estrato {estrato}\n");
                        }

                        Console.WriteLine("\n Presione ESC para salir... \n");
                        while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
                    }
                    else
                    {
                        Console.WriteLine("\nPor favor, ingrese una respuesta valida (s/n) \n");
                    }
                }
            }
        }
    }
}
