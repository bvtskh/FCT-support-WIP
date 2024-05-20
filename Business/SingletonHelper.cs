namespace NichiconJP_FCT_Support_WIP.Business
{
    public class SingletonHelper
    {
        private static volatile PVSServiceReference.PVSWebServiceSoapClient _pvs_service = null;
        private static readonly object sync = new object();
        private SingletonHelper()
        {

        }
        public static PVSServiceReference.PVSWebServiceSoapClient PVSInstance
        {
            get
            {
                if (_pvs_service == null)
                {
                    lock (sync)
                    {
                        if (_pvs_service == null)
                        {
                            _pvs_service = new PVSServiceReference.PVSWebServiceSoapClient();
                        }
                    }
                }
                return _pvs_service;
            }
        }
    }
}
