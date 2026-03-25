using System;

namespace Shared.Data
{
    public readonly struct Position2
    {
        public float X { get; }
        public float Y { get; }

        public Position2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Position2 Zero => new(0f, 0f);
        public static Position2 One => new(1f, 1f);

        public static Position2 UnitX => new(1f, 0f);
        public static Position2 UnitY => new(0f, 1f);

        public float Length()
        {
            return MathF.Sqrt(X * X + Y * Y);
        }

        public float LengthSquared()
        {
            return X * X + Y * Y;
        }

        public Position2 Normalize()
        {
            float len = Length();
            if (len == 0f)
                return Zero;

            float inv = 1f / len;
            return new Position2(X * inv, Y * inv);
        }

        public static float Dot(Position2 a, Position2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static float Distance(Position2 a, Position2 b)
        {
            return (a - b).Length();
        }

        public static float DistanceSquared(Position2 a, Position2 b)
        {
            return (a - b).LengthSquared();
        }

        public static Position2 Lerp(Position2 a, Position2 b, float t)
        {
            return a + (b - a) * t;
        }

        public static Position2 Clamp(Position2 value, Position2 min, Position2 max)
        {
            return new Position2(
                Math.Clamp(value.X, min.X, max.X),
                Math.Clamp(value.Y, min.Y, max.Y)
            );
        }

        public static Position2 Min(Position2 a, Position2 b)
        {
            return new Position2(MathF.Min(a.X, b.X), MathF.Min(a.Y, b.Y));
        }

        public static Position2 Max(Position2 a, Position2 b)
        {
            return new Position2(MathF.Max(a.X, b.X), MathF.Max(a.Y, b.Y));
        }

        public static Position2 Reflect(Position2 vector, Position2 normal)
        {
            return vector - 2f * Dot(vector, normal) * normal;
        }

        public static Position2 Abs(Position2 value)
        {
            return new Position2(MathF.Abs(value.X), MathF.Abs(value.Y));
        }

        public static Position2 operator +(Position2 a, Position2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Position2 operator -(Position2 a, Position2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Position2 operator -(Position2 v) => new(-v.X, -v.Y);
        public static Position2 operator *(Position2 v, float scalar) => new(v.X * scalar, v.Y * scalar);
        public static Position2 operator *(float scalar, Position2 v) => v * scalar;
        public static Position2 operator /(Position2 v, float scalar) => new(v.X / scalar, v.Y / scalar);

        public static Position3 operator *(Position2 a, Position3 b) =>
            new(a.X * b.X, a.Y * b.Y, b.Z);

        public static Position3 operator *(Position3 a, Position2 b) =>
            new(a.X * b.X, a.Y * b.Y, a.Z);

        public override string ToString() => $"({X}, {Y})";
    }
}