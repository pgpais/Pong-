using System;
using Godot;

namespace Helpers
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
    }
}