using Microsoft.SharePoint.Administration;
using System.Text.RegularExpressions;


namespace ICT.Publication.Tools
{
    public class Names
    {
        /// <summary>
        /// The list of content types to add to all relevant libraries
        /// </summary>
        public static string[] CONTENTTYPES_TO_ADD = { "MC Standard DE", "MC Standard EN", "MC Dokumentation DE", "MC Documentation EN", "MC Meeting Protokoll DE", "MC Presentation" };

        /// <summary>
        /// The ContentType Name for Libraries supporting Publication
        /// </summary>
        public static string PUBLICATION_CONTENTTYPE_NAME = "ICTPublicationDocument";

        /// <summary>
        /// The Static Fieldname of ICTPublication_IsPublishing
        /// </summary>
        public static string FIELDNAME_ISPUBLISHING = "ICTPublication_IsPublishing";

        /// <summary>
        /// The Static Fieldname of ICTPublication_LastPublished
        /// </summary>
        public static string FIELDNAME_LASTPUBLISHED = "ICTPublication_LastPublished";

        /// <summary>
        /// The Static Fieldname of ICTPublication_LastPublishedStatus
        /// </summary>
        public static string FIELDNAME_LASTPUBLISHEDSTATUS = "ICTPublication_LastPublishedStatus";

        /// <summary>
        /// The Title of the Feature
        /// </summary>
        public static string VERSIONHISTORY_FEATURE_TITLE = "ICT Publication Infrastructure";

        /// <summary>
        /// ToDo: Add Summary
        /// </summary>
        public static string PROPBAG_LIST_PUBLICATION_ENABLED_KEY = "ICTPublicationEnabled";

        /// <summary>
        /// ToDo: Add Summary
        /// </summary>
        public static bool PROPBAG_LIST_PUBLICATION_ENABLED_DEFAULTVALUE = false;

        /// <summary>
        /// ToDo: Add Summary
        /// </summary>
        public static string[] BLACKLIST_DOCUMENTLIBRARIES = { "Form Templates", "Site Assets", "Style Library", "Formatbibliothek", "Formularvorlagen", "Websiteobjekte" };

        /// <summary>
        /// ToDo: Add Summary
        /// </summary>
        public static string PROPBAG_BLACKLIST_KEY = "ICTPublicationBlackList";

        /// <summary>
        ///  TraceSeverity for Debug-Logging
        /// </summary>
        public static TraceSeverity TRACE_SERVERITY_NORMAL = TraceSeverity.Monitorable;

        /// <summary>
        /// TraceSeverity for Error-Logging
        /// </summary>
        public static TraceSeverity TRACE_SERVERITY_ERROR = TraceSeverity.High;
    }
}
