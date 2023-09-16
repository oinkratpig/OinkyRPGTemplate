using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// The controllable player within the RPG.
/// </summary>
public partial class RPGMoveable : RPGNode
{
    /// <summary>
    /// Possible directions the moveable can face
    /// </summary>
    public enum Direction { East, SouthEast, South, SouthWest, West, NorthWest, North, NorthEast }

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
    /// Updates "fake" position when modified.
    /// </summary>
    public new Vector2 GlobalPosition
    {
        get { return base.GlobalPosition; }
        set
        {
            base.GlobalPosition = value;
            _desiredGlobalPosition = value;
        }
    }

    /// <summary>
    /// Whether this <see cref="RPGMoveable"/> is moving or not.
    /// </summary>
    public bool Moving { get; private set; }

    /// <summary>
    /// Angle in radians last moved towards.
    /// </summary>
    public Direction Facing {
        get { return _facing; }
        private set
        {
            if (_facing != value)
            {
                _facing = value;
                OnFacingDirectionChanged();
            }
            _facing = value;
        }
    }

    // Fields
    private List<Vector2I> _movementQueue;
    private float _moveSpeed = 2f;
    private Vector2 _desiredGlobalPosition;
    private Direction _facing;

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
            float distToDesiredPosition = base.GlobalPosition.DistanceTo(_desiredGlobalPosition);
            // Stop moving
            if (distToDesiredPosition <= MoveSpeed) ArrivedAtDestination();
            // Moving
            else
            {
                float faceAngle = base.GlobalPosition.AngleToPoint(_desiredGlobalPosition);
                UpdateFacingDirection(faceAngle);

                float distance = Mathf.Min(MoveSpeed, distToDesiredPosition);
                base.GlobalPosition = base.GlobalPosition.LengthDir(distance, faceAngle);
            }
        }

    } // end _PhysicsProcess

    /// <summary>
    /// Called when movement starts.
    /// </summary>
    protected virtual void OnBeginMoving() { }

    /// <summary>
    /// Called when movement ends.
    /// </summary>
    protected virtual void OnStopMoving() { }

    /// <summary>
    /// Called when the direction faced is changed.
    /// </summary>
    protected virtual void OnFacingDirectionChanged() { }

    /// <summary>
    /// Update <see cref="Facing"/> to face the given angle
    /// </summary>
    private void UpdateFacingDirection(float angle)
    {
        float angleDegrees = Mathf.Round(Mathf.RadToDeg(angle));

        // Keep angle between [0, 360]
        while (angleDegrees > 360f) angleDegrees -= 360f;
        while (angleDegrees < 0f) angleDegrees += 360f;

        // Determine facing direction
        Direction[] directionOrder = new Direction[]
        {
            Direction.East, Direction.SouthEast, Direction.South,
            Direction.SouthWest, Direction.West, Direction.NorthWest,
            Direction.North, Direction.NorthEast, Direction.East
        };
        for(int i = 0; i < directionOrder.Length; i++)
            if (angleDegrees < (i + 1) * 45f)
            {
                Facing = directionOrder[i];
                break;
            }

    } // end UpdateFacingDirection

    /// <summary>
    /// Called when movement is stopped.
    /// </summary>
    private void ArrivedAtDestination()
    {
        GlobalPosition = _desiredGlobalPosition;

        if (_movementQueue.Count > 0)
        {
            Move(_movementQueue[0], false);
            _movementQueue.RemoveAt(0);
        }
        else
        {
            Moving = false;
            OnStopMoving();
        }

    } // end ArrivedAtDestination

    /// <summary>
    /// Move the <see cref="RPGMoveable"/> the specified distance.<br/>
    /// </summary>
    /// <param name="ignoreIfMoving">If true, nothing will happen if the <see cref="RPGMoveable"/> is moving.</param>
    public void Move(Vector2I distance, bool ignoreIfMoving)
    {
        if ((!Moving || !ignoreIfMoving) && distance != Vector2I.Zero)
        {
            Moving = true;
            _desiredGlobalPosition += Grid.TileSize * distance;
            OnBeginMoving();
        }

    } // end Move

    /// <summary>
    /// Move the <see cref="RPGMoveable"/> the specified distance.<br/>
    /// Cannot move if already moving.
    /// </summary>
    public void Move(Vector2I distance)
    {
        Move(distance, true);

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