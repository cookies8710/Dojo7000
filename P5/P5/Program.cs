using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P5
{
    class Program
    {

        class Coord
        {
            public double Longitude { get; set; }
            public double Latitude { get; set; }

            public override string ToString()
            {
                return String.Format("{1} {0}", Longitude, Latitude);
            }

            public static double Distance(Coord x, Coord y)
            {
                var dlo = Math.Abs(x.Longitude - y.Longitude);
                var dla = Math.Abs(x.Latitude - y.Latitude);
                return Math.Sqrt(dlo * dlo + dla * dla);
            }

            public class EqualityComparer:IEqualityComparer<Coord>
            {

                

                public bool Equals(Coord x, Coord y)
                {
                    return Distance(x, y) < 0.001;
                    //return Math.Abs(x.Longitude - y.Longitude) < 0.001 && Math.Abs(x.Latitude - y.Latitude) < 0.001;
                    //return x.Longitude == y.Longitude && x.Latitude == y.Latitude;
                }

                public int GetHashCode(Coord obj)
                {
                    return 0;//obj.Latitude.GetHashCode() * 13 + obj.Longitude.GetHashCode() * 7;
                }
            }

            public bool AtSchool()
            {
                var schoollo = 16.5989164;
                var schoolla = 49.2099831;

                return Math.Abs(Latitude - schoolla) < 0.001 && Math.Abs(Longitude - schoollo) < 0.001;
            }

            public double DistToSchool()
            {
                var schoollo = 16.5989164;
                var schoolla = 49.2099831;

                float pk = (float) (180/3.14169);

    double a1 = schoolla / pk;
    double a2 = schoollo / pk;
    double b1 = Latitude / pk;
    double b2 = Longitude / pk;

    double t1 = Math.Cos(a1)*Math.Cos(a2)*Math.Cos(b1)*Math.Cos(b2);
    double t2 = Math.Cos(a1)*Math.Sin(a2)*Math.Cos(b1)*Math.Sin(b2);
    double t3 = Math.Sin(a1)*Math.Sin(b1);
    double tt = Math.Acos(t1 + t2 + t3);
   
    return 6366000*tt;

            }
        }

        class Record
        {
            public Record(string s)
            {
                Regex sr = new Regex(@"\[(?<number>\d+)\]\s+\[\s*(?<longitude>\d+\.\d+)\s+(?<latitude>\d+\.\d+)\s*\]");
                var match = sr.Match(s);
                var nstr = match.Groups["number"].ToString();
                Number = int.Parse(nstr);
                var lostr = match.Groups["longitude"].ToString().Replace('.', ',');
                var lastr = match.Groups["latitude"].ToString().Replace('.', ',');
                Coordinates = new Coord();
                Coordinates.Longitude = double.Parse(lostr);
                Coordinates.Latitude = double.Parse(lastr);
            }

            public int Number { get; set; }
            public Coord Coordinates { get; set; }
        }

        class Snapshot
        {
            public Snapshot(String s)
            {
                Regex sr = new Regex(@"snapshot\s\[(.+)\]");
                var match = sr.Match(s);
                Timestamp = DateTime.Parse(match.Groups[1].ToString());
                Records = new List<Record>();
            }

            public DateTime Timestamp { get; set; }
            public List<Record> Records { get; set; }
        }

        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("P5-snaps.txt");

            List<Snapshot> snapshots = new List<Snapshot>();

            DateTime start = DateTime.Now;
            Snapshot currentSnapshot = null;
            String line;
            while((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("snapshot"))
                {
                    if (currentSnapshot != null)
                        snapshots.Add(currentSnapshot);
                    currentSnapshot = new Snapshot(line);
                }
                else
                {
                    currentSnapshot.Records.Add(new Record(line));
                }
            }
            snapshots.Add(currentSnapshot);

            TimeSpan duration = DateTime.Now - start;
            Console.WriteLine("{0} snapshots; dur: {1}", snapshots.Count, duration.ToString());
            var allnums = snapshots.SelectMany(x => x.Records.Select(y => y.Number)).Distinct().ToList();
            var snapshotsByNum = snapshots
                .SelectMany(x => x.Records.Select(y => new {y.Number, y.Coordinates, x.Timestamp}))
                .GroupBy(x => x.Number)
                .ToDictionary(x => x.Key, x => x.ToList());
                        
            Dictionary<DateTime, Coord> s = new Dictionary<DateTime,Coord>();
            foreach (var item in snapshotsByNum[736637736])
            {
                if (!s.Any() || Coord.Distance(item.Coordinates, s.Last().Value) > 0.0001)
                {
                    if (s.Any() && (item.Timestamp - s.Last().Key).TotalMinutes > 30)
                        Console.WriteLine("hospoda? [{0}]: {1}", item.Timestamp, item.Coordinates.ToString());

                    s[item.Timestamp] = item.Coordinates;
                    
                    
                }
            }

          /*  foreach (var item in s)
            {
                Console.WriteLine("[{0}]: {1}", item.Key, item.Value.ToString());
            }*/

            Console.ReadKey();
        }

    }
}
