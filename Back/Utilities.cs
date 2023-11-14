using BasicGraphicsEngine.Back.Extensions;
using System.Diagnostics;
using System.Numerics;
using Windows.Storage.Streams;

namespace BasicGraphicsEngine.Back.Utilities
{
    /// <summary>
    /// Events relating to cursor stuff
    /// </summary>
    public class CursorEventArgs : EventArgs
    {
        public CState State { get; private set; }
        public Vector2 Location { get; private set; }
        public MouseButtons CButton { get; private set; }

        public CursorEventArgs()
        {
            State = CState.NoChange;
            Location = new Vector2(0, 0);
            CButton = MouseButtons.None;
        }

        public CursorEventArgs(MouseEventArgs Me, CState _State)
        {
            State = _State;
            Location = Me.Location.ToV2();
            CButton = Me.Button;
        }

    }

    /// <summary>
    /// Enums for click events
    /// </summary>
    public enum CState
    {
        Up,
        Down,
        NoChange,
        DoubleClick
    }

    /// <summary>
    /// Event args for stuff to do with the display/canvas changing
    /// </summary>
    public class DisplayEventArgs : EventArgs
    {
        /// <summary>
        /// Size of the display (in pixels?)
        /// </summary>
        public Vector2 DisplaySize { get; private set; }
        /// <summary>
        /// Whether the application is full screened or not
        /// </summary>
        public bool FullScreen { get; private set; }

        public DisplayEventArgs() { }

        public DisplayEventArgs(Vector2 _DisplaySize, bool _FullScreen)
        {
            DisplaySize = _DisplaySize;
            FullScreen = _FullScreen;
        }
    }

    /// <summary>
    /// Handy maths stuff maybe
    /// </summary>
    public class Maths
    {
        /// <summary>
        /// Maps a value
        /// </summary>
        /// <param name="_Val">Value to map</param>
        /// <param name="_InMax">Maximum input can be</param>
        /// <param name="InMin">Minimum input can be</param>
        /// <param name="_OutMax">Maximum the output can be</param>
        /// <param name="_OutMin">Minimum the output can be</param>
        /// <returns>_Val mapped</returns>
        public static int Map(int _Val, int _InMax, int InMin, int _OutMax, int _OutMin)
        { return (_Val - InMin) * (_OutMax - _OutMin) / (_InMax - InMin) + _OutMin; }

        /// <summary>
        /// Method <c>RandomValue</c> Returns a random value between two integers.
        /// </summary>
        public static int RandomValue(int Min, int Max, bool IncludeZero)
        {
            int Temp = 0;

            Random R = new Random((int)DateTime.Now.Ticks);

            R.Next(Min, Max);

            if (!IncludeZero)
            {
                while (Temp == 0)
                { Temp = R.Next(Min, Max); }
            }

            return Temp;
        }

        /// <summary>
        /// Method <c>RandomValue</c> Returns a random value between two integers. (includes 0)
        /// </summary>
        public static int RandomValue(int Min, int Max)
        {
            Random R = new Random((int)DateTime.Now.Ticks);

            return R.Next(Min, Max);
        }

        /// <summary>
        /// Converts degrees to radians
        /// </summary>
        /// <param name="_Deg">Input in degrees</param>
        /// <returns>_Deg in radians</returns>
        public static float DegToRad(float _Deg)
        { return (float)(_Deg * (Math.PI / 180)); }
    }

    public class Logger
    {
        private Stopwatch SW = new Stopwatch();
        private string Message = "Time taken: ";
        public long Average = 0;

        public Logger(string _Msg)
        {Message = _Msg;}

        public async void Start()
        {
            await Task.Run(() =>
            {SW.Restart();});
        }

        public async void Stop()
        {
            await Task.Run(() =>
            {
                SW.Stop();
                //Debug.WriteLine($"{Message} {SW.Elapsed.TotalMilliseconds}ms");

                Average = (Average + SW.ElapsedMilliseconds) / 2;
            });
        }
    }
}
