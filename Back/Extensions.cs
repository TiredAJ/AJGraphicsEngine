using System.Numerics;

namespace BasicGraphicsEngine.Back.Extensions
{
    public static class Vector2Ext
    {
        /// <summary>
        /// Returns a float representing the magnitude of this vector
        /// </summary>
        public static float GetMagnitude(this Vector2 V2)
        { return MathF.Sqrt(((float)V2.X * (float)V2.X) + ((float)V2.Y * (float)V2.Y)); }

        /// <summary>
        /// Returns a float representing the magnitude of this and another vector
        /// </summary>
        public static float GetMagnitude(this Vector2 V2A, Vector2 V2B)
        {
            Vector2 AB = new Vector2(V2A.X - V2B.X, V2A.Y - V2B.Y);

            return MathF.Sqrt(((float)AB.X * (float)AB.X) + ((float)AB.Y * (float)AB.Y));
        }

        /// <summary>
        /// Returns a float representing the squared magnitude of this vector
        /// </summary>
        /// <returns></returns>
        public static float GetMagnitudeSQ(this Vector2 V2)
        { return (((float)V2.X * (float)V2.X) + ((float)V2.Y * (float)V2.Y)); }

        /// <summary>
        /// Normalises this Vector based off a scalar value.
        /// </summary>
        public static Vector2 Normalise(this Vector2 V2, float _Scalar)
        {
            if (_Scalar > 0)
            { return new Vector2((V2.X / _Scalar), (V2.Y / _Scalar)); }
            throw new DivideByZeroException();
        }

        /// <summary>
        /// Returns a <c>Point</c> representing this vector.
        /// </summary>
        public static Point ToPoint(this Vector2 V2)
        => new Point((int)V2.X, (int)V2.Y);

        /// <summary>
        /// Returns a <c>PointF</c> representing this vector.
        /// </summary>
        public static PointF ToPointF(this Vector2 V2)
        => new PointF(V2.X, V2.Y);

        /// <summary>
        /// Returns a <c>Vector2</c> representing this point.
        /// </summary>
        public static Vector2 ToV2(this Point V2)
        => new Vector2(V2.X, V2.Y);

        /// <summary>
        /// Returns a <c>Vector2</c> representing this point.
        /// </summary>
        public static Vector2 ToV2(this PointF V2)
        => new Vector2(V2.X, V2.Y);

        /// <summary>
        /// Returns an array of points from this array of vectors.
        /// </summary>
        public static Point[] ToPointArray(this Vector2[] _V2Arr)
        {
            List<Point> Temp = new List<Point>();

            foreach (Vector2 V2 in _V2Arr)
            { Temp.Add(ToPoint(V2)); }

            return Temp.ToArray();
        }

        /// <summary>
        /// Returns an array of points from this list of vectors.
        /// </summary>
        public static Point[] ToPointArray(this List<Vector2> _V2List)
        {
            List<Point> Temp = new List<Point>();

            foreach (Vector2 V2 in _V2List)
            { Temp.Add(ToPoint(V2)); }

            return Temp.ToArray();
        }

        /// <summary>
        /// Limits this vector to a maximum magnitude
        /// </summary>
        /// <param name="Max">Maximum magnitude</param>
        /// <returns>A vector who's magnitude <= Max</returns>
        public static Vector2 Limit(this Vector2 V2, float Max)
        {
            if (GetMagnitudeSQ(V2) > (Max * Max))
            {
                V2 = Vector2.Divide(V2, GetMagnitude(V2));

                V2 = Vector2.Multiply(V2, Max);
            }
            return V2;
        }

        /// <summary>
        /// Ensures this vector's components are no larger than Max's
        /// </summary>
        /// <param name="Max">Vector of maximum components</param>
        /// <returns>A Vector who's components are <= Max's</returns>
        public static Vector2 Limit(this Vector2 V2, Vector2 Max)
        {
            if (V2.X > Max.X)
            { V2.X = Max.X; }
            if (V2.Y > Max.Y)
            { V2.Y = Max.Y; }

            return V2;
        }

        /// <summary>
        /// Ensures this vector's components are within Max's and Min's components
        /// </summary>
        /// <param name="Max">Vector of maximum components</param>
        /// <param name="Min">Vector of minimum components</param>
        /// <returns>A Vector who's components are <= Max's and >= Min's</returns>
        public static Vector2 Limit(this Vector2 V2, Vector2 Max, Vector2 Min)
        {
            if (V2.X > Max.X)
            { V2.X = Max.X; }
            else if (V2.X < Min.X)
            { V2.X = Min.X; }

            if (V2.Y > Max.Y)
            { V2.Y = Max.Y; }
            else if (V2.Y < Min.Y)
            { V2.Y = Min.Y; }

            return V2;
        }

        /// <summary>
        /// Offsets this vector by a specific amount
        /// </summary>
        /// <param name="XOff">X offset</param>
        /// <param name="YOff">Y offset</param>
        /// <returns>The vector offsetted by XOff and YOff</returns>
        public static Vector2 Offset(this Vector2 V2, int XOff, int YOff)
        => new Vector2(V2.X + XOff, V2.Y + YOff);

        /// <summary>
        /// Applies a mvoe transform to this vector
        /// </summary>
        /// <param name="Destination"></param>
        /// <returns>The moved vector</returns>
        public static Vector2 MoveTransform(this Vector2 Point, Vector2 Destination)
        {
            Vector2 Delta = Vector2.Subtract(Destination, Point);

            Point += Delta;

            return Point;
        }
    }
}
