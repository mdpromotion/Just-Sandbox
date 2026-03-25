using Shared.Data;
using UnityEngine;

public static class Mapper
{
    public static Vector3 ToVector3(this Position3 position)
    {
        return new Vector3(position.X, position.Y, position.Z);
    }
    public static Position3 ToPosition3(this Vector3 vector)
    {
        return new Position3(vector.x, vector.y, vector.z);
    }
    public static Vector2 ToVector2(this Position2 position)
    {
        return new Vector2(position.X, position.Y);
    }
     public static Position2 ToPosition2(this Vector2 vector)
    {
        return new Position2(vector.x, vector.y);
    }
}
