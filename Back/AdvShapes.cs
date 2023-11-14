using System.Numerics;

namespace BasicGraphicsEngine
{
    //moved here to prevent ambigous errors with this newfound vector extension methods in
    //System.Numerics.VectorExtensions
    using BasicGraphicsEngine.Back.Extensions;

    /// <summary>
    /// Base object for all drawn objects. Do not use directly.
    /// </summary>
    public abstract class DrawObject
    {
        public Color PrimaryCol = Color.Red;
        public Vector2 DisplayCentre = new Vector2();
        public object? Tag = null;

        /// <summary>
        /// Method <c>Draw</c> renders the object using the inputted Graphics object.
        /// </summary>
        public abstract void Draw(Graphics G);

        public override string ToString()
        {return $"Primary Col:{PrimaryCol}, DisplayCentre:{DisplayCentre}";}
    }

    /// <summary>
    /// Base object for lines. Do not use directly.
    /// </summary>
    public abstract class LineBase : DrawObject
    {
        public float LineWidth = 5f;

        public abstract void MoveTransform(Vector2 V2);
        public abstract void Rotate(Vector2 _RotationPoint, float _Radians);
        public abstract void Rotate(float _Radians);
        public abstract Vector2 CalculateCentre();

        public override string ToString()
        {return base.ToString() + $", LineWidth:{LineWidth}";}
    }

    /// <summary>
    /// For drawing basic 2-point lines
    /// </summary>
    public class Line : LineBase
    {
        public Vector2 A, B;
        public Vector2 Centre { get; private set; }

        public Line()
        {
            A = new Vector2(0, 0);
            B = new Vector2(0, 0);
        }

        /// <summary>
        /// Constructor that takes in two <c>Vectors</c> for points.
        /// </summary>
        public Line(Vector2 _A, Vector2 _B)
        {
            A = _A;
            B = _B;

            CalculateCentre();
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

            CalculateCentre();
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

        public override void MoveTransform(Vector2 V2)
        {
            Centre = CalculateCentre();

            Vector2 Delta = Vector2.Subtract(V2, Centre);

            A += Delta;
            B += Delta;
            Centre += Delta;
        }

        public override void Rotate(Vector2 _RotationPoint, float _Radians)
        {
            Centre = CalculateCentre();
            Vector2 Delta = Vector2.Subtract(new Vector2(0, 0), _RotationPoint);

            float Cos = (float)Math.Cos(_Radians);
            float Sin = (float)Math.Sin(_Radians);

            Matrix3x2 Rotation = new Matrix3x2
            (
                Cos, -Sin,
                Sin, Cos,
                0, 0
            );

            A = Vector2.Add(Delta, A);
            B = Vector2.Add(Delta, B);
            Centre = Vector2.Add(Delta, Centre);

            A = Vector2.Transform(A, Rotation);
            B = Vector2.Transform(B, Rotation);
            Centre = Vector2.Transform(Centre, Rotation);

            A = Vector2.Add(_RotationPoint, A);
            B = Vector2.Add(_RotationPoint, B);
            Centre = Vector2.Add(_RotationPoint, Centre);
        }

        public override void Rotate(float _Radians)
        {
            Centre = CalculateCentre();
            Vector2 Delta = Vector2.Subtract(new Vector2(0, 0), Centre);

            float Cos = (float)Math.Cos(_Radians);
            float Sin = (float)Math.Sin(_Radians);

            Matrix3x2 Rotation = new Matrix3x2
            (
                Cos, -Sin,
                Sin, Cos,
                0, 0
            );

            A = Vector2.Add(Delta, A);
            B = Vector2.Add(Delta, B);

            A = Vector2.Transform(A, Rotation);
            B = Vector2.Transform(B, Rotation);

            A = Vector2.Add(Centre, A);
            B = Vector2.Add(Centre, B);
        }

        public override Vector2 CalculateCentre()
        {
            if(A.Length() != 0 && B.Length() != 0)
            { return new Vector2((A.X + B.X) / 2, (A.Y + B.Y) / 2); }
            else
            { throw new DivideByZeroException(); }
        }
    }

    /// <summary>
    /// For drawing a multi-point line.
    /// </summary>
    public class Lines : LineBase
    {
        public List<Vector2> Points = new List<Vector2>();
        public Vector2 Centre { get; private set; }

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
            if(Points.Count > 1)
            {
                G.DrawLines
                (
                    new Pen(PrimaryCol, LineWidth),
                    Points.ToPointArray()
                );
            }
        }

