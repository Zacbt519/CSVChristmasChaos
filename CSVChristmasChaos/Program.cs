using CsvHelper;
using GenericParsing;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSVChristmasChaos
{
    class Program
    {

        public static List<SantaList> santaList = new List<SantaList>();
        static void Main(string[] args)
        {
            string fileOne = "D:/ElvesLists/Hunny Dew/nicelist.csv";
            var fileTwo = "D:/ElvesLists/Hunny Dew/naughtylist.csv";
            var fileThree = "D:/ElvesLists/Buddy/nicelist.csv";
            var fileFour = "D:/ElvesLists/Buddy/naughtylist.csv";
            var fileFive = "D:/ElvesLists/Ninny Muggins/naughtylist.csv";
            var fileSix = "D:/ElvesLists/Ninny Muggins/nicelist.csv";

            createRecord(GetElfName(fileOne), GetNaughtyOrNice(fileOne), fileOne);
            createRecord(GetElfName(fileTwo), GetNaughtyOrNice(fileTwo), fileTwo);
            createRecord(GetElfName(fileThree), GetNaughtyOrNice(fileThree), fileThree);
            createRecord(GetElfName(fileFour), GetNaughtyOrNice(fileFour), fileFour);
            createRecord(GetElfName(fileFive), GetNaughtyOrNice(fileFive), fileFive);
            createRecord(GetElfName(fileSix), GetNaughtyOrNice(fileSix), fileSix);
            sortList();
            writeToFile();
            Console.WriteLine("File Generated successfully. Press and enter any button to exit");
            Console.ReadLine();
        }

        public static void createRecord(string elfName,string non, string url)
        {
            using (GenericParserAdapter parser = new GenericParserAdapter(url))
            {
                DataTable dt = parser.GetDataTable();



                foreach (DataRow row in dt.Rows)
                {
                    SantaList s = new SantaList()
                    {
                        Country = row[2].ToString(),
                        Gift = row[1].ToString(),
                    };

                    int fname = row[0].ToString().LastIndexOf(" ");
                    s.FirstName = row[0].ToString().Substring(0, fname);
                    string lname = row[0].ToString();
                    int length = lname.Length;
                    s.LastName = lname.Substring(fname).Replace(" ", "");
                    s.ElfName = elfName;
                    s.NaughtyOrNice = non;
                    santaList.Add(s);

                }
                
            }
        }

        public static void sortList()
        {
            santaList = santaList.OrderBy(c => c.NaughtyOrNice == "Naughty").ThenBy(c => c.LastName).ThenBy(c => c.FirstName).ThenBy(c => c.Country).ThenBy(c => c.ElfName).ToList();
        }

        public static void writeToFile()
        {
           using(StreamWriter writer = new StreamWriter(@"D:\GIT\santaslist.zbt"))
            {
                foreach(SantaList s in santaList)
                {
                    writer.WriteLine(s.LastName + "," + s.FirstName + "," + s.Gift + "," + s.Country + "," + s.ElfName + "," + s.NaughtyOrNice);
                }
            }
        }

        private static string GetElfName(string URL)
        {
            string elf = "";
            string fileName = Path.GetFileName(URL);
            int end = URL.LastIndexOf("/");
            int fileLength = URL.Substring(end).Length;
            int subLength = URL.Length - 14 - fileLength;
            elf = URL.Substring(14, subLength);
            return elf;

        }


        private static string GetNaughtyOrNice(string URL)
        {
            string non = "";
            string fileName = Path.GetFileName(URL);
            fileName = fileName.Replace(".csv", "");
            if (fileName.ToLower() == "nicelist")
            {
                non = "Nice";
            }
            else if (fileName.ToLower() == "naughtylist")
            {
                non = "Naughty";
            }
            return non;
        }
    }
}
