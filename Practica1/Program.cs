using System;

namespace Practica1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static Random rnd = new Random(); // generador de aleatorios (para mover abeja)
        const int ANCHO = 12, ALTO = 9; // dimensiones del área de juego
        public static void Main(string[] args)
        {        
        Console.SetWindowSize(ANCHO, ALTO); // tamaño de la consola
        int jugF = 6, jugC = 7,  // posición del jugador
        abejaF, abejaC, // posición de la abeja
        delta = 300; // retardo entre frames (ms)
        bool colision = false; // colisión entre abeja y jugador
        while (!colision) {
                // recogida de de input
                string s = "";
                while (Console.KeyAvailable) s = (Console.ReadKey(true)).KeyChar.ToString();

                switch (s)
                {
                    case "w":
                        if (jugF > 0) jugF--; // Move up
                        break;
                    case "s":
                        if (jugF < ALTO - 1) jugF++; // Move down
                        break;
                    case "a":
                        if (jugC > 0) jugC--; // Move left
                        break;
                    case "d":
                        if (jugC < ANCHO - 1) jugC++; // Move right
                        break;
                    case "W":
                        if (jugF > 0) jugF--; // Move up
                        break;
                    case "S":
                        if (jugF < ALTO - 1) jugF++; // Move down
                        break;
                    case "A":
                        if (jugC > 0) jugC--; // Move left
                        break;
                    case "D":
                        if (jugC < ANCHO - 1) jugC++; // Move right
                        break;
                }

                // movimiento aleatorio de la abeja
                int beeF = rnd.Next(0,ALTO -1);
                int beeC = rnd.Next(0, ANCHO - 1);

                // detección de colisión
                if (beeF == jugF && beeC == jugC)
                {
                    colision = true;
                }

                // renderizado de las entidades en consola
                Console.Clear();
                Console.SetCursorPosition(jugC, jugF);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write('O');
                if (!colision)
                {
                    Console.SetCursorPosition(beeC, beeF);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write('*');
                }
                if(colision)
                {
                    Console.SetCursorPosition(jugC, jugF);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write('+');
                }

                // retardo entre frames
                System.Threading.Thread.Sleep(delta);


            }
        }
    }
}

/*En cada vuelta del bucle se hacen las siguientes acciones: lectura del input, movimiento del
jugador, movimiento de la abeja, detección de colisión, renderizado y retardo (ya implementado).
La primera acción es la recogida del input que se da ya implementada en las líneas 3 y 4. Estas
instrucciones hacen lectura no bloqueante de teclado: si se pulsa una tecla, se lee y se guarda su
valor en s; si no hay pulsación la ejecución continúa con (s=””). En cualquier caso la ejecución
no se para como ocurriría con la lectura (bloqueante) habitual Console.ReadLine(). El while de
línea 4 funciona como sigue: si se pulsa más de una tecla en el mismo frame se consumen todas
las pulsaciones, pero se guarda como input solo la última de ellas. . . ¿por qué es conveniente este
comportamiento?*/