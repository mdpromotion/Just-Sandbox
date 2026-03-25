using System;

namespace Shared.Data
{
    /// <summary>
    /// Represents a three-dimensional position or vector in space using single-precision floating-point X, Y, and Z
    /// coordinates.
    /// </summary>
    /// <remarks>The Position3 struct provides methods for common vector operations such as normalization, dot
    /// product, cross product, and distance calculations. It also includes static properties for frequently used
    /// positions, including Zero, One, UnitX, UnitY, and UnitZ. Position3 is immutable and supports arithmetic
    /// operations for convenient manipulation of 3D positions or directions.</remarks>
    public struct Position3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Position3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Position3 Zero => new(0f, 0f, 0f);
        public static Position3 One => new(1f, 1f, 1f);

        public static Position3 UnitX => new(1f, 0f, 0f);
        public static Position3 UnitY => new(0f, 1f, 0f);
        public static Position3 UnitZ => new(0f, 0f, 1f);

        public float SqrMagnitude => LengthSquared();

        public float Length()
        {
            return MathF.Sqrt(X * X + Y * Y + Z * Z);
        }

        public float LengthSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        public Position3 Normalize()
        {
            float len = Length();
            if (len == 0f)
                return Zero;

            float inv = 1f / len;
            return new Position3(X * inv, Y * inv, Z * inv);
        }

        public static float Dot(Position3 a, Position3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static Position3 Cross(Position3 a, Position3 b)
        {
            return new Position3(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static float Distance(Position3 a, Position3 b)
        {
            return (a - b).Length();
        }

        public static float DistanceSquared(Position3 a, Position3 b)
        {
            return (a - b).LengthSquared();
        }

        public static Position3 Lerp(Position3 a, Position3 b, float t)
        {
            return a + (b - a) * t;
        }

        public static Position3 Clamp(Position3 value, Position3 min, Position3 max)
        {
            return new Position3(
                Math.Clamp(value.X, min.X, max.X),
                Math.Clamp(value.Y, min.Y, max.Y),
                Math.Clamp(value.Z, min.Z, max.Z)
            );
        }

        public static Position3 Min(Position3 a, Position3 b)
        {
            return new Position3(
                MathF.Min(a.X, b.X),
                MathF.Min(a.Y, b.Y),
                MathF.Min(a.Z, b.Z)
            );
        }

        public static Position3 Max(Position3 a, Position3 b)
        {
            return new Position3(
                MathF.Max(a.X, b.X),
                MathF.Max(a.Y, b.Y),
                MathF.Max(a.Z, b.Z)
            );
        }

        public static Position3 Reflect(Position3 vector, Position3 normal)
        {
            return vector - 2f * Dot(vector, normal) * normal;
        }

        public static Position3 Abs(Position3 value)
        {
            return new Position3(
                MathF.Abs(value.X),
                MathF.Abs(value.Y),
                MathF.Abs(value.Z)
            );
        }

        public static Position3 operator +(Position3 a, Position3 b)
        {
            return new Position3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Position3 operator -(Position3 a, Position3 b)
        {
            return new Position3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Position3 operator -(Position3 v)
        {
            return new Position3(-v.X, -v.Y, -v.Z);
        }

        public static Position3 operator *(Position3 v, float scalar)
        {
            return new Position3(v.X * scalar, v.Y * scalar, v.Z * scalar);
        }

        public static Position3 operator *(float scalar, Position3 v)
        {
            return v * scalar;
        }

        public static Position3 operator /(Position3 v, float scalar)
        {
            float inv = 1f / scalar;
            return new Position3(v.X * inv, v.Y * inv, v.Z * inv);
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}