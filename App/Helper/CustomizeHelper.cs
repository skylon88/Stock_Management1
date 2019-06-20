using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace App.Helper
{
    public static class StringExtensions
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }

    public static class CustomizeHelper
    {
        public static string LoadFileName()
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            string fileName = String.Empty;

            fdlg.Title = "Excel File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                fileName = fdlg.FileName;
            }
            return fileName;
        }

        public static string[] LoadFolder()
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            var filenames = new List<string>();

            fdlg.Title = "Excel File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            fdlg.Multiselect = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in fdlg.FileNames)
                {
                    filenames.Add(file);
                }
                return filenames.ToArray();
            }
            return null;
        }

        public static void ErrorPrint(string reportName, int row, int col)
        {
            string message = string.Format("Error, Please check '{0}', row : '{1}' and col : '{2}'", reportName, row, col);
            MessageBox.Show(message);
        }

        public static void PrintErrorList(IList<string> msgs)
        {
            var printMsg = string.Empty;
            foreach (var item in msgs)
            {
                printMsg += item + "\n";
            }
            MessageBox.Show(printMsg);
        }

        public static void NameErrorPrint(string s)
        {
            MessageBox.Show(s);
        }

    }
}
