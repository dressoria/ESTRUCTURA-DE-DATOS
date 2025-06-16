// Clase Estudiante.java

/**
 * Representa un estudiante con datos personales y tres teléfonos.
 */
public class Estudiante {
    // Atributos
    private int id;
    private String nombres;
    private String apellidos;
    private String direccion;
    private String[] telefonos; // Array de 3 teléfonos

    // Constructor
    public Estudiante(int id, String nombres, String apellidos, String direccion) {
        this.id = id;
        this.nombres = nombres;
        this.apellidos = apellidos;
        this.direccion = direccion;
        this.telefonos = new String[3]; // Inicializamos el array
    }

    // Setter para teléfono en posición
    public void setTelefono(int indice, String telefono) {
        if (indice >= 0 && indice < 3) {
            this.telefonos[indice] = telefono;
        } else {
            throw new IndexOutOfBoundsException("Índice fuera de rango");
        }
    }

    // Getter para teléfono
    public String getTelefono(int indice) {
        if (indice >= 0 && indice < 3) {
            return this.telefonos[indice];
        } else {
            throw new IndexOutOfBoundsException("Índice fuera de rango");
        }
    }

    // Método para mostrar datos completos
    public void mostrarInformacion() {
        System.out.println("ID: " + id);
        System.out.println("Nombres: " + nombres);
        System.out.println("Apellidos: " + apellidos);
        System.out.println("Dirección: " + direccion);
        System.out.println("Teléfonos:");
        for (int i = 0; i < telefonos.length; i++) {
            System.out.println("  Teléfono " + (i + 1) + ": " + telefonos[i]);
        }
    }

    // Método main para demostración
    public static void main(String[] args) {
        Estudiante e = new Estudiante(123, "Ana", "Pérez", "Av. Siempre Viva 123");
        e.setTelefono(0, "0991234567");
        e.setTelefono(1, "0987654321");
        e.setTelefono(2, "0971122334");
        e.mostrarInformacion();
    }
}
