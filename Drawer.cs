using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {
        private List<DrawObject> ShapeList = new List<DrawObject>();
        public RectangleF Display = new RectangleF();
        public Vector2 Cursor = new Vector2(0, 0);

        public Drawer(Rectangle _Display)
        { Display = _Display; }

        public void Add(DrawObject _NewShape)
        { ShapeList.Add(_NewShape); }

        public void CallDraw(Graphics G)
        {
            Frame();

            foreach (DrawObject S in ShapeList)
            { S.Draw(G); }
        }

        public void InitDraw(Graphics G)
        {
            SetUp();

            foreach (DrawObject S in ShapeList)
            { S.Draw(G); }

            ShapeList.Clear();
        }

        public void CleanUp()
        { ShapeList.Clear(); }

        public int RandomValue(int Min, int Max)
        {
            Random R = new Random(DateTime.Now.Microsecond);
            return R.Next(Min, Max);
        }

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

    public struct Vector2
    {
        public int X, Y;

        public Vector2()
        {
            X = 0;
            Y = 0;
        }

        public Vector2(int _X, int _Y)
        {
            X = _X;
            Y = _Y;
        }

        public Vector2(Vector2 _V)
        {
            X = _V.X;
            Y = _V.Y;
        }

        public Vector2(Point _P)
        {
            X = _P.X;
            Y = _P.Y;
        }

        public int GetMagnitude()
        { return (int)MathF.Sqrt((X * X) + (Y * Y)); }

        public Vector2 Normalise()
        {
            if (GetMagnitude() > 0)
            { return new Vector2((X / GetMagnitude()), (Y / GetMagnitude())); }
            throw new DivideByZeroException();
        }

        public Vector2 Normalise(int _Scalar)
        {
            if (_Scalar > 0)
            {return new Vector2((X / _Scalar), (Y / _Scalar));}
            throw new DivideByZeroException();
        }
        
        public Vector2 Normalise(Vector2 _V)
        {
            return new Vector2(_V.Normalise());
        }

        public static float Dot(Vector2 _VA, Vector2 _VB)
        {return (_VA.X * _VB.X) + (_VA.Y * _VB.Y);}

        public override string ToString()
        { return $"X:{X}, Y:{Y}"; }

        public Point ToPoint()
        {return new Point(X, Y);}

        public static Point[] ToPointArray(Vector2[] _V2Arr)
        {
            List<Point> Temp = new List<Point>();
            for (int i = 0; i < _V2Arr.Length; i++)
            {Temp.Add(_V2Arr[i].ToPoint());}

            return Temp.ToArray();
        }
        
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
