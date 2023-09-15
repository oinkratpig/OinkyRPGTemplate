using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public static class OinkyRPGExtensions
{
    /*
    /// <summary>
    /// Returns a new Vector2 with values more than or equal to the given value.
    /// </summary>
    public static Vector2I Min(this Vector2I vector, int value)
    {
        return new Vector2I(Math.Min(vector.X, value),
            Math.Min(vector.Y, value));

    } // end Min

    /// <summary>
    /// Returns a new Vector2 with values less than or equal to the given value.
    /// </summary>
    public static Vector2I Max(this Vector2I vector, int value)
    {
        return new Vector2I(Math.Max(vector.X, value),
            Math.Max(vector.Y, value));

    } // end Max
    */

    /// <summary>
    /// Same as using CallDeferred to add a child.
    /// </summary>
    public static void AddChildDeferred(this Node obj, Node child)
    {
        obj.CallDeferred("add_child", child);

    } // end AddChildDeferred

    /// <summary>
    /// Returns angle in radians from the <see cref="Vector2I"/> to the specified position.
    /// </summary>
    public static float AngleTo(this Vector2I from, Vector2I to)
    {
        return ((Vector2)from).AngleTo(to);

    } // end AngleTo

    /// <summary>
    /// Returns a new Vector2 moved by length in given direction.
    /// </summary>
    public static Vector2 LengthDir(this Vector2 position, float moveAmount, float directionDegrees)
    {
        return new Vector2(position.X.LengthDirX(moveAmount, directionDegrees),
            position.Y.LengthDirY(moveAmount, directionDegrees));

    } // end LengthDir

    /// <summary>
    /// Returns a new x position moved by length in given direction.
    /// </summary>
    public static float LengthDirX(this float num, float length, float directionRad)
    {
        return num + MathF.Cos(directionRad) * length;

    } // end LengthDirX

    /// <summary>
    /// Returns a new y position moved by length in given direction.
    /// </summary>
    public static float LengthDirY(this float num, float length, float directionRad)
    {
        return num + MathF.Sin(directionRad) * length;

    } // end LengthDirY

} // end class OinkyRPGExtensions