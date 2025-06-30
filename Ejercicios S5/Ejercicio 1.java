import java.util.ArrayList;
import java.util.List;

class Curso {
    private List<String> asignaturas;

    public Curso() {
        asignaturas = new ArrayList<>();
        asignaturas.add("Matemáticas");
        asignaturas.add("Física");
        asignaturas.add("Química");
        asignaturas.add("Historia");
        asignaturas.add("Lengua");
    }

    public void mostrarAsignaturas() {
        for (String asignatura : asignaturas) {
            System.out.println(asignatura);
        }
    }
}

public class Main {
    public static void main(String[] args) {
        Curso curso = new Curso();
        curso.mostrarAsignaturas();
    }
}
