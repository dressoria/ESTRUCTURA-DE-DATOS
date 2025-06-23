# Agenda Telefónica con Programación Orientada a Objetos

class Contacto:
    def __init__(self, nombre, telefono, email):
        self.nombre = nombre
        self.telefono = telefono
        self.email = email

    def mostrar_contacto(self):
        print(f"Nombre: {self.nombre}")
        print(f"Teléfono: {self.telefono}")
        print(f"Email: {self.email}")
        print("-" * 30)

class Agenda:
    def __init__(self):
        self.contactos = []

    def agregar_contacto(self, nombre, telefono, email):
        nuevo = Contacto(nombre, telefono, email)
        self.contactos.append(nuevo)
        print("Contacto agregado exitosamente.\n")

    def mostrar_todos(self):
        if not self.contactos:
            print("La agenda está vacía.\n")
        else:
            print("Lista de contactos:\n")
            for contacto in self.contactos:
                contacto.mostrar_contacto()

    def buscar_contacto(self, nombre):
        encontrados = [c for c in self.contactos if c.nombre.lower() == nombre.lower()]
        if encontrados:
            print(f"Se encontró {len(encontrados)} contacto(s) con el nombre '{nombre}':\n")
            for c in encontrados:
                c.mostrar_contacto()
        else:
            print("Contacto no encontrado.\n")

    def eliminar_contacto(self, nombre):
        for i, c in enumerate(self.contactos):
            if c.nombre.lower() == nombre.lower():
                del self.contactos[i]
                print(f"Contacto '{nombre}' eliminado.\n")
                return
        print("Contacto no encontrado.\n")

# Menú interactivo
def mostrar_menu():
    print("AGENDA TELEFÓNICA")
    print("1. Agregar contacto")
    print("2. Mostrar todos los contactos")
    print("3. Buscar contacto por nombre")
    print("4. Eliminar contacto")
    print("5. Salir")

def ejecutar_agenda():
    agenda = Agenda()
    while True:
        mostrar_menu()
        opcion = input("Seleccione una opción: ")

        if opcion == "1":
            nombre = input("Nombre: ")
            telefono = input("Teléfono: ")
            email = input("Email: ")
            agenda.agregar_contacto(nombre, telefono, email)
        elif opcion == "2":
            agenda.mostrar_todos()
        elif opcion == "3":
            nombre = input("Ingrese el nombre a buscar: ")
            agenda.buscar_contacto(nombre)
        elif opcion == "4":
            nombre = input("Ingrese el nombre a eliminar: ")
            agenda.eliminar_contacto(nombre)
        elif opcion == "5":
            print("Saliendo del programa. ¡Hasta luego!")
            break
        else:
            print("Opción no válida. Intente nuevamente.\n")

# Ejecutar el programa
if __name__ == "__main__":
    ejecutar_agenda()
