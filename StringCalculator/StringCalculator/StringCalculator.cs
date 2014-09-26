using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public static class StringCalculator
    {
        public static int Add(string numbers)
        {
            if ("" == numbers) return 0;
            string[] delimiters = { "," };
            
            if (numbers.StartsWith("//"))
            {
                
                string delimiter = numbers.Split('\n').First().Substring(2);

                delimiters = delimiter[0] == '['
                    ? delimiter.Substring(1, delimiter.Length - 2).Split(new[] { "][" }, StringSplitOptions.None)
                    : new[] { delimiter };

                numbers = numbers.Substring(numbers.IndexOf('\n') + 1);
            }

            string[] splitted = numbers.Replace("\n", delimiters[0]).Split(delimiters, StringSplitOptions.None);
            IEnumerable<int> numbersAsInts =  splitted.Select(int.Parse);

            var negativeNumbers = numbersAsInts.Where(x => x < 0);

            if (negativeNumbers.Any())
            {
                throw new ArgumentException(String.Join(",", negativeNumbers));
            }            

            return numbersAsInts.Where(x => x < 1001).Sum();
        }
    }
}
