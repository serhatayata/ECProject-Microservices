﻿using System.Collections.Generic;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string MethodName { get; set; }
        public string Explanation { get; set; }
        public byte Risk { get; set; }
        public List<LogParameter> LogParameters { get; set; }
        public string LoggingTime { get; set; }
    }

    public enum LogDetailRisks
    {
        NotRisky=1,
        Normal=2,
        Critical=3
    }
}
