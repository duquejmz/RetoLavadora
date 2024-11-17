using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace RetoLavadora
{
    // Funcion para reproducir el sonido de Inicio de la lavadora
    public class SonidoLavadora
    {
        public void ReproducirSonidoInicio(string Sonidos)
        {
            try
            {
                string ruta = $"Sonidos/Inicio.wav";
                using (SoundPlayer player = new SoundPlayer(ruta))
                {
                    player.PlaySync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al reproducir el sonido: {e.Message}");
            }
        }

        // Funcion para reproducir el sonido de enjuague de la lavadora
        public void ReproducirSonidoEnjuague(string Sonidos)
        {
            try
            {
                string ruta = $"Sonidos/Enjuague.wav";
                using (SoundPlayer player = new SoundPlayer(ruta))
                {
                    player.PlaySync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al reproducir el sonido: {e.Message}");
            }
        }

        // Funcion para reproducir el sonido de lavado de la lavadora
        public void ReproducirSonidoLavado(string Sonidos)
        {
            try
            {
                string ruta = $"Sonidos/Lavado.wav";
                using (SoundPlayer player = new SoundPlayer(ruta))
                {
                    player.PlaySync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al reproducir el sonido: {e.Message}");
            }
        }

        // Funcion para reproducir el sonido de llenado de la lavadora
        public void ReproducirSonidoLlenado(string Sonidos)
        {
            try
            {
                string ruta = $"Sonidos/Llenado.wav";
                using (SoundPlayer player = new SoundPlayer(ruta))
                {
                    player.PlaySync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al reproducir el sonido: {e.Message}");
            }
        }

        // Funcion para reproducir el sonido de secado de la lavadora
        public void ReproducirSonidoSecado(string Sonidos)
        {
            try
            {
                string ruta = $"Sonidos/Secado.wav";
                using (SoundPlayer player = new SoundPlayer(ruta))
                {
                    player.PlaySync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al reproducir el sonido: {e.Message}");
            }
        }

        // Funcion para reproducir el sonido de Finalización ciclo de la lavadora
        public void ReproducirSonidoFinal(string Sonidos)
        {
            try
            {
                string ruta = $"Sonidos/Final.wav";
                using (SoundPlayer player = new SoundPlayer(ruta))
                {
                    player.PlaySync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al reproducir el sonido: {e.Message}");
            }
        }
    }
}
