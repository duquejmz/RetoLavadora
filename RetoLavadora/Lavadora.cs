using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RetoLavadora
{
    public class Lavadora
    {
        // Declaración de atributos
        public int Kilos { get; set; }
        public string TipoDeRopa { get; set; }

        public int TiempoLavadoMinutos { get; set; }
        // Se utiliza la clase 'SoundPlayer' para acceder a los sonidos
        private SonidoLavadora Sonido { get; } = new SonidoLavadora();
        private bool EsperandoReanudar = false;
        private bool CicloSecadoPendiente = false;
        private decimal costoTotalActual;
        private decimal consumoEnergiaActual;

        // Constructor
        public Lavadora(int kilos, string tipoDeRopa, int tiempoLavadoMinutos)
        {
            Kilos = kilos;
            TipoDeRopa = tipoDeRopa;
            TiempoLavadoMinutos = tiempoLavadoMinutos;
        }
        // Funcion para que se vaya mostrando letra x letra de cada etapa del lavado.
        private void EscribirConParpadeo(string texto)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(texto);
                Thread.Sleep(500);
                Console.Clear();
                Thread.Sleep(500);
            }
            Console.WriteLine(texto);
        }
        // Funcion para reproducir el sonido de iniciar la lavadora
        private void InicioPrograma()
        {
            EscribirConParpadeo("\n  Iniciando programa...  \n");
            Sonido.ReproducirSonidoInicio("Inicio.wav");
            Thread.Sleep(7000);
        }
        // Funcion para reproducir el sonido de llenar la lavadora
        private void Llenado()
        {
            EscribirConParpadeo("\n  Llenando agua... \n");
            Sonido.ReproducirSonidoLlenado("Llenado.wav");
            Thread.Sleep(7000);
        }
        // Funcion para reproducir el sonido de lavado de la lavadora

        private void Lavando()
        {
            EscribirConParpadeo("\n  Lavando... \n");
            Sonido.ReproducirSonidoLavado("Lavado.wav");
            Thread.Sleep(TiempoLavadoMinutos * 7000);
        }
        // Funcion para reproducir el sonido de enjuague de la lavadora

        private void Enjuagando()
        {
            EscribirConParpadeo("\n  Enjuagando...  \n");
            Sonido.ReproducirSonidoEnjuague("Enjuague.wav");
            Thread.Sleep(7000);
        }
        // Funcion para reproducir el sonido de secado de la lavadora y mostrar el mensaje

        private void Secando()
        {
            EscribirConParpadeo("\n  Secando...  \n");
            Sonido.ReproducirSonidoSecado("Secado.wav");
            Thread.Sleep(7000);
        }
        // Funcion para determinar que ya se completo el ciclo

        public void CicloCompletado()
        {

            InicioPrograma();
            Llenado();
            Lavando();
            Enjuagando();
            GestionarSecado();
        } 
        // Funcion para gestionar el secado
        private void GestionarSecado()
        {
            while (true)
            {
                if (!CicloSecadoPendiente)
                {
                    Console.WriteLine("\n - ¿Desea secar las prendas ahora? (s/n) \n");
                    var respuesta = Console.ReadLine()?.ToLower();

                    if ( respuesta == "s" )
                    {
                        RealizarSecado();
                        break;
                    }
                    else if ( respuesta == "n" )
                    {
                        CicloSecadoPendiente = true;
                        EsperandoReanudar = true;
                        MostrarMenuEspera();
                    }
                    else
                    {
                        Console.WriteLine("\n Por favor ingrese 's' para sí o 'n' para no. \n");
                        var tecla = Console.ReadKey(true);
                    }
                }
                if ( EsperandoReanudar )
                {
                    //Console.WriteLine("\n Presione 'R' para reanudar el secado o 'ESC' para cancelar");
                    var tecla = Console.ReadKey(true);

                    if (tecla.Key == ConsoleKey.R)
                    {
                        EsperandoReanudar = false;
                        RealizarSecado();
                        break;
                    }
                    else if (tecla.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("\n Secado cancelado. \n");
                        break;
                    }
                }
            }
        }
        // Funcion para activar el secado
        private void RealizarSecado()
        {
            Secando();
            FinalizarCiclo();
        }
        // Funcion para mostrar un menu cuando, el usuario no ha elegido continuar el secado o finalizar el ciclo.
        private void MostrarMenuEspera()
        {
            Console.WriteLine("\n ================================= \n");
            Console.WriteLine("\n Ciclo de lavado completado \n");
            Console.WriteLine("\n Prendas esperando para ser secadas \n");
            Console.WriteLine("\n Presione 'R' cuando desee reanudar el secado \n");
            Console.WriteLine("\n Presione 'ESC' para cancelar el secado \n");
            Console.WriteLine("\n ================================= \n");
        }
        // Funcion para finalizar el ciclo
        private void FinalizarCiclo()
        {
            EscribirConParpadeo("\n Ciclo Completado... \n");
            Sonido.ReproducirSonidoFinal("Final.wav");
            Thread.Sleep(4000);
            CicloSecadoPendiente = false;
            EsperandoReanudar = false;
        }

        public decimal ObtenerCostoTotal()
        {
            return costoTotalActual;
        }

        public decimal ObtenerConsumoEnergia()
        {
            return consumoEnergiaActual;
        }
        // Funcion para realizar la ejecucion del ciclo y sus calculos.
        protected internal void EjecutarCicloLavado(int kilos, bool ropaEspecial, decimal potenciaKw, int estrato)
        {

            decimal costoSinIva = CalculosLavadora.CalcularCostoPorKilo(TipoDeRopa);
            decimal costoConIva = CalculosLavadora.CalcularCostoTotalConIva(costoSinIva);
            decimal ganancia = CalculosLavadora.CalcularCostoPorKilo(TipoDeRopa);
            decimal consumoEnergia = CalculosLavadora.CalcularConsumoEnergetico(potenciaKw, 1 /*horasDiarias*/, 30 /*dias*/, estrato);

            Console.WriteLine($"Costo sin IVA: {costoSinIva:C}");
            Console.WriteLine($"Costo con IVA: {costoConIva:C}");
            Console.WriteLine($"Ganancia del empresario: {ganancia:C}");
            Console.WriteLine($"Consumo energético mensual: {consumoEnergia:C} COP");

            decimal costoPorKilo = CalculosLavadora.CalcularCostoPorKilo(TipoDeRopa);
            decimal costoTotalSinIva = CalculosLavadora.CalcularCostoTotalSinIva(costoPorKilo, kilos);
            decimal costoTotalConIva = CalculosLavadora.CalcularCostoTotalConIva(costoTotalSinIva);
            decimal utilidadEmpresario = CalculosLavadora.CalcularUtilidadEmpresario(costoTotalSinIva);
            decimal costoEnergia = CalculosLavadora.CalcularCostoEnergia(consumoEnergia, estrato);

            costoTotalActual = CalculosLavadora.CalcularCostoTotalConIva(costoTotalSinIva);
            consumoEnergiaActual = CalculosLavadora.CalcularConsumoEnergetico(potenciaKw, 1, 30, estrato);

            Console.WriteLine("\n  Resumen del lavado: ");
            Console.WriteLine($"\n - Fecha y hora del lavado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine($"\n - Tipo de Ropa: {TipoDeRopa} ");
            Console.WriteLine($"\n - Kilos de Ropa: {kilos} kg ");
            Console.WriteLine($"\n - Costo de lavado sin IVA: ${costoTotalSinIva:N2} ");
            Console.WriteLine($"\n - Costo de lavado con IVA: ${costoTotalConIva:N2} ");
            Console.WriteLine($"\n - Utilidad para el Empresario: ${utilidadEmpresario:N2} ");
            Console.WriteLine($"\n - Consumo de Energia Estimado: ${consumoEnergiaActual:N2} Kwh ");
            Console.WriteLine($"\n - Costo de Energía: ${CalculosLavadora.CalcularCostoEnergia(consumoEnergiaActual, estrato):N2} \n");

            Console.WriteLine("\n ¡Gracias por utilizar la lavadora Haceb! \n");

        }
    }
}

