from __future__ import annotations
import random, string, time
from storage import Biblioteca
from models import Libro

def random_isbn(i: int) -> str:
    # Genera ISBN falsos pero únicos
    return f"978-{i:09d}"

def random_word(k=8) -> str:
    return ''.join(random.choice(string.ascii_letters) for _ in range(k))

def generar_libro(i: int) -> Libro:
    return Libro(
        isbn=random_isbn(i),
        titulo=f"Titulo {random_word(6)} {i}",
        autor=f"Autor {random.randint(1, max(2, i//10))}",
        categorias=frozenset({f"cat{random.randint(1,5)}"}),
        disponible=bool(random.getrandbits(1)),
    )

def benchmark(n: int) -> dict:
    b = Biblioteca()
    t0 = time.perf_counter()
    for i in range(n):
        b.registrar(generar_libro(i))
    t_insert = time.perf_counter() - t0

    # Búsquedas
    t0 = time.perf_counter()
    hits = 0
    for i in range(n):
        if b.obtener(random_isbn(i)) is not None:
            hits += 1
    t_lookup_hit = time.perf_counter() - t0

    t0 = time.perf_counter()
    for i in range(n, 2*n):
        _ = b.obtener(random_isbn(i))  # misses
    t_lookup_miss = time.perf_counter() - t0

    # Borrado de 10% de elementos
    t0 = time.perf_counter()
    for i in range(0, n, max(1, n//10)):
        b.eliminar(random_isbn(i))
    t_delete = time.perf_counter() - t0

    return {
        "n": n,
        "insert_s": t_insert,
        "lookup_hit_s": t_lookup_hit,
        "lookup_miss_s": t_lookup_miss,
        "delete_s": t_delete,
        "hits": hits,
    }

if __name__ == "__main__":
    random.seed(42)
    sizes = [1_000, 5_000, 10_000, 50_000]
    print("n,insert_s,lookup_hit_s,lookup_miss_s,delete_s,hits")
    for n in sizes:
        r = benchmark(n)
        print(f"{r['n']},{r['insert_s']:.6f},{r['lookup_hit_s']:.6f},{r['lookup_miss_s']:.6f},{r['delete_s']:.6f},{r['hits']}")
