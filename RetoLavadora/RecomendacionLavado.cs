using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoLavadora
{
    public class RecomendacionLavado
    {
        // declaracion atributo
        private readonly string TipoDeRopa;

        // declaracion de lista para recomendacion de ropa
        private static readonly List<String> RecomendacionesLista = new List<string>
        {
            "Blanca",
            "Algodon",
            "Lycra",
            "Sedas",
            "Colores",
            "Jeans",
            "Camperas",
            "Toallas",
            "Sabanas",
            "Tennis"
        };

        // Diccionario para almacenar las recomendaciones de lavado
        private static readonly Dictionary<string, string> Recomendaciones = new Dictionary<string, string>
        {
            {"Blanca", "Agua caliente (55-90°C)"},
            {"Algodon", "Agua fria (hasta 20°C)"},
            {"Lycra", "Agua fria (hasta 20°C)"},
            {"Sedas", "Agua fria (hasta 20°C)"},
            {"Colores", "Agua fria (hasta 20°C)"},
            {"Jeans", "Agua tibia (30-50°C)"},
            {"Camperas", "Agua tibia (30-50°C)"},
            {"Toallas", "Agua caliente (55-90°C)"},
            {"Sabanas", "Agua caliente (55-90°C)"},
            {"Tennis", "Agua fria (hasta 20°C)"},
        };

        // Declaracion de función de recomendacionLavado
        protected internal RecomendacionLavado(string tipoDeRopa)
        {
            TipoDeRopa = tipoDeRopa;
        }

        // Declaracion de funcion para mostrar la recomendacion de lavado
        protected internal void ShowRecomendation()
        {
            if (Recomendaciones.TryGetValue(TipoDeRopa, out string recomendacion))
            {
                Console.WriteLine($"Recomendación de lavado para {TipoDeRopa}: {recomendacion}");
            }
            else
            {
                Console.WriteLine("Tipo de ropa no identificado, usando agua fria por defecto.");
            }
        }
    }
}
