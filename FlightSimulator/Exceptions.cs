using System;

namespace FlightSimulator
{
    class ExceptionAirplaneCrushed : Exception
    {
        public ExceptionAirplaneCrushed(string message)
            : base(message)
        { }
    }
    class ExceptionUnfitToFly : Exception
    {
        public ExceptionUnfitToFly(string message)
            : base(message)
        { }
    }
}
