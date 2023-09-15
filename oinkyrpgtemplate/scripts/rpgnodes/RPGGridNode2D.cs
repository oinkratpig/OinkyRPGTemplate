using Godot;
using System;

/// <summary>
/// A Node2D that is intended to move along the game's set grid.
/// </summary>
public partial class RPGGridNode2D : Node2D
{
    /// <summary>
    /// Sets the <see cref="RPGGrid"/> instance used for grid positioning.<br/>
    /// All <see cref="RPGGridNode2D"/> use the main grid (<see cref="OinkyRPG.MainGrid"/>) by default.
    /// </summary>
    public RPGGrid Grid { get; set; }

    /// <summary>
    /// Grid position of the <see cref="RPGGridNode2D"/> instance.
    /// </summary>
    /// <remarks>
    /// Grid position (0,0) would be the top-left tile of the grid.<br/>
    /// Grid position (1,1) is one tile to the right and one tile down.
    /// </remarks>
    public Vector2I PositionGrid
    {
        get { return Grid.GlobalPositionToGrid(GlobalPosition); }
    }

    /* Constructor */
    public RPGGridNode2D()
    {
        Grid = OinkyRPG.MainGrid;

    } // end constructor

} // end class Node2DGrid
