using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace WindowsServiceExample
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        public void FileWrite(string message)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "/Logs";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string textPath = AppDomain.CurrentDomain.BaseDirectory + "Logs/serviceLog.txt";
            if (!File.Exists(textPath))
            {
                using (StreamWriter sw = File.CreateText(textPath))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(textPath))
                {
                    sw.WriteLine(message);
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            FileWrite("service çalışmaya başladı: " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; //5 saniyede bir çalışacak
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            FileWrite("service durdu: " + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs elapsedEventHandler)
        {
            FileWrite("service çalışmaya devam ediyor: " + DateTime.Now);
        }
    }
}
