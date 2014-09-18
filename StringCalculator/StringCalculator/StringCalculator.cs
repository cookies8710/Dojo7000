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
            if ("" == numbers) 
                return 0;

            if (numbers.StartsWith("//"))
            {
                var delimiter = numbers.Split('\n').First().Substring(2);
                var array = numbers
                                .Replace(delimiter, ",")
                                .SkipWhile(x => x != '\n')
                                .Skip(1).ToArray();
                numbers = new String(array);
            }

            string[] splitted = numbers.Replace('\n',',').Split(',');
            
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
