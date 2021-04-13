using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polizia_Ludovica
{
    class AgenteDuplicatoException : Exception
    {
        public string CodiceFiscale { get; }

        // costruttore di default di solito c'è
        public AgenteDuplicatoException()
        { }

        // costruttori specifici per istanziare membri specifici
        public AgenteDuplicatoException(string message)
            : base(message)
        { }

        public AgenteDuplicatoException(string message, string codiceFiscale)
            :this(message,  null, codiceFiscale)
        { }

        public  AgenteDuplicatoException (string message, Exception inner)
            :base(message, inner)
        { }

        public AgenteDuplicatoException(string message, Exception inner, string codiceFiscale)
            : this(message, inner)
        {
            CodiceFiscale = codiceFiscale;
        }
    }
}
