# ------------------------------------------------------------
# Práctica #04 - Cálculo de métricas de centralidades en grafos
# Universidad Estatal Amazónica - Árboles y Grafos
# ------------------------------------------------------------

import networkx as nx
import matplotlib.pyplot as plt
import time

# Inicio del cronómetro
inicio = time.time()

# Creación del grafo no dirigido
G = nx.Graph()

# Agregar nodos
G.add_nodes_from(["A", "B", "C", "D", "E"])

# Agregar aristas (conexiones)
edges = [("A", "B"), ("A", "C"), ("B", "C"), ("B", "E"), ("C", "E"), ("D", "E")]
G.add_edges_from(edges)

# Calcular métricas de centralidad
degree = nx.degree_centrality(G)
closeness = nx.closeness_centrality(G)
betweenness = nx.betweenness_centrality(G)
eigenvector = nx.eigenvector_centrality(G)

# Fin del cronómetro
fin = time.time()
tiempo_ejecucion = fin - inicio

# Mostrar resultados
print("=== MÉTRICAS DE CENTRALIDAD ===")
print("{:<5} {:<15} {:<15} {:<15} {:<15}".format("Nodo", "Grado", "Cercanía", "Intermediación", "Eigenvector"))
for nodo in G.nodes():
    print("{:<5} {:<15.3f} {:<15.3f} {:<15.3f} {:<15.3f}".format(
        nodo, degree[nodo], closeness[nodo], betweenness[nodo], eigenvector[nodo]))

print(f"\nTiempo de ejecución: {tiempo_ejecucion:.6f} segundos")

# Visualización del grafo
plt.figure(figsize=(6, 5))
pos = nx.spring_layout(G, seed=42)
nx.draw(G, pos, with_labels=True, node_color='skyblue', node_size=1000, edge_color='gray', font_weight='bold')
plt.title("Grafo - Práctica #04: Centralidades")
plt.show()
