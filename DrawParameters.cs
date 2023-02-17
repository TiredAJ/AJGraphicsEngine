using System.Diagnostics;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {

        //      Declaration     //
        BasicSquare SQ1 = new BasicSquare();
        BasicSquare SQ2 = new BasicSquare();
        Square SQ_ADV = new Square( new Rectangle(0,0,50,50));

        DebugCircle DCA = new DebugCircle(), DCB = new DebugCircle();
        DebugCircle DCC = new DebugCircle(), DCD = new DebugCircle();

        Vector2 SQ1V = new Vector2(2, 1);
        //Vector2 SQ2V = new Vector2(2, 1);
        Vector2 CollisionNormal = new Vector2();
        Vector2 Reflect = new Vector2();

        int Distance; float DotP;

        Vector2 DSQ_SQ = new Vector2();

        //                      //

        public void SetUp()
        {
            SQ1.Width = SQ2.Width = 50;
            SQ1.Height = SQ2.Height = 50;
            //
            SQ1.Centre = new Vector2
            (
                (int)Display.Width / 2,
                (int)Display.Height / 2
            );

            SQ_ADV.PrimaryCol = SQ1.PrimaryCol = SQ2.PrimaryCol = Color.Green;
            SQ_ADV.SecondaryCol = SQ1.SecondaryCol = SQ2.SecondaryCol = Color.Azure;
            SQ_ADV.BorderWidth = SQ1.BorderWidth = SQ2.BorderWidth = 5f;

            SQ_ADV.SetCentre(new Vector2(100, 100));

            Add(SQ1);
            //Add(SQ2);
            Add(SQ_ADV);
            Add(DCA);
            Add(DCB);
            Add(DCC);
            Add(DCD);
        }

        private void Frame()
        {
            SQ1.Centre.X += SQ1V.X;
            SQ1.Centre.Y += SQ1V.Y;

            //SQ2.Centre = Cursor;
            SQ_ADV.SetCentre(Cursor);

            DCA.Centre = SQ_ADV.CornerA;
            DCB.Centre = SQ_ADV.CornerB;
            DCC.Centre = SQ_ADV.CornerC;
            DCD.Centre = SQ_ADV.CornerD;

            if
            ((SQ1.Centre.X + (SQ1.Width / 2)) >= (Display.X + Display.Width) ||
             (SQ1.Centre.X - (SQ1.Width / 2)) <= Display.X)
            { SQ1V.X *= -1; }

            if
            ((SQ1.Centre.Y + (SQ1.Height / 2)) >= (Display.Y + Display.Height) ||
             (SQ1.Centre.Y - (SQ1.Height / 2)) <= Display.Y)
            { SQ1V.Y *= -1; }

            //DSQ_SQ.X = SQ1.Centre.X - SQ2.Centre.X;
            //DSQ_SQ.Y = SQ1.Centre.Y - SQ2.Centre.Y;
            DSQ_SQ.X = SQ1.Centre.X - SQ_ADV.Centre.X;
            DSQ_SQ.Y = SQ1.Centre.Y - SQ_ADV.Centre.Y;

            Distance = (int)Math.Sqrt((DSQ_SQ.X * DSQ_SQ.X) + (DSQ_SQ.Y * DSQ_SQ.Y));

            if (Distance <= 50)
            {
                //Debug.WriteLine("Collision!");

                //CollisionNormal = (new Vector2(((Vector2)SQ1.Centre) - ((Vector2)SQ2.Centre))).Normalise();
                CollisionNormal = (new Vector2(((Vector2)SQ1.Centre) - ((Vector2)SQ_ADV.Centre))).Normalise();

                DotP = Vector2.Dot(CollisionNormal, (Vector2)SQ1.Centre);

                Reflect = ((CollisionNormal * (int)DotP) * 2) - SQ1V;

                SQ1V = Reflect;

                //if ((SQ1.Centre.X - SQ2.Centre.X) < (SQ1.Centre.Y - SQ2.Centre.Y))
                //{SQ1V.X *= -1;}
                //else
                //{SQ1V.Y *= -1;}
            }

            
        }
    }
}