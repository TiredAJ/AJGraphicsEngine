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
        public Color CanvasColour = Color.White;
        public Vector2 DisplaySize = new Vector2();
        public Vector2 DisplayCentre = new Vector2();
        public Vector2 CursorPos = new Vector2(0, 0);
        public Vector2 ControlsAreaSize = new Vector2();
        public event EventHandler<CursorEventArgs>? CursorEvent;
        public event EventHandler<DisplayEventArgs>? DisplayEvent;
        public bool Running = false;
        public int TargetFPS = 60;
        //public bool ResetCanvasOnFrame = true; <- to implement


        public Drawer()
        {
            
        }

        /// <summary>
        /// Sets up the display area.
        /// </summary>
        public void Init(Vector2 _Display)
        {
            DisplaySize = _Display;
            DisplayCentre.X = _Display.X / 2;
            DisplayCentre.Y = _Display.Y / 2;
        }

        /// <summary>
        /// Adds an object to the draw list.
        /// </summary>
        public void AddShape(DrawObject _NewShape)
        { ShapeList.Add(_NewShape); }

        /// <summary>
        /// Removes an object from the draw list.
        /// </summary>
        public void Remove(DrawObject _NewShape)
        { ShapeList.Remove(_NewShape); }

        /// <summary>
        /// Adds an array of objects to the draw list.
        /// </summary>
        public void AddShapes(DrawObject[] _NewShapes)
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
            Running = true;

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
        public int RandomValue(int Min, int Max, bool IncludeZero)
        {
            int Temp = 0;

            Random R = new Random((int)DateTime.Now.Ticks);

            R.Next(Min, Max);

            if(!IncludeZero)
            {
                while(Temp == 0)
                { Temp = R.Next(Min, Max); }
            }

            return Temp;
        }

        /// <summary>
        /// Method <c>RandomValue</c> Returns a random value between two integers. (includes 0)
        /// </summary>
        public int RandomValue(int Min, int Max)
        {
            Random R = new Random((int)DateTime.Now.Ticks);

            return R.Next(Min, Max);
        }

        public float DegToRad(float _Deg)
        { return (float)(_Deg * (Math.PI / 180)); }

        public void MainCursorEvent(MouseEventArgs Me, CState _State)
        {
            if(Running)
            {
                CursorPos = V2Ext.ToV2(Me.Location);
                CursorEvent?.Invoke(this, new CursorEventArgs(Me, _State));
            }
        }

        public void MainDisplayEvent(Vector2 _DisplaySize, bool _FullScreen)
        {
            if(Running)
            {
                DisplaySize = _DisplaySize;
                DisplayCentre = new Vector2(DisplaySize.X / 2, DisplaySize.Y / 2);
                DisplayEvent?.Invoke(this, new DisplayEventArgs(_DisplaySize, _FullScreen));
            }
        }
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
        /// Returns an array of points from a list of vectors.
        /// </summary>
        public static Point[] ToPointArray(List<Vector2> _V2List)
        {
            List<Point> Temp = new List<Point>();

            foreach(Vector2 V2 in _V2List)
            { Temp.Add(ToPoint(V2)); }

            return Temp.ToArray();
        }

        /// <summary>
        /// Limits a vector to a maximum magnitude
        /// </summary>
        /// <param name="V2">Vector to limit</param>
        /// <param name="Max">Maximum magnitude</param>
        /// <returns>A vector who's magnitude <= Max</returns>
        public static Vector2 Limit(Vector2 V2, float Max)
        {
            if(GetMagnitudeSQ(V2) > (Max * Max))
            {
                V2 = Vector2.Divide(V2, GetMagnitude(V2));

                V2 = Vector2.Multiply(V2, Max);
            }
            return V2;
        }

        /// <summary>
        /// Ensures V2's components are no larger than Max's
        /// </summary>
        /// <param name="V2">Vector to limit</param>
        /// <param name="Max">Vector of maximum components</param>
        /// <returns>A Vector who's components are <= Max's</returns>
        public static Vector2 Limit(Vector2 V2, Vector2 Max)
        {
            if(V2.X > Max.X)
            { V2.X = Max.X; }
            if(V2.Y > Max.Y)
            { V2.Y = Max.Y; }

            return V2;
        }

        /// <summary>
        /// Ensures V2's components are within Max's and Min's components
        /// </summary>
        /// <param name="V2">Vector to limit</param>
        /// <param name="Max">Vector of maximum components</param>
        /// <param name="Min">Vector of minimum components</param>
        /// <returns>A Vector who's components are <= Max's and >= Min's</returns>
        public static Vector2 Limit(Vector2 V2, Vector2 Max, Vector2 Min)
        {
            if(V2.X > Max.X)
            { V2.X = Max.X; }
            else if(V2.X < Min.X)
            { V2.X = Min.X; }

            if(V2.Y > Max.Y)
            { V2.Y = Max.Y; }
            else if(V2.Y < Min.Y)
            { V2.Y = Min.Y; }

            return V2;
        }

        /// <summary>
        /// Offsets a vector by a specific amount
        /// </summary>
        /// <param name="V2">Vector to offset</param>
        /// <param name="XOff">X offset</param>
        /// <param name="YOff">Y offset</param>
        /// <returns>A vector offsetted by XOff and YOff</returns>
        public static Vector2 Offset(Vector2 V2, int XOff, int YOff)
        => new Vector2(V2.X + XOff, V2.Y + YOff);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Point"></param>
        /// <param name="Destination"></param>
        /// <returns></returns>
        public static Vector2 MoveTransform(Vector2 Point, Vector2 Destination)
        {
            Vector2 Delta = Vector2.Subtract(Destination, Point);

            Point += Delta;

            return Point;
        }
    }

    public class CursorEventArgs : EventArgs
    {
        public CState State { get; private set; }
        public Vector2 Location { get; private set; }
        public MouseButtons CButton { get; private set; }

        public CursorEventArgs() 
        {
            State = CState.NoChange;
            Location = new Vector2(0,0);
            CButton = MouseButtons.None;
        }

        public CursorEventArgs(MouseEventArgs Me, CState _State)
        {
            State = _State;
            Location = V2Ext.ToV2(Me.Location);
            CButton = Me.Button;
        }

    }

    public enum CState
    {
        Up,
        Down,
        NoChange,
        DoubleClick
    }

    public class DisplayEventArgs : EventArgs
    {
        public Vector2 DisplaySize { get; private set; }
        public bool FullScreen { get; private set; }

        public DisplayEventArgs() { }

        public DisplayEventArgs(Vector2 _DisplaySize, bool _FullScreen)
        {
            DisplaySize = _DisplaySize;
            FullScreen = _FullScreen;
        }
    }
}
