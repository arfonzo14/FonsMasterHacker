using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1RepasoJunio2021
{
    enum Eestilo
    {
        ARCADE, VIDEOAVENTURA, SHOOTEMUP, ESTRATEGIA, DEPORTIVO
    }
    class Videojuego
    {
        private string titulo;
        private int ano;
        private Eestilo estilo;


        public Eestilo Estilo
        {
            set
            {
                if (Enum.IsDefined(typeof(Eestilo), value))
                {
                    this.estilo = value;
                }
            }
            get
            {
                return this.estilo;
            }
        }

        public string Titulo
        {
            set
            {
                this.titulo = value;
            }
            get
            {
                return this.titulo;
            }
        }


        public int Ano
        {
            set
            {
                this.ano = value;
            }
            get
            {
                return this.ano;
            }
        }



        public Videojuego(string titulo, int ano, Eestilo e)
        {
            this.titulo = titulo;
            this.ano = ano;
            this.estilo = e;
        }


    }
}
