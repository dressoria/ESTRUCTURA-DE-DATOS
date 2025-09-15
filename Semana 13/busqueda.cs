
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CatalogoRevistas
{
    /// <summary>
    /// Catálogo de revistas con operaciones de normalización y búsqueda.
    /// </summary>
    public class MagazineCatalog
    {
        private readonly List<string> _titulos;
        private readonly List<string> _titulosNormalizadosOrdenados; // Para búsqueda binaria (recursiva)

        public MagazineCatalog(IEnumerable<string> titulos)
        {
            // Guardamos lista original (tal cual) para mostrar y para la búsqueda lineal
            _titulos = titulos
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => t.Trim())
                .ToList();

            // Preparamos una lista ordenada y normalizada para la búsqueda binaria recursiva
            _titulosNormalizadosOrdenados = _titulos
                .Select(Normalizar)
                .OrderBy(t => t, StringComparer.Ordinal) // Orden estable sobre la forma normalizada
                .ToList();
        }

        /// <summary>
        /// Búsqueda lineal (iterativa) en la lista original (case/acentos-insensible).
        /// </summary>
        public bool BuscarIterativo(string consulta)
        {
            string needle = Normalizar(consulta);
            foreach (var titulo in _titulos)
            {
                if (Normalizar(titulo) == needle)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Búsqueda binaria (recursiva) sobre la lista normalizada y ordenada.
        /// </summary>
        public bool BuscarRecursivo(string consulta)
        {
            string needle = Normalizar(consulta);
            return BinariaRecursiva(_titulosNormalizadosOrdenados, needle, 0, _titulosNormalizadosOrdenados.Count - 1);
        }

        private static bool BinariaRecursiva(List<string> data, string needle, int low, int high)
        {
            if (low > high) return false;

            int mid = low + (high - low) / 2;
            int cmp = string.CompareOrdinal(data[mid], needle);

            if (cmp == 0) return true;
            if (cmp > 0) return BinariaRecursiva(data, needle, low, mid - 1);
            return BinariaRecursiva(data, needle, mid + 1, high);
        }

        /// <summary>
        /// Normaliza para comparaciones: quita acentos, pasa a minúsculas e imprime en FormD.
        /// </summary>
        public static string Normalizar(string? s)
        {
            if (string.IsNullOrWhiteSpace(s)) return string.Empty;

            // 1) Lowercase invariante
            string lower = s.ToLowerInvariant();

            // 2) Descomponer (FormD) para separar letras de diacríticos
            string formD = lower.Normalize(NormalizationForm.FormD);

            // 3) Remover marcas diacríticas (NonSpacingMark)
            var chars = formD
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray();

            // 4) Re-componer
            return new string(chars).Normalize(NormalizationForm.FormC).Trim();
        }

        public void ImprimirCatalogo()
        {
            Console.WriteLine("\nCatálogo (10+ revistas):");
            int i = 1;
            foreach (var t in _titulos.OrderBy(t => t, StringComparer.CurrentCultureIgnoreCase))
            {
                Console.WriteLine($"  {i,2}. {t}");
                i++;
            }
            Console.WriteLine();
        }
    }

    internal static class Menu
    {
        public static void Mostrar()
        {
            // Creamos un catálogo con al menos 10 títulos
            var titulos = new List<string>
            {
                "National Geographic",
                "Scientific American",
                "The Economist",
                "Nature",
                "Time",
                "Forbes",
                "Wired",
                "Harvard Business Review",
                "El Malpensante",
                "Muy Interesante",
                "Rolling Stone",
                "New Scientist"
            };

            var catalogo = new MagazineCatalog(titulos);

            while (true)
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("      Catálogo de Revistas - Búsquedas");
                Console.WriteLine("==============================================");
                Console.WriteLine("1) Ver catálogo");
                Console.WriteLine("2) Buscar (algoritmo iterativo - lineal)");
                Console.WriteLine("3) Buscar (algoritmo recursivo - binario)");
                Console.WriteLine("0) Salir");
                Console.Write("Elige una opción: ");
                string? opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        catalogo.ImprimirCatalogo();
                        break;

                    case "2":
                        EjecutarBusqueda(catalogo, metodo: "iterativo");
                        break;

                    case "3":
                        EjecutarBusqueda(catalogo, metodo: "recursivo");
                        break;

                    case "0":
                        Console.WriteLine("¡Hasta luego!");
                        return;

                    default:
                        Console.WriteLine("Opción no válida. Intenta nuevamente.\n");
                        break;
                }
            }
        }

        private static void EjecutarBusqueda(MagazineCatalog catalogo, string metodo)
        {
            Console.Write("Ingresa el título a buscar: ");
            string? consulta = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(consulta))
            {
                Console.WriteLine("Entrada vacía. Intenta nuevamente.\n");
                return;
            }

            bool encontrado = metodo == "iterativo"
                ? catalogo.BuscarIterativo(consulta)
                : catalogo.BuscarRecursivo(consulta);

            Console.WriteLine(encontrado ? "Encontrado" : "No encontrado");
            Console.WriteLine();
        }
    }

    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Menu.Mostrar();
        }
    }
}
