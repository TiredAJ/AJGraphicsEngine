﻿namespace BasicGraphicsEngine
{
    public partial class Drawer
    {

        //      Declaration     //
        Square Mover = new Square(new Vector2(0, 0), 40, 40);

        Line Connector = new Line();

        Vector2 Accl = new Vector2(),
                Velocity = new Vector2(),
                Direction = new Vector2();

        //                      //



        public void SetUp()
        {
            Mover.SetCentre(DisplayCentre);

            Connector.A = Mover.Centre;
            Connector.B = DisplayCentre;

            Connector.PrimaryCol = Color.Aqua;

            Add(Connector);
            Add(Mover);
        }



        private void Resize()
        { Mover.SetCentre(DisplayCentre); }



        private void Frame()
        {
            Direction = Vector2.Sub(Cursor, Mover.Centre);

            Direction.NormaliseVoid();

            Direction *= 0.5f;

            Accl = Direction;

            Velocity += Accl;

            Velocity.Limit(10f);

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