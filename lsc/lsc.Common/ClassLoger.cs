using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bnuxq.Common
{
    public enum LogerType
    {
        DEBUG,
        ERROR,
        INFO,
        FAIL
    }
    /// <summary>
    /// Log 记录类  默认在程序根目录logs目录下, 如果是服务器使用请重新设置LogPath 路径
    /// </summary>
    public class ClassLoger
    {
        /// <summary>
        /// 0：debug 1:info 3:fail 4:error
        /// debug < info < fail < error
        /// </summary>
        public static int LogLevel = 0;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="logpath"></param>
        /// <param name="isserver"></param>
        public static void Init(string logpath, bool isserver)
        {
            LogPath = logpath;
            IsServer = isserver;
        }
        static bool IsWork = false;
        /// <summary>
        /// 开始记录日志
        /// </summary>
        public static void StartLogin()
        {
            if (IsWork)
                return;
            IsWork = true;
            Task.Run(() => {
                while (IsWork)
                {
                    try
                    {
                        if (UnLog.Count == 0)
                        {
                            IsWork = false;
                            return;
                        }
                        Thread.Sleep(1000);
                        LogerManager loger = UnLog.Dequeue() as LogerManager;
                        if (loger != null)
                        {
                            string filePath, fileName;
                            if (!IsServer)
                            {
                                filePath = LogPath + "\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                                fileName = filePath + "\\" + DateTime.Now.Day.ToString() + ".Log";
                            }
                            else
                            {
                                filePath = LogPath;
                                fileName = Path.Combine(filePath, DateTime.Now.Day.TryToString() + ".Log");
                            }
                            if (!System.IO.Directory.Exists(filePath))
                            {
                                System.IO.Directory.CreateDirectory(filePath);
                            }
                            FileStream filestream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                            StreamWriter writer = new StreamWriter(filestream, System.Text.Encoding.Default);
                            writer.BaseStream.Seek(0, SeekOrigin.End);
                            writer.WriteLine("{0} >{1}> {2} > {3}", DateTime.Now.TimeOfDay, loger.type.TryToString(), loger.title, loger.msg);
                            writer.Flush();
                            writer.Close();
                            filestream.Close();
                        }
                    }
                    catch
                    {
                    }
                }
            });
        }

        /// <summary>
        /// 停止记录日志
        /// </summary>
        public static void StopLogin()
        {
            IsWork = false;
        }
        /// <summary>
        /// LOG存放主目录  默认 程序根目录
        /// </summary>
        public static string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

        //public static string LogPath = Path.Combine(@"E:\WebServiceLog", "logs");
        /// <summary>
        /// 是否是服务器日志方式
        /// </summary>
        public static bool IsServer = false;
        private static Queue UnLog = new Queue();
        static bool logger(LogerType type, string title, string msg)
        {
            LogerManager loger = new LogerManager();
            loger.type = type;
            loger.title = title;
            loger.msg = msg;
            UnLog.Enqueue(loger);
            StartLogin();
            return true;
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="title">日志类型信息</param>
        /// <param name="msg">日志信息</param>
        /// <returns></returns>
        public static bool Error(string title, String msg)
        {
            return logger(LogerType.ERROR, title, msg);
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="title">日志类型信息</param>
        /// <param name="ex">异常信息</param>
        /// <returns></returns>
        public static bool Error(string title, Exception ex)
        {
            return Error(title, string.Format("{0},{1}", ex.Message, ex.Source));
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="title">日志类型信息</param>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常信息</param>
        /// <returns></returns>
        public static bool Error(string title, String msg, Exception ex)
        {
            return Error(title, string.Format("{0},{1},{2}", ex.Message, ex.Source, msg));
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="title">日志类型信息</param>
        /// <param name="msg">日志信息</param>
        /// <returns></returns>
        public static bool DEBUG(string title, String msg)
        {
            if (LogLevel > 0)
                return true;
            return logger(LogerType.DEBUG, title, msg);
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="title">日志类型信息</param>
        /// <param name="ex">异常信息</param>
        /// <returns></returns>
        public static bool DEBUG(string title, Exception ex)
        {
            return DEBUG(title, string.Format("{0},{1}", ex.Message, ex.Source));
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="title">日志类型信息</param>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常信息</param>
        /// <returns></returns>
        public static bool DEBUG(string title, String msg, Exception ex)
        {
            return DEBUG(title, string.Format("{0},{1},{2}", ex.Message, ex.Source, msg));
        }

        public static bool DEBUG(string title, params string[] args)
        {
            return DEBUG(title, GetStr(args));
        }
        /// <summary>
        /// 打印普通信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool Info(string title, params string[] args)
        {
            if (LogLevel > 1)
            {
                return true;
            }
            return logger(LogerType.INFO, title, GetStr(args));
        }
        /// <summary>
        /// 打印失败信息，如执行某逻辑时 变量为空
        /// </summary>
        /// <param name="title"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool Fail(string title, params string[] args)
        {
            if (LogLevel > 2)
            {
                return true;
            }
            return logger(LogerType.FAIL, title, GetStr(args));
        }
        /// <summary>
        /// 打印程序异常信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool Error(string title, params string[] args)
        {
            if (LogLevel > 3)
            {
                return true;
            }
            if (args != null && args.Length > 0)
            {
                return logger(LogerType.ERROR, title, GetStr(args));
            }
            return false;
        }
        static string GetStr(params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in args)
                {
                    sb.AppendFormat("{0},", s);
                }
                return sb.ToString();
            }
            return "";
        }
    }

    class LogerManager
    {
        public LogerType type { get; set; }
        public string title { get; set; }
        public string msg { get; set; }
    }

    /// <summary>
    /// 自动清理日志
    /// </summary>
    public class clearlogs
    {
        public void Start()
        {
            ClassLoger.DEBUG("clearlogs/Start", "日志清理开始");
            th = new Thread(new ThreadStart(Worker));
            th.Start();
        }


        Thread th;
        bool iswork = true;
        void Worker()
        {
            string logpath = ClassLoger.LogPath;
            int time = 0;
            while (iswork)
            {
                Thread.Sleep(1000);
                time++;
                if (time < 60 * 60)
                    continue;
                time = 0;
                while (!Directory.Exists(logpath))
                {
                    Thread.Sleep(3000);
                }
                try
                {
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(logpath);

                    System.IO.DirectoryInfo[] childsdir = di.GetDirectories(); ;

                    foreach (DirectoryInfo d in childsdir)
                    {
                        try
                        {
                            string dirname = d.Name;
                            DateTime now = DateTime.Now;
                            if (dirname.Length == 6 || dirname.Length == 5)
                            {
                                string yeay_str = dirname.Substring(0, 4);
                                string month_str = dirname.Substring(4);
                                int year = 0;
                                int month = 0;
                                if (!int.TryParse(yeay_str, out year) || !int.TryParse(month_str, out month))
                                {
                                    try
                                    {
                                        d.Delete(true);
                                    }
                                    catch
                                    { }
                                    continue;
                                }

                                //删除上月日志
                                if (year < now.Year || month + 2 < now.Month)
                                {
                                    try
                                    {
                                        d.Delete(true);
                                        continue;
                                    }
                                    catch
                                    { }

                                }
                                if (year == now.Year && month <= now.Month)
                                {
                                    FileInfo[] fis = d.GetFiles();
                                    foreach (FileInfo f in fis)
                                    {
                                        try
                                        {
                                            int day = 0;
                                            string day_str = f.Name.Replace(f.Extension, "");
                                            if (int.TryParse(day_str, out day))
                                            {
                                                DateTime ftime = new DateTime(year, month, day);
                                                if (((TimeSpan)(now - ftime)).TotalDays > 15)
                                                {
                                                    f.Delete();
                                                }
                                            }
                                            else
                                            {
                                                f.Delete();
                                            }
                                        }
                                        catch
                                        { }
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    d.Delete();
                                }
                                catch
                                { }
                            }
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
        }

        public void Stop()
        {
            iswork = false;
            if (th != null)
                th.Abort();
        }
    }
}
