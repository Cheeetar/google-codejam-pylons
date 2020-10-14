using System;

namespace Battlestarcraft_Algorithmica
{
    public static class Manual
    {
        public static int GetLinealIncrement(StrategyType strategy)
        {
            switch (strategy)
            {
                case StrategyType.ColumnWise:
                    return 2;
                case StrategyType.ThinColumnWise:
                    return 3;
                case StrategyType.RowWise:
                case StrategyType.ThinRowWise:
                    return 1;
                default:
                    throw new ArgumentException($"Strategy not found in manual: {strategy}");
            }
        }

        public static int GetLateralIncrement(StrategyType strategy)
        {
            switch (strategy)
            {
                case StrategyType.RowWise:
                    return 2;
                case StrategyType.ThinRowWise:
                    return 3;
                case StrategyType.ColumnWise:
                case StrategyType.ThinColumnWise:
                    return 1;
                default:
                    throw new ArgumentException($"Strategy not found in manual: {strategy}");
            }
        }
    }
}
