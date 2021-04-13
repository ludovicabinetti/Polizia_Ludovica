using System;
using System.Collections.Generic;

namespace Polizia_Ludovica
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Benvenuto presso la Stazione di polizia!");

            try
            {
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Cosa vuoi fare?");
                    Console.WriteLine("1. Mostra tutti gli agenti di polizia");
                    Console.WriteLine("2. Mostra agenti di polizia per area");
                    Console.WriteLine("3. Mostra agenti di polizia per anni di servizio");
                    Console.WriteLine("4. Inserisci un nuovo agente");
                    Console.WriteLine("0. Esci");

                    switch (Console.ReadKey().KeyChar)
                    {
                        case '1':
                            Console.WriteLine();
                            StampaElencoAgenti();
                            break;
                        case '2':
                            Console.WriteLine();
                            FiltraAgentiPerCodiceArea();
                            break;
                        case '3':
                            Console.WriteLine();
                            FiltraAgentiPerAnniServizio();
                            break;
                        case '4':
                            Console.WriteLine();
                            InserisciAgente();
                            break;
                        case '0':
                            return; // si esce dal programma
                        default:
                            Console.WriteLine("Scelta non valida. Riprova.");
                            break;
                    }

                } while (true);
            } 
            catch (Exception e)
            {
                Console.WriteLine("Si è generata un'eccezione imprevista: {0}", e);
                Console.WriteLine("Il programma verrà interrotto");
                return;
            }
         
        }

        private static void StampaElencoAgenti()
        {
            Console.WriteLine("Agenti in servizio presso la stazione:");
            foreach (AgentePolizia a in StazionePolizia.MostraAgenti())
                Console.WriteLine(a);
        }

        // stampa la lista dei codici area per agevolare l'utente nella scelta
        private static void StampaCodiceArea()
        {
            foreach (string s in StazionePolizia.RecuperaAreeMetropolitane())
                Console.WriteLine($"- {s}");
        }

        // stampa la lista degli agenti filtrati per area
        private static void FiltraAgentiPerCodiceArea()
        {
            // richiesta all'utente del codice area
            Console.WriteLine("Inserisci un codice area tra quelli disponibili: ");
            StampaCodiceArea();
            string codiceArea = Console.ReadLine();

            if (!StazionePolizia.RecuperaAreeMetropolitane().Contains(codiceArea))
            {
                Console.WriteLine("Codice area inserito non valido.");
                return;
            }

            List<AgentePolizia> agenti = StazionePolizia.MostraAgentiPerArea(codiceArea);

            if (agenti.Count == 0) // gestisco il caso in cui la lista è vuota (prova con EE002)
            {
                Console.WriteLine("Non ci sono agenti assegnati nell'area specificata.");
                return;
            }

            foreach (AgentePolizia a in agenti)
                Console.WriteLine(a);
        }

        // stampa lista degli agenti filtrati per anni di servizio
        private static void FiltraAgentiPerAnniServizio()
        {
            int anniServizio;
            do
            {
                Console.WriteLine("Inserisci anni di servizio: ");
            } while (!int.TryParse(Console.ReadLine(), out anniServizio));

            List<AgentePolizia> agenti = StazionePolizia.MostraAgentiPerAnniServizio(anniServizio);

            if (agenti.Count == 0) // gestisco il caso in cui la lista è vuota (prova con 5)
            {
                Console.WriteLine("Non ci sono agenti che soddisfano i criteri specificati.");
                return;
            }

            foreach (AgentePolizia a in agenti)
                Console.WriteLine(a);
        }

        private static void InserisciAgente()
        {
            Console.WriteLine("Inserisci il nome: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Inserisci il cognome: ");
            string cognome = Console.ReadLine();

            string codiceFiscale;
            do
            {
                Console.WriteLine("Inserisci il codiceFiscale: ");
                codiceFiscale = Console.ReadLine();
            } while (codiceFiscale.Length != 16);

            DateTime dataNascita;
            do
            {
                Console.WriteLine("Inserisci una data di nascita: ");
            } while (!DateTime.TryParse(Console.ReadLine(), out dataNascita) || dataNascita > DateTime.Today);

            int anniServizio;
            do
            {
                Console.WriteLine("Inserisci gli anni di servizio: ");
            } while (!int.TryParse(Console.ReadLine(), out anniServizio));

            try
            {
                AgentePolizia a = StazionePolizia.InserisciAgente(nome, cognome, codiceFiscale, dataNascita, anniServizio);
                Console.WriteLine($"Agente \"{a}\" aggiunto con successo.");
            }
            catch (AgenteDuplicatoException ex)
            {
                //Console.WriteLine("Inserimento non riuscito: codice fiscale ({0}) non valido.", ex.CodiceFiscale);
                Console.WriteLine(ex.Message);
            }
            catch (AgenteMinorenneException ex)
            {
                Console.WriteLine("Inserimento non riuscito: l'agente non può avere meno di 18 anni.");
            }
            
        }
    }
}
