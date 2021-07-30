using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazzino
{
    class ReportManager
    {
        const string connectionString = @"Server=(localdb)\mssqllocaldb;Database=Magazzino;Trusted_Connection=True;";


        public static void Start()
        {
            Console.Clear();
            Console.WriteLine("MENU REPORTS");
            Console.WriteLine();
            Console.WriteLine("1: Elenco Prodotti con giacenza limitata (Quantita < 10)");
            Console.WriteLine("2: Numero di Prodotti per Categoria");
            Console.WriteLine();
            Console.Write(">> ");
            bool isInt = int.TryParse(Console.ReadLine(), out int indexMenu);
            while (!isInt)
            {
                Console.WriteLine("Scelta non valida!");
                Console.Write(">> ");
                isInt = int.TryParse(Console.ReadLine(), out indexMenu);
            }

            switch (indexMenu)
            {
                case 1:
                    Console.Clear();
                    ReportManager.ProdottiGiacenzaLimitata();
                    Console.WriteLine();
                    break;
                case 2:
                    Console.Clear();
                    ReportManager.QuantitaProdottiPerCategoria();
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine("Indice menu non valido!");
                    break;
            }

        }

        public static void ProdottiGiacenzaLimitata()
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Connessione al DataBase non riuscita!");

                SqlCommand selectProdotto = new SqlCommand(
                    "select * from Prodotto where QuantitaDisponibile < 10"
                    , conn);
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

        public static void QuantitaProdottiPerCategoria()
        {

            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Connessione al DataBase non riuscita!");

                SqlCommand selectProdotto = new SqlCommand(
                    "select categoria, sum(QuantitaDisponibile) as quantita from prodotto group by categoria", conn);
                selectProdotto.CommandType = System.Data.CommandType.Text;

                SqlDataReader reader = selectProdotto.ExecuteReader();

                Console.WriteLine();
                Console.WriteLine("{0,20}{1,20}", "Categoria", "Quantità");
                Console.WriteLine(new String('-', 40));
                while (reader.Read())
                {
                    Console.WriteLine("{0,20}{1,20}", reader["categoria"], reader["quantita"]);
                }
                Console.WriteLine(new String('-', 40));

            }

        }

    }
}
