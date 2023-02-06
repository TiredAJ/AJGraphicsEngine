using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {

        //Declaration
            Square SQ = new Square();
            static int XPace = 2;
            static int YPace = 1;
        //

        public void SetUp()
        {
            SQ.ShapeSize = new Size(50, 50);
            SQ.Centre = new PointF
            (
                Display.Width / 2,
                Display.Height / 2
            );
            SQ.PrimaryCol = Color.Green;
            SQ.SecondaryCol = Color.Azure;
            SQ.BorderWidth = 5f;

            Add(SQ);
        }

		private void Frame()
		{
            SQ.Centre.X += XPace;
            SQ.Centre.Y += YPace;

            if ((SQ.Centre.X + (SQ.ShapeSize.Width/2)) >= (Display.X + Display.Width))
            {XPace *= -1;}
            
            if ((SQ.Centre.X - (SQ.ShapeSize.Width/2)) <= Display.X)
            {XPace *= -1;}
            
            if ((SQ.Centre.Y + (SQ.ShapeSize.Height/2)) >= (Display.Y + Display.Height))
            {YPace *= -1;}
            
            if ((SQ.Centre.Y - (SQ.ShapeSize.Height/2)) <= Display.Y)
            {YPace *= -1;}


        }
	}
}
