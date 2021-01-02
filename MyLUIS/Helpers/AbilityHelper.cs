using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyLUIS
{

    public static class AbilityHelper
    {
        public static string weather_url = @"https://opendata.cwb.gov.tw/fileapi/v1/opendataapi/F-C0032-005?Authorization=rdec-key-123-45678-011121314&format=JSON";


        public static string HttpGet(string strUrl, bool KeepAlive = false, int Timeout = 10000)
		{


			string UserID = string.Empty;     //連線驗證用的帳號
		    string Password = string.Empty;   //連線驗證用的密碼

		
			Encoding TextEncoding = Encoding.Default;
			string strRet = "";
			Uri uri = new Uri(strUrl);
			HttpWebRequest hwReq = WebRequest.Create(uri) as HttpWebRequest;

			if ((string.IsNullOrEmpty(UserID) == false) && (string.IsNullOrEmpty(Password) == false))
				hwReq.Credentials = new NetworkCredential(UserID, Password);

			hwReq.Method = WebRequestMethods.Http.Get;
			hwReq.KeepAlive = KeepAlive;
			hwReq.Timeout = Timeout;

			using (HttpWebResponse hwRes = hwReq.GetResponse() as HttpWebResponse)
			{
				using (StreamReader reader = new StreamReader(hwRes.GetResponseStream(), TextEncoding))
				{
					strRet = reader.ReadToEnd();
				}
			}
			return strRet;
		}

	}
}
