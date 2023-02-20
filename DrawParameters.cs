using System.Diagnostics;
using System.Media;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {

        //      Declaration     //
        BasicCircle[] CircleArray = new BasicCircle[500];
        Line[] LineArray = new Line[500];

        Vector2[] BC_Vel = new Vector2[500];

        //                      //



        public void SetUp()
        {
            for (int i = 0; i < BC_Vel.Count(); i++)
            {
                BC_Vel[i] = new Vector2
                (
                    RandomValue(-8, 8, false),
                    RandomValue(-8, 8, false)
                );

                LineArray[i] = new Line();

                LineArray[i].PrimaryCol = Color.DeepPink;

                LineArray[i].LineWidth = 1f;

                CircleArray[i] = new BasicCircle(DisplayCentre, 10);

                CircleArray[i].PrimaryCol = Color.Blue;
                CircleArray[i].SecondaryCol = Color.Green;
            }

            Add(LineArray);
            Add(CircleArray);
        }



        private void Frame()
        {
            for (int i = 0; i < CircleArray.Count(); i++)
            {
                CircleArray[i].Centre += BC_Vel[i];

                if (i < CircleArray.Count()-1)
                {
                    LineArray[i].A = CircleArray[i].Centre;
                    LineArray[i].B = CircleArray[i+1].Centre;
                }


                if
                ((CircleArray[i].Centre.X + (CircleArray[i].Width / 2)) == (Display.X + Display.Width) ||
                 (CircleArray[i].Centre.X - (CircleArray[i].Width / 2)) == Display.X)
                {BC_Vel[i].X *= -1;}

                if
                ((CircleArray[i].Centre.Y + (CircleArray[i].Height / 2)) == (Display.Y + Display.Height) ||
                 (CircleArray[i].Centre.Y - (CircleArray[i].Height / 2)) == Display.Y)
                {BC_Vel[i].Y *= -1;}
                
                if
                ((CircleArray[i].Centre.X + (CircleArray[i].Width / 2)) > (Display.X + Display.Width + 5) ||
                 (CircleArray[i].Centre.X - (CircleArray[i].Width / 2)) < Display.X - 5)
                {
                    BC_Vel[i].X *= -1;
                    CircleArray[i].Centre = DisplayCentre;
                
                }

                if
                ((CircleArray[i].Centre.Y + (CircleArray[i].Height / 2)) > (Display.Y + Display.Height + 5) ||
                 (CircleArray[i].Centre.Y - (CircleArray[i].Height / 2)) < Display.Y - 5)
                {
                    BC_Vel[i].Y *= -1;
                    CircleArray[i].Centre = DisplayCentre;
                }
            }      
        }
    }
}