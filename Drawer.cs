using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {
        TimeSpan Start, End;
        bool Run = true;
        Thread DrawThread;


        public void StartDrawThread()
        {
            DrawThread = new Thread(CallDraw);
        }

        public void CallDraw()
        {
            SetUp();

            while (Run)
            {
                Start = DateTime.Now.TimeOfDay;

                Frame();

                End = DateTime.Now.TimeOfDay;
                Debug.WriteLine("Render time: {0}s", (End - Start));
            }
        }

        public void Stop()
        {Run = false;}

        public void Restart()
        {
            Run = false;
            Thread.Sleep(100);
            Run = true;

            StartDrawThread();
        }
    }
}
