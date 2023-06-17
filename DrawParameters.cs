using System.Collections;
using System.Diagnostics;
using System.Numerics;

namespace BasicGraphicsEngine
{

    public partial class Drawer
    {


        //      Declaration     //
        const int NoRows = 20;
        const int NoColumns = 20;
                                                //x       y
        BitArray Tiles = new BitArray(NoColumns*NoRows, false);
        Size CellSize = new Size(5, 5);

        readonly BasicSquare DefaultCell = new BasicSquare()

        List<BasicSquare> Cells = new List<BasicSquare>();
        //                      //



        public void SetUp()
        {
            DisplayEvent += Resize;

            TargetFPS = 60;

            Cells.Add(new BasicSquare());


        }



        private void Resize(object? Sender, DisplayEventArgs De)
        {
            CalcCellSize();
        }



        private void Frame()
        {
            
        }


        private void CalcCellSize()
        {
            CellSize = new Size
            (
                (int)DisplaySize.X/NoColumns,
                (int)DisplaySize.Y/NoRows
            );
        }
    }
}