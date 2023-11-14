using System.Diagnostics;
using System.Numerics;
using BasicGraphicsEngine.Back.Extensions;
using BasicGraphicsEngine.Back.Utilities;

namespace BasicGraphicsEngine
{
    /// <summary>
    /// Handles all the drawing stuff.
    /// </summary>
    public partial class Drawer
    {
        private List<DrawObject> ShapeList = new List<DrawObject>();
        private Logger InitLog = new Logger("Init took");
        private Logger GFrameLog = new Logger("Graphics Frame took");
        private Logger PFrameLog = new Logger("Physics Frame took");
        private Logger DFrameLog = new Logger("Draw Frame took");

        private Graphics[] GArr = new Graphics[8];


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
        public void AddShape(DrawObject[] _NewShapes)
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

#if DEBUG
            GFrameLog.Start();
#endif
            GraphicsFrame();

#if DEBUG
            GFrameLog.Stop();
#endif


#if DEBUG
            DFrameLog.Start();
#endif

            foreach (DrawObject S in ShapeList)
            { S.Draw(G); }

#if DEBUG
            DFrameLog.Stop();
#endif
        }

        public void CallPhysics()
        {
#if DEBUG
            PFrameLog.Start();
#endif
            PhysicsFrame();

#if DEBUG
            PFrameLog.Stop();
#endif
        }

        /// <summary>
        /// Draws the draw list once.
        /// </summary>
        public void InitDraw(Graphics G)
        {
            SetUp();

#if DEBUG
            InitLog.Start();
#endif
            foreach(DrawObject S in ShapeList)
            { S.Draw(G); }

#if DEBUG
            InitLog.Stop();
#endif

            ShapeList.Clear();
        }

        /// <summary>
        /// Cleans up ¯\_(ツ)_/¯.
        /// </summary>
        public void CleanUp()
        {
            ShapeList.Clear();
            Controls.Clear();
        }

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

        public void GetAverageFrameTime()
        {
            Debug.WriteLine($"Average Draw Frametime:     {DFrameLog.Average}ms");
            Debug.WriteLine($"Average Graphics Frametime: {GFrameLog.Average}ms");
            Debug.WriteLine($"Average Physics Frametime:  {PFrameLog.Average}ms");
        }
    }    
}