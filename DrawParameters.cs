using System.Diagnostics;
using System.Numerics;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {

        //      Declaration     //
        BasicSquare[] Tiles = new BasicSquare[100]; //10x10

        //                      //



        public void SetUp()
        {
            TargetFPS = 60;

            Array.Fill<BasicSquare>(Tiles, new BasicSquare());

            Size TileSize = new Size((int)DisplaySize.X/10, (int)DisplaySize.Y/10);
            PointF StartPos = new PointF(TileSize.Width/2, (TileSize.Height/2)+1);

            for(int i = 0; i < 100; i++)
            {
                Tiles[i] = new BasicSquare
                (new RectangleF(StartPos, TileSize))
                { BorderWidth = 2f,PrimaryCol = Color.Red,SecondaryCol = Color.Transparent };

                StartPos.X += TileSize.Width;

                if((StartPos.X + TileSize.Width) > DisplaySize.X)
                {
                    StartPos.X = TileSize.Width/2;
                    StartPos.Y += TileSize.Height;
                    Debug.WriteLine($"{StartPos}, I= {i}");
                }
            }

            AddShapes(Tiles);
        }



        private void Resize(object? Sender, DisplayEventArgs De)
        {}



        private void Frame()
        {
            
        }
    }
}