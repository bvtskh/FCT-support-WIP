using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NichiconJP_FCT_Support_WIP.WebService
{
    public static class WS
    {
        public static PVSServiceReference.PVSWebServiceSoapClient _pvs_service;
        public static object sync = new object();
        public static PVSServiceReference.PVSWebServiceSoapClient PvsService
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
