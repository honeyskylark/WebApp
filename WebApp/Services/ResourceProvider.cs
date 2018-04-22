using System;
using WebApp.Constants;
using WebApp.Contexts;

namespace WebApp.Services
{
    public static class ResourceProvider
    {
        public static string GetResource(string key)
        {          
            if (key != String.Empty)
            {
                var instance = ResourceContext.GetInstance();

                string value = String.Empty;
                instance.Resources.TryGetValue(key, out value);
                if (value == null || value == String.Empty)
                {
                    return $"[{key}]";
                }
                return value;
            }
            return "Key is empty";
        }
    }
}
