﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGraphicsEngine
{
    public class BasicShape : DrawObject
    {
        public float BorderWidth = 2f;
        public Point Centre = new Point();
        //public float TopEdge, BottomEdge, LeftEdge, RightEdge;
        public int Width, Height;
        public Color SecondaryCol, TertiaryCol = Color.Transparent;

        public BasicShape()
        { }

        public override void Draw(Graphics G)
        {}

        public virtual void CalculateBounds()
        { }
    }

    public class BasicSquare : BasicShape
    {
        public BasicSquare()
        { }

        public BasicSquare(Rectangle _Rect)
        {
            Centre = new Point(0, 0);

            Width = _Rect.Size.Width;
            Height = _Rect.Size.Height;


        }

        public BasicSquare(BasicSquare _S)
        {
            BorderWidth = _S.BorderWidth;
            Centre = _S.Centre;
            Width = _S.Width;
            Height = _S.Height;
            PrimaryCol = _S.PrimaryCol;
            SecondaryCol = _S.SecondaryCol;
            TertiaryCol = _S.TertiaryCol;
        }

        public override void CalculateBounds()
        {
            //TopEdge = Centre.Y - (Height / 2);
            //BottomEdge = Centre.Y + (Height / 2);
            //LeftEdge = Centre.X - (Width / 2);
            //RightEdge = Centre.X + (Width / 2);
        }

        public override void Draw(Graphics G)
        {
            G.FillRectangle
            (
                new SolidBrush(SecondaryCol),
                Centre.X - ((float)Width) / 2,
                Centre.Y - ((float)Height) / 2,
                (float)Width,
                (float)Height
            );

            G.DrawRectangle
            (
                new Pen(PrimaryCol, BorderWidth),
                Centre.X - ((float)Width) / 2,
                Centre.Y - ((float)Height) / 2,
                (float)Width,
                (float)Height
            );

            //CalculateBounds();
        }
    }

    public class BasicCircle : BasicShape
    {
        public BasicCircle() { }

        public BasicCircle(Point _Centre, float _Radius)
        {
            Centre = _Centre;
            Width = Height = (int)_Radius * 2;
        }

        public BasicCircle(BasicCircle _C)
        {
            BorderWidth = _C.BorderWidth;
            Centre = _C.Centre;
            Width = _C.Width;
            Height = _C.Height;
            PrimaryCol = _C.PrimaryCol;
            SecondaryCol = _C.SecondaryCol;
            TertiaryCol = _C.TertiaryCol;
        }

        public BasicCircle(float _Radius)
        {Width = Height = (int)_Radius * 2;}

        public override void CalculateBounds()
        {
            //TopEdge = Centre.Y - (Height / 2);
            //BottomEdge = Centre.Y + (Height / 2);
            //LeftEdge = Centre.X - (Width / 2);
            //RightEdge = Centre.X + (Width / 2);
        }

        public override void Draw(Graphics G)
        {
            G.FillEllipse
            (
                new SolidBrush(SecondaryCol),
                Centre.X - ((float)Width) / 2,
                Centre.Y - ((float)Height) / 2,
                (float)Width,
                (float)Height
            );

            G.DrawEllipse
            (
                new Pen(PrimaryCol, BorderWidth),
                Centre.X - ((float)Width) / 2,
                Centre.Y - ((float)Height) / 2,
                (float)Width,
                (float)Height
            );

            CalculateBounds();
        }
    }
}
