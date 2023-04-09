using System.Diagnostics;

namespace BasicGraphicsEngine
{
    /// <summary>
    /// Class <c>Drawer</c> handles all the drawing stuff.
    /// </summary>
    public partial class Drawer
    {
        private List<DrawObject> ShapeList = new List<DrawObject>();
        public Rectangle Display = new Rectangle();
        public Color CanvasColour = Color.White;
        public Vector2 Cursor = new Vector2(0, 0);
        public Vector2 DisplayCentre = new Vector2();
        //public bool ResetCanvasOnFrame = true; <- to implement

        public Drawer()
        { }

        /// <summary>
        /// Sets up the display area.
        /// </summary>
        public void Init(Rectangle _Display)
        {
            Display = _Display;
            DisplayCentre.X = _Display.Width / 2;
            DisplayCentre.Y = _Display.Height / 2;
        }

        public void ResizeCanvas(Rectangle _Display)
        {
            Display = _Display;
            DisplayCentre.X = _Display.Width / 2;
            DisplayCentre.Y = _Display.Height / 2;

            Resize();
        }

        /// <summary>
        /// Adds an object to the draw list.
        /// </summary>
        public void Add(DrawObject _NewShape)
        {
            ShapeList.Add(_NewShape);

            Task.Run(new Action(() =>
            {
                Debug.WriteLine(ShapeList.Count());
            }));
        }

        /// <summary>
        /// Adds an array of objects to the draw list.
        /// </summary>
        public void Add(DrawObject[] _NewShapes)
        {
            foreach(DrawObject DO in _NewShapes)
            { ShapeList.Add(DO); }
        }

        /// <summary>
        /// Method <c>CallDraw</c> Goes through the draw list and draws.
        /// </summary>
        public void CallDraw(Graphics G)
        {
            Frame();

            foreach(DrawObject S in ShapeList)
            { S.Draw(G); }
        }

        public int Map(int _Val, int _InMax, int InMin, int _OutMax, int _OutMin)
        { return (_Val - InMin) * (_OutMax - _OutMin) / (_InMax - InMin) + _OutMin; }

        /// <summary>
        /// Method <c>InitDraw</c> Draws the draw list once.
        /// </summary>
        public void InitDraw(Graphics G)
        {
            SetUp();

            foreach(DrawObject S in ShapeList)
            { S.Draw(G); }

            ShapeList.Clear();
        }

        /// <summary>
        /// Method <c>CleanUp</c> Cleans up ¯\_(ツ)_/¯.
        /// </summary>
        public void CleanUp()
        { ShapeList.Clear(); }

        /// <summary>
        /// Method <c>RandomValue</c> Returns a random value between two integers.
        /// </summary>
        public static int RandomValue(int Min, int Max, bool IncludeZero)
        {
            int Temp = 0;

            Random R = new Random(DateTime.Now.Microsecond);

            R.Next(Min, Max);

            if(!IncludeZero)
            {
                while(Temp == 0)
                { Temp = R.Next(Min, Max); }
            }

            return Temp;
        }

        /// <summary>
        /// Method <c>SetCursorPos</c> Sets the position of the cursor.
        /// </summary>
        public void SetCursorPos(Point _Loc)
        { Cursor = (Vector2)_Loc; }

        public float DegToRad(float _Deg)
        { return (float)(_Deg * (Math.PI / 180)); }
    }

    //public struct ShapeColours
    //{
    //    Color Primary;
    //    Color Secondary;
    //    Color Tertiary;
    //    Color Border;
    //}

    /// <summary>
    /// Class <c>Vector2</c> A class representing a 2D Vector datatype.
    /// </summary>
    public struct Vector2
    {
        public float? X, Y;

        public Vector2()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Method <c>Vector2</c> Constructor that takes two integers for X and Y.
        /// </summary>
        public Vector2(float? _X, float? _Y)
        {
            X = _X;
            Y = _Y;
        }

        /// <summary>
        /// Method <c>Vector2</c> Constructor that takes a pre-existing Vector2 object.
        /// </summary>
        public Vector2(Vector2 _V)
        {
            X = _V.X;
            Y = _V.Y;
        }

        /// <summary>
        /// Method <c>Vector2</c> Constructor that takes a point object.
        /// </summary>
        public Vector2(Point _P)
        {
            X = _P.X;
            Y = _P.Y;
        }

        /// <summary>
        /// Method <c>GetMagnitude</c> returns a float representing the magnitude of the vector
        /// </summary>
        public float GetMagnitude()
        { return MathF.Sqrt(((float)X * (float)X) + ((float)Y * (float)Y)); }

        /// <summary>
        /// Method <c>GetMagnitudeSQ</c> returns a float representing the squared magnitude of the vector
        /// </summary>
        /// <returns></returns>
        public float GetMagnitudeSQ()
        { return (((float)X * (float)X) + ((float)Y * (float)Y)); }

        /// <summary>
        /// Method <c>Normalise</c> Normalises the vector.
        /// </summary>
        public void NormaliseVoid()
        {
            float Temp = GetMagnitude();

            if(Temp != 0)
            {
                X = X / Temp;
                Y = Y / Temp;
            }
            else
            { throw new DivideByZeroException(); }
        }

