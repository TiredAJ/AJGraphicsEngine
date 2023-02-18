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
            });
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            DrawerHandler.SetUp();

            Run = true;

            TDrawer = new Thread(Refresher);
            TDrawer.Start();
        }

        private void Refresher()
        {
            while (Run)
            {
                Start = DateTime.Now.TimeOfDay;

                Task.Run(() =>
                {
                    pbx_DisplayCanvas.Invoke(new Action(() =>
                    {
                        pbx_DisplayCanvas.Invalidate();
                    }));
                });

                End = DateTime.Now.TimeOfDay;
                Delta = (End - Start) - LastTime;
                LastTime = End - Start;

                Thread.Sleep(1);
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            Run = false;

            DrawerHandler.CleanUp();
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

            if (FirstTime)
            {
                DrawerHandler.InitDraw(e.Graphics);
                FirstTime = false;
            }
            else
            {DrawerHandler.CallDraw(e.Graphics);}
        }
    }
}