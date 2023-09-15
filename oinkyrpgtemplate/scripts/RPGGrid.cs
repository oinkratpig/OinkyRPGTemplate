using Godot;
using System;

/// <summary>
/// A grid for movement within the RPG.
/// </summary>
public class RPGGrid
{
    /// <summary>
    /// The width and height of each tile within the RPG's grid.
    /// </summary>
    [Export]
    public Vector2I TileSize
    {
        get { return _tileSize; }
        private set { _tileSize = value; }
    }

    /// <summary>
    /// Amount to move the entire grid's X and Y position.
    /// </summary>
    [Export]
    public Vector2I Offset
    {
        get { return _offset; }
        private set { _offset = value; }
    }

    // Fields
    private Vector2I _tileSize;
    private Vector2I _offset;

    /* Constructor */
    public RPGGrid(Vector2I cellSize, Vector2I offset)
    {
        TileSize = cellSize;
        Offset = offset;

    } // end constructor

    /* Overload - Constructor defaults */
    public RPGGrid() : this(new Vector2I(OinkyRPG.GRID_TILE_WIDTH_DEFAULT, OinkyRPG.GRID_TILE_HEIGHT_DEFAULT),
            new Vector2I(OinkyRPG.GRID_X_OFFSET_DEFAULT, OinkyRPG.GRID_Y_OFFSET_DEFAULT))
    { }

    /// <summary>
    /// Snaps the given global position to the grid's tiles positions.
    /// </summary>
    public Vector2 SnapPosition(Vector2 globalPosition)
    {
        float x = Mathf.Round(globalPosition.X / TileSize.X) * TileSize.X;
        float y = Mathf.Round(globalPosition.Y / TileSize.Y) * TileSize.Y;
        return Offset + new Vector2(x, y);

    } // end SnapPosition

    /// <summary>
    /// Converts a grid position to a global position.
    /// </summary>
    public Vector2 GridPositionToGlobal(Vector2I gridPosition)
    {
        return Offset + TileSize * gridPosition;

    } // end GridPositionToGlobal

    /// <summary>
    /// Converts a global position to a grid position.
    /// </summary>
    public Vector2I GlobalPositionToGrid(Vector2 globalPosition)
    {
        return (Vector2I)(SnapPosition(globalPosition) / TileSize);

    } // end GlobalPositionToGrid

} // end class RPGGrid