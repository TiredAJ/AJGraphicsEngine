using System.Diagnostics;

namespace BasicGraphicsEngine
{
    public partial class frm_Main : Form
    {
        private Drawer DrawerHandler;
        private bool Run = false;
        private Bitmap Canvas;
        private TimeSpan Start, End, LastTime = new TimeSpan(), Delta;
        private Thread TDrawer;

        public frm_Main()
        { InitializeComponent(); }
        private void frm_Main_Load(object sender, EventArgs e)
        {
            DrawerHandler = new Drawer(pbx_DisplayCanvas.DisplayRectangle);
            Canvas = new Bitmap(pbx_DisplayCanvas.Width, pbx_DisplayCanvas.Height);

            pbx_DisplayCanvas.Image = DrawerHandler.InitDraw();
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

                pbx_DisplayCanvas.Invoke(new Action(() =>
                {
                    pbx_DisplayCanvas.Image = DrawerHandler.CallDraw();
                }));

                End = DateTime.Now.TimeOfDay;
                Delta = (End - Start) - LastTime;
                //Debug.WriteLine("Render time: {0}s", (End-Start));
                //Debug.WriteLine("Render time Delta: {0}s", Delta);
                //Debug.WriteLine("--------------------------------");
                //Logger(LastTime, (End - Start), Delta);
                LastTime = End - Start;
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
    }
}