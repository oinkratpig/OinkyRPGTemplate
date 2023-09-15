using Godot;
using System;

/// <summary>
/// A Node2D that is intended to move along the game's set grid.
/// </summary>
public partial class Node2DGrid : Node2D
{
    /// <summary>
    /// Sets the <see cref="RPGGrid"/> instance used for grid positioning.<br/>
    /// All <see cref="Node2DGrid"/> use the main grid (<see cref="OinkyRPG.MainGrid"/>) by default.
    /// </summary>
    public RPGGrid Grid { get; set; }

    /// <summary>
    /// Grid position of the <see cref="Node2DGrid"/> instance.
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
    public Node2DGrid()
    {
        Grid = OinkyRPG.MainGrid;

    } // end constructor

} // end class Node2DGrid
