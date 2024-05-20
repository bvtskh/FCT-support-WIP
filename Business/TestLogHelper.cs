using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NichiconJP_FCT_Support_WIP.Entity;

namespace NichiconJP_FCT_Support_WIP.Business
{
    public static class TestLogHelper
    {
        private static string[] _text;
        public static PVSServiceReference.WORK_ORDER_ITEMSEntity TestLogResult(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw Error.ArgumentNull("Path");
            }
            PVSServiceReference.WORK_ORDER_ITEMSEntity entity = null;
            string[] text = GetData(path);
            _text = text;
            entity = new PVSServiceReference.WORK_ORDER_ITEMSEntity()
            {
                BOARD_NO = GetBoardNo(),
                BOARD_STATE = GetBoardState(),
                STATION_NO = GetStationNo(),
                INITIATE_TIME = GetDateTimeService(),
                CASE_NO = GetCaseNo(),
                BASE_NO = GetProduct()
            };
            return entity;
        }
        private static string GetBoardNo()
        {
            //mph7498,781629,1,36906,2021/12/25,09:13:26,158,,FNL101Z07290
            if (_text.Length < 8)
            {
                throw Error.ArgumentOutOfRange("BoardNo");
            }
            return _text[7].ToUpper();
        }
        private static int GetBoardState()
        {
            //mph7498,781629,1,36906,2021/12/25,09:13:26,158,,FNL101Z07290
            if (_text.Length < 3)
            {
                throw Error.ArgumentOutOfRange("BoardState");
            }
            return _text[2] == "1" ? 1 : 2;
        }
        private static DateTime GetDateTimeService()
        {
            return WebService.WS.PvsService.GetDateTime();
        }
        private static string GetStationNo()
        {
            return SystemSetting.stationNo;
        }
        private static string GetCaseNo()
        {
            return string.Format("{0} {1}", _text[4], _text[5]);
        }
        private static string[] GetData(string path)
        {
            string[] allText = FileMethods.ReadTextLines(path);
            var allData = allText.Where(r => r.ToUpper().Contains("MPH")).ToArray();
            var last = allData[allData.Length - 1];
            return last.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
        private static string GetProduct()
        {
            return _text[0];
        }
    }
}
