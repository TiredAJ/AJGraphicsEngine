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

        //Declaration
            Square SQ = new Square();
            Square SQ2 = new Square();
            static float XPace = 1;
            static float YPace = 0.5f;
        //

        public void SetUp()
        {
            SQ.Width = SQ2.Width = 50;
            SQ.Height = SQ2.Height = 50;

            SQ.Centre = SQ2.Centre = new PointF
            (
                Display.Width / 2,
                Display.Height / 2
            );
            SQ.PrimaryCol = SQ2.PrimaryCol = Color.Green;
            SQ.SecondaryCol = SQ2.SecondaryCol = Color.Azure;
            SQ.BorderWidth = SQ2.BorderWidth = 5f;

            Add(SQ);
            Add(SQ2);
        }

		private void Frame()
		{
            SQ.Centre.X += XPace;
            SQ.Centre.Y += YPace;

            SQ2.Centre.X += YPace;
            SQ2.Centre.Y += XPace;

            if ((SQ.Centre.X + (SQ.Width / 2)) >= (Display.X + Display.Width))
            { XPace *= -1; }

            if ((SQ.Centre.X - (SQ.Width / 2)) <= Display.X)
            { XPace *= -1; }

            if ((SQ.Centre.Y + (SQ.Height / 2)) >= (Display.Y + Display.Height))
            { YPace *= -1; }

            if ((SQ.Centre.Y - (SQ.Height / 2)) <= Display.Y)
            { YPace *= -1; }
        }
	}
}
