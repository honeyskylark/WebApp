using System;
using WebApp.Areas.Administration.Constants;

namespace WebApp.Areas.Administration.Services
{
    public static class ResourceProvider
    {
        public static string GetResource(string key)
        {          
            if (key != String.Empty)
            {
                string value = "";
                ViewConstants.Resources.TryGetValue(key, out value);
                return value;
            }
            return "Key Not Found";
        }
    }
}
