using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism;

namespace PrismAppDemo
{
    public delegate void LogHandle(string flag, string content, string source = null);

    public interface ILog
    {
        void Write(string tag, string content, string source = null);
    }

    public class Log : ILog
    {
        private int count = 0;
        private LogHandle LogHandle;

        public void SetLog(LogHandle handle)
        { 
            LogHandle = handle;
        }

        public void Write(string tag, string content, string source = null)
        {
            if (LogHandle == null)
            {
                MessageBox.Show(content + $" {++count}", tag, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            LogHandle(tag, content, source);
        }
    }
}
