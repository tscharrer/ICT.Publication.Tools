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
        /// The ContentType Name for Libraries supporting Publication
        /// </summary>
        public static string PUBLICATION_CONTENTTYPE_GROUPNAME = "ICTPublication";

        /// <summary>
        /// The Static Fieldname of ICTPublication_IsPublishing
        /// </summary>
        public static string FIELDNAME_ISPUBLISHING = "ICTPublication_IsPublishing";

        /// <summary>
        /// The Display Name of ICTPublication_IsPublishing
        /// </summary>
        public static string FIELDNAME_ISPUBLISHING_DISPLAYNAME = "ICTPublication_IsPublishing";

        /// <summary>
        /// The Static Fieldname of ICTPublication_LastPublished
        /// </summary>
        public static string FIELDNAME_LASTPUBLISHED = "ICTPublication_LastPublished";

        /// <summary>
        /// The Display Name of ICTPublication_LastPublished
        /// </summary>
        public static string FIELDNAME_LASTPUBLISHED_DISPLAYNAME = "ICTPublication_LastPublished";

        /// <summary>
        /// The Static Fieldname of ICTPublication_LastPublishedStatus
        /// </summary>
        public static string FIELDNAME_LASTPUBLISHEDSTATUS = "ICTPublication_LastPublishedStatus";

        /// <summary>
        /// The Display Name of ICTPublication_LastPublishedStatus
        /// </summary>
        public static string FIELDNAME_LASTPUBLISHEDSTATUS_DISPLAYNAME = "ICTPublication_LastPublishedStatus";



        /// <summary>
        /// The Static Fieldname of ICTPublication_LastPublishedHistory
        /// </summary>
        public static string FIELDNAME_LASTPUBLISHEDHISTORY = "ICTPublication_LastPublishedHistory";

        /// <summary>
        /// The Display Name of ICTPublication_LastPublishedHistory
        /// </summary>
        public static string FIELDNAME_LASTPUBLISHEDHISTORY_DISPLAYNAME = "ICTPublication_LastPublishedHistory";

        /// <summary>
        /// The Groupname where new Columns for Publication 2.0 are grouped under
        /// </summary>
        public static string COLUMNS_GROUP_NAME = "ICTPublication";

        /// <summary>
        /// The Title of the Feature
        /// </summary>
        public static string VERSIONHISTORY_FEATURE_TITLE = "ICT Publication Infrastructure";

        /// <summary>
        /// The Name of the SharePoint Service Application 'Word Automation Service'
        /// </summary>
        public static string WORD_AUTOMATION_SERVICE_NAME = "Word Automation Services";

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
        /// ToDo: Add Summary
        /// </summary>
        public static string PROPBAG_LIST_PUBLICATION_ISRUNNUNG_KEY = "ICTPublicationIsRunning";

        /// <summary>
        /// The destination List where the Publication is synchronized to  
        /// </summary>
        public static string PROPBAG_LIST_PUBLICATION_DEST = "ICTPublicationDestList";

        /// <summary>
        /// The number of elements which will be published  
        /// </summary>
        public static string PROPBAG_LIST_PUBLICATION_ELEMENTCOUNT = "ICTPublicationElementCount";

        /// <summary>
        /// The flag which indicates the PDF-Files should be generated  
        /// </summary>
        public static string PROPBAG_LIST_PUBLICATION_CREATEPDF = "ICTPublicationCreatePDF";

        /// <summary>
        /// The flag which indicates that the original World should also be copied parallel to the PDF-File  
        /// </summary>
        public static string PROPBAG_LIST_PUBLICATION_COPYADITIONALWORD = "ICTPublicationCopyAditionalWord";

        /// <summary>
        /// The name of the new Folder for the Destination Library  
        /// </summary>
        public static string PROPBAG_LIST_PUBLICATION_NEWFOLDERNAME = "ICTPublicationNewFolderName";

        /// <summary>
        /// The Loginname of the Administrator Account  
        /// </summary>
        public static string PROPBAG_SITE_ADMIN_LOGINNAME = "ICTPublicationAdminLoginName";

        /// <summary>
        /// ToDo: Add Summary
        /// </summary>
        public static string MESSAGE_LIBRARY_NOT_CONFIGURED = "Bibliothek ist nicht für die Publikation 2.0 konfiguriert!";

        /// <summary>
        /// ToDo: Add Summary
        /// </summary>
        public static string MESSAGE_LIBRARY_NOT_FOUND = "Bibliothek mit der Id '{0}' konnte nicht in der Site '{1}' gefunden werden!'";

        /// <summary>
        /// ToDo: Add Summary
        /// </summary>
        public static string MESSAGE_LIBRARYID_COULDNOT_PARSED = "Die angegebene Id '{0}' kann nicht als korrekte SharePoint Id erkannt werden!";

        /// <summary>
        /// The Message which will be uses by automatic CheckIn-Process
        /// </summary>
        public static string AUTOMATIC_CHECKIN_MESSAGE = "Automatisch publiziert mit ICT Publication";

        /// <summary>
        /// The Message which will be shown to the User if the Foldername
        /// is not valid
        /// </summary>
        public static string MESSAGE_FOLDERNAME_NOT_VALID = "Der Ordner zum veröffentlichen der ausgewählten Strukur enthält nicht gültige Zeichen bzw.kann nicht verwendet werden!";

        /// <summary>
        /// ToDo: Add SUmmary
        /// </summary>
        public static string WORD_AUTOMATION_SERVICE = "Word Automation Services";

        /// <summary>
        /// File Extensions which supports PDF Creation
        /// </summary>
        public static string[] PDFCREATION_SUPPORT_EXTENSION = { ".doc", ".docx" };

        /// <summary>
        /// Admin User (User sees Administration Button)
        /// </summary>
        public static string ADMIN_USERNAME = @"az1\\tscharrer";

        /// <summary>
        ///  TraceSeverity for Debug-Logging
        /// </summary>
        public static TraceSeverity TRACE_SERVERITY_NORMAL = TraceSeverity.Monitorable;

        /// <summary>
        /// TraceSeverity for Error-Logging
        /// </summary>
        public static TraceSeverity TRACE_SERVERITY_ERROR = TraceSeverity.High;

        /// <summary>
        /// ToDo: Add Summary
        /// </summary>
        public static Regex ILLEGAL_PATH_CHARS = new Regex(@"^\.|[\x00-\x1F,\x7B-\x9F,"",#,%,&,*,:,<,>,?,\\]+|(\.\.)+|\.$", RegexOptions.Compiled);
    }
}
