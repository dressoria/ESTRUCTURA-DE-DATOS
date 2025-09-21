// Program.cs
using System;
using System.Collections.Generic;

namespace ArbolBinarioCSharp
{
    // Nodo genérico para ABB
    public class Node<T> where T : IComparable<T>
    {
        public T Value;
        public Node<T>? Left;
        public Node<T>? Right;

        public Node(T value) => Value = value;
    }

    // Árbol Binario de Búsqueda genérico
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public Node<T>? Root { get; private set; }
        public int Count { get; private set; }

        public void Insert(T value)
        {
            Root = Insert(Root, value);
        }

        private Node<T> Insert(Node<T>? node, T value)
        {
            if (node == null)
            {
                Count++;
                return new Node<T>(value);
            }
            int cmp = value.CompareTo(node.Value);
            if (cmp < 0) node.Left = Insert(node.Left, value);
            else if (cmp > 0) node.Right = Insert(node.Right, value);
            // Si es igual, puede ignorarse o decidir manejar duplicados.
            return node;
        }

        public bool Contains(T value)
        {
            var cur = Root;
            while (cur != null)
            {
                int cmp = value.CompareTo(cur.Value);
                if (cmp == 0) return true;
                cur = (cmp < 0) ? cur.Left : cur.Right;
            }
            return false;
        }

        public bool Remove(T value)
        {
            bool removed;
            (Root, removed) = Remove(Root, value);
            if (removed) Count--;
            return removed;
        }

        private (Node<T>?, bool) Remove(Node<T>? node, T value)
        {
            if (node == null) return (null, false);

            int cmp = value.CompareTo(node.Value);
            if (cmp < 0)
            {
                (node.Left, var removed) = Remove(node.Left, value);
                return (node, removed);
            }
            else if (cmp > 0)
            {
                (node.Right, var removed) = Remove(node.Right, value);
                return (node, removed);
            }
            else
            {
                // Caso: nodo encontrado
                if (node.Left == null && node.Right == null)
                {
                    return (null, true);
                }
                if (node.Left == null)
                {
                    return (node.Right, true);
                }
                if (node.Right == null)
                {
                    return (node.Left, true);
                }
                // Dos hijos: reemplazar con sucesor (mínimo del subárbol derecho)
                var successor = Min(node.Right);
                node.Value = successor.Value;
                (node.Right, var removed) = Remove(node.Right, successor.Value);
                return (node, true);
            }
        }

        private Node<T> Min(Node<T> node)
        {
            while (node.Left != null) node = node.Left;
            return node;
        }

        public IEnumerable<T> InOrder()
        {
            var stack = new Stack<Node<T>>();
            var cur = Root;
            while (cur != null || stack.Count > 0)
            {
                while (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.Left;
                }
                cur = stack.Pop();
                yield return cur.Value;
                cur = cur.Right;
            }
        }

