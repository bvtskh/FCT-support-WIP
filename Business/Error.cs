using System;

namespace NichiconJP_FCT_Support_WIP.Business
{
    public static class Error
    {
        public static Exception Argument(string message)
        {
            return new ArgumentException(message);
        }

        public static Exception Argument(string format, params object[] args)
        {
            return new ArgumentException(string.Format(format, args));
        }

        public static Exception ArgumentNull(string paramName)
        {
            return new ArgumentNullException(paramName);
        }

        public static Exception ArgumentOutOfRange(string paramName)
        {
            return new ArgumentOutOfRangeException(paramName);
        }

        public static Exception InvalidCast()
        {
            return new InvalidCastException();
        }

        public static Exception InvalidCast(string message)
        {
            return new InvalidCastException(message);
        }

        public static Exception InvalidCast(string format, params object[] args)
        {
            return new InvalidCastException(string.Format(format, args));
        }

        public static Exception InvalidOperation(string message)
        {
            return new InvalidOperationException(message);
        }

        public static Exception InvalidOperation(string format, params object[] args)
        {
            return new InvalidOperationException(string.Format(format, args));
        }

        public static Exception NotImplemented()
        {
            return new NotImplementedException();
        }

        public static Exception NotImplemented(string message)
        {
            return new NotImplementedException(message);
        }

        public static Exception NotSupported()
        {
            return new NotSupportedException();
        }

        public static Exception NotSupported(string message)
        {
            return new NotSupportedException(message);
        }

        public static Exception Unexpected(string message, object arg)
        {
            return new UnexpectedException(message, new object[]
            {
                arg
            });
        }

        public static Exception Unexpected(string message, object arg1, object arg2)
        {
            return new UnexpectedException(message, new object[]
            {
                arg1,
                arg2
            });
        }

        public static Exception Unexpected(string message, object arg1, object arg2, object arg3)
        {
            return new UnexpectedException(message, new object[]
            {
                arg1,
                arg2,
                arg3
            });
        }

        public static Exception Unexpected(string message, params object[] args)
        {
            return new UnexpectedException(message, args);
        }
    }
}
