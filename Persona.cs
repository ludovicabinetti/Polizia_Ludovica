using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polizia_Ludovica
{
    abstract class Persona
    {
        public string Nome { get; /*protected set;*/ }
        public string Cognome { get; /*protected set;*/ }
        public string CodiceFiscale { get; /*protected set;*/ }
        public DateTime DataNascita { get; }

        public Persona(string nome, string cognome, string codiceFiscale, DateTime dataNascita)
        {
            Nome = nome;
            Cognome = cognome;
            CodiceFiscale = codiceFiscale;
            DataNascita = dataNascita;
        }

        // override del metodo Equals per stabilire uguaglianza di
        // due persone sulla base del loro codice fiscale
        public override bool Equals(object obj)
        {
            // controllo che il tipo dell'oggetto passato sia Persona
            if (obj.GetType() != this.GetType())
                return false;
            return ((Persona)obj).CodiceFiscale == CodiceFiscale;
        }

    }
}
