using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AsignacionAsientos
{
    // Clase Persona
    public class Persona
    {
        public string Nombre { get; set; }
        public int NumeroAsiento { get; set; }

        public Persona(string nombre, int numeroAsiento)
        {
            Nombre = nombre;
            NumeroAsiento = numeroAsiento;
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre}, Asiento: {NumeroAsiento}";
        }
    }

    // Clase Atracción que maneja la cola
    public class Atraccion
    {
        private Queue<Persona> cola = new Queue<Persona>();
        private int capacidadMaxima = 30;
        private int contadorAsientos = 0;

        public bool IngresarPersona(string nombre)
        {
            if (contadorAsientos >= capacidadMaxima)
            {
                Console.WriteLine($"❌ Capacidad llena. {nombre} no puede ingresar.");
                return false;
            }

            contadorAsientos++;
            Persona nueva = new Persona(nombre, contadorAsientos);
            cola.Enqueue(nueva);
            Console.WriteLine($"✅ {nombre} ingresó con el asiento #{contadorAsientos}");
            return true;
        }

        public void MostrarPersonas()
        {
            Console.WriteLine("\n🎟️ Lista de personas en la atracción:");
            foreach (var persona in cola)
            {
                Console.WriteLine(persona.ToString());
            }
        }

        public int TotalPersonas()
        {
            return cola.Count;
        }
    }

    // Clase Principal (Main)
    class Program
    {
        static void Main(string[] args)
        {
            Atraccion atraccion = new Atraccion();
            Stopwatch cronometro = new Stopwatch();
            cronometro.Start();

            // Simulamos 35 personas intentando ingresar
            for (int i = 1; i <= 35; i++)
            {
                string nombre = "Persona" + i;
                atraccion.IngresarPersona(nombre);
            }

            cronometro.Stop();

            atraccion.MostrarPersonas();

            Console.WriteLine($"\n⏱️ Tiempo total de ejecución: {cronometro.ElapsedMilliseconds} ms");
            Console.WriteLine($"👥 Total de personas ingresadas: {atraccion.TotalPersonas()}");
        }
    }
}
