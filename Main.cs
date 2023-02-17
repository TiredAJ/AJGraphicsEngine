using System.Diagnostics;

namespace BasicGraphicsEngine
{
    public partial class frm_Main : Form
    {
        private Drawer? DrawerHandler;
        private bool Run = false, FirstTime = true;
        private TimeSpan Start, End, LastTime = new TimeSpan(), Delta;
        private Thread? TDrawer;

        public frm_Main()
        { InitializeComponent(); }
        private void frm_Main_Load(object sender, EventArgs e)
        {
            DrawerHandler = new Drawer
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

        private async void Refresher()
        {
            while (Run)
            {
                Start = DateTime.Now.TimeOfDay;

                await Task.Run(async () =>
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

        private void Logger(TimeSpan Last, TimeSpan Current, TimeSpan Delta)
        {
            if (!File.Exists("./Log.csv"))
            {
                StreamWriter Temp = new StreamWriter("./Log.csv");
                Temp.WriteLineAsync("Last time (ms), current time (ms), delta (ms)");
                Temp.Close();
            }

            StreamWriter Writer = new StreamWriter("./Log.csv", true);

            Writer.WriteLineAsync($"{Last.Ticks},{Current.Ticks},{Delta.Ticks}");
            Writer.Close();
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
            if (FirstTime)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                DrawerHandler.InitDraw(e.Graphics);
                FirstTime = false;
            }
            else
            {DrawerHandler.CallDraw(e.Graphics);}
        }
    }
}