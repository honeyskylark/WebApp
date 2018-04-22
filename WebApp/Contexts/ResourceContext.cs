using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Contexts
{
    public class ResourceContext
    {
        private static readonly ResourceContext instance = new ResourceContext();

        public Dictionary<string,string> Resources { get; set; }

        private ResourceContext()
        { }

        public static ResourceContext GetInstance()
        {
            return instance;
        }
    }
}
