namespace Battlestarcraft_Algorithmica
{
    internal static class Navigation
    {
        public static bool IsUntraceableJump(Sector currentSector, Sector potentialJump) =>
                !currentSector.SharesRow(potentialJump)
             && !currentSector.SharesColumn(potentialJump)
             && !currentSector.SharesDiagonal(potentialJump);
        public static (int destinationRow, int destinationColumn) WrapStarpoints(Galaxy galaxy, int destinationRow, int destinationColumn) => 
            (WrapStarpoint(destinationRow, galaxy.Rows), WrapStarpoint(destinationColumn, galaxy.Columns));

        private static int WrapStarpoint(int starPoint, int galaxyBoundary)
        {
            starPoint -= 1;
            starPoint %= galaxyBoundary;
            starPoint += 1;
            return starPoint > 0
                ? starPoint
                : starPoint + galaxyBoundary;
        }
    }
}
