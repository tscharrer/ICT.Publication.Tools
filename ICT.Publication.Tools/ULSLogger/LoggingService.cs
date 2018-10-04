using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;

namespace ICT.Publication.Tools.ULSLogger
{
    public class LoggingService : SPDiagnosticsServiceBase
    {
        public static string vsDiagnosticAreaName = "ICT Puvlication Tools";

        public static string CategoryName = "ICT.Publication.Tools";

        public static uint uintEventID = 7903; // Event ID

        private static LoggingService _Current;

        public static LoggingService Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new LoggingService();
                }
                return _Current;
            }
        }

        private LoggingService()
            : base("ICT.Publication.Tools", SPFarm.Local)
        { }

        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            List<SPDiagnosticsArea> areas = new List<SPDiagnosticsArea>
            {
                new SPDiagnosticsArea(vsDiagnosticAreaName, new List<SPDiagnosticsCategory>
                {
                    new SPDiagnosticsCategory(CategoryName, TraceSeverity.Medium, EventSeverity.Error)
                })
            };
            return areas;
        }

        public static string LogErrorInULS(string errorMessage)
        {
            string strExecutionResult = "Message Not Logged in ULS. ";
            try
            {
                SPDiagnosticsCategory category = LoggingService.Current.Areas[vsDiagnosticAreaName].Categories[CategoryName];
                LoggingService.Current.WriteTrace(uintEventID, category, TraceSeverity.Unexpected, errorMessage);
                strExecutionResult = "Message Logged";
            }
            catch (Exception ex)
            {
                strExecutionResult += ex.Message;
            }
            return strExecutionResult;
        }

        public static string LogErrorInULS(string errorMessage, TraceSeverity tsSeverity)
        {
            string strExecutionResult = "Message Not Logged in ULS. ";
            try
            {
                SPDiagnosticsCategory category = LoggingService.Current.Areas[vsDiagnosticAreaName].Categories[CategoryName];
                LoggingService.Current.WriteTrace(uintEventID, category, tsSeverity, errorMessage);
                strExecutionResult = "Message Logged";
            }
            catch (Exception ex)
            {
                strExecutionResult += ex.Message;
            }
            return strExecutionResult;
        }

        public static string LogErrorInULS(string errorMessage, TraceSeverity tsSeverity, Exception errorExecption)
        {
            string strExecutionResult = "Message Not Logged in ULS. ";
            try
            {
                SPDiagnosticsCategory category = LoggingService.Current.Areas[vsDiagnosticAreaName].Categories[CategoryName];
                LoggingService.Current.WriteTrace(uintEventID, category, tsSeverity, errorMessage + "Exception: " + errorExecption.Message);
                strExecutionResult = "Message Logged";
            }
            catch (Exception ex)
            {
                strExecutionResult += ex.Message;
            }
            return strExecutionResult;
        }
    }
}
