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
        {}
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

    public class Circle : Shapes
    {
        public Circle() { }

        public Circle(PointF _Centre, float _Radius)
        {
            Centre = _Centre;
            Width = Height = _Radius * 2;
        }

        public Circle(Circle _C)
        {
            BorderWidth = _C.BorderWidth;
            Centre = _C.Centre;
            Width = _C.Width;
            Height = _C.Height;
            PrimaryCol = _C.PrimaryCol;
            SecondaryCol = _C.SecondaryCol;
            TertiaryCol = _C.TertiaryCol;
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

            return _Canvas;
        }
    }

    public class LineBase
    {
        public float LineWidth = 5f;
        public Color PrimaryCol = new Color();
        public Bitmap? _Canvas;

        public LineBase() { }

        public virtual Bitmap Draw(Bitmap _Canvas)
        {return _Canvas;}
    }

    public class Line : LineBase
    {
        public PointF A, B;

        public Line() { }

        public Line(PointF _A, PointF _B)
        {
            A = _A;
            B = _B;
        }

        public Line(Line _L)
        {
            A = _L.A;
            B = _L.B;
            LineWidth = _L.LineWidth;
            PrimaryCol = _L.PrimaryCol;
        }

        public override Bitmap Draw(Bitmap _Canvas)
        {
            Graphics G = Graphics.FromImage(_Canvas);

            G.DrawLine
            (
                new Pen(PrimaryCol,LineWidth),
                A, B
            );

            return _Canvas;
        }
    }

    public class Lines : LineBase
    {
        public List<PointF> Points = new List<PointF> ();
        
        public Lines() { }

        public Lines(List<PointF> _Points)
        {Points = _Points;}

        public Lines(Lines _L)
        {
            Points = _L.Points;
            LineWidth = _L.LineWidth;
            PrimaryCol = _L.PrimaryCol;
        }

        public override Bitmap Draw(Bitmap _Canvas)
        {
            Graphics G = Graphics.FromImage(_Canvas);

            G.DrawLines
            (
                new Pen(PrimaryCol, LineWidth),
                Points.ToArray()
            );

            return _Canvas;
        }
    }
}
