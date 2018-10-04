using Microsoft.SharePoint;
using System;
using System.Linq;
using System.Configuration;


namespace ICT.Publication.Tools
{
    class Program
    {
        static bool isDebugMode = bool.Parse(ConfigurationManager.AppSettings["isDebugMode"]);

        static void Main(string[] args)
        {
            string webUrl = ConfigurationManager.AppSettings["webUrl"];

            using (SPSite site = new SPSite(webUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    // Start with the Site Collection of the given web url
                    ProcessWeb(site.Url);
                }
            }
        }

        private static void ProcessWeb(string webUrl)
        {
            using (SPSite site = new SPSite(webUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    CheckWeb2(web.Url);

                    // Check Subwebs
                    if (web.Webs.Count > 0)
                    {
                        foreach (SPWeb subWeb in web.Webs)
                        {
                            ProcessWeb(subWeb.Url);
                        }
                    }
                }
            }
        }

        private static void CheckWeb1(string webUrl)
        {
            const string METHOD_NAME = "CheckWeb";

            Console.WriteLine("{0} - starting...", METHOD_NAME);

            using (SPSite site = new SPSite(webUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {

                    Console.WriteLine("{0} - Checking site '{1}'...", METHOD_NAME, webUrl);

                    // ContentType for Publishing
                    SPContentType ctPublication = site.RootWeb.ContentTypes[Names.PUBLICATION_CONTENTTYPE_NAME];
                    // Check Field 'ICTPublication_IsPublishing' in the Root Web
                    SPField fieldIsPublishing = site.RootWeb.Fields.TryGetFieldByStaticName(Names.FIELDNAME_ISPUBLISHING);
                    // Check Field 'ICTPublication_LastPublished' in the Root Web
                    SPField fieldLastPublished = site.RootWeb.Fields.TryGetFieldByStaticName(Names.FIELDNAME_LASTPUBLISHED);
                    // Check Field 'ICTPublication_LastPublishedStatus' in the Root Web
                    SPField fieldLastPublishedStatus = site.RootWeb.Fields.TryGetFieldByStaticName(Names.FIELDNAME_LASTPUBLISHEDSTATUS);

                    if (fieldIsPublishing != null &&
                        fieldLastPublished != null &&
                        fieldLastPublishedStatus != null &&
                        ctPublication != null)
                    {
                        // Check all Lists in the Web
                        for (int i = 0; i < web.Lists.Count; i++)
                        {
                            SPList list = web.Lists[i];

                            Console.WriteLine("Processing List '{0}'...", list.Title);

                            // Filter for Document Libraries
                            if (list.BaseType == SPBaseType.DocumentLibrary &&
                                list.BaseTemplate == SPListTemplateType.DocumentLibrary &&
                                !SPTools.GetBlackList(site.Url).Any(list.Title.Contains))
                            {
                                Console.WriteLine("List '{0}' is Document Library and List is not on Blacklist.", list.Title);

                                // Add the ContentType 'ICTPublicationDocument' to the List
                                // if not alredy done
                                if (list.ContentTypes[Names.PUBLICATION_CONTENTTYPE_NAME] != null)
                                {
                                    Console.WriteLine("{0} - ContentType '{1}' is already assigned to list '{2}'.",
                                        METHOD_NAME,
                                        ctPublication.Name,
                                        list.Title);

                                    if (list.ContentTypes[Names.PUBLICATION_CONTENTTYPE_NAME].Hidden)
                                    {
                                        Console.WriteLine("{0} - ContentType '{1}' is hidden to list '{2}' -> nothing to do.",
                                            METHOD_NAME,
                                            ctPublication.Name,
                                            list.Title);
                                    }
                                    else
                                    {
                                        Console.WriteLine("{0} - Set contenttype '{1}' hidden to list '{2}'.",
                                            METHOD_NAME,
                                            ctPublication.Name,
                                            list.Title);

                                        list.ContentTypes[Names.PUBLICATION_CONTENTTYPE_NAME].Hidden = true;
                                        list.ContentTypes[Names.PUBLICATION_CONTENTTYPE_NAME].Update();
                                        list.Update();
                                    }
                                }
                                else
                                {
                                    ULSLogger.LoggingService.LogErrorInULS(
                                        string.Format("{0} - Adding ContentType '{1}' to List '{2}'...",
                                            METHOD_NAME,
                                            ctPublication.Name,
                                            list.Title),
                                        Names.TRACE_SERVERITY_NORMAL
                                    );

                                    try
                                    {
                                        web.AllowUnsafeUpdates = true;
                                        list.ContentTypesEnabled = true;
                                        list.ContentTypes.Add(ctPublication);
                                        list.ContentTypes[Names.PUBLICATION_CONTENTTYPE_NAME].Hidden = true;
                                        list.Update();

                                        ULSLogger.LoggingService.LogErrorInULS(
                                            string.Format("{0} - Added hidden ContentType '{1}' to List '{2}' sucessfully.",
                                                METHOD_NAME,
                                                ctPublication.Name,
                                                list.Title),
                                            Names.TRACE_SERVERITY_NORMAL
                                        );
                                    }
                                    catch (Exception ex)
                                    {
                                        ULSLogger.LoggingService.LogErrorInULS(
                                            string.Format("{0} - Added hidden ContentType '{1}' to List '{2}' failed!\r\n ERROR: '{3}'",
                                                METHOD_NAME,
                                                ctPublication.Name,
                                                list.Title,
                                                ex.Message),
                                            Names.TRACE_SERVERITY_ERROR,
                                            ex
                                        );

                                        throw ex;
                                    }
                                    finally
                                    {
                                        web.AllowUnsafeUpdates = false;
                                    }
                                }

                                // Set the List Property for Publication
                                SPTools.PrepareList(list);
                            }
                            else if (SPTools.GetBlackList(site.Url).Any(list.Title.Contains))
                            {
                                ULSLogger.LoggingService.LogErrorInULS(
                                    string.Format("List '{0}' is on Blacklist.", list.Title),
                                    Names.TRACE_SERVERITY_NORMAL
                                );
                            }
                        }
                    }
                    else
                    {
                        ULSLogger.LoggingService.LogErrorInULS(
                            string.Format("{0} - Fields for Publishing Feature could not be found in Site Collection.",
                                METHOD_NAME,
                                Names.VERSIONHISTORY_FEATURE_TITLE),
                            Names.TRACE_SERVERITY_NORMAL
                        );
                    }
                }
            }

            // Field does not exist
            ULSLogger.LoggingService.LogErrorInULS(
                string.Format("{0} - finished.",
                    METHOD_NAME),
                Names.TRACE_SERVERITY_NORMAL
            );
        }

        private static void CheckWeb2(string webUrl)
        {
            const string METHOD_NAME = "Program.cs - CheckWeb2()";

            Console.WriteLine("{0} - starting...",
                    METHOD_NAME);

            using (SPSite site = new SPSite(webUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    // Check all lists in the web
                    for (int i = 0; i < web.Lists.Count; i++)
                    {
                        SPList list = web.Lists[i];

                        Console.WriteLine("{0} - Processing List '{1}' with Url '{2}'...",
                            METHOD_NAME,
                            list.Title,
                            list.RootFolder.ServerRelativeUrl);

                        // Filter for document libraries
                        if (list.BaseType == SPBaseType.DocumentLibrary &&
                            list.BaseTemplate == SPListTemplateType.DocumentLibrary &&
                            !SPTools.GetBlackList(web.Site.Url).Any(list.Title.Contains))
                        {
                            Console.WriteLine("List '{0}' is Document Library and List is not on Blacklist.", list.Title);

                            foreach (string CTName in Names.CONTENTTYPES_TO_ADD)
                            {
                                SPContentType ct = web.Site.RootWeb.AvailableContentTypes[CTName];
                                if (ct != null)
                                {
                                    // Make sure the list accepts content types
                                    list.ContentTypesEnabled = true;

                                    Console.WriteLine("{0} - Try to add content type '{1}' to list '{2}'...",
                                            METHOD_NAME,
                                            ct.Name,
                                            list.Title);

                                    // Add the content type to the list.
                                    if (!list.IsContentTypeAllowed(ct))
                                    {
                                        // Content type is not allowed on the list
                                        Console.WriteLine("{0} - Content type '{1}' is not allowed on the list '{2}'.",
                                                METHOD_NAME,
                                                ct.Name,
                                                list.Title);
                                    }
                                    else if (list.ContentTypes[ct.Name] != null)
                                    {
                                        // The content type is already in use on the list
                                        Console.WriteLine("{0} - Content type '{1}' is already assigned to list '{2}'.",
                                                METHOD_NAME,
                                                ct.Name,
                                                list.Title);
                                    }
                                    else
                                    {
                                        if (!isDebugMode)
                                        {
                                            // add the content type
                                            try
                                            {
                                                list.ContentTypes.Add(ct);

                                                Console.WriteLine("{0} - Added content type '{1}' to list '{2}' sucessfully.",
                                                        METHOD_NAME,
                                                        ct.Name,
                                                        list.Title);
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("{0} - Adding content type '{1}' to list '{2}' failed!\r\n ERROR: '{3}'",
                                                        METHOD_NAME,
                                                        ct.Name,
                                                        list.Title,
                                                        ex.Message);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("{0} - WE ARE IN DEBUG_MODE. No changes are made to list '{1}'.",
                                                    METHOD_NAME,
                                                    list.Title);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("{0} - Content type '{1}' is not available on site '{2}'.",
                                            METHOD_NAME,
                                            CTName,
                                            web.Site.RootWeb.Url);
                                }
                            }

                            if (!isDebugMode)
                            {
                                // Change the order of the content types for the list
                                SPTools.ChangeCTOrder(list);
                            }
                        }
                        else if (SPTools.GetBlackList(web.Site.Url).Any(list.Title.Contains))
                        {
                            Console.WriteLine("{0} - List '{1}' is on blacklist.",
                                    METHOD_NAME,
                                    list.Title);
                        }
                    }
                }
            }

            Console.WriteLine("{0} - finished.",
                    METHOD_NAME);
        }
    }
}
