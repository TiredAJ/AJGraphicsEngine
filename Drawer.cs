using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BasicGraphicsEngine
{
    public partial class Drawer
    {
        TimeSpan Start, End;
        bool Run = true;
        Thread DrawThread;


        public void StartDrawThread()
        {
            DrawThread = new Thread(CallDraw);
        }

        public void CallDraw()
        {
            SetUp();

            while (Run)
            {
                Start = DateTime.Now.TimeOfDay;

                Frame();

                End = DateTime.Now.TimeOfDay;
                Debug.WriteLine("Render time: {0}s", (End - Start));
            }
        }

        public void Stop()
        {Run = false;}

        public void Restart()
        {
            Run = false;
            Thread.Sleep(100);
            Run = true;

            StartDrawThread();
        }
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
