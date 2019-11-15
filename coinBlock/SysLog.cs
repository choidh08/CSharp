using System;
using NLog;

namespace coinBlock
{
    internal static class SysLog
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Debug(string msg)
        {
            _logger.Debug(msg);
        }

        public static void Debug(string msg, object arg)
        {
            _logger.Debug(msg, arg);
        }

        public static void Debug(string msg, params object[] args)
        {
            _logger.Debug(msg, args);
        }

        public static void Debug(IFormatProvider formatProvider, string msg, object arg)
        {
            _logger.Debug(formatProvider, msg, arg);
        }

        public static void Debug(IFormatProvider formatProvider, string msg, params object[] args)
        {
            _logger.Debug(formatProvider, msg, args);
        }

        public static void Error(string msg)
        {
            _logger.Error(msg);
        }

        public static void Error(string msg, object arg)
        {
            _logger.Error(msg, arg);
        }

        public static void Error(string msg, params object[] args)
        {
            _logger.Error(msg, args);
        }

        public static void Error(IFormatProvider formatProvider, string msg, object arg)
        {
            _logger.Error(formatProvider, msg, arg);
        }

        public static void Error(IFormatProvider formatProvider, string msg, params object[] args)
        {
            _logger.Error(formatProvider, msg, args);
        }

        public static void Fatal(string msg)
        {
            _logger.Fatal(msg);
        }

        public static void Fatal(string msg, object arg)
        {
            _logger.Fatal(msg, arg);
        }

        public static void Fatal(string msg, params object[] args)
        {
            _logger.Fatal(msg, args);
        }

        public static void Fatal(IFormatProvider formatProvider, string msg, object arg)
        {
            _logger.Fatal(formatProvider, msg, arg);
        }

        public static void Fatal(IFormatProvider formatProvider, string msg, params object[] args)
        {
            _logger.Fatal(formatProvider, msg, args);
        }

        
        public static void Info(string msg)
        {
            _logger.Info(msg);
        }

        public static void Info(string msg, object arg)
        {
            _logger.Info(msg, arg);
        }

        public static void Info(string msg, params object[] args)
        {
            _logger.Info(msg, args);
        }

        public static void Info(IFormatProvider formatProvider, string msg, object arg)
        {
            _logger.Info(formatProvider, msg, arg);
        }

        public static void Info(IFormatProvider formatProvider, string msg, params object[] args)
        {
            _logger.Info(formatProvider, msg, args);
        }

        public static void Trace(string msg)
        {
            _logger.Trace(msg);
        }

        public static void Trace(string msg, object arg)
        {
            _logger.Trace(msg, arg);
        }

        public static void Trace(string msg, params object[] args)
        {
            _logger.Trace(msg, args);
        }

        public static void Trace(IFormatProvider formatProvider, string msg, object arg)
        {
            _logger.Trace(formatProvider, msg, arg);
        }

        public static void Trace(IFormatProvider formatProvider, string msg, params object[] args)
        {
            _logger.Trace(formatProvider, msg, args);
        }
                
        public static void Warn(string msg)
        {
            _logger.Warn(msg);
        }

        public static void Warn(string msg, object arg)
        {
            _logger.Warn(msg, arg);
        }

        public static void Warn(string msg, params object[] args)
        {
            _logger.Warn(msg, args);
        }

        public static void Warn(IFormatProvider formatProvider, string msg, object arg)
        {
            _logger.Warn(formatProvider, msg, arg);
        }

        public static void Warn(IFormatProvider formatProvider, string msg, params object[] args)
        {
            _logger.Warn(formatProvider, msg, args);
        }
    }
}