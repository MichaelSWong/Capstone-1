using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Class
{
    public static class AuditLog
    {
        public static void Log(string message)
        {
            string directory = Environment.CurrentDirectory;
            
            string fileTo = ($"log.txt");
            string fullPath = Path.Combine(directory, fileTo);
            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                sw.WriteLine(DateTime.UtcNow + " " + message);                
            }
                       
        }
    }
}
