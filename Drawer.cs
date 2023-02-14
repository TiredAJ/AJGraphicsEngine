using System.Diagnostics;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {
        private List<DrawObject> ShapeList = new List<DrawObject>();
        public RectangleF Display = new RectangleF();
        public PointF Cursor = new PointF(0f, 0f);

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

        public void SetCursorPos(PointF _Loc)
        { Cursor = _Loc; }

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
        public float X, Y;

        public Vector2()
        {
            X = 0f;
            Y = 0f;
        }

        public Vector2(float _X, float _Y)
        {
            X = _X;
            Y = _Y;
        }

        public float GetMagnitude()
        { return MathF.Sqrt((X * X) + (Y * Y)); }

        public Vector2 Normalise()
        {
            if (GetMagnitude() > 0)
            { return new Vector2((X / GetMagnitude()), (Y / GetMagnitude())); }
            throw new DivideByZeroException();
        }

        public override string ToString()
        { return $"X:{X}, Y:{Y}"; }

        public static Vector2 operator +(Vector2 Va, Vector2 Vb)
        => new Vector2
        (
            Va.X + Vb.X,
            Va.Y + Vb.Y
        );

        public static Vector2 operator +(Vector2 Va, float Scalar)
        => new Vector2(Va.X + Scalar, Va.Y);

        public static Vector2 operator -(Vector2 Va, Vector2 Vb)
        => new Vector2
        (
            Va.X - Vb.X,
            Va.Y - Vb.Y
        );

        public static Vector2 operator -(Vector2 Va, float Scalar)
        => new Vector2(Va.X - Scalar, Va.Y - Scalar);

        public static Vector2 operator *(Vector2 Va, float Scalar)
        => new Vector2(Va.X * Scalar, Va.Y * Scalar);

        public static Vector2 operator /(Vector2 Va, float Scalar)
        => new Vector2(Va.X / Scalar, Va.Y / Scalar);
    }
}
