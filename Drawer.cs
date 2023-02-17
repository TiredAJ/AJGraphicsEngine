using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BasicGraphicsEngine
{
    /// <summary>
    /// Class <c>Drawer</c> handles all the drawing stuff.
    /// </summary>
    public partial class Drawer
    {
        private List<DrawObject> ShapeList = new List<DrawObject>();
        public RectangleF Display = new RectangleF();
        public Vector2 Cursor = new Vector2(0, 0);

        /// <summary>
        /// Method <c>Drawer</c> Constructor :P.
        /// </summary>
        public Drawer(Rectangle _Display)
        { Display = _Display; }

        /// <summary>
        /// Method <c>Add</c> adds an object to the draw list.
        /// </summary>
        public void Add(DrawObject _NewShape)
        { ShapeList.Add(_NewShape); }

        /// <summary>
        /// Method <c>CallDraw</c> Goes through the draw list and draws.
        /// </summary>
        public void CallDraw(Graphics G)
        {
            Frame();

            foreach (DrawObject S in ShapeList)
            { S.Draw(G); }
        }

        /// <summary>
        /// Method <c>InitDraw</c> Draws the draw list once.
        /// </summary>
        public void InitDraw(Graphics G)
        {
            SetUp();

            foreach (DrawObject S in ShapeList)
            { S.Draw(G); }

            ShapeList.Clear();
        }

        /// <summary>
        /// Method <c>CleanUp</c> Cleans up ¯\_(ツ)_/¯.
        /// </summary>
        public void CleanUp()
        { ShapeList.Clear(); }

        /// <summary>
        /// Method <c>RandomValue</c> Returns a random value between two integers.
        /// </summary>
        public int RandomValue(int Min, int Max)
        {
            Random R = new Random(DateTime.Now.Microsecond);
            return R.Next(Min, Max);
        }

        /// <summary>
        /// Method <c>SetCursorPos</c> Sets the position of the cursor.
        /// </summary>
        public void SetCursorPos(Point _Loc)
        { Cursor = (Vector2)_Loc; }

    }

    public struct ShapeColours
    {
        Color Primary;
        Color Secondary;
        Color Tertiary;
        Color Border;
    }

    /// <summary>
    /// Class <c>Vector2</c> A class representing a 2D Vector datatype.
    /// </summary>
    public struct Vector2
    {
        public int X, Y;

        public Vector2()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Method <c>Vector2</c> Constructor that takes two integers for X and Y.
        /// </summary>
        public Vector2(int _X, int _Y)
        {
            X = _X;
            Y = _Y;
        }

        /// <summary>
        /// Method <c>Vector2</c> Constructor that takes a pre-existing Vector2 object.
        /// </summary>
        public Vector2(Vector2 _V)
        {
            X = _V.X;
            Y = _V.Y;
        }

        /// <summary>
        /// Method <c>Vector2</c> Constructor that takes a point object.
        /// </summary>
        public Vector2(Point _P)
        {
            X = _P.X;
            Y = _P.Y;
        }

        /// <summary>
        /// Method <c>GetMagnitude</c> returns an integer representing the magnitude of the vector
        /// </summary>
        public int GetMagnitude()
        { return (int)MathF.Sqrt((X * X) + (Y * Y)); }

        /// <summary>
        /// Method <c>Normalise</c> Normalises the vector.
        /// </summary>
        public Vector2 Normalise()
        {
            if (GetMagnitude() > 0)
            { return new Vector2((X / GetMagnitude()), (Y / GetMagnitude())); }
            throw new DivideByZeroException();
        }

        /// <summary>
        /// Method <c>Normalise</c> Normalises based off a scalar value.
        /// </summary>
        public Vector2 Normalise(int _Scalar)
        {
            if (_Scalar > 0)
            {return new Vector2((X / _Scalar), (Y / _Scalar));}
            throw new DivideByZeroException();
        }

        /// <summary>
        /// Method <c>Dot</c> returns the dot product of two vectors.
        /// </summary>
        public static float Dot(Vector2 _VA, Vector2 _VB)
        {return (_VA.X * _VB.X) + (_VA.Y * _VB.Y);}

        /// <summary>
        /// Method <c>ToString()</c> returns a string of the vector.
        /// </summary>
        public override string ToString()
        { return $"X:{X}, Y:{Y}"; }

        /// <summary>
        /// Method <c>ToPoint</c> returns a point representing the vector.
        /// </summary>
        public Point ToPoint()
        {return new Point(X, Y);}

        /// <summary>
        /// Method <c>ToPointArray</c> returns an array of points from an array of vectors.
        /// </summary>
        public static Point[] ToPointArray(Vector2[] _V2Arr)
        {
            List<Point> Temp = new List<Point>();
            for (int i = 0; i < _V2Arr.Length; i++)
            {Temp.Add(_V2Arr[i].ToPoint());}

            return Temp.ToArray();
        }

        /// <summary>
        /// Method <c>ToPointArray</c> returns an array of points from a list of vectors.
        /// </summary>
        public static Point[] ToPointArray(List<Vector2> _V2List)
        {
            List<Point> Temp = new List<Point>();

            foreach (Vector2 V2 in _V2List)
            {Temp.Add(V2.ToPoint());}

            return Temp.ToArray();
        }

        public static Vector2 operator +(Vector2 Va, Vector2 Vb)
        => new Vector2
        (
            Va.X + Vb.X,
            Va.Y + Vb.Y
        );

        public static Vector2 operator +(Vector2 Va, int Scalar)
        => new Vector2(Va.X + Scalar, Va.Y);

        public static Vector2 operator -(Vector2 Va, Vector2 Vb)
        => new Vector2
        (
            Va.X - Vb.X,
            Va.Y - Vb.Y
        );

        public static Vector2 operator -(Vector2 Va, int Scalar)
        => new Vector2(Va.X - Scalar, Va.Y - Scalar);

        public static Vector2 operator *(Vector2 Va, int Scalar)
        => new Vector2(Va.X * Scalar, Va.Y * Scalar);

        public static Vector2 operator /(Vector2 Va, int Scalar)
        => new Vector2(Va.X / Scalar, Va.Y / Scalar);
        
        public static explicit operator Vector2(Point _P) => new Vector2(_P);

    }
}
