using System;
using System.Collections.Generic;
using System.Linq;

namespace NewServices
{
    public static class ServiceHelper
    {
        public static string GenerateCodeNumber(string preCode, int serialNo)
        {
            //TO DO 
            //Need to change to month 
            //return preCode + DateTime.Now.Year + "04" + serialNo.ToString("D" + 4);
            return preCode + DateTime.Now.Year + DateTime.Now.Month.ToString("d2") + serialNo.ToString("D" + 4);
        }
        public static string ErrorPrint(string reportName, int row, int col)
        {
            var message = $"Error, Please check '{reportName}', row : '{row}' and col : '{col}'";
            return message;
        }

        public static string PrintErrorList(IList<string> msgs)
        {
            return msgs.Aggregate(string.Empty, (current, item) => current + (item + "\n"));
        }

    }

}
