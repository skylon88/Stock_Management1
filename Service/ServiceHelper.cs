using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public static class ServiceHelper
    {
        public static string GenerateCodeNumber(string PreCode, int SerialNo)
        {
            return PreCode + DateTime.Now.Year + DateTime.Now.Month + SerialNo.ToString("D" + 4);
        }
        public static string ErrorPrint(string reportName, int row, int col)
        {
            string message = string.Format("Error, Please check '{0}', row : '{1}' and col : '{2}'", reportName, row, col);
            return message;
        }

        public static string PrintErrorList(IList<string> msgs)
        {
            var printMsg = string.Empty;
            foreach (var item in msgs)
            {
                printMsg += item + "\n";
            }
            return printMsg;
        }

    }

}
