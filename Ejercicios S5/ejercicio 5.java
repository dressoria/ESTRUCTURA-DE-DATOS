import java.util.ArrayList;
import java.util.Collections;

class Numeros {
    private ArrayList<Integer> numerosLista;

    public Numeros() {
        numerosLista = new ArrayList<>();
        for (int i = 1; i <= 10; i++) {
            numerosLista.add(i);
        }
    }

    public void mostrarInvertido() {
        Collections.reverse(numerosLista);
        for (int i = 0; i < numerosLista.size(); i++) {
            System.out.print(numerosLista.get(i));
            if (i < numerosLista.size() - 1) System.out.print(", ");
        }
        System.out.println();
    }
}

public class Main {
    public static void main(String[] args) {
        Numeros numeros = new Numeros();
        numeros.mostrarInvertido();
    }
}
