import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
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

    public void eliminarAprobadas() {
        Iterator<String> it = asignaturas.iterator();
        while (it.hasNext()) {
            String asignatura = it.next();
            if (notas.get(asignatura) >= 5.0) {
                it.remove();
            }
        }
    }

    public void mostrarRepetir() {
        System.out.println("Asignaturas que tienes que repetir:");
        for (String asignatura : asignaturas) {
            System.out.println(asignatura);
        }
    }
}

public class Main {
    public static void main(String[] args) {
        Curso curso = new Curso();
        curso.pedirNotas();
        curso.eliminarAprobadas();
        curso.mostrarRepetir();
    }
}

