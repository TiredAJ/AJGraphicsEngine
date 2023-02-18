using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGraphicsEngine
{
    /// <summary>
    /// Class <c>BasicShape</c>. Base object for all basic shapes. Do not use directly.
    /// </summary>
    public class BasicShape : DrawObject
    {
        public float BorderWidth = 2f;
        public Vector2 Centre = new Vector2();
        public int Width, Height;
        public Color SecondaryCol, TertiaryCol = Color.Transparent;

        public BasicShape()
        { }

        /// <summary>
        /// Method <c>Draw</c> renders the basic square object using the inputted Graphics object.
        /// </summary>
        public override void Draw(Graphics G)
        {}

        /// <summary>
        /// Method <c>CalculateBounds</c> Potentially subject to removal. Calculates the bounds of the object.
        /// </summary>
        public virtual void CalculateBounds()
        { }
    }

    /// <summary>
    /// Class <c>BasicSquare</c> Basic square class.
    /// </summary>
    public class BasicSquare : BasicShape
    {
        public BasicSquare()
        { }

        /// <summary>
        /// Method <c>BasicSquare</c>. Constructor that takes in a rectangle.
        /// </summary>
        public BasicSquare(Rectangle _Rect)
        {
            Centre = new Vector2(0, 0);

            Width = _Rect.Size.Width;
            Height = _Rect.Size.Height;
        }
        
        /// <summary>
        /// Method <c>BasicSquare</c>. Constructor that takes in a pre-existing BasicSquare object.
        /// </summary>
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

        /// <summary>
        /// Method <c>CalculateBounds</c>. Potentially subject to removal. Calculates the bounds of the object.
        /// </summary>
        public override void CalculateBounds()
        {}

        /// <summary>
        /// Method <c>Draw</c> renders the BasicSquare object using the inputted Graphics object.
        /// </summary>
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
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle
            (
                Centre.X - (Width/2), Centre.Y - (Height/2),
                Width, Height
            );
        }
    }

    /// <summary>
    /// Class <c>BasicCircle</c>. Basic circle class.
    /// </summary>
    public class BasicCircle : BasicShape
    {
        public BasicCircle() { }

        /// <summary>
        /// Method <c>BasicCircle</c>. Constructor that takes in a Vector2 for a centre point and a <c>float</c> for radius.
        /// </summary>
        public BasicCircle(Vector2 _Centre, float _Radius)
        {
            Centre = _Centre;
            Width = Height = (int)_Radius * 2;
        }

        /// <summary>
        /// Method <c>BasicCircle</c>. Constructor that takes a pre-existing BasicCircle object.
        /// </summary>
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

        /// <summary>
        /// Method <c>BasicCircle</c>. Constructor that takes a float for radius.
        /// </summary>
        public BasicCircle(float _Radius)
        {Width = Height = (int)_Radius * 2;}

        /// <summary>
        /// Method <c>CalculateBounds</c>. Potentially subject to removal. Calculates the bounds of the object.
        /// </summary>
        public override void CalculateBounds()
        {}

        /// <summary>
        /// Method <c>Draw</c> renders the object using the inputted Graphics object.
        /// </summary>
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
    
    public class DebugCircle : BasicShape
    {
        public DebugCircle() 
        {
            PrimaryCol = Color.Green;
            SecondaryCol = Color.Red;
            Width = Height = 10;
        }

        /// <summary>
        /// Method <c>DebugCircle</c>. Constructor that takes in a Vector2 for a centre point and a <c>float</c> for radius.
        /// </summary>
        public DebugCircle(Vector2 _Centre)
        {
            PrimaryCol = Color.Green;
            SecondaryCol = Color.Red;
            Centre = _Centre;
            Width = Height = 5;
        }

        /// <summary>
        /// Method <c>Draw</c> renders the object using the inputted Graphics object.
        /// </summary>
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
