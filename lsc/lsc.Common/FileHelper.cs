using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnuxq.Common
{
    public class FileHelper
    {
        public static string GetFileContent(string fileName)
        {
            string result = string.Empty;
            try
            {
                if (File.Exists(fileName))
                {
                    result = File.ReadAllText(fileName);
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
