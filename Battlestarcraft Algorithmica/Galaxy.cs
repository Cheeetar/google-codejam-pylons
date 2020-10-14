namespace Battlestarcraft_Algorithmica
{
    internal class Galaxy
    {
        public Sector[,] Sectors { get; }
        public int Rows { get; }
        public int Columns { get; }

        public Galaxy(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            var sectors = new Sector[rows, columns];
            for(int rowIndex = 1; rowIndex <= rows; rowIndex += 1)
            {
                for(int columnIndex = 1; columnIndex <= columns; columnIndex += 1)
                {
                    sectors[rowIndex - 1, columnIndex - 1] = new Sector(rowIndex, columnIndex);
                }
            }

            Sectors = sectors;
        }
    }
}
