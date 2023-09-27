using Godot;
using System;

/// <summary>
/// A Node that is intended to move along the game's set grid.
/// </summary>
[Tool]
public partial class RPGNode : Node
{
    /// <summary>
    /// Sets the <see cref="RPGGrid"/> instance used for grid positioning.<br/>
    /// All <see cref="RPGGridNode2D"/> use the main grid (<see cref="OinkyRPG.MainGrid"/>) by default.
    /// </summary>
    public RPGGrid Grid { get; set; }

    /// <summary>
    /// Also updates <see cref="PositionGrid"/>.
    /// </summary>
    public Vector2 GlobalPosition
    {
        get { return _globalPosition; }
        set
        {
            Vector2 globalPositionChange = value - GlobalPosition;
            _globalPosition = value;
            _positionGrid = Grid.GlobalPositionToGrid(value);

            // Update global position of children
            foreach (Node node in GetChildren())
            {
                // 2D Nodes
                if (node is Node2D && IsInstanceValid(node))
                    (node as Node2D).GlobalPosition += globalPositionChange;
                // RPG Nodes
                else if (node is RPGNode)
                    if ((node as RPGNode).Static == false)
                        (node as RPGNode).GlobalPosition += globalPositionChange;
            }
        }
    }

    /// <summary>
    /// Grid position of the <see cref="RPGGridNode2D"/> instance.<br/>
    /// Updates <see cref="GlobalPosition"/> and <see cref="GlobalPositionGrid"/>.
    /// </summary>
    /// <remarks>
    /// Grid position (0,0) would be the top-left tile of the grid.<br/>
    /// Grid position (1,1) is one tile to the right and one tile down.
    /// </remarks>
    [Export] public Vector2I PositionGrid
    {
        get { return _positionGrid; }
        set { GlobalPosition = Grid.GridPositionToGlobal(value); }
    }

    /// <summary>
    /// Whether or not the position is relative to the parent.<br/>
    /// 'true' ignores all <see cref="RPGNode"/> parents' positions. Its position in the grid will always be global.<br/>
    /// 'false' changes its position whenever any parents' position changes.
    /// </summary>
    [Export] public bool Static { get; set; }

    // Fields
    private Vector2I _positionGrid;
    private Vector2 _globalPosition;

    /* Constructor */
    public RPGNode()
    {
        Grid = OinkyRPG.MainGrid;

    } // end constructor

} // end class RPGNode
