using System.Diagnostics;
using System.Numerics;

namespace BasicGraphicsEngine
{

    public partial class Drawer
    {
        const int NoRows = 50;
        const int NoColumns = 50;


        //      Declaration     //
        BasicSquare[] Tiles = new BasicSquare[NoRows * NoColumns];

        //                      //



        public void SetUp()
        {
            TargetFPS = 60;

            Array.Fill<BasicSquare>(Tiles, new BasicSquare());

            Size TileSize = new Size((int)DisplaySize.X/NoColumns, (int)DisplaySize.Y/NoRows);
            PointF StartPos = new PointF((TileSize.Width/2)+1, (TileSize.Height/2)+1);

            for(int i = 0; i < (NoRows * NoColumns); i++)
            {
                Tiles[i] = new BasicSquare
                (new RectangleF(StartPos, TileSize))
                { BorderWidth = 2f,PrimaryCol = Color.Red,SecondaryCol = Color.Transparent };

                StartPos.X += TileSize.Width;

                if((StartPos.X + (TileSize.Width/2)) > DisplaySize.X)
                {
                    StartPos.X = (TileSize.Width/2)+1;
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