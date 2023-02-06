using System.Diagnostics;

namespace BasicGraphicsEngine
{
    public partial class frm_Main : Form
    {
        private Drawer DrawerHandler;
        private bool Run = false;
        private Bitmap Canvas;
        private TimeSpan Start, End;
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

        private void Refresher()
        {
            while (Run)
            {
                Start = DateTime.Now.TimeOfDay;

                //Task Refresher = Task.Run(() =>
                //{
                pbx_DisplayCanvas.Invoke(new Action(() =>
                {
                    pbx_DisplayCanvas.Image = DrawerHandler.CallDraw();
                }));
                //});

                End = DateTime.Now.TimeOfDay;
                Debug.WriteLine("Render time: {0}s", (End - Start));
            }
        }
    }
}