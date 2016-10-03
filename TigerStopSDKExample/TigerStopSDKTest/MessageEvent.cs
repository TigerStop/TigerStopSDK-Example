using System;

namespace TigerStopSDKExample
{
    class MessageEvent : EventArgs
    {
        //  =  =  =  ATTRIBUTES  =  =  =
        private string message = string.Empty;
        private DateTime time = DateTime.MinValue;
        private double value = double.NaN;

        //  =  =  =  CONSTRUCTORS  =  =  =
        public MessageEvent(string newMessage)
        {
            message = newMessage;
        }

        public MessageEvent(DateTime newTime)
        {
            time = newTime;
        }

        public MessageEvent(double newValue)
        {
            value = newValue;
        }

        //  =  =  =  GETTERS/SETTERS  =  =  =
        public string Message
        {
            get
            {
                return message;
            }
        }

        public DateTime Time
        {
            get
            {
                return time;
            }
        }

        public double Value
        {
            get
            {
                return value;
            }
        }
    }
}
