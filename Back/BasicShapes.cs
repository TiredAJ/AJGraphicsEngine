using System.Numerics;

namespace BasicGraphicsEngine
{
    /// <summary>
    /// Base object for all basic shapes. Do not use directly.
    /// </summary>
    public abstract class BasicShape : DrawObject
    {
        public Vector2 Centre = new Vector2();
        public float BorderWidth = 2f;
        public int Width, Height;
        public Color SecondaryCol = Color.Green;
        public Color TertiaryCol = Color.Transparent;

    }

    /// <summary>
    /// Basic square class.
    /// </summary>
    public class BasicSquare : BasicShape
    {
        public BasicSquare()
        { }

        /// <summary>
        /// Constructor that takes in a <c>Rectangle</c>.
        /// </summary>
        public BasicSquare(Rectangle _Rect)
        {
            Centre = V2Ext.ToV2(_Rect.Location);
            Width = _Rect.Size.Width;
            Height = _Rect.Size.Height;
        }

        /// <summary>
        /// Constructor that takes in a <c>RectangleF</c>.
        /// </summary>
        public BasicSquare(RectangleF _Rect)
        {
            Centre = V2Ext.ToV2(_Rect.Location);

            Width = (int)_Rect.Size.Width;
            Height = (int)_Rect.Size.Height;
        }

        /// <summary>
        /// Constructor that takes in a pre-existing <c>BasicSquare</c> object.
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
        /// Renders the <c>BasicSquare</c> object using the inputted <c>Graphics</c> object.
        /// </summary>
        public override void Draw(Graphics G)
        {
            G.FillRectangle
            (
                new SolidBrush(SecondaryCol),
                (float)Centre.X - ((float)Width) / 2,
                (float)Centre.Y - ((float)Height) / 2,
                (float)Width,
                (float)Height
            );

            G.DrawRectangle
            (
                new Pen(PrimaryCol, BorderWidth),
                (float)Centre.X - ((float)Width) / 2,
                (float)Centre.Y - ((float)Height) / 2,
                (float)Width,
                (float)Height
            );
        }

        /// <summary>
        /// Converts the <c>BasicSquare</c> to a <c>Rectangle</c> object
        /// </summary>
        /// <returns>
        /// Returns a <c>Rectangle</c> object
        /// </returns>
        public Rectangle ToRectangle()
        {
            return new Rectangle
            (
                (int)Centre.X - (Width / 2), (int)Centre.Y - (Height / 2),
                Width, Height
            );
        }
    }

    /// <summary>
    /// Basic circle class.
    /// </summary>
    public class BasicCircle : BasicShape
    {
        public BasicCircle() { }

        /// <summary>
        /// Constructor that takes in a <c>Vector2</c> for a centre point and a <c>float</c> for radius.
        /// </summary>
        public BasicCircle(Vector2 _Centre, float _Radius)
        {
            Centre = _Centre;
            Width = Height = (int)_Radius * 2;
        }

        /// <summary>
        /// Constructor that takes a pre-existing <c>BasicCircle</c> object.
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
        /// Constructor that takes a <c>float</c> for radius.
        /// </summary>
        public BasicCircle(float _Radius)
        { Width = Height = (int)_Radius * 2; }

        /// <summary>
        /// Constructor that takes a <c>float</c> for radius.
        /// </summary>
        public void SetRadius(float _Radius)
        { Width = Height = (int)_Radius * 2; }

        /// <summary>
        /// Renders the object using the inputted <c>Graphics</c> object.
        /// </summary>
        public override void Draw(Graphics G)
        {
            G.FillEllipse
            (
                new SolidBrush(SecondaryCol),
                (float)Centre.X - ((float)Width) / 2,
                (float)Centre.Y - ((float)Height) / 2,
                (float)Width,
                (float)Height
            );

            G.DrawEllipse
            (
                new Pen(PrimaryCol, BorderWidth),
                (float)Centre.X - ((float)Width) / 2,
                (float)Centre.Y - ((float)Height) / 2,
                (float)Width,
                (float)Height
            );
        }
    }

    /// <summary>
    /// Base class for debug shapes. Do not use directly
    /// </summary>
    public abstract class DebugShape : DrawObject
    {
        public int Width = 10, Height = 10;
        public Vector2 Centre = new Vector2();
        public Color SecondaryCol = Color.Green;
    }

    /// <summary>
    /// A quick and easy circle to draw when debugging.
    /// </summary>
    public class DebugCircle : DebugShape
    {
        public DebugCircle()
        { }

        /// <summary>
        /// Constructor that takes in a <c>Vector2</c> for a centre point and a <c>float</c> for radius.
        /// </summary>
        public DebugCircle(Vector2 _Centre, int _Size)
        {
            PrimaryCol = Color.Green;
            SecondaryCol = Color.Red;
            Centre = _Centre;
            Width = Height = _Size;
        }

        /// <summary>
        /// Renders the object using the inputted <c>Graphics</c> object.
        /// </summary>
        public override void Draw(Graphics G)
        {
            G.FillEllipse
            (
                new SolidBrush(SecondaryCol),
                (float)Centre.X - ((float)Width) / 2,
                (float)Centre.Y - ((float)Height) / 2,
                (float)Width,
                (float)Height
            );

            G.DrawEllipse
            (
                new Pen(PrimaryCol, 2f),
                (float)Centre.X - ((float)Width) / 2,
                (float)Centre.Y - ((float)Height) / 2,
                (float)Width,
                (float)Height
            );
        }
    }
}
