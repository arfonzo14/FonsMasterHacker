using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1RepasoJunio2021
{
    class Videojuegos
    {
        public List<Videojuego> videogames = new List<Videojuego>();

        public int Posicion(int ano)
        {
            foreach (Videojuego v in videogames)
            {
                if (ano > v.Ano)
                {
                    return videogames.IndexOf(v);
                }

            }
            return videogames.Count;
        }


        public bool Eliminar(int n2, int n1 = 0)
        {
            if (n1 > videogames.Count - 1 || n2 > videogames.Count)
            {
                return false;
            }
            else
            {

                videogames.RemoveRange(n1, n2);
                return true;
            }
        }


        public List<Videojuego> Busqueda(Eestilo e)
        {
            List<Videojuego> listAux = new List<Videojuego>();
            foreach (Videojuego v in videogames)
            {
                if (v.Estilo == e)
                {
                    listAux.Add(v);
                }
            }
            return listAux;
        }
    }
}
