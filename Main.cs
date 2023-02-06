namespace BasicGraphicsEngine
{
    public partial class frm_Main : Form
    {
        Drawer DrawerHandler = new Drawer();

        public frm_Main()
        { InitializeComponent(); }

        void OnPaint(object sender, PaintEventArgs e)
        {
            DrawerHandler.CallDraw();
        }
    }
}