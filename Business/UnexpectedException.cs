using System;

namespace NichiconJP_FCT_Support_WIP.Business
{
    [Serializable]
    public sealed class UnexpectedException : Exception
    {
        private readonly object[] _args_;

        public object[] Arguments
        {
            get
            {
                return this._args_;
            }
        }

        public UnexpectedException(string message, params object[] args) : base(message)
        {
            this._args_ = (args ?? new object[0]);
        }
    }
}
