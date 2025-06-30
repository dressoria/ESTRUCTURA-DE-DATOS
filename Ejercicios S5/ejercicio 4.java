import java.util.ArrayList;
import java.util.Collections;
import java.util.Scanner;

class Loteria {
    private ArrayList<Integer> numerosGanadores;

    public Loteria() {
        numerosGanadores = new ArrayList<>();
    }

    public void pedirNumeros() {
        Scanner scanner = new Scanner(System.in);
        System.out.println("Introduce 6 números ganadores:");
        for (int i = 0; i < 6; i++) {
            System.out.print("Número " + (i + 1) + ": ");
            while (!scanner.hasNextInt()) {
                System.out.println("Introduce un número válido.");
                scanner.next();
                System.out.print("Número " + (i + 1) + ": ");
            }
            int num = scanner.nextInt();
            numerosGanadores.add(num);
        }
    }

    public void mostrarNumerosOrdenados() {
        Collections.sort(numerosGanadores);
        System.out.println("Números ganadores ordenados:");
        for (int num : numerosGanadores) {
            System.out.print(num + " ");
        }
        System.out.println();
    }
}

public class Main {
    public static void main(String[] args) {
        Loteria loteria = new Loteria();
        loteria.pedirNumeros();
        loteria.mostrarNumerosOrdenados();
    }
}
