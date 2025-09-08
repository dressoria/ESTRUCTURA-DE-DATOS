from __future__ import annotations
from storage import Biblioteca
from models import Libro

DATAFILE = "data.json"

def menu():
    print("\n=== Biblioteca UEA — Conjuntos y Mapas ===")
    print("1) Cargar datos")
    print("2) Guardar datos")
    print("3) Registrar libro")
    print("4) Actualizar libro")
    print("5) Eliminar libro")
    print("6) Consultas y reportes")
    print("0) Salir")

def menu_reportes():
    print("\n--- Consultas/Reportería ---")
    print("1) Buscar por título")
    print("2) Buscar por autor")
    print("3) Buscar por categoría")
    print("4) Listar disponibles")
    print("5) Listar prestados")
    print("6) Conteo por autor")
    print("7) Conteo por categoría")
    print("8) Verificar duplicados por título")
    print("9) Cargar datos de ejemplo")
    print("0) Volver")

def input_set(mensaje: str):
    raw = input(mensaje + " (separa por coma): ").strip()
    return frozenset({x.strip() for x in raw.split(",") if x.strip()})

def main():
    b = Biblioteca()
    while True:
        menu()
        op = input("Opción: ").strip()
        if op == "1":
            b.cargar(DATAFILE)
            print("Datos cargados.")
        elif op == "2":
            b.guardar(DATAFILE)
            print("Datos guardados en", DATAFILE)
        elif op == "3":
            isbn = input("ISBN: ").strip()
            titulo = input("Título: ").strip()
            autor = input("Autor: ").strip()
            categorias = input_set("Categorías")
            disp = input("¿Disponible? (s/n): ").strip().lower() != "n"
            ok = b.registrar(Libro(isbn, titulo, autor, categorias, disp))
            print("Registrado." if ok else "ISBN ya existe.")
        elif op == "4":
            isbn = input("ISBN a actualizar: ").strip()
            titulo = input("Nuevo título (enter para mantener): ").strip()
            autor = input("Nuevo autor (enter para mantener): ").strip()
            cats_raw = input("Nuevas categorías (coma, enter para mantener): ").strip()
            disp_raw = input("¿Disponible? (s/n/enter para mantener): ").strip().lower()
            cambios = {}
            if titulo: cambios["titulo"] = titulo
            if autor: cambios["autor"] = autor
            if cats_raw: cambios["categorias"] = frozenset({c.strip() for c in cats_raw.split(",") if c.strip()})
            if disp_raw in ("s","n"): cambios["disponible"] = (disp_raw == "s")
            print("Actualizado." if b.actualizar(isbn, **cambios) else "ISBN no existe.")
        elif op == "5":
            isbn = input("ISBN a eliminar: ").strip()
            print("Eliminado." if b.eliminar(isbn) else "ISBN no existe.")
        elif op == "6":
            while True:
                menu_reportes()
                rop = input("Opción: ").strip()
                if rop == "1":
                    q = input("Palabra en título: ")
                    for l in b.buscar_por_titulo(q): print(l)
                elif rop == "2":
                    a = input("Autor: ")
                    for l in b.libros_por_autor(a): print(l)
                elif rop == "3":
                    c = input("Categoría: ")
                    for l in b.libros_por_categoria(c): print(l)
                elif rop == "4":
                    for l in b.disponibles(): print(l)
                elif rop == "5":
                    for l in b.prestados(): print(l)
                elif rop == "6":
                    for a, n in b.contar_por_autor(): print(a, n)
                elif rop == "7":
                    for c, n in b.contar_por_categoria(): print(c, n)
                elif rop == "8":
                    for t, isbns in b.verificar_duplicados_titulo(): print(t, isbns)
                elif rop == "9":
                    b.cargar_ejemplos(); print("Datos de ejemplo cargados.")
                elif rop == "0":
                    break
                else:
                    print("Opción inválida.")
        elif op == "0":
            break
        else:
            print("Opción inválida.")

if __name__ == "__main__":
    main()
