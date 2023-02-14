namespace BasicGraphicsEngine
{
    public partial class Drawer
    {

        //      Declaration     //
        BasicSquare SQ = new BasicSquare();
        BasicSquare SQ2 = new BasicSquare();
        Vector2 SQV = new Vector2(1f, 0.5f);
        Vector2 SQ2V = new Vector2(2f, 1f);

        //                      //

        public void SetUp()
        {
            SQ.Width = /*SQ2.Width =*/ 50;
            SQ.Height = /*SQ2.Height =*/ 50;
            //
            SQ.Centre = /*SQ2.Centre =*/ new PointF
            (
                Display.Width / 2,
                Display.Height / 2
            );
            SQ.PrimaryCol = /*SQ2.PrimaryCol =*/ Color.Green;
            SQ.SecondaryCol = /*SQ2.SecondaryCol =*/ Color.Azure;
            SQ.BorderWidth = /*SQ2.BorderWidth =*/ 5f;
            
            Add(SQ);
            Add(SQ2);
        }

        private void Frame()
        {
            SQ.Centre.X += SQV.X;
            SQ.Centre.Y += SQV.Y;

            SQ2.Centre.X += SQ2V.X;
            SQ2.Centre.Y += SQ2V.Y;

            SQ2.Centre = Cursor;



            if
            ((SQ.Centre.X + (SQ.Width / 2)) >= (Display.X + Display.Width) ||
             (SQ.Centre.X - (SQ.Width / 2)) <= Display.X)
            {SQV.X *= -1;}
            
            if
            ((SQ.Centre.Y + (SQ.Height / 2)) >= (Display.Y + Display.Height) ||
             (SQ.Centre.Y - (SQ.Height / 2)) <= Display.Y)
            {SQV.Y *= -1;}

            //Debug.WriteLine(SQV.ToString());
            //Debug.WriteLine(SQ2V.ToString());
            //Debug.WriteLine("--------------------------");

        }
    }
}
