﻿namespace BasicGraphicsEngine
{
    public partial class Drawer
    {

        //      Declaration     //
        BasicCircle[] CircleArray = new BasicCircle[1000];
        Line[] LineArray = new Line[1000];

        Vector2[] BC_Vel = new Vector2[1000];

        //                      //



        public void SetUp()
        {
            for (int i = 0; i < BC_Vel.Count(); i++)
            {
                BC_Vel[i] = new Vector2
                (
                    RandomValue(-20, 20, false),
                    RandomValue(-20, 20, false)
                );

                LineArray[i] = new Line();

                LineArray[i].PrimaryCol = Color.DeepPink;

                LineArray[i].LineWidth = 1f;

                CircleArray[i] = new BasicCircle(DisplayCentre, 10);

                CircleArray[i].PrimaryCol = Color.Blue;
                CircleArray[i].SecondaryCol = Color.Green;
            }

            CanvasColour = Color.Beige;

            Add(LineArray);
            Add(CircleArray);
        }



        private void Resize()
        {
            for (int i = 0; i < CircleArray.Count(); i++)
            {CircleArray[i].Centre = DisplayCentre;}
        }



        private void Frame()
        {
            for (int i = 0; i < CircleArray.Count(); i++)
            {
                if (i < CircleArray.Count()-1)
                {
                    LineArray[i].A = CircleArray[i].Centre;
                    LineArray[i].B = CircleArray[i+1].Centre;
                }

                CircleArray[i].Centre += BC_Vel[i];

                if
                ((CircleArray[i].Centre.X + (CircleArray[i].Width / 2)) >= (Display.X + Display.Width) ||
                 (CircleArray[i].Centre.X - (CircleArray[i].Width / 2)) <= Display.X)
                {BC_Vel[i].X *= -1;}

                if
                ((CircleArray[i].Centre.Y + (CircleArray[i].Height / 2)) >= (Display.Y + Display.Height) ||
                 (CircleArray[i].Centre.Y - (CircleArray[i].Height / 2)) <= Display.Y)
                {BC_Vel[i].Y *= -1;}
            }      
        }
    }
}