using System.Diagnostics;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {

        //      Declaration     //
        BasicSquare SQ1 = new BasicSquare();
        BasicSquare SQ2 = new BasicSquare();
        //BasicCircle BC_TopLeft = new BasicCircle(4);
        //BasicCircle BC_BottomLeft = new BasicCircle(4);
        //BasicCircle BC_TopRight = new BasicCircle(4);
        //BasicCircle BC_BottomRight = new BasicCircle(4);
        //BasicCircle BC2_TopLeft = new BasicCircle(4);
        //BasicCircle BC2_BottomLeft = new BasicCircle(4);
        //BasicCircle BC2_TopRight = new BasicCircle(4);
        //BasicCircle BC2_BottomRight = new BasicCircle(4);

        Vector2 SQ1V = new Vector2(2, 1);
        Vector2 SQ2V = new Vector2(2, 1);
        Vector2 CollisionNormal = new Vector2();
        Vector2 Reflect = new Vector2();

        int Distance; float DotP;

        Vector2 DSQ_SQ = new Vector2();
        Vector2 T_SQ = new Vector2();

        Point SQ1A, SQ1B, SQ2A, SQ2B;

        //                      //

        public void SetUp()
        {
            SQ1.Width = SQ2.Width = 50;
            SQ1.Height = SQ2.Height = 50;
            //
            SQ1.Centre = new Point
            (
                (int)Display.Width / 2,
                (int)Display.Height / 2
            );

            SQ1.PrimaryCol = SQ2.PrimaryCol = Color.Green;
            SQ1.SecondaryCol = SQ2.SecondaryCol = Color.Azure;
            SQ1.BorderWidth = SQ2.BorderWidth = 5f;

            ///*BC_TopLeft.PrimaryCol = BC_BottomLeft.PrimaryCol =*/ BC_TopRight.PrimaryCol = BC_TopRight.SecondaryCol = Color.Red;
            ///*BC_TopLeft.SecondaryCol = BC_BottomLeft.SecondaryCol =*/ BC_BottomRight.PrimaryCol = BC_BottomRight.SecondaryCol = Color.ForestGreen;

            //BC2_BottomLeft.PrimaryCol = BC2_BottomLeft.SecondaryCol = /*BC2_BottomRight.PrimaryCol = BC2_TopRight.PrimaryCol = */Color.ForestGreen;
            //BC2_TopLeft.PrimaryCol = BC2_TopLeft.SecondaryCol = /*BC2_BottomRight.SecondaryCol = BC2_TopRight.SecondaryCol =*/ Color.Red;

            Add(SQ1);
            Add(SQ2);
            //Add(BC_TopLeft);
            //Add(BC_BottomLeft);
            //Add(BC_BottomRight);
            //Add(BC_TopRight);
            //Add(BC2_TopLeft);
            //Add(BC2_BottomLeft);
            //Add(BC2_BottomRight);
            //Add(BC2_TopRight);
        }

        private void Frame()
        {
            SQ1.Centre.X += SQ1V.X;
            SQ1.Centre.Y += SQ1V.Y;

            //SQ2.Centre.X += SQ2V.X;
            //SQ2.Centre.Y += SQ2V.Y;

            SQ2.Centre = Cursor;

            //Debug.WriteLine($"SQ1: {SQ1.Centre} - SQ2: {SQ2.Centre}");

            if
            ((SQ1.Centre.X + (SQ1.Width / 2)) >= (Display.X + Display.Width) ||
             (SQ1.Centre.X - (SQ1.Width / 2)) <= Display.X)
            { SQ1V.X *= -1; }

            if
            ((SQ1.Centre.Y + (SQ1.Height / 2)) >= (Display.Y + Display.Height) ||
             (SQ1.Centre.Y - (SQ1.Height / 2)) <= Display.Y)
            { SQ1V.Y *= -1; }

            DSQ_SQ.X = SQ1.Centre.X - SQ2.Centre.X;
            DSQ_SQ.Y = SQ1.Centre.Y - SQ2.Centre.Y;

            Distance = (int)Math.Sqrt((DSQ_SQ.X * DSQ_SQ.X) + (DSQ_SQ.Y * DSQ_SQ.Y));

            if (Distance <= 51)
            {
                //Debug.WriteLine("Collision!");

                CollisionNormal = (new Vector2(((Vector2)SQ1.Centre) - ((Vector2)SQ2.Centre))).Normalise();

                DotP = Vector2.Dot(CollisionNormal, (Vector2)SQ1.Centre);

                Reflect = ((int)(DotP * 2)) * (CollisionNormal * 2) - SQ1V;

                if ((SQ1.Centre.X - SQ2.Centre.X) < (SQ1.Centre.Y - SQ2.Centre.Y))
                {SQ1V.X *= -1;}
                else
                {SQ1V.Y *= -1;}
            }

            
        }
    }
}

/*
 It looks like you're on the right track, but the current condition for checking 
whether to invert the X or Y speed may not be correct.

Assuming that you want to reflect the velocity of SQ1 about the line connecting the 
centers of SQ1 and SQ2, you need to calculate the unit normal vector of that line, 
which is simply the vector connecting the centers of the squares, normalized. Here's 
the updated C# code:

scss
Copy code
if (Distance <= 50)
{
    Vector2 collisionNormal = Vector2.Normalize(SQ1.Centre - SQ2.Centre);
    float dotProduct = Vector2.Dot(SQ1V, collisionNormal);
    Vector2 reflectVector = 2 * dotProduct * collisionNormal - SQ1V;
    SQ1V = reflectVector;
}
Here, collisionNormal is the unit normal vector of the line connecting the centers of SQ1 and SQ2. We calculate the dot product of SQ1V with collisionNormal to find its component along the line, and then use that to reflect it about the line using the reflection formula R = 2 * dot(N, V) * N - V, where N is the unit normal vector and V is the velocity vector. The result is the new velocity vector reflectVector, which we assign to SQ1V.

This code should work correctly for reflecting the velocity of SQ1 about the line connecting the centers of SQ1 and SQ2, regardless of which side the collision happens on.
 */