        /// <summary>
        /// Method <c>Normalise</c> Normalises the vector.
        /// </summary>
        public Vector2 NormaliseS()
        {
            float Temp = GetMagnitude();

            if(Temp != 0)
            { return new Vector2((X / Temp), (Y / Temp)); }
            throw new DivideByZeroException();
        }

        /// <summary>
        /// Method <c>Normalise</c> Normalises based off a scalar value.
        /// </summary>
        public Vector2 Normalise(int _Scalar)
        {
            if(_Scalar > 0)
            { return new Vector2((X / _Scalar), (Y / _Scalar)); }
            throw new DivideByZeroException();
        }

        /// <summary>
        /// Method <c>Normalise</c> Normalises based off a scalar value.
        /// </summary>
        public Vector2 Normalise(float _Scalar)
        {
            if(_Scalar > 0)
            { return new Vector2((X / _Scalar), (Y / _Scalar)); }
            throw new DivideByZeroException();
        }

        public bool IsNull()
        {
            if(X == null || Y == null) { return true; }
            else { return true; }
        }

        /// <summary>
        /// Method <c>Dot</c> returns the dot product of two vectors.
        /// </summary>
        public static float Dot(Vector2 _VA, Vector2 _VB)
        { return ((float)_VA.X * (float)_VB.X) + ((float)_VA.Y * (float)_VB.Y); }

        public void Limit(float Max)
        {
            if(GetMagnitudeSQ() > (Max * Max))
            {
                this = Vector2.Div(this, GetMagnitude());

                this = Vector2.Multi(this, Max);
            }
        }

        /// <summary>
        /// Method <c>ToString()</c> returns a string of the vector.
        /// </summary>
        public override string ToString()
        { return $"X:{X}, Y:{Y}"; }

        /// <summary>
        /// Method <c>ToPoint</c> returns a point representing the vector.
        /// </summary>
        public Point ToPoint()
        {
            Point Temp = new Point();

            if(X == null)
            { Temp.X = 0; }
            else
            { Temp.X = (int)X; }

            if(Y == null)
            { Temp.Y = 0; }
            else
            { Temp.Y = (int)Y; }

            return Temp;
        }

        /// <summary>
        /// Method <c>ToPointArray</c> returns an array of points from an array of vectors.
        /// </summary>
        public static Point[] ToPointArray(Vector2[] _V2Arr)
        {
            List<Point> Temp = new List<Point>();
            for(int i = 0; i < _V2Arr.Length; i++)
            { Temp.Add(_V2Arr[i].ToPoint()); }

            return Temp.ToArray();
        }

        /// <summary>
        /// Method <c>ToPointArray</c> returns an array of points from a list of vectors.
        /// </summary>
        public static Point[] ToPointArray(List<Vector2> _V2List)
        {
            List<Point> Temp = new List<Point>();

            foreach(Vector2 V2 in _V2List)
            { Temp.Add(V2.ToPoint()); }

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

        public static Vector2 Add(Vector2 Va, Vector2 Vb)
        => new Vector2
        (
            Va.X + Vb.X,
            Va.Y + Vb.Y
        );

        public static Vector2 Add(Vector2 Va, int Scalar)
        => new Vector2(Va.X + Scalar, Va.Y);

        public static Vector2 operator -(Vector2 Va, Vector2 Vb)
        => new Vector2
        (
            Va.X - Vb.X,
            Va.Y - Vb.Y
        );

        public static Vector2 operator -(Vector2 Va, int Scalar)
        => new Vector2(Va.X - Scalar, Va.Y - Scalar);

        public static Vector2 Sub(Vector2 Va, Vector2 Vb)
        => new Vector2
        (
            Va.X - Vb.X,
            Va.Y - Vb.Y
        );

        public static Vector2 Sub(Vector2 Va, int Scalar)
        => new Vector2(Va.X - Scalar, Va.Y - Scalar);

        public static Vector2 operator *(Vector2 Va, int Scalar)
        => new Vector2(Va.X * Scalar, Va.Y * Scalar);

        public static Vector2 operator *(Vector2 Va, float Scalar)
        => new Vector2(Va.X * Scalar, Va.Y * Scalar);

        public static Vector2 Multi(Vector2 Va, int Scalar)
        => new Vector2(Va.X * Scalar, Va.Y * Scalar);

        public static Vector2 Multi(Vector2 Va, float Scalar)
        => new Vector2(Va.X * Scalar, Va.Y * Scalar);

        public static Vector2 operator /(Vector2 Va, int Scalar)
        => new Vector2(Va.X / Scalar, Va.Y / Scalar);

        public static Vector2 operator /(Vector2 Va, float Scalar)
        => new Vector2(Va.X / Scalar, Va.Y / Scalar);

        public static Vector2 Div(Vector2 Va, int Scalar)
        => new Vector2(Va.X / Scalar, Va.Y / Scalar);

        public static Vector2 Div(Vector2 Va, float Scalar)
        => new Vector2(Va.X / Scalar, Va.Y / Scalar);

        public static explicit operator Vector2(Point _P) => new Vector2(_P);
    }
}
