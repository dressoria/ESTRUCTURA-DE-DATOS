#include <stdio.h>
#include <stdlib.h>

#define MAX 10

// Estructura de pila para representar una torre
typedef struct {
    int disks[MAX];
    int top;
    char name;
} Tower;

// Inicializar la torre
void initTower(Tower *t, char name) {
    t->top = -1;
    t->name = name;
}

// Verificar si la torre está vacía
int isEmptyTower(Tower *t) {
    return t->top == -1;
}

// Agregar un disco a la torre
void pushDisk(Tower *t, int disk) {
    t->disks[++t->top] = disk;
}

// Retirar un disco de la torre
int popDisk(Tower *t) {
    if (!isEmptyTower(t)) {
        return t->disks[t->top--];
    }
    return -1;
}

// Mover disco entre torres
void moveDisk(Tower *from, Tower *to) {
    int disk = popDisk(from);
    pushDisk(to, disk);
    printf("Mover disco %d desde %c hacia %c\n", disk, from->name, to->name);
}

// Resolver Hanoi con pilas
void hanoi(int n, Tower *src, Tower *aux, Tower *dest) {
    if (n == 1) {
        moveDisk(src, dest);
        return;
    }
    hanoi(n - 1, src, dest, aux);
    moveDisk(src, dest);
    hanoi(n - 1, aux, src, dest);
}

int main() {
    int n = 3; // Número de discos
    Tower A, B, C;
    initTower(&A, 'A');
    initTower(&B, 'B');
    initTower(&C, 'C');

    // Insertar discos en la torre A (origen)
    for (int i = n; i >= 1; i--) {
        pushDisk(&A, i);
    }

    printf("Movimientos para resolver Torres de Hanoi con %d discos:\n", n);
    hanoi(n, &A, &B, &C);

    return 0;
}
