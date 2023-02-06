using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace BasicGraphicsEngine
{

    public class Shapes
    {
        public float BorderWidth = 2f;
        public PointF Centre = new PointF();
        public Size ShapeSize = new Size();
        public Color PrimaryCol = new Color();
        public Color SecondaryCol, TertiaryCol = Color.Transparent;
        public Bitmap? Canvas;

        public Shapes()
        {}

        public virtual Bitmap Draw(Bitmap _Canvas)
        {return _Canvas;}
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
        public override Bitmap Draw(Bitmap _Canvas)
        {
            Graphics G = Graphics.FromImage(_Canvas);

            G.FillRectangle
            (
                new SolidBrush(SecondaryCol),
                Centre.X - ((float)ShapeSize.Width)/2,
                Centre.Y - ((float)ShapeSize.Height)/2,
                (float)ShapeSize.Width, 
                (float)ShapeSize.Height
            );
            
            G.DrawRectangle
            (
                new Pen(PrimaryCol,BorderWidth),
                Centre.X - ((float)ShapeSize.Width)/2,
                Centre.Y - ((float)ShapeSize.Height)/2,
                (float)ShapeSize.Width, 
                (float)ShapeSize.Height
            );

            return _Canvas;
        }

        
    }
}
