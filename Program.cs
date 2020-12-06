using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace aoc4
{
    class Program
    {
         
        static void Main(string[] args)
        {
            var passport = new Dictionary<string, string>();  //begin met een leeg paspoort. key=het passport veld
            int numberOfValidPassports = 0;
            //var passports = new List<Dictionary<string, string>>();
            string[] row;
            foreach (string line in File.ReadLines(@"C:\Repos\aoc4\input\passports.txt"))
            {
                Console.WriteLine(line);
                if (line == string.Empty)  // paspoort is helemaal ingelezen. let op je moet twee lege enters na het laatste password in inputfile hebben
                {
                    if (IsPassportValid(passport)) numberOfValidPassports++;   // controleer het paspoort
                    Console.WriteLine(IsPassportValid(passport));
                    passport = new Dictionary<string, string>();  //maak een nieuw leeg paspoort aan
                }
                else
                {
                    row = line.Split(" ").ToArray();  //de velden opslaan in een tijdelijke array
                    foreach (string field in row)
                    {
                        passport.Add(field.Split(":")[0], field.Split(":")[1]);   //sla de gelezen velden op in de password dictionary
                    }
                }

            }
            Console.WriteLine($"number of valid passports: {numberOfValidPassports}");
            // goede antwoord deel 1 = 250



        }

        public static bool IsPassportValid(Dictionary<string, string> passport)
        {
            //
            if (passport.Count == 8)  //alle velden aanwezig
            {
                return true;
            }
            else
            {
                if (passport.Count == 7 && !passport.ContainsKey("cid")) //er mist alleen het veld cid
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
           
        }

    }
}
