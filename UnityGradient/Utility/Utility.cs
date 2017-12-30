using UnityEngine;

namespace UnityGradient.Utility
{
    public static class Utility
    {
        public static float DistanceSquared(Vector2 a, Vector2 b)
        {
            return Mathf.Pow(b.x - a.x, 2) + Mathf.Pow(b.y - a.y, 2);
        }
        public static int GetIndex(float x, float y, float width)
        {
            return GetIndex(Mathf.RoundToInt(x), Mathf.RoundToInt(y), Mathf.RoundToInt(width));
        }
        public static int GetIndex(int x, int y, int width)
        {
            return y * width + x;
        }
        public static Vector2 Vector2Clamp(Vector2 value, Vector2 min, Vector2 max)
        {
            return new Vector2()
            {
                x = Mathf.Clamp(value.x, min.x, max.x),
                y = Mathf.Clamp(value.y, min.y, max.y),
            };
        }
        /// <summary>
        /// Returns a color based on a brush
        /// </summary>
        /// <param name="position">Specifies position in the space (0, 0) to (1, 0) - out of bounds values will be clamped</param>
        /// <param name="brush">Brush specified from editor</param>
        public static Color GetColor(Vector2 position, GradientBase brush)
        {
            position = Vector2Clamp(position, Vector2.zero, Vector2.one);

            if (brush is LinearGradient)
            {
                return GetColor(position, (LinearGradient)brush);
            }
            else
            {
                throw new System.NotImplementedException("Does not recognize " + brush.GetType());
            }
        }
        /*private static Color GetColor(Vector2 position, RadialGradientBrush brush)
        {
            Vector2 radial = brush.GradientOrigin.ToVector();

            Vector2 delta = radial - position;

            delta.X /= (float)brush.RadiusX;
            delta.Y /= (float)brush.RadiusY;

            return GetColor(delta.Length(), brush);
        }*/
        private static Color GetColor(Vector2 position, LinearGradient gradient)
        {
            float offset = PerpendicularDistance(gradient.StartPoint, gradient.EndPoint, position);

            return gradient.Brush.Evaluate(offset);
        }
        /// <summary>
        /// Returns point on line segment betwee <paramref name="a"/> & <paramref name="b"/> perpendicular to <paramref name="p"/>
        /// </summary>
        public static Vector2 LineSegmentPoint(Vector2 a, Vector2 b, Vector2 p)
        {
            float lineLength = DistanceSquared(a, b);

            //a == b
            if (lineLength == 0)
                return p;

            float dot = Vector2.Dot(p - a, b - a);
            float t = Mathf.Clamp(dot / lineLength, 0, 1);

            return new Vector2()
            {
                x = a.x + t * (b.x - a.x),
                y = a.y + t * (b.y - a.y),
            };
        }
        /// <summary>
        /// Returns the distance between <paramref name="a"/> and <paramref name="b"/> using <paramref name="p"/> as the perpendicular midway point
        /// </summary>
        /// <returns>Distance between <paramref name="a"/> and line segment point</returns>
        private static float PerpendicularDistance(Vector2 a, Vector2 b, Vector2 p)
        {
            return Mathf.Clamp(Vector2.Distance(a, LineSegmentPoint(a, b, p)), 0, 1);
        }
        private static double GetLocalDelta(double globalOffset, double leftOffset, double rightOffset)
        {
            if (rightOffset == leftOffset)
                return 0;

            return (globalOffset - leftOffset) / (rightOffset - leftOffset);
        }
    }
}
