namespace Battlestarcraft_Algorithmica
{
    internal class Sector
    {
        private readonly int _row;
        private readonly int _column;

        public Sector(int row, int column)
        {
            _row = row;
            _column = column;
        }

        public bool SharesDiagonal(Sector otherSector) =>
               _row - _column == otherSector._row - otherSector._column
            || _row + _column == otherSector._row + otherSector._column;

        public bool SharesRow(Sector otherSector) => _row == otherSector._row;

        public bool SharesColumn(Sector otherSector) => _column == otherSector._column;

        public override string ToString()
        {
            return $"{_row}, {_column}";
        }

        public override bool Equals(object obj)
        {
            var otherSector = obj as Sector;
            return otherSector != null
                && otherSector._row == _row
                && otherSector._column == _column;
        }
    }
}