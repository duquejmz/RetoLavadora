using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoLavadora
{
    public class CalculosLavadora
    {
        // Declaracion atributos
        private const decimal PrecioPorKilo = 4000;
        private const decimal Iva = 0.19m;
        private const decimal GananciaEmpresario = 0.40m;
        private const decimal IncrementoPorTipoRopa = 0.03m;

        // Calculo del costo por kilo con un incremento si es ropa especial
        public static decimal CalcularCostoPorKilo(string TipoDeRopa)
        {
            bool ropaEspecial = TipoDeRopa.Equals("Blanca", StringComparison.OrdinalIgnoreCase) ||
                                TipoDeRopa.Equals("Algodon", StringComparison.OrdinalIgnoreCase) ||
                                TipoDeRopa.Equals("Tennis", StringComparison.OrdinalIgnoreCase);

            decimal costoBase = PrecioPorKilo;
            if (ropaEspecial)
            {
                costoBase += costoBase * IncrementoPorTipoRopa;
            }
            return costoBase;
        }

        // Calculo costo sin IVA
        public static decimal CalcularCostoTotalSinIva(decimal costoPorKilo, int kilos)
        {
            return costoPorKilo * kilos;
        }

        // Calculo costo con IVA
        public static decimal CalcularCostoTotalConIva(decimal costoSinIva)
        {
            return costoSinIva * (1 + Iva);
        }

        // Calculo utilidad del empresario -> es lo mismo que la función CalcularGananciaEmpresario
        public static decimal CalcularUtilidadEmpresario(decimal costoSinIva)
        {
            return costoSinIva * GananciaEmpresario;
        }                                //    ^ ^   
        // Calculo ganancias del empresario. <*$o$*>
                                         //    | | 
        public decimal CalcularGananciaEmpresario(decimal costoSinIva)
        {
            return costoSinIva * GananciaEmpresario;
        }

        // Calculo consumo energetico mensual basado en potencia y precio por estrato
        public static decimal CalcularConsumoEnergetico(decimal potenciaKw, decimal horasDiarias, int dias, int estrato)
        {
            decimal consumoMensual = potenciaKw * horasDiarias * dias;
            decimal precioKwh = ObtenerPrecioPorEstrato(estrato);

            return consumoMensual * precioKwh;
        }

        // Calculo consumo energetico -_-
        public static decimal CalcularConsumoEnergia(int estrato)
        {
            const decimal potenciaKw = 0.5m;
            const decimal horasDiarias = 24;
            const int dias = 30;

            decimal consumoMensual = potenciaKw * horasDiarias * dias;
            return consumoMensual;
        }

        // Calcular Costo Energia
        public static decimal CalcularCostoEnergia(decimal consumoEnergia, int estrato)
        {
            decimal precioKwh = ObtenerPrecioPorEstrato(estrato);
            return consumoEnergia * precioKwh;
        }

        // Funcion para determinar el estrato del cliente
        private static decimal ObtenerPrecioPorEstrato(int estrato)
        {
            switch (estrato)
            {
                case 2:
                    return 867.8m;
                case 3:
                    return 737.6m;
                case 4:
                    return 867.8m;
                case 5:
                    return 1041m;
                default:
                    return 0m;

            };
        }
    }
}
