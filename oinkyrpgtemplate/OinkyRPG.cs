using Godot;
using System;

/// <summary>
/// RPG project globals.
/// </summary>
public static class OinkyRPG
{
    public const int GRID_TILE_WIDTH_DEFAULT = 32;
    public const int GRID_TILE_HEIGHT_DEFAULT = 32;
    public const int GRID_X_OFFSET_DEFAULT = 0;
    public const int GRID_Y_OFFSET_DEFAULT = 0;

    public static RPGGrid MainGrid { get; private set; }

    /* Constructor - sets the default values */
    static OinkyRPG()
    {
        // Main grid
        MainGrid = new RPGGrid();

    } // end constructor

} // end class OinkyRPG