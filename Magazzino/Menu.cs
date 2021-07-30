using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazzino
{
    class Menu
    {

        public static void Start()
        {
            
            bool keepOnGoing = true;
            do
            {
                Console.Clear();
                Console.WriteLine("MENU GESTIONE MAGAZZINO");
                Console.WriteLine();
                Console.WriteLine("1: Mostra Prodotti");
                Console.WriteLine("2: Nuovo Prodotto");
                Console.WriteLine("3: Modifica Prodotto");
                Console.WriteLine("4: Elimina Prodotto");
                Console.WriteLine("5: Reports");
                Console.WriteLine("6: Esci");
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
                        MagazzinoManager.MostraProdotti();
                        Console.WriteLine();
                        Console.Write("Premi un tasto\n>> ");
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();
                        MagazzinoManager.NuovoProdotto();
                        Console.WriteLine();
                        Console.Write("Premi un tasto\n>> ");
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.Clear();
                        MagazzinoManager.MostraProdotti();
                        MagazzinoManager.ModificaProdotto();
                        Console.WriteLine();
                        Console.Write("Premi un tasto\n>> ");
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.Clear();
                        MagazzinoManager.MostraProdotti();
                        MagazzinoManager.EliminaProdotto();
                        Console.WriteLine();
                        Console.Write("Premi un tasto\n>> ");
                        Console.ReadLine();
                        break;
                    case 5:
                        Console.Clear();
                        ReportManager.Start();
                        Console.WriteLine();
                        Console.Write("Premi un tasto\n>> ");
                        Console.ReadLine();
                        break;
                    case 6:
                        keepOnGoing = false;
                        break;
                    default:
                        Console.WriteLine("Indice menu non valido!");
                        break;
                }

            } while (keepOnGoing);

        }
    }
}
