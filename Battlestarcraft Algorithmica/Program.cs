using System;

namespace Battlestarcraft_Algorithmica
{
    class Program
    {
        static void Main(string[] args)
        {
            var rows = int.Parse(args[0]);
            var columns = int.Parse(args[1]);
            var galaxy = new Galaxy(rows, columns);
            var starship = new Starship("Lapsed Pacifist");
            var captain = new Captain("Jonathan", starship);

            var captainsLog = captain.ChartUntraceablePath(galaxy);
            if (captainsLog.Count == 0)
            {
                Console.WriteLine("IMPOSSIBLE");
            }
            else
            {
                Console.WriteLine("POSSIBLE");
                foreach(var log in captainsLog)
                {
                    Console.WriteLine($"{log}");
                }
            }    
        }
    }
}
