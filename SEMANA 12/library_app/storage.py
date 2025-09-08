from __future__ import annotations
from typing import Dict, Set, Iterable, List, Tuple
from collections import defaultdict
import json, os

from models import Libro

class Biblioteca:
    def __init__(self) -> None:
        self.catalogo: Dict[str, Libro] = {}
        self.indice_autor: Dict[str, Set[str]] = defaultdict(set)
        self.indice_categoria: Dict[str, Set[str]] = defaultdict(set)
        self.isbns: Set[str] = set()

    # --------- CRUD ---------
    def registrar(self, libro: Libro) -> bool:
        if libro.isbn in self.isbns:
            return False
        self.catalogo[libro.isbn] = libro
        self.isbns.add(libro.isbn)
        self.indice_autor[libro.autor].add(libro.isbn)
        for c in libro.categorias:
            self.indice_categoria[c].add(libro.isbn)
        return True

    def actualizar(self, isbn: str, **cambios) -> bool:
        if isbn not in self.catalogo:
            return False
        actual = self.catalogo[isbn]
        nuevo = Libro(
            isbn=isbn,
            titulo=cambios.get("titulo", actual.titulo),
            autor=cambios.get("autor", actual.autor),
            categorias=frozenset(cambios.get("categorias", actual.categorias)),
            disponible=cambios.get("disponible", actual.disponible),
        )
        # limpiar indices si cambian autor/categorias
        if actual.autor != nuevo.autor:
            self.indice_autor[actual.autor].discard(isbn)
            self.indice_autor[nuevo.autor].add(isbn)
        if actual.categorias != nuevo.categorias:
            for c in actual.categorias:
                self.indice_categoria[c].discard(isbn)
            for c in nuevo.categorias:
                self.indice_categoria[c].add(isbn)
        self.catalogo[isbn] = nuevo
        return True

    def eliminar(self, isbn: str) -> bool:
        if isbn not in self.catalogo: return False
        libro = self.catalogo.pop(isbn)
        self.isbns.discard(isbn)
        self.indice_autor[libro.autor].discard(isbn)
        for c in libro.categorias:
            self.indice_categoria[c].discard(isbn)
        return True

    # --------- Consultas / Reportería ---------
    def obtener(self, isbn: str) -> Libro | None:
        return self.catalogo.get(isbn)

    def buscar_por_titulo(self, palabra: str) -> List[Libro]:
        w = palabra.lower()
        return [l for l in self.catalogo.values() if w in l.titulo.lower()]

    def libros_por_autor(self, autor: str) -> List[Libro]:
        return [self.catalogo[i] for i in sorted(self.indice_autor.get(autor, set()))]

    def libros_por_categoria(self, categoria: str) -> List[Libro]:
        return [self.catalogo[i] for i in sorted(self.indice_categoria.get(categoria, set()))]

    def contar_por_autor(self) -> List[Tuple[str, int]]:
        return sorted(((a, len(isbns)) for a, isbns in self.indice_autor.items()), key=lambda x: x[1], reverse=True)

    def contar_por_categoria(self) -> List[Tuple[str, int]]:
        return sorted(((c, len(isbns)) for c, isbns in self.indice_categoria.items()), key=lambda x: x[1], reverse=True)

    def disponibles(self) -> List[Libro]:
        return [l for l in self.catalogo.values() if l.disponible]

    def prestados(self) -> List[Libro]:
        return [l for l in self.catalogo.values() if not l.disponible]

    def verificar_duplicados_titulo(self) -> List[Tuple[str, Set[str]]]:
        # títulos iguales con distinto ISBN
        rev: Dict[str, Set[str]] = defaultdict(set)
        for isbn, libro in self.catalogo.items():
            rev[libro.titulo.lower()].add(isbn)
        return [(t, isbns) for t, isbns in rev.items() if len(isbns) > 1]

    # --------- Persistencia ---------
    def guardar(self, path: str) -> None:
        data = [l.to_json() for l in self.catalogo.values()]
        with open(path, "w", encoding="utf-8") as f:
            json.dump(data, f, ensure_ascii=False, indent=2)

    def cargar(self, path: str) -> None:
        if not os.path.exists(path): return
        with open(path, "r", encoding="utf-8") as f:
            data = json.load(f)
        self.__init__()
        for item in data:
            self.registrar(Libro.from_json(item))

    # --------- Utilidades ---------
    def cargar_ejemplos(self) -> None:
        ejemplos = [
            Libro("978-0131103627", "The C Programming Language", "Kernighan & Ritchie", frozenset({"programacion"})),
            Libro("978-0262046305", "Introduction to Algorithms (4th)", "Cormen et al.", frozenset({"algoritmos", "teoria"})),
            Libro("978-0135166307", "Computer Networking: A Top-Down Approach", "Kurose & Ross", frozenset({"redes"})),
            Libro("978-1492051367", "Fluent Python, 2nd Edition", "Luciano Ramalho", frozenset({"python"})),
            Libro("978-0135972267", "Algorithms (4th printing 2022)", "Sedgewick & Wayne", frozenset({"algoritmos"})),
        ]
        for e in ejemplos:
            self.registrar(e)
