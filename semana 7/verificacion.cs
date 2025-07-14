#include <stdio.h>
#include <stdlib.h>

#define MAX 100

// Estructura de pila
typedef struct {
    char items[MAX];
    int top;
} Stack;

// Inicializar la pila
void initialize(Stack *s) {
    s->top = -1;
}

// Verificar si la pila está vacía
int isEmpty(Stack *s) {
    return s->top == -1;
}

// Insertar un elemento en la pila
void push(Stack *s, char value) {
    if (s->top < MAX - 1) {
        s->items[++s->top] = value;
    }
}

// Eliminar un elemento de la pila
char pop(Stack *s) {
    if (!isEmpty(s)) {
        return s->items[s->top--];
    }
    return '\0';
}

// Obtener el elemento del tope sin eliminarlo
char peek(Stack *s) {
    if (!isEmpty(s)) {
        return s->items[s->top];
    }
    return '\0';
}

// Verifica si los símbolos de apertura y cierre coinciden
int isMatchingPair(char opening, char closing) {
    return (opening == '(' && closing == ')') ||
           (opening == '{' && closing == '}') ||
           (opening == '[' && closing == ']');
}

// Función principal para verificar balanceo
int isBalanced(const char *expression) {
    Stack s;
    initialize(&s);

    for (int i = 0; expression[i] != '\0'; i++) {
        char c = expression[i];
        if (c == '(' || c == '{' || c == '[') {
            push(&s, c);
        } else if (c == ')' || c == '}' || c == ']') {
            if (isEmpty(&s) || !isMatchingPair(pop(&s), c)) {
                return 0;
            }
        }
    }
    return isEmpty(&s);
}

int main() {
    char expr[] = "{7 + (8 * 5) - [(9 - 7) + (4 + 1)]}";
    
    if (isBalanced(expr))
        printf("Fórmula balanceada.\n");
    else
        printf("Fórmula no balanceada.\n");

    return 0;
}