        public void AddPoint(Vector2 NewPoint, bool Back)
        {
            if(Back)
            { Points.Add(NewPoint); }
            else
            { Points.Prepend(NewPoint); }
        }

        public override void MoveTransform(Vector2 V2)
        {
            Centre = CalculateCentre();

            Vector2 Delta = Vector2.Subtract(V2, Centre);

            Vector2 TempPoint;

            foreach(Vector2 V2P in Points)
            {
                TempPoint = V2P;
                TempPoint += Delta;
            }

            Centre += Delta;
        }

        public override void Rotate(Vector2 _RotationPoint, float _Radians)
        {
            Centre = CalculateCentre();

            Vector2 Delta = Vector2.Subtract(new Vector2(0, 0), _RotationPoint);

            float Cos = (float)Math.Cos(_Radians);
            float Sin = (float)Math.Sin(_Radians);

            Matrix3x2 Rotation = new Matrix3x2
            (
                Cos, -Sin,
                Sin, Cos,
                0, 0
            );

            Vector2 TempPoint;

            foreach(Vector2 V2P in Points)
            {
                TempPoint = V2P;
                TempPoint = Vector2.Add(Delta, TempPoint);
                TempPoint = Vector2.Transform(TempPoint, Rotation);
                TempPoint = Vector2.Add(_RotationPoint, TempPoint);
            }

            Centre = Vector2.Add(Delta, Centre);
            Centre = Vector2.Transform(Centre, Rotation);
            Centre = Vector2.Add(_RotationPoint, Centre);
        }

        public override void Rotate(float _Radians)
        {
            Centre = CalculateCentre();

            Vector2 Delta = Vector2.Subtract(new Vector2(0, 0), Centre);

            float Cos = (float)Math.Cos(_Radians);
            float Sin = (float)Math.Sin(_Radians);

            Matrix3x2 Rotation = new Matrix3x2
            (
                Cos, -Sin,
                Sin, Cos,
                0, 0
            );

            Vector2 TempPoint;

            foreach(Vector2 V2P in Points)
            {
                TempPoint = V2P;
                TempPoint = Vector2.Add(Delta, TempPoint);
                TempPoint = Vector2.Transform(TempPoint, Rotation);
                TempPoint = Vector2.Add(Centre, TempPoint);
            }
        }

        public override Vector2 CalculateCentre()
        {
            if(Points.Count <= 1)
            { throw new Exception("Not enough Points"); }

            Vector2 Average = new Vector2(0, 0);

            foreach(Vector2 V2 in Points)
            {
                Average.X += V2.X;
                Average.Y += V2.Y;
            }

            Average.X /= Points.Count();
            Average.Y /= Points.Count();

            return Average;
        }
    }

    /// <summary>
    /// Base object for all drawn objects. Do not use directly.
    /// </summary>
    public abstract class AdvShapes : DrawObject
    {
        public float BorderWidth = 5f;
        public Vector2 Centre { get; protected set; } = new Vector2();
        public Color SecondaryCol = Color.Green;
        public Color TertiaryCol = Color.Transparent;

        public abstract void MoveTransform(Vector2 V2);
        public abstract void SizeTransform(Vector2 V2);
        public abstract void Rotate(Vector2 _RotationPoint, float _Angle);
        public abstract void Rotate(float _Angle);
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
                CornerA.ToPoint(), CornerB.ToPoint(),
                CornerC.ToPoint(), CornerD.ToPoint()
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
            Centre = Vector2.Add(Delta, Centre);

            CornerA = Vector2.Transform(CornerA, Rotation);
            CornerB = Vector2.Transform(CornerB, Rotation);
            CornerC = Vector2.Transform(CornerC, Rotation);
            CornerD = Vector2.Transform(CornerD, Rotation);
            Centre = Vector2.Transform(Centre, Rotation);

            CornerA = Vector2.Add(_RotationPoint, CornerA);
            CornerB = Vector2.Add(_RotationPoint, CornerB);
            CornerC = Vector2.Add(_RotationPoint, CornerC);
            CornerD = Vector2.Add(_RotationPoint, CornerD);
            Centre = Vector2.Add(_RotationPoint, Centre);
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
    }
}
