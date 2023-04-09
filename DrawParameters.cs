using System.Numerics;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {

        //      Declaration     //
        Square Mover = new Square(new Vector2(0, 0), 40, 40);

        Line Connector = new Line();

        Vector2 Accl = new Vector2(),
                Velocity = new Vector2(),
                Direction = new Vector2();

        NumericUpDown NUD_Limit = new NumericUpDown();

        //                      //



        public void SetUp()
        {
            Mover.SetCentre(DisplayCentre);

            Connector.A = Mover.Centre;
            Connector.B = DisplayCentre;

            Connector.PrimaryCol = Color.Aqua;

            NUD_Limit.Maximum = 45;
            NUD_Limit.Minimum = 0;

            NUD_Limit.Name = "nud_Limit";

            NUD_Limit.Width = (int)ControlsAreaSize.X - 10;

            Add(Connector);
            Add(Mover);

            AddControl(NUD_Limit);
        }



        private void Resize()
        { Mover.SetCentre(DisplayCentre); }



        private void Frame()
        {
            Direction = Vector2.Subtract(Cursor, Mover.Centre);

            Direction = Vector2.Normalize(Direction);

            Direction *= 0.5f;

            Accl = Direction;

            Velocity += Accl;

            Velocity = V2Ext.Limit(Velocity, (float)NUD_Limit.Value);

            Mover.SetCentre(Mover.Centre + Velocity);

            if
            ((Mover.Centre.X + (Mover.Width / 2)) >= (Display.X + Display.Width) ||
             (Mover.Centre.X - (Mover.Width / 2)) <= Display.X)
            { Velocity.X *= -1; }

            if
            ((Mover.Centre.Y + (Mover.Height / 2)) >= (Display.Y + Display.Height) ||
             (Mover.Centre.Y - (Mover.Height / 2)) <= Display.Y)
            { Velocity.Y *= -1; }

            Connector.A = Mover.Centre;
            Connector.B = Cursor;
        }
    }
}