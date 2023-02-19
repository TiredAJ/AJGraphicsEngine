
namespace BasicGraphicsEngine
{
    /// <summary>
    /// Base object for all drawn objects. Do not use directly.
    /// </summary>
    public class DrawObject
    {
        public Color PrimaryCol = new Color();
        public Color SecondaryCol = new Color();

        /// <summary>
        /// Method <c>Draw</c> renders the object using the inputted Graphics object.
        /// </summary>
        public virtual void Draw(Graphics G)
        { }
    }

    /// <summary>
    /// Base object for lines. Do not use directly.
    /// </summary>
    public class LineBase : DrawObject
    {
        public float LineWidth = 5f;

        public LineBase() { }

        public override void Draw(Graphics G)
        {}
    }

    /// <summary>
    /// For drawing basic 2-point lines
    /// </summary>
    public class Line : LineBase
    {
        public Vector2 A, B;

        public Line() { }

        /// <summary>
        /// Constructor that takes in two <c>Vectors</c> for points.
        /// </summary>
        public Line(Vector2 _A, Vector2 _B)
        {
            A = _A;
            B = _B;
        }

        /// <summary>
        /// Constructor that takes in a pre-existing <c>Line</c> object.
        /// </summary>
        public Line(Line _L)
        {
            A = _L.A;
            B = _L.B;
            LineWidth = _L.LineWidth;
            PrimaryCol = _L.PrimaryCol;
        }

        /// <summary>
        /// Renders the Line object using the inputted <c>Graphics</c> object.
        /// </summary>
        public override void Draw(Graphics G)
        {
            G.DrawLine
            (
                new Pen(PrimaryCol, LineWidth),
                A.ToPoint(), B.ToPoint()
            );
        }
    }

    /// <summary>
    /// For drawing a multi-point line.
    /// </summary>
    public class Lines : LineBase
    {
        public List<Vector2> Points = new List<Vector2>();

        public Lines() { }

        /// <summary>
        /// Constructor that takes a list of <c>Vector2</c>s.
        /// </summary>
        public Lines(List<Vector2> _Points)
        { Points = _Points; }

        /// <summary>
        /// Constructor that takes in a pre-existing <c>Lines</c> object.
        /// </summary>
        public Lines(Lines _L)
        {
            Points = _L.Points;
            LineWidth = _L.LineWidth;
            PrimaryCol = _L.PrimaryCol;
        }

        /// <summary>
        /// Renders the Lines object using the inputted <c>Graphics</c> object.
        /// </summary>
        public override void Draw(Graphics G)
        {
            G.DrawLines
            (
                new Pen(PrimaryCol, LineWidth),
                Vector2.ToPointArray(Points)
            );
        }
    }

    /// <summary>
    /// Base object for all drawn objects. Do not use directly.
    /// </summary>
    public class AdvShapes : DrawObject
    {
        public float BorderWidth = 5f;
        public Vector2 Centre = new Vector2();
        public Color TertiaryCol = Color.Transparent;

        /// <summary>
        /// Potentially subject to removal. Calculates the bounds of the object
        /// </summary>
        public virtual void CalculateBounds()
        {}

        /// <summary>
        /// Renders the object using the inputted <c>Graphics</c> object.
        /// </summary>
        public override void Draw(Graphics G)
        {}
    }

    /// <summary>
    /// "Advanced" square object.
    /// </summary>
    public class Square : AdvShapes
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Vector2 CornerA { get; private set; }
        public Vector2 CornerB { get; private set; }
        public Vector2 CornerC { get; private set; }
        public Vector2 CornerD { get; private set; }

        public Square() 
        {Centre = new Vector2();}

        /// <summary>
        /// Square constructor, takes in an array of <c>Vector2</c>s.
        /// </summary>
        public Square(Vector2[] _Corners)
        {
            if (_Corners.Length < 4 || _Corners.Length > 4)
            {throw new ArgumentException("Array must contain 4 values!");}

            Centre = new Vector2
            (
                CornerA.X + (Width/2),
                CornerA.Y + (Height/2)
            );

            CornerA = _Corners[0];
            CornerB = _Corners[1];
            CornerC = _Corners[2];
            CornerD = _Corners[3];
        }

        /// <summary>
        /// Constructor that takes in a pre-existing <c>Square</c> object.
        /// </summary>
        public Square(Square _S)
        {
            CornerA = _S.CornerA;
            CornerB = _S.CornerB;
            CornerC = _S.CornerC;
            CornerD = _S.CornerD;
            BorderWidth = _S.BorderWidth;
            Centre = new Vector2(_S.Centre.X, _S.Centre.Y);
            PrimaryCol = _S.PrimaryCol;
            SecondaryCol = _S.SecondaryCol;
            TertiaryCol = _S.TertiaryCol;
        }

        public Square(Rectangle _R)
        {
            Width = _R.Width;
            Height = _R.Height;
            CornerA = new Vector2(_R.X, _R.Y);
            CornerB = new Vector2(_R.X + _R.Width, _R.Y);
            CornerC = new Vector2(_R.X + _R.Width, _R.Y + _R.Height);
            CornerC = new Vector2(_R.X, _R.Y - _R.Height + _R.Height);

            Centre = new Vector2
            (
                CornerA.X + (Width / 2),
                CornerA.Y + (Height / 2)
            );
        }

        /// <summary>
        /// Renders the object using the inputted <c>Graphics</c> object.
        /// </summary>
        public override void Draw(Graphics G) 
        {
            Point[] TempPointArr = new Point[]
            {
                CornerA.ToPoint(), CornerB.ToPoint(),
                CornerC.ToPoint(), CornerD.ToPoint()
            };

            G.FillPolygon
            (
                new SolidBrush(SecondaryCol),
               TempPointArr
            );

            G.DrawPolygon
            (
                new Pen(PrimaryCol, BorderWidth),
               TempPointArr
            );
        }

        /// <summary>
        /// Sets the centre of the object.
        /// </summary>
        public void SetCentre(Vector2 _Centre)
        {
            Centre = _Centre;

            CornerA = new Vector2(_Centre.X - (Width / 2), _Centre.Y - (Height / 2));
            CornerB = new Vector2(_Centre.X + (Width / 2), _Centre.Y - (Height / 2));
            CornerC = new Vector2(_Centre.X + (Width / 2), _Centre.Y + (Height / 2));
            CornerD = new Vector2(_Centre.X - (Width / 2), _Centre.Y + (Height / 2));
        }
    }
}
