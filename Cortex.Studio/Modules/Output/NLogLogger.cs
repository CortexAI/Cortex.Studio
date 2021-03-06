﻿using System;
using Caliburn.Micro;

namespace Cortex.Studio.Modules.Output
{
    class NLogLogger : ILog
    {
        private readonly NLog.Logger _innerLogger;

        public NLogLogger(Type type)
        {
            _innerLogger = NLog.LogManager.GetLogger(type.FullName);
        }

        public void Info(string format, params object[] args)
        {
            _innerLogger.Info(format, args);
        }

        public void Warn(string format, params object[] args)
        {
            _innerLogger.Warn(format, args);
        }

        public void Error(Exception exception)
        {
            _innerLogger.Error(exception, exception.Message, exception.Data);
        }
    }
}
