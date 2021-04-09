using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polizia_Ludovica
{
    class AgentePolizia : Persona // estende la classe persona
    {
        public int AnniDiServizio { get; }

        public AgentePolizia(string nome, string cognome, string codiceFiscale, DateTime dataNascita,
            int anniDiServizio = 0) // se gli anni di servizio non vengono specificati
                                    // in automatico si suppone che sono 0
            : base(nome, cognome, codiceFiscale, dataNascita)
        {
            AnniDiServizio = anniDiServizio;
        }

        // implementazione di ToString per stampare a schermo le info desiderate
        public override string ToString()
        {
            if (AnniDiServizio == 1)
                return $"{CodiceFiscale} - {Nome} {Cognome} - {AnniDiServizio} anno di servizio";

            return $"{CodiceFiscale} - {Nome} {Cognome} - {AnniDiServizio} anni di servizio";
        }
    }
}
