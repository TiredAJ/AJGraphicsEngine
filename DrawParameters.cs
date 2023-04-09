﻿using System.Numerics;

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
        NumericUpDown NUD_SizeWidth = new NumericUpDown();
        NumericUpDown NUD_SizeHeight = new NumericUpDown();

        //                      //



        public void SetUp()
        {
            Mover.SetCentre(DisplayCentre);

            Connector.A = Mover.Centre;
            Connector.B = DisplayCentre;

            Connector.PrimaryCol = Color.Aqua;

            NUD_Limit.Maximum = 500;
            NUD_Limit.Minimum = 1m;
            NUD_Limit.Value = 25m;
            NUD_Limit.Name = "nud_Limit";
            NUD_Limit.Width = (int)ControlsAreaSize.X - 10;

            NUD_SizeWidth.Maximum = 200;
            NUD_SizeWidth.Minimum = 5;
            NUD_SizeWidth.Value = 40;
            NUD_SizeWidth.Name = "nud_SizeWidth";
            NUD_SizeWidth.Width = (int)ControlsAreaSize.X - 10;

            NUD_SizeHeight.Maximum = 200;
            NUD_SizeHeight.Minimum = 5;
            NUD_SizeHeight.Value = 40;
            NUD_SizeHeight.Name = "nud_SizeHeight";
            NUD_SizeHeight.Width = (int)ControlsAreaSize.X - 10;

            Add(Connector);
            Add(Mover);

            AddControl(NUD_Limit);
            AddControl(NUD_SizeWidth);
            AddControl(NUD_SizeHeight);
        }



        private void Resize()
        { Mover.SetCentre(DisplayCentre); }



        private void Frame()
        {
            Mover.Rotate((float)NUD_Limit.Value / 1000);

            Mover.MoveTransform(Cursor);

            Mover.SizeTransform(new Vector2((float)NUD_SizeWidth.Value, (float)NUD_SizeHeight.Value));
        }
    }
}