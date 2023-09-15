using Godot;
using System;

/// <summary>
/// The controllable player within the RPG.
/// </summary>
public partial class RPGPlayer : RPGMoveable
{
    public override void _Ready()
    {
        base._Ready();

    } // end _Ready

    public override void _Process(double delta)
    {
        // Amount of tiles to move
        Vector2I direction = new Vector2I();
        direction.X += Input.IsActionPressed("oinkyRPG_move_right") ? 1 : 0;
        direction.X -= Input.IsActionPressed("oinkyRPG_move_left") ? 1 : 0;
        direction.Y += Input.IsActionPressed("oinkyRPG_move_down") ? 1 : 0;
        direction.Y -= Input.IsActionPressed("oinkyRPG_move_up") ? 1 : 0;

        // Move
        Move(direction);

        // DEBUG
        if(Input.IsActionJustPressed("oinkyRPG_interact"))
        {
            MoveQueueAdd(new Vector2I(1, 0));
            MoveQueueAdd(new Vector2I(1, 0));
            MoveQueueAdd(new Vector2I(0, 1));
        }

    } // end _Ready

} // end class RPGPlayer
