#define DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1RepasoJunio2021
{
    class Program
    {
        public static void Main(string[] args)
        {
            menu();
        }


        public static void menu()
        {
            Videojuegos listaGames = new Videojuegos();
#if DEBUG
            listaGames.videogames.Insert(listaGames.Posicion(1995), new Videojuego("Jazz Jackrabbitt", 1995, Eestilo.ARCADE));
            listaGames.videogames.Insert(listaGames.Posicion(1993), new Videojuego("Duke Nukem 3D", 1993, Eestilo.SHOOTEMUP));
            listaGames.videogames.Insert(listaGames.Posicion(1981), new Videojuego("Galaga", 1981, Eestilo.VIDEOAVENTURA));
#endif

            int opcion;

            do
            {

                opcion = 0;

                Console.WriteLine("\nBienvenido al mejor menú del mundo, elige que quieres hacer con esta colección");
                Console.WriteLine("1. Inserta juego");
                Console.WriteLine("2. Elimina juego");
                Console.WriteLine("3. Visualiza juego según estilo");
                Console.WriteLine("4. Visualiza tabla");
                Console.WriteLine("5. Modifica un juego");
                Console.WriteLine("6. Salir\n");



                int.TryParse(Console.ReadLine(), out opcion);
                switch (opcion)
                {
                    case 1:
                        Videojuego vs = rellenaGame();
                        listaGames.videogames.Insert(listaGames.Posicion(vs.Ano), vs);

                        break;

                    case 2:
                        if (listaGames.videogames.Count != 0)
                        {
                            int posJuego1 = -1;
                            int posJuego2 = -1;
                            bool repiteElm = false;

                            Console.WriteLine("Selecciona  el juego que quieres eliminar");
                            Console.WriteLine("Dime un rango de videojuegos para eliminar, habiendo en la lista " + listaGames.videogames.Count + " elementos");

                            try
                            {
                                Console.WriteLine("Indice desde el cual borro:");
                                posJuego1 = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("\nCuantos borro:");
                                posJuego2 = Convert.ToInt32(Console.ReadLine());


                                if (posJuego1 > listaGames.videogames.Count - 1 || posJuego2 > listaGames.videogames.Count || posJuego1 < 0 || posJuego2 < 0 ||
                                    posJuego2 > (listaGames.videogames.Count - posJuego1))
                                {
                                    Console.WriteLine("Uno de los numeros está fuera de rango");
                                    repiteElm = true;
                                }
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Numero demasiado grande");
                                repiteElm = true;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Debes escribir un numero");
                                repiteElm = true;
                            }

                            if (!repiteElm)
                            {
                                Console.WriteLine("Juegos a eliminar:\n");
                                for (int i = posJuego1; i < posJuego2; i++)
                                {
                                    Console.WriteLine("Titulo: " + listaGames.videogames[i].Titulo);
                                    Console.WriteLine("Año: " + listaGames.videogames[i].Ano);
                                    Console.WriteLine("Estilo: " + listaGames.videogames[i].Estilo);
                                }
                                Console.WriteLine("Pulse 's' si desea borrar");
                                string respuesta = Console.ReadLine();
                                if (respuesta.Trim().ToLower() == "s")
                                {
                                    listaGames.Eliminar(posJuego2, posJuego1);
                                    Console.WriteLine("Juegos eliminados, BYE BYE");
                                }
                                else
                                {
                                    Console.WriteLine("Ningun juego ha sido eliminado");
                                }

                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay juegos que borrar");
                        }

                        break;

                    case 3:
                        if (listaGames.videogames.Count != 0)
                        {
                            Console.WriteLine("Te mostraré los videojuegos del género que escojas: ");


                            foreach (Videojuego v in listaGames.Busqueda(eligeEstilo()))
                            {
                                Console.WriteLine("Titulo: " + v.Titulo);
                                Console.WriteLine("Año: " + v.Ano);
                                Console.WriteLine("Estilo: " + v.Estilo);
                                Console.WriteLine("////////////");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay videojuegos que mostrar");
                        }
                        break;


                    case 4:
                        if (listaGames.videogames.Count != 0)
                        {
                            foreach (Videojuego v in listaGames.videogames)
                            {
                                if (v.Titulo.Length > 7)
                                {
                                    Console.WriteLine("Titulo: " + v.Titulo.Substring(0, 7) + "...");
                                }
                                else
                                {
                                    Console.WriteLine("Titulo: " + v.Titulo);
                                }

                                Console.WriteLine("Año: " + v.Ano);
                                Console.WriteLine("Estilo: " + v.Estilo.ToString());
                                Console.WriteLine("////////////");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay nada que visualizar");
                        }
                        break;

                    case 5:

                        if (listaGames.videogames.Count != 0)
                        {
                            int pos = -1;
                            Console.WriteLine("Dime la posicion del juego que quieres modificar");
                            bool numeroBueno = true;
                            try
                            {
                                Console.WriteLine("El numero debe estar situado entre 0 y " + (listaGames.videogames.Count - 1));
                                pos = Convert.ToInt32(Console.ReadLine());

                                if (pos > listaGames.videogames.Count - 1 || pos < 0)
                                {
                                    Console.WriteLine("No hay ningun juego con ese número");
                                    numeroBueno = false;
                                }

                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("Debes escribir un número");
                                numeroBueno = false;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Número demasiado grande");
                                numeroBueno = false;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Debes escribir un número");
                                numeroBueno = false;
                            }

                            if (numeroBueno)
                            {
                                Console.WriteLine("Juego a modificar:");

                                Console.WriteLine("Titulo: " + listaGames.videogames.ElementAt(pos).Titulo);
                                Console.WriteLine("Año: " + listaGames.videogames.ElementAt(pos).Ano);
                                Console.WriteLine("Estilo: " + listaGames.videogames.ElementAt(pos).Estilo.ToString());

                                Videojuego v = rellenaGame();

                                listaGames.videogames.RemoveAt(pos);
                                listaGames.videogames.Insert(listaGames.Posicion(v.Ano), v);

                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay nada que modificar. Deseas añadir un juego de todos modos? S/N");
                            string respuesta = Console.ReadLine();
                            if (respuesta.Trim().ToLower() == "s")
                            {
                                goto case 1;
                            }

                        }

                        break;

                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            } while (opcion != 6);
        }


        public static Videojuego rellenaGame()
        {
            int anInsert = 0;
            string titulGame;
            Eestilo styleGam = 0;
            Console.WriteLine("Dime el titulo del juego: ");
            titulGame = Console.ReadLine().Trim();
            bool anValid;


            do
            {
                anValid = true;
                Console.WriteLine("Dime el año");
                try
                {
                    anInsert = Convert.ToInt32(Console.ReadLine());
                    if (anInsert > 2024 || anInsert < 1958)
                    {
                        Console.WriteLine("Antes del 58 no existian los videojuegos y no voy a permitir anunciar juegos para sabe dios cuando");
                        anValid = false;
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Debes escribir un año válido");
                    anValid = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Debes escribir un año válido");
                    anValid = false;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Debes escribir un año válido");
                    anValid = false;
                }

            } while (!anValid);

            styleGam = eligeEstilo();

            return new Videojuego(titulGame, anInsert, styleGam);
        }



        public static Eestilo eligeEstilo()
        {
            int opcEnum = 0;
            bool flagEnum = false;
            Eestilo styleGam = Eestilo.ARCADE;

            do
            {
                flagEnum = true;
                opcEnum = -1;
                Console.WriteLine("Define el género eligiendo una opción del 0 al 4 incluidos");
                Console.WriteLine("0-ARCADE");
                Console.WriteLine("1-VIDEOAVENTURA");
                Console.WriteLine("2-SHOOTEMUP");
                Console.WriteLine("3-ESTRATEGIA");
                Console.WriteLine("4-DEPORTIVO");

                try
                {
                    opcEnum = Convert.ToInt32(Console.ReadLine());
                    if (opcEnum > Enum.GetNames(typeof(Eestilo)).Length - 1 || opcEnum < 0)
                    {
                        flagEnum = false;
                        Console.WriteLine("Valor fuera de la escala");
                    }
                    else
                    {
                        styleGam = (Eestilo)opcEnum;
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Debes escribir un numero integral con valores entre 0 y 4 incluidos");
                    flagEnum = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Debes escribir un numero integral con valores entre 0 y 4 incluidos");
                    flagEnum = false;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Debes escribir un numero integral con valores entre 0 y 4 incluidos");
                    flagEnum = false;
                }


            } while (!flagEnum);

            return styleGam;
        }


    }
}
