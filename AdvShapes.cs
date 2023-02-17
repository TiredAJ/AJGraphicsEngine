namespace BasicGraphicsEngine
{
    public class DrawObject
    {
        public Color PrimaryCol = new Color();

        public virtual void Draw(Graphics G)
        { }
    }

    public class LineBase : DrawObject
    {
        public float LineWidth = 5f;

        public LineBase() { }

        public override void Draw(Graphics G)
        {}
    }

    public class Line : LineBase
    {
        public Vector2 A, B;

        public Line() { }

        public Line(Vector2 _A, Vector2 _B)
        {
            A = _A;
            B = _B;
        }

        public Line(Line _L)
        {
            A = _L.A;
            B = _L.B;
            LineWidth = _L.LineWidth;
            PrimaryCol = _L.PrimaryCol;
        }

        public override void Draw(Graphics G)
        {
            G.DrawLine
            (
                new Pen(PrimaryCol, LineWidth),
                A.ToPoint(), B.ToPoint()
            );
        }
    }

    public class Lines : LineBase
    {
        public List<Vector2> Points = new List<Vector2>();

        public Lines() { }

        public Lines(List<Vector2> _Points)
        { Points = _Points; }

        public Lines(Lines _L)
        {
            Points = _L.Points;
            LineWidth = _L.LineWidth;
            PrimaryCol = _L.PrimaryCol;
        }

        public override void Draw(Graphics G)
        {
            G.DrawLines
            (
                new Pen(PrimaryCol, LineWidth),
                Vector2.ToPointArray(Points)
            );
        }
    }

    public class AdvShapes : DrawObject
    {
        public float BorderWidth = 2f;
        public Vector2 Centre = new Vector2();
        public float TopEdge, BottomEdge, LeftEdge, RightEdge;
        public Color PrimaryCol = new Color();
        public Color SecondaryCol, TertiaryCol = Color.Transparent;

        public virtual void CalculateBounds()
        {}

        public override void Draw(Graphics G)
        {}
    }

    public class Square : AdvShapes
    {
        Vector2 CornerA, CornerB, CornerC, CornerD;

        public Square() { }

        public Square(Vector2[] _Corners)
        {
            if (_Corners.Length < 4 || _Corners.Length > 4)
            {
                throw new ArgumentException("Array must contain 4 values!");
            }

            CornerA = _Corners[0];
            CornerB = _Corners[1];
            CornerC = _Corners[2];
            CornerD = _Corners[3];
        }

        public Square(Square _S)
        {
            _S.Corners.CopyTo(Corners, 0);
            BorderWidth = _S.BorderWidth;
            Centre = new Vector2(_S.Centre.X, _S.Centre.Y);
            TopEdge = _S.TopEdge; BottomEdge = _S.BottomEdge;
            LeftEdge = _S.LeftEdge; RightEdge = _S.RightEdge;
            PrimaryCol = _S.PrimaryCol;
            SecondaryCol = _S.SecondaryCol;
            TertiaryCol = _S.TertiaryCol;
        }

        public override void Draw(Graphics G) 
        {/*stuff*/ }
    }
}
