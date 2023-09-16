using Godot;
using System;
using System.ComponentModel.Design;

/// <summary>
/// A character within the RPG.<br/>
/// Sprite MUST use the 8-directional format used by the template.
/// </summary>
[Tool]
public partial class RPGCharacter : RPGMoveable
{
    [ExportGroup("Components")]
    [Export] private Sprite2D _characterSprite;
    [Export] private AnimationPlayer _characterAnimationPlayer;
    [ExportGroup("Animations")]
    [ExportSubgroup("Idle")]
    [Export] private string _animNameIdleEast       = "Idle_East";
    [Export] private string _animNameIdleSouthEast  = "Idle_SouthEast";
    [Export] private string _animNameIdleSouth      = "Idle_South";
    [Export] private string _animNameIdleSouthWest  = "Idle_SouthWest";
    [Export] private string _animNameIdleWest       = "Idle_West";
    [Export] private string _animNameIdleNorthWest  = "Idle_NorthWest";
    [Export] private string _animNameIdleNorth      = "Idle_North";
    [Export] private string _animNameIdleNorthEast  = "Idle_NorthEast";
    [ExportSubgroup("Walking")]
    [Export] private string _animNameWalkEast       = "Walk_East";
    [Export] private string _animNameWalkSouthEast  = "Walk_SouthEast";
    [Export] private string _animNameWalkSouth      = "Walk_South";
    [Export] private string _animNameWalkSouthWest  = "Walk_SouthWest";
    [Export] private string _animNameWalkWest       = "Walk_West";
    [Export] private string _animNameWalkNorthWest  = "Walk_NorthWest";
    [Export] private string _animNameWalkNorth      = "Walk_North";
    [Export] private string _animNameWalkNorthEast  = "Walk_NorthEast";

    protected override void OnFacingDirectionChanged()
    {
        // Face correct direction
        if(IsInstanceValid(_characterAnimationPlayer))
            switch(Facing)
            {
                case Direction.East:
                    if (Moving) _characterAnimationPlayer.Play(_animNameWalkEast);
                    else _characterAnimationPlayer.Play(_animNameIdleEast);
                    break;
                case Direction.SouthEast:
                    if (Moving) _characterAnimationPlayer.Play(_animNameWalkSouthEast);
                    else _characterAnimationPlayer.Play(_animNameIdleSouthEast);
                    break;
                case Direction.South:
                    if (Moving) _characterAnimationPlayer.Play(_animNameWalkSouth);
                    else _characterAnimationPlayer.Play(_animNameIdleSouth);
                    break;
                case Direction.SouthWest:
                    if (Moving) _characterAnimationPlayer.Play(_animNameWalkSouthWest);
                    else _characterAnimationPlayer.Play(_animNameIdleSouthWest);
                    break;
                case Direction.West:
                    if (Moving) _characterAnimationPlayer.Play(_animNameWalkWest);
                    else _characterAnimationPlayer.Play(_animNameIdleWest);
                    break;
                case Direction.NorthWest:
                    if (Moving) _characterAnimationPlayer.Play(_animNameWalkNorthWest);
                    else _characterAnimationPlayer.Play(_animNameIdleNorthWest);
                    break;
                case Direction.North:
                    if (Moving) _characterAnimationPlayer.Play(_animNameWalkNorth);
                    else _characterAnimationPlayer.Play(_animNameIdleNorth);
                    break;
                case Direction.NorthEast:
                    if (Moving) _characterAnimationPlayer.Play(_animNameWalkNorthEast);
                    else _characterAnimationPlayer.Play(_animNameIdleNorthEast);
                    break;
            }

    } // end _PhsyicsProcess

} // end class RPGCharacter
