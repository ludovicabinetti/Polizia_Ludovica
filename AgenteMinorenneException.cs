using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polizia_Ludovica
{
    class AgenteMinorenneException : Exception
    {
        public DateTime DataNascita { get; }

        // costruttore di default di solito c'è
        public AgenteMinorenneException()
        { }

        // costruttori specifici per istanziare membri specifici
        public AgenteMinorenneException(string message)
            : base(message)
        { }

        public AgenteMinorenneException (string message, Exception inner)
            :base(message, inner)
        { }

        public AgenteMinorenneException(string message, DateTime dataNascita)
            : this(message, null, dataNascita)
        { }
        public AgenteMinorenneException(string message, Exception inner, DateTime dataNascita)
            : this(message, inner)
        {
            DataNascita = dataNascita;
        }
    }
}
