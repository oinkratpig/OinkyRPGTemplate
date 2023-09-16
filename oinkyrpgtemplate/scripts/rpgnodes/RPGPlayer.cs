using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The controllable player within the RPG.
/// </summary>
[Tool]
public partial class RPGPlayer : RPGCharacter
{
    private enum MovementMode { Diagonal, FourWaySequence, FourWayClockwise, FourWayCounterClockwise }

    [Export] private MovementMode _movementMode = MovementMode.Diagonal;

    private bool _horizontalPressedLast = false;
    private List<Vector2I> _movementSequence;

    /* Constructor */
    public RPGPlayer()
    {
        _movementSequence = new List<Vector2I>();

    } // end constructor

    public override void _Ready()
    {
        base._Ready();

        // Center in viewport
        GlobalPosition = Grid.SnapPosition(GetViewport().GetVisibleRect().Size / 2f);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        // FourWaySequence movement sequence
        if(_movementMode == MovementMode.FourWaySequence)
        {
            // Remove keys from sequence
            if (!Input.IsActionPressed("oinkyRPG_move_right"))
                _movementSequence.Remove(Vector2I.Right);
            if (!Input.IsActionPressed("oinkyRPG_move_left"))
                _movementSequence.Remove(Vector2I.Left);
            if (!Input.IsActionPressed("oinkyRPG_move_up"))
                _movementSequence.Remove(Vector2I.Up);
            if (!Input.IsActionPressed("oinkyRPG_move_down"))
                _movementSequence.Remove(Vector2I.Down);

            // Add keys to sequence
            if (Input.IsActionJustPressed("oinkyRPG_move_right"))
                _movementSequence.Add(Vector2I.Right);
            if (Input.IsActionJustPressed("oinkyRPG_move_left"))
                _movementSequence.Add(Vector2I.Left);
            if (Input.IsActionJustPressed("oinkyRPG_move_up"))
                _movementSequence.Add(Vector2I.Up);
            if (Input.IsActionJustPressed("oinkyRPG_move_down"))
                _movementSequence.Add(Vector2I.Down);

        }

    }

    public override void _PhysicsProcess(double delta)
    {
        if (Engine.IsEditorHint())
            return;

        base._PhysicsProcess(delta);

        // FourWaySequence movement
        if (_movementMode == MovementMode.FourWaySequence && _movementSequence.Count > 0)
            Move(_movementSequence.Last());
        // Diagonal / FourWayClockwise / FourWayCounterClockwise movement
        else
        {
            // Amount of tiles to move
            Vector2I move = new Vector2I();
            // Get movement direction
            move.X += Input.IsActionPressed("oinkyRPG_move_right") ? 1 : 0;
            move.X -= Input.IsActionPressed("oinkyRPG_move_left") ? 1 : 0;
            move.Y += Input.IsActionPressed("oinkyRPG_move_down") ? 1 : 0;
            move.Y -= Input.IsActionPressed("oinkyRPG_move_up") ? 1 : 0;
            // Non-diagonal movement
            if (_movementMode != MovementMode.Diagonal && move != Vector2.Zero)
            {
                // Get angle of movement direction
                float moveAngle = Mathf.RadToDeg(Vector2.Zero.AngleToPoint(move));
                if (moveAngle < 0f) moveAngle += 360f;
                // Find angle
                if (_movementMode == MovementMode.FourWayClockwise) moveAngle += 1f;
                moveAngle = moveAngle - (moveAngle % 90f) + Mathf.Round(moveAngle % 90 / 90) * 90;
                // Move in direction
                move = (Vector2I)Vector2.Zero.LengthDir(1f, Mathf.DegToRad(moveAngle));
            }
            // Move
            Move(move);
        }
    }

} // end class RPGPlayer
