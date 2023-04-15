using System.Numerics;

namespace BasicGraphicsEngine
{
    public partial class frm_Main : Form
    {
        private Drawer DrawerHandler = new Drawer();
        private bool Run = false, FirstTime = true;
        private TimeSpan Start, End, LastTime = new TimeSpan(), Delta;
        private Thread? TDrawer;

        public frm_Main()
        { InitializeComponent(); }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            DrawerHandler.Init
            (new Rectangle
                (
                    pbx_DisplayCanvas.DisplayRectangle.Location,
                new Size
                    (
                        pbx_DisplayCanvas.DisplayRectangle.Width,
                        pbx_DisplayCanvas.DisplayRectangle.Height
                    )
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

            TDrawer = new Thread(Refresher);
            TDrawer.Start();
        }

        private void Refresher()
        {
            do
            {
                Start = DateTime.Now.TimeOfDay;

                Task.Run(() =>
                {
                    pbx_DisplayCanvas.Invoke(new Action(() =>
                    {
                        pbx_DisplayCanvas.Invalidate();
                        pbx_DisplayCanvas.BackColor = DrawerHandler.CanvasColour;
                    }));
                });

                End = DateTime.Now.TimeOfDay;
                Delta = (End - Start) - LastTime;
                LastTime = End - Start;

                Task.Run(() =>
                {
                    lblFrameTime.Invoke(new Action(() =>
                    {
                        lblFrameTime.Text = $"{LastTime.TotalMilliseconds}ms";
                    }));
                });

                Thread.Sleep(1);

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
            DrawerHandler.ResizeCanvas
                (new Rectangle
                    (
                        pbx_DisplayCanvas.DisplayRectangle.Location,
                    new Size
                        (
                            pbx_DisplayCanvas.DisplayRectangle.Width,
                            pbx_DisplayCanvas.DisplayRectangle.Height
                        )
                ));
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