using System;
using System.Collections.Generic;
using System.Linq;

namespace Battlestarcraft_Algorithmica
{
    internal class Captain
    {
        public string Name { get; }
        public Starship Ship { get; }
        private Stack<Log> CaptainsLog { get; } = new Stack<Log>();
        private List<Sector> BucketList { get; } = new List<Sector>();

        public Captain(string name, Starship ship)
        {
            Name = name;
            Ship = ship;
        }

        public List<Log> ChartUntraceablePath(Galaxy galaxy)
        {
            try
            {
                var strategy = DecideStrategy(galaxy);

                switch (strategy)
                {
                    case StrategyType.RowWise:
                    case StrategyType.ColumnWise:
                    case StrategyType.ThinRowWise:
                    case StrategyType.ThinColumnWise:
                        ChartStellarKnightPath(galaxy, strategy);
                        break;
                    case StrategyType.KobayashiMaru:
                        // The only winning move is not to play.
                        return CaptainsLog.ToList();
                    case StrategyType.Undecided:
                    default:
                        throw new Exception($"Captain could not decide on strategy type. Galaxy dimensions: {galaxy.Columns}, {galaxy.Rows}");
                }

                if (CaptainsLog.Distinct().Count() != CaptainsLog.Count)
                {
                    throw new Exception("Retraced steps at some point - pylons found us!");
                }

                return CaptainsLog.OrderBy(log => log.StarDate).ToList();
            }
            catch (Exception exception)
            {
                foreach (var sector in CaptainsLog.OrderBy(log => log.StarDate))
                {
                    Console.WriteLine(sector);
                }
                Console.WriteLine(exception);
                return new List<Log>();
            }
        }

        private void ChartStellarKnightPath(Galaxy galaxy, StrategyType strategy)
        {
            int destinationRow = 1, destinationColumn = 1, starDate = 1;

            var linealIncrement = Manual.GetLinealIncrement(strategy);
            var lateralIncrement = Manual.GetLateralIncrement(strategy);

            while (CaptainsLog.Count < galaxy.Sectors.Length)
            {
                if (strategy == StrategyType.RowWise || strategy == StrategyType.ThinRowWise)
                {
                    var iterations = galaxy.Rows - destinationRow == 2
                        ? 3
                        : 2;
                    while (destinationColumn <= galaxy.Columns)
                    {
                        EvasiveManeuvers(galaxy, destinationRow, destinationColumn, linealIncrement, lateralIncrement, true, iterations, ref starDate);
                        destinationColumn += 1;
                    }
                    destinationRow += iterations;
                    destinationColumn = 1;
                }
                else
                {
                    var iterations = galaxy.Columns - destinationColumn == 2
                        ? 3
                        : 2;
                    while (destinationRow <= galaxy.Rows)
                    {
                        EvasiveManeuvers(galaxy, destinationRow, destinationColumn, linealIncrement, lateralIncrement, false, iterations, ref starDate);
                        destinationRow += 1;
                    }
                    destinationRow = 1;
                    destinationColumn += iterations;
                }
            }
        }

        private void EvasiveManeuvers(Galaxy galaxy, int destinationRow, int destinationColumn, int linealIncrement, int lateralIncrement, bool rowWise, int iterations, ref int starDate)
        {
            for(var iteration = 0; iteration < iterations; iteration += 1)
            {
                Console.WriteLine($"iteration: {iteration}, iterations: {iterations}, destinationRow: {destinationRow}, destinationColumn: {destinationColumn}, linealIncrement: {linealIncrement}, lateralIncrement: {lateralIncrement}");
                ChartAndLogJump(galaxy, destinationRow, destinationColumn, starDate);

                destinationRow += linealIncrement;
                destinationColumn += lateralIncrement;
                (destinationRow, destinationColumn) = Navigation.WrapStarpoints(galaxy, destinationRow, destinationColumn);

                if (rowWise)
                {
                    lateralIncrement *= -1;
                }
                else
                {
                    linealIncrement *= -1;
                }

                starDate += 1;
            }
        }

        private void ChartAndLogJump(Galaxy galaxy, int destinationRow, int destinationColumn, int starDate)
        {
            var sector = galaxy.Sectors[destinationRow - 1, destinationColumn - 1];
            var log = MakeJump(sector, starDate);
            CaptainsLog.Push(log);
        }

        private Log MakeJump(Sector destination, int starDate)
        {
            if (Ship.SuccessfulUntraceableJump(destination))
            {
                return new Log(destination, starDate);
            }
            else
            {
                throw new Exception($"Pylons traced our path when we jumped to {destination} and captured our ship! We're doomed!");
            }
        }

        private StrategyType DecideStrategy(Galaxy galaxy)
        {
            if (galaxy.Sectors.Length < 10 || galaxy.Sectors.Length == 16)
            {
                return StrategyType.KobayashiMaru;
            }
            else if (galaxy.Rows >= 5)
            {
                return StrategyType.ThinColumnWise;
            }
            else if (galaxy.Columns >= 5)
            {
                return StrategyType.ThinRowWise;
            }
            else if (galaxy.Columns >= 4)
            {
                return StrategyType.RowWise;
            }
            else
            {
                return StrategyType.ColumnWise;
            }
        }
    }
}
