using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {
        private List<Shapes> ShapeList = new List<Shapes>();
        public RectangleF Display = new RectangleF();
        public PointF Cursor = new PointF(0f, 0f);

        public Drawer(Rectangle _Display)
        {Display = _Display;}

        public void Add(Shapes _NewShape)
        {ShapeList.Add(_NewShape);}

        public Bitmap CallDraw()
        {
            Bitmap Canvas = new Bitmap
            (
                (int)Display.Width, 
                (int)Display.Height
            );

            Frame();

            foreach (Shapes S in ShapeList)
            {S.Draw(Canvas);}

            return Canvas;
        }

        public Bitmap InitDraw()
        {
            SetUp();

            Bitmap Canvas = new Bitmap
            (
                (int)Display.Width,
                (int)Display.Height
            );

            foreach (Shapes S in ShapeList)
            { S.Draw(Canvas); }

            return Canvas;
        }

        public void CleanUp()
        {ShapeList.Clear();}

        public int RandomValue(int Min, int Max)
        {
            Random R = new Random(DateTime.Now.Microsecond);
            return R.Next(Min, Max);
        }

        public void SetCursorPos(PointF _Loc)
        {Cursor = _Loc;}
        
    }

    public struct ShapeColours
    {
        Color Primary;
        Color Secondary;
        Color Tertiary;
        Color Border;
    }

    public struct Vector2
    {
        public float X, Y;

        public Vector2()
        {
            X = 0f;
            Y = 0f;
        }
        
        public Vector2(float _X, float _Y)
        {
            X = _X;
            Y = _Y;
        }

        public void SpeedUp(float _Scalar)
        {
            X = X < 0 ? X -= _Scalar : X += _Scalar;
            Y = Y < 0 ? Y -= _Scalar : Y += _Scalar;
        }

        public float GetMagnitude()
        {return MathF.Sqrt((X * X) + (Y * Y));}

        public Vector2 Normalise()
        {
            if (GetMagnitude() > 0)
            {return new Vector2((X / GetMagnitude()), (Y / GetMagnitude()));}
            throw new DivideByZeroException();
        }

        public override string ToString()
        {return $"X:{X}, Y:{Y}";}

        public static Vector2 operator +(Vector2 Va, Vector2 Vb)
        => new Vector2
        (
            Va.X + Vb.X,
            Va.Y + Vb.Y
        );

        public static Vector2 operator +(Vector2 Va, float Scalar)
        => new Vector2(Va.X + Scalar, Va.Y);

        public static Vector2 operator -(Vector2 Va, Vector2 Vb)
        => new Vector2
        (
            Va.X - Vb.X,
            Va.Y - Vb.Y
        );

        public static Vector2 operator -(Vector2 Va, float Scalar)
        => new Vector2(Va.X - Scalar, Va.Y - Scalar);

        public static Vector2 operator *(Vector2 Va, float Scalar)
        => new Vector2(Va.X * Scalar, Va.Y * Scalar);

        public static Vector2 operator /(Vector2 Va, float Scalar) 
        => new Vector2(Va.X / Scalar, Va.Y / Scalar);
    }

    public struct Radian
    {
        private double Rad;

        public Radian()
        { }
        public Radian(double _Rad)
        { }

        public double GetDegree()
        {return (Rad * 180) / Math.PI;}

        public void FromDegrees(double _Degrees)
        {Rad = (_Degrees * Math.PI) / 180;}

        public override string ToString() => $"{Rad} Rad";

        public string ToString(string Format)
        {
            switch (Format)
            {
                case "Rads":
                {return $"{Rad} Rad";}
                case "Degrees":
                {return $"{GetDegree()}°";}
                default:
                {return $"{Rad} Rad [{GetDegree()}°]";}
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Radian radian &&
                   Rad == radian.Rad;
        }

        public override int GetHashCode()
        {return HashCode.Combine(Rad);}

        public static Radian operator +(Radian Ra, Radian Rb)
        => new Radian(Ra.Rad + Rb.Rad);

        public static Radian operator ++(Radian Ra)
        => new Radian(Ra.Rad++);

        public static Radian operator -(Radian Ra, Radian Rb)
        => new Radian(Ra.Rad - Rb.Rad);

        public static Radian operator -(Radian Ra)
        => new Radian(-(Ra.Rad));

        public static Radian operator --(Radian Ra)
        => new Radian(Ra.Rad--);

        public static bool operator ==(Radian Ra, Radian Rb)
        {
            if (Ra == null && Rb == null) return true;
            if (Ra == null || Rb == null) return false;

            return Ra.Rad == Rb.Rad;
        }
        
        public static bool operator !=(Radian Ra, Radian Rb)
        {
            if (Ra != null && Rb != null) return true;
            if (Ra != null || Rb != null) return false;

            return Ra.Rad != Rb.Rad;
        }

        public static implicit operator Radian(double Da)
        => new Radian(Da);
    }
}
