# Console Trivial

## Version 01: Board and movement

A board of 28 squares will be shown, similar to the one in the example image. 
Players start in the upper left square. Each box will be represented internally 
as a struct (or a class) with 3 data: X, Y, category. There will be an array of 
28 squares, so that you can calculate the square which you must move to. Each 
time a key is pressed, a random number between 1 and 6 will be generated and 
displayed and a single token will advance to the corresponding square, depending 
on the current square and the number obtained.

![](trivialConsole.png)


This version "will not be playable": the structure that was created previously 
will be preserved, but in this version no question will be asked to the player. 
In the next version, both skeletons will be merged.

---

## Entrega 01: Tablero y movimiento

Se mostrará un tablero de 28 casillas, similar al de la imagen de ejemplo. Los 
jugadores comienzan en la casilla superior izquierda. Cada casilla se 
representará internamente como un struct (o clase) con 3 datos: X, Y, 
especialidad. Existirá un array de 28 casillas, de modo que se pueda calcular 
la siguiente casilla a la que se deba mover. Cada vez que se pulse una tecla, 
se generará un número al azar entre 1 y 6, se mostrará dicho número y una única 
ficha avanzará a la casilla que corresponda, según cual sea su casilla actual y 
el número obtenido.

![](trivialConsole.png)

Por lo demás, esta versión "no será jugable": se conservará la estructura que 
se ha creado anteriormente, pero en esta versión no se hará ninguna pregunta al 
jugador. Será en la próxima versión en la que se unan ambos esqueletos.
