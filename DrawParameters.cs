using System.Diagnostics;

namespace BasicGraphicsEngine
{
    //      Class Declaration       //
    class Boundary : Line
    {
        public Boundary (int Ax, int Ay, int Bx, int By) 
        {
            A.X = Ax;
            A.Y = Ay;
            B.X = Bx;
            B.Y = By;
        }

    }

    class Ray : Line
    {
        Vector2 Direction = new Vector2(10, 0);

        public Ray (int PosX, int PosY) 
        {
            A = new Vector2(PosX, PosY);
            B = A + Direction;
        }

        public Vector2 Cast(Line _L) 
        {
            float T; // if 0 < t < 1 = intersecting
            float U; // if U > 0 = intersecting

            return new Vector2(null, null);
        }
    }


    //                              //



    public partial class Drawer
    {

        //      Declaration         //
        Boundary B = new Boundary(200, 100, 200, 400);
        Ray R = new Ray(100, 200);
        DebugCircle DC = new DebugCircle();

        //                          //



        public void SetUp()
        {
            B.PrimaryCol = Color.Bisque;

            Add(B);
            Add(R);
            Add(DC);
        }



        private void Frame()
        {
            Vector2 Temp = R.Cast(B);

            if(!Temp.IsNull()) 
            {
                DC.Centre = Temp;
            }
        }



        private void Resize()
        {}
    }
}