using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace InterLosP1
{
    class Program
    {

        static Movement GetMovement(Color color)
        {
            var rgb = String.Format("{0}{1}{2}", color.R & 1, color.G & 1, color.B & 1);
            switch (rgb)
            {
                case "000":
                    return Movement.Right;
                case "001":
                    return Movement.Left;
                case "010":
                    return Movement.Up;
                case "011":
                    return Movement.Down;
                case "100":
                    return Movement.Right | Movement.Up;
                case "101":
                    return Movement.Left | Movement.Down;
                case "110":
                    return Movement.Right | Movement.Down;
                case "111":
                    return Movement.Left | Movement.Up;
            }

            throw new Exception();
        }

        [Flags]
        enum Movement
        {
            Right = 1, Left = 2, Up = 4, Down = 8
        }

        class Jump
        {
            public Jump(Color curr, Color? prev)
            {
                Direction = GetMovement(curr);
                Length = 1;
                if (prev.HasValue)
                {
                    if (prev.Value.R % 2 == 1)
                        Length = 1 + prev.Value.G % 4;
                }
            }

            public Movement Direction { get; set; }
            public int Length { get; set; }

            public int LinearDiff(int sx, int sy)
            { 
                int d = 0;
                if (Direction.HasFlag(Movement.Right))
                    d += Length;
                if (Direction.HasFlag(Movement.Left))
                    d -= Length;
                if (Direction.HasFlag(Movement.Up))
                    d -= Length * sx;
                if (Direction.HasFlag(Movement.Down))
                    d += Length * sx;
                return d;
            }
        }
        
        static void Main(string[] args)
        {           
            var encoded = (Bitmap) Image.FromFile("P1-encoded.bmp", false);

            var xsize = encoded.Width / 30;
            var ysize = encoded.Height / 30;

            List<Color> linear = new List<Color>();

            for (var y = 0; y < ysize; y++)
                for (var x = 0; x < xsize; x++)
                    linear.Add(encoded.GetPixel(x * 30, y * 30));

            List<Jump> jumps = new List<Jump>();
            Color? prev = null;
            foreach (var item in linear)
            {
                jumps.Add(new Jump(item, prev));

                prev = item;
            }

            // Execute:
            List<int> msg = new List<int>();
            int j = 0;
            while (j >= 0 && j < jumps.Count)
            {
                msg.Add(j);
                var ld = jumps[j].LinearDiff(xsize, ysize);
                j += ld;
            }
           
            for (int y = 0; y < ysize; y++)
            {
                for (int x = 0; x < xsize; x++)
                {
                    var item = x + xsize * y;
                    Console.Write(msg.Contains(item) ? "X" : " ");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
