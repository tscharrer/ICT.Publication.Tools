using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ICT.Publication.Tools
{
    public static class SPTools
    {
        /// <summary>
        /// Get the Blacklist Items from PropertyBag as String Array
        /// </summary>
        /// <param name="siteUrl">The Url of the Site Collection</param>
        /// <returns></returns>
        public static string[] GetBlackList(string siteUrl)
        {
            using (SPSite site = new SPSite(siteUrl))
            {
                SPPropertyBag props = site.RootWeb.Properties;

                // Property is already there
                if (props.ContainsKey(Names.PROPBAG_BLACKLIST_KEY))
                {
                    return props[Names.PROPBAG_BLACKLIST_KEY].Split(';');
                }
                // Property is not set -> set it from default Value
                else
                {
                    props.Add(Names.PROPBAG_BLACKLIST_KEY, string.Join(";", Names.BLACKLIST_DOCUMENTLIBRARIES));
                    props.Update();

                    return Names.BLACKLIST_DOCUMENTLIBRARIES;
                }
            }
        }

        /// <summary>
        /// ToDo: Add SUmmary
        /// </summary>
        /// <param name="list"></param>
        /// <param name="propKey"></param>
        /// <param name="propValue"></param>
        public static void SetListProperty(SPList list, string propKey, object propValue)
        {
            const string METHOD_NAME = "SPTools.cs SetListProperty()";

            Console.WriteLine("{0} - starting...", METHOD_NAME);

            try
            {
                // Get List Properties
                Hashtable listProps = list.RootFolder.Properties;

                // Property is there
                if (listProps.ContainsKey(propKey))
                {
                    listProps[propKey] = propValue;
                }
                // Property is not there -> create it
                else
                {
                    Console.WriteLine("{0} - Adding List Property '{1}' to List '{2}'...",
                            METHOD_NAME,
                            propKey,
                            list.Title);

                    listProps.Add(propKey, propValue);
                }

                list.ParentWeb.AllowUnsafeUpdates = true;
                list.Update();

                Console.WriteLine("{0} - Added List Property '{1}' to List '{2}' successfully.",
                        METHOD_NAME,
                        propKey,
                        list.Title);

                Console.WriteLine("{0} - finished.", METHOD_NAME);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error while adding List Property!"), ex);
            }
            finally
            {
                list.ParentWeb.AllowUnsafeUpdates = false;
            }
        }

        /// <summary>
        /// ToDo: Add SUmmary
        /// </summary>
        /// <param name="list"></param>
        /// <param name="propKey"></param>
        /// <returns></returns>
        public static object GetListProperty(SPList list, string propKey)
        {
            try
            {
                if (list != null)
                {
                    // Get List Properties
                    var propValue = list.RootFolder.Properties[propKey];

                    return propValue;
                }
                else
                {
                    // List with ID could not be found
                    throw new Exception("List not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error while getting List Property!"), ex);
            }
        }

        /// <summary>
        /// Prepares a given List and set the Property for Publication
        /// if it was not already done. 
        /// </summary>
        /// <param name="list">The SharePoint List</param>
        public static void PrepareList(SPList list)
        {
            const string METHOD_NAME = "SPTools.cs PrepareList()";

            Console.WriteLine("{0} - starting...", METHOD_NAME);

            try
            {
                // Check if property is set to list
                if (SPTools.GetListProperty(list, Names.PROPBAG_LIST_PUBLICATION_ENABLED_KEY) == null)
                {
                    Console.WriteLine("{0} - Listproperty '{1}' is not assigned to list '{2}' -> assign it.",
                        METHOD_NAME,
                        Names.PROPBAG_LIST_PUBLICATION_ENABLED_KEY,
                        list.Title);

                    SetListProperty(list, Names.PROPBAG_LIST_PUBLICATION_ENABLED_KEY, Names.PROPBAG_LIST_PUBLICATION_ENABLED_DEFAULTVALUE);
                }
                else
                {
                    Console.WriteLine("{0} - Listproperty '{1}' is allredy assigned to list '{2}'.",
                        METHOD_NAME,
                        Names.PROPBAG_LIST_PUBLICATION_ENABLED_KEY,
                        list.Title);
                }

                Console.WriteLine("{0} - finished.",
                        METHOD_NAME);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} - ERROR! occured when preparing list '{1}'{2}:{3}",
                    METHOD_NAME,
                    list.Title,
                    Environment.NewLine,
                    ex.Message);
            }
        }


        /// <summary>
        /// Sets the order of the content types for the given list
        /// At the moment the order is static and should be
        /// in future develoment dynamic
        /// </summary>
        /// <param name="list">The SharePoint list to set the order of content types</param>
        public static void ChangeCTOrder(SPList list)
        {
            const string METHOD_NAME = "AddContentTypesEventReceiver - ChangeCTOrder()";

            Console.WriteLine("{0} - starting...", METHOD_NAME);

            try
            {
                List<string> contentTypeNamesToAdd = Names.CONTENTTYPES_TO_ADD.ToList<string>();
                List<string> currentContentTypeNames = new List<string>();

                foreach (SPContentType ct in list.ContentTypes)
                {
                    currentContentTypeNames.Add(ct.Name);
                }

                if (!contentTypeNamesToAdd.Except(currentContentTypeNames).Any())
                {
                    // list have all relevant content types
                    Console.WriteLine("{0} - List '{1}' have all relevant content types -> set the custom order.",
                        METHOD_NAME,
                        list.ParentWebUrl);

                    IList<SPContentType> contentTypesToAdd = new List<SPContentType>();
                    foreach (string ctName in contentTypeNamesToAdd)
                    {
                        contentTypesToAdd.Add(list.ContentTypes[ctName]);
                    }

                    list.RootFolder.UniqueContentTypeOrder = contentTypesToAdd;
                    list.RootFolder.Update();
                }
                else
                {
                    Console.WriteLine("{0} - List '{1}' does not contain all relevant content types. Please check that the list contains the following content types: {2}",
                        METHOD_NAME,
                        list.ParentWebUrl,
                        String.Join(";", contentTypeNamesToAdd));
                }

                Console.WriteLine("{0} - finished.",
                        METHOD_NAME);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} - Error while changing the position of the content types for list '{1}' on site '{2}'!\r\n ERROR: '{3}'",
                        METHOD_NAME,
                        list.Title,
                        list.ParentWebUrl,
                        ex.Message);
            }
        }
    }
}
