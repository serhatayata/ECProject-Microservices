using Core.CrossCuttingConcerns.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class LogExtensions
    {
        public static LogDetail GetLogDetails(MethodBase method,byte risk,string LoggingTime,string explanation)
        {
            var logParameters = new List<LogParameter>();

            for (var i = 0; i < method?.GetParameters().Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = method.GetParameters()[i].Name
                });
            }

            LogDetail logDetail = new()
            {
                MethodName = method?.Name,
                LoggingTime = LoggingTime,
                LogParameters = logParameters,
                Risk = risk,
                Explanation = explanation
            };

            return logDetail;
        }
    }
}
