using System.Diagnostics;

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
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            btn_Reset.Enabled = true;
            btn_Stop.Enabled = true;
            btn_Start.Enabled = false;

            DrawerHandler.CleanUp();

            DrawerHandler.SetUp();

            Run = true;

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

                Thread.Sleep(1);

            } while(Run);
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            Run = false;

            btn_Reset.Enabled = false;
            btn_Stop.Enabled = false;
            btn_Start.Enabled = true;
        }

        private void pbx_DisplayCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Task.Run(new Action(() =>
            { DrawerHandler.SetCursorPos(e.Location); }));
        }

        private void frm_Main_MouseMove(object sender, MouseEventArgs e)
        {
            Task.Run(new Action(() =>
            { DrawerHandler.SetCursorPos(e.Location); }));
        }

        private void pbx_DisplayCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if(FirstTime)
            {
                DrawerHandler.InitDraw(e.Graphics);
                FirstTime = false;
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

            Debug.WriteLine("Resized!");
        }

        private void frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        { DrawerHandler.CleanUp(); }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            Run = false;

            btn_Stop.Enabled = false;
            btn_Start.Enabled = true;

            DrawerHandler.CleanUp();

            DrawerHandler.SetUp();

            Task.Run(() =>
            {
                pbx_DisplayCanvas.Invalidate();
                pbx_DisplayCanvas.BackColor = DrawerHandler.CanvasColour;
            });

        }
    }
}