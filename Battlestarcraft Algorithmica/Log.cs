namespace Battlestarcraft_Algorithmica
{
    internal class Log
    {
        public Sector Sector { get; }
        public int StarDate { get; }

        public Log(Sector sector, int starDate)
        {
            Sector = sector;
            StarDate = starDate;
        }

        public bool DeadEnd => Sector == null;

        public override string ToString()
        {
            return $"{StarDate}: {Sector}";
        }

        public override bool Equals(object obj)
        {
            return obj is Log
                && ((Log)obj).Sector == Sector;
        }
    }
}
