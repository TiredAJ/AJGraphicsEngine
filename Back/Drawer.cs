using BasicGraphicsEngine.Back.Extensions;
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
                CursorPos = Me.Location.ToV2();
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
            Location = Me.Location.ToV2();
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