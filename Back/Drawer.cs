using System.Diagnostics;
using System.Numerics;

namespace BasicGraphicsEngine
{
    /// <summary>
    /// Class <c>Drawer</c> handles all the drawing stuff.
    /// </summary>
    public partial class Drawer
    {
        private List<DrawObject> ShapeList = new List<DrawObject>();
        public List<Control> Controls = new List<Control>();
        public Rectangle Display = new Rectangle();
        public Color CanvasColour = Color.White;
        public Vector2 Cursor = new Vector2(0, 0);
        public Vector2 DisplayCentre = new Vector2();
        public Vector2 ControlsAreaSize = new Vector2();
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

        public void AddControl(Control _Control)
        { Controls.Add(_Control); }

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
        {
            ShapeList.Clear();
            Controls.Clear();
        }

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
        { Cursor = V2Ext.ToVector(_Loc); }

        public float DegToRad(float _Deg)
        { return (float)(_Deg * (Math.PI / 180)); }

    }

    struct V2Ext
    {
        /// <summary>
        /// Method <c>ToVector</c> converts a Point to Vector2
        /// </summary>
        public static Vector2 ToVector(Point _P)
        => new Vector2(_P.X, _P.Y);

        /// <summary>
        /// Method <c>GetMagnitude</c> returns a float representing the magnitude of the vector
        /// </summary>
        public static float GetMagnitude(Vector2 V2)
        { return MathF.Sqrt(((float)V2.X * (float)V2.X) + ((float)V2.Y * (float)V2.Y)); }

        public static float GetMagnitude(Vector2 V2A, Vector2 V2B)
        {
            Vector2 AB = new Vector2(V2A.X - V2B.X, V2A.Y - V2B.Y);

            return MathF.Sqrt(((float)AB.X * (float)AB.X) + ((float)AB.Y * (float)AB.Y));
        }

        /// <summary>
        /// Method <c>GetMagnitudeSQ</c> returns a float representing the squared magnitude of the vector
        /// </summary>
        /// <returns></returns>
        public static float GetMagnitudeSQ(Vector2 V2)
        { return (((float)V2.X * (float)V2.X) + ((float)V2.Y * (float)V2.Y)); }

        /// <summary>
        /// Method <c>Normalise</c> Normalises based off a scalar value.
        /// </summary>
        public static Vector2 Normalise(Vector2 V2, float _Scalar)
        {
            if(_Scalar > 0)
            { return new Vector2((V2.X / _Scalar), (V2.Y / _Scalar)); }
            throw new DivideByZeroException();
        }

        /// <summary>
        /// Method <c>ToPoint</c> returns a point representing the vector.
        /// </summary>
        public static Point ToPoint(Vector2 V2)
        => new Point((int)V2.X, (int)V2.Y);

        /// <summary>
        /// Method <c>ToV2</c> returns a Vector2 representing the Point.
        /// </summary>
        public static Vector2 ToV2(Point V2)
        => new Vector2(V2.X, V2.Y);

        /// <summary>
        /// Method <c>ToPointArray</c> returns an array of points from an array of vectors.
        /// </summary>
        public static Point[] ToPointArray(Vector2[] _V2Arr)
        {
            List<Point> Temp = new List<Point>();

            foreach(Vector2 V2 in _V2Arr)
            { Temp.Add(ToPoint(V2)); }

            return Temp.ToArray();
        }

        /// <summary>
        /// Method <c>ToPointArray</c> returns an array of points from a list of vectors.
        /// </summary>
        public static Point[] ToPointArray(List<Vector2> _V2List)
        {
            List<Point> Temp = new List<Point>();

            foreach(Vector2 V2 in _V2List)
            { Temp.Add(ToPoint(V2)); }

            return Temp.ToArray();
        }

        public static Vector2 Limit(Vector2 V2, float Max)
        {
            if(GetMagnitudeSQ(V2) > (Max * Max))
            {
                V2 = Vector2.Divide(V2, GetMagnitude(V2));

                V2 = Vector2.Multiply(V2, Max);
            }
            return V2;
        }
    }
}
