using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("usa2.txt"))
            {
                var raw = File.ReadAllLines("usa2.txt");

                // we only want 5 letter US words
                var words = raw.Where(a => a.Length == 5).ToList();

                // write to new dictionary file
                StringBuilder sb = new StringBuilder();

                foreach (var w in words)
                {
                    sb.AppendLine(w.Trim());
                }

                // check whether output dictionary already exists
                if (!File.Exists("5LetterDictionary.txt"))
                {
                    File.Delete("5LetterDictionary.txt");
                }

                // output file
                File.WriteAllText("5LetterDictionary.txt", sb.ToString());

                // copy to dictionary parser project
                File.Copy("5LetterDictionary.txt", @"..\..\..\WordleSolver\5LetterDictionary.txt");
            }
            else
            {
                Console.WriteLine("Input dictionary not found!");
            }

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
