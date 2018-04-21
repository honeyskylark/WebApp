using System;
using WebApp.Constants;

namespace WebApp.Services
{
    public static class ResourceProvider
    {
        public static string GetResource(string key)
        {          
            if (key != String.Empty)
            {
                string value = String.Empty;
                ViewConstants.Resources.TryGetValue(key, out value);
                if (value == null || value == String.Empty)
                {
                    return "Key not found";
                }
                return value;
            }
            return "Key is empty";
        }
    }
}
