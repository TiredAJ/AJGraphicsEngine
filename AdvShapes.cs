namespace BasicGraphicsEngine
{
    public class DrawObject
    {
        public Color PrimaryCol = new Color();
        //public Bitmap? Canvas;

        public virtual void Draw(Graphics G)
        { }
    }

    public class LineBase : DrawObject
    {
        public float LineWidth = 5f;
        //public Color PrimaryCol = new Color();
        //public Bitmap? _Canvas;

        public LineBase() { }

        public override void Draw(Graphics G)
        {}
    }

    public class Line : LineBase
    {
        public Point A, B;

        public Line() { }

        public Line(Point _A, Point _B)
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
                A, B
            );

            //G.Dispose();
        }
    }

    public class Lines : LineBase
    {
        public List<Point> Points = new List<Point>();

        public Lines() { }

        public Lines(List<Point> _Points)
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
                Points.ToArray()
            );

            //G.Dispose();
        }
    }

    public class AdvShapes : DrawObject
    {
        public float BorderWidth = 2f;
        public Point Centre = new Point();
        public float TopEdge, BottomEdge, LeftEdge, RightEdge;
        public Color PrimaryCol = new Color();
        public Color SecondaryCol, TertiaryCol = Color.Transparent;
        public Bitmap? Canvas;

        public virtual void CalculateBounds()
        {}

        public override void Draw(Graphics G)
        {}
    }

    public class Square : AdvShapes
    {
        public Point[] Corners = new Point[4];

        public Square() { }

        public Square(Point[] _Corners)
        {Corners = _Corners;}

        public Square(Square _S)
        {
            _S.Corners.CopyTo(Corners, 0);
            BorderWidth = _S.BorderWidth;
            Centre = new Point(_S.Centre.X, _S.Centre.Y);
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
