import random

SEED_PFIZER = 42
SEED_AZ = 99

def sample(names, k, seed):
    rng = random.Random(seed)
    pool = list(names)
    rng.shuffle(pool)
    return set(pool[:k])

def main():
    # Universo U: 500 ciudadanos
    universo = [f"Ciudadano {i}" for i in range(1, 501)]
    U = set(universo)

    # Conjuntos vacunados (75 y 75, con posible intersección)
    pfizer = sample(universo, 75, SEED_PFIZER)
    astrazeneca = sample(universo, 75, SEED_AZ)

    # Teoría de conjuntos
    union = pfizer | astrazeneca
    ambas_dosis = pfizer & astrazeneca
    solo_pfizer = pfizer - astrazeneca
    solo_astra = astrazeneca - pfizer
    no_vacunados = U - union

    # Función utilitaria para imprimir
    def mostrar(titulo, elementos):
        print(f"\n=== {titulo} (total: {len(elementos)}) ===")
        for e in sorted(elementos, key=lambda x: int(x.split()[-1])):  # orden por número
            print(e)

    # Resultados requeridos
    mostrar("Ciudadanos que NO se han vacunado", no_vacunados)
    mostrar("Ciudadanos que han recibido AMBAS dosis", ambas_dosis)
    mostrar("Ciudadanos que SOLO han recibido Pfizer", solo_pfizer)
    mostrar("Ciudadanos que SOLO han recibido AstraZeneca", solo_astra)

    # Verificación resumida
    print("\n--- Verificación ---")
    print("Total ciudadanos:", len(U))
    print("Pfizer (objetivo 75):", len(pfizer))
    print("AstraZeneca (objetivo 75):", len(astrazeneca))
    print("Vacunados (P ∪ A):", len(union))
    print("No vacunados (U − (P ∪ A)):", len(no_vacunados))
    print("Ambas dosis (P ∩ A):", len(ambas_dosis))
    print("Solo Pfizer (P − A):", len(solo_pfizer))
    print("Solo AstraZeneca (A − P):", len(solo_astra))

if __name__ == "__main__":
    main()
