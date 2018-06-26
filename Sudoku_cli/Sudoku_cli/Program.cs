/*
SUDOKU 
NAJPROSTSZA WERSJA Z NAJPROSTSZYCH
ALGORYTM SPRAWDZANIA NIEEFEKTYWNY, ALE DZIAŁA !!!
 */


/**
 *
 * @author Marcin
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_cli
{
    class Program
    {
        static int[,] tab = new int[9, 9]; // Tabelka 9x9

        public static void RysujPlansze()
        { // rysujemy plansze 
            for (int i = 0; i < 10; i++)
            {
                if (i == 0 || i == 3 || i == 6 || i == 9)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        
                        if (k == 9)
                        {
                            Console.Write("****");
                            continue;
                        }
                        Console.Write("***");
                    }
                    Console.WriteLine();
                }
                if (i == 9)
                {
                    continue;
                }
                
                    for (int j = 0; j <= 9; j++) {

                    if (j == 0 || j == 3 || j == 6)
                    {
                        Console.Write("*");
                    }
                    if (j == 9)
                    {
                        Console.Write("*");
                        continue;
                    }

                    if (tab[i, j] == 0)
                    {
                        Console.Write(" - "); // jesli jakis element jest zerem - to rysujemy kreske 
                    }
                    else
                    {
                        Console.Write(" " + tab[i, j] + " "); // jezeli nie, to wypisujemy ten element 
                    }

                    //if ((i == 2 || i == 5 || i == 8) && (j == 2 || j == 5 || j == 8))
                    //{
                    //    Console.Write("*");
                    //}
                }
                Console.WriteLine("");
            }
        }

        public static void LosujPlansze()
        {
            // w przyszlosci zmienic na zwracanie tablicy ewentualnie zwracanie liczb ktore zostały wylosowane tak, by nie mozna bylo zmienic ich podczas wypelniania sudoku
            Random random = new Random();
            // uzupelniamy tablice z liczbami losowymi cyframi 1-9
            for (int i=0; i<9; i++)
            {
                int x = random.Next(0, 8);
                int y = random.Next(0, 8);

                tab[x, y] = random.Next(1, 9);
                if (SprawdzPlanszeR() > 0)
                {
                    int aktualna = tab[x, y];
                    tab[x, y] = random.Next(1, 9);
                    if (aktualna == tab[x, y])
                    {
                        tab[x, y] = random.Next(1, 9);
                    }
                }
            }
        }

        public static void ZmienCos()
        { // zmienanie elementu
            Console.WriteLine("x : ");
            int x = Int32.Parse(Console.ReadLine());
            Console.WriteLine("x : ");
            int y = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Wartosc : ");
            tab[x, y] = Int32.Parse(Console.ReadLine());
            RysujPlansze();
        }

        public static int checkNulls()
        { // sprawdz ile jeszcze zer pozostało do wypelnienia
            int nulls = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (tab[i, j] == 0)
                    {
                        nulls++;
                    }
                }
            }
            return nulls;
        }

        public static int checkCell(int wiersz, int kolumna)
        { // sprawdzanie komorki
            int temp = tab[wiersz, kolumna];
            int err = 0;
            for (int i = 0; i < 9; i++)
            {
                if (tab[i, kolumna] == 0) continue;
                if (wiersz == i) continue;
                if (temp == tab[i, kolumna])
                {
                    err++;
                }
            }

            for (int j = 0; j < 9; j++)
            {
                if (tab[wiersz, j] == 0) continue;
                if (kolumna == j) continue;
                if (temp == tab[wiersz, j])
                {
                    err++;
                }
            }
            return err;
        }

        public static void SprawdzPlansze()
        { // sprawdzanie planszy w poszukiwaniu bledow
            int errors = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int err = checkCell(i, j);
                    errors = errors + err;
                }
            }
            if (errors > 0)
            {
                Console.WriteLine("Sa bledy");
            }
            else
            {
                Console.WriteLine("Nie ma bledow");
            }
            int nulls = checkNulls(); // pobieramy ilosc zer
            if (nulls == 0 && errors == 0)
            { // jezeli nie ma juz pustych pol i nie ma bledow to rozwiazane sudoku
                Console.WriteLine("GRATULACJE !!!! ROZWIĄZAŁEŚ SUDOKU.");
            }
        }

        public static int SprawdzPlanszeR()
        {
            int errors = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int err = checkCell(i, j);
                    errors = errors + err;
                }
            }
            if (errors > 0)
            {
                return errors;
            }
            else
            {
                return 0;
            }
        }

        public static void Main(String[] args)
        {
            int w = 0;  // zmienna sterujaca, na razie bezuzyteczna
            int moves = 0; // ruchy
            LosujPlansze();
            RysujPlansze();
            ZmienCos();
            moves++;
            while (w == 0)
            {
                ZmienCos();
                moves++;
                if (moves > 2)
                {
                    SprawdzPlansze();
                }

            }
        }
    }
}

