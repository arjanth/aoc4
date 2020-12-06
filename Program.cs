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
            // goede antwoord deel 2 = 158



        }

        public static bool IsPassportValid(Dictionary<string, string> passport)
        {
            //
            if (passport.Count == 8 || (passport.Count == 7 && !passport.ContainsKey("cid")))  //alle velden aanwezig of er mist alleen het cid veld
            {
                if (passport["byr"].Length != 4) return false;
                if (int.Parse(passport["byr"]) < 1920 || int.Parse(passport["byr"])> 2002 ) return false;
                if (passport["iyr"].Length != 4) return false;
                if (int.Parse(passport["iyr"]) < 2010 || int.Parse(passport["iyr"]) > 2020) return false;
                if (passport["eyr"].Length != 4) return false;
                if (int.Parse(passport["eyr"]) < 2020 || int.Parse(passport["eyr"]) > 2030) return false;

                if (!(passport["hgt"].EndsWith("cm") || passport["hgt"].EndsWith("in")))  return false;
                try
                {
                    int hgt = int.Parse(passport["hgt"].Remove(passport["hgt"].Length - 2));  //haal laaste twee tekens (in of cm eraf) en maak er een int van
                    if (passport["hgt"].EndsWith("cm"))
                    {
                        if(hgt<150 || hgt>193) { return false; }
                    }
                    else  //maat is in inch
                    {
                        if (hgt < 59 || hgt > 76) { return false; }
                    }
                }
                catch
                {
                    // wat voor de cm of in staat is niet te converteren naar een getal.
                    return false;
                }

                if (!passport["hcl"].StartsWith('#'))  return false; 
                if(!Regex.IsMatch(passport["hcl"].Substring(1), "^[a-f0-9]{6}$")) return false;  //a-f of 0-9 precies 6 tekens

                var Eyecolors = new List<string> {"amb","blu","brn","gry","grn","hzl","oth"};
                if (!Eyecolors.Contains(passport["ecl"])) return false;

                // nog eenje
                if (!Regex.IsMatch(passport["pid"],"^[0-9]{9}$")) return false;

                // als alle velden goed zijn kom je hier:
                return true;
            }
            else // als het aantal velden niet klopt
            {
                
               return false;
               
            }
           
        }

    }
}
