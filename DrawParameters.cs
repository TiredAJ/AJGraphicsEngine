//using System.Numerics;

//namespace BasicGraphicsEngine
//{
//    public partial class Drawer
//    {

//        //      Declaration     //
//        BasicCircle[] Blues = new BasicCircle[500];
//        BasicCircle[] Reds = new BasicCircle[500];

//        Vector2[] Blue_Vel = new Vector2[500];
//        Vector2[] Red_Vel = new Vector2[500];

//        //                      //



//        public void SetUp()
//        {
//            PointF BlueStart = new PointF(DisplayCentre.Y, (DisplayCentre.X + DisplayCentre.X /4));

//            for (int i = 0; i < Blue_Vel.Count(); i++)
//            {
//                Blue_Vel[i] = new Vector2
//                (
//                    RandomValue(-20, 20, false),
//                    RandomValue(-20, 20, false)
//                );

//                Blues[i] = new BasicCircle(BlueStart.ToVector2(), 10);

//                Blues[i].PrimaryCol = Color.Blue;
//                Blues[i].SecondaryCol = Color.Green;
//            }

//            CanvasColour = Color.Beige;

//            DisplayEvent += Resize;

//            AddShapes(Blues);
//            AddShapes(Reds);
//        }



//        private void Resize(object Sender, DisplayEventArgs De)
//        {

//        }



//        private void Frame()
//        {
//            //for (int i = 0; i < CircleArray.Count(); i++)
//            //{
//            //    if (i < CircleArray.Count() - 1)
//            //    {
//            //        LineArray[i].A = CircleArray[i].Centre;
//            //        LineArray[i].B = CircleArray[i + 1].Centre;
//            //    }
//            //
//            //    CircleArray[i].Centre += BC_Vel[i];
//            //
//            //    if
//            //    ((CircleArray[i].Centre.X + (CircleArray[i].Width / 2)) >= DisplaySize.X ||
//            //     (CircleArray[i].Centre.X - (CircleArray[i].Width / 2)) <= 0)
//            //    { BC_Vel[i].X *= -1; }
//            //
//            //    if
//            //    ((CircleArray[i].Centre.Y + (CircleArray[i].Height / 2)) >= DisplaySize.Y ||
//            //     (CircleArray[i].Centre.Y - (CircleArray[i].Height / 2)) <= 0)
//            //    { BC_Vel[i].Y *= -1; }
//            //}
//        }
//    }
//}

using System.Numerics;
using BasicGraphicsEngine.Back.Utilities;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {

        //      Declaration     //
        BasicCircle[] CircleArray = new BasicCircle[100000];
        //Line[] LineArray = new Line[100000];

        Vector2[] BC_Vel = new Vector2[100000];

        //                      //



        public void SetUp()
        {
            for (int i = 0; i < BC_Vel.Count(); i++)
            {
                BC_Vel[i] = new Vector2
                (
                    Maths.RandomValue(-20, 20, false),
                    Maths.RandomValue(-20, 20, false)
                );

                //LineArray[i] = new Line();
                //
                //LineArray[i].PrimaryCol = Color.DeepPink;
                //
                //LineArray[i].LineWidth = 1f;

                CircleArray[i] = new BasicCircle(DisplayCentre, 10);

                CircleArray[i].PrimaryCol = Color.Blue;
                CircleArray[i].SecondaryCol = Color.Green;
            }

            CanvasColour = Color.Beige;

            DisplayEvent += Resize;

            //AddShape(LineArray);
            AddShape(CircleArray);
        }



        private void Resize(object Sender, DisplayEventArgs De)
        {
            for (int i = 0; i < CircleArray.Count(); i++)
            { CircleArray[i].Centre = DisplayCentre; }
        }



        private void Frame()
        {
            for (int i = 0; i < CircleArray.Count(); i++)
            {
                //if (i < CircleArray.Count() - 1)
                //{
                //    LineArray[i].A = CircleArray[i].Centre;
                //    LineArray[i].B = CircleArray[i + 1].Centre;
                //}

                CircleArray[i].Centre += BC_Vel[i];

                if
                ((CircleArray[i].Centre.X + (CircleArray[i].Width / 2)) >= DisplaySize.X ||
                 (CircleArray[i].Centre.X - (CircleArray[i].Width / 2)) <= 0)
                { BC_Vel[i].X *= -1; }

                if
                ((CircleArray[i].Centre.Y + (CircleArray[i].Height / 2)) >= DisplaySize.Y ||
                 (CircleArray[i].Centre.Y - (CircleArray[i].Height / 2)) <= 0)
                { BC_Vel[i].Y *= -1; }
            }
        }
    }
}