// Dorjee Khampa Herrezuelo Blasco

using System;

namespace Practica1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static Random rnd = new Random(); // generador de aleatorios (para mover abeja)
        const int ANCHO = 12, ALTO = 9; // dimensiones del área de juego
        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(ANCHO, ALTO); // tamaño de la consola
            int jugF = 6, jugC = 7; // posición del jugador
            int abejaF = 0;  // posicion de la abeja fil
            //int abejaC = rnd.Next(0, ANCHO-1); // posición de la abeja col
            int abejaC = ANCHO / 2;
            int delta = 100; // retardo entre frames (ms)
            bool colision = false; // colisión entre abeja y jugador
            int frameCounter = 0; // Para controlar el movimiento de la abeja cada 2 frames
            bool quit = false;

            // Renderizado inicial
            render(jugC, jugF, abejaC, abejaF, colision);

            // Bucle del juego
                while (!colision && !quit) {
                // recogida de de input
                string s = "";
                while (Console.KeyAvailable)
                    s = (Console.ReadKey(true)).KeyChar.ToString().ToUpper();

                switch (s)
                {
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
                    case "Q":
                        quit = true;
                        break;
                }

                //Detección de colisión después de mover al jugador
                if (abejaF == jugF && abejaC == jugC)
                {
                    colision = true;
                }

                if (!colision)
                {
                    //movimiento de la abeja sin IA
                    //movimientoAbeja(ref abejaC, ref abejaF);
                    frameCounter++;
                    if(frameCounter % 2 == 0)
                    {
                        // movimiento aleatorio de la abeja
                        movimientoIAAbejaIA(jugC, jugF, ref abejaC, ref abejaF);
                        //movimientoAbeja(ref abejaC, ref abejaF);

                    }

                    // Detección de colisión después de mover a la abeja
                    if (abejaF == jugF && abejaC == jugC)
                    {
                        colision = true;
                    }
                }

                // renderizado de las entidades en consola   
                render(jugC, jugF, abejaC, abejaF,colision);
                
                // retardo entre frames
                System.Threading.Thread.Sleep(delta);
            }
        } 
        static void render(int jugC, int jugF, int abejaC, int abejaF, bool collision)
        {
            if (collision)
            {
                Console.Clear();
                Console.SetCursorPosition(jugC, jugF);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write('*');
                Console.Beep(); // Sonido de muerte
                Console.SetCursorPosition(ANCHO / 2, ALTO / 2);
                Console.WriteLine("GAME OVER");
            }
            else
            {
                Console.Clear();
                Console.SetCursorPosition(jugC, jugF);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write('O');
                Console.SetCursorPosition(abejaC, abejaF);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write('+');
            }
            
        }

        static void movimientoAbeja(ref int abejaC, ref int abejaF)
        {
            // movimiento aleatorio de la abeja
            int direccion = rnd.Next(1, 5);  // Genera un número aleatorio entre 1 y 4
            switch (direccion)
            {
                case 1:  // Arriba
                    if (abejaF > 0) abejaF--;
                    break;
                case 2:  // Abajo
                    if (abejaF < ALTO - 1) abejaF++;
                    break;
                case 3:  // Izquierda
                    if (abejaC > 0) abejaC--;
                    break;
                case 4:  // Derecha
                    if (abejaC < ANCHO - 1) abejaC++;
                    break;
            }
        }
        static void movimientoIAAbejaIA(int jugC, int jugF, ref int abejaC, ref int abejaF)
        {
            // Calcula el vector de dirección
            int dirC = jugC - abejaC;
            int dirF = jugF - abejaF;

            // Decide el movimiento calculandio la diferencia
            if (Math.Abs(dirC) > Math.Abs(dirF))
            {
                if (dirC > 0 && abejaC < ANCHO - 1) abejaC++;      // Mover a la derecha
                else if (dirC < 0 && abejaC > 0) abejaC--;         // Mover a la izquierda
            }
            else
            {
                if (dirF > 0 && abejaF < ALTO - 1) abejaF++;       // Mover hacia abajo
                else if (dirF < 0 && abejaF > 0) abejaF--;         // Mover hacia arriba
            }
        }
    }
}
   

