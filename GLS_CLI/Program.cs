
using GLS_CLI.Models;

namespace GLS_CLI
{
    public class Program
    {
        public static List<AutoAdatok> autoAdatokKollekcio = new List<AutoAdatok>();
        static void Main(string[] args)
        {
            Beolvasas();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat6();
            Feladat7();
        }

        private static void Feladat7()
        {
            Console.WriteLine("7. Feladat:");
            var legtobbetVezetoSofor = autoAdatokKollekcio.GroupBy(a => a.SoforNeve).MaxBy(a => a.Count());
            Console.WriteLine($"\tA legtöbbet vezető sofőr: {legtobbetVezetoSofor.Key}, napok száma: {legtobbetVezetoSofor.Count()}");
        }

        private static void Feladat6()
        {
            Console.WriteLine("6. Feladat:");
            var haviLiterFogyasztassal = autoAdatokKollekcio.Average(a => AtlagosNapiLiterFogyasztas(a.NapiFogyasztasLiterben, a.NapiKilometer));
            Console.WriteLine($"Átlagos fogyasztás: {haviLiterFogyasztassal} liter/100 km");
        }

        public static double AtlagosNapiLiterFogyasztas(int literFogyasztas, int megtettKilometer)
        {
            if (megtettKilometer == 0 || literFogyasztas < 0)
            {
                return 0;
            }

            return (double)literFogyasztas / (double)megtettKilometer * 100;
            
        }

        private static void Feladat4()
        {
            Console.WriteLine("4. Feladat:");
            var osszesMegtettKilometer = autoAdatokKollekcio.Sum(a => a.NapiKilometer);
            Console.WriteLine($"\tAz összes megtett kilométer: {osszesMegtettKilometer} km");
        }

        private static void Feladat3()
        {
            Console.WriteLine("3. Feladat:");
            var kulonbozoSoforokSzama = autoAdatokKollekcio.DistinctBy(a => a.SoforNeve);
            Console.WriteLine($"\tKülönböző sofőrök száma: {kulonbozoSoforokSzama.Count()}");
        }

        private static void Feladat2()
        {
            Console.WriteLine("2. Feladat:");
            Console.WriteLine($"\tAz autó használatban töltött napjainak száma: {autoAdatokKollekcio.Count}");
        }

        public static void Beolvasas()
        {
            StreamReader streamReader = new StreamReader("GLS.txt");
            while (!streamReader.EndOfStream)
            {
                AutoAdatok autoadat = new AutoAdatok(streamReader.ReadLine());
                autoAdatokKollekcio.Add(autoadat);
            }
            streamReader.Close();
        }
    }
}
