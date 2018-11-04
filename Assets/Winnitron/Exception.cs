using System;

namespace Winnitron {
    public class Exception : System.Exception {
        public Exception() {}
        public Exception(string message) : base(message) {}
        public Exception(string message, Exception inner) : base(message, inner) {}
    }

    public class NetworkException : Winnitron.Exception {
        public NetworkException() {}
        public NetworkException(string message) : base(message) {}
        public NetworkException(string message, Exception inner) : base(message, inner) {}
    }

}