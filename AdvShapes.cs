namespace BasicGraphicsEngine
{
    public class DrawObject
    {
        public Color PrimaryCol = new Color();
        //public Bitmap? Canvas;

        public virtual Bitmap Draw(Bitmap _Canvas)
        { return _Canvas; }
    }

    public class LineBase : DrawObject
    {
        public float LineWidth = 5f;
        //public Color PrimaryCol = new Color();
        //public Bitmap? _Canvas;

        public LineBase() { }

        public override Bitmap Draw(Bitmap _Canvas)
        { return _Canvas; }
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

        public override Bitmap Draw(Bitmap _Canvas)
        {
            Graphics.FromImage(_Canvas).DrawLine
            (
                new Pen(PrimaryCol, LineWidth),
                A, B
            );

            return _Canvas;
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

        public override Bitmap Draw(Bitmap _Canvas)
        {
            Graphics G = Graphics.FromImage(_Canvas);

            G.DrawLines
            (
                new Pen(PrimaryCol, LineWidth),
                Points.ToArray()
            );

            G.Dispose();

            return _Canvas;
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

        public override Bitmap Draw(Bitmap _Canvas)
        {return _Canvas;}
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
    }
}
