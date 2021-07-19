using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core
{
    public enum APICall
    {
        EPRODUCTDETAILS,
        EPRODUCTDIMENSIONS
    }
    public class Helper
    {
        public static string GetURLFromEnum(APICall aPICall)
        {
            string url = "";
            switch(aPICall)
            {
                case APICall.EPRODUCTDETAILS:
                    url = "https://lkz1ni34e9.execute-api.ap-southeast-1.amazonaws.com/test/myresource?";
                    break;

                case APICall.EPRODUCTDIMENSIONS:
                    url = "https://sobtcolppf.execute-api.ap-southeast-1.amazonaws.com/test/myresource?";
                    break;
            }
            return url;
        }
    }
}
