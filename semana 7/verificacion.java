using System;
using System.Collections.Generic;

class VerificacionParentesis
{
    /// <summary>
    /// Verifica si los símbolos están balanceados en una expresión.
    /// </summary>
    static bool EstaBalanceada(string expresion)
    {
        Stack<char> pila = new Stack<char>();
        foreach (char c in expresion)
        {
            if (c == '(' || c == '{' || c == '[')
                pila.Push(c);
            else if (c == ')' || c == '}' || c == ']')
            {
                if (pila.Count == 0) return false;
                char tope = pila.Pop();
                if ((c == ')' && tope != '(') ||
                    (c == '}' && tope != '{') ||
                    (c == ']' && tope != '['))
                    return false;
            }
        }
        return pila.Count == 0;
    }

    static void Main()
    {
        Console.Write("Ingrese la expresión: ");
        string input = Console.ReadLine();

        if (EstaBalanceada(input))
            Console.WriteLine("Fórmula balanceada.");
        else
            Console.WriteLine("Fórmula no balanceada.");
    }
}
