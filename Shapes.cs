using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Security.Permissions;

namespace BasicGraphicsEngine
{

    public class Shapes
    {
        public float BorderWidth = 2f;
        public PointF Centre = new PointF();
        public float TopEdge, BottomEdge, LeftEdge, RightEdge;
        public float Width, Height;
        public Color PrimaryCol = new Color();
        public Color SecondaryCol, TertiaryCol = Color.Transparent;
        public Bitmap? Canvas;

        public Shapes()
        {}

        public virtual Bitmap Draw(Bitmap _Canvas)
        {return _Canvas;}

        public virtual void CalculateBounds()
        {

        }
    }

    public class Polygon : Shapes
    {
        public List<PointF> Corners = new List<PointF>();

        public void AddPoints(PointF[] _Points)
        {
            Corners.AddRange(_Points);

            if (Corners.Count > 1) CalculateCentre();
        }

        public void AddPoint(PointF _Point)
        {
            Corners.Add(_Point);

            if (Corners.Count > 1) CalculateCentre();
        }

        private void CalculateCentre()
        {
            PointF Intermediate = new PointF(0, 0);

            foreach (PointF P in Corners)
            {
                Intermediate.X += P.X;
                Intermediate.Y += P.Y;
            }

            Intermediate.X /= Corners.Count;
            Intermediate.Y /= Corners.Count;

            Centre = Intermediate;
        }

    }

    public class Square : Shapes
    {
        public Square()
        {}
        
        public Square(Rectangle _Rect)
        {
            Centre = new PointF(0, 0);

            Width = _Rect.Size.Width;
            Height = _Rect.Size.Height;
        }


        public Square(Square _S)
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
            TopEdge = Centre.Y - (Height / 2);
            BottomEdge = Centre.Y + (Height / 2);
            LeftEdge = Centre.X - (Width / 2);
            RightEdge = Centre.X + (Width / 2);
        }

        public override Bitmap Draw(Bitmap _Canvas)
        {
            Graphics G = Graphics.FromImage(_Canvas);


            G.FillRectangle
            (
                new SolidBrush(SecondaryCol),
                Centre.X - ((float)Width)/2,
                Centre.Y - ((float)Height)/2,
                (float)Width, 
                (float)Height
            );
                        
            G.DrawRectangle
            (
                new Pen(PrimaryCol,BorderWidth),
                Centre.X - ((float)Width)/2,
                Centre.Y - ((float)Height)/2,
                (float)Width, 
                (float)Height
            );

            CalculateBounds();

            return _Canvas;
        }

        
    }
}
