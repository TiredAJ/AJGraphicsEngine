using System.Numerics;

namespace BasicGraphicsEngine
{
    /// <summary>
    /// Base object for all drawn objects. Do not use directly.
    /// </summary>
    public class DrawObject
    {
        public Color PrimaryCol = Color.Red;
        public Color SecondaryCol = Color.Green;
        public Vector2 DisplayCentre = new Vector2();

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
        { }
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
                V2Ext.ToPoint(A), V2Ext.ToPoint(B)
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
                V2Ext.ToPointArray(Points)
            );
        }
    }

    /// <summary>
    /// Base object for all drawn objects. Do not use directly.
    /// </summary>
    public class AdvShapes : DrawObject
    {
        public float BorderWidth = 5f;
        public Vector2 Centre { get; protected set; } = new Vector2();
        public Color TertiaryCol = Color.Transparent;

        /// <summary>
        /// Potentially subject to removal. Calculates the bounds of the object
        /// </summary>
        public virtual void CalculateBounds()
        { }

        /// <summary>
        /// Renders the object using the inputted <c>Graphics</c> object.
        /// </summary>
        public override void Draw(Graphics G)
        { }

        public virtual void MoveTransform(Vector2 V2)
        { }

        public virtual void SizeTransform(Vector2 V2)
        { }


        public virtual void Rotate(Vector2 _RotationPoint, float _Angle)
        { }

        public virtual void Rotate(float _Angle)
        { }
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
        { Centre = new Vector2(); }

        /// <summary>
        /// Square constructor, takes in an array of <c>Vector2</c>s.
        /// </summary>
        public Square(Vector2[] _Corners)
        {
            if(_Corners.Length < 4 || _Corners.Length > 4)
            { throw new ArgumentException("Array must contain 4 values!"); }

            Centre = new Vector2
            (
                CornerA.X + (Width / 2),
                CornerA.Y + (Height / 2)
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

        public Square(Vector2 _Centre, int _Width, int _Height)
        {
            Centre = _Centre;
            Width = _Width;
            Height = _Height;

            CornerA = new Vector2(Centre.X - (Width / 2), Centre.Y - (Height / 2));
            CornerB = new Vector2(Centre.X + (Width / 2), Centre.Y - (Height / 2));
            CornerC = new Vector2(Centre.X + (Width / 2), Centre.Y + (Height / 2));
            CornerD = new Vector2(Centre.X - (Width / 2), Centre.Y + (Height / 2));
        }

        /// <summary>
        /// Renders the object using the inputted <c>Graphics</c> object.
        /// </summary>
        public override void Draw(Graphics G)
        {
            Point[] TempPointArr = new Point[]
            {
                V2Ext.ToPoint(CornerA), V2Ext.ToPoint(CornerB),
                V2Ext.ToPoint(CornerC), V2Ext.ToPoint(CornerD)
            };


            G.FillPolygon
            (
                new SolidBrush(PrimaryCol),
               TempPointArr
            );

            G.DrawPolygon
            (
                new Pen(SecondaryCol, BorderWidth),
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

        public override void MoveTransform(Vector2 V2)
        {
            Vector2 Delta = Vector2.Subtract(V2, Centre);

            CornerA += Delta;
            CornerB += Delta;
            CornerC += Delta;
            CornerD += Delta;
            Centre += Delta;
        }

        public override void SizeTransform(Vector2 V2)
        {
            Vector2 Delta = Vector2.Subtract(new Vector2(0, 0), Centre);

            Matrix3x2 Scaler = new Matrix3x2
            (
                V2.X / Width, 0,
                0, V2.Y / Height,
                0, 0
            );

            Width = (int)V2.X;
            Height = (int)V2.Y;

            CornerA = Vector2.Add(Delta, CornerA);
            CornerB = Vector2.Add(Delta, CornerB);
            CornerC = Vector2.Add(Delta, CornerC);
            CornerD = Vector2.Add(Delta, CornerD);

            CornerA = Vector2.Transform(CornerA, Scaler);
            CornerB = Vector2.Transform(CornerB, Scaler);
            CornerC = Vector2.Transform(CornerC, Scaler);
            CornerD = Vector2.Transform(CornerD, Scaler);

            CornerA = Vector2.Add(Centre, CornerA);
            CornerB = Vector2.Add(Centre, CornerB);
            CornerC = Vector2.Add(Centre, CornerC);
            CornerD = Vector2.Add(Centre, CornerD);
        }

        public override void Rotate(float _Radians)
        {
            Vector2 Delta = Vector2.Subtract(new Vector2(0, 0), Centre);

            float Cos = (float)Math.Cos(_Radians);
            float Sin = (float)Math.Sin(_Radians);

            Matrix3x2 Rotation = new Matrix3x2
            (
                Cos, -Sin,
                Sin, Cos,
                0, 0
            );

            CornerA = Vector2.Add(Delta, CornerA);
            CornerB = Vector2.Add(Delta, CornerB);
            CornerC = Vector2.Add(Delta, CornerC);
            CornerD = Vector2.Add(Delta, CornerD);

            CornerA = Vector2.Transform(CornerA, Rotation);
            CornerB = Vector2.Transform(CornerB, Rotation);
            CornerC = Vector2.Transform(CornerC, Rotation);
            CornerD = Vector2.Transform(CornerD, Rotation);

            CornerA = Vector2.Add(Centre, CornerA);
            CornerB = Vector2.Add(Centre, CornerB);
            CornerC = Vector2.Add(Centre, CornerC);
            CornerD = Vector2.Add(Centre, CornerD);
        }

        public override void Rotate(Vector2 _RotationPoint, float _Radians)
        {
            Vector2 Delta = Vector2.Subtract(new Vector2(0, 0), _RotationPoint);

            float Cos = (float)Math.Cos(_Radians);
            float Sin = (float)Math.Sin(_Radians);

            Matrix3x2 Rotation = new Matrix3x2
            (
                Cos, -Sin,
                Sin, Cos,
                0, 0
            );

            CornerA = Vector2.Add(Delta, CornerA);
            CornerB = Vector2.Add(Delta, CornerB);
            CornerC = Vector2.Add(Delta, CornerC);
            CornerD = Vector2.Add(Delta, CornerD);

            CornerA = Vector2.Transform(CornerA, Rotation);
            CornerB = Vector2.Transform(CornerB, Rotation);
            CornerC = Vector2.Transform(CornerC, Rotation);
            CornerD = Vector2.Transform(CornerD, Rotation);

            CornerA = Vector2.Add(_RotationPoint, CornerA);
            CornerB = Vector2.Add(_RotationPoint, CornerB);
            CornerC = Vector2.Add(_RotationPoint, CornerC);
            CornerD = Vector2.Add(_RotationPoint, CornerD);
        }
    }
}
