using System.Diagnostics;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {

        //      Declaration     //
        BasicSquare SQ = new BasicSquare();
        BasicSquare SQ2 = new BasicSquare();
        //BasicCircle BC_TopLeft = new BasicCircle(4);
        //BasicCircle BC_BottomLeft = new BasicCircle(4);
        BasicCircle BC_TopRight = new BasicCircle(4);
        BasicCircle BC_BottomRight = new BasicCircle(4);
        BasicCircle BC2_TopLeft = new BasicCircle(4);
        BasicCircle BC2_BottomLeft = new BasicCircle(4);
        //BasicCircle BC2_TopRight = new BasicCircle(4);
        //BasicCircle BC2_BottomRight = new BasicCircle(4);

        Vector2 SQV = new Vector2(1, 0);
        Vector2 SQ2V = new Vector2(2, 1);

        Point SQ1A, SQ1B, SQ2A, SQ2B;

        //                      //

        public void SetUp()
        {
            SQ.Width = SQ2.Width = 50;
            SQ.Height = SQ2.Height = 50;
            //
            SQ.Centre = new Point
            (
                (int)Display.Width / 2,
                (int)Display.Height / 2
            );
            
            SQ.PrimaryCol = SQ2.PrimaryCol = Color.Green;
            SQ.SecondaryCol = SQ2.SecondaryCol = Color.Azure;
            SQ.BorderWidth = SQ2.BorderWidth = 5f;

            /*BC_TopLeft.PrimaryCol = BC_BottomLeft.PrimaryCol =*/ BC_TopRight.PrimaryCol = BC_TopRight.SecondaryCol = Color.Red;
            /*BC_TopLeft.SecondaryCol = BC_BottomLeft.SecondaryCol =*/ BC_BottomRight.PrimaryCol = BC_BottomRight.SecondaryCol = Color.ForestGreen;

            BC2_BottomLeft.PrimaryCol = BC2_BottomLeft.SecondaryCol = /*BC2_BottomRight.PrimaryCol = BC2_TopRight.PrimaryCol = */Color.ForestGreen;
            BC2_TopLeft.PrimaryCol = BC2_TopLeft.SecondaryCol = /*BC2_BottomRight.SecondaryCol = BC2_TopRight.SecondaryCol =*/ Color.Red;

            Add(SQ);
            Add(SQ2);
            //Add(BC_TopLeft);
            //Add(BC_BottomLeft);
            Add(BC_BottomRight);
            Add(BC_TopRight);
            Add(BC2_TopLeft);
            Add(BC2_BottomLeft);
            //Add(BC2_BottomRight);
            //Add(BC2_TopRight);
        }

        private void Frame()
        {
            SQ.Centre.X += SQV.X;
            //SQ.Centre.Y += SQV.Y;

            //SQ2.Centre.X += SQ2V.X;
            //SQ2.Centre.Y += SQ2V.Y;

            SQ2.Centre = Cursor;

            Debug.WriteLine($"SQ1: {SQ.Centre} - SQ2: {SQ2.Centre}");

            if
            ((SQ.Centre.X + (SQ.Width / 2)) >= (Display.X + Display.Width) ||
             (SQ.Centre.X - (SQ.Width / 2)) <= Display.X)
            {SQV.X *= -1;}
            
            if
            ((SQ.Centre.Y + (SQ.Height / 2)) >= (Display.Y + Display.Height) ||
             (SQ.Centre.Y - (SQ.Height / 2)) <= Display.Y)
            {SQV.Y *= -1;}

            SQ1A = new Point(SQ.Centre.X + (SQ.Width/2), SQ.Centre.Y + (SQ.Height/2));
            SQ1B = new Point(SQ.Centre.X + (SQ.Width / 2), SQ.Centre.Y - (SQ.Height / 2));
                
            SQ2A = new Point(SQ2.Centre.X - (SQ2.Width/2), SQ2.Centre.Y + (SQ2.Height/2));
            SQ2B = new Point(SQ2.Centre.X - (SQ2.Width / 2), SQ2.Centre.Y - (SQ2.Height / 2));

            //BC_TopLeft.Centre = new Point(SQ.Centre.X, SQ.Centre.Y - (SQ.Height / 2));
            //BC_BottomLeft.Centre = new Point(SQ.Centre.X, SQ.Centre.Y + (SQ.Height / 2));
            BC_BottomRight.Centre = SQ1B;
            BC_TopRight.Centre = SQ1A;
            
            BC2_TopLeft.Centre = SQ2A;
            BC2_BottomLeft.Centre = SQ2B;

            if (SQ2.Centre.X - (SQ2.Width / 2) == SQ.Centre.X + (SQ.Width / 2))
            {
                if (
                    (
                     (SQ2.Centre.Y - (SQ2.Height / 2) >= SQ.Centre.Y + (SQ.Height / 2) || SQ.Centre.Y + (SQ.Height / 2) >= SQ2.Centre.Y + (SQ2.Height / 2)) ||
                     (SQ2.Centre.Y - (SQ2.Height / 2) >= SQ.Centre.Y - (SQ.Height / 2) || SQ.Centre.Y - (SQ.Height / 2) >= SQ2.Centre.Y + (SQ2.Height / 2))
                    ) /*||
                    (
                     ()||
                     ()
                    )*/
                    )
                {SQV.X *= -1;}
            }
            //else if (SQ2.Centre.X + (SQ2.Width / 2) == SQ.Centre.X - (SQ.Width / 2))
        }
    }
}
