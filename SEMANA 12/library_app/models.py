from __future__ import annotations
from dataclasses import dataclass, field
from typing import Set, Dict, Any

@dataclass(eq=True, frozen=True)
class Libro:
    isbn: str
    titulo: str
    autor: str
    categorias: frozenset[str] = field(default_factory=frozenset)
    disponible: bool = True

    def to_json(self) -> Dict[str, Any]:
        return {
            "isbn": self.isbn,
            "titulo": self.titulo,
            "autor": self.autor,
            "categorias": sorted(list(self.categorias)),
            "disponible": self.disponible,
        }

    @staticmethod
    def from_json(data: Dict[str, Any]) -> "Libro":
        return Libro(
            isbn=data["isbn"],
            titulo=data["titulo"],
            autor=data["autor"],
            categorias=frozenset(data.get("categorias", [])),
            disponible=bool(data.get("disponible", True)),
        )
