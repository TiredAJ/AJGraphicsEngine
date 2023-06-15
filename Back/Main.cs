using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace BasicGraphicsEngine
{
    public partial class frm_Main : Form
    {
        private Drawer DrawerHandler = new Drawer();
        private bool Run = false, FirstTime = true;
        private TimeSpan FrameTime;
        private Thread? TDrawer;
        private double MSPerFrame = 16.66666667;

        public frm_Main()
        { InitializeComponent(); }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            DrawerHandler.Init
            (new Vector2
                (
                        pbx_DisplayCanvas.DisplayRectangle.Width,
                        pbx_DisplayCanvas.DisplayRectangle.Height
            ));

            Task.Run(() =>
            {
                pbx_DisplayCanvas.Invalidate();
                pbx_DisplayCanvas.BackColor = DrawerHandler.CanvasColour;
            });

            DrawerHandler.ControlsAreaSize = new Vector2(flp_FlowPanel.Width, flp_FlowPanel.Height);
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            btn_Reset.Enabled = true;
            btn_Pause.Enabled = true;
            btn_Start.Enabled = false;

            DrawerHandler.CleanUp();

            DrawerHandler.SetUp();

            Run = true;

            StartTDrawer();
        }

        private void StartTDrawer()
        {
            DrawerHandler.Running = true;

            MSPerFrame = 1000d / DrawerHandler.TargetFPS;

            Debug.WriteLine(MSPerFrame);

            TDrawer = new Thread(Refresher);
            TDrawer.Start();
        }

        private void Refresher()
        {
            Stopwatch SW = new Stopwatch();
            SW.Start();

            do
            {
                SW.Restart();

                pbx_DisplayCanvas.Invoke(new Action(() =>
                {
                    pbx_DisplayCanvas.Invalidate();
                    pbx_DisplayCanvas.BackColor = DrawerHandler.CanvasColour;
                }));

                FrameTime = SW.Elapsed;

                lblFrameTime.Invoke(new Action(() =>
                { lblFrameTime.Text = $"{FrameTime.TotalMilliseconds}ms"; }));

                Task.Run(() =>
                { Debug.WriteLine(MSPerFrame - FrameTime.TotalMilliseconds); });

                if(FrameTime.TotalMilliseconds < MSPerFrame)
                { Thread.Sleep((int)(MSPerFrame - FrameTime.TotalMilliseconds)); }

            } while(Run);
        }

        private void btn_Pause_Click(object sender, EventArgs e)
        {
            if(Run)
            {
                Run = false;
                DrawerHandler.Running = false;

                btn_Pause.Text = "Play";
            }
            else
            {
                Run = true;
                DrawerHandler.Running = true;

                btn_Pause.Text = "Pause";

                StartTDrawer();
            }
        }

        private void pbx_DisplayCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if(FirstTime)
            {
                DrawerHandler.InitDraw(e.Graphics);
                FirstTime = false;
                LoadControls();
            }
            else
            { DrawerHandler.CallDraw(e.Graphics); }
        }

        private void frm_Main_Resize(object sender, EventArgs e)
        {
            bool FullScreen;

            if(WindowState == FormWindowState.Maximized)
            { FullScreen = true; }
            else
            { FullScreen = false; }

            DrawerHandler.MainDisplayEvent
            (
                new Vector2
                (
                    pbx_DisplayCanvas.DisplayRectangle.Width,
                    pbx_DisplayCanvas.DisplayRectangle.Height
                ),
                FullScreen
            );
        }

        private void frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        { DrawerHandler.CleanUp(); }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            DrawerHandler.Running = false;

            btn_Pause.Enabled = false;
            btn_Start.Enabled = true;

            DrawerHandler.CleanUp();

            Run = false;

            Task.Run(() =>
            {
                pbx_DisplayCanvas.Invalidate();
                pbx_DisplayCanvas.BackColor = DrawerHandler.CanvasColour;
            });
        }

        private void LoadControls()
        {
            foreach(Control CTRL in DrawerHandler.Controls)
            { flp_FlowPanel.Controls.Add(CTRL); }
        }

        private void MouseDownFnc(object sender, MouseEventArgs e)
        {
            Task.Run(new Action(() =>
            {
                DrawerHandler.MainCursorEvent(e, CState.Down);
            }));
        }

        private void MouseUpFnc(object sender, MouseEventArgs e)
        {
            if(Run)
            {
                Task.Run(new Action(() =>
                {
                    DrawerHandler.MainCursorEvent(e, CState.Up);
                }));
            }
        }

        private void MouseMoveFnc(object sender, MouseEventArgs e)
        {
            if(Run)
            {
                Task.Run(new Action(() =>
                {
                    DrawerHandler.MainCursorEvent(e, CState.NoChange);
                }));
            }
        }

        private void MouseDoubleClickFnc(object sender, MouseEventArgs e)
        {
            if(Run)
            {
                Task.Run(new Action(() =>
                {
                    DrawerHandler.MainCursorEvent(e, CState.DoubleClick);
                }));
            }
        }
    }
}