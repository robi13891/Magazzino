using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazzino
{
    class MagazzinoManager
    {
        const string connectionString = @"Server=(localdb)\mssqllocaldb;Database=Magazzino;Trusted_Connection=True;";

        public static void MostraProdotti()
        {
            Console.WriteLine("ELENCO PRODOTTI");
            Console.WriteLine();
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Connessione al DataBase non riuscita!");

                SqlCommand selectProdotto = new SqlCommand("Select * from Prodotto", conn);
                selectProdotto.CommandType = System.Data.CommandType.Text;

                SqlDataReader reader = selectProdotto.ExecuteReader();

                Console.WriteLine();
                Console.WriteLine("{0,5}{1,10}{2,25}{3,40}{4,20}{5,10}", "ID", "Codice", "Categoria", "Descrizione", "Prezzo (PU)", "Quantità");
                Console.WriteLine(new String('-', 110));
                while (reader.Read())
                {
                    Console.WriteLine("{0,5}{1,10}{2,25}{3,40}{4,20}{5,10}", reader["ID"], reader["CodiceProdotto"], reader["Categoria"], reader["Descrizione"], reader["PrezzoUnitario"], reader["QuantitaDisponibile"]);
                }
                Console.WriteLine(new String('-', 110));

            }



        }

        public static void NuovoProdotto()
        {
            Console.WriteLine("NUOVO PRODOTTO");
            Console.WriteLine();
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Connessione al DataBase non riuscita!");


                Console.Write("Codice Prodotto: ");
                string codice = Console.ReadLine();
                Console.Write("Categoria: ");
                string categoria = Console.ReadLine();
                Console.Write("Descrizione: ");
                string descrizione = Console.ReadLine();
                Console.Write("Prezzo Unitario: ");
                string prezzo = Console.ReadLine();
                Console.Write("Quantita Disponibile: ");
                string quantita = Console.ReadLine();

                string insertCommand = "insert into Prodotto values( @codice, @categoria, @descrizione, @prezzo, @quantita)";
                SqlCommand insert = conn.CreateCommand();
                insert.CommandText = insertCommand;
                insert.CommandType = System.Data.CommandType.Text;

                insert.Parameters.AddWithValue("@codice", codice);
                insert.Parameters.AddWithValue("@categoria", categoria);
                insert.Parameters.AddWithValue("@descrizione", descrizione);
                insert.Parameters.AddWithValue("@prezzo", prezzo);
                insert.Parameters.AddWithValue("@quantita", quantita);

                Console.WriteLine();

                int result = insert.ExecuteNonQuery();
                if (result != 1) Console.WriteLine("Errore inserimento dati");
                else Console.WriteLine("Inserimento avvenuto con successo!");
            }
        }

        public static void ModificaProdotto()
        {
            Console.WriteLine("MODIFICA PRODOTTO");
            Console.WriteLine();
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();

                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Connessione al DataBase non riuscita!");


                Console.WriteLine("Inserisci ID del Prodotto che vuoi modificare");
                Console.Write(">> ");
                string id = Console.ReadLine();

                Console.WriteLine("\nNUOVI DATI PRODOTTO\n");

                Console.Write("Codice Prodotto: ");
                string codice = Console.ReadLine();
                Console.Write("Categoria: ");
                string categoria = Console.ReadLine();
                Console.Write("Descrizione: ");
                string descrizione = Console.ReadLine();
                Console.Write("Prezzo Unitario: ");
                string prezzo = Console.ReadLine();
                Console.Write("Quantita Disponibile: ");
                string quantita = Console.ReadLine();
                int.Parse(quantita);


                SqlCommand updateCommand = new SqlCommand();

                updateCommand.Connection = conn;
                updateCommand.CommandText = "UPDATE Prodotto " +
                    "SET CodiceProdotto = @codice, Categoria = @categoria, Descrizione = @descrizione, " +
                    "PrezzoUnitario = @prezzo, QuantitaDisponibile = @quantita " +
                    "WHERE ID = @id";

                updateCommand.CommandType = System.Data.CommandType.Text;

                updateCommand.Parameters.Add(
                    new SqlParameter(
                        "@id",
                        SqlDbType.Int,
                        50,
                        "ID"
                    )
                );

                updateCommand.Parameters.Add(
                    new SqlParameter(
                        "@codice",
                        SqlDbType.NVarChar,
                        50,
                        "CodiceProdotto"
                    )
                );

                updateCommand.Parameters.Add(
                    new SqlParameter(
                        "@categoria",
                        SqlDbType.NVarChar,
                        50,
                        "Categoria"
                    )
                );

                updateCommand.Parameters.Add(
                    new SqlParameter(
                        "@descrizione",
                        SqlDbType.NVarChar,
                        50,
                        "Descrizione"
                    )
                );

                updateCommand.Parameters.Add(
                    new SqlParameter(
                        "@prezzo",
                        SqlDbType.Int,
                        50,
                        "PrezzoUnitario"
                    )
                );

                updateCommand.Parameters.Add(
                    new SqlParameter(
                        "@quantita",
                        SqlDbType.Int,
                        50,
                        "QuantitaDisponibile"
                    )
                );


                Console.WriteLine();

                int result = updateCommand.ExecuteNonQuery();
                if (result != 1) Console.WriteLine("Errore inserimento dati");
                else Console.WriteLine("ModiFica avvenuta con successo!");

            }
        }

        public static void EliminaProdotto()
        {
            Console.WriteLine("ELIMINA PRODOTTO");
            Console.WriteLine();
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Connessione al DataBase non riuscita!");

                Console.WriteLine("Inserisci ID del Prodotto da Eliminare");
                Console.Write(">> ");
                string id = Console.ReadLine();

                SqlCommand deleteProdotto = new SqlCommand("Delete Prodotto where id = @id ", conn);
                deleteProdotto.CommandType = System.Data.CommandType.Text;

                deleteProdotto.Parameters.AddWithValue("@id", id);

                Console.WriteLine();

                int result = deleteProdotto.ExecuteNonQuery();

                if (result != 1) Console.WriteLine("Errore Aggiornamento Dati!");
                else Console.WriteLine("Eliminazione avvenuta con successo!");

            }
        }
               
    }
}
