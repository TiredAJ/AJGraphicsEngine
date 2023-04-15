namespace BasicGraphicsEngine
{
    public partial class Drawer
    {

        //      Declaration     //
        const int NoCircles = 2;

        Lines Trail;
        BasicCircle[] Circles;
        Line[] Connections;

        //                      //



        public void SetUp()
        {
            Trail = new Lines();

            Circles = new BasicCircle[NoCircles];
            Connections = new Line[NoCircles];

            Circles[0] = new BasicCircle(DisplayCentre, 40);
            Circles[1] = new BasicCircle(V2Ext.Offset(DisplayCentre, 40, 0), 40);

            Connections[0] = new Line(Circles[0].Centre, V2Ext.Offset(Circles[0].Centre, 40, 0));
            Connections[1] = new Line(Circles[1].Centre, V2Ext.Offset(Circles[1].Centre, 40, 0));

            Connections[0].PrimaryCol = Color.Blue;
            Connections[1].PrimaryCol = Color.Magenta;

            for(int i = 0; i < NoCircles; i++)
            {
                //Circles[i] = new BasicCircle(V2Ext.Offset(DisplayCentre, 20 * i, 0), 40);
                Circles[i].SecondaryCol = Color.Transparent;
                //Connections[i] = new Line(Circles[i].Centre, V2Ext.Offset(Circles[i].Centre, 40 * (i + 1), 0));
                //Connections[i].PrimaryCol = Color.Blue;
                Connections[i].LineWidth = 10f;
            }

            Trail.PrimaryCol = Color.Green;

            Add(Trail);
            Add(Circles);
            Add(Connections);
        }



        private void Resize()
        { }



        private void Frame()
        {
            Connections[0].Rotate(Circles[0].Centre, DegToRad(2));
            Circles[1].Centre = Connections[0].B;

            Connections[1].MoveTransform(Circles[1].Centre);
            Connections[1].Rotate(Circles[1].Centre, DegToRad(2));
        }
    }
}