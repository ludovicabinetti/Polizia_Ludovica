using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polizia_Ludovica
{
    // classe che gestisce l'interazione con il database Polizia
    // (scelta di definirla static per evitare di istanziare inutilmente oggetti)
    static class StazionePolizia
    {
        static string _connectionString = ConfigurationManager.ConnectionStrings["Polizia"].ConnectionString;

        public static List<AgentePolizia> MostraAgenti()
        {
            List<AgentePolizia> agenti = new List<AgentePolizia>();

            using (SqlConnection conn = new SqlConnection(_connectionString)) // stabilisco una connessione
            using (SqlCommand cmd = new SqlCommand("Select * from AgentiPolizia", conn)) // definisco l'istruzione SQL da eseguire sul database
            {
                conn.Open(); // apro la connessione
                SqlDataReader reader = cmd.ExecuteReader(); // metto tutto dentro un reader

                while (reader.Read()) // vado a leggere gli elementi fintanto che ce ne sono
                {
                    // dal reader recupero gli elementi che mi interessano e faccio gli opportuni cast
                    // per istanziare un oggetto di tipo AgentePolizia e inserirlo nella lista da restituire
                    agenti.Add(new AgentePolizia(reader["Nome"].ToString(),
                        reader["Cognome"].ToString(), reader["CodiceFiscale"].ToString(), (DateTime)reader["DataNascita"],
                        (int)reader["AnniDiServizio"]));
                }
            }
            return agenti;
        }

        // restituisce una lista di aree metropolitane (usato per stampare all'utente la lista delle aree disponibili)
        // NB: è stato aggiunto nel database il vincolo di unicità del CodiceArea
        public static List<string> RecuperaAreeMetropolitane()
        {
            List<string> codiciAree = new List<string>();

            using (SqlConnection conn = new SqlConnection(_connectionString)) // stabilisco una connessione
            using (SqlCommand cmd = new SqlCommand("Select * from AreeMetropolitane", conn)) // istruzione SQL da eseguire sul database
            {
                conn.Open(); // apro la connessione

                SqlDataReader reader = cmd.ExecuteReader(); // metto tutto dentro un reader

                while (reader.Read()) // vado a leggere gli elementi fintanto che ce ne sono
                {
                    // dal reader recupero gli elementi che mi interessano e faccio gli opportuni cast
                    // per istanziare un oggetto di tipo AgentePolizia e inserirlo nella lista da restituire
                    codiciAree.Add(reader["CodiceArea"].ToString());
                }
            }

            return codiciAree;
        }

        public static List<AgentePolizia> MostraAgentiPerArea(string codiceArea)
        {
            List<AgentePolizia> agenti = new List<AgentePolizia>();

            using (SqlConnection conn = new SqlConnection(_connectionString)) // stabilisco una connessione
            using (SqlCommand cmd = new SqlCommand("Select Nome, Cognome, CodiceFiscale, DataNascita, AnniDiServizio from AreeMetropolitane " +
                "join Assegnazioni on AreeMetropolitane.IdArea = Assegnazioni.IdArea " +
                "join AgentiPolizia on Assegnazioni.IdAgente = AgentiPolizia.IdAgente " +
                "where AreeMetropolitane.CodiceArea = @codiceArea", conn)) // istruzione SQL da eseguire sul database
            {
                cmd.Parameters.AddWithValue("@codiceArea", codiceArea); // valorizzo il parametro

                conn.Open(); // apro la connessione

                SqlDataReader reader = cmd.ExecuteReader(); // metto tutto dentro un reader

                while (reader.Read()) // vado a leggere gli elementi fintanto che ce ne sono
                {
                    // dal reader recupero gli elementi che mi interessano e faccio gli opportuni cast
                    // per istanziare un oggetto di tipo AgentePolizia e inserirlo nella lista da restituire
                    agenti.Add(new AgentePolizia(reader["Nome"].ToString(),
                        reader["Cognome"].ToString(), reader["CodiceFiscale"].ToString(), (DateTime)reader["DataNascita"],
                        (int)reader["AnniDiServizio"]));
                }
            }

            return agenti;
        }

        public static List<AgentePolizia> MostraAgentiPerAnniServizio(int anniServizio)
        {
            List<AgentePolizia> agenti = new List<AgentePolizia>();

            using (SqlConnection conn = new SqlConnection(_connectionString)) // stabilisco una connessione
            using (SqlCommand cmd = new SqlCommand("Select * from AgentiPolizia " +
                "where AnniDiServizio >= @anniServizio ", conn)) // istruzione SQL da eseguire sul database
            {
                cmd.Parameters.AddWithValue("@anniServizio", anniServizio); // valorizzo il parametro

                conn.Open(); // apro la connessione

                SqlDataReader reader = cmd.ExecuteReader(); // metto tutto dentro un reader

                while (reader.Read()) // vado a leggere gli elementi fintanto che ce ne sono
                {
                    // dal reader recupero gli elementi che mi interessano e faccio gli opportuni cast
                    // per istanziare un oggetto di tipo AgentePolizia e inserirlo nella lista da restituire
                    agenti.Add(new AgentePolizia(reader["Nome"].ToString(),
                        reader["Cognome"].ToString(), reader["CodiceFiscale"].ToString(), (DateTime)reader["DataNascita"],
                        (int)reader["AnniDiServizio"]));
                }
            }
            return agenti;
        }

        public static AgentePolizia InserisciAgente(string nome, string cognome, string codiceFiscale,
                                    DateTime dataNascita, int anniServizio = 0)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString)) // stabilisco una connessione
            using (SqlDataAdapter da = new SqlDataAdapter("Select Nome, Cognome, CodiceFiscale, DataNascita, AnniDiServizio from AgentiPolizia ", conn)) // istruzione SQL da eseguire
                                                                                                                                                         // (semplicemente seleziono la tabella su cui voglio operare)
            {
                DataSet ds = new DataSet(); // creo un dataset

                // grazie al Data Adapter, inserisco i dati recuperati nel database in una tabella del mio dataset 
                da.Fill(ds, "AgentiPolizia"); // "AgentiPolizia" = nome della tabella 

                // per comodità metto la tabella in un oggetto DataTable
                DataTable tabellaAgenti = ds.Tables["AgentiPolizia"];

                // inserisco una nuova riga con i parametri passati alla funzione
                tabellaAgenti.Rows.Add(nome, cognome, codiceFiscale, dataNascita, anniServizio);

                // generazione automatica del comando SQL per l'inserimento
                SqlCommandBuilder cb = new SqlCommandBuilder(da);

                // aprire manualmente connessione conn:open
                // fai update, fare un cmd e tornare l'identity

                // aggiornamento del database con le modifiche apportate sul dataset
                da.Update(tabellaAgenti);
            }

            return new AgentePolizia(nome, cognome, codiceFiscale, dataNascita, anniServizio);
        }
    }
}
