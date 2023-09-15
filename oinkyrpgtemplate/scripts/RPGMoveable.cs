using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// The controllable player within the RPG.
/// </summary>
public partial class RPGMoveable : Node2DGrid
{

    /// <summary>
    /// Speed to move between tiles.
    /// </summary>
    [Export]
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    /// <summary>
    /// Whether this <see cref="RPGMoveable"/> is moving or not.
    /// </summary>
    public bool Moving { get; private set; }

    // Fields
    private List<Vector2I> _movementQueue;
    private float _moveSpeed = 3f;
    private Vector2 _desiredGlobalPosition;

    /* Constructor */
    public RPGMoveable()
    {
        _movementQueue = new List<Vector2I>();

    } // end constructor

    public override void _Ready()
    {
        _desiredGlobalPosition = PositionGrid;

    } // end _Ready

    public override void _PhysicsProcess(double delta)
    {
        // Move
        if(Moving)
        {
            float distToDesiredPosition = GlobalPosition.DistanceTo(_desiredGlobalPosition);
            // Stop moving
            if (distToDesiredPosition <= MoveSpeed) ArrivedAtDestination();
            // Moving
            else
            {
                float angle = GlobalPosition.AngleToPoint(_desiredGlobalPosition);
                float distance = Mathf.Min(MoveSpeed, distToDesiredPosition);
                GlobalPosition = GlobalPosition.LengthDir(distance, angle);
            }
        }

    } // end _PhysicsProcess

    /// <summary>
    /// Called when movement is stopped.
    /// </summary>
    private void ArrivedAtDestination()
    {
        GlobalPosition = _desiredGlobalPosition;

        if (_movementQueue.Count > 0)
        {
            _desiredGlobalPosition += Grid.TileSize * _movementQueue[0];
            _movementQueue.RemoveAt(0);
        }
        else Moving = false;

    } // end ArrivedAtDestination

    /// <summary>
    /// Move the <see cref="RPGMoveable"/> the specified distance.<br/>
    /// Cannot move if already moving.
    /// </summary>
    public void Move(Vector2I distance)
    {
        if (!Moving && distance != Vector2I.Zero)
        {
            Moving = true;
            _desiredGlobalPosition += Grid.TileSize * distance;
        }

    } // end Move

    /// <summary>
    /// Add a movement to be performed whenever possible.
    /// </summary>
    public void MoveQueueAdd(Vector2I distance)
    {
        if (Moving) _movementQueue.Add(distance);
        else Move(distance);
        Moving = true;

    } // end MoveQueueAdd

} // end class RPGMoveable