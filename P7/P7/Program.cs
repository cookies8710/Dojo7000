using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7
{
    class Program
    {
        static void PrintLetter(string s)
        {
            for (int y = 0; y < 5; y++)
            {
                Console.WriteLine(s.Substring(y*3, 3));
            }
        }

        class Pattern
        {
            public String StringPattern { get; private set; }
            public String Letter { get; private set; }

            public List<Boolean> BooleanPattern { get; private set; }

            public Pattern(String letter, String strPattern)
            {
                Letter = letter;
                StringPattern = strPattern;
                BooleanPattern = strPattern.Select(y => y == '*').ToList();
            }

            public bool Matches(String str)
            { 
                var chars = str.Distinct().Select(x => x.ToString()).ToList();
                Dictionary<string, Boolean> mapping = new Dictionary<string, bool>();
                
                mapping.Add(chars[0], true);
                mapping.Add(chars[1], false);

                var match = str.Select(x => mapping[x.ToString()]).Zip(BooleanPattern, (x, y) => x == y).All(x => x==true);
                if (match)
                    return true;
                mapping.Clear();
                mapping.Add(chars[0], false);
                mapping.Add(chars[1], true);
                match = str.Select(x => mapping[x.ToString()]).Zip(BooleanPattern, (x, y) => x == y).All(x => x == true);
                return match;
            }
        }

        class Codec
        {
            public Codec(Dictionary<string, string> strpatterns)
            {
                patterns = strpatterns.ToDictionary(x => x.Key, x => new Pattern(x.Key, x.Value));
            }

            Dictionary<string, Pattern> patterns;
            public String DecodeLetter(String encoded)
            {
                return patterns.First(x => x.Value.Matches(encoded)).Key;                
            }

            public Boolean Decodable(List<String> msgLines)
            {
                return msgLines.Count >= 5;
            }

            public List<String> DecodeMessage(List<String> msgLines)
            {
                List<String> decodedMsg = new List<String>();
                var nlines = msgLines.Count;
                var linelen = msgLines.First().Length;

                for(var y = 0; y * 5 < nlines; y++)
                {
                    StringBuilder decoded = new StringBuilder();
                    for (var x = 0; x * 3 < linelen; x++)
                    { 
                        StringBuilder encodedLetter = new StringBuilder();
                        for (var l = 0; l < 5; l++)
                            encodedLetter.Append(msgLines[y * 5 + l].Substring(x * 3, 3));

                        decoded.Append(DecodeLetter(encodedLetter.ToString()));
                    }
                    decodedMsg.Add(decoded.ToString());
                }

                return decodedMsg;
            }
        }

        static void Main(string[] args)
        {
            Dictionary<string, string> patterns = new Dictionary<string,string>(){ 
                {"A", ".*.*.*****.**.*" },
                {"B", "**.*.***.*.***." },
                {"C", ".*.*.**..*.*.*." },
                {"D", "**.*.**.**.***." },
                {"E", "****..**.*..***" },
                {"F", "****..**.*..*.." }
            };

            var codec = new Codec(patterns);

            StreamReader input = new StreamReader("in.txt");
            var line = "";
            var lines = new List<String>();
            while ((line = input.ReadLine()) != null) 
                lines.Add(line);

            var msg = lines;
            int i = 1;
            while (codec.Decodable(msg))
            {
                Console.WriteLine(String.Format("Decoding, it. {0}, len = {1} lines", i, msg.Count));
                msg = codec.DecodeMessage(msg);
                i++;
            }
            foreach (var item in msg)
            {                
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
