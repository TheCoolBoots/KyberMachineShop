using System;
public static class SnapPointTypeFilterConfigurations
{
    public static Type[,] allowedTypeLists = { { typeof(DrillpressWoodblock) } };

    public enum SnapPointPresets
    {
        drillpressWoodblock
    }
}
