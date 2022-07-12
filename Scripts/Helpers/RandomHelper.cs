using System;
using Godot;

namespace Pong.Scripts.Helpers
{

    public static class RandomHelper
    {
        public static Vector2 RandomInsideUnitCircle()
        {
            var random = new Random();
            var a = random.NextDouble() * (2 * Mathf.Pi) - Mathf.Pi;
            var x = Mathf.Cos((float)a);
            var y = Mathf.Sin((float)a);
            var result = new Vector2(x, y);
            if (result.LengthSquared() > 1)
            {
                GD.Print($"Created random unit vector has length over 1! Vector: {result}");
            }
            return result;
        }

        public static Vector2 RandomVector2(float angleMaxDegrees, float angleMinDegrees)
        {
            float angleDiffRad = Mathf.Deg2Rad(angleMaxDegrees) - Mathf.Deg2Rad(angleMinDegrees);
            float angleRad = (float)new Random().NextDouble() * angleDiffRad + angleMinDegrees;
            return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).Normalized();
        }

        public static float Interpolate(float start, float end, float t)
        {
            return start + (end - start) * t;
        }
    }
}