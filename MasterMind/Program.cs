using System;

namespace MasterMind
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creamos unos console.writeline para poder hacer una especie de menu para el juego
            Console.WriteLine("¡Bienvenido a Master Mind!");
            Console.WriteLine("Selecciona un modo de juego:");
            Console.WriteLine("1: Juego Normal");
            Console.WriteLine("2: Modo Desarrollador");
            // La variable modo de juego nos permite saber si el jugador quiere
            // activar el modo desarollador para saber el codigo y testear o no
            int Modo_de_Juego = int.Parse(Console.ReadLine());
            // Inicializamos una variable boleana llamada Modo Desarrollador, si es verdadera, significa
            // Que el jugador entro al modo desarrollador y quiere ver el codigo en su partida
            // Inicializamos esa variable boleana en false
            bool Modo_Desarrollador = false;
            // Con este pequeño if comparamos si el jugador escribio 1 o 2, si escribio 1 es que quiere jugar normal
            // Pero si escribio 2, es decir, la variable modo de juego es 2, entonces quier el modo desarrollador
            // Por lo tanto, volvemos la variable verdadera
            if (Modo_de_Juego == 2)
            {
                Modo_Desarrollador = true;
                // Llamamos la funcion creador de codigo secreto, el cual nos creara un codigo secreto cuando lo necesitemos
                string Codigo_Secreto_Main = Creador_de_Codigo_Secreto();

                Jugar(Codigo_Secreto_Main, Modo_Desarrollador);
            }
            else if (Modo_de_Juego == 1)
            {
                // Llamamos la funcion creador de codigo secreto, el cual nos creara un codigo secreto cuando lo necesitemos
                string Codigo_Secreto_Main = Creador_de_Codigo_Secreto();

                Jugar(Codigo_Secreto_Main, Modo_Desarrollador);
            }


        }
        static string Creador_de_Codigo_Secreto()
        {
            // Creamos un random
            Random rnd = new Random();
            // Nuestro codigo secreto va a ser un arreglo de chars
            char[] Codigo_Secreto = new char[4];
            // La variable llamada aleatorizador sirve para almacenar los numeros randoms
            // Eso para poder asignarle un numero randomizado y que ese valor sirva para
            // Poder asignarle una letra aleatoria a cada espacio de nuestro arreglo de chars
            // Da igual que valor le pongamos, por que le chancaremos un random
            // En este caso lo inicializare con 0 por que se me hace razonable
            int Aleatorizador = 0;
            // Usamos una estructura iterativa for para que en cada posicion del arreglo
            // Coloquemos un caracter randomizado
            for (int i = 0; i < 4; i++)
            {
                // La variable aleatorizador se va a actualizar con un valor aleatorio con cada iteracion del for
                Aleatorizador = rnd.Next(4);
                // El switch se me hace mas practico que usar puro if, es demasiado para algo simple
                // El switch lo que hace es que dependiendo de que valor tenga la variable aleatorizador 
                // Se le asignara a posicion i del arreglo llamado codigo secreto un char.
                switch (Aleatorizador)
                {
                    // R es Rojo
                    case 0:
                        Codigo_Secreto[i] = 'R';
                        break;
                    // B es Azul
                    case 1:
                        Codigo_Secreto[i] = 'B';
                        break;
                    // G es Gris
                    case 2:
                        Codigo_Secreto[i] = 'G';
                        break;
                    // Y es Amarillo
                    case 3:
                        Codigo_Secreto[i] = 'Y';
                        break;
                }
            }
            //Creamos un string, y a ese string le damos los valores del arreglo de caracteres
            // Es decir, creamos un string con un arreglo de caracteres
            string Codigo_Secreto_Creado = "RBRG";
            return Codigo_Secreto_Creado;
        }
        static void Jugar(string Codigo_Secreto_Jugar, bool Modo_Desarrollador_Jugar)
        {
            int Intentos = 10;

            while (Intentos > 0)
            {
                if (Modo_Desarrollador_Jugar == true)
                {
                    Console.WriteLine("El codigo secreto es: " + Codigo_Secreto_Jugar);
                }
                Console.WriteLine("Ingresa el codigo que crees que es el de la computadora:");
                string Codigo_Jugador = Console.ReadLine();
                Codigo_Jugador = Codigo_Jugador.ToUpper();

                bool Es_MayorQue4Caracteres = false;


                if (Codigo_Jugador.Length != 4)
                {
                    Es_MayorQue4Caracteres = true;
                    while (Es_MayorQue4Caracteres == true)
                    {
                        Console.WriteLine("Tu codigo debe tener 4 caracteres.");
                        Console.WriteLine("Ingresa el codigo que crees que es el de la computadora:");
                        Codigo_Jugador = Console.ReadLine();
                        Codigo_Jugador = Codigo_Jugador.ToUpper();
                        if (Codigo_Jugador.Length == 4)
                        {
                            Es_MayorQue4Caracteres = false;
                        }
                    }
                    break;
                }

                string Pista = GenerarPista(Codigo_Jugador, Codigo_Secreto_Jugar);
                Console.WriteLine("Pista: " + Pista);

                if (Pista == "0000")
                {
                    Console.WriteLine("¡Felicidades! Has adivinado el código secreto.");
                    Console.ReadLine();
                }

                Intentos = Intentos - 1;
                Console.WriteLine("Te quedan " + Intentos + " intentos");
            }

            Console.WriteLine("Lo siento, has agotado tus 10 intentos. El código secreto era " + Codigo_Secreto_Jugar + ".");
            Console.ReadLine();
        }
        static string GenerarPista(string Codigo_Jugador_Comparador, string Codigo_Secreto_Comparador)
        {
            bool[] jugadorCoincidencias = new bool[4];
            bool[] secretoCoincidencias = new bool[4];
            int a = 0;
            char[] Car = new char[4] { '-', '-', '-', '-' };


            for (int i = 0; i < Codigo_Jugador_Comparador.Length; i++)
            {
                if (Codigo_Jugador_Comparador[i] == Codigo_Secreto_Comparador[i])
                {
                    jugadorCoincidencias[i] = true;
                    secretoCoincidencias[i] = true;
                    Car[a] = '0';
                    a = a + 1;
                }
            }

            for (int i = 0; i < Codigo_Jugador_Comparador.Length; i++)
            {
                for (int j = 0; j < Codigo_Secreto_Comparador.Length; j++)
                {
                    if (secretoCoincidencias[i] != true && jugadorCoincidencias[j] != true)
                    {
                        if (Codigo_Secreto_Comparador[i] == Codigo_Jugador_Comparador[j])
                        {
                            Car[a] = 'X';
                            secretoCoincidencias[i] = true;
                            jugadorCoincidencias[j] = true;
                            a = a + 1;
                        }
                    }
                }
            }

            string pista = new string(Car);

            Console.WriteLine(pista);
            return pista;
        }
    }
}