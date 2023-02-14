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
        public PointF A, B;

        public Line() { }

        public Line(PointF _A, PointF _B)
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
        public List<PointF> Points = new List<PointF>();

        public Lines() { }

        public Lines(List<PointF> _Points)
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
        public PointF Centre = new PointF();
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
        public PointF[] Corners = new PointF[4];

        public Square() { }

        public Square(PointF[] _Corners)
        {Corners = _Corners;}

        public Square(Square _S)
        {
            _S.Corners.CopyTo(Corners, 0);
            BorderWidth = _S.BorderWidth;
            Centre = new PointF(_S.Centre.X, _S.Centre.Y);
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
