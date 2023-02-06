using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {
        private List<Shapes> ShapeList = new List<Shapes>();

        public RectangleF Display = new RectangleF();

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
    }

    public struct ShapeColours
    {
        Color Primary;
        Color Secondary;
        Color Tertiary;
        Color Border;
    }

    public class Vector2
    {
        public float Speed;
        public Radian Direction;

        public Vector2()
        {
            Speed = 0f;
            Direction = 0d;
        }

        public Vector2(float _Speed, Radian _Direction)
        {
            Speed = _Speed;
            Direction = _Direction;
        }

        public void SetSpeed(float _Speed)
        { Speed = _Speed; }

        public void SetDirection(Radian _Direction)
        { Direction = _Direction; }

        public Radian GetAngleDifference(Vector2 Vb)
        {return this.Direction - Vb.Direction;}

        public static Vector2 operator +(Vector2 Va, Vector2 Vb)
        => new Vector2
        (
            Va.Speed + Vb.Speed, 
            Va.Direction + Vb.Direction
        );

        public static Vector2 operator +(Vector2 Va, float Scalar)
        => new Vector2(Va.Speed + Scalar, Va.Direction);

        public static Vector2 operator -(Vector2 Va, Vector2 Vb)
        => new Vector2
        (
            -(Va.Speed) + Vb.Speed,
            -(Va.Direction) + Vb.Direction
        );

        public static Vector2 operator -(Vector2 Va, float Scalar)
        => new Vector2(Va.Speed - Scalar, Va.Direction);

        public static Vector2 operator *(Vector2 Va, float Scalar)
        => new Vector2(Va.Speed * Scalar, Va.Direction);
    }

    public class Radian
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

        public static bool operator ==(Radian? Ra, Radian? Rb)
        {
            if (Ra == null && Rb == null) return true;
            if (Ra == null || Rb == null) return false;

            return Ra.Rad == Rb.Rad;
        }
        
        public static bool operator !=(Radian? Ra, Radian? Rb)
        {
            if (Ra != null && Rb != null) return true;
            if (Ra != null || Rb != null) return false;

            return Ra.Rad != Rb.Rad;
        }

        public static implicit operator Radian(double Da)
        => new Radian(Da);
    }
}
