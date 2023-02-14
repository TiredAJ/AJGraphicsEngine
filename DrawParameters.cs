using System.Diagnostics;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {

        //      Declaration     //
        BasicSquare SQ = new BasicSquare();
        BasicSquare SQ2 = new BasicSquare();
        BasicCircle BC_Top = new BasicCircle(4);
        BasicCircle BC_Bottom = new BasicCircle(4);
        BasicCircle BC_Right = new BasicCircle(4);
        BasicCircle BC_Left = new BasicCircle(4);

        Vector2 SQV = new Vector2(1f, 0.5f);
        Vector2 SQ2V = new Vector2(2f, 1f);

        //                      //

        public void SetUp()
        {
            SQ.Width = SQ2.Width = 50;
            SQ.Height = SQ2.Height = 50;
            //
            SQ.Centre = new PointF
            (
                Display.Width / 2,
                Display.Height / 2
            );
            
            SQ.PrimaryCol = SQ2.PrimaryCol = Color.Green;
            SQ.SecondaryCol = SQ2.SecondaryCol = Color.Azure;
            SQ.BorderWidth = SQ2.BorderWidth = 5f;

            BC_Top.PrimaryCol = BC_Bottom.PrimaryCol = BC_Left.PrimaryCol = BC_Right.PrimaryCol = Color.Red;
            BC_Top.SecondaryCol = BC_Bottom.SecondaryCol = BC_Left.SecondaryCol = BC_Right.SecondaryCol = Color.Red;


            Add(SQ);
            Add(SQ2);
            Add(BC_Top);
            Add(BC_Bottom);
            Add(BC_Left);
            Add(BC_Right);
        }

        private void Frame()
        {
            SQ.Centre.X += SQV.X;
            SQ.Centre.Y += SQV.Y;

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

            BC_Top.Centre = new PointF(SQ.Centre.X, SQ.Centre.Y - (SQ.Height / 2));
            BC_Bottom.Centre = new PointF(SQ.Centre.X, SQ.Centre.Y + (SQ.Height / 2));
            BC_Left.Centre = new PointF(SQ.Centre.X - (SQ.Width / 2), SQ.Centre.Y);
            BC_Right.Centre = new PointF(SQ.Centre.X + (SQ.Width / 2), SQ.Centre.Y);

            if
            ((SQ.Centre.X + (SQ.Width / 2)) == (SQ2.Centre.X + (SQ2.Width/2)) ||
             (SQ.Centre.X - (SQ.Width / 2)) == SQ2.Centre.X - (SQ2.Width/2))
            {SQV.X *= -1;}
            
            if
            ((SQ.Centre.Y + (SQ.Height / 2)) == (SQ2.Centre.Y + (SQ2.Height/2)) ||
             (SQ.Centre.Y - (SQ.Height / 2)) == SQ2.Centre.Y - (SQ2.Height/2))
            {SQV.Y *= -1;}
        }
    }
}