        public IEnumerable<T> PreOrder()
        {
            if (Root == null) yield break;
            var stack = new Stack<Node<T>>();
            stack.Push(Root);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                yield return node.Value;
                if (node.Right != null) stack.Push(node.Right);
                if (node.Left  != null) stack.Push(node.Left);
            }
        }

        public IEnumerable<T> PostOrder()
        {
            if (Root == null) yield break;
            var stack1 = new Stack<Node<T>>();
            var stack2 = new Stack<Node<T>>();
            stack1.Push(Root);
            while (stack1.Count > 0)
            {
                var node = stack1.Pop();
                stack2.Push(node);
                if (node.Left  != null) stack1.Push(node.Left);
                if (node.Right != null) stack1.Push(node.Right);
            }
            while (stack2.Count > 0)
            {
                yield return stack2.Pop().Value;
            }
        }

        public void Clear()
        {
            Root = null;
            Count = 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var bst = new BinarySearchTree<int>();
            bool exit = false;
            while (!exit)
            {
                PrintMenu();
                Console.Write("Seleccione una opción: ");
                var input = Console.ReadLine();
                Console.WriteLine();
                switch (input)
                {
                    case "1":
                        InsertValues(bst);
                        break;
                    case "2":
                        PrintTraversal("Inorden", bst.InOrder());
                        break;
                    case "3":
                        PrintTraversal("Preorden", bst.PreOrder());
                        break;
                    case "4":
                        PrintTraversal("Postorden", bst.PostOrder());
                        break;
                    case "5":
                        SearchValue(bst);
                        break;
                    case "6":
                        DeleteValue(bst);
                        break;
                    case "7":
                        bst.Clear();
                        Console.WriteLine("Árbol vaciado.\n");
                        break;
                    case "8":
                        LoadSample(bst);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.\n");
                        break;
                }
            }
            Console.WriteLine("¡Hasta luego!");
        }

        static void PrintMenu()
        {
            Console.WriteLine("==== MENÚ ÁRBOL BINARIO (ABB) ====");
            Console.WriteLine("1) Insertar valor(es)");
            Console.WriteLine("2) Recorrido Inorden   (izq, raíz, der)");
            Console.WriteLine("3) Recorrido Preorden  (raíz, izq, der)");
            Console.WriteLine("4) Recorrido Postorden (izq, der, raíz)");
            Console.WriteLine("5) Buscar elemento");
            Console.WriteLine("6) Eliminar elemento");
            Console.WriteLine("7) Vaciar árbol");
            Console.WriteLine("8) Cargar datos de ejemplo");
            Console.WriteLine("0) Salir");
            Console.WriteLine();
        }

        static void InsertValues(BinarySearchTree<int> bst)
        {
            Console.WriteLine("Ingrese números enteros separados por espacios:");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                Console.WriteLine("No se ingresaron valores.\n");
                return;
            }
            var parts = line.Split(new[] {' ', ',', ';'}, StringSplitOptions.RemoveEmptyEntries);
            int inserted = 0, skipped = 0;
            foreach (var p in parts)
            {
                if (int.TryParse(p, out int v))
                {
                    bst.Insert(v);
                    inserted++;
                }
                else
                {
                    skipped++;
                }
            }
            Console.WriteLine($"Insertados: {inserted}. Omitidos (no válidos/duplicados): {skipped}.\n");
        }

        static void PrintTraversal(string name, IEnumerable<int> seq)
        {
            Console.WriteLine($"-- {name} --");
            bool any = false;
            foreach (var v in seq)
            {
                Console.Write(v + " ");
                any = true;
            }
            if (!any) Console.Write("(vacío)");
            Console.WriteLine("\n");
        }

        static void SearchValue(BinarySearchTree<int> bst)
        {
            Console.Write("Valor a buscar: ");
            if (int.TryParse(Console.ReadLine(), out int v))
            {
                Console.WriteLine(bst.Contains(v)
                    ? $"El valor {v} SÍ está en el árbol.\n"
                    : $"El valor {v} NO está en el árbol.\n");
            }
            else
            {
                Console.WriteLine("Entrada no válida.\n");
            }
        }

        static void DeleteValue(BinarySearchTree<int> bst)
        {
            Console.Write("Valor a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int v))
            {
                Console.WriteLine(bst.Remove(v)
                    ? $"Se eliminó {v} correctamente. Tamaño actual: {bst.Count}\n"
                    : $"No se encontró {v} en el árbol.\n");
            }
            else
            {
                Console.WriteLine("Entrada no válida.\n");
            }
        }

        static void LoadSample(BinarySearchTree<int> bst)
        {
            int[] sample = { 50, 30, 70, 20, 40, 60, 80, 35, 45, 65, 85 };
            foreach (var x in sample) bst.Insert(x);
            Console.WriteLine("Datos de ejemplo cargados: " + string.Join(", ", sample) + "\n");
        }
    }
}
