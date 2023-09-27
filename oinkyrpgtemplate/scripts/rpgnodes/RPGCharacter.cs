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
        if (IsInstanceValid(_characterAnimationPlayer))
            switch(Facing)
            {
                case Direction.East:
                    if (Moving) PlayAnimationIfExists(_animNameWalkEast);
                    else PlayAnimationIfExists(_animNameIdleEast);
                    break;
                case Direction.SouthEast:
                    if (Moving) PlayAnimationIfExists(_animNameWalkSouthEast);
                    else PlayAnimationIfExists(_animNameIdleSouthEast);
                    break;
                case Direction.South:
                    if (Moving) PlayAnimationIfExists(_animNameWalkSouth);
                    else PlayAnimationIfExists(_animNameIdleSouth);
                    break;
                case Direction.SouthWest:
                    if (Moving) PlayAnimationIfExists(_animNameWalkSouthWest);
                    else PlayAnimationIfExists(_animNameIdleSouthWest);
                    break;
                case Direction.West:
                    if (Moving) PlayAnimationIfExists(_animNameWalkWest);
                    else PlayAnimationIfExists(_animNameIdleWest);
                    break;
                case Direction.NorthWest:
                    if (Moving) PlayAnimationIfExists(_animNameWalkNorthWest);
                    else PlayAnimationIfExists(_animNameIdleNorthWest);
                    break;
                case Direction.North:
                    if (Moving) PlayAnimationIfExists(_animNameWalkNorth);
                    else PlayAnimationIfExists(_animNameIdleNorth);
                    break;
                case Direction.NorthEast:
                    if (Moving) PlayAnimationIfExists(_animNameWalkNorthEast);
                    else PlayAnimationIfExists(_animNameIdleNorthEast);
                    break;
            }

    } // end _PhsyicsProcess

    /// <summary>
    /// Play the given animation in the animation player if it exists.
    /// </summary>
    private void PlayAnimationIfExists(string animName)
    {
        if (_characterAnimationPlayer.HasAnimation(animName))
            _characterAnimationPlayer.Play(animName);

    } // end PlayAnimationIfExists

} // end class RPGCharacter
