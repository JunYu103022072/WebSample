using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBsourse
{
    public static class Logger
    {
        public static void WriteLog(Exception ex)
        {
            System.IO.File.AppendAllText("E:\\Logs\\Log.log", ex.ToString());

            throw ex;
        }
    }
}
