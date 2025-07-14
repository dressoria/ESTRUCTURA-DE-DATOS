using System;
using System.Collections.Generic;

class Torre
{
    public Stack<int> Discos { get; } = new Stack<int>();
    public string Nombre { get; }

    public Torre(string nombre) => Nombre = nombre;

    public void MoverDiscoA(Torre destino)
    {
        int disco = Discos.Pop();
        destino.Discos.Push(disco);
        Console.WriteLine($"Mover disco {disco} de {Nombre} a {destino.Nombre}");
    }
}

class TorresDeHanoi
{
    static void ResolverHanoi(int n, Torre origen, Torre auxiliar, Torre destino)
    {
        if (n == 1)
            origen.MoverDiscoA(destino);
        else
        {
            ResolverHanoi(n - 1, origen, destino, auxiliar);
            origen.MoverDiscoA(destino);
            ResolverHanoi(n - 1, auxiliar, origen, destino);
        }
    }

    static void Main()
    {
        int numDiscos = 3; // Puedes cambiar este valor
        Torre torreA = new Torre("A");
        Torre torreB = new Torre("B");
        Torre torreC = new Torre("C");

        for (int i = numDiscos; i >= 1; i--)
            torreA.Discos.Push(i);

        ResolverHanoi(numDiscos, torreA, torreB, torreC);
    }
}
