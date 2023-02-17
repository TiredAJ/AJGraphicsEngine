namespace BasicGraphicsEngine
{
    /// <summary>
    /// Class <c>DrawObject</c> Base object for all drawn objects. Do not use directly.
    /// </summary>
    public class DrawObject
    {
        public Color PrimaryCol = new Color();

        /// <summary>
        /// Method <c>Draw</c> renders the object using the inputted Graphics object.
        /// </summary>
        public virtual void Draw(Graphics G)
        { }
    }

    /// <summary>
    /// Class <c>LineBase</c>. Base object for lines. Do not use directly.
    /// </summary>
    public class LineBase : DrawObject
    {
        public float LineWidth = 5f;

        public LineBase() { }

        public override void Draw(Graphics G)
        {}
    }

    /// <summary>
    /// Class <c>Line</c> For drawing basic 2-point lines
    /// </summary>
    public class Line : LineBase
    {
        public Vector2 A, B;

        public Line() { }

        /// <summary>
        /// Method <c>Line</c>. Constructor that takes in two vectors for points.
        /// </summary>
        public Line(Vector2 _A, Vector2 _B)
        {
            A = _A;
            B = _B;
        }
        
        /// <summary>
        /// Method <c>Line</c>. Constructor that takes in a pre-existing line object.
        /// </summary>
        public Line(Line _L)
        {
            A = _L.A;
            B = _L.B;
            LineWidth = _L.LineWidth;
            PrimaryCol = _L.PrimaryCol;
        }

        /// <summary>
        /// Method <c>Draw</c> renders the Line object using the inputted Graphics object.
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
    /// Class <c>Lines</c>. For drawing a multi-point line.
    /// </summary>
    public class Lines : LineBase
    {
        public List<Vector2> Points = new List<Vector2>();

        public Lines() { }

        /// <summary>
        /// Method <c>Lines</c>. Constructor that takes a list of Vector2's.
        /// </summary>
        public Lines(List<Vector2> _Points)
        { Points = _Points; }

        /// <summary>
        /// Method <c>Lines</c>. Constructor that takes in a pre-exising Lines object.
        /// </summary>
        public Lines(Lines _L)
        {
            Points = _L.Points;
            LineWidth = _L.LineWidth;
            PrimaryCol = _L.PrimaryCol;
        }

        /// <summary>
        /// Method <c>Draw</c> renders the Lines object using the inputted Graphics object.
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
    /// Class <c>DrawObject</c> Base object for all drawn objects. Do not use directly.
    /// </summary>
    public class AdvShapes : DrawObject
    {
        public float BorderWidth = 2f;
        public Vector2 Centre = new Vector2();
        public float TopEdge, BottomEdge, LeftEdge, RightEdge;
        public Color PrimaryCol = new Color();
        public Color SecondaryCol, TertiaryCol = Color.Transparent;

        /// <summary>
        /// Method <c>CalculateBounds</c>. Potentially subject to removal. Calculates the bounds of the object
        /// </summary>
        public virtual void CalculateBounds()
        {}

        /// <summary>
        /// Method <c>Draw</c> renders the object using the inputted Graphics object.
        /// </summary>
        public override void Draw(Graphics G)
        {}
    }

    /// <summary>
    /// Class <c>Square</c>. "Advanced" square object.
    /// </summary>
    public class Square : AdvShapes
    {
        Vector2 CornerA, CornerB, CornerC, CornerD;

        public Square() { }
        
        /// <summary>
        /// Method <c>Square</c>. Square constructor, takes in an array of Vector2's.
        /// </summary>
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

        /// <summary>
        /// Method <c>Square</c> Constructor that takes in a pre-existing (advance) Square object.
        /// </summary>
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

        /// <summary>
        /// Method <c>Draw</c> renders the object using the inputted Graphics object.
        /// </summary>
        public override void Draw(Graphics G) 
        {/*stuff*/ }
    }
}
