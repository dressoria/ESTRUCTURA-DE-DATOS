import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Scanner;

class Curso {
    private List<String> asignaturas;
    private HashMap<String, Double> notas;

    public Curso() {
        asignaturas = new ArrayList<>();
        notas = new HashMap<>();
        asignaturas.add("Matemáticas");
        asignaturas.add("Física");
        asignaturas.add("Química");
        asignaturas.add("Historia");
        asignaturas.add("Lengua");
    }

    public void pedirNotas() {
        Scanner scanner = new Scanner(System.in);
        for (String asignatura : asignaturas) {
            System.out.print("Nota en " + asignatura + ": ");
            while (!scanner.hasNextDouble()) {
                System.out.println("Introduce un número válido.");
                scanner.next();
                System.out.print("Nota en " + asignatura + ": ");
            }
            double nota = scanner.nextDouble();
            notas.put(asignatura, nota);
        }
    }

    public void mostrarNotas() {
        for (String asignatura : asignaturas) {
            System.out.println("En " + asignatura + " has sacado " + notas.get(asignatura));
        }
    }
}

public class Main {
    public static void main(String[] args) {
        Curso curso = new Curso();
        curso.pedirNotas();
        curso.mostrarNotas();
    }
}
