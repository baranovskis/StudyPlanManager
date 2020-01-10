using log4net;
using System;
using System.IO;
using System.Reflection;
using System.Web.Http.ExceptionHandling;

namespace StudyPlanManager.Logic
{
    public class ExceptionManager : ExceptionLogger
    {
        private ILog _logger = null;

        public ExceptionManager()
        {
            var log4NetConfigDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var log4NetConfigFilePath = Path.Combine(log4NetConfigDirectory, "log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigFilePath));
        }

        public override void Log(ExceptionLoggerContext context)
        {
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _logger.Error(context.Exception.ToString() + Environment.NewLine);
        }

        public void Log(string ex)
        {
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _logger.Error(ex);
        }
    }
}
